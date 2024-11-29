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
    public class PackageTypeExportController : ParentController
    {
        private IPackageTypeExportRepository _packageTypeExportRepository;

        public PackageTypeExportController(IPackageTypeExportRepository packageTypeExportRepository)
        {
            _packageTypeExportRepository = packageTypeExportRepository;
        }

        public IActionResult PackageTypeExportView()
        {

            return View();
        }
        public JsonResult GetPackageTypeExport()
        {
            var data = _packageTypeExportRepository.GetAll();
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddPackageTypeExport(PackageTypeExport values)
        {

            var data = _packageTypeExportRepository.GetFirst(x => x.PackageType.Trim().ToUpper() == values.PackageType.Trim().ToUpper());
            if (data != null)
            {
                return new JsonResult(new { error = true, message = "This Package Type Already Exist" });
            }

            _packageTypeExportRepository.Add(values);

            return new JsonResult(new { error = false, message = "Saved" });


        }


        [HttpPost]
        public IActionResult UpdatePackageTypeExport(PackageTypeExport values)
        {
            var packageTypeExport = _packageTypeExportRepository.GetFirst(x => x.PackageType.Trim().ToUpper() == values.PackageType.Trim().ToUpper() && x.PackageTypeExportId != values.PackageTypeExportId);

            if (packageTypeExport != null)
            {
                return new JsonResult(new { error = true, message = "This Package Type Already Exist" });
            }

            _packageTypeExportRepository.Update(values);

            return new JsonResult(new { error = false, message = "Update" });
        }

        public void Delete(long key)
        {
            var data = _packageTypeExportRepository.Find(key);

            _packageTypeExportRepository.Delete(data);
        }
    }
}