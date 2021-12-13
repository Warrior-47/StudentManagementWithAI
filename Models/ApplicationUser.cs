using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudentManagementWithAI.Models {
    public class ApplicationUser : IdentityUser {

        [Display(Name = "Photo Name")]
        public string PhotoName { get; set; }
        public virtual Student Student { get; set; }
        public virtual Faculty Faculty { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType) {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = new ClaimsIdentity(await manager.GetClaimsAsync(this), authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
