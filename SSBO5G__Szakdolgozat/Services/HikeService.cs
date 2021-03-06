﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSBO5G__Szakdolgozat.Models;
using Microsoft.EntityFrameworkCore;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Exceptions;

namespace SSBO5G__Szakdolgozat.Services
{
    public interface IHikeService
    {
        Task<IEnumerable<Hike>> GetAllHike();
        Task<Hike> GetById(int id);
        Task<Comment> AddCommentToHike(Comment comment);
        Task AddHike(Hike hike);
        Task EditHike(Hike hike, int loggedInUserId);
        Task AddHelper(int hikeId, int loggedInUserId, string userName);
        Task<IEnumerable<Hike>> GetTodayHikes();
    }
    public class HikeService : IHikeService
    {
        private readonly ApplicationContext context;

        public HikeService(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<Comment> AddCommentToHike(Comment comment)
        {
            comment.TimeStamp = DateTime.UtcNow;
            Hiker hiker = await context.Hikers.FindAsync(comment.AuthorId);
            if (hiker == null)
            {
                throw new NotFoundException("túrázó");
            }
            Hike hike = await context.Hikes.FindAsync(comment.HikeId);
            if (hike == null)
            {
                throw new NotFoundException("túra");
            }
            if (String.IsNullOrWhiteSpace(comment.CommentText))
            {
                throw new ApplicationException("Üres komment szöveg!");
            }
            if (comment.TimeStamp == null)
            {
                throw new ApplicationException("Nincs időbélyeg!");
            }
            comment.Author = hiker;
            comment.Hike = hike;
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
            return comment;
        }

        public async Task AddHelper(int hikeId, int loggedInUserId, string userName)
        {
            var hike = await context.Hikes.FindAsync(hikeId);
            if (hike == null)
            {
                throw new NotFoundException("túra");
            }
            if (hike.OrganizerId != loggedInUserId)
            {
                throw new UnauthorizedException();
            }
            Hiker hikerToAdd = await context.Hikers
                .SingleOrDefaultAsync(x => x.UserName == userName);
            if (hikerToAdd == null)
            {
                throw new NotFoundException("túrázó");
            }
            var helpers = context.Hikes
                .Where(x => x.Id == hikeId)
                .SelectMany(x => x.Staff);
            if (helpers.Any(x => x.HikerId == hikerToAdd.Id))
            {
                throw new ApplicationException("Ez a túrázó már fel van véve segítőként");
            }
            HikeHelper helper = new HikeHelper
            {
                HikeId = hikeId,
                HikerId = hikerToAdd.Id
            };
            context.HikeHelpers.Add(helper);
            await context.SaveChangesAsync();
        }

        public async Task AddHike(Hike hike)
        {
            var organizer = await context.Hikers.FindAsync(hike.OrganizerId);
            if (organizer == null)
            {
                throw new NotFoundException("felhasználó");
            }
            if (hike.Date == null || hike.Date < DateTime.UtcNow)
            {
                throw new ApplicationException("Nem megfelelő dátum!");
            }
            await context.Hikes.AddAsync(hike);
            await context.HikeHelpers.AddAsync(new HikeHelper { HikeId = hike.Id, HikerId = hike.OrganizerId });
            await context.SaveChangesAsync();
        }

        public async Task EditHike(Hike hike, int loggedInUserId)
        {
            var organizer = await context.Hikers.FindAsync(hike.OrganizerId);
            if (organizer == null)
            {
                throw new NotFoundException("A felhasználó nem található!");
            }
            var hikeFromDb = await context.Hikes.
                Include(x => x.Courses)
                .ThenInclude(y => y.CheckPoints)
                .SingleOrDefaultAsync(x => x.Id == hike.Id);
            if (hikeFromDb == null)
            {
                throw new NotFoundException("túra");
            }
            if (hikeFromDb.OrganizerId != loggedInUserId)
            {
                throw new UnauthorizedException();
            }
            if (hike.Date < DateTime.UtcNow)
            {
                throw new ApplicationException("Nem módosíthatd a túra dátumát a mai napra vagy régebbre!!");
            }
            if (hikeFromDb.Date < DateTime.UtcNow)
            {
                throw new ApplicationException("Már elkezdődött túrát nem módosíthatsz!");
            }
            if (hikeFromDb.Date != hike.Date)
            {
                hikeFromDb.Date = hike.Date;
                foreach (HikeCourse course in hikeFromDb.Courses)
                {
                    course.EndOfStart = new DateTime(hike.Date.Year, hike.Date.Month, hike.Date.Day, course.EndOfStart.Hour, course.EndOfStart.Minute, 0).AddDays(1);
                    course.BeginningOfStart = new DateTime(hike.Date.Year, hike.Date.Month, hike.Date.Day, course.BeginningOfStart.Hour, course.BeginningOfStart.Minute, 0).AddDays(1);
                    foreach (CheckPoint cp in course.CheckPoints)
                    {
                        cp.Close = new DateTime(hike.Date.Year, hike.Date.Month, hike.Date.Day, cp.Close.Hour, cp.Close.Minute, 0).AddDays(1);
                        cp.Open = new DateTime(hike.Date.Year, hike.Date.Month, hike.Date.Day, cp.Open.Hour, cp.Open.Minute, 0).AddDays(1);
                    }
                    if (course.RegisterDeadline > hike.Date)
                    {
                        course.RegisterDeadline = new DateTime(hike.Date.Year, hike.Date.Month, hike.Date.Day, 21, 59, 59);
                    }
                }
            }
            hikeFromDb.Name = hike.Name;
            hikeFromDb.Description = hike.Description;
            hikeFromDb.Website = hike.Website;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Hike>> GetAllHike()
        {
            var hikes = await context.Hikes
                .Include(x => x.Courses)
                .ToListAsync();
            return hikes;
        }

        public async Task<Hike> GetById(int id)
        {
            var selectedHike = await context.Hikes
                .Include(hike => hike.Organizer)
                .Include(hike => hike.Courses)
                    .ThenInclude(course => course.CheckPoints)
                .Include(hike => hike.Courses)
                    .ThenInclude(course => course.Registrations)
                .Include(hike => hike.Comments)
                .ThenInclude(comments => comments.Author).
                Include(x => x.Staff)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (selectedHike == null)
            {
                throw new NotFoundException("túra");
            }
            return selectedHike;
        }

        public async Task<IEnumerable<Hike>> GetTodayHikes()
        {
            var ids = context.Hikes
                .Where(x => x.Date.AddHours(3).Date == DateTime.UtcNow.Date)
                .Select(x => x.Id);
            List<Hike> todayHikes = new List<Hike>();
            foreach (int id in ids)
            {
                todayHikes.Add(await GetById(id));
            }
            return todayHikes;
        }
    }
}
