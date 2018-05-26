using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Dtos
{
    public class CheckPointDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Open { get; set; }
        public DateTime Close { get; set; }
        public double DistanceFromStart { get; set; }
        public string Description { get; set; }
    }
}
