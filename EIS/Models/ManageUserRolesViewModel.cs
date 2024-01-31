using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EIS.Models
{
    public class ManageUserRolesViewModel
    {
        [Key]
        [Display(Name = "Id", ResourceType = typeof(Resources.Resource))]
        public string RoleId { get; set; }
        [Display(Name = "RoleName", ResourceType = typeof(Resources.Resource))]
        public string RoleName { get; set; }
        [Display(Name = "Selected", ResourceType = typeof(Resources.Resource))]
        public bool Selected { get; set; }
    }
}
