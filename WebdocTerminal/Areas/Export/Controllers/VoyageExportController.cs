using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class VoyageExportController : ParentController
    {
        private IVoyageExportRepository _voyageExportRepo;

        public VoyageExportController(IVoyageExportRepository voyageExportRepo)
        {
            _voyageExportRepo = voyageExportRepo;
        }

        public IActionResult VoyageExportView()
        {
            return View();
        }

        public JsonResult GetVoyageExport()
        {
            var data = _voyageExportRepo.GetAll();
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddVoyageExport(VoyageExport VoyageExport)
        {
            var virnumber = string.IsNullOrEmpty(VoyageExport.VirNumber) ? "" : VoyageExport.VirNumber.Trim().ToUpper();

            var data = _voyageExportRepo.GetFirst(x => ( x.VirNumber?.Trim().ToUpper() == virnumber) && (x.VesselExportId == VoyageExport.VesselExportId));
            if (data != null)
            {
                return new JsonResult(new { error = true, message = "This VIR Number Already Exist" });
            }

            _voyageExportRepo.Add(VoyageExport);

            return new JsonResult(new { error = false, message = "Saved" });

             
        }


        [HttpPost]
        public IActionResult updateVoyageExport(VoyageExport VoyageExport)
        {
           var virnumber = string.IsNullOrEmpty(VoyageExport.VirNumber) ? "" : VoyageExport.VirNumber.Trim().ToUpper();
            
            var voyageExport = _voyageExportRepo.GetFirst(x =>( x.VirNumber?.Trim().ToUpper() == virnumber) && (x.VoyageExportId != VoyageExport.VoyageExportId));
            if (voyageExport != null)
            {
                return new JsonResult(new { error = true, message = "This  VIR Number Already Exist" });
            }

            _voyageExportRepo.Update(VoyageExport);

            return new JsonResult(new { error = false, message = "Update" });
        }

        public void Delete(long key)
        {
            var data = _voyageExportRepo.Find(key);

            _voyageExportRepo.Delete(data);
        }
    }
}