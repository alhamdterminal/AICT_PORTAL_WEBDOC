using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class CargoReceivedController : ParentController
    {
        private ILoadingProgramRepository _lpRepo;
        private IEnteringCargoRepository _enteringCargoRepo;
        private ICargoReceivedRepository _cargoReceivedRepo;
        private IClearingAgentExportRepository _clearingAgentExportRepo;
        private IGateInExportRepository _gateinRepo;
        private WebocProcessor _webocProcessor;
        private IHostingEnvironment env;
        private readonly IOptions<AppConfig> _config;
        private IOGIERepository _ogieRepo;
        private IOSIMRepository _osimRepo;
        private IOPIARepository _opiasRepo;

        public CargoReceivedController(ICargoReceivedRepository cargoReceivedRepo,
                                       IClearingAgentExportRepository clearingAgentExportRepo,
                                       ILoadingProgramRepository lpRepo,
                                       IEnteringCargoRepository enteringCargoRepo,
                                       IGateInExportRepository gateinRepo,
                                       WebocProcessor webocProcessor,
                                       IOptions<AppConfig> config,
                                       IOGIERepository ogieRepo,
                                       IHostingEnvironment _env,
                                       IOSIMRepository osimRepo,
                                       IOPIARepository opiasRepo)
        {
            _cargoReceivedRepo = cargoReceivedRepo;
            _clearingAgentExportRepo = clearingAgentExportRepo;
            _lpRepo = lpRepo;
            _enteringCargoRepo = enteringCargoRepo;
            _gateinRepo = gateinRepo;
            _webocProcessor = webocProcessor;
            _config = config;
            _ogieRepo = ogieRepo;
            env = _env;
            _osimRepo = osimRepo;
            _opiasRepo = opiasRepo;
        }

        public IActionResult CargoReceivedView()
        {
            return View();
        }

        public IActionResult AddCargoReceived(CargoReceived model, string TruckNumber, long loadingProgramId, long clearingAgentExportId)
        {
            var loadingProgram = _lpRepo.GetAll().Where(x => x.LoadingProgramId == loadingProgramId).LastOrDefault();
            if (loadingProgram != null)
            {
                var entringCargo = _enteringCargoRepo.GetAll().Where(x => x.LoadingProgramNumber == loadingProgram.LoadingProgramNumber).LastOrDefault();

                if (entringCargo != null)
                {
                    var hold = _osimRepo.GetAll().Where(x => x.GDNumber == entringCargo.GDNumber ).LastOrDefault();

                    if (hold != null && hold.Status == "HO")
                    {
                        return new OkObjectResult(new { error = true, message = "This GD" + entringCargo.GDNumber + "is Hold" });

                    }
                    else
                    {
                        if (model.WeightDeclaredByDriver == 0)
                        {
                            return new OkObjectResult(new PostObjectResponce { error = true, Message = "Please Enter Weight  > 0 " });
                        }
                        else
                        {

                            model.TruckNumber = TruckNumber;
                            model.LoadingProgramId = loadingProgramId;
                            model.GateInDateTime = model.RecevingStartTime;
                            model.ClearingAgentExportId = clearingAgentExportId;
                            _cargoReceivedRepo.Add(model);

                            var lp = _lpRepo.Find(loadingProgramId);
                            var cargo = _enteringCargoRepo.GetFirst(s => s.LoadingProgramNumber == lp.LoadingProgramNumber);

                            var opias = _opiasRepo.GetAll().Where(x => x.GDNumber == cargo.GDNumber && x.MessageFrom == "Manual").LastOrDefault();

                            if (opias != null)
                            {  
                                lp.IsgateIn = true;
                                _lpRepo.Update(lp);
                                
                            }


                        }

                        return new OkObjectResult(new PostObjectResponce { error = false, Id = model.CargoReceivedId, Message = "Saved" });
                    }

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "No Entering Cargo Found" });

                }



            }
            else
            {
                return new OkObjectResult(new { error = true, message = "No LP Found" });

            }




        }

        [HttpGet]
        public IActionResult SendOGIE(long CargoRecievedId)
        {
            var local = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            var datetime = TimeZoneInfo.ConvertTime(DateTime.Now, local);

            var cargorcv = _cargoReceivedRepo.GetFirst(s => s.CargoReceivedId == CargoRecievedId, l => l.LoadingProgram);
            var cargo = _enteringCargoRepo.GetFirst(s => s.LoadingProgramNumber == cargorcv.LoadingProgram.LoadingProgramNumber);

            if (cargo != null)
            {
                var hold = _osimRepo.GetAll().Where(x => x.GDNumber == cargo.GDNumber ).LastOrDefault();

                if (hold != null && hold.Status == "HO")
                {
                    return new OkObjectResult(new { error = true, message = "This GD" + cargo.GDNumber + "is Hold" });

                }
                else
                {

                    var edimessage = _ogieRepo.GetAll().Where(x => x.GDNumber.Trim().ToUpper() == cargo.GDNumber.Trim().ToUpper() && x.GateInStatus == "F").FirstOrDefault();
                    if (edimessage != null)
                    {
                        return new OkObjectResult(new PostObjectResponce { error = true, Message = "Al Ready Received" });

                    }
                    else
                    {

                        // var fn =  CheckFileName();

                        var fn = $"OGIE_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}";
                        var filname = _ogieRepo.GetAll().Where(x => x.FileName == fn).FirstOrDefault();


                        if (filname != null)
                        {
                            return new OkObjectResult(new PostObjectResponce { error = true, Message = "Please Proceed After One Minute" });
                        }
                        else
                        {
                            var gatein = new GateInExport
                            {
                                GateInDate = cargorcv.RecevingStartTime,
                                GateInStatus = cargorcv.PackageType,
                                GDNumber = cargo.GDNumber,
                                NumberofPackages = cargorcv.NumberOfPackagesReceived,
                                PackageType = cargo.PackageType,
                                VehicleNumber = cargorcv.TruckNumber,
                                Weight = cargo.GrossWeight,

                            }; 

                            var ogie = new OGIE
                            {
                                GateIn = cargorcv.RecevingStartTime,
                                GateInStatus = gatein.GateInStatus,
                                GDNumber = gatein.GDNumber,
                                NoOfPackages = gatein.NumberofPackages,
                                VehicleNumber = gatein.VehicleNumber,
                                //Weight = gatein.Weight,
                                Weight = cargorcv.WeightDeclaredByDriver ?? 0,
                                PackageType = cargo.PackageType,
                                Performed = datetime,
                                MessageFrom = _config.Value.MessageFrom,
                                MessageTo = "WEBOC",
                                FileName = fn

                            };

                            var ogieList = new List<OGIE>();
                            ogieList.Add(ogie);                            

                            _ogieRepo.Add(ogie);
                            _gateinRepo.Add(gatein);

                            if (cargorcv != null)
                            {
                                var loadingProgram = _lpRepo.GetAll().Where(x => x.LoadingProgramId == cargorcv.LoadingProgramId).LastOrDefault();

                                loadingProgram.IsgateIn = true;

                                _lpRepo.Update(loadingProgram);
                            }


                         }



                    }
                }

               

            }

            return new OkObjectResult(new PostObjectResponce { error = false, Message = "Save" });


        }

        public string CheckFileName( )
        {
            var local = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            var datetime = TimeZoneInfo.ConvertTime(DateTime.Now, local);

            var fn =  $"OGIE_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}";
            var file = _ogieRepo.GetAll().Where(x => x.FileName == fn).FirstOrDefault();
            while (file.FileName == fn)
            {
                 fn = $"OGIE_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime.AddMinutes(1))}{".txt"}";
            }  

              
            return fn;
        }
        public JsonResult GetCargoReceived(string LPNumber)
        {

            var lp = _lpRepo.GetFirst(x => x.LoadingProgramNumber == LPNumber);

            if (lp != null)
            {
                var data = _cargoReceivedRepo.GetFirst(x => x.LoadingProgramId == lp.LoadingProgramId);

                return Json(data);

            }

            return Json("");
        }


        public IActionResult UpdateCargoReceived(CargoReceived values, string TruckNumber, long CargoReceivedId, long clearingAgentExportId)
        {

            var cargoReceived = _cargoReceivedRepo.GetFirst(x => x.CargoReceivedId == CargoReceivedId);

            if (cargoReceived != null)
            {
                cargoReceived.CargoReceivedId = CargoReceivedId;
                cargoReceived.ClearingAgentExportId = clearingAgentExportId;
                cargoReceived.CLearingAgentReprsentative = values.CLearingAgentReprsentative;
                cargoReceived.ClearingAgentCNIC = values.ClearingAgentCNIC;
                cargoReceived.DriverName = values.DriverName;
                cargoReceived.DriverCNIC = values.DriverCNIC;
                cargoReceived.PackageType = values.PackageType;
                cargoReceived.NumberOfPackagesReceived = values.NumberOfPackagesReceived;
                cargoReceived.RecevingStartTime = values.RecevingStartTime;
                cargoReceived.RecevingEndTime = values.RecevingEndTime;
                cargoReceived.CargoRecevingCondition = values.CargoRecevingCondition;
                cargoReceived.WeightDeclaredByDriver = values.WeightDeclaredByDriver;
                cargoReceived.TruckNumber = TruckNumber;
                cargoReceived.LoadingProgramId = cargoReceived.LoadingProgramId;
                cargoReceived.GateInDateTime = cargoReceived.GateInDateTime;
                _cargoReceivedRepo.Update(cargoReceived);

            }
            return Ok();

        }
    }
}