using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class CFSAndCYCargoInformationViewModel
    {
        public string VirNumber { get; set; }
        public string ContainerNumber  { get; set; }
        public string BLNumber  { get; set; }
        public int? IndexNumber  { get; set; }
        public int? ManifestedQTY { get; set; }
        public string GoodsDesc { get; set; }
        public string CRNumber { get; set; }
        public string DONumber { get; set; }
        public DateTime? VesselArrival { get; set; }
        public string Line { get; set; }
        public string VesselName { get; set; }
        public string VoyageNumber { get; set; }
        public DateTime? IGMDate { get; set; }
        public DateTime? ArrivalInAICT { get; set; }
        public string TruckNumber { get; set; }
        public string CFSCY { get; set; }
        public string LCLFCL { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public DateTime? DestuffingDate { get; set; }
        public DateTime? TruckOut { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? EmptyOffHireDate { get; set; }
        public string BRTShed { get; set; }
        public string ConsigneeName { get; set; }
        public string ClearingAgent { get; set; }
        public string LineDONumber { get; set; }
        public string DisPackages { get; set; }
        public int? FoundQTY { get; set; }
        public DateTime? ExaminationDate { get; set; }
        public DateTime? GatePassDate { get; set; }
        public bool IsExamin { get; set; }
        public string GatePassNumber { get; set; }
        public string ReleaseStatus { get; set; }
        public int ActualQty { get; set; }
        public string DeliveryTime { get; set; }
        public double BalanceQty { get; set; }
        public string GoodsType { get; set; }
        public string IsDGCargo { get; set; }
    }
}
