using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PreAlertPayOrderDetail")]
    public class PreAlertPayOrderDetail : CommonProperties
    {
        public long PreAlertPayOrderDetailId { get; set; }
        public DateTime PreAlertPayOrderDate { get; set; }
        public string ChequeNumber { get; set; }
        public string Bvp { get; set; }  
        public string key { get; set; }
        public string Remarks { get; set; }
        public string ShippingLineName { get; set; }
        public string ShippingAgentName { get; set; }
        public double Amount { get; set; } 
        public bool IsPreAlertPayOrder { get; set; } 
        public long BankId { get; set; }
        public Bank Bank { get; set; }
        public long BankBranchId { get; set; }
        public BankBranch BankBranch { get; set; }
        public long PreAlertPayOrderId { get; set; }
        public PreAlertPayOrder PreAlertPayOrder { get; set; }
    }
}
