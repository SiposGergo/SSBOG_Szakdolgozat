using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSBO5G__Szakdolgozat.Models
{
    public class Hike
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }

        // Szervező
        public int OrganizerId { get; set; }
        public virtual Hiker Organizer { get; set; }

        // segítők
        public virtual ICollection<HikeHelper> Staff { get; set; }
        // kommentek
        public virtual ICollection<Comment> Comments { get; set; }
        // távok
        public virtual ICollection<HikeCourse> Courses { get; set; }
    }
}
