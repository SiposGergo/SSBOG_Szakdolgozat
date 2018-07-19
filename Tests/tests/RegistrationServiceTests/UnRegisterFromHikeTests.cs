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

namespace Tests.tests.RegistrationServiceTests
{
    [TestFixture]
    class UnRegisterFromHikeTests
    {
        [Test]
        public void UnRegisterFromHikeWithNotExistingUser()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("UnRegisterFromHikeWithNotExistingUser")
                   .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                RegistrationService registrationService = new RegistrationService(db);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await registrationService.UnRegisterFromHike(new Registration { HikerId = 1 });
                });
            }
        }

        [Test]
        public void UnRegisterFromHikeWithNotExistingHikeCourse()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("UnRegisterFromHikeWithNotExistingHikeCourse")
                   .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                RegistrationService registrationService = new RegistrationService(db);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await registrationService.UnRegisterFromHike(new Registration { HikerId = 1, HikeCourseId = 1 });
                });
            }
        }

        [Test]
        public void UnRegisterFromHikeWithNotExistingRegistration()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("UnRegisterFromHikeWithNotExistingRegistration")
                   .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {
                db.Courses.Add(new HikeCourse { Id = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                RegistrationService registrationService = new RegistrationService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await registrationService.UnRegisterFromHike(new Registration { HikerId = 1, HikeCourseId = 1 });
                });
            }
        }

        [Test]
        public void UnRegisterFromHikeWithExpiredRegisterDeadline()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("UnRegisterFromHikeWithExpiredRegisterDeadline")
                   .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {
                db.Registrations.Add(new Registration { HikeCourseId = 1, HikerId = 1 });
                db.Courses.Add(new HikeCourse { Id = 1, RegisterDeadline = DateTime.Today.AddDays(-2) });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                RegistrationService registrationService = new RegistrationService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await registrationService.UnRegisterFromHike(new Registration { HikerId = 1, HikeCourseId = 1 });
                });
            }
        }

        [Test]
        public async Task UnregisterFromHike()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("UnregisterFromHike")
                   .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {
                db.Courses.Add(new HikeCourse
                {
                    Id = 1,
                    RegisterDeadline = DateTime.Today.AddDays(2),
                    MaxNumOfHikers = 10,
                    Distance = 14000,
                    NumOfRegisteredHikers = 1
                });
                db.Registrations.Add(new Registration { Id = 1, HikeCourseId = 1, HikerId = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                RegistrationService registrationService = new RegistrationService(db);

                var result = await registrationService.UnRegisterFromHike(new Registration { HikerId = 1, HikeCourseId = 1 });

                Assert.That(result, Is.EqualTo(1));
                Assert.That(db.Registrations.Count(),Is.EqualTo(0));
                Assert.That(db.Courses.Find(1).NumOfRegisteredHikers, Is.EqualTo(0));
            }
        }
    }
}
