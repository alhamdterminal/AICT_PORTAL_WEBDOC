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
    public class ClearingAgentExportController : ParentController
    {
        private IClearingAgentExportRepository _ClearingAgentExportRepo;

        public ClearingAgentExportController(IClearingAgentExportRepository ClearingAgentExportRep)
        {
            _ClearingAgentExportRepo = ClearingAgentExportRep;
        }

        public IActionResult ClearingAgentExportView()
        {

            return View();
        }
        public JsonResult GetClearingAgentExport()
        {
            var data = _ClearingAgentExportRepo.GetAll();
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddClearingAgentExport(ClearingAgentExport values)
        {

            var data = _ClearingAgentExportRepo.GetFirst(x => x.ChallanNumber.Trim().ToUpper() == values.ChallanNumber.Trim().ToUpper());
            var res = _ClearingAgentExportRepo.GetFirst(x => x.ClearingAgentName.Trim().ToUpper() == values.ClearingAgentName.Trim().ToUpper());
            if (data != null)
            {
                return new JsonResult(new { error = true, message = "This Challan Number Already Exist" });
            }
            if (res != null)
            {
                return new JsonResult(new { error = true, message = "This Name Already Exist" });
            }

            _ClearingAgentExportRepo.Add(values);

            return new JsonResult(new { error = false, message = "Saved" });


        }


        [HttpPost]
        public IActionResult UpdateClearingAgentExport(ClearingAgentExport values)
        {
            var clearingAgentExport = _ClearingAgentExportRepo.GetFirst(x => x.ChallanNumber.Trim().ToUpper() == values.ChallanNumber.Trim().ToUpper() && x.ClearingAgentExportId != values.ClearingAgentExportId);

            if (clearingAgentExport != null)
            {
                return new JsonResult(new { error = true, message = "This Challan Number Already Exist" });
            }

            var res = _ClearingAgentExportRepo.GetFirst(x => x.ClearingAgentName.Trim().ToUpper() == values.ClearingAgentName.Trim().ToUpper() && x.ClearingAgentExportId != values.ClearingAgentExportId);

            if (res != null)
            {
                return new JsonResult(new { error = true, message = "This Name Already Exist" });
            }

            _ClearingAgentExportRepo.Update(values);

            return new JsonResult(new { error = false, message = "Update" });
        }
         
    }
}