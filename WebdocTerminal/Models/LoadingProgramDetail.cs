using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("LoadingProgramDetail")]
    public class LoadingProgramDetail : CommonProperties
    {
        public long LoadingProgramDetailId { get; set; }

        public string PONumber { get; set; }

        public string Style { get; set; }

        public int? TotalPackages { get; set; }

        public string PackageType { get; set; }

        public int? TotalPieces { get; set; }

        public DateTime? INSDate { get; set; }

        public DateTime? DOCSDate { get; set; }

        //public string FinalDestination { get; set; }
        //public string DishargePort { get; set; }
        public string PLIDNumber { get; set; }
        public string WarehouseLocation { get; set; }
        public double? CBM { get; set; }
        public double? Weight { get; set; }
        public string Location { get; set; }
        public string ColorCode { get; set; }

        public string Remarks { get; set; }


        public long? LoadingProgramId { get; set; }

        public LoadingProgram LoadingProgram { get; set; }

        //public Cargo Cargo { get; set; }


    }
}
