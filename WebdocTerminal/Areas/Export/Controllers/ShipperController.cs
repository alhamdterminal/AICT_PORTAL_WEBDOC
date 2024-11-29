using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class ShipperController : ParentController
    {
        private IShipperRepository _shipperRepo;

        public ShipperController(IShipperRepository shipperRepo)
        {
            _shipperRepo = shipperRepo;
        }

        public IActionResult ShipperView()
        {
            return View();
        }
        public JsonResult GetShipperExport()
        {
            var data = _shipperRepo.GetAll();
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddShipperExport(Shipper values)
        {

            var data = _shipperRepo.GetFirst(x => x.NTNNumber  == values.NTNNumber);
            if (data != null)
            {
                return new JsonResult(new { error = true, message = "This Shipper NTN Already Exist" });
            }

            _shipperRepo.Add(values);

            return new JsonResult(new { error = false, message = "Saved" });


        }


        [HttpPost]
        public IActionResult UpdateShipperExport(Shipper values)
        {
            var shipper = _shipperRepo.GetFirst(x => x.ShipperName.Trim().ToUpper() == values.ShipperName.Trim().ToUpper() && x.ShipperId != values.ShipperId);

            if (shipper != null)
            {
                return new JsonResult(new { error = true, message = "This Shipper Already Exist" });
            }

            _shipperRepo.Update(values);

            return new JsonResult(new { error = false, message = "Update" });
        }

        public void Delete(long key)
        {
            var data = _shipperRepo.Find(key);

            _shipperRepo.Delete(data);
        }
    }
}