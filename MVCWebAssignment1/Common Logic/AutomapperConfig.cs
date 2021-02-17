using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MVCWebAssignment1.DTO;
using MVCWebAssignment1.Models;


namespace FYP_WebApp.Common_Logic
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Venue, VenueDto>();
            CreateMap<FamilyGroup, FamilyGroupDto>();
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<Meet, MeetDto>();
            CreateMap<Event, EventDto>();
            CreateMap<Round, RoundDto>();
            CreateMap<Lane, LaneDto>();

            //Other way
            CreateMap<MeetDto, Meet>();
            CreateMap<ApplicationUserDto, ApplicationUser>();
            CreateMap<EventDto, Event>();
            CreateMap<RoundDto, Round>();
            CreateMap<LaneDto, Lane>();
        }
    }

    public class AutomapperConfig
    {
        private static AutomapperConfig _instance;

        static AutomapperConfig()
        {
            _instance = new AutomapperConfig();
        }

        public static AutomapperConfig instance()
        {
            return _instance;
        }

        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            return config;
        }
    }
}