using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class GateInOutController : ParentController
    {
        private IEnteringCargoRepository _enteringCargoRepo;
        private IGateInExportRepository _gateInExportContainerRepo;
        private IGateOutGDRepository _gateOutGdRepo;
        private IOGIERepository _ogieRepo;
        private IOGTERepository _ogteRepo;
        private IGateoutExportRepository _gateOutRepo;
        private IExportContainerRepository _exportContainerRepo;
        private IDictionaryRepository _dictionaryRepo;
        private IPortAndTerminalRepository _portAndTerminalRepo;
        private IGDHoldRepository _gDHoldRepository;
        private ICargoRepository _cargoRepository;
        private IExportContainerItemRepositpory _exportContainerItemRepositpory;
        private IHostingEnvironment _env;
        private IOptions<AppConfig> _config;
        private WebocProcessor _webocProcessor;
        private IOSIMRepository _osimRepo;
        private ITransporterRepository _transporterRepository;


        public GateInOutController(IEnteringCargoRepository enteringCargoRepo, IGateInExportRepository gateInExportContainerRepo,
                                   IOGIERepository ogieRepo, IOGTERepository ogteRepo, IGateoutExportRepository gateOutRepo
                                    , IExportContainerRepository exportContainerRepo, IDictionaryRepository dictionaryRepo,
                                    IPortAndTerminalRepository portAndTerminalRepo, IGDHoldRepository gDHoldRepository
                                    , IGateOutGDRepository gateOutGdRepo, ICargoRepository cargoRepository
                                    , IExportContainerItemRepositpory exportContainerItemRepositpory
                                    , IHostingEnvironment env
                                    , IOptions<AppConfig> config
                                    , WebocProcessor webocProcessor
                                    , IOPIARepository oPIARepository
                                    , IOSIMRepository osimRepo
                                    , ITransporterRepository transporterRepository)
        {
            _portAndTerminalRepo = portAndTerminalRepo;
            _enteringCargoRepo = enteringCargoRepo;
            _gateInExportContainerRepo = gateInExportContainerRepo;
            _ogieRepo = ogieRepo;
            _ogteRepo = ogteRepo;
            _gateOutRepo = gateOutRepo;
            _exportContainerRepo = exportContainerRepo;
            _dictionaryRepo = dictionaryRepo;
            _gDHoldRepository = gDHoldRepository;
            _gateOutGdRepo = gateOutGdRepo;
            _cargoRepository = cargoRepository;
            _transporterRepository = transporterRepository;
            _exportContainerItemRepositpory = exportContainerItemRepositpory;
            _env = env;
            _config = config;
            _webocProcessor = webocProcessor;
            _osimRepo = osimRepo;

        }

        public IActionResult GateOut()
        {
            ViewData["ExportContainer"] = _exportContainerRepo.GetList(s => s.IsGateout == false)
                   .Select(x => new SelectListItem { Text = x.ContainerNumber, Value = x.ExportContainerId.ToString() });

            //ViewData["PortAndTerminal"] = _portAndTerminalRepo.GetAll()
            //    .Select(s => new SelectListItem { Text = s.TerminalCode, Value = s.PortAndTerminalId.ToString() });

            //ViewData["Transporter"] = _transporterRepository.GetAll()
            //    .Select(t => new SelectListItem { Text = t.TransporterName, Value = t.TransporterId.ToString() });
            return View();
        }


        public IActionResult GateInGDsView()
        {

            return View();
        }


        public IActionResult GateInGds(List<GateInExportGDsViewModel> cargoList)
        {
            try
            {
                var local = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");

                var datetime = TimeZoneInfo.ConvertTime(DateTime.Now, local);

                List<GateInExport> gateins = new List<GateInExport>();
                List<OGIE> ogies = new List<OGIE>();

                foreach (var cargoExport in cargoList)
                {

                    var status = "";

                    status = _gateInExportContainerRepo.getstatusbygdnumber(cargoExport.GDNumber, cargoExport.RemainingNoOfPackages, cargoExport.RemainingGrossWeight);

                    if (status != "F" && status != "P")
                    {
                        return new OkObjectResult(new { error = true, message = status });
                    }

                    var gatein = new GateInExport
                    {
                        GDNumber = cargoExport.GDNumber,
                        NumberofPackages = cargoExport.RemainingNoOfPackages,
                        PackageType = cargoExport.PackageType,
                        Weight = cargoExport.RemainingGrossWeight,
                        GateInDate = datetime,
                        VehicleNumber = cargoExport.VehicleNo,
                        GateInStatus = status

                    };

                    gateins.Add(gatein);

                    //if (cargoExport.MessageFrom == "Manual")
                    //{
                        var ogie = new OGIE
                        {
                            GDNumber = cargoExport.GDNumber,
                            NoOfPackages = cargoExport.RemainingNoOfPackages,
                            PackageType = cargoExport.PackageType,
                            Weight = cargoExport.RemainingGrossWeight,
                            GateIn = datetime,
                            VehicleNumber = cargoExport.VehicleNo,
                            GateInStatus = status,
                            MessageFrom = _config.Value.MessageFrom,
                            MessageTo = "WEBOC",
                            Performed = datetime,
                            IsProcessed = cargoExport.MessageFrom == "Manual" ? true : false,
                            FileName = $"OGIE_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                        };

                        ogies.Add(ogie);
                    //}

                }

                _ogieRepo.AddRange(ogies);

                _gateInExportContainerRepo.AddRange(gateins);

                return new OkObjectResult(new { error = false, message = "Saved" });
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException });
            }

        }

        public IActionResult SaveGateInContainers(List<GateInViewModel> cargoList)
        {
            var local = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");

            var datetime = TimeZoneInfo.ConvertTime(DateTime.Now, local);

            List<GateInExport> gateins = new List<GateInExport>();
            List<OGIE> ogies = new List<OGIE>();

            foreach (var cargoExport in cargoList)
            {
                var gatein = new GateInExport
                {
                    GDNumber = cargoExport.GDNumber,
                    NumberofPackages = cargoExport.NoOfPackages,
                    PackageType = cargoExport.PackageType,
                    Weight = cargoExport.GrossWeight,
                    GateInDate = datetime,
                    VehicleNumber = cargoExport.VehicleNo,
                    GateInStatus = "T"

                };

                gateins.Add(gatein);

                var cargo = _enteringCargoRepo.Find(cargoExport.ExportContainerId);

                if (cargo != null)
                {
                    cargo.isGateIn = true;
                    cargo.ClearingAgentName = cargo.ClearingAgentName;
                    _enteringCargoRepo.Update(cargo);
                }


                var ogie = new OGIE
                {
                    GDNumber = cargoExport.GDNumber,
                    NoOfPackages = cargoExport.NoOfPackages,
                    PackageType = cargoExport.PackageType,
                    Weight = cargoExport.GrossWeight,
                    GateIn = datetime,
                    VehicleNumber = cargoExport.VehicleNo,
                    GateInStatus = "F",
                    MessageFrom = _config.Value.MessageFrom,
                    MessageTo = "WEBOC",
                    Performed = datetime,
                    FileName = $"OGIE{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                };

                ogies.Add(ogie);
            }

            _ogieRepo.AddRange(ogies);

            _gateInExportContainerRepo.AddRange(gateins);

            return Ok();
        }





        public IActionResult SaveGateOutContainers(List<GateOutViewModel> containers )
        {

            foreach (var item in containers)
            {
                var GDNUMBER = string.IsNullOrEmpty(item.GDNumber) ? "" : item.GDNumber.Trim().ToUpper();

                var hold = _osimRepo.GetAll().Where(x => x.GDNumber == GDNUMBER).LastOrDefault();

                if (hold != null && hold.Status == "HO")
                {
                    return new OkObjectResult(new { error = true, message = "This GD" + item.GDNumber + "is Hold" });

                }
            }

            var local = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");

            var datetime = TimeZoneInfo.ConvertTime(DateTime.Now, local);

            //var port = _portAndTerminalRepo.Find(portAndTerminalId);

            List<GateoutExport> gateoutExports = new List<GateoutExport>();
            List<OGTE> oGTEs = new List<OGTE>();
            List<ExportContainer> exportContainers = new List<ExportContainer>();
            List<ExportContainerItem> exportContaineritems = new List<ExportContainerItem>();
            //var count = 0;
            if (containers.Where(x=>x.ContainerGrossWeight <= 0).Count() > 0)
            {
                return new OkObjectResult(new { error = true, message = "The Container Weight Must > 0" });

            }
            else
            {
                var fn = $"OGTE_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}";
                var fileName = _ogteRepo.GetAll().Where(x => x.FileName == fn).FirstOrDefault();

                if (fileName != null)
                {
                    return new OkObjectResult(new PostObjectResponce { error = true, Message = "Please Proceed After One Minute" });
                }
                else
                {
                    foreach (var exportContainer in containers)
                    {
                        var GDNUMBER = string.IsNullOrEmpty(exportContainer.GDNumber) ? "" : exportContainer.GDNumber.Trim().ToUpper();

                        //    count += 1;

                        var data = GetGDHold(exportContainer.GDNumber);

                        if (data != 0)
                        {
                            return new OkObjectResult(new { error = true, message = "This GD" + exportContainer.GDNumber + "is Hold" });
                        }




                        var exitm = _exportContainerItemRepositpory.GetFirst(x => (x.GDNumber?.Trim().ToUpper() == GDNUMBER) && (x.IsSubmitted == false));
                        if (exitm != null)
                        {
                            return new JsonResult(new { error = true, message = "This " + GDNUMBER + "Was Not Submited In GD Association" });
                        }


                        else
                        {

                            var gateout = new GateoutExport
                            {
                                ContainerNumber = exportContainer.ContainerNumber,
                                GDNumber = exportContainer.GDNumber,
                                Status = exportContainer.Status,
                                GateoutTime = datetime,
                                VehicleNumber = exportContainer.VehicleNumber,
                                PCCSSSeal = exportContainer.PCCSSSeal,
                                BondedCarrierNTN = exportContainer.BondedCarrierNTN,
                                //PortAndTerminalId = portAndTerminalId,
                                CountryofExit = exportContainer.CountryofExit,
                                ContainerGrossWeight = exportContainer.ContainerGrossWeight,
                                LineSeal = exportContainer.LineSeal,
                                TransporterId = exportContainer.TransporterId

                            };

                            gateoutExports.Add(gateout);

                            var cntr = _exportContainerRepo.Find(exportContainer.ExportContainerId);
                            if (cntr != null)
                            {
                                cntr.IsGateout = true;
                                exportContainers.Add(cntr);

                            }

                            var itmeslist = _exportContainerItemRepositpory.GetAll().Where(x=> x.ExportContainerId == exportContainer.ExportContainerId && x.GDNumber == exportContainer.GDNumber).ToList();
                            if (itmeslist.Count() > 0)
                            {
                                itmeslist.ForEach(x => x.IsGateOut = true);
                                itmeslist.ForEach(x => x.GateOutDate = DateTime.Now);

                                exportContaineritems.AddRange(itmeslist);
                            }


                            var oGTE = new OGTE
                            {
                                ContainerNumber = exportContainer.ContainerNumber,
                                GDNumber = exportContainer.GDNumber,
                                Status = "F",
                                GateOutTime = datetime,
                                VehicleNumber = exportContainer.VehicleNumber,
                                BondedCarrierNTN = exportContainer.BondedCarrierNTN,
                                PCCSSSeal = exportContainer.PCCSSSeal,
                                CountryofExit = exportContainer.CountryofExit,
                                ContainerGrossWeight = exportContainer.ContainerGrossWeight,
                                //PortofExit = port.TerminalCode,
                                PortofExit = exportContainer.Location,
                                LocationCode = exportContainer.Location,
                                MessageFrom = _config.Value.MessageFrom,
                                MessageTo = "WEBOC",
                                FileName = fn
                            };

                            oGTEs.Add(oGTE);


                            //var number = GetTrminalNumber() + count;


                            //var cargs = _cargoRepository.GetList(x => x.GDNumber?.Trim().ToUpper() == GDNUMBER);

                            //if (cargs != null)
                            //{
                            //    string day = datetime.ToString("dd");
                            //    string month = datetime.ToString("MM");
                            //    string year = datetime.ToString("yyyy");

                            //    foreach (var item in cargs)
                            //    {
                            //        item.TRNumber = "TR/" + number + "/" + month + "/" + day + "/" + year; ;
                            //        item.IssueDate = datetime;
                            //        _cargoRepository.Update(item);

                            //    }
                            //}

                        }


                    }



                    _exportContainerRepo.UpdateRange(exportContainers);
                    _ogteRepo.AddRange(oGTEs);
                    _gateOutRepo.AddRange(gateoutExports);
                    _exportContainerItemRepositpory.UpdateRange(exportContaineritems);




                    // GenerateTrminalNumber(count);
                }

            }



            return new OkObjectResult(new { error = false, message = "GateOut Container Created" });
        }


        public IActionResult CreateExportGatePass(string containerNo)
        {

            var dateTime = DateTime.Now;

            string day = DateTime.Now.ToString("dd");
            string month = DateTime.Now.ToString("MM");
            string year = DateTime.Now.ToString("yyyy");

            var data = _exportContainerRepo.GetFirst(x => x.ContainerNumber == containerNo);

            if (data != null)
            {
                var number = GenerateNumber();
                var no = "GP-BWP-EXP-" + number + "-" + day + month + year;
                if (data.GatePassNumber != null)
                {
                    data.GatePassNumber = data.GatePassNumber;
                    data.IssueDate = data.IssueDate;
                }
                else
                {
                    data.GatePassNumber = no;
                    data.IssueDate = dateTime;
                }


                _exportContainerRepo.Update(data);



                return new OkObjectResult(new { error = false, message = data.ContainerNumber });

            }
            return new OkObjectResult(new { error = true, message = "The Container No Not Exist" });

        }

        public string GenerateNumber()
        {
            var data = _dictionaryRepo.GetFirst(x => x.Key == "GatePassNumberExport");
            if (data == null)
            {
                data.Value = 0001.ToString();
                _dictionaryRepo.Update(data);
                return "0001";

            }
            var no = Convert.ToInt64(data.Value) + 1;
            data.Value = no.ToString();
            _dictionaryRepo.Update(data);

            return data.Value;
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

        public long GetGDHold(string gdNumber)
        {
            return (_gDHoldRepository.GetGDHold(gdNumber));

        }


        public IActionResult GateOutGD()
        {
            return View();
        }

        public JsonResult GetGateOutGDs()
        {
            var data = _gateOutGdRepo.GetGateOutGDs();
            if (data != null)

            {
                return Json(data);
            }
            return Json("");
        }


        public IActionResult SaveGateOutGds(List<GateOutGD> model)
        {
            var dateTime = DateTime.Now;
            var GateOutGDs = new List<GateOutGD>();
            foreach (var item in model)
            {
                item.GateOutDateime = dateTime;

                GateOutGDs.Add(item);
            }
            _gateOutGdRepo.AddRange(GateOutGDs);

            return new OkObjectResult(new { error = true, message = "Saved" });
        }


        public JsonResult GetGateInGdInfo(string gdnumber)
        {
            var res = _gateInExportContainerRepo.GetGateInGdInfo(gdnumber);

            return Json(res);
        }

    }
}