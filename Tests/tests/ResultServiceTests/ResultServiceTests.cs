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

namespace Tests.tests.ResultServiceTests
{
    [TestFixture]
    class ResultServiceTests
    {
        public ResultServiceTests()
        {
            AutoMapper.Mapper.Initialize(x => x.AddProfile<AutoMapperProfile>());
        }

        [Test]
        public void GetResultsWithNotExistingHike()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("GetResultsWithNotExistingHike")
                   .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                ResultService resultService = new ResultService(db, null);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await resultService.GetResults(1);
                });
            }
        }

        [Test]
        public void GetLiveResultsWithNotStartedHike()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("GetLiveResultsWithNotStartedHike")
                   .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Courses.Add(new HikeCourse { Id = 1, BeginningOfStart = DateTime.Now.AddDays(2) });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                ResultService resultService = new ResultService(db, null);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await resultService.GetLiveResult(1);
                });
            }
        }

        [Test]
        public async Task GetLiveResultsWithNoCompetitors()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("GetLiveResultsWithNoCompetitors")
                   .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Courses.Add(new HikeCourse { Id = 1, BeginningOfStart = DateTime.Now.AddDays(-1) });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                ResultService resultService = new ResultService(db, AutoMapper.Mapper.Instance);
                var result = await resultService.GetLiveResult(1);
                Assert.That(result, Has.Count.EqualTo(0));
            }
        }

        [Test]
        public async Task GetResultWith4Competitors()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("GetResultWith4Competitors")
                   .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);
            using (var db = new ApplicationContext(dbOptions))
            {

                db.Courses.Add(
                    new HikeCourse
                    {
                        Id = 1,
                        BeginningOfStart = DateTime.Now.AddDays(-1),
                        LimitTime = new TimeSpan(1, 0, 0),
                        CheckPoints = new List<CheckPoint>
                        {
                            new CheckPoint{Id = 1} ,
                            new CheckPoint{Id = 2} ,
                            new CheckPoint{Id = 3} ,
                        },
                        Registrations = new List<Registration>
                        {
                            new Registration { Id = 1, HikerId = 1  },
                            new Registration {
                                Id = 2,
                                HikerId = 1,
                                Passes = new List<CheckPointPass> {
                                    new CheckPointPass { CheckPointId = 1, TimeStamp = DateTime.Now},
                                    new CheckPointPass { CheckPointId = 2, TimeStamp = DateTime.Now.AddMinutes(4)}
                                }},
                            new Registration {
                                Id = 3,
                                HikerId = 1,
                                Passes = new List<CheckPointPass> {
                                    new CheckPointPass { CheckPointId = 1, TimeStamp = DateTime.Now},
                                    new CheckPointPass { CheckPointId = 2, TimeStamp = DateTime.Now}
                                } },
                            new Registration {
                                Id = 4,
                                HikerId = 1,
                                Passes = new List<CheckPointPass> {
                                    new CheckPointPass { CheckPointId = 1, TimeStamp = DateTime.Now},
                                } }
                        }
                    });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                ResultService resultService = new ResultService(db, AutoMapper.Mapper.Instance);
                var result = await resultService.GetResults(1);

                Assert.That(result.Registrations, Has.Count.EqualTo(4));
                Assert.That(result.Registrations.ElementAt(0).Id, Is.EqualTo(3));
                Assert.That(result.Registrations.ElementAt(1).Id, Is.EqualTo(2));
                Assert.That(result.Registrations.ElementAt(2).Id, Is.EqualTo(4));
                Assert.That(result.Registrations.ElementAt(3).Id, Is.EqualTo(1));


                Assert.That(result.Checkpoints, Has.Count.EqualTo(3));
                Assert.That(result.LimitTime, Is.EqualTo(new TimeSpan(1, 0, 0)));
            }
        }
    }
}
