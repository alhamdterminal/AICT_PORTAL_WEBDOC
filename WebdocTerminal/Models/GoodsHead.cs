using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GoodsHead")]
    public class GoodsHead : CommonProperties
    {
        public long GoodsHeadId { get; set; }
        public string GoodsHeadName { get; set; }
        public int StorageFreeDays { get; set; }
        public string AllowAutoGrounding { get; set; } 
        public string AllowAutoGroundingLCL { get; set; } 
        public string AllowAutoGroundingFCL { get; set; } 
    }
}
