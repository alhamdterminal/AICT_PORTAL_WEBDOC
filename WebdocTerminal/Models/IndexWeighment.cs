using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("IndexWeighment")]
    public class IndexWeighment : CommonProperties
    {
        public long IndexWeighmentId { get; set; }
        public string VirNo { get; set; }
        public int Indexno { get; set; }
        public string Remarks { get; set; }
        public double PalletWeight { get; set; }
        public double IndexWeight { get; set; }
        public double Weight { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsMarkComplete { get; set; }
        public long Sequence { get; set; }

    }
}
