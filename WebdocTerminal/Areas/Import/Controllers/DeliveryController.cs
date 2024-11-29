using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class DeliveryController : ParentController
    {
        public IDeliveryOrderRepository _deliveryOrderRepo;
        public IOrderDetailRepository _orderDetailRepo;
        public IVesselRepository _vesselRepo;
        public IVoyageRepository _voyageRepo;
        public IContainerRepository _containerRepo;
        public IContainerIndexRepository _containerIndexRepo;
        public IClearingAgentRepository _clearingAgentRepo;
        public ICRLORepository _crloRepo;
        public ICRLRepository _crlRepo;
        public ICYContainerRepository _cyContainerRepo;
        public IInvoiceRepository _invoiceRepo;
        public IAmountReceivedRepository _amountReceivedRepo;
        public IIGMORepository _iGMORepository;
        public IGDIORepository _gdioRepo;
        public IEmptyContainerGatePassRepository _emptyContainerGatePassRepository;
        public IDOItemRepository _doItemRepo;
        public IHostingEnvironment _env;
        public readonly IOptions<AppConfig> _config;
        public WebocProcessor _webocProcessor;
        public IGTOORepository _gtooRepo;
        public IGatePassAuctionRepository _gatePassAuctionRepo;
        public ISIMORepository _simoreport;
        public ISIMRepository _simreport;
        public IRandDClerkRepository _randDClerkRepo;
        public IManualGatePassRepository _manualGatePassRep;
        public IDestuffedContainerRepository _destuffedContainerRepo;
        public IShippingAgentRepository _shippingAgentRepository;
        public IISOCodeRepository _iSOCodeRepository;
        public IStorageFreeDayRepository _storageFreeDayRepository;
        public IExaminationRequestRepository _examinationRequestRepository;
        

        public DeliveryController(IDeliveryOrderRepository deliveryOrderRepo,
            ICYContainerRepository cyContainerRepo,
            IOrderDetailRepository orderDetailRepo,
            IDOItemRepository doItemRepo,
            IVesselRepository vesselRepo,
            IVoyageRepository voyageRepo,
            IContainerRepository containerRepo,
            IContainerIndexRepository containerIndexRepo,
            IClearingAgentRepository clearingAgentRepo,
            IInvoiceRepository invoiceRepo,
            IAmountReceivedRepository amountReceivedRepo,
            ICRLORepository crloRepo,
            ICRLRepository crlRepo,
            IIGMORepository iGMORepository,
            IGDIORepository gdioRepo,
            IHostingEnvironment env,
            IOptions<AppConfig> config,
            WebocProcessor webocProcessor,
            IGTOORepository gtooRepo,
            IEmptyContainerGatePassRepository emptyContainerGatePassRepository,
            IGatePassAuctionRepository gatePassAuctionRepo,
            ISIMORepository simoreport,
            ISIMRepository simreport,
            IRandDClerkRepository randDClerkRepo,
            IManualGatePassRepository manualGatePassRep,
            IDestuffedContainerRepository destuffedContainerRepo,
            IShippingAgentRepository shippingAgentRepository,
            IISOCodeRepository iSOCodeRepository,
            IStorageFreeDayRepository storageFreeDayRepository,
            IExaminationRequestRepository examinationRequestRepository)
        {
            _doItemRepo = doItemRepo;
            _deliveryOrderRepo = deliveryOrderRepo;
            _orderDetailRepo = orderDetailRepo;
            _cyContainerRepo = cyContainerRepo;
            _gdioRepo = gdioRepo;
            _vesselRepo = vesselRepo;
            _voyageRepo = voyageRepo;
            _containerRepo = containerRepo;
            _containerIndexRepo = containerIndexRepo;
            _crloRepo = crloRepo;
            _crlRepo = crlRepo;
            _amountReceivedRepo = amountReceivedRepo;
            _invoiceRepo = invoiceRepo;
            _env = env;
            _webocProcessor = webocProcessor;
            _config = config;
            _clearingAgentRepo = clearingAgentRepo;
            _iGMORepository = iGMORepository;
            _emptyContainerGatePassRepository = emptyContainerGatePassRepository;
            _gtooRepo = gtooRepo;
            _gatePassAuctionRepo = gatePassAuctionRepo;
            _simoreport = simoreport;
            _simreport = simreport;
            _randDClerkRepo = randDClerkRepo;
            _manualGatePassRep = manualGatePassRep;
            _destuffedContainerRepo = destuffedContainerRepo;
            _shippingAgentRepository = shippingAgentRepository;
            _iSOCodeRepository = iSOCodeRepository;
            _storageFreeDayRepository = storageFreeDayRepository;
            _examinationRequestRepository = examinationRequestRepository;
        }

        public IActionResult Order()
        {
            return View();
        }

        public IActionResult OrderDetail()
        {

            ViewData["RandDClerks"] = _randDClerkRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.RandDClerkName, Value = v.RandDClerkId.ToString() });

            return View();
        }

        public IActionResult GetAllDeliveryOrders()

        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitOrderDetail(GatePass data, List<OrderDetailItemViewModel> items)
        {

            foreach (var doItem in items)
            {
                data.DOItems.Add(new DOItem
                {
                    TruckNumber = doItem.TruckNumber,
                    Quantity = doItem.NumberofPackages,
                    PackageType = doItem.PackageType
                });
            }

            data.GatePassDate = DateTime.Now;

            _orderDetailRepo.Add(data);

            return new OkObjectResult(new { OrderDetailId = data.GatePassId });
        }

        public IActionResult DeliveryOrder()
        {

            ViewData["ClearingAgents"] = _clearingAgentRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.Name, Value = v.ClearingAgentId.ToString() });
            return View();
        }

        public IActionResult SearchGatePass()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetGatePassbyDoNumber(long doNumber)
        {
            var data = _orderDetailRepo.GetList(x => x.DONumber == doNumber.ToString(), y => y.DOItems);
            return Json(data);
        }


        [HttpGet]
        public JsonResult GetAuctionGatePassbyDoNumber(long containerId, string type)
        {
            if (type == "CFS")
            {
                var data = _gatePassAuctionRepo.GetList(x => x.ContainerIndexId == containerId, y => y.DOItems);
                return Json(data);
            }
            else
            {
                var data = _gatePassAuctionRepo.GetList(x => x.CYContainerId == containerId, y => y.DOItems);
                return Json(data);
            }

        }

        [HttpPost]
        public IActionResult CreateDeliveryOrder(DeliveryOrder order, List<DOCodeItem> documentListGrid)
        {
            var deliverOrder = _deliveryOrderRepo.GetAll().LastOrDefault();
            order.DONumber = deliverOrder != null ? deliverOrder.DONumber + 1 : 1;

            var cycontainer = _cyContainerRepo.GetFirst(x => x.CYContainerId == order.CYContainerId);

            if (order.ContainerIndexId > 0)
            {
                var index = _containerIndexRepo.GetFirst(x => x.ContainerIndexId == order.ContainerIndexId);

                if (index.InvoiceLocked == true)
                {
                    return new JsonResult(new { error = true, message = "This container is currently Invoice Locked" });
                }

                if (index.IsHold == true)
                {
                    return new JsonResult(new { error = true, message = "This container is currently on hold" });
                }
                else
                {
                    var invoice = _invoiceRepo.GetFirst(x => x.ContainerIndexId == order.ContainerIndexId && x.IsCancelled != true);

                    if (invoice == null)
                    {
                        return new JsonResult(new { error = true, message = "First Generate Invoice" });
                    }

                    //var amountRec = _amountReceivedRepo.GetFirst(x => x.InvoiceId == invoice.InvoiceId && invoice.IsCancelled == false, i => i.Invoice);

                    //if (amountRec == null)
                    //{
                    //    return new JsonResult(new { error = true, message = "First Pay Amount" });
                    //}

                    var deliveryOrder = _deliveryOrderRepo.GetFirst(x => x.ContainerIndexId == order.ContainerIndexId);

                    if (deliveryOrder != null)
                    {
                        return new JsonResult(new { error = true, message = "This Container  is Already  Delivered" });
                    }

                }
            }

            if (order.CYContainerId > 0)
            {
                var conainer = _cyContainerRepo.GetFirst(x => x.CYContainerId == order.CYContainerId);

                if (conainer.InvoiceLocked == true)
                {
                    return new JsonResult(new { error = true, message = "This container is currently Invoice Locked" });
                }

                if (conainer.IsHold == true)
                {
                    return new JsonResult(new { error = true, message = "This container is currently on hold" });
                }
                else
                {
                    var invoice = _invoiceRepo.GetFirst(x => x.CYContainerId == order.CYContainerId && x.IsCancelled == false);

                    if (invoice == null)
                    {
                        return new JsonResult(new { error = true, message = "First Generate Invoice" });
                    }

                    //var amountRec = _amountReceivedRepo.GetFirst(x => x.Invoice.InvoiceNo == invoice.InvoiceNo && invoice.IsCancelled == false, i => i.Invoice);

                    //if (amountRec == null)
                    //{
                    //    return new JsonResult(new { error = true, message = "First Pay Amount" });
                    //}

                    var deliveryOrder = _deliveryOrderRepo.GetFirst(x => x.CYContainerId == order.CYContainerId);

                    if (deliveryOrder != null)
                    {
                        return new JsonResult(new { error = true, message = "This Container  is Already  Delivered" });
                    }

                    var crl = _crlRepo.GetFirst(c => c.ContainerNumber == conainer.ContainerNo);

                    if (crl == null)
                    {
                        return new JsonResult(new { error = true, message = "RF not yet received" });
                    }

                }
            }

            if (documentListGrid != null && documentListGrid.Count() > 0)
            {
                order.DOCodeItems = documentListGrid;
            }
            var adas = order;
            _deliveryOrderRepo.Add(order);

            return new OkObjectResult(new { error = false, DONumber = order.DONumber });
        }

        [HttpPost]
        public IActionResult CreateOrderDetail(GatePass model, long doNumber, string type, string containernumber, string unitType, List<DOItem> iposvalue)
        {

            try
            {
                //bool checkautogateoutmark = false;
                var deliveryOrder = _deliveryOrderRepo.GetFirst(d => d.DONumber == doNumber, g => g.GatePasses);

                if (deliveryOrder.ContainerIndexId != null)
                {

                    //var examinationrequest = _examinationRequestRepository.GetExaminationRequestByContainerindexId(deliveryOrder.ContainerIndexId ?? 0);

                    //if(examinationrequest != null)
                    //{
                    //    if(examinationrequest.DeliveryStatus == "Auction" || examinationrequest.DeliveryStatus == "ReExport" || examinationrequest.DeliveryStatus == "ApprovalWithCustom")
                    //    {
                    //        checkautogateoutmark = true;
                    //    }
                    //}



                    model.DONumber = deliveryOrder.DONumber.ToString();

                    var dovaliddate = deliveryOrder.ValidTo.Value.ToString("MM/dd/yyyy");

                    var datetime = DateTime.Now.ToString("MM/dd/yyyy");

                    if (Convert.ToDateTime( dovaliddate) < Convert.ToDateTime(datetime))
                    {
                        return new OkObjectResult(new { error = true, message = "Do Valid Date Is Expire" });

                    }

                    //var container = _containerIndexRepo.GetFirst(c => c.ContainerIndexId == deliveryOrder.ContainerIndexId, v => v.Voyage);

                    //var simo = _simoreport.GetAll().Where(x => x.BLNumber == container.BLNo && x.IndexNumber == container.IndexNo.ToString() && x.VIRNumber == container.Voyage.VIRNo).LastOrDefault();

                    //var container = _containerIndexRepo.GetContainerIndexById( deliveryOrder.ContainerIndexId ?? 0 );

                    //var simo = _simoreport.GetAll().Where(x => x.BLNumber == container.BLNo && x.IndexNumber == container.IndexNo.ToString() && x.VIRNumber == container.VirNo).LastOrDefault();

                    //if (simo != null && simo.Status == "HO")
                    //{
                    //    return new OkObjectResult(new { error = true, message = "can't  create due To simo hold" });

                    //}

                    var invoice = _invoiceRepo.GetAll().Where(x => x.ContainerIndexId == deliveryOrder.ContainerIndexId && x.IsCancelled == false).ToList();

                    if (invoice.Count() <=  0)
                    {
                        return new JsonResult(new { error = true, message = "First Create Invoice" });
                    }

                    //if (invoice.Count() > 0 )
                    //{
                    //    foreach (var item in invoice)
                    //    {
                    //        if(item.BillType == "Normal")
                    //        {
                    //            var amountRec = _amountReceivedRepo.GetFirst(x => x.InvoiceId == item.InvoiceId);

                    //            if (amountRec == null)
                    //            {
                    //                return new JsonResult(new { error = true, message = "First Pay Amount  of this   Invoice no " + item.InvoiceNo });
                    //            }

                    //        }
                    //        if (item.BillType == "Auction")
                    //        {
                    //            var amountRec = _amountReceivedRepo.GetFirst(x => x.InvoiceId == item.InvoiceId);

                    //            if (amountRec == null)
                    //            {
                    //                return new JsonResult(new { error = true, message = "First Pay Amount Invoice no " + item.InvoiceNo });
                    //            }

                    //        }
                    //    }
                     
                    //}


                }
                if (deliveryOrder.CYContainerId != null)
                {
                    model.DONumber = deliveryOrder.DONumber.ToString();


                    var dovaliddate = deliveryOrder.ValidTo.Value.ToString("MM/dd/yyyy");

                    var datetime = DateTime.Now.ToString("MM/dd/yyyy");

                    if (Convert.ToDateTime(dovaliddate) < Convert.ToDateTime(datetime))
                    {
                        return new OkObjectResult(new { error = true, message = "Do Valid Date Is Expire" });

                    }

                    //   var container = _cyContainerRepo.GetAll().Where(x => x.CYContainerId == deliveryOrder.CYContainerId).LastOrDefault();

                    //var container = _cyContainerRepo.GetContainerById(deliveryOrder.CYContainerId ?? 0);

                    //var sim = _simreport.GetAll().Where(x => x.ContainerNumber == container.ContainerNo && x.VIRNumber == container.VIRNo).LastOrDefault();

                    //if (sim != null && sim.Status == "HO")
                    //{
                    //    return new OkObjectResult(new { error = true, message = "can't  create due To sim hold" });

                    //}

                    var invoice = _invoiceRepo.GetAll().Where(x => x.CYContainerId == deliveryOrder.CYContainerId && x.IsCancelled == false).ToList();

                    if (invoice.Count() <= 0)
                    {
                        return new JsonResult(new { error = true, message = "First Create Invoice" });
                    }
 

                    //if (invoice.Count() > 0)
                    //{
                    //    foreach (var item in invoice)
                    //    {
                    //        if (item.BillType == "Normal")
                    //        {
                    //            var amountRec = _amountReceivedRepo.GetFirst(x => x.InvoiceId == item.InvoiceId);
                    //            if (amountRec == null)
                    //            {
                    //                return new JsonResult(new { error = true, message = "First Pay Amount of this  Invoice no " + item.InvoiceNo });
                    //            }
                    //        }
                    //        if (item.BillType == "Auction")
                    //        {
                    //            var amountRec = _amountReceivedRepo.GetFirst(x => x.InvoiceId == item.InvoiceId);

                    //            if (amountRec == null)
                    //            {
                    //                return new JsonResult(new { error = true, message = "First Pay Amount Invoice no " + item.InvoiceNo });
                    //            }

                    //        }
                    //    }
                     

                    //}
                     

                }

                if(deliveryOrder == null)
                {
                    return new OkObjectResult(new { error = true, message = "Do Not Avaible" });
                }

                if (deliveryOrder.GatePasses.Count == 0)
                {
                    model.TotalDelivered = model.Delivered;

                    model.TotalDelivered = Math.Round(model.TotalDelivered, 2);
                }
                    
                else
                {
                     
                    model.TotalDelivered   =  deliveryOrder.GatePasses.Sum(s => s.Delivered) + model.Delivered ;

                    model.TotalDelivered = Math.Round(model.TotalDelivered, 2);
                }
                 

                model.UnitType = unitType;

                model.GatePasscontainerNumber = containernumber;
                model.GatePassNumber = Convert.ToString(deliveryOrder.DONumber) + "-" + Convert.ToString(deliveryOrder.GatePasses.Count + 1);

                model.DeliveryOrderId = deliveryOrder.DeliveryOrderId;

                model.GatePassDate = DateTime.Now;

                var status = "";
                if (model.TotalDelivered < model.TotalPackages)
                {
                    status = "P";
                }
                else
                {
                    status = "F";
                }

                foreach (var item in iposvalue)
                {
                    model.DOItems.Add(new DOItem
                    {
                        TruckNumber = item.TruckNumber,
                        Quantity = item.Quantity,
                        PackageType = item.PackageType,
                        Status = status
                    });
                }

                if (model.TotalDelivered > model.TotalPackages)
                {
                    return new OkObjectResult(new { error = true, message = "can't  generate gatepass due To > Deliveries" });

                }

                _orderDetailRepo.Add(model);

                if (type == "CY")
                {
                    if (containernumber != null || containernumber != "")
                    {

                        var cycontainerindexres = _cyContainerRepo.GetContainerCYByIGMAndIndex(model.VirNumber, model.IndexNo);

                        if (cycontainerindexres != null)
                        {

                            if (cycontainerindexres.IsCSEmtyptyRecieved == true)
                            {
                                var cycontainerres = _cyContainerRepo.GetLastCSContainerByIGMIndexAndContainerNo(model.VirNumber, model.IndexNo, containernumber);

                                if (cycontainerres != null)
                                {
                                    cycontainerres.IsDelivered = true;
                                    cycontainerres.DeliveryDate = DateTime.Now;
                                    //if(checkautogateoutmark == true)
                                    //{
                                    //    cycontainerres.IsGateOut = true;
                                    //    cycontainerres.CYContainerGateOutDate = DateTime.Now;
                                    //}
                                    _cyContainerRepo.Update(cycontainerres);
                                }
                            }
                            else
                            {
                                var cycontainerres = _cyContainerRepo.GetLastContainerByIGMIndexAndContainerNo(model.VirNumber, model.IndexNo, containernumber);

                                if (cycontainerres != null)
                                {
                                    cycontainerres.IsDelivered = true;
                                    cycontainerres.DeliveryDate = DateTime.Now;

                                    //if (checkautogateoutmark == true)
                                    //{
                                    //    cycontainerres.IsGateOut = true;
                                    //    cycontainerres.CYContainerGateOutDate = DateTime.Now;
                                    //}

                                    _cyContainerRepo.Update(cycontainerres);
                                }
                            }
                        }
                            
                        //var cycontainerres = _cyContainerRepo.GetLastContainerByIGMIndexAndContainerNo( model.VirNumber , model.IndexNo , containernumber);

                        //if (cycontainerres != null)
                        //{
                        //    cycontainerres.IsDelivered = true;

                        //    cycontainerres.DeliveryDate = DateTime.Now;
                        //    _cyContainerRepo.Update(cycontainerres);
                        //}
                         
                    }
                }


                if (type == "CFS")
                { 
                    if(model.CargoType == "FCL" && unitType == "PACKAGES")
                    {
                        if (containernumber != null || containernumber != "")
                        {

                            var cfscontainerres = _containerIndexRepo.GetContainerIndexByIGMIndexAndContainer(model.VirNumber, containernumber,  model.IndexNo );

                            if (cfscontainerres != null)
                            {

                                //var destuffcontainerdetail = _containerIndexRepo.GetDestuffedIndexDetail(cfscontainerres.ContainerIndexId);

                                //var gatepass = _containerIndexRepo.GetGatePassInfo(model.VirNumber, containernumber, model.IndexNo);

                                //if (destuffcontainerdetail != null && gatepass.Count() > 0)
                                //{

                                //    var deliveredpakages = gatepass.Sum(x => x.Delivered);

                                //    if (destuffcontainerdetail.PackageQuantity == deliveredpakages)
                                //    {

                                        cfscontainerres.IsDelivered = true;

                                        cfscontainerres.DeliveryDate = DateTime.Now;

                                        _containerIndexRepo.Update(cfscontainerres);

                                    //}


                                //}

                                //////cfscontainerres.IsDelivered = true;

                                //////cfscontainerres.DeliveryDate = DateTime.Now;

                                //////_containerIndexRepo.Update(cfscontainerres);
                            }

                        }
                    }
                    else
                    {

                        var containerindx = _containerIndexRepo.GetIndexInfo(model.VirNumber, model.IndexNo).ToList();

                        var sts = model.DOItems.Where(x => x.Status == "F").LastOrDefault();

                        if (containerindx.Count() > 0 && sts != null)
                        {
                            containerindx.ForEach(x => x.IsDelivered = true);
                            containerindx.ForEach(x => x.DeliveryDate= DateTime.Now);

                            _containerIndexRepo.UpdateRange(containerindx);
                        }
                    }


                    // if(deliveryOrder.ContainerIndexId != null)
                    //{
                    //    deliveryOrder.BalancePackages = model.BalancePackages;
                    //    deliveryOrder.TotalPackages = model.TotalPackages;
                    //    deliveryOrder.Delivered = model.Delivered;
                    //    deliveryOrder.TotalDelivered = model.TotalDelivered;
                    //    _deliveryOrderRepo.Update(deliveryOrder);
                    //}
                     
                }
                 

                return new OkObjectResult(new { error = false, GatePassNumber = model.GatePassNumber });
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, GatePassNumber = e.InnerException.InnerException.Message });
            }
             
        }


        [HttpPost]
        public IActionResult CreateOrderDetailAuction(GatePassAuction model, string type, long containerId, string unitType, List<DOItem> iposvalue)
        {

            if (type == "CFS")
            {

                var container = _containerIndexRepo.GetFirst(c => c.ContainerIndexId == containerId, v => v.Voyage);

                if (container != null)
                {
                    var simo = _simoreport.GetAll().Where(x => x.BLNumber == container.BLNo && x.IndexNumber == container.IndexNo.ToString() && x.VIRNumber == container.Voyage.VIRNo).LastOrDefault();

                    if (simo != null && simo.Status == "HO")
                    {
                        return new OkObjectResult(new { error = true, message = "can't  generate gatepass due To simo hold" });
                    }
                    var deliveryorder = _deliveryOrderRepo.GetAll().Where(x => x.ContainerIndexId == containerId).LastOrDefault();
                    if (deliveryorder != null)
                    {

                        var invoice = _invoiceRepo.GetAll().Where(x => x.ContainerIndexId == containerId && x.IsCancelled == false).LastOrDefault();

                        if (invoice == null)
                        {
                            return new JsonResult(new { error = true, message = "First Create Invoice" });
                        }
                        if (invoice != null)
                        {
                            var amountRec = _amountReceivedRepo.GetFirst(x => x.InvoiceId == invoice.InvoiceId && invoice.IsCancelled == false, i => i.Invoice);

                            if (amountRec != null)
                            {

                                var gatePasses = _gatePassAuctionRepo.GetAll().Where(d => d.ContainerIndexId == containerId).ToList();

                                if (gatePasses != null)
                                {
                                    if (gatePasses.Count == 0)
                                        model.TotalDelivered = model.Delivered;
                                    else
                                        model.TotalDelivered = gatePasses.Sum(s => s.Delivered) + model.Delivered;
                                }

                                model.ContainerIndexId = containerId;
                                model.UnitType = unitType;

                                model.GatePassNumber = "BWT/AC-" + Convert.ToString(gatePasses.Count + 1);


                                model.GatePassDate = DateTime.Now;

                                foreach (var item in iposvalue)
                                {
                                    model.DOItems.Add(new DOItem
                                    {
                                        TruckNumber = item.TruckNumber,
                                        Quantity = item.Quantity,
                                        PackageType = item.PackageType
                                    });
                                }


                                _gatePassAuctionRepo.Add(model);


                                var res = _containerIndexRepo.GetFirst(x => x.ContainerIndexId == containerId);
                                if (res != null)
                                {
                                    res.IsGateOut = true;

                                    _containerIndexRepo.Update(res);
                                }

                            }

                            if (amountRec == null)
                            {
                                return new JsonResult(new { error = true, message = "First Pay Amount" });
                            }

                        }
                    }
                    else
                    {
                        return new JsonResult(new { error = true, message = "First Create Do" });
                    }
                }
                else
                {
                    return new JsonResult(new { error = true, message = "container not found" });
                }



            }
            else
            {
                var cycontainer = _cyContainerRepo.GetAll().Where(x => x.CYContainerId == containerId).LastOrDefault();

                if (cycontainer != null)
                {

                    var sim = _simreport.GetAll().Where(x => x.ContainerNumber == cycontainer.ContainerNo && x.VIRNumber == cycontainer.VIRNo).LastOrDefault();

                    if (sim != null && sim.Status == "HO")
                    {
                        return new OkObjectResult(new { error = true, message = "can't generate gatepass due to sim hold" });
                    }

                    var cydeliveryodrder = _deliveryOrderRepo.GetAll().Where(x => x.CYContainerId == containerId).LastOrDefault();
                    if (cydeliveryodrder != null)
                    {

                        var invoice = _invoiceRepo.GetAll().Where(x => x.CYContainerId == containerId && x.IsCancelled == false).LastOrDefault();

                        if (invoice == null)
                        {
                            return new JsonResult(new { error = true, message = "First Create Invoice" });
                        }
                        if (invoice != null)
                        {
                            //var amountRec = _amountReceivedRepo.GetFirst(x => x.InvoiceId == invoice.InvoiceId && invoice.IsCancelled == false, i => i.Invoice);

                            var amountRec = _amountReceivedRepo.GetFirst(x => x.Invoice != null && x.Invoice.InvoiceNo == invoice.InvoiceNo && invoice.IsCancelled == false, i => i.Invoice);

                            if (amountRec == null)
                            {
                                return new JsonResult(new { error = true, message = "First Pay Amount" });
                            }
                            if (amountRec != null)
                            {

                                var gatePasses = _gatePassAuctionRepo.GetAll().Where(d => d.CYContainerId == containerId).ToList();

                                if (gatePasses != null)
                                {
                                    if (gatePasses.Count == 0)
                                        model.TotalDelivered = model.Delivered;
                                    else
                                        model.TotalDelivered = gatePasses.Sum(s => s.Delivered) + model.Delivered;
                                }

                                model.UnitType = unitType;
                                model.CYContainerId = containerId;
                                model.GatePassNumber = Convert.ToString(gatePasses.Count + 1);


                                model.GatePassDate = DateTime.Now;

                                foreach (var item in iposvalue)
                                {
                                    model.DOItems.Add(new DOItem
                                    {
                                        TruckNumber = item.TruckNumber,
                                        Quantity = item.Quantity,
                                        PackageType = item.PackageType
                                    });
                                }

                                var rescy = _cyContainerRepo.GetFirst(x => x.CYContainerId == containerId);
                                if (rescy != null)
                                {
                                    rescy.IsGateOut = true;

                                    _cyContainerRepo.Update(rescy);
                                }


                                _gatePassAuctionRepo.Add(model);

                            }

                        }
                    }
                    else
                    {
                        return new JsonResult(new { error = true, message = "First Create Do" });
                    }

                }
                else
                {
                    return new JsonResult(new { error = true, message = "container not found" });
                }


            }
            return new OkObjectResult(new { GatePassNumber = model.GatePassNumber });
        }

        public IActionResult SendGateOut(string DONumber, string Type, List<DOItem> doItems)
        {
            if (Type == "CY")
                return new OkObjectResult(new { error = true, message = "This option is available only for CFS" });

            try
            {

                var deliveryOrder = _deliveryOrderRepo.GetFirst(d => d.DONumber == Convert.ToInt64(DONumber));

                var index = _containerIndexRepo.GetFirst(c => c.ContainerIndexId == deliveryOrder.ContainerIndexId, v => v.Voyage);

                var simo = _simoreport.GetAll().Where(x => x.VIRNumber == index.Voyage.VIRNo && x.BLNumber == index.BLNo && x.IndexNumber == index.IndexNo.ToString()).LastOrDefault();

                if (simo != null && simo.Status == "HO")
                {
                    return new OkObjectResult(new { error = true, message = "can't send message due to simo hold" });
                }


                var containers = _containerIndexRepo.GetList(s => s.VoyageId == index.VoyageId && s.IndexNo == index.IndexNo);

                foreach (var container in containers)
                {
                    container.IsGateOut = true;
                    _containerIndexRepo.Update(container);
                }

                var datetime = DateTime.Now;

                List<GTOO> gTOOs = new List<GTOO>();

                //var gdio = _gdioRepo.GetFirst(d => d.VIRNumber == index.Voyage.VIRNo && d.IndexNumber == index.IndexNo);

                //if (gdio == null)
                //    return new OkObjectResult(new { error = true, message = "GDIO not yet received!" });

                var crlo = _crloRepo.GetAll().Where(x => x.VIRNumber == index.Voyage.VIRNo && x.BLNumber.ToUpper().Trim() == index.BLNo.ToUpper().Trim() && x.IndexNumber == index.IndexNo).LastOrDefault();

                if (crlo == null)
                {
                    return new OkObjectResult(new { error = true, message = "clearance not yet received!" });
                }

                else
                {



                    for (int i = 0; i < doItems.Count; i++)
                    {
                        var gatePassItem = _doItemRepo.GetFirst(d => d.GatePassId == doItems[i].GatePassId && d.TruckNumber == doItems[i].TruckNumber);
                        if (gatePassItem != null)
                        {
                            gatePassItem.IsGateOut = true;
                            _doItemRepo.Update(gatePassItem);
                        }

                        var doItem = _doItemRepo.Find(doItems[i].DOItemId);
                        if (doItem != null)
                        {
                            doItem.Status = doItems[i].Status;
                            doItem.PackageType = doItems[i].PackageType;
                            doItem.NoOfPackages = doItems[i].NoOfPackages;

                            _doItemRepo.Update(doItem);
                        }

                        var gtoo = new GTOO
                        {
                            Category = "I",
                            VIRNumber = index.Voyage.VIRNo,
                            //BLNumber = gdio != null ? gdio.BLNumber : index.BLNo,
                            ContainerNumber = index.ContainerNo,
                            BLNumber = index.BLNo,
                            NumberofPackages = doItems[i].NoOfPackages,
                            PackageType = doItems[i].PackageType + "=" + doItems[i].Quantity,
                            Status = doItems[i].Status,
                            GateOutTime = datetime,
                            Truck = doItems[i].TruckNumber,
                            Weight = doItems[i].Quantity,
                            MessageFrom = "BWP",
                            MessageTo = "WEBOC",
                            FileName = $"GTOO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                        };

                        gTOOs.Add(gtoo);
                    }




                    if ((crlo != null) && (crlo.Status == "RF" || crlo.Status == "IB"))
                    {

                        _gtooRepo.AddRange(gTOOs);

                        return new OkObjectResult(new { error = false, message = "Gateout Message Successfully Sent!" });

                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "Remove From Along Side But Edi Not Sent Due to clearance Not Found" });

                    }
                }




            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = "Error Creating EDI Message!" });
            }

        }
        public IActionResult SearchDeliveryOrder()
        {
            return View();
        }



        public IActionResult EmptyContainerGatePass()
        {
            return View();
        }


        public List<EmptyContainerGatePassViewModel> GetEmptyContainerGatePasses()
        {
            var data = _emptyContainerGatePassRepository.GetUnEmptyContainerGatePass().ToList();
            if (data != null)
            {
                return data;
            }

            return null;
        }

        [HttpPost]
        public IActionResult AddEmptyGatePass(List<EmptyContainerGatePassViewModel> model)
        {
            var datetime = DateTime.Now;
            List<EmptyContainerGatePass> emptyContainerGatePasses = new List<EmptyContainerGatePass>();
            foreach (var data in model)
            {


                var emptyContainerGatePass = new EmptyContainerGatePass
                {
                    ContainerCondition = data.ContainerCondition,
                    ShiftedYard = data.ShiftedYard,
                    ContainerIndexId = data.ContainerIndexId,
                    CreatedDate = datetime

                };

                emptyContainerGatePasses.Add(emptyContainerGatePass);
            }

            _emptyContainerGatePassRepository.AddRange(emptyContainerGatePasses);

            return Ok();
        }


        public IActionResult DeleteGatePass(long GatePassId)
        {
            try
            {
 

                var gatePass = _orderDetailRepo.GetAll().Where(x => x.GatePassId == GatePassId).FirstOrDefault();


                if (gatePass != null)
                {

                    var doitm = _doItemRepo.GetAll().Where(x => x.GatePassId == gatePass.GatePassId).LastOrDefault();

                    if (doitm != null && doitm.IsGateOut == true)
                    {

                        return new OkObjectResult(new { error = true, message = "Error Item Is Delivered mark" });

                    }

                    else
                    {


                        var dos = _orderDetailRepo.GetAll().Where(x => x.DONumber == gatePass.DONumber).ToList();

                        foreach (var item in dos)
                        {
                            var doitem = _doItemRepo.GetAll().Where(x => x.GatePassId == item.GatePassId).FirstOrDefault();

                            if (doitem != null && doitem.Status == "F" && doitm.Status != "F")
                            {
                                return new OkObjectResult(new { error = true, message = "Please Delete Full Gate Pass Then You can delete Partialy" });

                            }
                        } 

                        if(gatePass.GatePassType == "CFS" && gatePass.CargoType == "FCL" && gatePass.UnitType == "PACKAGES")
                        {
                            var cfscontainerres = _containerIndexRepo.GetContainerIndexByIGMIndexAndContainer(gatePass.VirNumber, gatePass.GatePasscontainerNumber, gatePass.IndexNo);

                            if (cfscontainerres != null)
                            {
                                cfscontainerres.IsDelivered = false;

                                cfscontainerres.DeliveryDate = null;

                                _containerIndexRepo.Update(cfscontainerres);
                            }
                        }
                        if (gatePass.GatePassType == "CFS" && gatePass.UnitType == "WEIGHT")
                        {
                            if (doitm.Status == "F")
                            {
                                var containerindx = _containerIndexRepo.GetIndexInfo(gatePass.VirNumber, gatePass.IndexNo).ToList();

                                if (containerindx.Count() > 0)
                                {
                                    containerindx.ForEach(x => x.IsDelivered = false);
                                    containerindx.ForEach(x => x.DeliveryDate = null);

                                    _containerIndexRepo.UpdateRange(containerindx);
                                }
                            }
                        }

                        var checkstaus = false;

                        if (gatePass.GatePassType == "CY" && checkstaus == false)
                        {
                            var cycontainerres = _cyContainerRepo.GetLastContainerByIGMIndexAndContainerNo(gatePass.VirNumber, gatePass.IndexNo, gatePass.GatePasscontainerNumber);

                            if (cycontainerres != null)
                            {
                                cycontainerres.IsDelivered = false;
                                cycontainerres.DeliveryDate = null;

                                _cyContainerRepo.Update(cycontainerres);
                                checkstaus = true;
                            }
                        }

                        if (gatePass.GatePassType == "CY" && checkstaus == false)
                        {
                            var cycontainerres = _cyContainerRepo.GetLastCSContainerByIGMIndexAndContainerNo(gatePass.VirNumber, gatePass.IndexNo, gatePass.GatePasscontainerNumber);

                            if (cycontainerres != null && cycontainerres.IsCSEmtyptyRecieved == true)
                            {
                                cycontainerres.IsDelivered = false;
                                cycontainerres.DeliveryDate = null;
                                _cyContainerRepo.Update(cycontainerres);
                                checkstaus = true;
                            }
                        }

                        _doItemRepo.Delete(doitm);
                        _orderDetailRepo.Delete(gatePass);

                        return new OkObjectResult(new { error = false, message = "Deleted" });

                    }

                    return new OkObjectResult(new { error = true, message = "Gate Pass Item Not Found" });

                }

                return new OkObjectResult(new { error = true, message = "Gate Pass  Not Found" });

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }




            //var doitem = _doItemRepo.GetAll().Where(x => x.DOItemId == DOItemId).FirstOrDefault();
            //if (doitem != null && doitem.IsGateOut == true)
            //{
            //    return new OkObjectResult(new { error = false, message = "Error Item Is Out" });

            //}
            //else
            //{
            //    _doItemRepo.Delete(doitem);
            //}

        }


        public IActionResult DeleteAuctionGatePass(long GatePassAuctionId)
        {
            var gatePass = _gatePassAuctionRepo.GetAll().Where(x => x.GatePassAuctionId == GatePassAuctionId).FirstOrDefault();


            if (gatePass != null)
            {
                var doitem = _doItemRepo.GetAll().Where(x => x.GatePassAuctionId == gatePass.GatePassAuctionId).ToList();
                if (doitem != null)
                {
                    _doItemRepo.DeleteRange(doitem);
                }
                _gatePassAuctionRepo.Delete(gatePass);
            }
            return Ok();
        }

        public IActionResult AuctionGatePassView()
        {
            return View();
        }


        public IActionResult ManualGatePassView()
        {
            ViewData["RandDClerks"] = _randDClerkRepo.GetAll()
                   .Select(v => new SelectListItem { Text = v.RandDClerkName, Value = v.RandDClerkId.ToString() });
            return View();
        }

        public IActionResult SaveManualGatePass(ManualGatePass data, string igm, long indexNo)
        {
            data.GatePassDate = DateTime.Now;

            if (data.Type == "CFS")
            {

                var res = new ManualGatePass
                {
                    BalancePackages = data.BalancePackages,
                    GatePassDate = data.GatePassDate,
                    ManualGatePassNumber = GetLastManualGatePassNumber(),
                    ContainerindexId = data.ContainerindexId,
                    CYContainerId = null,
                    Delivered = data.Delivered,
                    RandDClerkId = data.RandDClerkId,
                    Remarks = data.Remarks,
                    Shift = data.Shift,
                    TotalDelivered = data.TotalDelivered,
                    TotalPackages = data.TotalPackages,
                    TruckNumber = data.TruckNumber,
                    Type = data.Type
                };

                _manualGatePassRep.Add(res);

            }

            if (data.Type == "CY")
            {
                data.ContainerindexId = null;

                var cyContainer = _cyContainerRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == indexNo);

                if (cyContainer != null)
                {
                    data.CYContainerId = cyContainer.CYContainerId;

                    var res = new ManualGatePass
                    {

                        BalancePackages = data.BalancePackages,
                        GatePassDate = data.GatePassDate,
                        ManualGatePassNumber = GetLastManualGatePassNumber(),
                        ContainerindexId = null,
                        CYContainerId = data.CYContainerId,
                        Delivered = data.Delivered,
                        RandDClerkId = data.RandDClerkId,
                        Remarks = data.Remarks,
                        Shift = data.Shift,
                        TotalDelivered = data.TotalDelivered,
                        TotalPackages = data.TotalPackages,
                        TruckNumber = data.TruckNumber,
                        Type = data.Type

                    };

                    _manualGatePassRep.Add(res);

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "container index not found" });

                }
            }


            return new OkObjectResult(new { error = false, message = "created" });

        }


        public long GetLastManualGatePassNumber()
        {
            var manualgpno = _manualGatePassRep.GetAll();
            var mgpno = manualgpno.LastOrDefault();
            var ManualGatePassNumber = mgpno != null ? mgpno.ManualGatePassNumber + 1 : 1;

            return ManualGatePassNumber ?? 1;

        }


        public IActionResult DeleteManualGatePass(long GatePassId)
        {

            var gatePass = _manualGatePassRep.GetAll().Where(x => x.ManualGatePassId == GatePassId).FirstOrDefault();


            if (gatePass != null)
            { 

                _manualGatePassRep.Delete(gatePass);

                    return new OkObjectResult(new { error = false, message = "Deleted" }); 

            }

            return new OkObjectResult(new { error = true, message = "Gate Pass  Not Found" });
 

        }


        public IActionResult UpdateDoView()
        {


            return View();
        }


        public IActionResult UpdateDo(long dono )
        { 
            var deliveryorder = _deliveryOrderRepo.GetAll().Where(x => x.DONumber == dono).LastOrDefault();

            if(deliveryorder != null)
            {
                if(deliveryorder.ContainerIndexId != null)
                {
                    var invoice = _invoiceRepo.GetAll().Where(x => x.ContainerIndexId == deliveryorder.ContainerIndexId).LastOrDefault();

                    if (invoice != null)
                    {
                        var containerindex = _containerIndexRepo.GetAll().Where(x => x.ContainerIndexId == invoice.ContainerIndexId).LastOrDefault();

                        var destuff = _destuffedContainerRepo.GetAll(x=> x.TellySheet).Where(x => x.ContainerIndexId == invoice.ContainerIndexId).LastOrDefault();



                        var noofdays = GetStorageTotal(containerindex.ContainerIndexId, invoice.ClearingAgentId, DateTime.Now, containerindex.CFSContainerGateInDate ?? DateTime.Now, destuff.TellySheet != null ? destuff.TellySheet.DestuffDate ??   DateTime.Now :  DateTime.Now , containerindex.CargoType);

                        if(noofdays > 0)
                        {
                            deliveryorder.Date = DateTime.Now;
                            _deliveryOrderRepo.Update(deliveryorder);
                        }
                        else
                        {
                            return new OkObjectResult(new { error = true, message = "do not update due to storage days  > 0" });

                        }
                    }

                }

                if (deliveryorder.CYContainerId != null)
                {
                    var invoice = _invoiceRepo.GetAll().Where(x => x.CYContainerId == deliveryorder.CYContainerId).LastOrDefault();

                    if (invoice != null)
                    {
                        var cycontainer = _cyContainerRepo.GetAll().Where(x => x.CYContainerId == invoice.CYContainerId).LastOrDefault();

                        var indexcargotype = "CY";


                        var noofdays = GetStorageTotalCY(cycontainer.VIRNo , cycontainer.IndexNo ?? 0, DateTime.Now, cycontainer.CYContainerGateInDate ?? DateTime.Now , invoice.ClearingAgentId ?? 0, indexcargotype);

                        if (noofdays > 0)
                        {
                            deliveryorder.Date = DateTime.Now;
                            _deliveryOrderRepo.Update(deliveryorder);
                        }
                        else
                        {
                            return new OkObjectResult(new { error = true, message = "do not update due to storage days  > 0" });

                        }
                    }

                }


            }
            else
            {
                return new OkObjectResult(new { error = true, message = "DO  Not Found" });

            }

            return new OkObjectResult(new { error = true, message = "please try again" });

        }



        public long GetStorageTotal(long ContainerIndexId, long? clearingAgentId, DateTime BillDate, DateTime gateInDate, DateTime destuffdate , string indexcargotype)
        {
            var storageDays = 0;

            //var noOfDays = 0;

            //var container = _containerIndexRepo.GetAll().Where(x=> x.ContainerIndexId == ContainerIndexId).LastOrDefault();
             
            //var shippingAget = _shippingAgentRepository.GetAll().Where(x => x.ShippingAgentId == container.ShippingAgentId).LastOrDefault();
             
            //var isocode = _iSOCodeRepository.GetAll().Where(x => x.Code == container.ISOCode).FirstOrDefault();
              
            //var nooffreedays = 0;

            //var firstFreedays = _storageFreeDayRepository.GetAll().Where(x => (x.GoodsHeadId == container.GoodsHeadId || x.GoodsHeadId == null)
            //                                                             && (x.ShipperId == container.ShipperId || x.ShipperId == null)
            //                                                             && (x.ClearingAgentId == clearingAgentId || x.ClearingAgentId == null)
            //                                                             && (x.ConsigneId == container.ConsigneId || x.ConsigneId == null)
            //                                                             && (x.PortAndTerminalId == container.PortAndTerminalId || x.PortAndTerminalId == null)
            //                                                             && (x.ShippingAgentId == container.ShippingAgentId || x.ShippingAgentId == null)
            //                                                             && (x.ContainerType == isocode.Descrition || x.ContainerType == null)
            //                                                             && (x.IndexCargoType == indexcargotype || x.IndexCargoType == null)

            //                                                             ).FirstOrDefault();
             
            //if (firstFreedays != null)
            //{
            //    nooffreedays = Convert.ToInt32(firstFreedays.StorageFreeDays);
            //} 

            //else
            //{
            //    nooffreedays = 5;
            //}

            //nooffreedays = (nooffreedays - 1);
             
            //if (shippingAget.BillDateType == "VesselArrival")
            //{
            //     noOfDays = Convert.ToInt32((BillDate.Date - container.Voyage.BerthOn.Value.AddDays((nooffreedays)).Date).Days);                 
            //}
            //if (shippingAget.BillDateType == "Destuffed")
            //{ 
            //    noOfDays = Convert.ToInt32((BillDate.Date - destuffdate.AddDays((nooffreedays)).Date).Days);                 
            //}
            //if (shippingAget.BillDateType == "GateIn")
            //{                 
            //    noOfDays = Convert.ToInt32((BillDate.Date - gateInDate.AddDays((nooffreedays)).Date).Days);                 
            //}
             
            //storageDays = noOfDays;
             
             
            return    storageDays;
        }




        public long GetStorageTotalCY(string igm, int indexNo, DateTime BillDate, DateTime gateInDate, long clearingAgentId , string indexcargotype)
        { 
            var storageDays = 0;

            //var noOfDays = 0;

            //var container = _cyContainerRepo.GetAll().Where(x => x.VIRNo == igm && x.IndexNo == indexNo).FirstOrDefault();
             
            //var shippingAget = _shippingAgentRepository.GetAll().Where(x=> x.ShippingAgentId == container.ShippingAgentId).FirstOrDefault();
             
            //var isocode = _iSOCodeRepository.GetAll().Where(x => x.Code == container.ISOCode).FirstOrDefault();
             
            //var nooffreedays = 0;

            //var firstFreedays = _storageFreeDayRepository.GetAll().Where(x => (x.GoodsHeadId == container.GoodsHeadId || x.GoodsHeadId == null)
            //                                                             && (x.ShipperId == container.ShipperId || x.ShipperId == null)
            //                                                             && (x.ClearingAgentId == clearingAgentId || x.ClearingAgentId == null)
            //                                                             && (x.ConsigneId == container.ConsigneId || x.ConsigneId == null)
            //                                                             && (x.PortAndTerminalId == container.PortAndTerminalId || x.PortAndTerminalId == null)
            //                                                             && (x.ShippingAgentId == container.ShippingAgentId || x.ShippingAgentId == null)
            //                                                             && (x.ContainerType == isocode.Descrition || x.ContainerType == null)
            //                                                             && (x.IndexCargoType == indexcargotype || x.IndexCargoType == null)
            //                                                             ).FirstOrDefault();

            //if (firstFreedays != null)
            //{
            //    nooffreedays = Convert.ToInt32(firstFreedays.StorageFreeDays);
            //}
            //else
            //{
            //    nooffreedays = 5;
            //}

            //nooffreedays = (nooffreedays - 1);

            ////  var noOfDays = Convert.ToInt32((BillDate - gatein.GateInDateTime.Value.AddDays(3)).TotalDays);

            //if (shippingAget.BillDateType == "VesselArrival")
            //{

            //    //noOfDays = Convert.ToInt32((BillDate - destuff.TellySheet.DestuffDate.Value.AddDays(3)).TotalDays);
            //    noOfDays = Convert.ToInt32((BillDate.Date - container.BerthOn.Value.AddDays((nooffreedays)).Date).Days);
            //}
            //if (shippingAget.BillDateType == "GateIn")
            //{

            //    //noOfDays = Convert.ToInt32((BillDate - container.CFSContainerGateInDate.Value.AddDays(3)).TotalDays);

            //    noOfDays = Convert.ToInt32((BillDate.Date - gateInDate.AddDays((nooffreedays)).Date).Days);

            //}


            //storageDays = noOfDays;

             
            return storageDays;
        }


    }
}