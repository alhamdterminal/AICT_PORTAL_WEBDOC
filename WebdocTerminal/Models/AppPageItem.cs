using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("AppPageItem")]
    public class AppPageItem : CommonProperties
    {
        public long AppPageItemId { get; set; }
        public string FieldName { get; set; }
        public bool? IsChecked { get; set; }
        public string RoleId { get; set; }
        public IdentityRole Role { get; set; }
        public long AppPageId { get; set; }
        public AppPage AppPage { get; set; }

    }
}
