using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("CYContainer")]
    public class CYContainer : CommonProperties
    {
        public CYContainer()
        {
            ContainerIndexTariffs = new List<ContainerIndexTariff>();
            DisabledAgentTariffCYs = new List<DisabledAgentTariffCY>(); 
            //Holds = new List<Hold>();
            DeliveryOrders = new List<DeliveryOrder>();
        }

        public long CYContainerId { get; set; }

        public string VesselName { get; set; }

        public string IGMYear { get; set; }

        public string VIRNo { get; set; }

        public string VoyageNo { get; set; }

        public DateTime? BerthOn { get; set; }

        public string BerthAt { get; set; }

        public string ContainerNo { get; set; }

        public double? ContainerGrossWeight { get; set; }

        public int Size { get; set; }

        public string Status { get; set; }

        public string ISOCode { get; set; }

        public string ManifestedSealNumber { get; set; }

        public int NoOfPackages { get; set; }

        public string PackageType { get; set; }

        public string BLNo { get; set; }

        public int? IndexNo { get; set; }

        public double? BLGrossWeight { get; set; }

        public string Description { get; set; }

        public string MarksAndNumber { get; set; }

        public string ShippingLine { get; set; }
        public bool? IsCrossStuffed { get; set; }

        public bool? IsDestuffed { get; set; }

        public string AuctionLotNo { get; set; } 
        public long  AuctionLotNoSequence { get; set; }

        public string RO { get; set; }

        public double? CBM { get; set; } 
        public double? AmountSize20 { get; set; } 
        public double? AmountSize40 { get; set; } 
        public double? AmountSize45 { get; set; } 
        public bool? IsGrounded { get; set; }

        public bool? IsWeigment { get; set; }

        public bool? IsGateIn { get; set; }

        public bool? IsGateOut { get; set; }

        public bool IsDelivered { get; set; }


        public bool? IsHold { get; set; }

        public bool? IsAuction { get; set; }
 
        public bool? InvoiceLocked { get; set; }

        public int? AdditionalFreeDays { get; set; }

        public string ConsigneeName { get; set; }
        public string ConsigneeNTN { get; set; }
        public bool? IsWeighed { get; set; }
        public bool? IsPartialContainer { get; set; }
        public DateTime? CYContainerGateInDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? CYContainerGateOutDate { get; set; }

        public string ContainerLocation { get; set; }


        //  public bool? IsDeleted { get; set; }

        public string CSContainerNumber { get; set; }
        public string CSSize { get; set; }
        public string CSVehicleNumer { get; set; }
        public string CSCondition { get; set; }
        public double CSTireWeight { get; set; }
        public DateTime? CSReceivedDate { get; set; }
        public string CSCountryCode { get; set; }
        public bool IsCSEmtyptyRecieved { get; set; }

        public long? EmptyReceivedShippingLineId { get; set; }
        public string ContainerStatus { get; set; }

        public double? OtherCharges { get; set; }

        //     public DeliveryOrder DeliveryOrder { get; set; }
        public List<DeliveryOrder> DeliveryOrders { get; set; }


        public GatePass OrderDetail { get; set; }

        public Auction Auction { get; set; }

        public GateOut GateOut { get; set; }

       // public Hold Hold { get; set; }

        public List<ContainerIndexTariff> ContainerIndexTariffs { get; set; } 
        public List<DisabledAgentTariffCY> DisabledAgentTariffCYs { get; set; } 
        //public List<Hold> Holds { get; set; }

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

        public PortAndTerminal PortAndTerminal { get; set; }

        public string CargoType { get; set; }

        public bool IsDGCargo { get; set; }


        public long? PortOfLoadingId { get; set; }

        public PortOfLoading PortOfLoading { get; set; }

        public long? CSEmptyContainerReceiveId { get; set; }

        public CSEmptyContainerReceive CSEmptyContainerReceive { get; set; }

    }
}
