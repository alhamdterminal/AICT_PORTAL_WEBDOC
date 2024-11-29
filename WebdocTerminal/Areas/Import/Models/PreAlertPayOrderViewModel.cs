using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;

namespace WebdocTerminal.Areas.Import.Models
{
    public class PreAlertPayOrderViewModel
    {
        public long PreAlertPayOrderId { get; set; }
        public string Key { get; set; }
        public string PreAlertNumber { get; set; }
        public string ContainerNumber { get; set; }
        public long? Size { get; set; }
        public DateTime PreAlertPayOrderDate { get; set; }
        public double Amount { get; set; } 

        public string Description { get; set; }
        public string ChequeNumber { get; set; }
        public string Bvp { get; set; } 
        public string Remarks { get; set; } 
        public bool IsPreAlertPayOrder { get; set; }
        public string ShippingLineName { get; set; }
        public string ShippingAgentName { get; set; }
        //public long PartyId { get; set; }
        //public Party Party { get; set; }
        //public long BankId { get; set; }
        //public Bank Bank { get; set; }
        //public long BankBranchId { get; set; }
        //public BankBranch BankBranch { get; set; }
        //public long OperationDprtId { get; set; }
        //public OperationDprt OperationDprt { get; set; }

    }
}
