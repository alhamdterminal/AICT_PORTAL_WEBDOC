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
    public class CommodityController : ParentController
    {
        private ICommodityRepository _commodityRepo;

        public CommodityController(ICommodityRepository commodityRepo)
        {
            _commodityRepo = commodityRepo;
        }
        public IActionResult CommodityView()
        {
            return View();
        }

        public JsonResult GetCommodityExport()
        {
            var data = _commodityRepo.GetAll();
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddCommodityExport(Commodity values)
        {

            var data = _commodityRepo.GetFirst(x => x.CommodityCode.Trim().ToUpper() == values.CommodityCode.Trim().ToUpper());
            if (data != null)
            {
                return new JsonResult(new { error = true, message = "This Commodity Code Already Exist" });
            }

            _commodityRepo.Add(values);

            return new JsonResult(new { error = false, message = "Saved" });


        }


        [HttpPost]
        public IActionResult UpdateCommodityExport(Commodity values)
        {
            var commodity = _commodityRepo.GetFirst(x => x.CommodityCode.Trim().ToUpper() == values.CommodityCode.Trim().ToUpper() && x.CommodityId != values.CommodityId);

            if (commodity != null)
            {
                return new JsonResult(new { error = true, message = "This Commodity Code Already Exist" });
            }


            _commodityRepo.Update(values);
            return new JsonResult(new { error = false, message = "Update" });
        }

        public void Delete(long key)
        {
            var data = _commodityRepo.Find(key);

            _commodityRepo.Delete(data);
        }
    }
}