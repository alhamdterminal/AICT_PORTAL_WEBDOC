using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class CRShortSummaryViewModel
    {
        public long srno { get; set; }
        public DateTime srinvoicedate { get; set; }
        public string LineName { get; set; }
        public string ContainerNo { get; set; }
        public string GateInDate { get; set; }
        public string PortName { get; set; }
        public string VirNo { get; set; }
        public int IndexNo { get; set; }
        public string containerdescription { get; set; }
        public string VesselName { get; set; }
        public string Voyage { get; set; }
        public string InvoiceNo { get; set; }
        public string BillType { get; set; }
        public string InvoiceDate { get; set; }
        public string CfsCy { get; set; }
        public string CargoType { get; set; }
        public string GoodsHeadName { get; set; }
        public string BLNo { get; set; }
        public string ConsigneName { get; set; }
        public string ClearingAgentName { get; set; }
        public string DestuffDate { get; set; }
        public double TotalCharges { get; set; }
        public int Qty20 { get; set; }
        public int Qty40 { get; set; }
        public int Qty45 { get; set; }
        public int TotalArrivedCBM { get; set; }
        public int TotalArrivedIndexes { get; set; }
        public int? TotalDays { get; set; }
        public int FreeDays { get; set; }
        public int StorageDays { get; set; }
        public double BTLAmount { get; set; }
        public string BTLRemarks { get; set; }
        public double DLVCBMWt { get; set; }
        public double Auction { get; set; }
        public double ExaminationCharges { get; set; }
        public double SealCharge { get; set; }
        public double PortCharges { get; set; }
        public double SpecialHandlingCharges { get; set; }
        public double Storage { get; set; }
        public double TariffRate { get; set; }
        public double FuelAdjcharges { get; set; }
        public double FuelAdjustmentcharges { get; set; }
        public double AdditionalFUELCAF { get; set; }
        public double DocumentCharges { get; set; }
        public double DocumentionCharges { get; set; }
        public double Document { get; set; }
        public double ScannerDetention { get; set; }
        public double ScanningQ { get; set; }
        public double ShifftingChargesQ { get; set; }
        public double CargoHandling { get; set; }
        public double CFSCharges { get; set; }
        public double CurrencyAdjustment { get; set; }
        public double CustomerCharges { get; set; }
        public double CustomsSealCharges { get; set; }
        public double DataProcessiong { get; set; }
        public double DestuffingCharges { get; set; }
        public double DoubleHaulageCharges { get; set; }
        public double Examination { get; set; }
        public double GateInCharges { get; set; }
        public double GeneralCargoExamination { get; set; }
        public double HandlingCharges { get; set; }
        public double InsuranceCharges { get; set; }
        public double LCLCharges { get; set; }
        public double MeasurementAndSurvey { get; set; }
        public double MeasurementCharges { get; set; }
        public double PortTariffCharges { get; set; }
        public double QictRAndD { get; set; }
        public double RecevialDelivery { get; set; }
        public double ServiceChargesDestuffing { get; set; }
        public double ShiftingCharges { get; set; }
        public double SpecialCharges { get; set; }
        public double SurveyCharges { get; set; }
        public double TerminalCharges { get; set; }
        public double TransPortationDeliveryCharges { get; set; }
        public double Weighment { get; set; }
        public double WeighmentCharges { get; set; }
        public double Wharfage { get; set; }
        public double YardCharges { get; set; }
        public double AICTPerCBMRate { get; set; }
        public double AICTPerIndexRate { get; set; }
        public double FFPerCBMRate { get; set; }
        public double FFPerIndexRate { get; set; }
        public double AICTPerCBMRateShareRate { get; set; }
        public double AICTPerIndexRateShareRate { get; set; }
        public double FFPerCBMRateShareRate { get; set; }
        public double FFPerIndexRateShareRate { get; set; }
        public double PerBoxRate { get; set; }
        public double otherchargesParty { get; set; }
        public double otherchargesFFLine { get; set; }
        public double otherchargesAict { get; set; }
        public double otherchargesClearingAgent { get; set; }
        public double StorageWaiver { get; set; }
        public double OtherWaiver { get; set; }
        public double TotalWaiver { get; set; }
    }
}
