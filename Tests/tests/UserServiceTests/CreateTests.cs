using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.tests.UserServiceTests
{
    [TestFixture]
    class CreateTests
    {
        [Test]
        public void CreateHikerWithNoPassword()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("CreateHikerWithNoPassword")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                Assert.Throws<ApplicationException>(() =>
                {
                    userService.Create(new Hiker
                    {
                        UserName = "telek",
                        Name = "Teszt Elek",
                        Email = "teszt.elek@gmail.com",
                        Gender = GenderTypes.Male,
                        Town = "Budapest"

                    }, "");
                });
            }
        }

        [Test]
        public void CreateHikerWithTakenUserName()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase<ApplicationContext>("CreateHikerWithTakenUserName")
               .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikers.Add(new Hiker { UserName = "telek" });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                Assert.Throws<ApplicationException>(() =>
                {
                    userService.Create(new Hiker
                    {
                        UserName = "telek",
                        Name = "Teszt Elek",
                        Email = "teszt.elek@gmail.com",
                        Gender = GenderTypes.Male,
                        Town = "Budapest"

                    }, "sdfsdfsd");
                });
            }
        }

        [Test]
        public void CreateHiker()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase<ApplicationContext>("CreateHiker")
               .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                userService.Create(new Hiker
                {
                    UserName = "telek",
                    Name = "Teszt Elek",
                    Email = "teszt.elek@gmail.com",
                    Gender = GenderTypes.Male,
                    Town = "Budapest"

                }, "reallySecurePassword");
                var users = userService.GetAll().Result;
                Assert.That(users, Has.Count.EqualTo(1));
                Assert.That(users.ElementAt(0).UserName, Is.EqualTo("telek"));
            }
        }
    }
}
