using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("CFSWeight")]
    public class CFSWeight : CommonProperties
    {
        public long CFSWeightId { get; set; }
        public string SNO { get; set; }
        public string CHR_NO { get; set; }
        public string IGM_NO { get; set; }
        public long? INDEX_NO { get; set; }
        public DateTime? WEIGHT_DATE { get; set; }
        public long? PALLET_WEIGHT { get; set; }
        public long? GROSS_WEIGHT { get; set; }
        public long? NET_WEIGHT { get; set; }
        public string WEIGHT_STATUS { get; set; }
        public long? TOTAL_WEIGHT { get; set; }

    }
}
