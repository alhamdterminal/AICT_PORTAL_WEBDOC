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
    
    public partial class BrtRacksInformation : DevExpress.XtraReports.UI.XtraReport {
        private void InitializeComponent() {
            DevExpress.XtraReports.ReportInitializer reportInitializer = new DevExpress.XtraReports.ReportInitializer(this, "WebdocTerminal.Reports.BrtRacksInformation.repx");

            // Controls
            this.topMarginBand1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.TopMarginBand>("topMarginBand1");
            this.detailBand1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("detailBand1");
            this.bottomMarginBand1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.BottomMarginBand>("bottomMarginBand1");
            this.PageHeader = reportInitializer.GetControl<DevExpress.XtraReports.UI.PageHeaderBand>("PageHeader");
            this.DetailReport = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailReportBand>("DetailReport");
            this.pictureBox1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRPictureBox>("pictureBox1");
            this.label2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label2");
            this.label3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label3");
            this.label12 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label12");
            this.table1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("table1");
            this.tableRow1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("tableRow1");
            this.tableCell1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell1");
            this.tableCell3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell3");
            this.tableCell5 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell5");
            this.tableCell25 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell25");
            this.tableCell2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell2");
            this.tableCell4 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell4");
            this.tableCell6 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell6");
            this.tableCell7 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell7");
            this.tableCell8 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell8");
            this.tableCell9 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell9");
            this.Detail = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("Detail");
            this.table2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("table2");
            this.tableRow2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("tableRow2");
            this.tableCell10 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell10");
            this.tableCell11 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell11");
            this.tableCell12 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell12");
            this.tableCell13 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell13");
            this.tableCell14 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell14");
            this.tableCell15 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell15");
            this.tableCell16 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell16");
            this.tableCell17 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell17");
            this.tableCell18 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell18");
            this.tableCell19 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell19");

            // Parameters
            this.CompanyId = reportInitializer.GetParameter("CompanyId");
            this.path = reportInitializer.GetParameter("path");

            // Data Sources
            this.sqlDataSource1 = reportInitializer.GetDataSource<DevExpress.DataAccess.Sql.SqlDataSource>("sqlDataSource1");
        }
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.DetailBand detailBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.XRPictureBox pictureBox1;
        private DevExpress.XtraReports.UI.XRLabel label2;
        private DevExpress.XtraReports.UI.XRLabel label3;
        private DevExpress.XtraReports.UI.XRLabel label12;
        private DevExpress.XtraReports.UI.XRTable table1;
        private DevExpress.XtraReports.UI.XRTableRow tableRow1;
        private DevExpress.XtraReports.UI.XRTableCell tableCell1;
        private DevExpress.XtraReports.UI.XRTableCell tableCell3;
        private DevExpress.XtraReports.UI.XRTableCell tableCell5;
        private DevExpress.XtraReports.UI.XRTableCell tableCell25;
        private DevExpress.XtraReports.UI.XRTableCell tableCell2;
        private DevExpress.XtraReports.UI.XRTableCell tableCell4;
        private DevExpress.XtraReports.UI.XRTableCell tableCell6;
        private DevExpress.XtraReports.UI.XRTableCell tableCell7;
        private DevExpress.XtraReports.UI.XRTableCell tableCell8;
        private DevExpress.XtraReports.UI.XRTableCell tableCell9;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRTable table2;
        private DevExpress.XtraReports.UI.XRTableRow tableRow2;
        private DevExpress.XtraReports.UI.XRTableCell tableCell10;
        private DevExpress.XtraReports.UI.XRTableCell tableCell11;
        private DevExpress.XtraReports.UI.XRTableCell tableCell12;
        private DevExpress.XtraReports.UI.XRTableCell tableCell13;
        private DevExpress.XtraReports.UI.XRTableCell tableCell14;
        private DevExpress.XtraReports.UI.XRTableCell tableCell15;
        private DevExpress.XtraReports.UI.XRTableCell tableCell16;
        private DevExpress.XtraReports.UI.XRTableCell tableCell17;
        private DevExpress.XtraReports.UI.XRTableCell tableCell18;
        private DevExpress.XtraReports.UI.XRTableCell tableCell19;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.Parameters.Parameter CompanyId;
        private DevExpress.XtraReports.Parameters.Parameter path;
    }
}