using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class GoodsHeadExportController : ParentController
    {
        private IGoodsHeadExportRepository _goodsHeadExportRepository;

        public GoodsHeadExportController(IGoodsHeadExportRepository goodsHeadExportRepository)
        {
            _goodsHeadExportRepository = goodsHeadExportRepository;
        }

        public IActionResult GoodsHeadExportView()
        {
            return View();
        }

        public JsonResult GetGoodsHeadExportExport()
        {
            var data = _goodsHeadExportRepository.GetAll();
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddGoodsHeadExport(GoodsHeadExport data)
        {
            var res = _goodsHeadExportRepository.GetFirst(x => x.GoodsHeadName.Trim().ToUpper() == data.GoodsHeadName.Trim().ToUpper());

            if (res != null)
            {
                return new JsonResult(new { error = true, message = "This Good Name Already Exist" });
            }

            _goodsHeadExportRepository.Add(data);

            return new JsonResult(new { error = false, message = "Saved" });
        }


        [HttpPost]
        public IActionResult updateGoodsHeadExport(GoodsHeadExport data)
        {
            var res = _goodsHeadExportRepository.GetFirst(x => x.GoodsHeadName.Trim().ToUpper() == data.GoodsHeadName.Trim().ToUpper() && x.GoodsHeadExportId != data.GoodsHeadExportId);

            if (res != null)
            {
                return new JsonResult(new { error = true, message = "This Good Name Already Exist" });
            }

            _goodsHeadExportRepository.Update(data);

            return new JsonResult(new { error = false, message = "Saved" });
        }
 
    }
}