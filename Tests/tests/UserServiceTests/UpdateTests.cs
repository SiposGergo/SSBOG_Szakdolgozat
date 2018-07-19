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
    class UpdateTests
    {
        [Test]
        public void UpdateUserDataWithNewUserNameThatAlreadyExist()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase<ApplicationContext>("UpdateUserDataWithNewUserNameThatAlreadyExist")
               .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                db.Hikers.Add(new Hiker { UserName = "telek", Id = 1 });
                db.SaveChanges();
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
                Hiker user = new Hiker
                {
                    Id = 2,
                    UserName = "telek"
                };

                Assert.Throws<ApplicationException>(() => { userService.Update(user, "reallySecurePassword"); });
            }
        }

        [Test]
        public void UpdateUserDataWtithNoPassword()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
              .UseInMemoryDatabase<ApplicationContext>("UpdateUserDataWtithNoPassword")
              .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                Hiker user = new Hiker
                {
                    Id = 1,
                    UserName = "telek"
                };

                Assert.Throws<ArgumentException>(() => { userService.Update(user, ""); });
            }
        }

        [Test]
        public void UpdateUserDataWtithBadPassword()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
              .UseInMemoryDatabase<ApplicationContext>("UpdateUserDataWtithBadPassword")
              .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                Hiker user = new Hiker
                {
                    Id = 1,
                    UserName = "telek"
                };

                Assert.Throws<ApplicationException>(() => { userService.Update(user, "badassword"); });
            }
        }

        [Test]
        public void UpdateUserData()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
              .UseInMemoryDatabase<ApplicationContext>("UpdateUserData")
              .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                Hiker user = new Hiker
                {
                    Id = 1,
                    UserName = "telek12",
                    Email = "newEmail",
                    Town = "Szeged",
                    Gender = GenderTypes.Female,
                    DateOfBirth = DateTime.Today,
                    Name = "Teszt János",
                    PhoneNumber = "1234"
                };

                userService.Update(user, "reallySecurePassword");

                var editedUser = db.Hikers.Find(1);
                Assert.That(editedUser.UserName, Is.EqualTo("telek12"));
                Assert.That(editedUser.Email, Is.EqualTo("newEmail"));
                Assert.That(editedUser.Town, Is.EqualTo("Szeged"));
                Assert.That(editedUser.UserName, Is.EqualTo("telek12"));
                Assert.That(editedUser.Gender, Is.EqualTo(GenderTypes.Female));
                Assert.That(editedUser.DateOfBirth, Is.EqualTo(DateTime.Today));
                Assert.That(editedUser.Name, Is.EqualTo("Teszt János"));
                Assert.That(editedUser.PhoneNumber, Is.EqualTo("1234"));
            }
        }
    }
}
