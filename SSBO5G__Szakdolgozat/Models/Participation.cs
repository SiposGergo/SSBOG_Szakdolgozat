using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Models
{
    public class Participation
    {
        public int Id { get; set; }
        public ICollection<CheckPointPass> Passes { get; set; }
        public int CourseId { get; set; }
        public HikeCourse Course { get; set; }
        public int HikerId { get; set; }
        public Hiker Hiker { get; set; }
    }
}
