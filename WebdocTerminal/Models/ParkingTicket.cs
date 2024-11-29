using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ParkingTicket")]
    public class ParkingTicket : CommonProperties
    {
        public long ParkingTicketId { get; set; }
        public string ParkingTicketNumber { get; set; }
        public string TruckNo { get; set; }
        public string TruckingCompany { get; set; }
        public string DriverName { get; set; }
        public string DriverPhoneNo { get; set; }
        public DateTime? EnteryDate { get; set; } = DateTime.Now;
    }
}
