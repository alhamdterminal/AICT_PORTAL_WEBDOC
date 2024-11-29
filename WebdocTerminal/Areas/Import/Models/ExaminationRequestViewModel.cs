using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;

namespace WebdocTerminal.Areas.Import.Models
{
    public class ExaminationRequestViewModel
    {
        public long ExaminationRequestId { get; set; }
        public string LineDoNumber { get; set; }
        public double ExaminationRequestCBM { get; set; }
        public string BECashNo { get; set; }
        public string BLNumber { get; set; }
        public string ConsigneId { get; set; }
        public string ConsigneNTN { get; set; }
        public string STRegistrationNo { get; set; }
        public DateTime PresentationDate { get; set; }
        public string CustomRegNo { get; set; }
        public DateTime CustomRegDate { get; set; }
        public DateTime CustomOutChargeDate { get; set; }
        public string CNIC { get; set; }
        public string PhoneNumber { get; set; }
        public string ClearingAgentRep { get; set; }         
        public bool? IsTPCargo { get; set; }
        public bool? IsEPZCargo { get; set; }
        public DateTime? ExaminationDate { get; set; }
        public DateTime? GroundingDate { get; set; }
        public long? ClearingAgentId { get; set; }
        public ClearingAgent ClearingAgent { get; set; }
        public long? ContainerIndexId { get; set; }

        public ContainerIndex ContainerIndex { get; set; }

        public long? CYContainerId { get; set; }

        public CYContainer CYContainer { get; set; }

        public string Principal { get; set; }
        public string Line { get; set; }
        public string Consignee { get; set; }
        public string Commodity { get; set; }
        public double IgmDetilCBM { get; set; }
        public double DestuffCBM { get; set; }
        public string Email { get; set; }
        public string DeliveryStatus { get; set; }




    }
}
