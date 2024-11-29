using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;
 

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class ReportsController : ParentController
    {
        private IDictionaryRepository _dictionaryRepository;
        private IVesselExportRepository _vesselExportRepo;
        private IVoyageExportRepository _voyageExportRepo;
        private IExportContainerRepository _exportContainerRepo;
        private IPortAndTerminalRepository _portAndTerminalRepo;
        private IShippingAgentExportRepository _shippingAgentExportRepo;
        private IShipperRepository _shipperRepo;
        private IClearingAgentExportRepository _clearingAgentExportRepo;
        private ICargoRepository _cargoRepo;
        private IPartyExportRepository _partyExportRepo;
        private IBankRepository _bankRepository;
        private IEmptyReceivingRepository _emptyReceivingRepo;
        private IChequeRecivedExportRepository _chequeRecivedExportRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private IUsersRepository _userRepo;
        private WebocProcessor _webocProcessor;
        private IHostingEnvironment env;
        private IPortAndTerminalExportRepository   _portAndTerminalExportRepository;
        private IDeliveredYardExportRepository _deliveredYardExportRepository;
        private IPartyExportRepository _partyExportRepository;
        private IConfiguration Configuration;


        public ReportsController(IDictionaryRepository dictionaryRepository 
                                , IVesselExportRepository vesselExportRepo
                                , IVoyageExportRepository voyageExportRepo
                                , IExportContainerRepository exportContainerRepo
                                , IPortAndTerminalRepository portAndTerminalRepo
                                , IShippingAgentExportRepository shippingAgentExportRepo
                                , IClearingAgentExportRepository clearingAgentExportRepo
                                , IShipperRepository shipperRepo
                                , ICargoRepository cargoRepo
                                , IPartyExportRepository partyExportRepo
                                , IEmptyReceivingRepository emptyReceivingRepo
                                , IBankRepository bankRepository
                                , IChequeRecivedExportRepository chequeRecivedExportRepository
                                , UserManager<IdentityUser> userManager
                                , WebocProcessor webocProcessor
                                ,IHostingEnvironment _env
                                , IUsersRepository userRepo
                                , IPortAndTerminalExportRepository portAndTerminalExportRepository
                                , IDeliveredYardExportRepository deliveredYardExportRepository
                                , IPartyExportRepository partyExportRepository
                                , IConfiguration _configuration)
        {
            _dictionaryRepository = dictionaryRepository;
            _vesselExportRepo = vesselExportRepo;
            _voyageExportRepo = voyageExportRepo;
            _exportContainerRepo = exportContainerRepo;
            _portAndTerminalRepo = portAndTerminalRepo;
            _shippingAgentExportRepo = shippingAgentExportRepo;
            _clearingAgentExportRepo = clearingAgentExportRepo;
            _shipperRepo = shipperRepo;
            _cargoRepo = cargoRepo;
            _partyExportRepo = partyExportRepo;
            _emptyReceivingRepo = emptyReceivingRepo;
            _bankRepository = bankRepository;
            _chequeRecivedExportRepository = chequeRecivedExportRepository;
            _userManager = userManager;
            _userRepo = userRepo;
            _webocProcessor = webocProcessor;
            env = _env;
            _portAndTerminalExportRepository = portAndTerminalExportRepository;
            _deliveredYardExportRepository = deliveredYardExportRepository;
            _partyExportRepository = partyExportRepository;

            Configuration = _configuration;

            var newglobalConnectionStrings = Configuration.GetSection("ConnectionStrings").AsEnumerable(true)
             .ToDictionary(x => x.Key, x =>  x.Value);

            DevExpress.DataAccess.DefaultConnectionStringProvider.AssignConnectionStrings(newglobalConnectionStrings);

        }
        public long? CompanyID { get; set; }

        public long? ShippingAgentExportId { get; set; }


        public void GetUserId()
        {
             var userIdentity = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userRepo.GetAll().Where(x => x.IdentityUserId == userIdentity).FirstOrDefault();
            CompanyID = user.CompanyId;

            if (user.ShippingAgentExportId != null)
            {
                ShippingAgentExportId = user.ShippingAgentExportId;
            }
          
        }

        public IActionResult CargoDropoffTicket()
        {
            return View();
        }

        public IActionResult ViewCargoDropoffTicket(string lpNumber  )
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CargoDropoffTicket.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["lpNumber"].Value = lpNumber;
             report.Parameters["CompanyId"].Value = CompanyID;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ExrtTellySheet()
        {
            return View();
        }

        public IActionResult ViewExportTellySheet(string lpNumber)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExportTellySheet.repx");
            XtraReport report = XtraReport.FromStream(resource); 
            report.Parameters["lpNumber"].Value = lpNumber;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult CargoInterchangeReceipt()
        {
            return View();
        }

        public IActionResult ViewCargoInterchangeReceipt(  string lpNumber)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CargoInterchangeReceipt.repx");
            XtraReport report = XtraReport.FromStream(resource); 
            report.Parameters["lpNumber"].Value = lpNumber;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ExportBillInvoice()
        {
            return View();
        }

        public IActionResult ViewExportBillInvoice(string BillNo)
        {
            try
            {
                GetUserId();
                var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                var assembly = typeof(ReportStorageWebExtension1).Assembly;
                Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExportInvoice.repx");
                XtraReport report = XtraReport.FromStream(resource);
                report.Parameters["BillNo"].Value = BillNo;

                report.Parameters["path"].Value = path;
                report.Parameters["CompanyId"].Value = CompanyID;

                report.CreateDocument();
                return PartialView("_Report", report);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public IActionResult ExportDeletedInvoices()
        {
            return View();
        }

        public IActionResult ViewExportDeletedInvoices(DateTime? fromDate, DateTime? toDate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DeletedInvoiceExport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy"); ;
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy"); ;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult AnfCargoCheckList()
        {

            ViewData["VesselExports"] = _vesselExportRepo.GetAll().Select(x => new SelectListItem { Text = x.VesselName, Value = x.VesselExportId.ToString() });

            return View();
        }


        public IActionResult ViewAnfCargoCheckList(DateTime? fromDate, DateTime? toDate, long? vesselexport)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ANFCargoChecklist.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["vessel"].Value = vesselexport;
            //report.Parameters["voyage"].Value = voyageExport;
            //report.Parameters["portAndTerminal"].Value = portAndTerminal;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult AnfReport()
        {

            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            ViewData["ClearingAgent"] = _clearingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ClearingAgentName, Value = x.ClearingAgentExportId.ToString() });

            ViewData["Shipper"] = _shipperRepo.GetAll().Select(x => new SelectListItem { Text = x.ShipperName, Value = x.ShipperId.ToString() });

            ViewData["PortAndTerminal"] = _portAndTerminalExportRepository.GetAll()
                .Select(x => new SelectListItem { Text = x.PortName, Value = x.PortAndTerminalExportId.ToString() });

            ViewData["EGM"] = _voyageExportRepo.GetAll().Select(x => new SelectListItem { Text = x.EgmNumber, Value = x.VoyageExportId.ToString() });

            return View();
        }


        public IActionResult ViewAnfReport(DateTime? fromDate, DateTime? toDate, long vesselExport, long voyageExport,
                                           long shippingAgent, long clearingAgent, long shipper, long portAndTerminal, long egm)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ANFReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["vessel"].Value = vesselExport;
            report.Parameters["voyage"].Value = voyageExport;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["clearingAgent"].Value = clearingAgent;
            report.Parameters["shipper"].Value = shipper;
            report.Parameters["portAndTerminal"].Value = portAndTerminal;
            report.Parameters["egm"].Value = egm;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult APLDailyCFSReport()
        {
            return View();
        }


        public IActionResult ViewAPLDailyCFSReport(DateTime? fromDate, DateTime? toDate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.APLDailyCFSReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult CargoAlongsideReportBaywise()
        {

            ViewData["EgmNumber"] = _voyageExportRepo.GetAll().Select(x => new SelectListItem { Text = x.EgmNumber, Value = x.VoyageExportId.ToString() });
            GetUserId();
            if (ShippingAgentExportId != null)
            {
                ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Where(x=> x.ShippingAgentExportId == ShippingAgentExportId).Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            }
            else
            {
                ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            }


            ViewData["ClearingAgent"] = _clearingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ClearingAgentName, Value = x.ClearingAgentExportId.ToString() });

            ViewData["Shipper"] = _shipperRepo.GetAll().Select(x => new SelectListItem { Text = x.ShipperName, Value = x.ShipperId.ToString() });

            return View();
        }


        public IActionResult ViewCargoAlongsideReportBaywise(DateTime? fromDate, DateTime? toDate , long vesselExport, long voyageExport
                                                            , long egmNumber, long shippingAgent, long clearingAgent, long shipper
                                                            , long cbm  , string orientation)
        {
            GetUserId();
           
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = orientation == "Vessel" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.CargoAlongsideReportBaywise.repx") : assembly.GetManifestResourceStream("WebdocTerminal.Reports.CargoAlongsideReportBaywiseForAgentWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["vessel"].Value = vesselExport;
            report.Parameters["voyage"].Value = voyageExport;
            report.Parameters["egm"].Value = egmNumber;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["clearingAgent"].Value = clearingAgent;
            report.Parameters["shipper"].Value = shipper;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["cbm"].Value = cbm;
            report.CreateDocument();
            return PartialView("_Report", report);
        }


         public IActionResult CargoAlongsideReport()
        {
            GetUserId();
            ViewData["EgmNumber"] = _voyageExportRepo.GetAll().Select(x => new SelectListItem { Text = x.EgmNumber, Value = x.VoyageExportId.ToString() });

            if (ShippingAgentExportId != null)
            {
                ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Where(x=> x.ShippingAgentExportId == ShippingAgentExportId).Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });
            }
            else
            {
                ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            }

             
            ViewData["ClearingAgent"] = _clearingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ClearingAgentName, Value = x.ClearingAgentExportId.ToString() });

            ViewData["Shipper"] = _shipperRepo.GetAll().Select(x => new SelectListItem { Text = x.ShipperName, Value = x.ShipperId.ToString() });

            return View();
        }

        public void SendEmailToAgent(long Id , string path)
        { 

            //var agent = _shippingAgentExportRepo.GetAll().Where(x => x.ShippingAgentExportId == Id).LastOrDefault();
            //_webocProcessor.sendReportToAgent(agent.Email , path);
        }


        public IActionResult ViewCargoAlongsideReport(DateTime? fromDate , DateTime? toDate , long vesselExport , long voyageExport, long egmNumber, long shippingAgent, long clearingAgent, long shipper
                                                     , long cbm , string orientation)
        {

            GetUserId();
            if (ShippingAgentExportId != null)
            {
                shippingAgent = ShippingAgentExportId ?? 0;
            }
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = orientation == "Vessel" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.CargoAlongsideReport.repx") : assembly.GetManifestResourceStream("WebdocTerminal.Reports.CargoAlongsideReportAgentWise.repx");

            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["vessel"].Value = vesselExport;
            report.Parameters["voyage"].Value = voyageExport;
            report.Parameters["egm"].Value = egmNumber;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["clearingAgent"].Value = clearingAgent;
            report.Parameters["shipper"].Value = shipper;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["cbm"].Value = cbm;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult DailyDSRReport()
        {
            return View();
        }


        public IActionResult ViewDailyDSRReport(DateTime? fromdate, DateTime? todate, long vesselExport, long voyageExport)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DailyDSR.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["vessel"].Value = vesselExport;
            report.Parameters["voyage"].Value = voyageExport;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }



        public IActionResult CargoMEasurementCertificate(  )
        {
           
            return View();

        }


        public IActionResult ViewCargoMEasurementCertificate(string gdnumber, string date)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CargoMeasurementCertificate.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["gdnumber"].Value = gdnumber;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ExportAdvice()
        {
            return View();
        }


        public IActionResult ViewExportAdvice(string containerNumber, long vesselExport, long voyageExport)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExportAdvice.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["containerNumber"].Value = containerNumber;
            report.Parameters["vessel"].Value = vesselExport;
            report.Parameters["voyage"].Value = voyageExport;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult KerryExport()
        {
            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            return View();
        }


        public IActionResult ViewKerryExport(DateTime? fromdate, DateTime? todate, long vesselExport, long voyageExport , long shippingAgent
                                            , string ContainerNumber, string orientation)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = orientation == "Vessel" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.KerryExportReport.repx") : assembly.GetManifestResourceStream("WebdocTerminal.Reports.KerryExportReportForAgentWise.repx");

            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromDate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["vessel"].Value = vesselExport;
            report.Parameters["voyage"].Value = voyageExport;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["containerNo"].Value = ContainerNumber;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ExportGatePass()
        {
          
            return View();
        }


        public IActionResult ViewExportGatePass(string containerNumber)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExportGatepass.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["containerNumber"].Value = containerNumber;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ExportGatePasses()
        {

            return View();
        }


        public IActionResult ViewExportGatePasses(string containerNumber , long vessel , long voyage,DateTime? fromdate, DateTime? todate, string gdnumber)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExportGatePasses.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["containerNumber"].Value = containerNumber;
            report.Parameters["vessel"].Value = vessel;
            report.Parameters["voyage"].Value = voyage;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["GDNumber"].Value = gdnumber;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ExportFinalReport()
        {
      
            ViewData["EGMNumber"] = _voyageExportRepo.GetAll().Select(x => new SelectListItem { Text = x.EgmNumber, Value = x.VoyageExportId.ToString() });
           
            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            return View();
        }


        public IActionResult ViewExportFinalReport(string containerno , long vesselExport, long voyageExport, long egm , long shippingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExportFinalReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["containerno"].Value = containerno;
            report.Parameters["vessel"].Value = vesselExport;
            report.Parameters["voyage"].Value = voyageExport;
            report.Parameters["egm"].Value = egm;
            report.Parameters["shippingAgent"].Value = shippingAgent;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ExportDailyCFSReport()
        {
            ViewData["EGM"] = _voyageExportRepo.GetAll().Select(x => new SelectListItem { Text = x.EgmNumber, Value = x.VoyageExportId.ToString() });

            ViewData["Agent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            return View();
        }


        public IActionResult ViewExportDailyCFSReport(DateTime? fromdate, DateTime? todate, long vesselExport, long voyageExport
                                                     , long egm , long agent ,  string orientation)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = orientation == "Vessel" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExportDailyCFSReport.repx") : assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExportDailyCFSReportForAgentWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy"); ;
            report.Parameters["vessel"].Value = vesselExport;
            report.Parameters["voyage"].Value = voyageExport;
            report.Parameters["egm"].Value = egm;
            report.Parameters["agent"].Value = agent;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult SummaryofStuffing()
        { 
            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            return View();
        }


        public IActionResult ViewSummaryofStuffing(string containerno , long vesselExport , long voyageExport , long shippingAgent )
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.SummaryofStuffing.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["containerno"].Value = containerno;
            report.Parameters["vessel"].Value = vesselExport;
            report.Parameters["voyage"].Value = voyageExport;
            report.Parameters["shippingAgent"].Value = shippingAgent;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }



        public IActionResult TerminalReceipt()
        {
            return View(); 
        }


        public IActionResult ViewTerminalReceipt(long vesselId , long voyageId , string gdnumber )
        {

      
            if (vesselId != 0 && voyageId != 0)
            {
                var results =  _cargoRepo.GetGDsForTerminalReceipt(vesselId, voyageId).Distinct();


                foreach (var resp in results)
                {
                    GenerateTrNumber(resp);

                }
            }
            if (vesselId == 0 && voyageId == 0 && gdnumber != null)
            {
                 GenerateTrNumber(gdnumber);
            }

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.TerminalReceipt.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["gdnumber"].Value = gdnumber;
            report.Parameters["vessel"].Value = vesselId;
            report.Parameters["voyage"].Value = voyageId;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
           return PartialView("_Report", report);
        }

        public void GenerateTrNumber(string resp)
        {
            var datetime = DateTime.Now;
            string day = DateTime.Now.ToString("dd");
            string month = DateTime.Now.ToString("MM");
            string year = DateTime.Now.ToString("yyyy");
            var carg = _cargoRepo.GetFirst(x => x.GDNumber.Trim().ToUpper() == resp.Trim().ToUpper());

            var GDNUMBER = string.IsNullOrEmpty(resp) ? "" : resp.Trim().ToUpper();

             
            var cargs = _cargoRepo.GetAll().Where(x => x.GDNumber?.Trim().ToUpper() == GDNUMBER);



            var data = _dictionaryRepository.GetFirst(x => x.Key == "TerminalReceipt");

            if (carg != null)
            {

                if (carg.TRNumber != null)
                {
                    carg.TRNumber = carg.TRNumber;
                    carg.IssueDate = carg.IssueDate;
                    _cargoRepo.Update(carg);
                }
                else
                {
                    if (data.Value == 0.ToString())
                    {
                        data.Value = 1.ToString();
                        string number = Regex.Match(data.Value, "[0-9]+$").Value;
                        data.Value = number;
                        carg.TRNumber = "TR/" + number + "/" + month + "/" + day + "/" + year;
                        carg.IssueDate = datetime;
                    }
                    else
                    {
                        var x = Convert.ToInt32(data.Value) + 1;
                        string number = Regex.Match(x.ToString(), "[0-9]+$").Value;
                        data.Value = number;
                        carg.TRNumber = "TR/" + number + "/" + month + "/" + day + "/" + year;
                        carg.IssueDate = datetime;
                    }
                }

                foreach (var item in cargs)
                {
                    item.TRNumber = carg.TRNumber;
                    item.IssueDate = carg.IssueDate;
                    _cargoRepo.Update(item);

                }
                //var dictionary = new Dictionary()
                //{ 
                //    Value = data.Value,
                //    Key = data.Key
                //};
                _dictionaryRepository.Update(data);

            }
        }

        public IActionResult ListofExportFullOrEmptyContainers()
        {
            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            return View();
        }


        public IActionResult ViewListofExportFullOrEmptyContainers(string containerno , long vesselExport, long voyageExport 
                                                                    , long shippingAgent , string orientation , DateTime? fromDate , DateTime? toDate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = orientation == "Vessel" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.ListofexportFullOrEmptycontainersVesselWise.repx") : assembly.GetManifestResourceStream("WebdocTerminal.Reports.ListofexportFullOrEmptycontainers.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["containerno"].Value = containerno;
            report.Parameters["vessel"].Value = vesselExport;
            report.Parameters["voyage"].Value = voyageExport;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy"); 

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ListofExportFullLCLCargoStuffedcontainer()
        {
            ViewData["EGM"] = _voyageExportRepo.GetAll().Select(x => new SelectListItem { Text = x.EgmNumber, Value = x.VoyageExportId.ToString() });

            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            return View();
        }


        public IActionResult ViewListofExportFullLCLCargoStuffedcontainer(string containerno , long vesselExport , long voyageExport 
                                                                        , long egm, long shippingAgent , DateTime? fromDate, DateTime? toDate, string orientation) 
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = orientation == "Vessel" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.ListofExportFullLCLCargoStuffedcontainer.repx") : assembly.GetManifestResourceStream("WebdocTerminal.Reports.ListofExportFullLCLCargoStuffedcontainerAgentWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["containerno"].Value = containerno;
            report.Parameters["vessel"].Value = vesselExport;
            report.Parameters["voyage"].Value = voyageExport;
            report.Parameters["egm"].Value = egm;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy");

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;


            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult StatementShowingExportPosition()
        {

            ViewData["EGMNumber"] = _voyageExportRepo.GetAll().Select(x => new SelectListItem { Text = x.EgmNumber, Value = x.VoyageExportId.ToString() });

            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });


            return View();
        }


        public IActionResult ViewStatementShowingExportPosition(string containerno , long vesselExport , long voyageExport ,  long egm,
                                                                long shippingAgent , string orientation)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = orientation == "Vessel" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.StatementShowingExportPosition.repx") : assembly.GetManifestResourceStream("WebdocTerminal.Reports.StatementShowingExportPositionAgentWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["containerno"].Value = containerno;
            report.Parameters["vessel"].Value = vesselExport;
            report.Parameters["voyage"].Value = voyageExport;
            report.Parameters["egm"].Value = egm;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ExportTellySheetSec()
        {

            return View();
        }

        public IActionResult ViewExportTellySheetSec(string containerno, long vesselExport, long voyageExport)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExportTellySheetSEC.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["containerno"].Value = containerno;
            report.Parameters["vessel"].Value = vesselExport;
            report.Parameters["voyage"].Value = voyageExport;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult CargoReceivedStockAlongSidePosition()
        {

            ViewData["EGMNumber"] = _voyageExportRepo.GetAll().Select(x => new SelectListItem { Text = x.EgmNumber, Value = x.VoyageExportId.ToString() });
           
            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            return View();
        }


        public IActionResult ViewCargoReceivedStockAlongSidePosition( string containerno , string type, long vesselExport , long voyageExport,
                                                                    long egm, long shippingAgent, DateTime? fromDate, 
                                                                    DateTime? toDate, string orientation)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = orientation == "Vessel" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.CargoReceivedStockAlongSidePosition.repx") : assembly.GetManifestResourceStream("WebdocTerminal.Reports.CargoReceivedStockAlongSidePositionAgentWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["type"].Value = type;
            report.Parameters["containerno"].Value = containerno;
            report.Parameters["vessel"].Value = vesselExport;
            report.Parameters["voyage"].Value = voyageExport;
            report.Parameters["egm"].Value = egm;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy"); 

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult TerminalReceiptSec()
        {


            ViewData["EGMNumber"] = _voyageExportRepo.GetAll().Select(x => new SelectListItem { Text = x.EgmNumber, Value = x.VoyageExportId.ToString() });

            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            return View();
        }


        public IActionResult ViewTerminalReceiptSec(long vessel , long voyage , long egm , long shippingAgent, string containerNumber , string gdnumber)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.TeminalReceipt2.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["vessel"].Value = vessel;
            report.Parameters["voyage"].Value = voyage;
            report.Parameters["egm"].Value = egm;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["containerNumber"].Value = containerNumber;
            report.Parameters["gdNumber"].Value = gdnumber;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult PartyLedgerExport()
        {
            ViewData["Patry"] = _partyExportRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyExportId.ToString() });

            ViewData["bank"] = _bankRepository.GetAll()
           .Select(x => new SelectListItem { Text = x.BankName, Value = x.BankId.ToString() });

            return View();
        }

        public IActionResult ViewPartyLedgerExport(DateTime? fromdate, DateTime? todate, string party, string invoiceNo, string balanceFrom, string balanceTo
                                                    , string bank, string chequeNo, string IsnegativeBalance)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.PartyLedgerExportReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["party"].Value = party;
            report.Parameters["invoiceNo"].Value = invoiceNo;
            report.Parameters["balanceFrom"].Value = balanceFrom;
            report.Parameters["balanceTo"].Value = balanceTo;
            report.Parameters["bank"].Value = bank;
            report.Parameters["chequeNo"].Value = chequeNo;
            report.Parameters["IsnegativeBalance"].Value = IsnegativeBalance;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }




        public IActionResult MonthlyBillingExport()
        {
            ViewData["Agents"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            return View();
        }

        public IActionResult ViewMonthlyBillingExport(DateTime? fromdate, DateTime? todate,   string party, string invoiceNumber
                                           , string invoiceFromAmount, string invoiceToAmount, string igm, string indexNo, string cbm
                                           , string containerNo, string subBill)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.MonthlyBillingExport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["party"].Value = party;
            report.Parameters["invoiceNumber"].Value = invoiceNumber;
            report.Parameters["invoiceFromAmount"].Value = invoiceFromAmount;
            report.Parameters["invoiceToAmount"].Value = invoiceToAmount;
            report.Parameters["igm"].Value = igm;
            report.Parameters["indexNo"].Value = indexNo;
            report.Parameters["cbm"].Value = cbm;
            report.Parameters["containerNo"].Value = containerNo;
            report.Parameters["subBill"].Value = subBill;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ChequeDetailReportExport()
        {
            ViewData["Partys"] = _partyExportRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyExportId.ToString() });

            ViewData["Banks"] = _bankRepository.GetAll()
                .Select(x => new SelectListItem { Text = x.BankName, Value = x.BankName });

            ViewData["ChequeNo"] = _chequeRecivedExportRepository.GetAll()
                .Select(x => new SelectListItem { Text = x.Number.ToString(), Value = x.Number.ToString() });

            return View();
        }


        public IActionResult ViewChequeDetailReportExport(DateTime? fromdate, DateTime? todate, string party, string bank, string chequeNo)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ChequeDetailReportExport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["party"].Value = party;
            report.Parameters["bank"].Value = bank;
            report.Parameters["chequeNo"].Value = chequeNo;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult DailyCollectionReportExport()
        {
            ViewData["Partys"] = _partyExportRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyExportId.ToString() });

            return View();
        }


        public IActionResult ViewDailyCollectionReportExport(DateTime? fromdate, DateTime? todate, string party, string invoiceNo
                                                        , string fromRange, string toRange)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DailyCollectionReportExport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["party"].Value = party;
            report.Parameters["invoiceNo"].Value = invoiceNo;
            report.Parameters["fromRange"].Value = fromRange;
            report.Parameters["toRange"].Value = toRange;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult GlobelLinkForAllPIFFASOASReport()
        {

            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            return View();
        }


        public IActionResult ViewGlobelLinkForAllPIFFASOASReport(string year, string month, DateTime? fromDate, DateTime? toDate, long shippingagent, long Vessel
                                                     , long voyage, string tax, string shareofBayWest20, string shareofBayWest40 , string adjustment)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.GlobelLinkForAllPIFFASOASReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["year"].Value = year;
            report.Parameters["month"].Value = month;
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["shippingagent"].Value = shippingagent;
            report.Parameters["Vessel"].Value = Vessel;
            report.Parameters["voyage"].Value = voyage;
            report.Parameters["tax"].Value = tax;
            report.Parameters["shareofBayWest20"].Value = shareofBayWest20;
            report.Parameters["shareofBayWest40"].Value = shareofBayWest40;
            report.Parameters["adjustment"].Value = adjustment;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult SoaApllOtherThenPIFFAReport()
        {
            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });


            return View();
        }


        public IActionResult ViewSoaApllOtherThenPIFFAReport(string year, string month, DateTime? fromDate, DateTime? toDate, long shippingagent, long Vessel
                                                     , long voyage  )
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.SoaApllOtherThenPIFFAReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["year"].Value = year;
            report.Parameters["month"].Value = month;
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["shippingagent"].Value = shippingagent;
            report.Parameters["Vessel"].Value = Vessel;
            report.Parameters["voyage"].Value = voyage;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult SOADSVOtherThenPIFFA()
        {
            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });


            return View();
        }


        public IActionResult ViewSOADSVOtherThenPIFFA(string year, string month, DateTime? fromDate, DateTime? toDate, long shippingagent, long Vessel
                                                     , long voyage, string tax, string shareofBayWest20, string shareofBayWest40)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.SOADSVOtherThenPIFFA.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["year"].Value = year;
            report.Parameters["month"].Value = month;
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["shippingagent"].Value = shippingagent;
            report.Parameters["Vessel"].Value = Vessel;
            report.Parameters["voyage"].Value = voyage;
            report.Parameters["tax"].Value = tax;
            report.Parameters["shareofBayWest20"].Value = shareofBayWest20;
            report.Parameters["shareofBayWest40"].Value = shareofBayWest40;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult SoaEflOtherThenPIFFA()
        {
            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            return View();
        }


        public IActionResult ViewSoaEflOtherThenPIFFA(string year, string month, DateTime? fromDate, DateTime? toDate, long shippingagent, long Vessel
                                                     , long voyage)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.SoaEflOtherThenPIFFA.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["year"].Value = year;
            report.Parameters["month"].Value = month;
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["shippingagent"].Value = shippingagent;
            report.Parameters["Vessel"].Value = Vessel;
            report.Parameters["voyage"].Value = voyage;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
 

        public IActionResult DailyInvoiceReportExport()
        {
            ViewData["Agents"] = _clearingAgentExportRepo.GetAll()
                .Select(x => new SelectListItem { Text = x.ClearingAgentName, Value = x.ClearingAgentExportId.ToString() });


            return View();
        }


        public IActionResult ViewDailyInvoiceReportExport( DateTime? fromdate, DateTime? todate, string virNumber,
                                                    string invoiceNumber, string invoiceFromAmount, string invoiceToAmount,
                                                    string clearingAgent)
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DailyInvoices.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["type"].Value = "Export";
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["virNumber"].Value = virNumber;
            report.Parameters["invoiceNumber"].Value = invoiceNumber;
            report.Parameters["invoiceFromAmount"].Value = invoiceFromAmount;
            report.Parameters["invoiceToAmount"].Value = invoiceToAmount;
            report.Parameters["clearingAgent"].Value = clearingAgent;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ForwarderSOACBMWise()
        {
            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            return View();
        }

        public IActionResult ViewForwarderSOACBMWise(string year, string month ,  DateTime? fromDate,  DateTime? toDate , long shippingagent , long Vessel
                                                     , long voyage , long tax  , long usercbm , long usergd)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.SoaKerryLogisticsOtherThenPIFFA.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["year"].Value = year;
            report.Parameters["month"].Value = month;
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["shippingagent"].Value = shippingagent;
            report.Parameters["Vessel"].Value = Vessel;
            report.Parameters["voyage"].Value = voyage;
            report.Parameters["tax"].Value = tax;
            report.Parameters["usercbm"].Value = usercbm;
            report.Parameters["usergd"].Value = usergd;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ForwarderSOABoxWise()
        {
            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            return View();
        }

        public IActionResult ViewForwarderSOABoxWise(string year, string month, DateTime? fromDate, DateTime? toDate, long shippingagent, long Vessel
                                                     , long voyage, string tax, string shareofBayWest20, string shareofBayWest40)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ForwarderSOABoxWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["year"].Value = year;
            report.Parameters["month"].Value = month;
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["shippingagent"].Value = shippingagent;
            report.Parameters["Vessel"].Value = Vessel;
            report.Parameters["voyage"].Value = voyage;
            report.Parameters["tax"].Value = tax;
            report.Parameters["shareofBayWest20"].Value = shareofBayWest20;
            report.Parameters["shareofBayWest40"].Value = shareofBayWest40;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }



        public IActionResult OutstandingAmount()
        {
            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });

            ViewData["ClearingAgent"] = _clearingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ClearingAgentName, Value = x.ClearingAgentExportId.ToString() });

            return View();
        }

        public IActionResult ViewOutstandingAmount(DateTime? fromDate, DateTime? toDate, long Vessel, long voyage
                                                     , long shippingagent, long clearingAgent, string containerNo, string gdNumber)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.OutstandingAmount.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["shippingagent"].Value = shippingagent;
            report.Parameters["Vessel"].Value = Vessel;
            report.Parameters["voyage"].Value = voyage;
            report.Parameters["shippingagent"].Value = shippingagent;
            report.Parameters["clearingAgent"].Value = clearingAgent;
            report.Parameters["containerNo"].Value = containerNo;
            report.Parameters["gdNumber"].Value = gdNumber;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult DeliveryOrderExport()
        {
         
            return View();
        }
        public IActionResult ViewDeliveryOrderExport(string gdNumber)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DeliveryOrderExport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["gdnumber"].Value = gdNumber;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult ExportGatePassDrayOff()
        {

            return View();
        }
        public IActionResult ViewExportGatePassDrayOff(long gatePassID)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExprotGatePassDrayOff.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["gatePassID"].Value = gatePassID;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult EmptyReceving()
        {
            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().Select(x => new SelectListItem { Text = x.ShippingAgentName, Value = x.ShippingAgentExportId.ToString() });
            
            return View();
        }


        public IActionResult ViewEmptyReceving(long shippingAgent  , DateTime? fromdate, DateTime? todate)
        {
             
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.EmptyRecevingReport.repx"); 
            XtraReport report = XtraReport.FromStream(resource);
            
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult PendingGdsInvoices()
        {
            return View();
        }

        public IActionResult UnSettledInvoicesViewModel()
        {
            return View();
        }

        public IActionResult GDsAccoicatedbutAmountNotreceived()
        {
            return View();
        }

        public IActionResult ExportAlongSideDataView()
        {
            return View();
        }

        public IActionResult EmptyContainersLyingAtYard()
        {
            return View();
        }


        public IActionResult ViewEmptyContainersLyingAtYard()
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.EmptyContainersLyingAtYard.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.CreateDocument();

            return PartialView("_Report", report);
        }

        public IActionResult Exportfullout()
        {
            ViewData["Destinations"] = _deliveredYardExportRepository.GetAll().Select(s => new SelectListItem { Text = s.DeliveredYardName, Value = s.DeliveredYardName });

            return View();
        }


        public IActionResult ViewExportfullout(string Port, DateTime? fromdate, DateTime? todate)
        {

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.Exportfullout.repx");
            XtraReport report = XtraReport.FromStream(resource);
            
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["Port"].Value = Port;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult GateinReportExport()
        {
            return View();
        }


        public IActionResult ViewGateinReportExport(DateTime? fromdate, DateTime? todate)
        {

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.GateinReportExport.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult GDDetailReportExport()
        {
            return View();
        }


        public IActionResult ViewGDDetailReportExport(DateTime? fromdate, DateTime? todate)
        {

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.GDDetailReportExport.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult DailySalesReportExport()
        {

            ViewData["SAgent"] = _shippingAgentExportRepo.GetAll().Select(s => new SelectListItem { Text = s.ShippingAgentName, Value = s.ShippingAgentExportId.ToString() });

            return View();
        }

        public IActionResult ViewDailySalesReportExport(DateTime? fromdate, DateTime? todate, long shippingagent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DailySalesReportExport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["ShippingAgent"].Value = shippingagent;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);

        }

        public IActionResult GateOutReportExport()
        {

            ViewData["SAgent"] = _shippingAgentExportRepo.GetAll().Select(s => new SelectListItem { Text = s.ShippingAgentName, Value = s.ShippingAgentExportId.ToString() });

            return View();
        }

        public IActionResult ViewGateOutReportExport(DateTime? fromdate, DateTime? todate, long shippingagent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.GateOutReportExport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["ShippingAgent"].Value = shippingagent;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);

        }


        public IActionResult GDAssociationDetailedReport()
        {

            ViewData["SAgent"] = _shippingAgentExportRepo.GetAll().Select(s => new SelectListItem { Text = s.ShippingAgentName, Value = s.ShippingAgentExportId.ToString() });

            return View();
        }

        public IActionResult ViewGDAssociationDetailedReport(DateTime? fromdate, DateTime? todate, long shippingagent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.GDAssociationDetailedReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["ShippingAgent"].Value = shippingagent;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);

        }


        public IActionResult BreakupofCashierDailySaleReportExport()
        {
            ViewData["party"] = _partyExportRepository.GetAll().Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyExportId.ToString() });

            return View();
        }

        public IActionResult ViewBreakupofCashierDailySaleReportExport(DateTime? fromdate, DateTime? toDate, long? party)
        {
            GetUserId();
            
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.BreakupofCashierDailySaleReportExport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            
            //report.Parameters["path"].Value = path;
            //report.Parameters["CompanyId"].Value = CompanyID;

            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["party"].Value = party;
            
            report.CreateDocument();
            return PartialView("_Report", report);
        }

    }
}