using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("AccountNature")]
    public class AccountNature : BaseClass
    {
        public long AccountNatureId { get; set; }
        public string Code { get;  set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
