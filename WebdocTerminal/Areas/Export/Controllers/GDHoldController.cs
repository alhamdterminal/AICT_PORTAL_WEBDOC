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
    public class GDHoldController : ParentController
    {
         private IGDHoldRepository _GDHoldRepo;
         private IEnteringCargoRepository _enteringCargoRepository;
         private IOPIARepository _oPIARepository;

        public GDHoldController(IGDHoldRepository GDHoldRepo 
                                , IEnteringCargoRepository enteringCargoRepository
                                , IOPIARepository oPIARepository)
        {
            _GDHoldRepo = GDHoldRepo;
            _enteringCargoRepository = enteringCargoRepository;
            _oPIARepository = oPIARepository;

        }

        public IActionResult GDHoldView()
        {
            ViewData["GDNumbers"] = _oPIARepository.GetUnAssociatedGds()
            .Select(s => new SelectListItem { Text = s.GDNumber, Value = s.GDNumber.ToString() });

            return View();
        }

        public JsonResult GetCargoDetail(string gdnumber)
        {
            var data = _GDHoldRepo.GetCargoDetail( gdnumber);

            if (data != null)
            {
                return Json(data);
            }

            return Json("");

        }

        public IActionResult AddGDHold(long EnteringCargoId, string remarks)
        {
            var datetime = DateTime.Now;


            var EnteringCargo = _enteringCargoRepository.GetEnteringCargoById(EnteringCargoId);
            if (EnteringCargo != null)
            {
                if (EnteringCargo.IsHold == true)
                {
                    return new OkObjectResult(new PostObjectResponce { error = true, Message = "Already On Hold" });

                }
                else
                {
                    EnteringCargo.IsHold = true;
                    _enteringCargoRepository.Update(EnteringCargo);

                    var model = new GDHold
                    {
                        EnteringCargoId = EnteringCargoId,
                        Remarks = remarks,
                        HoldDate = datetime
                    };
                    _GDHoldRepo.Add(model);

                    return new OkObjectResult(new PostObjectResponce { error = false, Message = "Save" });

                }

            }
            return new OkObjectResult(new PostObjectResponce { error = true, Message = "Cargo not found" });

        }
        [HttpPost]
        public IActionResult RemoveHold(long EnteringCargoId, string remarks)
        {

            var datetime = DateTime.Now;


            var EnteringCargo = _enteringCargoRepository.GetEnteringCargoById(EnteringCargoId);
            if (EnteringCargo != null)
            {
                if (EnteringCargo.IsHold != true)
                {
                    return new OkObjectResult(new PostObjectResponce { error = true, Message = "already on Released" });

                }
                else
                {
                    EnteringCargo.IsHold = false;
                    _enteringCargoRepository.Update(EnteringCargo);

                    var model = new GDHold
                    {
                        EnteringCargoId = EnteringCargoId,
                        Remarks = remarks,
                        HoldDate = datetime
                    };
                    _GDHoldRepo.Add(model);

                    return new OkObjectResult(new PostObjectResponce { error = false, Message = "Save" });

                }

            }
            return new OkObjectResult(new PostObjectResponce { error = true, Message = "Cargo not found" });

            //var gdHold = _GDHoldRepo.GetFirst(c => c.EnteringCargoId == EnteringCargoId);

            //if (gdHold != null)
            //{
            //    _GDHoldRepo.Delete(gdHold);

            //    var EnteringCargo = _enteringCargoRepository.Find(EnteringCargoId);

            //    EnteringCargo.IsHold = false;

            //    _enteringCargoRepository.Update(EnteringCargo);
            //}

            //return Ok();
        }

        public JsonResult GetExportCargoDetailsByGD(string gdnumber)
        {
            var res = _GDHoldRepo.GetCargoDetailByGD(gdnumber);

            if (res != null)
            {
                return Json(res);
            }
            return Json("");
        }
    }
}