using SSBO5G__Szakdolgozat.Models;
using System;

namespace SSBO5G__Szakdolgozat.Dtos
{
    public class CheckPointPassDto
    {
        public int Id { get; set; }
        public DateTime? TimeStamp { get; set; }
        public TimeSpan? NettoTime { get; set; }
        // Melyik ellenőrzőpont
        public int CheckPointId { get; set; }
        //public CheckPoint CheckPoint { get; set; }

        // melyik teljesítéshez
        public int RegistrationId { get; set; }
        //public Registration Registration { get; set; }
    }
}