using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SSBO5G__Szakdolgozat.Exceptions;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Tests.tests.HikeServiceTests
{
    [TestFixture]
    class GetByIdTests
    {
        [Test]
        public async Task GetHikeDetailsWithExistingId()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                  .UseInMemoryDatabase<ApplicationContext>("GetHikeDetailsWithExistingId")
                  .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikers.Add(new Hiker { Name = "Béla" });
                db.Hikes.Add(new Hike
                {
                    Name = "Teszt Túra",
                    OrganizerId = 1,
                    Id = 1
                });
                db.Courses.Add(new HikeCourse { HikeId = 1 });
                db.SaveChanges();
            }

            Hike result;
            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                result = await hikeService.GetById(1);
                Assert.That(result.Name, Is.EqualTo("Teszt Túra"));
                Assert.That(result.Organizer.Name, Is.EqualTo("Béla"));
                Assert.That(result.Courses, Has.Count.EqualTo(1));
            }
        }

        [Test]
        public void GetHikeDetailsWithNotExistingId()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                  .UseInMemoryDatabase("GetHikeDetailsWithNotExistingId")
                  .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await hikeService.GetById(5);
                });
            }
        }
    }
}
