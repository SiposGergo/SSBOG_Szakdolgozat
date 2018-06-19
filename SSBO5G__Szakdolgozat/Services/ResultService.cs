using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SSBO5G__Szakdolgozat.Dtos;
using SSBO5G__Szakdolgozat.Exceptions;
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
        Task<IEnumerable<RegistrationWithPassesDto>> GetLiveResult(int courseId);
        Task<IEnumerable<RegistrationWithPassesDto>> GetLiveResultNettoTime(int courseId);
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

        public async Task<IEnumerable<RegistrationWithPassesDto>> GetLiveResultNettoTime(int courseId)
        {
            HikeCourse course = await context.Courses
                .Where(x => x.Id == courseId)
                .Include(x => x.Registrations)
                .ThenInclude(x => x.Passes)
                .Include(x => x.Registrations)
                .ThenInclude(x => x.Hiker)
                .SingleOrDefaultAsync();

            if (course == null)
            {
                throw new NotFoundException("túra táv");
            }

            var registrations = course.Registrations
                .OrderByDescending(x => x.Passes.Count(p => p.TimeStamp != null))
                .ThenBy(x => x.Passes.Count(p => p.TimeStamp != null) >= 2 ?
                    x.Passes.Last(p => p.TimeStamp != null).TimeStamp - x.Passes.First(p => p.TimeStamp != null).TimeStamp :
                    new TimeSpan(100, 0, 0, 0, 0));

            return mapper.Map<IEnumerable<RegistrationWithPassesDto>>(registrations);
        }

        public async Task<IEnumerable<RegistrationWithPassesDto>> GetLiveResult(int courseId)
        {
            HikeCourse course = await context.Courses
                .Where(x => x.Id == courseId)
                .Include(x => x.Registrations)
                .ThenInclude(x => x.Passes)
                .Include(x => x.Registrations)
                .ThenInclude(x => x.Hiker)
                .SingleOrDefaultAsync();

            if (course == null)
            {
                throw new NotFoundException("túra táv");
            }

            var registrations = course.Registrations
                .OrderByDescending(x => x.Passes.Count(p => p.TimeStamp != null))
                .ThenBy(x => x.Passes.Any(p => p.TimeStamp != null) ?
                    x.Passes.LastOrDefault(p => p.TimeStamp != null).TimeStamp :
                    new DateTime(1970, 01, 01));

            return mapper.Map<IEnumerable<RegistrationWithPassesDto>>(registrations);
        }

        public async Task<ResultDto> GetResults(int courseId)
        {
            HikeCourse course = await context.Courses
                .Where(x => x.Id == courseId)
                .Include(x => x.Registrations)
                .ThenInclude(x => x.Hiker)
                .Include(x => x.Registrations)
                .ThenInclude(x => x.Passes)
                .Include(x => x.CheckPoints)
                .SingleOrDefaultAsync();

            if (course == null)
            {
                throw new NotFoundException("túra táv");
            }

            var registrations = course.Registrations
                .OrderByDescending(x => x.Passes.Count(p => p.TimeStamp != null))
                .ThenBy(x => x.Passes.Any(p => p.TimeStamp != null) ?
                    x.Passes.LastOrDefault(p => p.TimeStamp != null).TimeStamp :
                    new DateTime(1970, 01, 01));

            return new ResultDto
            {
                Checkpoints = mapper.Map<ICollection<CheckPointDto>>(course.CheckPoints),
                Registrations = mapper.Map<ICollection<RegistrationWithPassesDto>>(registrations)
            };
        }
    }
}
