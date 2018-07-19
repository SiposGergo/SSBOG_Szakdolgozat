using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SSBO5G__Szakdolgozat.Exceptions;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Threading.Tasks;

namespace Tests.tests.HikeServiceTests
{
    [TestFixture]
    class AddHelperTests
    {
        [Test]
        public void AddHikeHelperWithNotOwnHike()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddHikeHelperWithNotOwnHike")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikers.Add(new Hiker { Id = 1 });
                db.Hikes.Add(new Hike { Id = 1, OrganizerId = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                Assert.ThrowsAsync<UnauthorizedException>(async () =>
                {
                    await hikeService.AddHelper(1,2,"user");
                });
            }
        }

        [Test]
        public void AddHikeHelperWithNotExistingUser()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddHikeHelperWithNotExistingUser")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikers.Add(new Hiker { Id = 1 });
                db.Hikes.Add(new Hike { Id = 1, OrganizerId = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await hikeService.AddHelper(1, 1, "user");
                });
            }
        }

        [Test]
        public async Task AddHikeHelperWithExistingUser()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddHikeHelperWithExistingUser")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikers.Add(new Hiker { Id = 1, UserName = "user" });
                db.Hikes.Add(new Hike { Id = 1, OrganizerId = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                await hikeService.AddHelper(1, 1, "user");
                var helper = db.HikeHelpers.Find(1,1);
                Assert.That(helper, Is.Not.Null);
                Assert.That(helper.HikeId, Is.EqualTo(1));
                Assert.That(helper.HikerId, Is.EqualTo(1));
            }
        }

        [Test]
        public void AddHikeHelperWithAlreadyAddedHelper()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddHikeHelperWithAlreadyAddedHelper")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikers.Add(new Hiker { Id = 1, UserName = "user" });
                db.Hikes.Add(new Hike { Id = 1, OrganizerId = 1 });
                db.HikeHelpers.Add(new HikeHelper { HikeId = 1, HikerId = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await hikeService.AddHelper(1, 1, "user");
                });
            }
        }

    }
}
