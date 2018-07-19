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
    class DeleteTests
    {
        [Test]
        public void DeleteUser()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                   .UseInMemoryDatabase<ApplicationContext>("DeleteUser")
                   .Options;

            UserServiceTestData.GetTesztElekUser(dbOptions);

            using (var db = new ApplicationContext(dbOptions))
            {
                UserService userService = new UserService(db, null);
                Assert.DoesNotThrowAsync(async () =>
                {
                    await userService.GetById(1);
                });
                
                userService.Delete(1);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await userService.GetById(1);
                });
            }
        }
    }
}
