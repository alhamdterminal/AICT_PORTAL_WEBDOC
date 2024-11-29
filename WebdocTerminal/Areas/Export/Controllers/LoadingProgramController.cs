using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class LoadingProgramController : ParentController
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


        public LoadingProgramController(IVoyageExportRepository voyageExportRepo,
                                            ICargoRepository cargoRepo,
                                            IVesselExportRepository vesselExportRepo
                                        , ILoadingProgramRepository loadingProgramRepo, IShippingLineRepository shippingLineRepo
                                        , IShipperRepository shipperRepo, IClearingAgentExportRepository clearingAgentExportRepo
                                        , ILoadingProgramDetailRepository loadingProgramDetailRepo, IPortAndTerminalRepository portAndTerminalRepo
                                        , IShippingAgentExportRepository shippingAgentExportRepo
                                        , IExportLocationCodeListRepository locationRepo
                                        , IEnteringCargoRepository enteringCargoRepository)
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
        }
        public IActionResult LoadingProgramView()
        {

            ViewData["VesselExports"] = _vesselExportRepo.GetAll().OrderBy(s => s.VesselName)
                .Select(s => new SelectListItem { Text = s.VesselName, Value = s.VesselExportId.ToString() });

            ViewData["ShippingLine"] = _shippingLineRepo.GetAll().OrderBy(s => s.ShippingLineName)
                .Select(s => new SelectListItem { Text = s.ShippingLineName, Value = s.ShippingLineId.ToString() });

            ViewData["Shipper"] = _shipperRepo.GetAll().OrderBy(s => s.ShipperName)
                .Select(s => new SelectListItem { Text = s.ShipperName, Value = s.ShipperId.ToString() });

            ViewData["ClearingAgent"] = _clearingAgentExportRepo.GetAll().OrderBy(s => s.ClearingAgentName)
                .Select(s => new SelectListItem { Text = s.ClearingAgentName, Value = s.ClearingAgentExportId.ToString() });


            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll().OrderBy(s => s.ShippingAgentName)
                .Select(s => new SelectListItem { Text = s.ShippingAgentName, Value = s.ShippingAgentExportId.ToString() });


            var portAndTerminal = _portAndTerminalRepo.GetAll();
            ;

            ViewData["PortAndTerminal"] = portAndTerminal.Select(s => new SelectListItem { Text = s.TerminalCode, Value = s.PortAndTerminalId.ToString() });

            ViewData["Destinations"] = _locationRepo.GetAll().Select(s => new SelectListItem { Text = s.PortName, Value = s.PortName });

            return View();
        }

        public IActionResult Index()
        {

            return View();
        }


        public string GenPCode()
        {
            var lpNumber = _loadingProgramRepo.GenerateLPNumber();
            string value = lpNumber.ToString();
            return "LP" + value;

        }


        public IActionResult AddLoadingProgram(LoadingProgram values, long voyageID, List<LoadingProgramDetail> loadingProgramDetail)
        {

            var previouslp = _loadingProgramRepo.GetFirst(s => s.LoadingProgramNumber == values.LoadingProgramNumber);
            if (previouslp != null)
            {
                var lpcount = _loadingProgramRepo.GenerateLPNumber();
                values.LoadingProgramNumber = "LP" + lpcount.ToString();
            }
            List<LoadingProgramDetail> loadingProgramDetails = new List<LoadingProgramDetail>();
            values.VoyageExportId = voyageID;
            values.IsgateIn = false;
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
                    TotalPieces = item.TotalPieces


                };

                loadingProgramDetails.Add(loadingProgm);

            }
            _loadingProgramDetailRepo.AddRange(loadingProgramDetails);
            return new OkObjectResult(new PostObjectResponce { error = false, Id = values.LoadingProgramId, Message = "Saved" });

        }

        public JsonResult GetLoadingProgramByLPNumber(string lpNumber)
        {
            var data = _loadingProgramRepo.GetFirst(x => x.LoadingProgramNumber == lpNumber, d => d.LoadingProgramDetails);

            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }

        public JsonResult GetLoadingPrograms()
        {
            var data = _loadingProgramRepo.GetAll().OrderByDescending(c => c.LoadingProgramId);

            return Json(data);
        }

        public IActionResult UpdateLoadingProgram(LoadingProgram values, long loadingProgramId, long voyageID, List<LoadingProgramDetail> loadingProgramDetails)
        {
            var loadingprogram = _loadingProgramRepo.GetFirst(x => x.LoadingProgramId == loadingProgramId);
            loadingprogram.LoadingProgramId = loadingProgramId;
            loadingprogram.LoadingProgramNumber = values.LoadingProgramNumber;
            loadingprogram.LoadingProgramDate = values.LoadingProgramDate;
            loadingprogram.ReferenceNumber = values.ReferenceNumber;
            loadingprogram.Destination = values.Destination;
            loadingprogram.InvoiceDate = values.InvoiceDate;
            loadingprogram.InvoiceNumber = values.InvoiceNumber;
            loadingprogram.CargoCutOff = values.CargoCutOff;
            loadingprogram.VesselExportId = values.VesselExportId;
            loadingprogram.VoyageExportId = voyageID;
            loadingprogram.VesselETD = values.VesselETD;
            loadingprogram.LoadingTerminal = values.LoadingTerminal;
            //loadingprogram.ShipperId = values.ShipperId;
            loadingprogram.ShippingAgentExportId = values.ShippingAgentExportId;
            loadingprogram.ShippingLineExportId = values.ShippingLineExportId;
            loadingprogram.ClearingAgentExportId = values.ClearingAgentExportId;

            foreach (var detail in loadingProgramDetails)
            {
                detail.LoadingProgramId = loadingprogram.LoadingProgramId;
                if (detail.LoadingProgramDetailId > 0)
                    _loadingProgramDetailRepo.Update(detail);
                else
                    _loadingProgramDetailRepo.Add(detail);

            }
            _loadingProgramRepo.Update(loadingprogram);

            var cargoInfo = _cargoRepo.GetList(c => c.LoadingProgramDetail.LoadingProgramId == loadingprogram.LoadingProgramId, c=>c.LoadingProgramDetail);
            foreach (var cargo in cargoInfo)
            {
                cargo.VesselExportId = loadingprogram.VesselExportId;
                cargo.VoyageExportId = loadingprogram.VoyageExportId;

                _cargoRepo.Update(cargo);
            }

            return Ok();
        }

        public IActionResult DeleteLoadingProgramDetail(long loadingProgramDetailId)
        {
            var data = _loadingProgramDetailRepo.Find(loadingProgramDetailId);
            if (data != null)
            {
                _loadingProgramDetailRepo.Delete(data);
            }

            return Ok();
        }

        public JsonResult GetLoadingProgramDetailsByLPId(long loadingProgramId)
        {
            var data = _loadingProgramDetailRepo.GetList(x => x.LoadingProgramId == loadingProgramId);

            if (data != null)
            {
                return Json(data);

            }

            return Json("");
        }


        public JsonResult GetVoyageExportById(long VoyageExportId)
        {
            var data = _voyageExportRepo.GetFirst(x => x.VoyageExportId == VoyageExportId);

            if (data != null)
            {
                return Json(data);
            }

            return Json("");
        }

        public JsonResult GetPortAndTerminalBy(long PortAndTerminalId)
        {
            var data = _portAndTerminalRepo.GetFirst(x => x.PortAndTerminalId == PortAndTerminalId);

            if (data != null)
            {
                return Json(data);
            }

            return Json("");
        }

        public IActionResult DeleteLoadingProgram(LoadingProgram model)
        {
            var enteringCargo = _enteringCargoRepository.GetAll().Where(x => x.LoadingProgramNumber == model.LoadingProgramNumber).FirstOrDefault();
            

            if(enteringCargo!= null)
            {
                 return new OkObjectResult(new PostObjectResponce { error = true, Message = "EnteringCargo Of This LoadingProam Have Done" });

            }

            else
            {
                var loadingProgramDetail = _loadingProgramDetailRepo.GetAll().Where(x=> x.LoadingProgramId == model.LoadingProgramId).ToList();
                if (loadingProgramDetail != null)
                {
                    _loadingProgramDetailRepo.DeleteRange(loadingProgramDetail);
                }

                var loadingProgram = _loadingProgramRepo.GetAll().Where(x=> x.LoadingProgramId == model.LoadingProgramId).FirstOrDefault();
                if (loadingProgram != null)
                {
                    _loadingProgramRepo.Delete(loadingProgram);
                }
                return new OkObjectResult(new PostObjectResponce { error = false, Message = "Deleted" });

            }
        }
    }
}