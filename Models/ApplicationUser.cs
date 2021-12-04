﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Models {
    public class ApplicationUser : IdentityUser {
        public virtual Student Student { get; set; }
        public virtual Faculty Faculty { get; set; }
    }
}
