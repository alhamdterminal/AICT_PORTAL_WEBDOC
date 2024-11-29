using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class DODropdown
    {
        public long DoNumber { get; set; }
        public string GD { get; set; }
    }
    public class DeliveryController : ParentController
    {
        private IExportDeliveryOrderRepository _repo;
        private IGatePassExportRepository _gatePassRepo;
        private IPackageTypeExportRepository _packageTypeExportRepository;
        private IDOItemExportRepository _doiemExportRepository;

        public DeliveryController(IExportDeliveryOrderRepository repo,
            IGatePassExportRepository gatePassRepo,
            IPackageTypeExportRepository packageTypeExportRepository,
            IDOItemExportRepository doiemExportRepository)
        {
            _repo = repo;
            _gatePassRepo = gatePassRepo;
            _packageTypeExportRepository = packageTypeExportRepository;
            _doiemExportRepository = doiemExportRepository;
        }

        public IActionResult DeliveryOrder()
        {

            ViewData["GDNumbers"] = _repo.GetUnDeliveredGDS()
            .Select(s => new SelectListItem { Text = s.GDNumber, Value = s.GDNumber.ToString() });


            return View();
        }

        public IActionResult AddDeliveryOrder(ExportDeliveryOrder model)
        {
            var dateTime = DateTime.Now;

            try
            {
                if (_repo.GetFirst(c => c.EnteringCargoId == model.EnteringCargoId) != null)
                    return Json(new { message = "Delivery Order Already Exists!", error = true });

                var lastDO = _repo.PreviousDONumber();
                model.DONumber = lastDO + 1;
                model.DODate = dateTime;
                _repo.Add(model);
            }
            catch (Exception e)
            {

                throw;
            }

            return Json(new { message = "Delivery Order Created!", error = false });
        }

        public List<DODropdown> NoGatePassDOs()
        {
            var dos = _repo.GetList(s => s.GatePasses.Count == 0, s => s.GatePasses, s => s.EnteringCargo).Select(c => new DODropdown { GD = c.EnteringCargo.GDNumber, DoNumber = c.DONumber }).ToList();

            return dos;
        }

        public IActionResult GatePass()
        {

            ViewData["GDNumbers"] = _repo.GetUnDeliveredGatePassGDS()
            .Select(s => new SelectListItem { Text = s.GDNumber, Value = s.GDNumber.ToString() });

            ViewData["PackageTypes"] = _packageTypeExportRepository.GetAll()
            .Select(s => new SelectListItem { Text = s.PackageType, Value = s.PackageType.ToString() });


            //var dos = NoGatePassDOs();

            //ViewData["DOs"] = dos.Select(s => new SelectListItem { Text = s.DoNumber.ToString(), Value = s.GD }).ToList();


            return View();
        }


        [HttpPost]
        public IActionResult CreateGatePass(ExportDeliveryOrderViewModel model)
        {
            try
            {

                var totaldelivered = 0;
                var gatepassres = _repo.GetGatePassbyGDnumber(model.GDNumber);

                if (gatepassres.Count() > 0)
                {

                    totaldelivered = gatepassres.Sum(x => x.TotalDelivered) ?? 0;

                }

                totaldelivered = totaldelivered + model.NumberOfPackges;

                if (totaldelivered > model.NoOfPackages)
                {
                    return Json(new { message = "packages exceed", error = true });
                }


                long gatepassnumber = 1;
                var res = _gatePassRepo.GetAll().Count();
                if (res > 0)
                {
                    gatepassnumber = res + 1;
                }

                var status = "";
                if (totaldelivered < model.NoOfPackages)
                {
                    status = "P";
                }
                else
                {
                    status = "F";
                }

                if (status == "F")
                {
                    var exportdo = _repo.GetExportDeliveryOrderByGDnumber(model.GDNumber);

                    if (exportdo != null)
                    {
                        exportdo.IsDeivered = true;
                        _repo.Update(exportdo);
                    }
                    else
                    {
                        return Json(new { message = "Do Not Found", error = true });
                    }
                }



                var gatepass = new GatePassExport
                {
                    DODate = DateTime.Now,
                    ExportDeliveryOrderId = model.ExportDeliveryOrderId,
                    GatePassDate = DateTime.Now,
                    Delivered = totaldelivered,
                    GDNumber = model.GDNumber,
                    Remarks = model.Remarks,
                    GatePassNumber = gatepassnumber.ToString(),
                    TotalPackages = model.NoOfPackages,
                    TotalDelivered = model.NumberOfPackges,
                };


                _gatePassRepo.Add(gatepass);

                var doitemexport = new DOItemExport
                {
                    CreatedDate = DateTime.Now,
                    GatePassExportId = gatepass.GatePassExportId,
                    NumberOfPackage = model.NumberOfPackges,
                    PackageType = model.PackgesType,
                    TruckNumber = model.TruckNumber,
                    Status = status,
                };

                _doiemExportRepository.Add(doitemexport);


                return Json(new { message = "Saved", error = false });
            }
            catch (Exception e)
            {
                return Json(new { message = e.InnerException.InnerException.Message, error = true });
            }
        }

        public IActionResult DeleteGatePass(long key)
        {
            try
            {
                var res = _repo.GetDOITemById(key);
                if (res != null)
                {
                    var gatepass = _repo.GetGatepassExportById(res.GatePassExportId);
                    if (gatepass != null)
                    {

                        var dos = _repo.GetGatepassExportByGDNumber(gatepass.GDNumber);

                        foreach (var item in dos)
                        {
                            var doitem = _repo.GetDOItemExportByID(item.GatePassExportId);

                            if (doitem != null && doitem.Status == "F" && res.Status != "F")
                            {
                                return new OkObjectResult(new { error = true, message = "Please Delete Full Gate Pass Then You can delete Partialy" });

                            }

                            if (doitem != null && doitem.Status == "F" && res.Status == "F")
                            {

                                var exportdo = _repo.GetExportDeliveryOrderByGDnumber(gatepass.GDNumber);

                                if (exportdo != null)
                                {
                                    exportdo.IsDeivered = false;
                                    _repo.Update(exportdo);
                                }
                                else
                                {
                                    return Json(new { message = "Do Not Found", error = true });
                                }

                            }
                        }

                        _gatePassRepo.Delete(gatepass);
                        _doiemExportRepository.Delete(res);
                        return Json(new { message = "Deleted", error = false });

                    }
                    else
                    {
                        return Json(new { message = "GatePass Not Found", error = true });

                    }


                }
                else
                {
                    return Json(new { message = "DO Item Not Found", error = true });
                }
            }
            catch (Exception e)
            {

                return Json(new { message = e.InnerException.InnerException.Message, error = true });
            }
        }

        public IActionResult ReprintGatePass()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult CreateGatePass(long doNumber, string remarks, List<DOItem> items)
        //{
        //    var deliveryOrder = _repo.GetFirst(d => d.DONumber == doNumber, c => c.EnteringCargo);
        //    var enteringCargo = deliveryOrder.EnteringCargo;
        //    var previousGatePass = _gatePassRepo.GetAll().OrderByDescending(c => c.GatePassNumber).FirstOrDefault();

        //    int gatePassNo = previousGatePass != null && previousGatePass.GatePassNumber == null ? (Convert.ToInt32(previousGatePass.GatePassNumber) + 1) : 1;

        //    var exportGatepass = new GatePassExport
        //    {
        //        GatePassNumber = gatePassNo.ToString(),
        //        Remarks = remarks,
        //        ExportDeliveryOrderId = deliveryOrder.ExportDeliveryOrderId,
        //        GatePassDate = DateTime.Now
        //    };

        //    //foreach (var item in items)
        //    //{
        //    //    exportGatepass.DOItems.Add(new DOItem
        //    //    {
        //    //        TruckNumber = item.TruckNumber,
        //    //        Quantity = item.Quantity,
        //    //        PackageType = item.PackageType
        //    //    });
        //    //}

        //    //if (exportGatepass.DOItems.Sum(c => c.Quantity) > enteringCargo.NumberOfPackages)
        //    //    return Json(new { message = "No of Packages cannot exceed the total packages", error = true });

        //    //exportGatepass.TotalPackages = enteringCargo.NumberOfPackages;
        //    //exportGatepass.TotalDelivered = exportGatepass.DOItems.Sum(c => c.Quantity);
        //    //exportGatepass.BalancePackages = exportGatepass.TotalPackages - exportGatepass.TotalDelivered;

        //    _gatePassRepo.Add(exportGatepass);

        //    return Json(new { message = $"Gate Pass #{exportGatepass.GatePassNumber} created successfully!", error = false , GatePassExportId = exportGatepass.GatePassExportId});
        //}
    }
}