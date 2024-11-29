using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("VesselExport")]
    public class VesselExport : CommonProperties
    {
        public VesselExport()
        {
            VoyageExports = new List<VoyageExport>();
            Cargos = new List<Cargo>();
            LoadingPrograms = new List<LoadingProgram>();
            CargoRollOvers = new List<CargoRollOver>();
        }

        public long VesselExportId { get; set; }

        public String VesselName { get; set; }

        //public long? ShippingLineId { get; set; }

        //public ShippingLine ShippingLine { get; set; }


        //public long? ShippingLineExportId { get; set; }

        //public ShippingLineExport ShippingLineExport { get; set; }

        public List<VoyageExport> VoyageExports { get; set; }

        public List<Cargo> Cargos { get; set; }

        public List<LoadingProgram> LoadingPrograms { get; set; }
        public List<CargoRollOver> CargoRollOvers { get; set; }


    }
}
