using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Permission")]
    public class Permission : CommonProperties
    {
        [Key]
        public long PermissionId { get; set; }
        public string FormUrl { get; set; }
        public bool Read { get; set; }
        public bool Create { get; set; }
        public bool Update { get; set; }
        public string RoleId { get; set; }
        public IdentityRole Role { get; set; }
        public long? AppPageId { get; set; }
        public AppPage AppPage { get; set; }
    }
}
