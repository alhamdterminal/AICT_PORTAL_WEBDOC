using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Claims;
using WebdocTerminal.Areas.Export.Models;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using WebdocTerminal.Helpers;

namespace WebdocTerminal.Controllers
{

    public class APICallsController : Controller
    {
        private IPermissionRepository _permissionsRepo;
        private IPackageTypeExportRepository _packageExportRepo;
        private IAuctionRepository _auctionRepo;
        private IDeliveryOrderRepository _deliveryOrderRepo;
        private IOrderDetailRepository _orderDetailRepo;
        private IVesselRepository _vesselRepo;
        private IVoyageRepository _voyageRepo;
        private IContainerRepository _containerRepo;
        private IContainerIndexRepository _containerIndexRepo;
        private IClearingAgentRepository _clearingAgentRepo;
        private IIGMORepository _iGMORepository;
        private IShippingAgentRepository _shippingAgentRepo;
        private IDeliveredYardRepositoryRepository _deliveredYardRepo;
        private ITransporterRepository _transporterRepo;
        private IIPAORepository _ipaosRepo;
        private IGateInRepository _gateinRepo;
        private IGateOutRepository _gateOutRepo;
        private IWeighmentRepository _weighmentRepo;
        private ITariffRepository _tariffRepo;
        private IShippingAgentTariffRepository _agentTariffRepo;
        private IContainerIndexTariffRepository _indexTariffRepo;
        private IInvoiceRepository _invoiceRepo;
        private IStorageSlabRepository _storageSlabRepo;
        private IInvoiceDocumentRepository _invDocRepo;
        private IAmountReceivedRepository _amountReceivedRepo;
        private IBRTRepository _brtRepo;
        private ISalesTaxRepository _taxRepo;
        private ISRCORepository _srcoRepo;
        private IECMORepository _ecmoRepo;
        private ICCMORepository _ccmoRepo;
        private ISCMORepository _scmoRepo;
        private ITTSORepository _ttscRepo;
        private IPGOORepository _pgooRepo;
        private ITarifHeadRepository _tarifHeadRepo;
        private IGroundedContainerRepository _groundedContainerRepo;
        private IDestuffedContainerRepository _destuffedRepo;
        private IECMRepository _examinationMarkRepo;
        private IHoldRepository _holdRepo;
        private ICYContainerRepository _cyContaienrRepo;
        private IPartyLedgerRepository _partyLedgerRepo;
        private UserManager<IdentityUser> _userManager;
        private IUsersRepository _userRepo;
        private IAuditRepository _auditRepo;
        private IExportContainerRepository _exportContainerRepo;
        private IEmptyGateOutContainerRepository _emptyGateOutContainerRepo;
        private IShippingLineRepository _shippingLineRepo;
        private IOPIARepository _oPIARepo;
        private ILoadingProgramRepository _loadingProgramRepo;
        private ILoadingProgramDetailRepository _loadingProgramDetailRepo;
        private IEnteringCargoRepository _enteringCargoRepo;
        private IExportGroundedCargoRepository _exportGroundedCargoRepo;
        private ICargoRepository _cargoRepo;
        private IDictionaryRepository _dictionaryRepo;
        private IPortAndTerminalRepository _portAndTerminalRepo;
        private ICargoReceivedRepository _cargoReceivedRepo;
        private IVesselExportRepository _vesselExportRepo;
        private IVoyageExportRepository _voyageExportRepo;
        private ICargoRollOverRepository _cargoRollOverRepo;
        private IAmountReceivedExportRepository _amountReceivedExportRepo;
        private IPartyLedgerExportRepository _partyLedgerExportRepo;
        private IInvoiceExportRepository _invoiceExportRepo;
        private IGDTariffRepository _GDTariffRepo;
        private IShippingAgentTariffExportRepository _agentTariffExportRepo;
        private IClearingAgentExportRepository _clearingAgentExportRepository;
        private IChequeRecivedRepository _chequeRecivedRepository;
        private IChequeRecivedExportRepository _chequeRecivedExportRepository;
        private IExportContainerItemRepositpory _exportContainerItemRepositpory;
        private IExportDeliveryOrderRepository _exportDeliveryOrderRepo;
        private IExportLocationCodeListRepository _unlocodeRepo;
        private IGDIORepository _gDIORepository;
        private IGDIRepository _gDIRepository;
        private IExportVehicleRepository _exportVehicleRepository;
        private IGDHoldRepository _GDHoldRepo;
        private IGateoutExportRepository _gateoutExportRepo;
        private IEmptyReceivingRepository _emptyReceivingRepo;
        private IOGIERepository _ogieRepo;
        private IOGTERepository _ogteRepo;
        private IOSIMRepository _osimRepo;
        private IOCRLRepository _ocrlRepo;
        private IDocumentCodeRepository _documentCodeRepository;
        private ISalesTaxIndexWiseRepository _salesTaxIndexWiseRepo;
        private IEmptyContainerGatePassRepository _emptyContainerGatePassRepo;
        private IReturnToYardRepository _returnToYardRepo;
        private IPreAlertRepository _preAlertRepo;
        private IPaymentUpdateRepository _paymentUpdateRepo;
        private IPreAlertPayOrderRepository _preAlertPayOrderRepo;
        private ITariffInformationRepository _tariffInformationRepo;
        private ITariffPercentageRepository _tariffPercentageRepo;
        private ISealPurchaseRepository _purchaseSealRepo;
        private IImportDropOfTicketRepository _importDropOfTicketRepo;
        private IWaiverRepository _waiverRepo;
        private IExaminationRequestRepository _examinationRequestRepo;
        private IExchangeRateRepository _exchangeRateRepo;
        private IGatePassWeightmentRepository _gatePassWeightmentRepo;
        private IManualGatePassRepository _manualGatePassRepo;
        private ITruckInOutRepository _truckInOutRepo;
        private IConsigneRepository _consigneRepo;
        private IPortChargeRepository _portChargeRepo;
        private ITerminalAndFFShareRateRepository _terminalAndFFShareRateRepo;
        private IInvoiceInquiryRepository _invoiceInquiryRepo;
        private IExaminationTariffInformationRepository _examinationTariffInformationRepo;
        private IGoodsHeadRepository _goodsHeadRepository;
        private IShipperRepository _shipperRepository;
        private IISOCodeHeadRepository _isoCodeHeadRepository;
        private IStorageFreeDayRepository _storageFreeDayRepository;
        private IAICTAndLineIndexTariffRepository _aictAndLineIndexTariffRepository;
        private IBankRepository _bankRepository;
        private IElectronicDeliveryOrderRepository _electronicDeliveryOrderRepository;
        private IExcessAmountRefundSettlementRepository _excessAmountRefundSettlementRepository;
        private IStorageFreeDaysForHolidayRepository _storageFreeDaysForHolidayRepository;
        private ISIMORepository _SIMORepository;
        private ISIMRepository _simrepository;
        private IOGIERepository _ogieRepository;
        private ICustomerNOCRepository _customerNOCRepository;
        private ITariffExportRepository _tariffExportRepository;
        private IShippingAgentExportRepository _shippingAgentExportRepository;
        private INatureOfTariffRepository _natureOfTariffRepository;
        private IGatePassSampleRepository _gatePassSampleRepository;
        private IPGateInOutRepository _pGateInOutRepository;
        private IAictBillingRepository _aictBillingRepository;
        private IConfiguration Configuration;
        private ICSEmptyContainerReceiveRepository _CSEmptyContainerReceiveRepository;
        private IStorageShareRateRepository _storageShareRateRepository;
        private ITariffPerBoxRateRepository _tariffPerBoxRateRepository;
        private IBoundedTranspoterRepository _boundedTranspoterRepository;
        public string Con { get; set; }
        public APICallsController(IPackageTypeExportRepository packageExportRepo,
                                    IPermissionRepository permissionsRepo,
                                    IExportLocationCodeListRepository unlocodeRepo,
                                    IDeliveryOrderRepository deliveryOrderRepo,
                                    IAuctionRepository auctionRepo,
                                    ICargoReceivedRepository cargoReceivedRepo,
                                    IDictionaryRepository dictionaryRepo,
                                    IOrderDetailRepository orderDetailRepo,
                                    IVesselRepository vesselRepo,
                                    IVoyageRepository voyageRepo,
                                    IContainerRepository containerRepo,
                                    IContainerIndexRepository containerIndexRepo,
                                    IClearingAgentRepository clearingAgentRepo,
                                    IIGMORepository iGMORepository,
                                    IShippingAgentRepository shippingAgentRepo,
                                    IDeliveredYardRepositoryRepository deliveredYardRepo,
                                    ITransporterRepository transporterRepo,
                                    IIPAORepository ipaosRepo,
                                    IGateInRepository gateinRepo,
                                    IWeighmentRepository weighmentRepo,
                                    ITariffRepository tariffRepo,
                                    IShippingAgentTariffRepository agentTariffRepo,
                                    IContainerIndexTariffRepository indexTariffRepo,
                                    IAmountReceivedRepository amountReceivedRepo,
                                    IInvoiceRepository invoiceRepo,
                                    IStorageSlabRepository storageSlabRepo,
                                    IShippingAgentTariffExportRepository agentTariffExportRepo,
                                    IBRTRepository brtRepo,
                                    ISalesTaxRepository taxRepo,
                                    ISRCORepository srcoRepo,
                                    IECMORepository ecmoRepo,
                                    IInvoiceDocumentRepository invDocRepo,
                                    ICCMORepository ccmoRepo,
                                    ISCMORepository scmoRepo,
                                    ITTSORepository ttscRepo,
                                    IPGOORepository pgooRepo,
                                    ITarifHeadRepository tarifHeadRepo,
                                    IGroundedContainerRepository groundedContainerRepo,
                                    IDestuffedContainerRepository destuffedRepo,
                                    IECMRepository examinationMarkRepo,
                                    IGateOutRepository gateOutRepo,
                                    IHoldRepository holdRepo,
                                    ICYContainerRepository cyContaienrRepo,
                                    IPartyLedgerRepository partyLedgerRepo,
                                    UserManager<IdentityUser> userManager,
                                    IUsersRepository userRepo,
                                    IAuditRepository auditRepo,
                                    IExportContainerRepository exportContainerRepo,
                                    IEmptyGateOutContainerRepository emptyGateOutContainerRepo,
                                    IShippingLineRepository shippingLineRepo,
                                    IOPIARepository oPIARepo,
                                    ILoadingProgramRepository loadingProgramRepo,
                                    ILoadingProgramDetailRepository loadingProgramDetailRepo,
                                    IEnteringCargoRepository enteringCargoRepo,
                                    IExportGroundedCargoRepository exportGroundedCargoRepo,
                                    ICargoRepository cargoRepo,
                                    IPortAndTerminalRepository portAndTerminalRepo,
                                    IVesselExportRepository vesselExportRepo,
                                    IVoyageExportRepository voyageExportRepo,
                                    ICargoRollOverRepository cargoRollOverRepo,
                                    IAmountReceivedExportRepository amountReceivedExportRepo,
                                    IPartyLedgerExportRepository partyLedgerExportRepo,
                                    IInvoiceExportRepository invoiceExportRepo,
                                    IGDTariffRepository GDTariffRepo,
                                    IExportDeliveryOrderRepository exportDeliveryOrderRepo,
                                    IClearingAgentExportRepository clearingAgentExportRepository,
                                    IChequeRecivedRepository chequeRecivedRepository,
                                    IChequeRecivedExportRepository chequeRecivedExportRepository,
                                    IExportContainerItemRepositpory exportContainerItemRepositpory,
                                    IGDIORepository gDIORepository,
                                    IGDIRepository gDIRepository,
                                    IExportVehicleRepository exportVehicleRepository,
                                    IGDHoldRepository GDHoldRepo,
                                    IGateoutExportRepository gateoutExportRepo,
                                    IEmptyReceivingRepository emptyReceivingRepo,
                                    IOGIERepository ogieRepo,
                                    IOGTERepository ogteRepo,
                                    IOSIMRepository osimRepo,
                                    IOCRLRepository ocrlRepo,
                                    IDocumentCodeRepository documentCodeRepository,
                                    ISalesTaxIndexWiseRepository salesTaxIndexWiseRepo,
                                    IEmptyContainerGatePassRepository emptyContainerGatePassRepo,
                                    IReturnToYardRepository returnToYardRepo,
                                    IPreAlertRepository preAlertRepo,
                                    IPaymentUpdateRepository paymentUpdateRepo,
                                    IPreAlertPayOrderRepository preAlertPayOrderRepo,
                                    ITariffInformationRepository tariffInformationRepo,
                                    ITariffPercentageRepository tariffPercentageRepo,
                                    ISealPurchaseRepository purchaseSealRepo,
                                    IImportDropOfTicketRepository importDropOfTicketRepo,
                                    IWaiverRepository waiverRepo,
                                    IExaminationRequestRepository examinationRequestRepo,
                                    IExchangeRateRepository exchangeRateRepo,
                                    IGatePassWeightmentRepository gatePassWeightmentRepo,
                                    IManualGatePassRepository manualGatePassRepo,
                                    ITruckInOutRepository truckInOutRepo,
                                    IConsigneRepository consigneRepo,
                                    IPortChargeRepository portChargeRepo,
                                    ITerminalAndFFShareRateRepository terminalAndFFShareRateRepo,
                                    IInvoiceInquiryRepository invoiceInquiryRepo,
                                    IExaminationTariffInformationRepository examinationTariffInformationRepo,
                                    IGoodsHeadRepository goodsHeadRepository,
                                    IShipperRepository shipperRepository,
                                    IISOCodeHeadRepository isoCodeHeadRepository,
                                    IStorageFreeDayRepository storageFreeDayRepository,
                                    IAICTAndLineIndexTariffRepository aictAndLineIndexTariffRepository,
                                    IBankRepository bankRepository,
                                    IElectronicDeliveryOrderRepository electronicDeliveryOrderRepository,
                                    IExcessAmountRefundSettlementRepository excessAmountRefundSettlementRepository,
                                    IStorageFreeDaysForHolidayRepository storageFreeDaysForHolidayRepository,
                                    ISIMORepository SIMORepository,
                                    ISIMRepository simrepository,
                                    IOGIERepository ogieRepository,
                                    ICustomerNOCRepository customerNOCRepository,
                                    ITariffExportRepository tariffExportRepository,
                                    IShippingAgentExportRepository shippingAgentExportRepository,
                                    INatureOfTariffRepository natureOfTariffRepository,
                                    IGatePassSampleRepository gatePassSampleRepository,
                                    IPGateInOutRepository pGateInOutRepository,
                                    IAictBillingRepository aictBillingRepository,
                                    IConfiguration _configuration,
                                    ICSEmptyContainerReceiveRepository CSEmptyContainerReceiveRepository,
                                    IStorageShareRateRepository storageShareRateRepository,
                                    ITariffPerBoxRateRepository tariffPerBoxRateRepository,
                                    IBoundedTranspoterRepository boundedTranspoterRepository)
        {
            _permissionsRepo = permissionsRepo;
            _unlocodeRepo = unlocodeRepo;
            _exportDeliveryOrderRepo = exportDeliveryOrderRepo;
            _packageExportRepo = packageExportRepo;
            _cargoReceivedRepo = cargoReceivedRepo;
            _auctionRepo = auctionRepo;
            _auditRepo = auditRepo;
            _dictionaryRepo = dictionaryRepo;
            _amountReceivedRepo = amountReceivedRepo;
            _deliveryOrderRepo = deliveryOrderRepo;
            _orderDetailRepo = orderDetailRepo;
            _deliveredYardRepo = deliveredYardRepo;
            _transporterRepo = transporterRepo;
            _weighmentRepo = weighmentRepo;
            _vesselRepo = vesselRepo;
            _voyageRepo = voyageRepo;
            _containerRepo = containerRepo;
            _containerIndexRepo = containerIndexRepo;
            _tariffRepo = tariffRepo;
            _clearingAgentRepo = clearingAgentRepo;
            _iGMORepository = iGMORepository;
            _shippingAgentRepo = shippingAgentRepo;
            _ipaosRepo = ipaosRepo;
            _gateinRepo = gateinRepo;
            _agentTariffRepo = agentTariffRepo;
            _indexTariffRepo = indexTariffRepo;
            _invoiceRepo = invoiceRepo;
            _storageSlabRepo = storageSlabRepo;
            _brtRepo = brtRepo;
            _invDocRepo = invDocRepo;
            _taxRepo = taxRepo;
            _srcoRepo = srcoRepo;
            _ecmoRepo = ecmoRepo;
            _ccmoRepo = ccmoRepo;
            _scmoRepo = scmoRepo;
            _ttscRepo = ttscRepo;
            _pgooRepo = pgooRepo;
            _tarifHeadRepo = tarifHeadRepo;
            _groundedContainerRepo = groundedContainerRepo;
            _destuffedRepo = destuffedRepo;
            _examinationMarkRepo = examinationMarkRepo;
            _gateOutRepo = gateOutRepo;
            _holdRepo = holdRepo;
            _cyContaienrRepo = cyContaienrRepo;
            _partyLedgerRepo = partyLedgerRepo;
            _userManager = userManager;
            _userRepo = userRepo;
            _exportContainerRepo = exportContainerRepo;
            _emptyGateOutContainerRepo = emptyGateOutContainerRepo;
            _shippingLineRepo = shippingLineRepo;
            _oPIARepo = oPIARepo;
            _loadingProgramRepo = loadingProgramRepo;
            _loadingProgramDetailRepo = loadingProgramDetailRepo;
            _enteringCargoRepo = enteringCargoRepo;
            _exportGroundedCargoRepo = exportGroundedCargoRepo;
            _cargoRepo = cargoRepo;
            _portAndTerminalRepo = portAndTerminalRepo;
            _vesselExportRepo = vesselExportRepo;
            _voyageExportRepo = voyageExportRepo;
            _cargoRollOverRepo = cargoRollOverRepo;
            _amountReceivedExportRepo = amountReceivedExportRepo;
            _partyLedgerExportRepo = partyLedgerExportRepo;
            _invoiceExportRepo = invoiceExportRepo;
            _GDTariffRepo = GDTariffRepo;
            _agentTariffExportRepo = agentTariffExportRepo;
            _clearingAgentExportRepository = clearingAgentExportRepository;
            _chequeRecivedRepository = chequeRecivedRepository;
            _chequeRecivedExportRepository = chequeRecivedExportRepository;
            _exportContainerItemRepositpory = exportContainerItemRepositpory;
            _gDIORepository = gDIORepository;
            _gDIRepository = gDIRepository;
            _exportVehicleRepository = exportVehicleRepository;
            _GDHoldRepo = GDHoldRepo;
            _gateoutExportRepo = gateoutExportRepo;
            _emptyReceivingRepo = emptyReceivingRepo;
            _ogieRepo = ogieRepo;
            _ogteRepo = ogteRepo;
            _osimRepo = osimRepo;
            _ocrlRepo = ocrlRepo;
            _documentCodeRepository = documentCodeRepository;
            _salesTaxIndexWiseRepo = salesTaxIndexWiseRepo;
            _emptyContainerGatePassRepo = emptyContainerGatePassRepo;
            _returnToYardRepo = returnToYardRepo;
            _preAlertRepo = preAlertRepo;
            _paymentUpdateRepo = paymentUpdateRepo;
            _preAlertPayOrderRepo = preAlertPayOrderRepo;
            _purchaseSealRepo = purchaseSealRepo;
            _tariffInformationRepo = tariffInformationRepo;
            _tariffPercentageRepo = tariffPercentageRepo;
            _importDropOfTicketRepo = importDropOfTicketRepo;
            _waiverRepo = waiverRepo;
            _examinationRequestRepo = examinationRequestRepo;
            _exchangeRateRepo = exchangeRateRepo;
            _gatePassWeightmentRepo = gatePassWeightmentRepo;
            _manualGatePassRepo = manualGatePassRepo;
            _truckInOutRepo = truckInOutRepo;
            _consigneRepo = consigneRepo;
            _portChargeRepo = portChargeRepo;
            _terminalAndFFShareRateRepo = terminalAndFFShareRateRepo;
            _invoiceInquiryRepo = invoiceInquiryRepo;
            _examinationTariffInformationRepo = examinationTariffInformationRepo;
            _goodsHeadRepository = goodsHeadRepository;
            _shipperRepository = shipperRepository;
            _isoCodeHeadRepository = isoCodeHeadRepository;
            _storageFreeDayRepository = storageFreeDayRepository;
            _aictAndLineIndexTariffRepository = aictAndLineIndexTariffRepository;
            _bankRepository = bankRepository;
            _electronicDeliveryOrderRepository = electronicDeliveryOrderRepository;
            _excessAmountRefundSettlementRepository = excessAmountRefundSettlementRepository;
            _storageFreeDaysForHolidayRepository = storageFreeDaysForHolidayRepository;
            _SIMORepository = SIMORepository;
            _simrepository = simrepository;
            _ogieRepository = ogieRepository;
            _customerNOCRepository = customerNOCRepository;
            _tariffExportRepository = tariffExportRepository;
            _shippingAgentExportRepository = shippingAgentExportRepository;
            _natureOfTariffRepository = natureOfTariffRepository;
            _gatePassSampleRepository = gatePassSampleRepository;
            _pGateInOutRepository = pGateInOutRepository;
            _aictBillingRepository = aictBillingRepository;
            Configuration = _configuration;

            Con =   _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            _CSEmptyContainerReceiveRepository = CSEmptyContainerReceiveRepository;
            _storageShareRateRepository = storageShareRateRepository;
            _tariffPerBoxRateRepository = tariffPerBoxRateRepository;
            _boundedTranspoterRepository = boundedTranspoterRepository;
        }

        [HttpGet]
        public JsonResult GetAllExportPackages()
        {
            return Json(_packageExportRepo.GetAll());
        }

        [HttpGet]
        public JsonResult GetUNLOCodeRepo()
        {
            return Json(_unlocodeRepo.GetAll());
        }

        [HttpGet]
        public JsonResult GetCountryList()
        {
            return Json(_unlocodeRepo.GetCountries());
        }

        [HttpGet]
        public JsonResult GetDestinationPorts()
        {
            return Json(_unlocodeRepo.GetDestinationPorts());
        }

        [HttpGet]
        public JsonResult GetDestinationPortsByCountry(string countryName)
        {
            return Json(_unlocodeRepo.GetDestinationPortsByCountry(countryName));
        }

        [HttpGet]
        public JsonResult GetPortAndTerminals()
        {
            return Json(_portAndTerminalRepo.GetAll());
        }

        [HttpGet]
        public IActionResult UpdateAuctionContainers()
        {
            _containerRepo.UpdateAuctionContainersCY();

            return Ok();

        }

        public List<Hold> CheckCyContainerHold(string igm, int index)
        {
            var holddata = new List<Hold>();

            var holdreslist = _holdRepo.GetHoldDetail(igm, index, "CY").ToList();

            if (holdreslist.Count() > 0)
            {
                holddata.AddRange(holdreslist);
            }

            var containerlistres = _cyContaienrRepo.GetContainerIndexByIGMAndContainerNo(igm, index).ToList();

            if (containerlistres.Count() > 0)
            {
                foreach (var item in containerlistres)
                {
                    var sim = _simrepository.GetSIMInfo(item.ContainerNo, item.VIRNo);

                    if (sim != null && sim.Status == "HO")
                    {

                        var res = new Hold
                        {
                            HoldDate = sim.CreateDT,
                            IsHold = true,
                            HoldType = "SIM",
                            Nature = "Normal",
                            SpecialInstructions = "can't  create due To sim hold on container " + item.ContainerNo + " and igm " + item.VIRNo,
                        };

                        holddata.Add(res);

                    }

                }

                foreach (var item in containerlistres)
                {
                    var sim = _simrepository.GetSIMInfo(item.CSContainerNumber, item.VIRNo);

                    if (sim != null && sim.Status == "HO")
                    {

                        var res = new Hold
                        {
                            HoldDate = sim.CreateDT,
                            IsHold = true,
                            HoldType = "SIM",
                            Nature = "Normal",
                            SpecialInstructions = "can't  create due To sim hold on container " + item.CSContainerNumber + " and igm " + item.VIRNo,
                        };

                        holddata.Add(res);

                    }

                }
            }

            return holddata;

        }


        public List<Hold> CheckCFSContainerHold(long ContainerIndexId)
        {
            var resdata = new List<Hold>();


            var holdContainer = _containerIndexRepo.GetContainerIndexById(ContainerIndexId);

            if (holdContainer != null)
            {
                var _hold = _holdRepo.GetHoldDetail(holdContainer.VirNo, holdContainer.IndexNo ?? 0, "CFS").ToList();

                if (_hold.Count() > 0)
                {
                    resdata.AddRange(_hold);
                }

                var simo = _SIMORepository.GetSIMOInfoByIGMIndex(holdContainer.VirNo, holdContainer.IndexNo ?? 0);

                if (simo != null && simo.Status == "HO")
                {

                    var ressimo = new Hold
                    {


                        HoldDate = DateTime.Now,
                        IsHold = true,
                        HoldType = "SIMO",
                        Nature = "Normal",
                        SpecialInstructions = "can't  create due To SIMO hold on index " + simo.IndexNumber + " bl " + simo.BLNumber + " igm  " + simo.VIRNumber,
                    };

                    resdata.Add(ressimo);
                }

                if (holdContainer.CargoType == "LCL")
                {
                    var edohold = _containerIndexRepo.GetEDOInfo(ContainerIndexId);

                    if (edohold == true)
                    {
                        var res = new Hold
                        {
                            HoldDate = DateTime.Now,
                            IsHold = true,
                            HoldType = "EDO",
                            Nature = "Normal",
                            SpecialInstructions = "can't  create due To EDO hold on index",
                        };

                        resdata.Add(res);
                    }

                    var lineindexrates = _aictAndLineIndexTariffRepository.GetLineIndexRates(holdContainer.VirNo, Convert.ToInt64(holdContainer.IndexNo));

                    if (lineindexrates == null)
                    {
                        var ressimo = new Hold
                        {
                            HoldDate = DateTime.Now,
                            IsHold = true,
                            HoldType = "Line Index Rates are Not Updated",
                            Nature = "Normal",
                            SpecialInstructions = "Index On Finance Hold",
                        };

                        resdata.Add(ressimo);
                    }

                    var conindexlist = _containerIndexRepo.GetContainerIndexByIgmAndIndex(holdContainer.VirNo, holdContainer.IndexNo ?? 0).ToList();

                    if (conindexlist.Count() > 0)
                    {
                        if (conindexlist.Where(x => x.InvoiceLocked == true).Count() > 0)
                        {
                            var ressimo = new Hold
                            {


                                HoldDate = DateTime.Now,
                                IsHold = true,
                                HoldType = "Un Lock Generate Bill",
                                Nature = "Normal",
                                SpecialInstructions = "Index On Finance Hold",
                            };

                            resdata.Add(ressimo);
                        }


                    }


                }

            }





            return resdata;

            //var container = _containerIndexRepo.GetContainerIndexById(ContainerIndexId);

            //if(container != null)
            //{
            //    if(container.IsDelivered == true)
            //    {

            //        return Json(new { HoldStatus = true, SpecialInstructions = "Index Mark Deliverd", LotNo = "" });
            //    }
            //    else
            //    {
            //        return Json(new { HoldStatus = false, SpecialInstructions = "", LotNo = "" });
            //    }
            //}
            //else
            //{
            //    return Json(new { HoldStatus = true, SpecialInstructions = "no container found", LotNo = "" });
            //}

            //var holdContainer = _containerIndexRepo.GetFirst(c => c.ContainerIndexId == ContainerIndexId && c.IsHold == true);

            //if (holdContainer != null)
            //{
            //    var _hold = _holdRepo.GetAll().Where(c => c.ContainerIndexId == holdContainer.ContainerIndexId && c.IsHold == false).LastOrDefault();

            //    if (_hold != null)
            //        return Json(new { HoldStatus = true, SpecialInstructions = _hold.SpecialInstructions, LotNo = "" });

            //    return Json(new { HoldStatus = true, SpecialInstructions = "container on hold", LotNo = "" });
            //}

            //var auctionContainer = _containerIndexRepo.GetFirst(c => c.ContainerIndexId == ContainerIndexId && c.IsAuction == true);

            //if (auctionContainer != null)
            //{

            //        return Json(new { HoldStatus = true, SpecialInstructions = "Auction Lot Apply", LotNo = auctionContainer.AuctionLotNo });

            //}

            //return Json(new { HoldStatus = false, SpecialInstructions = "", LotNo = "" });
        }

        public List<Hold> CheckCFSContainerHoldIGMIndexWise(string igm, int indexno)
        {
            var resdata = new List<Hold>();


            var _hold = _holdRepo.GetHoldDetail(igm, indexno, "CFS").ToList();

            if (_hold.Count() > 0)
            {
                resdata.AddRange(_hold);
            }



            var simo = _SIMORepository.GetSIMOInfoByIGMIndex(igm, indexno);

            if (simo != null && simo.Status == "HO")
            {

                var ressimo = new Hold
                {
                    HoldDate = DateTime.Now,
                    IsHold = true,
                    HoldType = "SIMO",
                    Nature = "Normal",
                    SpecialInstructions = "can't  create due To SIMO hold on index " + simo.IndexNumber + " bl " + simo.BLNumber + " igm  " + simo.VIRNumber,
                };

                resdata.Add(ressimo);
            }

            return resdata;


        }

        public IActionResult CheckCFSTellysheetHold(string virno, string containerno)
        {
            var resdata = new List<Hold>();

            var containerres = _containerIndexRepo.GetontainerIndexByIGMandContainerNumber(virno, containerno).ToList();

            if (containerres.Count() > 0)
            {
                foreach (var item in containerres)
                {
                    var _hold = _holdRepo.GetHoldDetail(item.VirNo, item.IndexNo ?? 0, "CFS").ToList();

                    if (_hold.Count() > 0)
                    {
                        foreach (var itemres in _hold)
                        {
                            if (itemres.HoldType == "ShippinLineHold")
                            {
                                resdata.AddRange(_hold);
                            }
                        }


                    }
                }

            }

            if (resdata.Count() > 0)
            {
                return new OkObjectResult(new { error = true, message = resdata });

            }
            else
            {
                return new OkObjectResult(new { error = false, message = "no data found" });
            }



        }


        public IActionResult CheckCFSTellysheetHoldbyindex(string virno, long indexno)
        {
            var resdata = new List<Hold>();

            var containerres = _containerIndexRepo.GetIndexInfo(virno, indexno).ToList();

            if (containerres.Count() > 0)
            {

                foreach (var item in containerres)
                {
                    var _hold = _holdRepo.GetHoldDetail(item.VirNo, item.IndexNo ?? 0, "CFS").ToList();

                    if (_hold.Count() > 0)
                    {
                        foreach (var itemres in _hold)
                        {
                            if (itemres.HoldType == "ShippinLineHold")
                            {
                                resdata.AddRange(_hold);
                            }
                        }
                    }
                }

            }

            if (resdata.Count() > 0)
            {
                return new OkObjectResult(new { error = true, message = resdata });

            }
            else
            {
                return new OkObjectResult(new { error = false, message = "no data found" });
            }



        }


        [HttpGet]
        public IActionResult GetAuditData()
        {
            var _audit = _auditRepo.GetAll();

            return Json(_audit);
        }

        [HttpGet]
        public IActionResult GetOrderDetail()
        {
            var gatePassId = _orderDetailRepo.GetOrderDetailNo();
            return Json(gatePassId);
        }

        [HttpGet]
        public object GetIGMS(DataSourceLoadOptions loadOptions)
        {
            //var igms = _voyageRepo.GetAll().Select(v => v.VIRNo);
            var igms = _voyageRepo.GetVoyageIGMSList().Select(v => v.VIRNo);

            return DataSourceLoader.Load(igms, loadOptions);
        }



        [HttpGet]
        public object GetIGMOIGMS(DataSourceLoadOptions loadOptions)
        {
            var igms = _iGMORepository.GetAll().Select(c => new IGMNumberViewModel
            {
                VirNumber = c.VIRNumber
            }).GroupBy(x => x.VirNumber).Select(x => x.FirstOrDefault());

            var newigm = igms.Select(x => x.VirNumber);
            return DataSourceLoader.Load(newigm, loadOptions);
        }


        [HttpGet]
        public object GetIGMOContainerNumbers(DataSourceLoadOptions loadOptions)
        {
            var igms = _iGMORepository.GetAll().Select(c => new IGMNumberViewModel
            {
                VirNumber = c.ContainerNumber
            }).GroupBy(x => x.VirNumber).Select(x => x.FirstOrDefault());

            var newigm = igms.Select(x => x.VirNumber);
            return DataSourceLoader.Load(newigm, loadOptions);
        }



        public IActionResult GetIndexesDropdownList(string IGM)
        {

            var indexes = _containerIndexRepo.GetList(c => c.VirNo == IGM)
                .Select(c => new IndexDropdownViewModel
                {
                    ContainerIndexId = c.ContainerIndexId,
                    IndexNo = c.IndexNo ?? 0
                }).GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault());

            return PartialView("_CFSIndexDropbox", indexes);



            return null;
        }
        public IActionResult GetIGMContainersDropdownList(string IGM)
        {

            var containers = _containerIndexRepo.GetList(c => c.VirNo == IGM).OrderBy(c => c.ContainerNo)
               .Select(c => new ContainerIndex
               {
                   ContainerIndexId = c.ContainerIndexId,
                   ContainerNo = c.ContainerNo
               }).GroupBy(x => x.ContainerNo).Select(x => x.FirstOrDefault());


            return PartialView("_Filters_Indexes", containers);


        }


        [HttpGet]
        public object GetGateInContainers(DataSourceLoadOptions loadOptions)
        {
            var containers = _gateinRepo.GetAll().Select(v => v.ContainerNo);

            return DataSourceLoader.Load(containers, loadOptions);
        }



        [HttpGet]
        public object GetAuctionDeliveryIGMS(DataSourceLoadOptions loadOptions)
        {

            var igms = _voyageRepo.GetVoyages().Select(v => v.VIRNo).Distinct();

            return DataSourceLoader.Load(igms, loadOptions);
        }


        [HttpGet]
        public object GetAuctionDeliveryIGMSCY(DataSourceLoadOptions loadOptions)
        {

            var igms = _voyageRepo.GetVoyagesCY().Select(v => v.VIRNo);

            return DataSourceLoader.Load(igms, loadOptions);
        }


        public object GetVoyageExport(DataSourceLoadOptions loadOptions)
        {
            var virNO = _voyageExportRepo.GetAll().Select(x => x.VirNumber);
            return DataSourceLoader.Load(virNO, loadOptions);

        }

        [HttpGet]
        public object Getinvoices(DataSourceLoadOptions loadOptions)
        {
            var invoices = _invoiceRepo.GetAll().Select(i => i.InvoiceNo);

            return DataSourceLoader.Load(invoices, loadOptions);
        }

        [HttpGet]
        public object ReprintGetinvoices(DataSourceLoadOptions loadOptions)
        {
            var resuser = GetLoginUserInfo();

            if (resuser != 0)
            {
                var invoices = _invoiceRepo.GetAll().Where(x => x.InvoiceCategory == "FF").Select(i => i.InvoiceNo);

                return DataSourceLoader.Load(invoices, loadOptions);
            }
            else
            {
                var invoices = _invoiceRepo.GetAll().Select(i => i.InvoiceNo);

                return DataSourceLoader.Load(invoices, loadOptions);
            }


        }
        public JsonResult GetinvoicesByInvoiceNo(string InvoiceNo)
        {
            var invoices = _invoiceRepo.GetAll().Where(x => x.InvoiceNo == InvoiceNo).ToList();

            if (invoices.Count() > 0)
            {
                return Json(invoices);
            }
            return Json("");
        }



        public JsonResult GetCFSinvoicesByContainerindexid(long containerindexid)
        {
            var invoices = _invoiceRepo.GetAll().Where(x => x.ContainerIndexId == containerindexid).ToList();

            if (invoices.Count() > 0)
            {
                return Json(invoices);
            }
            return Json("");
        }

        public JsonResult GetcyinvoicesByigmindex(string igm, long index)
        {
            var container = _cyContaienrRepo.GetAll().Where(x => x.VIRNo == igm && x.IndexNo == index).FirstOrDefault();
            if (container != null)
            {
                var invoices = _invoiceRepo.GetAll().Where(x => x.CYContainerId == container.CYContainerId).ToList();

                if (invoices.Count() > 0)
                {
                    return Json(invoices);
                }
            }

            return Json("");
        }


        public JsonResult GetInvoicesByIGMIndex(string virno, string indexno, string type)
        {
            var invoices = _invoiceRepo.GetInvoicesByIGMIndex(virno, indexno, type).ToList();

            return Json(invoices);

        }


        [HttpGet]
        public object GetinvoicesExport(DataSourceLoadOptions loadOptions)
        {
            var invoices = _invoiceExportRepo.GetAll().Select(i => i.InvoiceNo);

            return DataSourceLoader.Load(invoices, loadOptions);
        }


        [HttpGet]
        public string GetInvoiceType(string BillNo)
        {
            var invoice = _invoiceRepo.GetFirst(s => s.InvoiceNo == BillNo);

            if (invoice != null)
            {
                return invoice.Type;
            }

            return "";
        }

        [HttpGet]
        public IActionResult GetVessel(string IGM)
        {
            //var voyage = _voyageRepo.GetFirst(v => v.VIRNo == IGM);
            //if (voyage != null)
            //{
            //    var vessel = _vesselRepo.GetFirst(v => v.VesselId == voyage.VesselId, v => v.Voyages);
            //    return Json(vessel);
            //}

            var voyage = _voyageRepo.GetVoyageByIGM(IGM);
            if (voyage != null)
            {
                var vessel = _vesselRepo.GetFirst(v => v.VesselId == voyage.VesselId, v => v.Voyages);
                return Json(vessel);
            }

            return Ok();
        }

        [HttpGet]
        public IActionResult GetCyContainerSizes(string igm, long indexNo)
        {
            return Json(_cyContaienrRepo.GetList(s => s.IndexNo == indexNo && s.VIRNo == igm).Select(s => new { ContainerNo = s.ContainerNo, Size = s.Size }));
        }



        [HttpGet]
        public JsonResult GetCYContainerListBYIGM(string igm, long indexNo)
        {
            var data = _containerRepo.GetCYContainerListBYIGM(igm, indexNo).ToList();
            if (data.Count() > 0)
            {

                return Json(data);
            }

            return Json("");

        }







        [HttpGet]
        public IActionResult GetCFSIndexWiseContainerSizes(string igm, long indexNo)
        {
            var voyage = _voyageRepo.GetList(x => x.VIRNo == igm).First();
            return Json(_containerIndexRepo.GetList(s => s.IndexNo == indexNo && s.VoyageId == voyage.VoyageId).Select(s => new { ContainerNo = s.ContainerNo, Size = s.Size }));
        }
        [HttpGet]
        public IActionResult GetBLnumberByIndexNumber(long indexnumber)

        {
            var blnumber = _iGMORepository.GetList(v => v.IndexNumber == indexnumber);
            if (blnumber != null)
            {
                return PartialView("_BLNumberDropBox", blnumber);
            }

            return null;
        }


        [HttpGet]
        public IActionResult GetFiltersForHold(string Type)
        {
            if (Type == "CFS")
                return PartialView("~/Areas/Import/Views/Shared/_Filter_IndexAndBLWise.cshtml");

            return PartialView("~/Areas/Import/Views/Shared/_Filter_Index_CY.cshtml");
        }

        [HttpGet]
        public IActionResult GetFiltersForUnlockBill(string Type)
        {
            if (Type == "CFS")
                return PartialView("~/Areas/Import/Views/Shared/FilterIndexForUnLockGenerateBill.cshtml");

            return PartialView("~/Areas/Import/Views/Shared/_Filter_Index_CY.cshtml");
        }


        [HttpGet]
        public IActionResult GetFilters(string Type)
        {
            if (Type == "CFS")
                return PartialView("~/Areas/Import/Views/Shared/_Filter_Index.cshtml");

            return PartialView("~/Areas/Import/Views/Shared/_Filter_Index_CY.cshtml");
        }


        [HttpGet]
        public IActionResult GetFiltersAuction(string Type)
        {
            if (Type == "CFS")
                return PartialView("~/Areas/Import/Views/Shared/_Filter_IndexAuction.cshtml");

            return PartialView("~/Areas/Import/Views/Shared/_Filter_Index_CYAuction.cshtml");
        }
        [HttpGet]
        public IActionResult GetFiltersAuctionDelivery(string Type)
        {
            if (Type == "CFS")
                return PartialView("~/Areas/Import/Views/Shared/_Filter_IndexAuctionDelivery.cshtml");

            return PartialView("~/Areas/Import/Views/Shared/_Filter_Index_CYAuctionDelivery.cshtml");
        }




        [HttpGet]
        public IActionResult GetFiltersForDeliveryOrder(string Type)
        {
            if (Type == "CFS")
                return PartialView("~/Areas/Import/Views/Shared/FilterForCFSDeliveryOrder.cshtml");

            return PartialView("~/Areas/Import/Views/Shared/_Filter_Index_CY.cshtml");
        }


        [HttpGet]
        public IActionResult GetFiltersBrtCFS()
        {
            return PartialView("~/Areas/Import/Views/Shared/_Filter_Index.cshtml");
        }


        [HttpGet]
        public IActionResult GetFiltersBRTCY()
        {
            return PartialView("~/Areas/Import/Views/Shared/_Filter_Index_CY.cshtml");
        }


        [HttpGet]
        public IActionResult GetFilterForContainers(string Type)
        {
            if (Type == "CFS")
                return PartialView("~/Areas/Import/Views/Shared/_Filters_Container.cshtml");

            return PartialView("~/Areas/Import/Views/Shared/_Filter_Index_CY.cshtml");
        }

        [HttpGet]
        public IActionResult GetFilterForContainerTrackingReport(string Type)
        {
            if (Type == "CFS")
                return PartialView("~/Areas/Import/Views/Shared/FilterCFSTrackingontainers.cshtml");

            return PartialView("~/Areas/Import/Views/Shared/_Filter_Index_CY.cshtml");
        }

        [HttpGet]
        public IActionResult GetFiltersAll(string Type)
        {
            if (Type == "CFS")
                return PartialView("~/Areas/Import/Views/Shared/_ContainerOrientation.cshtml");

            return PartialView("~/Areas/Import/Views/Shared/_Filter_Index_CY_All.cshtml");
        }
        [HttpGet]
        public IActionResult GetFilterIndexWise(string Type)
        {
            if (Type == "CFS")
                return PartialView("~/Areas/Import/Views/Shared/_Filter_Index.cshtml");

            return PartialView("~/Areas/Import/Views/Shared/_Filter_Index_CY_All.cshtml");
        }


        [HttpGet]
        public IActionResult GetFilterIndexWiseInvoice(string Type)
        {
            if (Type == "CFS")
                return PartialView("~/Areas/Import/Views/Shared/_Filter_IndexForCFSInvoice.cshtml");

            return PartialView("~/Areas/Import/Views/Shared/_Filter_IndexForCYInvoice.cshtml");
        }

        [HttpGet]
        public IActionResult GetFilterIndexWiseForGatePassWeighment()
        {

            return PartialView("~/Areas/Import/Views/Shared/_Filter_Index.cshtml");

        }


        [HttpGet]
        public IActionResult GetFilterForIGMDetail(string Type)
        {
            if (Type == "CYContainers")
            {
                //return PartialView("~/Areas/Import/Views/Shared/_Filter_Index_CY_All.cshtml");
                return PartialView("~/Areas/Import/Views/Shared/_Filter_Index_CY_All_MultiSelect.cshtml");

            }
            if (Type == "CFSIndexWise")
            {
                return PartialView("~/Areas/Import/Views/Shared/_CFSIndexesMultiSelect.cshtml");
                //return PartialView("~/Areas/Import/Views/Shared/_CFSFilter_Index_MultiSelect.cshtml");
                //return PartialView("~/Areas/Import/Views/Shared/_Filter_Index.cshtml");
            }
            else
            {
                return PartialView("~/Areas/Import/Views/Shared/_CFSContainersMultiSelect.cshtml");
                //return PartialView("~/Areas/Import/Views/Shared/_Filters_Containers_ForMultiSelect.cshtml");
            }

        }


        [HttpGet]
        public IActionResult GetVoyages(long VesselExportId)
        {
            var voyages = _voyageExportRepo.GetList(v => v.VesselExportId == VesselExportId);

            ViewData["Voyages"] = voyages.Select(x => new SelectListItem { Text = x.VoyageNumber, Value = x.VoyageExportId.ToString() });

            return PartialView("~/Views/Shared/_VoyageDropbox.cshtml");


        }

        [HttpGet]
        public IActionResult GetPermissions()
        {
            var role = User.Claims.Where(c => c.Type == ClaimTypes.Role)
                       .Select(c => c.Value)
                       .FirstOrDefault();


            return Json(_permissionsRepo.GetUserPermissions(role));
        }


        [HttpGet]
        public IActionResult GetPermissionsmultiple()
        {


            var role = User.Claims.Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value).ToList();



            return Json(_permissionsRepo.GetUserPermissions(role));
        }




        [HttpGet]
        public IActionResult GetPermissionItems(string url)
        {
            var role = User.Claims.Where(c => c.Type == ClaimTypes.Role)
                       .Select(c => c.Value)
                       .FirstOrDefault();

            return Json(_permissionsRepo.GetUserGetPermissionItems(role, url));
        }



        [HttpGet]
        public IActionResult GetPermissionItemsList()
        {

            var role = User.Claims.Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value).ToList();


            return Json(_permissionsRepo.GetUserGetPermissionItemsList(role));
        }


        [HttpGet]
        public IActionResult GetPackTypeByBLNumber(string blnumber)
        {
            var value = _iGMORepository.GetList(v => v.BLNumber == blnumber).Select(s => new IGMO
            {
                IGMOId = s.IGMOId,
                PackageType = s.PackageType.Split('=')[0],
                NumberofPackages = s.NumberofPackages
            });
            if (value != null)
            {
                return Json(value);
            }
            return null;
        }

        [HttpGet]
        public IActionResult GetPartialViewIndexContainer(string orientation, bool destuff)
        {
            if (destuff)
            {
                return PartialView("~/Areas/Import/Views/Shared/_Filter_Index_Destuff.cshtml");
            }
            if (orientation == "index")
            {
                return PartialView("~/Areas/Import/Views/Shared/_Filter_Index.cshtml");
            }
            else
            {
                return PartialView("~/Areas/Import/Views/Shared/_Filters_Container.cshtml");
            }
        }

        [HttpGet]
        public IActionResult GetContainersDropdown(string voyageNo, string IGM)
        {
            var voyage = _voyageRepo.GetFirst(v => v.VoyageNo == voyageNo && v.VIRNo == IGM);

            if (voyage != null)
            {
                var containers = _containerIndexRepo.GetList(c => c.VoyageId == voyage.VoyageId && c.IsDestuffed == false).OrderBy(c => c.ContainerNo)
                 .Select(c => new ContainerIndex
                 {
                     ContainerIndexId = c.ContainerIndexId,
                     ContainerNo = c.ContainerNo
                 }).GroupBy(x => x.ContainerNo).Select(x => x.FirstOrDefault());


                return PartialView("_ContainerDropbox", containers);

            }

            return null;
        }


        [HttpGet]
        public IActionResult GetContainersDropdownList(string voyageNo, string IGM)
        {
            var voyage = _voyageRepo.GetFirst(v => v.VoyageNo == voyageNo && v.VIRNo == IGM);

            if (voyage != null)
            {
                var containers = _containerIndexRepo.GetList(c => c.VoyageId == voyage.VoyageId).OrderBy(c => c.ContainerNo)
                 .Select(c => new ContainerIndex
                 {
                     ContainerIndexId = c.ContainerIndexId,
                     ContainerNo = c.ContainerNo
                 }).GroupBy(x => x.ContainerNo).Select(x => x.FirstOrDefault());


                return PartialView("_ContainerDropbox", containers);

            }

            return null;
        }


        public IActionResult GetContainersDropdownListMultiSelect(string voyageNo, string IGM)
        {
            var voyage = _voyageRepo.GetFirst(v => v.VoyageNo == voyageNo && v.VIRNo == IGM);

            if (voyage != null)
            {
                var containers = _containerIndexRepo.GetList(c => c.VoyageId == voyage.VoyageId).OrderBy(c => c.ContainerNo)
                 .Select(c => new ContainerIndex
                 {
                     ContainerIndexId = c.ContainerIndexId,
                     ContainerNo = c.ContainerNo
                 }).GroupBy(x => x.ContainerNo).Select(x => x.FirstOrDefault());


                return PartialView("_Filters_Indexes", containers);

            }

            return null;
        }





        [HttpGet]
        public IActionResult GetAllContainersDropdown(string voyageNo, string IGM)
        {
            var voyage = _voyageRepo.GetFirst(v => v.VoyageNo == voyageNo && v.VIRNo == IGM);

            if (voyage != null)
            {
                var containers = _containerIndexRepo.GetList(c => c.VoyageId == voyage.VoyageId).OrderBy(c => c.ContainerNo)
                    .Select(c => new ContainerIndex
                    {
                        ContainerNo = c.ContainerNo
                    }).GroupBy(x => x.ContainerNo).Select(x => x.FirstOrDefault());


                return PartialView("_ContainerDropbox", containers);

            }

            return null;
        }


        [HttpGet]
        public IActionResult GetAllCFSAndCYContainersDropdown(string voyageNo, string IGM)
        {

            var containers = new List<ContainerIndexDetailViewModel>();



            var cfscontainers = _containerIndexRepo.GetList(c => c.VirNo == IGM).OrderBy(c => c.ContainerNo)
                .Select(c => new ContainerIndexDetailViewModel
                {
                    ContainerNumber = c.ContainerNo
                }).GroupBy(x => x.ContainerNumber).Select(x => x.FirstOrDefault());


            var cycontainers = _cyContaienrRepo.GetList(c => c.VIRNo == IGM).OrderBy(c => c.ContainerNo)
           .Select(c => new ContainerIndexDetailViewModel
           {
               ContainerNumber = c.ContainerNo
           }).GroupBy(x => x.ContainerNumber).Select(x => x.FirstOrDefault());

            containers.AddRange(cfscontainers);
            containers.AddRange(cycontainers);


            return PartialView("_CFSCYContainerDropDown", containers);



            return null;
        }


        [HttpGet]
        public JsonResult GetAllCFSAndCYIGMDropdown(string voyageNo, string IGM)
        {

            var data = _containerRepo.GetIGMCFSOrCYConatiner(voyageNo, IGM);

            if (data.Count() > 0)
            {
                return Json(data);

            }

            return Json("");
        }

        [HttpGet]
        public IActionResult GetContainersDropdownWithOutDestuff(string voyageNo, string IGM)
        {
            var voyage = _voyageRepo.GetFirst(v => v.VoyageNo == voyageNo && v.VIRNo == IGM);

            if (voyage != null)
            {
                var containers = _containerIndexRepo.GetList(c => c.VoyageId == voyage.VoyageId).OrderBy(c => c.ContainerNo)
                    .Select(c => new ContainerIndex
                    {
                        ContainerIndexId = c.ContainerIndexId,
                        ContainerNo = c.ContainerNo
                    }).GroupBy(x => x.ContainerNo).Select(x => x.FirstOrDefault());


                return PartialView("_ContainerDropbox", containers);

            }

            return null;
        }

        [HttpGet]
        public IActionResult GetIndexDropdown(string voyageNo, string IGM)
        {
            //var voyage = _voyageRepo.GetFirst(v => v.VoyageNo == voyageNo && v.VIRNo == IGM);

            //if (voyage != null)
            //{
            //    var indexes = _containerIndexRepo.GetList(c => c.VoyageId == voyage.VoyageId)
            //        .Select(c => new IndexDropdownViewModel
            //        {
            //            ContainerIndexId = c.ContainerIndexId,
            //            IndexNo = c.IndexNo ?? 0
            //        }).GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault());

            //    return PartialView("_IndexDropbox", indexes);

            //}


            var indexes = _containerIndexRepo.GetIndexesByVirno(IGM)
                .Select(c => new IndexDropdownViewModel
                {
                    ContainerIndexId = c.ContainerIndexId,
                    IndexNo = c.IndexNo ?? 0
                }).GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault());

            return PartialView("_IndexDropbox", indexes);



            return null;
        }



        [HttpGet]
        public IActionResult GetIndexDropdownForCFSInvoiceIndexes(string IGM)
        {
            var indexes = _containerIndexRepo.GetIndexesByVirno(IGM)
                .Select(c => new IndexDropdownViewModel
                {
                    ContainerIndexId = c.ContainerIndexId,
                    IndexNo = c.IndexNo ?? 0
                }).GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault());

            return PartialView("_IndexDropbox", indexes);

            return null;
        }

        [HttpGet]
        public IActionResult GetIndexDropdownForCYInvoiceIndexes(string IGM)
        {
            var containers = _cyContaienrRepo.GetCYIndexesByVirno(IGM);


            if (containers.Count > 0)
            {
                containers = containers.GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault()).ToList();
                return PartialView("_CYContainerIndexDropbox", containers);
            }


            return null;
        }


        [HttpGet]
        public IActionResult GetIndexDropdownForCFSAndCY(string IGM)
        {
            var Containersindexes = new List<CYContainer>();

            var containers = _cyContaienrRepo.GetCYIndexesByVirno(IGM);

            Containersindexes.AddRange(containers);

            var indexes = _containerIndexRepo.GetIndexesByVirno(IGM)
                .Select(c => new CYContainer
                {
                    IndexNo = c.IndexNo ?? 0
                }).GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault());

            Containersindexes.AddRange(indexes);

            if (Containersindexes.Count > 0)
            {
                Containersindexes = Containersindexes.GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault()).ToList();
                return PartialView("_CYContainerIndexDropbox", Containersindexes);
            }

            return null;
        }


        [HttpGet]
        public IActionResult GetIndexDropdownForCFS(string IGM)
        {
            var Containersindexes = new List<CYContainer>();



            var indexes = _containerIndexRepo.GetIndexesByVirno(IGM)
                .Select(c => new CYContainer
                {
                    IndexNo = c.IndexNo ?? 0
                }).GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault());

            Containersindexes.AddRange(indexes);

            if (Containersindexes.Count > 0)
            {
                Containersindexes = Containersindexes.GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault()).ToList();
                return PartialView("_CYContainerIndexDropbox", Containersindexes);
            }

            return null;
        }


        [HttpGet]
        public IActionResult GetIndexDropdownForCY(string IGM)
        {
            var Containersindexes = new List<ContainerIndexDetailViewModel>();



            var indexes = _cyContaienrRepo.GetCYIndexesByVirno(IGM)
                .Select(c => new ContainerIndexDetailViewModel
                {
                    ContainerNumber = c.ContainerNo
                }).GroupBy(x => x.ContainerNumber).Select(x => x.FirstOrDefault());

            Containersindexes.AddRange(indexes);

            if (Containersindexes.Count > 0)
            {
                Containersindexes = Containersindexes.GroupBy(x => x.ContainerNumber).Select(x => x.FirstOrDefault()).ToList();
                return PartialView("_CFSCYContainerDropDown", Containersindexes);
            }

            return null;
        }

        [HttpGet]
        public IActionResult GetVirNumberDropdownForCFSAndCY(string containernumber)
        {
            var Containersindexes = new List<VirNumberDetail>();

            var containers = _cyContaienrRepo.GetCYIGMsByContainerNumber(containernumber).Select(c => new VirNumberDetail
            {
                VirNumber = c.VIRNo
            }).ToList();

            Containersindexes.AddRange(containers);

            var indexes = _containerIndexRepo.GetCFSIGMsByContainerNumber(containernumber)
                .Select(c => new VirNumberDetail
                {
                    VirNumber = c.VirNo
                }).ToList();

            Containersindexes.AddRange(indexes);

            if (Containersindexes.Count > 0)
            {
                Containersindexes = Containersindexes.GroupBy(x => x.VirNumber).Select(x => x.FirstOrDefault()).ToList();
                return PartialView("_VirNumberListDropBox", Containersindexes);
            }

            return null;
        }


        [HttpGet]
        public IActionResult GetIGMOIndexDropdown(string IGM)
        {

            var indexes = _iGMORepository.GetIndexInfo(IGM)
                .Select(c => new IndexDropdownViewModel
                {
                    IndexNo = c.IndexNumber ?? 0
                }).GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault());


            return PartialView("_IndexDropbox", indexes);

        }


        public JsonResult GetMultipleInvoicesByIgmIndex(string IGM, int index)
        {
            var indexes = _iGMORepository.GetIndexDetail(IGM, index);

            if (indexes != null)
            {
                if (indexes.Status == "CFS")
                {
                    var res = _containerIndexRepo.GetSingleIndexInfo(IGM, index);

                    if (res != null)
                    {
                        var invoices = _invoiceRepo.GetAll().Where(x => x.ContainerIndexId == res.ContainerIndexId).ToList();

                        if (invoices.Count() > 0)
                        {
                            return Json(invoices);
                        }
                    }
                }
                if (indexes.Status == "CY")
                {

                    var res = _cyContaienrRepo.GetContainerCYByIGMAndIndex(IGM, index);

                    if (res != null)
                    {
                        var invoices = _invoiceRepo.GetAll().Where(x => x.CYContainerId == res.CYContainerId).ToList();

                        if (invoices.Count() > 0)
                        {
                            return Json(invoices);
                        }
                    }
                }
            }

            return Json("");

        }


        public JsonResult ReprintGetMultipleInvoicesByIgmIndex(string IGM, int index)
        {
            var resuser = GetLoginUserInfo();

            var indexes = _iGMORepository.GetIndexDetail(IGM, index);

            if (indexes != null)
            {
                if (indexes.Status == "CFS")
                {
                    var res = _containerIndexRepo.GetSingleIndexInfo(IGM, index);

                    if (res != null)
                    {

                        if (resuser != 0)
                        {
                            var invoices = _invoiceRepo.GetAll().Where(x => x.ContainerIndexId == res.ContainerIndexId && x.InvoiceCategory == "FF").ToList();

                            if (invoices.Count() > 0)
                            {
                                return Json(invoices);
                            }
                        }
                        else
                        {
                            var invoices = _invoiceRepo.GetAll().Where(x => x.ContainerIndexId == res.ContainerIndexId).ToList();

                            if (invoices.Count() > 0)
                            {
                                return Json(invoices);
                            }
                        }


                    }
                }
                if (indexes.Status == "CY")
                {

                    var res = _cyContaienrRepo.GetContainerCYByIGMAndIndex(IGM, index);

                    if (res != null)
                    {
                        if (resuser != 0)
                        {
                            var invoices = _invoiceRepo.GetAll().Where(x => x.CYContainerId == res.CYContainerId && x.InvoiceCategory == "FF").ToList();

                            if (invoices.Count() > 0)
                            {
                                return Json(invoices);
                            }
                        }
                        else
                        {
                            var invoices = _invoiceRepo.GetAll().Where(x => x.CYContainerId == res.CYContainerId).ToList();

                            if (invoices.Count() > 0)
                            {
                                return Json(invoices);
                            }
                        }


                    }
                }
            }

            return Json("");

        }

        public IActionResult GetIndexDropdownList(string voyageNo, string IGM)
        {
            var voyage = _voyageRepo.GetFirst(v => v.VoyageNo == voyageNo && v.VIRNo == IGM);

            if (voyage != null)
            {
                var indexes = _containerIndexRepo.GetList(c => c.VoyageId == voyage.VoyageId)
                    .Select(c => new IndexDropdownViewModel
                    {
                        ContainerIndexId = c.ContainerIndexId,
                        IndexNo = c.IndexNo ?? 0
                    }).GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault());

                return PartialView("_CFSIndexDropbox", indexes);

            }

            return null;
        }


        [HttpGet]
        public IActionResult GetAuctionIndexDropdown(string voyageNo, string IGM)
        {
            var voyage = _voyageRepo.GetFirst(v => v.VoyageNo == voyageNo && v.VIRNo == IGM);

            if (voyage != null)
            {
                var indexes = _containerIndexRepo.GetList(c => c.VoyageId == voyage.VoyageId && c.AuctionLotNo != null)
                    .Select(c => new IndexDropdownViewModel
                    {
                        ContainerIndexId = c.ContainerIndexId,
                        IndexNo = c.IndexNo ?? 0
                    }).GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault());

                return PartialView("_IndexDropbox", indexes);

            }

            return null;
        }


        [HttpGet]
        public IActionResult GetDeliverAuctionIndexDropdown(string voyageNo, string IGM)
        {

            //var voyage = _voyageRepo.GetVoyages().Where(v => v.VoyageNo == voyageNo && v.VIRNo == IGM).LastOrDefault();
            //if (voyage != null)
            //{
            //    var indexes = _containerIndexRepo.GetList(c => c.VoyageId == voyage.VoyageId )
            //        .Select(c => new IndexDropdownViewModel
            //        {
            //            ContainerIndexId = c.ContainerIndexId,
            //            IndexNo = c.IndexNo ?? 0
            //        }).GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault());

            var indexes = _voyageRepo.GetContainerIndexs(voyageNo, IGM).GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault());

            return PartialView("_IndexDropbox", indexes);

            // }

            return null;
        }

        [HttpGet]
        public IActionResult GetIndexDropdownForUnlockBill(string voyageNo, string IGM)
        {
            var voyage = _voyageRepo.GetFirst(v => v.VoyageNo == voyageNo && v.VIRNo == IGM);

            if (voyage != null)
            {
                var indexes = _containerIndexRepo.GetList(c => c.VoyageId == voyage.VoyageId && c.InvoiceLocked == true)
                    .Select(c => new IndexDropdownViewModel
                    {
                        ContainerIndexId = c.ContainerIndexId,
                        IndexNo = c.IndexNo ?? 0
                    }).GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault());

                return PartialView("_IndexDropbox", indexes);

            }

            return null;
        }


        [HttpGet]
        public IActionResult GetIndexNotDestuffedDropdown(string voyageNo, string IGM)
        {
            var voyage = _voyageRepo.GetFirst(v => v.VoyageNo == voyageNo && v.VIRNo == IGM);

            if (voyage != null)
            {
                var indexes = _containerIndexRepo.GetList(c => c.VoyageId == voyage.VoyageId)
                    .Where(c => c.IsDestuffed == false)
                    .Select(c => new IndexDropdownViewModel
                    {
                        ContainerIndexId = c.ContainerIndexId,
                        IndexNo = c.IndexNo ?? 0
                    }).GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault());

                return PartialView("_IndexDropbox", indexes);

            }

            return null;
        }

        [HttpPost]
        public IActionResult GetContainerIndexDropdownCY(List<CYContainer> containers)
        {

            if (containers.Count > 0)
            {
                containers = containers.GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault()).ToList();
                return PartialView("_CYContainerIndexDropbox", containers);
            }

            return null;
        }

        [HttpPost]
        public IActionResult GetContainerIndexDropdownCYList(List<CYContainer> containers)
        {

            if (containers.Count > 0)
            {
                containers = containers.GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault()).ToList();
                return PartialView("_CYContainerIndexMulti_Dropdown", containers);
            }

            return null;
        }


        [HttpPost]
        public IActionResult GetAuctionContainerIndexDropdownCY(List<CYContainer> containers)
        {

            if (containers.Count > 0)
            {
                containers = containers.Where(x => x.AuctionLotNo != null).GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault()).ToList();
                return PartialView("_CYContainerIndexDropbox", containers);
            }

            return null;
        }



        [HttpPost]
        public IActionResult GetContainerDropdownCY(List<CYContainer> containers, int IndexNo)
        {

            if (containers.Count > 0)
            {
                containers = containers.Where(c => c.IndexNo == IndexNo).ToList();
                return PartialView("_CYContainerDropbox", containers);
            }

            return null;
        }


        [HttpPost]
        public IActionResult GetAuctionContainerDropdownCY(List<CYContainer> containers, int IndexNo)
        {

            if (containers.Count > 0)
            {
                containers = containers.Where(c => c.IndexNo == IndexNo && c.AuctionLotNo != null).ToList();
                return PartialView("_CYContainerDropbox", containers);
            }

            return null;
        }


        [HttpGet]
        public List<CYContainer> GetCYContainersByIGM(string IGM)
        {
            var cyContainers = _cyContaienrRepo.GetList(c => c.VIRNo == IGM);

            return cyContainers.ToList();
        }

        [HttpGet]
        public List<CYContainer> GetAuctionCYContainersByIGM(string IGM)
        {

            var cyContainers = _cyContaienrRepo.GetList(c => c.VIRNo == IGM && c.AuctionLotNo != null);

            return cyContainers.ToList();
        }


        [HttpGet]
        public List<CYContainer> GetAuctionDeliverCYContainersByIGM(string IGM)
        {
            var cyContainers = _voyageRepo.GetCYContainers(IGM).ToList();
            //   var cyContainers = _cyContaienrRepo.GetList(c => c.VIRNo == IGM && c.AuctionLotNo != null) ;

            return cyContainers.ToList();
        }


        [HttpGet]
        public JsonResult GetShippingAgentCharges(long ShippingAgentId)
        {
            var charges = _shippingAgentRepo.Find(ShippingAgentId);

            return Json(charges);
        }


        [HttpGet]
        public JsonResult GetShippingAgentByContainerIndex(long ContainerIndexId)
        {
            var index = _containerIndexRepo.GetAll().Where(x => x.ContainerIndexId == ContainerIndexId).FirstOrDefault();
            if (index != null)
            {

                var agent = _shippingAgentRepo.Find(index.ShippingAgentId);

                return Json(agent);
            }

            return Json("");
        }

        [HttpGet]
        public JsonResult GetShippingAgentByCYConatinerId(long CYContainerId)
        {
            var container = _cyContaienrRepo.GetAll().Where(x => x.CYContainerId == CYContainerId).FirstOrDefault();
            if (container != null)
            {

                var agent = _shippingAgentRepo.Find(container.ShippingAgentId);

                return Json(agent);
            }

            return Json("");
        }

        [HttpGet]
        public string GetDocumentCodeCFS(long ContainerIndexId)
        {
            var code = _containerRepo.GetDocumentCodeCFS(ContainerIndexId);

            return code;
        }

        [HttpGet]
        public string GetDocumentCodeCY(long CyContainerId)
        {
            var code = _containerRepo.GetDocumentCodeCFS(CyContainerId);

            return code;
        }

        [HttpGet]
        public IActionResult GetContainerIndexByContainerId(long containerId)
        {
            var containerindex = _containerIndexRepo.GetList(x => x.ContainerIndexId == containerId);
            if (containerindex != null)
            {
                return PartialView("_ContainerIndexDropbox", containerindex);
            }

            return null;
        }

        [HttpGet]
        public IActionResult GetCFSContainerSize(long ContainerIndexId)
        {
            //var index = _containerIndexRepo.GetFirst(s => s.ContainerIndexId == ContainerIndexId);
            var index = _containerIndexRepo.GetContainerIndexById(ContainerIndexId);

            //var containers = _containerIndexRepo.GetList(s => s.IndexNo == index.IndexNo && s.VoyageId == index.VoyageId);

            var containers = _containerIndexRepo.GetIndexInfo(index.VirNo, index.IndexNo ?? 0);

            return Json(containers);
        }

        [HttpGet]
        public JsonResult GetContainerIndexBRTLocation(long ContainerIndexId)
        {
            var brt = _brtRepo.GetList(c => c.ContainerIndexId == ContainerIndexId, x => x.BrtItems).LastOrDefault();

            if (brt == null)
                return Json("");

            return Json(brt);
        }
        [HttpGet]
        public JsonResult GetDestuffContainerIndexInfo(long ContainerIndexId)
        {
            var result = _destuffedRepo.GetList(c => c.ContainerIndexId == ContainerIndexId).LastOrDefault();

            if (result == null)
                return Json("");

            return Json(result);
        }


        [HttpGet]
        public JsonResult GetCyContainerxBRTLocation(long CyContainerId)
        {
            var brt = _brtRepo.GetFirst(c => c.CyContainerId == CyContainerId);

            if (brt == null)
                return Json("");

            return Json(brt.Location);
        }


        [HttpGet]
        public JsonResult GetCyContainerxBRTLocationBYCyContainerId(long CyContainerId)
        {
            var brt = _brtRepo.GetAll().Where(c => c.CyContainerId == CyContainerId).LastOrDefault();

            if (brt == null)
                return Json("");

            return Json(brt);
        }

        [HttpGet]
        public JsonResult GetDestuffingCFSContainers(long ContainerId, string orientation)
        {
            var containers = _containerRepo.GetDestuffingCFSContainers(ContainerId, orientation).OrderByOrdinal(x => x.IndexNumber.ToString());

            return Json(containers);
        }

        [HttpGet]
        public JsonResult GetUngroundedCYContainers()
        {
            var containers = _containerRepo.GetUngroundedCYContainers();

            return Json(containers);
        }


        [HttpGet]
        public JsonResult GetUngroundedCYContainersByGoodHeadId(long goodheadId)
        {
            var containers = _containerRepo.GetUngroundedCYContainersByGoodHeadId(goodheadId);

            return Json(containers);
        }

        [HttpGet]
        public JsonResult GetUngroundedCFSContainers()
        {
            var containers = _containerRepo.GetUngroundedCFSContainers();

            return Json(containers);
        }

        [HttpGet]
        public JsonResult GetUngroundedCFSContainersByGoodHeadId(long goodheadId)
        {
            var containers = _containerRepo.GetUngroundedCFSContainersByGoodHeadId(goodheadId);

            return Json(containers);
        }

        [HttpGet]
        public JsonResult GetUnServiceCompletionContainers()
        {
            var containers = _exportContainerRepo.GetUnServiceCompletionContainers();

            return Json(containers);
        }

        [HttpGet]
        public JsonResult GetUnContainerAssociationContainers()
        {
            var containers = _exportContainerRepo.GetUnContainerAssociationContainers();

            return Json(containers);
        }


        [HttpGet]
        public JsonResult GetGateOutExportContainers(string containerNo)
        {
            var containers = _exportContainerRepo.GetGateOutExportContainers(containerNo);

            return Json(containers);
        }



        [HttpGet]
        public JsonResult changeBLNumberConsigneename(long indexnumber, string containernumber)
        {
            var BLNumberConsigneename = _deliveryOrderRepo.GetBLNumberConsigneename(indexnumber, containernumber).FirstOrDefault();

            return Json(BLNumberConsigneename);
        }

        [HttpGet]
        public JsonResult changeBLNumberConsigneenameCY(long indexnumber, string containernumber, string igm)
        {
            var BLNumberConsigneename = _deliveryOrderRepo.GetBLNumberConsigneenameCY(indexnumber, containernumber, igm).LastOrDefault();

            return Json(BLNumberConsigneename);
        }

        [HttpGet]
        public JsonResult changeBLNumberConsigneenameCFS(long Index, string blNumbr, string igm)
        {
            var BLNumberConsigneename = _deliveryOrderRepo.GetBLNumberConsigneenameCFS(Index, blNumbr, igm).LastOrDefault();

            return Json(BLNumberConsigneename);
        }



        [HttpGet]
        public JsonResult getDocumentCodeList(string res)
        {

            if (res != null)
            {
                List<string> code = res.Split(',').ToList();

                List<DocumentCodesViewModel> documentCodeList = new List<DocumentCodesViewModel>();
                foreach (var item in code)
                {
                    var result = _documentCodeRepository.GetFirst(x => x.Code == item);
                    if (result != null)
                    {
                        var data = new DocumentCodesViewModel
                        {
                            Code = result.Code,
                            DocumentCodeId = result.DocumentCodeId,
                            DocumentName = result.DocumentName,
                            Remarks = "",
                            IsChecked = false

                        };
                        documentCodeList.Add(data);
                    }


                }
                if (documentCodeList != null && documentCodeList.Count() > 0)
                {

                    return Json(documentCodeList);
                }

            }


            return Json("");
        }



        [HttpGet]

        public JsonResult AuctionNumberByContainerIndexNumber(long containerIndexId)
        {
            var data = _containerIndexRepo.GetFirst(x => x.ContainerIndexId == containerIndexId);
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }


        [HttpGet]

        public JsonResult AuctionNumberByCYContainerId(long CYContainerId)
        {
            var data = _cyContaienrRepo.GetFirst(x => x.CYContainerId == CYContainerId);
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }


        //[HttpGet]
        //public JsonResult GetHoldInfoCFSContainerId(long containerId)
        //{

        //    var data = _holdRepo.GetAll().Where(x => x.ContainerIndexId == containerId && x.IsHold == false).LastOrDefault();
        //    if (data != null)
        //    {
        //        return Json(data);
        //    }
        //    return Json("");
        //}


        //[HttpGet]
        //public JsonResult GetHoldInfoCYContainerId(long containerId)
        //{
        //    var data = _holdRepo.GetAll().Where(x => x.CYContainerId == containerId).LastOrDefault();
        //    if (data != null)
        //    {
        //        return Json(data);
        //    }
        //    return Json("");
        //}




        [HttpGet]
        public JsonResult GetAuctionInfo(string type, long containerId)
        {
            if (type == "CFS")
            {
                var data = _auctionRepo.GetFirst(x => x.ContainerIndexId == containerId);
                if (data != null)
                {
                    return Json(data);
                }

            }
            if (type == "CY")
            {
                var data = _auctionRepo.GetFirst(x => x.CYContainerId == containerId);
                if (data != null)
                {
                    return Json(data);
                }

            }


            return Json("");
        }


        [HttpGet]
        public JsonResult changeCYContainerNumber(string containerNumber)
        {
            var data = _cyContaienrRepo.GetFirst(x => x.ContainerNo == containerNumber);
            if (data != null)
            {
                return Json(data);
            }
            return null;
        }

        [HttpGet]
        public IActionResult GetBLnumberByIndexBumber(long indexnumber)

        {
            var blnumber = _iGMORepository.GetList(v => v.IndexNumber == indexnumber);
            if (blnumber != null)
            {
                return PartialView("_BLNumberDropBox", blnumber);
            }

            return null;
        }

        public IActionResult GetBLNumberByIGMAndIndex(long indexnumber, string IGM)

        {
            var blnumber = _iGMORepository.GetAll().Where(v => v.IndexNumber == indexnumber && v.VIRNumber == IGM).ToList();
            if (blnumber != null)
            {
                return PartialView("_BLNumberDropBox", blnumber);
            }

            return null;
        }

        [HttpGet]
        public IActionResult GetPackTypeByBLNumner(string blnumber, long containerIndexId, string type)
        {
            if (type == "CFS")
            {

                var Deliveredweighment = 0.0;
                var weighment = 0.0;
                var shortweight = 0.0;
                var excessweight = 0.0;

                List<IGMO> igmos = new List<IGMO>();

                var igmo = _iGMORepository.GetIGMODetailBYBLnumber(blnumber);

                var container = _containerIndexRepo.GetLastContainerIndexById(containerIndexId);


                if (container != null)
                {

                    if (container.CargoType == "LCL")
                    {
                        weighment = container.ManifestedWeight;

                        //var gatepassweighment = _gatePassWeightmentRepo.GetAll().Where(x => x.Virnumber == container.VirNo && x.IndexNumber == container.IndexNo).ToList();

                        //if(gatepassweighment.Count() > 0)
                        //{
                        //    weighment = gatepassweighment.Sum(x => x.ManifestedWeight) ?? 0;
                        //}


                        var destuffed = _destuffedRepo.GetDestuffByContainerIndexId(containerIndexId);



                        if (destuffed != null)
                        {
                            //var totalDestuffedWeight = _destuffedRepo.GetList(d => d.TellySheetId == destuffed.TellySheetId).Sum(s => s.FoundWeight);

                            igmo.BLGrossWeight = Math.Round(weighment, 2);

                            igmo.NumberofPackages = destuffed.PackageQuantity;

                            igmo.PackageType = destuffed.FoundPackageType;

                            igmos.Add(igmo);

                            return Json(igmos);
                        }
                    }

                    if (container.CargoType == "FCL")
                    {

                        var continerlist = _containerIndexRepo.GetIndexInfo(container.VirNo, container.IndexNo ?? 0);

                        var resshortExcessWeight = _containerIndexRepo.GetShortExcessWeigth(container.VirNo, container.IndexNo ?? 0, "CFS");

                        if (resshortExcessWeight != null)
                        {
                            excessweight = resshortExcessWeight.ExcesstWeight;
                            shortweight = resshortExcessWeight.ShortWeight;
                        }


                        if (continerlist.Count() > 0)
                        {

                            var containerindexids = string.Join(",", continerlist.Select(x => x.ContainerIndexId));


                            var destuffcontainer = _containerIndexRepo.GetDestuffedList(containerindexids).ToList();
                            if (destuffcontainer.Count() > 0)
                            {
                                igmo.BLGrossWeight = destuffcontainer.Sum(x => x.FoundWeight);

                                //igmo.NumberofPackages = destuffcontainer.Sum(x => x.PackageQuantity);
                                igmo.NumberofPackages = continerlist.Count();

                                igmo.PackageType = destuffcontainer.FirstOrDefault().FoundPackageType;


                                if (shortweight > 0)
                                {
                                    igmo.BLGrossWeight -= shortweight;
                                }

                                if (excessweight > 0)
                                {
                                    igmo.BLGrossWeight += excessweight;
                                }

                                igmos.Add(igmo);

                                return Json(igmos);
                            }

                        }

                    }




                }
            }
            else
            {
                List<IGMO> igmos = new List<IGMO>();
                //var balanceItems = _orderDetailRepo.deliveredCYContsiners(blnumber);
                var balanceItems = 0;
                var totalItems = _cyContaienrRepo.GetAll().Where(x => x.BLNo == blnumber).ToList().Count();

                var igmo = _iGMORepository.GetFirst(v => v.BLNumber == blnumber);

                if (igmo != null)
                {
                    igmo.NumberofPackages = totalItems -= Convert.ToInt32(balanceItems);
                    igmo.PackageType = "CONTAINER";

                    igmos.Add(igmo);

                    return Json(igmos);
                }
            }


            return null;
        }


        [HttpGet]
        public IActionResult GetPackTypeByIGMBLNumner(string blnumber, string igm)
        {

            List<IGMO> igmos = new List<IGMO>();
            //var balanceItems = _orderDetailRepo.deliveredCYContsiners(blnumber);
            var balanceItems = 0;
            var totalItems = _cyContaienrRepo.GetAll().Where(x => x.BLNo == blnumber && x.VIRNo == igm).ToList().Count();

            var igmo = _iGMORepository.GetFirst(v => v.BLNumber == blnumber && v.VIRNumber == igm);

            if (igmo != null)
            {
                igmo.NumberofPackages = totalItems -= Convert.ToInt32(balanceItems);
                igmo.PackageType = "CONTAINER";

                igmos.Add(igmo);

                return Json(igmos);
            }



            return null;
        }
        public JsonResult GetCYContainerNumbersByIgmIndex(string igm, int index)
        {

            // var res = _cyContaienrRepo.GetAll().Where(x => x.VIRNo == igm && x.IndexNo == index   && x.IsDelivered != true)
            //     .Select(v => new SelectListItem { Text = v.ContainerNo  , Value = v.ContainerNo }).ToList();

            // if (res.Count() > 0)
            // {
            //     return Json(res);
            // }
            //return Json("");

            var res = _cyContaienrRepo.GetAll().Where(x => x.VIRNo == igm && x.IndexNo == index && x.IsDelivered != true)
                .Select(v => new SelectListItem { Text = v.IsCSEmtyptyRecieved == true ? v.CSContainerNumber : v.ContainerNo, Value = v.IsCSEmtyptyRecieved == true ? v.CSContainerNumber : v.ContainerNo })
                .GroupBy(x => x.Text).Select(x => x.FirstOrDefault()).ToList();

            if (res.Count() > 0)
            {
                return Json(res);
            }
            return Json("");

        }

        public JsonResult GetALLCYContainerNumbersByIgmIndex(string igm, int index)
        {


            var res = _cyContaienrRepo.GetAll().Where(x => x.VIRNo == igm && x.IndexNo == index)
                .Select(v => new SelectListItem { Text = v.IsCSEmtyptyRecieved == true ? v.CSContainerNumber : v.ContainerNo, Value = v.IsCSEmtyptyRecieved == true ? v.CSContainerNumber : v.ContainerNo })
                .GroupBy(x => x.Text).Select(x => x.FirstOrDefault()).ToList();

            if (res.Count() > 0)
            {
                return Json(res);
            }
            return Json("");

        }


        public JsonResult GetcfsFcLContainerNumbersByIgmIndex(string igm, int index)
        {

            var res = _containerIndexRepo.GetIndexInfo(igm, index).Where(x => x.IsDelivered != true)
                .Select(v => new SelectListItem { Text = v.ContainerNo, Value = v.ContainerNo }).ToList();

            if (res.Count() > 0)
            {
                return Json(res);
            }
            return Json("");
        }


        public JsonResult GetALLcfsFcLContainerNumbersByIgmIndex(string igm, int index)
        {

            var res = _containerIndexRepo.GetIndexInfo(igm, index)
                .Select(v => new SelectListItem { Text = v.ContainerNo, Value = v.ContainerNo }).ToList();

            if (res.Count() > 0)
            {
                return Json(res);
            }
            return Json("");
        }




        [HttpGet]
        public JsonResult GetShippingAgentByGateInContainer(long ContainerIndexId)
        {
            var index = _containerIndexRepo.GetContainerIndexWithShippingAgent(ContainerIndexId);

            return Json(index);
        }

        [HttpGet]
        public JsonResult GetGateOutCYContainers()
        {
            var containers = _containerRepo.GetGateoutCYContainers();
            return Json(containers);
        }

        [HttpGet]
        public JsonResult GetGateOutCFSIndexes()
        {
            var containers = _containerRepo.GetGateoutCFSIndex();
            return Json(containers);
        }

        [HttpGet]
        public JsonResult GetGateInCYContainers()
        {
            var containers = _containerRepo.GetGateinCYContainers();
            return Json(containers);
        }

        [HttpGet]
        public JsonResult GetGateInCFSContainers()
        {
            var containers = _containerRepo.GetGateinCFSContainers();
            return Json(containers);
        }

        [HttpGet]
        public JsonResult GetUnlockContainers()
        {
            var containers = _containerIndexRepo.GetAll().Where(x => x.InvoiceLocked == true).ToList();
            if (containers != null)
            {
                return Json(containers);
            }

            return Json("");
        }



        [HttpGet]
        public JsonResult GetGateInTPContainers()
        {
            var constainers = _containerRepo.GetTPContainers();
            return Json(constainers);
        }

        [HttpGet]
        public List<CFSWeighmentViewModel> GetCFSWeightmentContainers()
        {
            return _weighmentRepo.GetCFSWeightmentContainers();
        }

        [HttpGet]
        public List<CFSWeighmentViewModel> GetCYWeightmentContainers()
        {
            return _weighmentRepo.GetCYWeightmentContainers();
        }

        [HttpGet]
        public JsonResult GetShippingAgents()
        {
            var agents = _shippingAgentRepo.GetAll();

            return Json(agents);
        }


        [HttpGet]
        public JsonResult GetDeliveredYards()
        {
            var dyard = _deliveredYardRepo.GetAll();

            return Json(dyard);
        }

        [HttpGet]
        public JsonResult GetAllVehicles()
        {
            var transporter = _transporterRepo.GetAll();

            return Json(transporter);
        }

        [HttpGet]
        public JsonResult GetClearingAgents()
        {
            var agents = _clearingAgentRepo.GetAll();

            return Json(agents);
        }
        [HttpGet]
        public JsonResult GetClearingAgentsImport()
        {
            var agents = _clearingAgentRepo.GetAll()
             .Select(x => new SelectListItem { Text = x.Name, Value = x.ClearingAgentId.ToString() }); ;
            return Json(agents);
        }

        [HttpGet]
        public JsonResult GetClearingAgentExport()
        {
            var agnets = _clearingAgentExportRepository.GetAll()
                .Select(x => new SelectListItem { Text = x.ClearingAgentName, Value = x.ClearingAgentExportId.ToString() }); ;

            return Json(agnets);

        }


        [HttpGet]
        public JsonResult GetCYIPAOSByDate(DateTime fromdate)
        {
            var ipaos = _ipaosRepo.GetIPAOSByDateCY(fromdate);
            return Json(ipaos);
        }


        [HttpGet]
        public JsonResult GetCFSIPAOSByDate(DateTime fromdate)
        {
            var ipaos = _ipaosRepo.GetIPAOSByDateCFS(fromdate);
            return Json(ipaos);
        }

        [HttpGet]
        public JsonResult GetAllDeliveryOrders()
        {
            var deliveryOrders = _deliveryOrderRepo.GetAll();
            return Json(deliveryOrders);
        }

        [HttpGet]
        public JsonResult GetDeliveryOrderByDONumber(long doNumber)
        {
            var deliveryOrder = _containerRepo.GetDeliveryOrderByDONumber(doNumber);
            return Json(deliveryOrder);
        }


        [HttpGet]
        public JsonResult GetDeliveryOrderBydeliveryOrderNumberForCyContainer(long doNumber)
        {
            var deliveryOrder = _containerRepo.GetDeliveryOrderByDONumberForCyConatiner(doNumber);
            return Json(deliveryOrder);
        }

        [HttpGet]
        public JsonResult GetTariffs()
        {
            var tariffs = _tariffRepo.GetAll(t => t.TariffHead);

            return Json(tariffs);
        }


        [HttpGet]
        public JsonResult GetAllTariffs()
        {
            var tariffs = _tariffRepo.GetAllTariffs();

            return Json(tariffs);
        }

        [HttpGet]
        public JsonResult GetAllTariffHead()
        {
            var tariffHead = _tarifHeadRepo.GetAll();

            return Json(tariffHead);
        }


        [HttpGet]
        public JsonResult GetShippingAgentTariffs(long ShippingAgentId)
        {
            var tariffs = _agentTariffRepo.GetTariffsByShippingAgentId(ShippingAgentId);

            return Json(tariffs);
        }

        [HttpGet]
        public JsonResult GetShippingAgentExportTariffs(long ShippingAgentExportId)
        {
            var tariffs = _agentTariffExportRepo.GetTariffsByShippingAgentId(ShippingAgentExportId);

            return Json(tariffs);
        }

        [HttpGet]
        public JsonResult GetUnassignedShippingAgentTariffs(long ShippingAgentId)
        {
            var tariffs = _agentTariffRepo.GetUnassignedShippingAgentTAriffs(ShippingAgentId);

            return Json(tariffs);
        }


        [HttpGet]
        public JsonResult GetUnassignedExportShippingAgentTariffs(long ShippingAgentExportId)
        {
            var tariffs = _agentTariffExportRepo.GetUnassignedShippingAgentTAriffs(ShippingAgentExportId);

            return Json(tariffs);
        }

        [HttpGet]
        public JsonResult GetContainerIndexTariffs(long ContainerIndexId)
        {
            var tariffs = _indexTariffRepo.GetContainerIndexTariffs(ContainerIndexId);

            return Json(tariffs);
        }


        [HttpGet]
        public JsonResult GetContainerIndexTariffsCY(string igm, long indexNo)
        {
            var tariffs = _indexTariffRepo.GetContainerIndexTariffsCY(igm, indexNo);

            return Json(tariffs);
        }

        [HttpGet]
        public JsonResult GetUnassignedContainerIndexTariffs(long ContainerIndexId)
        {
            var tariffs = _indexTariffRepo.GetUnassignedontainerIndexTariffs(ContainerIndexId);

            return Json(tariffs);
        }


        [HttpGet]
        public JsonResult GetUnassignedExportContainerGDTariffs(long EnteringCargoId)
        {
            var tariffs = _GDTariffRepo.GetUnassignedExportContainerGDTariffs(EnteringCargoId);

            return Json(tariffs);
        }


        [HttpGet]
        public JsonResult GetUnassignedContainerIndexTariffsCY(string igm, long indexNo)
        {
            var tariffs = _indexTariffRepo.GetUnassignedContainerTariffsCY(igm, indexNo);

            return Json(tariffs);
        }

        [HttpGet]
        public JsonResult GetExportInvoice(long EnteringCargoId)
        {
            var invoice = _invoiceExportRepo.GetExportInvoice(EnteringCargoId);

            return Json(invoice);
        }

        [HttpGet]
        public JsonResult GetInvoice(long ContainerIndexId)
        {
            var resdata = new InvoiceViewModel();

            var invoice = _invoiceRepo.GetInvoice(ContainerIndexId);

            //var role = User.Claims.Where(c => c.Type == ClaimTypes.Role)
            //                      .Select(c => c.Value).ToList();

            //if(invoice.IsDelivered == true &&  role.Contains("ADMINISTRATOR") == true)
            //{
            //    invoice.IsDelivered = false;
            //}

            var resuser = GetLoginUserInfo();

            if (resuser != 0 && resuser != invoice.ShippingAgentId)
            {
                resdata.SealChargers = 0;
                resdata.SalesTax = 0;
                return Json(resdata);
            }

            var ffuserslist = GetFFUsers();

            if (ffuserslist.Count() > 0 && ffuserslist.Where(x => x.ShippingAgentId == invoice.ShippingAgentId).Count() > 0 && resuser == 0)
            {
                invoice.IsLinkedWithFF = true;
            }


            return Json(invoice);
        }

        public JsonResult GetInvoiceCY(string igm, int indexNo)
        {
            var resdata = new InvoiceViewModel();


            //var cyContainer = _cyContaienrRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == indexNo);
            var cyContainer = _cyContaienrRepo.GetContainerCYByIGMAndIndex(igm, indexNo);


            var item = _invoiceRepo.GetInvoiceCY(cyContainer != null ? cyContainer.CYContainerId : 0);


            var resuser = GetLoginUserInfo();

            if (resuser != 0 && resuser != item.ShippingAgentId)
            {
                resdata.SealChargers = 0;
                resdata.SalesTax = 0;
                return Json(resdata);
            }

            var ffuserslist = GetFFUsers();

            if (ffuserslist.Count() > 0 && ffuserslist.Where(x => x.ShippingAgentId == item.ShippingAgentId).Count() > 0 && resuser == 0)
            {
                item.IsLinkedWithFF = true;
            }

            return Json(item);
        }



        public long GetLoginUserInfo()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var appUser = _userRepo.GetFirst(e => e.IdentityUserId == userId);

            if (appUser != null && appUser.ShippingAgentId != null)
            {
                return appUser.ShippingAgentId ?? 0;
            }

            return 0;

        }

        public List<User> GetFFUsers()
        {
            var appUser = _userRepo.GetAll().Where(x => x.ShippingAgentId != null).ToList();

            return appUser;

        }


        [HttpGet]
        public JsonResult GetStorageSlabsByTariff(long TariffId)
        {
            var slabs = _storageSlabRepo.GetList(c => c.TariffId == TariffId);

            return Json(slabs);
        }

        [HttpGet]
        public JsonResult GetTariffPerBoxRate(long tariffCode)
        {
            var slabs = _tariffPerBoxRateRepository.GetAll().Where(c => c.TariffInformationId == tariffCode);

            return Json(slabs);
        }

        [HttpGet]
        public JsonResult GetstorageFreedaysDetailbytariffId(long TariffId)
        {
            var slabs = _storageFreeDayRepository.GetAll().Where(c => c.TariffId == TariffId).LastOrDefault();

            return Json(slabs);
        }

        [HttpGet]
        public JsonResult Getffsharerates(long TariffId, long tariffCode)
        {
            var slabs = _storageShareRateRepository.GetAll().Where(c => c.TariffId == TariffId && c.TariffInformationId == tariffCode).LastOrDefault();

            return Json(slabs);
        }

        [HttpGet]
        public JsonResult GetExportCBMTariffs(long EnteringCargoId, int Weight, double CBM)
        {
            var items = _invoiceExportRepo.GetExportCargoCBMTotal(EnteringCargoId, Weight, CBM);

            return Json(items);
        }

        [HttpGet]
        public JsonResult GetCBMTariffs(long ContainerIndexId, long clearingAgentId, double Weight, double CBM, string indexcargotype, DateTime gateInDate, DateTime BillDate)
        {
            var items = _invoiceRepo.GetCBMTotal(ContainerIndexId, clearingAgentId, Weight, CBM, indexcargotype, gateInDate, BillDate);

            return Json(items);
        }


        [HttpGet]
        public JsonResult GetPerIndexTariffsExport(long EnteringCargoId)
        {
            var items = _invoiceExportRepo.GetIndexTotalExport(EnteringCargoId);

            return Json(items);
        }


        [HttpGet]
        public JsonResult GetPerIndexTariffs(long ContainerIndexId)
        {
            var items = _invoiceRepo.GetIndexTotal(ContainerIndexId);

            return Json(items);
        }

        [HttpGet]
        public JsonResult GetSizeTotal(string igm, int indexNo, long clearingAgentId, DateTime gateInDate, long containerCount, string indexcargotype, DateTime BillDate)
        {
            var items = _invoiceRepo.GetSizeTotal(igm, indexNo, clearingAgentId, gateInDate, containerCount, indexcargotype, BillDate);

            return Json(items);
        }


        [HttpGet]
        public JsonResult GetContainerIndexSizeTotal(long ContainerIndexId)
        {
            var index = _containerIndexRepo.GetFirst(x => x.ContainerIndexId == ContainerIndexId, x => x.Voyage);

            var items = _invoiceRepo.GetCFSSizeTotal(Convert.ToInt64(index.IndexNo), index.Voyage.VIRNo);

            return Json(items);
        }


        [HttpPost]
        public JsonResult GetStorageTotal(long ContainerIndexId, long clearingAgentId, DateTime BillDate, DateTime gateInDate, DateTime destuffdate, double Weight, double CBM, string indexcargotype)
        {
            var total = _invoiceRepo.GetStorageTotal(ContainerIndexId, clearingAgentId, BillDate, gateInDate, destuffdate, Weight, CBM, indexcargotype);

            return Json(total);
        }


        [HttpPost]
        public JsonResult GetStorageTotalIndexWise(long ContainerIndexId, DateTime BillDate, int Weight, double CBM)
        {
            var index = _containerIndexRepo.GetFirst(x => x.ContainerIndexId == ContainerIndexId, x => x.Voyage);

            // var items = _invoiceRepo.GetStorageTotalIndexWise(Convert.ToInt64(index.IndexNo), index.Voyage.VIRNo , BillDate);
            var items = _invoiceRepo.GetStorageTotalIndexWise(index != null ? index.ContainerIndexId : 0, BillDate, Weight, CBM);

            return Json(items);
        }

        [HttpPost]
        public JsonResult GetStorageTotalExport(long EnteringCargoId, DateTime BillDate, DateTime gateInDate, int Weight, double CBM)
        {
            var total = _invoiceExportRepo.GetStorageTotalExport(EnteringCargoId, BillDate, gateInDate, Weight, CBM);

            return Json(total);
        }

        [HttpPost]
        public JsonResult GetStorageTotalCY(string igm, int indexNo, DateTime BillDate, DateTime gateInDate, long clearingAgentId, string indexcargotype)
        {

            var total = _invoiceRepo.GetStorageTotalCY(igm, indexNo, BillDate, gateInDate, clearingAgentId, indexcargotype);

            return Json(total);
        }

        [HttpGet]
        public JsonResult GetInvoiceDocument()
        {
            var doc = _invDocRepo.GetAll();
            return Json(doc);
        }

        [HttpGet]
        public JsonResult GetVoyageId(long voyageId)
        {
            var doc = _voyageRepo.GetFirst(x => x.VoyageId == voyageId);
            return Json(doc);
        }

        [HttpGet]
        public JsonResult GetVesselId(long vesselId)
        {
            var doc = _vesselRepo.GetFirst(x => x.VesselId == vesselId);
            return Json(doc);
        }

        [HttpGet]
        public JsonResult GetVesselByName(string Name)
        {
            var doc = _vesselRepo.GetFirst(x => x.VesselName == Name, v => v.Voyages);
            return Json(doc);
        }

        [HttpGet]
        public JsonResult GetExaminationIndex(string blno)
        {
            var _marked = _srcoRepo.GetFirst(c => c.BLNumber == blno);
            var _completed = _ecmoRepo.GetFirst(c => c.BLNumber == blno);

            var _examinationMarked = new ExaminationViewModel { };
            var _examinationCompleted = new ExaminationViewModel { };

            if (_marked != null)
            {
                _examinationMarked.VirNumber = _marked.VIRNumber;
                _examinationMarked.BlNumber = _marked.BLNumber;
                _examinationMarked.IndexNumber = _marked.IndexNumber;
                _examinationMarked.HandlingCode = _marked.HandlingCode;
                _examinationMarked.Date = _marked.Performed;
            }

            if (_completed != null)
            {
                _examinationCompleted.VirNumber = _completed.VIRNumber;
                _examinationCompleted.BlNumber = _completed.BLNumber;
                _examinationCompleted.IndexNumber = _completed.IndexNumber;
                _examinationCompleted.HandlingCode = _completed.HandlingCode;
                _examinationCompleted.Date = _completed.Performed;
            }

            return Json(new { examinationMarked = _examinationMarked, examinationCompleted = _examinationCompleted });

        }


        [HttpGet]
        public JsonResult GetExaminationCompletedIndex(string blno)
        {
            var _index = _ecmoRepo.GetFirst(c => c.BLNumber == blno);
            if (_index != null)
            {
                return Json(new ExaminationViewModel
                {
                    BlNumber = blno,
                    Date = _index.Performed,
                    HandlingCode = _index.HandlingCode,
                    IndexNumber = _index.IndexNumber,
                    VirNumber = _index.VIRNumber
                });
            }

            return Json(null);
        }

        [HttpGet]
        public JsonResult GetReceivedAmount(string invoicenumber)
        {
            var received = _amountReceivedRepo.AmountReceivedByInvoice(invoicenumber);

            return Json(received);
        }

        [HttpGet]
        public JsonResult GetInvoiceByBillNo(string BillNo)
        {
            var invoice = _invoiceRepo.GetGeneratedInvoice(BillNo);

            return Json(invoice);
        }

        [HttpGet]
        public JsonResult GetExportInvoiceByBillNo(string BillNo)
        {
            var invoice = _invoiceExportRepo.GetExportInvoiceByBillNo(BillNo);

            return Json(invoice);
        }

        [HttpGet]
        public JsonResult GetCYInvoiceByBillNo(string BillNo)
        {
            var invoice = _invoiceRepo.GetGeneratedInvoiceCY(BillNo);

            return Json(invoice);
        }

        [HttpGet]
        public JsonResult GetSalesTaxExport()
        {
            var tax = _taxRepo.GetAll().Where(x => x.Type == "Export").FirstOrDefault();

            if (tax.SalesTaxAmount == null)
            {
                tax.SalesTaxAmount = 0;
                return Json(tax);
            }

            return Json(tax);
        }
        [HttpGet]
        public JsonResult GetSalesTaxImport(string type, long containerId)
        {

            if (type == "CFS")
            {
                var salestaxindex = _salesTaxIndexWiseRepo.GetAll().Where(x => x.ContainerIndexId == containerId).FirstOrDefault();
                if (salestaxindex != null)
                {
                    return Json(salestaxindex);
                }
                else
                {
                    var tax = _taxRepo.GetAll().Where(x => x.Type == "Import").FirstOrDefault();

                    if (tax.SalesTaxAmount == null)
                    {
                        tax.SalesTaxAmount = 0;
                        return Json(tax);
                    }

                    return Json(tax);

                }
            }

            if (type == "CY")
            {
                var salestaxindex = _salesTaxIndexWiseRepo.GetAll().Where(x => x.CYContainerId == containerId).FirstOrDefault();
                if (salestaxindex != null)
                {
                    return Json(salestaxindex);
                }
                else
                {
                    var tax = _taxRepo.GetAll().Where(x => x.Type == "Import").FirstOrDefault();

                    if (tax.SalesTaxAmount == null)
                    {
                        tax.SalesTaxAmount = 0;
                        return Json(tax);
                    }

                    return Json(tax);

                }
            }

            return Json(0);

        }

        [HttpGet]
        public JsonResult GetSalesTax()
        {
            var tax = _taxRepo.GetFirst();
            return Json(tax);
        }

        [HttpGet]
        public JsonResult GetUnAssignSCMO(string containerNumer)
        {
            var ccmo = _scmoRepo.GetUnAssignSCMO(containerNumer);
            return Json(ccmo);
        }

        [HttpGet]
        public JsonResult GetCCMOForTTSO()
        {
            var ccmo = _ttscRepo.GetCCMO();
            return Json(ccmo);
        }

        [HttpGet]
        public JsonResult GetShippingAgentBycontainerId(long containerId)
        {
            var agent = _tariffRepo.getShippingAgentByContainerId(containerId);
            return Json(agent);
        }

        [HttpGet]
        public JsonResult GetShippingAgentByCYContainerId(long CyContainerId)
        {
            var agent = _tariffRepo.getShippingAgentByCyContainerId(CyContainerId);
            return Json(agent);
        }

        [HttpGet]
        public JsonResult GetShippingAgentByIgm(string igm)
        {
            var agent = _tariffRepo.getShippingAgentByIgm(igm);

            return Json(agent);
        }

        public JsonResult GetShippingAgentByGdNumber(string gdnumber)
        {
            var enteringCargo = _enteringCargoRepo.GetFirst(c => c.GDNumber.Trim().ToUpper() == gdnumber.Trim().ToUpper());
            if (enteringCargo != null)
            {
                var loadingProgram = _loadingProgramRepo.GetFirst(l => l.LoadingProgramNumber == enteringCargo.LoadingProgramNumber);
                if (loadingProgram != null)
                {
                    return Json(new { ShippingAgentExportId = loadingProgram.ShippingAgentExportId });
                }
            }

            return Json("");
        }



        [HttpGet]
        public JsonResult GetUnAssignPGOO()
        {
            var ccmo = _pgooRepo.GetUnAssignPGOO();
            return Json(ccmo);
        }

        public JsonResult GetDashboardItem()
        {

            var gateInCFS = _gateinRepo.CountGateInContainerCFS();
            var groundedContainerCFS = _groundedContainerRepo.GrounedContainersCountCFS();
            var destuffedCFS = _destuffedRepo.DestuffedContainersCountCFS();
            var destuffedIndexCFS = _destuffedRepo.DestuffedContainersIndexCountCFS();
            var examinationMarkCFS = _examinationMarkRepo.ECMContainersCountCFS();
            var HoldCFS = _holdRepo.HoldContainersCountCFS();
            var GateOutCFS = _gateOutRepo.GateOutContainersCountCFS();
            var UpCommingContainerCFS = _gateinRepo.CountUpcommingContainerCFS();

            var gateInCY = _gateinRepo.CountGateInContainerCY();
            var groundedContainerCY = _groundedContainerRepo.GrounedContainersCountCY();
            var groundingAwaitedCFS = _groundedContainerRepo.GroundingAwaitedCFS();
            var groundingAwaitedCY = _groundedContainerRepo.GroundingAwaitedCY();
            var examinationMarkCY = _examinationMarkRepo.ECMContainersCountCY();
            var HoldCY = _holdRepo.HoldContainersCountCY();
            var GateOutCY = _gateOutRepo.GateOutContainersCountCY();
            var GateOutIndex = _gateOutRepo.GateOutContainersIndexCountCFS();
            var weighmentCY = _weighmentRepo.CountCYWeighment();
            var UpCommingContainerCY = _gateinRepo.CountUpcommingContainerCY();
            var GateInIndex = _gateinRepo.CountGateInContainerIndexCFS();

            var data = new DashboardItemViewModel
            {
                GateInContainerCFS = gateInCFS,
                GroundedCFS = groundedContainerCFS,
                DestuffedCFS = destuffedCFS,
                DestuffedIndexCFS = destuffedIndexCFS,
                ExaminationMarkCFS = examinationMarkCFS,
                OnHoldCFS = HoldCFS,
                GateOutCFS = GateOutCFS,
                UpCommingContainer = UpCommingContainerCFS,

                GateInContainerCY = gateInCY,
                GroundedCY = groundedContainerCY,
                GroundingAwaitedCFS = groundingAwaitedCFS,
                GroundingAwaitedCY = groundingAwaitedCY,
                ExaminationMarkCY = examinationMarkCY,
                OnHoldCY = HoldCY,
                GateOutCY = GateOutCY,
                WeighmentCY = weighmentCY,
                UpCommingContainerCY = UpCommingContainerCY,
                GateinIndex = GateInIndex,
                GateOutCountainerIndex = GateOutIndex


            };

            return Json(data);

        }

        public JsonResult GetDashboardItemCY()
        {

            var gateIn = _gateinRepo.CountGateInContainerCY();
            //var groundedContainer = _groundedContainerRepo.GetAll().Count();
            //var destuffed = _destuffedRepo.GetAll().Count();
            //var examinationMark = _examinationMarkRepo.GetAll().Count();
            //var onHold = _holdRepo.GetAll().Count();
            //var onHold = _holdRepo.GetAll().Count();


            return Json(gateIn);

        }

        public JsonResult GetCYContainerByContainerIndexId(long CYContainerId)
        {
            var data = _cyContaienrRepo.GetFirst(x => x.CYContainerId == CYContainerId);
            if (data != null)
            {
                return Json(data);
            }
            return null;
        }

        public JsonResult GetDeliveryOrderByContainerId(long containerId)
        {
            var data = _containerRepo.GetDeliveryOrderByContainerId(containerId);
            if (data != null)
            {
                return Json(data);
            }
            return null;
        }
        public JsonResult GetCYContainerByContainerId(long containerId)
        {
            var data = _cyContaienrRepo.GetFirst(x => x.CYContainerId == containerId);
            if (data != null)
            {
                return Json(data);
            }
            return null;
        }

        public JsonResult GetDeliveryOrderBYContainerIndexId(long ContainerIndexId)
        {
            var data = _deliveryOrderRepo.GetFirst(x => x.ContainerIndexId == ContainerIndexId);
            if (data != null)
            {
                return Json(data);
            }
            return Json(null);
        }

        public JsonResult GetDeliveryOrderBYdeliveryOrderNumber(long doNumber)
        {
            var data = _deliveryOrderRepo.GetFirst(x => x.DONumber == doNumber);
            if (data != null)
            {
                return Json(data);
            }
            return Json(null);
        }


        public JsonResult GetInvoiceByContainerIndexId(long containerIndexId)
        {
            var data = _invoiceRepo.GetFirst(x => x.ContainerIndexId == containerIndexId);

            if (data == null)
            {
                return new JsonResult(new { error = true, message = "First Generate Invoice" });
            }

            var value = _amountReceivedRepo.GetFirst(x => x.InvoiceId == data.InvoiceId);

            if (value == null)
            {
                return new JsonResult(new { error = true, message = "First Pay Amount" });
            }
            return Json(value);
        }





        [HttpGet]
        public JsonResult GetDeliveryOrderBill(long DONumber)
        {
            var details = _deliveryOrderRepo.GetDeliveryOrderBill(DONumber);

            return Json(details);
        }


        public JsonResult GetCYContainerInvoice(long CYContainerId)
        {

            var invoice = _invoiceRepo.GetFirst(x => x.CYContainerId == CYContainerId);

            return Json(invoice);

        }

        public JsonResult GetContainerInvoice(long ContainerIndexId)
        {

            var invoice = _invoiceRepo.GetAll().Where(x => x.ContainerIndexId == ContainerIndexId).FirstOrDefault();

            if (invoice != null)
            {

                return Json(invoice);

            }
            return Json("");


        }

        [HttpGet]
        public JsonResult CheckGatePassByDONumber(long doNumber)
        {
            var deliveryOrder = _deliveryOrderRepo.GetFirst(x => x.DONumber == doNumber);

            if (deliveryOrder != null)
            {
                if (deliveryOrder.ContainerIndexId != null)
                {
                    var data = _containerIndexRepo.GetFirst(x => x.ContainerIndexId == deliveryOrder.ContainerIndexId && x.IsHold == true);
                    if (data != null)
                    {
                        return new JsonResult(new { error = true, message = "The Index On Hold" });
                    }
                }
                else
                {
                    var data = _cyContaienrRepo.GetFirst(x => x.CYContainerId == deliveryOrder.CYContainerId && x.IsHold == true);
                    if (data != null)
                    {
                        return new JsonResult(new { error = true, message = "The Container On Hold" });
                    }
                }
            }

            return new JsonResult(new { error = false, message = "" });

        }


        public JsonResult GetCargoInformationCFSConatiner(long containerIndexId)
        {
            var data = _containerRepo.GetCargoInformationCFSConatiner(containerIndexId);
            return Json(data);
        }


        public JsonResult GetCargoInformationImport(string igm, string container, string index, string blnumberlist)
        {

            if (igm == "" || igm == "null")
            {
                igm = null;
            }
            if (container == "" || container == "null")
            {
                container = null;
            }

            if (index == "" || index == "null")
            {
                index = null;
            }

            if (blnumberlist == "" || blnumberlist == "null")
            {
                blnumberlist = null;
            }

            var data = _containerRepo.GetCargoInformationImport(igm, container, index, blnumberlist);
            return Json(data);
        }


        public JsonResult GetCargoInformationCYConatiner(long CYContainerId)
        {
            var data = _containerRepo.GetCargoInformationCYConatiner(CYContainerId);
            return Json(data);
        }

        public JsonResult GetCargoDetailCFSConatiner(string virno, string containerno, string indexno, string desc)
        {
            var data = _containerRepo.GetCargodetailCFSConatiner(virno, containerno, indexno, desc);
            return Json(data);
        }


        public JsonResult GetCargoDetailCYConatiner(string virno, string containerno, string indexno, string desc)
        {
            var data = _containerRepo.GetCargodetailCYConatiner(virno, containerno, indexno, desc);
            return Json(data);
        }


        public JsonResult GetSection79Cycontainers()
        {
            var data = _containerRepo.Section79ContainersCY();

            return Json(data);
        }
        public JsonResult GetSection79CycontainersCFS()
        {
            var data = _containerRepo.Section79CycontainersCFS();

            return Json(data);
        }

        public JsonResult GetIndexesBYIGM(string igm)
        {
            var value = _voyageRepo.GetFirst(x => x.VIRNo == igm);
            if (value != null)
            {
                var voyageid = value.VoyageId;
                var data = _containerRepo.GetContainerIndexBYVoyageId(voyageid);

                if (data != null)
                {
                    return Json(data);
                }


            }

            return Json("");

        }

        public JsonResult GetBalanceLedgerAmountByPartyId(long partyId)
        {
            var data = _partyLedgerRepo.GetList(x => x.PartyId == partyId).LastOrDefault();
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }

        public JsonResult GetBalanceLedgerPartyId(long partyId)
        {
            var data = _partyLedgerRepo.GetAll().Where(x => x.PartyId == partyId).ToList();
            if (data != null)
            {
                var debit = 0.0;
                var credit = 0.0;
                debit = data.Sum(x => x.Debit);
                credit = data.Sum(x => x.Credit);
                var balaance = credit - debit;
                return Json(balaance);
            }
            return Json("");
        }

        public JsonResult GetExportBalanceLedgerAmountByPartyId(long partyExportId)
        {
            var data = _partyLedgerExportRepo.GetList(x => x.PartyExportId == partyExportId).LastOrDefault();
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }


        public JsonResult GetExportBalanceLedgerByPartyId(long partyExportId)
        {
            var data = _partyLedgerExportRepo.GetAll().Where(x => x.PartyExportId == partyExportId).ToList();
            if (data != null)
            {
                var debit = 0.0;
                var credit = 0.0;
                debit = data.Sum(x => x.Debit);
                credit = data.Sum(x => x.Credit);
                var balaance = credit - debit;
                return Json(balaance);
            }
            return Json("");
        }




        public async Task<JsonResult> GetUser()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            var appUser = _userRepo.GetFirst(e => e.IdentityUserId == identityUser.Id);
            return Json(appUser);
        }


        public async Task<bool> GetUserRole()
        {
            var identityUser = await _userManager.GetUserAsync(User);

            var role = await _userManager.GetRolesAsync(identityUser);

            if (role.Contains("ADMINISTRATOR"))
            {
                return true;
            }
            return false;
        }

        public JsonResult GetEmptyGateOutContainer()
        {
            var data = _emptyGateOutContainerRepo.GetEmptyGateOutContainers();
            return Json(data);
        }



        public JsonResult GetShippingLines()
        {
            var data = _shippingLineRepo.GetAll();
            return Json(data);
        }

        public JsonResult GetContainerSizeCFS(string containernumber)
        {
            var data = _containerIndexRepo.GetFirst(x => x.ContainerNo == containernumber);
            if (data != null)
            {
                return Json(data.Size);
            }
            return Json("");

        }

        public JsonResult GetContainerSizeCY(string containernumber)
        {
            var data = _cyContaienrRepo.GetFirst(x => x.ContainerNo == containernumber);
            if (data != null)
            {
                return Json(data.Size);
            }
            return Json("");

        }


        [HttpGet]
        public object GetGDNumbers(DataSourceLoadOptions loadOptions)
        {
            var gdnumbers = _oPIARepo.GetAll().Select(v => v.GDNumber);

            return DataSourceLoader.Load(gdnumbers, loadOptions);
        }

        [HttpGet]
        public object GetTPContainers(DataSourceLoadOptions loadOptions)
        {
            var containerNumbers = _containerRepo.GetccmoContainers().Select(v => v.ContainerNo).Distinct(); ;
            //var gdnumbers =  _ccmoRepo.GetAll().Select(v => v.ContainerNo).Distinct();

            return DataSourceLoader.Load(containerNumbers, loadOptions);
        }

        [HttpGet]
        public object GetExprtContainers(DataSourceLoadOptions loadOptions)
        {
            var containers = _exportContainerRepo.GetList(s => s.IsGateout == false).Select(v => v.ContainerNumber);
            return DataSourceLoader.Load(containers, loadOptions);
        }

        [HttpGet]
        public object NotCreatedDOGDs(DataSourceLoadOptions loadOptions)
        {
            var gdnumbers = _exportDeliveryOrderRepo.GetGDsWithoutDOs();

            return DataSourceLoader.Load(gdnumbers, loadOptions);
        }

        [HttpGet]
        public JsonResult GetDeliveryOrderDisplayInfo(string gdnumber)
        {
            var resp = _exportDeliveryOrderRepo.GetDeliveryOrderDisplayInfo(gdnumber);
            return Json(resp);
        }

        [HttpGet]
        public object GetContainers(DataSourceLoadOptions loadOptions)
        {
            var containers = _exportContainerRepo.GetAll().Select(v => v.ContainerNumber);

            return DataSourceLoader.Load(containers, loadOptions);
        }

        [HttpGet]
        public JsonResult GetOPIABYGDNumber(string gdnumber)
        {
            var gdnumbers = _oPIARepo.GetFirst(x => x.GDNumber.Trim().ToUpper() == gdnumber.Trim().ToUpper());

            return Json(gdnumbers);
        }

        [HttpGet]
        public JsonResult GetLoadingProgramDetailByLPNumber(string lpNumber)
        {
            var LoadingProgram = _loadingProgramRepo.GetFirst(x => x.LoadingProgramNumber == lpNumber);

            if (LoadingProgram != null)
            {
                var LoadingProgramDetail = _loadingProgramDetailRepo.GetList(x => x.LoadingProgramId == LoadingProgram.LoadingProgramId);
                return Json(LoadingProgramDetail);
            }

            return Json("");
        }

        [HttpGet]
        public JsonResult GetCargoReceive(string lpnumber, string TruckNo)
        {

            var cargoReceived = _cargoReceivedRepo.GetCargoReceived(lpnumber, TruckNo);

            if (cargoReceived != null)
                return Json(cargoReceived);

            return null;
        }

        public JsonResult GetEnteringCargo(string LPNumber)
        {
            var enteringCargo = _enteringCargoRepo.GetFirst(x => x.LoadingProgramNumber == LPNumber);

            if (enteringCargo != null)
            {
                return Json(enteringCargo);
            }
            return Json("");
        }

        public object GetLoadingProgramNumbers(DataSourceLoadOptions loadOptions)
        {
            var loadingProgramNumbers = _loadingProgramRepo.GetAll()
                .Select(s => new { Text = $"{s.LoadingProgramNumber}-{s.ReferenceNumber}", Value = s.LoadingProgramNumber });

            return DataSourceLoader.Load(loadingProgramNumbers, loadOptions);
        }
        public object GetUnGateInLoadingProgramNumbers(DataSourceLoadOptions loadOptions)
        {
            var loadingProgramNumbers = _loadingProgramRepo.GetAll().Where(x => x.IsgateIn == false)
                .Select(s => new { Text = $"{s.LoadingProgramNumber}-{s.ReferenceNumber}", Value = s.LoadingProgramNumber });

            return DataSourceLoader.Load(loadingProgramNumbers, loadOptions);
        }


        [HttpGet]
        public JsonResult GetUngroundedCargos()
        {
            var cargos = _exportGroundedCargoRepo.GetUngroundedCargos();

            return Json(cargos);
        }

        public JsonResult GetLoadingPrograms()
        {

            var data = _loadingProgramRepo.GetAll().Select(x => x.LoadingProgramNumber);

            return Json(data);
        }
        public JsonResult GetExportCargoDetails(string gdnumber)
        {
            return Json(_enteringCargoRepo.GetCargoDetails(gdnumber));
        }

        public JsonResult GetGDInvoices(string gdnumber)
        {

            var data = _enteringCargoRepo.GetGDInvoices(gdnumber);

            if (data != null && data.Count() > 0)
            {
                return Json(data);
            }

            return Json(data);

        }


        public JsonResult FindEnteringCargo(string GDNumber, string LPNumner)
        {
            return Json(_enteringCargoRepo.GetEnteringCargo(GDNumber, LPNumner));
        }

        public JsonResult GetEnteringCargoByGD(string GDNumber)
        {
            return Json(_enteringCargoRepo.GetEnteringCargoByGD(GDNumber));
        }


        public JsonResult GetCargo(string gdnumber)
        {
            var data = _cargoRepo.GetFirst(x => x.GDNumber.Trim().ToUpper() == gdnumber.Trim().ToUpper());

            if (data != null)
            {
                return Json(data);
            }

            return Json("");
        }

        [HttpGet]
        public object GetExpotrContainers(DataSourceLoadOptions loadOptions)
        {
            var containers = _exportContainerRepo.GetAll().Select(v => v.ContainerNumber);

            return DataSourceLoader.Load(containers, loadOptions);
        }

        public JsonResult GetEnteringCarogo(string gdnumber, string lpnumber)
        {
            var data = _enteringCargoRepo.GetFirst(x => x.GDNumber.Trim().ToUpper() == gdnumber.Trim().ToUpper() && x.LoadingProgramNumber == lpnumber);

            if (data != null)
            {
                return Json(data);
            }

            return Json("");
        }

        public JsonResult GetCargoByPoNumber(string gdnumber, long loadingProgramDetailId)
        {
            var data = _cargoRepo.GetFirst(x => x.GDNumber.Trim().ToUpper() == gdnumber.Trim().ToUpper() && x.LoadingProgramDetailId == loadingProgramDetailId);
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }


        public JsonResult GetCargoInformation(string gdNumber, string lpNumber, long lpDetailId)
        {
            return Json(_cargoRepo.GetCargoInfo(gdNumber, lpNumber, lpDetailId));
        }



        public JsonResult GetExportContainerByContainerNo(string containerNo)
        {

            var containerNumber = containerNo.Replace("-", "");

            var data = _exportContainerRepo.GetAll().Where(x => x.ContainerNumber == containerNumber && x.IsGateout == false).LastOrDefault();
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }

        public JsonResult GetAgentNameAndPhoneNo(string val)
        {


            var data = _examinationRequestRepo.GetFirst(x => x.CNIC == val);
            if (data != null)
            {

                return Json(data);
            }
            return Json("");
        }


        public JsonResult GetExaminationRequestByClearingAgnetAndCNIC(string val, long clearingagentid)
        {
            var data = _examinationRequestRepo.GetExaminationRequestByClearingAgnetAndCNIC(val, clearingagentid);
            if (data != null)
            {

                return Json(data);
            }
            return Json("");

        }

        public JsonResult GetPGateInOutDetailBydCNIC(string val)
        {
            var data = _pGateInOutRepository.GetPGateInOutDetailBydCNIC(val);
            if (data != null)
            {

                return Json(data);
            }
            return Json("");

        }

        public JsonResult GetLoadingProramByLPNo(string LPNumber)
        {
            var data = _loadingProgramRepo.GetFirst(x => x.LoadingProgramNumber == LPNumber);

            if (data != null)
            {
                return Json(data);
            }

            return Json("");
        }

        public JsonResult GetLatestLPByGDNumber(string GDNumber)
        {
            var enteringCargo = _enteringCargoRepo.GetList(e => e.GDNumber.Trim().ToUpper() == GDNumber.Trim().ToUpper())
                .OrderByDescending(e => e.EnteringCargoId).FirstOrDefault();
            if (enteringCargo != null)
            {
                var lp = _loadingProgramRepo.GetFirst(l => l.LoadingProgramNumber == enteringCargo.LoadingProgramNumber);

                return Json(lp);
            }

            return Json("");
        }

        public JsonResult GetLoadingProgramBYGDnumber(string gdnumber)
        {
            var data = _enteringCargoRepo.GetList(x => x.GDNumber.Trim().ToUpper() == gdnumber.Trim().ToUpper());
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }
        public JsonResult GetLoadingProgramBYLPnumber(string lpnumber)
        {
            var data = _loadingProgramRepo.GetFirst(x => x.LoadingProgramNumber == lpnumber);

            if (data != null)
            {
                //var res = _portAndTerminalRepo.GetFirst(x => x.PortAndTerminalId == data.PortAndTerminalId);
                var res = _portAndTerminalRepo.GetAll();


                var genericResult = new { data = data, res = res };

                return Json(genericResult);
            }
            return Json("");
        }

        public JsonResult GetVoyagesFormvesselExportId(long vesselExportId)
        {
            var data = _voyageExportRepo.GetAll().Where(x => x.VesselExportId == vesselExportId);

            if (data != null)
            {
                return Json(data);

            }

            return Json("");
        }

        [HttpGet]
        public object GetUnAssignGDNumbers(DataSourceLoadOptions loadOptions)
        {
            var gdnumbers = _cargoRollOverRepo.GetUnAssignGDNumbers().Select(v => v.GDNumber);

            return DataSourceLoader.Load(gdnumbers, loadOptions);
        }

        [HttpGet]
        public object GetUnAssignGDNumbersForDO(DataSourceLoadOptions loadOptions)
        {
            var gdnumbers = _cargoRollOverRepo.GetUnAssignGDNumbersForDO().Select(v => v.GDNumber);

            return DataSourceLoader.Load(gdnumbers, loadOptions);
        }

        [HttpGet]
        public JsonResult GetReceivedAmountExport(long invoiceExportId)
        {
            var received = _amountReceivedExportRepo.AmountReceivedByInvoice(invoiceExportId);

            return Json(received);
        }


        public JsonResult GetBalanceLedgerAmountByPartyExportId(long partyId)
        {
            var data = _partyLedgerExportRepo.GetList(x => x.PartyExportId == partyId).LastOrDefault();
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }


        [HttpGet]
        public object GetchqeueNoImport(DataSourceLoadOptions loadOptions)
        {
            var gdnumbers = _chequeRecivedRepository.GetAll().Select(v => v.Number);

            return DataSourceLoader.Load(gdnumbers, loadOptions);
        }

        [HttpGet]
        public object GetchqeueNoExport(DataSourceLoadOptions loadOptions)
        {
            var gdnumbers = _chequeRecivedExportRepository.GetAll().Select(v => v.Number);

            return DataSourceLoader.Load(gdnumbers, loadOptions);
        }

        [HttpGet]
        public JsonResult GetCYContainersByIGMNumber(string IGM)
        {
            var cyContainers = _cyContaienrRepo.GetList(c => c.VIRNo == IGM)
                .Select(x => new SelectListItem { Text = x.ContainerNo, Value = x.ContainerNo }).ToList();

            return Json(cyContainers);
        }

        [HttpGet]
        public JsonResult GetCYIndexByContainerNo(string containerNo)
        {
            var indexNos = _cyContaienrRepo.GetList(c => c.ContainerNo == containerNo)
                .Select(x => new SelectListItem { Text = x.IndexNo.ToString(), Value = x.IndexNo.ToString() }).ToList();

            return Json(indexNos);
        }

        [HttpGet]
        public JsonResult GetCFSContainersByIGMNumber(string IGM)
        {
            var voyage = _voyageRepo.GetFirst(x => x.VIRNo == IGM);
            if (voyage != null)
            {
                var cfsContainers = _containerIndexRepo.GetList(c => c.VoyageId == voyage.VoyageId)
            .Select(x => new SelectListItem { Text = x.ContainerNo, Value = x.ContainerNo }).ToList();

                return Json(cfsContainers);

            }

            return Json("");

        }

        [HttpGet]
        public JsonResult GetCFSIndexByContainerNo(string containerNo)
        {
            var indexNos = _containerIndexRepo.GetList(c => c.ContainerNo == containerNo)
                .Select(x => new SelectListItem { Text = x.IndexNo.ToString(), Value = x.IndexNo.ToString() }).ToList();

            return Json(indexNos);
        }

        [HttpGet]
        public IActionResult GetCFSIndexByContainerNoForDo(string containerNo)
        {
            var indexNos = _containerIndexRepo.GetNotGeneratedDOIndexes(containerNo).ToList();
            // var indexNos = _containerIndexRepo.GetAll().Where(c => c.ContainerNo == containerNo).ToList();
            if (indexNos != null)
            {
                return PartialView("_ContainerIndexesView", indexNos);
            }

            return null;
        }

        [HttpGet]
        public JsonResult GetExportContainers()
        {
            var exportContainrs = _exportContainerRepo.GetAll()
                .Select(x => new SelectListItem { Text = x.ContainerNumber, Value = x.ExportContainerId.ToString() }).ToList();
            return Json(exportContainrs);
        }

        [HttpGet]
        public JsonResult GetExportContainerItemsByContainerId(long containerId)
        {
            var exportContainrs = _exportContainerItemRepositpory.GetList(x => x.ExportContainerId == containerId)
                .Select(x => new SelectListItem { Text = x.GDNumber, Value = x.VoyageExportId.ToString() }).ToList();
            return Json(exportContainrs);
        }

        [HttpGet]
        public JsonResult GetVIRbyVoyageId(long voyageId)
        {
            var exportContainrs = _voyageExportRepo.GetList(x => x.VoyageExportId == voyageId)
                .Select(x => new SelectListItem { Text = x.VirNumber, Value = x.VirNumber }).ToList();
            return Json(exportContainrs);
        }

        public JsonResult GetGDIO()
        {
            var gdio = _gDIORepository.GetAll()
                .Select(x => new SelectListItem { Value = x.ConsigneeName, Text = x.ConsigneeName });
            return Json(gdio);
        }

        public JsonResult GetGDI()
        {
            var gdio = _gDIRepository.GetAll()
                .Select(x => new SelectListItem { Value = x.ConsigneeName, Text = x.ConsigneeName });
            return Json(gdio);
        }

        public JsonResult GetTurckNumbersBYLPNo(string lpNumber)
        {
            var data = _exportVehicleRepository.GetTurckNumbersBYLPNo(lpNumber);

            if (data != null)
            {

                return Json(data.Select(x => new SelectListItem { Text = x.VehicleNumber, Value = x.VehicleNumber }));

            }

            return Json("");
        }

        public JsonResult GetCFSSalesTaxIndex(long containerIndexId)
        {
            var data = _salesTaxIndexWiseRepo.GetAll().Where(x => x.ContainerIndexId == containerIndexId).FirstOrDefault();

            if (data != null)
            {
                return Json(data);
            }

            return Json("");
        }

        public JsonResult GetCYSalesTaxIndex(long cycontainerid)
        {
            var data = _salesTaxIndexWiseRepo.GetAll().Where(x => x.CYContainerId == cycontainerid).FirstOrDefault();

            if (data != null)
            {
                return Json(data);
            }

            return Json("");
        }



        public object GetLoadingProgram(DataSourceLoadOptions loadOptions)
        {
            var loadingProgramNumbers = _loadingProgramRepo.GetAll().Where(x => x.IsgateIn == false)
                .Select(s => new { Text = $"{s.LoadingProgramNumber}-{s.ReferenceNumber}", Value = s.LoadingProgramId.ToString() });

            return DataSourceLoader.Load(loadingProgramNumbers, loadOptions);
        }

        public JsonResult GetDashboardItemExport()
        {

            List<DashboardItemViewModelExport> datasource = new List<DashboardItemViewModelExport>();
            var data = new DashboardItemViewModelExport();

            var TotalCargoReceived = _ogieRepo.CargoReceivedCount();

            data = new DashboardItemViewModelExport
            {
                complaint = "TotalCargoReceived",
                count = TotalCargoReceived

            };
            datasource.Add(data);


            var CargoReceivedCBM = _cargoReceivedRepo.CargoReceivedCBM();
            data = new DashboardItemViewModelExport
            {
                complaint = "CargoReceivedCBM",
                count = Convert.ToInt64(CargoReceivedCBM)

            };
            datasource.Add(data);

            var TotalContainerGateOut = _ogteRepo.ContainerGateOutCount();
            data = new DashboardItemViewModelExport
            {
                complaint = "TotalContainerGateOut",
                count = TotalContainerGateOut

            };
            datasource.Add(data);


            var EmptyContainerReceiving = _emptyReceivingRepo.EmptyReceivingCount();
            data = new DashboardItemViewModelExport
            {
                complaint = "EmptyContainerReceiving",
                count = EmptyContainerReceiving

            };
            datasource.Add(data);


            //var TotalEmptyReceiving = _emptyReceivingRepo.EmptyReceivingCount();
            //data = new DashboardItemViewModelExport
            //{
            //    complaint = "TotalEmptyReceiving",
            //    count = TotalEmptyReceiving

            //};
            //datasource.Add(data);



            var UpcomingGDsCount = _oPIARepo.UpcomingGDsCount();
            data = new DashboardItemViewModelExport
            {
                complaint = "UpcomingGDsCount",
                count = UpcomingGDsCount

            };
            datasource.Add(data);





            var WaitingforExaminationMarkedGDs = _enteringCargoRepo.WaitingforExaminationMarkedGDs();
            data = new DashboardItemViewModelExport
            {
                complaint = "WaitingforExaminationMarkedGDs",
                count = WaitingforExaminationMarkedGDs

            };
            datasource.Add(data);


            var GroundedGDs = _enteringCargoRepo.GroundedGDs();
            data = new DashboardItemViewModelExport
            {
                complaint = "GroundedGDs",
                count = GroundedGDs

            };
            datasource.Add(data);



            var ContainerReadyForGateOut = _exportContainerRepo.ContainerReadyForSuffing();
            data = new DashboardItemViewModelExport
            {
                complaint = "ContainerReadyForGateOut",
                count = ContainerReadyForGateOut

            };
            datasource.Add(data);





            var CurrentGDHold = _osimRepo.getCurrentGDHold();
            data = new DashboardItemViewModelExport
            {
                complaint = "CurrentGDHold",
                count = CurrentGDHold

            };
            datasource.Add(data);


            var CutterntGateOutContainer = _ogteRepo.ReadyForGateOutContainer();
            data = new DashboardItemViewModelExport
            {
                complaint = "CutterntGateOutContainer",
                count = CutterntGateOutContainer

            };
            datasource.Add(data);


            var TotalDrayOFFGDs = _ocrlRepo.TotalDrayOFFGDs();
            data = new DashboardItemViewModelExport
            {
                complaint = "TotalDrayOFFGDs",
                count = TotalDrayOFFGDs

            };
            datasource.Add(data);


            //var ExportContainers = _exportContainerRepo.ExportContainersCount();
            //  data = new DashboardItemViewModelExport
            //{
            //      complaint = "ExportContainers",
            //      count = ExportContainers

            //  };
            //datasource.Add(data);
            //var GDHold = _GDHoldRepo.GDHoldCount();

            //data = new DashboardItemViewModelExport
            //{
            //    complaint = "GDHold",
            //    count = GDHold

            //};
            //datasource.Add(data);
            //var GateOut = _gateoutExportRepo.GateOutExportCount();
            //data = new DashboardItemViewModelExport
            //{
            //    complaint = "GateOut",
            //    count = GateOut

            //};
            //datasource.Add(data);





            return Json(datasource);

        }

        public JsonResult GetDeliverEmptyGateOutContainer(string container)
        {
            var result = _emptyGateOutContainerRepo.GetEmptyDeliverContainerGatePass(container).ToList();

            if (result != null)
            {
                return Json(result);
            }
            return Json("");

        }


        //public JsonResult checkHoldContainerIndexes(List<CFSGroundingViewModel> containers)
        //{
        //    foreach (var item in containers)
        //    {
        //        //var holdContainer = _containerIndexRepo.GetFirst(c => c.ContainerIndexId ==  item.ContainerIndexId && c.IsHold == true);
        //        var holdContainer = _containerIndexRepo.GetContainerIndexById( item.ContainerIndexId );

        //        if (holdContainer != null && holdContainer.IsHold == true)
        //        {
        //            var _hold = _holdRepo.GetAll().Where(c => c.ContainerIndexId == holdContainer.ContainerIndexId).LastOrDefault();

        //            if (_hold != null)
        //            {
        //                return Json(new { HoldStatus = true,  SpecialInstructions =  "The Index Number "+ holdContainer.IndexNo +" is on hold & Message is " + _hold.SpecialInstructions + " & LotNo  = " +  holdContainer.AuctionLotNo  });
        //            }


        //            return Json(new { HoldStatus = true, SpecialInstructions = "The Index Number " + holdContainer.IndexNo +" is on hold " });
        //        }

        //        return Json(new { HoldStatus = false, SpecialInstructions = ""  });
        //    }


        //    return Json(new { HoldStatus = false, SpecialInstructions = "" });

        //}


        [HttpGet]
        public object Getvirno(DataSourceLoadOptions loadOptions)
        {
            var gdnumbers = _voyageRepo.GetAll().Select(v => v.VIRNo);

            return DataSourceLoader.Load(gdnumbers, loadOptions);
        }


        [HttpGet]
        public object GetBlNumberList(DataSourceLoadOptions loadOptions)
        {
            var gdnumbers = _iGMORepository.GetAll().Select(v => v.BLNumber);

            return DataSourceLoader.Load(gdnumbers, loadOptions);
        }

        [HttpGet]
        public object Getigmocontainers(DataSourceLoadOptions loadOptions)
        {
            var gdnumbers = _iGMORepository.GetAll().Select(v => v.ContainerNumber).Distinct();

            return DataSourceLoader.Load(gdnumbers, loadOptions);
        }


        [HttpGet]
        public object Getigmoindexes(DataSourceLoadOptions loadOptions)
        {
            var gdnumbers = _iGMORepository.GetAll().Select(v => v.IndexNumber).Distinct();

            return DataSourceLoader.Load(gdnumbers, loadOptions);
        }

        [HttpGet]
        public object GetBLNumberDetail(DataSourceLoadOptions loadOptions)
        {
            var gdnumbers = _iGMORepository.GetAll().Select(v => v.BLNumber).Distinct();

            return DataSourceLoader.Load(gdnumbers, loadOptions);
        }

        [HttpGet]
        public object GetPerAlertNumbers(DataSourceLoadOptions loadOptions)
        {
            var containerNumbers = _preAlertRepo.GetPreAlertNumber().Select(v => v.PreAlertNumber).Distinct(); ;

            return DataSourceLoader.Load(containerNumbers, loadOptions);
        }

        [HttpGet]
        public JsonResult GetPaymentDetails(string prealertNo)
        {
            var ccmo = _paymentUpdateRepo.GetUnAssignPaymentUpdate(prealertNo);
            return Json(ccmo);
        }


        [HttpGet]
        public JsonResult GetMuliplePaymentDetails(string prealertNo)
        {
            var res = _paymentUpdateRepo.GetUnAssignPaymentUpdateMultiple(prealertNo);
            return Json(res);
        }


        //[HttpGet]
        //public object GetPreAlertVessel(DataSourceLoadOptions loadOptions)
        //{
        //    var igms = _preAlertRepo.GetAll().Select(v => v.VesselName);

        //    return DataSourceLoader.Load(igms, loadOptions);
        //}

        public JsonResult GetVoyagesFormvesselId(long vesselId)
        {
            var data = _voyageRepo.GetAll().Where(x => x.VesselId == vesselId);

            if (data != null)
            {
                return Json(data);

            }

            return Json("");
        }



        [HttpGet]
        public JsonResult GetIGMDetailContainers(string igm, string number, string type)
        {
            if (type == "CFSContainerWise")
            {
                var containerWise = _containerRepo.GetIGMDetailContainerWise(igm, number);
                return Json(containerWise);
            }
            else if (type == "CFSIndexWise")
            {

                var indexWise = _containerRepo.GetIGMDetailIndexWise(igm, number);
                return Json(indexWise);
            }
            else
            {
                var cycontainers = _containerRepo.GetIGMDetailCYContainerIndexWise(igm, number);
                return Json(cycontainers);

            }

        }


        [HttpGet]
        public JsonResult GetCFSorCYContainerDetailForImportDropOfContainer(string igm, string containerNumber)
        {

            var cfscontainer = _containerRepo.GetLastContainerIndexByIGMAndContainerNo(igm, containerNumber);

            if (cfscontainer != null)
            {
                return Json(cfscontainer);

            }

            var cycontainer = _containerRepo.GetLastContainerCYByIGMAndContainerNo(igm, containerNumber);

            if (cycontainer != null)
            {
                return Json(cycontainer);

            }

            return Json("");

        }

        [HttpGet]
        public JsonResult GetCFSorCYContainerDetail(string igm, string containerNumber)
        {

            var cfscontainer = _containerIndexRepo.GetLastContainerIndexByIGMAndContainerNo(igm, containerNumber);

            if (cfscontainer != null)
            {
                return Json(cfscontainer);

            }

            var cycontainer = _cyContaienrRepo.GetLastContainerIndexByIGMAndContainerNo(igm, containerNumber);

            if (cycontainer != null)
            {
                return Json(cycontainer);

            }

            return Json("");

        }



        [HttpGet]
        public JsonResult GetCargoDetailCFSAndCYConatiner(string igm, string containerNumber)
        {

            var cfscontainer = _containerRepo.GetCargodetailCFSOrCYConatiner(igm, containerNumber).ToList();


            return Json(cfscontainer);



        }



        [HttpGet]
        public JsonResult GetCargoDetailCFSUnDeliveredCargo(string type, DateTime frmodate, DateTime todate)
        {

            var cfscontainer = _containerRepo.GetCargodetailCFSUnDeliveredCargo(type, frmodate, todate).ToList();


            return Json(cfscontainer);

        }



        [HttpGet]
        public JsonResult GetCargodetailCYUnDeliveredCargo(DateTime frmodate, DateTime todate)
        {

            var cycontainer = _containerRepo.GetCargodetailCYUnDeliveredCargo(frmodate, todate).ToList();

            return Json(cycontainer);

        }



        [HttpGet]
        public object GetSealsCode(DataSourceLoadOptions loadOptions)
        {
            //var igms = _purchaseSealRepo.GetAll().Where(d=> d.Status == false).Select(v => v.SealPurchaseNo).Distinct().ToList();
            var data = _purchaseSealRepo.GetAll().Where(x => x.AssignStatus == true && x.RemaingSeal > 0).Select(v => v.SealPurchaseNo).Distinct().ToList();

            return DataSourceLoader.Load(data, loadOptions);
        }

        [HttpGet]
        public JsonResult GetTransportAssigningContainers(string virno, string blnumber)
        {
            var containers = _containerRepo.GetTransportAssigningContainers(virno, blnumber);
            return Json(containers);
        }

        public JsonResult GetTariffsList()
        {
            var tariffs = _tariffInformationRepo.GetTariffs();

            return Json(tariffs);
        }


        //[HttpGet]
        //public JsonResult GetTariffsByPerametersId(long? ShippingAgentId, long? ConsigneId, long? ClearingAgentId, long? GoodId, long? ShipperId, string ContainerType , long percentAgeShippingAgentId , long ? portAndTerminalId)
        //{
        //    var tariffs = _tariffInformationRepo.GetTariffsByPerametersId(ShippingAgentId, ConsigneId, ClearingAgentId, GoodId, ShipperId, ContainerType , percentAgeShippingAgentId, portAndTerminalId).ToList();

        //    return Json(tariffs);
        //}



        [HttpGet]
        public JsonResult GetTariffsByPerametersId(long? ShippingAgentId, long? ConsigneId, long? ClearingAgentId, long? GoodId, long? ShipperId, long? ContainerType, long? portAndTerminalId, string indexCargoType)
        {
            var tariffs = _tariffInformationRepo.GetTariffsByPerametersId(ShippingAgentId, ConsigneId, ClearingAgentId, GoodId, ShipperId, ContainerType, portAndTerminalId, indexCargoType).ToList();

            return Json(tariffs);
        }

        [HttpGet]
        public JsonResult GetTariffsByCollectionId(long? collectionid)
        {
            var tariffs = _tariffInformationRepo.GetTariffsByCollectionId(collectionid).ToList();

            return Json(tariffs);
        }

        [HttpGet]
        public JsonResult GetExaminationTariffsByPerametersId(long? ShippingAgentId, long? GoodId, string indexCargoType, string examinationType)
        {
            var tariffs = _examinationTariffInformationRepo.GetExaminationTariffsByPerametersId(ShippingAgentId, GoodId, indexCargoType, examinationType).ToList();

            return Json(tariffs);
        }





        [HttpGet]
        public JsonResult GetStorageFreeDaysByPerametersId(long? ShippingAgentId, long? ConsigneId, long? ClearingAgentId, long? GoodId, long? ShipperId, long? ContainerType, long? portAndTerminalId, string indexCargoTypeid)
        {
            var result = _tariffInformationRepo.GetStorageFreeDaysByPerametersId(ShippingAgentId, ConsigneId, ClearingAgentId, GoodId, ShipperId, ContainerType, portAndTerminalId, indexCargoTypeid);

            return Json(result);

        }


        [HttpGet]
        public JsonResult GetTariffPercentages(long? ShippingAgentId)
        {

            var tariffs = _tariffPercentageRepo.GetAll().Where(x => x.ShippingAgentId == ShippingAgentId).ToList();

            return Json(tariffs);
        }


        public JsonResult GetDropOffTicket(string igm, string containerno)
        {

            var data = _importDropOfTicketRepo.GetLastImportDropOfTicket(igm.Trim().ToUpper(), containerno.Trim().ToUpper());

            return Json(data);
        }



        [HttpGet]
        public IActionResult GetFilterForFormB(string Type)
        {
            if (Type == "CYContainers")
            {
                return PartialView("~/Areas/Import/Views/Shared/_Filter_Index_CY_All.cshtml");

            }
            if (Type == "CFSIndexWise")
            {
                return PartialView("~/Areas/Import/Views/Shared/_Filter_Index.cshtml");
            }
            else
            {
                return new JsonResult(new { error = true, message = "NOT FOUND" });
            }

        }


        [HttpGet]
        public object GetWaivers(DataSourceLoadOptions loadOptions)
        {
            var waivers = _waiverRepo.GetAll().Where(x => x.IsWaive == false).Select(i => i.WaiverNumber).Distinct();

            return DataSourceLoader.Load(waivers, loadOptions);
        }

        [HttpGet]
        public object GetUnUsedWaivers(DataSourceLoadOptions loadOptions)
        {
            var waivers = _waiverRepo.GetAll().Where(x => x.InvoiceCreated == false).Select(i => i.WaiverNumber).Distinct();

            return DataSourceLoader.Load(waivers, loadOptions);
        }


        public JsonResult GetCYContainerSizeAmount(string igm, int index)
        {
            var data = _cyContaienrRepo.GetAll().Where(x => x.VIRNo == igm && x.IndexNo == index).FirstOrDefault();

            if (data != null)
            {
                return Json(data);
            }

            return Json("");
        }

        public JsonResult GetContainerDeatilInfo(string type, string igm, long indexnumber)
        {

            if (type == "CFS")
            {
                var containerinfo = _containerRepo.GetContainerDeatilInfoCFS(igm, indexnumber);

                return Json(containerinfo);
            }
            if (type == "CY")
            {
                var containerinfo = _containerRepo.GetContainerDeatilInfoCY(igm, indexnumber);
                return Json(containerinfo);
            }

            return Json("");


        }

        public JsonResult GetcfscontainergatePasses(long containerindexId)
        {
            var res = _manualGatePassRepo.GetAll().Where(x => x.ContainerindexId == containerindexId).ToList();

            if (res.Count() > 0)
            {
                return Json(res);

            }

            return Json("");
        }


        public JsonResult GetcycontainergatePasses(string igm, long index)
        {

            var cyContainer = _cyContaienrRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == index);

            if (cyContainer != null)
            {
                var res = _manualGatePassRepo.GetAll().Where(x => x.CYContainerId == cyContainer.CYContainerId).ToList();

                if (res.Count() > 0)
                {
                    return Json(res);

                }
            }


            return Json("");
        }



        public JsonResult GetCfsContainers(string igm, long index)
        {


            var containers = _containerIndexRepo.GetAll().Where(x => x.VirNo == igm && x.IndexNo == index).ToList();

            if (containers.Count() > 0)
            {
                return Json(containers);
            }

            return Json("");

        }

        public JsonResult GetContainerList(string igm, long index)
        {

            var containers = _iGMORepository.GetAll().Where(x => x.VIRNumber == igm && x.IndexNumber == index).Select(v => new SelectListItem { Text = v.ContainerNumber, Value = v.ContainerNumber }).ToList();

            if (containers.Count() > 0)
            {
                return Json(containers);
            }

            return Json("");

        }


        public JsonResult GetContainersListbyigmindex(string igm, long index)
        {

            var containers = _containerRepo.GetContainerList(igm, index).ToList();

            if (containers.Count() > 0)
            {
                var resdata = containers.Select(v => new SelectListItem { Text = v.ContainerNumber, Value = v.ContainerNumber }).ToList();

                return Json(resdata);
            }

            return Json("");

        }

        public JsonResult GetTruckInOutList(string igm, long index, string type)
        {

            var list = _containerRepo.GetTruckInOuts(igm, index, type).ToList();

            return Json(list);

        }

        public JsonResult GetALLCFSContainers(string virno, long indexno)
        {
            var consignees = _containerIndexRepo.GetContainerIndexByIgmAndIndex(virno, indexno).Select(x => new SelectListItem { Text = x.ContainerNo, Value = x.ContainerNo.ToString() }).ToList();

            return Json(consignees);
        }

        public JsonResult GetALLCYContainers(string virno, int indexno)
        {
            var consignees = _cyContaienrRepo.GetContainerIndexByIGMAndContainerNo(virno, indexno).Select(x => new SelectListItem { Text = x.ContainerNo, Value = x.ContainerNo.ToString() }).ToList();

            return Json(consignees);
        }

        public JsonResult GetALlConsignees()
        {
            var consignees = _consigneRepo.GetAll().Select(x => new SelectListItem { Text = x.ConsigneName, Value = x.ConsigneId.ToString() }).ToList();

            return Json(consignees);
        }


        public JsonResult GetAllGoodHead()
        {
            var goodheads = _goodsHeadRepository.GetAll().Select(x => new SelectListItem { Text = x.GoodsHeadName, Value = x.GoodsHeadId.ToString() }).ToList();

            return Json(goodheads);
        }


        public JsonResult GetAllShippers()
        {
            var shippers = _shipperRepository.GetAll().Select(x => new SelectListItem { Text = x.ShipperName, Value = x.ShipperId.ToString() }).ToList();

            return Json(shippers);
        }

        public JsonResult GetAllPortAndTerminals()
        {
            var res = _portAndTerminalRepo.GetAll().Select(x => new SelectListItem { Text = x.PortName, Value = x.PortAndTerminalId.ToString() }).ToList();

            return Json(res);
        }


        public JsonResult GetAllISOCodeHeads()
        {
            var res = _isoCodeHeadRepository.GetAll().Select(x => new SelectListItem { Text = x.ISOCodeHeadDescription, Value = x.ISOCodeHeadId.ToString() }).ToList();

            return Json(res);
        }


        public JsonResult GetCargoCFSConatiner(string virno, string containerno, string indexno, long? agent, string type, DateTime? from, DateTime? to)
        {
            var data = _invoiceRepo.GetCargodetailCFSConatiner(virno, containerno, indexno, agent, type, from, to);

            return Json(data);
        }

        public JsonResult GetAICTAndLineIndexTariffRates(string virno, string containerno, string indexno, long? agent, long? goodhead, string type, DateTime? from, DateTime? to)
        {
            var data = _invoiceRepo.AICTAndLineIndexTariffRates(virno, containerno, indexno, agent, goodhead, type, from, to);
            return Json(data);
        }



        public JsonResult GetCollectionBreakup(string virno, string indexno, DateTime BillDate)
        {
            var data = _invoiceRepo.GetCollectionBreakup(virno, indexno, BillDate);
            return Json(data);
        }

        public JsonResult GetPortChargesDetail(string virno, string containerno, string indexno, string type)
        {
            var data = _portChargeRepo.GetPortChargesDetail(virno, containerno, indexno, type);
            return Json(data);
        }

        public JsonResult GetPortChargesRates(string virno, string containerno, string indexno, string type, long? agent, long? goodhead, DateTime? fromdate, DateTime? todate, string status)
        {
            var data = _portChargeRepo.GetPortChargesRates(virno, containerno, indexno, type, agent, goodhead, fromdate, todate, status);
            return Json(data);
        }


        public JsonResult GetPortChargesDetailContainerWise(string virno, string containerno, string indexno, string type)
        {
            var data = _portChargeRepo.GetPortChargesDetailContainerWise(virno, containerno, indexno, type);
            return Json(data);
        }


        [HttpGet]
        public JsonResult GetShippingAgentName()
        {
            var agents = _shippingAgentRepo.GetAll().Select(s => new SelectListItem { Text = s.Name, Value = s.ShippingAgentId.ToString() });


            return Json(agents);
        }


        public JsonResult GetGateoutCFS()
        {
            var res = _containerRepo.GetGateoutCFS().ToList();
            if (res.Count() > 0)
            {
                return Json(res);

            }
            else
            {
                return Json("");
            }
        }

        //[HttpGet]
        //public JsonResult GetShareRateByPerametersId(long? ShippingAgentId, long? ConsigneId, long? ClearingAgentId, long? GoodId, long? ShipperId, long? ContainerType, long? portAndTerminalId, string indexCargoType)
        //{
        //    var tariffs = _terminalAndFFShareRateRepo.GetShareRateByPerametersId(ShippingAgentId, ConsigneId, ClearingAgentId, GoodId, ShipperId, ContainerType, portAndTerminalId , indexCargoType);

        //    return Json(tariffs);
        //}

        [HttpGet]
        public JsonResult GetShareRateByPerametersId(long? ShippingAgentId, long? GoodId, string indexCargoType)
        {
            var tariffs = _terminalAndFFShareRateRepo.GetShareRateByPerametersId(ShippingAgentId, GoodId, indexCargoType);

            return Json(tariffs);
        }


        [HttpGet]
        public JsonResult Getcontainerdetail()
        {
            var data = _containerRepo.GetUnlockGenerateContainers().ToList();

            return Json(data);
        }


        [HttpGet]
        public IActionResult GetConsignnessByName(string cons)
        {
            var res = _consigneRepo.GetFirst(v => v.ConsigneName == cons);
            if (res != null)
            {
                return Json(res);
            }

            return Ok();
        }


        [HttpGet]
        public object GetConsignees(DataSourceLoadOptions loadOptions)
        {
            var cons = _consigneRepo.GetAll().Select(v => v.ConsigneName);

            return DataSourceLoader.Load(cons, loadOptions);
        }


        public JsonResult AutoComplete(string term)
        {
            if (!String.IsNullOrEmpty(term))
            {
                var list = _consigneRepo.GetAll().Where(c => c.ConsigneName.ToUpper().Contains(term.ToUpper())).Select(c => new { Name = c.ConsigneName, ID = c.ConsigneId })
                    .ToList();
                return Json(list);
            }
            else
            {
                var list = _consigneRepo.GetAll().Select(c => new { Name = c.ConsigneName, ID = c.ConsigneId })
                    .ToList();
                return Json(list);
            }
        }


        public JsonResult GetAuctionAmounts(double CBM, double weight, string type, string igm, long index)
        {
            var data = _invoiceRepo.GetAuctionAmount(CBM, weight, type, igm, index);

            return Json(data);
        }

        public JsonResult GetConsigneeByInvoiceNo(string invoicenumber)
        {
            var res = _amountReceivedRepo.ConsigneeByInvoice(invoicenumber);

            return Json(res);
        }

        public JsonResult GetrPartyUnSettledAmount(long partyId)
        {
            var res = _partyLedgerRepo.GetrPartyUnSettledAmount(partyId).ToList();

            if (res.Count() > 0)
            {
                return Json(res);
            }
            return Json("");

        }


        public JsonResult GetPerviousInvoicesLogCFS(long ContainerIndexId)
        {

            var res = _invoiceInquiryRepo.GetAll().Where(x => x.ContainerIndexId == ContainerIndexId).ToList();

            if (res.Count() > 0)
            {

                return Json(res);
            }

            return Json("");
        }

        public JsonResult GetPerviousInvoicesLogCY(string igm, long indexNo)
        {

            var cyContainer = _cyContaienrRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == indexNo);

            if (cyContainer != null)
            {
                var res = _invoiceInquiryRepo.GetAll().Where(x => x.CYContainerId == cyContainer.CYContainerId).ToList();

                if (res.Count() > 0)
                {
                    return Json(res);
                }
            }



            return Json("");
        }

        public JsonResult GetGateInContainersDetail(string container)
        {
            var result = _gateinRepo.GetAll().Where(x => x.ContainerNo == container).ToList();

            if (result != null)
            {
                return Json(result);
            }

            return Json("");

        }


        [HttpGet]
        public object GetDonumbers(DataSourceLoadOptions loadOptions)
        {
            var dos = _deliveryOrderRepo.GetAll().Select(i => i.DONumber);

            return DataSourceLoader.Load(dos, loadOptions);
        }


        public JsonResult GetTotaldeliveredandbalanceqty(string dono)
        {

            var result = _orderDetailRepo.GetAll().Where(x => x.DONumber == dono).LastOrDefault();

            if (result != null)
            {

                var totalqty = result.TotalPackages;
                var balanceqty = result.BalancePackages;

                return Json(new OkObjectResult(new { totalqty = totalqty, balanceqty = balanceqty }));

            }

            else
            {

                return Json(new OkObjectResult(new { totalqty = 0, balanceqty = 0 }));

            }



        }


        public JsonResult GetShippingAgentById(long shippingagentid)
        {
            var result = _shippingAgentRepo.GetAll().Where(x => x.ShippingAgentId == shippingagentid).ToList();

            return Json(result);

        }

        public JsonResult GetEmptyContainertariff(long shippingAgentId, string size, DateTime arrivedate)
        {
            var items = _invoiceRepo.GetEmptyContainertariff(shippingAgentId, size, arrivedate);

            return Json(items);
        }

        public JsonResult GetEmptyContainerdays(long shippingAgentId, DateTime arrivedate)
        {
            var items = _invoiceRepo.GetEmptyContainerdays(shippingAgentId, arrivedate);

            return Json(items);
        }


        public JsonResult Getstatusofindexcargotype(long ContainerIndexId)
        {


            var indexinfo = _containerIndexRepo.GetContainerIndexById(ContainerIndexId);

            if (indexinfo != null)
            {
                if (indexinfo.InvoiceLocked == true)
                {
                    return new JsonResult(new { error = true, message = "Invoice Locked Be Finance" });
                }

                var resdata = _aictAndLineIndexTariffRepository.GetIGMIndexInfo(indexinfo.VirNo, Convert.ToInt64(indexinfo.IndexNo));

                var acitresdata = _aictAndLineIndexTariffRepository.GetIGMIndexRates(indexinfo.VirNo, Convert.ToInt64(indexinfo.IndexNo));

                if (acitresdata == true)
                {
                    return new JsonResult(new { error = true, message = "aict line index rates are not define" });
                }


                if (resdata != null)
                {
                    return new JsonResult(new { error = false, message = "ok" });
                }
                else
                {
                    return new JsonResult(new { error = true, message = "Index Line Rate not Define" });
                }
            }

            return new JsonResult(new { error = true, message = "Index Info Not Found" });
        }


        public JsonResult GetCreditAllowInfo(string IGM, int index)
        {
            var indexes = _iGMORepository.GetIndexDetail(IGM, index);

            if (indexes != null)
            {
                if (indexes.Status == "CFS")
                {
                    var res = _containerIndexRepo.GetSingleIndexInfo(IGM, index);

                    if (res != null)
                    {
                        var data = _invoiceRepo.GetInfoForCreditAllowCFS(IGM, index);

                        if (data != null)
                        {
                            return Json(data);
                        }
                    }
                }
                if (indexes.Status == "CY")
                {

                    var res = _cyContaienrRepo.GetContainerCYByIGMAndIndex(IGM, index);

                    if (res != null)
                    {
                        var invoices = _invoiceRepo.GetInfoForCreditAllowCY(IGM, index);

                        if (invoices != null)
                        {
                            return Json(invoices);
                        }
                    }
                }
            }

            return Json("");

        }


        public IActionResult GetKnockOfInvoiceByInvoiceNo(string invoiceno, long containerindexid, string igm, int index, long ClearingAgentId)
        {
            var containerindexinvoicedata = new ContainerIndex();
            var containerindexdata = new ContainerIndex();

            var cycontainerinvoicedata = new CYContainer();
            var cycontainerdata = new CYContainer();


            var resuser = GetLoginUserInfo();
            var users = _userRepo.GetAll().Where(x => x.ShippingAgentId != null).ToList();

            var statusofnature = "";

            var invoiceres = _invoiceRepo.GetInvoiceunsetteldexcessinvoice(invoiceno);

            if (invoiceres != null)
            {
                if (invoiceres.Type == "CFS")
                {
                    var invcontainerinfo = _containerIndexRepo.GetLastContainerIndexById(invoiceres.ContainerIndexId ?? 0);

                    var containerinfo = _containerIndexRepo.GetLastContainerIndexById(containerindexid);

                    if (invcontainerinfo != null)
                    {
                        if (resuser != 0 && resuser != invcontainerinfo.ShippingAgentId)
                        {
                            return new OkObjectResult(new { error = true, message = "this invoice is not pertain with (MSK)" });
                        }
                        if (resuser == 0 && users.Where(x => x.ShippingAgentId == invcontainerinfo.ShippingAgentId).Count() > 0 && containerinfo.ShippingAgentId != invcontainerinfo.ShippingAgentId)
                        {
                            return new OkObjectResult(new { error = true, message = "this invoice is not pertain with you" });
                        }
                    }
                }
                if (invoiceres.Type == "CY")
                {
                    var invcontainerinfo = _cyContaienrRepo.GetContainerById(invoiceres.CYContainerId ?? 0);
                    var containerinfo = _cyContaienrRepo.GetContainerCYByIGMAndIndex(igm, index);

                    if (invcontainerinfo != null)
                    {
                        if (resuser != 0 && resuser != invcontainerinfo.ShippingAgentId)
                        {
                            return new OkObjectResult(new { error = true, message = "this invoice is not pertain with (MSK)" });
                        }

                        if (resuser == 0 && users.Where(x => x.ShippingAgentId == invcontainerinfo.ShippingAgentId).Count() > 0 && containerinfo.ShippingAgentId != invcontainerinfo.ShippingAgentId)
                        {
                            return new OkObjectResult(new { error = true, message = "this invoice is not pertain with you" });
                        }
                    }
                }
            }

            if (invoiceres != null)
            {
                DateTime currentDate = DateTime.Now;

                DateTime InvoiceDate = invoiceres.InvoiceDate ?? DateTime.Now;

                TimeSpan tt = currentDate - InvoiceDate;

                int NrOftDays = Convert.ToInt32(tt.TotalDays);


                if (NrOftDays <= 14)
                {
                    var paymentreceivedres = _invoiceRepo.GetPaymentReceivedByInvoiceNumber(invoiceno).ToList();

                    if (paymentreceivedres.Count() > 0)
                    {
                        foreach (var item in paymentreceivedres)
                        {
                            if (item.PaymentFor == "ClearingAgent")
                            {
                                statusofnature = "ClearingAgent";
                                break;
                            }

                            if (item.PaymentFor == "Consignee")
                            {
                                statusofnature = "Consignee";
                            }
                        }

                        if (statusofnature == "Consignee" || statusofnature == "ClearingAgent")
                        {

                            containerindexdata = _containerIndexRepo.GetLastContainerIndexById(containerindexid);

                            containerindexinvoicedata = _containerIndexRepo.GetLastContainerIndexById(invoiceres.ContainerIndexId ?? 0);

                            cycontainerdata = _cyContaienrRepo.GetContainerCYByIGMAndIndex(igm, index);
                            cycontainerinvoicedata = _cyContaienrRepo.GetContainerById(invoiceres.CYContainerId ?? 0);

                            if (containerindexdata != null && containerindexinvoicedata != null)
                            {
                                if (statusofnature == "Consignee")
                                {
                                    if (containerindexdata.ConsigneId == containerindexinvoicedata.ConsigneId)
                                    {
                                        return new OkObjectResult(new { error = false, message = invoiceres.ExcessAmount, PaymentFor = "Consignee" });
                                    }
                                    else
                                    {
                                        return new OkObjectResult(new { error = true, message = "consignee not match" });
                                    }
                                }
                                if (statusofnature == "ClearingAgent")
                                {
                                    if (invoiceres.ClearingAgentId == ClearingAgentId)
                                    {
                                        return new OkObjectResult(new { error = false, message = invoiceres.ExcessAmount, PaymentFor = "ClearingAgent" });
                                    }
                                    else
                                    {
                                        return new OkObjectResult(new { error = true, message = "agent not match" });
                                    }
                                }
                                else
                                {
                                    return new OkObjectResult(new { error = true, message = "last payment was not settle with consignee or agent" });
                                }

                            }

                            if (containerindexdata != null && cycontainerinvoicedata != null)
                            {
                                if (statusofnature == "Consignee")
                                {
                                    if (containerindexdata.ConsigneId == cycontainerinvoicedata.ConsigneId)
                                    {
                                        return new OkObjectResult(new { error = false, message = invoiceres.ExcessAmount, PaymentFor = "Consignee" });
                                    }
                                    else
                                    {
                                        return new OkObjectResult(new { error = true, message = "consignee not match" });
                                    }
                                }
                                if (statusofnature == "ClearingAgent")
                                {
                                    if (invoiceres.ClearingAgentId == ClearingAgentId)
                                    {
                                        return new OkObjectResult(new { error = false, message = invoiceres.ExcessAmount, PaymentFor = "ClearingAgent" });
                                    }
                                    else
                                    {
                                        return new OkObjectResult(new { error = true, message = "agent not match" });
                                    }
                                }
                                else
                                {
                                    return new OkObjectResult(new { error = true, message = "last payment was not settle with consignee or agent" });
                                }

                            }

                            if (cycontainerdata != null && cycontainerinvoicedata != null)
                            {
                                if (statusofnature == "Consignee")
                                {
                                    if (cycontainerdata.ConsigneId == cycontainerinvoicedata.ConsigneId)
                                    {
                                        return new OkObjectResult(new { error = false, message = invoiceres.ExcessAmount, PaymentFor = "Consignee" });
                                    }
                                    else
                                    {
                                        return new OkObjectResult(new { error = true, message = "consignee not match" });
                                    }
                                }
                                if (statusofnature == "ClearingAgent")
                                {
                                    if (invoiceres.ClearingAgentId == ClearingAgentId)
                                    {
                                        return new OkObjectResult(new { error = false, message = invoiceres.ExcessAmount, PaymentFor = "ClearingAgent" });
                                    }
                                    else
                                    {
                                        return new OkObjectResult(new { error = true, message = "agent not match" });
                                    }
                                }
                                else
                                {
                                    return new OkObjectResult(new { error = true, message = "last payment was not settle with consignee or agent" });
                                }

                            }
                            if (cycontainerdata != null && containerindexinvoicedata != null)
                            {
                                if (statusofnature == "Consignee")
                                {
                                    if (cycontainerdata.ConsigneId == containerindexinvoicedata.ConsigneId)
                                    {
                                        return new OkObjectResult(new { error = false, message = invoiceres.ExcessAmount, PaymentFor = "Consignee" });
                                    }
                                    else
                                    {
                                        return new OkObjectResult(new { error = true, message = "consignee not match" });
                                    }
                                }
                                if (statusofnature == "ClearingAgent")
                                {
                                    if (invoiceres.ClearingAgentId == ClearingAgentId)
                                    {
                                        return new OkObjectResult(new { error = false, message = invoiceres.ExcessAmount, PaymentFor = "ClearingAgent" });
                                    }
                                    else
                                    {
                                        return new OkObjectResult(new { error = true, message = "agent not match" });
                                    }
                                }
                                else
                                {
                                    return new OkObjectResult(new { error = true, message = "last payment was not settle with consignee or agent" });
                                }

                            }

                            else
                            {
                                return new OkObjectResult(new { error = true, message = "invoice index not found" });
                            }

                        }
                        else
                        {
                            return new OkObjectResult(new { error = true, message = "last payment is not define with consignee or agnet" });
                        }




                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "please add item in online payment / counter payment" });
                    }

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "excess amount is settlement days are > 15" });
                }
            }
            else
            {

                return new OkObjectResult(new { error = true, message = "invoice not avaible for settlement" });

            }

            return new OkObjectResult(new { error = true, message = " not found " });
        }

        public JsonResult GetexcessamounteByInvoiceNo(string invoiceno)
        {

            var res = _invoiceRepo.GetAll().Where(x => x.InvoiceNo == invoiceno).LastOrDefault();
            if (res != null)
            {
                //DateTime currentDate = DateTime.Now;

                //DateTime InvoiceDate = res.InvoiceDate ?? DateTime.Now;


                //TimeSpan tt = currentDate - InvoiceDate;

                //int NrOftDays = Convert.ToInt32(tt.TotalDays);

                //if (NrOftDays > 14)
                //{
                return Json(res.ExcessAmount);
                //}
                //else
                //{
                //    return Json(0);
                //}
            }
            else
            {
                return Json(0);
            }


        }


        public JsonResult GeBanks()
        {
            var res = _bankRepository.GetAll().ToList();

            return Json(res);
        }

        public JsonResult GePartyBalanceByPartyID(long partyid)
        {
            var res = _partyLedgerRepo.PartyBalanceAmount(partyid);

            return Json(res);
        }

        public JsonResult PartyInfo(long partyid, string igm, long index, string type)
        {

            var res = _partyLedgerRepo.PartyInfo(partyid, igm, index, type);

            return Json(res);
        }

        public JsonResult GetElectronicDODetail(string virno, string blno, string indexno, long shippingagnetId)
        {
            var data = _electronicDeliveryOrderRepository.GetElectronicDODetail(virno, blno, indexno, shippingagnetId);
            return Json(data);
        }

        public JsonResult GetexcessRefundDetail(string invoiceno)
        {
            var data = _excessAmountRefundSettlementRepository.GetexcessRefundDetail(invoiceno);
            return Json(data);
        }



        [HttpGet]
        public object Getedovirs(DataSourceLoadOptions loadOptions)
        {
            var gdnumbers = _electronicDeliveryOrderRepository.GetAll().Select(v => v.IGM_No);

            return DataSourceLoader.Load(gdnumbers, loadOptions);
        }


        [HttpGet]
        public object GetedoIndexes(DataSourceLoadOptions loadOptions)
        {
            var gdnumbers = _electronicDeliveryOrderRepository.GetAll().Select(v => v.Index_No).Distinct();

            return DataSourceLoader.Load(gdnumbers, loadOptions);
        }

        [HttpGet]
        public object GetEDOBlnumbers(DataSourceLoadOptions loadOptions)
        {
            var gdnumbers = _electronicDeliveryOrderRepository.GetAll().Select(v => v.BL_No).Distinct();

            return DataSourceLoader.Load(gdnumbers, loadOptions);
        }

        [HttpGet]
        public JsonResult GetUnCrossStuffingDetail(string blno)
        {
            var containers = _containerRepo.GetUnCrossStuffingDetail(blno);

            return Json(containers);
        }

        public JsonResult GetCreditAllowRefoundInfo(string IGM, int index)
        {
            var indexes = _iGMORepository.GetIndexDetail(IGM, index);

            if (indexes != null)
            {
                if (indexes.Status == "CFS")
                {
                    var res = _containerIndexRepo.GetSingleIndexInfo(IGM, index);

                    if (res != null)
                    {
                        var data = _invoiceRepo.GetInfoForCreditAllowCFS(IGM, index);

                        if (data != null)
                        {
                            return Json(data);
                        }
                    }
                }
                if (indexes.Status == "CY")
                {

                    var res = _cyContaienrRepo.GetContainerCYByIGMAndIndex(IGM, index);

                    if (res != null)
                    {
                        var invoices = _invoiceRepo.GetInfoForCreditAllowCY(IGM, index);

                        if (invoices != null)
                        {
                            return Json(invoices);
                        }
                    }
                }
            }

            return Json("");

        }



        public JsonResult CheckCFSContainerStatus(string igm, long index)
        {

            var res = _containerIndexRepo.GetIndexInfo(igm, index);

            if (res.Count() > 0)
            {
                foreach (var item in res)
                {

                    if (item.ShippingAgentId == null)
                    {
                        return new JsonResult(new { error = true, message = "no agent info found from this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo });
                    }

                    if (item.GoodsHeadId == null)
                    {
                        return new JsonResult(new { error = true, message = "no goodshead found from this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo });
                    }

                    if (item.ShipperId == null)
                    {
                        return new JsonResult(new { error = true, message = "no shipper found from this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo });
                    }

                    if (item.ConsigneId == null)
                    {
                        return new JsonResult(new { error = true, message = "no consignee found from this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo });
                    }

                    if (item.PortAndTerminalId == null)
                    {
                        return new JsonResult(new { error = true, message = "no port found from this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo });
                    }

                    if (item.Size == 0)
                    {
                        return new JsonResult(new { error = true, message = "no size found from this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo });
                    }

                    if (item.CFSContainerGateInDate == null)
                    {
                        return new JsonResult(new { error = true, message = "no gatein date found from this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo });
                    }

                    if (item.IsGateIn == false)
                    {
                        return new JsonResult(new { error = true, message = "this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo + "are not gatein" });
                    }

                    if (item.IsDestuffed == false)
                    {
                        return new JsonResult(new { error = true, message = "no destuff found from this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo });
                    }
                }
            }
            else
            {
                return new JsonResult(new { error = true, message = "no data found from this igm  " + igm + " and index " + index });
            }

            return new JsonResult(new { error = false, message = " ok " });
        }


        public JsonResult CheckCYContainerStatus(string igm, long index)
        {

            var rescy = _cyContaienrRepo.GetContainerIndexByIGMAndContainerNo(igm, Convert.ToInt32(index));

            if (rescy.Count() > 0)
            {
                foreach (var item in rescy)
                {

                    if (item.ShippingAgentId == null)
                    {
                        return new JsonResult(new { error = true, message = "no agent info found from this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo });
                    }

                    if (item.GoodsHeadId == null)
                    {
                        return new JsonResult(new { error = true, message = "no goodshead found from this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo });
                    }

                    if (item.ShipperId == null)
                    {
                        return new JsonResult(new { error = true, message = "no shipper found from this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo });
                    }

                    if (item.ConsigneId == null)
                    {
                        return new JsonResult(new { error = true, message = "no consignee found from this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo });
                    }

                    if (item.PortAndTerminalId == null)
                    {
                        return new JsonResult(new { error = true, message = "no port found from this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo });
                    }

                    if (item.Size == 0)
                    {
                        return new JsonResult(new { error = true, message = "no size found from this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo });
                    }

                    if (item.CYContainerGateInDate == null)
                    {
                        return new JsonResult(new { error = true, message = "no gatein date found from this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo });
                    }

                    if (item.IsGateIn == false)
                    {
                        return new JsonResult(new { error = true, message = "this igm  " + igm + " and index " + index + " and Container " + item.ContainerNo + "are not gatein" });
                    }

                }
            }
            else
            {
                return new JsonResult(new { error = true, message = "no data found from this igm  " + igm + " and index " + index });
            }


            return new JsonResult(new { error = false, message = " ok " });
        }



        public JsonResult GetMultipleDosByIgmIndex(string IGM, int index)
        {
            var indexes = _iGMORepository.GetIndexDetail(IGM, index);

            if (indexes != null)
            {
                if (indexes.Status == "CFS")
                {
                    var res = _containerIndexRepo.GetSingleIndexInfo(IGM, index);

                    if (res != null)
                    {
                        var resdata = _deliveryOrderRepo.GetAll().Where(x => x.ContainerIndexId == res.ContainerIndexId).ToList();

                        if (resdata.Count() > 0)
                        {
                            return Json(resdata);
                        }
                    }
                }
                if (indexes.Status == "CY")
                {

                    var res = _cyContaienrRepo.GetContainerCYByIGMAndIndex(IGM, index);

                    if (res != null)
                    {
                        var resdata = _deliveryOrderRepo.GetAll().Where(x => x.CYContainerId == res.CYContainerId).ToList();

                        if (resdata.Count() > 0)
                        {
                            return Json(resdata);
                        }
                    }
                }
            }

            return Json("");

        }

        public JsonResult GetStorageHolidays(string igm, int index)
        {
            var res = 0;
            var resdata = _storageFreeDaysForHolidayRepository.GetAll().Where(x => x.VirNumber == igm && x.IndexNumber == index).LastOrDefault();

            if (resdata != null)
            {
                return Json(resdata.Freedays);
            }

            return Json(res);
        }


        public JsonResult GetInquiryFromData(string igm, int index)
        {
            var resdata = _containerRepo.GetInquiryFromData(igm, index);

            if (resdata != null)
            {
                return Json(resdata);
            }

            return Json("");

        }

        public JsonResult GetInquiryFromDatabyblnumber(string blnumber)
        {
            var resdata = _containerRepo.GetInquiryFromDatabyblnumber(blnumber);

            if (resdata != null)
            {
                return Json(resdata);
            }

            return Json("");

        }

        [HttpGet]
        public JsonResult GetGateInGDs()
        {
            var gds = _ogieRepository.GetGateInGDs();
            return Json(gds);
        }

        public JsonResult GetGDDetailGdnumber(string res)
        {
            var resdata = _ogieRepo.GetGDDetailGdnumber(res);

            return Json(resdata);
        }

        public JsonResult GetGDDetailingo(string res)
        {
            var resdata = _ogieRepo.GetGDDetailingo(res);

            return Json(resdata);
        }
        public JsonResult GetAgentNameByCNIC(string res)
        {
            var resdata = _ogieRepo.GetAgentNameByCNIC(res);

            return Json(resdata);
        }

        public JsonResult GetCountryByPort(string portname)
        {
            var res = _unlocodeRepo.GetAll().Where(x => x.PortName == portname).LastOrDefault();

            if (res != null)
            {
                return Json(res);
            }
            else
            {
                return Json("");
            }
        }

        public JsonResult GetDriverNameByCNIC(string res)
        {
            var resdata = _ogieRepo.GetDriverNameByCNIC(res);

            return Json(resdata);
        }

        public JsonResult GetConsolidateGDDetailByGDnumber(string gdnumber)
        {
            //var res = _loadingProgramRepo.GetAll(x=> x.LoadingProgramDetails).Where(x=> x.GDNumber == gdnumber).LastOrDefault();
            var res = _loadingProgramRepo.GetLoadingProgramgdnumberInfo(gdnumber);

            if (res != null)
            {
                return Json(res);
            }
            return Json("");
        }

        public JsonResult GetlpGdNumber(string gdnumber)
        {

            var loadingProgram = _loadingProgramRepo.GetLoadingProgrambygdnumber(gdnumber);
            if (loadingProgram != null)
            {
                return Json(new { ShippingAgentExportId = loadingProgram.ShippingAgentExportId });
            }

            return Json("");
        }

        public JsonResult GetCustomerNOCBygdnumber(string gdnumber)
        {
            var res = _customerNOCRepository.GetCustomerNOCbyGDnumber(gdnumber);

            if (res != null)
            {
                return Json(res);
            }
            return Json("");
        }

        [HttpGet]
        public JsonResult GetExportGatePassDisplayInfo(string gdnumber)
        {
            var resp = _exportDeliveryOrderRepo.GetExportGatePassDisplayInfo(gdnumber);
            return Json(resp);
        }

        public JsonResult GetExportTariffsByPerametersId(long ShippingAgentId)
        {
            var res = _tariffExportRepository.GetExportTariffsByPerametersId(ShippingAgentId).ToList();

            return Json(res);
        }
        public JsonResult GetShippingAgentExportById(long shippingagentid)
        {
            var res = _shippingAgentExportRepository.GetAll().Where(x => x.ShippingAgentExportId == shippingagentid).ToList();

            return Json(res);
        }

        public JsonResult GetNatureOftariffType()
        {
            var res = _natureOfTariffRepository.GetAll().ToList();

            return Json(res);

        }



        [HttpGet]
        public JsonResult GetShippingAgentBygd(string gdnumber)
        {
            var agent = _tariffExportRepository.GetShippingAgentBygd(gdnumber);

            return Json(agent);
        }

        [HttpGet]
        public JsonResult GetTariffTypeBygd(string gdnumber)
        {
            var agent = _tariffExportRepository.GetTariffTypeBygd(gdnumber);

            return Json(agent);
        }


        public JsonResult GetGdfreeDays(string gdnumber)
        {

            var res = _enteringCargoRepo.GetEnteringCargo(gdnumber);

            if (res != null)
            {
                return Json(res);
            }
            return Json("");
        }

        public JsonResult GetPendingGdsInvoicesViewModel()
        {


            var res = _exportContainerRepo.GetPendingGdsInvoicesViewModel();

            if (res != null)
            {
                return Json(res);
            }

            return Json("");

        }

        public JsonResult GetUnSettledInvoicesViewModel()
        {


            var res = _exportContainerRepo.GetUnSettledInvoicesViewModel();

            if (res != null)
            {
                return Json(res);
            }

            return Json("");

        }

        public JsonResult GetGDsAccoicatedbutAmountNotreceivedViewModel()
        {


            var res = _exportContainerRepo.GetGDsAccoicatedbutAmountNotreceivedViewModel();

            if (res != null)
            {
                return Json(res);
            }

            return Json("");

        }

        public JsonResult GetExportAlongSideData()
        {
            var res = _exportContainerRepo.GetExportAlongSideData();

            if (res != null)
            {
                return Json(res);
            }

            return Json("");

        }

        public JsonResult GetGDInvoicesExprot(string gdnumber)
        {

            var data = _enteringCargoRepo.GetGDInvoicesExprot(gdnumber);

            if (data != null && data.Count() > 0)
            {
                return Json(data);
            }

            return Json(data);

        }

        [HttpGet]
        public JsonResult GetExportInvoiceByGdnumber(string gdnumber)
        {
            var invoice = _invoiceExportRepo.GetExportInvoiceByGdnumber(gdnumber);

            return Json(invoice);
        }

        [HttpGet]
        public JsonResult GetExportCBMTariffsForInvoice(string gdnumber, int Weight, double CBM)
        {
            var items = _invoiceExportRepo.GetExportCBMTariffsForInvoice(gdnumber, Weight, CBM);

            return Json(items);
        }

        [HttpGet]
        public JsonResult GetExportTariffList(string gdnumber, double Weight, double CBM)
        {
            var items = _invoiceExportRepo.GetExportTariffList(gdnumber, Weight, CBM);

            return Json(items);
        }

        [HttpPost]
        public JsonResult GetStorageTotalExportForInvoice(string gdnumber, DateTime BillDate, DateTime gateInDate, int Weight, double CBM)
        {
            var total = _invoiceExportRepo.GetStorageTotalExportForInvoice(gdnumber, BillDate, gateInDate, Weight, CBM);

            return Json(total);
        }

        public JsonResult GetExportAgentNameAndPhoneNo(string val)
        {
            var data = _invoiceExportRepo.GetExportAgentNameAndPhoneNo(val);
            if (data != null)
            {

                return Json(data);
            }
            return Json("");
        }

        public JsonResult GetinvoicesBygdnumber(string gdnumber)
        {
            var invoices = _invoiceExportRepo.GetinvoicesBygdnumber(gdnumber).ToList();

            if (invoices.Count() > 0)
            {
                return Json(invoices);
            }
            return Json("");
        }

        public JsonResult GetinvoicesExportByInvoiceNo(string InvoiceNo)
        {
            var invoices = _invoiceExportRepo.GetinvoicesByInvoiceno(InvoiceNo).ToList();

            if (invoices.Count() > 0)
            {
                return Json(invoices);
            }
            return Json("");
        }

        public JsonResult GetinvoicesExportBygdno(string gdno)
        {
            var invoices = _invoiceExportRepo.GetinvoicesBygdno(gdno).ToList();

            if (invoices.Count() > 0)
            {
                return Json(invoices);
            }
            return Json("");
        }

        public JsonResult GetChequeReceivedDetailsexport(long partyId, long instrumentNo)
        {

            var res = _invoiceExportRepo.GetChequeReceivedDetailsExport(partyId, instrumentNo);

            if (res.Count() > 0)
            {
                return Json(res);
            }
            return Json("");
        }

        public JsonResult GetExportPartyBalanceLedgerPartyId(long partyId)
        {
            var data = _partyLedgerExportRepo.GetLedgerByPartyId(partyId).ToList();
            if (data.Count() > 0)
            {
                var debit = 0.0;
                var credit = 0.0;
                debit = data.Sum(x => x.Debit);
                credit = data.Sum(x => x.Credit);
                var balaance = credit - debit;
                return Json(balaance);
            }
            return Json("");
        }

        [HttpGet]
        public JsonResult GetExportGatePassInfo(string gdnumber)
        {
            var resp = _exportDeliveryOrderRepo.GetExportGatePassInfo(gdnumber);
            return Json(resp);
        }

        public JsonResult TruckInIndexByDONumber(long donumber)
        {
            var list = _containerRepo.GetDOIfno(donumber).ToList();

            if (list.Count() > 0)
            {
                return Json(list);
            }

            return Json("");

        }
        public JsonResult Getigmindexdetail(long donumber)
        {
            var list = _containerRepo.Getigmindexdetail(donumber);

            return Json(list);


        }

        public JsonResult GetDODetail(string igm, long index)
        {
            var list = _containerRepo.GetDODetail(igm, index);

            if (list != null)
            {
                return Json(list);
            }

            return Json("");

        }

        [HttpGet]
        public JsonResult GetEmptyReveivingCrossStuffing()
        {
            var containers = _containerRepo.GetEmptyReveivingCrossStuffing();
            return Json(containers);
        }

        [HttpGet]
        public JsonResult GetPreGateOutCrossStuffing()
        {
            var containers = _containerRepo.GetPreGateOutCrossStuffing();
            return Json(containers);
        }



        public JsonResult GetFFAndGoodsInfo(string virno, long indexno, string type)
        {

            var BillToLines = _containerRepo.GetFFAndGoodsInfo(virno, indexno, type);

            return Json(BillToLines);

        }

        [HttpGet]
        public IActionResult GetFilterForContainerLocation()
        {
            return PartialView("~/Areas/Import/Views/Shared/_Filter_ContainerLocation.cshtml");
        }

        [HttpGet]
        public IActionResult GetVIRNumberList(string Containernumber)
        {
            var indexes = _containerIndexRepo.GetVirNoListBycontainerNumber(Containernumber)
                .Select(c => new VirNumberDetail
                {
                    VirNumber = c.VIRNumber
                }).GroupBy(x => x.VirNumber).Select(x => x.FirstOrDefault());

            return PartialView("_VirNumberListDropBox", indexes);

            return null;
        }


        public JsonResult GetcontainerLocation(string virno, string containerno)
        {
            var res = _containerRepo.GetcontainerLocation(virno, containerno);

            return Json(res);
        }

        public JsonResult getcontainergatePasses(long containerid, string containertype)
        {
            if (containertype == "CFS")
            {
                var res = _gatePassSampleRepository.GetAll().Where(x => x.ContainerIndexId == containerid).ToList();


                return Json(res);

            }
            else if (containertype == "CY")
            {

                var res = _gatePassSampleRepository.GetAll().Where(x => x.CYContainerId == containerid).ToList();

                return Json(res);

            }
            else
            {
                return Json("");
            }



        }


        public JsonResult GetUndeliveredSRCO(string virno, long indexno)
        {

            var res = _groundedContainerRepo.GetUndeliveredSRCO(virno, indexno).ToList();

            return Json(res);

        }

        public JsonResult GetUndeliveredSRC(string virno, string containerno)
        {
            var res = _groundedContainerRepo.GetUndeliveredSRC(virno, containerno).ToList();

            return Json(res);
        }

        public JsonResult GetReProcessGateIn()
        {
            var containers = _containerRepo.GetReProcessGateIn();
            return Json(containers);
        }

        public JsonResult GetPendingGds()
        {
            var consignees = _exportContainerRepo.GetPendingGds().Select(x => new SelectListItem { Text = x.GDNumber, Value = x.GDNumber }).ToList();

            return Json(consignees);
        }

        public JsonResult GetPendingGdsForAssociation()
        {
            var consignees = _exportContainerRepo.GetPendingGdsForAssociation().Select(x => new SelectListItem { Text = x.GDNumber, Value = x.GDNumber }).ToList();

            return Json(consignees);
        }

        public JsonResult GetPendingcontainers()
        {
            var containers = _exportContainerRepo.GetPendingcontainers().Select(x => new SelectListItem { Text = x.ContainerNumber, Value = x.ExportContainerId.ToString() }).ToList();

            return Json(containers);
        }
        public JsonResult GetgGdDetail(string Gdnumber)
        {
            var containers = _exportContainerRepo.GetgGdDetail(Gdnumber);

            return Json(containers);
        }


        public JsonResult GetExportContainerItemsByGdnumber(string Gdnumber)
        {
            var containers = _exportContainerRepo.GetExportContainerItemsByGdnumber(Gdnumber);

            return Json(containers);
        }


        public JsonResult GetExportContainerItemsByexportcontainerid(long exportcontainerid)
        {
            var containers = _exportContainerRepo.GetExportContainerItemsByexportcontainerid(exportcontainerid);

            return Json(containers);
        }

        public JsonResult GetExportContainerItemsByGdnumberFOrAssociation(string Gdnumber)
        {
            var containers = _exportContainerRepo.GetExportContainerItemsByGdnumberFOrAssociation(Gdnumber);

            return Json(containers);
        }

        public JsonResult GetExportContainerItemsByExportcontainerIdFOrAssociation(long exportcontainerid)
        {
            var containers = _exportContainerRepo.GetExportContainerItemsByExportcontainerIdFOrAssociation(exportcontainerid);

            return Json(containers);
        }

        public JsonResult AlongsideGds()
        {
            var containers = _exportContainerRepo.AlongsideGds();

            return Json(containers);
        }

        [HttpGet]
        public object GetinvoicesAictBillingtoLine(DataSourceLoadOptions loadOptions)
        {
            var invoices = _aictBillingRepository.GetAll().Select(i => i.AictBillingNumber);

            return DataSourceLoader.Load(invoices, loadOptions);
        }
        public JsonResult GetinvoicesAictBillingtoLineByInvoiceNo(string InvoiceNo)
        {
            var invoices = _aictBillingRepository.GetAll().Where(x => x.AictBillingNumber == InvoiceNo).ToList();

            if (invoices.Count() > 0)
            {
                return Json(invoices);
            }
            return Json("");
        }

        public JsonResult GetUnBindPreAlert(long? shippingAgent, long? line, long? portAndTerminal, string vessel, string voyage, DateTime? from, DateTime? to)
        {
            var data = _preAlertRepo.GetUnBindPreAlert(shippingAgent, line, portAndTerminal, vessel, voyage, from, to);
            return Json(data);
        }

        [HttpGet]
        public object GetPreAlertVesselName(DataSourceLoadOptions loadOptions)
        {
            var vessel = _voyageRepo.GetPreAlertVesselName().Distinct();

            return DataSourceLoader.Load(vessel, loadOptions);
        }


        public JsonResult GetPreAlertVoyage(string vesselname)
        {
            var vessel = _voyageRepo.GetPreAlertVoyage(vesselname).Distinct();

            return Json(vessel);
        }


        public JsonResult GetOnlineTracking(string number, string type)
        {
            var res = _containerRepo.GetOnlineTracking(number, type);

            return Json(res);
        }


        public JsonResult ListOfUpCommingcontainers()
        {

            var listItems = new List<ListOfUpCommingContainersViewModel>();

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string conString = Con;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("UpcomingContainer", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 360;
                    command.Parameters.AddWithValue("@type", null);
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(command);

                    sda.Fill(ds);

                    listItems = ds.Tables[0].AsEnumerable()
                        .Select(datarow => new ListOfUpCommingContainersViewModel()
                        {
                            SerialNo = datarow.Field<long>("SerialNo"),
                            VIRNumber = datarow.Field<string>("VIRNumber"),
                            ContainerNo = datarow.Field<string>("ContainerNo"),
                            BLNo = datarow.Field<string>("BLNo"),
                            BLGrossWeight = datarow.Field<double>("BLGrossWeight"),
                            IndexNo = datarow.Field<int>("IndexNo"),
                            VesselName = datarow.Field<string>("VesselName"),
                            VesselArrival = datarow.Field<DateTime?>("VesselArrival"),
                            BerthAt = datarow.Field<string>("BerthAt"),
                            Commodity = datarow.Field<string>("Commodity"),
                            ManifestedSealNumber = datarow.Field<string>("ManifestedSealNumber"),
                            ISOCodeName = datarow.Field<string>("ISOCode")

                        }).ToList();

                    con.Close();

                }
            }

            return Json(listItems);
        }

        public JsonResult GetUnAssignedCScontainers()
        {


            var res = _CSEmptyContainerReceiveRepository.GetAll().Where(x => x.IsInUse == false)
                .Select(v => new SelectListItem { Text = v.ContainerNo, Value = v.CSEmptyContainerReceiveId.ToString() })
                .GroupBy(x => x.Text).Select(x => x.FirstOrDefault()).ToList();

            if (res.Count() > 0)
            {
                return Json(res);
            }
            return Json("");

        }

        public JsonResult GetUnAssignedCScontainersList()
        {


            var res = _CSEmptyContainerReceiveRepository.GetAll().Where(x => x.IsInUse == false).ToList();

            return Json(res);

        }


        public JsonResult GetAllUser()
        {
            var res = _userManager.Users.ToList();

            return Json(res);
        }

        public JsonResult GetAllAuctionUnMark()
        {
            var res = _auctionRepo.GetAllAuctionUnMark().ToList();

            return Json(res);
        }

        public JsonResult GetAllBoundedTranspoter()
        {
            var res = _boundedTranspoterRepository.GetAll().ToList();

            return Json(res);
        }

        [HttpGet]
        public JsonResult GetGateOutExport()
        {
            var containers = _exportContainerRepo.GetGateOutExport();

            return Json(containers);
        }

    }
}

