using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tests.tests.HikeServiceTests
{
    [TestFixture]
    class GetTodayHikesTests
    {
        [Test]
        public async Task GetTodayHikesWithTwoExistngHikeToday()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("GetTodayHikesWithTwoExistngHikeToday")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikers.Add(new Hiker { Name = "Béla", Id = 1 });
                db.Courses.Add(new HikeCourse { HikeId = 1, Id = 1 });
                db.Courses.Add(new HikeCourse { HikeId = 2, Id = 2 });
                db.Hikes.AddRange(
                    new Hike { Date = DateTime.Today, Id = 1, OrganizerId = 1 },
                    new Hike { Date = DateTime.Today.AddDays(8), Id = 2, OrganizerId = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                var result = await hikeService.GetTodayHikes();
                Assert.That(result, Has.Count.EqualTo(1));
            }
        }

        [Test]
        public async Task GetTodayHikesWithNoHikeToday()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("GetTodayHikesWithNoHikeToday")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikers.Add(new Hiker { Name = "Béla", Id = 1 });
                db.Courses.Add(new HikeCourse { HikeId = 1, Id = 1 });
                db.Courses.Add(new HikeCourse { HikeId = 2, Id = 2 });
                db.Hikes.AddRange(
                    new Hike { Date = DateTime.Today.AddDays(2), Id = 1, OrganizerId = 1 },
                    new Hike { Date = DateTime.Today.AddDays(8), Id = 2, OrganizerId = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                var result = await hikeService.GetTodayHikes();
                Assert.That(result, Has.Count.EqualTo(0));
            }
        }
    }
}
