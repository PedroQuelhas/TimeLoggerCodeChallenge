using AutoMapper;
using ServerApi.CodeGen.Models;
using System;
using System.Collections.Generic;

namespace Timelogger.Model
{
	public class Project :BaseEntity
	{
        public string Name { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset Deadline { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public bool Completed { get; set; }
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<Timeslot> Timeslots { get; set; }
    }

    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDTO, Project>();
            CreateMap<Project, ProjectDTO>();
        }
    }
}
