using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using WebdocTerminal.Reports;
using WebdocTerminal.Repos.Interfaces;
  

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class ReportsController : ParentController
    {

        public IVesselRepository _vesselRepo;
        public IShippingAgentRepository _shippingRepo;
        public IPartyRepository _partyRepo;
        public IClearingAgentRepository _clearingAgentRepository;
        public IBankRepository _bankRepository;
        public IVoyageRepository _voyageRepository;
        private IChequeRecivedRepository _chequeRecivedRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private IUsersRepository _userRepo;
        private IShippingAgentRepository _shippingAgentRepo;
        private IShippingLineRepository _shippingLineRepo;
        private IPaymentUpdateRepository _paymentUpdateRepo;
        private IPreAlertRepository _preAlertRepo;
        private IContainerIndexRepository _containerIndexRepo;
        private ICYContainerRepository _cycontainerRepo;
        private IExaminationRequestRepository _examinationRequestRepo;
        private IConsigneRepository _consigneRepo;
        private IPreAlertVesselRepository _preAlertVesselRepo;
        public IGoodsHeadRepository _goodsHeadRepository;
        public ITariffInformationRepository _tariffInformationRepo;
        public IShipperRepository _shipperRepo;
        public IISOCodeHeadRepository _ISOCodeHeadRepository;
        public IInvoiceRepository _invoiceRepo;
        public IBillToLineRepository _billToLineRepository;
        public IScheduleOfChargeRepository _scheduleOfChargeRepository;

        public IPartyLedgerRepository  _partyLedgerRepository;
        public ISIMORepository _simoRepository;
        public ISIMRepository _simRepository;
        public IPortAndTerminalRepository _portAndTerminalRepository;
        public ITransporterRepository _transporterRepository;
        public IMasterShippingAgentRepository _masterShippingAgentRepository;
        private IConfiguration Configuration;
        public IContainerRepository _containerRepository;
        public string Con { get; set; }
        public ReportsController(IVesselRepository vesselRepo, IShippingAgentRepository shippingRepo,
                                 IPartyRepository partyRepo, IClearingAgentRepository clearingAgentRepository,
                                 IBankRepository bankRepository,
                                 IVoyageRepository voyageRepository,
                                 IChequeRecivedRepository chequeRecivedRepository
                                 , UserManager<IdentityUser> userManager
                                , IUsersRepository userRepo
                                , IShippingAgentRepository shippingAgentRepo
                                , IShippingLineRepository shippingLineRepo
                                , IPaymentUpdateRepository paymentUpdateRepo
                                , IPreAlertRepository preAlertRepo
                                , IContainerIndexRepository containerIndexRepo
                                , ICYContainerRepository cycontainerRepo
                                , IExaminationRequestRepository examinationRequestRepo
                                , IConsigneRepository consigneRepo
                                , IPreAlertVesselRepository preAlertVesselRepo
                                , IGoodsHeadRepository goodsHeadRepository
                                , ITariffInformationRepository tariffInformationRepo
                                ,IShipperRepository shipperRepo
                                ,IISOCodeHeadRepository ISOCodeHeadRepository
                                , IInvoiceRepository invoiceRepo
                                , IBillToLineRepository billToLineRepository,
                                 IScheduleOfChargeRepository scheduleOfChargeRepository,
                                 IPartyLedgerRepository partyLedgerRepository,
                                 ISIMORepository simoRepository,
                                 ISIMRepository simRepository,
                                 IPortAndTerminalRepository portAndTerminalRepository,
                                 ITransporterRepository transporterRepository,
                                 IMasterShippingAgentRepository masterShippingAgentRepository,
                                 IConfiguration _configuration,
                                 IContainerRepository containerRepository)
        {
            _vesselRepo = vesselRepo;
            _shippingRepo = shippingRepo;
            _partyRepo = partyRepo;
            _clearingAgentRepository = clearingAgentRepository;
            _bankRepository = bankRepository;
            _voyageRepository = voyageRepository;
            _chequeRecivedRepository = chequeRecivedRepository;
            _userManager = userManager;
            _userRepo = userRepo;
            _shippingAgentRepo = shippingAgentRepo;
            _shippingLineRepo = shippingLineRepo;
            _paymentUpdateRepo = paymentUpdateRepo;
            _preAlertRepo = preAlertRepo;
            _containerIndexRepo = containerIndexRepo;
            _cycontainerRepo = cycontainerRepo;
            _examinationRequestRepo = examinationRequestRepo;
            _consigneRepo = consigneRepo;
            _preAlertVesselRepo = preAlertVesselRepo;
            _goodsHeadRepository = goodsHeadRepository;
            _tariffInformationRepo = tariffInformationRepo;
            _shipperRepo = shipperRepo;
            _ISOCodeHeadRepository = ISOCodeHeadRepository;
            _invoiceRepo = invoiceRepo;
            _billToLineRepository = billToLineRepository;
            _scheduleOfChargeRepository = scheduleOfChargeRepository;
            _partyLedgerRepository = partyLedgerRepository;
            _simoRepository = simoRepository;
            _simRepository = simRepository;
            _portAndTerminalRepository = portAndTerminalRepository;
            _transporterRepository = transporterRepository;
            _masterShippingAgentRepository = masterShippingAgentRepository;
            Configuration = _configuration;
             Con =  _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            _containerRepository = containerRepository;
             
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
        public static void InsertBulkData(string connString, string tableName, DataTable dataTable)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlBulkCopy bulkCopy = new SqlBulkCopy(
                    connection,
                    //SqlBulkCopyOptions.TableLock |
                    //SqlBulkCopyOptions.FireTriggers |
                    //SqlBulkCopyOptions.UseInternalTransaction,
                     SqlBulkCopyOptions.KeepIdentity,
                    null
                    );

                


                bulkCopy.BatchSize = 4000;
                bulkCopy.BulkCopyTimeout = 0;

                bulkCopy.DestinationTableName = tableName;
                connection.Open();
                bulkCopy.WriteToServer(dataTable);
                connection.Close();
            }
        }
        public IActionResult AgingReportCY()
        {
            //string query = string.Format("select * from finalvoucherdetail where VoucherServiceTypeId is not null order by VoucherDetailId asc  ");
            ////string query = string.Format("select * from finalvouchermst order by VoucherId asc  ");

            //DataTable dt = new DataTable();
            //using (SqlConnection con = new SqlConnection("Data Source=192.168.1.7;Initial Catalog=Finwave;Persist Security Info=True;User ID=sibtain;Password=raza321--;"))
            //{
            //    con.Open();
            //    using (SqlCommand cmd = con.CreateCommand())
            //    {
            //        cmd.CommandText = query;
            //        cmd.CommandType = CommandType.Text;
            //        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
            //        {
            //            adp.Fill(dt);
            //        }
            //    }
            //}

            //string connString = Configuration.GetConnectionString("DefaultCoasdasdnnection");
            //InsertBulkData(connString, "VoucherDetail", dt);


            ViewData["Agents"] = _shippingRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult TellySheetReport()
        {
            return View();
        }


        public IActionResult ViewAgingReportCY(DateTime? fromdate, DateTime? toDate, long shippingagentId, string descTtype)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.AgingReportCY.repx");
            XtraReport report = XtraReport.FromStream(resource);
            //var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            //report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["shippingAgent"].Value = shippingagentId;
            report.Parameters["type"].Value = descTtype;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult AgingReportCFS()
        {
            ViewData["Agents"] = _shippingRepo.GetAll()
             .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }


        public IActionResult ViewAgingReportCFS(DateTime ? fromdate, DateTime ? toDate , long shippingagentId , string descTtype)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.AgingReportCFS.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["shippingAgent"].Value = shippingagentId;
            report.Parameters["type"].Value = descTtype;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult CargoAgingReportCFSAndCY()
        {
            ViewData["Agents"] = _shippingRepo.GetAll()
        .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }


        public IActionResult ViewCargoAgingReportCFSAndCY(DateTime fromdate, DateTime toDate, long type  )
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CargoAgingReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate.ToString("MM/dd/yyyy");
            report.Parameters["type"].Value = type;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult AlongSideLocationWise()
        {
            ViewData["shippingAgents"] = _shippingRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["clearingAgents"] = _clearingAgentRepository.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ClearingAgentId.ToString() });

            return View();
        }


        public IActionResult ViewAlongSideLocationWise(int type, DateTime fromdate, DateTime todate, string igm, string index, string vessel, string voyage
                                                      , string consignee, string shippingAgent, string clearingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.AlongsideReportLocationWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["type"].Value = type;
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["igm"].Value = igm;
            report.Parameters["index"].Value = index;
            report.Parameters["vessel"].Value = vessel;
            report.Parameters["voyage"].Value = voyage;
            report.Parameters["consignee"].Value = consignee;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["clearingAgent"].Value = clearingAgent;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult AlongsideReport()
        {


            ViewData["shippingAgents"] = _shippingRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["clearingAgents"] = _clearingAgentRepository.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ClearingAgentId.ToString() });


            return View();
        }


        public IActionResult ViewAlongsideReport(int type, string cargoType , string igm, string index, string vessel, string voyage
                                                      , string consignee, string shippingAgent, string clearingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.AlongsideReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["type"].Value = type;
            report.Parameters["cargoType"].Value = cargoType; 
            report.Parameters["igm"].Value = igm;
            report.Parameters["index"].Value = index;
            report.Parameters["vessel"].Value = vessel;
            report.Parameters["voyage"].Value = voyage;
            report.Parameters["consignee"].Value = consignee;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["clearingAgent"].Value = clearingAgent;
           
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult AuctionReport()
        {
            ViewData["shippingAgents"] = _shippingRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewAuctionReport(DateTime? fromdate, DateTime? toDate, long type2, long type , long shippingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.AuctionReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["type2"].Value = type2;
            report.Parameters["type"].Value = type;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.CreateDocument();
            return PartialView("_Report", report);
        }
        
        public IActionResult AuctionReportForCustom()
        {
            ViewData["shippingAgents"] = _shippingRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewAuctionReportForCustom(DateTime? fromdate, DateTime? toDate, long type2, long type , long shippingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.AuctionReportForCustom.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["type2"].Value = type2;
            report.Parameters["type"].Value = type;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult UpdatedVehiclesLyingAtTerminal()
        {
            ViewData["shippingAgents"] = _shippingRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewUpdatedVehiclesLyingAtTerminal(DateTime? fromdate, DateTime? toDate , string type, string cargotype, long shippingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.UpdatedVehiclesLyingAtTerminal.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["type"].Value = type;
            report.Parameters["cargotype"].Value = cargotype;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["shippingagent"].Value = shippingAgent;
            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult TPDeliveryReport()
        {
            ViewData["shippingAgents"] = _shippingRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewTPDeliveryReport(DateTime? fromdate, DateTime? toDate, long type ,  long shippingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.TPDeliveryReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["type"].Value = type;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult CYDeliveryReport()
        {
            ViewData["Agents"] = _shippingRepo.GetAll()
          .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewCYDeliveryReport(DateTime fromdate, DateTime toDate, long ShippingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CYDeliveryReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate.ToString("MM/dd/yyyy");
            report.Parameters["ShippingAgent"].Value = ShippingAgent;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult DailyExaminationComplete()
        {
            return View();
        }

        public IActionResult DailyInvoices()
        {
            return View();
        }



        public IActionResult DeliveryReport()
        {
            return View();
        }

        public IActionResult DeStuffingContainerWiseReport()
        {
            return View();
        }

        public IActionResult DeStuffingReport()

        { 
            return View();
        }



        public IActionResult ViewDestuffReport(DateTime? fromdate, DateTime? todate  )
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DeStuffingReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.CreateDocument();
            // return View(report);
            return PartialView("_Report", report);
        }




        public IActionResult DeStuffingReportWithoutCBM()
        {
            ViewData["Agents"] = _shippingRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }



        public IActionResult ViewDestuffReportWithoutCBM(DateTime? fromdate, DateTime? todate ,  long ShippingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DestuffingReportWithoutCBM.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["ShippingAgent"].Value = ShippingAgent;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.CreateDocument();
            // return View(report);
            return PartialView("_Report", report);
        }



        public IActionResult EmptyContainersAtYardReport()
        {
            return View();
        }

        public IActionResult ExaminationMark()
        {
            return View();
        }

        public IActionResult ViewExaminationMark(DateTime fromdate, DateTime toDate)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExaminationMark.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate.ToString("MM/dd/yyyy");

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ListContainerLyingInYard()
        {
            return View();
        }

        public IActionResult TallySheet(string orientation)
        {
            ViewData["Vessels"] = _vesselRepo.GetAll()
             .Select(v => new SelectListItem { Text = v.VesselName, Value = v.VesselName });
            return View();
        }

        public IActionResult ViewTallySheet(string containerNo , string virno, string orientation)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
             
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = orientation == "container" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.TallySheet.repx") : assembly.GetManifestResourceStream("WebdocTerminal.Reports.TallySheet_IndexWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["containerNo"].Value = containerNo;
            report.Parameters["virno"].Value = virno;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult invoice()
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.invoice.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["BillNo"].Value = 1;
            report.CreateDocument();
            return View(report);
        }

        public IActionResult AlongsideReportLocationWise()
        {
            return View();
        }

        public IActionResult PreliminaryReport()
        {
            return View();
        }


        public IActionResult GateInandOut()
        {
            return View();
        }

        public IActionResult ListofUpcomingContainer()
        {
            return View();
        }
        public IActionResult ViewListofUpcomingContainer(string type)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ListofUpcomingContainer.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["type"].Value = type;


            return PartialView("_Report", report);
        }


        public IActionResult ListofOutGoingContainer()
        {
            return View();
        }
        public IActionResult ViewListofOutGoingContainer( )
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.OutGoingContainers.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ListofUpcomingContainerCY()
        {
            return View();
        }
        public IActionResult ViewListofUpcomingContainerCY(string type)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ListofUpcomingContainer.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["type"].Value = type;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult CargoAgingReport()
        {
            return View();
        }

        public IActionResult ContainerReceivedDuringthePeriod()
        {
            return View();
        }

        public IActionResult CYReleasedContainers()
        {

            return View();
        }

        public IActionResult ViewCYReleasedContainers(DateTime fromdate, DateTime todate)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CYReleasedContainers.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult CFSReleasedContainers()
        {
            ViewData["shippingAgents"] = _shippingRepo.GetAll()
         .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });


            return View();
        }

        public IActionResult ViewCFSReleasedContainers(DateTime? fromdate, DateTime? todate , long shippingAgentId)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CFSReleasedContainersReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["ShippingAgent"].Value = shippingAgentId;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult BillInvoice()
        {
            return View();
        }

        public IActionResult ViewBillInvoice(long BillNo)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.invoice.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["BillNo"].Value = BillNo;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult BillInvoiceCyContainer()
        {
            return View();
        }

        public IActionResult ViewBillInvoiceCyContainer(long BillNo)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.InvoiceCyContainer.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["BillNo"].Value = BillNo;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult GatePass()
        {
            return View();
        }

        public IActionResult ViewGatePass(string type, long gatePassId)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.GatePass.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["type"].Value = type;
            report.Parameters["gatePassId"].Value = gatePassId;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult GatePassDotMatrix()
        {
            return View();
        }

        public IActionResult ViewGatePassDotMatrix(string type, long gatePassId)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.GatepassReportForDotMatrix.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["type"].Value = type;
            report.Parameters["gatePassId"].Value = gatePassId;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult GatePassAuction()
        {
            return View();
        }


        public IActionResult ViewGatePassAuctionReport(string type, long gatePassId)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.GatePassAuctionReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["type"].Value = type;
            report.Parameters["gatePassId"].Value = gatePassId;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ContainerReceiveDuringThePeriod()
        {
            ViewData["Agents"] = _shippingRepo.GetAll()
             .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewContainerReceiveDuringThePeriod(DateTime fromdate, DateTime todate, long type, long ShippingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ContainerReceivedDuringthePeriod.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["type"].Value = type;
            report.Parameters["ShippingAgent"].Value = ShippingAgent;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }



        public IActionResult ContainerAvailableToYard()
        {
            ViewData["Agents"] = _shippingRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewContainerAvailableToYard(int ShippingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.EmptyContainersAtYardReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["ShippingAgent"].Value = ShippingAgent;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult DeStuffingContainerWise()
        {

            ViewData["Agents"] = _shippingRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });
            return View();
        }

        public IActionResult ViewDeStuffingContainerWise(DateTime? fromdate, DateTime ? todate  , long ShippingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DeStuffingContainerWiseReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["ShippingAgent"].Value = ShippingAgent;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult EmptyOutReport()
        {

            ViewData["Agents"] = _transporterRepository.GetAll()
             .Select(v => new SelectListItem { Text = v.TransporterName, Value = v.TransporterId.ToString() });

            return View();
        }

        public IActionResult ViewEmptyOutReport(DateTime fromdate, DateTime todate, int Transporter)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.EmptyOutReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["Transporter"].Value = Transporter;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult DeliveryOrder()
        {
            return View();
        }

        public IActionResult ViewDeliveryOrder(long donum)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DeliveryOrder.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["deliveryOrderNumber"].Value = donum;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();

            //PrintToolBase printTool = new PrintToolBase(report.PrintingSystem);
            //printTool.Print();
 
            return PartialView("_Report", report);
        }


        public IActionResult ViewDeliveryOrderAuto(long donum)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DeliveryOrder.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["deliveryOrderNumber"].Value = donum;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();

            using (MemoryStream ms = new MemoryStream())
            {
                report.ExportToPdf(ms, new PdfExportOptions() { ShowPrintDialogOnOpen = true });
                return File(ms.ToArray(), String.Format("application/{0}", "pdf"));
             }
        }


        public IActionResult DeliveryOrderCY()
        {
            return View();
        }

        public IActionResult ViewDeliveryOrderCY(long donum)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DeliveryOrderReportCycontainer.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["deliveryOrderNumber"].Value = donum;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult GateInContainerByDate()
        {
            ViewData["Agents"] = _shippingRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewGateInContainerByDate(DateTime fromdate, DateTime todate, int ShippingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.GateInContainersByDate.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["ShippingAgent"].Value = ShippingAgent;            
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult DestuffContainerDateWise()
        {

            ViewData["Agents"] = _shippingRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewDestuffContainerDateWise(DateTime fromdate, DateTime todate, int ShippingAgent)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DestuffContainerDateWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["ShippingAgent"].Value = ShippingAgent;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ChequeDetailByDate()
        {
            return View();
        }

        public IActionResult ViewChequeDetailByDate(DateTime fromdate, DateTime todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ChequeDetailByDate.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult GateInContainerShippingAgentWise()
        {
            ViewData["Agents"] = _shippingRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewGateInContainerShippingAgentWise(DateTime fromdate, DateTime todate, string type, long shippingAgentId)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.GateInContainerShippingAgentWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["type"].Value = type;
            report.Parameters["shippingAgentId"].Value = shippingAgentId;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult DestuffContainerShippingAgentWise()
        { 
            return View();
        }

        public IActionResult ViewDestuffContainerShippingAgentWise(string virnumber , string containernumber)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DestuffContainerShippingAgentWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            //report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            //report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            //report.Parameters["ShippingAgent"].Value = ShippingAgent;
            report.Parameters["virnumber"].Value = virnumber;
            report.Parameters["containernumber"].Value = containernumber;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
           public IActionResult DestuffContainerClientWise()
        { 
            return View();
        }

        public IActionResult ViewDestuffContainerClientWise(string virnumber , string containernumber)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DestuffContainerClientWise.repx");
            XtraReport report = XtraReport.FromStream(resource); 
            report.Parameters["virnumber"].Value = virnumber;
            report.Parameters["containernumber"].Value = containernumber;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult DestuffingPreliminaryReport()
        {
            return View();
        }

        public IActionResult ViewDestuffingPreliminaryReport(string virnumber, string containernumber)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DestuffingPreliminaryReport.repx");
            XtraReport report = XtraReport.FromStream(resource); 
            report.Parameters["virnumber"].Value = virnumber;
            report.Parameters["containernumber"].Value = containernumber;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult PartyLedgerPatryWise()
        {
            ViewData["patry"] = _partyRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyId.ToString() });

            return View();
        }

        public IActionResult ViewPartyLedgerPatryWise(long partyId)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.PartyLedgerPatryWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["partyId"].Value = partyId;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult PartyLedgerDateWise()
        {
            ViewData["patry"] = _partyRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyId.ToString() });

            ViewData["bank"] = _bankRepository.GetAll()
            .Select(x => new SelectListItem { Text = x.BankName, Value = x.BankId.ToString() });


            return View();
        }

        public IActionResult ViewPartyLedgerDateWise(DateTime fromdate, DateTime todate, string party, string invoiceNo, string balanceFrom, string balanceTo
                                                    , string bank, string chequeNo, string IsnegativeBalance)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.PartyLedgerDateWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
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


        public IActionResult DailySalesReportDatewise()
        {
            return View();
        }

        public IActionResult ViewDailySalesReportDatewise(DateTime fromdate, DateTime todate)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DailySalesReportTypewise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);

        }


        public IActionResult MonthlyBilling()
        {
            ViewData["Agents"] = _shippingRepo.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.ShippingAgentId.ToString() });
            return View();
        }

        public IActionResult ViewMonthlyBilling(DateTime fromdate, DateTime todate, string type, string party, string invoiceNumber
                                           , string invoiceFromAmount, string invoiceToAmount, string igm, string indexNo, string cbm
                                           , string containerNo, string subBill)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.MonthlyBillingReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["type"].Value = type;
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

        public IActionResult DailySummaryReport()
        {
            return View();
        }

        public IActionResult ViewDailySummaryReport(DateTime todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DailySummaryReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ReceiptForPackagesDelivered()
        {
            return View();
        }


        public IActionResult ViewReceiptForPackagesDelivered(long containerIndexId)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ReceiptForPackagesDelivered.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["containerIndexId"].Value = containerIndexId;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult MessageLog()
        {
            return View();
        }

        public IActionResult ViewMessageLog(DateTime fromdate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.MessageLog.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult GroundingContainerCY()
        {
            ViewData["Agents"] = _shippingRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["GoodsHeadItems"] = _goodsHeadRepository.GetAll()
            .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadName });

            return View();
        }

        public IActionResult ViewGroundingContainerCY(DateTime fromdate, DateTime todate, long ShippingAgent, string commodity)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.GroundingContainerCYReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["ShippingAgent"].Value = ShippingAgent;
            report.Parameters["commodity"].Value = commodity;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult DeliveryOrderReportCYContainer()
        {
            ViewData["Agents"] = _shippingRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewDeliveryOrderReportCYContainer(DateTime fromdate, DateTime todate, long ShippingAgent)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DeliveryReportCyContainer.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["ShippingAgent"].Value = ShippingAgent;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult DeliveryOrderReportCFSContainer()
        {

            ViewData["Agents"] = _shippingRepo.GetAll()
        .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });


            return View();
        }

        public IActionResult ViewDeliveryOrderReportCFSContainer(DateTime fromdate, DateTime todate, long ShippingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DeliveryReportCFSContainer.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["ShippingAgent"].Value = ShippingAgent;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult DeliveryOrderReportCFSContainerWeightWise()
        {

            ViewData["Agents"] = _shippingRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });


            return View();
        }

        public IActionResult ViewDeliveryOrderReportCFSContainerWeightWise(string igm, string indexNo, DateTime? fromdate, DateTime? todate, long shippingAgent)
        {


            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DeliveryOrderReportCFSContainerSecond.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["igm"].Value = igm;
            report.Parameters["index"].Value = indexNo;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyy hh:mm");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyy hh:mm");
            report.Parameters["ShippingAgent"].Value = shippingAgent;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);

        }

        public IActionResult ExaminationMarkCYContainer()
        {

            ViewData["shippingAgent"] = _shippingRepo.GetAll()
                .Select(x => new SelectListItem { Text = x.Name, Value = x.ShippingAgentId.ToString() });

            ViewData["GoodsHeadItems"] = _goodsHeadRepository.GetAll()
            .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadName });

            return View();
        }

        public IActionResult ViewExaminationMarkCYContainer(DateTime fromdate, DateTime todate , string shippingAgent, string commodity)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExaminationMarkCYContainer.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["commodity"].Value = commodity;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ExaminationMarkESCYContainer()
        {
            ViewData["shippingAgent"] = _shippingRepo.GetAll()
                .Select(x => new SelectListItem { Text = x.Name, Value = x.ShippingAgentId.ToString() });
            return View();
        }

        public IActionResult ViewExaminationMarkESCYContainer(DateTime? fromdate, DateTime? todate , string shippingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExaminationMarkESContainerCY.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            
            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult ExaminationMarkESContainerCFS()
        {
            ViewData["shippingAgent"] = _shippingRepo.GetAll()
                .Select(x => new SelectListItem { Text = x.Name, Value = x.ShippingAgentId.ToString() });
            return View();
        }

        public IActionResult ViewExaminationMarkESContainerCFS(DateTime? fromdate, DateTime? todate , string shippingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExaminationMarkESContainerCFS.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ExaminationCompleteContainerCY()
        {

            ViewData["shippingAgent"] = _shippingRepo.GetAll()
                .Select(x => new SelectListItem { Text = x.Name, Value = x.ShippingAgentId.ToString() });

            ViewData["GoodsHeadItems"] = _goodsHeadRepository.GetAll()
            .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadName });

            return View();
        }

        public IActionResult ViewExaminationCompleteContainerCY(DateTime fromdate, DateTime todate , string shippingAgent, string commodity)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExaminationCompleteContainerCY.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["commodity"].Value = commodity;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
        
        public IActionResult ExamitionMarkCFS()
        {

            ViewData["shippingAgent"] = _shippingRepo.GetAll()
                .Select(x => new SelectListItem { Text = x.Name, Value = x.ShippingAgentId.ToString() });
            
            ViewData["clearingagent"] = _clearingAgentRepository.GetAll()
                .Select(x => new SelectListItem { Text = x.Name, Value = x.ClearingAgentId.ToString() });

            ViewData["GoodsHeadItems"] = _goodsHeadRepository.GetAll()
            .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadName });

            return View();
        }

        public IActionResult ViewExamitionMarkCFS(DateTime? fromdate, DateTime? todate , string shippingAgent, string clearingagent, string type, string commodity)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExamitionMarkCFS.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["type"].Value = type;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["shippingagent"].Value = shippingAgent; 
            report.Parameters["clearingagent"].Value = clearingagent;
            report.Parameters["commodity"].Value = commodity;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ExamitionMarkForCustom()
        {
 
            return View();
        }

        public IActionResult ViewExamitionMarkForCustom(DateTime? fromdate, DateTime? todate)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExaminationReportForCustom.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult InventoryCyContainerReport()
        {

            ViewData["Agents"] = _shippingRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewInventoryCyContainerReport(long ShippingAgent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.InventoryCyContainerReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["ShippingAgent"].Value = ShippingAgent;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult WeighmentCyContainerReport()
        {
            return View();
        }

        public IActionResult ViewWeighmentCyContainerReport(DateTime fromdate, DateTime todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.WeighmentCYContainer.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ChequeDetailReport()
        {
            ViewData["Partys"] = _partyRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyId.ToString() });

            ViewData["Banks"] = _bankRepository.GetAll()
                .Select(x => new SelectListItem { Text = x.BankName, Value = x.BankName });

            ViewData["ChequeNo"] = _chequeRecivedRepository.GetAll()
                .Select(x => new SelectListItem { Text = x.Number, Value = x.Number });
            return View();
        }


        public IActionResult ViewChequeDetailReport(DateTime fromdate, DateTime todate, string party, string bank, string chequeNo)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ChequeDetailReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["party"].Value = party;
            report.Parameters["bank"].Value = bank;
            report.Parameters["chequeNo"].Value = chequeNo;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult GateInWeighmentContainerReport()
        {

            return View();
        }


        public IActionResult ViewGateInWeighmentContainerReport(DateTime fromdate, DateTime todate, long type)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.GateInWeighmentContainer.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["type"].Value = type;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ContainerInformationReport()
        {

            return View();
        }


        public IActionResult ViewContainerInformationReport(string number, string type, string indexnumber)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ContainerTrackingReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            if(type == "CFS")
            {
                report.Parameters["ContainerNo"].Value = number;
                report.Parameters["number"].Value = indexnumber;
                report.Parameters["type"].Value = type;
                report.Parameters["path"].Value = path;
                report.Parameters["CompanyId"].Value = CompanyID;
            }
            else
            {
                report.Parameters["ContainerNo"].Value = indexnumber;
                report.Parameters["number"].Value = number;
                report.Parameters["type"].Value = type;
                report.Parameters["path"].Value = path;
                report.Parameters["CompanyId"].Value = CompanyID;

            }

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult EmptyContainerGateOutReport()
        {

            return View();
        }


        public IActionResult ViewEmptyContainerGateOutReport(DateTime fromdate, DateTime todate, string containernumber)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.EmptyDeliveryReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["containerNumber"].Value = containernumber;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult EmptyContainerGatePassReport()
        {

            return View();
        }

        public IActionResult ViewEmptyContainerGatePassReport(long emptyGatePass)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.EmptyContainerGatePass.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["EmptyContainerGatePassId"].Value = emptyGatePass;


            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult DailyCollectionReport()
        {
            ViewData["Partys"] = _partyRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyId.ToString() });
            return View();
        }


        public IActionResult ViewDailyCollectionReport(DateTime fromdate, DateTime todate, string party, string invoiceNo
                                                        , string fromRange, string toRange)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DailyCollectionReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["party"].Value = party;
            report.Parameters["invoiceNo"].Value = invoiceNo;
            report.Parameters["fromRange"].Value = fromRange;
            report.Parameters["toRange"].Value = toRange;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult PendingCFSContainersReport()
        {

            return View();
        }


        public IActionResult ViewPendingCFSContainersReport()
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.PendingContainerReportCFS.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult DailyInvoiceReport()
        {
            ViewData["Agents"] = _clearingAgentRepository.GetAll()
                .Select(x => new SelectListItem { Text = x.Name, Value = x.ClearingAgentId.ToString() });


            return View();
        }


        public IActionResult ViewDailyInvoiceReport(string type, DateTime fromdate, DateTime todate, string virNumber,
                                                    string invoiceNumber, string invoiceFromAmount, string invoiceToAmount,
                                                    string clearingAgent)
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DailyInvoices.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["type"].Value = type;
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["virNumber"].Value = virNumber;
            report.Parameters["invoiceNumber"].Value = invoiceNumber;
            report.Parameters["invoiceFromAmount"].Value = invoiceFromAmount;
            report.Parameters["invoiceToAmount"].Value = invoiceToAmount;
            report.Parameters["clearingAgent"].Value = clearingAgent;
            report.CreateDocument();
            return PartialView("_Report", report);
        }



        public IActionResult EmptyGatePassReport()
        {
 
            return View();
        }


        public IActionResult ViewEmptyGatePassReport(string emptyGateOutContainerId)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.EmptyGatePass.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["emptyGateOutContainerId"].Value = emptyGateOutContainerId;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult BRTReport()
        {

            return View();
        }


        public IActionResult ViewBRTReport( )
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.BRTReport.repx");
            XtraReport report = XtraReport.FromStream(resource); 
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult OutStandingPaymentReport()
        {

            ViewData["ShippingAgent"] = _shippingAgentRepo.GetAll().OrderBy(s => s.Name)
                .Select(s => new SelectListItem { Text = s.Name, Value = s.ShippingAgentId.ToString() });

             
            ViewData["RequestNumber"] = _paymentUpdateRepo.GetAll().OrderBy(s => s.RequestNumber)
                .Select(s => new SelectListItem { Text = s.RequestNumber.ToString(), Value = s.RequestNumber.ToString() });

            ViewData["VesselName"] = _preAlertVesselRepo.GetAll().OrderBy(s => s.PreAlertVesselName)
                .Select(s => new SelectListItem { Text = s.PreAlertVesselName.ToString().Trim().ToUpper(), Value = s.PreAlertVesselName.Trim().ToUpper() });
             
  
            return View();
        }


        public IActionResult ViewOutStandingPaymentReport(string requestNumber , string shippingLine ,string numberOfcontainers , string vesselName , string shippingAgent , DateTime ? fromdate , DateTime ? todate)
        {
             GetUserId();
             var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.OutStandingPaymentReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
             report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;


            report.Parameters["alertfromDate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["alerttoDate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["principal"].Value = shippingAgent;
            report.Parameters["line"].Value = shippingLine;
            report.Parameters["requestNumber"].Value = requestNumber;
            report.Parameters["containerCount"].Value = numberOfcontainers;
            report.Parameters["vessel"].Value = vesselName;
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult PayOrderReport()
        {

            ViewData["Bank"] = _bankRepository.GetAll().OrderBy(s => s.BankName)
                .Select(s => new SelectListItem { Text = s.BankName, Value = s.BankId.ToString() });
              

            return View();
        }


        public IActionResult ViewPayOrderReport(string PayOrderNumber, string bankName  )
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.PayOrderReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
             report.Parameters["path"].Value = path;
             report.Parameters["CompanyId"].Value = CompanyID;  
            report.Parameters["PayOrderNumber"].Value = PayOrderNumber;
            report.Parameters["bankName"].Value = bankName; 
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult DropOfTicketImportReport()
        { 
            return View();
        }


        public IActionResult ViewDropOfTicketImportReport(string igm, string ContainerNumber)
        {
               GetUserId();
              var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DropOfTicketImport.repx");
            XtraReport report = XtraReport.FromStream(resource);
             report.Parameters["path"].Value = path;
             report.Parameters["CompanyId"].Value = CompanyID;  
            report.Parameters["igm"].Value = igm;
            report.Parameters["ContainerNumber"].Value = ContainerNumber; 
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ParkingTicketReport()
        { 
            return View();
        }


        public IActionResult ViewParkingTicketReport(string tokenNo)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ParkingTicketReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;          
            report.Parameters["tokenNo"].Value = tokenNo;
            report.CreateDocument();
            return PartialView("_Report", report);
        }


       


        public IActionResult CargoDeliveredAuction()
        {
            ViewData["Agents"] = _shippingRepo.GetAll()
                 .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });


            return View();
        }


        public IActionResult ViewCargoDeliveredAuction(DateTime? fromdate, DateTime? todate,  long shippingAgent )
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CargoDeliveredAuction.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["shippingAgent"].Value = shippingAgent;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult FormBAuction()
        {
            

            return View();
        }


        public IActionResult ViewFormBAuction(string igm, long index , string type)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
             
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.FormBAuction.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
             
            report.Parameters["igm"].Value = igm;
            report.Parameters["index"].Value = index;
            report.Parameters["type"].Value = type;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult RequestForWaiver()
        {


            return View();
        }


        public IActionResult ViewRequestForWaiver(string containerId, string type, string waiverno)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.RequestForWaiver.repx");
            XtraReport report = XtraReport.FromStream(resource);
 
            report.Parameters["containerId"].Value = containerId;            
            report.Parameters["type"].Value = type;
            report.Parameters["waiverno"].Value = waiverno;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ScheduleofCharges(Invoice invoice, string igm, long indexNo)
        {



            long socid = 0;

            List<InvoiceItem> invoiceitemList = new List<InvoiceItem>();
            List<InvoiceItem> normalinvoiceitemList = new List<InvoiceItem>();
 
            List<InvoiceItem> oldinvoiceitem = new List<InvoiceItem>();


            var invoicedetail = new List<ScheduleOfChargesViewModel>();

            var socitems = new List<ScheduleOfChargeItem>();


            foreach (var item in invoice.InvoiceItems)
            {
                if (item.Description == "Port Charges")
                {

                    var invoiceItem = new InvoiceItem
                    {
                        Description = item.Description,
                        Amount = item.Amount,
                        Type = "Other",
                        Category = item.Category

                    };
                    invoiceitemList.Add(invoiceItem);
                }
                else
                {

                    var invoiceItem = new InvoiceItem
                    {
                        Description = item.Description,
                        Amount = item.Amount,
                        Type = "Tariff",
                        Category = item.Category

                    };
                    invoiceitemList.Add(invoiceItem);
                }
            }

            if (invoice.SealCharger > 0)
            {
                var res = new InvoiceItem
                {
                    Description = "Seal Charges",
                    Type = "Other",
                    Amount = invoice.SealCharger,
                    Category = "SealCharges"
                };

                invoiceitemList.Add(res);
            }
            if (invoice.PortChargeAmount > 0)
            {
                var res = new InvoiceItem
                {
                    Description = "Port Charges",
                    Type = "Other",
                    Amount = invoice.PortChargeAmount,
                    Category = "PortCharges"
                };

                invoiceitemList.Add(res);
            }
            if (invoice.OtherChargeAmount > 0)
            {
                var res = new InvoiceItem
                {
                    Description = "Special Handling Charges",
                    Type = "Other",
                    Amount = invoice.OtherChargeAmount,
                    Category = "SpecialHandlingCharges"
                };

                invoiceitemList.Add(res);
            }
            if (invoice.VehicleCharges > 0)
            {
                var res = new InvoiceItem
                {
                    Description = "Vehicle Charges",
                    Type = "Other",
                    Amount = invoice.VehicleCharges,
                    Category = "VehicleCharges"
                };

                invoiceitemList.Add(res);
            }

            if (invoice.StorageTotal > 0)
            {
                var res = new InvoiceItem
                {
                    Description = "Storage Amount",
                    Type = "Storage",
                    Amount = invoice.StorageTotal,
                    Category = "Storage"
                };

                invoiceitemList.Add(res);
            }


            if (invoice.CYStorageSizeAmount > 0)
            {
                var result = invoiceitemList.Find(x => x.Type == "Storage");

                if (result != null)
                {
                    result.Amount += invoice.CYStorageSizeAmount;
                }
                else
                {
                    var res = new InvoiceItem
                    {
                        Description = "Storage Amount",
                        Type = "Storage",
                        Amount = invoice.CYStorageSizeAmount,
                         Category = "Storage"
                    };

                    invoiceitemList.Add(res);
                }
            }

            invoice.InvoiceNo = "1";
            invoice.InvoiceDate = DateTime.Now;
             
            if (invoice.Type == "CFS")
            {
                invoice.CYContainerId = null;

                invoice.InvoiceDate = DateTime.Now;

                if (invoice.AdvanceDate == null)
                {
                    invoice.AdvanceDate = DateTime.Now;
                }
                 
                if (Convert.ToDateTime(invoice.AdvanceDate.Value.Date.ToString("MM/dd/yyyy")) < Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy")))
                {
                    return new OkObjectResult(new { error = true, message = "advance date is < current date" });
                }

                var containerindex = _containerIndexRepo.GetContainerIndexById(invoice.ContainerIndexId ?? 0);

                 //var invoices = _invoiceRepo.GetInvoiceItemBycontainerIndexId(invoice.ContainerIndexId ?? 0).ToList();

 
                var consignees = _containerRepository.GetConsigneById(containerindex.ConsigneId);

                if(consignees == null)
                {
                    return new OkObjectResult(new { error = true, message = "Consignee not avaible" });
                }

                var shippingAgent = _containerRepository.GetShippingAgentId(containerindex.ShippingAgentId);

                if (shippingAgent == null)
                {
                    return new OkObjectResult(new { error = true, message = "ShippingAgent not avaible" });
                }

                var goodshead = _containerRepository.GetGoodsheadId(containerindex.GoodsHeadId);

                if (goodshead == null)
                {
                    return new OkObjectResult(new { error = true, message = "Goodshead not avaible" });
                }

                var shipping = _containerRepository.GetShippingLineId(containerindex.ShippingLineId);

                if (shipping == null)
                {
                    return new OkObjectResult(new { error = true, message = "Shipping Line not avaible" });
                }

                var examination = _containerRepository.GetExaminationRequestContainerIndexId(containerindex.ContainerIndexId);

                if (examination == null)
                {  
                    return new OkObjectResult(new { error = true, message = "examination Request not avaible" });
                }

                var cAgent = _containerRepository.GetClearingAgentId(invoice.ClearingAgentId);

                if (cAgent == null)
                {
                    return new OkObjectResult(new { error = true, message = "ClearingAgent not avaible" });
                }

                var voyge = _containerRepository.GetVoyageId(containerindex.VoyageId);

                if (voyge == null)
                {
                    return new OkObjectResult(new { error = true, message = "Voyge not avaible" });
                }
                if(voyge.VesselId == null)
                {
                    return new OkObjectResult(new { error = true, message = "Voyge not Created avaible" });
                }

                var vessl = _containerRepository.GetVesselByVoyageId(voyge.VesselId);

                if (vessl == null)
                {
                    return new OkObjectResult(new { error = true, message = "Vessel not avaible" });
                }

 

                //if (containerindex != null)
                //{
                //    var containerlist = _containerIndexRepo.GetIndexInfo(containerindex.VirNo, containerindex.IndexNo ?? 0).ToList();
                //    if (containerlist.Count() > 0)
                //    {

                //        foreach (var itemofcontainer in containerlist)
                //        {
                //            if (itemofcontainer.InvoiceLocked == true && itemofcontainer.CargoType == "LCL")
                //            {
                //                return new OkObjectResult(new { error = true, message = "The Container No" + itemofcontainer.ContainerNo + " on invoice Locked" });

                //            }
                //        }
                //    }
                //}
                //if (containerindex.IsDestuffed == false)
                //{
                //    return new OkObjectResult(new { error = true, message = "this container is not destuff" });
                //}

                //var simo = _simoRepository.GetSIMOInfo(containerindex.BLNo, containerindex.IndexNo ?? 0, containerindex.VirNo);

                //if (simo != null && simo.Status == "HO")
                //{
                //    return new OkObjectResult(new { error = true, message = "can't  create due To simo hold" });

                //}

                //if (containerindex.IsHold == true)
                //{

                //    return new OkObjectResult(new { error = true, message = "this container is currently on hold" + containerindex.AuctionLotNo });
                //}

                var invoiceres = _invoiceRepo.GetCFSFirstInvoice(invoice.ContainerIndexId ?? 0);

                if (invoiceres != null)
                {

                    var paidinvoiceitems = _invoiceRepo.GetInvoiceItemBycontainerIndexId(invoice.ContainerIndexId ?? 0).ToList();
                    
                    var usedwaive = _invoiceRepo.GetPreviousWaiverCFS(invoice.ContainerIndexId ?? 0).Where(x => x.Category != "Tariff").ToList();

                    if(paidinvoiceitems.Count() > 0)
                    {
                        
                        var newinvoiceitemList = invoiceitemList.Where(x => x.Category != "Tariff").ToList();

                        var afterinvoices = paidinvoiceitems.Where(x => x.Category != "Tariff");

                        if(newinvoiceitemList.Count() > 0)
                        {
                            foreach (var item in newinvoiceitemList)
                            {
                                var result = afterinvoices.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Amount);

                                if (result > 0)
                                {
                                    item.Amount -= result;
                                }
                            }

                            newinvoiceitemList.RemoveAll(c => c.Amount <= 0);

                            foreach (var item in newinvoiceitemList)
                            {
                                var result = usedwaive.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Discount);

                                if (result > 0)
                                {
                                    item.Amount -= result;
                                }
                            }


                            newinvoiceitemList.RemoveAll(c => c.Amount <= 0);

                            invoiceitemList = newinvoiceitemList;

                        }

                    }

                    else
                    {
                        invoiceitemList.RemoveAll(x => x.Category == "Tariff");

                        foreach (var item in invoiceitemList)
                        {
                            var result = usedwaive.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Discount);

                            if (result > 0)
                            {
                                item.Amount -= result;
                            }
                        }

                        invoiceitemList.RemoveAll(c => c.Amount <= 0);


                    }


                }

                var undeliverdresbillToLine = _containerIndexRepo.GetBillToLineInfo(containerindex.VirNo, containerindex.IndexNo ?? 0, "CFS");

                if (undeliverdresbillToLine != null)
                {
                    if (undeliverdresbillToLine.Type == "HundredPercent")
                    {
                        normalinvoiceitemList = new List<InvoiceItem>();
                    }

                    if (undeliverdresbillToLine.Type == "ExStorage")
                    {
                        normalinvoiceitemList = invoiceitemList.Where(x => x.Category == "Storage").ToList();
                    }
                    if (undeliverdresbillToLine.Type == "OnlyTariff")
                    {
                        normalinvoiceitemList = invoiceitemList.Where(x => x.Category != "Tariff").ToList();
                    }

                    if (undeliverdresbillToLine.Type == "ManualAmount")
                    {
                        var manamt = undeliverdresbillToLine.TariffAmount;
                        var manualstatus = false;
                        foreach (var item in invoiceitemList)
                        {

                            if (manamt != 0)
                            {
                                manamt = manamt - item.Amount;

                            }

                            if (manamt == 0 && manualstatus == true)
                            {

                                var req = new InvoiceItem
                                {
                                    Amount = item.Amount,
                                    Description = item.Description,
                                    Type = item.Type,
                                };

                                normalinvoiceitemList.Add(req);

                            }

                            if (manamt < 0)
                            {

                                manamt = manamt + item.Amount;

                                var req = new InvoiceItem
                                {
                                    Amount = item.Amount - manamt,
                                    Description = item.Description,
                                    Type = item.Type,

                                };

                                normalinvoiceitemList.Add(req);

                                manamt = 0;

                            }

                            if (manamt == 0)
                            {
                                manualstatus = true;
                            }

                        }

                    }

                }

                else
                {
                    normalinvoiceitemList = invoiceitemList;
                }

                //if(invoice.InvoiceItems.Count() > 0)
                //{

                //foreach (var item in invoice.InvoiceItems)
                //{

                foreach (var item in normalinvoiceitemList)
                {
                    var resdata = new ScheduleOfChargeItem
                    {
                        Amount = item.Amount,
                        Description = item.Description,
                        Type = item.Type

                    };

                    socitems.Add(resdata);

                }

                var invoicetotalchrges = socitems.Sum(x => x.Amount);
                var invoicetotalchrgesafterwaiver = invoicetotalchrges - invoice.WaiverDiscountAmount;
                var invoicetotalchrgesaftertax = Math.Ceiling ((invoicetotalchrgesafterwaiver) * (Convert.ToDouble(invoice.SalesTax) / 100));




                var inv = new ScheduleOfCharge
                        {
                            TariffInformationId = invoice.TariffInformationId,
                            LineName = shippingAgent != null ? shippingAgent.Name : "",
                            VesselName = vessl != null ? vessl.VesselName : "",
                            VoyageNo = voyge != null ? voyge.VoyageNo : "",
                            Consignee = consignees != null ? consignees.ConsigneName : "",
                            NTNNo = consignees != null ? consignees.ConsigneNTN : "",
                            STNNo = consignees != null ? consignees.STRegistrationNo : "",
                            ClearingAgent = cAgent != null ? cAgent.Name : "",
                            ContainerNo = containerindex.ContainerNo,
                            Size = containerindex.Size,
                            CYCFS = containerindex.Status,
                            ManifestedM3 = containerindex.CBM,
                            ManifestedWt = containerindex.BLGrossWeight,
                            CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                            ActualM3 = containerindex.MeasurementCBM,
                            OtherChargeAmount = invoice != null  ? invoice.OtherChargeAmount : 0,
                            Commodity = goodshead != null ? goodshead.GoodsHeadName : "",
                            BillEnteryNo = examination != null ? examination.BECashNo : "",
                            IGMDate = voyge != null ? voyge.BerthOn : null,
                            ActualWt = containerindex.BLGrossWeight,
                            Principal = shipping != null ? shipping.ShippingLineName : "",

                            InvoiceDate = invoice.InvoiceDate,
                            AdvanceDate = invoice.AdvanceDate,

                            BLM3 = examination != null ? examination.ExaminationRequestCBM : 0,
                            Indexno = containerindex.IndexNo.ToString(),
                            IGMNo = containerindex.VirNo,
                            BLNo = containerindex.BLNo,                            
                            ArriveDate = invoice.GateInDate,                             
                            StorageDays = invoice.StorageDays, 
                            Igmodesc = containerindex.Description,
                            //Amount = item.Amount,
                            //TariffName = item.Description,
                            TotalCharges = invoicetotalchrges,
                            WaiverAmount = invoice.WaiverDiscountAmount,
                            AfterWaiverAmount = invoicetotalchrgesaftertax,
                            SalesTaxAmount = invoicetotalchrgesaftertax,
                            SalesTax = invoice.SalesTax,
                            NetCharges = invoicetotalchrgesafterwaiver + invoicetotalchrgesaftertax,
                            ScheduleOfChargeItems = socitems

                };

                _scheduleOfChargeRepository.Add(inv);
                // invoicedetail.Add(inv);
                //}

                //}

                socid = inv.ScheduleOfChargeId;




            }

            if (invoice.Type == "CY")
            {
                invoice.ContainerIndexId = null;

                invoice.InvoiceDate = DateTime.Now;

                if (invoice.AdvanceDate == null)
                {
                    invoice.AdvanceDate = DateTime.Now;
                }

                if (Convert.ToDateTime(invoice.AdvanceDate.Value.Date.ToString("MM/dd/yyyy")) < Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy")))
                {
                    return new OkObjectResult(new { error = true, message = "advance date is < current date" });
                }

                //var cyContainer = _cycontainerRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == indexNo);

                 var cyContainer = _cycontainerRepo.GetFirstCYContainerByIGMIndex(igm , indexNo);



                var consignees = _containerRepository.GetConsigneById(cyContainer.ConsigneId);

                if (consignees == null)
                {
                    return new OkObjectResult(new { error = true, message = "Consignee not avaible" });
                }

                var shippingAgent = _containerRepository.GetShippingAgentId(cyContainer.ShippingAgentId);

                if (shippingAgent == null)
                {
                    return new OkObjectResult(new { error = true, message = "ShippingAgent not avaible" });
                }

                var goodshead = _containerRepository.GetGoodsheadId(cyContainer.GoodsHeadId);

                if (goodshead == null)
                {
                    return new OkObjectResult(new { error = true, message = "Goodshead not avaible" });
                }

                var shippingLine = _containerRepository.GetShippingLineId(cyContainer.ShippingLineId);

                if (shippingLine == null)
                {
                    return new OkObjectResult(new { error = true, message = "Shipping Line not avaible" });
                }

                var examination = _containerRepository.GetExaminationRequestCYContainerId(cyContainer.CYContainerId);

                if (examination == null)
                {
                    return new OkObjectResult(new { error = true, message = "examination Request not avaible" });
                }

                var cAgent = _containerRepository.GetClearingAgentId(invoice.ClearingAgentId);

                if (cAgent == null)
                {
                    return new OkObjectResult(new { error = true, message = "ClearingAgent not avaible" });
                }

              

                //var consignees = _consigneRepo.GetAll().Where(x => x.ConsigneId == cyContainer.ConsigneId).FirstOrDefault();

                //var shippingAgent = _shippingAgentRepo.GetAll().Where(x => x.ShippingAgentId == cyContainer.ShippingAgentId).FirstOrDefault();

                //var shippingLine = _shippingLineRepo.GetAll().Where(x => x.ShippingLineId == cyContainer.ShippingLineId).FirstOrDefault();

                //var examination = _examinationRequestRepo.GetAll().Where(x => x.CYContainerId == cyContainer.CYContainerId).FirstOrDefault();

                //var cAgent = _clearingAgentRepository.GetAll().Where(x => x.ClearingAgentId == invoice.ClearingAgentId).FirstOrDefault();

                ////var invoices = _invoiceRepo.GetInvoiceItemBycycontainerIdForNormal(cyContainer.CYContainerId  ).ToList();

                // var invoices = _invoiceRepo.GetAll(x => x.InvoiceItems).Where(x => x.CYContainerId == cyContainer.CYContainerId ).ToList();

                //var goodshead = _goodsHeadRepository.GetAll().Where(x => x.GoodsHeadId == cyContainer.GoodsHeadId).FirstOrDefault();

                //if (cyContainer.IsHold == true)
                //{
                //    return new OkObjectResult(new { error = true, message = "This container is currently on hold" });
                //}

                //var sim = _simRepository.GetAll().Where(x => x.ContainerNumber == cyContainer.ContainerNo && x.VIRNumber == cyContainer.VIRNo).LastOrDefault();

                //if (sim != null && sim.Status == "HO")
                //{
                //    return new OkObjectResult(new { error = true, message = "can't  create due To sim hold" });

                //}


                var invoiceres = _invoiceRepo.GetCYFirstInvoice(cyContainer.CYContainerId);
 
                if (invoiceres != null)
                {

                    var paidinvoiceitems = _invoiceRepo.GetInvoiceItemBycycontainerId(cyContainer.CYContainerId).ToList();
                     
                    var usedwaivewithoutstorage = _invoiceRepo.GetPreviousWaiverCY(cyContainer.CYContainerId).Where(x => x.Category != "Tariff" && x.Category != "Storage").ToList();

                    var usedwaivewithstorage = _invoiceRepo.GetPreviousWaiverCY(cyContainer.CYContainerId).Where(x => x.Category == "Storage").ToList();

                    if (paidinvoiceitems.Count() > 0)
                    {

                        var newinvoiceitemList = invoiceitemList.Where(x => x.Category != "Tariff").ToList();

                        var afterinvoices = paidinvoiceitems.Where(x => x.Category != "Tariff");

                        if (newinvoiceitemList.Count() > 0)
                        {
                            foreach (var item in newinvoiceitemList)
                            {
                                var result = afterinvoices.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Amount);

                                if (result > 0)
                                {
                                    item.Amount -= result;
                                }
                            }

                            newinvoiceitemList.RemoveAll(c => c.Amount <= 0);


                            foreach (var item in newinvoiceitemList.Where(x => x.Category != "Storage"))
                            {
                                var result = usedwaivewithoutstorage.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Discount);

                                if (result > 0)
                                {
                                    item.Amount -= result;
                                }
                            }


                            newinvoiceitemList.RemoveAll(c => c.Amount <= 0);


                            foreach (var item in newinvoiceitemList.Where(x => x.Description == "Storage Amount"))
                            {
                                var result = usedwaivewithstorage.Where(x => x.Category == "Storage").ToList().Sum(x => x.Discount);

                                if (result > 0)
                                {
                                    item.Amount -= result;
                                }
                            }


                            newinvoiceitemList.RemoveAll(c => c.Amount <= 0);


                            invoiceitemList = newinvoiceitemList;

                        }

                    }

                    else
                    {
                        invoiceitemList.RemoveAll(x => x.Category == "Tariff");
                          

                        foreach (var item in invoiceitemList.Where(x => x.Category != "Storage"))
                        {
                            var result = usedwaivewithoutstorage.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Discount);

                            if (result > 0)
                            {
                                item.Amount -= result;
                            }
                        }

                        invoiceitemList.RemoveAll(c => c.Amount <= 0);

                        foreach (var item in invoiceitemList.Where(x => x.Description == "Storage Amount"))
                        {

                            var result = usedwaivewithstorage.Where(x => x.Category == "Storage").ToList().Sum(x => x.Discount);

                            if (result > 0)
                            {
                                item.Amount -= result;
                            }

                        }

                        invoiceitemList.RemoveAll(c => c.Amount <= 0);
                    }


                }
                 

                //var undeliverdresbillToLine = _billToLineRepository.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexNo && x.IndexType == "CY" && x.InoviceCreated == false).LastOrDefault();
                var undeliverdresbillToLine = _cycontainerRepo.GetCYContainerBillToLineInfo(igm  ,  indexNo );
 
                if (undeliverdresbillToLine != null)
                {
                    if (undeliverdresbillToLine.Type == "HundredPercent")
                    {
                        normalinvoiceitemList = new List<InvoiceItem>();
                    }

                    if (undeliverdresbillToLine.Type == "ExStorage")
                    {
                        normalinvoiceitemList = invoiceitemList.Where(x => x.Category == "Storage").ToList();
                    }
                    if (undeliverdresbillToLine.Type == "OnlyTariff")
                    {
                        normalinvoiceitemList = invoiceitemList.Where(x => x.Category != "Tariff").ToList();
                    }

                    if (undeliverdresbillToLine.Type == "ManualAmount")
                    {
                        var manamt = undeliverdresbillToLine.TariffAmount;
                        var manualstatus = false;
                        foreach (var item in invoiceitemList)
                        {

                            if (manamt != 0)
                            {
                                manamt = manamt - item.Amount;

                            }

                            if (manamt == 0 && manualstatus == true)
                            {

                                var req = new InvoiceItem
                                {
                                    Amount = item.Amount,
                                    Description = item.Description,
                                    Type = item.Type,
                                };

                                normalinvoiceitemList.Add(req);

                            }

                            if (manamt < 0)
                            {
                                manamt = manamt + item.Amount;

                                var req = new InvoiceItem
                                {
                                    Amount = item.Amount - manamt,
                                    Description = item.Description,
                                    Type = item.Type,

                                };

                                normalinvoiceitemList.Add(req);

                                manamt = 0;

                            }

                            if (manamt == 0)
                            {
                                manualstatus = true;
                            }


                        }
                    }

                }
                else
                {
                    normalinvoiceitemList = invoiceitemList;
                }

                  


                if (cyContainer != null)
                {
                    foreach (var item in normalinvoiceitemList)
                    {
                        var resdata = new ScheduleOfChargeItem
                        {
                            Amount = item.Amount,
                            Description = item.Description,
                            Type = item.Type

                        };

                        socitems.Add(resdata);
 
                    }

                    var invoicetotalchrges = normalinvoiceitemList.Sum(x => x.Amount);
                    var invoicetotalchrgesafterwaiver = invoicetotalchrges - invoice.WaiverDiscountAmount;
                    var invoicetotalchrgesaftertax = Math.Ceiling((invoicetotalchrgesafterwaiver) * (Convert.ToDouble(invoice.SalesTax) / 100));


                    var inv = new ScheduleOfCharge
                            {

                                TariffInformationId = invoice.TariffInformationId,
                                LineName = shippingAgent != null ? shippingAgent.Name : "",
                                VesselName =   cyContainer.VesselName  ,
                                VoyageNo =  cyContainer.VoyageNo  ,
                                Consignee = consignees != null ? consignees.ConsigneName : "",
                                NTNNo = consignees != null ? consignees.ConsigneNTN : "",
                                STNNo = consignees != null ? consignees.STRegistrationNo : "",
                                ClearingAgent = cAgent != null ? cAgent.Name : "",
                                ContainerNo = cyContainer.ContainerNo,
                                Size = cyContainer.Size,
                                CYCFS = cyContainer.Status,
                                ManifestedM3 = cyContainer.CBM ?? 0,
                                ManifestedWt = cyContainer.BLGrossWeight,
                                Commodity = goodshead != null ? goodshead.GoodsHeadName : "",
                                ActualM3 = 0,

                                InvoiceDate = invoice.InvoiceDate,
                                AdvanceDate = invoice.AdvanceDate,
 
                                BillEnteryNo = examination != null ? examination.BECashNo : "",
                                IGMDate = cyContainer.BerthOn != null ? cyContainer.BerthOn : null,
                                ActualWt = cyContainer.BLGrossWeight,
                                Principal = shippingLine != null ? shippingLine.ShippingLineName : "",

                                CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                                OtherChargeAmount = invoice != null ? invoice.OtherChargeAmount : 0,
                                BLM3 = examination != null ? examination.ExaminationRequestCBM : 0,
                                Indexno = cyContainer.IndexNo.ToString() ,
                                IGMNo = cyContainer.VIRNo,
                                BLNo = cyContainer.BLNo,
                                ArriveDate = invoice.GateInDate,
                                
                                StorageDays = invoice.StorageDays,
                                Igmodesc = cyContainer.Description, 
                                TotalCharges = normalinvoiceitemList.Sum(x => x.Amount),
                                WaiverAmount = invoice.WaiverDiscountAmount,
                                AfterWaiverAmount = invoicetotalchrgesaftertax,
                                SalesTaxAmount = invoicetotalchrgesaftertax,
                                SalesTax = invoice.SalesTax,
                                NetCharges = invoicetotalchrgesafterwaiver + invoicetotalchrgesaftertax,
                                ScheduleOfChargeItems = socitems
                                 
                    };
 

                    _scheduleOfChargeRepository.Add(inv);
                    socid = inv.ScheduleOfChargeId;

                }
             
            }

             

            return new OkObjectResult(new { error = false, message = "created", InvoiceNo = socid});

        }


        public IActionResult SOCReportView()
        {  
            return View();
        }

        public IActionResult SOCReport(long SOCId)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ScheduleOfCharges.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["ScheduleOfChargeId"].Value = SOCId;
            report.Parameters["UserName"].Value = UserName;
            //  report.DataSource = invoicedetail;

            report.CreateDocument();

            return PartialView("_Report", report);
        }


        public IActionResult ScheduleofChargesEmptycontainer(SocOfEmptyContainerViewModel invoice , List<InvoiceItem> invoiceItems)
        {
            var invoicedetail = new List<SocOfEmptyContainerViewModel>();

            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.SOCOfEmptyContainer.repx");
            XtraReport report = XtraReport.FromStream(resource);
            invoicedetail.Add(invoice);
            report.DataSource = invoicedetail;
            report.CreateDocument();

            return PartialView("_Report", report);

        }



        public IActionResult SalesTaxInvoice()
        {
            return View();
        }

        public IActionResult ViewSalesTaxInvoice(string BillNo , string BillType , string InvoiceCategory)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = InvoiceCategory == "AICT" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.invoice.repx") : assembly.GetManifestResourceStream("WebdocTerminal.Reports.InvoiceForLine.repx");

            //Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.invoice.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["BillNo"].Value = BillNo;
            report.Parameters["BillType"].Value = BillType;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            
            //  string printName = new PrintDocument().PrinterSettings.PrinterName.Trim();

            //PrintToolBase printTool = new PrintToolBase(report.PrintingSystem);
            //printTool.Print();

            return PartialView("_Report", report);
        }

        //public IActionResult SalesTaxInvoiceForLine()
        //{
        //    return View();
        //}

        //public IActionResult ViewSalesTaxInvoiceForLine(string BillNo, string BillType)
        //{
        //    GetUserId();
        //    var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

        //    var assembly = typeof(ReportStorageWebExtension1).Assembly;
        //    Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.InvoiceForLine.repx");
        //    XtraReport report = XtraReport.FromStream(resource);
        //    report.Parameters["BillNo"].Value = BillNo;
        //    report.Parameters["BillType"].Value = BillType;

        //    report.Parameters["path"].Value = path;
        //    report.Parameters["CompanyId"].Value = CompanyID;

        //    report.CreateDocument();
              
        //    return PartialView("_Report", report);
        //}

        public IActionResult ViewSalesTaxInvoiceAutoPrint(string BillNo, string BillType ,   string InvoiceCategory)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            //Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.invoice.repx");
            Stream resource = InvoiceCategory == "AICT" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.invoice.repx") : assembly.GetManifestResourceStream("WebdocTerminal.Reports.InvoiceForLine.repx");

            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["BillNo"].Value = BillNo;
            report.Parameters["BillType"].Value = BillType;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();

            using (MemoryStream ms = new MemoryStream())
            {
                report.ExportToPdf(ms, new PdfExportOptions() { ShowPrintDialogOnOpen = true });
                return File(ms.ToArray(), String.Format("application/{0}", "pdf"));
             }
        }


        public IActionResult SalesTaxInvoiceAution()
        {
            return View();
        }

        public IActionResult ViewSalesTaxInvoiceAution(string BillNo , string BillType , string InvoiceCategory)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;

            Stream resource = InvoiceCategory == "AICT" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.AuctionInvoiceBill.repx") : assembly.GetManifestResourceStream("WebdocTerminal.Reports.AuctionInvoiceBillForLine.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["BillNo"].Value = BillNo;
            report.Parameters["BillType"].Value = BillType;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
  
            //PrintToolBase printTool = new PrintToolBase(report.PrintingSystem);
            //printTool.Print();

            return PartialView("_Report", report);
        }

        public IActionResult EquipmentInterchangeAndInspectionReport()
        {
            return View();
        }

        public IActionResult ViewEquipmentInterchangeAndInspectionReport(string igm, string ContainerNumber, string Status)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.EquipmentInterchangeAndInspectionReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["containerNumber"].Value = ContainerNumber;
            report.Parameters["virnumber"].Value = igm;
            report.Parameters["status"].Value = Status;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult PickUpTicket()
        {
            return View();
        }

        public IActionResult ViewPickUpTicket(string type, long TruckInOutId)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.Pickupticket.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["ContainerType"].Value = type;
            report.Parameters["TruckInOutId"].Value = TruckInOutId;

            report.Parameters["path"].Value = path;
            report.Parameters["ConpanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }



        public IActionResult TruckInOutSummary()
        {
            return View();
        }

        public IActionResult ViewTruckInOutSummary(DateTime? fromdate, DateTime? toDate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.TruckInOutSummary.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult TruckInDetail()
        {
            return View();
        }

        public IActionResult ViewTruckInDetail()
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.TruckInDetail.repx");
            XtraReport report = XtraReport.FromStream(resource);
            
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult TariffSummaryReport()
        {
            return View();
        }

        public IActionResult ViewTariffSummaryReport()
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.AssignTariffDetail.repx");
            XtraReport report = XtraReport.FromStream(resource);
              
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult DesealingCheckReport()
        {
            return View();
        }


        public IActionResult ViewDesealingCheckReport(DateTime? fromdate, DateTime? toDate)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DesealingCheck.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
 
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult FullContainerInventoryReport()
        {
            return View();
        }


        public IActionResult ViewFullContainerInventoryReport(DateTime? fromdate, DateTime? toDate)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.FullContainerInventory.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult EmptyContainerReceivedDeliveryOrder()
        {
            return View();
        }

        public IActionResult ViewEmptyContainerReceivedDeliveryOrder(long donum)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.EmptyContainerReceivedDeliveryOrder.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["deliveryOrderNumber"].Value = donum;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult EmptyContainerReceivedInvoice()
        {
            return View();
        }

        public IActionResult ViewEmptyContainerReceivedInvoice(string invoiceno)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.EmptyContainerReceivedInvoice.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["invoiceno"].Value = invoiceno;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult CFSShedClearIndex()
        {
            return View();
        }

        public IActionResult ViewCFSShedClearIndex(DateTime? fromdate, DateTime? todate, string type, string ctype)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CfsShedClearedIndexes.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy"); 
            report.Parameters["type"].Value = type;
            report.Parameters["containertype"].Value = ctype;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ExaminationMarkedNotGrounded()
        {
            return View();
        }
        public IActionResult ViewExaminationMarkedNotGrounded(DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ExaminationMarkedNotGrounded.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy"); ;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult CFSCRLREPORT()
        {
            return View();
        }
        public IActionResult ViewCFSCRLREPORT(DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CFSCRLREPORT.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy"); ;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult WebocIGMDetail()
        {
            return View();
        }

        public IActionResult ViewWebocIGMDetail(DateTime? fromdate, DateTime? toDate , string igmnumber)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.WebocIGMDetail.repx");
            XtraReport report = XtraReport.FromStream(resource);


            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["igmnumber"].Value = igmnumber;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            //report.CreateDocument(false);
            //PrintToolBase printTool = new PrintToolBase(report.PrintingSystem);
            ////printTool.PrinterSettings.PrinterName = "";
            //printTool.Print();

            return PartialView("_Report", report);
        }
        public IActionResult IgmRecContNotArrive()
        {
            return View();
        }
        public IActionResult ViewIgmRecContNotArrive(DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.IGMRECCONTNOTARRIVE.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy"); ;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult IgmRecNotInAtlas()
        {
            return View();
        }
        public IActionResult ViewIgmRecNotInAtlas(DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.IGMRECNOTINATLAS.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy"); ;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult DoWithPendingGatepass()
        {
            return View();
        }

        public IActionResult ViewDoWithPendingGatepass(DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.Dowithpendinggatepass.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy"); ;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult ShedBrtContainerWise()
        {
            return View();
     
        }
        public IActionResult ViewShedBrtContainerWise(string containerNo)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.shedbrtcontainerwise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["containerNo"].Value = containerNo;
        
            report.Parameters["path"].Value = path;
            report.Parameters["ComapnyId"].Value = CompanyID;
            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult shedexamination()
        {

            ViewData["GoodsHeadItems"] = _goodsHeadRepository.GetAll()
            .Select(v => new SelectListItem { Text = v.GoodsHeadName , Value = v.GoodsHeadName });

            return View();
        }
        public IActionResult Viewshedexamination(DateTime? fromdate, DateTime? todate, string commodity)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.shedexamination.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["commodity"].Value = commodity;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy"); ;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult ShedExcessWeight()
        {
            return View();
        }
        public IActionResult ViewShedExcessWeight(DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ShedExcessWeight.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy"); ;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult Track_Destuff_Container()

        {
            ViewData["Shippingline"] = _shippingLineRepo.GetAll()
                .Select(s => new SelectListItem { Text = s.ShippingLineName, Value = s.ShippingLineId.ToString() });

            ViewData["Goodshead"] = _goodsHeadRepository.GetAll()
               .Select(s => new SelectListItem { Text = s.GoodsHeadName, Value = s.GoodsHeadId.ToString() });


            return View();
        }
        public IActionResult ViewTrack_Destuff_Container( long Shippingline,string Goodshead)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.Track_Destuff_Container.repx");
            XtraReport report = XtraReport.FromStream(resource);
           
            report.Parameters["Shippingline"].Value = Shippingline;
            report.Parameters["Goodshead"].Value = Goodshead;
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.CreateDocument();
            // return View(report);
            return PartialView("_Report", report);
        }
        public IActionResult MaerskAfterArrival()
        {
            return View();
        }

        public IActionResult ViewMaerskAfterArrival(string igmno,string containerno)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.MaerskAfterArrival.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["igmno"].Value = igmno;
            report.Parameters["containerno"].Value = containerno;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult MaerskArrival()
        {
            return View();
        }

        public IActionResult ViewMaerskArrival(DateTime? fromdate, DateTime? toDate, string igmno)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.MaerskArrival.repx");
            XtraReport report = XtraReport.FromStream(resource);


            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["igmno"].Value = igmno;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult PendingAtQICT()
        {
            return View();
        }
        public IActionResult ViewPendingAtQICT(DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.PendingAtQICT.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy"); 

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult InvoiceSummaryAgentWise()

        {
            ViewData["agent"] = _clearingAgentRepository.GetAll()
                .Select(s => new SelectListItem { Text = s.Name, Value = s.ClearingAgentId.ToString() });

            return View();
        }
        public IActionResult ViewInvoiceSummaryAgentWise(DateTime? fromdate, DateTime? todate ,long agent)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.InvoiceSummaryAgentWise.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["agent"].Value = agent;

            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.CreateDocument();
            // return View(report);
            return PartialView("_Report", report);
        }
        public IActionResult ImportContainerDestuffinglList()
        {
            return View();
        }
        public IActionResult ViewImportContainerDestuffinglList(DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ImportContainerDestuffinglList.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy"); ;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult CarDestuffingContainerWise()
        {
            return View();
        }


        public IActionResult ViewCarDestuffingContainerWise(DateTime? fromdate, DateTime? toDate)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CarDestuffingContainerWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult WorkOrderForLabour()
        {
            return View();
        }


        public IActionResult ViewWorkOrderForLabour(DateTime? fromdate, DateTime? toDate)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.WorkOrderForLabour.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult BrtRacksInformation()
        {

            return View();
        }


        public IActionResult ViewBrtRacksInformation()
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.BrtRacksInformation.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult invoicelist()
        {
            return View();
        }

        public IActionResult Viewinvoicelist(string container, DateTime? fromdate, DateTime? toDate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.invoicelist.repx");
            XtraReport report = XtraReport.FromStream(resource);


            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["container"].Value = container;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();

            //PrintToolBase tool = new PrintToolBase(report.PrintingSystem);
            //tool.Print();
             
            //PrintToolBase toola = new PrintToolBase(report.PrintingSystem);

            //toola.PrintingSystem.ShowMarginsWarning = false; 

            //toola.PrintingSystem.ShowPrintStatusDialog = false;  
 
            //toola.Print();


            return PartialView("_Report", report);
        }
        public IActionResult ImportFullStockArriveMF()
        {
            ViewData["Agents"] = _shippingAgentRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });



            return View();
        }


        public IActionResult ViewImportFullStockArriveMF(DateTime? fromdate, DateTime? todate , long shippingAgent , string containerType)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ImportFullStockArriveMF.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            report.Parameters["Startdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["Enddate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["agent"].Value = shippingAgent;
            report.Parameters["Type"].Value = containerType;

            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult ImportFullStockMF()
        {
            ViewData["Agents"] = _shippingAgentRepo.GetAll()
          .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });



            return View();
        }


        public IActionResult ViewImportFullStockMF(DateTime? fromdate , DateTime? todate , long agent)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ImportFullStockMF.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            
            report.Parameters["Startdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["Enddate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["agent"].Value = agent;
            

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult DestuffingDeliveryReport()
        {
            return View();
        }


        public IActionResult ViewDestuffingDeliveryReport(DateTime? fromdte, DateTime? todte)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DestuffingDeliveryReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["fromdte"].Value = fromdte?.ToString("MM/dd/yyyy");
            report.Parameters["todte"].Value = todte?.ToString("MM/dd/yyyy");             
            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult DailyTerminalReport()
        {
            return View();
        }


        public IActionResult ViewDailyTerminalReport(DateTime? fromdate)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DailyTerminalReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");


            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult DailySalesSummaryLineWise()
        {
            return View();
        }


        public IActionResult ViewDailySalesSummaryLineWise(DateTime? fromdate)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DailySalesSummaryLineWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");


            report.CreateDocument();
            return PartialView("_Report", report);
        }
        public IActionResult ShortDailySalesSummary()
        {
            return View();
        }


        public IActionResult ViewShortDailySalesSummary(DateTime? fromdate ,  DateTime? todate)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ShortDailySalesSummary.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");


            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult AICTAndLineIndexReport()
        {

            ViewData["Shippingline"] = _shippingAgentRepo.GetAll()
                    .Select(s => new SelectListItem { Text = s.Name, Value = s.ShippingAgentId.ToString() });

            ViewData["Goodshead"] = _goodsHeadRepository.GetAll()
               .Select(s => new SelectListItem { Text = s.GoodsHeadName, Value = s.GoodsHeadId.ToString() });

            return View();
        }


        public IActionResult ViewAICTAndLineIndexReport(string goodsheadid , string shippingagentid)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.AICTAndLineIndexReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["goodsheadid"].Value = goodsheadid;
            report.Parameters["shippingagentid"].Value = shippingagentid;


            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult IndexUploadErrorTracking()
        {
            return View();
        }

        public IActionResult ViewIndexUploadErrorTracking(DateTime? fromdate, DateTime? toDate)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.IndexUploadErrorTracking.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");

             
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult EdoReportView()
        {
            return View();
        }



        public IActionResult ViewEdoReport(long edoid)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.EdoReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["edoid"].Value = edoid; 


            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult PartyLetPassAmount()
        {
            ViewData["patry"] = _clearingAgentRepository.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ClearingAgentId.ToString() });

            ViewData["shippingagent"] = _shippingAgentRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewPartyLetPassAmountFromAgent(long partyId , long shippingAgent ,  DateTime? fromdate ,  DateTime? todate)
        {

            long letpassartyid = 0;

            var res = _partyLedgerRepository.PartyByClearnignAgentID(partyId);

            if(res != null)
            {
                letpassartyid = res.PartyId;
            }


            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.PartyLetPassAmount.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["partyId"].Value = letpassartyid;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ViewPartyLetPassAmount(long partyId)
        {
             
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.PartyLetPassAmount.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["partyId"].Value = partyId;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult BillToLineReportView()
        {
            ViewData["Agents"] = _shippingAgentRepo.GetAll()
             .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });
             
            return View();
        }


        


        public IActionResult ViewBillToLineReport(DateTime? fromdate, DateTime? toDate, long shippingagentId )
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.BillToLineReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["agent"].Value = shippingagentId;
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult CashierDailySaleReportView()
        { 
            return View();
        }





        public IActionResult ViewCashierDailySaleReport(DateTime? fromdate, DateTime? toDate , string type)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            //Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CashierDailySaleReport.repx");
            Stream resource = type == "WithOutConsignee" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.CashierDailySaleReport.repx") : assembly.GetManifestResourceStream("WebdocTerminal.Reports.CashierDailySaleReportSec.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult BreakupofCashierDailySaleReportView()
        {
            ViewData["shippingagent"] = _shippingAgentRepo.GetAll().Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });


            return View();
        }





        public IActionResult ViewBreakupofCashierDailySaleReport(DateTime? fromdate, DateTime? toDate , string type , long? shippingagentId)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.BreakupofCashierDailySaleReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["type"].Value = type;
            report.Parameters["shipingagent"].Value = shippingagentId;
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult DailySalesReportView()
        {
            ViewData["shippingagent"] = _shippingAgentRepo.GetAll().Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        } 
        public IActionResult ViewDailySalesReport(DateTime? fromdate, DateTime? toDate, long? shippingagentId)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DailySalesReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.Parameters["shipingagent"].Value = shippingagentId;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ArrivalBoxWiseView()
        {

            ViewData["portAndTerminal"] = _portAndTerminalRepository.GetAll()
             .Select(v => new SelectListItem { Text = v.PortName, Value = v.PortAndTerminalId.ToString() });


            ViewData["shippingAgent"] = _shippingAgentRepo.GetAll()
             .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["transporter"] = _transporterRepository.GetAll()
             .Select(v => new SelectListItem { Text = v.TransporterName, Value = v.TransporterId.ToString() });


            ViewData["goodsHead"] = _goodsHeadRepository.GetAll()
             .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });

            return View();
        }
        public IActionResult ViewArrivalBoxWise(DateTime? fromdate, DateTime? toDate , long? port , long? Principle , long? Transporter , string Type , string Cargo , long? Goods)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ArrivalBoxWise.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy"); ;
            report.Parameters["port"].Value = port;
            report.Parameters["Principle"].Value = Principle;
            report.Parameters["Transporter"].Value = Transporter;
            report.Parameters["Type"].Value = Type;
            report.Parameters["Cargo"].Value = Cargo;
            report.Parameters["Goods"].Value = Goods;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult LCLIndexRateView()
        {

            ViewData["masterShippingAgent"] = _masterShippingAgentRepository.GetAll()
             .Select(v => new SelectListItem { Text = v.MasterShippingAgentName, Value = v.MasterShippingAgentId.ToString() });
             
            ViewData["shippingAgent"] = _shippingAgentRepo.GetAll()
             .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });
             
            return View();
        }

        public IActionResult ContainerArrivalDetailView()
        {  
            return View();
        }


        public IActionResult ViewContainerArrivalDetailView(string ContainerNumber)
        { 
            
              

            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ContainerArrivalReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
             
            report.Parameters["ContainerNo"].Value = ContainerNumber; 

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult LineBoxIndexWiseDetailReportView()
        {
            ViewData["shippingAgent"] = _shippingAgentRepo.GetAll()
          .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["masterShippingAgent"] = _masterShippingAgentRepository.GetAll()
             .Select(v => new SelectListItem { Text = v.MasterShippingAgentName, Value = v.MasterShippingAgentId.ToString() });

            return View();
        }

        public JsonResult  UploadAictLineIndexReport(DateTime? fromdate, DateTime? todate, DateTime? uploadfromdate, DateTime? uploadtodate, string igm, string Containerno,
                                                                                 long? masterline, long? line, string Delivered, string billtoline)
        {

            if ( igm == "" || igm == "null")
            {
                igm = null;
            }
            if ( Containerno == "" || Containerno == "null")
            {
                Containerno = null;
            }

            var res = _invoiceRepo.UploadAictLineIndexReport( fromdate,  todate,  uploadfromdate,  uploadtodate, igm, Containerno, masterline, line, Delivered, billtoline).ToList();

            return Json(res);
        }
        public IActionResult InquiryFormView()
        {         
            return View();
        }

        public IActionResult DODetailView()
        {
            return View();
        }

        public IActionResult ViewDODetail(DateTime? date)
        {           
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DODetail.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            
            report.Parameters["date"].Value = date?.ToString("MM/dd/yyyy");
            

            return PartialView("_Report", report);
        }


        public IActionResult ContianerTrackingReportCFSAndCYView()
        {
            ViewData["Agents"] = _masterShippingAgentRepository.GetAll()
            .Select(v => new SelectListItem { Text = v.MasterShippingAgentName, Value = v.MasterShippingAgentId.ToString() });


            return View();
        }

        public IActionResult ViewContianerTrackingReportCFSAndCY( long shippingagnet ,   DateTime? fromDate ,   DateTime? todate, string type)
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ContianerTrackingReportCFSAndCY.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.Parameters["type"].Value = type;
            report.Parameters["shippingagnet"].Value = shippingagnet;
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
             

            return PartialView("_Report", report);
        }

        public IActionResult IGMReceivedContainerNotArriveView()
        { 

            return View();
        }

        public IActionResult ViewIGMReceivedContainerNotArrive()
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.IGMReceivedContainerNotArrive.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            
            return PartialView("_Report", report);
        }

        public IActionResult ContainerDestuffingReportView()
        {
            ViewData["Agents"] = _shippingAgentRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });


            return View();
        }

        public IActionResult ViewContainerDestuffingReport(long line, DateTime? fromdate, DateTime? todate, string containerno)
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ContainerDestuffingReport1.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            //report.Parameters["path"].Value = path;
            //report.Parameters["CompanyId"].Value = CompanyID;

            report.Parameters["continerno"].Value = containerno;
            report.Parameters["line"].Value = line;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");


            return PartialView("_Report", report);
        }



        public IActionResult EIRReport()
        {
            return View();
        }

        public IActionResult ViewEIRReport(string igm, string ContainerNumber )
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.EquipmentInterchangeAndInspectionReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["containerNumber"].Value = ContainerNumber;
            report.Parameters["virnumber"].Value = igm;
            report.Parameters["status"].Value = null;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ContainerOffHireReport()
        {

            ViewData["Agents"] = _shippingAgentRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });


            return View();
        }

        public IActionResult ViewContainerOffHireReport(long line, DateTime? fromdate, DateTime? todate, string containerno)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ContainerOffHireReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
             

            report.Parameters["ContainerNo"].Value = containerno;
            report.Parameters["ShippingAgent"].Value = line;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ContainerInventory()
        { 
            return View();
        }

        public IActionResult ViewContainerInventory()
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ContainerInventory.repx");
            XtraReport report = XtraReport.FromStream(resource);
             
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult MonthlyBillToLineAccount()
        {

            ViewData["Agents"] = _shippingAgentRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["MasterLine"] = _masterShippingAgentRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.MasterShippingAgentName, Value = v.MasterShippingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewCMonthlyBillToLineAccount(long line, long masterline, DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.MonthlyBillToLineAccount.repx");
            XtraReport report = XtraReport.FromStream(resource);


            report.Parameters["line"].Value = line;
            report.Parameters["masterline"].Value = masterline;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult DailySalesSummaryReport()
        {
            return View();
        }
        public IActionResult ViewDailySalesSummaryReport(DateTime? fromdate, DateTime? toDate)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DailySalesSummaryReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult DailySalesSummary()
        {
            return View();
        }
        public IActionResult ViewDailySalesSummary(DateTime? fromdate, DateTime? toDate)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DailySalesSummary.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = toDate?.ToString("MM/dd/yyyy");
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult UndeliveredIndexesAgentWise()
        {

            ViewData["Agents"] = _shippingAgentRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });
             
            ViewData["ClearingAgent"] = _clearingAgentRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.Name, Value = v.ClearingAgentId.ToString() });

            ViewData["Good"] = _goodsHeadRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });

            return View();
        }

        public IActionResult ViewUndeliveredIndexesAgentWise(long shippingAgent, long type,  string cargotype,  long consignee , long clearingagent
                                                            , long good, DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.UndeliveredIndexesAgentWise.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["type"].Value = type;
            report.Parameters["cargotype"].Value = cargotype;
            report.Parameters["consignee"].Value = consignee;
            report.Parameters["clearingagent"].Value = clearingagent;
            report.Parameters["good"].Value = good;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ImportBalanceCycontainer()
        {

            ViewData["Agents"] = _shippingLineRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.ShippingLineName, Value = v.ShippingLineId.ToString() });


            ViewData["GoodsHead"] = _goodsHeadRepository.GetAll()
            .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });


            return View();
        }

        public IActionResult ViewImportBalanceCycontainer(long shippingAgent, DateTime? fromdate, DateTime? todate, long goodsHeadId)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ImportBalanceCycontainer.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["goodsHead"].Value = goodsHeadId;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult ImportBalanceCfsCargo()
        {

            ViewData["Agents"] = _shippingLineRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.ShippingLineName, Value = v.ShippingLineId.ToString() });

            ViewData["GoodsHead"] = _goodsHeadRepository.GetAll()
            .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });

            return View();
        }


        public IActionResult ViewImportBalanceCfsCargo(long shippingAgent ,  string cargoType, DateTime? fromdate, DateTime? todate , long goodsHeadId)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ImportBalanceCfsCargo.repx");
            XtraReport report = XtraReport.FromStream(resource);
             
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["type"].Value = cargoType;
            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["goodsHead"].Value = goodsHeadId;

            report.CreateDocument();
            return PartialView("_Report", report);
        }



        public IActionResult CargoDeliveredCY()
        {

            ViewData["shippingLine"] = _shippingLineRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.ShippingLineName, Value = v.ShippingLineId.ToString() });

            return View();
        }


        public IActionResult ViewCargoDeliveredCY(long shippingAgent, DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CargoDeliveredCY.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["line"].Value = shippingAgent;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult CargoDeliveredLCL()
        {

            ViewData["shippingLine"] = _shippingLineRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.ShippingLineName, Value = v.ShippingLineId.ToString() });


            return View();
        }


        public IActionResult ViewCargoDeliveredLCL(long line, DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CargoDeliveredLCL.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["line"].Value = line;

            report.CreateDocument();
            return PartialView("_Report", report);

        }

        public IActionResult CargoDeliveredLCLLineWise()
        {

            ViewData["shippingLine"] = _shippingLineRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.ShippingLineName, Value = v.ShippingLineId.ToString() });


            return View();
        }

        // in this report for line name printing in group header edit and reports binds behavior tab use group unique
        public IActionResult ViewCargoDeliveredLCLWithLine(long line, DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CargoDeliveredLCLLineWise.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["line"].Value = line;

            report.CreateDocument();
            return PartialView("_Report", report);

        }


        public IActionResult ReprtInvoiceBillList()
        {

            ViewData["shippingLine"] = _shippingLineRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.ShippingLineName, Value = v.ShippingLineId.ToString() });


            return View();
        }
         
        public IActionResult ViewReprtInvoiceBillList(long line, DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ReprtInvoiceBillList.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["fromDate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["line"].Value = line;

            report.CreateDocument();
            return PartialView("_Report", report);

        }

        public IActionResult PendingContainer()
        {

            ViewData["Agents"] = _shippingAgentRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });


            return View();
        }

        public IActionResult ViewPendingContainer(long line, string port)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.PendingContainer.repx");
            XtraReport report = XtraReport.FromStream(resource);
             
            report.Parameters["line"].Value = line;
            report.Parameters["port"].Value = port;

            report.CreateDocument();
            return PartialView("_Report", report);

        }


        public IActionResult CRData()
        {

            ViewData["Agents"] = _shippingAgentRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewCRData(long line, DateTime? fromdate, DateTime? todate , string cargoType)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = cargoType == "CY" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.CRDataReport.repx") : cargoType == "FCL" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.CRDataFCL.repx") : cargoType == "LCL" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.CRDataLCL.repx") : cargoType == "ALL" ? assembly.GetManifestResourceStream("WebdocTerminal.Reports.ALLCRData.repx") : assembly.GetManifestResourceStream("");
              
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["fromDate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["line"].Value = line;

            report.CreateDocument();
            return PartialView("_Report", report);

        }


        public IActionResult GatePassSampleReport()
        { 
            return View();
        }

        public IActionResult ViewGatePassSampleReport(long gatePassSampleid, string type)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.GatePassSampleReport.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["gatePassSampleid"].Value = gatePassSampleid;
            report.Parameters["type"].Value = type;
       

            report.CreateDocument();
            return PartialView("_Report", report);

        }



        public IActionResult ImportFullContainersStock()
        {

            ViewData["Agents"] = _shippingAgentRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["shippingLine"] = _shippingLineRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.ShippingLineName, Value = v.ShippingLineId.ToString() });

            ViewData["Port"] = _portAndTerminalRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.PortName, Value = v.PortAndTerminalId.ToString() });

            return View();
        }

        public IActionResult ViewImportFullContainersStock(long shippingAgent  , long shipingLine, long port , string cargoType, DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ImportFullContainersStock.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["Principle"].Value = shippingAgent;
            report.Parameters["containerline"].Value = shipingLine;
            report.Parameters["port"].Value = port;
            report.Parameters["Cargo"].Value = cargoType;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult TuesReport()
        {

            ViewData["Agents"] = _shippingAgentRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });
             
            ViewData["Port"] = _portAndTerminalRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.PortName, Value = v.PortAndTerminalId.ToString() });

            return View();
        }

        public IActionResult ViewTuesReport(long shippingAgent,  long port,   DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.TuesReport.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["line"].Value = shippingAgent; 
            report.Parameters["port"].Value = port; 
            report.Parameters["Startdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["Enddate"].Value = todate?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult CRDataSummary()
        {

            ViewData["Agents"] = _shippingAgentRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["CAgents"] = _clearingAgentRepository.GetAll()
            .Select(c => new SelectListItem { Text = c.Name, Value = c.ClearingAgentId.ToString() });

             
            return View();
        }

        public IActionResult ViewCRDataSummary(long? shippingAgent, long? clearingagent, long? consignee, string type , string cargotype, DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CRDataSummary.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["shippingAgent"].Value = shippingAgent;
            report.Parameters["clearingagent"].Value = clearingagent;
            report.Parameters["consignee"].Value = consignee;
            report.Parameters["type"].Value = type;
            report.Parameters["cargotype"].Value = cargotype;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }

         

        public JsonResult CRDataSummaryRes(long? shippingAgent, long? clearingagent, long? consignee, string type, string cargotype, DateTime? fromdate, DateTime? todate)
        {

            var listItems = new List<CRShortSummaryViewModel>();



            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string conString = Con;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("CRShortSummary", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 360;

                    command.Parameters.AddWithValue("@shippingAgent", shippingAgent);
                    command.Parameters.AddWithValue("@clearingagent", clearingagent);
                    command.Parameters.AddWithValue("@consignee", consignee);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@cargotype", cargotype);
                    command.Parameters.AddWithValue("@fromdate", fromdate?.ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("@todate", todate?.ToString("MM/dd/yyyy"));


                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(command);

                    sda.Fill(ds);

                    listItems = ds.Tables[0].AsEnumerable()
                        .Select(datarow => new CRShortSummaryViewModel()
                        {
                            srno = datarow.Field<long>("srno"),
                            srinvoicedate = datarow.Field<DateTime>("srinvoicedate"),
                            LineName = datarow.Field<string>("LineName"),
                            ContainerNo = datarow.Field<string>("ContainerNo"),
                            GateInDate = datarow.Field<string>("GateInDate"),
                            PortName = datarow.Field<string>("PortName"),
                            VirNo = datarow.Field<string>("VirNo"),
                            IndexNo = datarow.Field<int>("IndexNo"),
                            containerdescription = datarow.Field<string>("containerdescription"),                            
                            VesselName = datarow.Field<string>("VesselName"),
                            Voyage = datarow.Field<string>("Voyage"),
                            InvoiceNo = datarow.Field<string>("InvoiceNo"),
                            BillType = datarow.Field<string>("BillType"),
                            InvoiceDate = datarow.Field<string>("InvoiceDate"),
                            CfsCy = datarow.Field<string>("CfsCy"),
                            CargoType = datarow.Field<string>("CargoType"),
                            GoodsHeadName = datarow.Field<string>("GoodsHeadName"),
                            BLNo = datarow.Field<string>("BLNo"),
                            ConsigneName = datarow.Field<string>("ConsigneName"),
                            ClearingAgentName = datarow.Field<string>("ClearingAgentName"),
                            DestuffDate = datarow.Field<string>("DestuffDate"),
                            TotalCharges = datarow.Field<double>("TotalCharges"),
                            Qty20 = datarow.Field<int>("Qty20"),
                            Qty40 = datarow.Field<int>("Qty40"),
                            Qty45 = datarow.Field<int>("Qty45"),
                            TotalArrivedCBM = datarow.Field<int>("TotalArrivedCBM"),
                            TotalArrivedIndexes = datarow.Field<int>("TotalArrivedIndexes"),
                            TotalDays = datarow.Field<int?>("TotalDays"),
                            FreeDays = datarow.Field<int>("FreeDays"), 
                            StorageDays = datarow.Field<int>("StorageDays"),
                            BTLAmount = datarow.Field<double>("BTLAmount"),
                            BTLRemarks = datarow.Field<string>("BTLRemarks"),
                            DLVCBMWt = datarow.Field<double>("DLVCBMWt"),
                            Auction = datarow.Field<double>("Auction"),
                            ExaminationCharges = datarow.Field<double>("ExaminationCharges"),
                            SealCharge = datarow.Field<double>("SealCharge"),
                            PortCharges = datarow.Field<double>("PortCharges"),
                            SpecialHandlingCharges = datarow.Field<double>("SpecialHandlingCharges"),
                            Storage = datarow.Field<double>("Storage"),
                            TariffRate = datarow.Field<double>("TariffRate"),
                            FuelAdjcharges = datarow.Field<double>("FuelAdjcharges"),
                            FuelAdjustmentcharges = datarow.Field<double>("FuelAdjustmentcharges"),
                            AdditionalFUELCAF = datarow.Field<double>("AdditionalFUELCAF"),
                            DocumentCharges = datarow.Field<double>("DocumentCharges"),
                            DocumentionCharges = datarow.Field<double>("DocumentionCharges"),
                            Document = datarow.Field<double>("Document"),
                            ScannerDetention = datarow.Field<double>("ScannerDetention"),
                            ScanningQ = datarow.Field<double>("ScanningQ"),
                            ShifftingChargesQ = datarow.Field<double>("ShifftingChargesQ"),
                            CargoHandling = datarow.Field<double>("CargoHandling"),
                            CFSCharges = datarow.Field<double>("CFSCharges"),
                            CurrencyAdjustment = datarow.Field<double>("CurrencyAdjustment"),
                            CustomerCharges = datarow.Field<double>("CustomerCharges"),
                            CustomsSealCharges = datarow.Field<double>("CustomsSealCharges"),
                            DataProcessiong = datarow.Field<double>("DataProcessiong"),
                            DestuffingCharges = datarow.Field<double>("DestuffingCharges"),
                            DoubleHaulageCharges = datarow.Field<double>("DoubleHaulageCharges"),
                            Examination = datarow.Field<double>("Examination"),
                            GateInCharges = datarow.Field<double>("GateInCharges"),
                            GeneralCargoExamination = datarow.Field<double>("GeneralCargoExamination"),
                            HandlingCharges = datarow.Field<double>("HandlingCharges"),
                            InsuranceCharges = datarow.Field<double>("InsuranceCharges"),
                            LCLCharges = datarow.Field<double>("LCLCharges"),
                            MeasurementAndSurvey = datarow.Field<double>("MeasurementAndSurvey"),
                            MeasurementCharges = datarow.Field<double>("MeasurementCharges"),
                            PortTariffCharges = datarow.Field<double>("PortTariffCharges"),
                            QictRAndD = datarow.Field<double>("QictRAndD"),
                            RecevialDelivery = datarow.Field<double>("RecevialDelivery"),
                            ServiceChargesDestuffing = datarow.Field<double>("ServiceChargesDestuffing"),
                            ShiftingCharges = datarow.Field<double>("ShiftingCharges"),
                            SpecialCharges = datarow.Field<double>("SpecialCharges"),
                            SurveyCharges = datarow.Field<double>("SurveyCharges"),
                            TerminalCharges = datarow.Field<double>("TerminalCharges"),
                            TransPortationDeliveryCharges = datarow.Field<double>("TransPortationDeliveryCharges"),
                            Weighment = datarow.Field<double>("Weighment"),
                            WeighmentCharges = datarow.Field<double>("WeighmentCharges"),
                            Wharfage = datarow.Field<double>("Wharfage"),
                            YardCharges = datarow.Field<double>("YardCharges"),
                            AICTPerCBMRate = datarow.Field<double>("AICTPerCBMRate"),
                            AICTPerIndexRate = datarow.Field<double>("AICTPerIndexRate"),
                            FFPerCBMRate = datarow.Field<double>("FFPerCBMRate"),
                            FFPerIndexRate = datarow.Field<double>("FFPerIndexRate"),
                            AICTPerCBMRateShareRate = datarow.Field<double>("AICTPerCBMRateShareRate"),
                            AICTPerIndexRateShareRate = datarow.Field<double>("AICTPerIndexRateShareRate"),
                            FFPerCBMRateShareRate = datarow.Field<double>("FFPerCBMRateShareRate"),
                            FFPerIndexRateShareRate = datarow.Field<double>("FFPerIndexRateShareRate"),
                            PerBoxRate = datarow.Field<double>("PerBoxRate"),
                            otherchargesParty = datarow.Field<double>("otherchargesParty"),
                            otherchargesFFLine = datarow.Field<double>("otherchargesFFLine"),
                            otherchargesAict = datarow.Field<double>("otherchargesAict"),
                            otherchargesClearingAgent = datarow.Field<double>("otherchargesClearingAgent"),
                            StorageWaiver = datarow.Field<double>("StorageWaiver"),
                            OtherWaiver = datarow.Field<double>("OtherWaiver"),


                        }).ToList();

                    con.Close();

                }
            }

            return Json(listItems);
        }

        public IActionResult ManualGatepassReport()
        { 
            return View();
        }

        public IActionResult ViewManualGatepassReport(string type,  long gatePassId)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ManualGatepassReport.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["type"].Value = type;
            report.Parameters["gatePassId"].Value = gatePassId; 

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult EPZCargo()
        {     
            return View();
        }

        public IActionResult ViewEPZCargo(  DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.EPZCargoReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
             
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult OutstandingSpecialHanlingCharges()
        {
            return View();
        }

        public IActionResult ViewOutstandingSpecialHanlingCharges(DateTime? fromDate, DateTime? toDate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.OutstandingSpecialHanlingCharges.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["toDate"].Value = toDate?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult DeliveryOrderDetailView()
        {
            return View();
        }

        public IActionResult ViewDeliveryOrderDetail(DateTime? fromdate , DateTime? todate)
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DeliveryOrderDetail.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");

            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
             
            return PartialView("_Report", report);
        }

        public IActionResult SealIssuanceDetailView()
        {
            return View();
        }

        public IActionResult ViewSealIssuanceDetail(DateTime? fromdate, DateTime? todate)
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.SealIssuanceDetail.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");

            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");

            return PartialView("_Report", report);
        }

        public IActionResult ContainerArrivalSummaryReportCSD()
        {

            ViewData["PortAndterminal"] = _portAndTerminalRepository.GetAll()
            .Select(v => new SelectListItem { Text = v.PortName, Value = v.PortAndTerminalId.ToString() });

            ViewData["GoodsHead"] = _goodsHeadRepository.GetAll()
            .Select(g => new SelectListItem { Text = g.GoodsHeadName, Value = g.GoodsHeadId.ToString() });

            return View();
        }

        public IActionResult ViewContainerArrivalSummaryReportCSD(string Cargo, string Type, long? port, long? commodity , DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ContainerArrivalSummaryReportCSD.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["Cargo"].Value = Cargo;
            report.Parameters["Type"].Value = Type;
            report.Parameters["port"].Value = port;
            report.Parameters["commodity"].Value = commodity;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult IndexDetailReport()
        {
            ViewData["Agents"] = _shippingAgentRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["Good"] = _goodsHeadRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });

            return View();
        }

        public IActionResult ViewIndexDetailReport(long? shippingAgent , string Type,   string cargotype, long? consignee, long? good, DateTime? fromdate, DateTime? todate, DateTime? fromdatedelivered, DateTime? todatedelivered)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.IndexDetailReport.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["Linename"].Value = shippingAgent;
            report.Parameters["Type"].Value = Type;
            report.Parameters["Cargo"].Value = cargotype;
            report.Parameters["Consingee"].Value = consignee;          
            report.Parameters["goods"].Value = good;
            report.Parameters["FromArrivalDate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["ToArrivalDate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["FromDeliveryDate"].Value = fromdatedelivered?.ToString("MM/dd/yyyy");
            report.Parameters["toeliveryDate"].Value = todatedelivered?.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult LetPassBalance()
        { 
            return View();
        }

        public IActionResult ViewLetPassBalance()
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.LetPassBalance.repx");
            XtraReport report = XtraReport.FromStream(resource);
             

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public ActionResult Print()
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.LetPassBalance.repx");
            XtraReport report = XtraReport.FromStream(resource);


            report.CreateDocument();

            using (MemoryStream ms = new MemoryStream())
            {
                report.ExportToPdf(ms, new PdfExportOptions() { ShowPrintDialogOnOpen = true });
                return File(ms.ToArray(), String.Format("application/{0}", "pdf"));
                //return ExportDocument(ms.ToArray(), "pdf", "Report.pdf", true);
            }
        }

        private FileResult ExportDocument(byte[] document, string format, string fileName, bool isInline)
        {
            string contentType;
            string disposition = (isInline) ? "inline" : "attachment";

            switch (format.ToLower())
            {
                case "docx":
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case "xls":
                    contentType = "application/vnd.ms-excel";
                    break;
                case "xlsx":
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case "mht":
                    contentType = "message/rfc822";
                    break;
                case "html":
                    contentType = "text/html";
                    break;
                case "txt":
                case "csv":
                    contentType = "text/plain";
                    break;
                case "png":
                    contentType = "image/png";
                    break;
                default:
                    contentType = String.Format("application/{0}", format);
                    break;
            }
             
            return File(document, contentType);
        }
        public IActionResult PODetailBank()
        {

            ViewData["Banks"] = _bankRepository.GetAll().Where(x=> x.Type == "PayOrder")
                .Select(v => new SelectListItem { Text = $"{v.BankName} -- {v.BankCode} -- {v.AccountNo} ", Value = v.BankId.ToString() });


            ViewData["shippingagent"] = _shippingAgentRepo.GetAll().Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });


            return View();
        }

        public IActionResult ViewPODetailBank(DateTime fromdate , DateTime todate, string fromrange , string torange, DateTime date , string amountRange, string branch , string bankid , string title , long? shippingagentId)
        {  
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.PODetailBankReport.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["fromrange"].Value = fromrange;
            report.Parameters["torange"].Value = torange;
            report.Parameters["amountRange"].Value = amountRange;

            report.Parameters["date"].Value = date.ToString("MM/dd/yyyy");
            report.Parameters["branch"].Value = branch;
            report.Parameters["bankid"].Value = bankid;
            report.Parameters["title"].Value = title;
            report.Parameters["shipingagent"].Value = shippingagentId;
              
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult WorkOrderLogistics()
        {

            ViewData["Port"] = _portAndTerminalRepository.GetAll()
           .Select(v => new SelectListItem { Text = v.PortName, Value = v.PortName });

            return View();

            
        }

        public IActionResult ViewWorkOrderLogistics(long? workordno, string port, string fromdate, string todate)
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.WorkOrderLogistics.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["workordno"].Value = workordno;
            report.Parameters["port"].Value = port;
            report.Parameters["fromdate"].Value = fromdate;
            report.Parameters["todate"].Value = todate;

            report.CreateDocument();
            return PartialView("_Report", report);

        }

        public IActionResult IndexSummaryReport()
        {
            ViewData["agent"] = _shippingAgentRepo.GetAll()
           .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });
            return View();
        }

        public IActionResult ViewIndexSummaryReport(long? shippingagentid, string fromdate, string todate)
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.IndexSummaryReport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["shippingagentid"].Value = shippingagentid;
            report.Parameters["fromdate"].Value = fromdate;
            report.Parameters["todate"].Value = todate;
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public   IActionResult ChequePrinting()
        {
            return View();
        }

        public IActionResult ViewChequePrinting(string PartyName,string Amountinword,long amount, DateTime Date)
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ChequePrinting.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            report.Parameters["PartyName"].Value = PartyName;
            report.Parameters["Amountinword"].Value = Amountinword;
            report.Parameters["amount"].Value = amount;
            report.Parameters["date"].Value = Date.ToString("MM/dd/yyyy");

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ChequePrintingLog()
        {
            return View();
        }

        public IActionResult ViewChequePrintingLog(DateTime? fromdate , DateTime? todate, string search, long companyid)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ChequePrintingLog.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["amount"].Value = search;
            report.Parameters["companyid"].Value = companyid;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult Balancedetails()
        {
            return View();
        }

        public IActionResult ViewBalancedetails(DateTime? date, string customerid, string companyid)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.Balancedetails.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            report.Parameters["date"].Value = date?.ToString("MM/dd/yyyy");
            report.Parameters["customerid"].Value = customerid;
            report.Parameters["companyid"].Value = companyid;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult Agingwisebalancereport()
        {
            return View();
        }

        public IActionResult ViewAgingwisebalancereport(DateTime? date, string customerid, string companyid)
        {
            GetUserId();
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.Agingwisebalancereport.repx");
            XtraReport report = XtraReport.FromStream(resource);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            report.Parameters["date"].Value = date?.ToString("MM/dd/yyyy");
            report.Parameters["customerid"].Value = customerid;
            report.Parameters["companyid"].Value = companyid;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult ContianerTrackingReportForFinanceView()
        {
            ViewData["Agents"] = _masterShippingAgentRepository.GetAll()
            .Select(v => new SelectListItem { Text = v.MasterShippingAgentName, Value = v.MasterShippingAgentId.ToString() });


            return View();
        }

        public IActionResult ViewContianerTrackingReportForFinance(long? shippingagnet, DateTime? fromDate, DateTime? todate, string type , string Containerno)
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.ContianerTrackingReportForFinance.repx");
            XtraReport report = XtraReport.FromStream(resource);
             
            if(Containerno != null )
            {
                type = null;
                fromDate = null;
                todate = null;
                shippingagnet = null;
            }
             
            report.Parameters["type"].Value = type;
            report.Parameters["shippingagnet"].Value = shippingagnet;
            report.Parameters["fromDate"].Value = fromDate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["containerno"].Value = Containerno;


            return PartialView("_Report", report);
        }
        
        public IActionResult AICTInvoiceToLineView()
        {  
            return View();
        }

        public IActionResult ViewAICTInvoiceToLine(string invoiceno)
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.AICTInvoiceToLine.repx");
            XtraReport report = XtraReport.FromStream(resource);
            
            report.Parameters["invoiceno"].Value = invoiceno;
             
            return PartialView("_Report", report);
        }


        public IActionResult AssignStoragetariff()
        {  
            return View();
        }

        public IActionResult ViewAssignStoragetariff(string invoiceno)
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.AssignStoragetariff.repx");
            XtraReport report = XtraReport.FromStream(resource);

            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult LetPassDashboard()
        {
            return View();
        }

        public IActionResult ViewLetPassDashboard()
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.LetPassDashboard.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult BoxesLyingAtPort()
        {
            return View();
        }

        public IActionResult ViewBoxesLyingAtPort()
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.BoxesLyingAtPort.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult BoxesLyingAtPortContainerWise()
        {
            return View();
        }

        public IActionResult ViewBoxesLyingAtPortContainerWise()
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.BoxesLyingAtPortContainerWise.repx");
            XtraReport report = XtraReport.FromStream(resource);

            //report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult PGateInOutReport()
        {
            return View();
        }

        public IActionResult ViewPGateInOutReport(DateTime? fromdate, DateTime? todate, string cnic, string name, string phone)
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.PGateInOutReport.repx");
            XtraReport report = XtraReport.FromStream(resource);

            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            report.Parameters["path"].Value = path;
            

            report.Parameters["name"].Value = name;
            report.Parameters["cnic"].Value = cnic;
            report.Parameters["phone"].Value = phone;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");

            //report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult DailyShedDestuffing()
        {
            ViewData["Agents"] = _shippingRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["GoodsHeadItems"] = _goodsHeadRepository.GetAll()
            .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });

            return View();
        }

        public IActionResult ViewDailyShedDestuffing(DateTime fromdate, DateTime todate, long ShippingAgent, string commodity)
        {

            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.DailyShedDestuffing.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["fromdate"].Value = fromdate.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate.ToString("MM/dd/yyyy");
            report.Parameters["ShippingAgent"].Value = ShippingAgent;
            report.Parameters["GoodsHead"].Value = commodity;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult WeightSlipView()
        {
            return View();
        }

        public IActionResult ViewWeightSlip(long IndexWeighmentId , string type)
        { 
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.WeightSlip.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["IndexWeighmentId"].Value = IndexWeighmentId;
            report.Parameters["type"].Value = type;
            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult CrossStuffingReportView()
        {

            return View();
        }

        public IActionResult CrossStuffingReport(string BLNumber)
        {
            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.CrossStuffing.repx");
            XtraReport report = XtraReport.FromStream(resource);
            report.Parameters["BLNumber"].Value = BLNumber;
            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult OperationalMarketingReport()
        {
            ViewData["CAgent"] = _clearingAgentRepository.GetAll()
                .Select(c => new SelectListItem { Text = c.Name, Value = c.ClearingAgentId.ToString() });

            return View();
        }

        public IActionResult ViewOperationalMarketingReport(long? clearingagent, string type, string status, long? consignee, DateTime? fromdate, DateTime? todate)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.OperationalMarketingReport.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["ClearingAgent"].Value = clearingagent;
            report.Parameters["Type"].Value = type;
            report.Parameters["Status"].Value = status;
            report.Parameters["Consignee"].Value = consignee;
            report.Parameters["fromdate"].Value = fromdate?.ToString("MM/dd/yyyy");
            report.Parameters["todate"].Value = todate?.ToString("MM/dd/yyyy");
            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }

        public IActionResult AssignedTariffsDetailReport()
        {
            ViewData["TariffCode"] = _tariffInformationRepo.GetAll().Select(v => new SelectListItem { Text = v.TariffInformationId.ToString(), Value = v.TariffInformationId.ToString() });
            ViewData["FF"] = _shippingAgentRepo.GetAll().Select(c => new SelectListItem { Text = c.Name, Value = c.ShippingAgentId.ToString() });
            ViewData["ClearingAgent"] = _clearingAgentRepository.GetAll().Select(c => new SelectListItem { Text = c.Name, Value = c.ClearingAgentId.ToString() });
            ViewData["Commodity"] = _goodsHeadRepository.GetAll().Select(c => new SelectListItem { Text = c.GoodsHeadName, Value = c.GoodsHeadId.ToString() });
            ViewData["Shipper"] = _shipperRepo.GetAll().Select(c => new SelectListItem { Text = c.ShipperName, Value = c.ShipperId.ToString() });
            ViewData["ContainerType"] = _ISOCodeHeadRepository.GetAll().Select(c => new SelectListItem { Text = c.ISOCodeHeadDescription, Value = c.ISOCodeHeadId.ToString() });
            ViewData["Port"] = _portAndTerminalRepository.GetAll().Select(c => new SelectListItem { Text = c.PortName, Value = c.PortAndTerminalId.ToString() });

            return View();
        }

        public IActionResult ViewAssignedTariffsDetailReport(long? tariffcode, long? ff, long? clearingagent, long? commodity, long? shipper, long? containertype, string cargotype, long? port, long? consignee)
        {
            GetUserId();
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var assembly = typeof(ReportStorageWebExtension1).Assembly;
            Stream resource = assembly.GetManifestResourceStream("WebdocTerminal.Reports.AssignedTariffsDetailReport.repx");
            XtraReport report = XtraReport.FromStream(resource);

            report.Parameters["TariffCode"].Value = tariffcode;
            report.Parameters["FF"].Value = ff;
            report.Parameters["ClearingAgent"].Value = clearingagent;
            report.Parameters["Commodity"].Value = commodity;
            report.Parameters["Shipper"].Value = shipper;
            report.Parameters["ContainerType"].Value = containertype;
            report.Parameters["CargoType"].Value = cargotype;
            report.Parameters["Port"].Value = port;
            report.Parameters["Consignee"].Value = consignee;

            report.Parameters["path"].Value = path;
            report.Parameters["CompanyId"].Value = CompanyID;

            report.CreateDocument();
            return PartialView("_Report", report);
        }


        public IActionResult CRSummaryv2()
        {


            ViewData["Agents"] = _shippingAgentRepo.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["CAgents"] = _clearingAgentRepository.GetAll()
            .Select(c => new SelectListItem { Text = c.Name, Value = c.ClearingAgentId.ToString() });


            ViewData["GoodsHeads"] = _goodsHeadRepository.GetAll()
            .Select(c => new SelectListItem { Text = c.GoodsHeadName, Value = c.GoodsHeadId.ToString() });


            return View();
        }


        public JsonResult ViewCRSummaryv2(long? shippingAgent, long? clearingagent, long? consignee , long? goodsHeads, string type, string cargotype, DateTime? fromdate, DateTime? todate)
        {


            var res = _invoiceRepo.GetQueryResult( shippingAgent, clearingagent, consignee, goodsHeads, type, cargotype, fromdate, todate);

            return Json(res);
        }
         
    }

}