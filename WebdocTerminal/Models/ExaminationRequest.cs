using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ExaminationRequest")]
    public class ExaminationRequest :  CommonProperties
    {
        public long ExaminationRequestId { get; set; }

        public string LineDoNumber { get; set; }
        public double ExaminationRequestCBM { get; set; }
        public string BECashNo { get; set; }
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
        public string Email { get; set; }
        public string DeliveryStatus { get; set; }

        public long? ClearingAgentId { get; set; }

        public ClearingAgent ClearingAgent { get; set; }

        public long? ContainerIndexId { get; set; }

        public ContainerIndex ContainerIndex { get; set; }

        public long? CYContainerId { get; set; }

        public CYContainer CYContainer { get; set; }

    }
}
