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
    class GetAllHikesTests
    {
        [Test]
        public async Task GetAllHikesWithThreeHikesInDb()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("GetAllHikesWithThreeHikesInDb")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikes.AddRange(new Hike(), new Hike(), new Hike());
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                var result = await hikeService.GetAllHike();
                Assert.That(result, Has.Count.EqualTo(3));
            }
        }

        [Test]
        public async Task GetAllHikesWithNoHikes()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase("GetAllHikesWithNoHikes")
               .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                var result = await hikeService.GetAllHike();
                Assert.That(result, Has.Count.EqualTo(0));
            }
        }
    }
}
