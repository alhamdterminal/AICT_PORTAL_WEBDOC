using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class UsersViewModel : CommonProperties
    {
        public long UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }

        public string CNIC { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage ="Please Enter New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage ="Confirmation Password is required")]
        [Compare("NewPassword",ErrorMessage ="New Password and Confirmation Password must match")]
        public string ReNewPassword { get; set; }
    }
}
