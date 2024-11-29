using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;

namespace WebdocTerminal.Areas.Export.Models
{
    public class RolePermissionViewModel
    {
        public long AppPageId { get; set; }
        public string PageName { get; set; }
        public string PageType { get; set; }
        public long PermissionId { get; set; }
        public bool Create { get; set; }
        public bool Update { get; set; }
        public bool Read { get; set; }

        public List<AppPageItem> PageItem { get; set; }
    }
}
