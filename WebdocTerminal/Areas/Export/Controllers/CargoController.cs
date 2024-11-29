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
    public class CargoController : ParentController
    {
        private ICargoRepository _cargoRepo;
        private IVesselExportRepository _vesselExportRepos;
        private IVoyageExportRepository _voyageExportRepos;
        private ICommodityRepository _commodityRepo;
        private IPortAndTerminalRepository _portAndTerminalRepo;
        private IGDHoldRepository _gDHoldRepo;
        private IExportLocationCodeListRepository _unloCodeRepo;
        private IEnteringCargoRepository _enteringCargoRepository;
        private IOSIMRepository _osimRepo;

        public CargoController(IPortAndTerminalRepository portAndTerminalRepo, ICargoRepository cargoRepo, IVesselExportRepository vesselExportRepos
                                , IVoyageExportRepository voyageExportRepos, ICommodityRepository commodityRepo
                                , IGDHoldRepository gDHoldRepo
                                , IExportLocationCodeListRepository unloCodeRepo
                                , IEnteringCargoRepository enteringCargoRepository
                                , IOSIMRepository osimRepo)
        {
            _cargoRepo = cargoRepo;
            _vesselExportRepos = vesselExportRepos;
            _voyageExportRepos = voyageExportRepos;
            _commodityRepo = commodityRepo;
            _portAndTerminalRepo = portAndTerminalRepo;
            _gDHoldRepo = gDHoldRepo;
            _unloCodeRepo = unloCodeRepo;
            _enteringCargoRepository = enteringCargoRepository;
            _osimRepo = osimRepo;
        }

        public IActionResult CargoView()
        {
            ViewData["VesselExport"] = _vesselExportRepos.GetAll()
                   .Select(s => new SelectListItem { Text = s.VesselName, Value = s.VesselExportId.ToString() });

            ViewData["VoyageExport"] = _voyageExportRepos.GetAll()
                   .Select(s => new SelectListItem { Text = s.VoyageNumber, Value = s.VoyageExportId.ToString() });

            ViewData["Commodity"] = _commodityRepo.GetAll()
                   .Select(s => new SelectListItem { Text = s.CommodityName, Value = s.CommodityId.ToString() });

            var portAndTerminal = _portAndTerminalRepo.GetAll();

            var codeList = _unloCodeRepo.GetAll();

            ViewData["PortAndTerminal"] = portAndTerminal
               .Select(s => new SelectListItem { Text = s.TerminalCode, Value = s.PortAndTerminalId.ToString() });

            ViewData["CityOfDischarge"] = codeList
               .Select(s => new SelectListItem { Text = s.PortName, Value = s.PortName });

            ViewData["CountryOfDischarge"] = codeList
               .Select(s => new SelectListItem { Text = s.CountryName, Value = s.CountryName });

            return View();
        }

        public JsonResult GetCargo(string gdnumber, string lpnumber)
        {
            var data = _cargoRepo.GetCargoInformation(gdnumber, lpnumber);
            if (data != null)
            {
                return Json(data);
            }

            return Json("");
        }

        public IActionResult AddCargo(Cargo model, long loadingProgramDetailId, string gdnumber)
        {
            var hold = _osimRepo.GetAll().Where(x => x.GDNumber == gdnumber).LastOrDefault();

            if (hold != null && hold.Status == "HO")
            {
                return new OkObjectResult(new { error = true, message = "This GD" + gdnumber + "is Hold" });

            }
            else
            {
                var ec = _enteringCargoRepository.GetFirst(x => x.GDNumber == gdnumber);
                if (ec != null)
                {
                    var gdhold = _gDHoldRepo.GetFirst(x => x.EnteringCargoId == ec.EnteringCargoId);
                    if (gdhold != null)
                    {
                        return new JsonResult(new { error = true, message = "GD Hold By System" });
                    }


                }
                if(ec == null) { 
                    return new JsonResult(new { error = true, message = "This GD Has No EnteringCargo" });
                }

                model.LoadingProgramDetailId = loadingProgramDetailId;
                model.GDNumber = gdnumber;
                _cargoRepo.Add(model);
                return new OkObjectResult(new PostObjectResponce { error = false, Id = model.CargoId, Message = "Saved" });

            }




        }

        public IActionResult updateCargo(Cargo model, long loadingProgramDetailId, string gdnumber, long CargoId)
        {
            var hold = _osimRepo.GetAll().Where(x => x.GDNumber == gdnumber).LastOrDefault();

            if (hold != null && hold.Status == "HO")
            {
                return new OkObjectResult(new { error = true, message = "This GD" + gdnumber + "is Hold" });

            }
            else
            {

                var data = _cargoRepo.GetFirst(x => x.CargoId == CargoId);
                data.ColorCode = model.ColorCode;
                data.FinalDestination = model.FinalDestination;
                data.GDNumber = gdnumber;
                data.DishargePort = model.DishargePort;
                data.PLIDNumber = model.PLIDNumber;
                data.WarehouseLocation = model.WarehouseLocation;
                data.CBM = model.CBM;
                data.Weight = model.Weight;
                data.Location = model.Location;
                data.LoadingProgramDetailId = loadingProgramDetailId;
                data.VesselExportId = model.VesselExportId;
                data.VoyageExportId = model.VoyageExportId;
                data.CommodityId = model.CommodityId;
                data.PortAndTerminalId = model.PortAndTerminalId;
                data.PackageReceived = model.PackageReceived;

                _cargoRepo.Update(data);
                return new OkObjectResult(new { error = false, message = "Updated" });

            }


        }

    }
}