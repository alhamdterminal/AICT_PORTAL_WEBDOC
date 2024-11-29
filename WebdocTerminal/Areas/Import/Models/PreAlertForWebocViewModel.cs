using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class PreAlertForWebocViewModel
    {
        public long PreAlterDetailId { get; set; }
        public string ContainerNo { get; set; }
        public string MasterBLNumber { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        public string CargoType { get; set; }
        public string LineFFName { get; set; }
        public string SOCCOC { get; set; }
        public string PortAndTerminalName { get; set; }
        public DateTime PreAlertDate { get; set; }
        public string VirNumber { get; set; }
        public bool IsCheck { get; set; }


    }
}
