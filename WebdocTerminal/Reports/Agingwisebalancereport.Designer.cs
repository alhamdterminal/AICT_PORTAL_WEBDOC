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
    
    public partial class Agingwisebalancereport : DevExpress.XtraReports.UI.XtraReport {
        private void InitializeComponent() {
            DevExpress.XtraReports.ReportInitializer reportInitializer = new DevExpress.XtraReports.ReportInitializer(this, "WebdocTerminal.Reports.Agingwisebalancereport.repx");

            // Controls
            this.topMarginBand1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.TopMarginBand>("topMarginBand1");
            this.detailBand1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("detailBand1");
            this.bottomMarginBand1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.BottomMarginBand>("bottomMarginBand1");
            this.PageHeader = reportInitializer.GetControl<DevExpress.XtraReports.UI.PageHeaderBand>("PageHeader");
            this.DetailReport = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailReportBand>("DetailReport");
            this.label3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label3");
            this.pageInfo1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRPageInfo>("pageInfo1");
            this.label4 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label4");
            this.label1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label1");
            this.label2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label2");
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
            this.Detail = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("Detail");
            this.table2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("table2");
            this.tableRow2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("tableRow2");
            this.tableCell9 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell9");
            this.tableCell10 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell10");
            this.tableCell11 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell11");
            this.tableCell12 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell12");
            this.tableCell13 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell13");
            this.tableCell14 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell14");
            this.tableCell15 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell15");
            this.tableCell16 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell16");
            this.tableCell17 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell17");
            this.tableCell27 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell27");
            this.tableCell26 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell26");
            this.tableCell24 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell24");
            this.tableCell23 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell23");
            this.tableCell22 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell22");
            this.tableCell21 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell21");
            this.tableCell20 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell20");
            this.tableCell19 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell19");
            this.tableCell18 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell18");
            this.tableRow3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("tableRow3");
            this.table3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("table3");
            this.ReportFooter = reportInitializer.GetControl<DevExpress.XtraReports.UI.ReportFooterBand>("ReportFooter");

            // Parameters
            this.date = reportInitializer.GetParameter("date");
            this.companyid = reportInitializer.GetParameter("companyid");
            this.customerid = reportInitializer.GetParameter("customerid");
            this.path = reportInitializer.GetParameter("path");

            // Data Sources
            this.sqlDataSource1 = reportInitializer.GetDataSource<DevExpress.DataAccess.Sql.SqlDataSource>("sqlDataSource1");
        }
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.DetailBand detailBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.XRLabel label3;
        private DevExpress.XtraReports.UI.XRPageInfo pageInfo1;
        private DevExpress.XtraReports.UI.XRLabel label4;
        private DevExpress.XtraReports.UI.XRLabel label1;
        private DevExpress.XtraReports.UI.XRLabel label2;
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
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRTable table2;
        private DevExpress.XtraReports.UI.XRTableRow tableRow2;
        private DevExpress.XtraReports.UI.XRTableCell tableCell9;
        private DevExpress.XtraReports.UI.XRTableCell tableCell10;
        private DevExpress.XtraReports.UI.XRTableCell tableCell11;
        private DevExpress.XtraReports.UI.XRTableCell tableCell12;
        private DevExpress.XtraReports.UI.XRTableCell tableCell13;
        private DevExpress.XtraReports.UI.XRTableCell tableCell14;
        private DevExpress.XtraReports.UI.XRTableCell tableCell15;
        private DevExpress.XtraReports.UI.XRTableCell tableCell16;
        private DevExpress.XtraReports.UI.XRTableCell tableCell17;
        private DevExpress.XtraReports.Parameters.Parameter date;
        private DevExpress.XtraReports.Parameters.Parameter companyid;
        private DevExpress.XtraReports.Parameters.Parameter customerid;
        private DevExpress.XtraReports.Parameters.Parameter path;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.XRTableCell tableCell27;
        private DevExpress.XtraReports.UI.XRTableCell tableCell26;
        private DevExpress.XtraReports.UI.XRTableCell tableCell24;
        private DevExpress.XtraReports.UI.XRTableCell tableCell23;
        private DevExpress.XtraReports.UI.XRTableCell tableCell22;
        private DevExpress.XtraReports.UI.XRTableCell tableCell21;
        private DevExpress.XtraReports.UI.XRTableCell tableCell20;
        private DevExpress.XtraReports.UI.XRTableCell tableCell19;
        private DevExpress.XtraReports.UI.XRTableCell tableCell18;
        private DevExpress.XtraReports.UI.XRTableRow tableRow3;
        private DevExpress.XtraReports.UI.XRTable table3;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
    }
}