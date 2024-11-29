using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ClearingAgentExport")]
    public class ClearingAgentExport : CommonProperties
    {
        //public ClearingAgentExport()
        //{
        //    LoadingPrograms = new List<LoadingProgram>();
        //    EnteringCargos = new List<EnteringCargo>();
        //    CargoReceiveds = new List<CargoReceived>();
        //    InvoiceExports = new List<InvoiceExport>();
        // }
        public long ClearingAgentExportId { get; set; }

        public string ChallanNumber { get; set; }

        public string ClearingAgentName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        //public List<LoadingProgram> LoadingPrograms { get; set; }

        //public List<EnteringCargo> EnteringCargos { get; set; }

        //public List<CargoReceived> CargoReceiveds { get; set; }

        //public List<InvoiceExport>  InvoiceExports { get; set; }




    }
}
