using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class ContainerInfoViewModel
    {

        public string CashRegNoCBE { get; set; }
        public string CashRegDateCBE { get; set; }
        public string CustomRegNo { get; set; }
        public string CustomHouse { get; set; }
        public string ClearingAgent { get; set; }
        public string RegNoConsignee { get; set; }
        public string ChlicenceNo { get; set; }
        public string AgentRep { get; set; }
        public string Nicno { get; set; }
        public string CustomPerNo { get; set; }
        public string LineName { get; set; }
        public string Vessel { get; set; }
        public long? AictDoNo { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string Voyage { get; set; }
        public string MarksNo { get; set; }
        public string GoodsDesc { get; set; }
        public string ContainerNo { get; set; }
        public string Cycfs { get; set; }
        public int Size { get; set; }
        public int Packages { get; set; }
                   
    }
}
