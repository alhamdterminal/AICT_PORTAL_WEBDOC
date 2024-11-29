using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class GroundingController : ParentController
    {
        private IContainerRepository _containerRepo;
        private IContainerIndexRepository _containerIndexRepo;
        private IGroundedContainerRepository _groundedRepo;
        private ISRCRepository _srcRepo;
        private ISRCORepository _srcoRepo;
        private IECMORepository _ecmoRepo;
        private ICYContainerRepository _cyContainerRepo;
        private IVoyageRepository _voyageRepo;
        private WebocProcessor _webocProcessor;
        private IHostingEnvironment env;
        private readonly IOptions<AppConfig> _config;
        private ISIMORepository _simoRepository;
        private ISIMRepository _simRepository;
        public IGDCRRepository _gdcrRepository;
        public IGoodsHeadRepository _goodsHeadRepository;
        public IPGORepository _pgoRepository;
        public ICSEmptyContainerReceiveRepository _cSEmptyContainerReceiveRepository;

        public GroundingController(IContainerRepository containerRepo,
                                   ISRCRepository srcRepo,
                                   IGroundedContainerRepository groundedRepo,
                                   IVoyageRepository voyageRepo,
                                   ISRCORepository srcoRepo,
                                   IECMORepository ecmoRepo,
                                   WebocProcessor webocProcessor,
                                   IContainerIndexRepository containerIndexRepo,
                                   IHostingEnvironment _env,
                                   IOptions<AppConfig> config,
                                   ICYContainerRepository cyContainerRepo,
                                   ISIMORepository simoRepository,
                                   ISIMRepository simRepository,
                                   IGDCRRepository gdcrRepository,
                                   IGoodsHeadRepository goodsHeadRepository,
                                   IPGORepository pgoRepository,
                                   ICSEmptyContainerReceiveRepository cSEmptyContainerReceiveRepository)
        {
            _containerRepo = containerRepo;
            _voyageRepo = voyageRepo;
            _groundedRepo = groundedRepo;
            _containerIndexRepo = containerIndexRepo;
            _srcRepo = srcRepo;
            _srcoRepo = srcoRepo;
            _ecmoRepo = ecmoRepo;
            _webocProcessor = webocProcessor;
            env = _env;
            _config = config;
            _cyContainerRepo = cyContainerRepo;
            _simoRepository = simoRepository;
            _simRepository = simRepository;
            _gdcrRepository = gdcrRepository;
            _goodsHeadRepository = goodsHeadRepository;
            _pgoRepository = pgoRepository;
            _cSEmptyContainerReceiveRepository = cSEmptyContainerReceiveRepository;
        }

        public IActionResult CYGrounding()
        {

            ViewData["GoodsHead"] = _goodsHeadRepository.GetAll()
      .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });

            return View();
        }

        public IActionResult CFSGrounding()
        {

            ViewData["GoodsHead"] = _goodsHeadRepository.GetAll()
      .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });

            return View();
        }

        public IActionResult GroundingStatusCFS()
        {
            //var totalRecords = _groundedRepo.GrounedContainers();
            //var examinationCompleted = _ecmoRepo.GetAll();

            var totalRecords = _groundedRepo.CountTotalIHCOEm();
            var examinationCompleted = _groundedRepo.CountTotalECMO();
            var totalsrco = _groundedRepo.CountTotalSRCO();

            ViewData["TotalRecords"] = totalRecords;
            ViewData["ExaminationCompleted"] = examinationCompleted;
            ViewData["TotalSRCO"] = totalsrco;

            return View();
        }

        [HttpGet]
        public List<GroundingStatusViewModel> GetGroundingContainers()
        {
            var gorundedContainers = _groundedRepo.GrounedContainers();

            return gorundedContainers;
        }
        
        [HttpGet]
        public List<GroundingStatusViewModel> GetGroundingStatusCFS()
        {
            var gorundedContainers = _groundedRepo.GroundingStatusCFS();

            return gorundedContainers;
        }

        [HttpGet]
        public List<GroundingStatusViewModel> GetUngroundedCYContainers()
        {
            var gorundedContainers = _groundedRepo.GrounedCYContainers();

            return gorundedContainers;
        }

        [HttpGet]
        public JsonResult GStatusCFS()
        {
            var gorundedContainers = _groundedRepo.GrounedContainers();
            return Json(gorundedContainers);
        }

        public IActionResult SaveCYGroundedContainers(List<CYGroundingViewModel> containers)
        {
            try
            {
                var datetime = DateTime.Now;


                bool hasMatch = _srcRepo.GetAll()
               .Any(x => containers.Any(y => y.VIRNumber == x.VIRNumber
                                              && y.ContainerNo == x.ContainerNumber
                                              && x.Performed > datetime.AddMinutes(-10)));

                if (!hasMatch)
                {

                    foreach (var item in containers)
                    {
                        var sim = _simRepository.GetAll().Where(x => x.ContainerNumber == item.ContainerNo && x.VIRNumber == item.VIRNumber).LastOrDefault();

                        if (sim != null && sim.Status == "HO")
                        {
                            return new OkObjectResult(new { error = true, message = "can't  ground due To sim hold on container no " + item.ContainerNo });

                        }

                    }

                    var groundedContainers = new List<GroundedContainer>();
                    var cycontainers = new List<CYContainer>();
                    var srcs = new List<SRC>();

                    foreach (var container in containers)
                    {
                        var groundedC = new GroundedContainer
                        {
                            GroundedDate = datetime,
                            ActivityType = container.ActivityType,
                            Category = "I",
                            CyContainerId = container.ContainerId,
                            HandlingCode = container.HandlingCode,
                            Location = container.Location,
                            Status = container.Status,
                            Weight = container.Weight
                        };

                        groundedContainers.Add(groundedC);

                        var contr = _cyContainerRepo.GetFirst(x => x.ContainerNo == container.ContainerNo && x.VIRNo == container.VIRNumber);
                        if (contr != null)
                        {
                            contr.IsGrounded = true;
                            cycontainers.Add(contr);
                        }

                        if(container.HandlingCode == "EM")
                        {
                            var src = new SRC
                            {
                                Weight =  container.Weight,
                                VIRNumber = container.VIRNumber,
                                ContainerNumber = container.ContainerNo,
                                Status = container.Status,
                                Category = "I",
                                ActivityType = container.ActivityType,
                                HandlingCode = container.HandlingCode,
                                Location = container.Location,
                                Performed = datetime,
                                //FileName = $"SRC_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                            };

                            srcs.Add(src);

                        }

                 
                    }
                     
                    var resdata = _groundedRepo.GetFileNameSRC($"SRC_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}");

                    if(resdata == true)
                    {
                        return Json(new { error = true, message = "File Name Already Exist please update after one min" });

                    }

                    srcs.ForEach(x => x.FileName = $"SRC_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}");

                    _srcRepo.AddRange(srcs);

                    _groundedRepo.AddRange(groundedContainers);

                    _cyContainerRepo.UpdateRange(cycontainers);


                    return Json(new { error = false, message = "Saved" });
                }

                else
                {
                    return new OkObjectResult(new { error = true, message = "The list contains a container which is already grounded. Please remove the container and try again." });
                }

            }
            catch (Exception e)
            {


                foreach (var container in containers)
                { 

                    var srco = _srcRepo.GetFirst(g => g.VIRNumber == container.VIRNumber &&  g.ContainerNumber == container.ContainerNo);

                    if (srco == null)
                    {
                        var contList = _cyContainerRepo.GetAll(c => c.VIRNo == container.VIRNumber && c.ContainerNo == container.ContainerNo);

                        foreach (var cntr in contList)
                        {
                            cntr.IsGrounded = false;
                            _cyContainerRepo.Update(cntr);

                            var grounded = _groundedRepo.GetFirst(g => g.CyContainerId == cntr.CYContainerId);
                            if (grounded != null)
                                _groundedRepo.Delete(grounded);
                        }
                    }

                }

                return new OkObjectResult(new { error = true, message = "Error! Please Ground Again  ." });
            }

            return null;

         
        }

         

        public IActionResult SaveCFSGroundedContainers(List<CFSGroundingViewModel> containers)
        {
            try
            {
                var datetime = DateTime.Now;


                bool hasMatch = _srcoRepo.GetAll()
               .Any(x => containers.Any(y => y.VIRNumber == x.VIRNumber
                                              && y.BLNumber == x.BLNumber
                                              && y.IndexNo == x.IndexNumber
                                              && x.Performed > datetime.AddMinutes(-10)));

                if (!hasMatch)
                {
                    var groundedContainers = new List<GroundedContainer>();
                    var indexList = new List<ContainerIndex>();
                    var srcos = new List<SRCO>();

                    foreach (var container in containers)
                    {

                       // var indexes = _containerIndexRepo.GetList(s => s.BLNo == container.BLNumber && s.Voyage.VIRNo == container.VIRNumber, s => s.Voyage);
                        var indexes = _containerIndexRepo.GetContainerIndexByIGMAndBLNo(container.VIRNumber , container.IgmoBLNumber  ).ToList();

                        var checkData = indexes.Where(x => x.IsDestuffed == false).FirstOrDefault();

                        if (checkData != null)
                        {
                            return Json(new { error = true, message = "Please First Destuff " + checkData.ContainerNo + "This Container" });

                        }


                        foreach (var item in containers)
                        {
                            var simooldbl = _simoRepository.GetAll().Where(x => x.BLNumber == item.IgmoBLNumber && x.IndexNumber == item.IndexNo.ToString() && x.VIRNumber == item.VIRNumber).LastOrDefault();

                            if (simooldbl != null && simooldbl.Status == "HO")
                            {
                                return new OkObjectResult(new { error = true, message = "can't  ground due To simo hold on Index No " + item.IndexNo });
                            }

                            var simo = _simoRepository.GetAll().Where(x => x.BLNumber == item.BLNumber && x.IndexNumber == item.IndexNo.ToString() && x.VIRNumber == item.VIRNumber).LastOrDefault();

                            if (simo != null && simo.Status == "HO")
                            {
                                return new OkObjectResult(new { error = true, message = "can't  ground due To simo hold on Index No " + item.IndexNo });

                            }
                        }

                        //foreach (var index in indexes)
                        //{
                        //    index.IsGrounded = true;
                        //    index.Voyage = null;
                        //    indexList.Add(index);

                        //    //var groundedC = new GroundedContainer
                        //    //{
                        //    //    GroundedDate = datetime,
                        //    //    ActivityType = container.ActivityType,
                        //    //    Category = "I",
                        //    //    ContainerIndexId = index.ContainerIndexId,
                        //    //    HandlingCode = container.HandlingCode,
                        //    //    Location = container.Location,
                        //    //    Status = container.Status,
                        //    //    Weight = container.Weight
                        //    //};

                        //    //groundedContainers.Add(groundedC);
                        //}

                        indexes.ForEach(x => x.IsGrounded = true);
                        indexList.AddRange(indexes);
                        if (container.HandlingCode == "EM")
                        {
                            var srco = new SRCO
                            {
                                Weight =   container.Weight,
                                VIRNumber = container.VIRNumber,
                                BLNumber = container.BLNumber,
                                IndexNumber = container.IndexNo,
                                Category = "I",
                                ActivityType = container.ActivityType,
                                HandlingCode = container.HandlingCode,
                                Location = container.Location,
                                Performed = datetime,
                                //FileName = $"SRCO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}",
                                CreateDT = DateTime.Now,
                                MessageFrom = "AIT",
                                MessageTo = "WEBOC"
                            };

                            srcos.Add(srco);
                        }

                      
                    }



                    var resdata = _groundedRepo.GetFileNameSRCO($"SRCO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}");

                    if (resdata == true)
                    {
                        return Json(new { error = true, message = "File Name Already Exist please update after one min" });

                    }

                    srcos.ForEach(x => x.FileName = $"SRCO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}");

                    //_groundedRepo.AddRange(groundedContainers);
                    _srcoRepo.AddRange(srcos);
                    _containerIndexRepo.UpdateRange(indexList);

                    return Json(new { error = false, message = "Saved" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "The list contains a container which is already grounded. Please remove the container and try again." });
                }
                
            }
            catch (Exception e)
            {

                foreach (var container in containers)
                {
                    var voyage = _voyageRepo.GetFirst(v => v.VIRNo == container.VIRNumber);

                    var srco = _srcoRepo.GetFirst(g => g.VIRNumber == container.VIRNumber && g.IndexNumber == container.IndexNo && g.BLNumber == container.BLNumber);

                    if (srco == null)
                    {
                        var contList = _containerIndexRepo.GetAll(c => c.VoyageId == voyage.VoyageId && c.IndexNo == container.IndexNo && container.BLNumber == c.BLNo);

                        foreach (var cntr in contList)
                        {
                            cntr.IsGrounded = false;
                            _containerIndexRepo.Update(cntr);

                            var grounded = _groundedRepo.GetFirst(g => g.ContainerIndexId == cntr.ContainerIndexId);
                            if (grounded != null)
                                _groundedRepo.Delete(grounded);
                        }
                    }

                }

                return new OkObjectResult(new { error = true, message = "Error! Please Ground  again." });

            }

            return null;

        }


        public IActionResult CrossStuffingView()
        {
            ViewData["BLNumber"] = _containerRepo.GetUnCrossStuffingDetailBL()
              .Select(v => new SelectListItem { Text = v.BLNumber, Value = v.BLNumber });


            return View();
        }


        public IActionResult SaveGDCRLst(List<GDCR> containers)
        {
            try
            {

                var resdata = _cyContainerRepo.GetCYContainersByBL(containers[0].BLNumber).ToList();

                if (resdata.Count() > 0)
                {

                    //containers.ForEach(x => x.Status = "P");
                    //containers.LastOrDefault().Status = "F";

                    foreach (var item in resdata)
                    {
                        var result = containers.Where(x => x.VirNumber == item.VIRNo && x.BLNumber == item.BLNo && x.OldContainerNumber == item.ContainerNo).LastOrDefault();
                        if (result == null)
                        {
                            return new OkObjectResult(new { error = true, message = "please select " + item.ContainerNo + " - " + item.BLNo + " - " + item.VIRNo });
                        }
                    }

                    foreach (var container in containers)
                    {
                        container.CreateDT = DateTime.Now;

                        var result = _gdcrRepository.GetInfo(container.VirNumber, container.OldContainerNumber, container.BLNumber);

                        if (result != null)
                        {
                            result.Flag1 = container.Flag1;
                            result.Flag2 = container.Flag2;
                            result.Flag3 = container.Flag3;
                            result.IsContainerStuffed = container.IsContainerStuffed;
                            result.NewContainerNumber = container.NewContainerNumber;
                            result.OperationType = container.OperationType;
                            result.Status = container.Status;
                            result.OrderNumber = container.OrderNumber;

                            _gdcrRepository.Update(result);

                        }
                        else
                        {
                            _gdcrRepository.Add(container);

                        }

                    }

                    return Json(new { error = false, message = "Saved" });


                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no bl data found" });
                }



            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }

            return null;

        }



        public IActionResult CreateEdiGDCRLst(List<GDCR> containers)
        {
            try
            {

                var resdata = _cyContainerRepo.GetCYContainersByBL(containers[0].BLNumber).ToList();
                 

                if (resdata.Count() > 0)
                {
                    containers = containers.OrderBy(x => x.OrderNumber).ToList();

                    containers.ForEach(x => x.Status = "P");
                    containers.OrderBy(x => x.OrderNumber).LastOrDefault().Status = "F";

                    foreach (var item in resdata)
                    {
                        var result = containers.Where(x => x.VirNumber == item.VIRNo && x.BLNumber == item.BLNo && x.OldContainerNumber == item.ContainerNo).LastOrDefault();
                        if (result == null)
                        {
                            return new OkObjectResult(new { error = true, message = "please select " + item.ContainerNo + " - " + item.BLNo + " - " + item.VIRNo });
                        }
                    }

                    var datetime = DateTime.Now;



                    foreach (var container in containers)
                    {
                        container.CreateDT = DateTime.Now;
                        container.IsSubmit = true;

                        var result = _gdcrRepository.GetInfo(container.VirNumber, container.OldContainerNumber, container.BLNumber);

                        if (result != null)
                        {
                            result.ContainerConsolidation = container.ContainerConsolidation;
                            result.Flag1 = container.Flag1;
                            result.Flag2 = container.Flag2;
                            result.Flag3 = container.Flag3;
                            result.IsContainerStuffed = container.IsContainerStuffed;
                            result.NewContainerNumber = container.NewContainerNumber;
                            result.OperationType = container.OperationType;
                            result.IsSubmit = container.IsSubmit;
                            result.Status = container.Status;
                            result.Performed = DateTime.Now;
                            result.FileName = $"GDCR_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}";
                            _gdcrRepository.Update(result);

                        }
                        else
                        {
                            container.Performed = DateTime.Now;
                            container.FileName = $"GDCR_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}";
                            _gdcrRepository.Add(container);

                        }

                        //var containersresult = _gdcrRepository.GetContainerInfo(container.VirNumber, container.OldContainerNumber, container.BLNumber).ToList();

                        //if (containersresult.Count() > 0)
                        //{
                        //    containersresult.ForEach(x => x.IsCrossStuffed = true);

                        //    _cyContainerRepo.UpdateRange(containersresult);
                        //}

                    }

                    resdata.ForEach(x => x.IsCrossStuffed = true);
                    
                    _cyContainerRepo.UpdateRange(resdata);


                    if (resdata.Count() > 0)
                    {
                        var asdasd = _cSEmptyContainerReceiveRepository.GetAll().Where(x => resdata.Any(y => y.CSEmptyContainerReceiveId == x.CSEmptyContainerReceiveId)).ToList();

                        if(asdasd.Count() > 0)
                        {
                            asdasd.ForEach(x => x.OutDate = DateTime.Now);
                            _cSEmptyContainerReceiveRepository.UpdateRange(asdasd);
                        }

                    }

                    return Json(new { error = false, message = "Saved" });

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no bl data found" });
                }


            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }

            return null;

        }


        public IActionResult CrossStuffPreGateOutView()
        {
            return View();
        }

        public IActionResult CreatePreGateOutCrossStuff(List<PGO> modelist)
        {
            var resdata = new List<PGO>();

            var datetime = DateTime.Now;

            bool hasMatch = _pgoRepository.GetAll().Any(x => modelist.Any(y => y.VIRNumber == x.VIRNumber && y.ContainerNumber == x.ContainerNumber));


            if (hasMatch)
            {
                return Json(new { error = true, message = "Already Avaible" });
            }
            else
            {

                try
                {
                    foreach (var item in modelist)
                    {
                        var pgo = new PGO
                        {
                            VIRNumber = item.VIRNumber,
                            ContainerNumber = item.ContainerNumber,
                            VehicleNumber = item.VehicleNumber,
                            BondedCarrierId = item.BondedCarrierId,
                            BondedCarrierName = item.BondedCarrierName,
                            Performed = datetime,
                            CreateDT = datetime,
                            MessageTo = "WEBOC",
                            OpType = "A",
                            IsProcessed = false,
                            FileName = $"PGO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}",
                        };

                        resdata.Add(pgo);

                    }

                    _pgoRepository.AddRange(resdata);




                    return Json(new { error = false, message = "Save" });
                }
                catch (Exception e)
                {
                    return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
                }

            }

        }


        public IActionResult ReprocessSCRO()
        {
            return View();
        }

        public IActionResult SaveReprocessSCRO(List<SRCO> containers)
        {
            try
            {
                var datetime = DateTime.Now;


                bool hasMatch = _srcoRepo.GetAll()
               .Any(x => containers.Any(y => y.VIRNumber == x.VIRNumber
                                              && y.BLNumber == x.BLNumber
                                              && y.IndexNumber == x.IndexNumber
                                              && x.Performed > datetime.AddMinutes(-10)));

                if (!hasMatch)
                {
                      var srcos = new List<SRCO>();

                    foreach (var container in containers)
                    {
                         
                        foreach (var item in containers)
                        { 

                            var simo = _simoRepository.GetAll().Where(x => x.BLNumber == item.BLNumber && x.IndexNumber == item.IndexNumber.ToString() && x.VIRNumber == item.VIRNumber).LastOrDefault();

                            if (simo != null && simo.Status == "HO")
                            {
                                return new OkObjectResult(new { error = true, message = "can't  ground due To simo hold on Index No " + item.IndexNumber });

                            }
                        }

                        

                        if (container.HandlingCode == "EM")
                        {
                            var srco = new SRCO
                            {
                                Weight = container.Weight,
                                VIRNumber = container.VIRNumber,
                                BLNumber = container.BLNumber,
                                IndexNumber = container.IndexNumber,
                                Category = "I",
                                ActivityType = container.ActivityType,
                                HandlingCode = container.HandlingCode,
                                Location = container.Location,
                                Performed = datetime,
                                //FileName = $"SRCO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}",
                                CreateDT = DateTime.Now,
                                MessageFrom = "AIT",
                                MessageTo = "WEBOC"
                            };

                            srcos.Add(srco);
                        }


                    }



                    var resdata = _groundedRepo.GetFileNameSRCO($"SRCO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}");

                    if (resdata == true)
                    {
                        return Json(new { error = true, message = "File Name Already Exist please update after one min" });

                    }

                    srcos.ForEach(x => x.FileName = $"SRCO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}");
                     
                    _srcoRepo.AddRange(srcos); 

                    return Json(new { error = false, message = "Saved" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "The list contains a container which is already grounded. Please remove the container and try again." });
                }

            }
            catch (Exception e)
            { 
                return new OkObjectResult(new { error = true, message = "Error! Please Ground  again." });

            }

            return null;

        }

        public IActionResult ReprocessSCR()
        {
            return View();
        }

        public IActionResult SaveReprocessSCR(List<SRC> containers)
        {
            try
            {
                var datetime = DateTime.Now;


                bool hasMatch = _srcRepo.GetAll()
               .Any(x => containers.Any(y => y.VIRNumber == x.VIRNumber
                                              && y.ContainerNumber == x.ContainerNumber
                                              && x.Performed > datetime.AddMinutes(-10)));

                if (!hasMatch)
                {

                    foreach (var item in containers)
                    {
                        var sim = _simRepository.GetAll().Where(x => x.ContainerNumber == item.ContainerNumber && x.VIRNumber == item.VIRNumber).LastOrDefault();

                        if (sim != null && sim.Status == "HO")
                        {
                            return new OkObjectResult(new { error = true, message = "can't  ground due To sim hold on container no " + item.ContainerNumber });

                        }

                    }
                     
                    var srcs = new List<SRC>();

                    foreach (var container in containers)
                    {
                         
                        if (container.HandlingCode == "EM")
                        {
                            var src = new SRC
                            {
                                Weight = container.Weight,
                                VIRNumber = container.VIRNumber,
                                ContainerNumber = container.ContainerNumber,
                                Status = container.Status,
                                Category = "I",
                                ActivityType = container.ActivityType,
                                HandlingCode = container.HandlingCode,
                                Location = container.Location,
                                Performed = datetime,
                                //FileName = $"SRC_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                            };

                            srcs.Add(src);

                        }


                    }

                    var resdata = _groundedRepo.GetFileNameSRC($"SRC_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}");

                    if (resdata == true)
                    {
                        return Json(new { error = true, message = "File Name Already Exist please update after one min" });

                    }

                    srcs.ForEach(x => x.FileName = $"SRC_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}");

                    _srcRepo.AddRange(srcs); 


                    return Json(new { error = false, message = "Saved" });
                }

                else
                {
                    return new OkObjectResult(new { error = true, message = "The list contains a container which is already grounded. Please remove the container and try again." });
                }

            }
            catch (Exception e)
            { 
                return new OkObjectResult(new { error = true, message = "Error! Please Ground Again  ." });
            }

            return null;


        }


    }
}
 