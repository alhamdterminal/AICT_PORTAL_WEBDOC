using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("DeliveryOrder")]

    public class DeliveryOrder : CommonProperties
    {
        public DeliveryOrder()
        {
            GatePasses = new List<GatePass>();
            DOCodeItems = new List<DOCodeItem>();
        }

        public long DeliveryOrderId { get; set; }

        public DateTime? Date { get; set; }
        public DateTime? ValidTo { get; set; }

        //public string GDNumber { get; set; }
        public string InvoiceNo { get; set; }

        //public string Consingee { get; set; }

        //public string ConsigneeNTN { get; set; }

        //public string CNIC { get; set; }

        //public string SealNo { get; set; }

        //public string Status { get; set; }

        //public int? HoldRelease { get; set; }

        //public int? DetainConfiscate { get; set; }

        ////public int? NoOfPackages { get; set; }
        ////public double? Weight  { get; set; }

        ////public int? TotalPackages { get; set; }

        ////public int? BalancePackages { get; set; }

        ////public int? Delivered { get; set; }

        ////public int? TotalDelivered { get; set; }


        //public int? NumberConfiscated { get; set; }

        public long DONumber { get; set; }
        public string Remarks { get; set; }
         
        public long? ContainerIndexId { get; set; }

        public ContainerIndex ContainerIndex { get; set; }

        public long? CYContainerId { get; set; }
         
        public CYContainer CYContainer { get; set; }

        public List<GatePass> GatePasses { get; set; }
        public List<DOCodeItem> DOCodeItems { get; set; }




    }
}
