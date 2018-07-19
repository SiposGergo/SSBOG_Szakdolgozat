using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SSBO5G__Szakdolgozat.Exceptions;
using SSBO5G__Szakdolgozat.Helpers;
using SSBO5G__Szakdolgozat.Models;
using SSBO5G__Szakdolgozat.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace Tests.tests.HikeServiceTests
{
    [TestFixture]
    class AddCommentToHikesTests
    {
        [Test]
        public void AddCommentToHikeWithNoHikerInDb()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddCommentToHikeWithNoHikerInDb")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikes.Add(new Hike { Id = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await hikeService.AddCommentToHike(new Comment { HikeId = 1, AuthorId = 1 });
                });
            }
        }

        [Test]
        public void AddCommentToHikeWithNoHikeInDb()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddCommentToHikeWithNoHikeInDb")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikers.Add(new Hiker { Id = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await hikeService.AddCommentToHike(new Comment { HikeId = 1, AuthorId = 1 });
                });
            }
        }

        [Test]
        public void AddCommentToHikeWithNoCommentTextAndDate()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddCommentToHikeWithNoCommentTextAndDate")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikers.Add(new Hiker { Id = 1 });
                db.Hikes.Add(new Hike { Id = 1, OrganizerId = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                Assert.ThrowsAsync<ApplicationException>(async () =>
                {
                    await hikeService.AddCommentToHike(new Comment { HikeId = 1, AuthorId = 1 });
                });
            }
        }

        [Test]
        public async Task AddCommentToHikeWithValidData()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase<ApplicationContext>("AddCommentToHikeWithValidData")
                .Options;

            using (var db = new ApplicationContext(dbOptions))
            {
                db.Hikers.Add(new Hiker { Id = 1 });
                db.Hikes.Add(new Hike { Id = 1, OrganizerId = 1 });
                db.SaveChanges();
            }

            using (var db = new ApplicationContext(dbOptions))
            {
                HikeService hikeService = new HikeService(db);
                await hikeService.AddCommentToHike(new Comment
                {
                    HikeId = 1,
                    AuthorId = 1,
                    CommentText = "komment",
                    TimeStamp = DateTime.Now
                });
                Assert.That(db.Hikes.Find(1).Comments, Has.Count.EqualTo(1));
                Assert.That(db.Hikes.Find(1).Comments.ElementAt(0).CommentText, Is.EqualTo("komment"));

            }
        }
    }
}
