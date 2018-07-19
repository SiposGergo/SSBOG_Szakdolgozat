using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SSBO5G__Szakdolgozat.Exceptions;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Threading.Tasks;

namespace Tests.tests.CourseServiceTests
{
    [TestFixture]
    class GetCourseTests
    {
        [Test]
        public void GetCourseWithNotExistingCourse()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("GetCourseWithNotExistingCourse")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Courses.Add(new HikeCourse { Id = 1});
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await courseService.GetCourse(2);
                });
            }
        }

        [Test]
        public async Task GetCourseWithExistingCourse()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("GetCourseWithExistingCourse")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Courses.Add(new HikeCourse { Id = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                var result = await courseService.GetCourse(1);
                Assert.That(result.Id, Is.EqualTo(1));
            }
        }
    }
}
