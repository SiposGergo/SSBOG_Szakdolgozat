using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Models
{
    // Kapcsoló tábla több a többhöz kapcsolathoz
    public class HikeHelper
    {
        public int HikerId { get; set; }
        public Hiker Hiker { get; set; }
        public int HikeId { get; set; }
        public Hike Hike { get; set; }
    }
}
