using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class EnteringCargoController : ParentController
    {
        private IEnteringCargoRepository _enteringCargoRepo;
        private IExportVehicleRepository _exportVehicleRepository;
        private IOSIMRepository _osimRepo;

        public EnteringCargoController(IEnteringCargoRepository enteringCargoRepo  
                                        , IExportVehicleRepository exportVehicleRepository
                                        , IOSIMRepository osimRepo)
        {
            _enteringCargoRepo = enteringCargoRepo;
            _exportVehicleRepository = exportVehicleRepository;
            _osimRepo = osimRepo;
        }

        public IActionResult EnteringCargoView()
        {
            return View();
        }

        public IActionResult AddEnteringCargo(EnteringCargo values, string GDNumber, string lpnumber , List<ExportVehicle> VehicleDetail)
        {
            var hold = _osimRepo.GetAll().Where(x => x.GDNumber == GDNumber ).LastOrDefault();

            if(hold != null && hold.Status == "HO")
            {
                 return new OkObjectResult(new { error = true, message = "This GD" + GDNumber  + "is Hold" });

            }
            else
            {
                List<ExportVehicle> ExportVehicles = new List<ExportVehicle>();

                if (_enteringCargoRepo.GetFirst(e => e.GDNumber.Trim().ToUpper() != GDNumber.Trim().ToUpper() && e.LoadingProgramNumber == lpnumber) != null)
                    return Json(new { error = true, message = "GD Already associated" });
                var dateTime = DateTime.Now;
                var enteringCargo = new EnteringCargo
                {

                    GDNumber = GDNumber,
                    ChallanNumber = values.ChallanNumber,
                    ClearingAgentName = values.ClearingAgentName,
                    ConsigneeName = values.ConsigneeName,
                    GateInStatus = values.GateInStatus,
                    GrossWeight = values.GrossWeight,
                    LoadingProgramNumber = lpnumber,
                    NumberOfPackages = values.NumberOfPackages,
                    PackageType = values.PackageType,
                    ShipperName = values.ShipperName,
                    Key = lpnumber + '-' + GDNumber,
                    OPIAReceiveTime = dateTime,
                    Remarks = values.Remarks,
                    isGrounded = false

                };
                _enteringCargoRepo.Add(enteringCargo);


                foreach (var item in VehicleDetail)
                {
                    var exportVehicle = new ExportVehicle
                    {
                        VehicleNumber = item.VehicleNumber,
                        ShipperSeal = item.ShipperSeal,
                        EnteringCargoId = enteringCargo.EnteringCargoId,

                    };

                    ExportVehicles.Add(exportVehicle);

                }
                _exportVehicleRepository.AddRange(ExportVehicles);

                return new OkObjectResult(new PostObjectResponce { error = false, Id = enteringCargo.EnteringCargoId, Message = "GD associated successfully" });

            }

        }


        public IActionResult UpdateEnteringCargo(EnteringCargo values, string gdnumber, string lpnumber, long enteringCargoId , List<ExportVehicle> ExportVehicles)
        {


            var hold = _osimRepo.GetAll().Where(x => x.GDNumber == gdnumber ).LastOrDefault();

            if (hold != null  && hold.Status == "HO")
            {
                return new OkObjectResult(new { error = true, message = "This GD" + gdnumber + "is Hold" });

            }
            else
            {

                if (_enteringCargoRepo.GetFirst(e => e.GDNumber.Trim().ToUpper() != gdnumber.Trim().ToUpper() && e.LoadingProgramNumber == lpnumber) != null)
                    return Json(new { error = true, message = "GD Already associated" });

                var data = _enteringCargoRepo.GetFirst(x => x.EnteringCargoId == enteringCargoId);

                data.GDNumber = gdnumber;
                data.ChallanNumber = values.ChallanNumber;
                data.ClearingAgentName = values.ClearingAgentName;
                data.ConsigneeName = values.ConsigneeName;
                data.GateInStatus = values.GateInStatus;
                data.GrossWeight = values.GrossWeight;
                data.LoadingProgramNumber = lpnumber;
                data.NumberOfPackages = values.NumberOfPackages;
                data.PackageType = values.PackageType;
                data.ShipperName = values.ShipperName;
                data.Key = lpnumber + '-' + gdnumber;
                data.OPIAReceiveTime = values.OPIAReceiveTime;
                data.Remarks = values.Remarks;
                data.IsHold = data.IsHold;

                var exportVehcile = _exportVehicleRepository.GetList(x => x.EnteringCargoId == data.EnteringCargoId);
                if (exportVehcile != null)
                {
                    _exportVehicleRepository.DeleteRange(exportVehcile);
                }


                foreach (var detail in ExportVehicles)
                {
                    detail.EnteringCargoId = data.EnteringCargoId;
                    detail.ExportVehicleId = 0;
                    _exportVehicleRepository.Add(detail);

                }

                _enteringCargoRepo.Update(data);

                return Json(new { error = false, message = "Saved" });
            }
        }

 

 
    }
}