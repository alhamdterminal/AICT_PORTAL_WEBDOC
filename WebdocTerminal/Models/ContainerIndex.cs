using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ContainerIndex")]
    public class ContainerIndex : CommonProperties
    {
        public ContainerIndex()
        {
            ContainerIndexTariffs = new List<ContainerIndexTariff>();
            DisabledAgentTariffs = new List<DisabledAgentTariff>();
            //Holds = new List<Hold>();
            DeliveryOrders = new List<DeliveryOrder>();
        }
        public long ContainerIndexId { get; set; }

        public string ContainerNo { get; set; }

        public double? ContainerGrossWeight { get; set; }

        public double? FoundWeight { get; set; }

        public double ManifestedWeight { get; set; }

        public int Size { get; set; }

        public string Status { get; set; }

        public string ISOCode { get; set; }

        public string ManifestedSealNumber { get; set; }

        public int NoOfPackages { get; set; }

        public string PackageType { get; set; }

        public bool IsEmptyOut { get; set; }

        public bool IsGateIn { get; set; }

        public bool? IsEmptyGateOut { get; set; } = false;

        public bool IsGrounded { get; set; }

        public bool IsAuction { get; set; }
        public string RO { get; set; }
        public bool IsHold { get; set; }

       // public bool? IsDeleted { get; set; }

        public string BLNo { get; set; }

        public int? IndexNo { get; set; }

        public double? BLGrossWeight { get; set; }

        public string Description { get; set; }

        public string MarksAndNumber { get; set; }

        public string ShippingLine { get; set; }

        public bool IsDestuffed { get; set; }

        public string AuctionLotNo { get; set; }
        public long AuctionLotNoSequence { get; set; }

        public string VirNo { get; set; }

        public double CBM { get; set; }
        public double MeasurementCBM { get; set; }
        public double ExaminationRequestCBM { get; set; }
        public double FFCBM { get; set; }

        public bool IsGateOut { get; set; }
        public bool IsDelivered { get; set; }

        public bool InvoiceLocked { get; set; }

        public int AdditionalFreeDays { get; set; }

        public bool IsWeighed { get; set; }

        public string ConsigneeName { get; set; }
        public string ConsigneeNTN { get; set; }

        public DateTime? CFSContainerGateInDate { get; set; }
        public DateTime? CFSContainerGateOutDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public string ContainerLocation { get; set; }

        public double OtherCharges { get; set; }

        //public DeliveryOrder DeliveryOrder { get; set; }

        public List<DeliveryOrder> DeliveryOrders { get; set; }


        public GatePass OrderDetail { get; set; }

        public DestuffedContainer DestuffedContainer { get; set; }

        public Auction Auction { get; set; }

        public GateOut GateOut { get; set; }

      //  public Hold Hold { get; set; }

        public Weighment Weighment { get; set; }

        public BRT BRT { get; set; }

        public List<ContainerIndexTariff> ContainerIndexTariffs { get; set; }
        public List<DisabledAgentTariff> DisabledAgentTariffs { get; set; }
        //public List<Hold> Holds { get; set; }

        public long? VoyageId { get; set; }

        public Voyage Voyage { get; set; }

        public GroundedContainer GroundedContainer { get; set; }

        public long? ShippingAgentId { get; set; }

        public ShippingAgent ShippingAgent { get; set; }


        public long? TransporterId { get; set; }

        public Transporter Transporter { get; set; }
         
        public long? GoodsHeadId { get; set; }
        public GoodsHead GoodsHead { get; set; }
         
        public long? ConsigneId { get; set; }

        public Consigne Consigne { get; set; }

        public long? ShippingLineId { get; set; }

        public ShippingLine Shipping { get; set; }

        public long? ShipperId { get; set; }

        public Shipper Shipper { get; set; }

        public long? PortAndTerminalId { get; set; }

        public PortAndTerminal PortAndTerminal  { get; set; }

        public string CargoType { get; set; }

        public bool? IsPartialContainer { get; set; }

        public bool IsDGCargo { get; set; }

        public long? PortOfLoadingId { get; set; }
        public PortOfLoading PortOfLoading { get; set; }

        public bool IsPortChargesAssign { get; set; }

    }
}
