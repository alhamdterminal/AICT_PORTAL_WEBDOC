using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdocTerminal.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class GateInOutController : ParentController
    {

        private IContainerRepository _containerRepo;
        private IShippingAgentRepository _shippingAgentRepo;
        private IGIIORepository _giioRepo;
        private IGateInRepository _gateInRepo;
        private IGateOutRepository _gateOutRepo;
        private IGTOORepository _gtooRepo;
        private IGTTORepository _gttoRepo;
        private IGTORepository _gtoRepo;
        private IVesselRepository _vesselRepo;
        private IVoyageRepository _voyageRepository;
        private IContainerIndexRepository _containerIndexRepo;
        private IHostingEnvironment env;
        private readonly IOptions<AppConfig> _config;
        private WebocProcessor _webocProcessor;
        private IPGOORepository _pgooProcessor;
        private ICYContainerRepository _cyContainerRepo;
        private IEmptyGateOutContainerRepository _emptyGateOutContainerRepositroy;
        private ISVMORepository _svmoRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private IDeliveryOrderRepository _deliveryOrderRepository;
        private ISIMORepository _simoreport;
        private ISIMRepository _simreport;
        private IReturnToYardRepository _returnToYardRepo;
        private ICRLORepository _crloRepo;
        private IDOItemRepository _dOItemRepo;
        private IEmptyContainerReceivedRepository _emptyContainerReceivedRepository;
        private IShippingLineRepository _shippingLineRepository;
        private IClearingAgentRepository _clearingAgentRepository;
        private IISOCodeHeadRepository _ISOCodeHeadRepository;
        private ITransporterRepository _transporterRepository;
        private IShipperRepository _shipperRepository;
        private IOffHireLocationRepository _offHireLocationRepository;
        private IRandDClerkRepository _randDClerkRepository;
        private IEmptyContainerDeliveredRepository _emptyContainerDeliveredRepository;
        private IPGateInOutRepository  _pGateInOutRepository;
        private ICRLRepository _crlRepository;
        private IHoldRepository  _holdRepository;
        private ICCMORepository  _cCMORepository;
        private ICSEmptyContainerReceiveRepository _cSEmptyContainerReceiveRepository;
        private IGDCRRepository _gDCRRepository;
         
        public GateInOutController(IContainerRepository containerRepo,
            IShippingAgentRepository shippingAgentRepo,
            IGateInRepository gateInRepo,
            IGateOutRepository gateOutRepo,
            IVesselRepository vesselRepo,
            IVoyageRepository voyageRepository,
            IGTOORepository gtooRepo,
            IGTTORepository gttoRepo,
            IGTORepository gtoRepo,
            IContainerIndexRepository containerIndexRepo,
            WebocProcessor webocProcessor,
            IHostingEnvironment _env,
            IOptions<AppConfig> config,
            IPGOORepository pgooProcessor,
            IGIIORepository giioRepo,
            ICYContainerRepository cyContainerRepo,
            IEmptyGateOutContainerRepository emptyGateOutContainerRepositroy,
            ISVMORepository svmoRepository,
            IOrderDetailRepository orderDetailRepository,
            IDeliveryOrderRepository deliveryOrderRepository,
            ISIMORepository simoreport,
            ISIMRepository simreport,
            IReturnToYardRepository returnToYardRepo,
            ICRLORepository crloRepo,
            IDOItemRepository dOItemRepo ,
            IEmptyContainerReceivedRepository emptyContainerReceivedRepository,
            IShippingLineRepository shippingLineRepository,
            IClearingAgentRepository clearingAgentRepository ,
            IISOCodeHeadRepository ISOCodeHeadRepository,
            ITransporterRepository transporterRepository,
            IShipperRepository shipperRepository,
            IOffHireLocationRepository offHireLocationRepository,
            IRandDClerkRepository randDClerkRepository,
            IEmptyContainerDeliveredRepository emptyContainerDeliveredRepository,
            IPGateInOutRepository pGateInOutRepository,
            ICRLRepository crlRepository,
            IHoldRepository holdRepository,
            ICCMORepository cCMORepository,
            ICSEmptyContainerReceiveRepository cSEmptyContainerReceiveRepository,
            IGDCRRepository gDCRRepository)
        {
            _containerRepo = containerRepo;
            _shippingAgentRepo = shippingAgentRepo;
            _giioRepo = giioRepo;
            _gateInRepo = gateInRepo;
            _gateOutRepo = gateOutRepo;
            _vesselRepo = vesselRepo;
            _voyageRepository = voyageRepository;
            _gtooRepo = gtooRepo;
            _gttoRepo = gttoRepo;
            _gtoRepo = gtoRepo;
            _containerIndexRepo = containerIndexRepo;
            _webocProcessor = webocProcessor;
            env = _env;
            _config = config;
            _pgooProcessor = pgooProcessor;
            _cyContainerRepo = cyContainerRepo;
            _emptyGateOutContainerRepositroy = emptyGateOutContainerRepositroy;
            _svmoRepository = svmoRepository;
            _orderDetailRepository = orderDetailRepository;
            _deliveryOrderRepository = deliveryOrderRepository;
            _simoreport = simoreport;
            _simreport = simreport;
            _returnToYardRepo = returnToYardRepo;
            _crloRepo = crloRepo;
            _dOItemRepo = dOItemRepo;
            _emptyContainerReceivedRepository = emptyContainerReceivedRepository;
            _shippingLineRepository = shippingLineRepository;
            _clearingAgentRepository = clearingAgentRepository;
            _ISOCodeHeadRepository = ISOCodeHeadRepository;
            _transporterRepository = transporterRepository;
            _shipperRepository = shipperRepository;
            _offHireLocationRepository = offHireLocationRepository;
            _randDClerkRepository = randDClerkRepository;
            _emptyContainerDeliveredRepository = emptyContainerDeliveredRepository;
            _pGateInOutRepository = pGateInOutRepository;
            _crlRepository = crlRepository;
            _holdRepository = holdRepository;
            _cCMORepository = cCMORepository;
            _cSEmptyContainerReceiveRepository = cSEmptyContainerReceiveRepository;
            _gDCRRepository = gDCRRepository;
        }

        public IActionResult GateIn()
        {
            return View();
        }
        public IActionResult GateOut()
        {

            ViewData["Vessels"] = _vesselRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.VesselName, Value = v.VesselName });

            ViewData["ShippingAgent"] = _shippingAgentRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });
            return View();
        }


        public IActionResult GateOutCYContainers()
        {
            return View();
        }

        public IActionResult GateOutCFSContainers()
        {
            return View();
        }

        public IActionResult GateInCYContainers()
        {
            return View();
        }

        public IActionResult GateInCFSContainers()
        {
            return View();
        }

        public IActionResult GateInTPContainers()
        {
            return View();
        }

        public IActionResult SaveGateInContainers(List<GateInViewModel> containers)
        {
            try
            {


                bool hasMatch = _gateInRepo.GetAll().Any(x => containers.Any(y => y.ContainerNo == x.ContainerNo && y.VIRNo == x.VIRNo));

                if (!hasMatch)
                {

                    var datetime = DateTime.Now;

                    List<GateIn> gateins = new List<GateIn>();
                    List<GIIO> giios = new List<GIIO>();
                    List<ContainerIndex> containerIndes = new List<ContainerIndex>();
                    List<CYContainer> cycontainers = new List<CYContainer>();

                    foreach (var container in containers)
                    {

                        if(container.ContainerTypeStatus == "WeBOC")
                        {
                            var giio = new GIIO
                            {
                                ContainerNumber = container.ContainerNo,
                                GateInTime = container.GateInDateTime,
                                MessageFrom = "AIT",
                                MessageTo = "WeBOC",
                                PCCSSSealNumber = container.PACSSSealNo,
                                VehicleNumber = container.VehicleNo,
                                Weight = container.FoundWeight,
                                VIRNumber = container.VIRNo,
                                Performed = DateTime.Now,
                                FileName = $"GIIO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                            };

                            giios.Add(giio);
                        }
                      


                        var gatein = new GateIn
                        {
                            CarrierId = container.CarrierId,
                            ShippingAgentId = container.ShippingAgentId,
                            CarrierName = container.CarrierName,
                            ContainerNo = container.ContainerNo,
                            ContainerSize = container.ContainerSize,
                            GateInDateTime = container.GateInDateTime,
                            ISOCode = container.ISOCode,
                            MenifestedSealNo = container.MenifestedSealNo,
                            PACSSSealNo = container.PACSSSealNo,
                            VehicleNo = container.VehicleNo,
                            FoundSealNumber = container.PACSSSealNo,
                            VIRNo = container.VIRNo,
                            Weight = container.FoundWeight,
                            DPManifestWeight = container.DPManifestWeight,
                            DPTareweight = container.DPTareweight
                        };

                        gateins.Add(gatein);

                        if(container.Status == "CFS"  )
                        {
                         
                            
                            //var containerIndexes = _containerIndexRepo.GetAll().Where(x => x.ContainerNo == container.ContainerNo && x.VoyageId == container.VoyageId).ToList();
                            var containerIndexes = _containerIndexRepo.GetContainerIndexByIGMAndContainerNo( container.VIRNo , container.ContainerNo).ToList();
                            
                            
                            if (containerIndexes.Count() > 0)
                            {
                                foreach (var cntr in containerIndexes)
                                {
                                    if (cntr != null)
                                    {
                                        cntr.IsGateIn = true;                                         
                                        cntr.InvoiceLocked = true;
                                        cntr.ShippingAgentId = container.ShippingAgentId ?? null;
                                        cntr.TransporterId = container.TransporterId ?? null;
                                        cntr.Size = container.ContainerSize != null ? Convert.ToInt32(container.ContainerSize) : 0;
                                        cntr.CFSContainerGateInDate = container.GateInDateTime;
                                        cntr.ContainerLocation = container.ContainerLocation;
                                        containerIndes.Add(cntr);

                                    }
                                }

                            }

                        }
                   

                        if (container.Status == "CY")
                        {
                           
                            // var cntrs = _cyContainerRepo.GetAll().Where(x => x.ContainerNo == container.ContainerNo && x.VIRNo == container.VIRNo).ToList();
                            
                            var cntrs = _cyContainerRepo.GetContainerIndexByIGMAndContainerNo(   container.VIRNo , container.ContainerNo).ToList();
                            
                            if (cntrs.Count() > 0)
                            {

                                foreach (var item in cntrs)
                                {
                                    if(item != null)
                                    {
                                        item.IsGateIn = true;
                                        item.InvoiceLocked = true;
                                        item.IsAuction = false;
                                        item.ShippingAgentId = container.ShippingAgentId ?? null;
                                        item.TransporterId = container.TransporterId ?? null;
                                        item.Size = container.ContainerSize != null ? Convert.ToInt32(container.ContainerSize) : 0;
                                        item.CYContainerGateInDate = container.GateInDateTime;
                                        item.ContainerLocation = container.ContainerLocation;
                                        cycontainers.Add(item);

                                    }
                                   

                                }
                               

                            }

                        }



                    }

                     
                    _giioRepo.AddRange(giios);                   
                     
                    _gateInRepo.AddRange(gateins);

                    _containerIndexRepo.UpdateRange(containerIndes);
                    
                    _cyContainerRepo.UpdateRange(cycontainers);



                    return new OkObjectResult(new { error = false, message = "Save" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "The list contains a container which is already gated in. Please remove the container and try again." });
                }



            }
            catch (Exception e)
            {
                 
                    return new OkObjectResult(new { error = true, message = "Error! Please Gate in again." });
                
            }

            return null;
        }



        [HttpPost]
        public IActionResult AddGetOutCY(List<CYGateOutViewModel> model)
        {
            try
            {
                var datetime = DateTime.Now;

                 bool hasMatch = _gtoRepo.GetAll().Any(x => model.Any(y => y.ContainerNumber == x.ContainerNumber && y.VIRNumber == x.VIRNumber));

                if (!hasMatch)
                {
                    var holddata = new List<Hold>();

                    //List<GateOut> gateOuts = new List<GateOut>();
                    List<DOItem> DOItemts = new List<DOItem>();
                    List<GTO> gtos = new List<GTO>();
                    List<CYContainer> cycontainers = new List<CYContainer>();

                    foreach (var data in model)
                    {

                        if (data.Type == "Normal")
                        {
                            var contrnormala = _cyContainerRepo.GetCYContainersByIGMContainer(data.VIRNumber, data.ContainerNumber);


                            foreach (var item in contrnormala)
                            {
                                if(item.IsPartialContainer == true)
                                {

                                    var checkgp = _containerRepo.CheckDeliveryStatusForPart(item.VIRNo, item.IndexNo ?? 0, item.ContainerNo);

                                    if(checkgp == false)
                                    {
                                        var res = new Hold
                                        {
                                            HoldDate = datetime,
                                            IsHold = true,
                                            HoldType = "GatePass",
                                            Nature = "Normal",
                                            SpecialInstructions = "no gatepass avaible on containerno " + item.ContainerNo + " and igm " + item.VIRNo + " and indexno " + item.IndexNo,
                                        };

                                        holddata.Add(res);
                                    } 
                                }
                            }
                        }

                        if (data.Type == "CrossStuff")
                        {
                            var contrs = _cyContainerRepo.GetCSContainerIndexByIGMAndContainerNo(data.VIRNumber, data.ContainerNumber);
                            foreach (var item in contrs)
                            {
                                if (item.IsPartialContainer == true)
                                {

                                    var checkgp = _containerRepo.CheckDeliveryStatusForPart(item.VIRNo, item.IndexNo ?? 0, item.ContainerNo);

                                    if (checkgp == false)
                                    {
                                        var res = new Hold
                                        {
                                            HoldDate = datetime,
                                            IsHold = true,
                                            HoldType = "GatePass",
                                            Nature = "Normal",
                                            SpecialInstructions = "no gatepass avaible on containerno " + item.ContainerNo + " and igm " + item.VIRNo + " and indexno " + item.IndexNo,
                                        };

                                        holddata.Add(res);
                                    }
                                }
                            }

                        }

                        var contrnormal = _cyContainerRepo.GetCYContainersByIGMContainer(data.VIRNumber, data.ContainerNumber);

                        if (contrnormal.Count() > 0)
                        {
                            foreach (var item in contrnormal)
                            {
                                var holdreslist = _holdRepository.GetHoldDetail(item.VIRNo, item.IndexNo ?? 0, "CY").ToList();

                                if (holdreslist.Count() > 0)
                                {
                                    holddata.AddRange(holdreslist);
                                }

                             } 
                             
                        }

                        if (data.OldContainerNumberForCS != null && data.OldContainerNumberForCS != "")
                        {

                            var contrnormalsec = _cyContainerRepo.GetCYContainersByIGMContainer(data.VIRNumber, data.OldContainerNumberForCS);

                            if (contrnormalsec.Count() > 0)
                            {
                                foreach (var item in contrnormalsec)
                                {
                                    var holdreslist = _holdRepository.GetHoldDetail(item.VIRNo, item.IndexNo ?? 0, "CY").ToList();

                                    if (holdreslist.Count() > 0)
                                    {
                                        holddata.AddRange(holdreslist);
                                    }
                                }
                            }

                        }


                        if (data.OldContainerNumberForCS != null && data.OldContainerNumberForCS != "")
                        {

                            var csconts = _cyContainerRepo.GetCSContainerIndexByIGMAndContainerNo(data.VIRNumber, data.OldContainerNumberForCS);

                            if (csconts.Count() > 0)
                            {
                                foreach (var item in csconts)
                                {
                                    var holdreslist = _holdRepository.GetHoldDetail(item.VIRNo, item.IndexNo ?? 0, "CY").ToList();

                                    if (holdreslist.Count() > 0)
                                    {
                                        holddata.AddRange(holdreslist);
                                    }
                                }
                            }
                        }

                        var cscontsec = _cyContainerRepo.GetCSContainerIndexByIGMAndContainerNo(data.VIRNumber, data.ContainerNumber);

                        if (cscontsec.Count() > 0)
                        {
                            foreach (var item in cscontsec)
                            {
                                var holdreslist = _holdRepository.GetHoldDetail(item.VIRNo, item.IndexNo ?? 0, "CY").ToList();

                                if (holdreslist.Count() > 0)
                                {
                                    holddata.AddRange(holdreslist);
                                }
                            }
                        }

                        var sim = _simreport.GetSIMInfo(data.ContainerNumber, data.VIRNumber);

                        if (sim != null && sim.Status == "HO")
                        {

                            var res = new Hold
                            {
                                HoldDate = sim.CreateDT,
                                IsHold = true,
                                HoldType = "SIM",
                                Nature = "Normal",
                                SpecialInstructions = "can't  create due To sim hold on container " + data.ContainerNumber + " and igm " + data.VIRNumber,
                            };

                            holddata.Add(res);

                        }

                        if(data.OldContainerNumberForCS != null && data.OldContainerNumberForCS != "")
                        {
                            var simoldcontainer = _simreport.GetSIMInfo(data.OldContainerNumberForCS, data.VIRNumber);

                            if (simoldcontainer != null && simoldcontainer.Status == "HO")
                            {

                                var res = new Hold
                                {
                                    HoldDate = simoldcontainer.CreateDT,
                                    IsHold = true,
                                    HoldType = "SIM",
                                    Nature = "Normal",
                                    SpecialInstructions = "can't  create due To sim hold on container " + data.OldContainerNumberForCS + " and igm " + data.VIRNumber,
                                };

                                holddata.Add(res);

                            }

                        }



                        //var CYContainer = _cyContainerRepo.GetList(x => x.CYContainerId == data.CYContainerId);

                        //foreach (var item in CYContainer)
                        //{
                        //    if (item.IsHold == true)
                        //    {
                        //        return new OkObjectResult(new { error = true, message = "This container is currently on hold" });
                        //    }
                        //}

                        //if (data.Type == "CrossStuff")
                        //    {
                        //        var oldsim = _simreport.GetAll().Where(x => x.ContainerNumber == data.OldContainerNumberForCS && x.VIRNumber == data.VIRNumber).LastOrDefault();

                        //        if (oldsim != null && oldsim.Status == "HO")
                        //        {
                        //            return new OkObjectResult(new { error = true, message = "This OLD container " + data.OldContainerNumberForCS + "  is currently on simo hold" });

                        //        }
                        //    }

                        //    var sim = _simreport.GetAll().Where(x => x.ContainerNumber == data.ContainerNumber && x.VIRNumber == data.VIRNumber ).LastOrDefault();

                        //    if (sim != null && sim.Status == "HO")
                        //    {
                        //        return new OkObjectResult(new { error = true, message = "This container is currently on simo hold" });

                        //    }

                        //var crl = _crlRepository.GetAll().Where(x => x.VIRNumber == data.VIRNumber && x.ContainerNumber.ToUpper().Trim() == data.ContainerNumber.ToUpper().Trim() ).LastOrDefault();

                        //if (crl == null)
                        //{
                        //    return new OkObjectResult(new { error = true, message = "clearance not yet received!" });
                        //}

                    }

                    if (holddata.Count() > 0)
                    {
                        return new OkObjectResult(new { error = true, message = holddata });
                    }

                    foreach (var data in model)
                    {
                        //var gateout = new GateOut
                        //{
                        //    CYContainerId = data.CYContainerId,
                        //    //ShippingAgentId = data.ShippingAgentId,
                        //    DeliverDate = data.GateOutDate,
                        //    TruckNo = data.TruckNumber,
                        //    ShiftedYard = data.ShiftedYard
                        //};

                        //gateOuts.Add(gateout);


                        if (data.Type == "Normal")
                        {
                            var rescrl = _crlRepository.GetManualCrlInfo(data.VIRNumber, data.ContainerNumber);

                            var contr = _cyContainerRepo.GetContainercydetilByIGMAndContainerNo(data.VIRNumber , data.ContainerNumber);

                            if (contr.Count() > 0)
                            {
                                foreach (var item in contr)
                                {
                                    if (item != null)
                                    {
                                        item.IsGateOut = true;
                                        item.CYContainerGateOutDate = datetime;
                                        cycontainers.Add(item);
                                    }
                                }
                            }

                            //var doitm = _dOItemRepo.GetAll().Where(x => x.DOItemId == data.DoItemId).LastOrDefault();

                            //if (doitm != null)
                            //{
                            //    doitm.IsGateOut = true;
                            //    DOItemts.Add(doitm);

                            //}

                            var doitms = _containerRepo.Checkcontainerwisegp(data.VIRNumber, data.ContainerNumber);

                            if(doitms.Count() > 0)
                            {
                                doitms.ForEach(x => x.IsGateOut = true);
                                DOItemts.AddRange(doitms);
                            }


                            var gto = new GTO
                            {
                                VIRNumber = data.VIRNumber,
                                ContainerNumber = data.ContainerNumber,
                                GateOutDate = data.GateOutDate,
                                Status = data.Status,
                                TruckNumber = data.TruckNumber,
                                SealNumber = data.SealNumber,
                                BondedCarrier = "",
                                PortofExit = "",
                                Country = "",
                                GrossWeight = data.TerminalWeight,
                                Category = "I",
                                MessageFrom = "AIT",
                                MessageTo = "WeBOC",
                                IsProcessed = rescrl != null ? true : false,
                                FileName = $"GTO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                            };

                            gtos.Add(gto);

                        }
                        
                        if (data.Type == "CrossStuff")
                        {

                            var rescrl = _crlRepository.GetManualCrlInfo(data.VIRNumber, data.ContainerNumber);

                            var contr = _cyContainerRepo.GetCSContainerIndexByIGMAndContainerNo(data.VIRNumber , data.ContainerNumber);

                            if (contr.Count() > 0)
                            {
                                foreach (var item in contr)
                                {
                                    if (item != null)
                                    {
                                        item.IsGateOut = true;
                                        item.CYContainerGateOutDate = DateTime.Now;
                                        cycontainers.Add(item);
                                    }
                                }
                            }

                            //var doitm = _dOItemRepo.GetAll().Where(x => x.DOItemId == data.DoItemId).LastOrDefault();

                            //if (doitm != null)
                            //{
                            //    doitm.IsGateOut = true;
                            //    DOItemts.Add(doitm);

                            //}



                            var doitms = _containerRepo.Checkcontainerwisegp(data.VIRNumber, data.ContainerNumber);

                            if (doitms.Count() > 0)
                            {
                                doitms.ForEach(x => x.IsGateOut = true);
                                DOItemts.AddRange(doitms);
                            }


                            var gto = new GTO
                            {
                                VIRNumber = data.VIRNumber,
                                ContainerNumber = data.ContainerNumber,
                                GateOutDate = data.GateOutDate,
                                Status = data.Status,
                                TruckNumber = data.TruckNumber,
                                SealNumber = data.SealNumber,
                                BondedCarrier = data.BondedCarrier,
                                PortofExit = data.PortOfExit,
                                Country = data.Country,
                                GrossWeight = data.TerminalWeight,
                                Category = "I",
                                MessageFrom = "AIT",
                                MessageTo = "WeBOC",
                                IsProcessed = rescrl != null ? true : false,
                                FileName = $"GTO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                            };

                            gtos.Add(gto);

                        }
                       
                    }


                    _gtoRepo.AddRange(gtos);
                    _dOItemRepo.UpdateRange(DOItemts);
                    //_gateOutRepo.AddRange(gateOuts); 
                    _cyContainerRepo.UpdateRange(cycontainers);

                    return Json(new { error = false, message = "Saved" });
                }
                else
                {
                    var holddata = new List<Hold>();

                    var res = new Hold
                    {
                        HoldDate = DateTime.Now,
                        IsHold = true,
                        HoldType = "",
                        Nature = "Normal",
                        SpecialInstructions = "The list contains a container which is already gated out. Please remove the container and try again.",
                    };

                    holddata.Add(res);

                    return new OkObjectResult(new { error = true, message = holddata });
                }
               

            }
            catch (Exception e)
            {


                foreach (var container in model)
                {
                 //   var voyage = _voyageRepo.GetFirst(v => v.VIRNo == container.VIRNumber);

                    var gto = _gtoRepo.GetFirst(g => g.VIRNumber == container.VIRNumber && g.ContainerNumber == container.ContainerNumber );

                    if (gto == null)
                    {
                        var contList = _cyContainerRepo.GetAll(c => c.VIRNo == container.VIRNumber && c.ContainerNo == container.ContainerNumber);

                        foreach (var cntr in contList)
                        {
                            cntr.IsGateOut = false;
                            _cyContainerRepo.Update(cntr);

                            var containr = _gateOutRepo.GetFirst(g => g.CYContainerId == cntr.CYContainerId);
                            if (containr != null)
                                _gateOutRepo.Delete(containr);
                        }
                    }

                }


                var holddata = new List<Hold>();

                var res = new Hold
                {
                    HoldDate = DateTime.Now,
                    IsHold = true,
                    HoldType = "",
                    Nature = "Normal",
                    SpecialInstructions = "Error! Please Gate Out  again.",
                };

                holddata.Add(res);

                return new OkObjectResult(new { error = true, message = holddata }); 

            }
            
            
            return Ok();

        }


        [HttpPost]
        public IActionResult AddGetOutCFS(List<CFSGateOutViewModel> model)
        {
            var datetime = DateTime.Now;
 
            List<GTOO> gTOOs = new List<GTOO>();
            List<ContainerIndex> containerIndexs = new List<ContainerIndex>();
               
            try
            {
                var resdata = new List<Hold>();


                foreach (var item in model)
                 {


                    var _hold = _holdRepository.GetHoldDetail(item.VIRNo, item.IndexNo ?? 0, "CFS").ToList();

                    if (_hold.Count() > 0)
                    {
                        resdata.AddRange(_hold);
                    } 
                    var simo = _simoreport.GetSIMOInfoByIGMIndex(item.VIRNo, item.IndexNo ?? 0);

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

                    

                    //    var simo = _simoreport.GetAll().Where(x => x.VIRNumber == item.VIRNo && x.BLNumber == item.BLNo && x.IndexNumber == item.IndexNo.ToString()).LastOrDefault();

                    //    if (simo != null && simo.Status == "HO")
                    //    {
                    //        return new OkObjectResult(new { error = true, message = "can't send message due to simo hold" });
                    //    }

                    //    var containerindx = _containerIndexRepo.GetAll().Where(x => x.VirNo == item.VIRNo   && x.IndexNo == item.IndexNo ).LastOrDefault();

                    //    //if (containerindx != null && containerindx.IsHold == true)
                    //    //{
                    //    //    return new OkObjectResult(new { error = true, message = "index on terminal hold" });
                    //    //}
                    //    //if (containerindx != null && containerindx.IsAuction == true)
                    //    //{
                    //    //    return new OkObjectResult(new { error = true, message = "index on auction hold" });
                    //    //}

                    //    //var crlo = _crloRepo.GetAll().Where(x => x.VIRNumber == item.VIRNo && x.BLNumber.ToUpper().Trim() == item.BLNo.ToUpper().Trim() && x.IndexNumber == item.IndexNo).LastOrDefault();

                    //    //if (crlo == null)
                    //    //{
                    //    //    return new OkObjectResult(new { error = true, message = "clearance not yet received!" });
                    //    //}
                }

                

                if (resdata.Count() > 0)
                {
                    return new OkObjectResult(new { error = true, message = resdata });

                }



                foreach (var item in model)
                {
                    var rescrlo = _crloRepo.GetManualCrloInfo(item.VIRNo,   Convert.ToInt32( item.IndexNo) , item.BLNo);

                    var gtoo = new GTOO
                    {
                        Category = "I",
                        VIRNumber = item.VIRNo,
                        //BLNumber = gdio != null ? gdio.BLNumber : index.BLNo,
                        ContainerNumber = item.ContainerNo,
                        BLNumber = item.BLNo,
                        NumberofPackages =  item.NoOfPackages,
                        PackageType = item.PackageType ,
                        Status = item.Status,
                        GateOutTime = datetime,
                        Truck = item.TruckNo,
                        Weight = item.NoOfPackages,
                        MessageFrom = "AIT",
                        MessageTo = "WEBOC",
                        IsProcessed = rescrlo != null ? true : false,
                        FileName = $"GTOO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                    };

                    gTOOs.Add(gtoo);
                }
                 
                _gtooRepo.AddRange(gTOOs);

                foreach (var item in model)
                {
                    if (item.Status == "F")
                    {
                        //var res = _containerIndexRepo.GetAll().Where(s => s.VirNo == item.VIRNo && s.IndexNo == item.IndexNo).LastOrDefault();
                        var res = _containerIndexRepo.GetContainerIndexByIgmAndIndex(item.VIRNo, item.IndexNo ?? 0).ToList();

                        if (res.Count() > 0)
                        {
                            res.ForEach(x => x.IsGateOut = true);
                            res.ForEach(x => x.CFSContainerGateOutDate = datetime);
                        }

                        _containerIndexRepo.UpdateRange(res);
                    }

                    var doitm = _dOItemRepo.GetAll().Where(x => x.DOItemId == item.DoItemId).LastOrDefault();

                    if (doitm != null)
                    {
                        doitm.IsGateOut = true;
                        _dOItemRepo.Update(doitm);
                    }
                     
                }

                return new OkObjectResult(new { error = false, message = "Gateout Message Successfully Sent!" });
                //var containers = _containerIndexRepo.GetList(s => s.VoyageId == index.VoyageId && s.IndexNo == index.IndexNo);

                //foreach (var container in containers)
                //{
                //    container.IsGateOut = true;
                //    _containerIndexRepo.Update(container);
                //}


                ////var gdio = _gdioRepo.GetFirst(d => d.VIRNumber == index.Voyage.VIRNo && d.IndexNumber == index.IndexNo);

                ////if (gdio == null)
                ////    return new OkObjectResult(new { error = true, message = "GDIO not yet received!" });

                //var crlo = _crloRepo.GetAll().Where(x => x.VIRNumber == index.Voyage.VIRNo && x.BLNumber.ToUpper().Trim() == index.BLNo.ToUpper().Trim() && x.IndexNumber == index.IndexNo).LastOrDefault();

                //if (crlo == null)
                //{
                //    return new OkObjectResult(new { error = true, message = "clearance not yet received!" });
                //}

                //else
                //{



                //    for (int i = 0; i < doItems.Count; i++)
                //    {
                //        var gatePassItem = _doItemRepo.GetFirst(d => d.GatePassId == doItems[i].GatePassId && d.TruckNumber == doItems[i].TruckNumber);
                //        if (gatePassItem != null)
                //        {
                //            gatePassItem.IsGateOut = true;
                //            _doItemRepo.Update(gatePassItem);
                //        }

                //        var doItem = _doItemRepo.Find(doItems[i].DOItemId);
                //        if (doItem != null)
                //        {
                //            doItem.Status = doItems[i].Status;
                //            doItem.PackageType = doItems[i].PackageType;
                //            doItem.NoOfPackages = doItems[i].NoOfPackages;

                //            _doItemRepo.Update(doItem);
                //        }

                //        var gtoo = new GTOO
                //        {
                //            Category = "I",
                //            VIRNumber = index.Voyage.VIRNo,
                //            //BLNumber = gdio != null ? gdio.BLNumber : index.BLNo,
                //            ContainerNumber = index.ContainerNo,
                //            BLNumber = index.BLNo,
                //            NumberofPackages = doItems[i].NoOfPackages,
                //            PackageType = doItems[i].PackageType + "=" + doItems[i].Quantity,
                //            Status = doItems[i].Status,
                //            GateOutTime = datetime,
                //            Truck = doItems[i].TruckNumber,
                //            Weight = doItems[i].Quantity,
                //            MessageFrom = "BWP",
                //            MessageTo = "WEBOC",
                //            FileName = $"GTOO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                //        };

                //        gTOOs.Add(gtoo);
                //    }




                //    if ((crlo != null) && (crlo.Status == "RF" || crlo.Status == "IB"))
                //    {

                //        _gtooRepo.AddRange(gTOOs);

                //        return new OkObjectResult(new { error = false, message = "Gateout Message Successfully Sent!" });

                //    }
                //    else
                //    {
                //        return new OkObjectResult(new { error = true, message = "Remove From Along Side But Edi Not Sent Due to clearance Not Found" });

                //    }
                //}
                 
            }
            catch (Exception e)
            {
                  
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }

            //foreach (var data in model)
            //{
            //    var containerindex = _containerIndexRepo.GetList(x => x.Voyage.VIRNo == data.VIRNo && x.BLNo == data.BLNo, v => v.Voyage);

            //    foreach (var item in containerindex)
            //    {
            //        if (item.IsHold == true)
            //        {
            //            return new OkObjectResult(new { error = true, message = "This index is currently on hold" });
            //        }
            //    }

            //    var index = containerindex.FirstOrDefault();

            //    if (index != null)
            //    {
            //        var gateoutContainers = _containerRepo.GetGateoutCFSContainers(index.Voyage.VIRNo, index.IndexNo);

            //        var gateout = new GateOut
            //        {
            //            ContainerIndexId = data.ContainerIndexId,
            //            DeliverDate = data.GateOutTime,
            //            //ShippingAgentId = data.ShippingAgentId,
            //            TruckNo = data.TruckNo,
            //            ShiftedYard = data.ShiftedYard
            //        };

            //        gateOuts.Add(gateout);

            //        var cntr = _containerIndexRepo.Find(data.ContainerIndexId);
            //        if (cntr != null)
            //        {
            //            cntr.IsGateOut = true;
            //            containerIndexs.Add(cntr);

            //        }


            //        var gtoo = new GTOO
            //        {
            //            Category = data.Category,
            //            VIRNumber = data.VIRNo,
            //            ContainerNumber = data.ContainerNo,
            //            BLNumber = data.BLNo,
            //            NumberofPackages = data.NoOfPackages,
            //            PackageType = data.PackageType,
            //            Status = data.Status,
            //            GateOutTime = data.GateOutTime,
            //            Truck = data.TruckNo,
            //            PCCSSSealNo = data.PCCSSSealNo,
            //            BondedCarrierNTN = data.BondedCarrierNTN,
            //            PortOfExit = data.PortOfExit,
            //            CountryofExit = data.CountryOfExit,
            //            Weight = Convert.ToInt32(data.Weight),
            //            FileName = $"GTOO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
            //        };

            //        gTOOs.Add(gtoo);
            //    }


            //    foreach (var item in gTOOs)
            //    {
            //        var result = _gtooRepo.GetAll().Where(x => x.FileName == item.FileName).ToList();

            //        if (result != null)
            //        {
            //            return new OkObjectResult(new { error = true, message = "File Already Generated! Please Send After Few Minute" });
            //        }
            //    }

            //    _containerIndexRepo.UpdateRange(containerIndexs);

            //    _gtooRepo.AddRange(gTOOs);

            //    _gateOutRepo.AddRange(gateOuts);


            //}


            return Ok();

        }

        public IActionResult PreGateout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePreGateOut(List<StuffingConfirmationViewModel> data)
        {
            var datetime = DateTime.Now;


            try
            {

                bool hasMatch = _pgooProcessor.GetAll().Any(x => data.Any(y => y.ContainerNo == x.ContainerNo && y.VIRNo == x.VIRNo && y.BLNo == x.BLNo));

                if (!hasMatch)
                {
                    List<PGOO> pGOOs = new List<PGOO>();
                    foreach (var item in data)
                    {
                        var pgo = new PGOO
                        {
                            VIRNo = item.VIRNo,
                           ContainerNo = item.ContainerNo,
                            VehicleNo = item.VehicleNo,
                          //  BLNo = item.BLNo,
                          //   IndexNo = item.IndexNo ?? 0,
                            BondedCarrierId = item.BondedCarrierId,
                            BondedCarrierName = item.BondedCarrierName,
                            OpType = "A",
                            MessageFrom = "AIT",
                            MessageTo = "WeBOC",
                            Performed = DateTime.Now,
                            FileName = $"PGOO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"



                        };

                        pGOOs.Add(pgo);
                    }




                    _pgooProcessor.AddRange(pGOOs);

                    return new OkObjectResult(new { error = false, message = "Save" });

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "The list contains a container which is Pre GateOut . Please remove the container and try again." });
                }

                
            }
            catch (Exception e)
            {
                foreach (var item in data)
                {

                    var result = _pgooProcessor.GetFirst(g => g.ContainerNo == item.ContainerNo && g.VIRNo == item.VIRNo);
                    if (result != null)
                        _pgooProcessor.Delete(result);
                }

                return new OkObjectResult(new { error = true, message = "Error! Please Pre GateOut again." });
            }
            
         


            return Ok();

        }

        public IActionResult SaveGateInContainersCY(List<GateInViewModel> containers)
        {
            try
            {

                bool hasMatch = _gateInRepo.GetAll().Any(x => containers.Any(y => y.ContainerNo == x.ContainerNo && y.VIRNo == x.VIRNo));

                if (!hasMatch)
                {


                    var datetime = DateTime.Now;

                    List<GateIn> gateins = new List<GateIn>();
                    List<GIIO> giios = new List<GIIO>();
                    List<CYContainer> cycontainers = new List<CYContainer>();

                    foreach (var container in containers)
                    {
                        var gatein = new GateIn
                        {
                            CarrierId = container.CarrierId,
                            ShippingAgentId = container.ShippingAgentId,
                            CarrierName = container.CarrierName,
                            ContainerNo = container.ContainerNo,
                            ContainerSize = container.ContainerSize,
                            GateInDateTime = container.GateInDateTime,
                            ISOCode = container.ISOCode,
                            MenifestedSealNo = container.MenifestedSealNo,
                            PACSSSealNo = container.PACSSSealNo,
                            VehicleNo = container.VehicleNo,
                            FoundSealNumber = container.FoundSealNumber,
                            VIRNo = container.VIRNo,
                            Weight = container.Weight,
                            DPManifestWeight = container.DPManifestWeight,
                            DPTareweight = container.DPTareweight
                        };

                        gateins.Add(gatein);
                        var cntr = _cyContainerRepo.GetFirst(x => x.ContainerNo == container.ContainerNo && x.VIRNo == container.VIRNo);
                        if (cntr != null)
                        {
                            cntr.IsGateIn = true;
                            cntr.InvoiceLocked = true;
                            cntr.ShippingAgentId = container.ShippingAgentId ?? 0;
                            cntr.Size = container.ContainerSize != null ? Convert.ToInt32(container.ContainerSize) : 0;
                            cntr.CYContainerGateInDate = container.GateInDateTime;
                            cycontainers.Add(cntr);

                        }


                        var giio = new GIIO
                        {
                            ContainerNumber = container.ContainerNo,
                            GateInTime = container.GateInDateTime,
                            MessageFrom = "AIT",
                            MessageTo = "WeBOC",
                            PCCSSSealNumber = container.PACSSSealNo,
                            VehicleNumber = container.VehicleNo,
                            Weight = container.Weight,
                            VIRNumber = container.VIRNo,
                            Performed = DateTime.Now,
                            FileName = $"GIIO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                        };

                        giios.Add(giio);
                    }
                      

                    
                    _giioRepo.AddRange(giios);

                    _gateInRepo.AddRange(gateins);

                    _cyContainerRepo.UpdateRange(cycontainers);

                    

                    return new OkObjectResult(new { error = false, message = "Save" });

                }

                else
                {
                    return new OkObjectResult(new { error = true, message = "The list contains a container which is already gated in. Please remove the container and try again." });
                }

            }
            catch (Exception e)
            {
                //foreach (var container in containers)
                //{
                //    var giio = _giioRepo.GetFirst(g => g.ContainerNumber == container.ContainerNo && g.VIRNumber == container.VIRNo);

                //    if (giio == null)
                //    {
                //        var cntr = _cyContainerRepo.GetFirst(x => x.ContainerNo == container.ContainerNo && x.VIRNo == container.VIRNo );

                //        if (cntr != null)
                //        {
                //            cntr.IsGateIn = false;
                //            _cyContainerRepo.Update(cntr);
                //        }

                //        var gatein = _gateInRepo.GetFirst(g => g.ContainerNo == container.ContainerNo && g.VIRNo == container.VIRNo);
                //        if (gatein != null)
                //            _gateInRepo.Delete(gatein);

                        return new OkObjectResult(new { error = true, message = "Error! Please Gate in again." });
                //    }
                //}
            }

            return null;
        }

        public IActionResult EmptyGateOutContainers()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmptyGateOutContainers(List<EmptyGateOutContainerViewModel> model)
        {
            try
            {

                bool hasMatch = _emptyGateOutContainerRepositroy.GetAll().Any(x => model.Any(y => y.ContainerIndexId == x.ContainerIndexId));

                if (!hasMatch)
                {
                    var datetime = DateTime.Now;
                    List<EmptyGateOutContainer> emptyGateOutContainers = new List<EmptyGateOutContainer>();
                    List<ContainerIndex> containerIndes = new List<ContainerIndex>();

                    foreach (var data in model)
                    {
                        var emptyGateOutContainer = new EmptyGateOutContainer
                        {
                            ContainerCondition = data.ContainerCondition,
                            DeliveredYard = data.DeliveredYard,
                            DeliveredYardDate = data.DeliveredYardDate,
                            TransporterId = data.TransporterId,
                            ContainerIndexId = data.ContainerIndexId,
                            ShippingAgentId = data.ShippingAgentId,
                            ShippingLine = data.ShippingLine,
                            ContainerNo = data.ContainerNumber,
                            VirNo = data.VirNumber,
                            VehicleNumber = data.VehicleNumber,
                            CreatedDate = datetime

                        };

                        emptyGateOutContainers.Add(emptyGateOutContainer);
                    }


                    foreach (var container in model)
                    {

                        var containerIndexes = _containerIndexRepo.GetAll().Where(x => x.ContainerNo == container.ContainerNumber && x.VirNo == container.VirNumber).ToList();
                        if (containerIndexes.Count() > 0)
                        {
                            foreach (var cntr in containerIndexes)
                            {
                                if (cntr != null)
                                {
                                    cntr.IsEmptyGateOut = true;
                                    containerIndes.Add(cntr);

                                }
                            }


                        }

                    }



                    _containerIndexRepo.UpdateRange(containerIndes);

                    _emptyGateOutContainerRepositroy.AddRange(emptyGateOutContainers);

                    return new OkObjectResult(new { error = false, message = "Save" });


                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "The list contains a container which is already gated in. Please remove the container and try again." });
                }

            }
            catch (Exception)
            {

                return new OkObjectResult(new { error = true, message = "please try again" });
            }


           
        }


        [HttpPost]
        public IActionResult AddEmptyGateOutContainersCFS(EmptyGateOutContainerViewModel model)
        {
            try
            {

                bool hasMatch = _emptyGateOutContainerRepositroy.GetAll().Any(x => x.VirNo == model.VirNumber && x.ContainerNo == model.ContainerNumber);

                if (!hasMatch)
                {
                    var datetime = DateTime.Now;
                     List<ContainerIndex> containerIndes = new List<ContainerIndex>();
                     
                        var emptyGateOutContainer = new EmptyGateOutContainer
                        {
                            ContainerCondition = model.ContainerCondition,
                            DeliveredYard = model.DeliveredYard,
                            DeliveredYardDate = model.DeliveredYardDate,
                            TransporterId = model.TransporterId,
                            ContainerIndexId = model.Type == "CFS" ? model.Key : null,
                            CYContainerId = model.Type == "CY" ? model.Key : null,
                            ShippingAgentId = model.ShippingAgentId,
                            ShippingLine = model.ShippingLine,
                            ContainerNo = model.ContainerNumber,
                            VirNo = model.VirNumber,
                            VehicleNumber = model.VehicleNumber,
                            CreatedDate = datetime,
                            Type = model.Type

                        };


                  //  var containerIndexes = _containerIndexRepo.GetAll().Where(x => x.ContainerNo == model.ContainerNumber && x.VirNo == model.VirNumber).ToList();

                    if(model.Type == "CFS")
                    {
                        var containerIndexes = _containerIndexRepo.GetContainerIndexByIGMAndContainerNo(model.VirNumber, model.ContainerNumber).ToList();
                        if (containerIndexes.Count() > 0)
                        {
                            foreach (var cntr in containerIndexes)
                            {
                                if (cntr != null)
                                {
                                    //cntr.IsEmptyGateOut = true;
                                    //containerIndes.Add(cntr);

                                    var tempUpdateQuery = string.Format(@"UPDATE ContainerIndex SET IsEmptyGateOut = '" + 1 + "'  WHERE  ContainerIndexId  = '" + cntr.ContainerIndexId + "'  ");
                                    _containerIndexRepo.ExecuteNonSQL(tempUpdateQuery);

                                }
                            }

                        }
                    }
                    


                    //_containerIndexRepo.UpdateRange(containerIndes);

                    _emptyGateOutContainerRepositroy.Add(emptyGateOutContainer);

                    return new OkObjectResult(new { error = false, message = "Save" , emptyGateOutContainerno = emptyGateOutContainer.EmptyGateOutContainerId });

                }

                 
                else
                {
                    return new OkObjectResult(new { error = true, message = "The list contains a container which is already gated in. Please remove the container and try again." });
                }

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = "please try again" });
            }



        }


        public IActionResult SaveDeletedContainersCFS(string containerNumber)
        {
            var container = _containerIndexRepo.GetFirst(x => x.ContainerNo == containerNumber);
            if (container != null)
            {
                container.IsDeleted = true;
                _containerIndexRepo.Update(container);

            }
            return Ok();
        }


        public IActionResult SaveDeletedContainersCY(string containerNumber)
        {
            var cycontainer = _cyContainerRepo.GetFirst(x => x.ContainerNo == containerNumber);
            if (cycontainer != null)
            {
                cycontainer.IsDeleted = true;
                _cyContainerRepo.Update(cycontainer);

            }
            return Ok();
        }


        [HttpPost]
        public IActionResult AddGetOutTP(List<GTTOViewModel> model)
        {
            try
            {

                var datetime = DateTime.Now;


                bool hasMatch = _gttoRepo.GetAll()
             .Any(x => model.Any(y => y.VIRNo == x.VIRNumber
                                            && y.ContainerNo == x.ContainerNo
                                            && x.CreateDT > datetime.AddMinutes(-10)));

                if (!hasMatch)
                {

                    //List<GateOut> gateOuts = new List<GateOut>();
                    var resdata = new List<Hold>();
                    var doitemlist = new List<DOItem>();
                    var containerindexlist = new List<ContainerIndex>();



                    List<GTTO> gttos = new List<GTTO>();
                    //List<ContainerIndex> containerIndexs = new List<ContainerIndex>();

                    foreach (var item in model)
                    {

                        var ccmo = _cCMORepository.GetCCMOsByIgmContainer(item.VIRNo, item.ContainerNo);

                        if (ccmo != null)
                        {
                            var ccmolist = _cCMORepository.GetCCMOsByContainerAndGd(ccmo.ContainerNo, ccmo.TPNo).ToList();

                            if (ccmolist.Count() > 0)
                            {
                                foreach (var ccmolistitem in ccmolist)
                                {
                                    var _hold = _holdRepository.GetHoldDetail(ccmolistitem.VIRNo, ccmolistitem.IndexNo ?? 0, "CFS").ToList();

                                    if (_hold.Count() > 0)
                                    {
                                        resdata.AddRange(_hold);
                                    }
                                    var simo = _simoreport.GetSIMOInfoByIGMIndex(ccmolistitem.VIRNo, ccmolistitem.IndexNo ?? 0);

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

                                    var deliveryres = _containerRepo.CheckDeliveryStatus(ccmolistitem.VIRNo, ccmolistitem.IndexNo ?? 0);

                                    if (deliveryres == false)
                                    {

                                        var ressimo = new Hold
                                        {
                                            HoldDate = DateTime.Now,
                                            IsHold = true,
                                            HoldType = "",
                                            Nature = "Normal",
                                            SpecialInstructions = "please create gatepass IGM " + ccmolistitem.VIRNo + " and index " + ccmolistitem.IndexNo + " with status F ",
                                        };

                                        resdata.Add(ressimo);

                                    }
                                }
                            }


                            var ccmolistforindexes = _cCMORepository.ccmodatewiselist(ccmo.ContainerNo).ToList();

                            if (ccmolistforindexes.Count() > 1 )
                            {
                                foreach (var ccmolistitem in ccmolistforindexes)
                                {
                                    var _hold = _holdRepository.GetHoldDetail(ccmolistitem.VIRNo, ccmolistitem.IndexNo ?? 0, "CFS").ToList();

                                    if (_hold.Count() > 0)
                                    {
                                        resdata.AddRange(_hold);
                                    }
                                    var simo = _simoreport.GetSIMOInfoByIGMIndex(ccmolistitem.VIRNo, ccmolistitem.IndexNo ?? 0);

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

                                    var deliveryres = _containerRepo.CheckDeliveryStatus(ccmolistitem.VIRNo, ccmolistitem.IndexNo ?? 0);

                                    if (deliveryres == false)
                                    {

                                        var ressimo = new Hold
                                        {
                                            HoldDate = DateTime.Now,
                                            IsHold = true,
                                            HoldType = "",
                                            Nature = "Normal",
                                            SpecialInstructions = "please create gatepass IGM " + ccmolistitem.VIRNo + " and index " + ccmolistitem.IndexNo + " with status F ",
                                        };

                                        resdata.Add(ressimo);

                                    }
                                }
                            }


                        }

                    }



                    


                    foreach (var item in model)
                    {

                        var ccmo = _cCMORepository.GetCCMOsByIgmContainer(item.VIRNo, item.ContainerNo);

                        if (ccmo != null)
                        {
                            var ccmolist = _cCMORepository.GetCCMOsByContainerAndGd(ccmo.ContainerNo, ccmo.TPNo).ToList();

                            if (ccmolist.Count() > 0)
                            {
                                foreach (var ccmolistitem in ccmolist)
                                { 
                                    var Indexesres = _containerIndexRepo.GetContainerIndexByIgmAndIndex(ccmolistitem.VIRNo, ccmolistitem.IndexNo ?? 0).ToList();

                                    if (Indexesres.Count() > 0)
                                    {
                                        Indexesres.ForEach(x => x.IsGateOut = true);
                                        Indexesres.ForEach(x => x.CFSContainerGateOutDate = datetime);

                                        containerindexlist.AddRange(Indexesres);
                                    }

                                    var doitm = _containerRepo.GetDOItems(ccmolistitem.VIRNo, ccmolistitem.IndexNo ?? 0).ToList();

                                    if(doitm.Count() > 0)
                                    {
                                        doitm.ForEach(x => x.IsGateOut = true);
                                        doitemlist.AddRange(doitm);
                                    }
                                }
                            }

                            var ccmolistforindexes = _cCMORepository.ccmodatewiselist(ccmo.ContainerNo).ToList();

                            if (ccmolistforindexes.Count() > 1)
                            {

                                foreach (var ccmolistitem in ccmolistforindexes)
                                {
                                    var Indexesres = _containerIndexRepo.GetContainerIndexByIgmAndIndex(ccmolistitem.VIRNo, ccmolistitem.IndexNo ?? 0).ToList();

                                    if (Indexesres.Count() > 0)
                                    {
                                        Indexesres.ForEach(x => x.IsGateOut = true);
                                        Indexesres.ForEach(x => x.CFSContainerGateOutDate = datetime);

                                        containerindexlist.AddRange(Indexesres);
                                    }

                                    var doitm = _containerRepo.GetDOItems(ccmolistitem.VIRNo, ccmolistitem.IndexNo ?? 0).ToList();

                                    if (doitm.Count() > 0)
                                    {
                                        doitm.ForEach(x => x.IsGateOut = true);
                                        doitemlist.AddRange(doitm);
                                    }
                                }

                            }


                        }
                        else
                        {

                            var ressimo = new Hold
                            {
                                HoldDate = DateTime.Now,
                                IsHold = true,
                                HoldType = "",
                                Nature = "Normal",
                                SpecialInstructions = "CCMO not Avaible",
                            };

                            resdata.Add(ressimo);
 
                        }

                    }


                    if (resdata.Count() > 0)
                    {
                        return new OkObjectResult(new { error = true, message = resdata });

                    }

                    foreach (var data in model)
                    { 

                        var gtto = new GTTO
                        {
                            Category = data.Category,
                            VIRNumber = data.VIRNo,
                            ContainerNo = data.ContainerNo,
                            GateOutDate = data.GateOutTime,
                            Status = data.Status,
                            TruckNumber = data.VehicleNo,
                            SealNumber = data.PCCSSSealNumber,
                            BondedCarrier = data.BondedCarrier,
                            Country = data.CountryOfExit,
                            GrossWeight = data.GrossWeight,
                            MessageFrom = "AIT",
                            MessageTo = "WeBOC",
                            FileName = $"GTTO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                        };

                        gttos.Add(gtto);
                        //  }

                    }


                    _gttoRepo.AddRange(gttos);
                    // _gateOutRepo.AddRange(gateOuts);
                    _dOItemRepo.UpdateRange(doitemlist);

                    _containerIndexRepo.UpdateRange(containerindexlist);

                    return new OkObjectResult(new { error = false, message = "Saved" });

                }


                else
                {
                    var resdata = new List<Hold>();

                    var ressimo = new Hold
                    {
                        HoldDate = DateTime.Now,
                        IsHold = true,
                        HoldType = "",
                        Nature = "Normal",
                        SpecialInstructions = "The list contains a container which is already out. Please remove the container and try again.",
                    };

                    resdata.Add(ressimo);

                    return new OkObjectResult(new { error = true, message = resdata });
                }

            }
            catch (Exception e)
            {
                foreach (var item in model)
                {

                    var result = _gttoRepo.GetAll().Where(g => g.ContainerNo == item.ContainerNo && g.VIRNumber == item.VIRNo).LastOrDefault();
                    if (result != null)
                        _gttoRepo.Delete(result);
                }


                var resdata = new List<Hold>();

                var ressimo = new Hold
                {
                    HoldDate = DateTime.Now,
                    IsHold = true,
                    HoldType = "",
                    Nature = "Normal",
                    SpecialInstructions = "The list contains a container which is already out. Please remove the container and try again.",
                };

                resdata.Add(ressimo);

                return new OkObjectResult(new { error = true, message = resdata });
            }



            return Ok();

        }



        public IActionResult EmptyGatePass()
        {
            
            return View();
        }


        public IActionResult ReturnToYardView()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddReturnToYardContainers(List<ReturnToYardViewModel> model)
        {

            bool hasMatch = _returnToYardRepo.GetAll().Any(x => model.Any(y => y.EmptyGateOutContainerId == x.EmptyGateOutContainerId));

            if (!hasMatch)
            {
                var datetime = DateTime.Now;
                List<ReturnToYard> returnToYards = new List<ReturnToYard>();
 
                foreach (var data in model)
                {
                    var returnToYard = new ReturnToYard
                    {
                        EmptyGateOutContainerId = data.EmptyGateOutContainerId,
                        ReturnToYardDate = data.ReturnToYardDate ,
                        ReturnToYardName = data.ReturnToYardName ,
                        DocumentReceived = data.DocumentReceived

                    };

                    returnToYards.Add(returnToYard);
                }





                _returnToYardRepo.AddRange(returnToYards);

            }
            else
            {
                return new OkObjectResult(new { error = true, message = "The list contains a container which is already Return To Yard. Please remove the container and try again." });
            }


            return Ok();
        }


        public IActionResult EmptyContainerReceivedView()
        {

            ViewData["ShippingAgent"] = _shippingAgentRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });


            ViewData["ShippingLine"] = _shippingLineRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.ShippingLineName, Value = v.ShippingLineId.ToString() });

            ViewData["ClearingAgents"] = _clearingAgentRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.Name, Value = v.ClearingAgentId.ToString() });
  

            ViewData["ISOCodes"] = _ISOCodeHeadRepository.GetAll()
                .Select(s => new SelectListItem { Text = s.ISOCodeHeadDescription.ToString().Trim().ToUpper(), Value = s.ISOCodeHeadId.ToString() });  
            
            ViewData["Transporters"] = _transporterRepository.GetAll()
                .Select(s => new SelectListItem { Text = s.TransporterName.ToString().Trim().ToUpper(), Value = s.TransporterId.ToString() });
             
            return View();
        }


        public IActionResult AddEmptyContainerReceived(EmptyContainerReceived model)
        {
            try
            {
                var res = _emptyContainerReceivedRepository.GetAll().Where(x => x.ContainerNo == model.ContainerNo).LastOrDefault();
                if (res != null)
                {
                    if (res.IsEmptyGateOut == false)
                    {
                        return new OkObjectResult(new { error = true, message = "Container is already Received but not delivered" });
                    }
                    else
                    {
                        model.IsEmptyReceived = true;
                        _emptyContainerReceivedRepository.Add(model);

                        return new OkObjectResult(new { error = false, message = "Save" });
                    }
                }

               
                else
                {
                    model.IsEmptyReceived = true;
                    _emptyContainerReceivedRepository.Add(model);

                    return new OkObjectResult(new { error = false, message = "Save" });

                }
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }
        }


        public JsonResult GetAllEmptyContainerReceived()
        {
            var res = _emptyContainerReceivedRepository.GetAll().Where(x => x.IsEmptyGateOut == false).ToList();

            return Json(res);

        }


        public IActionResult UpdateEmptyContainerReceived(EmptyContainerReceived model)
        {
            try
            {
                 
                    _emptyContainerReceivedRepository.Update(model);

                    return new OkObjectResult(new { error = true, message = "Save" });
          
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }
        }
        public IActionResult DeleteContainerReceived(long id)
        {
            try
            {

                var res = _emptyContainerReceivedRepository.GetAll().Where(x => x.EmptyContainerReceivedId == id).LastOrDefault();

                if(res.IsEmptyGateOut == true)
                {
                    return new OkObjectResult(new { error = true, message = "Can't Delete due to Container is deliverd" });
                }
                else
                {
                    _emptyContainerReceivedRepository.Delete(res);
                    return new OkObjectResult(new { error = false, message = "Delete" });

                }


            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }
        }

        public IActionResult EmptyContainerDeliveredView()
        {

            ViewData["ContainerNumber"] = _emptyContainerReceivedRepository.GetAll().Where(x => x.IsEmptyGateOut == false)
                .Select(s => new SelectListItem { Text = s.ContainerNo.ToString().Trim().ToUpper(), Value = s.EmptyContainerReceivedId.ToString() });

            ViewData["Shiper"] = _shipperRepository.GetAll()
                .Select(s => new SelectListItem { Text = s.ShipperName.ToString().Trim().ToUpper(), Value = s.ShipperId.ToString() });
            
            ViewData["OffHireLocation"] = _offHireLocationRepository.GetAll()
                .Select(s => new SelectListItem { Text = s.OffHireLocationName.ToString().Trim().ToUpper(), Value = s.OffHireLocationId.ToString() });

            ViewData["ShippingLine"] = _shippingLineRepository.GetAll()
               .Select(s => new SelectListItem { Text = s.ShippingLineName.ToString().Trim().ToUpper(), Value = s.ShippingLineId.ToString() });

            ViewData["RandDCleark"] = _randDClerkRepository.GetAll()
               .Select(s => new SelectListItem { Text = s.RandDClerkName.ToString().Trim().ToUpper(), Value = s.RandDClerkId.ToString() });

            ViewData["ShippingAgent"] = _shippingAgentRepo.GetAll()
               .Select(s => new SelectListItem { Text = s.Name.ToString().Trim().ToUpper(), Value = s.ShippingAgentId.ToString() });
            return View();
        }

        public JsonResult GetEmptyReceiveContainerData(long EmptyContainerReceivedId)
        {
            var res = _emptyContainerReceivedRepository.GetEmptyReceiveContainerData(EmptyContainerReceivedId);

            return Json(res);
        }

        public IActionResult AddEmptyContainerDelivered(EmptyContainerDelivered model)
        {
            try
            {
                var res = _emptyContainerReceivedRepository.GetAll().Where(x => x.EmptyContainerReceivedId == model.EmptyContainerReceivedId && x.IsEmptyGateOut == false).LastOrDefault();
                if (res != null)
                {
                     
                    var result = _emptyContainerDeliveredRepository.GetAll().Where(x => x.EmptyContainerReceivedId == model.EmptyContainerReceivedId  ).LastOrDefault();

                    if(result != null)
                    {
                        return new OkObjectResult(new { error = true, message = "Already Delivered" });
                    }
                    else
                    {
                        model.PortOfDischarge = "AICT";
                        model.DelDate = DateTime.Now;
                        _emptyContainerDeliveredRepository.Add(model);

                        res.IsEmptyGateOut = true;
                        _emptyContainerReceivedRepository.Update(res);

                        return new OkObjectResult(new { error = false, message = "Save Info" });
                    }

                    
                }


                else
                { 
                    return new OkObjectResult(new { error = true, message = "Record not found" });

                }
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }
        }


        public IActionResult PGateInOutView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveCapturedImage(string name, string repname, string phonenumber , string cnic, string visitReason, string documentRetain, string visitorCompany , string remarks
                                              //, string passNumber
                                              )
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
                        var filepath = Path.Combine(env.ContentRootPath, "wwwroot\\CameraPhotos") + $@"\{newFileName}";
                        var filepathname = Path.Combine( "CameraPhotos") + $@"\{newFileName}";

                        if (!string.IsNullOrEmpty(filepath))
                        {
                            // Storing Image in Folder  
                            StoreInFolder(file, filepath);

                            var resdata = new PGateInOut
                            {
                                CNIC = cnic,
                                ImageUrl = filepathname,
                                InDateTime = DateTime.Now,
                                IsGateIn = true,
                                IsGateOut = false,
                                Name = repname,
                                PhoneNumber = phonenumber,
                                VisitReason = visitReason,
                                VisitorCompany = visitorCompany,
                                DocumentRetain = documentRetain,
                                Remarks = remarks,
                                //PassNumber = passNumber
                            };

                            _pGateInOutRepository.Add(resdata);



                             
                        }

                    }
                }
                return new OkObjectResult(new { error = false, message = "Save Info" });
            }
            else
            {
                return new OkObjectResult(new { error = true, message = "please try again" });
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


        public IActionResult OldInforeplace( string repname, string phonenumber, string cnic, string visitReason, string documentRetain, string visitorCompany, string remarks , string ImageUrl)
        {
            var res = _pGateInOutRepository.GetPGateInOutDetailByImageUrl(ImageUrl);

            if (res != null)
            { 
                            var resdata = new PGateInOut
                            {
                                CNIC = cnic,
                                ImageUrl = ImageUrl,
                                InDateTime = DateTime.Now,
                                IsGateIn = true,
                                IsGateOut = false,
                                Name = repname,
                                PhoneNumber = phonenumber,
                                VisitReason = visitReason,
                                VisitorCompany = visitorCompany,
                                DocumentRetain = documentRetain,
                                Remarks = remarks,
                                //PassNumber = passNumber
                            };

                            _pGateInOutRepository.Add(resdata);
 
                
                return new OkObjectResult(new { error = false, message = "Save Info" });
            }
            else
            {
                return new OkObjectResult(new { error = true, message = "image is not avaible" });
            }
        }


        public JsonResult GetAllGateOutUsers()
        {

            var res = _pGateInOutRepository.GetAll().Where(x => x.IsGateOut == false).ToList();

            return Json(res);
        }


        public IActionResult MarkOutUsers(List<PGateInOut> users)
        {
            try
            {


                users.ForEach(x => x.IsGateOut = true);
                users.ForEach(x => x.OuttDateTime = DateTime.Now);

                _pGateInOutRepository.UpdateRange(users);

                return new OkObjectResult(new { error = false, message = "updated" });


            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = "Error! Please try again." });

            }

            return null;
        }


        public IActionResult EmptyReceivingCrossStuffView()
        {

            return View();
        }

        public IActionResult UpdateCyContainer(CYContainer model, string virNo, string containerNo)
        {
            var resdata = new List<CYContainer>();

            var csres = _containerRepo.GetCsContainerbycontainerno( model.CSEmptyContainerReceiveId ?? 0);

            if(csres  == null)
            {
                return new OkObjectResult(new { error = true, message = "No result found" });
            }
            if (csres.IsInUse  == true)
            {
                return new OkObjectResult(new { error = true, message = "already assign" });
            }
            if (csres.OutDate != null)
            {
                return new OkObjectResult(new { error = true, message = "already assign  and  mark out" });
            }

            var res = _cyContainerRepo.GetCYContainersByIGMContainer(virNo, containerNo);
            if (res.Count() > 0)
            {
                res.ForEach(c => c.CSContainerNumber = model.CSContainerNumber);
                res.ForEach(c => c.CSSize = model.CSSize);
                res.ForEach(c => c.EmptyReceivedShippingLineId = model.EmptyReceivedShippingLineId);
                res.ForEach(c => c.CSReceivedDate = DateTime.Now);
                res.ForEach(c => c.CSCondition = model.CSCondition);
                res.ForEach(c => c.CSTireWeight = model.CSTireWeight);
                res.ForEach(c => c.IsCSEmtyptyRecieved = true);
                res.ForEach(c => c.IsCrossStuffed = false);
                res.ForEach(c => c.CSVehicleNumer = model.CSVehicleNumer);
                res.ForEach(c => c.CSEmptyContainerReceiveId = model.CSEmptyContainerReceiveId);

                _cyContainerRepo.UpdateRange(res);

                csres.IsInUse = true;

                _cSEmptyContainerReceiveRepository.Update(csres);

                return new OkObjectResult(new { error = false, message = "Updated" });
            }
            else
            {
                return new OkObjectResult(new { error = true, message = "No result found" });
            }


        }

        public IActionResult DeleteCSContainerAssociation(CYContainer model, string virNo, string containerNo)
        {
            try
            {
                var csres = _containerRepo.GetCsContainerbycontainerno(model.CSEmptyContainerReceiveId ?? 0);

            if (csres.OutDate != null)
            {
                return new OkObjectResult(new { error = true, message = "already assign  and  mark out" });
            }

            var result = _gDCRRepository.GetInfo(model.VIRNo, model.ContainerNo, model.BLNo);

            if(result != null)
            {
                return new OkObjectResult(new { error = true, message = "can't update due to gdcr process" });
            }

            var res = _cyContainerRepo.GetCYContainersByIGMContainer(virNo, containerNo);
            if (res.Count() > 0)
            {
                res.ForEach(c => c.CSContainerNumber = "");
                res.ForEach(c => c.CSSize = "");
                res.ForEach(c => c.EmptyReceivedShippingLineId = null);
                res.ForEach(c => c.CSReceivedDate = null);
                res.ForEach(c => c.CSCondition = "");
                res.ForEach(c => c.CSTireWeight = 0);
                res.ForEach(c => c.IsCSEmtyptyRecieved = false);
                res.ForEach(c => c.IsCrossStuffed = null);
                res.ForEach(c => c.CSVehicleNumer = "");
                res.ForEach(c => c.CSEmptyContainerReceiveId = null);

                _cyContainerRepo.UpdateRange(res);

                csres.IsInUse = false;

                _cSEmptyContainerReceiveRepository.Update(csres);

                return new OkObjectResult(new { error = false, message = "Updated" });
            }
             
             


           
                return new OkObjectResult(new { error = false, message = "Updated" });
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }

        }

        public   IActionResult ReProcessGateIn()
        {
            return View();
        }


        public IActionResult SaveReProcessGateIn(List<GIIO> containers)
        {
            try
            {
                var datetime = DateTime.Now;


                bool hasMatch = _giioRepo.GetAll()
               .Any(x => containers.Any(y => y.VIRNumber == x.VIRNumber
                                              && y.ContainerNumber == x.ContainerNumber
                                              && x.Performed > datetime.AddMinutes(-10)));

                if (!hasMatch)
                {
                    var giios = new List<GIIO>();

                    foreach (var container in containers)
                    { 

                         
                            var giio = new GIIO
                            {
                                
                                VIRNumber = container.VIRNumber,
                                ContainerNumber = container.ContainerNumber,
                                PCCSSSealNumber = container.PCCSSSealNumber,
                                Weight = container.Weight,
                                GateInTime = datetime,
                                VehicleNumber = container.VehicleNumber,
                                Performed = datetime,
                                CreateDT = datetime,
                                MessageFrom = "AIT",
                                MessageTo = "WEBOC",
                                IsProcessed = false
                            };

                        giios.Add(giio);
                        


                    }



                    var resdata = _containerRepo.GetFileNameGIIO($"GIIO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}");

                    if (resdata == true)
                    {
                        return Json(new { error = true, message = "File Name Already Exist please update after one min" });

                    }

                    giios.ForEach(x => x.FileName = $"GIIO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}");
                     

                    _giioRepo.AddRange(giios);
                      
                    return Json(new { error = false, message = "Saved" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "The list contains a container which is already gatein . Please Re-Process After 10 mints" });
                }

            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = "Error! Please  try  again." });

            }

            return null;

        }


        public IActionResult UpdateEmptyGateOutContainers()
        {
            return View();
        }

        public JsonResult GetEmptyGateOutContainers(string containerno )
        {

            var res = _emptyGateOutContainerRepositroy.GetAll().Where(x=> x.ContainerNo == containerno).ToList();


            return Json(res);
        }


        public IActionResult UpdateEmptyGateOutContainersInfo(EmptyGateOutContainer model)
        {
            try
            {
                var res = _emptyGateOutContainerRepositroy.GetAll().Where(x => x.EmptyGateOutContainerId == model.EmptyGateOutContainerId).LastOrDefault();

                if (res != null)
                {
                    DateTime currentDate = DateTime.Now;

                    DateTime createdDate = res.CreatedDate ?? DateTime.Now;

                    TimeSpan tt = currentDate - createdDate;

                    int NrOftDays = Convert.ToInt32(tt.TotalDays);


                    if (NrOftDays > 35)
                    {
                        return new OkObjectResult(new { error = true, message = "request number of days > 35" });

                    }

                    res.ContainerCondition = model.ContainerCondition;
                    res.DeliveredYard = model.DeliveredYard;
                    res.DeliveredYardDate = model.DeliveredYardDate;
                    res.TransporterId = model.TransporterId;
                    res.VehicleNumber = model.VehicleNumber;

                    _emptyGateOutContainerRepositroy.Update(res);

                    return new OkObjectResult(new { error = false, message = "updated" });


                }

                return new OkObjectResult(new { error = true, message = "data not found" });

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }

        }


        #region CS Empty Container Received


        public IActionResult CSEmptyContainerReceivedView()
        {


            ViewData["ClearingAgents"] = _clearingAgentRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.Name, Value = v.ClearingAgentId.ToString() });


            ViewData["ISOCodes"] = _ISOCodeHeadRepository.GetAll()
                .Select(s => new SelectListItem { Text = s.ISOCodeHeadDescription.ToString().Trim().ToUpper(), Value = s.ISOCodeHeadId.ToString() });

            ViewData["Transporters"] = _transporterRepository.GetAll()
                .Select(s => new SelectListItem { Text = s.TransporterName.ToString().Trim().ToUpper(), Value = s.TransporterId.ToString() });

            return View();
        }

        public JsonResult GetAllCSEmptyContainerReceived()
        {
            var res = _cSEmptyContainerReceiveRepository.GetAll().Where(x => x.OutDate == null).ToList();

            return Json(res);

        }


        public IActionResult AddCSEmptyContainerReceived(CSEmptyContainerReceive model)
        {
            try
            {
                var res = _cSEmptyContainerReceiveRepository.GetAll().Where(x => x.ContainerNo == model.ContainerNo).LastOrDefault();
                if (res != null)
                {
                    if (res.OutDate == null)
                    {
                        return new OkObjectResult(new { error = true, message = "Container is already Received but not delivered" });
                    } 
                }
                 
                model.ReceivedDate = DateTime.Now;
                _cSEmptyContainerReceiveRepository.Add(model);

                return new OkObjectResult(new { error = false, message = "Save" });
 
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }
        }

        public IActionResult UpdateCSEmptyContainerReceived(CSEmptyContainerReceive model)
        {
            try
            {
                var res = _cSEmptyContainerReceiveRepository.GetAll().Where(x => x.CSEmptyContainerReceiveId == model.CSEmptyContainerReceiveId).LastOrDefault();
                if (res != null)
                {
                    if (res.OutDate != null)
                    {
                        return new OkObjectResult(new { error = true, message = "Container is already Received   delivered" });
                    }
                    if (res.IsInUse == true)
                    {
                        return new OkObjectResult(new { error = true, message = "Container is acossociated" });
                    }
                }

                res.ContainerNo = model.ContainerNo;
                res.Size = model.Size;
                res.OtherRemarks = model.OtherRemarks;
                res.Shift = model.Shift;
                res.TireWeight = model.TireWeight;
                res.TrukNo = model.TrukNo;
                res.DamageType = model.DamageType;
                _cSEmptyContainerReceiveRepository.Update(res);

                return new OkObjectResult(new { error = false, message = "Save" });

            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }
        }

        public JsonResult GetCSEmptyContainerReceivedById(long csemptyContainerReceiveId)
        {
            var res = _cSEmptyContainerReceiveRepository.GetAll().Where(x => x.CSEmptyContainerReceiveId == csemptyContainerReceiveId && x.OutDate == null && x.IsInUse == false).LastOrDefault();

            return Json(res);

        }

        #endregion

    }
}