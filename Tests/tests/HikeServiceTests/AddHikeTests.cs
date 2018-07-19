using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SSBO5G__Szakdolgozat.Exceptions;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;

namespace Tests.tests.HikeServiceTests
{
    [TestFixture]
    class AddHikeTests
    {
        [Test]
        public void AddHikeWithNotExistingOrganizer()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddHikeWithNotExistingOrganizer")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await hikeService.AddHike(new Hike { OrganizerId = 1, Date = DateTime.Today.AddDays(1) });
                });
            }
        }

        [Test]
        public void AddHikeWithPastDate()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddHikeWithPastDate")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikers.Add(new Hiker { Id = 1 });
                HikeService hikeService = new HikeService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await hikeService.AddHike(new Hike { OrganizerId = 1, Date = DateTime.Today.AddDays(-4) });
                });
            }
        }

        [Test]
        public void AddHikeWithValidData()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddHikeWithValidData")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikers.Add(new Hiker { Id = 1 });
                HikeService hikeService = new HikeService(db);
                Assert.DoesNotThrowAsync(async () =>
                {
                    await hikeService.AddHike(new Hike { OrganizerId = 1, Date = DateTime.Today.AddDays(4) });
                });
            }
        }
    }
}
