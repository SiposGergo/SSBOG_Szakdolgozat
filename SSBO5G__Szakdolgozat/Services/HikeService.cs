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
    }
    public class HikeService : IHikeService
    {
        ApplicationContext context;
        public HikeService(ApplicationContext context)
        {
            this.context = context;
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
