using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Models
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }
        public string StartNumber { get; set; }
        public int HikeCourseId { get; set; }
        public int HikerId { get; set; }
        public Hiker Hiker { get; set; }
        public HikeCourse HikeCourse { get; set; }

        public virtual IList<CheckPointPass> Passes { get; set; }
        public double AvgSpeed { get; set; }
    }
}
