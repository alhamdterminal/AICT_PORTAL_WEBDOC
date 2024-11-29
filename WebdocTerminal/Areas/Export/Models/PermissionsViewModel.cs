using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class PermissionsViewModel
    {
        public string Page { get; set; }
        public List<String> Permissions { get; set; }
    }
}
