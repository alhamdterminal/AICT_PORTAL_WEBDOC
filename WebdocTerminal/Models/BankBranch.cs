using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("BankBranch")]
    public class BankBranch : CommonProperties
    {
        public long BankBranchId { get; set; }
        public string BankBranchCode { get; set; }
        public string BankBranchName { get; set; }
    }
}
