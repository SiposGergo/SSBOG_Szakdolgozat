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
using Tests.TestData;

namespace Tests.tests.UserServiceTests
{
    [TestFixture]
    class ChangePasswordTests
    {
        [Test]
        public void ChangePasswordWithNotExistingUser()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("ChangePasswordWithNotExistingUser")
                   .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await userService.ChangePassword(1, null);
                });
            }
        }

        [Test]
        public void ChangePasswordWithoutNewPassword()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("ChangePasswordWithNotExistingUser")
                   .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await userService.ChangePassword(1,
                        new SSBO5G__Szakdolgozat.Dtos.ChangePasswordDto
                        { CurrentPassword = "gfh", NewPassword = "" });
                });
            }
        }

        [Test]
        public void ChangePasswordWithBadCurrentPassword()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("ChangePasswordWithBadCurrentPassword")
                   .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await userService.ChangePassword(1,
                        new SSBO5G__Szakdolgozat.Dtos.ChangePasswordDto
                        { CurrentPassword = "bad", NewPassword = "new" });
                });
            }
        }

        [Test]
        public async Task ChangePassword()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("ChangePassword")
                   .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                await userService.ChangePassword(1,
                    new SSBO5G__Szakdolgozat.Dtos.ChangePasswordDto
                    { CurrentPassword = "reallySecurePassword", NewPassword = "newnewnew" });
            }
            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                var user = userService.Authenticate("telek1", "reallySecurePassword");
                Assert.That(user, Is.Null);

                user = userService.Authenticate("telek1", "newnewnew");
                Assert.That(user, Is.Not.Null);
            }

        }
    }
}
