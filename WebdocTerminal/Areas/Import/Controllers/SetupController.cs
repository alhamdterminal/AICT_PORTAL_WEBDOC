using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class SetupController : ParentController
    {
        private IConsigneRepository _consigneRepo;
        private IGoodRepository _goodRepo;
        private IIHCORepository _ihcoRepo;
        private IIHCRepository _ihcRepo;
        private ICYContainerRepository _cycontainerRepo;
        private IContainerIndexRepository _containerIndexRepo;
        private IImportDropOfTicketRepository _importDropOfTicketRepo;
        private IParkingTicketRepository _parkingTicketRepo;
        private IBidderRepository _bidderRepo;
        private ILabourWorkOrderRepository _LabourWorkOrderRepo;
        private ISealPurchaseRepository _sealPurchaseRepo;
        private ISealIssueRepository _sealIssueRepo;
        private IWorkOrderCSDRepository _workOrderCSDRepo;
        private ITransporterRepository _transporterRepo;
        private IDeliveredYardRepositoryRepository _deliveredYardRepositoryRepo;
        private IBillToLineRepository _billToLineRepository;
        private IExchangeRateRepository _exchangeRateRepo;
        private IOtherChargeRepository _otherChargeRepo;
        private IOtherChargeAssigningRepository _otherChargeAssigningRepo;
        private IInvoiceRepository _invoiceRepo;
        private IGoodsHeadRepository _goodsHeadRepo;
        private IContainerSizeAmountRepository _containerSizeAmountRepo;
        private IPreAlertVesselRepository  _preAlertVesselRepo;
        private IRandDClerkRepository _randDClerkRepo;
        private IGatePassWeightmentRepository _gatePassWeightmentRepo;
        private ITruckInOutRepository _truckInOutRepo;
        private IGatePassSampleRepository _gatePassSampleRepository;
        private IPortChargeRepository _portChargeRepo;
        private ILineSealAmountRepository _lineSealAmountRepo;
        private IGenerealSealAmountRepository _generealSealAmountRepo;
        private IExaminationChargeRepository _examinationChargeRepo;
        private IExaminationChargeAssignRepository _examinationChargeAssignRepos;
        private IPartyRepository _partyRepository;
        private IPartyLedgerRepository _partyLedgerRepo;
        private IISOCodeHeadRepository _ISOCodeHeadRepository;
        private IGroundingFreeDayRepository _groundingFreeDayRepository;
        private IShippingAgentRepository _shippingAgentRepository;
        private IBRTLocationRepository _brtLocationRepository;
        private IGroundedDaysRepository _groundedDaysRepository;
        private IOffHireLocationRepository _offHireLocationRepository;
        private IEmptyContainerTaxAndFreeDayRepository _emptyContainerTaxAndFreeDayRepository;
        private IClearingAgentRepository _clearingAgentRepository;
        private ICreditAllowedRepository _creditAllowedRepository;
        private IBankRepository _bankRepository;
        private IVIRORepository _VIRORepository;
        private IVesselRepository _vesselRepository;
        private IVoyageRepository _voyageRepository;
        private IIGMORepository _IGMORepository;
        private IIPAORepository _iPAORepository;
        private IAutoExaminationChargeRepository  _autoExaminationChargeRepository;
        private ISealPurchaseRepository _purchaseSealRepo;
        public IBoundedTranspoterRepository _boundedTranspoterRepository;
        public IPortOfLoadingRepository _portOfLoadingRepository;
        public IImportContainerLocationRepository _importContainerLocationRepository;
        private IEmailSender _emailSender;
        private IUsersEmailRepository _usersEmailRepository;
        private IShortExcessWeigthRepository _shortExcessWeigthRepository;
        private IVehicleMeasurementRepository _vehicleMeasurementRepository;
        private IContainerRepository _containerRepository;
        private IUsersRepository _userRepo;
        private IHoldPrivilegeRepository _holdPrivilegeRepository;
        private UserManager<IdentityUser> _userManager;
        private IPaymentReceivedRepository _paymentReceivedRepository;
        private ICreditAllowRefoundSettlementRepository  _creditAllowRefoundSettlementRepository;
        private IPercentForAictBillingToLineRepository _percentForAictBillingToLineRepository ;
        private IAictBillingRepository _aictBillingRepository;
        private IAictBillingItemRepository _aictBillingItemRepository;
        private ICRLORepository  _cRLORepository;
        private ICRLRepository _cRLRepository;
        private ICRSummaryHeadDetailRepository _cRSummaryHeadDetailRepository;


        public SetupController(IConsigneRepository consigneRepo,
                              IGoodRepository goodRepo,
                              IIHCORepository ihcoRepo,
                              IIHCRepository ihcRepo,
                              ICYContainerRepository cycontainerRepo,
                              IContainerIndexRepository containerIndexRepo,
                              IImportDropOfTicketRepository importDropOfTicketRepo,
                              IParkingTicketRepository parkingTicketRepo,
                              IBidderRepository bidderRepo,
                              ISealPurchaseRepository sealPurchaseRepo,
                              ISealIssueRepository sealIssueRepo,
                              ILabourWorkOrderRepository LabourWorkOrderRepo,
                              IWorkOrderCSDRepository workOrderCSDRepo,
                              ITransporterRepository transporterRepo,
                              IDeliveredYardRepositoryRepository deliveredYardRepositoryRepo,
                              IBillToLineRepository billToLineRepository,
                              IExchangeRateRepository exchangeRateRepo,
                              IOtherChargeRepository otherChargeRepo,
                              IOtherChargeAssigningRepository otherChargeAssigningRepo,
                              IInvoiceRepository invoiceRepo,
                              IGoodsHeadRepository goodsHeadRepo,
                              IContainerSizeAmountRepository containerSizeAmountRepo,
                              IPreAlertVesselRepository preAlertVesselRepo,
                              IRandDClerkRepository randDClerkRepo,
                              IGatePassWeightmentRepository gatePassWeightmentRepo,
                              ITruckInOutRepository truckInOutRepo,
                              IGatePassSampleRepository gatePassSampleRepository,
                              IPortChargeRepository portChargeRepo,
                              ILineSealAmountRepository lineSealAmountRepo,
                              IGenerealSealAmountRepository generealSealAmountRepo,
                              IExaminationChargeRepository examinationChargeRepo,
                              IExaminationChargeAssignRepository examinationChargeAssignRepos, 
                              IPartyRepository partyRepository,
                              IPartyLedgerRepository partyLedgerRepo,
                              IISOCodeHeadRepository ISOCodeHeadRepository,
                              IGroundingFreeDayRepository groundingFreeDayRepository,
                              IShippingAgentRepository shippingAgentRepository,
                              IBRTLocationRepository brtLocationRepository,
                              IGroundedDaysRepository groundedDaysRepository,
                              IOffHireLocationRepository offHireLocationRepository,
                              IEmptyContainerTaxAndFreeDayRepository emptyContainerTaxAndFreeDayRepository,
                              IClearingAgentRepository clearingAgentRepository,
                              ICreditAllowedRepository creditAllowedRepository,
                              IBankRepository bankRepository,
                              IVIRORepository VIRORepository,
                              IVesselRepository vesselRepository,
                              IVoyageRepository voyageRepository,
                              IIGMORepository IGMORepository,
                              IIPAORepository iPAORepository,
                              IAutoExaminationChargeRepository autoExaminationChargeRepository,
                              ISealPurchaseRepository purchaseSealRepo,
                              IBoundedTranspoterRepository boundedTranspoterRepository,
                              IPortOfLoadingRepository portOfLoadingRepository,
                              IImportContainerLocationRepository importContainerLocationRepository,
                              IEmailSender emailSender,
                              IUsersEmailRepository usersEmailRepository,
                              IShortExcessWeigthRepository shortExcessWeigthRepository,
                              IVehicleMeasurementRepository vehicleMeasurementRepository,
                              IContainerRepository containerRepository,
                              IUsersRepository userRepo,
                              IHoldPrivilegeRepository holdPrivilegeRepository,
                              UserManager<IdentityUser> userManager,
                              IPaymentReceivedRepository paymentReceivedRepository,
                              ICreditAllowRefoundSettlementRepository creditAllowRefoundSettlementRepository,
                              IPercentForAictBillingToLineRepository percentForAictBillingToLineRepository,
                              IAictBillingRepository aictBillingRepository,
                              IAictBillingItemRepository aictBillingItemRepository,
                              ICRLORepository cRLORepository,
                              ICRLRepository cRLRepository,
                              ICRSummaryHeadDetailRepository cRSummaryHeadDetailRepository)
        {
            _consigneRepo = consigneRepo;
            _goodRepo = goodRepo;
            _ihcoRepo = ihcoRepo;
            _ihcRepo = ihcRepo;
            _cycontainerRepo = cycontainerRepo;
            _containerIndexRepo = containerIndexRepo;
            _importDropOfTicketRepo = importDropOfTicketRepo;
            _parkingTicketRepo = parkingTicketRepo;
            _bidderRepo = bidderRepo;
            _sealPurchaseRepo = sealPurchaseRepo;
            _LabourWorkOrderRepo = LabourWorkOrderRepo;
            _workOrderCSDRepo = workOrderCSDRepo;
            _transporterRepo = transporterRepo;
            _sealIssueRepo = sealIssueRepo;
            _deliveredYardRepositoryRepo = deliveredYardRepositoryRepo;
            _billToLineRepository = billToLineRepository;
            _exchangeRateRepo = exchangeRateRepo;
            _otherChargeRepo = otherChargeRepo;
            _otherChargeAssigningRepo = otherChargeAssigningRepo;
            _invoiceRepo = invoiceRepo;
            _goodsHeadRepo = goodsHeadRepo;
            _containerSizeAmountRepo = containerSizeAmountRepo;
            _preAlertVesselRepo = preAlertVesselRepo;
            _randDClerkRepo = randDClerkRepo;
            _gatePassWeightmentRepo = gatePassWeightmentRepo;
            _truckInOutRepo = truckInOutRepo;
            _gatePassSampleRepository = gatePassSampleRepository;
            _portChargeRepo = portChargeRepo;
            _lineSealAmountRepo = lineSealAmountRepo;
            _generealSealAmountRepo = generealSealAmountRepo;
            _examinationChargeRepo = examinationChargeRepo;
            _examinationChargeAssignRepos = examinationChargeAssignRepos;
            _partyRepository = partyRepository;
            _partyLedgerRepo = partyLedgerRepo;
            _ISOCodeHeadRepository = ISOCodeHeadRepository;
            _groundingFreeDayRepository = groundingFreeDayRepository;
            _shippingAgentRepository = shippingAgentRepository;
            _brtLocationRepository = brtLocationRepository;
            _groundedDaysRepository = groundedDaysRepository;
            _offHireLocationRepository = offHireLocationRepository;
            _emptyContainerTaxAndFreeDayRepository = emptyContainerTaxAndFreeDayRepository;
            _clearingAgentRepository = clearingAgentRepository;
            _creditAllowedRepository = creditAllowedRepository;
            _bankRepository = bankRepository;
            _VIRORepository = VIRORepository;
            _vesselRepository = vesselRepository;
            _voyageRepository = voyageRepository;
            _IGMORepository = IGMORepository;
            _iPAORepository = iPAORepository;
            _autoExaminationChargeRepository = autoExaminationChargeRepository;
            _purchaseSealRepo = purchaseSealRepo;
            _boundedTranspoterRepository = boundedTranspoterRepository;
            _portOfLoadingRepository = portOfLoadingRepository;
            _importContainerLocationRepository = importContainerLocationRepository;
            _emailSender = emailSender;
            _usersEmailRepository = usersEmailRepository;
            _shortExcessWeigthRepository = shortExcessWeigthRepository;
            _vehicleMeasurementRepository = vehicleMeasurementRepository;
            _containerRepository = containerRepository;
            _userRepo = userRepo;
            _holdPrivilegeRepository = holdPrivilegeRepository;
            _userManager = userManager;
            _paymentReceivedRepository = paymentReceivedRepository;
            _creditAllowRefoundSettlementRepository = creditAllowRefoundSettlementRepository;
            _percentForAictBillingToLineRepository = percentForAictBillingToLineRepository;
            _aictBillingRepository = aictBillingRepository;
            _aictBillingItemRepository = aictBillingItemRepository;
            _cRLORepository = cRLORepository;
            _cRLRepository = cRLRepository;
            _cRSummaryHeadDetailRepository = cRSummaryHeadDetailRepository;
        }


        #region Bounded Carrier

        public IActionResult BoundedCarrierView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetBoundedCarrier()
        {
            var banks = _boundedTranspoterRepository.GetAll();
            return Json(banks);
        }

        [HttpPost]
        public IActionResult AddtBoundedCarrier(BoundedTranspoter model)
        {
            try
            {
                _boundedTranspoterRepository.Add(model);

                return new OkObjectResult(new { error = false, message = "Created" });
            }

            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }


        #endregion

        #region Consigne
        public IActionResult ConsigneView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetConsignes()
        {
            var consignes = _consigneRepo.GetAll();
            return Json(consignes);
        }

        [HttpPost]
        public IActionResult AddConsigne(Consigne data)
        {

            var res = _consigneRepo.GetAll().Where(x => x.ConsigneNTN == data.ConsigneNTN).LastOrDefault();

            if(res != null)
            {
                return new OkObjectResult(new { error = true, Message = "already avaible" });

            }
            _consigneRepo.Add(data);


            //var party = new Party
            //{
            //    PartyName = data.ConsigneName,
            //    Consignee = data.ConsigneNTN,
            //    ConsigneId = data.ConsigneId
            //};
            //_partyRepository.Add(party);




            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdateConsigne(Consigne data)
        {
            

           // var party = _partyRepository.GetAll().Where(x => x.ConsigneId == data.ConsigneId).FirstOrDefault();

            //if (party != null)
            //{
            //    party.PartyName = data.ConsigneName;
            //    party.Consignee = data.ConsigneNTN;

            //    _partyRepository.Update(party);

            //}
            _consigneRepo.Update(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        #endregion



        #region Good
        public IActionResult GoodView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetGoods()
        {
            var goods = _goodRepo.GetAll();
            return Json(goods);
        }

        [HttpPost]
        public IActionResult AddGood(Good data)
        {
            _goodRepo.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdateGood(Good data)
        {
            _goodRepo.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        #endregion



        #region GoodHead
        public IActionResult GoodHeadView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetGoodHeads()
        {
            var goodshead = _goodsHeadRepo.GetAll();
            return Json(goodshead);
        }

        [HttpPost]
        public IActionResult AddGoodshead(GoodsHead data)
        {
            _goodsHeadRepo.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdateGoodHeads(GoodsHead data)
        {
            _goodsHeadRepo.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        #endregion



        #region IHCO
        public IActionResult IHCOView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetIHCOs()
        {
            var ihcos = _ihcoRepo.GetAll().Where(x => x.MessageFrom == "Terminal" && x.ISGround == false).ToList();
            return Json(ihcos);
        }

        [HttpPost]
        public IActionResult AddIHCO(IHCO data)
        {


            data.MessageFrom = "Terminal";
            data.MessageTo = "Yard";
            data.FileName = "Manual";
            data.Performed = DateTime.Now;
            data.TotalRecords = 1;
            data.RecordSequence = 1;
            data.ISGround = false;

            _ihcoRepo.Add(data);



            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdateIHCO(IHCO data)
        {
            _ihcoRepo.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        #endregion

        #region NON Weboc IHCO Grounding
        public IActionResult NonWebocGroundingView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetNonWebocGrounding()
        {
            var ihcos = _ihcoRepo.GetAll().Where(x => x.MessageFrom == "Terminal" && x.ISGround == false).ToList();
            return Json(ihcos);
        }

        [HttpPost]
        public IActionResult UpdateGroundingIHCOs(List<IHCO> data)
        {
            try
            {
                var ihcos = new List<IHCO>();

                foreach (var item in data)
                {
                    var res = _ihcoRepo.GetAll().Where(x => x.IHCOId == item.IHCOId).LastOrDefault();
                     
                    if(res != null)
                    {
                        res.ISGround = true;

                        ihcos.Add(res);
                    }

                   
                }
                 
                _ihcoRepo.UpdateRange(ihcos);

                return new OkObjectResult(new { error = false, Message = "Save" });

            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });

            }
          


        }

        #endregion

        #region NON Weboc IHC Grounding
        public IActionResult NonWebocGroundingCYView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetNonWebocGroundingCY()
        {
            var ihcos = _ihcRepo.GetAll().Where(x => x.FileName == "Manual" && x.ISGround == false).ToList();
            return Json(ihcos);
        }

        [HttpPost]
        public IActionResult UpdateGroundingIHCs(List<IHC> data)
        {
            try
            {
                var ihcs = new List<IHC>();

                foreach (var item in data)
                {
                    var res = _ihcRepo.GetAll().Where(x => x.IHCId == item.IHCId).LastOrDefault();

                    if (res != null)
                    {
                        res.ISGround = true;

                        ihcs.Add(res);
                    }


                }

                _ihcRepo.UpdateRange(ihcs);

                return new OkObjectResult(new { error = false, Message = "Save" });

            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });

            }



        }

        #endregion


        #region IHC
        public IActionResult IHCView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetIHCs()
        {
            var ihcs = _ihcRepo.GetAll().Where(x => x.FileName == "Manual" && x.ISGround == false).ToList();
            return Json(ihcs);
        }

        [HttpPost]
        public IActionResult AddIHC(IHC data)
        {
            data.FileName = "Manual";
            data.Performed = DateTime.Now;
            data.TotalRecords = 1;
            data.RecordSequence = 1;
            data.ISGround = false;

            _ihcRepo.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdateIHC(IHC data)
        {
            _ihcRepo.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        #endregion




        #region DropOfTicket
        public IActionResult DropOfTicketView()
        {
            return View();
        }

        [HttpGet]
        [HttpGet]
        public JsonResult GetDropOfTickets(string virnno, string containerno)
        {
            var ihcos = _importDropOfTicketRepo.GetAll().Where(x => x.VirNumber == virnno && x.ContainerNo == containerno).ToList();
            return Json(ihcos);
        }

        [HttpPost]
        public IActionResult AddGetDropOfTicket(ImportDropOfTicket data, string igm, string containerNumber)
        {

            var result = _importDropOfTicketRepo.GetLastImportDropOfTicket( igm.Trim().ToUpper()  , containerNumber.Trim().ToUpper());

            if (result != null)
            {
                return new OkObjectResult(new { error = true, Message = "already exist" });

            }

            data.VirNumber = igm;
            data.ContainerNo = containerNumber;
            data.InTime = DateTime.Now;
            _importDropOfTicketRepo.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }



        [HttpPost]
        public IActionResult UpdatteDropOfTicket(ImportDropOfTicket data, string igm, string containerNumber , bool status)
        {

            var result = _importDropOfTicketRepo.GetAll().Where(x => x.VirNumber.Trim().ToUpper() == igm.Trim().ToUpper() && x.ContainerNo.Trim().ToUpper() == containerNumber.Trim().ToUpper()).LastOrDefault();

            if (result != null)
            {
                if(result.OutTime == null)
                {
                    if(status == true)
                    {
                        result.OutTime = DateTime.Now;
                    }
                    else
                    {
                        result.OutTime = null;
                    }

                    result.VirNumber = igm;
                    result.ContainerNo = containerNumber;
                    result.TruckNumber = data.TruckNumber;
                    result.ManifestedSealNumber = data.ManifestedSealNumber;
                    _importDropOfTicketRepo.Update(result);
                    return new OkObjectResult(new { error = false, Message = "Updated" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "can't updated due to mark out" });
                }
                

                return new OkObjectResult(new { error = false, Message = "Updated" });
            }
            else
            {

                return new OkObjectResult(new { error = true, Message = "record not found" });
            }

        }




        #endregion

        #region ParkingTicket
        public IActionResult ParkingTicketView()
        {
            return View();
        }

        public string GenParkingTicketCode()
        {
            var date = DateTime.Now;
            var peralterNumber = _parkingTicketRepo.GenerateParkingTicketNumber();
            string value = peralterNumber.ToString();
            return "PS-" + value + "/" + date.ToString("yy");

        }


        [HttpPost]
        public IActionResult AddParkingTicket(ParkingTicket data)
        {
            _parkingTicketRepo.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        #endregion



        #region Bidder
        public IActionResult BidderView()
        {
            return View();
        }

        public JsonResult GetBidders()
        {
            var ihcs = _bidderRepo.GetAll().ToList();
            return Json(ihcs);
        }


        [HttpPost]
        public IActionResult AddBidder(Bidder data)
        {
            _bidderRepo.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdateBidder(Bidder data)
        {
            _bidderRepo.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }


        #endregion



        #region LabourWorkOrder
        public IActionResult LabourWorkOrderView()
        {
            return View();
        }

        public JsonResult GetLabourWorkOrders()
        {
            var LabourWorkOrder = _LabourWorkOrderRepo.GetAll().ToList();
            return Json(LabourWorkOrder);
        }


        [HttpPost]
        public IActionResult AddLabourWorkOrder(LabourWorkOrder data)
        {
            //var number = GenCode();
            //data.WorkOrderNumber = number;
            data.WorkOrderDate = DateTime.Now;
            _LabourWorkOrderRepo.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }


        public void DeleteWorkorder(long key)
        {
            var worder = _LabourWorkOrderRepo.Find(key);

            _LabourWorkOrderRepo.Delete(worder);
        }

        //public long GenCode()
        //{
        //    var date = DateTime.Now;
        //    var Number = _LabourWorkOrderRepo.GenerateNumber();

        //    return Number;
        //    //string value = Number.ToString();
        //    //return value + "/" + date.ToString("yy");

        //}

        #endregion



        #region SealPurchase
        public IActionResult SealPurchase()
        {
            return View();
        }

        public JsonResult GetSealPurchases()
        {
            var sealPurchase = _sealPurchaseRepo.GetAll().Where(x=> x.AssignStatus == false).ToList();
            return Json(sealPurchase);
        }

        public JsonResult GetSealPurchasesByNo(long from, long to, string batch)
        {
            var sealPurchase = _sealPurchaseRepo.GetSealsByNo(from, to, batch);
            return Json(sealPurchase);
        }

        public JsonResult GetSealPurchasesByBatch(string batch)
        {
            var sealPurchase = _sealPurchaseRepo.GetSealsByBatchCode(batch);
            return Json(sealPurchase);
        }



        public string SealPurchaseNo()
        {
            var date = DateTime.Now;
            var peralterNumber = _sealPurchaseRepo.GenerateSealNumber();
            string value = peralterNumber.ToString();
            return value + "/" + date.ToString("dd-MM-yy");

        }

      
        [HttpPost]
        public IActionResult AddSealPurchase(SealPurchase data)
        {
            try
            {

                data.PurchaseDate = DateTime.Now;

                long count = 0;

                for (var i = data.SealFrom; i <= data.SealTo; i++)
                {
                    count += 1;
                }
                 



                var sealpurchaseno = SealPurchaseNo();


                var req = new SealPurchase
                {
                    Rate = data.Rate,
                    PurchaseDate = data.PurchaseDate,
                    SealFrom = data.SealFrom,
                    SealTo = data.SealTo,
                     SealPurchaseNo = sealpurchaseno,
                    AssignStatus = false,
                    CurrentSealNumber = data.SealFrom,
                    RemaingSeal = count,
                    TotalSeal = count

                };

                _sealPurchaseRepo.Add(req);

                return new OkObjectResult(new { error = false, Message = "Save" });

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            }

        }


        public JsonResult GetCurrentSeal(string batch)
        {
            var data = _sealPurchaseRepo.GetAll().Where(x => x.SealPurchaseNo == batch).FirstOrDefault();

            if (data != null)
            {
                return Json(data);
            }
            else
            {
                return Json("");
            }
        }

        [HttpPost]
        public IActionResult UpdateSealPurchase(SealPurchase data)
        {
            try
            {
                var res = _sealPurchaseRepo.GetAll().Where(x => x.SealPurchaseId == data.SealPurchaseId).LastOrDefault();
                if(res != null)
                {
                    res.AssignStatus = true;
                    _sealPurchaseRepo.Update(res);
                    return new OkObjectResult(new { error = false, Message = "Update" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "result not found" });
                }
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
                
            }
        }

        public IActionResult DeleteSealPurchase(long key)
        {

            try
            {
                var res = _sealPurchaseRepo.GetAll().Where(x => x.SealPurchaseId == key).LastOrDefault();
                if (res != null)
                {
                    if (res.AssignStatus == true)
                    {
                        return new OkObjectResult(new { error = true, Message = "not delete due to seal assign" });
                    }
                    else
                    {
                         
                        _sealPurchaseRepo.Delete(res);
                        return new OkObjectResult(new { error = false, Message = "Update" });
                    }
                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "result not found" });
                }
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });

            }

        }

        #endregion


        #region WorkOrderCSD
        public IActionResult WorkOrderCSDView()
        {
            return View();
        }

        public JsonResult GetWorkOrderCSDs(string igm)
        {
            var workOrders = _workOrderCSDRepo.GetAll().Where(x => x.IGM == igm).ToList();
            return Json(workOrders);
        }


        [HttpPost]
        public IActionResult AddWorkOrderCSD(string igm, WorkOrderCSD data)
        {
            var number = GenCodeWorkOrder();
            data.WorkOrderNumber = number;
            data.WorkOrderDate = DateTime.Now;
            data.IGM = igm;
            _workOrderCSDRepo.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }


        public long GenCodeWorkOrder()
        {
            var Number = _workOrderCSDRepo.GenerateNumber();
            return Number;

        }


        #endregion


        #region Transporter
        public IActionResult TransporterView()
        {
            return View();
        }

        public JsonResult GetTransporters()
        {
            var Transporters = _transporterRepo.GetAll().ToList();
            return Json(Transporters);
        }


        [HttpPost]
        public IActionResult AddTransporters(Transporter data)
        {
            _transporterRepo.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdateTransporter(Transporter data)
        {
            _transporterRepo.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }


        #endregion



        #region TransporterAssigning
        public IActionResult TransporterAssigningView()
        {
            return View();
        }

        public JsonResult GetTransporterAssignings()
        {
            var Transporters = _transporterRepo.GetAll().ToList();
            return Json(Transporters);
        }


        [HttpPost]
        public IActionResult AddTransporterAssignings(List<TransporterViewModel> containers)
        {

            var containerIndes = new List<ContainerIndex>();
            var cycontainers = new List<CYContainer>();
            foreach (var container in containers)
            {
                if (container.Status == "CFS")
                {
                    var containerIndexes = _containerIndexRepo.GetAll().Where(x => x.ContainerNo == container.ContainerNo && x.VirNo == container.VIRNo).ToList();
                    if (containerIndexes.Count() > 0)
                    {
                        foreach (var cntr in containerIndexes)
                        {
                            if (cntr != null)
                            {
                                cntr.TransporterId = container.TransporterId;
                                containerIndes.Add(cntr);

                            }
                        }

                    }

                }
                if (container.Status == "CY")
                {
                    var cntr = _cycontainerRepo.GetFirst(x => x.ContainerNo == container.ContainerNo && x.VIRNo == container.VIRNo);
                    if (cntr != null)
                    {
                        cntr.TransporterId = container.TransporterId ?? null;
                        cycontainers.Add(cntr);

                    }

                }


            }



            _containerIndexRepo.UpdateRange(containerIndes);

            _cycontainerRepo.UpdateRange(cycontainers);




            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdateTransporterAssigning()
        {
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }


        #endregion

        #region SealIssue

         

        public IActionResult SealIssuance()
        {
            ViewData["SealIssuance"] = _sealPurchaseRepo.GetAll().OrderBy(s => s.SealFrom)
               .Select(s => new SelectListItem { Text = s.SealPurchaseNo, Value = s.SealFrom.ToString() });
            return View();
        }


        [HttpPost]
        public IActionResult UpdateSealIssuance(List<SealPurchase> model)
        {

            foreach (var item in model)
            {
                var data = _sealPurchaseRepo.Find(item.SealPurchaseId);

                if (data != null)
                {
                   // data.Status = true;
                    _sealPurchaseRepo.Update(data);
                }
            }

            return new OkObjectResult(new { error = false, Message = "Seals Assigned Successfully" });
        }


        public IActionResult SealAssign()
        {

             ViewData["SealAssign"] = _purchaseSealRepo.GetAll().Where(x => x.AssignStatus == true && x.RemaingSeal > 0)
                .Select(s => new SelectListItem { Text = s.SealPurchaseNo, Value = s.SealPurchaseNo });
            return View();
        }


        public JsonResult GetContainerAssignSealDetail(long containerid)
        {
            var data = _sealIssueRepo.GetAll().Where(x => x.CYContainerId == containerid).ToList();
            if(data.Count() > 0)
            {
                return Json(data);
            }
            else
            {
                return Json("");

            }
        }


        public IActionResult AddSealAssign(long cycontainerid , string batchnumber)
        {
            try
            {
                long amount = 0;
                var date = DateTime.Now;

                var res = _containerRepository.GetStatusCYCargoByCycontainerId(cycontainerid);

                if(res != "OK")
                {
                    return new OkObjectResult(new { error = true, Message = res });
                }

                var sealpurchase = _sealPurchaseRepo.GetAll().Where(x => x.SealPurchaseNo == batchnumber).LastOrDefault();

                if (sealpurchase != null)
                {
                    if(sealpurchase.RemaingSeal <= 0)
                    {
                        return new OkObjectResult(new { error = true, Message = "there is no reaming seal" });
                    }
                    else
                    {
                        var container = _cycontainerRepo.GetAll().Where(x => x.CYContainerId == cycontainerid).LastOrDefault();

                        if(container.ShippingAgentId == null)
                        {
                            return new OkObjectResult(new { error = true, Message = "Container Line Not Define" });
                        }
                        else
                        {
                            
                            var generealSealAmount  = _generealSealAmountRepo.GetFirst();

                            if (generealSealAmount != null)
                            {
                                amount = generealSealAmount.Amount;
                            }

                            var SeallineAmount = _lineSealAmountRepo.GetAll().Where(x => x.ShippingAgentId == container.ShippingAgentId).LastOrDefault();

                            if (SeallineAmount != null)
                            {
                                amount = SeallineAmount.Amount;
                            }

                            if (SeallineAmount == null && generealSealAmount == null)
                            {
                                return new OkObjectResult(new { error = true, Message = "Line Seal And General amount Not Define" });
                            }
                             

                                var data = new SealIssue
                                {
                                    BatchNumber = sealpurchase.SealPurchaseNo,
                                    CYContainerId = cycontainerid,
                                    Rate = amount,
                                    SealNumber = sealpurchase.CurrentSealNumber,
                                };
                                
                                _sealIssueRepo.Add(data);

                                sealpurchase.RemaingSeal = sealpurchase.RemaingSeal - 1;
                                sealpurchase.CurrentSealNumber = sealpurchase.CurrentSealNumber + 1;
                                _sealPurchaseRepo.Update(sealpurchase);


                            var groundedDays = _groundedDaysRepository.GetAll().Where(x => x.CYContainerId == cycontainerid && x.ExaminationDate != null && x.SealIssueDate == null).LastOrDefault();

                            if (groundedDays != null)
                            { 
                                groundedDays.SealIssueDate = DateTime.Now;
                                groundedDays.Days = Convert.ToInt32((date.Date - groundedDays.ExaminationDate.Value.AddDays(-1).Date).Days);
                                 
                                _groundedDaysRepository.Update(groundedDays);

                            }




                        }
                    }
                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "Seal Batch Not Found" });
                }

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });

            }

            //var sdxc = new SealIssue
            //{
            //    IssueDate = date,
            //    SealPurchaseId = model.SealPurchaseId,
            //    Rate = model.Rate,
            //    SealFrom = model.SealFrom,
            //    Container = model.Container,
            //    IGM = model.IGM,
            //    SealPurchaseNo = model.SealPurchaseNo
            //};

            //var data = _sealPurchaseRepo.Find(model.SealPurchaseId);

            //if (data != null)
            //{
            //    data.AssignStatus = true;
            //    _sealPurchaseRepo.Update(data);
            //}
            //_sealIssueRepo.Add(sdxc);

            return new OkObjectResult(new { error = false, Message = "Seals Assigned Successfully" });


        }


        public IActionResult DeleteSealAssign(long key)
        {
            try
            { 
                var res = _sealIssueRepo.GetAll().Where(x=> x.SealIssueId == key).LastOrDefault();

                if(res != null)
                {
                    var resdata = _sealIssueRepo.GetAll().Where(x => x.BatchNumber == res.BatchNumber && x.SealNumber > res.SealNumber).LastOrDefault();

                    if(resdata != null)
                    {
                        return new OkObjectResult(new { error = true, Message = "you have use the upcomming seal" });

                    }
                    else
                    {
                        var sealpurchase = _sealPurchaseRepo.GetAll().Where(x => x.SealPurchaseNo == res.BatchNumber ).LastOrDefault();

                        if(sealpurchase != null)
                        {
                            sealpurchase.CurrentSealNumber = res.SealNumber;
                            sealpurchase.RemaingSeal -= 1;

                            _sealIssueRepo.Delete(res);

                            _sealPurchaseRepo.Update(sealpurchase);

                            return new OkObjectResult(new { error = false, Message = "deleted" });

                        }
                        else
                        {
                            return new OkObjectResult(new { error = true, Message = "no data found" });

                        }


                    }

                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "no data found" });
                }

              

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });

            }

            //var sdxc = new SealIssue
            //{
            //    IssueDate = date,
            //    SealPurchaseId = model.SealPurchaseId,
            //    Rate = model.Rate,
            //    SealFrom = model.SealFrom,
            //    Container = model.Container,
            //    IGM = model.IGM,
            //    SealPurchaseNo = model.SealPurchaseNo
            //};

            //var data = _sealPurchaseRepo.Find(model.SealPurchaseId);

            //if (data != null)
            //{
            //    data.AssignStatus = true;
            //    _sealPurchaseRepo.Update(data);
            //}
            //_sealIssueRepo.Add(sdxc);

            return new OkObjectResult(new { error = false, Message = "Seals deleted Successfully" });


        }


        [HttpPost]
        public IActionResult UpdateSealAssign(SealIssue model)
        {
            //var date = DateTime.Now;


            //var sdxc = new SealIssue
            //{
            //    IssueDate = date,
            //    SealPurchaseId = model.SealPurchaseId,
            //    Rate = model.Rate,
            //    SealFrom = model.SealFrom,
            //    Container = model.Container,
            //    IGM = model.IGM,
            //    SealPurchaseNo = model.SealPurchaseNo
            //};

            //var data = _sealPurchaseRepo.Find(model.SealPurchaseId);

            //if (data != null)
            //{
            //    data.AssignStatus = true;
            //    _sealPurchaseRepo.Update(data);
            //}
            //_sealIssueRepo.Add(sdxc);
            return new OkObjectResult(new { error = false, Message = "Seals Assigned Successfully" });
        }

        #endregion

        #region DeliveredYard
        public IActionResult DeliveredYardView()
        {
            return View();
        }

        public JsonResult GetDeliveredYards()
        {
            var deliveredYards = _deliveredYardRepositoryRepo.GetAll().ToList();
            return Json(deliveredYards);
        }


        [HttpPost]
        public IActionResult AddDeliveredYards(DeliveredYard data)
        {
            _deliveredYardRepositoryRepo.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdateDeliveredYard(DeliveredYard data)
        {
            _deliveredYardRepositoryRepo.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }


        #endregion


        #region DAT File


        public List<string> DDTFILE()
        {

            var _giioFiles = new List<string>();

            StringBuilder sb = new StringBuilder();
            List<string> list = new List<string>();
            using (StreamReader sr = new StreamReader("C:\\MCIGMIX.dat"))
            {
                while (sr.Peek() >= 0)
                {
                    list.Add(sr.ReadLine()); //Using readline method to read text file.
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                string[] strlist1 = list[i].Split("  "); //using string.split() method to split the string.
                string[] strlist = strlist1.Where(ds => !string.IsNullOrEmpty(ds)).ToArray();

                var y = strlist[0].Trim();
                var d = strlist[1].Trim();
                var x = strlist[2].Trim();
                var xHG = strlist[3].Trim();
                var xHM = strlist[4].Trim();
                var xMH = strlist[5].Trim();
                var xMM = strlist[6].Trim();
                var xNN = strlist[7].Trim();
                var xBN = strlist[8].Trim();
                var xVV = strlist[9].Trim();
                var xCF = strlist[10].Trim();
                var xXX = strlist[11].Trim();
                var xZZ = strlist[12].Trim();
                var xA = strlist[13].Trim();
                var xQ = strlist[14].Trim();
                var xUU = strlist[15].Trim();
                var xTR = strlist[16].Trim();
                var xRE = strlist[17].Trim();
                var xR = strlist[18].Trim();
                var xFS = strlist[19].Trim();
                var xS = strlist[20].Trim();


                //_giioFiles.Add(new GIIO
                //{
                //    TotalRecords = Convert.ToInt32(strlist[0]),
                //    RecordSequence = Convert.ToInt32(strlist[1]),
                //    VIRNumber = strlist[2],
                //    ContainerNumber = strlist[3],
                //    PCCSSSealNumber = strlist[4],
                //    Weight = Convert.ToDouble(strlist[5]),
                //    GateInTime = DateFormatter.ConvertToEdiFormat(strlist[6]),
                //    VehicleNumber = strlist[7],
                //    Performed = DateFormatter.ConvertToEdiFormat(strlist[8]),
                //    MessageFrom = strlist[9],
                //    MessageTo = strlist[10],
                //    FileName = file.Name
                //});

            }
            return _giioFiles;
        }

        //public  void DDTFILE()
        //{
        //    StreamReader reader = new StreamReader("C:\\MCIGMIX.dat");

        //    string strAllFile = reader.ReadToEnd().Replace("\r\n", "\n").Replace("\n\r", "\n");
        //    string[] arrLines = strAllFile.Split(new char[] { '\n' });

        //     //string contents = objInput.ReadToEnd().Trim();
        //    //string[] split = System.Text.RegularExpressions.Regex.Split(contents, "\\s+", RegexOptions.None);
        //    foreach (string s in arrLines)
        //    {

        //    }
        //}
        #endregion

        #region BillToLine
        public IActionResult BillToLineView()
        {
            return View();
        }

        public JsonResult GetBillToLines(long containerId, string type)
        {

            //if (type == "CFS")
            //{

            //    var BillToLines = _billToLineRepository.GetAll().Where(x => x.ContainerIndexId == containerId && x.InoviceCreated == false).LastOrDefault();
            //    return Json(BillToLines);

            //}
            //if (type == "CY")
            //{

            //    var BillToLines = _billToLineRepository.GetAll().Where(x => x.CyContainerId == containerId && x.InoviceCreated == false).LastOrDefault();

            //    return Json(BillToLines);
            //}

            return Json("");

        }

        

        public JsonResult GetBillToLineDetailIgmIndexWise(string virno , long indexno, string type)
        {
             
                var BillToLines = _billToLineRepository.GetAll().Where(x => x.VirNo == virno  && x.IndexNo == indexno && x.IndexType == type).ToList();
                return Json(BillToLines);
             
        }




        [HttpPost]
        public IActionResult AddBillToLineCFS(long containerId, double amount, string remarks)
        {

            //var req = _billToLineRepository.GetAll().Where(x => x.ContainerIndexId == containerId && x.InoviceCreated == false).LastOrDefault();

            //if (req != null)
            //{
            //    return new OkObjectResult(new { error = true, Message = "already amount given" });

            //}
            //else
            //{
            //    var data = new BillToLine();

            //    //data.BillToLineAmount = amount;
            //    data.ContainerIndexId = containerId;
            //    data.Remarks = remarks;
            //    _billToLineRepository.Add(data);

                return new OkObjectResult(new { error = false, Message = "Saved" });

            //}


        }

        [HttpPost]
        public IActionResult AddBillToLineCy(string igm, int index, double amount, string remarks)
        {
            try
            {

                //var cyContainer = _cycontainerRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == index);

                //if (cyContainer != null)
                //{


                //    var res = _billToLineRepository.GetAll().Where(x => x.CyContainerId == cyContainer.CYContainerId && x.InoviceCreated == false).LastOrDefault();

                //    if (res != null)
                //    {
                //        return new OkObjectResult(new { error = true, Message = "already amount given" });

                //    }
                //    else
                //    {
                //        var data = new BillToLine();

                //       // data.BillToLineAmount = amount;
                //        data.CyContainerId = cyContainer.CYContainerId;
                //        data.Remarks = remarks;
                //        _billToLineRepository.Add(data);

                //        return new OkObjectResult(new { error = false, Message = "Saved" });

                //    }




                //}
                //else
                //{
                      return new OkObjectResult(new { error = true, Message = "container index not found" });

                //}




            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, Message = "please try again" });

            }





        }

        [HttpPost]
        public IActionResult UpdateBillToLine(BillToLine data)
        {
            _billToLineRepository.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }


        #endregion

        #region ExchangeRate

        public IActionResult ExchangeRateView()
        {
            return View();
        }



        [HttpGet]
        public JsonResult GetExchangeRate()
        {
            var data = _exchangeRateRepo.GetAll().LastOrDefault();
            return Json(data);
        }


        [HttpPost]
        public IActionResult UpdateExchangeRate(double rate , double rate20 , double rate40, double rate45)
        {
            try
            {
                var data = _exchangeRateRepo.GetAll().LastOrDefault();

                if (data != null)
                {
                    data.ExchangeRateAmount = rate;
                    data.RateAmount20 = rate20;
                    data.RateAmount40 = rate40;
                    data.RateAmount45 = rate45;
                    _exchangeRateRepo.Update(data);

                    return new OkObjectResult(new { error = false, Message = "Update" });

                }
                else
                {
                    var res = new ExchangeRate
                    {
                        ExchangeRateAmount = rate,
                        RateAmount20 = rate20,
                        RateAmount40 = rate40,
                        RateAmount45 = rate45,
                };
                    _exchangeRateRepo.Add(res);

                    return new OkObjectResult(new { error = false, Message = "Saved" });


                }

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = "Please try again" });
            }


            return Ok();
        }

        #endregion




        #region OtherCharge
        public IActionResult OtherChargeView()
        {
            return View();
        }

        public JsonResult GetOtherCharge()
        {
            var OtherCharges = _otherChargeRepo.GetAll();


            if (OtherCharges != null)
            {
                return Json(OtherCharges);
            }
            return Json("");

        }


        [HttpPost]
        public IActionResult AddOtherCharge(OtherCharge data)
        {
            _otherChargeRepo.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdateOtherCharge(OtherCharge data)
        {
            _otherChargeRepo.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }




        #endregion


        #region OtherChargeAssigning
        public IActionResult OtherChargeAssigningView()
        {
            return View();
        }

        public IActionResult GetCargoStatusCFS(long containerId)
        {
            var res = _containerRepository.GetCFSCargoStatus(containerId);

            if(res != "OK")
            {
                return new OkObjectResult(new { error = true, Message = res });

            }
            else
            {
                return new OkObjectResult(new { error = false, Message = "" });
 
            }
        }

        public IActionResult GetStatusCYCargo(string virno , long indexno)
        {
            var res = _containerRepository.GetStatusCYCargo(virno , indexno);

            if (res != "OK")
            {
                return new OkObjectResult(new { error = true, Message = res });

            }
            else
            {
                return new OkObjectResult(new { error = false, Message = "" });

            }
        }

        public JsonResult GetOtherChargeAssigningCFS(long containerId)
        {

            var OtherChargeAssignings = _otherChargeAssigningRepo.GetAll().Where(x => x.ContainerIndexId == containerId).ToList();

            if (OtherChargeAssignings != null)
            {
                return Json(OtherChargeAssignings);
            }


            return Json("");

        }


        public JsonResult GetOtherChargeAssigningCY(string igm, int index)
        {


            var cyContainer = _cycontainerRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == index);

            if (cyContainer != null)
            {
                var OtherChargeAssignings = _otherChargeAssigningRepo.GetAll().Where(x => x.CyContainerId == cyContainer.CYContainerId ).ToList();

                if (OtherChargeAssignings != null)
                {
                    return Json(OtherChargeAssignings);
                }

            }



            return Json("");

        }


        [HttpPost]
        public IActionResult AddOtherChargeAssigningCFS(long ContainerId, OtherChargeAssigning data)
        {

            var userIdentity = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userRepo.GetAll().Where(x => x.IdentityUserId == userIdentity).FirstOrDefault();
            var UserName = user.FirstName + " - " + user.LastName;

            //var otherChargeAssigning = _otherChargeAssigningRepo.GetAll().Where(x => x.ContainerIndexId == ContainerId    && x.InvoiceCreated == false).ToList();

            //if (otherChargeAssigning.Count() > 0)
            //{
            //    return new OkObjectResult(new { error = true, message = "already created" });

            //}
            //else
            //{

            var container = _containerIndexRepo.GetLastContainerIndexById(ContainerId);
            {
                if (container != null)
                {
                    //if(container.IsDelivered == true)
                    //{
                    //    return new OkObjectResult(new { error = true, Message = "container Mark Delivered" });
                    //}
                    //else
                    //{

                        if (data.ChargeQuantity != 0)
                        {
                            data.ChargeAmount = data.ChargeAmount * data.ChargeQuantity;
                        }

                        if (data.ChargeQuantity == 0)
                        {
                            data.ChargeQuantity = 1;
                            data.ChargeAmount = data.ChargeAmount * data.ChargeQuantity;
                        }



                        data.ContainerIndexId = ContainerId;
                        data.InvoiceCreated = false;
                        data.CreatedDate = DateTime.Now;
                        data.UserName = UserName;
                        _otherChargeAssigningRepo.Add(data);
                        return new OkObjectResult(new { error = false, Message = "Saved" });
                    //}

                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "no container found" });
                }
            }

            //}
             
           // return new OkObjectResult(new { error = true, Message = "Please Try Again" });
        }

        [HttpPost]
        public IActionResult AddOtherChargeAssigningCY(string igm, int index, OtherChargeAssigning data)
        {

            var userIdentity = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userRepo.GetAll().Where(x => x.IdentityUserId == userIdentity).FirstOrDefault();
            var UserName = user.FirstName + " - " + user.LastName;

            var cyContainer = _cycontainerRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == index);

            if (cyContainer != null)
            {


                //var otherChargeAssigning = _otherChargeAssigningRepo.GetAll().Where(x => x.CyContainerId == cyContainer.CYContainerId && x.InvoiceCreated == false).ToList();

                //if (otherChargeAssigning.Count() > 0)
                //{
                //    return new OkObjectResult(new { error = true, message = "already created" });

                //}

                //if(cyContainer.IsDelivered == true)
                //{
                //    return new OkObjectResult(new { error = true, Message = "Already Mark Deliverd" });
                //}
                //else
                //{

                    if (data.ChargeQuantity != 0)
                    {
                        data.ChargeAmount = data.ChargeAmount * data.ChargeQuantity;
                    }

                    if (data.ChargeQuantity == 0)
                    {
                        data.ChargeQuantity = 1;
                        data.ChargeAmount = data.ChargeAmount * data.ChargeQuantity;
                    }

                    data.CyContainerId = cyContainer.CYContainerId;
                    data.InvoiceCreated = false;
                    data.CreatedDate = DateTime.Now;
                data.UserName = UserName;
                    _otherChargeAssigningRepo.Add(data);
                    return new OkObjectResult(new { error = false, Message = "Saved" });
                //}

            }
            else
            {
                return new OkObjectResult(new { error = false, Message = "Index Not Found" });

            }





            return new OkObjectResult(new { error = true, Message = "Please Try Again" });
        }


        public IActionResult DeleteOtherChargeAssigned(long key)
        {


            var data = _otherChargeAssigningRepo.Find(key);

            if (data != null)
            {

                if (data.ContainerIndexId != 0)
                {
                    if(data.InvoiceCreated == true)
                    {
                        return new OkObjectResult(new { error = true, Message = "Invoice Created" });
                    }

                    _otherChargeAssigningRepo.Delete(data);

                    return new OkObjectResult(new { error = false, Message = "Deleted" });


                }

                if (data.CyContainerId != 0)
                {
                    if (data.InvoiceCreated == true)
                    {
                        return new OkObjectResult(new { error = true, Message = "Invoice Created" });
                    }

                    _otherChargeAssigningRepo.Delete(data);

                    return new OkObjectResult(new { error = false, Message = "Deleted" });


                }

            }

            return new OkObjectResult(new { error = true, Message = "not found" });


        }


        #endregion



        #region Container Size Amount

        public IActionResult ContainerSizeAmountView()
        {
            return View();
        }



        [HttpGet]
        public JsonResult GetContainerSizeAmount()
        {
            var data = _containerSizeAmountRepo.GetAll().LastOrDefault();
            return Json(data);
        }


        [HttpPost]
        public IActionResult UpdateContainerSizeAmount(ContainerSizeAmount model)
        {
            try
            {


                var data = _containerSizeAmountRepo.GetAll().Where(x => x.ContainerSizeAmountId == model.ContainerSizeAmountId).FirstOrDefault();

                if (data != null)
                {
                    data.AmountSize20 = model.AmountSize20;
                    data.AmountSize40 = model.AmountSize40;
                    data.AmountSize45 = model.AmountSize45;
                    _containerSizeAmountRepo.Update(data);

                    return new OkObjectResult(new { error = false, Message = "Update" });
                }
                else
                {
                    var req = new ContainerSizeAmount
                    {
                        AmountSize20 = model.AmountSize20,
                        AmountSize40 = model.AmountSize40,
                        AmountSize45 = model.AmountSize45
                    };

                    _containerSizeAmountRepo.Add(req);

                    return new OkObjectResult(new { error = false, Message = "Save" });


                }



            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = "Please try again" });
            }


            return Ok();
        }

        #endregion




        #region Pre Alert Vessel
        public IActionResult PreAlertVesselView()
        {
            return View();
        }

        public JsonResult GetPreAlertVessels()
        {
            var preAlertVessels = _preAlertVesselRepo.GetAll().ToList();
            return Json(preAlertVessels);
        }


        [HttpPost]
        public IActionResult AddpreAlertVessel(PreAlertVessel data)
        {
            var result = _preAlertVesselRepo.GetAll().Where(x => x.PreAlertVesselName.Trim().ToUpper() == data.PreAlertVesselName.Trim().ToUpper()).FirstOrDefault();
            if(result != null)
            {
                return new OkObjectResult(new { error = true, Message = "Vessel Name Already exist" });

            }

            _preAlertVesselRepo.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdatePreAlertVessel(PreAlertVessel data)
        {
            _preAlertVesselRepo.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }


        #endregion


        #region R and D Clerk
        public IActionResult RandDClerkView()
        {
            return View();
        }

        public JsonResult GetRandDClerks()
        {
            var randDClerks = _randDClerkRepo.GetAll().ToList();
            return Json(randDClerks);
        }


        [HttpPost]
        public IActionResult AddRandDClerk(RandDClerk data)
        {
            var result = _randDClerkRepo.GetAll().Where(x => x.RandDClerkName.Trim().ToUpper() == data.RandDClerkName.Trim().ToUpper()).FirstOrDefault();
            if (result != null)
            {
                return new OkObjectResult(new { error = true, Message = "Already exist" });

            }

            _randDClerkRepo.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdateRandDClerk(RandDClerk data)
        {
            _randDClerkRepo.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }


        #endregion




        #region GatePass Weighment
        public IActionResult GatePassWeighmentView()
        {
            return View();
        }

         
        public JsonResult GetGatePassWeightments(string igm , int index)
        {
            var res = _gatePassWeightmentRepo.GetAll().Where(x => x.Virnumber.Trim().ToUpper() == igm.Trim().ToUpper() && x.IndexNumber == index).ToList();

            if(res.Count() > 0)
            {
                return Json(res);
            }

            return Json("");
        }


        [HttpPost]
        public IActionResult AddGatePassWeighment(string igm , int indexno , GatePassWeightment data)
        {
            var result = _gatePassWeightmentRepo.GetAll().Where(x => x.TruckNumber.Trim().ToUpper() == data.TruckNumber.Trim().ToUpper() && x.Virnumber == igm && x.IndexNumber == indexno).FirstOrDefault();
            if (result != null)
            {
                return new OkObjectResult(new { error = true, Message = "Already exist" });

            }


            data.Virnumber = igm;
            data.IndexNumber = indexno;
            _gatePassWeightmentRepo.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult DeleteGatePassWeighment(long key)
        {

            var data = _gatePassWeightmentRepo.GetAll().Where(x => x.GatePassWeightmentId == key).FirstOrDefault();


            _gatePassWeightmentRepo.Delete(data);
             
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }


        #endregion



        #region Truck In Out Process
        public IActionResult TruckInOutProcessView()
        {
            return View();
        }


        public JsonResult GetTruckInOutProcess(string igm, int index)
        {
            var res = _truckInOutRepo.GetAll().Where(x => x.VirNumber.Trim().ToUpper() == igm.Trim().ToUpper() && x.IndexNumber == index).ToList();

            if (res.Count() > 0)
            {
                return Json(res);
            }

            return Json("");
        }


        [HttpPost]
        public IActionResult AddTruckInOutProcess(TruckInOut model )
        {
            try
            {
                var res = _containerRepository.GetInvoiceInfo(model.VirNumber, model.IndexNumber, model.Type);
                if(res == false)
                {
                    return new OkObjectResult(new { error = true, Message = "please create invoice" });
                } 
                model.TruckEntryDate = DateTime.Now;
                _truckInOutRepo.Add(model);

                return new OkObjectResult(new { error = false, Message = "Save" });
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            } 
        }
        
        public IActionResult MarkInTruckInOutProcess(long truckInOutId , DateTime validate)
        {
            try
            { 
               var res = _truckInOutRepo.GetAll().Where(x=> x.TruckInOutId == truckInOutId).LastOrDefault();

                if(res.TruckInDate != null)
                {
                    return new OkObjectResult(new { error = true, Message = "already mark in" });
                }
                res.TruckInDate = DateTime.Now;
                res.ValidTo = validate;
                _truckInOutRepo.Update(res);


                return new OkObjectResult(new { error = false, Message = "Save" });
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            } 
        }

        public IActionResult MarkOutTruckInOutProcess(long truckInOutId , string status)
        {
            try
            {
                var res = _truckInOutRepo.GetAll().Where(x => x.TruckInOutId == truckInOutId).LastOrDefault();

                if (res.TruckOutDate != null)
                {
                    return new OkObjectResult(new { error = true, Message = "already mark out" });
                }

                res.TruckOut = true;
                res.Status = status;
                res.TruckOutDate = DateTime.Now;
              
                _truckInOutRepo.Update(res);


                return new OkObjectResult(new { error = false, Message = "Save" });
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            }
        }


        public IActionResult UpdateTruckInOutProcess(string igm, int indexno, string type, TruckInOut data)
        {
            var result = _truckInOutRepo.GetAll().Where(x => x.ContainerNumber.Trim().ToUpper() == data.ContainerNumber.Trim().ToUpper() && x.VirNumber == igm && x.IndexNumber == indexno).FirstOrDefault();
            if (result != null)
            {

                if (data.TruckInDate != null && data.TruckInDate != result.TruckInDate &&   result.TruckInDate  != null)
                {
                    return new OkObjectResult(new { error = true, Message = "can't change truck in date" });

                }

                if (data.TruckEntryDate != null && data.TruckEntryDate != result.TruckEntryDate && result.TruckEntryDate != null)
                {
                    return new OkObjectResult(new { error = true, Message = "can't change truck entry date" });

                }
                
                if (data.TruckOutDate != null && data.TruckOutDate != result.TruckOutDate && result.TruckOutDate != null)
                {
                    return new OkObjectResult(new { error = true, Message = "can't change truck entry date" });

                }

                result.TruckNo = data.TruckNo;
                result.ContainerNumber = data.ContainerNumber;
                result.VirNumber = data.VirNumber;
                result.IndexNumber = data.IndexNumber;
                result.ValidTo = data.ValidTo;
                result.Type = data.Type;                 
                result.TruckEntryDate = data.TruckEntryDate;
                result.TruckInDate = data.TruckInDate;
                result.TruckOutDate = data.TruckOutDate;

                _truckInOutRepo.Update(result);

  
                return new OkObjectResult(new { error = false, Message = "Upated" });

            }
            else
            {
                var resdata = new TruckInOut
                {
                    TruckNo = data.TruckNo,
                    ContainerNumber = data.ContainerNumber,
                    VirNumber = igm,
                    IndexNumber = indexno,
                    ValidTo = data.ValidTo,
                    Type = data.Type,
                    TruckEntryDate = data.TruckEntryDate,
                    TruckInDate = data.TruckInDate,
                    TruckOutDate = data.TruckOutDate,
                };
                _truckInOutRepo.Add(resdata);
                return new OkObjectResult(new { error = true, Message = "Saved" });
            }

             
             
        }
        [HttpPost]
        public IActionResult DeleteTruckInOutProcess(long key)
        {

            var data = _truckInOutRepo.GetAll().Where(x => x.TruckInOutId == key).FirstOrDefault();


            _truckInOutRepo.Delete(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }


        #endregion


        #region GatePass Sample


        public IActionResult GatePassSampleView()
        {

            return View();
        }


        public IActionResult CreateGatePassSample(GatePassSample model)
        {
            try
            {
                model.GatePassDate = DateTime.Now;
                model.GatePassNumber = GetGatePassSampleyNo();
                _gatePassSampleRepository.Add(model);

                return new OkObjectResult(new { error = false, Message = "save" });

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            }

        }

        public long GetGatePassSampleyNo()
        {
            var pass = _gatePassSampleRepository.GetAll().Count();
         
            var passNo = pass != 0 ? pass + 1 : 1;

            return passNo;


        }

        #endregion


        #region Port Charges

        public IActionResult PortChargesView()
        {
            ViewData["ShippingAgents"] = _shippingAgentRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });


            ViewData["GoodsHead"] = _goodsHeadRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });
            return View();
        }


        public IActionResult AddPortCharges(PortChargesViewModel model)
        {
            try
            {
                if(model.Type == "CFS")
                {
                    var containerindex = _containerIndexRepo.GetContainerIndexById(model.ContainerId);
                    if (containerindex != null)
                    {
                        if (containerindex.IsDelivered == true)
                        {
                            return new JsonResult(new { error = true, message = "Index Delivered" });
                        }

                         
 
                            var charges = _portChargeRepo.GetAll().Where(x => x.VirNumber == model.VirNumber && x.IndexNumber == model.IndexNumber).LastOrDefault();
                            if (charges != null)
                            {
                                charges.DemurrageCharges = model.DemurrageCharges;
                                charges.WeighmentCharges = model.WeighmentCharges;
                                charges.OverWeightPenalty = model.OverWeightPenalty; 
                                charges.DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal; 
                                charges.ThcPhcKdlpCharges = model.ThcPhcKdlpCharges; 
                                charges.LoloCharges = model.LoloCharges; 
                                charges.QictCharges = model.QictCharges; 
                                charges.OtherCharges = model.OtherCharges; 
                                charges.WashAndCleanCharges = model.WashAndCleanCharges;
                                charges.ANF = model.ANF;
                                charges.Pallet = model.Pallet;
                                charges.Recover = model.Recover;
                                charges.TransportCharges = model.TransportCharges;


        charges.TotalCharges = model.DemurrageCharges + model.WeighmentCharges + model.OverWeightPenalty + model.DetentionChargesOrBulletSeal + model.ThcPhcKdlpCharges 
                                                      + model.LoloCharges + model.QictCharges + model.OtherCharges + model.WashAndCleanCharges + model.ANF + model.Pallet + model.Recover
                                                      + model.TransportCharges;
                            _portChargeRepo.Update(charges);

                            return new JsonResult(new { error = false, message = "Update" });

                            }
                            else
                            {
                                var charge = new PortCharge
                                {
                                    DemurrageCharges = model.DemurrageCharges,
                                    WeighmentCharges = model.WeighmentCharges,
                                    OverWeightPenalty = model.OverWeightPenalty,
                                    DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal,
                                    ThcPhcKdlpCharges = model.ThcPhcKdlpCharges,
                                    LoloCharges = model.LoloCharges,
                                    QictCharges = model.QictCharges,
                                    OtherCharges = model.OtherCharges,
                                    WashAndCleanCharges = model.WashAndCleanCharges,
                                    TotalCharges = model.DemurrageCharges + model.WeighmentCharges + model.OverWeightPenalty + model.DetentionChargesOrBulletSeal
                                                    + model.ThcPhcKdlpCharges + model.LoloCharges + model.QictCharges + model.OtherCharges + model.WashAndCleanCharges
                                                    + model.ANF + model.Recover + model.Pallet + model.TransportCharges,
                                    VirNumber =  model.VirNumber,
                                    IndexNumber = model.IndexNumber, 
                                    ContainerNumber = model.ContainerNumber,
                                    ANF = model.ANF,
                                    Pallet = model.Pallet,
                                    Recover = model.Recover,
                                    TransportCharges = model.TransportCharges

                                    };
                                     _portChargeRepo.Add(charge);
                                return new JsonResult(new { error = false, message = "Update" });

                            }
                        

                    }
                    else
                    {

                        return new JsonResult(new { error = true, message = "Index Not Found" });
                    }
                }
                if(model.Type == "CY")
                {
                    var container = _cycontainerRepo.GetAll().Where(x => x.CYContainerId == model.ContainerId).LastOrDefault();
                    if (container  != null)
                    {
                        if (container.IsDelivered == true)
                        {
                            return new JsonResult(new { error = true, message = "Index Delivered" });
                        }

                         
 
                            var charges = _portChargeRepo.GetAll().Where(x => x.VirNumber == model.VirNumber && x.IndexNumber == model.IndexNumber).LastOrDefault();
                            if (charges != null)
                            {
                                charges.DemurrageCharges = model.DemurrageCharges;
                                charges.WeighmentCharges = model.WeighmentCharges;
                                charges.OverWeightPenalty = model.OverWeightPenalty; 
                                charges.DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal; 
                                charges.ThcPhcKdlpCharges = model.ThcPhcKdlpCharges; 
                                charges.LoloCharges = model.LoloCharges; 
                                charges.QictCharges = model.QictCharges; 
                                charges.OtherCharges = model.OtherCharges; 
                                charges.WashAndCleanCharges = model.WashAndCleanCharges;
                                charges.ANF = model.ANF;
                                charges.Pallet = model.Pallet;
                                charges.Recover = model.Recover;
                                charges.TransportCharges = model.TransportCharges;


                            charges.TotalCharges = model.DemurrageCharges + model.WeighmentCharges + model.OverWeightPenalty + model.DetentionChargesOrBulletSeal + model.ThcPhcKdlpCharges
                                                                          + model.LoloCharges + model.QictCharges + model.OtherCharges + model.WashAndCleanCharges + model.ANF + model.Pallet + model.Recover
                                                                          + model.TransportCharges;
                            _portChargeRepo.Update(charges);

                            return new JsonResult(new { error = false, message = "Update" });

                            }
                            else
                            {
                                var charge = new PortCharge
                                {
                                    DemurrageCharges = model.DemurrageCharges,
                                    WeighmentCharges = model.WeighmentCharges,
                                    OverWeightPenalty = model.OverWeightPenalty,
                                    DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal,
                                    ThcPhcKdlpCharges = model.ThcPhcKdlpCharges,
                                    LoloCharges = model.LoloCharges,
                                    QictCharges = model.QictCharges,
                                    OtherCharges = model.OtherCharges,
                                    WashAndCleanCharges = model.WashAndCleanCharges,
                                    VirNumber =  model.VirNumber,
                                    IndexNumber = model.IndexNumber,
                                    ContainerNumber = model.ContainerNumber,
                                    ANF = model.ANF,
                                    Pallet = model.Pallet,
                                    Recover = model.Recover,
                                    TransportCharges = model.TransportCharges,
                                    TotalCharges = model.DemurrageCharges + model.WeighmentCharges + model.OverWeightPenalty + model.DetentionChargesOrBulletSeal
                                                    + model.ThcPhcKdlpCharges + model.LoloCharges + model.QictCharges + model.OtherCharges + model.WashAndCleanCharges
                                                    + model.ANF + model.Recover + model.Pallet + model.TransportCharges,

                                };
                                     _portChargeRepo.Add(charge);
                                return new JsonResult(new { error = false, message = "Update" });

                            }
                        

                    }
                    else
                    {

                        return new JsonResult(new { error = true, message = "Index Not Found" });
                    }
                }
                else
                {
                    return new JsonResult(new { error = true, message = "Type Not Found" });

                }




            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }



        public IActionResult AddPortChargesList(List<PortChargesViewModel> data)
        {
            try
            {
                var count = 0;
                var countDelivered = 0;

                foreach (var model in data)
                {
                    if (model.Type == "CFS")
                    {
                        var res = _containerIndexRepo.GetIndexInfo(model.VirNumber, model.IndexNumber ).ToList(); 

                        var containerindexidlist = string.Join(",", res.Select(n => n.ContainerIndexId.ToString()).ToArray());

                        var invoiceres = _invoiceRepo.GetCfsInvoices(containerindexidlist).ToList();
                         
                        if (res.Count() > 0 )
                        {
                            if (invoiceres.Count() > 0)
                            {
                                countDelivered += 1;
                            }
                            else
                            {
                                var charges = _portChargeRepo.GetPortChargesByIgmIndexContainer(model.VirNumber ,model.IndexNumber , model.ContainerNumber);
                                if (charges != null)
                                {
                                    charges.DemurrageCharges = model.DemurrageCharges;
                                    charges.WeighmentCharges = model.WeighmentCharges;
                                    charges.OverWeightPenalty = model.OverWeightPenalty;
                                    charges.DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal;
                                    charges.ThcPhcKdlpCharges = model.ThcPhcKdlpCharges;
                                    charges.LoloCharges = model.LoloCharges;
                                    charges.QictCharges = model.QictCharges;
                                    charges.OtherCharges = model.OtherCharges;
                                    charges.WashAndCleanCharges = model.WashAndCleanCharges;
                                    charges.ANF = model.ANF;
                                    charges.Pallet = model.Pallet;
                                    charges.Recover = model.Recover;
                                    charges.TransportCharges = model.TransportCharges;


                                    charges.TotalCharges = model.DemurrageCharges + model.WeighmentCharges + model.OverWeightPenalty + model.DetentionChargesOrBulletSeal + model.ThcPhcKdlpCharges
                                                                                  + model.LoloCharges + model.QictCharges + model.OtherCharges + model.WashAndCleanCharges + model.ANF + model.Pallet + model.Recover
                                                                                  + model.TransportCharges;
                                    _portChargeRepo.Update(charges);

 
                                }
                                else
                                {
                                    var charge = new PortCharge
                                    {
                                        DemurrageCharges = model.DemurrageCharges,
                                        WeighmentCharges = model.WeighmentCharges,
                                        OverWeightPenalty = model.OverWeightPenalty,
                                        DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal,
                                        ThcPhcKdlpCharges = model.ThcPhcKdlpCharges,
                                        LoloCharges = model.LoloCharges,
                                        QictCharges = model.QictCharges,
                                        OtherCharges = model.OtherCharges,
                                        WashAndCleanCharges = model.WashAndCleanCharges,
                                        TotalCharges = model.DemurrageCharges + model.WeighmentCharges + model.OverWeightPenalty + model.DetentionChargesOrBulletSeal
                                                        + model.ThcPhcKdlpCharges + model.LoloCharges + model.QictCharges + model.OtherCharges + model.WashAndCleanCharges
                                                        + model.ANF + model.Recover + model.Pallet + model.TransportCharges,
                                        VirNumber = model.VirNumber,
                                        IndexNumber = model.IndexNumber,
                                        ContainerNumber = model.ContainerNumber,
                                        ANF = model.ANF,
                                        Pallet = model.Pallet,
                                        Recover = model.Recover,
                                        TransportCharges = model.TransportCharges

                                    };
                                    _portChargeRepo.Add(charge);
 
                                }

                                count += 1;

                            }





                        }
                         
                    }
                   else if (model.Type == "CY")
                    {

                        var res = _cycontainerRepo.GetUndeliveredIndexbyigmindex(model.VirNumber, model.IndexNumber ).ToList();
                         
                        var containerindexidlist = string.Join(",", res.Select(n => n.CYContainerId.ToString()).ToArray());

                        var invoiceres = _invoiceRepo.GetCYInvoices(containerindexidlist).ToList();
                          
                        if (res.Count() > 0)
                        {
                            if (invoiceres.Count() > 0)
                            {
                                countDelivered += 1;
                            }
                             
                            else
                            {

                                var charges = _portChargeRepo.GetPortChargesByIgmIndexContainer(model.VirNumber ,model.IndexNumber ,model.ContainerNumber);
                                if (charges != null)
                                {
                                    charges.DemurrageCharges = model.DemurrageCharges;
                                    charges.WeighmentCharges = model.WeighmentCharges;
                                    charges.OverWeightPenalty = model.OverWeightPenalty;
                                    charges.DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal;
                                    charges.ThcPhcKdlpCharges = model.ThcPhcKdlpCharges;
                                    charges.LoloCharges = model.LoloCharges;
                                    charges.QictCharges = model.QictCharges;
                                    charges.OtherCharges = model.OtherCharges;
                                    charges.WashAndCleanCharges = model.WashAndCleanCharges;
                                    charges.ANF = model.ANF;
                                    charges.Pallet = model.Pallet;
                                    charges.Recover = model.Recover;
                                    charges.TransportCharges = model.TransportCharges;


                                    charges.TotalCharges = model.DemurrageCharges + model.WeighmentCharges + model.OverWeightPenalty + model.DetentionChargesOrBulletSeal + model.ThcPhcKdlpCharges
                                                                                  + model.LoloCharges + model.QictCharges + model.OtherCharges + model.WashAndCleanCharges + model.ANF + model.Pallet + model.Recover
                                                                                  + model.TransportCharges;
                                    _portChargeRepo.Update(charges);

 
                                }
                                else
                                {
                                    var charge = new PortCharge
                                    {
                                        DemurrageCharges = model.DemurrageCharges,
                                        WeighmentCharges = model.WeighmentCharges,
                                        OverWeightPenalty = model.OverWeightPenalty,
                                        DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal,
                                        ThcPhcKdlpCharges = model.ThcPhcKdlpCharges,
                                        LoloCharges = model.LoloCharges,
                                        QictCharges = model.QictCharges,
                                        OtherCharges = model.OtherCharges,
                                        WashAndCleanCharges = model.WashAndCleanCharges,
                                        VirNumber = model.VirNumber,
                                        IndexNumber = model.IndexNumber,
                                        ContainerNumber = model.ContainerNumber,
                                        ANF = model.ANF,
                                        Pallet = model.Pallet,
                                        Recover = model.Recover,
                                        TransportCharges = model.TransportCharges,
                                        TotalCharges = model.DemurrageCharges + model.WeighmentCharges + model.OverWeightPenalty + model.DetentionChargesOrBulletSeal
                                                        + model.ThcPhcKdlpCharges + model.LoloCharges + model.QictCharges + model.OtherCharges + model.WashAndCleanCharges
                                                        + model.ANF + model.Recover + model.Pallet + model.TransportCharges,

                                    };
                                    _portChargeRepo.Add(charge);
 
                                }

                                count += 1;

                            }
                             
                        }
                        else
                        {

                            return new JsonResult(new { error = true, message = "Index Not Found" });
                        }
                    }

                    else
                    {
                        return new JsonResult(new { error = false, message = "Total " + count + "Index Info Updated  and total " + countDelivered + " Are Deliverd" });
                    }



                }

                return new JsonResult(new { error = false, message = "Total " + count + "Index Info Updated  and total " + countDelivered + " Are Deliverd" });


            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }

        #endregion


        #region Port Charges Container Wise


        public IActionResult PortChargesPerBoxView()
        {
            return View();
        }



        public IActionResult AddPortChargesContainerWise(List<PortChargesViewModel> data)
        {
            try
            {
                var totalDelivered = 0;
                var totalUnDelivered = 0;
                var totalUnDeliveredIndex = 0;

                foreach (var model in data)
                {
                    if (model.Type == "CFS")
                    {

                       

                        var containerindex = _containerIndexRepo.GetContainerIndexByIGMContainerNo(model.VirNumber, model.ContainerNumber).ToList();
                        if (containerindex.Count() > 0)
                        {

                            foreach (var item in containerindex)
                            {

                                totalUnDeliveredIndex = containerindex.Where(x => x.IsDelivered == false).Count();


                                if (item.IsDelivered == true)
                                {
                                    totalDelivered += 1;
                                }
                                else
                                {
                                    totalUnDelivered += 1;
                                }

                                if (item.IsDelivered == false)
                                {
                                    var charges = _portChargeRepo.GetPortChargesByIgmIndexContainer(item.VirNo  , item.IndexNo ?? 0 ,  item.ContainerNo );

                                    if (totalUnDeliveredIndex > 0)
                                    {
                                        if (charges != null)
                                        {
                                            charges.DemurrageCharges = model.DemurrageCharges > 0 ? Math.Round(model.DemurrageCharges / totalUnDeliveredIndex) : 0;
                                            charges.WeighmentCharges = model.WeighmentCharges > 0 ? Math.Round(model.WeighmentCharges / totalUnDeliveredIndex) : 0;
                                            charges.OverWeightPenalty = model.OverWeightPenalty > 0 ? Math.Round(model.OverWeightPenalty / totalUnDeliveredIndex) : 0;
                                            charges.DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal > 0 ? Math.Round(model.DetentionChargesOrBulletSeal / totalUnDeliveredIndex) : 0;
                                            charges.ThcPhcKdlpCharges = model.ThcPhcKdlpCharges > 0 ? Math.Round(model.ThcPhcKdlpCharges / totalUnDeliveredIndex) : 0;
                                            charges.LoloCharges = model.LoloCharges > 0 ? Math.Round(model.LoloCharges / totalUnDeliveredIndex) : 0;
                                            charges.QictCharges = model.QictCharges > 0 ? Math.Round(model.QictCharges / totalUnDeliveredIndex) : 0;
                                            charges.OtherCharges = model.OtherCharges > 0 ? Math.Round(model.OtherCharges / totalUnDeliveredIndex) : 0;
                                            charges.WashAndCleanCharges = model.WashAndCleanCharges > 0 ? Math.Round(model.WashAndCleanCharges / totalUnDeliveredIndex) : 0;
                                            charges.ANF = model.ANF > 0 ? Math.Round(model.ANF / totalUnDeliveredIndex) : 0;
                                            charges.Pallet = model.Pallet > 0 ? Math.Round(model.Pallet / totalUnDeliveredIndex) : 0;
                                            charges.Recover = model.Recover > 0 ? Math.Round(model.Recover / totalUnDeliveredIndex) : 0;
                                            charges.TransportCharges = model.TransportCharges > 0 ? Math.Round(model.TransportCharges / totalUnDeliveredIndex) : 0;

                                            charges.TotalCharges = model.DemurrageCharges + model.WeighmentCharges + model.OverWeightPenalty + model.DetentionChargesOrBulletSeal + model.ThcPhcKdlpCharges
                                                                                          + model.LoloCharges + model.QictCharges + model.OtherCharges + model.WashAndCleanCharges + model.ANF + model.Pallet + model.Recover
                                                                                          + model.TransportCharges;

                                            charges.TotalCharges = charges.TotalCharges > 0 ? Math.Round(charges.TotalCharges / totalUnDeliveredIndex) : 0;

                                            _portChargeRepo.Update(charges);


                                        }
                                        else
                                        {
                                            var charge = new PortCharge
                                            {
                                                DemurrageCharges = model.DemurrageCharges > 0 ? Math.Round(model.DemurrageCharges / totalUnDeliveredIndex) : 0,
                                                WeighmentCharges = model.WeighmentCharges > 0 ? Math.Round(model.WeighmentCharges / totalUnDeliveredIndex) : 0,
                                                OverWeightPenalty = model.OverWeightPenalty > 0 ? Math.Round(model.OverWeightPenalty / totalUnDeliveredIndex) : 0,
                                                DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal > 0 ? Math.Round(model.DetentionChargesOrBulletSeal / totalUnDeliveredIndex) : 0,
                                                ThcPhcKdlpCharges = model.ThcPhcKdlpCharges > 0 ? Math.Round(model.ThcPhcKdlpCharges / totalUnDeliveredIndex) : 0,
                                                LoloCharges = model.LoloCharges > 0 ? Math.Round(model.LoloCharges / totalUnDeliveredIndex) : 0,
                                                QictCharges = model.QictCharges > 0 ? Math.Round(model.QictCharges / totalUnDeliveredIndex) : 0,
                                                OtherCharges = model.OtherCharges > 0 ? Math.Round(model.OtherCharges / totalUnDeliveredIndex) : 0,
                                                WashAndCleanCharges = model.WashAndCleanCharges > 0 ? Math.Round(model.WashAndCleanCharges / totalUnDeliveredIndex) : 0,
                                                TotalCharges = model.DemurrageCharges + model.WeighmentCharges + model.OverWeightPenalty + model.DetentionChargesOrBulletSeal
                                                                + model.ThcPhcKdlpCharges + model.LoloCharges + model.QictCharges + model.OtherCharges + model.WashAndCleanCharges
                                                                + model.ANF + model.Recover + model.Pallet + model.TransportCharges,
                                                VirNumber = model.VirNumber,
                                                IndexNumber = item.IndexNo ?? 0,
                                                ContainerNumber = model.ContainerNumber,
                                                ANF = model.ANF > 0 ? Math.Round(model.ANF / totalUnDeliveredIndex) : 0,
                                                Pallet = model.Pallet > 0 ? Math.Round(model.Pallet / totalUnDeliveredIndex) : 0,
                                                Recover = model.Recover > 0 ? Math.Round(model.Recover / totalUnDeliveredIndex) : 0,
                                                TransportCharges = model.TransportCharges > 0 ? Math.Round(model.TransportCharges / totalUnDeliveredIndex) : 0

                                            };

                                            if (charge.TotalCharges > 0)
                                            {
                                                var tc = Math.Round(charge.TotalCharges / totalUnDeliveredIndex);
                                                charge.TotalCharges = tc;
                                            }

                                            _portChargeRepo.Add(charge);

                                        }
                                    }

                                }


                            }


                         }
                    }
                    if (model.Type == "CY")
                    {
                        var container = _containerIndexRepo.GetCYContainerByIGMContainerNo(model.VirNumber, model.ContainerNumber).ToList();
                        if (container.Count() > 0)
                        { 

                            foreach (var item in container)
                            {
                                totalUnDeliveredIndex = container.Where(x => x.IsDelivered == false).Count();

                                if (item.IsDelivered == true)
                                {
                                    totalDelivered += 1;
                                }
                                else
                                {
                                    totalUnDelivered += 1;
                                }

                                if (item.IsDelivered == false)
                                {
                                    var charges = _portChargeRepo.GetPortChargesByIgmIndexContainer(item.VIRNo , item.IndexNo ?? 0, item.ContainerNo  );

                                    if (totalUnDeliveredIndex > 0)
                                    {
                                        if (charges != null)
                                        {
                                            charges.DemurrageCharges = model.DemurrageCharges > 0 ? Math.Round(model.DemurrageCharges / totalUnDeliveredIndex) : 0;
                                            charges.WeighmentCharges = model.WeighmentCharges > 0 ? Math.Round(model.WeighmentCharges / totalUnDeliveredIndex) : 0;
                                            charges.OverWeightPenalty = model.OverWeightPenalty > 0 ? Math.Round(model.OverWeightPenalty / totalUnDeliveredIndex) : 0;
                                            charges.DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal > 0 ? Math.Round(model.DetentionChargesOrBulletSeal / totalUnDeliveredIndex) : 0;
                                            charges.ThcPhcKdlpCharges = model.ThcPhcKdlpCharges > 0 ? Math.Round(model.ThcPhcKdlpCharges / totalUnDeliveredIndex) : 0;
                                            charges.LoloCharges = model.LoloCharges > 0 ? Math.Round(model.LoloCharges / totalUnDeliveredIndex) : 0;
                                            charges.QictCharges = model.QictCharges > 0 ? Math.Round(model.QictCharges / totalUnDeliveredIndex) : 0;
                                            charges.OtherCharges = model.OtherCharges > 0 ? Math.Round(model.OtherCharges / totalUnDeliveredIndex) : 0;
                                            charges.WashAndCleanCharges = model.WashAndCleanCharges > 0 ? Math.Round(model.WashAndCleanCharges / totalUnDeliveredIndex) : 0;
                                            charges.ANF = model.ANF > 0 ? Math.Round(model.ANF / totalUnDeliveredIndex) : 0;
                                            charges.Pallet = model.Pallet > 0 ? Math.Round(model.Pallet / totalUnDeliveredIndex) : 0;
                                            charges.Recover = model.Recover > 0 ? Math.Round(model.Recover / totalUnDeliveredIndex) : 0;
                                            charges.TransportCharges = model.TransportCharges > 0 ? Math.Round(model.TransportCharges / totalUnDeliveredIndex) : 0;

                                            charges.TotalCharges = model.DemurrageCharges + model.WeighmentCharges + model.OverWeightPenalty + model.DetentionChargesOrBulletSeal + model.ThcPhcKdlpCharges
                                                                                          + model.LoloCharges + model.QictCharges + model.OtherCharges + model.WashAndCleanCharges + model.ANF + model.Pallet + model.Recover
                                                                                          + model.TransportCharges;

                                            charges.TotalCharges = charges.TotalCharges > 0 ? Math.Round(charges.TotalCharges / totalUnDeliveredIndex) : 0;

                                            _portChargeRepo.Update(charges);

                                        }
                                        else
                                        {
                                            var charge = new PortCharge
                                            {
                                                DemurrageCharges = model.DemurrageCharges > 0 ? Math.Round(model.DemurrageCharges / totalUnDeliveredIndex) : 0,
                                                WeighmentCharges = model.WeighmentCharges > 0 ? Math.Round(model.WeighmentCharges / totalUnDeliveredIndex) : 0,
                                                OverWeightPenalty = model.OverWeightPenalty > 0 ? Math.Round(model.OverWeightPenalty / totalUnDeliveredIndex) : 0,
                                                DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal > 0 ? Math.Round(model.DetentionChargesOrBulletSeal / totalUnDeliveredIndex) : 0,
                                                ThcPhcKdlpCharges = model.ThcPhcKdlpCharges > 0 ? Math.Round(model.ThcPhcKdlpCharges / totalUnDeliveredIndex) : 0,
                                                LoloCharges = model.LoloCharges > 0 ? Math.Round(model.LoloCharges / totalUnDeliveredIndex) : 0,
                                                QictCharges = model.QictCharges > 0 ? Math.Round(model.QictCharges / totalUnDeliveredIndex) : 0,
                                                OtherCharges = model.OtherCharges > 0 ? Math.Round(model.OtherCharges / totalUnDeliveredIndex) : 0,
                                                WashAndCleanCharges = model.WashAndCleanCharges > 0 ? Math.Round(model.WashAndCleanCharges / totalUnDeliveredIndex) : 0,
                                                TotalCharges = model.DemurrageCharges + model.WeighmentCharges + model.OverWeightPenalty + model.DetentionChargesOrBulletSeal
                                                                + model.ThcPhcKdlpCharges + model.LoloCharges + model.QictCharges + model.OtherCharges + model.WashAndCleanCharges
                                                                + model.ANF + model.Recover + model.Pallet + model.TransportCharges,
                                                VirNumber = model.VirNumber,
                                                IndexNumber = item.IndexNo ?? 0,
                                                ContainerNumber = model.ContainerNumber,
                                                ANF = model.ANF > 0 ? Math.Round(model.ANF / totalUnDeliveredIndex) : 0,
                                                Pallet = model.Pallet > 0 ? Math.Round(model.Pallet / totalUnDeliveredIndex) : 0,
                                                Recover = model.Recover > 0 ? Math.Round(model.Recover / totalUnDeliveredIndex) : 0,
                                                TransportCharges = model.TransportCharges > 0 ? Math.Round(model.TransportCharges / totalUnDeliveredIndex) : 0

                                            };


                                            if (charge.TotalCharges > 0)
                                            {
                                                var tc = Math.Round(charge.TotalCharges / totalUnDeliveredIndex);
                                                charge.TotalCharges = tc;
                                            }

                                            _portChargeRepo.Add(charge);

                                        }
                                    }

                                }


                            }


                         } 
                    }
                    else
                    {
                        return new JsonResult(new { error = true, message = "Type Not Found" });

                    }
                }

                return new JsonResult(new { error = false, message = "Total " + totalUnDelivered + "Continer Info Updated  and total " + totalDelivered + " Are Deliverd" });



            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }

        #endregion

        #region Line Seal Amount
        public IActionResult LineSealAmountView()
        {
            return View();
        }

        public JsonResult GetLineSealAmounts()
        {
            var res = _lineSealAmountRepo.GetAll().ToList();
            return Json(res);
        }


        [HttpPost]
        public IActionResult AddLineSealAmount(LineSealAmount model)
        {


            try
            {
                var data = _lineSealAmountRepo.GetAll().Where(x => x.ShippingAgentId == model.ShippingAgentId).LastOrDefault();

                if(data != null)
                {
                    return new OkObjectResult(new { error = true, Message = "already amount assign" });

                }
                else
                {
                    _lineSealAmountRepo.Add(model);
                    return new OkObjectResult(new { error = false   , Message = "Save" });

                }

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            }



        }


        public IActionResult DeleteSealLineAmount(long key)
        {
            try
            {
                
                var res = _lineSealAmountRepo.Find(key);
                if(res != null)
                {

                    _lineSealAmountRepo.Delete(res);

                    return new OkObjectResult(new { error = false, Message = "Delete" });

                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "not found" });

                }


            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });

            }
        }


        #endregion


        #region Genereal Seal Amount
         
        public IActionResult GenerealSealAmountView()
        { 
            return View();
        }

        public JsonResult GetGenerealSealAmountView()
        {
            var data = _generealSealAmountRepo.GetFirst();
            return Json(data);
        }

        [HttpPost]
        public IActionResult UpdateGeneralSealAmount(long amount )
        {
            try
            {
                var value = _generealSealAmountRepo.GetFirst();
                if(value!= null)
                {

                    value.Amount = amount;
                    _generealSealAmountRepo.Update(value);
                    return new OkObjectResult(new { error = false, Message = "Update" });
                }
                else
                {
                    var model = new GenerealSealAmount
                    {
                        Amount = amount
                    }; 
                    _generealSealAmountRepo.Add(model);
                    return new OkObjectResult(new { error = false, Message = "Add" });
                }

            }
            catch (Exception e)
            {

                 return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });

            }
            
        }
        #endregion


        #region Examination Charge
        public IActionResult ExaminationChargeView()
        {
            return View();
        }

        public JsonResult GetExaminationCharges()
        {
            var Charges = _examinationChargeRepo.GetAll();
              
            return Json(Charges);
             
        }


        [HttpPost]
        public IActionResult AddExaminationCharges(ExaminationCharge data)
        {
            try
            {

                _examinationChargeRepo.Add(data);

                return new OkObjectResult(new { error = false, Message = "Saved" });
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message});
            }
        }

        [HttpPost]
        public IActionResult UpdateExaminationCharges(ExaminationCharge data)
        {
            
            try
            {

                _examinationChargeRepo.Update(data);
                return new OkObjectResult(new { error = false, Message = "Saved" });

            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            }
        }




        #endregion


        #region Examination Charge Assigning

          
        public JsonResult GetExaminationChargeAssignCY(string igm, int index)
        {


            var cyContainer = _cycontainerRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == index);

            if (cyContainer != null)
            {
                var ExaminationChargeAssignings = _examinationChargeAssignRepos.GetAll().Where(x => x.CYContainerId == cyContainer.CYContainerId).ToList();

                if (ExaminationChargeAssignings != null)
                {
                    return Json(ExaminationChargeAssignings);
                }

            }



            return Json("");

        }

         
        [HttpPost]
        public IActionResult AddExaminationChargeAssign(string type , string igm, int index, long examinationChargeId, ExaminationChargeAssign data)
        {
            try
            {
                if(type == "CFS")
                {
                    return new OkObjectResult(new { error = true, Message = "only for cy" });

                }

                //if(type == "CY")
                //{
                //    var topindexes = _cycontainerRepo.GetContainerIndexByIGMAndContainerNo(igm, index ).ToList();

                //    if (topindexes.Count() == 0)
                //    {
                //        return new OkObjectResult(new { error = true, message = "no index found" });

                //    }
                //    var containerindexidlist = string.Join(",", topindexes.Select(n => n.CYContainerId.ToString()).ToArray());

                //    var invoiceres = _invoiceRepo.GetCYInvoices(containerindexidlist).ToList();

                //    if (invoiceres.Count() > 0)
                //    {
                //        return new JsonResult(new { error = true, message = "invoice created agains't IGM : " + igm + " and INDEX " + index });
                //    }
                //}
                var cyContainer = _cycontainerRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == index);

                if (cyContainer != null)
                {

                    if(cyContainer.ShippingAgentId != null)
                    {
                        var shippingagnet = _shippingAgentRepository.GetAll().Where(x => x.ShippingAgentId == cyContainer.ShippingAgentId).LastOrDefault();

                        if(shippingagnet != null)
                        {

                            if (cyContainer.IsDelivered == true)
                            {
                                return new OkObjectResult(new { error = true, Message = "Container Mark Delivered " });
                            }
                            else
                            {
                                //if (data.ChargeQuantity != 0)
                                //{
                                    data.ChargeAmount20 = data.ChargeAmount20 * data.ChargeQuantity;
                                    data.ChargeAmount40 = data.ChargeAmount40 * data.ChargeQuantity40;
                                    data.ChargeAmount45 = data.ChargeAmount45 * data.ChargeQuantity45;
                                //}

                                //if (data.ChargeQuantity == 0)
                                //{
                                //    data.ChargeQuantity = 1;
                                //    data.ChargeAmount20 = data.ChargeAmount20 * data.ChargeQuantity;
                                //    data.ChargeAmount40 = data.ChargeAmount40 * data.ChargeQuantity;
                                //    data.ChargeAmount45 = data.ChargeAmount45 * data.ChargeQuantity;
                                //}

                                data.CYContainerId = cyContainer.CYContainerId;

                                _examinationChargeAssignRepos.Add(data);

                                if(shippingagnet.AllowExaminationAutoChrges == "Yes")
                                {

                                    var autoexamintionchrges = _autoExaminationChargeRepository.GetAll().Where(x => x.ExaminationChargeId == examinationChargeId).ToList();

                                    if (autoexamintionchrges.Count() > 0)
                                    {
                                        var resdataList = new List<ExaminationChargeAssign>();
                                        foreach (var item in autoexamintionchrges)
                                        {

                                            //if (data.ChargeQuantity != 0)
                                            //{
                                                var resdata = new ExaminationChargeAssign
                                                {

                                                    ChargeAmount20 = item.ExaminationChargeAmount20 * data.ChargeQuantity,
                                                    ChargeAmount40 = item.ExaminationChargeAmount40 * data.ChargeQuantity40,
                                                    ChargeAmount45 = item.ExaminationChargeAmount45 * data.ChargeQuantity45,
                                                    ChargeQuantity = data.ChargeQuantity,
                                                    ChargeQuantity40 = data.ChargeQuantity40,
                                                    ChargeQuantity45 = data.ChargeQuantity45,
                                                    ChargeName = item.AutoExaminationChargeName,
                                                    Remarks = data.Remarks,
                                                    CYContainerId = cyContainer.CYContainerId


                                                };

                                                resdataList.Add(resdata);
                                            //}

                                            //if (data.ChargeQuantity == 0)
                                            //{
                                            //    data.ChargeQuantity = 1;
                                            //    var resdata = new ExaminationChargeAssign
                                            //    {

                                            //        ChargeAmount20 = item.ExaminationChargeAmount20 * data.ChargeQuantity,
                                            //        ChargeAmount40 = item.ExaminationChargeAmount40 * data.ChargeQuantity,
                                            //        ChargeAmount45 = item.ExaminationChargeAmount45 * data.ChargeQuantity,
                                            //        ChargeQuantity = data.ChargeQuantity,
                                            //        ChargeName = item.AutoExaminationChargeName,
                                            //        Remarks = data.Remarks,
                                            //        CYContainerId = cyContainer.CYContainerId

                                            //    };

                                            //    resdataList.Add(resdata);
                                            //}


                                        }

                                        _examinationChargeAssignRepos.AddRange(resdataList);

                                    }


                                    return new OkObjectResult(new { error = false, Message = "Saved" });
                                }

                                return new OkObjectResult(new { error = false, Message = "Saved" });
                            }
                        }
                        else
                        {
                            return new OkObjectResult(new { error = true, Message = "FF Not AAvaible " });
                        }

                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, Message = "FF Not Define " });
                    }

                   

                }
                else
                {
                    return new OkObjectResult(new { error = false, Message = "Index Not Found" });

                }

            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });

            }
           




        }


        public IActionResult DeleteexaminationChargeAssigned(long key)
        {
            try
            {

                var data = _examinationChargeAssignRepos.Find(key);

                if (data != null)
                {
                    if (data.CYContainerId != 0)
                    {
                        var cycontainer = _cycontainerRepo.GetContainerById(data.CYContainerId);

                        if(cycontainer != null)
                        {
                            if (cycontainer.IsDelivered == true)
                            {
                                return new OkObjectResult(new { error = true, Message = "Container Mark Delivered" });
                            }
                            else {


                                var inv = _invoiceRepo.GetAll().Where(x => x.CYContainerId == data.CYContainerId).FirstOrDefault();

                                if (inv != null)
                                {

                                    return new OkObjectResult(new { error = true, Message = "not delete due to invoice created" });
                                }

                                _examinationChargeAssignRepos.Delete(data);

                                return new OkObjectResult(new { error = false, Message = "Deleted" });

                            }
                          
                        }
                        else
                        {
                            return new OkObjectResult(new { error = true, Message = "no container found" });
                        }

                        
                         
                    }

                }

                return new OkObjectResult(new { error = true, Message = "not found" });

            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });

            }


        }


        #endregion



        #region ISO Code Head
        public IActionResult ISOCodeHeadView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetISOCodeHeads()
        {
            var heads = _ISOCodeHeadRepository.GetAll();
            return Json(heads);
        }

        [HttpPost]
        public IActionResult AddISOCodeHead(ISOCodeHead data)
        {
            _ISOCodeHeadRepository.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdateISOCodeHeads(ISOCodeHead data)
        {
            _ISOCodeHeadRepository.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        #endregion


        #region Grounding Free Days
        public IActionResult GroundingFreeDaysView()
        {
            ViewData["ShippingAgent"] = _shippingAgentRepository.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["ClearingAgent"] = _clearingAgentRepository.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ClearingAgentId.ToString() });

            ViewData["GoodSHead"] = _goodsHeadRepo.GetAll()
                   .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });


            return View();
        }

        [HttpGet]
        public JsonResult GetGroundingFreeDays()
        {
            //var res = _groundingFreeDayRepository.GetGroundingFreedays();
            var res = _groundingFreeDayRepository.GetAll();
            return Json(res);
        }

        [HttpPost]
        public IActionResult AddGroundingFreeDays(GroundingFreeDay data)
        {

            var res = _groundingFreeDayRepository.GetAll().Where(x => x.ShippingAgentId == data.ShippingAgentId && x.ConsigneId == data.ConsigneId && x.ClearingAgentId == data.ClearingAgentId && x.GoodsHeadId == data.GoodsHeadId  && x.CargoType == data.CargoType).LastOrDefault();

            if (res != null)
            {
                return new OkObjectResult(new { error = true, Message = "already avaible" });

            }
            _groundingFreeDayRepository.Add(data);
             

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdateGroundingFreeDays(GroundingFreeDay data)
        {

            _groundingFreeDayRepository.Update(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }


        public IActionResult DeleteGroundingFreeDays(long key)
        {

            try
            {
                var res = _groundingFreeDayRepository.GetAll().Where(x => x.GroundingFreeDayId == key).LastOrDefault();
                if (res != null)
                {

                    _groundingFreeDayRepository.Delete(res);
                        return new OkObjectResult(new { error = false, Message = "Update" });
                     
                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "result not found" });
                }
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });

            }

        }


        public IActionResult GroundingFreeDaysUpdate(GroundingFreeDay model)
        {

            try
            {
                var res = _groundingFreeDayRepository.GetAll().Where(x => x.GroundingFreeDayId == model.GroundingFreeDayId).LastOrDefault();
                if (res != null)
                {
                    res.CargoType = model.CargoType;
                    res.ClearingAgentId = model.ClearingAgentId;
                    res.ConsigneId = model.ConsigneId;
                    res.GoodsHeadId = model.GoodsHeadId;
                    res.GroundingFreeDays = model.GroundingFreeDays;
                    res.ShippingAgentId = model.ShippingAgentId;
                    res.StorageFreeFreeDays = model.StorageFreeFreeDays;


                    _groundingFreeDayRepository.Update(res);
                    return new OkObjectResult(new { error = false, Message = "Update" });

                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "result not found" });
                }
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });

            }

        }



        #endregion


        #region BRT Location
        public IActionResult BRTLocationView()
        {
             

            return View();
        }

        [HttpGet]
        public JsonResult GetBRTLocations()
        {
            var brts = _brtLocationRepository.GetAll();
            return Json(brts);
        }

        [HttpPost]
        public IActionResult AddBRTLocation(BRTLocation data)
        {
            _brtLocationRepository.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdateBRTLocation(BRTLocation data)
        {
            _brtLocationRepository.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        #endregion




        #region Off Hire Location
        public IActionResult OffHireLocationView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetOffHireLocations()
        {
            var res = _offHireLocationRepository.GetAll();
            return Json(res);
        }

        [HttpPost]
        public IActionResult AddOffHireLocation(OffHireLocation data)
        {
            var res = _offHireLocationRepository.GetAll().Where(x => x.OffHireLocationName == data.OffHireLocationName).LastOrDefault();

            if(res != null)
            {
                return new OkObjectResult(new { error = true, Message = "Already Created" });
            }

            else
            {
                _offHireLocationRepository.Add(data);

                return new OkObjectResult(new { error = false, Message = "Saved" });

            }
            
        }

        [HttpPost]
        public IActionResult UpdateOffHireLocation(OffHireLocation data)
        {
            _offHireLocationRepository.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        #endregion


        #region Empty Container Sale Tax And Free days

        public IActionResult EmptyContainerSaleTaxAndFreedaysView()
        {

            ViewData["ShippingAgent"] = _shippingAgentRepository.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }



        [HttpGet]
        public JsonResult GetEmptyContainerSaleTaxAndFreeday(long shippingagentId)
        {
            var data = _emptyContainerTaxAndFreeDayRepository.GetAll().Where(x=> x.ShippingAgentId == shippingagentId).LastOrDefault();


            return Json(data);
        }


        [HttpPost]
        public IActionResult UpdateEmptyContainerSaleTaxAndFreeday(long shippingagentId, long days, long tax)
        {
            try
            {
                var data = _emptyContainerTaxAndFreeDayRepository.GetAll().Where(x => x.ShippingAgentId == shippingagentId).LastOrDefault();

                if (data != null)
                {
                    data.FreeDays = days;
                    data.SalesTax = tax; 
                    _emptyContainerTaxAndFreeDayRepository.Update(data);

                    return new OkObjectResult(new { error = false, Message = "Update" });

                }
                else
                {
                    var res = new EmptyContainerTaxAndFreeDay
                    {
                        FreeDays = days,
                        SalesTax = tax, 
                        ShippingAgentId = shippingagentId
                    };
                    _emptyContainerTaxAndFreeDayRepository.Add(res);

                    return new OkObjectResult(new { error = false, Message = "Saved" });


                }

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = "Please try again" });
            }


            return Ok();
        }

        #endregion



        #region Credit Allowed
        public IActionResult CreditAllowedView()
        {
             
            return View();
             
        }

        public IActionResult CreditRefoundView()
        {

            ViewData["Banks"] = _bankRepository.GetAll()
                .Select(v => new SelectListItem { Text = $"{v.BankName} -- {v.BankCode} -- {v.AccountNo} ", Value = v.BankId.ToString() });


            //ViewData["UnSettledCreditAllow"] = _creditAllowedRepository.GetUnSettledCreditAllow() for multi selection; 

            ViewData["UnSettledCreditAllow"] = _paymentReceivedRepository.GetAll().Where(x=> x.IsSettle == false && x.NatureOfAmount == "CreditAllowed" && x.CreditAllowed > 0)
                    .Select(v => new SelectListItem { Text = v.InoviceNo , Value = v.PaymentReceivedId.ToString() });
            
            

            //paymentReceived.IsSettle == false
            //          && paymentReceived.NatureOfAmount == "CreditAllowed"
            //          && paymentReceived.CreditAllowed > 0
            //          && invoice.ContainerIndexId != null

            return View();

        }

        public JsonResult GetUnSettledCreditAllow()
        {
            var UnSettledCreditAllow = _creditAllowedRepository.GetUnSettledCreditAllow();

            return Json(UnSettledCreditAllow);
        }

        public JsonResult GetPaymentReceivedById(long paymentReceivedIds)
        {

            var UnSettledCreditAllow = _creditAllowedRepository.GetPaymentReceivedById(paymentReceivedIds);

            return Json(UnSettledCreditAllow);
        }

        public JsonResult GetCreditAllowedAmount(string paymentReceivedIds)
        {

            var UnSettledCreditAllow = _creditAllowedRepository.GetCreditAllowedAmount(paymentReceivedIds);

            return Json(UnSettledCreditAllow);
        }


        public JsonResult GetCreditAllowRefoundSettlement(long paymentReceivedIds)
        {

            var UnSettledCreditAllow = _creditAllowedRepository.GetCreditAllowRefoundSettlement(paymentReceivedIds);

            return Json(UnSettledCreditAllow);
        }




        //[HttpGet]
        //public JsonResult GetBRTLocations()
        //{
        //    var brts = _brtLocationRepository.GetAll();
        //    return Json(brts);
        //}

        [HttpPost]
        public IActionResult AddCreditAllowed(CreditAllowed data)
        {
            var users = _usersEmailRepository.GetAll().Where(x=> x.Department == "CREDITALLOW").ToList();

            var currentdate = DateTime.Now;

            //var result = _creditAllowedRepository.GetAll().Where(x => x.VirNumber == data.VirNumber && x.IndexNumber == data.IndexNumber && x.InvoiceNo != null).LastOrDefault();

            //if(result != null)
            //{
            //    return new OkObjectResult(new { error = true, Message = "invoice Created can't change" });
            //} 


            var resdata = _creditAllowedRepository.GetCreditAllowDetail(data.VirNumber , data.IndexNumber).ToList();

            if(resdata.Count() > 0)
            {
                return new OkObjectResult(new { error = true, Message = "already assign" }); 
            }

            var creditAllow = _creditAllowedRepository.GetAll().Where(x => x.VirNumber == data.VirNumber && x.IndexNumber == data.IndexNumber && x.InvoiceNo == null && x.IsCancel == false && x.IsCancel == false && Convert.ToDateTime(x.CreatedDate.Value.ToString("MM/dd/yyyy")) < Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"))).LastOrDefault();

            if(creditAllow != null )
            {
                if (creditAllow.IsReject == true)
                {
                    return new OkObjectResult(new { error = true, Message = "your last  created credit Allow Reject by management" });

                }
                if (creditAllow.IsApprove == true)
                {
                    return new OkObjectResult(new { error = true, Message = "your last  created credit Allow Approve by management" });

                }
                if (creditAllow.IsCancel == true)
                {
                    return new OkObjectResult(new { error = true, Message = "your last  created credit Allow auto cancel due to change of days" });

                }
                creditAllow.CreatedDate = DateTime.Now;
                creditAllow.CreditAllowedRs = data.CreditAllowedRs;
                creditAllow.CreditDays = data.CreditDays;
                creditAllow.Remarks = data.Remarks;

                _creditAllowedRepository.Update(creditAllow);
                  
                _emailSender.SendBulkCompanyEmailAsync(users, "Credit Allow Alert! - " + data.VirNumber + '-' + data.IndexNumber, "IGM : " + data.VirNumber + " And Index " + data.IndexNumber + " has been updated  Credit Allow Amount : " + data.CreditAllowedRs);

                return new OkObjectResult(new { error = false, Message = "Saved" });

            }

            data.CreatedDate = DateTime.Now; 

            _emailSender.SendBulkCompanyEmailAsync(users, "Credit Allow Alert! - " + data.VirNumber + '-' + data.IndexNumber, "IGM : " + data.VirNumber + " And Index " + data.IndexNumber + " has been Added Credit Allow Amount : " + data.CreditAllowedRs);

            _creditAllowedRepository.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        //[HttpPost]
        //public IActionResult UpdateBRTLocation(BRTLocation data)
        //{
        //    _brtLocationRepository.Update(data);
        //    return new OkObjectResult(new { error = false, Message = "Saved" });
        //}

        //public IActionResult SaveCreditAllowRefoundSettlement(string paymentReceivedIds  , List<CreditAllowRefoundSettlement> PaymentNatureList)
        //{
        //    try
        //    {

        //        PaymentNatureList.ForEach(x => x.KnockOffAmount = x.ReceivedAmount);
        //        var paymentReceivedlist = new List<PaymentReceived>();

        //         var totalamountforsettlement = _creditAllowedRepository.GetUnSetteledRefund(paymentReceivedIds);

        //        var totalamountrefund = PaymentNatureList.Sum(x=> x.ReceivedAmount);

        //        if (totalamountforsettlement != totalamountrefund )
        //        {
        //            return new OkObjectResult(new { error = true, Message = "your refund amount are not  total collection" });
        //        }

        //        if (totalamountforsettlement > 0 && totalamountrefund > 0  )
        //        {

        //            var iNo = paymentReceivedIds.Split(",").ToList();

        //            foreach (var paymentReceivedId in iNo)
        //            {
        //                var respayment = _creditAllowedRepository.GetPaymentById(Convert.ToInt64(paymentReceivedId));

        //                if (respayment != null && respayment.ReceivedAmount > 0)
        //                {
        //                    var amount = respayment.ReceivedAmount;

        //                    var resdata = PaymentNatureList.Where(x => x.KnockOffAmount > 0);
        //                    foreach (var item in resdata)
        //                    {

        //                        if (amount > 0)
        //                        {
        //                            amount -= item.ReceivedAmount;
        //                            item.KnockOffAmount -= amount;

        //                            item.PaymentReceivedId = Convert.ToInt64(paymentReceivedId);

        //                            respayment.IsSettle = true;

        //                            paymentReceivedlist.Add(respayment);
        //                        }

        //                        if (amount < 0)
        //                        {
        //                            amount += item.ReceivedAmount;

        //                            item.ReceivedAmount  -= amount;
        //                            item.KnockOffAmount -= amount;

        //                            item.PaymentReceivedId = Convert.ToInt64(paymentReceivedId);

        //                            break;


        //                        }
        //                        if (amount == 0)
        //                        { 
        //                            break; 
        //                        }

        //                    }
        //                }

        //            }

        //            _creditAllowRefoundSettlementRepository.AddRange(PaymentNatureList);
        //            _paymentReceivedRepository.UpdateRange(paymentReceivedlist);


        //            return new OkObjectResult(new { error = false, Message = "Saved" });
        //        }

        //        else
        //        {
        //            return new OkObjectResult(new { error = true, Message = "your refund amount or payment  <= 0 " });
        //        }



        //    }
        //    catch (Exception e)
        //    {
        //        return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
        //    }
        //}


        public IActionResult SaveCreditAllowRefoundSettlement(long paymentReceivedIds, List<CreditAllowRefoundSettlement> PaymentNatureList)
        {
            try
            {

 
                var totalamountforsettlement = _creditAllowedRepository.GetPaymentReceivedById(paymentReceivedIds);

                if(totalamountforsettlement <= 0)
                {
                    return new OkObjectResult(new { error = true, Message = "your refund amount is less then 0" });
                } 

                var respayment = _creditAllowedRepository.GetPaymentById(paymentReceivedIds);
                if (respayment == null)
                {
                    return new OkObjectResult(new { error = true, Message = "no data found" });
                }
                else
                {
                    respayment.IsSettle = true;
                }

                var totalamountrefund = PaymentNatureList.Sum(x => x.ReceivedAmount);

                if (totalamountforsettlement != totalamountrefund)
                {
                    return new OkObjectResult(new { error = true, Message = "your refund amount are not equal to total collection" });
                }

                PaymentNatureList.ForEach(x => x.PaymentReceivedId = paymentReceivedIds);
                PaymentNatureList.ForEach(x => x.CreatedDate = DateTime.Now);
                 
                _creditAllowRefoundSettlementRepository.AddRange(PaymentNatureList);
                _paymentReceivedRepository.Update(respayment);


                return new OkObjectResult(new { error = false, Message = "Settled" });
                 
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            }
        }


        public IActionResult CreditAllowApproval()
        { 
            return View();
        }


        public JsonResult CreditAllowApprovalData()
        {
            var res = _creditAllowedRepository.GetCreditAllowDetail();
             
            return Json(res);
        }


        public IActionResult ApproveCreditAllow(long id)
        {
            var res = _creditAllowedRepository.GetAll().Where(x => x.CreditAllowedId == id).LastOrDefault();


            if (res != null)
            {

                if (Convert.ToDateTime(res.CreatedDate.Value.ToString("MM/dd/yyyy")) < Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy")))
                {
                    return new OkObjectResult(new { error = true, Message = "Created Date < then current date" });
                }

                if (res.IsCancel == true)
                {
                    return new OkObjectResult(new { error = true, Message = "Request Cancelled" });
                }
                if (res.IsSettled == true)
                {
                    return new OkObjectResult(new { error = true, Message = "Request Settled" });
                }
                if (res.InvoiceNo != null)
                {
                    return new OkObjectResult(new { error = true, Message = "Adjust Againtst " + res.InvoiceNo });
                }
                res.IsReject = false;
                res.IsApprove = true;

                _creditAllowedRepository.Update(res);

                return new OkObjectResult(new { error = true, Message = "Approved" });
            }
            else
            {
                return new OkObjectResult(new { error = true, Message = "not data found" });

            }

        }


        public IActionResult RejectCreditAllow(long id)
        {
            var res = _creditAllowedRepository.GetAll().Where(x => x.CreditAllowedId == id).LastOrDefault();


            if (res != null)
            {
                if (Convert.ToDateTime(res.CreatedDate.Value.ToString("MM/dd/yyyy")) < Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"))) {
                    return new OkObjectResult(new { error = true, Message = "Created Date < then current date" });
                }

                if (res.IsCancel == true )
                {
                    return new OkObjectResult(new { error = true, Message = "Request Cancelled" });
                }
                if (res.IsSettled == true)
                {
                    return new OkObjectResult(new { error = true, Message = "Request Settled" });
                }
                if (res.InvoiceNo != null)
                {
                    return new OkObjectResult(new { error = true, Message = "Adjust Againtst " + res.InvoiceNo });
                }
                res.IsReject = true;
                res.IsApprove = false;

                _creditAllowedRepository.Update(res);
                return new OkObjectResult(new { error = false, Message = "Rejected" });
            }
            else
            {
                return new OkObjectResult(new { error = true, Message = "not data found" });

            }

        }
        #endregion


        public IActionResult VIROManual()
        {


            return View();
        }


        public JsonResult GetVIROManual()
        {
            var res = _VIRORepository.GetMenualViros();

            return Json(res);
        }


        public IActionResult AddVIROManual(VIRO model)
        {
            try
            {

                var viro = _VIRORepository.GetMenualVirosbyigm(model.VIRNumber);

                if(viro != null)
                {
                    return new OkObjectResult(new { error = true, Message = "al ready avaible" });
                }

                model.VIRNumber = model.VIRNumber.ToUpper().Trim();
                model.VIRNumber = $"{model.VIRNumber.Substring(0, 4)}-{model.VIRNumber.Substring(4, 4)}-{model.VIRNumber.Substring(8, 8)}";


                model.MessageFrom = "MANUAL";
                model.MessageTo = "AIT";
                model.FileName = "MANUALData";
                model.CreateDT = DateTime.Now;
                model.Performed = DateTime.Now;
                model.OPType = "A";
                model.TotalRecords = 1;
                model.RecordSequence = 1;
                var igmYear = model.BerthOn != null ? model.BerthOn.Value.Year.ToString() : "";

                var vesselimport = new Vessel
                {
                    IGM = model.VIRNumber,
                    IGMYear = igmYear,
                    VesselName = model.VesselName
                };

                _vesselRepository.Add(vesselimport);

                var vesselId = vesselimport.VesselId;


                var voyageimport = new Voyage
                {
                    VesselId = vesselimport.VesselId,
                    VoyageNo = model.Voyage,
                    VIRNo = model.VIRNumber,
                    BerthOn = model.BerthOn,
                    BerthAt = model.BerthAt,


                };

                _voyageRepository.Add(voyageimport);
                _VIRORepository.Add(model);


                return new OkObjectResult(new { error = false, Message = "Saved" });
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            }
        }

        public IActionResult DeleteViro(long key)
        {
            try
            {  
                 
                var res = _VIRORepository.GetAll().Where(x => x.VIROId == key).LastOrDefault();

                if(res != null)
                { 
                    var vessel = _vesselRepository.GetAll().Where(x => x.IGM == res.VIRNumber).LastOrDefault();

                    if(vessel != null)
                    {


                        var voyage = _voyageRepository.GetAll().Where(x => x.VesselId == vessel.VesselId).LastOrDefault();

                        if(voyage != null)
                        {

                            _voyageRepository.Delete(voyage);
                            _vesselRepository.Delete(vessel);
                            _VIRORepository.Delete(res);

                            return new OkObjectResult(new { error = true, Message = "deleted" });
                        }

                        else
                        {

                             _vesselRepository.Delete(vessel);
                            _VIRORepository.Delete(res);

                            return new OkObjectResult(new { error = true, Message = "deleted" });
                        }
                    }
                    else
                    {
                        _VIRORepository.Delete(res);

                        return new OkObjectResult(new { error = true, Message = "deleted" });
                    }

                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "No Result Found" });
                }
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            }
        }


        public IActionResult IGMOManual()
        { 
            return View();
        }


        public JsonResult GetIGMOOManual()
        {
            var res = _IGMORepository.GetManualIgmoList();

            return Json(res);
        }


        public IActionResult AddIGMOManual(IGMO model)
        {
            try
            {
                model.VIRNumber = model.VIRNumber.ToUpper().Trim();
                model.VIRNumber = $"{model.VIRNumber.Substring(0, 4)}-{model.VIRNumber.Substring(4, 4)}-{model.VIRNumber.Substring(8, 8)}";
                model.TotalRecord = 1;
                model.Status = "CFS";
                model.CountryCode = "PK";
                model.DestinationCode = "AIT";
                model.RecordSequence = 1;
                model.MessageFrom = "MANUAL";
                model.MessageTo = "AIT";
                model.FileName = "MANUALData";
                model.CreateDT = DateTime.Now;
                model.Performed = DateTime.Now;
                model.OpType = "A";


                var containersize = model.ISOCode;
                var containercode = _IGMORepository.GetISOCodeBySize(model.ISOCode);

                if(containercode != null)
                {
                    model.ISOCode = containercode.Code;
                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "ISOCode not found" });
                }

                var viro = _VIRORepository.GetMenualVirosbyigm(model.VIRNumber);

                if(viro != null)
                {
                    var voyage = _VIRORepository.GetMenuaVoyagebyigm(model.VIRNumber);

                    if(voyage != null)
                    {

                        var igmo = _IGMORepository.GetIGMOInfo(model.VIRNumber, model.ContainerNumber, Convert.ToInt64( model.IndexNumber), model.BLNumber);

                        if(igmo == null)
                        { 

                            var Index = new ContainerIndex
                            {
                                ContainerNo = model.ContainerNumber,
                                ContainerGrossWeight = model.ContainerGrossWeight,
                                NoOfPackages = Convert.ToInt32(model.NumberofPackages),
                                Status = model.Status,
                                PackageType = model.PackageType,
                                VoyageId = voyage != null ? voyage.VoyageId : 0,
                                VirNo = model.VIRNumber,
                                ManifestedSealNumber = model.ManifestedSealNumber,
                                ISOCode = model.ISOCode,
                                Size = Convert.ToInt32(containersize),
                                BLGrossWeight = model.BLGrossWeight,
                                BLNo = model.BLNumber,
                                Description = model.Description,
                                IndexNo = model.IndexNumber,
                                MarksAndNumber = model.MarksAndNumber,
                                ShippingLine = model.ShippingLine,
                                IsGateIn = false,
                                IsHold = false,
                                IsDeleted = false,
                                IsDestuffed = false,
                                IsGrounded = false,
                                IsGateOut = false
                            };

                            _containerIndexRepo.Add(Index);
                            _IGMORepository.Add(model);
                             
                            return new OkObjectResult(new { error = false, Message = "Created" });
                        }
                        else
                        {
                            return new OkObjectResult(new { error = true, Message = "IGMO Already Avaible" });
                        }
                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, Message = "Voyage Not Avaible" });
                    }

                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "VIRO Not Avaible" });
                }
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            }
        }



        public IActionResult IPAOManual()
        {
            return View();
        }


        public JsonResult GetIPAOsManual()
        {
            var res = _iPAORepository.GetMANUALIPAOsList();

            return Json(res);
        }

        public IActionResult AddIPOAManual(IPAO model)
        {
            try
            {


                model.VIRNumber = model.VIRNumber.ToUpper().Trim();
                model.VIRNumber = $"{model.VIRNumber.Substring(0, 4)}-{model.VIRNumber.Substring(4, 4)}-{model.VIRNumber.Substring(8, 8)}";

                var ipaosres = _iPAORepository.GetMANUALIPAOsByIGMandContainer(model.VIRNumber, model.ContainerNumber);
                if(ipaosres.Count() > 0)
                {
                    return new OkObjectResult(new { error = true, Message = "Already Avaible" });
                }


                var igmores = _IGMORepository.GetMANUALIGMOByVirAndContainerno(model.VIRNumber, model.ContainerNumber);
                if (igmores == null)
                {
                    return new OkObjectResult(new { error = true, Message = "IGMO Not Avaible" });
                }

                model.TotalRecords = 1;
                model.RecordSequence = 1;
                model.OpType = "A";
                model.Performed = DateTime.Now;
                model.MessageFrom = "MANUAL";
                model.MessageTo = "AIT";
                model.FileName = "MANUALData";
                model.CreateDT = DateTime.Now;

                _iPAORepository.Add(model);

                return new OkObjectResult(new { error = false, Message = "Created" });
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            }
        }
         

        #region CRLO 
        public IActionResult CRLOManualView()
        {
            return View();
        }

        public JsonResult GetCRLOManual()
        {
            var res = _cRLORepository.GetMANUALCRLOList();

            return Json(res);
        }

        public IActionResult AddCRLOManual(CRLO model)
        {
            try
            {
                model.VIRNumber = model.VIRNumber.ToUpper().Trim();
                model.VIRNumber = $"{model.VIRNumber.Substring(0, 4)}-{model.VIRNumber.Substring(4, 4)}-{model.VIRNumber.Substring(8, 8)}";

                var ipaosres = _cRLORepository.GetMANUALICRLObyigmindexbl(model.VIRNumber, model.IndexNumber ?? 0 , model.BLNumber);
                if (ipaosres.Count() > 0)
                {
                    return new OkObjectResult(new { error = true, Message = "Already Avaible" });
                }
                 
                var igmores = _containerIndexRepo.GetContainerIndexByIgmAndIndex(model.VIRNumber, Convert.ToInt64(model.IndexNumber)).ToList();
                if (igmores.Count() == 0)
                {
                    return new OkObjectResult(new { error = true, Message = "IGMO Not Avaible" });
                }

                model.TotalRecords = 1;
                model.RecordSequence = 1;
                model.Category = "I";
                model.Status = "RF";
                model.DocumentCodes = "000";
                model.Performed = DateTime.Now;
                model.ReleaseReference = model.GDNumber;
                model.MessageFrom = "MANUAL";
                model.MessageTo = "AIT";
                model.FileName = "MANUALData";
                model.CreateDT = DateTime.Now;

                _cRLORepository.Add(model);

                return new OkObjectResult(new { error = false, Message = "Created" });
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            }
        }


        #endregion


        #region CRL
        public IActionResult CRLManualView()
        {
            return View();
        }

        public JsonResult GetCRLManual()
        {
            var res = _cRLRepository.GetMANUALCRLList();

            return Json(res);
        }

        public IActionResult AddCRLManual(CRL model)
        {
            try
            {
                model.VIRNumber = model.VIRNumber.ToUpper().Trim();
                model.VIRNumber = $"{model.VIRNumber.Substring(0, 4)}-{model.VIRNumber.Substring(4, 4)}-{model.VIRNumber.Substring(8, 8)}";

                var ipaosres = _cRLRepository.GetMANUALICRLbyigmcontainer(model.VIRNumber, model.ContainerNumber );
                if (ipaosres.Count() > 0)
                {
                    return new OkObjectResult(new { error = true, Message = "Already Avaible" });
                } 

                var igmores = _cycontainerRepo.GetCYContainersByIGMContainer(model.VIRNumber,  model.ContainerNumber ).ToList();

                var csigmores = _cycontainerRepo.GetCSContainerIndexByIGMAndContainerNo(model.VIRNumber, model.ContainerNumber).ToList();

                if (igmores.Count() == 0 && csigmores.Count() == 0)
                {
                    return new OkObjectResult(new { error = true, Message = "IGMO Not Avaible" });
                }

                model.TotalRecords = 1;
                model.RecordSequence = 1;
                model.Category = "I";
                model.Status = "RF";
                model.DocumentCodes = "000";
                model.Performed = DateTime.Now;
                model.FileName = "MANUALData";
                model.CreateDT = DateTime.Now;

                _cRLRepository.Add(model);

                return new OkObjectResult(new { error = false, Message = "Created" });
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            }
        }


        #endregion


        public IActionResult AutoExaminationChargesView()
        {
            return View();
        }


        public JsonResult GetAutoExaminationCharges()
        {
            var res = _autoExaminationChargeRepository.GetAll();

            return Json(res);
        }

        [HttpPost]
        public IActionResult AddAutoExaminationCharges(AutoExaminationCharge model)
        {
             
            _autoExaminationChargeRepository.Add(model);
              
            return new OkObjectResult(new { error = false, message = "Save" });

        }

        [HttpPost]
        public IActionResult UpdateAutoExaminationCharges(AutoExaminationCharge model)
        { 
            _autoExaminationChargeRepository.Update(model);

            return new OkObjectResult(new { error = false, message = "Updated" });

        }


        public void DeleteAutoExaminationCharges(long key)
        {

            var res = _autoExaminationChargeRepository.Find(key);
             
            if (res != null)
            {
                _autoExaminationChargeRepository.Delete(res);
                 
            }
 
        }

        #region Port Of loading

        public IActionResult PortOfLoadingView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPortOfLoading()
        {
            var banks = _portOfLoadingRepository.GetAll();
            return Json(banks);
        }

        [HttpPost]
        public IActionResult AddtPortOfLoading(PortOfLoading model)
        {
            try
            {
                _portOfLoadingRepository.Add(model);

                return new OkObjectResult(new { error = false, message = "Created" });
            }

            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }



        [HttpPost]
        public IActionResult UpdatePortOfLoading(PortOfLoading model)
        {
            _portOfLoadingRepository.Update(model);

            return new OkObjectResult(new { error = false, Message = "Update" });
        }


        #endregion

        #region Container Location

        public IActionResult ContainerLocationView()
        {
            return View();
        }

         
        public IActionResult SaveImportContainerLocation(string virno , string containerno , string type , string location)
        {
            try
            {
                var res = new ImportContainerLocation
                {
                    ContainerNo = containerno,
                    Virno = virno,
                    Type = type,
                    Location = location

                };

                _importContainerLocationRepository.Add(res);

                return new OkObjectResult(new { error = false, message = "Save" });
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = false, message = "please try againg" });

            }

         

        }


        #endregion


        #region Short Excess Weight

        public IActionResult ShortExcessWeightView()
        {
             
            return View();
        }


        public JsonResult GetShortExcessWeight(string virno, long indexno, string type)
        {

            var resdata = _shortExcessWeigthRepository.GetAll().Where(x => x.VirNumber == virno && x.IndexNumber == indexno && x.ContainerType == type).ToList();

            return Json(resdata);

        }


        public IActionResult AddShortExcessWeight(string VirNumber, long IndexNumber, string ContainerType, double ShortWeight , double ExcesstWeight , string Remarks)
        {
            try
            {
                //var res = _shortExcessWeigthRepository.GetAll().Where(x => x.VirNumber == VirNumber && x.IndexNumber == IndexNumber && x.ContainerType == ContainerType).LastOrDefault();

                //if(res != null)
                //{
                //    res.ShortWeight = ShortWeight;
                //    res.ExcesstWeight = ExcesstWeight;
                //    res.Remarks = Remarks;

                //    _shortExcessWeigthRepository.Update(res);
                //    return new OkObjectResult(new { error = false, Message = "save" });
                //}
                //else
                //{

                    var shortexcess = new ShortExcessWeigth
                    {
                        ContainerType = ContainerType,
                        ExcesstWeight = ExcesstWeight,
                        IndexNumber = IndexNumber,
                        Remarks = Remarks,
                        ShortWeight = ShortWeight,
                        VirNumber = VirNumber
                    };

                    _shortExcessWeigthRepository.Add(shortexcess);
                    return new OkObjectResult(new { error = false, Message = "save" });

                //}
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = "please try again" });

            }


        }

        #endregion


        #region Vehicle Measurement

        public IActionResult VehicleMeasurementView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetVehicleMeasurement()
        {
            var banks = _vehicleMeasurementRepository.GetAll();
            return Json(banks);
        }

        [HttpPost]
        public IActionResult AddtVehicleMeasurement(VehicleMeasurement model)
        {
            try
            {
                _vehicleMeasurementRepository.Add(model);

                return new OkObjectResult(new { error = false, message = "Created" });
            }

            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }



        [HttpPost]
        public IActionResult UpdateVehicleMeasurement(VehicleMeasurement model)
        {
            _vehicleMeasurementRepository.Update(model);

            return new OkObjectResult(new { error = false, Message = "Update" });
        }


        #endregion

        #region Hold Privilege

        public IActionResult HoldPrivilegeView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult HoldPrivilege()
        {
            var res = _holdPrivilegeRepository.GetAll();
            return Json(res);
        }

        public   JsonResult GetUsers()
        {
            var res   =   _userManager.Users;

            return Json(res);
        }
         
        [HttpPost]
        public IActionResult AddHoldPrivilegeView(HoldPrivilege model)
        {
            try
            {
                _holdPrivilegeRepository.Add(model);

                return new OkObjectResult(new { error = false, message = "Created" });
            }

            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }             
        }


        [HttpPost]
        public IActionResult UpdateHoldPrivilegeView(HoldPrivilege model)
        {
            try
            {
                var res = _holdPrivilegeRepository.GetAll().Where(x => x.HoldPrivilegeId == model.HoldPrivilegeId).LastOrDefault();

                if(res != null)
                {
                    res.IsActive = model.IsActive;

                    _holdPrivilegeRepository.Update(res);

                    return new OkObjectResult(new { error = false, message = "Updated" });

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no data found" });
                }

            }

            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }


        public void DeletePrivilege(long key)
        {

            var resdata = _holdPrivilegeRepository.Find(key);
             
            if (resdata != null)
            {  
                    _holdPrivilegeRepository.Delete(resdata);
                    

            }
             

        }



        #endregion


        #region Percent For Aict Billing To Line
        public IActionResult PercentForAictBillingToLineView()
        {
            ViewData["ShippingAgent"] = _shippingAgentRepository.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });
             
            return View();
        }

        [HttpGet]
        public JsonResult GetPercentForAictBillingToLine(long ShippingAgentId)
        { 
            var res = _percentForAictBillingToLineRepository.GetAll().Where(x=> x.ShippingAgentId == ShippingAgentId).OrderByDescending(x=> x.PercentForAictBillingToLineId).ToList();
            return Json(res);
        }

        [HttpPost]
        public IActionResult AddPercentForAictBillingToLine(PercentForAictBillingToLine data)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

                var appUser = _userRepo.GetFirst(e => e.IdentityUserId == userId);

                data.UserNmae = appUser.FirstName + " - " + appUser.LastName;
                data.PerformedDate = DateTime.Now;


                IPHostEntry ip = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = Convert.ToString(ip.AddressList.FirstOrDefault(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork));

                _percentForAictBillingToLineRepository.Add(data);
                return new OkObjectResult(new { error = false, Message = "Saved" });
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            }

            
        }

        #endregion

        #region Aict Billing To Line 

        public IActionResult AictBillingView()
        {
            ViewData["ShippingAgent"] = _shippingAgentRepository.GetAll()
            .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public JsonResult GetAictBillingView(long shippingAgentId, DateTime fromdate, DateTime todate)
        {
            var res = _invoiceRepo.GetAictBillingtoLine(shippingAgentId, fromdate, todate).ToList();

            return Json(res);
        }

        public IActionResult AddAictBillingToMSK(long ? shippingagentId , long? SaleTax  , List<AictBillingItem> model)
        {
            try
            {
                var aictbilling = new AictBilling();
                 
                if (_invoiceRepo.GetInvoiceByInvoiceNumber( model.FirstOrDefault().LineInvoiceno)  != null)
                {
                    aictbilling.FromDate = _invoiceRepo.GetInvoiceByInvoiceNumber( model.FirstOrDefault().LineInvoiceno).InvoiceDate  ;
                }

                if (_invoiceRepo.GetInvoiceByInvoiceNumber(model.LastOrDefault().LineInvoiceno) != null)
                {
                    aictbilling.ToDate = _invoiceRepo.GetInvoiceByInvoiceNumber(model.LastOrDefault().LineInvoiceno).InvoiceDate ;
                }
                 

                if (aictbilling.FromDate == null)
                {
                    return new OkObjectResult(new { error = false, Message = "please select from date" });
                }
                if(aictbilling.ToDate == null)
                {
                    return new OkObjectResult(new { error = false, Message = "please select to date" });
                }
                if(shippingagentId == null)
                {
                    return new OkObjectResult(new { error = false, Message = "please select Line / FF" });
                }
                if(SaleTax == null)
                {
                    return new OkObjectResult(new { error = false, Message = "please add SaleTax" });
                }
                if(model.Count() == 0)
                {
                    return new OkObjectResult(new { error = false, Message = "please select list items" });
                }

                model.ForEach(x => x.AICTBillingToLine = Math.Round(x.AICTBillingToLine));
                 
                aictbilling.ShippingAgentId = shippingagentId ?? 0;
                aictbilling.PerformedDate = DateTime.Now;
                aictbilling.BillingFrom = "DMC";
                aictbilling.SalesTax = SaleTax ?? 0;
                aictbilling.AictBillingNumber = GetLastInvoiceNoForFF(shippingagentId ?? 0);
                
                _aictBillingRepository.Add(aictbilling);

                  model.ForEach(x => x.AictBillingId = aictbilling.AictBillingId);

                _aictBillingItemRepository.AddRange(model);

                return new OkObjectResult(new { error = false, message = "created", InvoiceNo = aictbilling.AictBillingNumber});
                 
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message });
            }
            

        }


        public string GetLastInvoiceNoForFF(long ShippingAgentId)
        {

            var year = DateTime.Now.ToString("yyyy");
            var month = DateTime.Now.ToString("MM");

            var invo = _invoiceRepo.GetAictBillingNumberDetail(ShippingAgentId);

            if (invo != null)
            {
                if (invo.DeleteDate.ToString("MM") == month)
                {
                    var no = invo.AictBillingNumber.Substring(0, invo.AictBillingNumber.IndexOf("/"));

                    var number = Convert.ToInt64(no) + 1;

                    var InvoiceNo = number + "/" + month + "/" + year;

                    return InvoiceNo;
                }
                else
                {
                    var no = 1;

                    var InvoiceNo = no + "/" + month + "/" + year;

                    return InvoiceNo;
                }
            }
            else
            {
                var no = 1;

                var InvoiceNo = no + "/" + month + "/" + year;

                return InvoiceNo;

            }


        }

        #endregion

        #region Re print Aict Billing To Line

        public IActionResult RePrintAictBillingToLineView()
        {
            return View();
        }

        public JsonResult GetAictBillingList(DateTime fromdate, DateTime todate)
        {
            var res = _invoiceRepo.GetAictBillingList( fromdate, todate).ToList();

            return Json(res);
        }

        #endregion

        #region Settle Un Settle Invoices

        public IActionResult AictBillingSettleUnSettleInvoicesView()
        {
            ViewData["ShippingAgent"] = _shippingAgentRepository.GetAll()
           .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public JsonResult GetAictBillingSettleUnSettleInvoicesList(long ShippingAgentId , string nature, DateTime fromdate, DateTime todate)
        {
            var res = _invoiceRepo.GetAictBillingSettleUnSettleInvoices(ShippingAgentId , nature, fromdate, todate).ToList();

            return Json(res);
        }

        #endregion

        #region CR Summary Head Detail

        public IActionResult CRSummaryHeadDetailView()
        {
            return View();

        }

        [HttpGet]
        public JsonResult GetCRSummaryHeadDetail()
        {
            var res = _cRSummaryHeadDetailRepository.GetAll();
            return Json(res);
        }

         
        public JsonResult GetInvoiceItemHeadsDetail()
        {
            var res = _invoiceRepo.GetInvoiceItemHeadsDetail();

            return Json(res);
        }

        [HttpPost]
        public IActionResult AddCRSummaryHeadDetail(string name, string category , string caption , long serialno)
        {
            var resdata = _cRSummaryHeadDetailRepository.GetAll().Where(x => x.Description == name && x.Category ==  category).LastOrDefault();

            if(resdata != null)
            {
                return new OkObjectResult(new { error = true, Message =   name + " " + category + " already avaible" });
            }

            var resdatacaption = _cRSummaryHeadDetailRepository.GetAll().Where(x => x.Caption == caption).LastOrDefault();

            if (resdatacaption != null)
            {
                return new OkObjectResult(new { error = true, Message = caption + " already avaible" });
            }


            var res = _cRSummaryHeadDetailRepository.GetAll().Where(x => x.SerialNo == serialno).LastOrDefault();
            if(res == null)
            {
                if(category == "Storage")
                {
                    var datares = new CRSummaryHeadDetail()
                    {
                        Category = category,
                        Description = name,
                        Caption = caption.Trim(),
                        SerialNo = serialno,
                        Query = "CONVERT(varchar, CAST(     isnull ((sum(case when   w.Category ='" + category + "'  then  w.Amount else 0  end ) + sum(case when   x.Category ='" + category + "'   then  x.Discount else 0  end )) , 0)     AS money), 1) as  " + caption + " ,"
                    };
                    _cRSummaryHeadDetailRepository.Add(datares);

                    return new OkObjectResult(new { error = false, Message = "Saved" });
                }
                else
                {
                    var datares = new CRSummaryHeadDetail()
                    {
                        Category = category,
                        Description = name,
                        Caption = caption.Trim(),
                        SerialNo = serialno,
                        Query = "CONVERT(varchar, CAST(     isnull ((sum(case when   w.Category ='" + category + "'and  w.Description ='" + name + "' then  w.Amount else 0  end ) + sum(case when   x.Category ='" + category + "'and  x.Description ='" + name + "' then  x.Discount else 0  end )) , 0)        AS money), 1) as  " + caption + " ,"
                    };
                    _cRSummaryHeadDetailRepository.Add(datares);

                    return new OkObjectResult(new { error = false, Message = "Saved" });
                }

           
            }
            else
            {
                return new OkObjectResult(new { error = true, Message = "SerialNo " + serialno + " already avaible" });
            }
            

            
        }

        [HttpPost]
        public IActionResult UpdateCRSummaryHeadDetail(string name , string category  , string caption, long crsummaryHeadDetailId)
        {
            var res = _cRSummaryHeadDetailRepository.GetAll().Where(x => x.CRSummaryHeadDetailId == crsummaryHeadDetailId).LastOrDefault();
            if(res != null)
            {
                if (category == "Storage")
                {
                    res.Query = "CONVERT(varchar, CAST(       isnull ((sum(case when   w.Category ='" + category + "'  then  w.Amount else 0  end ) + sum(case when   x.Category ='" + category + "'   then  x.Discount else 0  end )) , 0)       AS money), 1) as  " + caption + " ,";
                }
                else
                {
                    res.Query = "CONVERT(varchar, CAST(       isnull ((sum(case when   w.Category ='" + category + "'and  w.Description ='" + name + "' then  w.Amount else 0  end ) + sum(case when   x.Category ='" + category + "'and  x.Description ='" + name + "' then  x.Discount else 0  end )) , 0)      AS money), 1) as  " + caption +" ,";
                }
                res.Category = category;
                res.Description = name;
                res.Caption = caption.Trim();

                _cRSummaryHeadDetailRepository.Update(res);

                return new OkObjectResult(new { error = false, Message = "Updated" });

            }
            else
            {
                return new OkObjectResult(new { error = true, Message = "no record found" });

            }
            
        }

        public IActionResult DeleteCRSummaryHeadDetail(long crsummaryHeadDetailId)
        {
            var res = _cRSummaryHeadDetailRepository.GetAll().Where(x => x.CRSummaryHeadDetailId == crsummaryHeadDetailId).LastOrDefault();
            if (res != null)
            { 
                _cRSummaryHeadDetailRepository.Delete(res);

                return new OkObjectResult(new { error = false, Message = "Updated" });

            }
            else
            {
                return new OkObjectResult(new { error = true, Message = "no record found" });
            }

        }
        #endregion


    }
}
