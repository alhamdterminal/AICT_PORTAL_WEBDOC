using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class TransshipmentController : ParentController
    {
        public IClearingAgentExportRepository _clearingAgentExportRepo;
        public ITPReceiveVehicleRepository _tPReceiveVehicleRepository;
        public IVesselExportRepository _vesselRepo;
        public IVoyageExportRepository _voyageRepo;
        public IPackageTypeExportRepository _packageTypeExportRepository;
        public ITPCargoDetailsRepository _tpCargoDetailsRepository;
        private IPortAndTerminalRepository _portAndTerminalRepo;
        private IExportLocationCodeListRepository _unloCodeRepo;

        public TransshipmentController(IVesselExportRepository vesselRepo, IVoyageExportRepository voyageRepo
                                      , IClearingAgentExportRepository clearingAgentExportRepo
                                      ,  ITPReceiveVehicleRepository tPReceiveVehicleRepository
                                      , IExportLocationCodeListRepository unloCodeRepo
                                      , ITPCargoDetailsRepository tpCargoDetailsRepository
                                      , IPortAndTerminalRepository portAndTerminalRepo
                                      , IPackageTypeExportRepository packageTypeExportRepository)
        {
            _tPReceiveVehicleRepository = tPReceiveVehicleRepository;
            _clearingAgentExportRepo = clearingAgentExportRepo;
            _vesselRepo = vesselRepo;
            _voyageRepo = voyageRepo;
            _packageTypeExportRepository = packageTypeExportRepository;
            _unloCodeRepo = unloCodeRepo;
            _portAndTerminalRepo = portAndTerminalRepo;
            _tpCargoDetailsRepository = tpCargoDetailsRepository;
        }

        public IActionResult Index()
        {
            ViewData["ClearingAgent"] = _clearingAgentExportRepo.GetAll()
                .Select(s =>
                new SelectListItem
                {
                    Text = s.ClearingAgentName,
                    Value = s.ClearingAgentExportId.ToString()
                });

            return View();
        }

        public IActionResult CargoDetails()
        {
            ViewData["VesselExport"] = _vesselRepo.GetAll()
                     .Select(s => new SelectListItem { Text = s.VesselName, Value = s.VesselExportId.ToString() });

            ViewData["VoyageExport"] = _voyageRepo.GetAll()
                   .Select(s => new SelectListItem { Text = s.VoyageNumber, Value = s.VoyageExportId.ToString() });

            ViewData["PackageType"] = _packageTypeExportRepository.GetAll()
                    .Select(s => new SelectListItem { Text = s.PackageType, Value = s.PackageType });
            var portAndTerminal = _portAndTerminalRepo.GetAll();
            var codeList = _unloCodeRepo.GetAll();

            ViewData["PortAndTerminal"] = portAndTerminal
           .Select(s => new SelectListItem { Text = s.TerminalCode, Value = s.TerminalCode });

            ViewData["CityOfDischarge"] = codeList
               .Select(s => new SelectListItem { Text = s.PortName, Value = s.PortName });

            ViewData["CountryOfDischarge"] = codeList
               .Select(s => new SelectListItem { Text = s.CountryName, Value = s.CountryName });
            return View();
        }

        public IActionResult AddTransshipment(TPReceiveVehicle model )
        {
            var dateTime = DateTime.Now;
            model.CreatedAt = dateTime;
            _tPReceiveVehicleRepository.Add(model);

            return new OkObjectResult(new PostObjectResponce { error = false, Id = model.TPReceiveVehicleId, Message = "Saved" });
        }

        public JsonResult GetTransshipmentByVehicleNumber(string VehicleNumber)
        {
            var data = _tPReceiveVehicleRepository.GetAll().Where(x => x.VehicleNo.Trim().ToUpper() == VehicleNumber.Trim().ToUpper()).FirstOrDefault();
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }



        public JsonResult GetTransshipmentsByVehicleNumber(string VehicleNumber)
        {
            var data = _tPReceiveVehicleRepository.GetAll().Where(x => x.VehicleNo.Trim().ToUpper() == VehicleNumber.Trim().ToUpper());
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }

        public IActionResult updateTransshipmentByModel(TPReceiveVehicle model)
        { 
            _tPReceiveVehicleRepository.Update(model);
            return new OkObjectResult(new { error = false, message = "Saved" });
        }



        public IActionResult updateTransshipment(TPReceiveVehicle model, long TPReceiveVehicleId)
        {
            model.TPReceiveVehicleId = TPReceiveVehicleId;
            _tPReceiveVehicleRepository.Update(model);
            return new OkObjectResult(new { error = true, message = "Saved" });
        }


        public IActionResult AddTPCargoDetail(TPCargoDetails model, long TPReceiveVehicleId, long LoadingProgramId  , long voyageExportId)
        {
            model.VoyageExportId = voyageExportId;
            model.TPReceiveVehicleId = TPReceiveVehicleId;
            model.LoadingProgramId = LoadingProgramId;
            _tpCargoDetailsRepository.Add(model);

            return new OkObjectResult(new PostObjectResponce { error = false, Id = model.TPCargoDetailsId, Message = "Saved" });
        }


        public IActionResult GetTPCargoDetailByLP(long LoadingProgramId)
        {
            var data = _tpCargoDetailsRepository.GetAll().Where(x => x.LoadingProgramId == LoadingProgramId).FirstOrDefault();
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }

        public IActionResult updatePCargoDetail(TPCargoDetails model, long TPReceiveVehicleId , long LoadingProgramId , long TPCargoDetailsId  , long voyageExportId)
        {
            model.VoyageExportId = voyageExportId;
            model.TPReceiveVehicleId = TPReceiveVehicleId;
            model.LoadingProgramId = LoadingProgramId;
            model.TPCargoDetailsId = TPCargoDetailsId;
            _tpCargoDetailsRepository.Update(model);
            return new OkObjectResult(new { error = true, message = "Saved" });
        }

        public JsonResult GetCargoDetailBYTPReceive(long TPReceiveVehicleId)
        {
            var data = _tpCargoDetailsRepository.GetAll().Where(x => x.TPReceiveVehicleId == TPReceiveVehicleId).ToList();

            if(data != null)
            {
                return Json(data);
            }
            return Json("");
        }
    }
}