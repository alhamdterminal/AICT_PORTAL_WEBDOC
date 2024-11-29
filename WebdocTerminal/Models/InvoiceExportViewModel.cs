using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class InvoiceExportViewModel
    {
        public long EnteringCargoId { get; set; }

        public string GDNumber { get; set; }



        public long InvoiceExportId { get; set; }

        public string BillNo { get; set; }
        public string Vessel { get; set; }
        public string Voyage { get; set; }
        public DateTime? ETD { get; set; }

        public string ShippingAgnet { get; set; }
        public long ClearingAgentId { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public DateTime? GateInDate { get; set; }
        public DateTime? AdvanceDate { get; set; }

        public string Consignee { get; set; }
        public string ConsigneeNTN { get; set; }
        public int StorageDays { get; set; }

        public int AdditionalFreeDays { get; set; }
        public double OtherCharges { get; set; }

        public long WaiverAmount { get; set; }

        public string ChargesName { get; set; }

        public double? Weight { get; set; }


        public bool IsAdvanceBill { get; set; }

        public string DocumentCode { get; set; }
        public bool IsSubBill { get; set; }

        public double? CBM { get; set; }


        public int SubTotal { get; set; }

        public int TotalAmount { get; set; }


        public double StorageTotal { get; set; }

        public int? SalesTax { get; set; }

        public int GrandTotal { get; set; }


        public string PackageType { get; set; }
        public int? NoOfPackages { get; set; }

        public string DishargePort { get; set; }
        public string ShippingAgentName { get; set; }

        public double PreviousBillAmount { get; set; }

        public string ClearingAgentName { get; set; }
        public string ClearingAgentNTN { get; set; }
        public string ClearingAgentNo { get; set; }
        public string CNIC { get; set; }
        public string AgentRepName { get; set; }
        public string PhoneNumber { get; set; }


    }
}
