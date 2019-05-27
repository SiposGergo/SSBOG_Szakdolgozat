using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SSBO5G__Szakdolgozat.Exceptions;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests.tests.CourseServiceTests
{

    [TestFixture]
    class GetPdfCourseInfoTests
    {
        [Test]
        public void GetPdfCourseInfoCourseWithNotExistingCourse()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("GetPdfCourseInfoCourseWithNotExistingCourse")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikes.Add(new Hike { Id = 1, OrganizerId = 1 });
                db.Courses.Add(new HikeCourse { Id = 1, HikeId = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await courseService.GetPdfCourseInfo(2, 2);
                });
            }
        }

        /* [Test]
        public void GetPdfCourseInfoCourseWithNoOrganizer()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("GetPdfCourseInfoCourseWithNoOrganizer")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikes.Add(new Hike { Id = 1, OrganizerId = 1 });
                db.Courses.Add(new HikeCourse { Id = 1, HikeId = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                Assert.ThrowsAsync<UnauthorizedException>(async () =>
                {
                    await courseService.GetPdfCourseInfo(1, 2);
                });
            }
        }

        [Test]
        public void GetPdfCourseInfoWithNoRegistrations()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("GetPdfCourseInfoWithNoRegistrations")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikes.Add(new Hike { Id = 1, OrganizerId = 1 });
                db.Courses.Add(new HikeCourse { Id = 1, HikeId = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await courseService.GetPdfCourseInfo(1, 1);
                });
            }
        }

        [Test]
        public async Task GetPdfCourseInfoCourse()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("GetPdfCourseInfoCourse")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikes.Add(new Hike { Id = 1, OrganizerId = 1 });
                db.Hikers.Add(new Hiker { Id = 1 });
                db.Courses.Add(new HikeCourse
                {
                    Id = 1,
                    HikeId = 1
                ,
                    Registrations = new List<Registration>(){
                        new Registration { HikerId = 1, HikeCourseId = 1, Id = 1, StartNumber = "1" }
                }
                });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                await courseService.GetPdfCourseInfo(1, 1);
            }
        } */
    }
}