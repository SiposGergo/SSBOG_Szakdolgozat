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

namespace Tests.tests.AdminServiceTests
{
    [TestFixture]
    class RecordCheckpointPassTests
    {
        [Test]
        public void RecordCheckpointPassWithNotExistingCheckPoint()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase("RecordCheckpointPassWithNotExistingCheckPoint")
                   .Options;
            AdminServiceTestData.GetTestData(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                AdminService adminService = new AdminService(db, null);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await adminService.RecordCheckpointPass(1, new RecordDto { CheckpointId = 10 });
                });

            }
        }

        [Test]
        public void RecordCheckpointPassWithNotHikeHelperUser()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase("RecordCheckpointPassWithNotHikeHelperUser")
                   .Options;
            AdminServiceTestData.GetTestData(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                AdminService adminService = new AdminService(db, null);
                Assert.ThrowsAsync<UnauthorizedException>(async () =>
                {
                    await adminService.RecordCheckpointPass(2, new RecordDto { CheckpointId = 1 });
                });

            }
        }

        [Test]
        public void RecordCheckpointPassWithNotExistingStartNumber()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase("RecordCheckpointPassWithNotExistingStartNumber")
                   .Options;
            AdminServiceTestData.GetTestData(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                AdminService adminService = new AdminService(db, null);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await adminService.RecordCheckpointPass(1, new RecordDto
                    {
                        CheckpointId = 1,
                        StartNumber = "5"
                    });
                });

            }
        }

        [Test]
        public void RecordCheckpointPassWithNotOpenCheckpoint()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase("RecordCheckpointPassWithNotOpenCheckpoint")
                   .Options;
            AdminServiceTestData.GetTestData(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                AdminService adminService = new AdminService(db, null);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await adminService.RecordCheckpointPass(1, new RecordDto
                    {
                        CheckpointId = 2,
                        StartNumber = "1"
                    });
                });

            }
        }

        [Test]
        public void RecordCheckpointPassWithClosedCheckPoint()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase("RecordCheckpointPassWithClosedCheckPoint")
                   .Options;
            AdminServiceTestData.GetTestData(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                AdminService adminService = new AdminService(db, null);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await adminService.RecordCheckpointPass(1, new RecordDto
                    {
                        CheckpointId = 3,
                        StartNumber = "1"
                    });
                });

            }
        }

        [Test]
        public void RecordCheckpointPassWithNoStartTime()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase("RecordCheckpointPassWithNoStartTime")
                   .Options;
            AdminServiceTestData.GetTestData(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                AdminService adminService = new AdminService(db, null);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await adminService.RecordCheckpointPass(1, new RecordDto
                    {
                        CheckpointId = 4,
                        StartNumber = "1"
                    });
                });

            }
        }

        [Test]
        public async Task RecordCheckpointPassStart()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase("RecordCheckpointPassStart")
                   .Options;
            AdminServiceTestData.GetTestData(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                AdminService adminService = new AdminService(db, null);
                var result = await adminService.RecordCheckpointPass(1, new RecordDto
                {
                    CheckpointId = 1,
                    StartNumber = "1"
                });
                Assert.That(result, Is.EqualTo("Túrázó elindítva: Teszt Elek"));

            }
        }

        [Test]
        public async Task RecordCheckpointPassFinish()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase("RecordCheckpointPassFinish")
                   .EnableSensitiveDataLogging()
                   .Options;
            AdminServiceTestData.GetTestData(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Registrations.Find(1).Passes = new List<CheckPointPass> {
                    new CheckPointPass {RegistrationId = 1, CheckPointId = 1, TimeStamp = DateTime.Now},
                    new CheckPointPass(){ },
                    new CheckPointPass(){ },
                    new CheckPointPass(){}
                };
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                FakeEmailSender emailSender = new FakeEmailSender();
                AdminService adminService = new AdminService(db, emailSender);
                var result = await adminService.RecordCheckpointPass(1, new RecordDto
                {
                    CheckpointId = 4,
                    StartNumber = "1",
                    TimeStamp = DateTime.Now.AddMilliseconds(400)
                });

                StringAssert.Contains("Áthaladás", result);
                StringAssert.Contains("Gratulálunk", emailSender.Email.Text);
                Assert.That(emailSender.Email.Address, Is.EqualTo("teszt.elek@gmail.com"));
                Assert.That(emailSender.Email.Subject, Is.EqualTo("Túra teljesítés"));
                Assert.That(emailSender.Email.PdfFile, Is.Not.Null);
                Assert.That(emailSender.Email.FileName, Is.EqualTo("oklevél.pdf"));
            }
        }
    }
}
