using AutoMapper;
using TimetableManager.Helpers.Models;
using TimetableManager.Repositories.Models;

namespace TimetableManager.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserInfoResponse>();
            CreateMap<UserInfoResponse, User>();
            CreateMap<UserCreateRequest, User>()
                .ForMember(tu => tu.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>();
        }
}
    
}
