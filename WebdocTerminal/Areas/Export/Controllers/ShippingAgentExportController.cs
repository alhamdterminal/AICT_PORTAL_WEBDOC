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
    public class ShippingAgentExportController : ParentController
    {
        private IShippingAgentExportRepository _ShippingAgentExportRepo;

        public ShippingAgentExportController(IShippingAgentExportRepository ShippingAgentExportRepo)
        {
            _ShippingAgentExportRepo = ShippingAgentExportRepo;
        }

        public IActionResult ShippingAgentExportView()
        {
            return View();
        }

      


        public JsonResult GetShippingAgentExport()
        {
            var data = _ShippingAgentExportRepo.GetAll();
            return Json(data);
        }


        [HttpPost]
        public IActionResult AddShippingAgentExport(ShippingAgentExport values)
        {
            var vesselExport = _ShippingAgentExportRepo.GetFirst(x => x.ShippingAgentName.Trim().ToUpper() == values.ShippingAgentName.Trim().ToUpper());

            if (vesselExport != null)
            {
                return new JsonResult(new { error = true, message = "This Agent Name Already Exist" });
            }

            _ShippingAgentExportRepo.Add(values);

            return new JsonResult(new { error = false, message = "Saved" });
        }
        [HttpPost]
        public IActionResult UpdateShippingAgentExport(ShippingAgentExport values)
        {
            var shippingAgentExport = _ShippingAgentExportRepo.GetFirst(x => x.ShippingAgentName.Trim().ToUpper() == values.ShippingAgentName.Trim().ToUpper() && x.ShippingAgentExportId != values.ShippingAgentExportId);

            if (shippingAgentExport != null)
            {
                return new JsonResult(new { error = true, message = "This Agent Name Already Exist" });
            }

            _ShippingAgentExportRepo.Update(values);

            return new JsonResult(new { error = false, message = "Update" });
        }


        public void Delete(long key)
        {
            var data = _ShippingAgentExportRepo.Find(key);

            _ShippingAgentExportRepo.Delete(data);
        }
    }
}