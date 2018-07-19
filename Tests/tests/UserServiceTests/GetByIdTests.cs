using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SSBO5G__Szakdolgozat.Exceptions;
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
    class GetByIdTests
    {
        [Test]
        public async Task GetHikerByIdWithExsitingId()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("GetHikerByIdWithExsitingId")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                userService.Create(new Hiker
                {
                    Id = 2,
                    UserName = "telek1",
                    Name = "Teszt Elek",
                    Email = "teszt.elek@gmail.com",
                    Gender = GenderTypes.Male,
                    Town = "Budapest"
                }, "reallySecurePassword");
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                var result = await userService.GetById(2);
                Assert.That(result.UserName, Is.EqualTo("telek1"));
            }
        }

        [Test]
        public void GetHikerByIdWithNotExistingId()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                 .UseInMemoryDatabase<ApplicationContext>("GetHikerByIdWithNotExistingId")
                 .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                userService.Create(new Hiker
                {
                    Id = 2,
                    UserName = "telek1",
                    Name = "Teszt Elek",
                    Email = "teszt.elek@gmail.com",
                    Gender = GenderTypes.Male,
                    Town = "Budapest"
                }, "reallySecurePassword");
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await userService.GetById(8);
                });
            }
        }
    }
}
