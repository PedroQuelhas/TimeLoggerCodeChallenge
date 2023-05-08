using AutoMapper;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerApi.CodeGen.Models;
using System;
using System.Threading.Tasks;
using Timelogger.Model;
using Timelogger.Repos;
using Timelogger.Services;
using FluentAssertions;


namespace Timelogger.Tests.Services
{
    [TestClass]
    public class CustomerServiceTests
    {

        [TestMethod]
        public async Task CreateCustomer_NewUser_Success()
        {
            var fakeRepo = A.Fake<ICustomerRepository>();
            var fakeMapper = A.Fake<IMapper>();

            var id = Guid.NewGuid();
            var customerDto = new CustomerDTO
            {
                Name = "TestName",
            };

            var customerDtoWithId = new CustomerDTO
            {
                Id = id,
                Name = "TestName",
            };

            var customer = new Customer
            {
                Name = "TestName",
            };

            var customerWithId = new Customer
            {
                ID = id,
                Name = "TestName",
            };

            A.CallTo(() => fakeMapper.Map<Customer>(customerDto)).Returns(customer);
            A.CallTo(() => fakeRepo.Create(customer)).Returns(customerWithId);
            A.CallTo(() => fakeMapper.Map<CustomerDTO>(customerWithId)).Returns(customerDtoWithId);

            var service = new CustomerService(fakeRepo, fakeMapper);
            var result = await service.Create(customerDto);

            A.CallTo(() => fakeMapper.Map<Customer>(customerDto)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeRepo.Create(customer)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeMapper.Map<CustomerDTO>(customerWithId)).MustHaveHappenedOnceExactly();

            customerDtoWithId.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void CreateCustomer_UserExists_Fail()
        {
            var fakeRepo = A.Fake<ICustomerRepository>();
            var fakeMapper = A.Fake<IMapper>();

            var id = Guid.NewGuid();
            var customerDto = new CustomerDTO
            {
                Name = "TestName",
            };

            var customer = new Customer
            {
                Name = "TestName",
            };

            var msg = "USER ALREADY EXISTS";
            A.CallTo(() => fakeMapper.Map<Customer>(customerDto)).Returns(customer);
            A.CallTo(() => fakeRepo.Create(customer)).ThrowsAsync(new DbUpdateException(msg));

            var service = new CustomerService(fakeRepo, fakeMapper);
            Assert.ThrowsExceptionAsync<DbUpdateException>(() => service.Create(customerDto), msg);

            A.CallTo(() => fakeMapper.Map<Customer>(customerDto)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeRepo.Create(customer)).MustHaveHappenedOnceExactly();
        }
    }
}
