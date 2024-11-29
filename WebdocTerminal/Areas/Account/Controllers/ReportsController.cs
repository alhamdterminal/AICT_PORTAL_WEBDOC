using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;
namespace WebdocTerminal.Areas.Account.Controllers
{
    public class ReportsController : ParentController
    {
        private IUsersRepository _userRepo;
        private IDepartmentRepository _departmentRepository;
        private IChartOfAccountRepository _chartOfAccountRepository;
        public ReportsController(IUsersRepository userRepo,
                                 IDepartmentRepository departmentRepository,
                                 IChartOfAccountRepository chartOfAccountRepository)
        {
            _userRepo = userRepo;
            _departmentRepository = departmentRepository;
            _chartOfAccountRepository = chartOfAccountRepository;

        }

        public long? CompanyID { get; set; }
        public string UserName { get; set; }
        public void GetUserId()
        {
            var userIdentity = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userRepo.GetAll().Where(x => x.IdentityUserId == userIdentity).FirstOrDefault();
            UserName = user.FirstName + " - " + user.LastName;
            CompanyID = user.CompanyId;

        }
        public IActionResult BalanceDetail()
        {
            return View();
        }

        public IActionResult ViewBalanceDetail(string tdate,long csid)
        {
            GetUserId();
             var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
      
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.Balancedetails.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["path"].Value = path;
            report.Parameters["date"].Value = tdate;
            report.Parameters["customerid"].Value = csid;
            report.Parameters["companyid"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);

        }

        public IActionResult ChequePrintingLog()
        {
            return View();
        }

        public IActionResult ViewChequePrintingLog(string fromdate, string todate,string partyname)
        {
            GetUserId();
            //var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ChequePrintingLog.repx");
            XtraReport report = XtraReport.FromStream(resource);
            //report.Parameters["path"].Value = path;
            report.Parameters["fromdate"].Value = fromdate;
            report.Parameters["todate"].Value = todate;
            report.Parameters["search"].Value = partyname;
            report.Parameters["companyid"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult AgingWiseBalanceReport()
        {
            return View();
        }

        public IActionResult ViewAgingWiseBalanceReport(string tdate, long csid)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.Agingwisebalancereport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["customerid"].Value = csid;
            report.Parameters["path"].Value = path;
            report.Parameters["date"].Value = tdate;
            report.Parameters["companyid"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ChequePrintReport()
        {
            return View();
        }

        public IActionResult ViewChequePrintReport( decimal amount , string partyname)
        {
            partyname = partyname.Replace("A00A11", "&");
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ChequePrinting.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["PartyName"].Value = partyname;
            report.Parameters["amount"].Value = amount;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult VoucherReport()
        {
            return View();
        }

        public IActionResult ViewVoucherReport(string VoucherId)
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
 
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.Vouchers.repx"); 
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["VoucherId"].Value = VoucherId;
            //report.Parameters["userid"].Value = userid;
            //report.Parameters["status"].Value = status;
            //report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            //report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy");
            //report.Parameters["CompanyId"].Value = CompanyID; 
            //report.Parameters["VoucherNo"].Value = VoucherNo;

 

            report.CreateDocument();
            return PartialView("_Report", report);
        }

         
        public IActionResult TransactionView()
        {
            return View();
        }


        public IActionResult Ledger()
        {

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _userRepo.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            if(users != null && users.CompanyId != null && users.CompanyId != 0)
            {
                ViewData["Department"] = _departmentRepository.GetAll().Where(x => x.CompanyId == users.CompanyId)
                                            .Select(v => new SelectListItem { Text = v.DepartmentCode + "-" + v.DepartmentName, Value = v.DepartmentId.ToString() });
                 
            }


            return View();
        }

        public IActionResult ViewLedger(long? departmentid, string accountid, string fromdate, string todate)
        {
            GetUserId();
            //accountid = "' " + accountid + " '";
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.Ledger.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["departmentid"].Value = departmentid;
            report.Parameters["accountid"].Value = accountid;
            report.Parameters["fromdate"].Value = fromdate;
            report.Parameters["todate"].Value = todate;
            report.Parameters["path"].Value = path;
            report.Parameters["companyid"].Value = CompanyID;
            report.CreateDocument();
            return PartialView("_Report", report);

        }


        public IActionResult CustmerLedger()
        {

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _userRepo.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            if (users != null && users.CompanyId != null && users.CompanyId != 0)
            {
                ViewData["Department"] = _departmentRepository.GetAll().Where(x => x.CompanyId == users.CompanyId)
                                            .Select(v => new SelectListItem { Text = v.DepartmentCode + "-" + v.DepartmentName, Value = v.DepartmentId.ToString() });

            }


            return View();
        }

        public IActionResult ViewCustmerLedger(long? departmentid, string customerid, string fromdate, string todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            //accountid = "' " + accountid + " '";
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CustomerWiseLedger.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["departmentid"].Value = departmentid;
            report.Parameters["Customerid"].Value = customerid;
            report.Parameters["fromdate"].Value = fromdate;
            report.Parameters["todate"].Value = todate;
            report.Parameters["Companyid"].Value = CompanyID;
            report.Parameters["path"].Value = path;

            report.CreateDocument();
            return PartialView("_Report", report);

        }

        public IActionResult TrialBalance()
        {


            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _userRepo.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            if (users != null && users.CompanyId != null && users.CompanyId != 0)
            {
                ViewData["Department"] = _departmentRepository.GetAll().Where(x => x.CompanyId == users.CompanyId)
                                             .Select(v => new SelectListItem { Text = v.DepartmentCode + "-" + v.DepartmentName, Value = v.DepartmentId.ToString() });

            }

            return View();
        }

        public IActionResult ViewTrialBalance(string accountid, string fromdate, string todate , long departmentid)
        {
            GetUserId();

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.TrialBalance.repx");
            XtraReport report = XtraReport.FromStream(resource);

             report.Parameters["accountid"].Value = accountid;
            report.Parameters["sdate"].Value = fromdate;
            report.Parameters["edate"].Value = todate;
            report.Parameters["departmentid"].Value = departmentid;
            report.Parameters["path"].Value = path;
            report.Parameters["companyid"].Value = CompanyID;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult TrialBalanceParties()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _userRepo.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            if (users != null && users.CompanyId != null && users.CompanyId != 0)
            {
                ViewData["Department"] = _departmentRepository.GetAll().Where(x => x.CompanyId == users.CompanyId)
                                             .Select(v => new SelectListItem { Text = v.DepartmentCode + "-" + v.DepartmentName, Value = v.DepartmentId.ToString() });

            }

            return View();
        }

        public IActionResult ViewTrialBalanceParties(string customerid, string fromdate, string todate , long departmentid)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.TrialBalanceParties.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["Customerid"].Value = customerid;
            report.Parameters["fromdate"].Value = fromdate;
            report.Parameters["todate"].Value = todate;
            report.Parameters["departmentid"].Value = departmentid;
            report.Parameters["Companyid"].Value = CompanyID;
            report.Parameters["path"].Value = path;
            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult PartyTrialBalanceDetail()
        {
            return View();
        }

        public IActionResult ViewPartyTrialBalanceDetail(long customerid, long accountid, string IsCredit, string Type, string fromdate, string ToDate)
        {
            GetUserId();

            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.PartyTrialBalanceDetail.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["IsCredit"].Value = IsCredit;
            report.Parameters["Type"].Value = Type;
            report.Parameters["path"].Value = path;
            report.Parameters["customerid"].Value = customerid;
            report.Parameters["accountid"].Value = accountid;
            report.Parameters["fromdate"].Value = fromdate;
            report.Parameters["ToDate"].Value = ToDate;
            report.Parameters["companyid"].Value = CompanyID;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult SingleVoucherView()
        { 
            return View();
        }

        public IActionResult ViewSingleVoucher( string VoucherId)
        {
            GetUserId();
            //accountid = "' " + accountid + " '";
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.SingleVoucher.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["VoucherId"].Value = VoucherId;            
            report.CreateDocument();
            return PartialView("_Report", report);

        }
    }
}
