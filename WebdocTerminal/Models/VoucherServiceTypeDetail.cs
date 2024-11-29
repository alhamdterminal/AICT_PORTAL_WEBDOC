using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("VoucherServiceTypeDetail")]
    public class VoucherServiceTypeDetail : CommonProperties
    {
        public long VoucherServiceTypeDetailId { get; set; }
        public long VoucherServiceTypeId { get; set; }
        public VoucherServiceType VoucherServiceType { get; set; }

        public long ChartOfAccountId { get; set; }
        public ChartOfAccount ChartOfAccount { get; set; }
    }
}
