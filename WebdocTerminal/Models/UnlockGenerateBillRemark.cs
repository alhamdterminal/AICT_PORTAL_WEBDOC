using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("UnlockGenerateBillRemark")]
    public class UnlockGenerateBillRemark : CommonProperties
    {
        public long UnlockGenerateBillRemarkId { get; set; }
        public string VirNo { get; set; }
        public string ContainerNo { get; set; }
        public string Remarks { get; set; }
    }
}
