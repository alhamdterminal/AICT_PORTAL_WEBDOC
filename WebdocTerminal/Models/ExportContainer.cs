using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class ExportContainer : CommonProperties
    {
        public ExportContainer()
        {
            Cargos = new List<Cargo>();
            ExportContainerItems = new List<ExportContainerItem>();

        }

        public long ExportContainerId { get; set; }

        public string ContainerNumber { get; set; }

        public string ContainerSize { get; set; }

        public string Status { get; set; }

        public string CRONumber { get; set; }

        public string GDNumber { get; set; }

        //public string POD { get; set; }
        public string EmptyReceivedFromYard { get; set; }

        public string VehicleNumber { get; set; }

        public string ClearingAgentName { get; set; }

        public int? NumberofPackages { get; set; }

        public string PackageType { get; set; }

        public int? GrossWeight { get; set; }

        public bool? ExaminationArranged { get; set; }

        public bool? ContainerAssociated { get; set; }

        public string GatePassNumber { get; set; }

        public DateTime? IssueDate { get; set; }

        public bool? IsGateout { get; set; }
        public bool? IsAssociated { get; set; }

        //  public Boolean? IsDeleted { get; set; }

        public long? ShippingLineExportId { get; set; }

        public ShippingLineExport ShippingLineExport { get; set; }


        public DateTime? RecevingDate { get; set; }
        public double? ContainerTareWeight { get; set; }

        public string ContainerCondition { get; set; }

        //public string VehicleNumber { get; set; }

        public long? ShippingAgentExportId { get; set; }

        //public long? TransporterId { get; set; }

        public ShippingAgentExport ShippingAgentExport { get; set; }
         
        public List<Cargo> Cargos { get; set; }

        public List<ExportContainerItem> ExportContainerItems { get; set; }
     
    }
}
