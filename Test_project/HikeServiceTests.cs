using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test_project
{
    [TestFixture]
    class HikeServiceTests
    {
        [Test]
        public async Task getAllHikesWithThreeHikesInDb()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test1");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
            HikeService hikeService = new HikeService(db);
            DbSeeder.FillWithTestData(db);
            var result = await hikeService.GetAllHike();
            Assert.That(result, Has.Count.EqualTo(3));

        }

        [Test]
        public async Task getAllHikesWithNoHikes()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test2");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
            HikeService hikeService = new HikeService(db);
            var result = await hikeService.GetAllHike();
            Assert.That(result, Has.Count.EqualTo(0));
        }

        [Test]
        public async Task getHikeDetailsWithExistingId()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test1");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
            HikeService hikeService = new HikeService(db);
            var result = await hikeService.GetById(1);
            Assert.That(result.Name, Is.EqualTo("Mátrahegy"));
            Assert.That(result.Comments, Has.Count.EqualTo(3));
        }
    }
}
