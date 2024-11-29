using System;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebdocTerminal.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.SqlServer;
using DevExpress.AspNetCore;
using System.IO;
using DevExpress.AspNetCore.Reporting;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Repos;
using WebdocTerminal.Helpers;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using WebdocTerminal.Services;
using Microsoft.AspNetCore.Http.Features;
using Korzh.EasyQuery.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

//signalR
//using WebdocTerminal.Hubs;

namespace WebdocTerminal
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public WebocProcessor WebocProcessor { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //signalR
            //services.AddSignalR();

            services.AddEasyQuery().UseSqlManager();

            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
            });

            services.AddMvc();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")                   
                    ));

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Identity/Account/LogIn");
           

            services.AddDevExpressControls();
 
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });


            services.AddScoped<IClearingAgentRepository, ClearingAgentRepository>();
            services.AddScoped<IShippingAgentRepository, ShippingAgentRepository>();
            services.AddScoped<IDeliveryOrderRepository, DeliveryOrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

            services.AddScoped<ISealIssueRepository, SealIssueRepository>();
            services.AddScoped<ISealPurchaseRepository, SealPurchaseRepository>();
            
            
            
            services.AddScoped<IECMORepository, ECMORepository>();
            services.AddScoped<IGDIRepository, GDIRepository>();
            services.AddScoped<IIHCRepository, IHCRepository>();
            services.AddScoped<IIHCORepository, IHCORepository>();
            services.AddScoped<ISRCORepository, SRCORepository>();
            services.AddScoped<IVIRORepository, VIRORepository>();
            services.AddScoped<IIGMORepository, IGMORepository>();
            services.AddScoped<IIPAORepository, IPAORepository>();
            services.AddScoped<IGIIORepository, GIIORepository>();
            services.AddScoped<IGDIORepository, GDIORepository>();
            services.AddScoped<ICCMORepository, CCMORepository>();
            services.AddScoped<ISCMORepository, SCMORepository>();
            services.AddScoped<ITTSORepository, TTSORepository>();
            services.AddScoped<ICRLORepository, CRLORepository>();
            services.AddScoped<IPGOORepository, PGOORepository>();
            services.AddScoped<IGTOORepository, GTOORepository>();
            services.AddScoped<IACKRepository, ACKRepository>();
            services.AddScoped<ISVMORepository, SVMORepository>();
            services.AddScoped<IVesselRepository, VesselRepository>();
            services.AddScoped<IVoyageRepository, VoyageRepository>();
            services.AddScoped<IContainerRepository, ContainerRepository>();
            services.AddScoped<IContainerIndexRepository, ContainerIndexRepository>();
            services.AddScoped<IShippingAgentRepository, ShippingAgentRepository>();
            services.AddScoped<IGateOutRepository, GateOutRepository>();
            services.AddScoped<IDeliveryOrderRepository, DeliveryOrderRepository>();
            services.AddScoped<ISIMRepository, SIMRepository>();
            services.AddScoped<ISIMORepository, SIMORepository>();
            services.AddScoped<IECMRepository, ECMRepository>();
            services.AddScoped<IGTTORepository, GTTORepository>();
            services.AddScoped<ISRCRepository, SRCRepository>();
            services.AddScoped<ICRLRepository, CRLRepository>();
            services.AddScoped<IGTORepository, GTORepository>();
            services.AddScoped<IGroundedContainerRepository, GroundedContainerRepository>();
            services.AddScoped<IGateInRepository, GateInRepository>();
            services.AddScoped<IDestuffedContainerRepository, DestuffedContainerRepository>();
            services.AddScoped<ITellySheetRepository, TellySheetRepository>();
            services.AddScoped<IAuctionRepository, AuctionRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IWeighmentRepository, WeighmentRepository>();
            services.AddScoped<IHoldRepository, HoldRepository>();
            services.AddScoped<ITariffRepository, TariffRepository>();
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IPartyRepository, PartyRepository>();
            services.AddScoped<IChequeRecivedRepository, ChequeRecivedRepository>();
            services.AddScoped<IShippingAgentTariffRepository, ShippingAgentTariffRepository>();
            services.AddScoped<IStorageSlabRepository, StorageSlabRepository>();
            services.AddScoped<IContainerIndexTariffRepository, ContainerIndexTariffRepository>();
            services.AddScoped<IRefundAmountRepository, RefundAmountRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IAmountReceivedRepository, AmountReceivedRepository>();
            services.AddScoped<IInvoiceDocumentRepository, InvoiceDocumentRepository>();
            services.AddScoped<ISalesTaxRepository, SalesTaxRepository>();
            services.AddScoped<IBRTRepository, BRTRepository>();
            services.AddScoped<ITarifHeadRepository, TarifHeadRepository>();
            services.AddScoped<IPartyLedgerRepository, PartyLedgerRepository>();
            services.AddScoped<ICYContainerRepository, CYContainerRepository>();
            services.AddScoped<WebocProcessor, WebocProcessor>();
            services.AddScoped<IUserResolveService, UserResolveService>();
            services.AddScoped<IOPIARepository, OPIARepository>();
            services.AddScoped<IOGDERepository, OGDERepository>();
            services.AddScoped<IOGIERepository, OGIERepository>();
            services.AddScoped<IOEHCRepository, OEHCRepository>();
            services.AddScoped<IEHCRepository, EHCRepository>();
            services.AddScoped<IOSRCRepository, OSRCRepository>();
            services.AddScoped<IOECMRepository, OECMRepository>();
            services.AddScoped<IOCRLRepository, OCRLRepository>();
            services.AddScoped<IOGDCRepository, OGDCRepository>();
            services.AddScoped<IOSVMRepository, OSVMRepository>();
            services.AddScoped<IOGTERepository, OGTERepository>();
            services.AddScoped<IOVAMRepository, OVAMRepository>();
            services.AddScoped<IPIARepository, PIARepository>();
            services.AddScoped<IGTIRepository, GTIRepository>();
            services.AddScoped<IGDERepository, GDERepository>();
            services.AddScoped<ISVMRepository, SVMRepository>();
            services.AddScoped<IGTERepository, GTERepository>();
            services.AddScoped<IVAMRepository, VAMRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<ITPCargoDetailsRepository, TPCargoDetailsRepository>();
            services.AddScoped<IExportContainerRepository, ExportContainerRepository>();
            services.AddScoped<IGateInExportRepository, GateInExportRepository>();
            services.AddScoped<IServiceCompletionRepository, ServiceCompletionRepository>();
            services.AddScoped<IContainerAssociationRepository, ContainerAssociationRepository>();
            services.AddScoped<IGateoutExportRepository, GateoutExportRepository>();
            services.AddScoped<IShippingLineRepository, ShippingLineRepository>();
            services.AddScoped<IEmptyGateOutContainerRepository, EmptyGateOutContainerRepository>();
            services.AddScoped<IEmptyContainerGatePassRepository, EmptyContainerGatePassRepository>();
            services.AddScoped<ICommodityRepository, CommodityRepository>();
            services.AddScoped<IPortAndTerminalRepository, PortAndTerminalRepository>();
            services.AddScoped<IClearingAgentExportRepository, ClearingAgentExportRepository>();
            services.AddScoped<IShipperRepository, ShipperRepository>();
            services.AddScoped<IShippingAgentExportRepository, ShippingAgentExportRepository>();
            services.AddScoped<IVesselExportRepository, VesselExportRepository>();
            services.AddScoped<IVoyageExportRepository, VoyageExportRepository>();
            services.AddScoped<ILoadingProgramRepository, LoadingProgramRepository>();
            services.AddScoped<ILoadingProgramDetailRepository, LoadingProgramDetailRepository>();
            services.AddScoped<IEnteringCargoRepository, EnteringCargoRepository>();
            services.AddScoped<IExportCargoRepository, ExportCargoRepository>();
            services.AddScoped<ICargoReceivedRepository, CargoReceivedRepository>();
            services.AddScoped<ICargoRepository, CargoRepository>();
            services.AddScoped<IExportBrtRepository, ExportBrtRepository>();
            services.AddScoped<IPOLocationRepository, POLocationRepository>();
            services.AddScoped<IExportGroundedCargoRepository, ExportGroundedCargoRepository>();
            services.AddScoped<IExportContainerItemRepositpory, ExportContainerItemRepositpory>();
            services.AddScoped<IDictionaryRepository, DictionaryRepository>();
            services.AddScoped<IExportContainerItemRepositpory, ExportContainerItemRepositpory>();
            services.AddScoped<ICustomerNOCRepository, CustomerNOCRepository>();
            services.AddScoped<IEmptyReceivingRepository, EmptyReceivingRepository>();
            services.AddScoped<IGDHoldRepository, GDHoldRepository>();
            services.AddScoped<ICargoRollOverRepository, CargoRollOverRepository>();

            services.AddScoped<IInvoiceExportRepository, InvoiceExportRepository>();
            services.AddScoped<IPartyExportRepository, PartyExportRepository>();
            services.AddScoped<IAmountReceivedExportRepository, AmountReceivedExportRepository>();
            services.AddScoped<IChequeRecivedExportRepository, ChequeRecivedExportRepository>();
            services.AddScoped<IInvoiceDocumentExportRepository, InvoiceDocumentExportRepository>();
            services.AddScoped<IPartyLedgerExportRepository, PartyLedgerExportRepository>();
            services.AddScoped<ITariffExportRepository, TariffExportRepository>();
            services.AddScoped<ITariffHeadExportRepository, TariffHeadExportRepository>();
            services.AddScoped<IGDTariffRepository, GDTariffRepository>();
            services.AddScoped<IInvoiceItemExportRepository, InvoiceItemExportRepository>();
            services.AddScoped<IStorageSlabExportRepository, StorageSlabExportRepository>();
            services.AddScoped<IShippingAgentTariffExportRepository, ShippingAgentTariffExportRepository>();
            services.AddScoped<IExportDeliveryOrderRepository, ExportDeliveryOrderRepository>();
            services.AddScoped<IGatePassExportRepository, GatePassExportRepository>();
            services.AddScoped<ICreditToCustomerRepository, CreditToCustomerRepository>();
            services.AddScoped<IExportLocationCodeListRepository, ExportLocationCodeListRepository>();
            services.AddScoped<IPackageTypeExportRepository, PackageTypeExportRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IGateOutGDRepository, GateOutGDRepository>();
            services.AddScoped<IExportVehicleRepository, ExportVehicleRepository>();
            services.AddScoped<IDOItemRepository, DOItemRepository>();
            services.AddScoped<ITPReceiveVehicleRepository, TPReceiveVehicleRepository>();
            services.AddScoped<IGateOutGDRepository, GateOutGDRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IOSIMRepository, OSIMRepository>();
            services.AddScoped<IGatePassAuctionRepository, GatePassAuctionRepository>();
            services.AddScoped<IDocumentCodeRepository, DocumentCodeRepository>();
            services.AddScoped<IUsersEmailRepository, UsersEmailRepository>();
            services.AddScoped<IFailedFileRepository, FailedFileRepository>();
            services.AddScoped<IDisabledAgentTariffRepository, DisabledAgentTariffRepository>();
            services.AddScoped<IDisabledAgentTariffCYRepository, DisabledAgentTariffCYRepository>();

            services.AddScoped<IBrtItemRepository, BrtItemRepository>();
            services.AddScoped<IAppPageItemRepository, AppPageItemRepository>();
            services.AddScoped<IRefundAmountExportRepository, RefundAmountExportRepository>();
            services.AddScoped<ISalesTaxIndexWiseRepository, SalesTaxIndexWiseRepository>();
            services.AddScoped<IEdiMessageRepository, EdiMessageRepository>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IReturnToYardRepository, ReturnToYardRepository>();
            services.AddScoped<IPreAlertRepository, PreAlertRepository>();
            services.AddScoped<IPreAlterDetailRepository, PreAlterDetailRepository>();
            services.AddScoped<IPaymentUpdateRepository, PaymentUpdateRepository>();
            services.AddScoped<IPaymentUpdateDetailRepository, PaymentUpdateDetailRepository>();
            services.AddScoped<IServicesInfoRepository, ServicesInfoRepository>();
            services.AddScoped<IServicesTypeRepository, ServicesTypeRepository>();
            services.AddScoped<IServicesSectionRepository, ServicesSectionRepository>();
            services.AddScoped<INatureOfPaymentRepository , NatureOfPaymentRepository>();
            services.AddScoped<IOperationDprtRepository, OperationDprtRepository>();
            services.AddScoped<IBankBranchRepository, BankBranchRepository>();
            services.AddScoped<IPreAlertPayOrderRepository, PreAlertPayOrderRepository>();
            services.AddScoped<IPreAlertPayOrderDetailRepository, PreAlertPayOrderDetailRepository>();
            services.AddScoped<IISOCodeRepository, ISOCodeRepository>();
            services.AddScoped<IConsigneRepository, ConsigneRepository>();
            services.AddScoped<IGoodRepository, GoodRepository>();
            services.AddScoped< IImportDropOfTicketRepository , ImportDropOfTicketRepository>();
            services.AddScoped<IParkingTicketRepository, ParkingTicketRepository>();
            services.AddScoped<IBidderRepository, BidderRepository>();
            services.AddScoped<ILabourWorkOrderRepository, LabourWorkOrderRepository>();
            services.AddScoped<IWorkOrderCSDRepository, WorkOrderCSDRepository>();
            services.AddScoped<ITransporterRepository, TransporterRepository>();
            services.AddScoped<ITariffInformationRepository, TariffInformationRepository>();
            services.AddScoped<ITariffPercentageRepository, TariffPercentageRepository>();
            services.AddScoped<IDeliveredYardRepositoryRepository, DeliveredYardRepositoryRepository>();
            services.AddScoped<IBillToLineRepository, BillToLineRepository>();
            services.AddScoped<IWaiverRepository, WaiverRepository>();
            services.AddScoped<IExchangeRateRepository , ExchangeRateRepository>();
            services.AddScoped<IExaminationRequestRepository, ExaminationRequestRepository>();
            services.AddScoped<IOtherChargeRepository, OtherChargeRepository>();
            services.AddScoped<IOtherChargeAssigningRepository, OtherChargeAssigningRepository>();
            services.AddScoped<IGoodsHeadRepository, GoodsHeadRepository>();
            services.AddScoped<IStorageFreeDayRepository, StorageFreeDayRepository>();
            services.AddScoped<IContainerSizeAmountRepository, ContainerSizeAmountRepository>();
            services.AddScoped<IPreAlertVesselRepository , PreAlertVesselRepository>();
            services.AddScoped<ITariffInofrmationTariffRepository, TariffInofrmationTariffRepository>();
            services.AddScoped<IRandDClerkRepository, RandDClerkRepository>();
            services.AddScoped<IGatePassWeightmentRepository, GatePassWeightmentRepository>();
            services.AddScoped<IManualGatePassRepository, ManualGatePassRepository>();
            services.AddScoped<ITruckInOutRepository, TruckInOutRepository>();
            services.AddScoped<IGatePassSampleRepository, GatePassSampleRepository>();
            services.AddScoped<IAICTAndLineIndexTariffRepository, AICTAndLineIndexTariffRepository>();
            services.AddScoped<IPortChargeRepository, PortChargeRepository>();
            services.AddScoped<ILineSealAmountRepository, LineSealAmountRepository>();
            services.AddScoped<IGenerealSealAmountRepository, GenerealSealAmountRepository>();
            services.AddScoped<ITerminalAndFFShareRateRepository, TerminalAndFFShareRateRepository>();
            services.AddScoped<IExaminationChargeRepository, ExaminationChargeRepository>();
            services.AddScoped<IExaminationChargeAssignRepository, ExaminationChargeAssignRepository>();
            services.AddScoped<IAuctionAmountRepository, AuctionAmountRepository>();
            services.AddScoped<IInvoiceInquiryRepository, InvoiceInquiryRepository>();
            services.AddScoped<IISOCodeHeadRepository, ISOCodeHeadRepository>();
            services.AddScoped<IContainerWeightRepository, ContainerWeightRepository>();
            services.AddScoped<ICFSWeightRepository, CFSWeightRepository>();
            services.AddScoped<IGroundingFreeDayRepository, GroundingFreeDayRepository>();
            services.AddScoped<IExaminationTariffRepository, ExaminationTariffRepository>();
            services.AddScoped<IExaminationTariffInformationRepository, ExaminationTariffInformationRepository>();
            services.AddScoped<IExaminationTariffInformationTariffRepository, ExaminationTariffInformationTariffRepository>();
            services.AddScoped<IBRTLocationRepository, BRTLocationRepository>();
            services.AddScoped<IGroundedDaysRepository, GroundedDaysRepository>();
            services.AddScoped<IEmptyContainerReceivedRepository, EmptyContainerReceivedRepository>();
            services.AddScoped<ITransportCollectionRepository, TransportCollectionRepository>();
            services.AddScoped<ITransportCollectionTariffRepository, TransportCollectionTariffRepository>();
            services.AddScoped<IOffHireLocationRepository, OffHireLocationRepository>();
            services.AddScoped<IEmptyContainerDeliveredRepository, EmptyContainerDeliveredRepository>();
            services.AddScoped<IEmptyContainerTariffRepository, EmptyContainerTariffRepository>();
            services.AddScoped<IEmptyContainerStorageSlabRepository, EmptyContainerStorageSlabRepository>();
            services.AddScoped<IEmptyContainerInvoiceItemRepository, EmptyContainerInvoiceItemRepository>();
            services.AddScoped<IEmptyContainerInvoiceRepository, EmptyContainerInvoiceRepository>();
            services.AddScoped<IEmptyContainerDeliveryOrderRepository, EmptyContainerDeliveryOrderRepository>();
            services.AddScoped<IEmptyContainerTaxAndFreeDayRepository, EmptyContainerTaxAndFreeDayRepository>();
            services.AddScoped<ICreditAllowedRepository, CreditAllowedRepository>();
            services.AddScoped<IPaymentReceivedRepository, PaymentReceivedRepository>();
            services.AddScoped<IExcessAmountRefundSettlementRepository, ExcessAmountRefundSettlementRepository>();
            services.AddScoped<IElectronicDeliveryOrderRepository, ElectronicDeliveryOrderRepository>();
            services.AddScoped<IGDCRRepository, GDCRRepository>();
            services.AddScoped<IScheduleOfChargeRepository, ScheduleOfChargeRepository>();
            services.AddScoped<IScheduleOfChargeItemRepository, ScheduleOfChargeItemRepository>();
            services.AddScoped<ICreditAllowRefoundSettlementRepository, CreditAllowRefoundSettlementRepository>();
            services.AddScoped<IEDI_Maersk_LookupRepository, EDI_Maersk_LookupRepository>();
            services.AddScoped<IEDI_Maersk_APIResponseRepository, EDI_Maersk_APIResponseRepository>();
            services.AddScoped<IEDI_Maersk_MessageRepository, EDI_Maersk_MessageRepository>();
            services.AddScoped<IEDI_Tradelens_APIResponseRepository, EDI_Tradelens_APIResponseRepository>();
            services.AddScoped<IEDI_Tradelens_LookupRepository, EDI_Tradelens_LookupRepository>();
            services.AddScoped<IEDI_Tradelens_MessageRepository, EDI_Tradelens_MessageRepository>();
            services.AddScoped<IMasterShippingAgentRepository, MasterShippingAgentRepository>();
            services.AddScoped<IStorageFreeDaysForHolidayRepository, StorageFreeDaysForHolidayRepository>();
            services.AddScoped<IEDIMaerskMessageTOSRepository, EDIMaerskMessageTOSRepository>();
            services.AddScoped<IEDOHoldRepository, EDOHoldRepository>();
            services.AddScoped<IAutoExaminationChargeRepository, AutoExaminationChargeRepository>();
            services.AddScoped<IPGateInOutRepository, PGateInOutRepository>();
            services.AddScoped<INatureOfTariffRepository, NatureOfTariffRepository>();
            services.AddScoped<IDOItemExportRepository, DOItemExportRepository>();
            services.AddScoped<ITariffRateSlabWiseRepository, TariffRateSlabWiseRepository>();
            services.AddScoped<IDisabledAgentTariffExportRepository, DisabledAgentTariffExportRepository>();
            services.AddScoped<IShipperExportRepository, ShipperExportRepository>();
            services.AddScoped<IShippingLineExportRepository, ShippingLineExportRepository>();
            services.AddScoped<IPortAndTerminalExportRepository, PortAndTerminalExportRepository>();
            services.AddScoped<IBoundedTranspoterRepository, BoundedTranspoterRepository>();
            services.AddScoped<IPGORepository, PGORepository>();
            services.AddScoped<IPortOfLoadingRepository, PortOfLoadingRepository>();
            services.AddScoped<IImportContainerLocationRepository, ImportContainerLocationRepository>();
            services.AddScoped<IShortExcessWeigthRepository , ShortExcessWeigthRepository>();
            services.AddScoped<IVehicleMeasurementRepository , VehicleMeasurementRepository>();
            services.AddScoped<IHoldPrivilegeRepository, HoldPrivilegeRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IAccountNatureRepository, AccountNatureRepository>();
            services.AddScoped<IAccountCategoryRepository, AccountCategoryRepository>();
            services.AddScoped<IAccountSubCategoryRepository, AccountSubCategoryRepository>();
            services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();
            services.AddScoped<IVoucherServiceTypeRepository, VoucherServiceTypeRepository>();
            services.AddScoped<IVoucherServiceTypeDetailRepository, VoucherServiceTypeDetailRepository>();
            services.AddScoped<IChartOfAccountRepository, ChartOfAccountRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAccountBalanceRepository, AccountBalanceRepository>();
            services.AddScoped<IVoucherTypeRepository, VoucherTypeRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<IVoucherDetailRepository, VoucherDetailRepository>();
            services.AddScoped<IChequePrintingRepository, ChequePrintingRepository>();
            services.AddScoped<IGoodsHeadExportRepository, GoodsHeadExportRepository>();
            services.AddScoped<IExportConsigneeRepository, ExportConsigneeRepository>();
            services.AddScoped<IExportDropOfTicketRepository, ExportDropOfTicketRepository>();
            services.AddScoped<IDeliveredYardExportRepository, DeliveredYardExportRepository>();
            services.AddScoped<ITariffInformationExportRepository, TariffInformationExportRepository>();
            services.AddScoped<ITariffInofrmationTariffExportRepository, TariffInofrmationTariffExportRepository>();
            services.AddScoped<IExportTariffRepository, ExportTariffRepository>();
            services.AddScoped<ITariffPercentageExportRepository, TariffPercentageExportRepository>();
            services.AddScoped<ITerminalAndFFShareRateExportRepository, TerminalAndFFShareRateExportRepository>();
            services.AddScoped<IPortChargeExportRepository, PortChargeExportRepository>();
            services.AddScoped<IOtherChargeExportRepository, OtherChargeExportRepository>();
            services.AddScoped<IOtherChargeAssigningExportRepository, OtherChargeAssigningExportRepository>();
            services.AddScoped<ILineInvoiceDetailRepository, LineInvoiceDetailRepository>();
            services.AddScoped<IPercentForAictBillingToLineRepository, PercentForAictBillingToLineRepository>();
            services.AddScoped<IAictBillingRepository, AictBillingRepository>();
            services.AddScoped<IAictBillingItemRepository, AictBillingItemRepository>();
            services.AddScoped<IUnlockGenerateBillRemarkRepository, UnlockGenerateBillRemarkRepository>();
            services.AddScoped<ICSEmptyContainerReceiveRepository, CSEmptyContainerReceiveRepository>();
            services.AddScoped<IIndexWeighmentRepository , IndexWeighmentRepository>();
            services.AddScoped<IEmailAlertRepository, EmailAlertRepository>();
            services.AddScoped<ICRSummaryHeadDetailRepository, CRSummaryHeadDetailRepository>();
            services.AddScoped<IAllowBackDateTransactionRepository, AllowBackDateTransactionRepository>();
            services.AddScoped<IStorageShareRateRepository, StorageShareRateRepository>();
            services.AddScoped<ITariffPerBoxRateRepository, TariffPerBoxRateRepository>();
            services.AddScoped<IModifyPointRepository, ModifyPointRepository>();
            services.AddScoped<IFinancialYearRepository, FinancialYearRepository>();
            services.AddScoped<IFinancialYearClosureRepository, FinancialYearClosureRepository>();

   

            services.AddScoped<IdentityRole>();

            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));

            services.Configure<EmailConfig>(Configuration.GetSection("EmailConfig"));

            // create service provider
            var serviceProvider = services.BuildServiceProvider();
            WebocProcessor = ActivatorUtilities.CreateInstance<WebocProcessor>(serviceProvider);

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(
                 Configuration.GetConnectionString("DefaultConnection")                
                , new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.FromMinutes(15),
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();
            services.AddSession();

            services
                .AddMvc()
                .AddJsonOptions(o => o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            //{
            //    options.LoginPath = "/auth/login";
            //    options.AccessDeniedPath = "/auth/accessdenied";
            //    options.Cookie.IsEssential = true;
            //    options.SlidingExpiration = true; // here 1
            //    options.ExpireTimeSpan = TimeSpan.FromSeconds(10);// here 2
            //});


            services.ConfigureReportingServices(configurator =>
            {
                configurator.ConfigureReportDesigner(designerConfigurator =>
                {
                    designerConfigurator.RegisterDataSourceWizardConfigFileConnectionStringsProvider();
                });
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IOptions<AppConfig> config, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            

            app.UseEasyQuery(options =>
            {
                options.Endpoint = "/api/easyquery";
                options.UseDbContext<ApplicationDbContext>();
                options.UsePaging(25);
            });

            app.UseMvc();
           
            var reportDirectory = Path.Combine(env.ContentRootPath, "Reports");
            DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension.RegisterExtensionGlobal(new ReportStorageWebExtension1(reportDirectory));
            DevExpress.XtraReports.Configuration.Settings.Default.UserDesignerOptions.DataBindingMode = DevExpress.XtraReports.UI.DataBindingMode.Expressions;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //add due to  UseIdentity use in repo classes
 

            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new[] { new HangFireAuthorizationFilter() }
            });

            RecurringJob.RemoveIfExists("edi-processor");
            RecurringJob.RemoveIfExists("edi-outgoing-job");
            if (config.Value.EDIEnabled)
            {
                //RecurringJob.AddOrUpdate<WebocProcessor>("edi-processor", x => x.PollWeboc(env.ContentRootPath), Cron.MinuteInterval(5));
                //RecurringJob.AddOrUpdate<WebocProcessor>("edi-outgoing-job", x => x.GenerateEDIMessages(env.ContentRootPath), Cron.MinuteInterval(5));
            }

            RecurringJob.RemoveIfExists("edi-tradelens");
            RecurringJob.RemoveIfExists("edi-tradelens-Message");

            //RecurringJob.AddOrUpdate<EDITradelensService>("edi-tradelens", x => x.GetTradelensMessages(), Cron.MinuteInterval(5));
            //RecurringJob.AddOrUpdate<EDITradelensService>("edi-tradelens-Message", x => x.GenerateCODECOMessageFile(), Cron.MinuteInterval(10));


            RecurringJob.RemoveIfExists("edi-dynamic-edo");
            //RecurringJob.AddOrUpdate<EDIDynamicEDO>("edi-dynamic-edo", x => x.PollEDOFiles(env.ContentRootPath), Cron.MinuteInterval(10));

            RecurringJob.RemoveIfExists("UpdateIndexDetail");
            //RecurringJob.AddOrUpdate<WebocProcessor>("UpdateIndexDetail", x => x.UpdateIndexDetail(), Cron.MinuteInterval(5));


            RecurringJob.RemoveIfExists("edi-maersk-TOS");
            RecurringJob.RemoveIfExists("edi-maersk-Message-TOS");

            //RecurringJob.AddOrUpdate<EDIMaerskService>("edi-maersk-TOS", x => x.SaveCODECOMessages(), Cron.MinuteInterval(5));
            //RecurringJob.AddOrUpdate<EDIMaerskService>("edi-maersk-Message-TOS", x => x.GenerateCODECOMessageFile(env.ContentRootPath), Cron.MinuteInterval(10));


            RecurringJob.RemoveIfExists("Cancel-Credit-Allow");
            //RecurringJob.AddOrUpdate<UnSettledKnockOfInvoices>("Cancel-Credit-Allow", x => x.UpdateUnSettelCreditAllow(), Cron.Daily());

            RecurringJob.RemoveIfExists("Out-standing-SpecialHandling");
            //RecurringJob.AddOrUpdate<UnSettledKnockOfInvoices>("Out-standing-SpecialHandling", x => x.OutstandingSpecialHandling(), Cron.Daily());

            RecurringJob.RemoveIfExists("UpdatePortChargesForVechiles");
            //RecurringJob.AddOrUpdate<UnSettledKnockOfInvoices>("UpdatePortChargesForVechiles", x => x.UpdateProtcharges(), Cron.MinuteInterval(10));

            RecurringJob.RemoveIfExists("ReProcessGIIO");
            //RecurringJob.AddOrUpdate<WebocProcessor>("ReProcessGIIO", x => x.ReProcessGIIO(env.ContentRootPath), Cron.MinuteInterval(5));

            RecurringJob.RemoveIfExists("AlertsForOperation");
            //RecurringJob.AddOrUpdate<EmailAlerts>("AlertsForOperation", x => x.EmailsAltersForOperation(), Cron.HourInterval(1));

            RecurringJob.RemoveIfExists("EmailAlertAutoReport");
            //RecurringJob.AddOrUpdate<EmailAlerts>("EmailAlertAutoReport", x => x.EmailAlertAutoReport(), "0 9,21 * * *" ,  TimeZoneInfo.Local);


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseDevExpressControls();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();

            //signalR
            //app.UseSignalR(builder =>
            //{
            //    builder.MapHub<NotificationHub>("/notificationHub");
            //});



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                 name: "areas",
                 template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
               );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"); 
            });
             
            CreateRoles(serviceProvider);
         }


        private void CreateRoles(IServiceProvider serviceProvider)
        {
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                Task<IdentityResult> roleResult;
                string[] roles = { "ADMINISTRATOR" };
                string email = "ovais.khan@ciphertronix.com";
                string username = "admin";

                foreach (var item in roles)
                {
                    //Check that there is an Administrator role and create if not
                    Task<bool> hasAdminRole = roleManager.RoleExistsAsync(item);
                    hasAdminRole.Wait();

                    if (!hasAdminRole.Result)
                    {
                        roleResult = roleManager.CreateAsync(new IdentityRole(item));
                        roleResult.Wait();
                    }
                }


                //Check if the admin user exists and create it if not
                //Add to the Administrator role

                Task<IdentityUser> testUser = userManager.FindByEmailAsync(email);
                testUser.Wait();

                if (testUser.Result == null)
                {
                    IdentityUser admin = new IdentityUser();
                    admin.Email = email;
                    admin.UserName = username;
                    admin.LockoutEnabled = false;

                    Task<IdentityResult> newUser = userManager.CreateAsync(admin, "Pass@2019");
                    newUser.Wait();

                    if (newUser.Result.Succeeded)
                    {
                        Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(admin, "ADMINISTRATOR");
                        newUserRole.Wait();
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }


        }

    }
}
