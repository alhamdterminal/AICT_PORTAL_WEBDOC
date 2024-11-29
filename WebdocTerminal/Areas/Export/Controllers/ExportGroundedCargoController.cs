using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class ExportGroundedCargoController : ParentController
    {
        private IExportGroundedCargoRepository _exportGroundedCargoRepo;
        private IEnteringCargoRepository _enteringCargoRepo;
        private IOSRCRepository _oSRCRepo;
        private IExportBrtRepository _exportBrtRepo;
        private WebocProcessor _webocProcessor;
        private IHostingEnvironment env;
        private readonly IOptions<AppConfig> _config;
        private IOSIMRepository _osimRepo;
        private IOEHCRepository _ohcRepo;


        public ExportGroundedCargoController(IExportGroundedCargoRepository exportGroundedCargoRepo
                                            , IEnteringCargoRepository enteringCargoRepo
                                            , IOSRCRepository oSRCRepo
                                            , IExportBrtRepository exportBrtRepo
                                            , WebocProcessor webocProcessor
                                            , IHostingEnvironment _env
                                            , IOptions<AppConfig> config
                                            , IOSIMRepository osimRepo
                                            , IOEHCRepository ohcRepo)
        {
            _exportGroundedCargoRepo = exportGroundedCargoRepo;
            _enteringCargoRepo = enteringCargoRepo;
            _oSRCRepo = oSRCRepo;
            _exportBrtRepo = exportBrtRepo;
            _webocProcessor = webocProcessor;
             env = _env;
            _config = config;
            _osimRepo = osimRepo;
            _ohcRepo = ohcRepo;
        }

        public IActionResult ExportGroundedCargoView()
        {
            return View();
        }

        public IActionResult SaveGroundedCargos(List<ExportGroundingCargoViewModel> cargos)
        {


            var exportGroundedCargos = new List<ExportGroundedCargo>();
            //  var exportBRTs = new List<ExportBRT>();
            var osrcs = new List<OSRC>();
            var enteringCargos = new List<EnteringCargo>();

            var fn = $"OSRC_{DateFormatter.ConvertToyyyyMMddFormat(DateTime.Now)}_{DateFormatter.ConvertToHHmmFormat(DateTime.Now)}{".txt"}";
            var fileName = _oSRCRepo.GetAll().Where(x => x.FileName == fn).FirstOrDefault();

            if (fileName != null)
            {
                return new OkObjectResult(new PostObjectResponce { error = true, Message = "Please Proceed After One Minute" });
            }
            else
            {
                foreach (var cargo in cargos)
                {


                    var hold = _osimRepo.GetOSIMInfo(cargo.GDNumber);

                    if (hold != null && hold.Status == "HO")
                    {
                        return new OkObjectResult(new { error = true, message = "This GD" + cargo.GDNumber + "is Hold" });

                    }


                }

                foreach (var cargo in cargos)
                {

                    var grounded = new ExportGroundedCargo
                    {
                        ActivityType = cargo.ActivityType,
                        EnteringCargoId = cargo.EnteringCargoId,
                        Location = cargo.Location,
                        Weight = cargo.Weight,
                        GDNumber = cargo.GDNumber
                    };

                    //var exportBrt = new ExportBRT
                    //{
                    //    Location = cargo.Location
                    //};
                    //exportBRTs.Add(exportBrt);
                    exportGroundedCargos.Add(grounded);

                    var carg = _exportGroundedCargoRepo.GetEnteringCargoInfo(cargo.GDNumber.Trim().ToUpper());

                    if (carg != null)
                    {
                        carg.isGrounded = true;
                        enteringCargos.Add(carg);

                    }
                    var ohc = _ohcRepo.GetOEHCInfo(carg.GDNumber.Trim().ToUpper());
                    if (ohc != null)
                    {
                        var osrc = new OSRC
                        {
                            GDNumber = carg.GDNumber,
                            Weight = Convert.ToDouble(cargo.Weight),
                            Category = "E",
                            ActivityType = cargo.ActivityType,
                            HandlingCode = "EM",
                            Location = cargo.Location,
                            MessageFrom = _config.Value.MessageFrom,
                            MessageTo = "WEBOC",
                            FileName = fn
                        };

                        osrcs.Add(osrc);
                    }

                }

                _oSRCRepo.AddRange(osrcs);

                _exportGroundedCargoRepo.AddRange(exportGroundedCargos);
                //  _exportBrtRepo.AddRange(exportBRTs);
                _enteringCargoRepo.UpdateRange(enteringCargos);
            }
            return new OkObjectResult(new PostObjectResponce { error = false, Message = "Save" });
        }


    }
}