using Microsoft.EntityFrameworkCore;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.TestData
{
    class UserServiceTestData
    {
        public static void GetTesztElekUser(DbContextOptions<ApplicationContext> dbOptions)
        {
            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                userService.Create(new Hiker
                {
                    Id = 1,
                    UserName = "telek1",
                    Name = "Teszt Elek",
                    Email = "teszt.elek@gmail.com",
                    Gender = GenderTypes.Male,
                    Town = "Budapest"
                }, "reallySecurePassword");
            }
        }
    }
}
