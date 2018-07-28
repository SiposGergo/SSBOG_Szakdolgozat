using Microsoft.EntityFrameworkCore;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.TestData
{
    class AdminServiceTestData
    {
        public static void GetTestData(DbContextOptions<ApplicationContext> dbOptions)
        {
            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                userService.Create(new Hiker
                {
                    Id = 1,
                    UserName = "telek",
                    Name = "Teszt Elek",
                    Email = "teszt.elek@gmail.com",
                    Gender = GenderTypes.Male,
                    Town = "Budapest"

                }, "reallySecurePassword");

                userService.Create(new Hiker
                {
                    Id = 2,
                    UserName = "gergo",
                    Name = "Sipos Gergo",
                    Email = "gergo@gmail.com",
                    Gender = GenderTypes.Male,
                    Town = "Budapest"

                }, "reallySecurePassword");

                db.Hikes.Add(new Hike
                {
                    Id = 1,
                    OrganizerId = 1,
                });

                db.HikeHelpers.Add(new HikeHelper { HikeId = 1, HikerId = 1 });

                db.Courses.Add(new HikeCourse()
                {
                    Id = 1,
                    HikeId = 1,
                    LimitTime = new TimeSpan(1, 0, 0),
                });

                db.Registrations.Add(new Registration
                {
                    Id = 1,
                    HikeCourseId = 1,
                    HikerId = 1,
                    StartNumber = "1"
                });

                db.CheckPoints.AddRange(
                    new CheckPoint
                    {
                        Id = 1,
                        CourseId = 1,
                        DistanceFromStart = 10,
                        Open = DateTime.UtcNow.AddMinutes(-2),
                        Close = DateTime.UtcNow.AddMinutes(2)
                    },
                    new CheckPoint
                    {
                        Id = 2,
                        CourseId = 1,
                        DistanceFromStart = 20,
                        Open = DateTime.UtcNow.AddMinutes(2),
                        Close = DateTime.UtcNow.AddMinutes(3)
                    },
                    new CheckPoint
                    {
                        Id = 3,
                        CourseId = 1,
                        DistanceFromStart = 30,
                        Open = DateTime.UtcNow.AddMinutes(-2),
                        Close = DateTime.UtcNow.AddMinutes(-1)
                    },
                     new CheckPoint
                     {
                         Id = 4,
                         CourseId = 1,
                         DistanceFromStart = 40,
                         Open = DateTime.UtcNow.AddMinutes(-2),
                         Close = DateTime.UtcNow.AddMinutes(1)
                     }
                    );

                db.SaveChanges();
            }
        }
    }
}
