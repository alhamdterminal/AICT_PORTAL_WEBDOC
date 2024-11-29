using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("HoldPrivilege")]
    public class HoldPrivilege : CommonProperties
    {
        public long HoldPrivilegeId { get; set; }
        public string HoldType { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
    }
}
