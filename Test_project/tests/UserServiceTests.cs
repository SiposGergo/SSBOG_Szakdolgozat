using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test_project.tests
{
    [TestFixture]
    class UserServiceTests
    {
        [Test]
        public async Task GetAllUsersWith5UserInDb()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test1");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
            UserService userService = new UserService(db,null);
            var result = await userService.GetAll();
            Assert.That(result, Has.Count.EqualTo(5));
        }

        [Test]
        public async Task GetAllHikesWithThNoUserInDb()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test2");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
            UserService userService = new UserService(db, null);
            var result = await userService.GetAll();
            Assert.That(result, Has.Count.EqualTo(0));
        }

        [Test]
        public async Task GetHikerByIdWithExsitingId()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test1");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
            UserService userService = new UserService(db, null);
            var result = await userService.GetById(1);
            Assert.That(result.UserName, Is.EqualTo("prosipinho"));
        }

        [Test]
        public void GetHikerByIdWithNotExistingId()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test1");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
            UserService userService = new UserService(db, null);
            Assert.ThrowsAsync<ApplicationException>(async () =>
            {
                await userService.GetById(8);
            });
        }

        [Test]
        public void CreateHikerWithNoPassword()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test2");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
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

        [Test]
        public void CreateHikerWithTakenUserName()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test1");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
            UserService userService = new UserService(db, null);
            Assert.Throws<ApplicationException>(() =>
            {
                userService.Create(new Hiker
                {
                    UserName = "prosipinho",
                    Name = "Teszt Elek",
                    Email = "teszt.elek@gmail.com",
                    Gender = GenderTypes.Male,
                    Town = "Budapest"

                }, "sdfsdfsd");
            });
        }

        [Test]
        public void CreateHiker()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test3");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
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
        }

        [Test]
        public void AuthenticateUser()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test4");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
            UserService userService = new UserService(db, null);
            userService.Create(new Hiker
            {
                UserName = "telek1",
                Name = "Teszt Elek",
                Email = "teszt.elek@gmail.com",
                Gender = GenderTypes.Male,
                Town = "Budapest"

            }, "reallySecurePassword");
            var result = userService.Authenticate("telek1", "reallySecurePassword");
            Assert.That(result.Name, Is.EqualTo("Teszt Elek"));
        }

        [Test]
        public void AuthenticateUserBadUserName()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test4");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
            UserService userService = new UserService(db, null);
            var result = userService.Authenticate("telek11", "reallySecurePassword");
            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void AuthenticateUserBadPassword()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test4");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
            UserService userService = new UserService(db, null);
            var result = userService.Authenticate("telek1", "reallySecurePassword123");
            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void UpdateUserDataWtihNewUserName()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test1");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
            UserService userService = new UserService(db, null);
            Hiker user = new Hiker
            {
                Id = 2,
                UserName = "new"
            };
            
            Assert.DoesNotThrow(() => { userService.Update(user); });
        }

        [Test]
        public void UpdateUserDataWtithTakenUserName()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test1");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
            UserService userService = new UserService(db, null);
            Hiker user = new Hiker
            {
                Id = 1,
                UserName = "gyuri"
            };
            Assert.Throws<ApplicationException>(() => { userService.Update(user); });
        }

        [Test]
        public void DeleteUser()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("test6");
            var db = new ApplicationContext(dbOptionsBuilder.Options);
            UserService userService = new UserService(db, null);
            userService.Create(new Hiker
            {
                UserName = "telek",
                Name = "Teszt Elek",
                Email = "teszt.elek@gmail.com",
                Gender = GenderTypes.Male,
                Town = "Budapest"

            }, "reallySecurePassword");

            userService.Delete(1);

            Assert.ThrowsAsync<ApplicationException>(async () =>
            {
                await userService.GetById(1);
            });
        }

    }
}
