using AutoMapper;
using SSBO5G__Szakdolgozat.Dtos;
using SSBO5G__Szakdolgozat.Models;

namespace SSBO5G__Szakdolgozat.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Hiker, HikerDto>();
            CreateMap<HikerDto, Hiker>();

            CreateMap<CommentDto, Comment>().ForMember(x => x.Hike, opt => opt.Ignore());

            CreateMap<Registration, RegistrationDto>();

            CreateMap<HikeDto, Hike>()
                 .IgnoreAllPropertiesWithAnInaccessibleSetter()
                 .ForMember(x => x.Organizer, opt => opt.Ignore());


        }
    }
}
