using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSBO5G__Szakdolgozat.Models;
using Microsoft.EntityFrameworkCore;
using SSBO5G__Szakdolgozat.Helpers;

namespace SSBO5G__Szakdolgozat.Services
{
    public interface IHikeService
    {
        Task<IEnumerable<Hike>> GetAllHike();
        Task<Hike> GetById(int id);
        Task<Comment> AddCommentToHike(Comment comment);
        Task AddHike(Hike hike);
        Task EditHike(Hike hike, int loggedInUserId);
        Task AddHelper(int hikeId, int loggedInUserId, string userName);
    }
    public class HikeService : IHikeService
    {
        ApplicationContext context;
        public HikeService(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<Comment> AddCommentToHike(Comment comment)
        {
            comment.TimeStamp = DateTime.Now;
            Hiker hiker = await context.Hikers.FindAsync(comment.AuthorId);
            if (hiker == null)
            {
                throw new ApplicationException("Nem található a komment szerzője");
            }
            Hike hike = await context.Hikes.FindAsync(comment.HikeId);
            if (hike == null)
            {
                throw new ApplicationException("Nem található a kommentált túra");
            }
            comment.Author = hiker;
            comment.Hike = hike;
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
            return comment;
        }

        public async Task AddHelper(int hikeId, int loggedInUserId, string userName)
        {
            var hike = await context.Hikes.FindAsync(hikeId);
            if (hike == null)
            {
                throw new ApplicationException("Nem található ilyen túra");
            }
            if (hike.OrganizerId != loggedInUserId)
            {
                throw new ApplicationException("Ehhez nincs jogod");
            }
            Hiker hikerToAdd = await context.Hikers
                .SingleOrDefaultAsync(x => x.UserName == userName);
            if (hikerToAdd == null)
            {
                throw new ApplicationException("Nem található túrázó ezzel a felahsználónévvel");
            }
            var helpers = context.Hikes
                .Where(x => x.Id == hikeId)
                .SelectMany(x => x.Staff);
            if (helpers.Any(x => x.HikerId == hikerToAdd.Id))
            {
                throw new ApplicationException("Ez a túrázó már fel van véve segítőként");
            }
            HikeHelper helper = new HikeHelper
            {
                HikeId = hikeId,
                HikerId = hikerToAdd.Id
            };
            context.HikeHelpers.Add(helper);
            await context.SaveChangesAsync();
        }

        public async Task AddHike(Hike hike)
        {
            var organizer = await context.Hikers.FindAsync(hike.OrganizerId);
            if (organizer == null)
            {
                throw new ApplicationException("A felhasználó nem található!");
            }
            if (hike.Date < DateTime.Now)
            {
                throw new ApplicationException("A múltba nem szervezünk túrát!");
            }
            await context.Hikes.AddAsync(hike);
            await context.SaveChangesAsync();
        }

        public async Task EditHike(Hike hike, int loggedInUserId)
        {
            var organizer = await context.Hikers.FindAsync(hike.OrganizerId);
            if (organizer == null)
            {
                throw new ApplicationException("A felhasználó nem található!");
            }
            var hikeFromDb = await context.Hikes.FindAsync(hike.Id);
            if (hikeFromDb.OrganizerId != loggedInUserId)
            {
                throw new ApplicationException("Tiltott művelet!");
            }
            if (hike.Date < DateTime.Now)
            {
                throw new ApplicationException("A múltba nem szervezünk túrát!");
            }
            hikeFromDb.Name = hike.Name;
            hikeFromDb.Description = hike.Description;
            hikeFromDb.Date = hike.Date;
            hikeFromDb.Website = hike.Website;
            await context.SaveChangesAsync();            
        }

        public async Task<IEnumerable<Hike>> GetAllHike()
        {
            var hikes = await context.Hikes
                .Include(x => x.Courses)
                .ToListAsync();
            return hikes;
        }

        public async Task<Hike> GetById(int id)
        {
            var selectedHike = await context.Hikes
                .Include(hike => hike.Organizer)
                .Include(hike => hike.Courses)
                    .ThenInclude(course => course.CheckPoints)
                .Include(hike => hike.Courses)
                    .ThenInclude(course => course.Registrations)
                .Include(hike => hike.Comments)
                .ThenInclude(comments => comments.Author)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (selectedHike == null)
            {
                throw new ApplicationException("Nem található ilyen azonosítóval túra.");
            }
            return selectedHike;
        }
    }
}
