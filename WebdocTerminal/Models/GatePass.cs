using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GatePass")]
    public class GatePass : CommonProperties
    {
        public GatePass()
        {
            DOItems = new List<DOItem>();
        }
        public long GatePassId { get; set; }

        public DateTime? DODate { get; set; }

        public DateTime DOYear { get; set; }

        public string DONumber { get; set; }
        public string VirNumber { get; set; }
        public int IndexNo { get; set; }

        public string GatePassNumber { get; set; }

        public string UnitType { get; set; }
         

        public string Remarks { get; set; }

        public double TotalPackages { get; set; }

        public double BalancePackages { get; set; }

        public DateTime? GatePassDate { get; set; }

        public double Delivered { get; set; }

        public double TotalDelivered { get; set; }
         
        public string GatePassType { get; set; }
        public string GatePasscontainerNumber { get; set; }
        public string Shift { get; set; }
        public string CargoType { get; set; }
        public long RandDClerkId { get; set; }
        public RandDClerk RandDClerk { get; set; }
        public long DeliveryOrderId { get; set; }

        public DeliveryOrder DeliveryOrder { get; set; }

        public List<DOItem> DOItems { get; set; }




    }
}
