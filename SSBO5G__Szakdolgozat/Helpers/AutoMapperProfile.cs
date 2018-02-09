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
        }
    }
}
