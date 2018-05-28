using SSBO5G__Szakdolgozat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Dtos
{
    public class HikeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }

        public virtual BaseHiker Organizer { get; set; }

        public virtual ICollection<CommentDto> Comments { get; set; }
        public virtual ICollection<HikeCourseDto> Courses { get; set; }
    }
}
