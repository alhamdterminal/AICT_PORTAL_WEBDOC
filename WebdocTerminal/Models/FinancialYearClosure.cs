using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("FinancialYearClosure")]
    public class FinancialYearClosure : BaseClass
    {
        public long FinancialYearClosureId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public long FinancialYearId { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
