using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PreAlertPayOrder")]
    public class PreAlertPayOrder : CommonProperties
    {
        public PreAlertPayOrder()
        {
            PreAlertPayOrderDetails = new List<PreAlertPayOrderDetail>();
        }
        public long PreAlertPayOrderId { get; set; }
        public string PreAlertPayOrderNumber { get; set; }
        public string ChequeNumber { get; set; }
        public string Bvp { get; set; }
        public long BankId { get; set; }        
        public long BankBranchId { get; set; }
        public long RequestNumber { get; set; } 
        public DateTime PayOrderCreatedDate { get; set; } = DateTime.Now;

        public List<PreAlertPayOrderDetail> PreAlertPayOrderDetails { get; set; }

    }
}
