using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ContainerWeight")]
    public class ContainerWeight : CommonProperties
    {
        public long ContainerWeightId { get; set; }
        public string CHR_NO { get; set; }
        public string CONTAINER_NO { get; set; }
        public DateTime? WEIGHT_DATE { get; set; }
        public string TRUCK_NO { get; set; }
        public long? VEHICLE_WEIGHT { get; set; }
        public long? CONTAINER_WEIGHT { get; set; }
        public string USER_NAME { get; set; }
        public long? EMPTY_CONTAINER_WEIGHT { get; set; }
    }
}
