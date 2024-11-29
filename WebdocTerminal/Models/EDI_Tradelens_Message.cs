using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("EDI_Tradelens_Message")]
    public class EDI_Tradelens_Message
    {
        public long EDI_Tradelens_MessageId { get; set; } // MESSAGE_ID (Primary key)
        public long? CONTAINER_ID { get; set; } // CONTAINER_ID
        public string EVENT_ID { get; set; } // EVENT_ID (length: 25)
        public long? SHIPPING_LINE_ID { get; set; } // SHIPPING_LINE_ID
        public string MAERSKPARTNERID { get; set; } // MAERSKPARTNERID (length: 25)
        public string VOYAGE_NO { get; set; } // VOYAGE_NO (length: 25)
        public string VESSEL_CODE { get; set; } // VESSEL_CODE (length: 50)
        public string CONTAINER_NO { get; set; } // CONTAINER_NO (length: 250)
        public string CN_ISO_CODE { get; set; } // CN_ISO_CODE (length: 250)
        public string BL_NO { get; set; } // BL_NO (length: 250)
        public string GATE_IN_DATE { get; set; } // GATE_IN_DATE (length: 25)
        public string GATE_OUT_DATE { get; set; } // GATE_OUT_DATE (length: 25)
        public string DESTUFF_DATE { get; set; } // DESTUFF_DATE (length: 25)
        public decimal? BL_GROSS_WEIGHT { get; set; } // BL_GROSS_WEIGHT
        public string MAN_SEAL_NO { get; set; } // MAN_SEAL_NO (length: 250)
        public string REMARKS { get; set; } // REMARKS (length: 250)
        public string TRUCK_NO { get; set; } // TRUCK_NO (length: 25)
        public long? CONTAINER_COUNT { get; set; } // CONTAINER_COUNT
        public long? SEGMENT_NO { get; set; } // SEGMENT_NO
        public string SEQUENCE_NO { get; set; } // SEQUENCE_NO (length: 25)
        public string MESSAGE_SEND_DATETIME { get; set; } // MESSAGE_SEND_DATETIME (length: 25)
        public string CARRIER_ID { get; set; } // CARRIER_ID (length: 25)
        public string VESSEL_CALL_SIGN { get; set; } // VESSEL_CALL_SIGN (length: 25)
        public string CONTAINER_TYPE { get; set; } // CONTAINER_TYPE (length: 25)
        public bool? IS_MARSK { get; set; } = false; // IS_MARSK
        public string CONTAINER_STATUS { get; set; } // CONTAINER_STATUS (length: 25)
        public string CONTAINER_HISTORY_DATE { get; set; } // CONTAINER_HISTORY_DATE (length: 25)
        public bool? IS_PROCESSED { get; set; } = false; // IS_PROCESSED
        public string PROCESSING_COMMENTS { get; set; } // PROCESSING_COMMENTS (length: 100)
        public string CONTAINER_LINE_CODE { get; set; }
    }
}
