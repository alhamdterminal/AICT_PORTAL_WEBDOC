using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.AuthTradelens
{
    public class ActualStripping
    {
        public string originatorName { get; set; }
        public string originatorId { get; set; }
        public string eventSubmissionTime8601 { get; set; }
        public string transportEquipmentId { get; set; }
        public object transportEquipmentRef { get; set; }
        public object equipmentNumber { get; set; }
        public object carrierBookingNumber { get; set; }
        public object billOfLadingNumber { get; set; }
        public string eventOccurrenceTime8601 { get; set; }
        public string transportationPhase { get; set; }
        public Location location { get; set; }
        public string fullStatus { get; set; }
        public object terminal { get; set; }
    }
}
