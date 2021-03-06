﻿using Microsoft.EntityFrameworkCore;
using SSBO5G__Szakdolgozat.Exceptions;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Services
{
    public interface IRegistrationService
    {
        Task<Registration> RegisterToHike(Registration registration);
        Task<int> UnRegisterFromHike(Registration registration);
    }
    public class RegistrationService: IRegistrationService
    {
        private readonly ApplicationContext context;
        public RegistrationService(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<Registration> RegisterToHike(Registration registration)
        {
            Hiker hiker = await context.Hikers.FindAsync(registration.HikerId);
            if (hiker == null)
            {
                throw new NotFoundException("túrázó");
            }
            HikeCourse course = await context.Courses.FindAsync(registration.HikeCourseId);
            if (course == null)
            {
                throw new NotFoundException("táv");
            }
            var reg = await context
                .Registrations
                .FirstOrDefaultAsync(x =>
                   x.HikeCourseId == registration.HikeCourseId
                   && x.HikerId == registration.HikerId);
            if (reg != null)
            {
                throw new ApplicationException("Ez a regisztráció már létezik!");
            }
            if (course.RegisterDeadline < DateTime.UtcNow )
            {
                throw new ApplicationException("Lejárt a nevezési határidő!");
            }
            if (course.NumOfRegisteredHikers >= course.MaxNumOfHikers )
            {
                throw new ApplicationException("Elfogytak a helyek!");
            }
            int startNumber = ++course.StartnumInc;
            course.NumOfRegisteredHikers++;
            registration.StartNumber = ((int)course.Distance)
                .ToString() + startNumber . ToString();
            await context.Registrations.AddAsync(registration);
            await context.SaveChangesAsync();
            registration.HikeCourse = course;
            registration.Hiker = hiker;
            return registration;
        }

        public async Task<int> UnRegisterFromHike(Registration registration)
        {
            Hiker hiker = await context.Hikers.FindAsync(registration.HikerId);
            if (hiker == null)
            {
                throw new NotFoundException("túrázó");
            }
            HikeCourse course = await context.Courses.FindAsync(registration.HikeCourseId);
            if (course == null)
            {
                throw new NotFoundException("táv");
            }
            var reg = await context
                .Registrations
                .FirstOrDefaultAsync(x =>
                   x.HikeCourseId == registration.HikeCourseId
                   && x.HikerId == registration.HikerId);
            if (reg == null)
            {
                throw new ApplicationException("Ez a regisztráció nem létezik!");
            }
            if (DateTime.UtcNow > course.RegisterDeadline)
            {
                throw new ApplicationException("Az előnevezési határidő után már nem lehetséges a nevezés lemondása!");
            }
            course.NumOfRegisteredHikers--;
            context.Registrations.Remove(reg);
            await context.SaveChangesAsync();
            return reg.Id;
        }
    }
}
