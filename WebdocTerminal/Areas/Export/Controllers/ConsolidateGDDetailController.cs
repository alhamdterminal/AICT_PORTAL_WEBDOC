using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class ConsolidateGDDetailController : ParentController
    {

        private IVoyageExportRepository _voyageExportRepo;
        private IVesselExportRepository _vesselExportRepo;
        private ILoadingProgramRepository _loadingProgramRepo;
        private ILoadingProgramDetailRepository _loadingProgramDetailRepo;
        private IShippingLineRepository _shippingLineRepo;
        private IShipperRepository _shipperRepo;
        private IClearingAgentExportRepository _clearingAgentExportRepo;
        private IPortAndTerminalRepository _portAndTerminalRepo;
        private IShippingAgentExportRepository _shippingAgentExportRepo;
        private IExportLocationCodeListRepository _locationRepo;
        private ICargoRepository _cargoRepo;
        private IEnteringCargoRepository _enteringCargoRepository;
        private IOGIERepository _oGIERepository;
        private ICommodityRepository _commodityRepository;
        private IExportLocationCodeListRepository _exportLocationCodeListRepository;
        private IShippingLineExportRepository  _shippingLineExportRepository;
        private IGoodsHeadExportRepository _goodsHeadExportRepository;
        private IClearingAgentExportRepository _clearingAgentExportRepository;
        private IExportConsigneeRepository _exportConsigneeRepository;
        private IPortAndTerminalExportRepository  _portAndTerminalExportRepository;
        private IGateInExportRepository _gateInExportRepository;



        public ConsolidateGDDetailController(IVoyageExportRepository voyageExportRepo,
                                            ICargoRepository cargoRepo,
                                            IVesselExportRepository vesselExportRepo
                                        , ILoadingProgramRepository loadingProgramRepo, IShippingLineRepository shippingLineRepo
                                        , IShipperRepository shipperRepo, IClearingAgentExportRepository clearingAgentExportRepo
                                        , ILoadingProgramDetailRepository loadingProgramDetailRepo, IPortAndTerminalRepository portAndTerminalRepo
                                        , IShippingAgentExportRepository shippingAgentExportRepo
                                        , IExportLocationCodeListRepository locationRepo
                                        , IEnteringCargoRepository enteringCargoRepository,
                                            IOGIERepository oGIERepository,
                                            ICommodityRepository commodityRepository,
                                            IExportLocationCodeListRepository exportLocationCodeListRepository,
                                            IShippingLineExportRepository shippingLineExportRepository,
                                            IGoodsHeadExportRepository goodsHeadExportRepository,
                                            IClearingAgentExportRepository clearingAgentExportRepository,
                                            IExportConsigneeRepository exportConsigneeRepository,
                                            IPortAndTerminalExportRepository portAndTerminalExportRepository,
                                            IGateInExportRepository gateInExportRepository)
        {
            _cargoRepo = cargoRepo;
            _voyageExportRepo = voyageExportRepo;
            _vesselExportRepo = vesselExportRepo;
            _loadingProgramRepo = loadingProgramRepo;
            _loadingProgramDetailRepo = loadingProgramDetailRepo;
            _shippingLineRepo = shippingLineRepo;
            _shipperRepo = shipperRepo;
            _clearingAgentExportRepo = clearingAgentExportRepo;
            _portAndTerminalRepo = portAndTerminalRepo;
            _shippingAgentExportRepo = shippingAgentExportRepo;
            _locationRepo = locationRepo;
            _enteringCargoRepository = enteringCargoRepository;
            _oGIERepository = oGIERepository;
            _commodityRepository = commodityRepository;
            _exportLocationCodeListRepository = exportLocationCodeListRepository;
            _shippingLineExportRepository = shippingLineExportRepository;
            _goodsHeadExportRepository = goodsHeadExportRepository;
            _clearingAgentExportRepository = clearingAgentExportRepository;
            _exportConsigneeRepository = exportConsigneeRepository;
            _portAndTerminalExportRepository = portAndTerminalExportRepository;
            _gateInExportRepository = gateInExportRepository;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ConsolidateGdDetailView()
        {
            ViewData["VesselExports"] = _vesselExportRepo.GetAll().OrderBy(s => s.VesselName)
          .Select(s => new SelectListItem { Text = s.VesselName, Value = s.VesselExportId.ToString() });

            ViewData["UnSetteledGDs"] = _oGIERepository.GetUnSetteledGDNumbers()
          .Select(s => new SelectListItem { Text = s.GDNumber, Value = s.GDNumber });

            ViewData["ShippingLine"] = _shippingLineExportRepository.GetAll().OrderBy(s => s.ShippingLineName)
                .Select(s => new SelectListItem { Text = s.ShippingLineName, Value = s.ShippingLineExportId.ToString() });

            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().OrderBy(s => s.ShippingAgentName)
                .Select(s => new SelectListItem { Text = s.ShippingAgentName, Value = s.ShippingAgentExportId.ToString() });

            ViewData["Destinations"] = _locationRepo.GetAll().Select(s => new SelectListItem { Text = s.PortName, Value = s.PortName });

            ViewData["GoodsHeadName"] = _goodsHeadExportRepository.GetAll().Select(s => new SelectListItem { Text = s.GoodsHeadName, Value = s.GoodsHeadExportId.ToString() });

            ViewData["Consignee"] = _exportConsigneeRepository.GetAll().Select(s => new SelectListItem { Text = s.ConsigneName, Value = s.ExportConsigneeId.ToString() });

            ViewData["Port"] = _portAndTerminalExportRepository.GetAll().Select(s => new SelectListItem { Text = s.PortName, Value = s.PortAndTerminalExportId.ToString() });

            ViewData["CLearingagent"] = _clearingAgentExportRepository.GetAll().Select(s => new SelectListItem { Text = s.ClearingAgentName, Value = s.ClearingAgentExportId.ToString() });

            return View();
        }

        public IActionResult AddLoadingProgram(LoadingProgram values, List<LoadingProgramDetail> loadingProgramDetail)
        {


            var res = _loadingProgramRepo.GetLoadingProgrambygdnumber(values.GDNumber);

            if (res != null)
            {
                return new OkObjectResult(new PostObjectResponce { error = true, Message = "Already Info updated" });

            }

            List<LoadingProgramDetail> loadingProgramDetails = new List<LoadingProgramDetail>();

            _loadingProgramRepo.Add(values);



            foreach (var item in loadingProgramDetail)
            {
                var loadingProgm = new LoadingProgramDetail
                {

                    DOCSDate = item.DOCSDate,
                    INSDate = item.INSDate,
                    LoadingProgramId = values.LoadingProgramId,
                    PackageType = item.PackageType,
                    PONumber = item.PONumber,
                    Remarks = item.Remarks,
                    Style = item.Style,
                    TotalPackages = item.TotalPackages,
                    TotalPieces = item.TotalPieces,
                    PLIDNumber = item.PLIDNumber,
                    WarehouseLocation = item.WarehouseLocation,
                    CBM = item.CBM,
                    Weight = item.Weight,
                    Location = item.Location,
                    ColorCode = item.ColorCode,
                };

                loadingProgramDetails.Add(loadingProgm);

            }

            _loadingProgramDetailRepo.AddRange(loadingProgramDetails);


            var entringcargo = new EnteringCargo
            {
                LoadingProgramId = values.LoadingProgramId,
                GDNumber = values.GDNumber,
                isGrounded = false

            };
            _enteringCargoRepository.Add(entringcargo);

            return new OkObjectResult(new PostObjectResponce { error = false, Id = values.LoadingProgramId, Message = "Saved" });

        }


        public IActionResult ConsolidateGdView()
        {

            return View();
        }



        public IActionResult UpdateConsolidateGdView()
        {
            ViewData["VesselExports"] = _vesselExportRepo.GetAll().OrderBy(s => s.VesselName)
              .Select(s => new SelectListItem { Text = s.VesselName, Value = s.VesselExportId.ToString() });

            ViewData["ShippingLine"] = _shippingLineExportRepository .GetAll().OrderBy(s => s.ShippingLineName)
                .Select(s => new SelectListItem { Text = s.ShippingLineName, Value = s.ShippingLineExportId.ToString() });

            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().OrderBy(s => s.ShippingAgentName)
                .Select(s => new SelectListItem { Text = s.ShippingAgentName, Value = s.ShippingAgentExportId.ToString() });

            ViewData["Destinations"] = _locationRepo.GetAll().Select(s => new SelectListItem { Text = s.PortName, Value = s.PortName });

            ViewData["GoodsHeadName"] = _goodsHeadExportRepository.GetAll().Select(s => new SelectListItem { Text = s.GoodsHeadName, Value = s.GoodsHeadExportId.ToString() });


            ViewData["Consignee"] = _exportConsigneeRepository.GetAll().Select(s => new SelectListItem { Text = s.ConsigneName, Value = s.ExportConsigneeId.ToString() });

            ViewData["Port"] = _portAndTerminalExportRepository.GetAll().Select(s => new SelectListItem { Text = s.PortName, Value = s.PortAndTerminalExportId.ToString() });

            ViewData["CLearingagent"] = _clearingAgentExportRepository.GetAll().Select(s => new SelectListItem { Text = s.ClearingAgentName, Value = s.ClearingAgentExportId.ToString() });


            return View();
        }


        public IActionResult UpdateConsolidateinfo(LoadingProgram res, long loadingProgramId)
        {

            var data = _loadingProgramRepo.GetLoadingProgramgInfobyid(loadingProgramId);

            if (data != null)
            {
                data.CargoCutOff = res.CargoCutOff;
                data.CargoRecevingCondition = res.CargoRecevingCondition;
                data.ClearingAgentCNIC = res.ClearingAgentCNIC;
                data.ClearingAgentExportId = res.ClearingAgentExportId;
                data.CLearingAgentReprsentative = res.CLearingAgentReprsentative;
                data.Destination = res.Destination;
                data.DriverCNIC = res.DriverCNIC;
                data.DriverName = res.DriverName;
                data.FinalDestination = res.FinalDestination;
                data.GateInDate = res.GateInDate;
                data.InvoiceDate = res.InvoiceDate;
                data.FinalDestination = res.FinalDestination;
                data.InvoiceNumber = res.InvoiceNumber;
                data.LoadingProgramDate = res.LoadingProgramDate;
                data.LoadingTerminal = res.LoadingTerminal;
                //data.PortAndTerminalId = res.PortAndTerminalId;
                data.RecevingEndTime = res.RecevingEndTime;
                data.RecevingStartTime = res.RecevingStartTime;
                data.ReferenceNumber = res.ReferenceNumber;
                //data.ShipperId = res.ShipperId;
                data.CommodityName = res.CommodityName;
                data.ShipperSeal = res.ShipperSeal;
                data.ShippingAgentExportId = res.ShippingAgentExportId;
                data.ShippingLineExportId = res.ShippingLineExportId;
                data.VesselETD = res.VesselETD;
                data.VesselExportId = res.VesselExportId;
                data.VoyageExportId = res.VoyageExportId;
                data.GoodsHeadExportId = res.GoodsHeadExportId;
                data.CargoType = res.CargoType;
                data.ExportConsigneeId = res.ExportConsigneeId;
                data.ClearingAgentExportId = res.ClearingAgentExportId;
                data.PortAndTerminalExportId = res.PortAndTerminalExportId;
                //data.DischargeCountry = res.DischargeCountry;
                _loadingProgramRepo.Update(data);

                return new OkObjectResult(new PostObjectResponce { error = false, Message = "Updated" });
            }
            else
            {
                return new OkObjectResult(new PostObjectResponce { error = true, Message = "no record found" });
            }
        }


        public IActionResult UpdateConsolidateDetailinfo(LoadingProgramDetail res)
        {
            _loadingProgramDetailRepo.Update(res);

            return new OkObjectResult(new PostObjectResponce { error = false, Message = "Updated" });
        }


        public IActionResult AddLPDetail(long loadingProgramId, LoadingProgramDetail res)
        {
            try
            {
                res.LoadingProgramId = loadingProgramId;

                _loadingProgramDetailRepo.Add(res);

                return new OkObjectResult(new PostObjectResponce { error = false, Message = "Add" });

            }
            catch (Exception e)
            {
                return new OkObjectResult(new PostObjectResponce { error = true, Message = e.InnerException.InnerException.Message });

            }

        }

        public IActionResult CargoDropOffTicketView()
        {
            ViewData["GDNumbers"] = _enteringCargoRepository.GetAlongSideGDs()
                .Select(s => new SelectListItem { Text = s.GDNumber, Value = s.GDNumber.ToString() }).GroupBy(x => x.Text).Select(x => x.FirstOrDefault()); ;


            return View();
        }

        public JsonResult GetGateInGDsDetail(string res)
        {

            var resdata = _gateInExportRepository.GetAll().Where(x => x.GDNumber == res).ToList();

            return Json(resdata);
        }
    }
}
