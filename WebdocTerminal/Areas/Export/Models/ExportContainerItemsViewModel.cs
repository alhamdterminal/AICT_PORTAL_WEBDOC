using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class ExportContainerItemsViewModel
    {
        public long? CargoReceivingInformationId { get; set; }

        public string GDNumber { get; set; }

        public int? NoOfPackages { get; set; }

        public long? ShipperId { get; set; }
        public string ShipperName { get; set; }

        public string AllowLoading { get; set; }

        public string Destination { get; set; }


        public DateTime? PaperReadyDate { get; set; }

        public DateTime? ANFDate { get; set; }
        public string MessageTo { get; set; }

        public long? ExportContainerId { get; set; }

        public long? ExportContainerItemId { get; set; }

        public bool? Ischecked { get; set; }

        public long? ShippingLineId { get; set; }

        public long? OrderNumber { get; set; }

        public string Status { get; set; }

        public long? VesselExportId { get; set; }

        public long? VoyageExportId { get; set; }

        //public string InvoiceNumber { get; set; }
        //public string IsAmountReceived { get; set; }


    }
}
