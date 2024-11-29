using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PreAlterDetail")]

    public class PreAlterDetail : CommonProperties
    {
        public long PreAlterDetailId { get; set; }
        public string VoyageNumber { get; set; }
        public string Vessel { get; set; }
        public DateTime ETADate { get; set; }
        public string ContainerNumber { get; set; }
        public string MasterBLNumber { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        public string CargoType { get; set; }
        public string SOCCOC { get; set; }
        public string Remarks { get; set; }
        public long ShippingLineId { get; set; }
        public ShippingLine ShippingLine { get; set; }
        public string PortAndTerminalName { get; set; }
        public string VirNumber { get; set; }
        public DateTime CreatedDate { get; set; }

        //public long PortAndTerminalId { get; set; }
        //public PortAndTerminal PortAndTerminal { get; set; }u8

        //public DateTime EirReceivedDate { get; set; }
        //public DateTime OffHiredDate { get; set; }
        //public DateTime EirHandOverDate { get; set; }
        //public DateTime NocDate { get; set; }

        public long? PreAlertId { get; set; }
        public PreAlert PreAlert { get; set; }
    }
}
