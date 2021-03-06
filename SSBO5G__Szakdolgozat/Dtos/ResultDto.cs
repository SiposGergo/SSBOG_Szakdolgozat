﻿using SSBO5G__Szakdolgozat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Dtos
{
    public class ResultDto
    {
        public ICollection<CheckPointDto> Checkpoints { get; set; }
        public ICollection<RegistrationWithPassesDto> Registrations { get; set; }
        public TimeSpan LimitTime { get; set; }
    }
}
