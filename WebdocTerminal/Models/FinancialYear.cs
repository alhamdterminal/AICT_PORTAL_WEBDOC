using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("FinancialYear")]
    public class FinancialYear : BaseClass
    {
        public long FinancialYearId { get; set; }
        public string Year { get; set; }                 
        public long CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
