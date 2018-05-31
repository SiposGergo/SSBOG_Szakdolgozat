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

        public async Task AddHike(Hike hike)
        {
            var organizer = await context.Hikers.FindAsync(hike.OrganizerId);
            if (organizer == null)
            {
                throw new ApplicationException("A felhasználó nem található!");
            }
            await context.Hikes.AddAsync(hike);
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
