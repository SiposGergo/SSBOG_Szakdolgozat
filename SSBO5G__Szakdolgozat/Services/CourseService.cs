using Microsoft.EntityFrameworkCore;
using SSBO5G__Szakdolgozat.Exceptions;
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
        Task <byte[]> GetPdfCourseInfo(int courseId, int loggedInUserId);
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

        private bool CheckCheckPoints(ICollection<CheckPoint> checkPoints)
        {
            double dist = -1;
            foreach (CheckPoint cp in checkPoints)
            {
                if (cp.DistanceFromStart <= dist)
                {
                    throw new ApplicationException("Az ellenőrzőpontok távolság adatai helytelenek!");
                }
                else
                {
                    dist = cp.DistanceFromStart;
                }
            }
            return true;
        }

        public async Task AddCourse(HikeCourse hikeCourse, int userId, int hikeId)
        {
            var user = await context.Hikers.FindAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("felhasználó");
            }
            var hike = await context.Hikes
                .Include(x => x.Courses)
                .SingleOrDefaultAsync(x => x.Id == hikeId);
            if (hike == null)
            {
                throw new NotFoundException("túra");
            }
            if (hike.Date <= DateTime.Now)
            {
                throw new ApplicationException("Ehhez a túrához már nem adhatsz új távot!");
            }
            if (hike.Courses.Any(x=>x.Distance ==hikeCourse.Distance))
            {
                throw new ApplicationException("Egy túrán belül nem lehet két azonos táv!");
            }
            if (userId != hike.OrganizerId)
            {
                throw new UnauthorizedException();
            }
            if (hikeCourse.CheckPoints.Count < 2)
            {
                throw new ApplicationException("Minimum 2 ellenőrzőpont megadása szükséges (rajt és cél)");
            }
            if (hikeCourse.CheckPoints.ElementAt(0).DistanceFromStart != 0)
            {
                throw new ApplicationException("A start távolsága a starttól 0 méter!");
            }
            CheckCheckPoints(hikeCourse.CheckPoints);
            hikeCourse.BeginningOfStart = RemoveSecondsFromDateTime(hikeCourse.BeginningOfStart);
            hikeCourse.EndOfStart = RemoveSecondsFromDateTime(hikeCourse.EndOfStart);
            foreach (CheckPoint checkPoint in hikeCourse.CheckPoints)
            {
                checkPoint.Open = RemoveSecondsFromDateTime(checkPoint.Open);
                checkPoint.Close = RemoveSecondsFromDateTime(checkPoint.Close);
            }
            hikeCourse.HikeId = hike.Id;
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
                throw new NotFoundException("táv!");
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
                throw new NotFoundException("táv!");
            }
            if (course.Hike.OrganizerId != userId)
            {
                throw new UnauthorizedException();
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
            if (course.CheckPoints.ElementAt(0).DistanceFromStart != 0)
            {
                throw new ApplicationException("A start távolsága a starttól 0 méter!");
            }
            CheckCheckPoints(courseParam.CheckPoints);
            context.CheckPoints.RemoveRange(course.CheckPoints);
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

        public async Task<byte[]> GetPdfCourseInfo(int courseId, int loggedInUserId)
        {
            HikeCourse course = await context.Courses
                .Include(x => x.Registrations)
                .ThenInclude(y=> y.Hiker)
                .Include(x=> x.Hike)
                .SingleOrDefaultAsync(x=> x.Id == courseId);
            if (course == null)
            {
                throw new NotFoundException("táv");
            }
            if (loggedInUserId != course.Hike.OrganizerId)
            {
                throw new UnauthorizedException();
            }
            if (course.Registrations.Count == 0)
            {
                throw new ApplicationException("Még nem nevezett senki");
            }
            return PdfGenerator.GetCourseInfoPdf(course);
        }
    }
}