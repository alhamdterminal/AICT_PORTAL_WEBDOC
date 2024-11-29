using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("VoyageExport")]
    public class VoyageExport : CommonProperties
    {
        public VoyageExport()
        {
            Cargos = new List<Cargo>();

            LoadingPrograms = new List<LoadingProgram>();

            CargoRollOvers = new List<CargoRollOver>();
        }
        public long VoyageExportId { get; set; }

        public string VoyageNumber { get; set; }

        public DateTime ETA { get; set; }

        public DateTime ETD { get; set; }

        public DateTime CutOff { get; set; }

        public string VirNumber{ get; set; }

        public string EgmNumber{ get; set; }

        public long? VesselExportId { get; set; }

        public VesselExport VesselExport { get; set; }

        public long? PortAndTerminalExportId { get; set; }

        public PortAndTerminalExport PortAndTerminalExport { get; set; }

        public List<Cargo> Cargos { get; set; }

        public List<LoadingProgram> LoadingPrograms { get; set; }

        public List<CargoRollOver> CargoRollOvers { get; set; }
         

    }
}
