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
    public class ExportLocationCodeListController : ParentController
    {
        private IExportLocationCodeListRepository _exportLocationCodeListRepository;

        public ExportLocationCodeListController(IExportLocationCodeListRepository exportLocationCodeListRepository)
        {
            _exportLocationCodeListRepository = exportLocationCodeListRepository;
        }

        public IActionResult ExportLocationCodeListView()
        {

            return View();
        }
        public JsonResult GetExportLocationCodeList()
        {
            var data = _exportLocationCodeListRepository.GetAll();
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddExportLocationCodeList(ExportLocationCodeList values)
        {

            _exportLocationCodeListRepository.Add(values);

            return new JsonResult(new { error = false, message = "Saved" });


        }


        [HttpPost]
        public IActionResult UpdateExportLocationCodeList(ExportLocationCodeList values)
        {
            _exportLocationCodeListRepository.Update(values);
            return new OkObjectResult(new { ExportLocationCodeListID = values.ExportLocationCodeListId });
        }

        public void Delete(long key)
        {
            var data = _exportLocationCodeListRepository.Find(key);

            _exportLocationCodeListRepository.Delete(data);
        }
    }
}