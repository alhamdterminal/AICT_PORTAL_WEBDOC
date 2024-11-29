using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PreAlert")]
    public class PreAlert : CommonProperties
    {
        public PreAlert()
        {
            PreAlterDetails = new List<PreAlterDetail>();
        }
        public long PreAlertId { get; set; }
        public string PreAlertNumber { get; set; }
        public string Vessel { get; set; }
        public string Voyage { get; set; }
        public bool AlertStatus { get; set; }
        public DateTime PreAlertDate { get; set; }
        public long ShippingAgentLineId { get; set; }
        public long  ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }

        public long PreAlertVesselId { get; set; }
        public PreAlertVessel PreAlertVessel { get; set; }

        //public long VesselId { get; set; }
        //public Vessel Vessel { get; set; }
        //public long VoyageId { get; set; }
        //public Voyage Voyage { get; set; }
        public long PortAndTerminalId { get; set; }
        public PortAndTerminal PortAndTerminal { get; set; }
        public DateTime ETADate { get; set; }
        public List<PreAlterDetail> PreAlterDetails { get; set; }



    }
}
