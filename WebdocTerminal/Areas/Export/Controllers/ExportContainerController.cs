using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class ExportContainerController : ParentController
    {
        private IShippingLineRepository _shippingLineRepo;
        private IVesselExportRepository _vesselExportRepo;
        private IVoyageExportRepository _voyageExportRepo;
        private IExportContainerRepository _exportContainerRepo;
        private IExportContainerItemRepositpory _exportContainerItemRepo;
        private IGDHoldRepository _gDHoldRepository;
        private IOSVMRepository _oSVMRepository;
        private IOGDCRepository _ogdcRepo;
        private IOPIARepository oPIARepository;
        private IHostingEnvironment env;
        private ICargoRepository _cargoRepository;
        private IOptions<AppConfig> config;
        private WebocProcessor webocProcessor;
        private IDictionaryRepository _dictionaryRepo;
        private IOSIMRepository _osimRepo;
        private IEnteringCargoRepository _enteringCargoRepo;
        private IExportLocationCodeListRepository _locationRepo;
        private IShippingAgentExportRepository _shippingAgentExportRepo;
        private IShippingLineExportRepository  _ShippingLineExportRepository; 
        private IDeliveredYardExportRepository _deliveredYardExportRepository; 
        private ILoadingProgramRepository _loadingProgramRepository; 
        private IOCRLRepository _oCRLRepository;
        private ITransporterRepository _transporterRepository;



        public ExportContainerController(IShippingLineRepository shippingLineRepo, IExportContainerRepository exportContainerRepo
                                        , IVesselExportRepository vesselExportRepo, IVoyageExportRepository voyageExportRepo
                                        , IExportContainerItemRepositpory exportContainerItemRepo
                                        , IGDHoldRepository gDHoldRepository
                                        , IOSVMRepository oSVMRepository
                                        , IOGDCRepository ogdcRepo
                                        , WebocProcessor _webocProcessor
                                        , IContainerIndexRepository containerIndexRepo
                                        , IHostingEnvironment _env
                                        , IOptions<AppConfig> _config
                                        , IOPIARepository _oPIARepository
                                        , ICargoRepository cargoRepository
                                        , IDictionaryRepository dictionaryRepo
                                        , IOSIMRepository osimRepo
                                        , IEnteringCargoRepository enteringCargoRepo
                                        , IExportLocationCodeListRepository locationRepo
                                        , IShippingAgentExportRepository shippingAgentExportRepo
                                        , IShippingLineExportRepository ShippingLineExportRepository
                                        , IDeliveredYardExportRepository deliveredYardExportRepository
                                        , ILoadingProgramRepository loadingProgramRepository
                                        , IOCRLRepository oCRLRepository
                                        , ITransporterRepository transporterRepository)
        {
            _shippingLineRepo = shippingLineRepo;
            _exportContainerRepo = exportContainerRepo;
            _vesselExportRepo = vesselExportRepo;
            _voyageExportRepo = voyageExportRepo;
            _exportContainerItemRepo = exportContainerItemRepo;
            _gDHoldRepository = gDHoldRepository;
            _oSVMRepository = oSVMRepository;
            _ogdcRepo = ogdcRepo;
            oPIARepository = _oPIARepository;
            config = _config;
            _cargoRepository = cargoRepository;
            webocProcessor = _webocProcessor;
            env = _env;
            _dictionaryRepo = dictionaryRepo;
            _osimRepo = osimRepo;
            _enteringCargoRepo = enteringCargoRepo;
            _locationRepo = locationRepo;
            _shippingAgentExportRepo = shippingAgentExportRepo;
            _transporterRepository = transporterRepository;
            _ShippingLineExportRepository = ShippingLineExportRepository;
            _deliveredYardExportRepository = deliveredYardExportRepository;
            _loadingProgramRepository = loadingProgramRepository;
            _oCRLRepository = oCRLRepository;
        }
        public IActionResult CreateContainer()
        {
            ViewData["ShippingLine"] = _ShippingLineExportRepository.GetAll().Select(x => new SelectListItem { Text = x.ShippingLineName, Value = x.ShippingLineExportId.ToString() });

            ViewData["Destinations"] = _deliveredYardExportRepository.GetAll().Select(s => new SelectListItem { Text = s.DeliveredYardName, Value = s.DeliveredYardName });

            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll()
                .Select(s => new SelectListItem { Text = s.ShippingAgentName, Value = s.ShippingAgentExportId.ToString() });

            //ViewData["Transporter"] = _transporterRepository.GetAll().Select(t => new SelectListItem { Text = t.TransporterName, Value = t.TransporterId.ToString() });
            return View();
        }

        public IActionResult AddExportContainer(ExportContainer model)
        {
            model.ContainerNumber = model.ContainerNumber.Replace("-", "");

            var container = _exportContainerRepo.GetAll().Where(x => x.ContainerNumber == model.ContainerNumber && x.IsGateout == false).FirstOrDefault();
            if (container != null)
            {

                return new OkObjectResult(new PostObjectResponce { error = true, Message = "This Container Already Exist Please First Gate Out It" });

            }
            else
            {
                model.IsGateout = false;

                _exportContainerRepo.Add(model);

                return new OkObjectResult(new { error = false, Message = "Saved", ExportContainerId = model.ExportContainerId });

            }

        }

        public IActionResult DeleteExportContainer(long ExportContainerId)
        {
 
            var data = _exportContainerRepo.GetFirst(x => x.ExportContainerId == ExportContainerId);

            if (data != null)
            {
                var exportContainerItem = _exportContainerItemRepo.GetAll().Where(x => x.ExportContainerId == data.ExportContainerId).ToList();

                if (exportContainerItem.Count() > 0 )
                {
                    return new OkObjectResult(new PostObjectResponce { error = true, Message = "This Container Association Completed" });
                }
                
                _exportContainerRepo.Delete(data);
                return new OkObjectResult(new { error = false, Message = "delete"});
                 
            }
            else
            {

                return new OkObjectResult(new { error = true, Message = "no recored found"});

            }

        }
                public IActionResult UpdateExportContainer(ExportContainer model, long ExportContainerId)
        {
            model.ContainerNumber = model.ContainerNumber.Replace("-", "");

            var data = _exportContainerRepo.GetFirst(x => x.ExportContainerId == ExportContainerId);

            if (data != null)
            {
                var exportContainerItem = _exportContainerItemRepo.GetAll().Where(x => x.ExportContainerId == data.ExportContainerId).ToList();

                if (exportContainerItem.Count() > 0 )
                {
                    return new OkObjectResult(new PostObjectResponce { error = true, Message = "This Container Association Completed" });
                }


                data.ContainerSize = model.ContainerSize;
                data.ContainerNumber = model.ContainerNumber;
                data.Status = model.Status;
                data.VehicleNumber = model.VehicleNumber;
                data.ShippingLineExportId = model.ShippingLineExportId;
                data.CRONumber = model.CRONumber;
                data.ShippingAgentExportId = model.ShippingAgentExportId;
                data.VehicleNumber = model.VehicleNumber;
                data.RecevingDate = model.RecevingDate;
                data.ContainerTareWeight = model.ContainerTareWeight;
                data.ContainerCondition = model.ContainerCondition;
                data.EmptyReceivedFromYard = model.EmptyReceivedFromYard;

                _exportContainerRepo.Update(data);
                return new OkObjectResult(new { error = false, Message = "Saved", ExportContainerId = model.ExportContainerId });


            }
            else
            {

                return new OkObjectResult(new { error = true, Message = "no recored found", ExportContainerId = model.ExportContainerId });

            }

        }

        public IActionResult AssociateGD(long exportContainerId)
        {


            ViewData["VesselExport"] = _vesselExportRepo.GetAll().OrderBy(x => x.VesselName).Select(x => new SelectListItem { Text = x.VesselName, Value = x.VesselExportId.ToString() });

            ViewData["ExportContainer"] = _exportContainerRepo.GetList(s => s.IsGateout == false).OrderBy(x => x.ContainerNumber)
                .Select(x => new SelectListItem { Text = x.ContainerNumber, Value = x.ExportContainerId.ToString() });



            return View();
        }


        public JsonResult GetExportContainerItems(long vesselid, long voyageId, long exportContainerId)
        {
            var data = _exportContainerRepo.GetExportContainerItems(vesselid, voyageId, exportContainerId);
            if (data != null)
            {

                return Json(data);
            }
            return Json("");
        }


        public IActionResult AddExportContainerItems(List<ExportContainerItemsViewModel> models, long exportContainerId, bool ischecked, long vesselid, long voyageId, bool submit)
        {
            List<ExportContainerItem> exportContainerItems = new List<ExportContainerItem>();
            List<OGDC> ogdcs = new List<OGDC>();
            List<EnteringCargo> cargos = new List<EnteringCargo>();
            var count = 0;


            var exportcontainer = _exportContainerRepo.GetExportContainerById(exportContainerId);

            if (exportcontainer != null)
            {
                foreach (var item in models)
                {
                    var hold = _osimRepo.GetOSIMInfo(item.GDNumber);

                    if (hold != null && hold.Status == "HO")
                    {
                        return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "is Hold" });

                    }
                }


                if (ischecked == false)
                {


                    bool hasMatch = _exportContainerItemRepo.GetAll().Any(x => models.Any(y => y.GDNumber == x.GDNumber && x.ExportContainerId == exportContainerId));

                    if (!hasMatch)
                    {


                        //var containerItems = _exportContainerItemRepo.GetList(s => s.ExportContainerId == exportContainerId);
                        var containerItems = _exportContainerRepo.GetExportContainerItemsByExportContainerId(exportContainerId);

                        if (containerItems.Count() > 0)
                        {
                            if (containerItems.FirstOrDefault(c => c.VoyageExportId != voyageId) != null)
                            {
                                return Json(new { error = true, message = "GDs already associated for a vessel and cannot be changed" });
                            }
                        }


                        foreach (var item in models)
                        {
                            var data = _enteringCargoRepo.GetEnteringCargo(item.GDNumber);

                            if (data != null && data.IsHold == true)
                            {
                                return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "is Hold on HOLD From Terminal" });
                            }
                        }

                        foreach (var item in models.OrderBy(x => x.OrderNumber))
                        {

                            var exportContainerItem = new ExportContainerItem
                            {
                                AllowLoading = item.AllowLoading,
                                Destination = item.Destination,
                                ContainerNumber = exportcontainer != null ? exportcontainer.ContainerNumber : "",
                                ExportContainerId = exportContainerId,
                                GDNumber = item.GDNumber,
                                NumberOfPackages = item.NoOfPackages,
                                ShipperName = item.ShipperName,
                                Ischecked = ischecked,
                                CreatedDate = DateTime.Now,
                                VesselExportId = vesselid,
                                OrderNumber = item.OrderNumber,
                                //isAmountReceived = item.IsAmountReceived,
                                VoyageExportId = voyageId,
                                IsSubmitted = false
                            };



                            //  if (_exportContainerItemRepo.GetFirst(c => c.GDNumber.Trim().ToUpper() == item.GDNumber.Trim().ToUpper() && item.ExportContainerId == exportContainerId) == null)
                            //    exportContainerItems.Add(exportContainerItem);

                            var resdata = _exportContainerRepo.GetExportContainerItemsByExportContainerIdandGD(exportContainerId, item.GDNumber);
                            if (resdata == null)
                            {
                                exportContainerItems.Add(exportContainerItem);
                            }


                        }
                        _exportContainerItemRepo.AddRange(exportContainerItems);
                        //  _ogdcRepo.AddRange(ogdcs);
                        // GenerateTrminalNumber(count);
                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "GD's Already Saved" });
                    }





                }

                else
                {
                    var datetime = DateTime.Now;

                    var number = GetTrminalNumber();


                    var fn = $"OGDC_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}";
                    var fileName = _ogdcRepo.GetAll().Where(x => x.FileName == fn).FirstOrDefault();

                    if (fileName != null)
                    {
                        return new OkObjectResult(new PostObjectResponce { error = true, Message = "Please Proceed After One Minute" });
                    }

                    //var containerItems = _exportContainerItemRepo.GetList(s => s.ExportContainerId == exportContainerId && s.IsSubmitted ==  false);
                    var containerItems = _exportContainerItemRepo.GetUnSubbmitedGDs(exportContainerId);

                    foreach (var item in containerItems)
                    {
                        item.Ischecked = true;
                        item.IsSubmitted = true;
                        item.SubmitedDate = DateTime.Now;
                    }


                    foreach (var item in containerItems)
                    {


                        //var opia = oPIARepository.GetFirst(o => o.GDNumber == item.GDNumber && o.MessageFrom != "Manual");
                        var opia = oPIARepository.GetOPIAByGD(item.GDNumber);

                        // var container = _exportContainerRepo.GetFirst(c => c.ExportContainerId == exportContainerId);
                        var container = oPIARepository.GetExportContainerById(exportContainerId);

                        if (opia != null && container != null)
                        {
                            //var shippingline = _shippingLineRepo.GetFirst(s => s.ShippingLineId == container.ShippingLineId);
                            var shippingline = _ShippingLineExportRepository.GetShippingLineById(container.ShippingLineExportId ?? 0);

                            var status = "";

                            if (item == containerItems.OrderBy(x => x.OrderNumber).LastOrDefault())
                                status = "F";
                            else
                                status = "P";


                            var ogdc = new OGDC
                            {
                                ContainerNumber = container.ContainerNumber,
                                GDNumber = item.GDNumber,
                                NumberofPackages = Convert.ToInt32(item.NumberOfPackages),
                                ShippingLineCode = shippingline.ShippingLineCode,
                                ShippingLineName = shippingline.ShippingLineName,
                                ContainerConsolidationStatus = status,
                                OperationType = "A",
                                MessageFrom = config.Value.MessageFrom,
                                MessageTo = "WEBOC",
                                IsProcessed = opia != null ? opia.MessageFrom == "Manual" ? true : false : false,
                                FileName = fn
                            };

                            ogdcs.Add(ogdc);



                            //var cargs = _cargoRepository.GetList(x => x.GDNumber?.Trim().ToUpper() == item.GDNumber);
                            var cargs = _enteringCargoRepo.GetEnteringCargo(item.GDNumber);

                            if (cargs != null)
                            {
                                number = number + 1;
                                string day = datetime.ToString("dd");
                                string month = datetime.ToString("MM");
                                string year = datetime.ToString("yyyy");

                                cargs.TRNumber = "TR/" + number + "/" + month + "/" + day + "/" + year; ;
                                cargs.IssueDate = datetime;
                                cargos.Add(cargs);

                            }

                        }
                    }

                    //var container = _exportContainerRepo.Find(exportContainerId);

                    //ogdcs = _ogdcRepo.GetList(o => o.ContainerNumber == container.ContainerNumber).ToList();

                    _exportContainerItemRepo.UpdateRange(containerItems);

                    _enteringCargoRepo.UpdateRange(cargos);

                    _ogdcRepo.AddRange(ogdcs);
                    GenerateTrminalNumber(number);


                }




                return Json(new { error = false, message = "GDs Associated Successfully" });

            }
            else
            {
                return Json(new { error = true, message = "Container Not Found" });
            }


        }


        public int GetTrminalNumber()
        {
            var data = _dictionaryRepo.GetFirst(x => x.Key == "TerminalReceipt");
            if (data == null)
            {
                data.Value = "0001";
                _dictionaryRepo.Update(data);
                return 1;

            }
            var no = Convert.ToInt64(data.Value);
            data.Value = no.ToString();

            return Convert.ToInt32(data.Value);
        }

        public string GenerateTrminalNumber(long count)
        {



            var data = _dictionaryRepo.GetFirst(x => x.Key == "TerminalReceipt");
            if (data == null)
            {
                data.Value = 0001.ToString();
                _dictionaryRepo.Update(data);
                return "0001";

            }
            var no = Convert.ToInt64(data.Value) + count;
            data.Value = no.ToString();

            _dictionaryRepo.Update(data);

            return data.Value;
        }
        public IActionResult DeleteContainer()
        {
            return View();
        }

        public IActionResult DeletionOfContainerAssociated()
        {
            ViewData["ExportContainer"] = _exportContainerRepo.GetAll()
                 .Select(x => new SelectListItem { Text = x.ContainerNumber, Value = x.ExportContainerId.ToString() });

            return View();
        }

        public JsonResult GetExportContainerItemsBYContainerNumber(string containerNumber)
        {
            var data = _exportContainerRepo.GetExportContainerItemsBYContainerNumber(containerNumber);
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }


        public void DeleteContainerByExportContainerItemId(long exportContainerItemId)
        {
            var data = _exportContainerItemRepo.Find(exportContainerItemId);

            if (data.IsSubmitted != true)
            {
                _exportContainerItemRepo.Delete(data);
            }

        }


        public long GetGDHold(string gdNumber)
        {
            return (_gDHoldRepository.GetGDHold(gdNumber));

        }

        public IActionResult DeleteAssociatedGD()
        {
            ViewData["VesselExport"] = _vesselExportRepo.GetAll().OrderBy(x => x.VesselName).Select(x => new SelectListItem { Text = x.VesselName, Value = x.VesselExportId.ToString() });

            ViewData["ExportContainer"] = _exportContainerRepo.GetList(s => s.IsGateout == false).OrderBy(x => x.ContainerNumber)
                .Select(x => new SelectListItem { Text = x.ContainerNumber, Value = x.ExportContainerId.ToString() });


            return View();
        }

        public JsonResult GetExportContainerItemsAssociated(long vesselid, long voyageId, long exportContainerId)
        {
            var data = _exportContainerItemRepo.GetAll().Where(x => x.VesselExportId == vesselid && x.VoyageExportId == voyageId && x.ExportContainerId == exportContainerId && x.IsSubmitted == true && x.Ischecked == true && x.IsOvams == false).ToList();
            if (data.Count() > 0)
            {
                return Json(data);
            }
            return Json("");
        }

        public IActionResult AddExportContainerItem(ExportContainerItem gddata)
        {
            List<ExportContainerItem> exportContainerItems = new List<ExportContainerItem>();
            List<OGDC> ogdcs = new List<OGDC>();
            List<EnteringCargo> cargos = new List<EnteringCargo>();
            var count = 0;


            var datetime = DateTime.Now;



            var fn = $"OGDC_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}";
            var fileName = _ogdcRepo.GetAll().Where(x => x.FileName == fn).FirstOrDefault();

            if (fileName != null)
            {
                return new OkObjectResult(new PostObjectResponce { error = true, Message = "Please Proceed After One Minute" });
            }


            var containerItems = _exportContainerItemRepo.GetAll().Where(x => x.ExportContainerId == gddata.ExportContainerId && x.VesselExportId == gddata.VesselExportId && x.VoyageExportId == gddata.VoyageExportId && x.IsSubmitted == true && x.Ischecked == true && x.IsOvams == false).ToList();

            if (containerItems.Count() > 0)
            {
                var opia = oPIARepository.GetOPIAByGD(gddata.GDNumber);

                var container = oPIARepository.GetExportContainerById(gddata.ExportContainerId ?? 0);
                if (opia != null && container != null)
                {

                    var shippingline = _ShippingLineExportRepository.GetShippingLineById(container.ShippingLineExportId ?? 0);


                    if (shippingline != null)
                    {
                        var ogdc = new OGDC
                        {
                            ContainerNumber = container.ContainerNumber,
                            GDNumber = gddata.GDNumber,
                            NumberofPackages = Convert.ToInt32(gddata.NumberOfPackages),
                            ShippingLineCode = shippingline.ShippingLineCode,
                            ShippingLineName = shippingline.ShippingLineName,
                            ContainerConsolidationStatus = "F",
                            OperationType = "D",
                            MessageFrom = config.Value.MessageFrom,
                            MessageTo = "WEBOC",
                            IsProcessed = opia != null ? opia.MessageFrom == "Manual" ? true : false : false,
                            FileName = fn
                        };

                        ogdcs.Add(ogdc);



                        //var cargs = _cargoRepository.GetList(x => x.GDNumber?.Trim().ToUpper() == item.GDNumber);
                        var cargs = _enteringCargoRepo.GetEnteringCargo(gddata.GDNumber);

                        if (cargs != null)
                        {

                            cargs.TRNumber = "";
                            cargs.IssueDate = null;
                            cargos.Add(cargs);

                        }
                    }

                    else
                    {
                        return Json(new { error = true, message = "Line Not avaible" });
                    }


                }


                _exportContainerItemRepo.DeleteRange(containerItems);

                _enteringCargoRepo.UpdateRange(cargos);

                _ogdcRepo.AddRange(ogdcs);

                return Json(new { error = false, message = "GDs Associated Deleted Successfully" });

            }
            else
            {
                return Json(new { error = true, message = "no data found" });
            }




        }

        public IActionResult GdBindingDetail()
        { 
            return View();
        }

        public IActionResult AddGdBindingDetail(ExportContainerItem model)
        {
            if(model.NumberOfPackages <= 0 || model.NumberOfPackages == null)
            {
                return new OkObjectResult(new { error = true, message = " please add number of packages" });

            }

            var hold = _osimRepo.GetOSIMInfo(model.GDNumber);

            if (hold != null && hold.Status == "HO")
            {
                return new OkObjectResult(new { error = true, message = "This GD" + model.GDNumber + "is Hold" });

            }

            var data = _enteringCargoRepo.GetEnteringCargo(model.GDNumber);

            if (data != null && data.IsHold == true)
            {
                return new OkObjectResult(new { error = true, message = "This GD" + model.GDNumber + "is Hold on HOLD From Terminal" });
            }


            var opiasers = oPIARepository.GeLastGdnumberInfo(model.GDNumber);
            var loadingprogramers = _loadingProgramRepository.GetLoadingProgrambygdnumber(model.GDNumber);

            var ocrlres = _oCRLRepository.GetOcrlfromGdnumber(model.GDNumber);

            if(loadingprogramers  == null)
            {
                return new OkObjectResult(new { error = true, message = "This GD" + model.GDNumber + "info not avaible" });

            }
            else
            {
                if (loadingprogramers.VesselExportId == null || loadingprogramers.VoyageExportId == null || loadingprogramers.VesselExportId == 0 || loadingprogramers.VoyageExportId == 0)
                {
                    return new OkObjectResult(new { error = true, message = "This GD" + model.GDNumber + "Vesel Voyage not avaible" });

                }
            }

            if (ocrlres == null )
            {
                return new OkObjectResult(new { error = true, message = "This GD" + model.GDNumber + "OCRL not avaible" });

            }

            if (opiasers != null )
            {

                var hasMatch = _exportContainerItemRepo.GetAll().Where(x => x.GDNumber == model.GDNumber && x.ExportContainerId == model.ExportContainerId).ToList();

                if (hasMatch.Count() == 0)
                {


                    var containerItems = _exportContainerRepo.GetExportContainerItemsByExportContainerId(model.ExportContainerId ?? 0);

                    if (containerItems.Count() > 0)
                    {
                        if (containerItems.FirstOrDefault(c => c.VoyageExportId != loadingprogramers.VoyageExportId) != null)
                        {
                            return Json(new { error = true, message = "This is not possible to assign multiple vessel voyages simultaneously with this container." });
                        }
                    }
                     
                    var totaldelivered = _exportContainerItemRepo.GetAll().Where(x => x.GDNumber == model.GDNumber).Select(l => l.NumberOfPackages).DefaultIfEmpty(0).Sum() + model.NumberOfPackages;

                    var balancepackages = opiasers.NoOfPackages - totaldelivered;
                     
                    if(balancepackages < 0)
                    {
                        return Json(new { error = true, message = "please check your packages exceed" });

                    }

                    var exportContainerItem = new ExportContainerItem
                    {
                        AllowLoading = ocrlres.Status,
                        Destination = loadingprogramers.Destination,
                        ContainerNumber = model.ContainerNumber,
                        ExportContainerId = model.ExportContainerId,
                        GDNumber = model.GDNumber,
                        NumberOfPackages = model.NumberOfPackages,
                        ShipperName = "",
                        Ischecked = false,
                        CreatedDate = DateTime.Now,
                        VesselExportId = loadingprogramers.VesselExportId,
                        OrderNumber = null, 
                        VoyageExportId = loadingprogramers.VoyageExportId,
                        IsSubmitted = false
                    };
                     

                    var resdata = _exportContainerRepo.GetExportContainerItemsByExportContainerIdandGD(model.ExportContainerId ?? 0, model.GDNumber);
                    if (resdata != null)
                    {
                        return new OkObjectResult(new { error = true, message = "GD's Already Bind with same container" });
                    } 

                    _exportContainerItemRepo.Add(exportContainerItem);

                    //if(balancepackages == 0)
                    //{
                    //    loadingprogramers.GDAssociateStatus = true;
                    //    _loadingProgramRepository.Update(loadingprogramers);
                    //}
                    

                    return new OkObjectResult(new { error = false, message = "Save Info" });
                     
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "GD's Already Bind with same container" });
                }
            }
            else
            {
                return new OkObjectResult(new { error = true, message = "Gd Not found" });

            } 
        }

        public IActionResult DeleteGdBindingDetail(long key)
        {
            try
            {
                

                var res = _exportContainerItemRepo.GetAll().Where(x => x.ExportContainerItemId == key).LastOrDefault();

                if(res != null)
                {
                    var loadingprogramers = _loadingProgramRepository.GetLoadingProgrambygdnumber(res.GDNumber);

                    if (loadingprogramers == null)
                    {
                        return new OkObjectResult(new { error = true, message = "This GD" + res.GDNumber + "info not avaible" });

                    }

                    if (res.Ischecked == true)
                    {
                        return new OkObjectResult(new { error = true, message = "gd associated" });
                    }
                    else
                    { 
                        _exportContainerItemRepo.Delete(res);

                        //if(loadingprogramers.GDAssociateStatus == true)
                        //{
                        //    loadingprogramers.GDAssociateStatus = false;
                        //    _loadingProgramRepository.Update(loadingprogramers);
                        //}
                        return new OkObjectResult(new { error = false, message = "Deleted" });

                    }

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

        public IActionResult AddGdBindingDetailList(List<ExportContainerItem> model )
        {
 

            var hold = _osimRepo.GetOSIMInfo(model[0].GDNumber);

            if (hold != null && hold.Status == "HO")
            {
                return new OkObjectResult(new { error = true, message = "This GD" + model[0].GDNumber + "is Hold" });
            }

            var data = _enteringCargoRepo.GetEnteringCargo(model[0].GDNumber);

            if (data != null && data.IsHold == true)
            {
                return new OkObjectResult(new { error = true, message = "This GD" + model[0].GDNumber + "is Hold on HOLD From Terminal" });
            }


            var opiasers = oPIARepository.GeLastGdnumberInfo(model[0].GDNumber);
            var loadingprogramers = _loadingProgramRepository.GetLoadingProgrambygdnumber(model[0].GDNumber);

            var ocrlres = _oCRLRepository.GetOcrlfromGdnumber(model[0].GDNumber);

            if (loadingprogramers == null)
            {
                return new OkObjectResult(new { error = true, message = "This GD" + model[0].GDNumber + "info not avaible" });

            }
            else
            {
                if (loadingprogramers.VesselExportId == null || loadingprogramers.VoyageExportId == null || loadingprogramers.VesselExportId == 0 || loadingprogramers.VoyageExportId == 0)
                {
                    return new OkObjectResult(new { error = true, message = "This GD" + model[0].GDNumber + "Vesel Voyage not avaible" });

                }
            }

            if (ocrlres == null)
            {
                return new OkObjectResult(new { error = true, message = "This GD" + model[0].GDNumber + "OCRL not avaible" });

            }

            if (opiasers != null)
            {  
                    var balancepackages = opiasers.NoOfPackages - _exportContainerItemRepo.GetAll().Where(x => x.GDNumber == model[0].GDNumber).Select(l => l.NumberOfPackages).DefaultIfEmpty(0).Sum();

                    if (balancepackages < 0)
                    {
                        return Json(new { error = true, message = "please check your packages exceed" });

                    }
                     
                model.ForEach(x => x.Ischecked = true);

                  var resdata =  _exportContainerRepo.UpdateIscheckStatus(model);

                if(resdata == "true")
                {
                    if (balancepackages == 0)
                    {
                        loadingprogramers.GDAssociateStatus = true;
                        _loadingProgramRepository.Update(loadingprogramers);
                    }

                    return new OkObjectResult(new { error = false, message = "Save Info" });

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = resdata });


                }



            }
                else
                {
                    return new OkObjectResult(new { error = true, message = "GD's Already Bind with same container" });
                }

        }


        public IActionResult AddGdBindingDetailListContainerwise(List<ExportContainerItem> models)
        {

            foreach (var item in models)
            {
                var hold = _osimRepo.GetOSIMInfo(item.GDNumber);

                if (hold != null && hold.Status == "HO")
                {
                    return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "is Hold" });
                }

                var data = _enteringCargoRepo.GetEnteringCargo(item.GDNumber);

                if (data != null && data.IsHold == true)
                {
                    return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "is Hold on HOLD From Terminal" });
                } 
            }

            foreach (var item in models)
            {
                var opiasers = oPIARepository.GeLastGdnumberInfo(item.GDNumber);
                var loadingprogramers = _loadingProgramRepository.GetLoadingProgrambygdnumber(item.GDNumber);

                var ocrlres = _oCRLRepository.GetOcrlfromGdnumber(item.GDNumber);

                if (loadingprogramers == null)
                {
                    return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "info not avaible" });

                }
                else
                {
                    if (loadingprogramers.VesselExportId == null || loadingprogramers.VoyageExportId == null || loadingprogramers.VesselExportId == 0 || loadingprogramers.VoyageExportId == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "This GD " + item.GDNumber + " Vesel Voyage not avaible" });

                    }
                }

                if (ocrlres == null)
                {
                    return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "OCRL not avaible" });

                }

                if (opiasers != null)
                {
                    var balancepackages = opiasers.NoOfPackages - _exportContainerItemRepo.GetAll().Where(x => x.GDNumber == item.GDNumber).Select(l => l.NumberOfPackages).DefaultIfEmpty(0).Sum();

                    if (balancepackages < 0)
                    {
                        return Json(new { error = true, message = "please check your packages exceed" });
                    } 
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "GD's Already Bind with same container" });
                }
            }

            var loadindprograms = new List<LoadingProgram>();
            var exportcontaineritems = new List<ExportContainerItem>();
            foreach (var item in models)
            {
                var opiasers = oPIARepository.GeLastGdnumberInfo(item.GDNumber);

                var loadingprogramers = _loadingProgramRepository.GetLoadingProgrambygdnumber(item.GDNumber);

                var balancepackages = opiasers.NoOfPackages - _exportContainerItemRepo.GetAll().Where(x => x.GDNumber == item.GDNumber).Select(l => l.NumberOfPackages).DefaultIfEmpty(0).Sum();
                  
                if (balancepackages == 0)
                {
                    loadingprogramers.GDAssociateStatus = true;
                    loadindprograms.Add(loadingprogramers);
                }

            }

            models.ForEach(x => x.Ischecked = true);

            var resdata = _exportContainerRepo.UpdateIscheckStatus(models);

            if (resdata == "true")
            {
                _loadingProgramRepository.UpdateRange(loadindprograms);


                return new OkObjectResult(new { error = false, message = "Save Info" });

            }
            else
            {
                return new OkObjectResult(new { error = true, message = resdata });


            }

 
            return new OkObjectResult(new { error = false, message = "Save Info" });



        }


        public IActionResult GDAssociationView()
        {
            return View();
        }

        public IActionResult AddGDAssociationList(List<ExportContainerItem> models)
        {


             
                var datetime = DateTime.Now;

                //var number = GetTrminalNumber();


                var fn = $"OGDC_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}";
                var fileName = _ogdcRepo.GetAll().Where(x => x.FileName == fn).FirstOrDefault();

                if (fileName != null)
                {
                    return new OkObjectResult(new PostObjectResponce { error = true, Message = "Please Proceed After One Minute" });
                }

            //var containerItems = _exportContainerItemRepo.GetList(s => s.ExportContainerId == exportContainerId && s.IsSubmitted ==  false);
             


            foreach (var item in models)
            {
                var hold = _osimRepo.GetOSIMInfo(item.GDNumber);

                if (hold != null && hold.Status == "HO")
                {
                    return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "is Hold" });
                }

                var data = _enteringCargoRepo.GetEnteringCargo(item.GDNumber);

                if (data != null && data.IsHold == true)
                {
                    return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "is Hold on HOLD From Terminal" });
                }
            }

            foreach (var item in models)
            {
                var opiasers = oPIARepository.GeLastGdnumberInfo(item.GDNumber);
                var loadingprogramers = _loadingProgramRepository.GetLoadingProgrambygdnumber(item.GDNumber);

                var ocrlres = _oCRLRepository.GetOcrlfromGdnumber(item.GDNumber);

                if (loadingprogramers == null)
                {
                    return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "info not avaible" });

                }
                else
                {
                    if (loadingprogramers.VesselExportId == null || loadingprogramers.VoyageExportId == null || loadingprogramers.VesselExportId == 0 || loadingprogramers.VoyageExportId == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "This GD " + item.GDNumber + " Vesel Voyage not avaible" });

                    }
                }

                if (ocrlres == null)
                {
                    return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "OCRL not avaible" });

                }

                if (opiasers != null)
                {
                    var balancepackages = opiasers.NoOfPackages - _exportContainerItemRepo.GetAll().Where(x => x.GDNumber == item.GDNumber).Select(l => l.NumberOfPackages).DefaultIfEmpty(0).Sum();

                    if (balancepackages < 0)
                    {
                        return Json(new { error = true, message = "please check your packages exceed" });
                    }
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "GD's Already Bind with same container" });
                }
            }

            var loadindprograms = new List<LoadingProgram>();
            var exportcontaineritems = new List<ExportContainerItem>();
            var ogdcs = new List<OGDC>();
            var osvms = new List<OSVM>();
            var EnteringCargos = new List<EnteringCargo>();
            foreach (var item in models)
            {
                var opiasers = oPIARepository.GeLastGdnumberInfo(item.GDNumber);

                var loadingprogramers = _loadingProgramRepository.GetLoadingProgrambygdnumber(item.GDNumber);

                var balancepackages = opiasers.NoOfPackages - _exportContainerItemRepo.GetAll().Where(x => x.GDNumber == item.GDNumber).Select(l => l.NumberOfPackages).DefaultIfEmpty(0).Sum();

                if (balancepackages == 0 && loadingprogramers.GDAssociateStatus == true)
                {
                    loadingprogramers.GDSubmitedStatus = true;
                    loadindprograms.Add(loadingprogramers);
                }

                var cargs = _enteringCargoRepo.GetEnteringCargo(item.GDNumber);

                if (cargs != null)
                {
                    //number = number + 1;
                    string day = datetime.ToString("dd");
                    string month = datetime.ToString("MM");
                    string year = datetime.ToString("yyyy");

                    cargs.TRNumber = "TR/" + cargs.GDNumber + "/" + month + "/" + day + "/" + year; ;
                    cargs.IssueDate = datetime;
                    EnteringCargos.Add(cargs);

                }

                var ogdc = new OGDC
                {
                    ContainerNumber = item.ContainerNumber,
                    GDNumber = item.GDNumber,
                    NumberofPackages = Convert.ToInt32(item.NumberOfPackages),
                    ShippingLineCode = "",//shippingline.ShippingLineCode,
                    ShippingLineName = "",//shippingline.ShippingLineName,
                    //ContainerConsolidationStatus = opiasers != null ? opiasers.MessageFrom == "Manual" ? "P" :  "F" : "P",
                    OperationType = "A",
                    MessageFrom = config.Value.MessageFrom,
                    MessageTo = "WEBOC",
                    IsProcessed = opiasers != null ? opiasers.MessageFrom == "Manual" ? true : false : false,
                    FileName = fn
                };

                ogdcs.Add(ogdc);

                if(opiasers != null && opiasers.MessageFrom == "Manual" )
                {
                    var OSVM = new OSVM
                    {
                        GDNumber = item.GDNumber,
                        ContainerNo = item.ContainerNumber,
                        OPType = "A",
                        MessageFrom = "Manual",
                        MessageTo = config.Value.MessageFrom,
                        Performed = DateTime.Now,
                        FileName = $"OSVM_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                    };

                    osvms.Add(OSVM);
                }


            }

            models.ForEach(x => x.IsSubmitted = true);
            models.ForEach(x => x.IsOvams = true);
            ogdcs.ForEach(x => x.ContainerConsolidationStatus = "P");

            if(ogdcs.Where(x => x.IsProcessed == false).Count() == 0)
            {
                ogdcs.LastOrDefault().ContainerConsolidationStatus = "F";
            }
            else
            {
                ogdcs.Where(x => x.IsProcessed == false).LastOrDefault().ContainerConsolidationStatus = "F";
            }

            var resdata = _exportContainerRepo.UpdateIscheckStatus(models);

            if (resdata == "true")
            {

                _enteringCargoRepo.UpdateRange(EnteringCargos);
                _ogdcRepo.AddRange(ogdcs);
                //GenerateTrminalNumber(number);
                _loadingProgramRepository.UpdateRange(loadindprograms);
                _oSVMRepository.AddRange(osvms);


                return new OkObjectResult(new { error = false, message = "Save Info" });

            }
            else
            {
                return new OkObjectResult(new { error = true, message = resdata });


            }


            return new OkObjectResult(new { error = false, message = "Save Info" });



        }

        public IActionResult AddMultipleGdBindingDetail(long exportcontainerid ,  List<ExportContainerItem> model)
        {

            try
            {
                var exportcontaineritems = new List<ExportContainerItem>();

                var exportcontainerres = _exportContainerRepo.GetExportContainerByID(exportcontainerid);

                if (exportcontainerres == null)
                {
                    return new OkObjectResult(new { error = true, message = "container not avaible" });

                }

                foreach (var item in model)
                {
                    var hold = _osimRepo.GetOSIMInfo(item.GDNumber);

                    if (hold != null && hold.Status == "HO")
                    {
                        return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "is Hold" });

                    }

                    var data = _enteringCargoRepo.GetEnteringCargo(item.GDNumber);

                    if (data != null && data.IsHold == true)
                    {
                        return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "is Hold on HOLD From Terminal" });
                    }


                    var loadingprogramers = _loadingProgramRepository.GetLoadingProgrambygdnumber(item.GDNumber);

                    var ocrlres = _oCRLRepository.GetOcrlfromGdnumber(item.GDNumber);

                    if (loadingprogramers == null)
                    {
                        return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "info not avaible" });

                    }
                    else
                    {
                        if (loadingprogramers.VesselExportId == null || loadingprogramers.VoyageExportId == null || loadingprogramers.VesselExportId == 0 || loadingprogramers.VoyageExportId == 0)
                        {
                            return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "Vesel Voyage not avaible" });

                        }
                    }

                    if (ocrlres == null)
                    {
                        return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "OCRL not avaible" });

                    }


                }

                foreach (var item in model)
                {
                    var opiasers = oPIARepository.GeLastGdnumberInfo(item.GDNumber);

                    var loadingprogramers = _loadingProgramRepository.GetLoadingProgrambygdnumber(item.GDNumber);


                    if (opiasers != null)
                    {

                        var resdata = _exportContainerRepo.GetExportContainerItemsByExportContainerIdandGD(exportcontainerres.ExportContainerId, item.GDNumber);
                        if (resdata != null)
                        {
                            return new OkObjectResult(new { error = true, message = "GD's Already Bind with same container" });
                        }

                        var hasMatch = _exportContainerItemRepo.GetAll().Where(x => x.GDNumber == item.GDNumber && x.ExportContainerId == exportcontainerres.ExportContainerId).ToList();

                        if (hasMatch.Count() == 0)
                        {


                            var containerItems = _exportContainerRepo.GetExportContainerItemsByExportContainerId(exportcontainerres.ExportContainerId);

                            if (containerItems.Count() > 0)
                            {
                                if (containerItems.FirstOrDefault(c => c.VoyageExportId != loadingprogramers.VoyageExportId) != null)
                                {
                                    return Json(new { error = true, message = "This is not possible to assign multiple vessel voyages simultaneously with this container." });
                                }
                            }

                            var totaldelivered = _exportContainerItemRepo.GetAll().Where(x => x.GDNumber == item.GDNumber).Select(l => l.NumberOfPackages).DefaultIfEmpty(0).Sum() + item.NumberOfPackages;

                            var balancepackages = opiasers.NoOfPackages - totaldelivered;

                            if (balancepackages < 0)
                            {
                                return Json(new { error = true, message = "please check your packages exceed" });

                            }

                            var exportContainerItem = new ExportContainerItem
                            {
                                AllowLoading = "AL",
                                Destination = loadingprogramers.Destination,
                                ContainerNumber = exportcontainerres.ContainerNumber,
                                ExportContainerId = exportcontainerres.ExportContainerId,
                                GDNumber = item.GDNumber,
                                NumberOfPackages = item.NumberOfPackages,
                                ShipperName = "",
                                Ischecked = false,
                                CreatedDate = DateTime.Now,
                                VesselExportId = loadingprogramers.VesselExportId,
                                OrderNumber = null,
                                VoyageExportId = loadingprogramers.VoyageExportId,
                                IsSubmitted = false
                            };



                            exportcontaineritems.Add(exportContainerItem);



 
                        }
                        else
                        {
                            return new OkObjectResult(new { error = true, message = "GD's Already Bind with same container" });
                        }
                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "Gd Not found" });

                    }
                }

                if(exportcontaineritems.Count() > 0)
                {
                    var hasMatch = exportcontaineritems.Where(x => exportcontaineritems.Any(y => y.VoyageExportId != x.VoyageExportId)).LastOrDefault();

                    if(hasMatch != null)
                    {
                        return new OkObjectResult(new { error = true, message = "GD " + hasMatch.GDNumber + " has different Vessel Voyage" });

                    }

                    _exportContainerItemRepo.AddRange(exportcontaineritems);

                    return new OkObjectResult(new { error = false, message = "Save" });

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "Gd Not found" });

                }

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }


      
          



        }




    }
}