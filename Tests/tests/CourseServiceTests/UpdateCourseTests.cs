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
    class UpdateCourseTests
    {
        [Test]
        public void UpdateCourseWithNotExistingCourse()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("UpdateCourseWithNotExistingCourse")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await courseService.UpdateCourse(1, 1, new HikeCourse { });
                });
            }
        }

        [Test]
        public void EditCourseWithInvalidHikerNumberLimit()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("EditCourseWithInvalidHikerNumberLimit")
                .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {

                db.Hikes.Add(new Hike { Id = 1, Date = DateTime.Now.AddDays(2), OrganizerId = 1 });
                db.Courses.Add(new HikeCourse { Id = 1, HikeId = 1, Distance = 1010, NumOfRegisteredHikers = 10 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await courseService.UpdateCourse(1, 1,
                        new HikeCourse
                        {
                            Id = 1,
                            Distance = 1000,
                            CheckPoints = new List<CheckPoint> {
                                new CheckPoint { Id = 1, CourseId = 1, DistanceFromStart = 0 },
                                new CheckPoint { Id = 1, CourseId = 1, DistanceFromStart = 1000 }
                            },
                            MaxNumOfHikers = 5
                        });
                });
            }
        }

        [Test]
        public async Task EditCourse()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("EditCourse")
                .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {

                db.Hikes.Add(new Hike { Id = 1, Date = DateTime.Now.AddDays(2), OrganizerId = 1 });
                db.Courses.Add(new HikeCourse
                {
                    Id = 1,
                    HikeId = 1,
                    Distance = 1010,
                    NumOfRegisteredHikers = 10,
                    BeginningOfStart = DateTime.Now.AddDays(5)
                });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                CourseService courseService = new CourseService(db);
                await courseService.UpdateCourse(1, 1,
                        new HikeCourse
                        {
                            Name = "newName",
                            Id = 1,
                            Distance = 1000,
                            CheckPoints = new List<CheckPoint> {
                                new CheckPoint { Id = 1, CourseId = 1, DistanceFromStart = 0 },
                                new CheckPoint { Id = 2, CourseId = 1, DistanceFromStart = 1000 }
                            },
                            MaxNumOfHikers = 13,
                            Elevation = 1500,
                            LimitTime = new TimeSpan(1, 0, 0),
                            PlaceOfStart = "start",
                            PlaceOfFinish = "finish",
                            Price = 500,
                            RegisterDeadline = DateTime.Today,
                            BeginningOfStart = DateTime.Today.AddHours(10),
                            EndOfStart = DateTime.Today.AddHours(12),
                        });
                var course = db.Courses.Find(1);

                Assert.That(course.Name, Is.EqualTo("newName"));
                Assert.That(course.Id, Is.EqualTo(1));
                Assert.That(course.Distance, Is.EqualTo(1000));
                Assert.That(course.CheckPoints, Has.Count.EqualTo(2));
                Assert.That(course.MaxNumOfHikers, Is.EqualTo(13));
                Assert.That(course.LimitTime, Is.EqualTo(new TimeSpan(1, 0, 0)));
                Assert.That(course.PlaceOfStart, Is.EqualTo("start"));
                Assert.That(course.PlaceOfFinish, Is.EqualTo("finish"));
                Assert.That(course.Price, Is.EqualTo(500));
                Assert.That(course.RegisterDeadline, Is.EqualTo(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 21, 59, 59)));
                Assert.That(course.BeginningOfStart, Is.EqualTo(DateTime.Today.AddHours(10)));
                Assert.That(course.EndOfStart, Is.EqualTo(DateTime.Today.AddHours(12)));
            }
        }
    }
}


