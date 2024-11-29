using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class OCRLController : ParentController
    {
        private IOCRLRepository _OCRLRepo;
        private IOPIARepository _oPIARepository;
        public OCRLController(IOCRLRepository OCRLRepo,
                              IOPIARepository oPIARepository)
        {
            _OCRLRepo = OCRLRepo;
            _oPIARepository = oPIARepository;
        }

        public IActionResult OCRLView()
        {
            return View();
        }

        public JsonResult GetOCRLs()
        {
            var data = _OCRLRepo.GetAll().Where(x => x.MessageFrom == "Manual"); ;
            return Json(data);
        }
        public JsonResult GetOCRLsList()
        {
            var data = _oPIARepository.Getocrlslist().ToList();
            return Json(data);
        }

        [HttpGet]
        public IEnumerable<OCRL> Get()
        {
            return _OCRLRepo.GetAll().OrderByDescending(x => x.OCRLId);
        }

        [HttpPost]
        public IActionResult Post(OCRL values)
        {
            var dateTime = DateTime.Now;

            values.Performed = dateTime;
            values.TotalRecords = 1;
            values.RecordSequence = 1;
            values.MessageFrom = "WEBOC";
            values.MessageTo = "BWP";
            values.FileName = $"OCRL_{DateFormatter.ConvertToyyyyMMddFormat(dateTime)}_{DateFormatter.ConvertToHHmmFormat(dateTime)}{".txt"}";

            _OCRLRepo.Add(values);

            return new JsonResult(new { error = false, message = "Saved" });
        }

        public IActionResult Put(OCRL values)
        {
            var dateTime = DateTime.Now;

            values.Performed = dateTime;
            values.TotalRecords = 1;
            values.RecordSequence = 1;
            values.MessageFrom = "Manual";


            values.FileName = $"OCRL_{DateFormatter.ConvertToyyyyMMddFormat(dateTime)}_{DateFormatter.ConvertToHHmmFormat(dateTime)}{".txt"}";

            _OCRLRepo.Update(values);

            return new JsonResult(new { error = false, message = "Saved" });
        }



        public void Delete(long key)
        {
            var data = _OCRLRepo.Find(key);

            _OCRLRepo.Delete(data);
        }
    }
}