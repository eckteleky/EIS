using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EIS.Models
{
    public class UserRolesViewModel
    {
        [Key]
        [Display(Name = "Id", ResourceType = typeof(Resources.Resource))]
        public string UserId { get; set; }
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resource))]
        public string FirstName { get; set; }
        [Display(Name = "LastName", ResourceType = typeof(Resources.Resource))]
        public string LastName { get; set; }
        [Display(Name = "UserName", ResourceType = typeof(Resources.Resource))]
        public string UserName { get; set; }
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
