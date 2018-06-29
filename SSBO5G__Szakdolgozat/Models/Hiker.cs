using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SSBO5G__Szakdolgozat.Models
{
    public enum GenderTypes { Male, Female }
    public class Hiker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderTypes Gender { get; set; }
        public string Town { get; set; }
        public string PhoneNumber { get; set; }

        public bool mustChangePassword { get; set; }

        // Auth-hoz
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        // Szervezett túrák ==> főszervező
        public virtual ICollection<Hike> OrganizedHikes { get; set; }

        // ahol segítő
        public virtual ICollection<HikeHelper> HelpedHikes { get; set; }

        // kommentek
        public virtual ICollection<Comment> Coments { get; set; }

        //előnevezések
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
