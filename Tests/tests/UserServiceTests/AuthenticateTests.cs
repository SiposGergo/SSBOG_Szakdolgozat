using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tests.TestData;

namespace Tests.tests.UserServiceTests
{
    [TestFixture]
    class AuthenticateTests
    {
        [Test]
        public void AuthenticateUser()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AuthenticateUser")
                .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                var result = userService.Authenticate("telek1", "reallySecurePassword");
                Assert.That(result.Name, Is.EqualTo("Teszt Elek"));
            }
        }

        [Test]
        public void AuthenticateUserBadUserName()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                 .UseInMemoryDatabase<ApplicationContext>("AuthenticateUserBadUserName")
                 .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                var result = userService.Authenticate("telek11", "reallySecurePassword");
                Assert.That(result, Is.EqualTo(null));
            }
        }

        [Test]
        public void AuthenticateUserBadPassword()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AuthenticateUserBadPassword")
                .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                var result = userService.Authenticate("telek1", "reallySecurePassword123");
                Assert.That(result, Is.EqualTo(null));
            }
        }
    }
}
