using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public int StartNumber { get; set; }
        public int HikeCourseId { get; set; }
        public int HikerId { get; set; }
        public Hiker Hiker { get; set; }
        public HikeCourse HikeCourse { get; set; }
    }
}
