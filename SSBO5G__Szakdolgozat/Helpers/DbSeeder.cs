using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;

namespace SSBO5G__Szakdolgozat.Helpers
{
    public class DbSeeder
    {
        //////////TÚRÁZÓK//////////
        public static void FillWithTestData(ApplicationContext context)
        {
            IUserService userService = new UserService(context);

            Hiker h1 = new Hiker
            {
                Name = "Gergő",
                Email = "asd@lol.com",
                DateOfBirth = new DateTime(1997, 03, 26),
                Gender = GenderTypes.Male,
                Town = "Nagyréde",
                UserName = "prosipinho",
                PhoneNumber = "06309119162"
            };
            Hiker h2 = new Hiker
            {
                Name = "Laci",
                Email = "asd@bme.com",
                DateOfBirth = new DateTime(1997, 01, 01),
                Gender = GenderTypes.Male,
                Town = "Nagyréde",
                UserName = "laci01"
            };
            Hiker h3 = new Hiker
            {
                Name = "György",
                Email = "asd@konyvtar.com",
                DateOfBirth = new DateTime(1973, 01, 01),
                Gender = GenderTypes.Male,
                Town = "Nagyréde",
                UserName = "gyuri",
                PhoneNumber = "06303084317"
            };
            Hiker h4 = new Hiker
            {
                Name = "Erdei László",
                Email = "asd@alfold.com",
                DateOfBirth = new DateTime(1995, 01, 01),
                Gender = GenderTypes.Male,
                Town = "Nagyréde",
                UserName = "laci02",
                PhoneNumber = "06303084417"
            };
            Hiker h5 = new Hiker
            {
                Name = "Matécz Péter",
                Email = "mpn@bthe.hu",
                DateOfBirth = new DateTime(1975, 01, 01),
                Gender = GenderTypes.Male,
                Town = "BörzsönyValami",
                UserName = "peti1",
                PhoneNumber = "063077111012"
            };

            // Hozzá is adja
            Hiker h11 = userService.Create(h1, "abc1");
            Hiker h21 = userService.Create(h2, "abc2");
            Hiker h31 = userService.Create(h3, "abc3");
            Hiker h41 = userService.Create(h4, "abc4");
            Hiker h51 = userService.Create(h5, "abc5");

            //////////TÚRÁK//////////
            context.Hikes.AddRange(
                new Hike
                {
                    Name = "Mátrahegy",
                    Website = "http://kekesturista.hu",
                    Date = new DateTime(2018, 03, 09),
                    Description = "Mátrahegy telejsítménytúra",
                    OrganizerId = 3,
                }, new Hike
                {
                    Name = "Patai Mátra",
                    Website = "http://www.alfoldte.hu",
                    Date = new DateTime(2018, 07, 22),
                    Description = "Patai Mátra telejsítménytúra. Túra Gyöngyöspatáról 4 távon.",
                    OrganizerId = 4,
                },
                new Hike
                {
                    Name = "Andezit",
                    Website = "http://www.bthe.hu",
                    Date = new DateTime(2014, 04, 13),
                    Description = "Andezit 30/15",
                    OrganizerId = 5,
                }
                );

            context.SaveChanges();

            context.HikeHelpers.AddRange(
                new HikeHelper { HikeId = 1, HikerId = 1 },
                new HikeHelper { HikeId = 1, HikerId = 2 },
                new HikeHelper { HikeId = 2, HikerId = 1 },
                new HikeHelper { HikeId = 2, HikerId = 2 }
                );
            context.SaveChanges();

            //////////Kommentek//////////
            context.Comments.AddRange(
                new Comment
                {
                    AuthorId = 1,
                    HikeId = 1,
                    CommentText = "slalalalalalaaa",
                    TimeStamp = DateTime.Now
                },
                new Comment
                {
                    AuthorId = 2,
                    HikeId = 1,
                    CommentText = "lollooll",
                    TimeStamp = DateTime.Now
                },
                new Comment
                {
                    AuthorId = 3,
                    HikeId = 1,
                    CommentText = ":DDD",
                    TimeStamp = DateTime.Now
                },
                new Comment
                {
                    AuthorId = 2,
                    HikeId = 2,
                    CommentText = "trolololoolo",
                    TimeStamp = DateTime.Now
                });
            context.SaveChanges();

            //////////Távok//////////
            context.Courses.Add(
                new HikeCourse
                {
                    BeginningOfStart = new DateTime(2018, 03, 09, 7, 00, 00),
                    EndOfStart = new DateTime(2018, 03, 09, 8, 00, 00),
                    Distance = 43320,
                    Elevation = 1922,
                    Name = "Mátrahegy 40",
                    HikeId = 1,
                    MaxNumOfHikers = 100,
                    PlaceOfFinish = "Mátrafüred, Mátra Szakképző Iskola (Erdész u. 11.)",
                    PlaceOfStart = "Mátrafüred, Mátra Szakképző Iskola (Erdész u. 11.)",
                    Price = 1000,
                    RegisterDeadline = new DateTime(2018, 03, 08, 0, 00, 00),
                    LimitTime = new TimeSpan(11, 00, 00),
                    NumOfRegisteredHikers = 0
                });

            context.Courses.Add(
                new HikeCourse
                {
                    BeginningOfStart = new DateTime(2018, 07, 22, 6, 00, 00),
                    EndOfStart = new DateTime(2018, 07, 22, 8, 00, 00),
                    Distance = 51400,
                    Elevation = 1992,
                    Name = "Patai Mátra 50",
                    HikeId = 2,
                    MaxNumOfHikers = 3,
                    PlaceOfFinish = "Gyöngyöspata, általános iskola",
                    PlaceOfStart = "Gyöngyöspata, általános iskola",
                    Price = 1600,
                    RegisterDeadline = new DateTime(2018, 07, 21, 0, 00, 00),
                    LimitTime = new TimeSpan(12, 00, 00),
                    NumOfRegisteredHikers = 2,
                    StartnumInc = 2
                });

            context.Courses.Add(
                new HikeCourse
                {
                    BeginningOfStart = new DateTime(2018, 07, 22, 6, 00, 00),
                    EndOfStart = new DateTime(2018, 07, 22, 8, 00, 00),
                    Distance = 34400,
                    Elevation = 1338,
                    Name = "Patai Mátra 35",
                    HikeId = 2,
                    MaxNumOfHikers = 100,
                    PlaceOfFinish = "Gyöngyöspata, általános iskola",
                    PlaceOfStart = "Gyöngyöspata, általános iskola",
                    Price = 1400,
                    RegisterDeadline = new DateTime(2018, 07, 21, 0, 00, 00),
                    LimitTime = new TimeSpan(10, 00, 00),
                    NumOfRegisteredHikers = 0,
                    StartnumInc = 0
                });

            context.Courses.Add(
                new HikeCourse
                {
                    BeginningOfStart = new DateTime(2014, 04, 13, 7, 30, 00),
                    EndOfStart = new DateTime(2014, 04, 13, 9, 00, 00),
                    Distance = 29750,
                    Elevation = 915,
                    Name = "30 kilométeres táv.",
                    HikeId = 3,
                    MaxNumOfHikers = 40,
                    PlaceOfFinish = "Galgaguta, evangélikus templom melletti park",
                    PlaceOfStart = "Galgaguta, evangélikus templom melletti park",
                    Price = 1200,
                    RegisterDeadline = new DateTime(2014, 04, 10, 0, 00, 00),
                    LimitTime = new TimeSpan(8, 00, 00),
                    NumOfRegisteredHikers = 0,
                    StartnumInc = 0
                });

            context.SaveChanges();


            //////////Ellebőrzőpontok//////////
            //// Mátrahegy
            context.CheckPoints.AddRange(
                new CheckPoint
                {
                    CourseId = 1,
                    Name = "Rajt",
                    Description = "",
                    DistanceFromStart = 0,
                    Open = new DateTime(2018, 03, 09, 07, 00, 00),
                    Close = new DateTime(2018, 03, 09, 8, 00, 00)
                },
                 new CheckPoint
                 {
                     CourseId = 1,
                     Name = "Muzsla Tető",
                     Description = "kilátő",
                     DistanceFromStart = 2500,
                     Open = new DateTime(2018, 03, 09, 07, 00, 00),
                     Close = new DateTime(2018, 03, 09, 10, 45, 00)
                 }, new CheckPoint
                 {
                     CourseId = 1,
                     Name = "Sástó",
                     Description = "étterem terasza",
                     DistanceFromStart = 3960,
                     Open = new DateTime(2018, 03, 09, 07, 20, 00),
                     Close = new DateTime(2018, 03, 09, 13, 15, 00)
                 },
                 new CheckPoint
                 {
                     CourseId = 1,
                     Name = "Köves Bérc",
                     Description = "útkereszteződés",
                     DistanceFromStart = 8940,
                     Open = new DateTime(2018, 03, 09, 8, 00, 00),
                     Close = new DateTime(2018, 03, 09, 10, 10, 00)
                 }, new CheckPoint
                 {
                     CourseId = 1,
                     Name = "Galyatető",
                     Description = "parkoló",
                     DistanceFromStart = 17840,
                     Open = new DateTime(2018, 03, 09, 9, 30, 00),
                     Close = new DateTime(2018, 03, 09, 12, 30, 00)
                 },
                 new CheckPoint
                 {
                     CourseId = 1,
                     Name = "Parádsasvár",
                     Description = "büfé",
                     DistanceFromStart = 23790,
                     Open = new DateTime(2018, 03, 09, 8, 30, 00),
                     Close = new DateTime(2018, 03, 09, 14, 00, 00)
                 }, new CheckPoint
                 {
                     CourseId = 1,
                     Name = "Sós-cseri tető",
                     Description = "bója",
                     DistanceFromStart = 24580,
                     Open = new DateTime(2018, 03, 09, 8, 00, 00),
                     Close = new DateTime(2018, 03, 09, 10, 10, 00)
                 },
                 new CheckPoint
                 {
                     CourseId = 1,
                     Name = "Parádóhuta",
                     Description = "söröző",
                     DistanceFromStart = 28820,
                     Open = new DateTime(2018, 03, 09, 9, 15, 00),
                     Close = new DateTime(2018, 03, 09, 15, 30, 00)
                 }, new CheckPoint
                 {
                     CourseId = 1,
                     Name = "Pisztrángos-tó",
                     Description = "esőház",
                     DistanceFromStart = 32320,
                     Open = new DateTime(2018, 03, 09, 9, 30, 00),
                     Close = new DateTime(2018, 03, 09, 16, 30, 00)
                 },
                 new CheckPoint
                 {
                     CourseId = 1,
                     Name = "Kékestető",
                     Description = "Tető étterem",
                     DistanceFromStart = 35080,
                     Open = new DateTime(2018, 03, 09, 9, 45, 00),
                     Close = new DateTime(2018, 03, 09, 17, 00, 00)
                 }, new CheckPoint
                 {
                     CourseId = 1,
                     Name = "Vályús kút",
                     Description = "forrásnál",
                     DistanceFromStart = 37920,
                     Open = new DateTime(2018, 03, 09, 10, 20, 00),
                     Close = new DateTime(2018, 03, 09, 18, 00, 00)
                 },
                 new CheckPoint
                 {
                     CourseId = 1,
                     Name = "Cél",
                     Description = "",
                     DistanceFromStart = 43420,
                     Open = new DateTime(2018, 03, 09, 11, 00, 00),
                     Close = new DateTime(2018, 03, 09, 19, 10, 00)
                 }
                );

            //// Patai Mátra 50
            context.CheckPoints.AddRange(
                new CheckPoint
                {
                    CourseId = 2,
                    Name = "Rajt",
                    Description = "",
                    DistanceFromStart = 0,
                    Open = new DateTime(2018, 07, 22, 6, 00, 00),
                    Close = new DateTime(2018, 07, 22, 7, 00, 00)
                },
                 new CheckPoint
                 {
                     CourseId = 2,
                     Name = "Kecske kő",
                     Description = "",
                     DistanceFromStart = 2600,
                     Open = new DateTime(2018, 07, 22, 6, 10, 00),
                     Close = new DateTime(2018, 07, 22, 7, 40, 00)
                 },
                  new CheckPoint
                  {
                      CourseId = 2,
                      Name = "Várhegy",
                      Description = "",
                      DistanceFromStart = 5800,
                      Open = new DateTime(2018, 07, 22, 6, 20, 00),
                      Close = new DateTime(2018, 07, 22, 8, 30, 00)
                  },
                   new CheckPoint
                   {
                       CourseId = 2,
                       Name = "Diós-kút",
                       Description = "",
                       DistanceFromStart = 8900,
                       Open = new DateTime(2018, 07, 22, 6, 50, 00),
                       Close = new DateTime(2018, 07, 22, 9, 15, 00)
                   },
                    new CheckPoint
                    {
                        CourseId = 2,
                        Name = "Fajzatpuszta",
                        Description = "",
                        DistanceFromStart = 11100,
                        Open = new DateTime(2018, 07, 22, 7, 00, 00),
                        Close = new DateTime(2018, 07, 22, 9, 40, 00)
                    },
                     new CheckPoint
                     {
                         CourseId = 2,
                         Name = "Káva",
                         Description = "",
                         DistanceFromStart = 13300,
                         Open = new DateTime(2018, 07, 22, 7, 5, 00),
                         Close = new DateTime(2018, 07, 22, 10, 20, 00)
                     },
                      new CheckPoint
                      {
                          CourseId = 2,
                          Name = "Tót-hegyes",
                          Description = "",
                          DistanceFromStart = 16900,
                          Open = new DateTime(2018, 07, 22, 7, 25, 00),
                          Close = new DateTime(2018, 07, 22, 11, 20, 00)
                      },
                       new CheckPoint
                       {
                           CourseId = 2,
                           Name = "Hideg-kút",
                           Description = "",
                           DistanceFromStart = 20100,
                           Open = new DateTime(2018, 07, 22, 7, 40, 00),
                           Close = new DateTime(2018, 07, 22, 12, 10, 00)
                       },
                        new CheckPoint
                        {
                            CourseId = 2,
                            Name = "Nyikom",
                            Description = "kilátó",
                            DistanceFromStart = 24500,
                            Open = new DateTime(2018, 07, 22, 8, 00, 00),
                            Close = new DateTime(2018, 07, 22, 13, 15, 00)
                        },
                         new CheckPoint
                         {
                             CourseId = 2,
                             Name = "Mátrakeresztes",
                             Description = "",
                             DistanceFromStart = 28700,
                             Open = new DateTime(2018, 07, 22, 8, 40, 00),
                             Close = new DateTime(2018, 07, 22, 14, 30, 00)
                         },
                         new CheckPoint
                         {
                             CourseId = 2,
                             Name = "Világos-hegy",
                             Description = "",
                             DistanceFromStart = 36500,
                             Open = new DateTime(2018, 07, 22, 8, 50, 00),
                             Close = new DateTime(2018, 07, 22, 16, 20, 00)
                         },
                          new CheckPoint
                          {
                              CourseId = 2,
                              Name = "Nagy-lénia",
                              Description = "",
                              DistanceFromStart = 39200,
                              Open = new DateTime(2018, 07, 22, 9, 00, 00),
                              Close = new DateTime(2018, 07, 22, 17, 00, 00)
                          },
                           new CheckPoint
                           {
                               CourseId = 2,
                               Name = "Fajzatpuszta",
                               Description = "",
                               DistanceFromStart = 41200,
                               Open = new DateTime(2018, 07, 22, 9, 10, 00),
                               Close = new DateTime(2018, 07, 22, 17, 30, 00)
                           },
                            new CheckPoint
                            {
                                CourseId = 2,
                                Name = "Havas",
                                Description = "",
                                DistanceFromStart = 44200,
                                Open = new DateTime(2018, 07, 22, 9, 30, 00),
                                Close = new DateTime(2018, 07, 22, 18, 15, 00)
                            }, new CheckPoint
                            {
                                CourseId = 2,
                                Name = "Puskaporos-kút",
                                Description = "",
                                DistanceFromStart = 46400,
                                Open = new DateTime(2018, 07, 22, 9, 50, 00),
                                Close = new DateTime(2018, 07, 22, 18, 50, 00)
                            },
                             new CheckPoint
                             {
                                 CourseId = 2,
                                 Name = "Cél",
                                 Description = "",
                                 DistanceFromStart = 51400,
                                 Open = new DateTime(2018, 07, 22, 10, 00, 00),
                                 Close = new DateTime(2018, 07, 22, 20, 00, 00)
                             }
                );
            context.SaveChanges();

            //////////Előnevezések//////////
            context.Registrations.AddRange(
                new Registration
                {
                    HikeCourseId = 1,
                    HikerId = 1,
                    StartNumber = "1"
                },
                new Registration
                {
                    HikeCourseId = 1,
                    HikerId = 3,
                    StartNumber = "2"
                }
                );
            context.SaveChanges();

            //////////Teljesítések//////////
            context.Participations.AddRange(
                new Participation { CourseId = 1, HikerId = 1 },
                new Participation { CourseId = 1, HikerId = 2 }
                );
            context.SaveChanges();


            //////////áthaladások//////////
            context.CheckPointPasses.AddRange(
                new CheckPointPass
                {
                    CheckPointId = 1,
                    ParticipationId = 1,
                    TimeStamp = new DateTime(2018, 03, 09, 7, 10, 0)
                },
                new CheckPointPass
                {
                    CheckPointId = 2,
                    ParticipationId = 1,
                    TimeStamp = new DateTime(2018, 03, 09, 8, 10, 0),
                },
                new CheckPointPass
                {
                    CheckPointId = 3,
                    ParticipationId = 1,
                    TimeStamp = new DateTime(2018, 03, 09, 8, 50, 0),
                },
                new CheckPointPass
                {
                    CheckPointId = 4,
                    ParticipationId = 1,
                    TimeStamp = new DateTime(2018, 03, 09, 9, 40, 0),
                },
                new CheckPointPass
                {
                    CheckPointId = 5,
                    ParticipationId = 1,
                    TimeStamp = new DateTime(2018, 03, 09, 10, 25, 0),
                },
                new CheckPointPass
                {
                    CheckPointId = 6,
                    ParticipationId = 1,
                    TimeStamp = new DateTime(2018, 03, 09, 11, 0, 0),
                },
                new CheckPointPass
                {
                    CheckPointId = 7,
                    ParticipationId = 1,
                    TimeStamp = new DateTime(2018, 03, 09, 11, 46, 0),
                },
                new CheckPointPass
                {
                    CheckPointId = 8,
                    ParticipationId = 1,
                    TimeStamp = new DateTime(2018, 03, 09, 12, 40, 0),
                },
                new CheckPointPass
                {
                    CheckPointId = 9,
                    ParticipationId = 1,
                    TimeStamp = new DateTime(2018, 03, 09, 13, 40, 0),
                },
                new CheckPointPass
                {
                    CheckPointId = 10,
                    ParticipationId = 1,
                    TimeStamp = new DateTime(2018, 03, 09, 14, 40, 0),
                },
                new CheckPointPass
                {
                    CheckPointId = 11,
                    ParticipationId = 1,
                    TimeStamp = new DateTime(2018, 03, 09, 15, 25, 0),
                },
                new CheckPointPass
                {
                    CheckPointId = 12,
                    ParticipationId = 1,
                    TimeStamp = new DateTime(2018, 03, 09, 16, 10, 0),
                }
                    );
            context.SaveChanges();
        }
    }
}
