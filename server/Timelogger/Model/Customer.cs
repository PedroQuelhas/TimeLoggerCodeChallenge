using AutoMapper;
using ServerApi.CodeGen.Models;
using System.Collections.Generic;

namespace Timelogger.Model
{
    public class Customer :BaseEntity
    {
        public string Name { get; set; }

        public List<Project> Projects { get; set; }
    }

    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDTO, Customer>();
            CreateMap<Customer, CustomerDTO>();
        }
    }
}
