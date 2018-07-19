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
    class EditHikeTests
    {
        [Test]
        public void EditHikeWithNotExistingHikeInDb()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("EditHikeWithNotExistingHikeInDb")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikers.Add(new Hiker { Id = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await hikeService.EditHike(new Hike { Id = 1 }, 1);
                });
            }
        }

        [Test]
        public void EditHikeWithNoOrganizerId()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("EditHikeWithNoOrganizerId")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikes.Add(new Hike { Id = 1 });
                db.Hikers.Add(new Hiker { Id = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                Assert.ThrowsAsync<UnauthorizedException>(async () =>
                {
                    await hikeService.EditHike(new Hike { Id = 1, OrganizerId = 1 }, 2);
                });
            }
        }

        [Test]
        public void EditHikeWithTooOldHike()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("EditHikeWithTooOldHike")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikes.Add(new Hike { Id = 1, OrganizerId = 1, Date = DateTime.Now.AddDays(-5) });
                db.Hikers.Add(new Hiker { Id = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await hikeService.EditHike(new Hike { Id = 1, OrganizerId = 1 }, 1);
                });
            }
        }
        [Test]

        public void EditHikeWithModifyToFuture()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("EditHikeWithModifyToFuture")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikes.Add(new Hike { Id = 1, OrganizerId = 1, Date = DateTime.Now.AddDays(5) });
                db.Hikers.Add(new Hiker { Id = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await hikeService.EditHike(new Hike { Id = 1, OrganizerId = 1, Date = DateTime.Now.AddDays(-5) }, 1);
                });
            }
        }

        [Test]
        public async Task EditHikeWithCorrectData()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("EditHikeWithCorrectData")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikes.Add(new Hike { Id = 1, OrganizerId = 1, Date = DateTime.Now.AddDays(5) });
                db.Hikers.Add(new Hiker { Id = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                await hikeService.EditHike(new Hike
                {
                    Id = 1,
                    OrganizerId = 1,
                    Date = DateTime.Today.AddDays(5),
                    Description = "desc",
                    Name = "name",
                    Website = "website"
                }, 1);
                var hike = db.Hikes.Find(1);
                Assert.That(hike.Name, Is.EqualTo("name"));
                Assert.That(hike.Description, Is.EqualTo("desc"));
                Assert.That(hike.Website, Is.EqualTo("website"));
                Assert.That(hike.Date, Is.EqualTo(DateTime.Today.AddDays(5)));
            }
        }
    }
}
