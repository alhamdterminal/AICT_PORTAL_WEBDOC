using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Controllers
{
    public static class SystemExtension
    {
        public static T Clone<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }


    [MyAuthorize]
    public class InvoiceController : Controller
    {
        private IVesselRepository _vesselRepo;
        private IShippingAgentRepository _agentRepo;
        private IClearingAgentRepository _clearingAgentRepo;
        private IInvoiceRepository _invoiceRepo;
        private IInvoiceDocumentRepository _invoiceDocRepo;
        private IHostingEnvironment env;
        private IContainerIndexRepository _containerIndexRepo;
        private ICYContainerRepository _cyContainerRepo;
        private IAmountReceivedRepository _amountReceiveRepo;
        private ICRLORepository _crloRepo;
        private ICRLRepository _crlRepo;
        private IPartyLedgerRepository _partyLedgerRepo;
        private IPartyRepository _partyRepo;
        private IHoldRepository _holdrepo;
        private IClearingAgentExportRepository _clearingAgentExportRepo;
        private IDeliveryOrderRepository _deliveryOrderRepository;
        private IWaiverRepository _waiverRepo;
        private IExaminationRequestRepository _examinationRequestRepo;
        private IBillToLineRepository _billToLineRepository;
        private IOtherChargeAssigningRepository _otherChargeAssigningRepo;
        private IGoodsHeadRepository _goodsHeadRepository;
        private IShippingLineRepository _shippingLineRepo;
        private IShipperRepository _shipperRepo;
        private IPortAndTerminalRepository _portAndTerminalRepo;
        private IInvoiceInquiryRepository _invoiceInquiryRepo;
        private ISIMORepository _simoRepository;
        private ISIMRepository _simRepository;
        private IExaminationChargeRepository _examinationChargeRepository;
        private IExaminationTariffRepository _examinationTariffRepository;
        private IExaminationTariffInformationRepository _examinationTariffInformationRepository;
        private IExaminationTariffInformationTariffRepository _examinationTariffInformationTariffRepository;
        private IEmptyContainerReceivedRepository _emptyContainerReceivedRepository;
        private IEmptyContainerInvoiceRepository _emptyContainerInvoiceRepository;
        private IEmptyContainerDeliveryOrderRepository _emptyContainerDeliveryOrderRepository;
        private IAICTAndLineIndexTariffRepository _aictAndLineIndexTariffRepository;
        private IBankRepository _bankRepository;
        private IPaymentReceivedRepository _paymentReceivedRepository;
        private ICreditAllowedRepository _creditAllowedRepository;
        private IExcessAmountRefundSettlementRepository _excessAmountRefundSettlementRepo;
        private IScheduleOfChargeRepository _scheduleOfChargeRepository;
        private IContainerRepository _containerRepository;
        private IDestuffedContainerRepository  _destuffedContainerRepository;
        private IConsigneRepository _consigneRepository;
        private IAuctionRepository _auctionRepository;

 
        public InvoiceController(IVesselRepository vesselRepo,
            ICRLORepository crloRepo,
            ICRLRepository crlRepo,
            IShippingAgentRepository agentRepo, IClearingAgentRepository clearingAgentRepo,
            IInvoiceRepository invoiceRepo, ICYContainerRepository cyContainerRepo, IInvoiceDocumentRepository invoiceDocRepo, IHostingEnvironment _env, IContainerIndexRepository containerIndexRepo,
            IAmountReceivedRepository amountReceiveRepo, IPartyLedgerRepository partyLedgerRepo, IHoldRepository holdrepo,
            IClearingAgentExportRepository clearingAgentExportRepo , IDeliveryOrderRepository deliveryOrderRepository,
            IWaiverRepository waiverRepo,
            IExaminationRequestRepository examinationRequestRepo,
            IBillToLineRepository billToLineRepository,
            IOtherChargeAssigningRepository otherChargeAssigningRepo,
            IPartyRepository partyRepo,
            IGoodsHeadRepository goodsHeadRepository,
            IShippingLineRepository shippingLineRepo,
            IShipperRepository shipperRepo,
            IPortAndTerminalRepository portAndTerminalRepo,
            IInvoiceInquiryRepository invoiceInquiryRepo,
            ISIMORepository simoRepository,
            ISIMRepository simRepository,
            IExaminationChargeRepository examinationChargeRepository,
            IExaminationTariffRepository examinationTariffRepository,
            IExaminationTariffInformationRepository examinationTariffInformationRepository,
            IExaminationTariffInformationTariffRepository examinationTariffInformationTariffRepository,
            IEmptyContainerReceivedRepository emptyContainerReceivedRepository,
            IEmptyContainerInvoiceRepository emptyContainerInvoiceRepository,
            IEmptyContainerDeliveryOrderRepository emptyContainerDeliveryOrderRepository,
            IAICTAndLineIndexTariffRepository aictAndLineIndexTariffRepository,
            IBankRepository bankRepository,
            IPaymentReceivedRepository paymentReceivedRepository,
            ICreditAllowedRepository creditAllowedRepository,
            IExcessAmountRefundSettlementRepository excessAmountRefundSettlementRepo,
            IScheduleOfChargeRepository scheduleOfChargeRepository,
            IContainerRepository containerRepository,
            IDestuffedContainerRepository destuffedContainerRepository,
            IConsigneRepository consigneRepository,
            IAuctionRepository auctionRepository)
        {
            _vesselRepo = vesselRepo;
            _agentRepo = agentRepo;
            _crloRepo = crloRepo;
            _crlRepo = crlRepo;
            _clearingAgentRepo = clearingAgentRepo;
            _invoiceRepo = invoiceRepo;
            _invoiceDocRepo = invoiceDocRepo;
            env = _env;
            _containerIndexRepo = containerIndexRepo;
            _cyContainerRepo = cyContainerRepo;
            _amountReceiveRepo = amountReceiveRepo;
            _partyLedgerRepo = partyLedgerRepo;
            _holdrepo = holdrepo;
            _clearingAgentExportRepo = clearingAgentExportRepo;
            _deliveryOrderRepository = deliveryOrderRepository;
            _waiverRepo = waiverRepo;
            _examinationRequestRepo = examinationRequestRepo;
            _billToLineRepository = billToLineRepository;
            _otherChargeAssigningRepo = otherChargeAssigningRepo;
            _partyRepo = partyRepo;
            _goodsHeadRepository = goodsHeadRepository;
            _shippingLineRepo = shippingLineRepo;
            _shipperRepo = shipperRepo;
            _portAndTerminalRepo = portAndTerminalRepo;
            _invoiceInquiryRepo = invoiceInquiryRepo;
            _simoRepository = simoRepository;
            _simRepository = simRepository;
            _examinationChargeRepository = examinationChargeRepository;
            _examinationTariffRepository = examinationTariffRepository;
            _examinationTariffInformationRepository = examinationTariffInformationRepository;
            _examinationTariffInformationTariffRepository = examinationTariffInformationTariffRepository;
            _emptyContainerReceivedRepository = emptyContainerReceivedRepository;
            _emptyContainerInvoiceRepository = emptyContainerInvoiceRepository;
            _emptyContainerDeliveryOrderRepository = emptyContainerDeliveryOrderRepository;
            _aictAndLineIndexTariffRepository = aictAndLineIndexTariffRepository;
            _bankRepository = bankRepository;
            _paymentReceivedRepository = paymentReceivedRepository;
            _creditAllowedRepository = creditAllowedRepository;
            _excessAmountRefundSettlementRepo = excessAmountRefundSettlementRepo;
            _scheduleOfChargeRepository = scheduleOfChargeRepository;
            _containerRepository = containerRepository;
            _destuffedContainerRepository = destuffedContainerRepository;
            _consigneRepository = consigneRepository;
            _auctionRepository = auctionRepository;
        }


        public IActionResult ImportBill()
        {


            ViewData["ClearingAgentsAgents"] = _clearingAgentRepo.GetAll()
                .Select(v => new SelectListItem { Text = $"{v.Name}-{v.ChallanNumber} -- {v.LicenceNumber} ", Value = v.ClearingAgentId.ToString() });


            ViewData["Banks"] = _bankRepository.GetAll()
                .Select(v => new SelectListItem { Text = $"{v.BankName} -- {v.BankCode} -- {v.AccountNo} ", Value = v.BankId.ToString() });

            return View();
        }

        //public IActionResult ExportInvoice()
        //{
        //    ViewData["ClearingAgentsAgents"] = _clearingAgentExportRepo.GetAll()
        //        .Select(v => new SelectListItem { Text = v.ClearingAgentName, Value = v.ClearingAgentExportId.ToString() });

        //    return View();
        //}


        public IActionResult InvoiceiInquiry()
        {

            ViewData["ClearingAgentsAgents"] = _clearingAgentRepo.GetAll()
              .Select(v => new SelectListItem { Text = $"{v.Name}-{v.ChallanNumber} -- {v.LicenceNumber} ", Value = v.ClearingAgentId.ToString() });

            ViewData["Goods"] = _goodsHeadRepository.GetAll()
            .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });

            //ViewData["Consigne"] = _consigneRepo.GetAll()
            //  .Select(v => new SelectListItem { Text = v.ConsigneName, Value = v.ConsigneId.ToString() });

            ViewData["ShippingLine"] = _shippingLineRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.ShippingLineName, Value = v.ShippingLineId.ToString() });

            ViewData["ShippingAgent"] = _agentRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["Shipper"] = _shipperRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.ShipperName, Value = v.ShipperId.ToString() });

            ViewData["PortAndTerminal"] = _portAndTerminalRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.PortName, Value = v.PortAndTerminalId.ToString() });


            return View();

        }


        [HttpPost]
        public IActionResult SaveCapturedImage(string name, string BillNo)
        {
            var files = HttpContext.Request.Form.Files;
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        // Getting Filename  
                        var fileName = file.FileName;
                        // Unique filename "Guid"  
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                        // Getting Extension  
                        var fileExtension = Path.GetExtension(fileName);
                        // Concating filename + fileExtension (unique filename)  
                        var newFileName = string.Concat(name, myUniqueFileName, fileExtension);
                        //  Generating Path to store photo   
                        var filepath = Path.Combine(env.ContentRootPath, "CameraPhotos") + $@"\{newFileName}";

                        if (!string.IsNullOrEmpty(filepath))
                        {
                            // Storing Image in Folder  
                            StoreInFolder(file, filepath);

                            var invoice = _invoiceRepo.GetFirst(b => b.InvoiceNo == BillNo);

                            if (invoice != null)
                            {
                                //invoice.ImageUrl = filepath;
                                _invoiceRepo.Update(invoice);
                            }
                        }

                    }
                }
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        private void StoreInFolder(IFormFile file, string fileName)
        {
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        
        
        
        
        }


        public IActionResult CreateInvoice(Invoice invoice, IList<InvoiceDocument> invoiceDoc, string igm, long indexNo , string type)
        {
            IList<InvoiceDocument> docs = new List<InvoiceDocument>();

            var invo = _invoiceRepo.GetAll();
            var inv = invo.LastOrDefault();
          //  invoice.InvoiceNo = inv != null ? inv.InvoiceNo + 1 : 1;
            invoice.InvoiceDate = DateTime.Now;
            invoice.IsCancelled = false;
            //invoice.InvoiceType = "I";

           
            if (type == "CFS")
            {
                invoice.CYContainerId = null;

                var containerindex = _containerIndexRepo.Find(invoice.ContainerIndexId);

                if (containerindex.IsDestuffed == false)
                {
                    return new OkObjectResult(new { error = true, message = "This container is not Destuff" });
                }
                 
                if (containerindex.IsHold == true)
                {

                    return new OkObjectResult(new { error = true, message = "This container is currently on hold" + containerindex.AuctionLotNo });
                }
                 

                _invoiceRepo.Add(invoice);

            }
             
            else
            {
                invoice.ContainerIndexId = null;

                var cyContainers = _cyContainerRepo.GetList(s => s.VIRNo == igm && s.IndexNo == indexNo);

                var cyInvoices = new List<Invoice>();

                foreach (var container in cyContainers)
                {
                    var newInvoice = invoice.Clone();
                    newInvoice.CYContainerId = container.CYContainerId;


                    if (container.InvoiceLocked == true)
                    {
                        return new OkObjectResult(new { error = true, message = "This container is currently Locked" });
                    }

                    if (container.IsHold == true)
                    {
                        return new OkObjectResult(new { error = true, message = "This container is currently on hold" });
                    }

                    //if (_invoiceRepo.GetFirst(i => i.CYContainerId == container.CYContainerId && i.IsCancelled == false) != null && !invoice.IsSubBill)
                    //{
                    //    return new OkObjectResult(new { error = true, message = "Invoice already created!" });
                    //}

                    var crl = _crlRepo.GetFirst(c => c.ContainerNumber == container.ContainerNo);

                    if (crl == null)
                    {
                        return new JsonResult(new { error = true, message = "RF not yet received" });
                    }

                    cyInvoices.Add(newInvoice);
                }

                _invoiceRepo.AddRange(cyInvoices);

            }


            foreach (var item in invoiceDoc)
            {
                InvoiceDocument doc = new InvoiceDocument
                {
                    FilePath = item.FilePath,
                    InvoiceId = invoice.InvoiceId,
                    Name = item.Name
                };
                docs.Add(doc);

            }
            _invoiceDocRepo.AddRange(docs);

            return new OkObjectResult(new { InvoiceId = invoice.InvoiceId, InvoiceNo = invoice.InvoiceNo });

        }

         
        public IActionResult SaveBill(Invoice invoice , string igm, long indexNo, bool islastPass , long? partyId, List<PaymentReceived> paymentReceivedList)
        {
            long creditallowsamt = 0;
            //var creditallowstatus = false;
            //var creditallowremarks = "";

            //var resdata = paymentReceivedList.Where(x => x.NatureOfAmount == "CreditAllowed").Sum(x=> x.ReceivedAmount);
            //var resdataCashRefund = paymentReceivedList.Where(x => x.NatureOfAmount == "CashRefund").ToList().Sum(x=> x.ReceivedAmount);

            //if (invoice.ExcessAmount > 0 && invoice.ExcessAmount <= 5000 && resdata <= 0  && resdataCashRefund <= 0)
            //{
            //    var resPaymentReceived = new PaymentReceived
            //    {
            //        NatureOfAmount = "CashRefund",
            //        ReceivedAmount = invoice.ExcessAmount
            //    };

            //    paymentReceivedList.Add(resPaymentReceived);
            //    invoice.ExcessAmount = 0;
            //}


            //if (invoice.ExcessAmount > 0 && resdata > 0)
            //{
            //    var res = paymentReceivedList.Where(x => x.NatureOfAmount == "CreditAllowed").LastOrDefault();
            //    if (res != null)
            //    {
            //        //creditallowstatus = true;
            //        //creditallowremarks = "Due to exceeding the threshold of access amount, we have reduced the credit amount. to" + invoice.ExcessAmount;

            //        var resPaymentReceived = new PaymentReceived
            //        {
            //            NatureOfAmount = "CreditAllowed",
            //            ReceivedAmount = (-invoice.ExcessAmount)
            //        };

            //        paymentReceivedList.Add(resPaymentReceived);

            //        creditallowsamt = invoice.ExcessAmount;

            //        invoice.ExcessAmount = 0;
            //    }

            //}



            #region MyRegion1

            var dovalidate = DateTime.Now;

            if (invoice.AdvanceDate == null)
            {
                invoice.AdvanceDate = DateTime.Now;
            }

            if (Convert.ToDateTime(invoice.AdvanceDate.Value.ToString("MM/dd/yyyy")) < Convert.ToDateTime(dovalidate.Date.ToString("MM/dd/yyyy")))
            {
                dovalidate = DateTime.Now;
            }
            else
            {
                dovalidate = invoice.AdvanceDate ?? DateTime.Now;
            }
            List<InvoiceItem> invoiceitemList = new List<InvoiceItem>();
            List<InvoiceItem> normalinvoiceitemList = new List<InvoiceItem>();
            List<InvoiceItem> billtolineinvoiceitemList = new List<InvoiceItem>();

            List<InvoiceItem> oldinvoiceitem = new List<InvoiceItem>();

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
                if (item.Description == "AuctionHandingCharges")
                {

                    var invoiceItem = new InvoiceItem
                    {
                        Description = item.Description,
                        Amount = item.Amount,
                        Type = "Auction",
                        Category = item.Category

                    };
                    invoiceitemList.Add(invoiceItem);
                }
                if (item.Description == "AuctionWeightmentCharges")
                {

                    var invoiceItem = new InvoiceItem
                    {
                        Description = item.Description,
                        Amount = item.Amount,
                        Type = "Auction",
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
                    Category = "Storage",
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
                        Category = "Storage",
                    };

                    invoiceitemList.Add(res);
                }
            }

            long donumber = 0;

            string NormalNumber = "";
            string BillToLineNumber = "";
            string AuctionInvoiveNumber = "";

            invoice.InvoiceDate = DateTime.Now;
            invoice.IsCancelled = false;
            #endregion



            if (invoice.Type == "CFS")
            {


                #region MyRegion2
                //if(invoice.CargoType == "LCL")
                //{ 

                //    var indexinfo = _containerIndexRepo.GetContainerIndexById(invoice.ContainerIndexId ?? 0 );

                //    if (indexinfo != null)
                //    {
                //        var resdata = _aictAndLineIndexTariffRepository.GetIGMIndexInfo(indexinfo.VirNo, Convert.ToInt64(indexinfo.IndexNo));

                //        if (resdata == null)
                //        {
                //            return new JsonResult(new { error = true, message = "Index Line Rate not Define" });
                //        }
                //    }
                //    else
                //    {
                //        return new JsonResult(new { error = true, message = "Index Info Not Found" });
                //    }


                //}

                if (invoice.AdvanceDate == null)
                {
                    invoice.AdvanceDate = DateTime.Now;
                }

                invoice.CYContainerId = null;

                //var containerindex = _containerIndexRepo.Find(invoice.ContainerIndexId);
                var containerindex = _containerIndexRepo.GetContainerIndexById(invoice.ContainerIndexId ?? 0);


                if (containerindex != null)
                {
                    var soc = _scheduleOfChargeRepository.GetSOCBYIGMIndex(containerindex.VirNo, containerindex.IndexNo ?? 0);

                    if (soc.Count() == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "please generate soc " });
                    }

                }



                //var invoices = _invoiceRepo.GetAll(x=> x.InvoiceItems).Where(x=> x.ContainerIndexId == invoice.ContainerIndexId).ToList();
                //var invoices = _invoiceRepo.GetInvoiceItemBycontainerIndexId(invoice.ContainerIndexId ?? 0).ToList();

                var invoiceres = _invoiceRepo.GetCFSFirstInvoice(invoice.ContainerIndexId ?? 0);

                if (invoiceres != null)
                {

                    var paidinvoiceitems = _invoiceRepo.GetInvoiceItemBycontainerIndexId(invoice.ContainerIndexId ?? 0).ToList();

                    var usedwaive = _invoiceRepo.GetPreviousWaiverCFS(invoice.ContainerIndexId ?? 0).Where(x => x.Category != "Tariff").ToList();

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

                // var undeliverdresbillToLine = _billToLineRepository.GetAll().Where(x => x.VirNo == containerindex.VirNo && x.IndexNo == containerindex.IndexNo && x.IndexType == "CFS" && x.InoviceCreated == false).LastOrDefault();

                //  var waiver = _waiverRepo.GetAll().Where(x => x.ContainerIndexId == invoice.ContainerIndexId && x.IsWaive == true && x.InvoiceCreated == false).ToList();

                var waiver = _containerIndexRepo.GetWaiverInfo(invoice.ContainerIndexId ?? 0);

                //if (containerindex != null)
                //{
                //  //  var containerlist = _containerIndexRepo.GetAll().Where(x => x.VirNo == containerindex.VirNo && x.IndexNo == containerindex.IndexNo).ToList();
                //    var containerlist = _containerIndexRepo.GetIndexInfo( containerindex.VirNo  , containerindex.IndexNo ?? 0).ToList();
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

                // var simo = _simoRepository.GetAll().Where(x => x.BLNumber == containerindex.BLNo && x.IndexNumber == containerindex.IndexNo.ToString() && x.VIRNumber == containerindex.VirNo).LastOrDefault();
                //var simo = _simoRepository.GetSIMOInfo( containerindex.BLNo , containerindex.IndexNo ?? 0, containerindex.VirNo);

                //if (simo != null && simo.Status == "HO")
                //{
                //    return new OkObjectResult(new { error = true, message = "can't  create due To simo hold" });

                //}


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
                                    Category = item.Category
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
                                    Category = item.Category

                                };

                                normalinvoiceitemList.Add(req);

                                manamt = 0;

                            }

                            if(manamt == 0)
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


                if (waiver.Count() > 0)
                {
                    foreach (var item in waiver)
                    {
                        var result = normalinvoiceitemList.Find(t => t.Description == item.Description && t.Category == item.Category);

                        if (result != null)
                        {
                            result.Amount -= item.Discount;
                        }
                    }
                }

                normalinvoiceitemList.RemoveAll(c => c.Amount <= 0);


                var invoi = new Invoice
                {
                    AdvanceDate = invoice.AdvanceDate,
                    BalanceAmount = invoice.BalanceAmount,
                    BillType = "Normal",
                    InvoiceCategory = "AICT",
                    InvoiceType = "SSTI",
                    CBM = invoice.CBM,
                    ClearingAgentNTN = invoice.ClearingAgentNTN,
                    CYContainerId = null,
                    ContainerIndexId = invoice.ContainerIndexId,
                    DestuffDate = invoice.DestuffDate,
                    InvoiceNatureType = islastPass,
                    StorageApplicableon = invoice.StorageApplicableon,
                    GateInDate = invoice.GateInDate,
                    InvoiceDate = invoice.InvoiceDate,
                    BillToLineAmount = invoice.BillToLineAmount,
                    Year = DateTime.Now.ToString("yyyy"),
                    WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                    TariffAmountTotal = invoice.TariffAmountTotal,
                    AdditionalFreeDays = invoice.AdditionalFreeDays,
                    InvoiceNo = GetLastInvoiceNo(),
                    IsAdvanceBill = invoice.IsAdvanceBill,
                    IsCancelled = false,
                    ClearingAgentId = invoice.ClearingAgentId,
                    CNIC = invoice.CNIC,
                    ClearingAgentRep = invoice.ClearingAgentRep,
                    BalanceCargo = invoice.BalanceCargo,
                    PhoneNumber = invoice.PhoneNumber,
                    InvoiceItems = normalinvoiceitemList,
                    Size = invoice.Size,
                    StorageDays = invoice.StorageDays,
                    TotalFreeDays = invoice.TotalFreeDays,
                    CargoType = invoice.CargoType,
                    Weight = invoice.Weight,
                    ExcessAmount = invoice.ExcessAmount,
                    Type = invoice.Type,
                    PartContainer = invoice.PartContainer,
                    OtherChargeAmount = invoice.OtherChargeAmount,
                    CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                    StorageTotal = invoice.StorageTotal,
                    PortChargeAmount = invoice.PortChargeAmount,
                    ExchangeRateAmount = invoice.ExchangeRateAmount,
                    SealCharger = invoice.SealCharger,
                    VehicleCharges = invoice.VehicleCharges,
                    TotalCharges = normalinvoiceitemList.Sum(x => x.Amount),
                    PreviousBillAmount = invoice.PreviousBillAmount,
                    BalanceAmountTotal = normalinvoiceitemList.Sum(x => x.Amount),
                    SalesTax = invoice.SalesTax,
                    //AfterSalesTax = (((invoice.TotalCharges - (invoice.BillToLineAmount + invoice.WaiverDiscountAmount)) - (invoice.PreviousBillAmount)) *   (invoice.SalesTax / 100)  ),
                    AfterSalesTax = Math.Ceiling((normalinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                    GrandTotal = (normalinvoiceitemList.Sum(x => x.Amount)) + Math.Ceiling((normalinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),

                };

                invoi.GrandTotal = Math.Round(invoi.GrandTotal, 2);

                //if (islastPass == true)
                //{
                //    var partybalance = _partyLedgerRepo.PartyBalanceAmount(invoice.ClearingAgentId ?? 0);

                //    if (partybalance < invoi.GrandTotal)
                //    {
                //        return new OkObjectResult(new { error = true, message = "Party Balance Amount is less then invoice amount " });

                //    }
                //}

                _invoiceRepo.Add(invoi);

                NormalNumber = invoi.InvoiceNo;

                if (invoice.AuctionGrandTotal > 0)
                {

                    var invoAuction = new Invoice
                    {
                        AdvanceDate = invoice.AdvanceDate,
                        BalanceAmount = invoice.BalanceAmount,
                        BillType = "Auction",
                        InvoiceCategory = "AICT",
                        InvoiceType = "SSTI",
                        CBM = invoice.CBM,
                        ClearingAgentNTN = invoice.ClearingAgentNTN,
                        CYContainerId = null,
                        Year = DateTime.Now.ToString("yyyy"),
                        ContainerIndexId = invoice.ContainerIndexId,
                        DestuffDate = invoice.DestuffDate,
                        StorageApplicableon = invoice.StorageApplicableon,
                        InvoiceNatureType = islastPass,
                        GateInDate = invoice.GateInDate,
                        InvoiceDate = invoice.InvoiceDate,
                        BillToLineAmount = invoice.BillToLineAmount,
                        WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                        TariffAmountTotal = invoice.TariffAmountTotal,
                        PhoneNumber = invoice.PhoneNumber,
                        ClearingAgentRep = invoice.ClearingAgentRep,
                        CNIC = invoice.CNIC,
                        ClearingAgentId = invoice.ClearingAgentId,
                        BalanceCargo = invoice.BalanceCargo,
                        AdditionalFreeDays = invoice.AdditionalFreeDays,
                        InvoiceNo = GetLastInvoiceNo(),
                        IsAdvanceBill = invoice.IsAdvanceBill,
                        ExchangeRateAmount = invoice.ExchangeRateAmount,
                        IsCancelled = false,
                        CargoType = invoice.CargoType,
                        Size = invoice.Size,
                        StorageDays = invoice.StorageDays,
                        TotalFreeDays = invoice.TotalFreeDays,
                        PartContainer = invoice.PartContainer,
                        Weight = invoice.Weight,
                        Type = invoice.Type,
                        ActualTariffCharges = invoice.TotalCharges,
                        OtherChargeAmount = invoice.OtherChargeAmount,
                        CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                        StorageTotal = 0,
                        TotalCharges = invoice.BillToLineAmount,
                        PreviousBillAmount = invoice.PreviousBillAmount,
                        BalanceAmountTotal = invoice.BillToLineAmount,
                        SalesTax = invoice.SalesTax,
                        //AfterSalesTax = ((invoice.BillToLineAmount) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                        //GrandTotal = invoice.BillToLineAmount + ((invoice.BillToLineAmount) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                        AuctionGrandTotal = invoice.AuctionGrandTotal,
                        AuctionSalesTax = invoice.AuctionSalesTax,
                        HandingCharges = invoice.HandingCharges,
                        WeightmentCharges = invoice.WeightmentCharges,

                    };

                    _invoiceRepo.Add(invoAuction);


                    AuctionInvoiveNumber = invoAuction.InvoiceNo;

                }

                var otherChargeAssign = _otherChargeAssigningRepo.GetAll().Where(x => x.ContainerIndexId == invoice.ContainerIndexId && x.InvoiceCreated == false).ToList();


                if (otherChargeAssign.Count() > 0)
                {
                    otherChargeAssign.ForEach(x => x.InvoiceCreated = true);

                    _otherChargeAssigningRepo.UpdateRange(otherChargeAssign);
                }

                if (waiver.Count() > 0)
                {
                    waiver.ForEach(x => x.InvoiceCreated = true);
                    _waiverRepo.UpdateRange(waiver);
                }

                if (undeliverdresbillToLine != null)
                {
                    if (undeliverdresbillToLine.Type == "HundredPercent")
                    {
                        billtolineinvoiceitemList = invoiceitemList;
                    }

                    if (undeliverdresbillToLine.Type == "ExStorage")
                    {
                        billtolineinvoiceitemList = invoiceitemList.Where(x => x.Category != "Storage").ToList();
                    }
                    if (undeliverdresbillToLine.Type == "OnlyTariff")
                    {
                        billtolineinvoiceitemList = invoiceitemList.Where(x => x.Category == "Tariff").ToList();
                    }

                    if (undeliverdresbillToLine.Type == "ManualAmount")
                    {
                        var manamt = undeliverdresbillToLine.TariffAmount;
                        foreach (var item in invoiceitemList)
                        {


                            if (manamt > 0)
                            {

                                manamt = manamt - item.Amount;

                                if (manamt < 0)
                                {
                                    var req = new InvoiceItem
                                    {
                                        Amount = manamt + item.Amount,
                                        Description = item.Description,
                                        Type = item.Type,
                                        Category = item.Category
                                    };

                                    billtolineinvoiceitemList.Add(req);

                                    break;
                                }

                                if (manamt > 0)
                                {
                                    billtolineinvoiceitemList.Add(item);

                                }

                                if (manamt == 0)
                                {
                                    billtolineinvoiceitemList.Add(item);
                                    break;
                                }

                            }

                        }
                    }

                    billtolineinvoiceitemList.RemoveAll(c => c.Amount <= 0);


                    var invo = new Invoice
                    {
                        AdvanceDate = invoice.AdvanceDate,
                        BalanceAmount = invoice.BalanceAmount,
                        BillType = "Bill To Line",
                        InvoiceCategory = "AICT",
                        InvoiceType = "SSTI",
                        CBM = invoice.CBM,
                        ClearingAgentNTN = invoice.ClearingAgentNTN,
                        InvoiceNatureType = islastPass,
                        CYContainerId = null,
                        ContainerIndexId = invoice.ContainerIndexId,
                        DestuffDate = invoice.DestuffDate,
                        Year = DateTime.Now.ToString("yyyy"),
                        StorageApplicableon = invoice.StorageApplicableon,
                        GateInDate = invoice.GateInDate,
                        AdditionalFreeDays = invoice.AdditionalFreeDays,
                        PhoneNumber = invoice.PhoneNumber,
                        ClearingAgentRep = invoice.ClearingAgentRep,
                        CNIC = invoice.CNIC,
                        ClearingAgentId = invoice.ClearingAgentId,
                        InvoiceDate = invoice.InvoiceDate,
                        BillToLineAmount = invoice.BillToLineAmount,
                        WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                        ExchangeRateAmount = invoice.ExchangeRateAmount,
                        CargoType = invoice.CargoType,
                        TariffAmountTotal = invoice.TariffAmountTotal,
                        BalanceCargo = invoice.BalanceCargo,
                        InvoiceNo = GetLastInvoiceNo(),
                        IsAdvanceBill = invoice.IsAdvanceBill,
                        InvoiceItems = billtolineinvoiceitemList,
                        IsCancelled = false,
                        Size = invoice.Size,
                        StorageDays = invoice.StorageDays,
                        TotalFreeDays = invoice.TotalFreeDays,
                        PartContainer = invoice.PartContainer,
                        Weight = invoice.Weight,
                        Type = invoice.Type,
                        ActualTariffCharges = invoice.TotalCharges,
                        OtherChargeAmount = invoice.OtherChargeAmount,
                        CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                        StorageTotal = 0,
                        TotalCharges = billtolineinvoiceitemList.Sum(x => x.Amount),
                        PreviousBillAmount = invoice.PreviousBillAmount,
                        //BalanceAmountTotal = invoice.BillToLineAmount,
                        BalanceAmountTotal = billtolineinvoiceitemList.Sum(x => x.Amount),
                        SalesTax = invoice.SalesTax,
                        AfterSalesTax = Math.Ceiling((billtolineinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                        GrandTotal = billtolineinvoiceitemList.Sum(x => x.Amount) + Math.Ceiling((billtolineinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),

                    };

                    invo.GrandTotal = Math.Round(invo.GrandTotal, 2);
                    _invoiceRepo.Add(invo);

                    BillToLineNumber = invo.InvoiceNo;

                    var billtoline = _billToLineRepository.GetAll().Where(x => x.VirNo == containerindex.VirNo && x.IndexNo == containerindex.IndexNo && x.IndexType == "CFS" && x.InoviceCreated == false).LastOrDefault();
                    if (billtoline != null)
                    {

                        billtoline.InoviceCreated = true;
                        billtoline.InvoiceNumber = invo.InvoiceNo;
                        billtoline.InvoiceAmount = invo.TotalCharges;
                        billtoline.TariffAmount = invo.TotalCharges;


                        _billToLineRepository.Update(billtoline);
                    }

                    //if (containerindex.ShippingAgentId != null)
                    //{
                    //    var party = _partyRepo.GetAll().Where(x => x.ShippingAgentId == containerindex.ShippingAgentId).FirstOrDefault();

                    //    if (party != null)
                    //    {
                    //        var partyLedger = new PartyLedger();
                    //        partyLedger.IsSettled = false;
                    //        partyLedger.LedgerDate = DateTime.Now;
                    //        partyLedger.PartyId = party.PartyId;
                    //        partyLedger.Type = "Bill To Line";
                    //        partyLedger.Debit = invo.GrandTotal;
                    //        partyLedger.Credit = 0;
                    //        partyLedger.BillNo = invo.InvoiceNo;

                    //        _partyLedgerRepo.Add(partyLedger);
                    //    }

                    //}

                }


                var deliveryorder = _deliveryOrderRepository.GetDeliveryOrderByContainerIndexId(invoice.ContainerIndexId ?? 0);
                if (deliveryorder == null)
                {
                    var dod = new DeliveryOrder
                    {
                        ContainerIndexId = invoice.ContainerIndexId,
                        Date = DateTime.Now,
                        ValidTo = dovalidate,
                        InvoiceNo = invoi.InvoiceNo,
                        DONumber = GetDeliveryOrderNo()
                    };

                    _deliveryOrderRepository.Add(dod);
                    donumber = dod.DONumber;
                }
                else
                {

                    deliveryorder.ContainerIndexId = invoice.ContainerIndexId;
                    deliveryorder.ValidTo = dovalidate;
                    deliveryorder.InvoiceNo = invoi.InvoiceNo;

                    _deliveryOrderRepository.Update(deliveryorder);
                    donumber = deliveryorder.DONumber;
                }

                #endregion


                if (islastPass == true)
                {
                    // new work for saving letpass;\


                    //var res = _partyLedgerRepo.PartyByClearnignAgentID(invoice.ClearingAgentId ?? 0);

                    //if (res != null)
                    //{
                        var ledger = new PartyLedger
                        {
                            LedgerDate = DateTime.Now,
                            BillNo = NormalNumber,
                            Debit = invoi.GrandTotal,
                            PartyId = partyId,
                            IsSettled = true,
                            Type = "LetPass"


                        };

                        _partyLedgerRepo.Add(ledger);
                    //}

                }
                else if (paymentReceivedList.Count() > 0)
                {

                    var rescreditalow = paymentReceivedList.Where(x => x.NatureOfAmount == "CreditAllowed").LastOrDefault();

                    var KnockOffinvoice = paymentReceivedList.Where(x => x.NatureOfAmount == "KnockOff").ToList();


                    if (rescreditalow != null)
                    {
                        var creditallowres = _partyLedgerRepo.CreditAllowedCFSBYid(invoice.ContainerIndexId ?? 0);

                        if (creditallowres != null)
                        {
                            creditallowres.IsSettled = true;
                            creditallowres.InvoiceNo = NormalNumber;
                            creditallowres.CreditAllowedExcessSettelment = creditallowsamt;
                            _creditAllowedRepository.Update(creditallowres);


                            //if (creditallowstatus == true)
                            //{
                            //    var crallow = new CreditAllowed
                            //    {
                            //        IsApprove = true,
                            //        IsSettled = true,
                            //        InvoiceNo = NormalNumber,
                            //        CreditAllowedRs = -creditallowsamt,
                            //        CreditDays = 0,
                            //        CreatedDate = DateTime.Now,
                            //        IsCancel = false,
                            //        IsReject = false,
                            //        Remarks = creditallowremarks,
                            //        IsRefound = false,
                            //        VirNumber = containerindex.VirNo,
                            //        IndexNumber = containerindex.IndexNo ?? 0

                            //    };

                            //    _creditAllowedRepository.Add(crallow);

                            //}

                        }

                    }

                    if (KnockOffinvoice.Count() > 0)
                    {

                        foreach (var item in KnockOffinvoice)
                        {
                            var knockoffinvoiceres = _partyLedgerRepo.FindInvoiceAndupdate(item.InoviceNo , NormalNumber);
                             
                        }
                    }

                    //foreach (var item in paymentReceivedList)
                    //{
                    //    if (item.NatureOfAmount == "KnockOff")
                    //    {
                    //        item.KnockOffInvoiceNo = item.InoviceNo;

                    //    }
                    //}


                    paymentReceivedList.ForEach(x => x.InoviceNo = NormalNumber);


                    _paymentReceivedRepository.AddRange(paymentReceivedList);


                    //foreach (var item in paymentReceivedList)
                    //{
                    //    if (item.NatureOfAmount == "KnockOff")
                    //    {
                    //        var excessAmountRefundSettlement = new ExcessAmountRefundSettlement
                    //        {
                    //            CreatedDate = DateTime.Now,
                    //            Type = "ExcessRefound",
                    //            RefoundAmount = item.ReceivedAmount,
                    //            InvoiceNo = item.KnockOffInvoiceNo
                    //        };
                    //        _excessAmountRefundSettlementRepo.Add(excessAmountRefundSettlement);
                    //    }
                    //    if (item.NatureOfAmount == "CashRefund")
                    //    {
                    //        var excessAmountRefundSettlement = new ExcessAmountRefundSettlement
                    //        {
                    //            CreatedDate = DateTime.Now,
                    //            Type = "CashRefund",
                    //            RefoundAmount = item.ReceivedAmount,
                    //            InvoiceNo = item.InoviceNo
                    //        };
                    //        _excessAmountRefundSettlementRepo.Add(excessAmountRefundSettlement);
                    //    }
                    //}
                    // new work for saving debit or credit note;
                }
 
            }

            if (invoice.Type == "CY")
            {

               
                if (invoice.AdvanceDate == null)
                {
                    invoice.AdvanceDate = DateTime.Now;
                }

                invoice.ContainerIndexId = null;

                var cyContainer = _cyContainerRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == indexNo);
                  
                if (cyContainer != null)
                {
                    #region MyRegion 3
                    var soc = _scheduleOfChargeRepository.GetSOCBYIGMIndex(cyContainer.VIRNo, cyContainer.IndexNo ?? 0);

                    if (soc.Count() == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "please generate soc " });
                    }

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




                    //if (cyContainer.IsHold == true)
                    //{
                    //    return new OkObjectResult(new { error = true, message = "This container is currently on hold" });
                    //}

                    //var sim = _simRepository.GetAll().Where(x => x.ContainerNumber == cyContainer.ContainerNo && x.VIRNumber == cyContainer.VIRNo).LastOrDefault();

                    //if (sim != null && sim.Status == "HO")
                    //{
                    //    return new OkObjectResult(new { error = true, message = "can't  create due To sim hold" });

                    //}


                    var undeliverdresbillToLine = _billToLineRepository.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexNo && x.IndexType == "CY" && x.InoviceCreated == false).LastOrDefault();
                    var waiver = _waiverRepo.GetAll().Where(x => x.CYContainerId == cyContainer.CYContainerId && x.IsWaive == true && x.InvoiceCreated == false).ToList();

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
                                        Category = item.Category
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
                                        Category = item.Category

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



                    if (waiver.Count() > 0)
                    {
                        foreach (var item in waiver)
                        {
                            var result = normalinvoiceitemList.Find(t => t.Description == item.Description && t.Category == item.Category);

                            if (result != null)
                            {
                                result.Amount -= item.Discount;

                            }

                        }

                        var dd = waiver.Where(x => x.Description == "Container Storage").LastOrDefault();

                        var ss = normalinvoiceitemList.Where(x => x.Description == "Storage Amount").LastOrDefault();

                        if (dd != null && dd.Discount > 0 && ss != null && ss.Amount > 0)
                        {
                            normalinvoiceitemList.Where(x => x.Description == "Storage Amount").LastOrDefault().Amount -= dd.Discount;
                        }

                        normalinvoiceitemList.RemoveAll(c => c.Amount <= 0);

                    }

                    invoice.CYContainerId = cyContainer.CYContainerId;


                    //var deliverystatus = CheckDeliverystatus(invoice.CYContainerId ?? 0, "CY");

                    //if (deliverystatus == true)
                    //{
                    //    return new OkObjectResult(new { error = true, message = "Index Delivered" });
                    //}

                    var invoi = new Invoice
                    {
                        AdvanceDate = invoice.AdvanceDate,
                        BalanceAmount = invoice.BalanceAmount,
                        BillType = "Normal",
                        InvoiceCategory = "AICT",
                        InvoiceType = "SSTI",
                        InvoiceNatureType = islastPass,
                        CBM = invoice.CBM,
                        ClearingAgentNTN = invoice.ClearingAgentNTN,
                        CYContainerId = invoice.CYContainerId,
                        ExcessAmount = invoice.ExcessAmount,
                        StorageApplicableon = invoice.StorageApplicableon,
                        ContainerIndexId = null,
                        DestuffDate = invoice.DestuffDate,
                        Year = DateTime.Now.ToString("yyyy"),
                        GateInDate = invoice.GateInDate,
                        InvoiceDate = invoice.InvoiceDate,
                        BillToLineAmount = invoice.BillToLineAmount,
                        WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                        BalanceCargo = invoice.BalanceCargo,
                        TariffAmountTotal = invoice.TariffAmountTotal,
                        InvoiceNo = GetLastInvoiceNo(),
                        IsAdvanceBill = invoice.IsAdvanceBill,
                        PartContainer = invoice.PartContainer,
                        IsCancelled = false,
                        ActualTariffCharges = invoice.TotalCharges,
                        PhoneNumber = invoice.PhoneNumber,
                        ClearingAgentRep = invoice.ClearingAgentRep,
                        CNIC = invoice.CNIC,
                        CargoType = invoice.CargoType,
                        ClearingAgentId = invoice.ClearingAgentId,
                        InvoiceItems = normalinvoiceitemList,
                        Size = invoice.Size,
                        StorageDays = invoice.StorageDays,
                        TotalFreeDays = invoice.TotalFreeDays,
                        AdditionalFreeDays = invoice.AdditionalFreeDays,
                        Weight = invoice.Weight,
                        ExchangeRateAmount = invoice.ExchangeRateAmount,
                        Type = invoice.Type,
                        OtherChargeAmount = invoice.OtherChargeAmount,
                        CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                        VehicleCharges = invoice.VehicleCharges,
                        SealCharger = invoice.SealCharger,
                        PortChargeAmount = invoice.PortChargeAmount,
                        StorageTotal = invoice.StorageTotal,
                        TotalCharges = normalinvoiceitemList.Sum(x => x.Amount),
                        PreviousBillAmount = invoice.PreviousBillAmount,
                        BalanceAmountTotal = normalinvoiceitemList.Sum(x => x.Amount),
                        SalesTax = invoice.SalesTax,
                        //AfterSalesTax = (((invoice.TotalCharges - (invoice.BillToLineAmount + invoice.WaiverDiscountAmount)) - (invoice.PreviousBillAmount)) *   (invoice.SalesTax / 100)  ),
                        AfterSalesTax = Math.Ceiling((normalinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                        //GrandTotal = (((normalinvoiceitemList.Sum(x => x.Amount) - invoice.WaiverDiscountAmount)) + ((((normalinvoiceitemList.Sum(x => x.Amount) - invoice.WaiverDiscountAmount) - (invoice.PreviousBillAmount)) * (Convert.ToDouble(invoice.SalesTax) / 100)))),
                        //GrandTotal = (normalinvoiceitemList.Sum(x => x.Amount) - invoice.WaiverDiscountAmount) + ((((normalinvoiceitemList.Sum(x => x.Amount) - invoice.WaiverDiscountAmount)) * (Convert.ToDouble(invoice.SalesTax) / 100))),
                        GrandTotal = (normalinvoiceitemList.Sum(x => x.Amount)) + Math.Ceiling((normalinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),

                    };

                    invoi.GrandTotal = Math.Round(invoi.GrandTotal, 2);

                    //if (islastPass == true)
                    //{
                    //    var partybalance = _partyLedgerRepo.PartyBalanceAmount(invoice.ClearingAgentId ?? 0);

                    //    if (partybalance < invoi.GrandTotal)
                    //    {
                    //        return new OkObjectResult(new { error = true, message = "Party Balance Amount is less then invoice amount " });

                    //    }
                    //}


                    _invoiceRepo.Add(invoi);

                    NormalNumber = invoi.InvoiceNo;

                    if (invoice.AuctionGrandTotal > 0)
                    {

                        var invoAuctioncy = new Invoice
                        {
                            AdvanceDate = invoice.AdvanceDate,
                            BalanceAmount = invoice.BalanceAmount,
                            BillType = "Auction",
                            InvoiceCategory = "AICT",
                            InvoiceType = "SSTI",
                            InvoiceNatureType = islastPass,
                            GateInDate = invoice.GateInDate,
                            CBM = invoice.CBM,
                            ClearingAgentNTN = invoice.ClearingAgentNTN,
                            CYContainerId = invoice.CYContainerId,
                            ContainerIndexId = null,
                            InvoiceDate = invoice.InvoiceDate,
                            BillToLineAmount = invoice.BillToLineAmount,
                            WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                            TariffAmountTotal = invoice.TariffAmountTotal,
                            Year = DateTime.Now.ToString("yyyy"),
                            StorageApplicableon = invoice.StorageApplicableon,
                            InvoiceNo = GetLastInvoiceNo(),
                            IsAdvanceBill = invoice.IsAdvanceBill,
                            AdditionalFreeDays = invoice.AdditionalFreeDays,
                            IsCancelled = false,
                            Size = invoice.Size,
                            BalanceCargo = invoice.BalanceCargo,
                            PhoneNumber = invoice.PhoneNumber,
                            CargoType = invoice.CargoType,
                            ExchangeRateAmount = invoice.ExchangeRateAmount,
                            ClearingAgentRep = invoice.ClearingAgentRep,
                            CNIC = invoice.CNIC,
                            ClearingAgentId = invoice.ClearingAgentId,
                            StorageDays = invoice.StorageDays,
                            TotalFreeDays = invoice.TotalFreeDays,
                            PartContainer = invoice.PartContainer,
                            Weight = invoice.Weight,
                            Type = invoice.Type,
                            ActualTariffCharges = invoice.TotalCharges,
                            OtherChargeAmount = invoice.OtherChargeAmount,
                            CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                            StorageTotal = 0,
                            TotalCharges = invoice.BillToLineAmount,
                            PreviousBillAmount = invoice.PreviousBillAmount,
                            BalanceAmountTotal = invoice.BillToLineAmount,
                            SalesTax = invoice.SalesTax,
                            AuctionGrandTotal = invoice.AuctionGrandTotal,
                            AuctionSalesTax = invoice.AuctionSalesTax,
                            HandingCharges = invoice.HandingCharges,
                            WeightmentCharges = invoice.WeightmentCharges,

                        };


                        _invoiceRepo.Add(invoAuctioncy);


                        AuctionInvoiveNumber = invoAuctioncy.InvoiceNo;

                    }



                    //if (invoice.OtherChargeAmount > 0)
                    //{

                    //var invo = new Invoice
                    //{

                    //    BillType = "Additional Charges",
                    //    AdvanceDate = invoice.AdvanceDate,
                    //    BalanceAmount = invoice.BalanceAmount,
                    //    CBM = invoice.CBM,
                    //    ClearingAgentNTN = invoice.ClearingAgentNTN,
                    //    CYContainerId = invoice.CYContainerId,
                    //    ContainerIndexId = null,
                    //    DestuffDate = invoice.DestuffDate,
                    //    GateInDate = invoice.GateInDate,
                    //    InvoiceDate = invoice.InvoiceDate,
                    //    BillToLineAmount = invoice.BillToLineAmount,
                    //    WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                    //    TariffAmountTotal = invoice.TariffAmountTotal,
                    //    InvoiceNo = 3,
                    //    IsAdvanceBill = invoice.IsAdvanceBill,
                    //    IsCancelled = false,
                    //    ActualTariffCharges = invoice.TotalCharges,
                    //    InvoiceItems = invoice.InvoiceItems,
                    //    Size = invoice.Size,
                    //    StorageDays = invoice.StorageDays,
                    //    Weight = invoice.Weight,
                    //    Type = invoice.Type,
                    //    OtherChargeAmount = invoice.OtherChargeAmount,
                    //    CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                    //    StorageTotal = invoice.StorageTotal,
                    //    TotalCharges = invoice.OtherChargeAmount,
                    //    PreviousBillAmount = invoice.PreviousBillAmount,
                    //    BalanceAmountTotal = invoice.OtherChargeAmount,
                    //    SalesTax = invoice.SalesTax,
                    //    AfterSalesTax = ((invoice.OtherChargeAmount) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                    //    GrandTotal = invoice.OtherChargeAmount + ((invoice.OtherChargeAmount) * (Convert.ToDouble(invoice.SalesTax) / 100))

                    //};


                    //_invoiceRepo.Add(invo);

                    var otherChargeAssign = _otherChargeAssigningRepo.GetAll().Where(x => x.CyContainerId == invoice.CYContainerId && x.InvoiceCreated == false).ToList();


                    if (otherChargeAssign.Count() > 0)
                    {

                        //foreach (var item in otherChargeAssign)
                        //{
                        //    item.InvoiceCreated = true;
                        //}

                        otherChargeAssign.ForEach(x => x.InvoiceCreated = true);

                        _otherChargeAssigningRepo.UpdateRange(otherChargeAssign);
                    }


                    //}


                    if (waiver.Count() > 0)
                    {
                        foreach (var item in waiver)
                        {
                            item.InvoiceCreated = true;

                        }

                        _waiverRepo.UpdateRange(waiver);
                    }
                    //if (invoice.WaiverDiscountAmount > 0)
                    //{

                    //var invo = new Invoice
                    //{

                    //    BillType = "Waiver Charges",
                    //    AdvanceDate = invoice.AdvanceDate,
                    //    BalanceAmount = invoice.BalanceAmount,
                    //    CBM = invoice.CBM,
                    //    ClearingAgentNTN = invoice.ClearingAgentNTN,
                    //    CYContainerId = invoice.CYContainerId,
                    //    ContainerIndexId = null,
                    //    DestuffDate = invoice.DestuffDate,
                    //    GateInDate = invoice.GateInDate,
                    //    InvoiceDate = invoice.InvoiceDate,
                    //    BillToLineAmount = invoice.BillToLineAmount,
                    //    WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                    //    TariffAmountTotal = invoice.TariffAmountTotal,
                    //    InvoiceNo = 3,
                    //    IsAdvanceBill = invoice.IsAdvanceBill,
                    //    IsCancelled = false,
                    //    ActualTariffCharges = invoice.TotalCharges,
                    //    InvoiceItems = invoice.InvoiceItems,
                    //    Size = invoice.Size,
                    //    StorageDays = invoice.StorageDays,
                    //    Weight = invoice.Weight,
                    //    Type = invoice.Type,
                    //    OtherChargeAmount = invoice.OtherChargeAmount,
                    //    CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                    //    StorageTotal = invoice.StorageTotal,
                    //    TotalCharges = invoice.WaiverDiscountAmount,
                    //    PreviousBillAmount = invoice.PreviousBillAmount,
                    //    BalanceAmountTotal = invoice.WaiverDiscountAmount,
                    //    SalesTax = invoice.SalesTax,
                    //    AfterSalesTax = ((invoice.WaiverDiscountAmount) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                    //    GrandTotal = invoice.WaiverDiscountAmount + ((invoice.WaiverDiscountAmount) * (Convert.ToDouble(invoice.SalesTax) / 100))

                    //};


                    //_invoiceRepo.Add(invo);


                    //var waiver = _waiverRepo.GetAll().Where(x => x.CYContainerId == invoice.CYContainerId && x.IsWaive == true && x.InvoiceCreated == false).ToList();


                    //if(waiver.Count() > 0)
                    //{

                    //    foreach (var item in waiver)
                    //    {
                    //        item.InvoiceCreated = true;
                    //        //item.InvoiceNumber = invo.InvoiceNo;

                    //    }

                    //    _waiverRepo.UpdateRange(waiver);
                    //}


                    //}

                    if (undeliverdresbillToLine != null)
                    {
                        if (undeliverdresbillToLine.Type == "HundredPercent")
                        {
                            billtolineinvoiceitemList = invoiceitemList;
                        }

                        if (undeliverdresbillToLine.Type == "ExStorage")
                        {
                            billtolineinvoiceitemList = invoiceitemList.Where(x => x.Category != "Storage").ToList();
                        }
                        if (undeliverdresbillToLine.Type == "OnlyTariff")
                        {
                            billtolineinvoiceitemList = invoiceitemList.Where(x => x.Category == "Tariff").ToList();
                        }

                        if (undeliverdresbillToLine.Type == "ManualAmount")
                        {
                            var manamt = undeliverdresbillToLine.TariffAmount;


                            foreach (var item in invoiceitemList)
                            {


                                if (manamt > 0)
                                {

                                    manamt = manamt - item.Amount;

                                    if (manamt < 0)
                                    {
                                        var req = new InvoiceItem
                                        {
                                            Amount = manamt + item.Amount,
                                            Description = item.Description,
                                            Type = item.Type,
                                            Category = item.Category



                                        };

                                        billtolineinvoiceitemList.Add(req);

                                        break;
                                    }

                                    if (manamt > 0)
                                    {
                                        billtolineinvoiceitemList.Add(item);

                                    }

                                    if (manamt == 0)
                                    {
                                        billtolineinvoiceitemList.Add(item);
                                        break;
                                    }



                                }





                            }

                        }


                        var invo = new Invoice
                        {
                            AdvanceDate = invoice.AdvanceDate,
                            BalanceAmount = invoice.BalanceAmount,
                            BillType = "Bill To Line",
                            InvoiceCategory = "AICT",
                            InvoiceType = "SSTI",
                            InvoiceNatureType = islastPass,
                            CBM = invoice.CBM,
                            StorageApplicableon = invoice.StorageApplicableon,
                            ClearingAgentNTN = invoice.ClearingAgentNTN,
                            CYContainerId = invoice.CYContainerId,
                            ContainerIndexId = null,
                            DestuffDate = invoice.DestuffDate,
                            GateInDate = invoice.GateInDate,
                            InvoiceDate = invoice.InvoiceDate,
                            BillToLineAmount = invoice.BillToLineAmount,
                            AdditionalFreeDays = invoice.AdditionalFreeDays,
                            PhoneNumber = invoice.PhoneNumber,
                            ClearingAgentRep = invoice.ClearingAgentRep,
                            CNIC = invoice.CNIC,
                            ClearingAgentId = invoice.ClearingAgentId,
                            CargoType = invoice.CargoType,
                            Year = DateTime.Now.ToString("yyyy"),
                            PartContainer = invoice.PartContainer,
                            WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                            TariffAmountTotal = invoice.TariffAmountTotal,
                            InvoiceNo = GetLastInvoiceNo(),
                            IsAdvanceBill = invoice.IsAdvanceBill,
                            IsCancelled = false,
                            InvoiceItems = billtolineinvoiceitemList,
                            ExchangeRateAmount = invoice.ExchangeRateAmount,
                            Size = invoice.Size,
                            StorageDays = invoice.StorageDays,
                            TotalFreeDays = invoice.TotalFreeDays,
                            Weight = invoice.Weight,
                            Type = invoice.Type,
                            ActualTariffCharges = invoice.TotalCharges,
                            BalanceCargo = invoice.BalanceCargo,
                            OtherChargeAmount = invoice.OtherChargeAmount,
                            CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                            StorageTotal = 0,
                            TotalCharges = billtolineinvoiceitemList.Sum(x => x.Amount),
                            PreviousBillAmount = invoice.PreviousBillAmount,
                            BalanceAmountTotal = billtolineinvoiceitemList.Sum(x => x.Amount),
                            SalesTax = invoice.SalesTax,
                            AfterSalesTax = Math.Ceiling((billtolineinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                            GrandTotal = billtolineinvoiceitemList.Sum(x => x.Amount) + Math.Ceiling((billtolineinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),

                        };

                        invo.GrandTotal = Math.Round(invo.GrandTotal, 2);

                        _invoiceRepo.Add(invo);

                        BillToLineNumber = invo.InvoiceNo;

                        var billtolines = _billToLineRepository.GetAll().Where(x => x.VirNo == cyContainer.VIRNo && x.IndexNo == cyContainer.IndexNo && x.IndexType == "CY" && x.InoviceCreated == false).ToList();
                        if (billtolines.Count() > 0)
                        {

                            foreach (var item in billtolines)
                            {
                                item.InoviceCreated = true;
                                item.InvoiceNumber = invo.InvoiceNo;
                                item.InvoiceAmount = invo.TotalCharges;
                                item.TariffAmount = invo.TotalCharges;

                            }

                            _billToLineRepository.UpdateRange(billtolines);
                        }
                        //if (cyContainer.ShippingAgentId != null)
                        //{
                        //    var party = _partyRepo.GetAll().Where(x => x.ShippingAgentId == cyContainer.ShippingAgentId).FirstOrDefault();

                        //    if (party != null)
                        //    {
                        //        var partyLedger = new PartyLedger();
                        //        partyLedger.IsSettled = false;
                        //        partyLedger.LedgerDate = DateTime.Now;
                        //        partyLedger.PartyId = party.PartyId;
                        //        partyLedger.Type = "Bill To Line";
                        //        partyLedger.Debit = invo.GrandTotal;
                        //        partyLedger.Credit = 0;
                        //        partyLedger.BillNo = invo.InvoiceNo;

                        //        _partyLedgerRepo.Add(partyLedger);
                        //    }
                        //}

                    }


                    var deliveryorder = _deliveryOrderRepository.GetAll().Where(x => x.CYContainerId == cyContainer.CYContainerId).FirstOrDefault();

                    if (deliveryorder == null)
                    {
                        var dod = new DeliveryOrder
                        {
                            CYContainerId = cyContainer.CYContainerId,
                            Date = DateTime.Now,
                            ValidTo = dovalidate,
                            InvoiceNo = invoi.InvoiceNo,
                            DONumber = GetDeliveryOrderNo()
                        };


                        _deliveryOrderRepository.Add(dod);
                        donumber = dod.DONumber;
                    }
                    else
                    {

                        deliveryorder.CYContainerId = cyContainer.CYContainerId;
                        deliveryorder.ValidTo = dovalidate;
                        deliveryorder.InvoiceNo = invoi.InvoiceNo;
                        _deliveryOrderRepository.Update(deliveryorder);
                        donumber = deliveryorder.DONumber;
                    }
                    #endregion


                    if (islastPass == true)
                    {
                        // new work for saving letpass;\


                        //var res = _partyLedgerRepo.PartyByClearnignAgentID(invoice.ClearingAgentId ?? 0);

                        //if (res != null)
                        //{
                            var ledger = new PartyLedger
                            {
                                LedgerDate = DateTime.Now,
                                BillNo = NormalNumber,
                                Debit = invoi.GrandTotal,
                                PartyId = partyId,
                                IsSettled = true,
                                Type = "LetPass"

                            };

                            _partyLedgerRepo.Add(ledger);
                        //}

                    }
                    else if (paymentReceivedList.Count() > 0)
                    {

                        var rescreditalow = paymentReceivedList.Where(x => x.NatureOfAmount == "CreditAllowed").LastOrDefault();

                        var KnockOffinvoice = paymentReceivedList.Where(x => x.NatureOfAmount == "KnockOff").ToList();


                        if (rescreditalow != null)
                        {
                            var creditallowres = _partyLedgerRepo.CreditAllowedCYbyid(invoice.CYContainerId ?? 0);

                            if (creditallowres != null)
                            {
                                creditallowres.IsSettled = true;
                                creditallowres.InvoiceNo = NormalNumber;
                                creditallowres.CreditAllowedExcessSettelment = creditallowsamt;
                                _creditAllowedRepository.Update(creditallowres);

                                //if (creditallowstatus == true)
                                //{
                                //    var crallow = new CreditAllowed
                                //    {
                                //        IsApprove = true,
                                //        IsSettled = true,
                                //        InvoiceNo = NormalNumber,
                                //        CreditAllowedRs = -creditallowsamt,
                                //        CreditDays = 0,
                                //        CreatedDate = DateTime.Now,
                                //        IsCancel = false,
                                //        IsReject = false,
                                //        Remarks = creditallowremarks,
                                //        IsRefound = false,
                                //        VirNumber = cyContainer.VIRNo ,
                                //        IndexNumber = cyContainer.IndexNo ?? 0 

                                //    };

                                //    _creditAllowedRepository.Add(crallow);

                                //}

                            }


                        }

                        if (KnockOffinvoice.Count() > 0)
                        {
                            foreach (var item in KnockOffinvoice)
                            {
                                //var knockoffinvoiceres = _partyLedgerRepo.GetInvoiceByInvoiceNo(item.InoviceNo);
                                var knockoffinvoiceres = _partyLedgerRepo.FindInvoiceAndupdate(item.InoviceNo , NormalNumber);

                                //if (knockoffinvoiceres != null)
                                //{
                                //    knockoffinvoiceres.ExcessAmount = 0;
                                //    _invoiceRepo.Update(knockoffinvoiceres);

                                //}
                            }
                        }

                        //foreach (var item in paymentReceivedList)
                        //{
                        //    if (item.NatureOfAmount == "KnockOff")
                        //    {
                        //        item.KnockOffInvoiceNo = item.InoviceNo;

                        //    }
                        //}


                        paymentReceivedList.ForEach(x => x.InoviceNo = NormalNumber);

                        _paymentReceivedRepository.AddRange(paymentReceivedList);


                        //foreach (var item in paymentReceivedList)
                        //{
                        //    if (item.NatureOfAmount == "KnockOff")
                        //    {

                        //        var excessAmountRefundSettlement = new ExcessAmountRefundSettlement
                        //        {
                        //            CreatedDate = DateTime.Now,
                        //            Type = "ExcessRefound",
                        //            RefoundAmount = item.ReceivedAmount,
                        //            InvoiceNo = item.KnockOffInvoiceNo


                        //        };

                        //        _excessAmountRefundSettlementRepo.Add(excessAmountRefundSettlement);
                        //    }

                        //    if (item.NatureOfAmount == "CashRefund")
                        //    {
                        //        var excessAmountRefundSettlement = new ExcessAmountRefundSettlement
                        //        {
                        //            CreatedDate = DateTime.Now,
                        //            Type = "CashRefund",
                        //            RefoundAmount = item.ReceivedAmount,
                        //            InvoiceNo = item.InoviceNo

                        //        };
                        //        _excessAmountRefundSettlementRepo.Add(excessAmountRefundSettlement);
                        //    }
                        //}



                        // new work for saving debit or credit note;
                    }


                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "container index not found" });

                }
            }
             


            return new OkObjectResult(new { error = false, message = "created"  ,  InvoiceNo = NormalNumber , DeliveryOrderNumber = donumber  , InvoiceNoBillToLine  = BillToLineNumber  , AuctionInvoiveNo = AuctionInvoiveNumber });

        }


        public string GetLastInvoiceNo()
        {

            var year = DateTime.Now.ToString("yyyy");
                 
            var invo = _invoiceRepo.GetInvoiceLast();

            if(invo != null)
            {
                if (invo.Year == year)
                {
                    var no = invo.InvoiceNo.Substring(0, invo.InvoiceNo.IndexOf("/"));

                    var number = Convert.ToInt64(no) + 1;
                    var InvoiceNo = number + "/" + year;

                    return InvoiceNo;
                }
                else
                {
                    var no = 1;

                    var InvoiceNo = no + "/" + year;

                    return InvoiceNo;
                }
            }
            else
            {
                var no = 1;

                var InvoiceNo = no + "/" + year;

                return InvoiceNo;

            }
             

        }


        public string GetLastInvoiceNoForFF()
        {

            var year = DateTime.Now.ToString("yyyy");
            var month = DateTime.Now.ToString("MM");

            var invo = _invoiceRepo.GetInvoiceLastForFF();

            if (invo != null)
            {
                if (invo.DeleteDate.ToString("MM") == month)
                {
                    var no = invo.InvoiceNo.Substring(0, invo.InvoiceNo.IndexOf("/"));

                    var number = Convert.ToInt64(no) + 1;

                    var InvoiceNo = number + "/" + month  + "/" + year;

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

                var InvoiceNo = no + "/" + month +  "/" + year;

                return InvoiceNo;

            }


        }


        public long GetDeliveryOrderNo()
        {
            var deli = _invoiceRepo.GetDeliveryOrderLast();
            if(deli != null)
            {
                var DonumberNo = deli != null ? deli.DONumber + 1 : 1;
                return DonumberNo;
            }
            else
            {
                var DonumberNo =  1;
                return DonumberNo;
            }
            


        }


        public async Task<IActionResult> AddInVoiceDocument(IFormFile formData, string name)
        {
            IList<string> filepaths = new List<string>();
            // IList<InvoiceDocument> docs = new List<InvoiceDocument>();

            var f = formData;
            string storePath = f.Name;
            string docFolder = Path.Combine("InvoiceDouments", storePath);

            try
            {
                if (f == null || f.Length == 0)
                {
                    return Content("File not selected");
                }

                string path = FileUploadHandler.GetFilePathForUpload(docFolder, f.FileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await f.CopyToAsync(stream);
                }
            }
            catch (Exception e)
            {
                return Json(new { result = "Upload Failed", error = e.Message });
            }

            string filePath = FileUploadHandler.FileReturnPath(docFolder, f.FileName);

            filepaths.Add(filePath);

            return Ok();
        }

        public IActionResult ReprintInvoice()
        {
            return View();
        }


        public IActionResult DeleteInvoice(long invoiceId)
        {
            var invoice = _invoiceRepo.GetFirst(x => x.InvoiceId == invoiceId);

            invoice.IsCancelled = true;
            invoice.IsDeleted = true;

            if (invoice != null)
            {
                if(invoice.ContainerIndexId !=  null)
                {
                    var res = _invoiceRepo.GetAll().Where(x => x.ContainerIndexId == invoice.ContainerIndexId).ToList();

                    if(res.Count() > 1)
                    {

                        if(invoice.InvoiceId == res.LastOrDefault().InvoiceId)
                        {

                            var invo = _invoiceRepo.GetAll().Where(x => x.InvoiceId == invoice.InvoiceId).LastOrDefault();

                            invo.IsCancelled = true;
                            invo.IsDeleted = true;
                            _invoiceRepo.Update(invo);

                            return new OkObjectResult(new { error = false, message = "Invoice Deleted" });
                        }
                        else
                        {
                            return new OkObjectResult(new { error = true, message = "first delete Invoice " + res.LastOrDefault().InvoiceNo });
                        }
                        

                    }
                    else
                    {
                        var invo = _invoiceRepo.GetAll().Where(x => x.InvoiceId == invoice.InvoiceId).LastOrDefault();

                        invo.IsCancelled = true;
                        invo.IsDeleted = true;
                        _invoiceRepo.Update(invo);


                        var deliveryorder = _deliveryOrderRepository.GetAll().Where(x => x.ContainerIndexId == invoice.ContainerIndexId).LastOrDefault();

                        if (deliveryorder != null)
                        {
                            _deliveryOrderRepository.Delete(deliveryorder);
                        } 

                        return new OkObjectResult(new { error = false, message = "Invoice And Do Deleted" });

                    }
                }
                if (invoice.CYContainerId != null)
                {
                    var res = _invoiceRepo.GetAll().Where(x => x.CYContainerId == invoice.CYContainerId).ToList();

                    if (res.Count() > 1)
                    {

                        if (invoice.InvoiceId == res.LastOrDefault().InvoiceId)
                        {
                            var invo = _invoiceRepo.GetAll().Where(x => x.InvoiceId == invoice.InvoiceId).LastOrDefault();

                            invo.IsCancelled = true;
                            invo.IsDeleted = true;
                            _invoiceRepo.Update(invo); 

                            return new OkObjectResult(new { error = false, message = "Invoice Deleted" });
                        }
                        else
                        {
                            return new OkObjectResult(new { error = true, message = "first delete Invoice " + res.LastOrDefault().InvoiceNo });
                        }

                    }
                    else
                    {
                        var invo = _invoiceRepo.GetAll().Where(x => x.InvoiceId == invoice.InvoiceId).LastOrDefault();

                        invo.IsCancelled = true;
                        invo.IsDeleted = true;
                        _invoiceRepo.Update(invo);


                        var deliveryorder = _deliveryOrderRepository.GetAll().Where(x => x.CYContainerId == invoice.CYContainerId).LastOrDefault();

                        if(deliveryorder != null)
                        {
                            _deliveryOrderRepository.Delete(deliveryorder);
                        }

                        

                        return new OkObjectResult(new { error = false, message = "Invoice And Do Deleted" });

                    }
                }
                else
                {
                    return new OkObjectResult(new { error = false, message = "there is some issue" });
                }
                //var amountRecive = _amountReceiveRepo.GetFirst(x => x.InvoiceId == invoice.InvoiceId);
                //if (amountRecive != null)
                //{
                //    return new OkObjectResult(new { error = true, message = "Invoice Amount Received" });
                //}




              
            }

            return new OkObjectResult(new { error = true, message = "Invoice Not Found" });
        }

        public IActionResult WaiverView()
        {
            return View();
        }
         
        public IActionResult AddWaiVerDetail(string type, string remarks, long? containerId, double storageAmount , double sealChargr  
                                            , double otherChargAmount , double vehicleChargs , List<InvoiceItemViewModel> tariffdata )
        {


            List<Waiver> waivers = new List<Waiver>();

            var waiverno = GenWaiverCode();

            tariffdata.RemoveAll(x => x.Type == "Auction");

            if (type == null)
            {
                return new OkObjectResult(new { error = true, message = "Please Select Type CY Or CFS" });

            }
            if (remarks == null)
            {
                return new OkObjectResult(new { error = true, message = "Please add remarks" });

            }
            if (containerId == null)
            {
                return new OkObjectResult(new { error = true, message = "Please select index" });

            }
            
            if (tariffdata.Count() <= 0)
            {
                return new OkObjectResult(new { error = true, message = "Please select tariff detail" });

            }
            if (waiverno == null)
            {
                return new OkObjectResult(new { error = true, message = "Please wait" });

            }


            if (type == "CFS")
            {

                var waver = _waiverRepo.GetAll().Where(x => x.ContainerIndexId == containerId && x.InvoiceCreated == false).ToList();

                if(waver.Count() > 0  )
                {
                    return new OkObjectResult(new { error = true, message = "already created" });

                }

                foreach (var item in tariffdata)
                {
                    var waiver = new Waiver
                    {
                        ContainerIndexId = containerId,
                        Description = item.Description,
                        IsWaive = false,
                        Remarks =  remarks,
                        ///StorageAmount = storageAmount,
                        TariffAmount = item.Amount,
                        WaiverNumber = waiverno,
                        Category = item.Category,
                        //TariffId = item.TariffId


                    };
                    waivers.Add(waiver);
                }


                if(storageAmount > 0)
                {
                    var res = new Waiver
                    {
                        ContainerIndexId = containerId,
                        Description = "Storage Amount",
                        IsWaive = false,
                        Remarks = remarks,
                        ///StorageAmount = storageAmount,
                        TariffAmount = storageAmount,
                        WaiverNumber = waiverno,
                        Category = "Storage",
                    };

                    waivers.Add(res);
                }
                
                if(sealChargr > 0)
                {
                    var res = new Waiver
                    {
                        ContainerIndexId = containerId,
                        Description = "Seal Charges",
                        IsWaive = false,
                        Remarks = remarks,                 
                        TariffAmount = sealChargr,
                        WaiverNumber = waiverno,
                        Category = "SealCharges",
                    };

                    waivers.Add(res);
                }
                             
                if(otherChargAmount > 0)
                {
                    var res = new Waiver
                    {
                        ContainerIndexId = containerId,
                        Description = "Special Handling Charges",
                        IsWaive = false,
                        Remarks = remarks,                 
                        TariffAmount = otherChargAmount,
                        WaiverNumber = waiverno,
                        Category = "SpecialHandlingCharges"
                    };

                    waivers.Add(res);
                }
                if(vehicleChargs > 0)
                {
                    var res = new Waiver
                    {
                        ContainerIndexId = containerId,
                        Description = "Vehicle Charges",
                        IsWaive = false,
                        Remarks = remarks,                 
                        TariffAmount = vehicleChargs,
                        WaiverNumber = waiverno,
                        Category = "VehicleCharges"

                    };

                    waivers.Add(res);
                }

                var invoiceres = _invoiceRepo.GetCFSFirstInvoice(containerId ?? 0);

                if (invoiceres != null)
                {
                    var invoices = _invoiceRepo.GetInvoiceItemBycontainerIndexId(containerId ?? 0).ToList();
                    var usedwaive = _invoiceRepo.GetPreviousWaiverCFS(containerId ?? 0).Where(x => x.Category != "Tariff").ToList();

                    if (invoices.Count() > 0)
                    {
                        var waiversres = waivers.Where(x => x.Category != "Tariff").ToList();

                        var afterinvoices = invoices.Where(x => x.Category != "Tariff");


                        if (waiversres.Count() > 0)
                        {
                            foreach (var item in waiversres)
                            {

                                var result = afterinvoices.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Amount);

                                if (result > 0)
                                {
                                    item.TariffAmount -= result;
                                }

                            }

                            waiversres.RemoveAll(c => c.TariffAmount <= 0);

                            foreach (var item in waiversres)
                            {

                                var result = usedwaive.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Discount);

                                if (result > 0)
                                {
                                    item.TariffAmount -= result;
                                }

                            }


                            waiversres.RemoveAll(c => c.TariffAmount <= 0);

                            if (waiversres.Count() > 0)
                            {
                                _waiverRepo.AddRange(waiversres);

                                return new OkObjectResult(new { error = false, message = "Request generated Waiver No", Waiverno = waiverno });
                            }
                            else
                            {
                                return new OkObjectResult(new { error = true, message = "there is no charges to Waive" });
                            }


                        }
                        else
                        {
                            return new OkObjectResult(new { error = true, message = "there is no charges to Waive" });

                        }

                      

                    }
                    else
                    {
                        waivers.RemoveAll(x => x.Category == "Tariff");


                        foreach (var item in waivers)
                        {

                            var result = usedwaive.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Discount);

                            if (result > 0)
                            {
                                item.TariffAmount -= result;
                            }

                        }


                        waivers.RemoveAll(c => c.TariffAmount <= 0);

                        if (waivers.Count() > 0)
                        {
                            _waiverRepo.AddRange(waivers);

                            return new OkObjectResult(new { error = false, message = "Request generated Waiver No", Waiverno = waiverno });

                        }
                        else
                        {
                            return new OkObjectResult(new { error = true, message = "there is no charges to Waive" });
                        }
                    }

                    
                     
                }

                else
                {
                    if(waivers.Count() > 0)
                    {
                        _waiverRepo.AddRange(waivers);

                        return new OkObjectResult(new { error = false, message = "Request generated Waiver No", Waiverno = waiverno   });
                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "there is no charges to Waive" });

                    }
                }


      
            }
             


            return new OkObjectResult(new { error = true, message = "please try again" });


        }
         
        public IActionResult AddWaiVerDetailCY(string type, string remarks, string igm , long? indexno, double storageAmount, double sealChargr 
                                            , double otherChargAmount, double vehicleChargs , double cyStoragSizeAmount, List<InvoiceItemViewModel> tariffdata)
        {

            List<InvoiceItem> invoiceitemList = new List<InvoiceItem>();
            List<InvoiceItem> normalinvoiceitemList = new List<InvoiceItem>();
            var oldinvoiceitem = new List<InvoiceItem>();
             
            List<Waiver> waivers = new List<Waiver>();
            tariffdata.RemoveAll(x => x.Type == "Auction");

            var waiverno = GenWaiverCode();

            if (type == null)
            {
                return new OkObjectResult(new { error = true, message = "Please Select Type CY Or CFS" });

            }
            if (remarks == null)
            {
                return new OkObjectResult(new { error = true, message = "Please add remarks" });

            }
            if (igm == null )
            {
                return new OkObjectResult(new { error = true, message = "Please select igm" });

            }

            if (igm == "")
            {
                return new OkObjectResult(new { error = true, message = "Please select igm" });

            }

            if (indexno == null)
            {
                return new OkObjectResult(new { error = true, message = "Please select index" });

            }

            if (tariffdata.Count() <= 0)
            {
                return new OkObjectResult(new { error = true, message = "Please select tariff detail" });

            }
            if (waiverno == null)
            {
                return new OkObjectResult(new { error = true, message = "Please wait" });

            }
 
            if (type == "CY")
            {

                var cyContainer = _cyContainerRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == indexno);

                if(cyContainer != null)
                {

                    var waver = _waiverRepo.GetAll().Where(x => x.CYContainerId == cyContainer.CYContainerId && x.InvoiceCreated == false).ToList();
                     
                    if (waver.Count() > 0)
                    {
                        return new OkObjectResult(new { error = true, message = "already created" });

                    }

                    else
                    {
                         
                        foreach (var item in tariffdata)
                        {
                            var waiver = new Waiver
                            {
                                CYContainerId = cyContainer.CYContainerId,
                                Description = item.Description,
                                IsWaive = false,
                                Remarks = remarks,
                                //StorageAmount = storageAmount,
                                TariffAmount = item.Amount,
                                WaiverNumber = waiverno,
                                Category = item.Category
                                //TariffId = item.TariffId

                            };
                            waivers.Add(waiver);
                        }

                        if (storageAmount > 0)
                        {
                            var res = new Waiver
                            {
                                CYContainerId = cyContainer.CYContainerId,
                                Description = "Storage Amount",
                                IsWaive = false,
                                Remarks = remarks,
                                ///StorageAmount = storageAmount,
                                TariffAmount = storageAmount,
                                WaiverNumber = waiverno,
                                Category = "Storage"

                            };

                            waivers.Add(res);
                        }


                        if (sealChargr > 0)
                        {
                            var res = new Waiver
                            {
                                CYContainerId = cyContainer.CYContainerId,
                                Description = "Seal Charges",
                                IsWaive = false,
                                Remarks = remarks,
                                TariffAmount = sealChargr,
                                WaiverNumber = waiverno,
                                Category = "SealCharges"
                            };

                            waivers.Add(res);
                        } 
                        if (otherChargAmount > 0)
                        {
                            var res = new Waiver
                            {
                                CYContainerId = cyContainer.CYContainerId,
                                Description = "Special Handling Charges",
                                IsWaive = false,
                                Remarks = remarks,
                                TariffAmount = otherChargAmount,
                                WaiverNumber = waiverno,
                                Category = "SpecialHandlingCharges",
                            };

                            waivers.Add(res);
                        }
                        if (vehicleChargs > 0)
                        {
                            var res = new Waiver
                            {
                                CYContainerId = cyContainer.CYContainerId,
                                Description = "Vehicle Charges",
                                IsWaive = false,
                                Remarks = remarks,
                                TariffAmount = vehicleChargs,
                                WaiverNumber = waiverno,
                                Category = "VehicleCharges"
                            };

                            waivers.Add(res);
                        }
                        if (cyStoragSizeAmount > 0)
                        {
                            var res = new Waiver
                            {
                                CYContainerId = cyContainer.CYContainerId,
                                Description = "Container Storage",
                                IsWaive = false,
                                Remarks = remarks,
                                TariffAmount = cyStoragSizeAmount,
                                WaiverNumber = waiverno,
                                Category = "Storage"
                            };

                            waivers.Add(res);
                        }


                        var invoiceres = _invoiceRepo.GetCYFirstInvoice(cyContainer.CYContainerId);

                        if (invoiceres != null)
                        {
                            var invoices = _invoiceRepo.GetInvoiceItemBycycontainerId(cyContainer.CYContainerId).ToList();

                            var usedwaivewithoutstorage = _invoiceRepo.GetPreviousWaiverCY(cyContainer.CYContainerId).Where(x => x.Category != "Tariff" && x.Category != "Storage").ToList();

                            var usedwaivewithstorage = _invoiceRepo.GetPreviousWaiverCY(cyContainer.CYContainerId).Where(x => x.Category == "Storage").ToList();

                            if (invoices.Count() > 0)
                            {
                                var waiversres = waivers.Where(x => x.Category != "Tariff").ToList();

                                var afterinvoices = invoices.Where(x => x.Category != "Tariff");


                                if (waiversres.Count() > 0)
                                {
                                    foreach (var item in waiversres)
                                    {

                                        var result = afterinvoices.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Amount);

                                        if (result > 0)
                                        {
                                            item.TariffAmount -= result;
                                        }

                                    }

                                    waiversres.RemoveAll(c => c.TariffAmount <= 0);

                                    foreach (var item in waiversres.Where(x => x.Category != "Storage"))
                                    {

                                        var result = usedwaivewithoutstorage.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Discount);

                                        if (result > 0)
                                        {
                                            item.TariffAmount -= result;
                                        }

                                    }


                                    waiversres.RemoveAll(c => c.TariffAmount <= 0);

                                    foreach (var item in waiversres.Where(x => x.Description == "Storage Amount"))
                                    {

                                        var result = usedwaivewithstorage.Where(x => x.Category == "Storage").ToList().Sum(x => x.Discount);

                                        if (result > 0)
                                        {
                                            item.TariffAmount -= result;
                                        }

                                    }

                                    waiversres.RemoveAll(c => c.TariffAmount <= 0);

                                    if (waiversres.Count() > 0)
                                    {
                                        _waiverRepo.AddRange(waiversres);

                                        return new OkObjectResult(new { error = false, message = "Request generated Waiver No", Waiverno = waiverno, ContainerId = cyContainer.CYContainerId });
                                    }
                                    else
                                    {
                                        return new OkObjectResult(new { error = true, message = "there is no charges to Waive" });
                                    }


                                }
                                else
                                {
                                    return new OkObjectResult(new { error = true, message = "there is no charges to Waive" });

                                }

                                

                            }

                            else
                            {
                                waivers.RemoveAll(x => x.Category == "Tariff" );

                                foreach (var item in waivers.Where(x => x.Category != "Storage"))
                                {

                                    var result = usedwaivewithoutstorage.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Discount);

                                    if (result > 0)
                                    {
                                        item.TariffAmount -= result;
                                    }

                                }


                                foreach (var item in waivers.Where(x=> x.Description == "Storage Amount"))
                                {

                                    var result = usedwaivewithstorage.Where(x => x.Category == "Storage").ToList().Sum(x => x.Discount);
                                     
                                    if (result > 0)
                                    {
                                        item.TariffAmount -= result;
                                    }

                                }


                                waivers.RemoveAll(c => c.TariffAmount <= 0);

                                if (waivers.Count() > 0)
                                {
                                    _waiverRepo.AddRange(waivers);

                                    return new OkObjectResult(new { error = false, message = "Request generated Waiver No", Waiverno = waiverno, ContainerId = cyContainer.CYContainerId });

                                }
                                else
                                {
                                    return new OkObjectResult(new { error = true, message = "there is no charges to Waive" });
                                }
                            }

                            

                        }

                        else
                        {
                            if (waivers.Count() > 0)
                            {
                                _waiverRepo.AddRange(waivers);

                                return new OkObjectResult(new { error = false, message = "Request generated Waiver No", Waiverno = waiverno, ContainerId = cyContainer.CYContainerId });

                            }
                            else
                            {
                                return new OkObjectResult(new { error = true, message = "there is no charges to Waive" });

                            }
                        }


                    }



                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "container not found" });

                }


            }



            return new OkObjectResult(new { error = true, message = "please try again" });


        }


        public string GenWaiverCode()
        {
            var date = DateTime.Now;
            var waiverNumber = _waiverRepo.GenerateWaiverNumber();
            string value = waiverNumber.ToString();
            return value + "/" + date.ToString("yy");

        }

        public JsonResult GetWaiverByNumber(string waiverno)
        {
            var data = _waiverRepo.GetAll().Where(x => x.WaiverNumber == waiverno && x.IsWaive == false).ToList();

            if (data.Count() > 0)
            {
                return Json(data);
            }

            return Json("");

        }

        public JsonResult GeUnUsedtWaiverByNumber(string waiverno)
        {
            var data = _waiverRepo.GetAll().Where(x => x.WaiverNumber == waiverno && x.InvoiceCreated == false).ToList();

            if (data.Count() > 0)
            {
                return Json(data);
            }

            return Json("");

        }
        public IActionResult UpdateWaiverInfo(string waiverno, List<Waiver> tariffdata)
        {
             
            try
            {
                var res = _containerRepository.GetWaiverIfnoByWaiverno(waiverno);
                 
                if(res != "OK")
                {
                    return new OkObjectResult(new { error = true, message = res });
                }

                tariffdata.ForEach(x => x.IsWaive = true);

                _waiverRepo.UpdateRange(tariffdata);

                return new OkObjectResult(new { error = false, message = "Updated" });
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = "please try Again" });
            }

            return new OkObjectResult(new { error = true, message = "please try again" });


        }
        
        public IActionResult DeleteWaiverInfo(string waiverno  )
        {
             
            try
            {
                var res = _containerRepository.GetWaiverIfnoByWaiverno(waiverno);
                 
                if(res != "OK")
                {
                    return new OkObjectResult(new { error = true, message = res });
                }

                var resdata =  _waiverRepo.GetAll().Where(x => x.WaiverNumber == waiverno).ToList();
                  
                if(resdata.Where(x=> x.IsWaive == true && x.InvoiceCreated == true).Count() > 0)
                {
                    return new OkObjectResult(new { error = true, message = "can't update or delete due to invoice created" });
                }

                _waiverRepo.DeleteRange(resdata);

                return new OkObjectResult(new { error = false, message = "delete" });
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = "please try Again" });
            }

            return new OkObjectResult(new { error = true, message = "please try again" });


        }


       

        public IActionResult AddBillToLineDetail(string type, string remarks, long? containerId, double storageAmount , double sealChargr 
                                        , double portChargAmount , double otherChargAmount, double vehicleChargs, List<InvoiceItemViewModel> tariffdata)
        {
            List<BillToLine> billToLines = new List<BillToLine>();

            var billToLineno = GenBillToLineCode();

            if (type == null)
            {
                return new OkObjectResult(new { error = true, message = "Please Select Type CY Or CFS" });

            }
             
            if (containerId == null)
            {
                return new OkObjectResult(new { error = true, message = "Please select index" });

            }

            if (tariffdata.Count() <= 0)
            {
                return new OkObjectResult(new { error = true, message = "Please select tariff detail" });

            }
            if (billToLineno == null)
            {
                return new OkObjectResult(new { error = true, message = "Please wait" });

            }


            if (type == "CFS")
            {

                //var billToLine = _billToLineRepository.GetAll().Where(x => x.ContainerIndexId == containerId && x.InoviceCreated == false).ToList();

                //if (billToLine.Count() > 0)
                //{
                //    return new OkObjectResult(new { error = false, message = "Request generated Bill To Line No", BillToLineNumber = billToLine[0].BillToLineNumber });

                //}

                //foreach (var item in tariffdata)
                //{
                //    var BillToLine = new BillToLine
                //    {
                //        ContainerIndexId = containerId,
                //        Description = item.Description,
                //        IsBillToLine = false,
                //        Percent = 0,
                //        Remarks = remarks,
                //        ///StorageAmount = storageAmount,
                //        TariffAmount = item.Amount,
                //        Type = "Tariff",
                //        BillToLineNumber = billToLineno,
                //        //TariffId = item.TariffId


                //    };
                //    billToLines.Add(BillToLine);
                //}


                //if (storageAmount > 0)
                //{
                //    var res = new BillToLine
                //    {
                //        ContainerIndexId = containerId,
                //        Description = "Storage Amount",
                //        IsBillToLine = false,
                //        Remarks = remarks,
                //        Percent = 0,
                //        ///StorageAmount = storageAmount,
                //        Type = "Storage",
                //        TariffAmount = storageAmount,
                //        BillToLineNumber = billToLineno,
                //    };

                //    billToLines.Add(res);
                //}
                //if (sealChargr > 0)
                //{
                //    var res = new BillToLine
                //    {
                //        ContainerIndexId = containerId,
                //        Description = "Seal Charger",
                //        Type = "Other",
                //        Percent = 0,
                //        IsBillToLine = false,
                //        Remarks = remarks,
                //        TariffAmount = sealChargr,
                //        BillToLineNumber = billToLineno,
                //    };

                //    billToLines.Add(res);
                //}
                //if (portChargAmount > 0)
                //{
                //    var res = new BillToLine
                //    {
                //        ContainerIndexId = containerId,
                //        Description = "Port Chargers",
                //        Type = "Other",
                //        Percent = 0,
                //        IsBillToLine = false,
                //        Remarks = remarks,
                //        TariffAmount = portChargAmount,
                //        BillToLineNumber = billToLineno,
                //    };

                //    billToLines.Add(res);
                //}
                //if (otherChargAmount > 0)
                //{
                //    var res = new BillToLine
                //    {
                //        ContainerIndexId = containerId,
                //        Description = "Special Handling Chargers",
                //        Type = "Other",
                //        Percent = 0,
                //        IsBillToLine = false,
                //        Remarks = remarks,
                //        TariffAmount = otherChargAmount,
                //        BillToLineNumber = billToLineno,
                //    };

                //    billToLines.Add(res);
                //}
                //if (vehicleChargs > 0)
                //{
                //    var res = new BillToLine
                //    {
                //        ContainerIndexId = containerId,
                //        Description = "Vehicle Charges",
                //        Type = "Other",
                //        Percent = 0,
                //        IsBillToLine = false,
                //        Remarks = remarks,
                //        TariffAmount = vehicleChargs,
                //        BillToLineNumber = billToLineno,
                //    };

                //    billToLines.Add(res);
                //}

                 
                _billToLineRepository.AddRange(billToLines);

                return new OkObjectResult(new { error = false, message = "Request generated Bill To Line No", BillToLineNumber = billToLineno });

            }



            return new OkObjectResult(new { error = true, message = "please try again" });


        }



        public IActionResult AddBillToLineDetailCY(string type, string remarks, string igm, long? indexno, double storageAmount, double sealChargr, 
                                                   double portChargAmount , double otherChargAmount, double vehicleChargs, double cyStoragSizeAmount, 
                                                    List<InvoiceItemViewModel> tariffdata)
        {

            List<BillToLine> billToLines = new List<BillToLine>();

            var billToLineno = GenBillToLineCode();

            if (type == null)
            {
                return new OkObjectResult(new { error = true, message = "Please Select Type CY Or CFS" });

            }
             
            if (igm == null)
            {
                return new OkObjectResult(new { error = true, message = "Please select igm" });

            }

            if (igm == "")
            {
                return new OkObjectResult(new { error = true, message = "Please select igm" });

            }

            if (indexno == null)
            {
                return new OkObjectResult(new { error = true, message = "Please select index" });

            }

            if (tariffdata.Count() <= 0)
            {
                return new OkObjectResult(new { error = true, message = "Please select tariff detail" });

            }
            if (billToLineno == null)
            {
                return new OkObjectResult(new { error = true, message = "Please wait" });

            }

            //if (type == "CY")
            //{

            //    var cyContainer = _cyContainerRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == indexno);

            //    if (cyContainer != null)
            //    {

            //        var biltolin = _billToLineRepository.GetAll().Where(x => x.CyContainerId == cyContainer.CYContainerId && x.InoviceCreated == false).ToList();



            //        if (biltolin.Count() > 0)
            //        {
            //            return new OkObjectResult(new { error = false, message = "Request generated Bill To Line No", BillToLineNumber = biltolin[0].BillToLineNumber });
            //        }

            //        else
            //        {

            //            foreach (var item in tariffdata)
            //            {
            //                var billtoline = new BillToLine
            //                {
            //                    CyContainerId = cyContainer.CYContainerId,
            //                    Description = item.Description,
            //                    IsBillToLine = false,
            //                    Remarks = remarks,
            //                    //StorageAmount = storageAmount,
            //                    TariffAmount = item.Amount,
            //                    Percent = 0,
            //                    Type = "Tariff",
            //                    BillToLineNumber = billToLineno,
            //                    //TariffId = item.TariffId

            //                };
            //                billToLines.Add(billtoline);
            //            }

            //            if (storageAmount > 0)
            //            {
            //                var res = new BillToLine
            //                {
            //                    CyContainerId = cyContainer.CYContainerId,
            //                    Description = "Storage Amount",
            //                    Type = "Storage",
            //                    IsBillToLine = false,
            //                    Percent = 0,
            //                    Remarks = remarks,
            //                    ///StorageAmount = storageAmount,
            //                    TariffAmount = storageAmount,
            //                    BillToLineNumber = billToLineno,
            //                };

            //                billToLines.Add(res);
            //            }


            //            if (sealChargr > 0)
            //            {
            //                var res = new BillToLine
            //                {
            //                    ContainerIndexId = cyContainer.CYContainerId,
            //                    Description = "Seal Charger",
            //                    Type = "Other",
            //                    Percent = 0,
            //                    IsBillToLine = false,
            //                    Remarks = remarks,
            //                    TariffAmount = sealChargr,
            //                    BillToLineNumber = billToLineno,
            //                };

            //                billToLines.Add(res);
            //            }
            //            if (portChargAmount > 0)
            //            {
            //                var res = new BillToLine
            //                {
            //                    ContainerIndexId = cyContainer.CYContainerId,
            //                    Description = "Port Chargers",
            //                    IsBillToLine = false,
            //                    Type = "Other",
            //                    Percent = 0,
            //                    Remarks = remarks,
            //                    TariffAmount = portChargAmount,
            //                    BillToLineNumber = billToLineno,
            //                };

            //                billToLines.Add(res);
            //            }
            //            if (otherChargAmount > 0)
            //            {
            //                var res = new BillToLine
            //                {
            //                    ContainerIndexId = cyContainer.CYContainerId,
            //                    Description = "Special Handling Chargers",
            //                    IsBillToLine = false,
            //                    Remarks = remarks,
            //                    Type = "Other",
            //                    Percent = 0,
            //                    TariffAmount = otherChargAmount,
            //                    BillToLineNumber = billToLineno,
            //                };

            //                billToLines.Add(res);
            //            }
            //            if (vehicleChargs > 0)
            //            {
            //                var res = new BillToLine
            //                {
            //                    ContainerIndexId = cyContainer.CYContainerId,
            //                    Description = "Vehicle Charges",
            //                    IsBillToLine = false,
            //                    Type = "Other",
            //                    Percent = 0,
            //                    Remarks = remarks,
            //                    TariffAmount = vehicleChargs,
            //                    BillToLineNumber = billToLineno,
            //                };

            //                billToLines.Add(res);
            //            }

                        
            //            if (cyStoragSizeAmount > 0)
            //            {
            //                var res = new BillToLine
            //                {
            //                    ContainerIndexId = cyContainer.CYContainerId,
            //                    Description = "Container Storage",
            //                    IsBillToLine = false,
            //                    Type = "Other",
            //                    Percent = 0,
            //                    Remarks = remarks,
            //                    TariffAmount = cyStoragSizeAmount,
            //                    BillToLineNumber = billToLineno,
            //                };

            //                billToLines.Add(res);
            //            }



            //            _billToLineRepository.AddRange(billToLines);
            //        }



            //        return new OkObjectResult(new { error = false, message = "Request generated Bill To line No", BillToLineNumber = billToLineno, ContainerId = cyContainer.CYContainerId });

            //    }
            //    else
            //    {
            //        return new OkObjectResult(new { error = true, message = "container not found" });

            //    }


            //}



            return new OkObjectResult(new { error = true, message = "please try again" });


        }


        public string GenBillToLineCode()
        {
            var date = DateTime.Now;
            var BillToLineNumber = _billToLineRepository.GenerateBillToLineNumber();
            string value = BillToLineNumber.ToString();
            return value + "/" + date.ToString("yy");

        }
         
        public JsonResult GetBillToLineByNumber(string billtolineno)
        {
            //var data = _billToLineRepository.GetAll().Where(x => x.BillToLineNumber == billtolineno && x.InoviceCreated == false && x.IsActive == false).ToList();

            //if (data.Count() > 0)
            //{
            //    return Json(data);
            //}

            return Json("");

        }

        public IActionResult UpdateBillTollineInfo(string billtolineno, List<BillToLine> tariffdata)
        {
            var BillToLines = new List<BillToLine>();


            try
            {

                foreach (var item in tariffdata)
                {

                    // item.StorageAmount = storageAmount;
                     item.InoviceCreated = false;
                    BillToLines.Add(item);
                }

                _billToLineRepository.UpdateRange(BillToLines);

                return new OkObjectResult(new { error = true, message = "Updated" });


            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = "please try Again" });

            }

            return new OkObjectResult(new { error = true, message = "please try again" });


        }
         
        public IActionResult ReprintWaiver()
        {
            return View();
        }
         
        public IActionResult ExaminationRequest()
        {

            ViewData["ClearingAgentsAgents"] = _clearingAgentRepo.GetAll()
                .Select(v => new SelectListItem { Text = $"{v.Name}-{v.ChallanNumber} -- {v.LicenceNumber} ", Value = v.ClearingAgentId.ToString() });

            return View();
        }

        public IActionResult SaveExaminationRequest(  ExaminationRequest model , string type , long? containerid, double igmDetilCBM, double destuffCBM)
        {
            try
            {
                if (type != null && containerid != null)
                {
                    if(type == "CFS")
                    {

                        var cfsres = _containerIndexRepo.GetContainerIndexById(containerid ?? 0);

                        if(cfsres == null)
                        {
                            return new OkObjectResult(new { error = true, message = "no index found" });
                        }

                        var listcfsres = _containerIndexRepo.GetContainerIndexByIgmAndIndex(cfsres.VirNo, cfsres.IndexNo ?? 0).ToList();
                        
                        if(listcfsres.Count() == 0)
                        {
                            return new OkObjectResult(new { error = true, message = "no index detail found" });
                        }

                        var containerindexidlist = string.Join(",", listcfsres.Select(n => n.ContainerIndexId.ToString()).ToArray());

                        var invoiceres = _invoiceRepo.GetCfsInvoices(containerindexidlist).ToList();

                        if (invoiceres.Count() > 0)
                        {
                            return new JsonResult(new { error = true, message = "invoice created agains't IGM : " + cfsres.VirNo + " and INDEX " + cfsres.IndexNo });
                        }

                        var auction = _auctionRepository.GetAll().Where(x => x.ContainerIndexId == containerid).LastOrDefault();

                        if(auction != null && model.DeliveryStatus != "Auction")
                        {
                            return new OkObjectResult(new { error = true, message = "please select Delivery Status Auction in examination form" });
                        }

                        if (auction == null && model.DeliveryStatus == "Auction")
                        {
                            return new OkObjectResult(new { error = true, message = "Delivery Status are aution but auction info is not updated " });
                        }

                        //var data = _invoiceRepo.GetAll().Where(x => x.ContainerIndexId == containerid).LastOrDefault();
                        //if(data != null)
                        //{
                        //    return new OkObjectResult(new { error = true, message = "info not update due to  Invoice Creeate" });

                        //}


                        var containerindex = _containerIndexRepo.GetContainerIndexById(containerid ?? 0);

                        if(containerindex != null)
                        {
                            containerindex.CBM = destuffCBM;
                            containerindex.ExaminationRequestCBM = model.ExaminationRequestCBM;
                            containerindex.MeasurementCBM = igmDetilCBM;

                            _containerIndexRepo.Update(containerindex);
                        }

                        var destuffedContainer = _containerIndexRepo.GetDestuffedIndexDetail(containerid ?? 0);

                        if (destuffedContainer != null)
                        {
                            destuffedContainer.CBM = destuffCBM;
                            _destuffedContainerRepository.Update(destuffedContainer);
                        }

                        var examinationreqdata = _examinationRequestRepo.GetExaminationRequestByContainerindexId( containerid ?? 0);

                        if (examinationreqdata != null)
                        {


                            examinationreqdata.BECashNo = model.BECashNo;
                            examinationreqdata.ClearingAgentId = model.ClearingAgentId;
                            examinationreqdata.ClearingAgentRep = model.ClearingAgentRep;
                            examinationreqdata.CNIC = model.CNIC;
                            examinationreqdata.ContainerIndexId = containerid;
                            examinationreqdata.CustomOutChargeDate = model.CustomOutChargeDate;
                            examinationreqdata.CustomRegDate = model.CustomRegDate;
                            examinationreqdata.CustomRegNo = model.CustomRegNo;
                            examinationreqdata.ExaminationRequestCBM = model.ExaminationRequestCBM;
                            examinationreqdata.LineDoNumber = model.LineDoNumber;
                            examinationreqdata.PhoneNumber = model.PhoneNumber;
                            examinationreqdata.PresentationDate = model.PresentationDate;
                            examinationreqdata.ExaminationDate = model.ExaminationDate;
                            examinationreqdata.GroundingDate = model.GroundingDate;
                            examinationreqdata.IsTPCargo = model.IsTPCargo;
                            examinationreqdata.IsEPZCargo = model.IsEPZCargo;
                            examinationreqdata.Email = model.Email;
                            examinationreqdata.DeliveryStatus = model.DeliveryStatus;

                            _examinationRequestRepo.Update(examinationreqdata);

                            return new OkObjectResult(new { error = false, message = "Updated" });

                        }
                        else
                        {
                            var modelData = new ExaminationRequest
                            {
                                BECashNo = model.BECashNo,
                                ClearingAgentId = model.ClearingAgentId,
                                ClearingAgentRep = model.ClearingAgentRep,
                                CNIC = model.CNIC,
                                ContainerIndexId = containerid,
                                CustomOutChargeDate = model.CustomOutChargeDate,
                                CustomRegDate = model.CustomRegDate,
                                CustomRegNo = model.CustomRegNo,
                                ExaminationRequestCBM = model.ExaminationRequestCBM,
                                LineDoNumber = model.LineDoNumber,
                                PhoneNumber = model.PhoneNumber,
                                PresentationDate = model.PresentationDate,
                                ExaminationDate = model.ExaminationDate,
                                GroundingDate = model.GroundingDate,
                                IsTPCargo = model.IsTPCargo,
                                IsEPZCargo = model.IsEPZCargo,
                                Email = model.Email,
                                DeliveryStatus = model.DeliveryStatus

                            };

                            _examinationRequestRepo.Add(modelData);

                            return new OkObjectResult(new { error = false, message = "Save" });
                        }
                     


                    }

                    

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "please select type and index" });

                }
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = "please try again" });

            }
            



            return new OkObjectResult(new { error = true, message = "please try again" });
        }


        public IActionResult SaveExaminationRequestCY(ExaminationRequest model , string igm , int ? indexno )
        {
            try
            {

                var topindexes = _cyContainerRepo.GetContainerIndexByIGMAndContainerNo(igm, indexno ?? 0).ToList();

                if(topindexes.Count() == 0)
                {
                    return new OkObjectResult(new { error = true, message = "no index found" });

                }
                var containerindexidlist = string.Join(",", topindexes.Select(n => n.CYContainerId.ToString()).ToArray());

                var invoiceres = _invoiceRepo.GetCYInvoices(containerindexidlist).ToList();

                if (invoiceres.Count() > 0)
                {
                    return new JsonResult(new { error = true, message = "invoice created agains't IGM : " + igm + " and INDEX " + indexno });
                }
                 
                var auction = _auctionRepository.GetAll().Where(x => x.CYContainerId == topindexes[0].CYContainerId).LastOrDefault();

                if (auction != null && model.DeliveryStatus != "Auction")
                {
                    return new OkObjectResult(new { error = true, message = "please select Delivery Status Auction in examination form" });
                }

                if (auction == null && model.DeliveryStatus == "Auction")
                {
                    return new OkObjectResult(new { error = true, message = "Delivery Status are aution but auction info is not updated " });
                }

                //var cycontainer = _cyContainerRepo.GetAll().Where(x => x.VIRNo == igm && x.IndexNo == indexno).ToList();

                //foreach (var item in cycontainer)
                //{
                //    var data = _invoiceRepo.GetAll().Where(x => x.CYContainerId == item.CYContainerId).LastOrDefault();
                //    if (data != null)
                //    {
                //        return new OkObjectResult(new { error = true, message = "info not update due to  Invoice Creeate" });
                //    }
                //}


                //foreach (var item in cycontainer)
                //{
                var examinationreqdata = _examinationRequestRepo.GetExaminationRequestByCYContainerId(topindexes[0].CYContainerId);
                    if (examinationreqdata != null)
                    {
                        examinationreqdata.BECashNo = model.BECashNo;
                        examinationreqdata.ClearingAgentId = model.ClearingAgentId;
                        examinationreqdata.ClearingAgentRep = model.ClearingAgentRep;
                        examinationreqdata.CNIC = model.CNIC;
                        //examinationreqdata.CYContainerId = item.CYContainerId;
                        examinationreqdata.CYContainerId = topindexes[0].CYContainerId;
                        examinationreqdata.CustomOutChargeDate = model.CustomOutChargeDate;
                        examinationreqdata.CustomRegDate = model.CustomRegDate;
                        examinationreqdata.CustomRegNo = model.CustomRegNo;
                        examinationreqdata.ExaminationRequestCBM = model.ExaminationRequestCBM;
                        examinationreqdata.LineDoNumber = model.LineDoNumber;
                        examinationreqdata.PhoneNumber = model.PhoneNumber;
                        examinationreqdata.PresentationDate = model.PresentationDate;
                        examinationreqdata.ExaminationDate = model.ExaminationDate;
                        examinationreqdata.GroundingDate = model.GroundingDate;
                        examinationreqdata.IsTPCargo = model.IsTPCargo;
                        examinationreqdata.IsEPZCargo = model.IsEPZCargo;
                        examinationreqdata.Email = model.Email;
                        examinationreqdata.DeliveryStatus = model.DeliveryStatus;
                     
                        _examinationRequestRepo.Update(examinationreqdata);
                     
                    }
                    else
                    {

                        var modelData = new ExaminationRequest
                        {
                            BECashNo = model.BECashNo,
                            ClearingAgentId = model.ClearingAgentId,
                            ClearingAgentRep = model.ClearingAgentRep,
                            CNIC = model.CNIC,
                            //CYContainerId = item.CYContainerId,
                            CYContainerId = topindexes[0].CYContainerId,
                            CustomOutChargeDate = model.CustomOutChargeDate,
                            CustomRegDate = model.CustomRegDate,
                            CustomRegNo = model.CustomRegNo,
                            ExaminationRequestCBM = model.ExaminationRequestCBM,
                            LineDoNumber = model.LineDoNumber,
                            PhoneNumber = model.PhoneNumber,
                            PresentationDate = model.PresentationDate,
                            ExaminationDate = model.ExaminationDate,
                            GroundingDate = model.GroundingDate,
                            IsTPCargo = model.IsTPCargo,
                            IsEPZCargo = model.IsEPZCargo,
                            Email = model.Email,
                            DeliveryStatus = model.DeliveryStatus

                        };

                        _examinationRequestRepo.Add(modelData);

                    }


                //}

                return new OkObjectResult(new { error = false, message = "Save" });




            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = "please try again" });
            }
            return new OkObjectResult(new { error = true, message = "Please Try Again" });

        }


        public JsonResult GetExaminationRequestdata(long? containerId  )
        {

            var resdata = _containerRepository.GetExaminationRequestCFS(containerId);

            if(resdata != null)
            {
                return Json(resdata);
            }

            return Json("");

            //    var cfsdata = _examinationRequestRepo.GetAll().Where(x => x.ContainerIndexId == containerId).FirstOrDefault();

            //    if(cfsdata != null)
            //    { 
            //        return Json(cfsdata);
            //    }

            //    return Json("");

            //return Json("");
        }


        public JsonResult GetExaminationRequestdatacy(string igm , int ? indexno)
        {
            //var cycontainer = _cyContainerRepo.GetAll().Where(x => x.VIRNo == igm && x.IndexNo == indexno).FirstOrDefault();
            //if(cycontainer != null)
            //{

            //    var cfsdata = _examinationRequestRepo.GetAll().Where(x => x.CYContainerId == cycontainer.CYContainerId).FirstOrDefault();

            //    if (cfsdata != null)
            //    {
            //        return Json(cfsdata);
            //    }

            //    return Json("");

            //}
             
            //return Json("");


            var resdata = _containerRepository.GetExaminationRequestCY(igm, indexno);

            if (resdata != null)
            {
                return Json(resdata);
            }

            return Json("");
        }




        public IActionResult ExaminationChargesView()
        {

            ViewData["ShippingAgents"] = _agentRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.Name , Value = v.ShippingAgentId.ToString() });


            ViewData["GoodsHead"] = _goodsHeadRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });
            
            ViewData["ExaminationCharge"] = _examinationChargeRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.ExaminationChargeName, Value = v.ExaminationChargeId.ToString() });
             

            return View();
        }


        public IActionResult AssignHundredPercent(string igm, long indexno, long maunualAmount, string type, string remarks)
        {
            try
            {

                if(type == "CFS")
                {
                    var containerindex = _containerIndexRepo.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno).LastOrDefault();

                    if (containerindex != null)
                    {
                        //var resdata = _containerRepository.CheckDeliveryStatus(igm, indexno);

                        //if (resdata == true)
                        //{
                        //    return new OkObjectResult(new { error = true, message = "cant create BTL due to cargo is delivered" });
                        //}

                        //if (containerindex.IsGateOut == false)
                        //{
                            var billtoline = _billToLineRepository.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno && x.IndexType == type && x.InoviceCreated == false).ToList();

                            if (billtoline.Count() > 0)
                            {
                                return new OkObjectResult(new { error = true, message = "alreday assign" });

                            }
                            else
                            {
                                var biltolin = new BillToLine
                                {
                                    VirNo = igm,
                                    IndexNo = indexno,
                                    IndexType = type,
                                    InoviceCreated = false,
                                    Type = "HundredPercent",
                                    CreatedDate = DateTime.Now,
                                    Remarks = remarks,
                                    TariffAmount = maunualAmount,

                                };
                                _billToLineRepository.Add(biltolin);

                                return new OkObjectResult(new { error = false, message = "save" });

                            }
                        //}

                        //else
                        //{
                        //    return new OkObjectResult(new { error = true, message = "index out" });
                        //}




                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "Index Not found" });
                    }
                }
                if(type == "CY")
                {
                    var cycontainer = _cyContainerRepo.GetAll().Where(x => x.VIRNo == igm && x.IndexNo == indexno).LastOrDefault();

                    if (cycontainer != null)
                    {
                        //var resdata = _containerRepository.CheckDeliveryStatus(igm, indexno);

                        //if (resdata == true)
                        //{
                        //    return new OkObjectResult(new { error = true, message = "cant create BTL due to cargo is delivered" });
                        //}

                        //if (cycontainer.IsGateOut == false)
                        //{
                            var billtoline = _billToLineRepository.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno && x.IndexType == type && x.InoviceCreated == false).ToList();

                            if (billtoline.Count() > 0)
                            {
                                return new OkObjectResult(new { error = true, message = "alreday assign" });

                            }
                            else
                            {
                                var biltolin = new BillToLine
                                {
                                    VirNo = igm,
                                    IndexNo = indexno,
                                    IndexType = type,
                                    InoviceCreated = false,
                                    Type = "HundredPercent",
                                    CreatedDate = DateTime.Now,
                                    Remarks = remarks,
                                    TariffAmount = maunualAmount,

                                };
                                _billToLineRepository.Add(biltolin);

                                return new OkObjectResult(new { error = false, message = "save" });

                            }
                        //}

                        //else
                        //{
                        //    return new OkObjectResult(new { error = true, message = "index out" });
                        //}




                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "Index Not found" });
                    }
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "type not deffine" });
                }


            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }
        }
        public IActionResult AssignExStorage(string igm, long indexno , string type, string remarks)
        {
            try
            { 
                if(type == "CFS")
                {
                    var containerindex = _containerIndexRepo.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno).LastOrDefault();

                    if (containerindex != null)
                    {
                        //var resdata = _containerRepository.CheckDeliveryStatus(igm, indexno);

                        //if (resdata == true)
                        //{
                        //    return new OkObjectResult(new { error = true, message = "cant create BTL due to cargo is delivered" });
                        //}

                        //if (containerindex.IsGateOut == false)
                        //{
                            var billtoline = _billToLineRepository.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno && x.IndexType == type && x.InoviceCreated == false).ToList();

                            if (billtoline.Count() > 0)
                            {
                                return new OkObjectResult(new { error = true, message = "alreday assign" });

                            }
                            else
                            {
                                var biltolin = new BillToLine
                                {
                                    VirNo = igm,
                                    IndexNo = indexno,
                                    IndexType = type,
                                    InoviceCreated = false,
                                    Type = "ExStorage",
                                    CreatedDate = DateTime.Now,
                                    Remarks = remarks,

                                };
                                _billToLineRepository.Add(biltolin);

                                return new OkObjectResult(new { error = false, message = "save" });

                            }
                        //}

                        //else
                        //{
                        //    return new OkObjectResult(new { error = true, message = "index out" });
                        //}




                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "Index Not found" });
                    }
                }
                if(type == "CY")
                {
                    var cycontainer = _cyContainerRepo.GetAll().Where(x => x.VIRNo == igm && x.IndexNo == indexno).LastOrDefault();

                    if (cycontainer != null)
                    {
                        //var resdata = _containerRepository.CheckDeliveryStatus(igm, indexno);

                        //if (resdata == true)
                        //{
                        //    return new OkObjectResult(new { error = true, message = "cant create BTL due to cargo is delivered" });
                        //}

                        //if (cycontainer.IsGateOut == false)
                        //{
                            var billtoline = _billToLineRepository.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno && x.IndexType == type && x.InoviceCreated == false).ToList();

                            if (billtoline.Count() > 0)
                            {
                                return new OkObjectResult(new { error = true, message = "alreday assign" });

                            }
                            else
                            {
                                var biltolin = new BillToLine
                                {
                                    VirNo = igm,
                                    IndexNo = indexno,
                                    IndexType = type,
                                    InoviceCreated = false,
                                    Type = "ExStorage",
                                    CreatedDate = DateTime.Now,
                                    Remarks = remarks,

                                };
                                _billToLineRepository.Add(biltolin);

                                return new OkObjectResult(new { error = false, message = "save" });

                            }
                        //}

                        //else
                        //{
                        //    return new OkObjectResult(new { error = true, message = "index out" });
                        //}




                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "Index Not found" });
                    }
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "Type Not Deffine" });
                }



            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }


        }
        public IActionResult AssignOnlyTariff(string igm, long indexno , string type, string remarks)
        {
            try
            {
                if(type == "CFS")
                {
                    var containerindex = _containerIndexRepo.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno).LastOrDefault();

                    if (containerindex != null)
                    {
                        //var resdata = _containerRepository.CheckDeliveryStatus(igm, indexno);

                        //if (resdata == true)
                        //{
                        //    return new OkObjectResult(new { error = true, message = "cant create BTL due to cargo is delivered" });
                        //}

                        //if (containerindex.IsGateOut == false)
                        //{
                            var billtoline = _billToLineRepository.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno && x.IndexType == type && x.InoviceCreated == false).ToList();

                            if (billtoline.Count() > 0)
                            {
                                return new OkObjectResult(new { error = true, message = "alreday assign" });

                            }
                            else
                            {
                                var biltolin = new BillToLine
                                {
                                    VirNo = igm,
                                    IndexNo = indexno,
                                    IndexType = type,
                                    InoviceCreated = false,
                                    Type = "OnlyTariff",
                                    CreatedDate = DateTime.Now,
                                    Remarks = remarks,

                                };
                                _billToLineRepository.Add(biltolin);

                                return new OkObjectResult(new { error = false, message = "save" });

                            }
                        //}

                        //else
                        //{
                        //    return new OkObjectResult(new { error = true, message = "index out" });
                        //}




                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "Index Not found" });
                    }
                }
                if(type == "CY")
                {
                    var cycontainer = _cyContainerRepo.GetAll().Where(x => x.VIRNo == igm && x.IndexNo == indexno).LastOrDefault();

                    if (cycontainer != null)
                    {
                        //var resdata = _containerRepository.CheckDeliveryStatus(igm, indexno);

                        //if (resdata == true)
                        //{
                        //    return new OkObjectResult(new { error = true, message = "cant create BTL due to cargo is delivered" });
                        //}

                        //if (cycontainer.IsGateOut == false)
                        //{
                            var billtoline = _billToLineRepository.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno && x.IndexType == type && x.InoviceCreated == false).ToList();

                            if (billtoline.Count() > 0)
                            {
                                return new OkObjectResult(new { error = true, message = "alreday assign" });

                            }
                            else
                            {
                                var biltolin = new BillToLine
                                {
                                    VirNo = igm,
                                    IndexNo = indexno,
                                    IndexType = type,
                                    InoviceCreated = false,
                                    Type = "OnlyTariff",
                                    CreatedDate = DateTime.Now,
                                    Remarks = remarks,

                                };
                                _billToLineRepository.Add(biltolin);

                                return new OkObjectResult(new { error = false, message = "save" });

                            }
                        //}

                        //else
                        //{
                        //    return new OkObjectResult(new { error = true, message = "index out" });
                        //}




                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "Index Not found" });
                    }
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "type not deffine" });
                }



            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }


        }


        public IActionResult SaveManualAmount(string igm , long indexno ,  long maunualAmount , string type, string remarks)
        {
            try
            {
                if(type == "CFS")
                {
                    var containerindex = _containerIndexRepo.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno).LastOrDefault();

                    

                    if (containerindex != null)
                    {
                        //var resdata = _containerRepository.CheckDeliveryStatus(igm, indexno);

                        //if (resdata == true)
                        //{
                        //    return new OkObjectResult(new { error = true, message = "cant create BTL due to cargo is delivered" });
                        //}

                        //if (containerindex.IsGateOut == false)
                        //{
                        var billtoline = _billToLineRepository.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno && x.IndexType == type && x.InoviceCreated == false).ToList();

                            if (billtoline.Count() > 0)
                            {
                                return new OkObjectResult(new { error = true, message = "alreday assign" });

                            }
                            else
                            {
                                var biltolin = new BillToLine
                                {
                                    VirNo = igm,
                                    IndexNo = indexno,
                                    IndexType = type,
                                    InoviceCreated = false,
                                    Type = "ManualAmount",
                                    CreatedDate = DateTime.Now,
                                    Remarks = remarks,
                                    TariffAmount = maunualAmount,

                                };
                                _billToLineRepository.Add(biltolin);

                                return new OkObjectResult(new { error = false, message = "save" });

                            }
                        //}

                        //else
                        //{
                        //    return new OkObjectResult(new { error = true, message = "index out" });
                        //}




                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "Index Not found" });
                    }
                }
                if(type == "CY")
                {
                    var cycontainer = _cyContainerRepo.GetAll().Where(x => x.VIRNo == igm && x.IndexNo == indexno).LastOrDefault();


                    if (cycontainer != null)
                    {
                        //if (cycontainer.IsGateOut == false)
                        //{
                            //var resdata = _containerRepository.CheckDeliveryStatus(igm, indexno);

                            //if (resdata == true)
                            //{
                            //    return new OkObjectResult(new { error = true, message = "cant create BTL due to cargo is delivered" });
                            //}

                            var billtoline = _billToLineRepository.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno && x.IndexType == type && x.InoviceCreated == false).ToList();

                            if (billtoline.Count() > 0)
                            {
                                return new OkObjectResult(new { error = true, message = "alreday assign" });

                            }
                            else
                            {
                                var biltolin = new BillToLine
                                {
                                    VirNo = igm,
                                    IndexNo = indexno,
                                    IndexType = type,
                                    InoviceCreated = false,
                                    Type = "ManualAmount",
                                    CreatedDate = DateTime.Now,
                                    Remarks = remarks,
                                    TariffAmount = maunualAmount,

                                };
                                _billToLineRepository.Add(biltolin);

                                return new OkObjectResult(new { error = false, message = "save" });

                            }
                        //}

                        //else
                        //{
                        //    return new OkObjectResult(new { error = true, message = "index out" });
                        //}




                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "Index Not found" });
                    }
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "Type not deffine" });
                }


            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }
        }


        public IActionResult DeleteBillToLineById(long key)
        {
            try
            {
                var res = _billToLineRepository.GetAll().Where(x => x.BillToLineId == key && x.InoviceCreated == false).LastOrDefault();

                if(res != null)
                { 
                    _billToLineRepository.Delete(res);
                      
                    return new OkObjectResult(new { error = false, message = "Delete" });

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "not delete due to invoice created" });
                }
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }


        }



        public double CalculateBillToLineCFS(double storageAmount, double sealChargr  , double otherChargAmount, double vehicleChargs , string igm , long indexno , string type
                                            , List<InvoiceItemViewModel> tariffdata)
        {
            //List<BillToLine> billToLines = new List<BillToLine>();
            List<InvoiceItemViewModel> newbillToLines = new List<InvoiceItemViewModel>();
            var  billToLineAmount = 0.00;

            var container =  _containerIndexRepo.GetSingleIndexInfo(igm , indexno);

            if(container  == null)
            {
                return Math.Round(billToLineAmount);
            }

            tariffdata.RemoveAll(x => x.Type == "Auction");

            //var resbillToLine = _billToLineRepository.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno && x.IndexType == type && x.InoviceCreated == true).ToList();

            //if (resbillToLine.Count() > 0)
            //{
            //    billToLineAmount = resbillToLine.Sum(x => x.InvoiceAmount);
            //}

            var undeliverdresbillToLine = _billToLineRepository.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno && x.IndexType == type  && x.InoviceCreated == false).LastOrDefault();

                if(undeliverdresbillToLine != null)
                {

                foreach (var item in tariffdata)
                {
                    var BillToLine = new InvoiceItemViewModel
                    {
                       Amount = item.Amount,
                        Type = item.Category,
                        Description = item.Description
                    };
                    newbillToLines.Add(BillToLine);
                }


                if (storageAmount > 0)
                {
                    var res = new InvoiceItemViewModel
                    {
                        Type = "Storage",
                        Amount = storageAmount,
                        Description = "Storage Amount",
                    };

                    newbillToLines.Add(res);
                }
                if (sealChargr > 0)
                {
                    var res = new InvoiceItemViewModel
                    {
                        Type = "SealCharges",
                        Amount = sealChargr,
                        Description = "Seal Charges"
                    };

                    newbillToLines.Add(res);
                }
                //if (portChargAmount > 0)
                //{
                //    var res = new BillToLine
                //    {
                //        Type = "Other",
                //        TariffAmount = portChargAmount,
                //    };

                //    billToLines.Add(res);
                //}
                if (otherChargAmount > 0)
                {
                    var res = new InvoiceItemViewModel
                    {
                        Type = "SpecialHandlingCharges",
                        Amount = otherChargAmount,
                        Description = "Special Handling Charges"
                    };

                    newbillToLines.Add(res);
                }
                if (vehicleChargs > 0)
                {
                    var res = new InvoiceItemViewModel
                    {
                        Type = "VehicleCharges",
                        Amount = vehicleChargs,
                        Description = "Vehicle Charges",
                    };

                    newbillToLines.Add(res);
                }


                var invoiceres = _invoiceRepo.GetCFSFirstInvoice(container.ContainerIndexId );

                if(invoiceres != null)
                {

                    var paidinvoiceitems = _invoiceRepo.GetInvoiceItemBycontainerIndexId(container.ContainerIndexId).ToList();

                    var usedwaive = _invoiceRepo.GetPreviousWaiverCFS(container.ContainerIndexId).Where(x => x.Category != "Tariff").ToList();

                    if (paidinvoiceitems.Count() > 0)
                    {

                        var newinvoiceitemList = newbillToLines.Where(x => x.Type != "Tariff").ToList();

                        var afterinvoices = paidinvoiceitems.Where(x => x.Category != "Tariff");

                        if (newinvoiceitemList.Count() > 0 )
                        {
                            foreach (var item in newinvoiceitemList)
                            {
                                var result = afterinvoices.Where(x => x.Category == item.Type && x.Description == item.Description).ToList().Sum(x => x.Amount);

                                if (result > 0)
                                {
                                    item.Amount -= result;
                                }
                            }

                            newinvoiceitemList.RemoveAll(c => c.Amount <= 0);

                            foreach (var item in newinvoiceitemList)
                            {
                                var result = usedwaive.Where(x => x.Category == item.Type && x.Description == item.Description).ToList().Sum(x => x.Discount);

                                if (result > 0)
                                {
                                    item.Amount -= result;
                                }
                            }


                            newinvoiceitemList.RemoveAll(c => c.Amount <= 0);

                            newbillToLines = newinvoiceitemList;

                        }

                    }

                    else
                    {
                        newbillToLines.RemoveAll(x => x.Type == "Tariff");

                        foreach (var item in newbillToLines)
                        {
                            var result = usedwaive.Where(x => x.Category == item.Type && x.Description == item.Description).ToList().Sum(x => x.Discount);

                            if (result > 0)
                            {
                                item.Amount -= result;
                            }
                        }

                        newbillToLines.RemoveAll(c => c.Amount <= 0);


                    }


                }


                if (undeliverdresbillToLine.Type == "HundredPercent")
                {
                    var Amount = newbillToLines.Sum(x => x.Amount);

                    billToLineAmount += Amount;
                }

                if (undeliverdresbillToLine.Type == "ExStorage")
                {
                    var Amount = newbillToLines.Where(x => x.Type != "Storage").Sum(x => x.Amount);

                    billToLineAmount += Amount;
                }
                if (undeliverdresbillToLine.Type == "OnlyTariff")
                {
                    var Amount = newbillToLines.Where(x => x.Type == "Tariff").Sum(x => x.Amount);

                    billToLineAmount += Amount;
                }
                
                if (undeliverdresbillToLine.Type == "ManualAmount")
                { 
                    var Amount = undeliverdresbillToLine.TariffAmount;

                    billToLineAmount += Amount;
                }


            }
                 
            return Math.Round( billToLineAmount);
             
        }


        public double CalculateBillToLinCY(  double storageAmount, double sealChargr,  double otherChargAmount, double vehicleChargs, double cyStoragSizeAmount , string igm, long indexno, string type ,
                                                    List<InvoiceItemViewModel> tariffdata)
        {
            //List<BillToLine> billToLines = new List<BillToLine>();
            List<InvoiceItemViewModel> newbillToLines = new List<InvoiceItemViewModel>();

            var billToLineAmount = 0.00;

            var container = _cyContainerRepo.GetContainerCYByIGMAndIndex(igm, Convert.ToInt32(indexno));

            if (container == null)
            {
                return Math.Round(billToLineAmount);
            }

            tariffdata.RemoveAll(x => x.Type == "Auction");


           //var resbillToLine = _billToLineRepository.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno && x.IndexType == type && x.InoviceCreated == true).ToList();

           // if (resbillToLine.Count() > 0 )
           // {
           //     billToLineAmount = resbillToLine.Sum(x => x.InvoiceAmount);

           // }

            var undeliverdresbillToLine = _billToLineRepository.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno && x.IndexType == type && x.InoviceCreated == false).LastOrDefault();

            if (undeliverdresbillToLine != null)
            {
                foreach (var item in tariffdata)
                {
                    var BillToLine = new InvoiceItemViewModel
                    {
                        Amount = item.Amount,
                        Type = item.Category,
                        Description = item.Description
                    };
                    newbillToLines.Add(BillToLine);

                }


                if (storageAmount > 0)
                {
                    var res = new InvoiceItemViewModel
                    {
                        Type = "Storage",
                        Amount = storageAmount,
                        Description = "Storage Amount"
                    };

                    newbillToLines.Add(res);
                }

                if (sealChargr > 0)
                {
                    var res = new InvoiceItemViewModel
                    {
                        Type = "SealCharges",
                        Amount = sealChargr,
                        Description = "Seal Charges"
                    };

                    newbillToLines.Add(res);
                }
                //if (portChargAmount > 0)
                //{
                //    var res = new BillToLine
                //    { 
                //        Type = "Other",
                //        TariffAmount = portChargAmount,
                //    };

                //    billToLines.Add(res);
                //}
                if (otherChargAmount > 0)
                {
                    var res = new InvoiceItemViewModel
                    { 
                        Type = "SpecialHandlingCharges", 
                        Amount = otherChargAmount,
                        Description = "Special Handling Charges"
                    };

                    newbillToLines.Add(res);
                }
                if (vehicleChargs > 0)
                {
                    var res = new InvoiceItemViewModel
                    { 
                        Type = "VehicleCharges", 
                        Amount = vehicleChargs,
                        Description = "Vehicle Charges"
                    };

                    newbillToLines.Add(res);
                }

                //if (cyStoragSizeAmount > 0)
                //{
                //    var res = new InvoiceItemViewModel
                //    { 
                //        Type = "Other", 
                //        TariffAmount = cyStoragSizeAmount,
                //    };

                //    newbillToLines.Add(res);
                //}



                if (cyStoragSizeAmount > 0)
                {
                    var result = newbillToLines.Find(x => x.Type == "Storage");

                    if (result != null)
                    {
                        result.Amount += cyStoragSizeAmount;
                    }
                    else
                    {
                        var res = new InvoiceItemViewModel
                        {
                            Description = "Storage Amount",
                            Type = "Storage",
                            Amount = cyStoragSizeAmount,
                            
                        };

                        newbillToLines.Add(res);
                    }
                }

                var invoiceres = _invoiceRepo.GetCYFirstInvoice(container.CYContainerId);

                if (invoiceres != null)
                {
                    var paidinvoiceitems = _invoiceRepo.GetInvoiceItemBycycontainerId(container.CYContainerId).ToList();

                    var usedwaivewithoutstorage = _invoiceRepo.GetPreviousWaiverCY(container.CYContainerId).Where(x => x.Category != "Tariff" && x.Category != "Storage").ToList();

                    var usedwaivewithstorage = _invoiceRepo.GetPreviousWaiverCY(container.CYContainerId).Where(x => x.Category == "Storage").ToList();

                    if (paidinvoiceitems.Count() > 0)
                    {

                        var newinvoiceitemList = newbillToLines.Where(x => x.Type != "Tariff").ToList();

                        var afterinvoices = paidinvoiceitems.Where(x => x.Category != "Tariff");

                        if (newinvoiceitemList.Count() > 0)
                        {
                            foreach (var item in newinvoiceitemList)
                            {
                                var result = afterinvoices.Where(x => x.Category == item.Type && x.Description == item.Description).ToList().Sum(x => x.Amount);

                                if (result > 0)
                                {
                                    item.Amount -= result;
                                }
                            }

                            newinvoiceitemList.RemoveAll(c => c.Amount <= 0);


                            foreach (var item in newinvoiceitemList.Where(x => x.Type != "Storage"))
                            {
                                var result = usedwaivewithoutstorage.Where(x => x.Category == item.Type && x.Description == item.Description).ToList().Sum(x => x.Discount);

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

                            newbillToLines = newinvoiceitemList;
 
                        }

                    }

                    else
                    {
                        newbillToLines.RemoveAll(x => x.Type == "Tariff");

                        foreach (var item in newbillToLines.Where(x => x.Type != "Storage"))
                        {
                            var result = usedwaivewithoutstorage.Where(x => x.Category == item.Type && x.Description == item.Description).ToList().Sum(x => x.Discount);

                            if (result > 0)
                            {
                                item.Amount -= result;
                            }
                        }

                        newbillToLines.RemoveAll(c => c.Amount <= 0);


                        foreach (var item in newbillToLines.Where(x => x.Description == "Storage Amount"))
                        {

                            var result = usedwaivewithstorage.Where(x => x.Category == "Storage").ToList().Sum(x => x.Discount);

                            if (result > 0)
                            {
                                item.Amount -= result;
                            }

                        }

                        newbillToLines.RemoveAll(c => c.Amount <= 0);


                    }


                }



                if (undeliverdresbillToLine.Type == "HundredPercent")
                {
                    var Amount = newbillToLines.Sum(x => x.Amount);

                    billToLineAmount += Amount;
                }

                if (undeliverdresbillToLine.Type == "ExStorage")
                {
                    var Amount = newbillToLines.Where(x => x.Type != "Storage").Sum(x => x.Amount);

                    billToLineAmount += Amount;
                }
                if (undeliverdresbillToLine.Type == "OnlyTariff")
                {
                    var Amount = newbillToLines.Where(x => x.Type == "Tariff").Sum(x => x.Amount);

                    billToLineAmount += Amount;
                }

                if (undeliverdresbillToLine.Type == "ManualAmount")
                {
                    var Amount = undeliverdresbillToLine.TariffAmount;

                    billToLineAmount += Amount;
                }

            } 

            return Math.Round(billToLineAmount);


        }



        public IActionResult UpdateBillTolline(BillToLine data)
        {
             

            try
            {      
                _billToLineRepository.Update(data);

                return new OkObjectResult(new { error = true, message = "Updated" });
                 
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = "please try Again" });

            }

            return new OkObjectResult(new { error = true, message = "please try again" });


        }


        public IActionResult AddInvoiceInquiryCFS(long? containerindxid, InvoiceInquiry data)
        {
            try
            {
                data.CYContainerId = null;

                 
                if (containerindxid != null)
                {
                    var res = new InvoiceInquiry
                    {
                        Ammount = data.Ammount,
                        CallerName = data.CallerName,
                        CBM = data.CBM,
                        ContainerIndexId = containerindxid,
                        CYContainerId = data.CYContainerId,
                        InquiryAbout = data.InquiryAbout,
                        InquiryDate = data.InquiryDate,
                        Remarks = data.Remarks
                    };

                    _invoiceInquiryRepo.Add(res);

                    return new OkObjectResult(new { error = false, message = "Save" });

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "please try again" });

                }
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }
        
        public IActionResult AddInvoiceInquiryCY(string igm , long cyindexno, InvoiceInquiry data)
        {
            try
            {
                data.ContainerIndexId = null;

                var cyContainer = _cyContainerRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == cyindexno);

                if (cyContainer != null)
                {
                    var res = new InvoiceInquiry
                    {
                        Ammount = data.Ammount,
                        CallerName = data.CallerName,
                        CBM = data.CBM,
                        ContainerIndexId = data.ContainerIndexId,
                        CYContainerId = cyContainer.CYContainerId,
                        InquiryAbout = data.InquiryAbout,
                        InquiryDate = data.InquiryDate,
                        Remarks = data.Remarks
                    };

                    _invoiceInquiryRepo.Add(res);

                    return new OkObjectResult(new { error = false, message = "Save" });

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "please try again" });

                }
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }


        public IActionResult UpdateDoValidity(long doNum)
        {
            var billdate = DateTime.Now;

            var storageamount = 0.0;

            var res = _invoiceRepo.GetDeliveryOrder(doNum);
            if(res != null)
            {
                if (Convert.ToDateTime(res.ValidTo.Value.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(billdate.Date.ToString("MM/dd/yyyy"))) {
                    return new OkObjectResult(new { error = false, message = "OK" , DeliveryOrderNumber = doNum });
                }

                if (res.ContainerIndexId != null)
                {
                    var invoice = _invoiceRepo.GetCFSFirstInvoice(res.ContainerIndexId ?? 0);

                    if (invoice != null)
                    {

                        var storageres = _invoiceRepo.GetStorageTotal(invoice.ContainerIndexId ?? 0, invoice.ClearingAgentId ?? 0 , billdate, invoice.GateInDate ?? DateTime.Now, invoice.DestuffDate ?? DateTime.Now , invoice.Weight, invoice.CBM, invoice.CargoType );
                        
                        var invoicestorage = _invoiceRepo.GetInvoiceItemBycontainerIndexId(res.ContainerIndexId ?? 0).Where(x=> x.Category == "Storage").Sum(s=> s.Amount);

                        var usedwaive = _invoiceRepo.GetPreviousWaiverCFS(res.ContainerIndexId ?? 0).Where(x => x.Category == "Storage").Sum(s => s.Discount);

                        storageamount += invoicestorage;
                        storageamount += usedwaive;

                        if(storageres.StorageTotal > 0)
                        {
                            storageres.StorageTotal -= storageamount;
                        } 

                        if (storageres.StorageDays > 0 && storageres.StorageTotal > 0)
                        {
                            return new OkObjectResult(new { error = true, message = "Please Create Storage Invoice" });
                        }
                        else
                        {

                            res.ValidTo = DateTime.Now;

                            _deliveryOrderRepository.Update(res);
                            return new OkObjectResult(new { error = false, message = "updated", DeliveryOrderNumber = doNum });
                        }
                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "invoce not found" });
                    }

                }
                if (res.CYContainerId != null)
                {
                    var invoice = _invoiceRepo.GetCYFirstInvoice(res.CYContainerId ?? 0 );


                    if (invoice != null)
                    {

                        var cycontainer = _invoiceRepo.GetFirstCyContainer(invoice.CYContainerId ?? 0);

                        var storageres = _invoiceRepo.GetStorageTotalCY(cycontainer.VIRNo, cycontainer.IndexNo ?? 0, billdate, invoice.GateInDate ?? DateTime.Now, invoice.ClearingAgentId ?? 0,  "CY");


                        var invoicestorage = _invoiceRepo.GetInvoiceItemBycycontainerId(res.CYContainerId ?? 0).Where(x => x.Category == "Storage").Sum(s => s.Amount);

                        var usedwaive = _invoiceRepo.GetPreviousWaiverCY(res.CYContainerId ?? 0).Where(x => x.Category == "Storage").Sum(s => s.Discount);

                        storageamount += invoicestorage;

                        storageamount += usedwaive;

                        if (storageres.StorageTotal > 0)
                        {
                            storageres.StorageTotal -= storageamount;
                        }

                        if (storageres.StorageDays > 0 && storageres.StorageTotal > 0)
                        {
                            return new OkObjectResult(new { error = true, message = "Please Create Storage Invoice" });
                        }
                        else
                        {

                            res.ValidTo = DateTime.Now;
                            _deliveryOrderRepository.Update(res);
                            return new OkObjectResult(new { error = false, message = "updated", DeliveryOrderNumber = doNum });
                        }
                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "invoce not found" });
                    }

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "Index not found" });
                }

            }
            else
            {
                return new OkObjectResult(new { error = true, message = "do not found"  });
            }
        }



        public IActionResult SaveTariffCondition(ExaminationTariff model, ExaminationTariffInformation tariff)
        {
            try
            {
                try
                {
                    if (model.ExaminationChargeId != null)
                    {
                          
                            _examinationTariffRepository.Add(model);
                          
                    }
                    else
                    {
                        return new JsonResult(new { error = true, message = "Please Select Tariff Head" });

                    }


                }
                catch (Exception e)
                {

                    return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });

                }

                 
                var res = _examinationTariffInformationRepository.GetAll().Where(x => x.ShippingAgentId == tariff.ShippingAgentId  && x.GoodsHeadId == tariff.GoodsHeadId &&   x.IndexCargoType == tariff.IndexCargoType
                                                                                && x.ExaminationType == tariff.ExaminationType).FirstOrDefault();
                if (res != null)
                {
                    var examinationTariffInformationTariff = new ExaminationTariffInformationTariff
                    {
                        ExaminationTariffInformationId = res.ExaminationTariffInformationId,
                        ExaminationTariffId = model.ExaminationTariffId

                    };
                      
                    _examinationTariffInformationTariffRepository.Add(examinationTariffInformationTariff);
                }
                else
                {
                    _examinationTariffInformationRepository.Add(tariff);

                    var examinationTariffInformationTariff = new ExaminationTariffInformationTariff
                    {
                        ExaminationTariffInformationId = tariff.ExaminationTariffInformationId,
                        ExaminationTariffId = model.ExaminationTariffId

                    };
                    _examinationTariffInformationTariffRepository.Add(examinationTariffInformationTariff);
                }





                return new JsonResult(new { error = false, message = "Save " });

            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = "Please Create Again" });

                throw;
            }

        }


        public IActionResult updateExaminationTariff(ExaminationTariff tariff)
        {

            try
            {
                 
                _examinationTariffRepository.Update(tariff);
                return new JsonResult(new { error = false, message = "save" });

            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }


        }

        public IActionResult DeleteSaveTariffCondition(long TariffId)
        {

            var tariff = _examinationTariffInformationTariffRepository.GetAll().Where(x => x.ExaminationTariffInformationTariffId == TariffId).LastOrDefault();

            try
            {
                if (tariff != null)
                {
                    _examinationTariffInformationTariffRepository.Delete(tariff);

                    return new JsonResult(new { error = false, message = "delete" });

                }
                else
                {
                    return new JsonResult(new { error = true, message = "Record Not Found" });

                }
            }
            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });


            } 

        }


        public IActionResult EmptyContainerInvoiceView()
        {
             
            ViewData["ContainerNumber"] = _emptyContainerReceivedRepository.GetAll().Where(x => x.IsEmptyGateOut == true && x.IsEmptyReceived == true && x.IsEmptyOut == false)
           .Select(s => new SelectListItem { Text = s.ContainerNo.ToString().Trim().ToUpper(), Value = s.EmptyContainerReceivedId.ToString() });

            return View();

        }

        public IActionResult SaveBillEmptyContainer(EmptyContainerInvoice invoice , List<EmptyContainerInvoiceItem> invoiceItems)
        {
              
            var res = _emptyContainerInvoiceRepository.GetAll().Where(x => x.EmptyContainerReceivedId == invoice.EmptyContainerReceivedId).FirstOrDefault();
            if (res != null)
            {
                return new OkObjectResult(new { error = true, message = "already  created" });
            }

            long donumber = 0;

            string NormalNumber = ""; 

            invoice.InvoiceDate = DateTime.Now;
            
                var invoi = new EmptyContainerInvoice
                {
                    
                    EmptyContainerReceivedId = invoice.EmptyContainerReceivedId,
                    InvoiceDate = invoice.InvoiceDate,                     
                    Year = DateTime.Now.ToString("yyyy"),                   
                    InvoiceNo = GetEmptyContainerLastInvoiceNo(),                 
                    EmptyContainerInvoiceItems = invoiceItems,
                    Size = invoice.Size,
                    StorageDays = invoice.StorageDays,                    
                    TotalCharges =  invoice.TotalCharges,
                    SalesTax = invoice.SalesTax,
                    AfterSalesTax = invoice.AfterSalesTax,
                    GrandTotal = invoice.GrandTotal,
                    ArrivalDate = invoice.ArrivalDate,
                    

                };

                invoi.GrandTotal = Math.Round(invoi.GrandTotal);

                _emptyContainerInvoiceRepository.Add(invoi);
                NormalNumber = invoi.InvoiceNo;
                 

                var deliveryorder = _emptyContainerDeliveryOrderRepository.GetAll().Where(x => x.EmptyContainerReceivedId == invoice.EmptyContainerReceivedId).FirstOrDefault();
                if (deliveryorder == null)
                {
                    var dod = new EmptyContainerDeliveryOrder
                    {
                        EmptyContainerReceivedId = invoice.EmptyContainerReceivedId,
                        Date = DateTime.Now,
                        InvoiceNo = invoi.InvoiceNo,
                        DONumber = GetDeliveryOrderNoEmptycontainer()
                    };

                    _emptyContainerDeliveryOrderRepository.Add(dod);
                    donumber = dod.DONumber;
                }
                else
                {

                    deliveryorder.EmptyContainerReceivedId = invoice.EmptyContainerReceivedId;
                    deliveryorder.ValidTo = DateTime.Now;
                    deliveryorder.InvoiceNo = invoi.InvoiceNo;

                    _emptyContainerDeliveryOrderRepository.Update(deliveryorder);
                    donumber = deliveryorder.DONumber;

                }


            var result = _emptyContainerReceivedRepository.GetAll().Where(x => x.EmptyContainerReceivedId == invoice.EmptyContainerReceivedId).FirstOrDefault();
            if (result != null)
            {
                result.IsEmptyOut = true;
                _emptyContainerReceivedRepository.Update(result);

            }


            return new OkObjectResult(new { error = false, message = "created", InvoiceNo = NormalNumber, DeliveryOrderNumber = donumber  });

        }




        public string GetEmptyContainerLastInvoiceNo()
        {

            var year = DateTime.Now.ToString("yyyy");

            var invo = _emptyContainerInvoiceRepository.GetLast();

            if (invo != null)
            {
                if (invo.Year == year)
                {
                    var no = invo.InvoiceNo.Substring(0, invo.InvoiceNo.IndexOf("/"));

                    var number = Convert.ToInt16(no) + 1;
                    var InvoiceNo = number + "/" + year;

                    return InvoiceNo;
                }
                else
                {
                    var no = 1;

                    var InvoiceNo = no + "/" + year;

                    return InvoiceNo;
                }
            }
            else
            {
                var no = 1;

                var InvoiceNo = no + "/" + year;

                return InvoiceNo;

            }


        }

        public long GetDeliveryOrderNoEmptycontainer()
        {
            var deli = _emptyContainerDeliveryOrderRepository.GetAll();
            var del = deli.LastOrDefault();
            var DonumberNo = del != null ? del.DONumber + 1 : 1;

            return DonumberNo;


        }


        public IActionResult CollectionBreakUpView()
        { 
            return View();             
        }


        public bool CheckDeliverystatus(long containerid ,   string containertype)
        {
             
            if(containertype == "CFS" )
            {
                var container = _containerIndexRepo.GetLastContainerIndexById(containerid);
                if(container != null)
                {
                    var containerlist = _containerIndexRepo.GetIndexInfoUndelivered(container.VirNo, container.IndexNo ?? 0).ToList();

                    if(containerlist.Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
             
            if (containertype == "CY" )
            {
                var container = _cyContainerRepo.GetContainerById(containerid);
                if (container != null)
                {
                    var containerlist = _cyContainerRepo.GetIndexInfoUndelivered(container.VIRNo, container.IndexNo ?? 0).ToList();

                    if (containerlist.Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return true;
        }

        public IActionResult UpdateConsigneeInfo(long consigneId , string consigneNTN , string stRegistrationNo)
        {
            try
            {
                var consigneres = _consigneRepository.GetConsigneById(consigneId);
                if(consigneres!= null)
                {
                    consigneres.ConsigneNTN = consigneNTN;
                    consigneres.STRegistrationNo = stRegistrationNo;

                    _consigneRepository.Update(consigneres);

                    return new JsonResult(new { error = false , message = "Updated" });

                }
                else
                {
                    return new JsonResult(new { error = true, message = "no data found" });

                }
            }
            catch (Exception e)
            {

             return new JsonResult(new { error = true, message = "please try again" });

            }
        }

        public IActionResult GenerateInvoice(Invoice invoice, string igm, long indexNo, bool islastPass, long? partyId, List<PaymentReceived> paymentReceivedList)
        {

          

            var userslist = _invoiceRepo.GetLineUsers().ToList();

            long creditallowsamt = 0;
            long CFSBTLInvoiceID = 0;
            long CYBTLInvoiceID = 0;
           
            #region MyRegion1

            var dovalidate = DateTime.Now;
           
            if (invoice.AdvanceDate != null)
            { 
                 invoice.AdvanceDate = invoice.AdvanceDate.Value.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second).AddMilliseconds(DateTime.Now.Millisecond);
               
                 
            }

            if (invoice.AdvanceDate == null)
            {
                invoice.AdvanceDate = DateTime.Now;
            }

            if (Convert.ToDateTime(invoice.AdvanceDate.Value.ToString("MM/dd/yyyy")) < Convert.ToDateTime(dovalidate.Date.ToString("MM/dd/yyyy")))
            {
                dovalidate = DateTime.Now;
            }
            else
            {
                dovalidate = invoice.AdvanceDate ?? DateTime.Now;
            }

           

            List<InvoiceItem> invoiceitemList = new List<InvoiceItem>();
            List<InvoiceItem> normalinvoiceitemList = new List<InvoiceItem>();
            List<InvoiceItem> billtolineinvoiceitemList = new List<InvoiceItem>();

            List<InvoiceItem> oldinvoiceitem = new List<InvoiceItem>();


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
               else if (item.Description == "AuctionHandingCharges")
                {

                    var invoiceItem = new InvoiceItem
                    {
                        Description = item.Description,
                        Amount = item.Amount,
                        Type = "Auction",
                        Category = item.Category

                    };
                    invoiceitemList.Add(invoiceItem);
                }
                else if (item.Description == "AuctionWeightmentCharges")
                {

                    var invoiceItem = new InvoiceItem
                    {
                        Description = item.Description,
                        Amount = item.Amount,
                        Type = "Auction",
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
              
            #endregion
            #region create list of items
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
                    Category = "Storage",
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
                        Category = "Storage",
                    };

                    invoiceitemList.Add(res);
                }
            }
            #endregion


            long donumber = 0;

            string NormalNumber = "";
            string BillToLineNumber = "";
            string AuctionInvoiveNumber = "";

            invoice.InvoiceDate = DateTime.Now;
            invoice.IsCancelled = false;

            bool IsForLine = false;

            bool PerviousInvoicesCount = true;


            if (invoice.Type == "CFS")
            {


                #region MyRegion2
                
                if (invoice.AdvanceDate == null)
                {
                    invoice.AdvanceDate = DateTime.Now;
                }

                invoice.CYContainerId = null;

                //var containerindex = _containerIndexRepo.Find(invoice.ContainerIndexId);
                var containerindex = _containerIndexRepo.GetContainerIndexById(invoice.ContainerIndexId ?? 0);

                if(containerindex == null)
                {
                    return new OkObjectResult(new { error = true, message = "no index found" });
                }

                if (Convert.ToDateTime(invoice.AdvanceDate.Value.Date.ToString("MM/dd/yyyy")) < Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy")))
                {
                    return new OkObjectResult(new { error = true, message = "advance date is < current date" });
                }



                var isauction = _invoiceRepo.GetAuctionInfo("CFS", containerindex.VirNo, containerindex.IndexNo ?? 0);


                if (containerindex != null)
                {
                    var soc = _scheduleOfChargeRepository.GetSOCBYIGMIndex(containerindex.VirNo, containerindex.IndexNo ?? 0);

                    if (soc.Count() == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "please generate soc " });
                    }

                } 

                if(userslist.Where(x=> x.ShippingAgentId ==  containerindex.ShippingAgentId).Count() > 0  )
                {
                    IsForLine = true;
                }
 

                var invoiceres = _invoiceRepo.GetCFSFirstInvoice(invoice.ContainerIndexId ?? 0);

                if (invoiceres != null)
                {
                    PerviousInvoicesCount = false;
                   var paidinvoiceitems = _invoiceRepo.GetInvoiceItemBycontainerIndexId(invoice.ContainerIndexId ?? 0).ToList();

                    var usedwaive = _invoiceRepo.GetPreviousWaiverCFS(invoice.ContainerIndexId ?? 0).Where(x => x.Category != "Tariff").ToList();

                    if (paidinvoiceitems.Count() > 0)
                    {

                        var newinvoiceitemList = invoiceitemList.Where(x => x.Category != "Tariff").ToList();

                        var afterinvoices = paidinvoiceitems.Where(x => x.Category != "Tariff");

                        if (newinvoiceitemList.Count() > 0) // current invoice form  list with out tariff
                        {
                            foreach (var item in newinvoiceitemList) // this section is  substractin prevouis amount from current amount
                            {
                                var result = afterinvoices.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Amount);

                                if (result > 0)
                                {
                                    item.Amount -= result;
                                }
                            }

                            newinvoiceitemList.RemoveAll(c => c.Amount <= 0);

                            foreach (var item in newinvoiceitemList)  // this section is  substractin prevouis waiver amount from current amount
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
                 
                if(isauction == true)
                {
                    var waiver = _containerIndexRepo.GetWaiverInfo(invoice.ContainerIndexId ?? 0);
                    normalinvoiceitemList = invoiceitemList;

                    if (waiver.Count() > 0)
                    {
                        foreach (var item in waiver)
                        {
                            var result = normalinvoiceitemList.Find(t => t.Description == item.Description && t.Category == item.Category);

                            if (result != null)
                            {
                                result.Amount -= item.Discount;
                            }
                        }
                    }

                    normalinvoiceitemList.RemoveAll(c => c.Amount <= 0);
                    #region normarl invoice
                    var invoi = new Invoice
                    {
                        TariffInformationId = invoice.TariffInformationId,
                        AdvanceDate = invoice.AdvanceDate,
                        BalanceAmount = invoice.BalanceAmount,
                        BillType = "Normal",
                        InvoiceCategory = IsForLine == true ? "FF" : "AICT",
                        InvoiceType = IsForLine == true ? "DMC" : "SSTI",
                        CBM = invoice.CBM,
                        ClearingAgentNTN = invoice.ClearingAgentNTN,
                        CYContainerId = null,
                        ContainerIndexId = invoice.ContainerIndexId,
                        DestuffDate = invoice.DestuffDate,
                        InvoiceNatureType = islastPass,
                        StorageApplicableon = invoice.StorageApplicableon,
                        GateInDate = invoice.GateInDate,
                        InvoiceDate = invoice.InvoiceDate,
                        BillToLineAmount = invoice.BillToLineAmount,
                        Year = DateTime.Now.ToString("yyyy"),
                        WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                        TariffAmountTotal = invoice.TariffAmountTotal,
                        AdditionalFreeDays = invoice.AdditionalFreeDays,
                        InvoiceNo = IsForLine == true ? GetLastInvoiceNoForFF() :  GetLastInvoiceNo(),
                        IsAdvanceBill = invoice.IsAdvanceBill,
                        IsCancelled = false,
                        ClearingAgentId = invoice.ClearingAgentId,
                        CNIC = invoice.CNIC,
                        ClearingAgentRep = invoice.ClearingAgentRep,
                        BalanceCargo = invoice.BalanceCargo,
                        PhoneNumber = invoice.PhoneNumber,
                        Size = invoice.Size,
                        StorageDays = invoice.StorageDays,
                        TotalFreeDays = invoice.TotalFreeDays,
                        CargoType = invoice.CargoType,
                        Weight = invoice.Weight,
                        ExcessAmount = 0,
                        Type = invoice.Type,
                        PartContainer = invoice.PartContainer,
                        OtherChargeAmount = 0,
                        CYStorageSizeAmount = 0,
                        StorageTotal = 0,
                        PortChargeAmount = 0,
                        //update exchange rate in all invoice without perfoma invoices
                        ExchangeRateAmount = invoice.ExchangeRateAmount,
                        SealCharger = 0,
                        VehicleCharges = 0,
                        TotalCharges = 0,
                        PreviousBillAmount = 0,
                        BalanceAmountTotal = 0,
                        SalesTax = 0,
                        AfterSalesTax = 0,
                        GrandTotal = 0,
                        IsAdjust = false,
                        IsLineIndexRate = PerviousInvoicesCount  ,

                        IGMCBM = invoice.IGMCBM,
                        DestuffCBM = invoice.DestuffCBM,
                        ExaminationCBM = invoice.ExaminationCBM,
                        FFCBM = invoice.FFCBM,
                        ActualWT = invoice.ActualWT,
                        MFTWT = invoice.MFTWT,
                        FFStorageShare = invoice.FFStorageShare,
                        AictPerBoxRate = invoice.AictPerBoxRate,
                        TotalArriveIndexes = invoice.TotalArriveIndexes,
                         
                    }; 
                    _invoiceRepo.Add(invoi);

                    NormalNumber = invoi.InvoiceNo;


                    #endregion

                    #region Adjustment invocie zero
                    if(IsForLine == true)
                    {
                        var Adjustmentinvociezeroinvoi = new Invoice
                        {
                            TariffInformationId = invoice.TariffInformationId,
                            AdvanceDate = invoice.AdvanceDate,
                            BalanceAmount = invoice.BalanceAmount,
                            BillType = "Normal",
                            InvoiceCategory =  "AICT" ,
                            InvoiceType =  "SSTI"  ,
                            CBM = 0,
                            ClearingAgentNTN = invoice.ClearingAgentNTN,
                            CYContainerId = null,
                            ContainerIndexId = invoice.ContainerIndexId,
                            DestuffDate = invoice.DestuffDate,
                            InvoiceNatureType = islastPass,
                            StorageApplicableon = invoice.StorageApplicableon,
                            GateInDate = invoice.GateInDate,
                            InvoiceDate = invoice.InvoiceDate,
                            BillToLineAmount = 0,
                            Year = DateTime.Now.ToString("yyyy"),
                            WaiverDiscountAmount = 0,
                            TariffAmountTotal = 0,
                            AdditionalFreeDays = 0,
                            InvoiceNo = GetLastInvoiceNo(),
                            IsAdvanceBill = invoice.IsAdvanceBill,
                            IsCancelled = false,
                            ClearingAgentId = invoice.ClearingAgentId,
                            CNIC = invoice.CNIC,
                            ClearingAgentRep = invoice.ClearingAgentRep,
                            BalanceCargo = 0,
                            PhoneNumber = invoice.PhoneNumber,
                            Size = invoice.Size,
                            StorageDays = 0,
                            TotalFreeDays = 0,
                            CargoType = invoice.CargoType,
                            Weight = 0,
                            ExcessAmount = 0,
                            Type = invoice.Type,                            
                            PartContainer = invoice.PartContainer,
                            OtherChargeAmount = 0,
                            CYStorageSizeAmount = 0,
                            StorageTotal = 0,
                            PortChargeAmount = 0,
                            ExchangeRateAmount = 0,
                            SealCharger = 0,
                            VehicleCharges = 0,
                            TotalCharges = 0,
                            PreviousBillAmount = 0,
                            BalanceAmountTotal = 0,
                            SalesTax = 0,
                            AfterSalesTax = 0,
                            GrandTotal = 0,
                            IsAdjust = false,
                            PerfomaReceiptNumber = NormalNumber,
                            IsLineIndexRate = false,

                            IGMCBM = invoice.IGMCBM,
                            DestuffCBM = invoice.DestuffCBM,
                            ExaminationCBM = invoice.ExaminationCBM,
                            FFCBM = invoice.FFCBM,
                            ActualWT = invoice.ActualWT,
                            MFTWT = invoice.MFTWT,
                            FFStorageShare = invoice.FFStorageShare,
                            AictPerBoxRate = invoice.AictPerBoxRate,
                            TotalArriveIndexes = invoice.TotalArriveIndexes,

                        };
                        _invoiceRepo.Add(Adjustmentinvociezeroinvoi);
                    }
                    

                    #endregion

                    #region normarl auction invoice
                    var invoAuction = new Invoice
                    {
                        TariffInformationId = invoice.TariffInformationId,
                        AdvanceDate = invoice.AdvanceDate,
                        BalanceAmount = invoice.BalanceAmount,
                        BillType = "Auction",
                        InvoiceCategory = IsForLine == true ? "FF" : "AICT",
                        InvoiceType = IsForLine == true ? "DMC" : "SSTI",
                        CBM = invoice.CBM,
                        ClearingAgentNTN = invoice.ClearingAgentNTN,
                        CYContainerId = null,
                        ContainerIndexId = invoice.ContainerIndexId,
                        DestuffDate = invoice.DestuffDate,
                        InvoiceNatureType = islastPass,
                        StorageApplicableon = invoice.StorageApplicableon,
                        GateInDate = invoice.GateInDate,
                        InvoiceDate = invoice.InvoiceDate,
                        BillToLineAmount = invoice.BillToLineAmount,
                        Year = DateTime.Now.ToString("yyyy"),
                        WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                        TariffAmountTotal = invoice.TariffAmountTotal,
                        AdditionalFreeDays = invoice.AdditionalFreeDays,
                        InvoiceNo = IsForLine == true ? GetLastInvoiceNoForFF() : GetLastInvoiceNo(),
                        IsAdvanceBill = invoice.IsAdvanceBill,
                        IsCancelled = false,
                        ClearingAgentId = invoice.ClearingAgentId,
                        CNIC = invoice.CNIC,
                        ClearingAgentRep = invoice.ClearingAgentRep,
                        BalanceCargo = invoice.BalanceCargo,
                        PhoneNumber = invoice.PhoneNumber,
                        InvoiceItems = normalinvoiceitemList,
                        Size = invoice.Size,
                        StorageDays = invoice.StorageDays,
                        TotalFreeDays = invoice.TotalFreeDays,
                        CargoType = invoice.CargoType,
                        Weight = invoice.Weight,
                        ExcessAmount = invoice.ExcessAmount,
                        Type = invoice.Type,
                        PartContainer = invoice.PartContainer,
                        OtherChargeAmount = invoice.OtherChargeAmount,
                        CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                        StorageTotal = invoice.StorageTotal,
                        PortChargeAmount = invoice.PortChargeAmount,
                        ExchangeRateAmount = invoice.ExchangeRateAmount,
                        SealCharger = invoice.SealCharger,
                        VehicleCharges = invoice.VehicleCharges,
                        TotalCharges = normalinvoiceitemList.Sum(x => x.Amount),
                        PreviousBillAmount = invoice.PreviousBillAmount,
                        BalanceAmountTotal = normalinvoiceitemList.Sum(x => x.Amount),
                        SalesTax = invoice.SalesTax,
                        AfterSalesTax = Math.Ceiling((normalinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                        GrandTotal = (normalinvoiceitemList.Sum(x => x.Amount)) + Math.Ceiling((normalinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                        IsAdjust = false,
                        IsLineIndexRate = false ,

                        IGMCBM = invoice.IGMCBM,
                        DestuffCBM = invoice.DestuffCBM,
                        ExaminationCBM = invoice.ExaminationCBM,
                        FFCBM = invoice.FFCBM,
                        ActualWT = invoice.ActualWT,
                        MFTWT = invoice.MFTWT,
                        FFStorageShare = invoice.FFStorageShare,
                        AictPerBoxRate = invoice.AictPerBoxRate,
                        TotalArriveIndexes = invoice.TotalArriveIndexes,

                    };

                    invoAuction.GrandTotal = Math.Round(invoAuction.GrandTotal, 2);

                    _invoiceRepo.Add(invoAuction);

                    AuctionInvoiveNumber = invoAuction.InvoiceNo;
                    #endregion

                    #region A1 Adjustment invocie zero
                    if (IsForLine == true)
                    {
                        var A1Adjustmentinvociezeroinvoi = new Invoice
                        {
                            TariffInformationId = invoice.TariffInformationId,
                            AdvanceDate = invoice.AdvanceDate,
                            BalanceAmount = invoice.BalanceAmount,
                            BillType = "Auction",
                            InvoiceCategory = "AICT",
                            InvoiceType = "SSTI",
                            CBM = 0,
                            ClearingAgentNTN = invoice.ClearingAgentNTN,
                            CYContainerId = null,
                            ContainerIndexId = invoice.ContainerIndexId,
                            DestuffDate = invoice.DestuffDate,
                            InvoiceNatureType = islastPass,
                            StorageApplicableon = invoice.StorageApplicableon,
                            GateInDate = invoice.GateInDate,
                            InvoiceDate = invoice.InvoiceDate,
                            BillToLineAmount = 0,
                            Year = DateTime.Now.ToString("yyyy"),
                            WaiverDiscountAmount = 0,
                            TariffAmountTotal = 0,
                            AdditionalFreeDays = 0,
                            InvoiceNo = GetLastInvoiceNo(),
                            IsAdvanceBill = invoice.IsAdvanceBill,
                            IsCancelled = false,
                            ClearingAgentId = invoice.ClearingAgentId,
                            CNIC = invoice.CNIC,
                            ClearingAgentRep = invoice.ClearingAgentRep,
                            BalanceCargo = 0,
                            PhoneNumber = invoice.PhoneNumber,
                            Size = invoice.Size,
                            StorageDays = 0,
                            TotalFreeDays = 0,
                            CargoType = invoice.CargoType,
                            Weight = 0,
                            ExcessAmount = 0,
                            Type = invoice.Type,
                            PartContainer = invoice.PartContainer,
                            OtherChargeAmount = 0,
                            CYStorageSizeAmount = 0,
                            StorageTotal = 0,
                            PortChargeAmount = 0,
                            ExchangeRateAmount = 0,
                            SealCharger = 0,
                            VehicleCharges = 0,
                            TotalCharges = 0,
                            PreviousBillAmount = 0,
                            BalanceAmountTotal = 0,
                            SalesTax = 0,
                            AfterSalesTax = 0,
                            GrandTotal = 0,
                            IsAdjust = false,
                            PerfomaReceiptNumber = AuctionInvoiveNumber,
                            IsLineIndexRate = false ,

                            IGMCBM = invoice.IGMCBM,
                            DestuffCBM = invoice.DestuffCBM,
                            ExaminationCBM = invoice.ExaminationCBM,
                            FFCBM = invoice.FFCBM,
                            ActualWT = invoice.ActualWT,
                            MFTWT = invoice.MFTWT,
                            FFStorageShare = invoice.FFStorageShare,
                            AictPerBoxRate = invoice.AictPerBoxRate,
                            TotalArriveIndexes = invoice.TotalArriveIndexes,
                        };
                        _invoiceRepo.Add(A1Adjustmentinvociezeroinvoi);
                    }


                    #endregion

                    var otherChargeAssign = _otherChargeAssigningRepo.GetUnPaidOtherChargeAssigning( "CFS" , invoice.ContainerIndexId ?? 0 ).ToList();
                    if (otherChargeAssign.Count() > 0)
                    {
                         //update invoiceno in specialhandling 
                         if (invoAuction.InvoiceItems.Count() >0)
                        {
                            if (invoAuction.InvoiceItems.Where(x=> x.Category == "SpecialHandlingCharges").Count() > 0)
                            {
                                otherChargeAssign.ForEach(x => x.InvoiceId = invoAuction.InvoiceId);

                            }
                        }
                        else
                        {
                            otherChargeAssign.ForEach(x => x.InvoiceId = invoi.InvoiceId);
                        }
                        otherChargeAssign.ForEach(x => x.InvoiceCreated = true);
                        _otherChargeAssigningRepo.UpdateRange(otherChargeAssign);
                    }

                    if (waiver.Count() > 0)
                    {
                        //update waiver againsts invoice
                        waiver.ForEach(x => x.InvoiceNumber = NormalNumber);
                        waiver.ForEach(x => x.InvoiceCreated = true);
                        _waiverRepo.UpdateRange(waiver);
                    }

                    var deliveryorder = _deliveryOrderRepository.GetDeliveryOrderByContainerIndexId(invoice.ContainerIndexId ?? 0);
                    if (deliveryorder == null)
                    {
                        var dod = new DeliveryOrder
                        {
                            ContainerIndexId = invoice.ContainerIndexId,
                            Date = DateTime.Now,
                            ValidTo = dovalidate,
                            InvoiceNo = invoi.InvoiceNo,
                            DONumber = GetDeliveryOrderNo()
                        };

                        _deliveryOrderRepository.Add(dod);
                        donumber = dod.DONumber;
                    }
                    else
                    {

                        deliveryorder.ContainerIndexId = invoice.ContainerIndexId;
                        deliveryorder.ValidTo = dovalidate;
                        deliveryorder.InvoiceNo = invoi.InvoiceNo;

                        _deliveryOrderRepository.Update(deliveryorder);
                        donumber = deliveryorder.DONumber;
                    }

                    if (islastPass == true)
                    {

                        var ledger = new PartyLedger
                        {
                            LedgerDate = DateTime.Now,
                            BillNo = AuctionInvoiveNumber,
                            Debit = invoAuction.GrandTotal,
                            PartyId = partyId,
                            IsSettled = true,
                            ShippingAgentId = IsForLine == true ? containerindex.ShippingAgentId : null,
                            Type = "LetPass"


                        };

                        _partyLedgerRepo.Add(ledger);

                    }

                    else if (paymentReceivedList.Count() > 0)
                    {

                        var rescreditalow = paymentReceivedList.Where(x => x.NatureOfAmount == "CreditAllowed").LastOrDefault();

                        var KnockOffinvoice = paymentReceivedList.Where(x => x.NatureOfAmount == "KnockOff").ToList();


                        if (rescreditalow != null)
                        {
                            var creditallowres = _partyLedgerRepo.CreditAllowedCFSBYid(invoice.ContainerIndexId ?? 0);

                            if (creditallowres != null)
                            {
                                creditallowres.IsSettled = true;
                                creditallowres.InvoiceNo = AuctionInvoiveNumber;
                                creditallowres.CreditAllowedExcessSettelment = creditallowsamt;
                                _creditAllowedRepository.Update(creditallowres);

                            }

                        }

                        if (KnockOffinvoice.Count() > 0)
                        {

                            foreach (var item in KnockOffinvoice)
                            {
                                var knockoffinvoiceres = _partyLedgerRepo.FindInvoiceAndupdate(item.InoviceNo, AuctionInvoiveNumber);
                            }
                        } 

                        paymentReceivedList.ForEach(x => x.InoviceNo = AuctionInvoiveNumber);

                        _paymentReceivedRepository.AddRange(paymentReceivedList);


                    }

                }
                else
                {
                  var undeliverdresbillToLine = _containerIndexRepo.GetBillToLineInfo(containerindex.VirNo, containerindex.IndexNo ?? 0, "CFS");
                 
                    var waiver = _containerIndexRepo.GetWaiverInfo(invoice.ContainerIndexId ?? 0);
                  
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
                                    Category = item.Category
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
                                    Category = item.Category

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


                if (waiver.Count() > 0)
                {
                    foreach (var item in waiver)
                    {
                        var result = normalinvoiceitemList.Find(t => t.Description == item.Description && t.Category == item.Category);

                        if (result != null)
                        {
                            result.Amount -= item.Discount;
                        }
                    }
                }

                normalinvoiceitemList.RemoveAll(c => c.Amount <= 0);


                var invoi = new Invoice
                {
                    TariffInformationId = invoice.TariffInformationId,
                    AdvanceDate = invoice.AdvanceDate,
                    BalanceAmount = invoice.BalanceAmount,
                    BillType = "Normal",
                    InvoiceCategory = IsForLine == true ? "FF" : "AICT",
                    InvoiceType = IsForLine == true ? "DMC" : "SSTI",
                    CBM = invoice.CBM,
                    ClearingAgentNTN = invoice.ClearingAgentNTN,
                    CYContainerId = null,
                    ContainerIndexId = invoice.ContainerIndexId,
                    DestuffDate = invoice.DestuffDate,
                    InvoiceNatureType = islastPass,
                    StorageApplicableon = invoice.StorageApplicableon,
                    GateInDate = invoice.GateInDate,
                    InvoiceDate = invoice.InvoiceDate,
                    BillToLineAmount = invoice.BillToLineAmount,
                    Year = DateTime.Now.ToString("yyyy"),
                    WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                    TariffAmountTotal = invoice.TariffAmountTotal,
                    AdditionalFreeDays = invoice.AdditionalFreeDays,
                    InvoiceNo = IsForLine == true ? GetLastInvoiceNoForFF() : GetLastInvoiceNo(),
                    IsAdvanceBill = invoice.IsAdvanceBill,
                    IsCancelled = false,
                    ClearingAgentId = invoice.ClearingAgentId,
                    CNIC = invoice.CNIC,
                    ClearingAgentRep = invoice.ClearingAgentRep,
                    BalanceCargo = invoice.BalanceCargo,
                    PhoneNumber = invoice.PhoneNumber,
                    InvoiceItems = normalinvoiceitemList,
                    Size = invoice.Size,
                    StorageDays = invoice.StorageDays,
                    TotalFreeDays = invoice.TotalFreeDays,
                    CargoType = invoice.CargoType,
                    Weight = invoice.Weight,
                    ExcessAmount = invoice.ExcessAmount,
                    Type = invoice.Type,
                    PartContainer = invoice.PartContainer,
                    OtherChargeAmount = invoice.OtherChargeAmount,
                    CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                    StorageTotal = invoice.StorageTotal,
                    PortChargeAmount = invoice.PortChargeAmount,
                    ExchangeRateAmount = invoice.ExchangeRateAmount,
                    SealCharger = invoice.SealCharger,
                    VehicleCharges = invoice.VehicleCharges,
                    TotalCharges = normalinvoiceitemList.Sum(x => x.Amount),
                    PreviousBillAmount = invoice.PreviousBillAmount,
                    BalanceAmountTotal = normalinvoiceitemList.Sum(x => x.Amount),
                    SalesTax = invoice.SalesTax,
                     AfterSalesTax = Math.Ceiling((normalinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                    GrandTotal = (normalinvoiceitemList.Sum(x => x.Amount)) + Math.Ceiling((normalinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                    IsAdjust = false,
                    IsLineIndexRate = undeliverdresbillToLine != null ? false : PerviousInvoicesCount ,

                    IGMCBM = invoice.IGMCBM,
                    DestuffCBM = invoice.DestuffCBM,
                    ExaminationCBM = invoice.ExaminationCBM,
                    FFCBM = invoice.FFCBM,
                    ActualWT = invoice.ActualWT,
                    MFTWT = invoice.MFTWT,
                    FFStorageShare = invoice.FFStorageShare,
                    AictPerBoxRate = invoice.AictPerBoxRate,
                    TotalArriveIndexes = invoice.TotalArriveIndexes,
                };

                invoi.GrandTotal = Math.Round(invoi.GrandTotal, 2);
 

                _invoiceRepo.Add(invoi);

                NormalNumber = invoi.InvoiceNo;



                    #region A2 Adjustment invocie zero
                    
                    if (IsForLine == true)
                    {
                        var A2Adjustmentinvociezeroinvoi = new Invoice
                        {
                            TariffInformationId = invoice.TariffInformationId,
                            AdvanceDate = invoice.AdvanceDate,
                            BalanceAmount = invoice.BalanceAmount,
                            BillType = "Normal",
                            InvoiceCategory = "AICT",
                            InvoiceType = "SSTI",
                            CBM = 0,
                            ClearingAgentNTN = invoice.ClearingAgentNTN,
                            CYContainerId = null,
                            ContainerIndexId = invoice.ContainerIndexId,
                            DestuffDate = invoice.DestuffDate,
                            InvoiceNatureType = islastPass,
                            StorageApplicableon = invoice.StorageApplicableon,
                            GateInDate = invoice.GateInDate,
                            InvoiceDate = invoice.InvoiceDate,
                            BillToLineAmount = 0,
                            Year = DateTime.Now.ToString("yyyy"),
                            WaiverDiscountAmount = 0,
                            TariffAmountTotal = 0,
                            AdditionalFreeDays = 0,
                            InvoiceNo = GetLastInvoiceNo(),
                            IsAdvanceBill = invoice.IsAdvanceBill,
                            IsCancelled = false,
                            ClearingAgentId = invoice.ClearingAgentId,
                            CNIC = invoice.CNIC,
                            ClearingAgentRep = invoice.ClearingAgentRep,
                            BalanceCargo = 0,
                            PhoneNumber = invoice.PhoneNumber,
                            Size = invoice.Size,
                            StorageDays = 0,
                            TotalFreeDays = 0,
                            CargoType = invoice.CargoType,
                            Weight = 0,
                            ExcessAmount = 0,
                            Type = invoice.Type,
                            PartContainer = invoice.PartContainer,
                            OtherChargeAmount = 0,
                            CYStorageSizeAmount = 0,
                            StorageTotal = 0,
                            PortChargeAmount = 0,
                            ExchangeRateAmount = 0,
                            SealCharger = 0,
                            VehicleCharges = 0,
                            TotalCharges = 0,
                            PreviousBillAmount = 0,
                            BalanceAmountTotal = 0,
                            SalesTax = 0,
                            AfterSalesTax = 0,
                            GrandTotal = 0,
                            IsAdjust = false,
                            PerfomaReceiptNumber = NormalNumber,
                            IsLineIndexRate = false ,

                            IGMCBM = invoice.IGMCBM,
                            DestuffCBM = invoice.DestuffCBM,
                            ExaminationCBM = invoice.ExaminationCBM,
                            FFCBM = invoice.FFCBM,
                            ActualWT = invoice.ActualWT,
                            MFTWT = invoice.MFTWT,
                            FFStorageShare = invoice.FFStorageShare,
                            AictPerBoxRate = invoice.AictPerBoxRate,
                            TotalArriveIndexes = invoice.TotalArriveIndexes,

                        };
                        _invoiceRepo.Add(A2Adjustmentinvociezeroinvoi);
                    }


                    #endregion
 

                if (waiver.Count() > 0)
                {
                    //update waiver againsts invoice
                    waiver.ForEach(x => x.InvoiceCreated = true);
                    waiver.ForEach(x => x.InvoiceNumber = NormalNumber);
                    _waiverRepo.UpdateRange(waiver);
                }

                if (undeliverdresbillToLine != null)
                {
                    if (undeliverdresbillToLine.Type == "HundredPercent")
                    {
                        billtolineinvoiceitemList = invoiceitemList;
                    }

                    if (undeliverdresbillToLine.Type == "ExStorage")
                    {
                        billtolineinvoiceitemList = invoiceitemList.Where(x => x.Category != "Storage").ToList();
                    }
                    if (undeliverdresbillToLine.Type == "OnlyTariff")
                    {
                        billtolineinvoiceitemList = invoiceitemList.Where(x => x.Category == "Tariff").ToList();
                    }

                    if (undeliverdresbillToLine.Type == "ManualAmount")
                    {
                        var manamt = undeliverdresbillToLine.TariffAmount;
                        foreach (var item in invoiceitemList)
                        {


                            if (manamt > 0)
                            {

                                manamt = manamt - item.Amount;

                                if (manamt < 0)
                                {
                                    var req = new InvoiceItem
                                    {
                                        Amount = manamt + item.Amount,
                                        Description = item.Description,
                                        Type = item.Type,
                                        Category = item.Category
                                    };

                                    billtolineinvoiceitemList.Add(req);

                                    break;
                                }

                                if (manamt > 0)
                                {
                                    billtolineinvoiceitemList.Add(item);

                                }

                                if (manamt == 0)
                                {
                                    billtolineinvoiceitemList.Add(item);
                                    break;
                                }

                            }

                        }
                    }

                    billtolineinvoiceitemList.RemoveAll(c => c.Amount <= 0);


                    var invo = new Invoice
                    {
                        TariffInformationId = invoice.TariffInformationId,
                        AdvanceDate = invoice.AdvanceDate,
                        BalanceAmount = invoice.BalanceAmount,
                        BillType = "Bill To Line",
                        InvoiceCategory = IsForLine == true ? "FF" : "AICT",
                        InvoiceType = IsForLine == true ? "DMC" : "SSTI",
                        CBM = invoice.CBM,
                        ClearingAgentNTN = invoice.ClearingAgentNTN,
                        InvoiceNatureType = islastPass,
                        CYContainerId = null,
                        ContainerIndexId = invoice.ContainerIndexId,
                        DestuffDate = invoice.DestuffDate,
                        Year = DateTime.Now.ToString("yyyy"),
                        StorageApplicableon = invoice.StorageApplicableon,
                        GateInDate = invoice.GateInDate,
                        AdditionalFreeDays = invoice.AdditionalFreeDays,
                        PhoneNumber = invoice.PhoneNumber,
                        ClearingAgentRep = invoice.ClearingAgentRep,
                        CNIC = invoice.CNIC,
                        ClearingAgentId = invoice.ClearingAgentId,
                        InvoiceDate = invoice.InvoiceDate,
                        BillToLineAmount = invoice.BillToLineAmount,
                        WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                        ExchangeRateAmount = invoice.ExchangeRateAmount,
                        CargoType = invoice.CargoType,
                        TariffAmountTotal = invoice.TariffAmountTotal,
                        BalanceCargo = invoice.BalanceCargo,
                        InvoiceNo = IsForLine == true ? GetLastInvoiceNoForFF() : GetLastInvoiceNo(),
                        IsAdvanceBill = invoice.IsAdvanceBill,
                        InvoiceItems = billtolineinvoiceitemList,
                        IsCancelled = false,
                        Size = invoice.Size,
                        StorageDays = invoice.StorageDays,
                        TotalFreeDays = invoice.TotalFreeDays,
                        PartContainer = invoice.PartContainer,
                        Weight = invoice.Weight,
                        Type = invoice.Type,
                        ActualTariffCharges = invoice.TotalCharges,
                        OtherChargeAmount = invoice.OtherChargeAmount,
                        CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                        StorageTotal = 0,
                        TotalCharges = billtolineinvoiceitemList.Sum(x => x.Amount),
                        PreviousBillAmount = invoice.PreviousBillAmount,
                        //BalanceAmountTotal = invoice.BillToLineAmount,
                        BalanceAmountTotal = billtolineinvoiceitemList.Sum(x => x.Amount),
                        SalesTax = invoice.SalesTax,
                        AfterSalesTax = Math.Ceiling((billtolineinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                        GrandTotal = billtolineinvoiceitemList.Sum(x => x.Amount) + Math.Ceiling((billtolineinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                        IsAdjust = false,
                        IsLineIndexRate = PerviousInvoicesCount ,

                        IGMCBM = invoice.IGMCBM,
                        DestuffCBM = invoice.DestuffCBM,
                        ExaminationCBM = invoice.ExaminationCBM,
                        FFCBM = invoice.FFCBM,
                        ActualWT = invoice.ActualWT,
                        MFTWT = invoice.MFTWT,
                        FFStorageShare = invoice.FFStorageShare,
                        AictPerBoxRate = invoice.AictPerBoxRate,
                        TotalArriveIndexes = invoice.TotalArriveIndexes,

                    };

                    invo.GrandTotal = Math.Round(invo.GrandTotal, 2);
                    _invoiceRepo.Add(invo);

                    BillToLineNumber = invo.InvoiceNo;
                    CFSBTLInvoiceID = invo.InvoiceId;

                    var billtoline = _billToLineRepository.GetAll().Where(x => x.VirNo == containerindex.VirNo && x.IndexNo == containerindex.IndexNo && x.IndexType == "CFS" && x.InoviceCreated == false).LastOrDefault();
                    if (billtoline != null)
                    {

                        billtoline.InoviceCreated = true;
                        billtoline.InvoiceNumber = invo.InvoiceNo;
                        billtoline.InvoiceAmount = invo.TotalCharges;
                        billtoline.TariffAmount = invo.TotalCharges;


                        _billToLineRepository.Update(billtoline);
                    }

                        #region A3 Adjustment invocie zero

                        if (IsForLine == true)
                        {
                            var A3Adjustmentinvociezeroinvoi = new Invoice
                            {
                                TariffInformationId = invoice.TariffInformationId,
                                AdvanceDate = invoice.AdvanceDate,
                                BalanceAmount = invoice.BalanceAmount,
                                BillType = "Bill To Line",
                                InvoiceCategory = "AICT",
                                InvoiceType = "SSTI",
                                CBM = 0,
                                ClearingAgentNTN = invoice.ClearingAgentNTN,
                                CYContainerId = null,
                                ContainerIndexId = invoice.ContainerIndexId,
                                DestuffDate = invoice.DestuffDate,
                                InvoiceNatureType = islastPass,
                                StorageApplicableon = invoice.StorageApplicableon,
                                GateInDate = invoice.GateInDate,
                                InvoiceDate = invoice.InvoiceDate,
                                BillToLineAmount = 0,
                                Year = DateTime.Now.ToString("yyyy"),
                                WaiverDiscountAmount = 0,
                                TariffAmountTotal = 0,
                                AdditionalFreeDays = 0,
                                InvoiceNo = GetLastInvoiceNo(),
                                IsAdvanceBill = invoice.IsAdvanceBill,
                                IsCancelled = false,
                                ClearingAgentId = invoice.ClearingAgentId,
                                CNIC = invoice.CNIC,
                                ClearingAgentRep = invoice.ClearingAgentRep,
                                BalanceCargo = 0,
                                PhoneNumber = invoice.PhoneNumber,
                                Size = invoice.Size,
                                StorageDays = 0,
                                TotalFreeDays = 0,
                                CargoType = invoice.CargoType,
                                Weight = 0,
                                ExcessAmount = 0,
                                Type = invoice.Type,
                                PartContainer = invoice.PartContainer,
                                OtherChargeAmount = 0,
                                CYStorageSizeAmount = 0,
                                StorageTotal = 0,
                                PortChargeAmount = 0,
                                ExchangeRateAmount = 0,
                                SealCharger = 0,
                                VehicleCharges = 0,
                                TotalCharges = 0,
                                PreviousBillAmount = 0,
                                BalanceAmountTotal = 0,
                                SalesTax = 0,
                                AfterSalesTax = 0,
                                GrandTotal = 0,
                                IsAdjust = false,
                                PerfomaReceiptNumber = BillToLineNumber,
                                IsLineIndexRate = false ,

                                IGMCBM = invoice.IGMCBM,
                                DestuffCBM = invoice.DestuffCBM,
                                ExaminationCBM = invoice.ExaminationCBM,
                                FFCBM = invoice.FFCBM,
                                ActualWT = invoice.ActualWT,
                                MFTWT = invoice.MFTWT,
                                FFStorageShare = invoice.FFStorageShare,
                                AictPerBoxRate = invoice.AictPerBoxRate,
                                TotalArriveIndexes = invoice.TotalArriveIndexes,
                            };
                            _invoiceRepo.Add(A3Adjustmentinvociezeroinvoi);
                        }


                        #endregion


                    }


                    var otherChargeAssign = _otherChargeAssigningRepo.GetUnPaidOtherChargeAssigning(  "CFS", invoice.ContainerIndexId ?? 0 ).ToList();

                    if (otherChargeAssign.Count() > 0)
                    {
                        //update invoiceno in specialhandling 
                        if (billtolineinvoiceitemList.Count() > 0)
                        {
                            if (billtolineinvoiceitemList.Where(x => x.Category == "SpecialHandlingCharges").Count() > 0)
                            {
                                otherChargeAssign.ForEach(x => x.InvoiceId = CFSBTLInvoiceID);

                            }
                        }
                        else
                        {
                            otherChargeAssign.ForEach(x => x.InvoiceId = invoi.InvoiceId);
                        }
                        otherChargeAssign.ForEach(x => x.InvoiceCreated = true);
                        _otherChargeAssigningRepo.UpdateRange(otherChargeAssign);
                    }
                     

                var deliveryorder = _deliveryOrderRepository.GetDeliveryOrderByContainerIndexId(invoice.ContainerIndexId ?? 0);
                if (deliveryorder == null)
                {
                    var dod = new DeliveryOrder
                    {
                        ContainerIndexId = invoice.ContainerIndexId,
                        Date = DateTime.Now,
                        ValidTo = dovalidate,
                        InvoiceNo = invoi.InvoiceNo,
                        DONumber = GetDeliveryOrderNo()
                    };

                    _deliveryOrderRepository.Add(dod);
                    donumber = dod.DONumber;
                }
                else
                {

                    deliveryorder.ContainerIndexId = invoice.ContainerIndexId;
                    deliveryorder.ValidTo = dovalidate;
                    deliveryorder.InvoiceNo = invoi.InvoiceNo;

                    _deliveryOrderRepository.Update(deliveryorder);
                    donumber = deliveryorder.DONumber;
                }

                #endregion


                if (islastPass == true)
                {
                    
                    var ledger = new PartyLedger
                    {
                        LedgerDate = DateTime.Now,
                        BillNo = NormalNumber,
                        Debit = invoi.GrandTotal,
                        PartyId = partyId,
                        IsSettled = true,
                        ShippingAgentId = IsForLine == true ? containerindex.ShippingAgentId : null,
                        Type = "LetPass"


                    };

                    _partyLedgerRepo.Add(ledger);
                   
                }
                else if (paymentReceivedList.Count() > 0)
                {

                    var rescreditalow = paymentReceivedList.Where(x => x.NatureOfAmount == "CreditAllowed").LastOrDefault();

                    var KnockOffinvoice = paymentReceivedList.Where(x => x.NatureOfAmount == "KnockOff").ToList();


                    if (rescreditalow != null)
                    {
                        var creditallowres = _partyLedgerRepo.CreditAllowedCFSBYid(invoice.ContainerIndexId ?? 0);

                        if (creditallowres != null)
                        {
                            creditallowres.IsSettled = true;
                            creditallowres.InvoiceNo = NormalNumber;
                            creditallowres.CreditAllowedExcessSettelment = creditallowsamt;
                            _creditAllowedRepository.Update(creditallowres);
  
                        }

                    }

                    if (KnockOffinvoice.Count() > 0)
                    {

                        foreach (var item in KnockOffinvoice)
                        {
                            var knockoffinvoiceres = _partyLedgerRepo.FindInvoiceAndupdate(item.InoviceNo, NormalNumber);

                        }
                    }
                     

                    paymentReceivedList.ForEach(x => x.InoviceNo = NormalNumber);
                         
                    _paymentReceivedRepository.AddRange(paymentReceivedList);
                        
                    }
                }

              

            }

            if (invoice.Type == "CY")
            {


                if (invoice.AdvanceDate == null)
                {
                    invoice.AdvanceDate = DateTime.Now;
                }

                invoice.ContainerIndexId = null;

                if (Convert.ToDateTime(invoice.AdvanceDate.Value.Date.ToString("MM/dd/yyyy")) < Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy")))
                {
                    return new OkObjectResult(new { error = true, message = "advance date is < current date" });
                }

                var cyContainer = _cyContainerRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == indexNo);

                if (cyContainer != null)
                {
                    invoice.CYContainerId = cyContainer.CYContainerId;
                     
                    var isauction = _invoiceRepo.GetAuctionInfo("CY", cyContainer.VIRNo , cyContainer.IndexNo ?? 0);

                    #region MyRegion 3
                    var soc = _scheduleOfChargeRepository.GetSOCBYIGMIndex(cyContainer.VIRNo, cyContainer.IndexNo ?? 0);

                    if (soc.Count() == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "please generate soc " });
                    }

                    if (userslist.Where(x => x.ShippingAgentId == cyContainer.ShippingAgentId).Count() > 0)
                    {
                        IsForLine = true;
                    }

                    var invoiceres = _invoiceRepo.GetCYFirstInvoice(cyContainer.CYContainerId);

                    if (invoiceres != null)
                    {
                        PerviousInvoicesCount = false;
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

                    if (isauction == true)
                    {
                        var waiver = _waiverRepo.GetAll().Where(x => x.CYContainerId == cyContainer.CYContainerId && x.IsWaive == true && x.InvoiceCreated == false).ToList();
                        normalinvoiceitemList = invoiceitemList;
                        if (waiver.Count() > 0)
                        {
                            foreach (var item in waiver)
                            {
                                var result = normalinvoiceitemList.Find(t => t.Description == item.Description && t.Category == item.Category);

                                if (result != null)
                                {
                                    result.Amount -= item.Discount;

                                }

                            }

                            var dd = waiver.Where(x => x.Description == "Container Storage").LastOrDefault();

                            var ss = normalinvoiceitemList.Where(x => x.Description == "Storage Amount").LastOrDefault();

                            if (dd != null && dd.Discount > 0 && ss != null && ss.Amount > 0)
                            {
                                normalinvoiceitemList.Where(x => x.Description == "Storage Amount").LastOrDefault().Amount -= dd.Discount;
                            }
                             
                        }

                        normalinvoiceitemList.RemoveAll(c => c.Amount <= 0);

                        #region normarl invoice

                        var invoi = new Invoice
                        {
                            TariffInformationId = invoice.TariffInformationId,
                            AdvanceDate = invoice.AdvanceDate,
                            BalanceAmount = invoice.BalanceAmount,
                            BillType = "Normal",
                            InvoiceCategory = IsForLine == true ? "FF" : "AICT",
                            InvoiceType = IsForLine == true ? "DMC" : "SSTI",
                            InvoiceNatureType = islastPass,
                            CBM = invoice.CBM,
                            ClearingAgentNTN = invoice.ClearingAgentNTN,
                            CYContainerId = invoice.CYContainerId,
                            ExcessAmount = invoice.ExcessAmount,
                            StorageApplicableon = invoice.StorageApplicableon,
                            ContainerIndexId = null,
                            DestuffDate = invoice.DestuffDate,
                            Year = DateTime.Now.ToString("yyyy"),
                            GateInDate = invoice.GateInDate,
                            InvoiceDate = invoice.InvoiceDate,
                            BillToLineAmount = 0,
                            WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                            BalanceCargo = invoice.BalanceCargo,
                            TariffAmountTotal = invoice.TariffAmountTotal,
                            InvoiceNo = IsForLine == true ? GetLastInvoiceNoForFF() : GetLastInvoiceNo(),
                            IsAdvanceBill = invoice.IsAdvanceBill,
                            PartContainer = invoice.PartContainer,
                            IsCancelled = false,
                            ActualTariffCharges = 0,
                            PhoneNumber = invoice.PhoneNumber,
                            ClearingAgentRep = invoice.ClearingAgentRep,
                            CNIC = invoice.CNIC,
                            CargoType = invoice.CargoType,
                            ClearingAgentId = invoice.ClearingAgentId,
                            Size = invoice.Size,
                            StorageDays = invoice.StorageDays,
                            TotalFreeDays = invoice.TotalFreeDays,
                            AdditionalFreeDays = invoice.AdditionalFreeDays,
                            Weight = invoice.Weight,
                            //update exchange rate in all invoice without perfoma invoices
                            ExchangeRateAmount = invoice.ExchangeRateAmount,
                            Type = invoice.Type,
                            OtherChargeAmount = 0,
                            CYStorageSizeAmount = 0,
                            VehicleCharges = 0,
                            SealCharger = 0,
                            PortChargeAmount = 0,
                            StorageTotal = 0,
                            TotalCharges = 0,
                            PreviousBillAmount = 0,
                            BalanceAmountTotal = 0,
                            SalesTax = 0,
                            AfterSalesTax = 0,
                            GrandTotal = 0,
                            IsAdjust = false,
                            IsLineIndexRate = PerviousInvoicesCount ,

                            IGMCBM = invoice.IGMCBM,
                            DestuffCBM = invoice.DestuffCBM,
                            ExaminationCBM = invoice.ExaminationCBM,
                            FFCBM = invoice.FFCBM,
                            ActualWT = invoice.ActualWT,
                            MFTWT = invoice.MFTWT,
                            FFStorageShare = invoice.FFStorageShare,
                            AictPerBoxRate = invoice.AictPerBoxRate,
                            TotalArriveIndexes = invoice.TotalArriveIndexes,

                        };

                        invoi.GrandTotal = Math.Round(invoi.GrandTotal, 2);

                        _invoiceRepo.Add(invoi);

                        NormalNumber = invoi.InvoiceNo;
                        #endregion

                        #region A4 Adjustment invocie zero

                        if (IsForLine == true)
                        {
                            var A4Adjustmentinvociezeroinvoi = new Invoice
                            {
                                TariffInformationId = invoice.TariffInformationId,
                                AdvanceDate = invoice.AdvanceDate,
                                BalanceAmount = invoice.BalanceAmount,
                                BillType = "Normal",
                                InvoiceCategory = "AICT",
                                InvoiceType = "SSTI",
                                CBM = 0,
                                ClearingAgentNTN = invoice.ClearingAgentNTN,
                                ContainerIndexId = null,
                                CYContainerId = invoice.CYContainerId,
                                DestuffDate = invoice.DestuffDate,
                                InvoiceNatureType = islastPass,
                                StorageApplicableon = invoice.StorageApplicableon,
                                GateInDate = invoice.GateInDate,
                                InvoiceDate = invoice.InvoiceDate,
                                BillToLineAmount = 0,
                                Year = DateTime.Now.ToString("yyyy"),
                                WaiverDiscountAmount = 0,
                                TariffAmountTotal = 0,
                                AdditionalFreeDays = 0,
                                InvoiceNo = GetLastInvoiceNo(),
                                IsAdvanceBill = invoice.IsAdvanceBill,
                                IsCancelled = false,
                                ClearingAgentId = invoice.ClearingAgentId,
                                CNIC = invoice.CNIC,
                                ClearingAgentRep = invoice.ClearingAgentRep,
                                BalanceCargo = 0,
                                PhoneNumber = invoice.PhoneNumber,
                                Size = invoice.Size,
                                StorageDays = 0,
                                TotalFreeDays = 0,
                                CargoType = invoice.CargoType,
                                Weight = 0,
                                ExcessAmount = 0,
                                Type = invoice.Type,
                                PartContainer = invoice.PartContainer,
                                OtherChargeAmount = 0,
                                CYStorageSizeAmount = 0,
                                StorageTotal = 0,
                                PortChargeAmount = 0,
                                ExchangeRateAmount = 0,
                                SealCharger = 0,
                                VehicleCharges = 0,
                                TotalCharges = 0,
                                PreviousBillAmount = 0,
                                BalanceAmountTotal = 0,
                                SalesTax = 0,
                                AfterSalesTax = 0,
                                GrandTotal = 0,
                                IsAdjust = false,
                                PerfomaReceiptNumber = NormalNumber,
                                IsLineIndexRate = false ,

                                IGMCBM = invoice.IGMCBM,
                                DestuffCBM = invoice.DestuffCBM,
                                ExaminationCBM = invoice.ExaminationCBM,
                                FFCBM = invoice.FFCBM,
                                ActualWT = invoice.ActualWT,
                                MFTWT = invoice.MFTWT,
                                FFStorageShare = invoice.FFStorageShare,
                                AictPerBoxRate = invoice.AictPerBoxRate,
                                TotalArriveIndexes = invoice.TotalArriveIndexes,
                            };
                            _invoiceRepo.Add(A4Adjustmentinvociezeroinvoi);
                        }


                        #endregion


                        #region normarl auction invoice

                        var invoAuction = new Invoice
                        {
                            TariffInformationId = invoice.TariffInformationId,
                            AdvanceDate = invoice.AdvanceDate,
                            BalanceAmount = invoice.BalanceAmount,
                            BillType = "Auction",
                            InvoiceCategory = IsForLine == true ? "FF" : "AICT",
                            InvoiceType = IsForLine == true ? "DMC" : "SSTI",
                            InvoiceNatureType = islastPass,
                            CBM = invoice.CBM,
                            ClearingAgentNTN = invoice.ClearingAgentNTN,
                            CYContainerId = invoice.CYContainerId,
                            ExcessAmount = invoice.ExcessAmount,
                            StorageApplicableon = invoice.StorageApplicableon,
                            ContainerIndexId = null,
                            DestuffDate = invoice.DestuffDate,
                            Year = DateTime.Now.ToString("yyyy"),
                            GateInDate = invoice.GateInDate,
                            InvoiceDate = invoice.InvoiceDate,
                            BillToLineAmount = invoice.BillToLineAmount,
                            WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                            BalanceCargo = invoice.BalanceCargo,
                            TariffAmountTotal = invoice.TariffAmountTotal,
                            InvoiceNo = IsForLine == true ? GetLastInvoiceNoForFF() : GetLastInvoiceNo(),
                            IsAdvanceBill = invoice.IsAdvanceBill,
                            PartContainer = invoice.PartContainer,
                            IsCancelled = false,
                            ActualTariffCharges = invoice.TotalCharges,
                            PhoneNumber = invoice.PhoneNumber,
                            ClearingAgentRep = invoice.ClearingAgentRep,
                            CNIC = invoice.CNIC,
                            CargoType = invoice.CargoType,
                            ClearingAgentId = invoice.ClearingAgentId,
                            InvoiceItems = normalinvoiceitemList,
                            Size = invoice.Size,
                            StorageDays = invoice.StorageDays,
                            TotalFreeDays = invoice.TotalFreeDays,
                            AdditionalFreeDays = invoice.AdditionalFreeDays,
                            Weight = invoice.Weight,
                            ExchangeRateAmount = invoice.ExchangeRateAmount,
                            Type = invoice.Type,
                            OtherChargeAmount = invoice.OtherChargeAmount,
                            CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                            VehicleCharges = invoice.VehicleCharges,
                            SealCharger = invoice.SealCharger,
                            PortChargeAmount = invoice.PortChargeAmount,
                            StorageTotal = invoice.StorageTotal,
                            TotalCharges = normalinvoiceitemList.Sum(x => x.Amount),
                            PreviousBillAmount = invoice.PreviousBillAmount,
                            BalanceAmountTotal = normalinvoiceitemList.Sum(x => x.Amount),
                            SalesTax = invoice.SalesTax,
                            AfterSalesTax = Math.Ceiling((normalinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                            GrandTotal = (normalinvoiceitemList.Sum(x => x.Amount)) + Math.Ceiling((normalinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                            IsAdjust = false,
                            IsLineIndexRate = false ,

                            IGMCBM = invoice.IGMCBM,
                            DestuffCBM = invoice.DestuffCBM,
                            ExaminationCBM = invoice.ExaminationCBM,
                            FFCBM = invoice.FFCBM,
                            ActualWT = invoice.ActualWT,
                            MFTWT = invoice.MFTWT,
                            FFStorageShare = invoice.FFStorageShare,
                            AictPerBoxRate = invoice.AictPerBoxRate,
                            TotalArriveIndexes = invoice.TotalArriveIndexes,
                        };

                        invoAuction.GrandTotal = Math.Round(invoAuction.GrandTotal, 2);

                        _invoiceRepo.Add(invoAuction);

                        AuctionInvoiveNumber = invoAuction.InvoiceNo;

                        #endregion


                        #region A5 Adjustment invocie zero
                        if (IsForLine == true)
                        {
                            var A5Adjustmentinvociezeroinvoi = new Invoice
                            {
                                TariffInformationId = invoice.TariffInformationId,
                                AdvanceDate = invoice.AdvanceDate,
                                BalanceAmount = invoice.BalanceAmount,
                                BillType = "Auction",
                                InvoiceCategory = "AICT",
                                InvoiceType = "SSTI",
                                CBM = 0,
                                ClearingAgentNTN = invoice.ClearingAgentNTN,
                                ContainerIndexId = null,
                                CYContainerId = invoice.CYContainerId,
                                DestuffDate = invoice.DestuffDate,
                                InvoiceNatureType = islastPass,
                                StorageApplicableon = invoice.StorageApplicableon,
                                GateInDate = invoice.GateInDate,
                                InvoiceDate = invoice.InvoiceDate,
                                BillToLineAmount = 0,
                                Year = DateTime.Now.ToString("yyyy"),
                                WaiverDiscountAmount = 0,
                                TariffAmountTotal = 0,
                                AdditionalFreeDays = 0,
                                InvoiceNo = GetLastInvoiceNo(),
                                IsAdvanceBill = invoice.IsAdvanceBill,
                                IsCancelled = false,
                                ClearingAgentId = invoice.ClearingAgentId,
                                CNIC = invoice.CNIC,
                                ClearingAgentRep = invoice.ClearingAgentRep,
                                BalanceCargo = 0,
                                PhoneNumber = invoice.PhoneNumber,
                                Size = invoice.Size,
                                StorageDays = 0,
                                TotalFreeDays = 0,
                                CargoType = invoice.CargoType,
                                Weight = 0,
                                ExcessAmount = 0,
                                Type = invoice.Type,
                                PartContainer = invoice.PartContainer,
                                OtherChargeAmount = 0,
                                CYStorageSizeAmount = 0,
                                StorageTotal = 0,
                                PortChargeAmount = 0,
                                ExchangeRateAmount = 0,
                                SealCharger = 0,
                                VehicleCharges = 0,
                                TotalCharges = 0,
                                PreviousBillAmount = 0,
                                BalanceAmountTotal = 0,
                                SalesTax = 0,
                                AfterSalesTax = 0,
                                GrandTotal = 0,
                                IsAdjust = false,
                                PerfomaReceiptNumber = AuctionInvoiveNumber,
                                IsLineIndexRate = false ,

                                IGMCBM = invoice.IGMCBM,
                                DestuffCBM = invoice.DestuffCBM,
                                ExaminationCBM = invoice.ExaminationCBM,
                                FFCBM = invoice.FFCBM,
                                ActualWT = invoice.ActualWT,
                                MFTWT = invoice.MFTWT,
                                FFStorageShare = invoice.FFStorageShare,
                                AictPerBoxRate = invoice.AictPerBoxRate,
                                TotalArriveIndexes = invoice.TotalArriveIndexes,
                            };
                            _invoiceRepo.Add(A5Adjustmentinvociezeroinvoi);
                        }


                        #endregion

                        var otherChargeAssign = _otherChargeAssigningRepo.GetUnPaidOtherChargeAssigning( "CY" ,invoice.CYContainerId ?? 0).ToList();


                        if (otherChargeAssign.Count() > 0)
                        {

                            //update invoiceno in specialhandling 
                            if (invoAuction.InvoiceItems.Count() > 0)
                            {
                                if (invoAuction.InvoiceItems.Where(x => x.Category == "SpecialHandlingCharges").Count() > 0)
                                {
                                    otherChargeAssign.ForEach(x => x.InvoiceId = invoAuction.InvoiceId);
                                }
                            }
                            else
                            {
                                otherChargeAssign.ForEach(x => x.InvoiceId = invoi.InvoiceId);
                            }
                            otherChargeAssign.ForEach(x => x.InvoiceCreated = true);
                            _otherChargeAssigningRepo.UpdateRange(otherChargeAssign);
                        }

                        if (waiver.Count() > 0)
                        {
                            //update waiver againsts invoice
                            waiver.ForEach(x => x.InvoiceNumber = NormalNumber);
                            waiver.ForEach(x => x.InvoiceCreated = true);

                            _waiverRepo.UpdateRange(waiver);
                        }

                        var deliveryorder = _deliveryOrderRepository.GetAll().Where(x => x.CYContainerId == cyContainer.CYContainerId).FirstOrDefault();

                        if (deliveryorder == null)
                        {
                            var dod = new DeliveryOrder
                            {
                                CYContainerId = cyContainer.CYContainerId,
                                Date = DateTime.Now,
                                ValidTo = dovalidate,
                                InvoiceNo = invoi.InvoiceNo,
                                DONumber = GetDeliveryOrderNo()
                            };


                            _deliveryOrderRepository.Add(dod);
                            donumber = dod.DONumber;
                        }
                        else
                        {

                            deliveryorder.CYContainerId = cyContainer.CYContainerId;
                            deliveryorder.ValidTo = dovalidate;
                            deliveryorder.InvoiceNo = invoi.InvoiceNo;
                            _deliveryOrderRepository.Update(deliveryorder);
                            donumber = deliveryorder.DONumber;
                        }

                        if (islastPass == true)
                        {
                            var ledger = new PartyLedger
                            {
                                LedgerDate = DateTime.Now,
                                BillNo = AuctionInvoiveNumber,
                                Debit = invoAuction.GrandTotal,
                                PartyId = partyId,
                                IsSettled = true,
                                ShippingAgentId = IsForLine == true ? cyContainer.ShippingAgentId : null,
                                Type = "LetPass"

                            };

                            _partyLedgerRepo.Add(ledger);


                        }
                        else if (paymentReceivedList.Count() > 0)
                        {

                            var rescreditalow = paymentReceivedList.Where(x => x.NatureOfAmount == "CreditAllowed").LastOrDefault();

                            var KnockOffinvoice = paymentReceivedList.Where(x => x.NatureOfAmount == "KnockOff").ToList();


                            if (rescreditalow != null)
                            {
                                var creditallowres = _partyLedgerRepo.CreditAllowedCYbyid(invoice.CYContainerId ?? 0);

                                if (creditallowres != null)
                                {
                                    creditallowres.IsSettled = true;
                                    creditallowres.InvoiceNo = AuctionInvoiveNumber;
                                    creditallowres.CreditAllowedExcessSettelment = creditallowsamt;
                                    _creditAllowedRepository.Update(creditallowres);


                                }


                            }

                            if (KnockOffinvoice.Count() > 0)
                            {
                                foreach (var item in KnockOffinvoice)
                                {
                                    var knockoffinvoiceres = _partyLedgerRepo.FindInvoiceAndupdate(item.InoviceNo, AuctionInvoiveNumber);

                                }
                            }

                            paymentReceivedList.ForEach(x => x.InoviceNo = AuctionInvoiveNumber);

                            _paymentReceivedRepository.AddRange(paymentReceivedList);

                        }

                    }
                    else
                    {
                        var undeliverdresbillToLine = _billToLineRepository.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexNo && x.IndexType == "CY" && x.InoviceCreated == false).LastOrDefault();
                        var waiver = _waiverRepo.GetAll().Where(x => x.CYContainerId == cyContainer.CYContainerId && x.IsWaive == true && x.InvoiceCreated == false).ToList();

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
                                            Category = item.Category
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
                                            Category = item.Category

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



                        if (waiver.Count() > 0)
                        {
                            foreach (var item in waiver)
                            {
                                var result = normalinvoiceitemList.Find(t => t.Description == item.Description && t.Category == item.Category);

                                if (result != null)
                                {
                                    result.Amount -= item.Discount;

                                }

                            }

                            var dd = waiver.Where(x => x.Description == "Container Storage").LastOrDefault();

                            var ss = normalinvoiceitemList.Where(x => x.Description == "Storage Amount").LastOrDefault();

                            if (dd != null && dd.Discount > 0 && ss != null && ss.Amount > 0)
                            {
                                normalinvoiceitemList.Where(x => x.Description == "Storage Amount").LastOrDefault().Amount -= dd.Discount;
                            }

                           

                        }
                        normalinvoiceitemList.RemoveAll(c => c.Amount <= 0);
                        var invoi = new Invoice
                        {
                            TariffInformationId = invoice.TariffInformationId,
                            AdvanceDate = invoice.AdvanceDate,
                            BalanceAmount = invoice.BalanceAmount,
                            BillType = "Normal",
                            InvoiceCategory = IsForLine == true ? "FF" : "AICT",
                            InvoiceType = IsForLine == true ? "DMC" : "SSTI",
                            InvoiceNatureType = islastPass,
                            CBM = invoice.CBM,
                            ClearingAgentNTN = invoice.ClearingAgentNTN,
                            CYContainerId = invoice.CYContainerId,
                            ExcessAmount = invoice.ExcessAmount,
                            StorageApplicableon = invoice.StorageApplicableon,
                            ContainerIndexId = null,
                            DestuffDate = invoice.DestuffDate,
                            Year = DateTime.Now.ToString("yyyy"),
                            GateInDate = invoice.GateInDate,
                            InvoiceDate = invoice.InvoiceDate,
                            BillToLineAmount = invoice.BillToLineAmount,
                            WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                            BalanceCargo = invoice.BalanceCargo,
                            TariffAmountTotal = invoice.TariffAmountTotal,
                            InvoiceNo = IsForLine == true ? GetLastInvoiceNoForFF() : GetLastInvoiceNo(),
                            IsAdvanceBill = invoice.IsAdvanceBill,
                            PartContainer = invoice.PartContainer,
                            IsCancelled = false,
                            ActualTariffCharges = invoice.TotalCharges,
                            PhoneNumber = invoice.PhoneNumber,
                            ClearingAgentRep = invoice.ClearingAgentRep,
                            CNIC = invoice.CNIC,
                            CargoType = invoice.CargoType,
                            ClearingAgentId = invoice.ClearingAgentId,
                            InvoiceItems = normalinvoiceitemList,
                            Size = invoice.Size,
                            StorageDays = invoice.StorageDays,
                            TotalFreeDays = invoice.TotalFreeDays,
                            AdditionalFreeDays = invoice.AdditionalFreeDays,
                            Weight = invoice.Weight,
                            ExchangeRateAmount = invoice.ExchangeRateAmount,
                            Type = invoice.Type,
                            OtherChargeAmount = invoice.OtherChargeAmount,
                            CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                            VehicleCharges = invoice.VehicleCharges,
                            SealCharger = invoice.SealCharger,
                            PortChargeAmount = invoice.PortChargeAmount,
                            StorageTotal = invoice.StorageTotal,
                            TotalCharges = normalinvoiceitemList.Sum(x => x.Amount),
                            PreviousBillAmount = invoice.PreviousBillAmount,
                            BalanceAmountTotal = normalinvoiceitemList.Sum(x => x.Amount),
                            SalesTax = invoice.SalesTax,
                            AfterSalesTax = Math.Ceiling((normalinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                            GrandTotal = (normalinvoiceitemList.Sum(x => x.Amount)) + Math.Ceiling((normalinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                            IsAdjust = false,
                            IsLineIndexRate = undeliverdresbillToLine != null ? false : PerviousInvoicesCount ,

                            IGMCBM = invoice.IGMCBM,
                            DestuffCBM = invoice.DestuffCBM,
                            ExaminationCBM = invoice.ExaminationCBM,
                            FFCBM = invoice.FFCBM,
                            ActualWT = invoice.ActualWT,
                            MFTWT = invoice.MFTWT,
                            FFStorageShare = invoice.FFStorageShare,
                            AictPerBoxRate = invoice.AictPerBoxRate,
                            TotalArriveIndexes = invoice.TotalArriveIndexes,
                        };

                        invoi.GrandTotal = Math.Round(invoi.GrandTotal, 2);

                        _invoiceRepo.Add(invoi);

                        NormalNumber = invoi.InvoiceNo;


                        #region A6 Adjustment invocie zero
                        if (IsForLine == true)
                        {
                            var A6Adjustmentinvociezeroinvoi = new Invoice
                            {
                                TariffInformationId = invoice.TariffInformationId,
                                AdvanceDate = invoice.AdvanceDate,
                                BalanceAmount = invoice.BalanceAmount,
                                BillType = "Normal",
                                InvoiceCategory = "AICT",
                                InvoiceType = "SSTI",
                                CBM = 0,
                                ClearingAgentNTN = invoice.ClearingAgentNTN,
                                ContainerIndexId = null,
                                CYContainerId = invoice.CYContainerId,
                                DestuffDate = invoice.DestuffDate,
                                InvoiceNatureType = islastPass,
                                StorageApplicableon = invoice.StorageApplicableon,
                                GateInDate = invoice.GateInDate,
                                InvoiceDate = invoice.InvoiceDate,
                                BillToLineAmount = 0,
                                Year = DateTime.Now.ToString("yyyy"),
                                WaiverDiscountAmount = 0,
                                TariffAmountTotal = 0,
                                AdditionalFreeDays = 0,
                                InvoiceNo = GetLastInvoiceNo(),
                                IsAdvanceBill = invoice.IsAdvanceBill,
                                IsCancelled = false,
                                ClearingAgentId = invoice.ClearingAgentId,
                                CNIC = invoice.CNIC,
                                ClearingAgentRep = invoice.ClearingAgentRep,
                                BalanceCargo = 0,
                                PhoneNumber = invoice.PhoneNumber,
                                Size = invoice.Size,
                                StorageDays = 0,
                                TotalFreeDays = 0,
                                CargoType = invoice.CargoType,
                                Weight = 0,
                                ExcessAmount = 0,
                                Type = invoice.Type,
                                PartContainer = invoice.PartContainer,
                                OtherChargeAmount = 0,
                                CYStorageSizeAmount = 0,
                                StorageTotal = 0,
                                PortChargeAmount = 0,
                                ExchangeRateAmount = 0,
                                SealCharger = 0,
                                VehicleCharges = 0,
                                TotalCharges = 0,
                                PreviousBillAmount = 0,
                                BalanceAmountTotal = 0,
                                SalesTax = 0,
                                AfterSalesTax = 0,
                                GrandTotal = 0,
                                IsAdjust = false,
                                PerfomaReceiptNumber = NormalNumber,
                                IsLineIndexRate = false  ,

                                IGMCBM = invoice.IGMCBM,
                                DestuffCBM = invoice.DestuffCBM,
                                ExaminationCBM = invoice.ExaminationCBM,
                                FFCBM = invoice.FFCBM,
                                ActualWT = invoice.ActualWT,
                                MFTWT = invoice.MFTWT,
                                FFStorageShare = invoice.FFStorageShare,
                                AictPerBoxRate = invoice.AictPerBoxRate,
                                TotalArriveIndexes = invoice.TotalArriveIndexes,

                            };
                            _invoiceRepo.Add(A6Adjustmentinvociezeroinvoi);
                        }


                        #endregion

                        

                        if (waiver.Count() > 0)
                        {
                            //update waiver againsts invoice
                            waiver.ForEach(x => x.InvoiceNumber = NormalNumber);
                            waiver.ForEach(x => x.InvoiceCreated = true);
                             
                            _waiverRepo.UpdateRange(waiver);
                        }


                        if (undeliverdresbillToLine != null)
                        {
                            if (undeliverdresbillToLine.Type == "HundredPercent")
                            {
                                billtolineinvoiceitemList = invoiceitemList;
                            }

                            if (undeliverdresbillToLine.Type == "ExStorage")
                            {
                                billtolineinvoiceitemList = invoiceitemList.Where(x => x.Category != "Storage").ToList();
                            }
                            if (undeliverdresbillToLine.Type == "OnlyTariff")
                            {
                                billtolineinvoiceitemList = invoiceitemList.Where(x => x.Category == "Tariff").ToList();
                            }

                            if (undeliverdresbillToLine.Type == "ManualAmount")
                            {
                                var manamt = undeliverdresbillToLine.TariffAmount;


                                foreach (var item in invoiceitemList)
                                {


                                    if (manamt > 0)
                                    {

                                        manamt = manamt - item.Amount;

                                        if (manamt < 0)
                                        {
                                            var req = new InvoiceItem
                                            {
                                                Amount = manamt + item.Amount,
                                                Description = item.Description,
                                                Type = item.Type,
                                                Category = item.Category



                                            };

                                            billtolineinvoiceitemList.Add(req);

                                            break;
                                        }

                                        if (manamt > 0)
                                        {
                                            billtolineinvoiceitemList.Add(item);

                                        }

                                        if (manamt == 0)
                                        {
                                            billtolineinvoiceitemList.Add(item);
                                            break;
                                        }



                                    }





                                }

                            }


                            var invo = new Invoice
                            {
                                TariffInformationId = invoice.TariffInformationId,
                                AdvanceDate = invoice.AdvanceDate,
                                BalanceAmount = invoice.BalanceAmount,
                                BillType = "Bill To Line",
                                InvoiceCategory = IsForLine == true ? "FF" : "AICT",
                                InvoiceType = IsForLine == true ? "DMC" : "SSTI",
                                InvoiceNatureType = islastPass,
                                CBM = invoice.CBM,
                                StorageApplicableon = invoice.StorageApplicableon,
                                ClearingAgentNTN = invoice.ClearingAgentNTN,
                                CYContainerId = invoice.CYContainerId,
                                ContainerIndexId = null,
                                DestuffDate = invoice.DestuffDate,
                                GateInDate = invoice.GateInDate,
                                InvoiceDate = invoice.InvoiceDate,
                                BillToLineAmount = invoice.BillToLineAmount,
                                AdditionalFreeDays = invoice.AdditionalFreeDays,
                                PhoneNumber = invoice.PhoneNumber,
                                ClearingAgentRep = invoice.ClearingAgentRep,
                                CNIC = invoice.CNIC,
                                ClearingAgentId = invoice.ClearingAgentId,
                                CargoType = invoice.CargoType,
                                Year = DateTime.Now.ToString("yyyy"),
                                PartContainer = invoice.PartContainer,
                                WaiverDiscountAmount = invoice.WaiverDiscountAmount,
                                TariffAmountTotal = invoice.TariffAmountTotal,
                                InvoiceNo = IsForLine == true ? GetLastInvoiceNoForFF() : GetLastInvoiceNo(),
                                IsAdvanceBill = invoice.IsAdvanceBill,
                                IsCancelled = false,
                                InvoiceItems = billtolineinvoiceitemList,
                                ExchangeRateAmount = invoice.ExchangeRateAmount,
                                Size = invoice.Size,
                                StorageDays = invoice.StorageDays,
                                TotalFreeDays = invoice.TotalFreeDays,
                                Weight = invoice.Weight,
                                Type = invoice.Type,
                                ActualTariffCharges = invoice.TotalCharges,
                                BalanceCargo = invoice.BalanceCargo,
                                OtherChargeAmount = invoice.OtherChargeAmount,
                                CYStorageSizeAmount = invoice.CYStorageSizeAmount,
                                StorageTotal = 0,
                                TotalCharges = billtolineinvoiceitemList.Sum(x => x.Amount),
                                PreviousBillAmount = invoice.PreviousBillAmount,
                                BalanceAmountTotal = billtolineinvoiceitemList.Sum(x => x.Amount),
                                SalesTax = invoice.SalesTax,
                                AfterSalesTax = Math.Ceiling((billtolineinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                                GrandTotal = billtolineinvoiceitemList.Sum(x => x.Amount) + Math.Ceiling((billtolineinvoiceitemList.Sum(x => x.Amount)) * (Convert.ToDouble(invoice.SalesTax) / 100)),
                                IsAdjust = false,
                                IsLineIndexRate = PerviousInvoicesCount ,

                                IGMCBM = invoice.IGMCBM,
                                DestuffCBM = invoice.DestuffCBM,
                                ExaminationCBM = invoice.ExaminationCBM,
                                FFCBM = invoice.FFCBM,
                                ActualWT = invoice.ActualWT,
                                MFTWT = invoice.MFTWT,
                                FFStorageShare = invoice.FFStorageShare,
                                AictPerBoxRate = invoice.AictPerBoxRate,
                                TotalArriveIndexes = invoice.TotalArriveIndexes,
                            };

                            invo.GrandTotal = Math.Round(invo.GrandTotal, 2);

                            _invoiceRepo.Add(invo);

                            BillToLineNumber = invo.InvoiceNo;
                            CYBTLInvoiceID = invo.InvoiceId;

                            var billtolines = _billToLineRepository.GetAll().Where(x => x.VirNo == cyContainer.VIRNo && x.IndexNo == cyContainer.IndexNo && x.IndexType == "CY" && x.InoviceCreated == false).ToList();
                            if (billtolines.Count() > 0)
                            {

                                foreach (var item in billtolines)
                                {
                                    item.InoviceCreated = true;
                                    item.InvoiceNumber = invo.InvoiceNo;
                                    item.InvoiceAmount = invo.TotalCharges;
                                    item.TariffAmount = invo.TotalCharges;

                                }

                                _billToLineRepository.UpdateRange(billtolines);
                            }

                            #region A7 Adjustment invocie zero
                            if (IsForLine == true)
                            {
                                var A7Adjustmentinvociezeroinvoi = new Invoice
                                {
                                    TariffInformationId = invoice.TariffInformationId,
                                    AdvanceDate = invoice.AdvanceDate,
                                    BalanceAmount = invoice.BalanceAmount,
                                    BillType = "Bill To Line",
                                    InvoiceCategory = "AICT",
                                    InvoiceType = "SSTI",
                                    CBM = 0,
                                    ClearingAgentNTN = invoice.ClearingAgentNTN,
                                    ContainerIndexId = null,
                                    CYContainerId = invoice.CYContainerId,
                                    DestuffDate = invoice.DestuffDate,
                                    InvoiceNatureType = islastPass,
                                    StorageApplicableon = invoice.StorageApplicableon,
                                    GateInDate = invoice.GateInDate,
                                    InvoiceDate = invoice.InvoiceDate,
                                    BillToLineAmount = 0,
                                    Year = DateTime.Now.ToString("yyyy"),
                                    WaiverDiscountAmount = 0,
                                    TariffAmountTotal = 0,
                                    AdditionalFreeDays = 0,
                                    InvoiceNo = GetLastInvoiceNo(),
                                    IsAdvanceBill = invoice.IsAdvanceBill,
                                    IsCancelled = false,
                                    ClearingAgentId = invoice.ClearingAgentId,
                                    CNIC = invoice.CNIC,
                                    ClearingAgentRep = invoice.ClearingAgentRep,
                                    BalanceCargo = 0,
                                    PhoneNumber = invoice.PhoneNumber,
                                    Size = invoice.Size,
                                    StorageDays = 0,
                                    TotalFreeDays = 0,
                                    CargoType = invoice.CargoType,
                                    Weight = 0,
                                    ExcessAmount = 0,
                                    Type = invoice.Type,
                                    PartContainer = invoice.PartContainer,
                                    OtherChargeAmount = 0,
                                    CYStorageSizeAmount = 0,
                                    StorageTotal = 0,
                                    PortChargeAmount = 0,
                                    ExchangeRateAmount = 0,
                                    SealCharger = 0,
                                    VehicleCharges = 0,
                                    TotalCharges = 0,
                                    PreviousBillAmount = 0,
                                    BalanceAmountTotal = 0,
                                    SalesTax = 0,
                                    AfterSalesTax = 0,
                                    GrandTotal = 0,
                                    IsAdjust = false,
                                    PerfomaReceiptNumber = BillToLineNumber,
                                    IsLineIndexRate = false ,

                                    IGMCBM = invoice.IGMCBM,
                                    DestuffCBM = invoice.DestuffCBM,
                                    ExaminationCBM = invoice.ExaminationCBM,
                                    FFCBM = invoice.FFCBM,
                                    ActualWT = invoice.ActualWT,
                                    MFTWT = invoice.MFTWT,
                                    FFStorageShare = invoice.FFStorageShare,
                                    AictPerBoxRate = invoice.AictPerBoxRate,
                                    TotalArriveIndexes = invoice.TotalArriveIndexes,
                                };
                                _invoiceRepo.Add(A7Adjustmentinvociezeroinvoi);
                            }


                            #endregion

                        }


                        var otherChargeAssign = _otherChargeAssigningRepo.GetUnPaidOtherChargeAssigning("CY", invoice.CYContainerId ?? 0 ).ToList();

                        if (otherChargeAssign.Count() > 0)
                        {
                            //update invoiceno in specialhandling 
                            if (billtolineinvoiceitemList.Count() > 0)
                            {
                                if (billtolineinvoiceitemList.Where(x => x.Category == "SpecialHandlingCharges").Count() > 0)
                                {
                                    otherChargeAssign.ForEach(x => x.InvoiceId = CYBTLInvoiceID);

                                }
                            }
                            else
                            {
                                otherChargeAssign.ForEach(x => x.InvoiceId = invoi.InvoiceId);
                            }
                            otherChargeAssign.ForEach(x => x.InvoiceCreated = true);
                            _otherChargeAssigningRepo.UpdateRange(otherChargeAssign);
                        }


                        var deliveryorder = _deliveryOrderRepository.GetAll().Where(x => x.CYContainerId == cyContainer.CYContainerId).FirstOrDefault();

                        if (deliveryorder == null)
                        {
                            var dod = new DeliveryOrder
                            {
                                CYContainerId = cyContainer.CYContainerId,
                                Date = DateTime.Now,
                                ValidTo = dovalidate,
                                InvoiceNo = invoi.InvoiceNo,
                                DONumber = GetDeliveryOrderNo()
                            };


                            _deliveryOrderRepository.Add(dod);
                            donumber = dod.DONumber;
                        }
                        else
                        {

                            deliveryorder.CYContainerId = cyContainer.CYContainerId;
                            deliveryorder.ValidTo = dovalidate;
                            deliveryorder.InvoiceNo = invoi.InvoiceNo;
                            _deliveryOrderRepository.Update(deliveryorder);
                            donumber = deliveryorder.DONumber;
                        }
                        #endregion


                        if (islastPass == true)
                        {
                            var ledger = new PartyLedger
                            {
                                LedgerDate = DateTime.Now,
                                BillNo = NormalNumber,
                                Debit = invoi.GrandTotal,
                                PartyId = partyId,
                                IsSettled = true,
                                ShippingAgentId = IsForLine == true ? cyContainer.ShippingAgentId : null,
                                Type = "LetPass"

                            };

                            _partyLedgerRepo.Add(ledger);


                        }
                        else if (paymentReceivedList.Count() > 0)
                        {

                            var rescreditalow = paymentReceivedList.Where(x => x.NatureOfAmount == "CreditAllowed").LastOrDefault();

                            var KnockOffinvoice = paymentReceivedList.Where(x => x.NatureOfAmount == "KnockOff").ToList();


                            if (rescreditalow != null)
                            {
                                var creditallowres = _partyLedgerRepo.CreditAllowedCYbyid(invoice.CYContainerId ?? 0);

                                if (creditallowres != null)
                                {
                                    creditallowres.IsSettled = true;
                                    creditallowres.InvoiceNo = NormalNumber;
                                    creditallowres.CreditAllowedExcessSettelment = creditallowsamt;
                                    _creditAllowedRepository.Update(creditallowres);


                                }


                            }

                            if (KnockOffinvoice.Count() > 0)
                            {
                                foreach (var item in KnockOffinvoice)
                                {
                                    var knockoffinvoiceres = _partyLedgerRepo.FindInvoiceAndupdate(item.InoviceNo, NormalNumber);

                                }
                            }

                            paymentReceivedList.ForEach(x => x.InoviceNo = NormalNumber);

                            _paymentReceivedRepository.AddRange(paymentReceivedList);

                        }
                    }


                     


                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "container index not found" });

                }
            }



            return new OkObjectResult(new { error = false, message = "created", InvoiceNo = NormalNumber, DeliveryOrderNumber = donumber, InvoiceNoBillToLine = BillToLineNumber, AuctionInvoiveNo = AuctionInvoiveNumber , IsForLine = IsForLine });

        }


    }
}