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
    class RegisterToHikeTests
    {
        [Test]
        public void RegisterToHikeWithNotExistingUser()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("RegisterToHikeWithNotExistingUser")
                   .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                RegistrationService registrationService = new RegistrationService(db);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await registrationService.RegisterToHike(new Registration { HikerId = 1 });
                });
            }
        }

        [Test]
        public void RegisterToHikeWithNotExistingHikeCourse()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("RegisterToHikeWithNotExistingHikeCourse")
                   .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                RegistrationService registrationService = new RegistrationService(db);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await registrationService.RegisterToHike(new Registration { HikerId = 1, HikeCourseId = 1 });
                });
            }
        }

        [Test]
        public void RegisterToHikeWithAlreadyExistingRegistration()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("RegisterToHikeWithAlreadyExistingRegistration")
                   .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {
                db.Courses.Add(new HikeCourse { Id = 1 });
                db.Registrations.Add(new Registration { HikeCourseId = 1, HikerId = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                RegistrationService registrationService = new RegistrationService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await registrationService.RegisterToHike(new Registration { HikerId = 1, HikeCourseId = 1 });
                });
            }
        }

        [Test]
        public void RegisterToHikeWithExpiredRegisterDeadline()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("RegisterToHikeWithExpiredRegisterDeadline")
                   .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {
                db.Courses.Add(new HikeCourse { Id = 1, RegisterDeadline = DateTime.Today.AddDays(-2) });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                RegistrationService registrationService = new RegistrationService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await registrationService.RegisterToHike(new Registration { HikerId = 1, HikeCourseId = 1 });
                });
            }
        }

        [Test]
        public void RegisterToHikeWithNoMorePlace()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("RegisterToHikeWithNoMorePlace")
                   .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {
                db.Courses.Add(new HikeCourse
                {
                    Id = 1,
                    RegisterDeadline = DateTime.Today.AddDays(2),
                    MaxNumOfHikers = 0
                });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                RegistrationService registrationService = new RegistrationService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await registrationService.RegisterToHike(new Registration { HikerId = 1, HikeCourseId = 1 });
                });
            }
        }

        [Test]
        public async Task RegisterToHike()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("RegisterToHike")
                   .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {
                db.Courses.Add(new HikeCourse
                {
                    Id = 1,
                    RegisterDeadline = DateTime.Today.AddDays(2),
                    MaxNumOfHikers = 10,
                    Distance = 14000
                });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                RegistrationService registrationService = new RegistrationService(db);
                
                var result = await registrationService.RegisterToHike(new Registration { HikerId = 1, HikeCourseId = 1 });

                Assert.NotNull(result);
                Assert.That(db.Registrations.Find(1).StartNumber, Is.EqualTo("140001"));
                Assert.That(db.Courses.Find(1).NumOfRegisteredHikers, Is.EqualTo(1));
            }
        }
    }
}