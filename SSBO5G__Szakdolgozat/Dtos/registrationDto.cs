using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Dtos
{
    public class RegistrationDto
    {
        public int Id { get; set; }
        public int HikeCourseId { get; set; }
        public int HikerId { get; set; }
        public BaseHiker Hiker { get; set; }
        public BaseHikeCourse HikeCourse { get; set; }
        public string StartNumber { get; set; }
    }
}
