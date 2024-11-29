using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ACK")]
    public class ACK : CommonProperties
    {
        public long ACKId { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public string MessageId { get; set; }

        public string FileName { get; set; }

        public string Status { get; set; }

        public string FileErrorCode { get; set; }

        public string VIRNo { get; set; }

        public string ContainerNo { get; set; }

        public int RecordNo { get; set; }

        public int RecordErrorCode { get; set; }

        public DateTime Performed { get; set; }

        public string Direction { get; set; }

        public DateTime? CreateDT { get; set; } = DateTime.Now;
    }

}
