using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // több a többhöz kapcsolat definiálása kapcsolótáblával
            modelBuilder.Entity<HikeHelper>().HasKey(x => new { x.HikeId, x.HikerId });

            modelBuilder.Entity<HikeHelper>()
                .HasOne(x => x.Hike)
                .WithMany(x => x.Staff)
                .HasForeignKey(x => x.HikeId);

            modelBuilder.Entity<HikeHelper>()
               .HasOne(x => x.Hiker)
               .WithMany(x => x.HelpedHikes)
               .HasForeignKey(x => x.HikerId);
        }

        public DbSet<Hiker> Hikers { get; set; }
        public DbSet<Hike> Hikes { get; set; }
        public DbSet<HikeHelper> HikeHelpers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<HikeCourse> Courses { get; set; }
        public DbSet<CheckPoint> CheckPoints { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<CheckPointPass> CheckPointPasses { get; set; }
    }
}
