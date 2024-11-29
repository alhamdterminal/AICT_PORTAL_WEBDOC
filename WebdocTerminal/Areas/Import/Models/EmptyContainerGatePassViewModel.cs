using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class EmptyContainerGatePassViewModel
    {

        public long EmptyContainerGatePassId { get; set; }
        public string VirNumber { get; set; }
        public string ContainerNumber { get; set; }
        public string BLNumber { get; set; }
        public long Size { get; set; }
        public long ContainerIndexId { get; set; }
        public string ShiftedYard { get; set; }
        public string ContainerCondition { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsChecked { get; set; }



    }
}
