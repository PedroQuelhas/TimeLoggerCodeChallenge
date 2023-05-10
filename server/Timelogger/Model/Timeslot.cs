using AutoMapper;
using ServerApi.CodeGen.Models;
using System;

namespace Timelogger.Model
{
    public class Timeslot :BaseEntity
    {
        public DateTimeOffset StartTime { get; set; }
        public int DurationInMinutes { get; set; }
        public Guid ProjectId { get; set; }
    }

    public class TimeslotProfile : Profile
    {
        public TimeslotProfile()
        {
            CreateMap<TimeslotDTO, Timeslot>();
            CreateMap<Timeslot, TimeslotDTO>();
        }
    }
}
