using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class VesselExportController : ParentController
    {
        private IVesselExportRepository _vesselExportRepo;
        public VesselExportController(IVesselExportRepository vesselExportRepo)
        {
            _vesselExportRepo = vesselExportRepo;
        }

        public IActionResult VesselExportView()
        {
            return View();
        }

        public JsonResult GetVesselExport()
        {
            var data = _vesselExportRepo.GetAll();
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddVesselExport(VesselExport VesselExport)
        {
            var vesselExport = _vesselExportRepo.GetFirst(x => x.VesselName.Trim().ToUpper() == VesselExport.VesselName.Trim().ToUpper());

            if (vesselExport != null)
            {
                return new JsonResult(new { error = true, message = "This Vessel Name Already Exist" });
            }

            _vesselExportRepo.Add(VesselExport);

            return new JsonResult(new { error = false, message = "Saved" });
        }


        [HttpPost]
        public IActionResult updateVesselExport(VesselExport VesselExport)
        {
            var vesselExport = _vesselExportRepo.GetFirst(x => x.VesselName.Trim().ToUpper() == VesselExport.VesselName.Trim().ToUpper() && x.VesselExportId != VesselExport.VesselExportId);

            if (vesselExport != null)
            {
                return new JsonResult(new { error = true, message = "This Vessel Name Already Exist" });
            }

            _vesselExportRepo.Update(VesselExport);

            return new JsonResult(new { error = false, message = "Saved" });
        }

        public void Delete(long key)
        {
            var data = _vesselExportRepo.Find(key);

            _vesselExportRepo.Delete(data);
        }
    }
}