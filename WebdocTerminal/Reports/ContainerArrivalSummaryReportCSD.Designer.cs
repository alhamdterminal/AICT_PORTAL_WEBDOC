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
    
    public partial class ContainerArrivalSummaryReportCSD : DevExpress.XtraReports.UI.XtraReport {
        private void InitializeComponent() {
            DevExpress.XtraReports.ReportInitializer reportInitializer = new DevExpress.XtraReports.ReportInitializer(this, "WebdocTerminal.Reports.ContainerArrivalSummaryReportCSD.repx");

            // Controls
            this.topMarginBand1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.TopMarginBand>("topMarginBand1");
            this.detailBand1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("detailBand1");
            this.bottomMarginBand1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.BottomMarginBand>("bottomMarginBand1");
            this.label12 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label12");
            this.label13 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label13");
            this.label10 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label10");
            this.label11 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label11");
            this.label9 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label9");
            this.label8 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label8");
            this.label7 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label7");
            this.label6 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label6");
            this.label4 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label4");
            this.label5 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label5");
            this.label3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label3");
            this.label1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label1");
            this.label2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label2");
            this.label16 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label16");
            this.pictureBox1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRPictureBox>("pictureBox1");
            this.pivotGrid1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRPivotGrid>("pivotGrid1");
            this.fieldName1 = reportInitializer.GetPivotGridField("pivotGrid1", "fieldName1");
            this.fieldgateindate1 = reportInitializer.GetPivotGridField("pivotGrid1", "fieldgateindate1");
            this.fields201 = reportInitializer.GetPivotGridField("pivotGrid1", "fields201");
            this.fields401 = reportInitializer.GetPivotGridField("pivotGrid1", "fields401");
            this.fields451 = reportInitializer.GetPivotGridField("pivotGrid1", "fields451");
            this.fieldtues1 = reportInitializer.GetPivotGridField("pivotGrid1", "fieldtues1");

            // Parameters
            this.fromdate = reportInitializer.GetParameter("fromdate");
            this.todate = reportInitializer.GetParameter("todate");
            this.port = reportInitializer.GetParameter("port");
            this.Type = reportInitializer.GetParameter("Type");
            this.Cargo = reportInitializer.GetParameter("Cargo");
            this.commodity = reportInitializer.GetParameter("commodity");

            // Data Sources
            this.sqlDataSource1 = reportInitializer.GetDataSource<DevExpress.DataAccess.Sql.SqlDataSource>("sqlDataSource1");
        }
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.DetailBand detailBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private DevExpress.XtraReports.UI.XRLabel label12;
        private DevExpress.XtraReports.UI.XRLabel label13;
        private DevExpress.XtraReports.UI.XRLabel label10;
        private DevExpress.XtraReports.UI.XRLabel label11;
        private DevExpress.XtraReports.UI.XRLabel label9;
        private DevExpress.XtraReports.UI.XRLabel label8;
        private DevExpress.XtraReports.UI.XRLabel label7;
        private DevExpress.XtraReports.UI.XRLabel label6;
        private DevExpress.XtraReports.UI.XRLabel label4;
        private DevExpress.XtraReports.UI.XRLabel label5;
        private DevExpress.XtraReports.UI.XRLabel label3;
        private DevExpress.XtraReports.UI.XRLabel label1;
        private DevExpress.XtraReports.UI.XRLabel label2;
        private DevExpress.XtraReports.UI.XRLabel label16;
        private DevExpress.XtraReports.UI.XRPictureBox pictureBox1;
        private DevExpress.XtraReports.UI.XRPivotGrid pivotGrid1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldName1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldgateindate1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fields201;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fields401;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fields451;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldtues1;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.Parameters.Parameter fromdate;
        private DevExpress.XtraReports.Parameters.Parameter todate;
        private DevExpress.XtraReports.Parameters.Parameter port;
        private DevExpress.XtraReports.Parameters.Parameter Type;
        private DevExpress.XtraReports.Parameters.Parameter Cargo;
        private DevExpress.XtraReports.Parameters.Parameter commodity;
    }
}
