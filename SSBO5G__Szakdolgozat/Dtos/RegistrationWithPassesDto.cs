using SSBO5G__Szakdolgozat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Dtos
{
    public class RegistrationWithPassesDto
    {
        public int Id { get; set; }
        public int HikerId { get; set; }
        public BaseHiker Hiker { get; set; }
        public string StartNumber { get; set; }
        public ICollection<CheckPointPassDto> Passes { get; set; }
        public double AvgSpeed { get; set; }
    }
}
