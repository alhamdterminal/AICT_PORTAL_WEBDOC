using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("VoucherServiceType")]
    public class VoucherServiceType  : BaseClass
    {
        public VoucherServiceType()
        {
            VoucherServiceTypeDetails = new List<VoucherServiceTypeDetail>();
        }

        public long VoucherServiceTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public long CompanyId { get; set; }
        public List<VoucherServiceTypeDetail> VoucherServiceTypeDetails { get; set; }

    }
}
