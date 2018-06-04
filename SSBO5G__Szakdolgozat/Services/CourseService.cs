using Microsoft.EntityFrameworkCore;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Services
{
    public interface ICourseService
    {
        Task AddCourse(HikeCourse hikeCourse, int userId, int hikeId);
        Task<HikeCourse> GetCourse(int courseId);
        Task UpdateCourse(int userId, int courseId, HikeCourse course);
    }
    public class CourseService : ICourseService
    {
        ApplicationContext context;
        public CourseService(ApplicationContext context)
        {
            this.context = context;
        }

        private DateTime RemoveSecondsFromDateTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, 0);
        }

        public async Task AddCourse(HikeCourse hikeCourse, int userId, int hikeId)
        {
            var user = await context.Hikers.FindAsync(userId);
            if (user == null)
            {
                throw new ApplicationException("A felhasználó nem található!");
            }
            var hike = await context.Hikes
                .Include(x => x.Courses)
                .SingleOrDefaultAsync(x => x.Id == hikeId);
            if (hike == null)
            {
                throw new ApplicationException("A túra nem található!");
            }
            if (userId != hike.OrganizerId)
            {
                throw new ApplicationException("Nincs jogod a művelethez!");
            }
            if (hikeCourse.CheckPoints.Count < 2)
            {
                throw new ApplicationException("Minimum 2 ellenőrzőpont megadása szükséges (rajt és cél)");
            }
            hikeCourse.BeginningOfStart = RemoveSecondsFromDateTime(hikeCourse.BeginningOfStart);
            hikeCourse.EndOfStart = RemoveSecondsFromDateTime(hikeCourse.EndOfStart);
            foreach (CheckPoint checkPoint in hikeCourse.CheckPoints)
            {
                checkPoint.Open = RemoveSecondsFromDateTime(checkPoint.Open);
                checkPoint.Close = RemoveSecondsFromDateTime(checkPoint.Close);
            }
            hike.Courses.Add(hikeCourse);
            await context.SaveChangesAsync();
        }

        public async Task<HikeCourse> GetCourse(int courseId)
        {
            HikeCourse course = await context.Courses
                .Include(x => x.CheckPoints)
                .SingleOrDefaultAsync(x => x.Id == courseId);
            if (course == null)
            {
                throw new ApplicationException("Nincs ilyen táv!");
            }
            return course;
        }

        public async Task UpdateCourse(int userId, int courseId, HikeCourse courseParam )
        {
            HikeCourse course = await context.Courses
                .Include(x => x.CheckPoints)
                .Include(x => x.Hike)
                .SingleOrDefaultAsync(x=>x.Id == courseId);
            if (course == null)
            {
                throw new ApplicationException("Nincs ilyen táv!");
            }
            if (course.Hike.OrganizerId != userId)
            {
                throw new ApplicationException("Nincs jogod a művelethez!");
            }
            if (courseParam.CheckPoints.Count < 2)
            {
                throw new ApplicationException("Minimum 2 ellenőrzőpont megadása szükséges (rajt és cél)");
            }
            if (courseParam.MaxNumOfHikers< course.NumOfRegisteredHikers)
            {
                throw new ApplicationException("Nem lehetséges a már nevezettek száma alá vinni a létszámkorlátot!");
            }
            if (DateTime.Now > course.BeginningOfStart)
            {
                throw new ApplicationException("A rajt után már nem lehet változtatni!");
            }
            courseParam.BeginningOfStart = RemoveSecondsFromDateTime(courseParam.BeginningOfStart);
            courseParam.EndOfStart = RemoveSecondsFromDateTime(courseParam.EndOfStart);
            foreach (CheckPoint checkPoint in courseParam.CheckPoints)
            {
                checkPoint.Open = RemoveSecondsFromDateTime(checkPoint.Open);
                checkPoint.Close = RemoveSecondsFromDateTime(checkPoint.Close);
            }
            course.Name = courseParam.Name;
            course.Distance = courseParam.Distance;
            course.Elevation = courseParam.Distance;
            course.LimitTime = courseParam.LimitTime;
            course.MaxNumOfHikers = courseParam.MaxNumOfHikers;
            course.PlaceOfFinish = courseParam.PlaceOfFinish;
            course.PlaceOfStart = courseParam.PlaceOfStart;
            course.Price = courseParam.Price;
            course.RegisterDeadline = courseParam.RegisterDeadline;
            course.BeginningOfStart = courseParam.BeginningOfStart;
            course.EndOfStart = courseParam.EndOfStart;
            course.CheckPoints = courseParam.CheckPoints;
            await context.SaveChangesAsync();
        }
    }
}
