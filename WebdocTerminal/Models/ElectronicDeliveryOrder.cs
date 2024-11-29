using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
	[Table("ElectronicDeliveryOrder")]
    public class ElectronicDeliveryOrder : CommonProperties
    {

        public long ElectronicDeliveryOrderId { get; set; }
        public string Ref_No { get; set; }
        public string Doc_Tran_No { get; set; }
        public DateTime? Do_Date { get; set; }
        public DateTime? Valid_Date { get; set; }
        public string Agent_Name { get; set; }
        public string IGM_No { get; set; }
        public string Index_No { get; set; }
        public string BL_No { get; set; }
        public string Container_No { get; set; }
        public string CONT_Size { get; set; }
        public string Vessel_Name { get; set; }
        public string Voyage { get; set; }
        public double NetWeight { get; set; }
        public string Port_of_Arrival { get; set; }
        public string Marks_No { get; set; }
        public string No_Of_Pkgs { get; set; }
        public double Gross_Weight { get; set; }
        public string PKg_Description { get; set; }
        public string Goods_Desc { get; set; }
        public int? TotalRecords { get; set; }
        public int? RecordSequence { get; set; }
        public string ShippingLineCode { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeNTN { get; set; }
        public string ConsigneeAddress { get; set; }
        public string AgentsChalNo { get; set; }
        public string PackagesUnit { get; set; }
        public string OpType { get; set; }
        public string Performedby { get; set; }
        public DateTime? PerformedDate { get; set; } = DateTime.Now;
        public string FileName { get; set; }
         public bool Oracle { get; set; }
 
    }
}
