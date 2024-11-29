using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ImportContainerLocation")]
    public class ImportContainerLocation : CommonProperties
    {
        public long ImportContainerLocationId { get; set; }
        public string Virno { get; set; }
        public string ContainerNo { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
    }
}
