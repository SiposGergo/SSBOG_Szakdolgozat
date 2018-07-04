using AutoMapper;
using SSBO5G__Szakdolgozat.Dtos;
using SSBO5G__Szakdolgozat.Models;
using System;
using System.Linq;

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
                 .ForMember(x => x.Organizer, opt => opt.Ignore())
                 .ForMember(x => x.Comments, opt => opt.Ignore())
                 .ForMember(x => x.Courses, opt => opt.Ignore())
                 .ForMember(x => x.Staff, opt => opt.Ignore());

            CreateMap<HikeCourseDto, HikeCourse>()
                .ForMember(X => X.LimitTime, opt => opt.ResolveUsing(src =>
                {
                    int hours = (int)Math.Floor(src.LimitTime);
                    int minutes = (int)((src.LimitTime - hours) * 60);
                    return new TimeSpan(hours, minutes, 0);
                }))
                .ForMember(x=> x.Registrations, opt => opt.Ignore())
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<HikeCourse, HikeCourseDto>()
                .ForMember(x => x.LimitTime, opt => opt.ResolveUsing(src =>
                {
                    return src.LimitTime.TotalHours;
                }));

            CreateMap<CheckPointDto, CheckPoint>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
            
        }
    }
}
