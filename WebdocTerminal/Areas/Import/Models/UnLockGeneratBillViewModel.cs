using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class UnLockGeneratBillViewModel
    {
        public string VIRNo { get; set; } 
        public string ShippingAgent { get; set; }
        public string ShippingLine { get; set; }
        public string PortName { get; set; }
        public string ContainerNo { get; set; }
        public string GoodName { get; set; }
        public int ContainerSize { get; set; }
        public DateTime? GateInDate { get; set; }
        public string Key { get; set; }
        public string CargoType { get; set; } 
        public string Status { get; set; } 
        public bool InvoiceLocked { get; set; }
        public string UpdationDate { get; set; }
        public long CountofIndexes { get; set; }
        public string Remarks { get; set; }

    }
}
