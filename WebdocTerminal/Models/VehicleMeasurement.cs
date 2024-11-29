using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("VehicleMeasurement")]
    public class VehicleMeasurement : CommonProperties
    {
        public long VehicleMeasurementId { get; set; }
        public string Model { get; set; }
        public string VehicleName { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public double VehicleMeasurementCBM { get; set; }
    }
}
