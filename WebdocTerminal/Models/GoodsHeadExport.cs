using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebdocTerminal.Models
{
    [Table("GoodsHeadExport")]
    public class GoodsHeadExport : CommonProperties
    {
        public long GoodsHeadExportId { get; set; }
        public string GoodsHeadName { get; set; }
        public int StorageFreeDays { get; set; }
    }
}
