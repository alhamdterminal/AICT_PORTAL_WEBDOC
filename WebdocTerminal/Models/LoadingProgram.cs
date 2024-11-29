using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("LoadingProgram")]
    public class LoadingProgram : CommonProperties
    {
        public LoadingProgram()
        {
            LoadingProgramDetails = new List<LoadingProgramDetail>();
            //CargoReceiveds = new List<CargoReceived>();
        }
        public long LoadingProgramId { get; set; }

        public DateTime? LoadingProgramDate { get; set; }

        public string LoadingProgramNumber { get; set; }

        public string ReferenceNumber { get; set; }
        public bool? IsgateIn { get; set; }

        public string Destination { get; set; }

        public string InvoiceNumber { get; set; }
        public string FinalDestination { get; set; }
        public string GDNumber { get; set; }

        public DateTime? InvoiceDate { get; set; }
        public DateTime? GateInDate { get; set; }

        public DateTime? CargoCutOff { get; set; }

        public DateTime? VesselETD { get; set; }

        public string LoadingTerminal { get; set; }

        public string ShipperSeal { get; set; }
        public string CLearingAgentReprsentative { get; set; }
        public string ClearingAgentCNIC { get; set; }
        public string DriverName { get; set; }
        public string DriverCNIC { get; set; }
        public DateTime? RecevingStartTime { get; set; }
        public DateTime? RecevingEndTime { get; set; }
        public string CargoRecevingCondition { get; set; }
        public string CargoType { get; set; }
        public bool GDAssociateStatus { get; set; }
        public bool GDSubmitedStatus { get; set; }

        //public string DischargeCountry { get; set; }

        public long? NatureOfTariffId { get; set; }

        public NatureOfTariff NatureOfTariff { get; set; }

        public long? ShippingLineExportId { get; set; }

        public ShippingLineExport ShippingLineExport { get; set; }
          

        public long? ClearingAgentExportId { get; set; }


        //public long? CommodityId { get; set; }

        //public Commodity Commodity { get; set; }

        public string CommodityName { get; set; }

        public ExportCargo ExportCargo { get; set; }

        //public List<CargoReceived> CargoReceiveds { get; set; }

        public ClearingAgentExport ClearingAgentExport { get; set; }

        public long? ShippingAgentExportId { get; set; }

        public ShippingAgentExport ShippingAgentExport { get; set; }

        public long? VesselExportId { get; set; }

        public VesselExport VesselExport { get; set; }

        public long? VoyageExportId { get; set; }

        public VoyageExport VoyageExport { get; set; }
         
        public long? GoodsHeadExportId { get; set; }

        public GoodsHeadExport GoodsHeadExport { get; set; }


        public long? ExportConsigneeId { get; set; }

        public ExportConsignee ExportConsignee { get; set; }

        public long? PortAndTerminalExportId { get; set; }

        public PortAndTerminalExport PortAndTerminalExport { get; set; }

        public List<LoadingProgramDetail> LoadingProgramDetails { get; set; }
         

    }
}
