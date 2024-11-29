using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ShortExcessWeigth")]
    public class ShortExcessWeigth : CommonProperties
    {
        public long ShortExcessWeigthId { get; set; }
        public string ContainerType { get; set; }
        public string VirNumber { get; set; }
        public long IndexNumber { get; set; }
        public double ShortWeight { get; set; }
        public double ExcesstWeight { get; set; }
        public string Remarks { get; set; }
    }
}
