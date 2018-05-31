using SSBO5G__Szakdolgozat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Dtos
{
    public class HikerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderTypes Gender { get; set; }
        public string Town { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public virtual ICollection<RegistrationDto> Registrations { get; set; }
        public virtual ICollection<BaseHike> OrganizedHikes { get; set; }
    }

}
