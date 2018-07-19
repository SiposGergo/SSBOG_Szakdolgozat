using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SSBO5G__Szakdolgozat.Dtos;
using SSBO5G__Szakdolgozat.Exceptions;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Fakes;
using Tests.TestData;

namespace Tests.tests.UserServiceTests
{
    [TestFixture]
    class ForgottenPasswordTests
    {
        [Test]
        public void ForgottenPasswordWithNoEmailAddress()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("ForgottenPasswordWithNoEmailAddress")
                   .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await userService.ForgottenPassword(new ForgottenPasswordDto
                    {
                        UserName = "username"
                    });
                });
            }
        }

        [Test]
        public void ForgottenPasswordWithNoUserName()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("ForgottenPasswordWithNoUserName")
                   .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await userService.ForgottenPassword(new ForgottenPasswordDto
                    {
                        Email = "email"
                    });
                });
            }
        }

        [Test]
        public void ForgottenPasswordWithNotValidData()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("ForgottenPasswordWithNoUserName")
                   .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await userService.ForgottenPassword(new ForgottenPasswordDto
                    {
                        Email = "email",
                        UserName = "user"
                    });
                });
            }
        }

        [Test]
        public async Task ForgottenPassword()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("ForgottenPassword")
                   .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                FakeEmailSender emailSender = new FakeEmailSender();
                UserService userService = new UserService(db, emailSender);

                var auth = userService.Authenticate("telek1", "reallySecurePassword");
                Assert.That(auth, Is.Not.Null);

                await userService.ForgottenPassword(new ForgottenPasswordDto
                {
                    Email = "teszt.elek@gmail.com",
                    UserName = "telek1"
                });

                auth = userService.Authenticate("telek1", "reallySecurePassword");
                Assert.That(auth, Is.Null);

                Assert.That(emailSender.Email.Address, Is.EqualTo("teszt.elek@gmail.com"));
                Assert.That(emailSender.Email.Subject, Is.EqualTo("Elfelejtett jelszó!"));
                StringAssert.Contains("Kedves Teszt Elek", emailSender.Email.Text);
                Assert.That(emailSender.Email.PdfFile, Is.Null);
                Assert.That(emailSender.Email.FileName, Is.Null);
            }
        }
    }
}
