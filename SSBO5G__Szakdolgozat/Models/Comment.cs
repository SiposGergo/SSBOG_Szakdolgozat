using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string CommentText { get; set; }

        // szerző
        public int AuthorId { get; set; }
        public virtual Hiker Author { get; set; }

        // túra
        public int HikeId { get; set; }
        public virtual Hike Hike { get; set; }
    }
}
