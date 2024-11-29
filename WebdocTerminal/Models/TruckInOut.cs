using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TruckInOut")]
    public class TruckInOut : CommonProperties
    {

        public long TruckInOutId { get; set; }
        public string TruckNo { get; set; }
        public string ContainerNumber { get; set; }
        public string Type { get; set; }
        public string VirNumber { get; set; }
        public long IndexNumber { get; set; }
        public bool TIP { get; set; }
        public DateTime? TruckInDate { get; set; }
        public DateTime? TruckEntryDate { get; set; }
        public bool TruckOut { get; set; }
        public string Status { get; set; }
        public DateTime? TruckOutDate { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
