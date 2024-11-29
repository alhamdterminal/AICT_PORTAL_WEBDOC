using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("AppPage")]
    public class AppPage : CommonProperties
    {
        public long AppPageId { get; set; }
        public string PageName { get; set; }
        public string PageUrl { get; set; }
        public string PageType { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
