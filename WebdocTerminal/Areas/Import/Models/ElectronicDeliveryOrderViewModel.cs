﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class ElectronicDeliveryOrderViewModel
    {

        public long ElectronicDeliveryOrderId { get; set; }
        public string Key { get; set; }
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
        public bool Oracle { get; set; }
        public bool IsEdo { get; set; }

    }
}