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
    public class ContainerDestuffController : ParentController
    {
        private IContainerRepository _containerRepo;

        private IDestuffedContainerRepository _destuffRepo;

        private IContainerIndexRepository _cIndexRepo;

        public IVesselRepository _vesselRepo;

        public ITellySheetRepository _tellySheetRepo;
        public ITTSORepository _ttSORepo;

        public IShippingAgentRepository _shippingAgentRepo;
        private readonly IOptions<AppConfig> _config;
        private WebocProcessor _webocProcessor;
        private IHostingEnvironment env;
        private IInvoiceRepository _invoiceRepository;

        public ContainerDestuffController(IContainerRepository containerRepo, IDestuffedContainerRepository destuffRepo, IVesselRepository vesselRepo, IContainerIndexRepository cIndexRepo,
             ITellySheetRepository tellySheetRepo, ITTSORepository ttSORepo, IShippingAgentRepository shippingAgentRepo, IOptions<AppConfig> config,
             IHostingEnvironment _env, WebocProcessor webocProcessor,
             IInvoiceRepository invoiceRepository)
        {
            _containerRepo = containerRepo;
            _destuffRepo = destuffRepo;
            _vesselRepo = vesselRepo;
            _cIndexRepo = cIndexRepo;
            _tellySheetRepo = tellySheetRepo;
            _ttSORepo = ttSORepo;
            _shippingAgentRepo = shippingAgentRepo;
            _config = config;
            _webocProcessor = webocProcessor;
            env = _env;
            _invoiceRepository = invoiceRepository;

        }

        public IActionResult ContainerDestuffCFS()
        {
            ViewData["Vessels"] = _vesselRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.VesselName, Value = v.VesselName });

            ViewData["ShippingAgents"] = _shippingAgentRepo.GetAll()
         .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        [HttpPost]
        public IActionResult Destuff(TellySheet tellySheet, List<DestuffingViewModel> containers)
        {
           
            
            var datetime = DateTime.Now;
            var datetime2 = DateTime.UtcNow;


            if (tellySheet.DestuffDate == null)
            {

                tellySheet.DestuffDate = datetime;
            }
            else
            {
                tellySheet.DestuffDate = tellySheet.DestuffDate.Value.Add(DateTime.Now.TimeOfDay);
            }

            var dstfContainers = new List<DestuffedContainer>();

            if (containers.Count > 0)
            {
                var containerindex = _cIndexRepo.GetAll().Where(x => x.ContainerIndexId == containers[0].ContainerIndexId).FirstOrDefault();
                var bl = _cIndexRepo.GetAll().Where(x => x.BLNo == containerindex.BLNo).ToList().Count;
                var conts = containers.Count;
                 
                if ( conts == bl)
                {
                    _tellySheetRepo.Add(tellySheet);


                    foreach (var container in containers)
                    {

                        var destutuffedContainer = _destuffRepo.GetAll().Where(x => x.ContainerIndexId == container.ContainerIndexId).FirstOrDefault();
                        if (destutuffedContainer != null)
                        {

                            destutuffedContainer.TellySheetId = tellySheet.TellySheetId;
                            destutuffedContainer.ContainerIndexId = container.ContainerIndexId;
                            destutuffedContainer.Index = container.IndexNumber ?? 0;
                            destutuffedContainer.Description = container.Description;
                            destutuffedContainer.PackageQuantity = container.Package;
                            destutuffedContainer.PackageType = container.PackageType;
                            destutuffedContainer.FoundPackageType = container.FoundPackageType;
                            destutuffedContainer.CBM = container.CBM;
                            destutuffedContainer.ManifestWeight = container.ManifestWeight > 0 ? Convert.ToInt32(container.ManifestWeight) : 0;
                            destutuffedContainer.FoundWeight = container.FoundWeight > 0 ? Convert.ToInt32(container.FoundWeight) : 0;
                            destutuffedContainer.MarksAndNumber = container.MarksAndNumber;
                            destutuffedContainer.Location = container.Location;
                            destutuffedContainer.Remarks = container.Remarks;
                            destutuffedContainer.InvoiceFound = container.InvoiceFoud;
                            destutuffedContainer.ShortExcess = container.ShortExcess;
                            destutuffedContainer.ShortExcessRemarks = container.ShortExcessRemarks;
                            destutuffedContainer.ConsigneeName = container.CosigneeName;
                            destutuffedContainer.ManifestQuantity = container.ManifestQuantity;

                            _destuffRepo.Update(destutuffedContainer);

                            var indexdata = _cIndexRepo.Find(container.ContainerIndexId);
                            if (indexdata != null)
                            {
                                indexdata.IsDestuffed = true;
                                indexdata.CBM = container.CBM ?? 0;
                                indexdata.BLGrossWeight = container.FoundWeight;
                                _cIndexRepo.Update(indexdata);
                            }
                        }

                        else {

                            var destuff = new DestuffedContainer
                            {
                                TellySheetId = tellySheet.TellySheetId,
                                ContainerIndexId = container.ContainerIndexId,
                                Index = container.IndexNumber ?? 0,
                                Description = container.Description,
                                PackageQuantity = container.Package,
                                PackageType = container.PackageType,
                                FoundPackageType = container.FoundPackageType,
                                CBM = container.CBM,
                                ManifestWeight = container.ManifestWeight > 0 ? Convert.ToInt32(container.ManifestWeight) : 0,
                                FoundWeight = container.FoundWeight > 0 ? Convert.ToInt32(container.FoundWeight) : 0,
                                MarksAndNumber = container.MarksAndNumber,
                                Location = container.Location,
                                Remarks = container.Remarks,
                                InvoiceFound = container.InvoiceFoud,
                                ShortExcess = container.ShortExcess,
                                ShortExcessRemarks = container.ShortExcessRemarks,
                                ConsigneeName = container.CosigneeName,
                                ManifestQuantity = container.ManifestQuantity
                            };

                            dstfContainers.Add(destuff);

                            var index = _cIndexRepo.Find(container.ContainerIndexId);
                            if (index != null)
                            {
                                index.IsDestuffed = true;
                                index.CBM = container.CBM ?? 0;
                                index.BLGrossWeight = container.FoundWeight;
                                _cIndexRepo.Update(index);
                            }
                        }


                    }

                    _destuffRepo.AddRange(dstfContainers);
 
                }
                else
                {
                    return Json(new { error = true, message = "Please Must Destuffed This BL All Container" });
                }
             
            }


            return Json(new { error = false, message = "Saved" });

        }


        [HttpPost]
        public IActionResult DestuffSaved(  List<DestuffingViewModel> containers)
        { 

            var dstfContainers = new List<DestuffedContainer>();

            if (containers.Count > 0)
            {
                foreach (var container in containers)
                {

                    var destutuffedContainer = _destuffRepo.GetAll().Where(x => x.ContainerIndexId == container.ContainerIndexId).FirstOrDefault();
                    if (destutuffedContainer != null)
                    {


                        destutuffedContainer.ContainerIndexId = container.ContainerIndexId;
                        destutuffedContainer.Index = container.IndexNumber ?? 0;
                        destutuffedContainer.Description = container.Description;
                        destutuffedContainer.PackageQuantity = container.Package;
                        destutuffedContainer.PackageType = container.PackageType;
                        destutuffedContainer.FoundPackageType = container.FoundPackageType;
                        destutuffedContainer.CBM = container.CBM;
                        destutuffedContainer.ManifestWeight = container.ManifestWeight > 0 ? Convert.ToInt32(container.ManifestWeight) : 0;
                        destutuffedContainer.FoundWeight = container.FoundWeight > 0 ? Convert.ToInt32(container.FoundWeight) : 0;
                        destutuffedContainer.MarksAndNumber = container.MarksAndNumber;
                        destutuffedContainer.Location = container.Location;
                        destutuffedContainer.Remarks = container.Remarks;
                        destutuffedContainer.InvoiceFound = container.InvoiceFoud;
                        destutuffedContainer.ShortExcess = container.ShortExcess;
                        destutuffedContainer.ShortExcessRemarks = container.ShortExcessRemarks;
                        destutuffedContainer.ConsigneeName = container.CosigneeName;
                        destutuffedContainer.ManifestQuantity = container.ManifestQuantity;
                         
                        _destuffRepo.Update(destutuffedContainer);

                        var index = _cIndexRepo.Find(container.ContainerIndexId);
                        if (index != null)
                        {
                            index.CBM = container.CBM ?? 0;
                            index.BLGrossWeight = container.FoundWeight;
                            _cIndexRepo.Update(index);
                        }
                    }
                    else
                    {

                  

                    var destuff = new DestuffedContainer
                    {

                        ContainerIndexId = container.ContainerIndexId,
                        Index = container.IndexNumber ?? 0,
                        Description = container.Description,
                        PackageQuantity = container.Package,
                        PackageType = container.PackageType,
                        FoundPackageType = container.FoundPackageType,
                        CBM = container.CBM,
                        ManifestWeight = container.ManifestWeight > 0 ? Convert.ToInt32(container.ManifestWeight) : 0,
                        FoundWeight = container.FoundWeight > 0 ? Convert.ToInt32(container.FoundWeight) : 0,
                        MarksAndNumber = container.MarksAndNumber,
                        Location = container.Location,
                        Remarks = container.Remarks,
                        InvoiceFound = container.InvoiceFoud,
                        ShortExcess = container.ShortExcess,
                        ShortExcessRemarks = container.ShortExcessRemarks,
                        ConsigneeName = container.CosigneeName,
                        ManifestQuantity = container.ManifestQuantity
                    };

                    dstfContainers.Add(destuff);

                    var index = _cIndexRepo.Find(container.ContainerIndexId);
                    if (index != null)
                    {
                        index.CBM = container.CBM ?? 0;
                        index.BLGrossWeight = container.FoundWeight;
                        _cIndexRepo.Update(index);
                    }
                }
                    }

                    _destuffRepo.AddRange(dstfContainers);

                
                

            }


            return Json(new { error = false, message = "Saved" });

        }



        [HttpPost]
        public IActionResult DestuffIndexWise(TellySheet tellySheet, List<DestuffingViewModel> containers)
        {
            try
            {
                bool hasMatch = _ttSORepo.GetAll().Any(x => containers.Any(y => y.VIRNumber == x.VIRNo && y.IndexNumber == x.IndexNo && y.BLNo == x.BLNo && y.ContainerNo == x.ContainerNo));

                if (!hasMatch)
                {
                    var datetime = DateTime.Now;
                    var datetime2 = DateTime.UtcNow;

                    if (tellySheet.DestuffDate == null)
                    {
                        tellySheet.DestuffDate = datetime;

                    }
                    else
                    {
                        tellySheet.DestuffDate = tellySheet.DestuffDate.Value.Add(DateTime.Now.TimeOfDay);
                    }

                    var dstfContainers = new List<DestuffedContainer>();
                    var containerIndexs = new List<ContainerIndex>();
                    var ttsos = new List<TTSO>();

                    //foreach (var item in containers)
                    //{

                    //    if (item.CosigneeName == null)
                    //    {
                    //        return Json(new { error = true, message = "The BL " + item.BLNo + " Has No Consignee" });

                    //    }
                    //}

                    if (containers.Count > 0)
                    {

                        foreach (var container in containers)
                        {
                            //   var indeces = _cIndexRepo.GetAll().Where(x => x.ContainerNo == container.ContainerNo ,x=> x.Voyage.VIRNo == container.VIRNumber).Select(v => v.ContainerIndexId).ToList();
                            // var indeces2 = _cIndexRepo.GetAll().Where(x => x.ContainerNo == container.ContainerNo && x.Voyage.VIRNo == container.VIRNumber).Select(v => v.ContainerIndexId).ToList();
                            //var indeces = _cIndexRepo.GetList(x=> x.ContainerNo == container.ContainerNo , x=> x.Voyage.VIRNo == container.VIRNumber).Select(v => v.ContainerIndexId).ToList();
                            //


                            // var indeces = _destuffRepo.getContainerIndex(container.ContainerNo, container.VIRNumber).Select(v => v.ContainerIndexId).ToList();
                            var indeces = _cIndexRepo.GetIndexInfo(container.VIRNumber, container.IndexNumber ?? 0).Select(v => v.ContainerIndexId).ToList();


                            if (indeces != null)
                            {

                                var destutuffedContainer = _destuffRepo.GetAll().Where(t => indeces.Contains(t.ContainerIndexId)).LastOrDefault();
                                if (destutuffedContainer != null)
                                {
                                    var destuff = new DestuffedContainer
                                    {
                                        TellySheetId = destutuffedContainer.TellySheetId,
                                        ContainerIndexId = container.ContainerIndexId,
                                        Index = container.IndexNumber ?? 0,
                                        Description = container.Description,
                                        PackageQuantity = container.Package,
                                        PackageType = container.PackageType,
                                        FoundPackageType = container.FoundPackageType,
                                        CBM = container.CBM,
                                        ManifestWeight = container.ManifestWeight > 0 ? Convert.ToInt32(container.ManifestWeight) : 0,
                                        FoundWeight = container.FoundWeight > 0 ? container.FoundWeight : 0,
                                        MarksAndNumber = container.MarksAndNumber,
                                        Location = container.Location,
                                        Remarks = container.Remarks,
                                        InvoiceFound = container.InvoiceFoud,
                                        ShortExcess = container.ShortExcess,
                                        ShortExcessRemarks = container.ShortExcessRemarks,
                                        ConsigneeName = container.CosigneeName,
                                        ManifestQuantity = container.ManifestQuantity,
                                        InvoiceAmount = container.InvoiceAmount,
                                        VehicleMeasurementId = container.VehicleMeasurementId
                                    };

                                    _destuffRepo.Add(destuff);
                                     
                                    var indexdata = _cIndexRepo.GetContainerIndexById(container.ContainerIndexId);

                                    if (indexdata != null)
                                    {
                                        indexdata.IsDestuffed = true;
                                        indexdata.CBM = container.CBM ?? 0;
                                        //indexdata.IsHold = container.IsHold;
                                        containerIndexs.Add(indexdata);
                                    }
                                }

                                else
                                {

                                    _tellySheetRepo.Add(tellySheet);
                                    var destuff = new DestuffedContainer
                                    {
                                        TellySheetId = tellySheet.TellySheetId,
                                        ContainerIndexId = container.ContainerIndexId,
                                        Index = container.IndexNumber ?? 0,
                                        Description = container.Description,
                                        PackageQuantity = container.Package,
                                        PackageType = container.PackageType,
                                        FoundPackageType = container.FoundPackageType,
                                        CBM = container.CBM,
                                        ManifestWeight = container.ManifestWeight > 0 ? Convert.ToInt32(container.ManifestWeight) : 0,
                                        FoundWeight = container.FoundWeight > 0 ? container.FoundWeight : 0,
                                        MarksAndNumber = container.MarksAndNumber,
                                        Location = container.Location,
                                        Remarks = container.Remarks,
                                        InvoiceFound = container.InvoiceFoud,
                                        ShortExcess = container.ShortExcess,
                                        ShortExcessRemarks = container.ShortExcessRemarks,
                                        ConsigneeName = container.CosigneeName,
                                        ManifestQuantity = container.ManifestQuantity,
                                        InvoiceAmount = container.InvoiceAmount,
                                        VehicleMeasurementId = container.VehicleMeasurementId
                                    };
                                    _destuffRepo.Add(destuff);

                                    var ttso = new TTSO()
                                    {
                                        Category = "I",
                                        VIRNo = container.VIRNumber,
                                        BLNo = container.BLNo,
                                        IndexNo = container.IndexNumber,
                                        Weight = container.ManifestWeight,
                                        ContainerNo = container.ContainerNo,
                                        OpType = "A",
                                        Performed = datetime,
                                        CreateDT = DateTime.Now,
                                        MessageFrom = "AIT",
                                        MessageTo = "WEBOC",
                                        FileName = $"TTSO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"

                                    };

                                    _ttSORepo.Add(ttso);

                                    var indexdata = _cIndexRepo.GetContainerIndexById(container.ContainerIndexId);
                                    if (indexdata != null)
                                    {
                                        indexdata.IsDestuffed = true;
                                        indexdata.CBM = container.CBM ?? 0;
                                        //indexdata.IsHold = container.IsHold;
                                        containerIndexs.Add(indexdata);
                                    }
                                }
                            }
                            else
                            {

                                return Json(new { error = false, message = "This Conatainer No " + container.ContainerNo + " And This Vir " + container.VIRNumber + "Not Found" });
                            }




                        }
                         
                        _cIndexRepo.UpdateRange(containerIndexs);

                        if (containers.Count() > 0)
                        {
                            //var indeces2 = _cIndexRepo.GetAll().Where(x => x.ContainerNo == containers[0].ContainerNo && x.VirNo == containers[0].VIRNumber).ToList();
                            var indeces2 = _cIndexRepo.GetIndexInfo(containers[0].VIRNumber, containers[0].IndexNumber ?? 0).ToList();

                            if (indeces2.Count() > 0)
                            {
                                var checkData = indeces2.Where(x => x.IsDestuffed == false).FirstOrDefault();

                                if (checkData == null)
                                {




                                    foreach (var item in indeces2)
                                    {
                                        var tempUpdateQuery = string.Format(@"UPDATE ContainerIndex SET IsEmptyOut = '" + 1 + "'  WHERE  ContainerIndexId  = '" + item.ContainerIndexId + "'  ");
                                        _cIndexRepo.ExecuteNonSQL(tempUpdateQuery);

                                        //item.IsEmptyOut = true;
                                    }

                                    //   _cIndexRepo.UpdateRange(indeces2);

                                }

                            }
                        }







                    }

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "The list indexes a indexes which is already destuffed in. Please remove the index and try again." });
                }





                return Json(new { error = false, message = "Saved" });

            }
            catch (Exception e)
            {
                return Json(new { error = true, message = "Alert Please Destuffed Again" });

            }

        }




        [HttpPost]
        public IActionResult DestuffContainerWise(TellySheet tellySheet, List<DestuffingViewModel> containers)
        {
            try
            {
                bool hasMatch = _ttSORepo.GetAll().Any(x => containers.Any(y => y.VIRNumber == x.VIRNo && y.IndexNumber == x.IndexNo && y.BLNo == x.BLNo && y.ContainerNo == x.ContainerNo));

                if (!hasMatch)
                {
                    var datetime = DateTime.Now;
                    var datetime2 = DateTime.UtcNow;

                    if (tellySheet.DestuffDate == null)
                    {
                        tellySheet.DestuffDate = datetime;

                    }
                    else
                    {
                        tellySheet.DestuffDate = tellySheet.DestuffDate.Value.Add(DateTime.Now.TimeOfDay);
                    }


                    var dstfContainers = new List<DestuffedContainer>();
                    var containerIndexs = new List<ContainerIndex>();
                    var ttsos = new List<TTSO>();

                    //foreach (var item in containers)
                    //{

                    //    if (item.CosigneeName == null)
                    //    {
                    //        return Json(new { error = true, message = "The BL " + item.BLNo + " Has No Consignee" });

                    //    }
                    //}

                    if (containers.Count > 0)
                    {

                        foreach (var container in containers)
                        {
                            //   var indeces = _cIndexRepo.GetAll().Where(x => x.ContainerNo == container.ContainerNo ,x=> x.Voyage.VIRNo == container.VIRNumber).Select(v => v.ContainerIndexId).ToList();
                            // var indeces2 = _cIndexRepo.GetAll().Where(x => x.ContainerNo == container.ContainerNo && x.Voyage.VIRNo == container.VIRNumber).Select(v => v.ContainerIndexId).ToList();
                            //var indeces = _cIndexRepo.GetList(x=> x.ContainerNo == container.ContainerNo , x=> x.Voyage.VIRNo == container.VIRNumber).Select(v => v.ContainerIndexId).ToList();
                            //


                           // var indeces = _destuffRepo.getContainerIndex(container.ContainerNo, container.VIRNumber).Select(v => v.ContainerIndexId).ToList();
                            var indeces = _cIndexRepo.GetContainerIndexByIGMAndContainerNo(container.VIRNumber , container.ContainerNo ).Select(v => v.ContainerIndexId).ToList();


                            if (indeces != null)
                            {

                                var destutuffedContainer = _destuffRepo.GetAll().Where(t => indeces.Contains(t.ContainerIndexId)).LastOrDefault();
                                if (destutuffedContainer != null)
                                {
                                    var destuff = new DestuffedContainer
                                    {
                                        TellySheetId = destutuffedContainer.TellySheetId,
                                        ContainerIndexId = container.ContainerIndexId,
                                        Index = container.IndexNumber ?? 0,
                                        Description = container.Description,
                                        PackageQuantity = container.Package,
                                        PackageType = container.PackageType,
                                        FoundPackageType = container.FoundPackageType,
                                        CBM = container.CBM,
                                        ManifestWeight = container.ManifestWeight > 0 ? Convert.ToInt32(container.ManifestWeight) : 0,
                                        FoundWeight = container.FoundWeight > 0 ?  container.FoundWeight : 0,
                                        MarksAndNumber = container.MarksAndNumber,
                                        Location = container.Location,
                                        Remarks = container.Remarks,
                                        InvoiceFound = container.InvoiceFoud,
                                        ShortExcess = container.ShortExcess,
                                        ShortExcessRemarks = container.ShortExcessRemarks,
                                        ConsigneeName = container.CosigneeName,
                                        ManifestQuantity = container.ManifestQuantity,
                                        InvoiceAmount = container.InvoiceAmount,
                                        VehicleMeasurementId = container.VehicleMeasurementId
                                    };

                                    _destuffRepo.Add(destuff);

                                    var ttso = new TTSO()
                                    {
                                        Category = "I",
                                        VIRNo = container.VIRNumber,
                                        BLNo = container.BLNo,
                                        IndexNo = container.IndexNumber,
                                        Weight = container.ManifestWeight,
                                        ContainerNo = container.ContainerNo,
                                        OpType = "A",
                                        Performed = datetime,
                                        CreateDT = DateTime.Now,
                                        MessageFrom = "AIT",
                                        MessageTo = "WEBOC",
                                        FileName = $"TTSO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"

                                    };

                                    ttsos.Add(ttso);

                                    var indexdata = _cIndexRepo.GetContainerIndexById(container.ContainerIndexId);

                                    if (indexdata != null)
                                    {
                                        indexdata.IsDestuffed = true;                                        
                                        indexdata.CBM = container.CBM ?? 0;
                                        //indexdata.IsHold = container.IsHold; 
                                        containerIndexs.Add(indexdata);
                                    }
                                }

                                else
                                {

                                    _tellySheetRepo.Add(tellySheet);
                                    var destuff = new DestuffedContainer
                                    {
                                        TellySheetId = tellySheet.TellySheetId,
                                        ContainerIndexId = container.ContainerIndexId,
                                        Index = container.IndexNumber ?? 0,
                                        Description = container.Description,
                                        PackageQuantity = container.Package,
                                        PackageType = container.PackageType,
                                        FoundPackageType = container.FoundPackageType,
                                        CBM = container.CBM,
                                        ManifestWeight = container.ManifestWeight > 0 ? Convert.ToInt32(container.ManifestWeight) : 0,
                                        FoundWeight = container.FoundWeight > 0 ?  container.FoundWeight : 0,
                                        MarksAndNumber = container.MarksAndNumber,
                                        Location = container.Location,
                                        Remarks = container.Remarks,
                                        InvoiceFound = container.InvoiceFoud,
                                        ShortExcess = container.ShortExcess,
                                        ShortExcessRemarks = container.ShortExcessRemarks,
                                        ConsigneeName = container.CosigneeName,
                                        ManifestQuantity = container.ManifestQuantity,
                                        InvoiceAmount = container.InvoiceAmount,
                                        VehicleMeasurementId = container.VehicleMeasurementId
                                    };
                                    _destuffRepo.Add(destuff);
                                    var ttso = new TTSO()
                                    {
                                        Category = "I",
                                        VIRNo = container.VIRNumber,
                                        BLNo = container.BLNo,
                                        IndexNo = container.IndexNumber,
                                        Weight = container.ManifestWeight,
                                        ContainerNo = container.ContainerNo,
                                        OpType = "A",
                                        Performed = datetime,
                                        CreateDT = DateTime.Now,
                                        MessageFrom = "AIT",
                                        MessageTo = "WEBOC",
                                        FileName = $"TTSO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"

                                    };

                                    ttsos.Add(ttso);

                                    var indexdata = _cIndexRepo.GetContainerIndexById(container.ContainerIndexId);
                                    if (indexdata != null)
                                    {
                                        indexdata.IsDestuffed = true;
                                        indexdata.CBM = container.CBM ?? 0;
                                        //indexdata.IsHold = container.IsHold; 
                                        containerIndexs.Add(indexdata);
                                    }
                                }
                            }
                            else
                            {

                                return Json(new { error = false, message = "This Conatainer No " + container.ContainerNo + " And This Vir " + container.VIRNumber + "Not Found" });
                            }




                        }




                        _ttSORepo.AddRange(ttsos);
                        _cIndexRepo.UpdateRange(containerIndexs);

                        if (containers.Count() > 0)
                        {
                            //var indeces2 = _cIndexRepo.GetAll().Where(x => x.ContainerNo == containers[0].ContainerNo && x.VirNo == containers[0].VIRNumber).ToList();
                            var indeces2 = _cIndexRepo.GetContainerIndexByIGMAndContainerNo(containers[0].VIRNumber , containers[0].ContainerNo).ToList();

                            if (indeces2.Count() > 0)
                            {
                                var checkData = indeces2.Where(x => x.IsDestuffed == false).FirstOrDefault();

                                if (checkData == null)
                                {
                                    foreach (var item in indeces2)
                                    {
                                        var tempUpdateQuery = string.Format(@"UPDATE ContainerIndex SET IsEmptyOut = '" + 1 + "'  WHERE  ContainerIndexId  = '" + item.ContainerIndexId + "'  ");
                                        _cIndexRepo.ExecuteNonSQL(tempUpdateQuery);

                                        //item.IsEmptyOut = true;
                                    } 

                                 //   _cIndexRepo.UpdateRange(indeces2);

                                }

                            }
                        }
                      
                          
                        




                    }

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "The list indexes a indexes which is already destuffed in. Please remove the index and try again." });
                }





                return Json(new { error = false, message = "Saved" });

            }
            catch (Exception e)
            {
                return Json(new { error = true, message = "Alert Please Destuffed Again" });

            }

        }

        public IActionResult MarkEmptyOut(string igm , string containernumber)
        {
            try
            {
                var containers = _cIndexRepo.GetAll().Where(x => x.VirNo == igm && x.ContainerNo == containernumber).ToList();

                if(containers.Count() > 0)
                {
                    foreach (var item in containers)
                    {
                        item.IsEmptyOut = true;
                    }
                    _cIndexRepo.UpdateRange(containers);

                    return new OkObjectResult(new { error = false, message = "save" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no record found" });
                }
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }


        public IActionResult MarkEmptyOutIndexWise(string igm, int indexnumber)
        {
            try
            {
                var containers = _cIndexRepo.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexnumber).ToList();

                if (containers.Count() > 0)
                {
                    foreach (var item in containers)
                    {
                        item.IsEmptyOut = true;
                    }
                    _cIndexRepo.UpdateRange(containers);

                    return new OkObjectResult(new { error = false, message = "save" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no record found" });
                }
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }


        public IActionResult UpdateDestuffInfo(long containerIndexId , double ? foundWeight  , int? package, string foundPackageType , double? cbm ,  string location 
                                              , string remarks , string invoiceFoud , string shortExcess , string invoiceAmount , long? vehicleMeasurementId)
        {
            try
            {
                var res = _cIndexRepo.GetContainerIndexById(containerIndexId);

                var destuffres = _destuffRepo.GetAll().Where(x => x.ContainerIndexId == containerIndexId).LastOrDefault();
                 
                 

                var indexesdetail = _cIndexRepo.GetIndexInfo(res.VirNo, res.IndexNo ?? 0).Select(v => v.ContainerIndexId).ToList();

                if (indexesdetail.Count() > 0)
                {
                    var resdata = _invoiceRepository.GetAll().Where(t => indexesdetail.Contains(t.ContainerIndexId ?? 0)).ToList();
                    if(resdata.Count() > 0) {
                        return Json(new { error = true, message = "can't update due to invoice created" });
                    }
                    
                }

                if (res != null && destuffres != null)
                {

                    //destuffres.FoundWeight = foundWeight > 0 ? Convert.ToInt32(foundWeight) : 0;
                    destuffres.FoundWeight = foundWeight > 0 ? foundWeight : 0 ;
                    destuffres.PackageQuantity = package;
                    destuffres.FoundPackageType = foundPackageType;
                    destuffres.CBM = cbm;
                    destuffres.Location = location;
                    destuffres.Remarks = remarks;
                    destuffres.InvoiceFound = invoiceFoud;
                    destuffres.ShortExcess = shortExcess;
                    destuffres.InvoiceAmount = invoiceAmount;
                    destuffres.VehicleMeasurementId = vehicleMeasurementId;

                    res.CBM = cbm ?? 0;

                    _cIndexRepo.Update(res);
                    _destuffRepo.Update(destuffres);

                    return Json(new { error = false, message = "Update" });

                }
                else
                {
                    return Json(new { error = true, message = "please try again" });
                }
                 


            }
            catch (Exception e)
            {
                return Json(new { error = true, message = e.InnerException.InnerException.Message });
            }
            
             
        }



    }
}