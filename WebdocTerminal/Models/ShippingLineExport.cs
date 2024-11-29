using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ShippingLineExport")]
    public class ShippingLineExport : CommonProperties
    {

        public ShippingLineExport()
        { 
            LoadingPrograms = new List<LoadingProgram>();
            ExportContainers = new List<ExportContainer>(); 
        }



        public long ShippingLineExportId { get; set; }

        public string ShippingLineName { get; set; }
        public string ShippingLineCode { get; set; }
         
        public string NTNNumber { get; set; }

        public List<LoadingProgram> LoadingPrograms { get; set; }
        public List<ExportContainer> ExportContainers { get; set; }
    }
}
