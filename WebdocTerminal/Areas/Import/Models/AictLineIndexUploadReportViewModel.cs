using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class AictLineIndexUploadReportViewModel
    {
        public string MasterLineName { get; set; }
        public string LineName { get; set; }
        public string ContainerNumber { get; set; }
        public long ContainerIndexId { get; set; }
        public DateTime? ArrivalDate { get; set; }        
        public string Key { get; set; }         
        public string IGMNo { get; set; }
        public int Size { get; set; }
        public int? IndexNo { get; set; }
        public double? HigherCBM { get; set; }

        public double AICTPerCBMRate { get; set; }
        public double AICTPerIndexRate { get; set; }
        public double FFPerIndexRate { get; set; }
        public double FFPerCBMRate { get; set; }

        //Additional Charge
        public double AdditionalChargeAICTPerCBMRate { get; set; }
        public double AdditionalChargeAICTPerIndexRate { get; set; }
        public double AdditionalChargeFFPerIndexRate { get; set; }
        public double AdditionalChargeFFPerCBMRate { get; set; }



        public double TotalAICT { get; set; }
        public double TotalFF { get; set; }


        public double TotalCBMRate { get; set; }
        public double TotalPerIndexRate { get; set; }

        public double TotalCharges { get; set; }

        public double PortCharges { get; set; }
        public double SpecialCharges { get; set; }

        public double StorageCharges { get; set; }
        public double BillToLine { get; set; }
         
        public DateTime? DeliveryDate { get; set; }

        public double Rate20 { get; set; }
        public double Rate40 { get; set; }
        public double Rate45 { get; set; }
        public double PerBoxRate { get; set; }


    }
}
