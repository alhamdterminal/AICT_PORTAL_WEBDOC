using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("EDI_Maersk_APIResponse")]
    public class EDI_Maersk_APIResponse 
    {
        public long EDI_Maersk_APIResponseId { get; set; }
        public string message { get; set; }
        public long messageId { get; set; }
        public string eventTransactionId { get; set; }
        public string code { get; set; }
        public DateTime timestamp { get; set; } = DateTime.Now;
    }
}
