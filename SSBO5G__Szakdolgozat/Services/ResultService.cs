using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SSBO5G__Szakdolgozat.Dtos;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Services
{
    public interface IResultService
    {
        Task<ResultDto> GetResults(int courseId);
    }
    public class ResultService : IResultService
    {
        ApplicationContext context;
        IMapper mapper;
        public ResultService(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<ResultDto> GetResults(int courseId)
        {
            HikeCourse course = await context.Courses
                .Where(x => x.Id == courseId)
                .Include(x => x.Registrations)
                .ThenInclude(x=> x.Hiker)
                .Include(x => x.Registrations)
                .ThenInclude(x => x.Passes)
                .Include(x => x.CheckPoints)
                .SingleOrDefaultAsync();

            var registrations = course.Registrations
                .OrderByDescending(x => x.Passes.Count(p => p.TimeStamp != null))
                .ThenBy(x => x.Passes.Any(p => p.TimeStamp != null) ? x.Passes.LastOrDefault(p => p.TimeStamp != null).TimeStamp : new DateTime(1970, 01, 01));
            return new ResultDto
            {
                Checkpoints = mapper.Map<ICollection<CheckPointDto>>(course.CheckPoints),
                Registrations = mapper.Map<ICollection<RegistrationWithPassesDto>>(registrations)
            };
        }
    }
}
