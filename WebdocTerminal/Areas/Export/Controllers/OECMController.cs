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
    public class OECMController : ParentController
    {
        private IOECMRepository _OECMRepo;
        private IOPIARepository _oPIARepository;

        public OECMController(IOECMRepository OECMRepo,
                              IOPIARepository oPIARepository)
        {
            _OECMRepo = OECMRepo;
            _oPIARepository = oPIARepository;
        }
        public IActionResult OECMView()
        {
            return View();
        }

        public JsonResult GetOECMs()
        {
            var data = _oPIARepository.Getoecmslist().ToList();

            return Json(data);
        }

        [HttpGet]
        public IEnumerable<OECM> Get()
        {
            return _OECMRepo.GetAll().OrderByDescending(x => x.OECMId);
        }

        public IActionResult Post(OECM values)
        {
            var dateTime = DateTime.Now;

            var data = new OECM();
            data.GDNumber = values.GDNumber;
            data.HandlingCode = values.HandlingCode;
            data.Performed = dateTime;
            data.TotalRecords = 1;
            data.RecordSequence = 1;
            data.MessageFrom = "WEBOC";
            data.MessageTo = "BWP";
            data.FileName = $"OECM_{DateFormatter.ConvertToyyyyMMddFormat(dateTime)}_{DateFormatter.ConvertToHHmmFormat(dateTime)}{".txt"}";

            _OECMRepo.Add(data);

            return Ok();
        }

        public IActionResult Put(OECM data)
        {
            var dateTime = DateTime.Now;

            data.Performed = dateTime;
            data.TotalRecords = 1;
            data.RecordSequence = 1;
            data.MessageFrom = "Manual";

            data.FileName = $"OECM_{DateFormatter.ConvertToyyyyMMddFormat(dateTime)}_{DateFormatter.ConvertToHHmmFormat(dateTime)}{".txt"}";
            _OECMRepo.Update(data);




            return Ok();
        }



        public void Delete(long key)
        {
            var data = _OECMRepo.Find(key);

            _OECMRepo.Delete(data);
        }
    }
}