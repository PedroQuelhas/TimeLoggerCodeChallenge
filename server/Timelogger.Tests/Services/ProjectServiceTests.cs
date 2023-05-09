using AutoMapper;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerApi.CodeGen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timelogger.Model;
using Timelogger.Repos;
using Timelogger.Services;
using FluentAssertions;

namespace Timelogger.Tests.Services
{
    [TestClass]
    public class ProjectServiceTests
    {
        [TestMethod]
        public async Task GetProjectsOverview_AllProjectsNoFiltersNoSorting_Success()
        {
            var fakeRepo = A.Fake<IProjectRepository>();
            var fakeRimeslotRepo = A.Fake<ITimeslotRepository>();
            var fakeMapper = A.Fake<IMapper>();

            var projects = GetProjects();
            var results = GetProjectResults();

            A.CallTo(() => fakeRepo.GetAllProjectsExpanded(null, null, null, null, "ID", SortOrder.DESC)).Returns((projects, 2));
            
            var paginationDTO = new PaginationDTO
            {
                Page = 0,
                PerPage = 0,
                TotalRecords = 2
            };

            var service = new ProjectService(fakeRepo, fakeRimeslotRepo, fakeMapper);
            var result = await service.GetProjectsOverview(null, null, null, null, null, null);
            result.pagination.Should().BeEquivalentTo(paginationDTO);
            result.data.ToList().Should().BeEquivalentTo(results);
        }

        [TestMethod]
        public async Task GetProjectsOverview_OnePageProjectsNoFiltersNoSorting_Success()
        {
            var fakeRepo = A.Fake<IProjectRepository>();
            var fakeRimeslotRepo = A.Fake<ITimeslotRepository>();
            var fakeMapper = A.Fake<IMapper>();

            var projects = GetProjects().Skip(1);
            var results = GetProjectResults().Skip(1);

            A.CallTo(() => fakeRepo.GetAllProjectsExpanded(1, 1, null, null, "ID", SortOrder.DESC)).Returns((projects, 1));

            var paginationDTO = new PaginationDTO
            {
                Page = 1,
                PerPage = 1,
                TotalRecords = 1
            };

            var service = new ProjectService(fakeRepo, fakeRimeslotRepo, fakeMapper);
            var result = await service.GetProjectsOverview(1, 1, null, null, null, null);
            result.pagination.Should().BeEquivalentTo(paginationDTO);
            result.data.ToList().Should().BeEquivalentTo(results);
        }


        private List<ProjectReportDTO> GetProjectResults() => new List<ProjectReportDTO>
        {
            new ProjectReportDTO
            {
                ProjectId=Guid.Parse("bfa1cd55-3d06-4327-b7bb-5925bba4017e"),
                ProjectName="Test1",
                Completed = false,
                Deadline ="08/01/2023 00:00:00 +00:00",
                EndDate = "06/01/2023 00:00:00 +00:00",
                StartDate = "01/01/2023 00:00:00 +00:00",
                CustomerName="customer1",
                TotalRecords=2,
                TotalTime=90
            },
             new ProjectReportDTO
            {
                ProjectId=Guid.Parse("9135c6f7-4339-4742-a2ae-20acebfb3458"),
                ProjectName="Test2",
                Completed = true,
                Deadline = "07/01/2023 00:00:00 +00:00",
                EndDate = "12/01/2023 00:00:00 +00:00",
                StartDate = "02/01/2023 00:00:00 +00:00",
                CustomerName="customer2",
                TotalRecords=3,
                TotalTime=270
            },
        };


        private List<Project> GetProjects() => new List<Project>
        {
            new Project
            {
                ID = Guid.Parse("bfa1cd55-3d06-4327-b7bb-5925bba4017e"),
                StartDate = DateTimeOffset.Parse("01-01-2023"),
                EndDate = DateTimeOffset.Parse("01-01-2023").AddDays(5),
                CustomerId = Guid.NewGuid(),
                Completed = false,
                Deadline = DateTimeOffset.Parse("01-01-2023").AddDays(7),
                Name = "Test1",
                Timeslots = new List<Timeslot>
                {
                    new Timeslot
                    {
                          ID = Guid.NewGuid(),
                        Duration = TimeSpan.FromMinutes(30),
                        ProjectId = Guid.NewGuid(),
                        StartTime = DateTimeOffset.Now,
                    },
                      new Timeslot
                    {
                         ID = Guid.NewGuid(),
                        Duration = TimeSpan.FromMinutes(60),
                        ProjectId = Guid.NewGuid(),
                        StartTime = DateTimeOffset.Now,
                    },
                },
                Customer = new Customer
                {
                    ID = Guid.Parse("78d491af-5c2c-4faa-9cc6-c232e56165d0"),
                    Name="customer1"
                }
            },
            new Project
            {
                ID = Guid.Parse("9135c6f7-4339-4742-a2ae-20acebfb3458"),
                StartDate = DateTimeOffset.Parse("02-01-2023"),
                EndDate = DateTimeOffset.Parse("02-01-2023").AddDays(10),
                CustomerId = Guid.NewGuid(),
                Completed = true,
                Deadline = DateTimeOffset.Parse("02-01-2023").AddDays(5),
                Name = "Test2",
                Timeslots = new List<Timeslot>
                {
                    new Timeslot
                    {
                        ID = Guid.NewGuid(),
                        Duration = TimeSpan.FromMinutes(60),
                        ProjectId = Guid.NewGuid(),
                        StartTime = DateTimeOffset.Now,
                    },
                      new Timeslot
                    {
                        ID = Guid.NewGuid(),
                        Duration = TimeSpan.FromMinutes(90),
                        ProjectId = Guid.NewGuid(),
                        StartTime = DateTimeOffset.Now,
                    },
                       new Timeslot
                    {
                        ID = Guid.NewGuid(),
                        Duration = TimeSpan.FromMinutes(120),
                        ProjectId = Guid.NewGuid(),
                        StartTime = DateTimeOffset.Now,
                    },
                },
                Customer = new Customer
                {
                    ID = Guid.Parse("c0c67e74-457f-4e45-973d-3d584a91ce2a"),
                    Name="customer2"
                }
            }
        };
    }
}
