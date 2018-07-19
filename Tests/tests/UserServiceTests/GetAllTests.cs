using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tests.tests.UserServiceTests
{
    [TestFixture]
    class GetAllTests
    {
        [Test]
        public async Task GetAllUsersWith5UserInDb()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase<ApplicationContext>("GetAllUsersWith5UserInDb")
               .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikers.AddRange(new Hiker(), new Hiker(), new Hiker(), new Hiker(), new Hiker());
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                var result = await userService.GetAll();
                Assert.That(result, Has.Count.EqualTo(5));
            }
        }

        [Test]
        public async Task GetAllHikesWithThNoUserInDb()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("GetAllUsersWith5UserInDb")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                var result = await userService.GetAll();
                Assert.That(result, Has.Count.EqualTo(0));
            }
        }
    }
}
