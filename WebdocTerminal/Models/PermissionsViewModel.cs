using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class PermissionsViewModel : CommonProperties
    {
        public string PageName { get; set; }
        public List<string> Permissions { get; set; }
    }
}
