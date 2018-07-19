using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SSBO5G__Szakdolgozat.Exceptions;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.TestData;

namespace Tests.tests.CourseServiceTests
{
    [TestFixture]
    class AddCourseTests
    {
        [Test]
        public void AddCourseWithNotExistingHike()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddCourseWithNotExistingHike")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await courseService.AddCourse(null, 1, 1);
                });
            }
        }

        [Test]
        public void AddCourseWithNotExistingUser()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddCourseWithNotExistingHike")
                .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await courseService.AddCourse(null, 1, 1);
                });
            }
        }

        [Test]
        public void AddCourseWithHikeOrganizedInThePast()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddCourseWithHikeOrganizedInThePast")
                .Options;


            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikes.Add(new Hike { Id = 1, Date = DateTime.Now.AddDays(-2) });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await courseService.AddCourse(null, 1, 1);
                });
            }
        }

        [Test]
        public void AddCourseWithTwoCourseWithSameDistances()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddCourseWithTwoCourseWithSameDistances")
                .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikes.Add(new Hike { Id = 1, Date = DateTime.Now.AddDays(2) });
                db.Courses.Add(new HikeCourse { Id = 1, HikeId = 1, Distance = 1000 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await courseService.AddCourse(new HikeCourse { Distance = 1000 }, 1, 1);
                });
            }
        }


        [Test]
        public void AddCourseWithNotHikeOrganizerUser()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddCourseWithNotHikeOrganizerUser")
                .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {

                db.Hikes.Add(new Hike { Id = 1, Date = DateTime.Now.AddDays(2), OrganizerId = 2 });
                db.Courses.Add(new HikeCourse { Id = 1, HikeId = 1, Distance = 1010 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                Assert.ThrowsAsync<UnauthorizedException>(async () =>
                {
                    await courseService.AddCourse(new HikeCourse { Distance = 1000 }, 1, 1);
                });
            }
        }

        [Test]
        public void AddCourseWith1CheckPoint()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddCourseWith1CheckPoint")
                .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {

                db.Hikes.Add(new Hike { Id = 1, Date = DateTime.Now.AddDays(2), OrganizerId = 1 });
                db.Courses.Add(new HikeCourse { Id = 1, HikeId = 1, Distance = 1010 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await courseService.AddCourse(
                        new HikeCourse
                        {
                            Distance = 1000,
                            CheckPoints = new List<CheckPoint> { new CheckPoint { Id = 1, CourseId = 1 } }
                        }, 1, 1);
                });
            }
        }

        [Test]
        public void AddCourseWithBadStart()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddCourseWithBadStart")
                .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikes.Add(new Hike { Id = 1, Date = DateTime.Now.AddDays(2), OrganizerId = 1 });
                db.Courses.Add(new HikeCourse { Id = 1, HikeId = 1, Distance = 1010 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await courseService.AddCourse(
                        new HikeCourse
                        {
                            Distance = 1000,
                            CheckPoints = new List<CheckPoint> {
                                new CheckPoint { Id = 1, CourseId = 1, DistanceFromStart = 10 },
                                new CheckPoint { Id = 2, CourseId = 1 },
                            }
                        }, 1, 1);
                });
            }
        }

        [Test]
        public void AddCourseWithBadFinish()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddCourseWithBadFinish")
                .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikes.Add(new Hike { Id = 1, Date = DateTime.Now.AddDays(2), OrganizerId = 1 });
                db.Courses.Add(new HikeCourse { Id = 1, HikeId = 1, Distance = 1010 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await courseService.AddCourse(
                        new HikeCourse
                        {
                            Distance = 1000,
                            CheckPoints = new List<CheckPoint> {
                                new CheckPoint { Id = 1, CourseId = 1, DistanceFromStart = 0 },
                                new CheckPoint { Id = 2, CourseId = 1, DistanceFromStart = 900 },
                            }
                        }, 1, 1);
                });
            }
        }

        [Test]
        public async Task AddCourse()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddCourse")
                .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikes.Add(new Hike { Id = 1, Date = DateTime.Now.AddDays(2), OrganizerId = 1 });
                db.Courses.Add(new HikeCourse { Id = 1, HikeId = 1, Distance = 1010 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                await courseService.AddCourse(
                    new HikeCourse
                    {
                        Id = 2,
                        Distance = 1000,
                        CheckPoints = new List<CheckPoint> {
                                new CheckPoint { Id = 1, CourseId = 1, DistanceFromStart = 0 },
                                new CheckPoint { Id = 2, CourseId = 1, DistanceFromStart = 1000 },
                        }
                    }, 1, 1);
                Assert.That(db.Hikes.Find(1).Courses, Has.Count.EqualTo(2));
            }
        }

    }
}
