//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebdocTerminal.Reports {
    
    public partial class GlobelLinkForAllPIFFASOASReport : DevExpress.XtraReports.UI.XtraReport {
        private void InitializeComponent() {
            DevExpress.XtraReports.ReportInitializer reportInitializer = new DevExpress.XtraReports.ReportInitializer(this, "WebdocTerminal.Reports.GlobelLinkForAllPIFFASOASReport.repx");

            // Controls
            this.TopMargin = reportInitializer.GetControl<DevExpress.XtraReports.UI.TopMarginBand>("TopMargin");
            this.BottomMargin = reportInitializer.GetControl<DevExpress.XtraReports.UI.BottomMarginBand>("BottomMargin");
            this.Detail = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("Detail");
            this.pictureBox1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRPictureBox>("pictureBox1");
            this.label2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label2");
            this.label3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label3");
            this.label10 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label10");
            this.label26 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label26");
            this.label7 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label7");
            this.label13 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label13");
            this.label25 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label25");
            this.label24 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label24");
            this.label23 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label23");
            this.label21 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label21");
            this.label22 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label22");
            this.label19 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label19");
            this.label20 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label20");
            this.label18 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label18");
            this.label17 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label17");
            this.label16 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label16");
            this.label14 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label14");
            this.label15 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label15");
            this.label12 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label12");
            this.label9 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label9");
            this.label11 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label11");
            this.label8 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label8");
            this.label5 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label5");
            this.label6 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label6");
            this.label4 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label4");
            this.label1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label1");
            this.pivotGrid1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRPivotGrid>("pivotGrid1");
            this.fieldDate1 = reportInitializer.GetPivotGridField("pivotGrid1", "fieldDate1");
            this.fieldClearingAgentName1 = reportInitializer.GetPivotGridField("pivotGrid1", "fieldClearingAgentName1");
            this.fieldGDNumber1 = reportInitializer.GetPivotGridField("pivotGrid1", "fieldGDNumber1");
            this.fieldCBM1 = reportInitializer.GetPivotGridField("pivotGrid1", "fieldCBM1");
            this.fieldTariffHeadExport1 = reportInitializer.GetPivotGridField("pivotGrid1", "fieldTariffHeadExport1");
            this.fieldTOTALCBM1 = reportInitializer.GetPivotGridField("pivotGrid1", "fieldTOTALCBM1");
            this.fieldVesselName1 = reportInitializer.GetPivotGridField("pivotGrid1", "fieldVesselName1");
            this.pivotGridField1 = reportInitializer.GetPivotGridField("pivotGrid1", "pivotGridField1");
            this.pivotGridField2 = reportInitializer.GetPivotGridField("pivotGrid1", "pivotGridField2");
            this.pivotGridField3 = reportInitializer.GetPivotGridField("pivotGrid1", "pivotGridField3");
            this.pivotGridField4 = reportInitializer.GetPivotGridField("pivotGrid1", "pivotGridField4");

            // Parameters
            this.fromDate = reportInitializer.GetParameter("fromDate");
            this.toDate = reportInitializer.GetParameter("toDate");
            this.year = reportInitializer.GetParameter("year");
            this.month = reportInitializer.GetParameter("month");
            this.shippingagent = reportInitializer.GetParameter("shippingagent");
            this.Vessel = reportInitializer.GetParameter("Vessel");
            this.voyage = reportInitializer.GetParameter("voyage");
            this.tax = reportInitializer.GetParameter("tax");
            this.shareofBayWest20 = reportInitializer.GetParameter("shareofBayWest20");
            this.shareofBayWest40 = reportInitializer.GetParameter("shareofBayWest40");
            this.adjustment = reportInitializer.GetParameter("adjustment");
            this.CompanyId = reportInitializer.GetParameter("CompanyId");
            this.path = reportInitializer.GetParameter("path");

            // Data Sources
            this.sqlDataSource1 = reportInitializer.GetDataSource<DevExpress.DataAccess.Sql.SqlDataSource>("sqlDataSource1");

            // Calculated Fields
            this.calculatedField1 = reportInitializer.GetCalculatedField("calculatedField1");
        }
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRPictureBox pictureBox1;
        private DevExpress.XtraReports.UI.XRLabel label2;
        private DevExpress.XtraReports.UI.XRLabel label3;
        private DevExpress.XtraReports.UI.XRLabel label10;
        private DevExpress.XtraReports.UI.XRLabel label26;
        private DevExpress.XtraReports.UI.XRLabel label7;
        private DevExpress.XtraReports.UI.XRLabel label13;
        private DevExpress.XtraReports.UI.XRLabel label25;
        private DevExpress.XtraReports.UI.XRLabel label24;
        private DevExpress.XtraReports.UI.XRLabel label23;
        private DevExpress.XtraReports.UI.XRLabel label21;
        private DevExpress.XtraReports.UI.XRLabel label22;
        private DevExpress.XtraReports.UI.XRLabel label19;
        private DevExpress.XtraReports.UI.XRLabel label20;
        private DevExpress.XtraReports.UI.XRLabel label18;
        private DevExpress.XtraReports.UI.XRLabel label17;
        private DevExpress.XtraReports.UI.XRLabel label16;
        private DevExpress.XtraReports.UI.XRLabel label14;
        private DevExpress.XtraReports.UI.XRLabel label15;
        private DevExpress.XtraReports.UI.XRLabel label12;
        private DevExpress.XtraReports.UI.XRLabel label9;
        private DevExpress.XtraReports.UI.XRLabel label11;
        private DevExpress.XtraReports.UI.XRLabel label8;
        private DevExpress.XtraReports.UI.XRLabel label5;
        private DevExpress.XtraReports.UI.XRLabel label6;
        private DevExpress.XtraReports.UI.XRLabel label4;
        private DevExpress.XtraReports.UI.XRLabel label1;
        private DevExpress.XtraReports.UI.XRPivotGrid pivotGrid1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDate1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldClearingAgentName1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldGDNumber1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldCBM1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldTariffHeadExport1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldTOTALCBM1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldVesselName1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField pivotGridField1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField pivotGridField2;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField pivotGridField3;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.CalculatedField calculatedField1;
        private DevExpress.XtraReports.Parameters.Parameter fromDate;
        private DevExpress.XtraReports.Parameters.Parameter toDate;
        private DevExpress.XtraReports.Parameters.Parameter year;
        private DevExpress.XtraReports.Parameters.Parameter month;
        private DevExpress.XtraReports.Parameters.Parameter shippingagent;
        private DevExpress.XtraReports.Parameters.Parameter Vessel;
        private DevExpress.XtraReports.Parameters.Parameter voyage;
        private DevExpress.XtraReports.Parameters.Parameter tax;
        private DevExpress.XtraReports.Parameters.Parameter shareofBayWest20;
        private DevExpress.XtraReports.Parameters.Parameter shareofBayWest40;
        private DevExpress.XtraReports.Parameters.Parameter adjustment;
        private DevExpress.XtraReports.Parameters.Parameter CompanyId;
        private DevExpress.XtraReports.Parameters.Parameter path;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField pivotGridField4;
    }
}
