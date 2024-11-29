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
    public class OEHCController : ParentController
    {
        private IOEHCRepository _OECHRepo;
        private IOPIARepository _oPIARepository;

        public OEHCController(IOEHCRepository OECHRepo,
                              IOPIARepository oPIARepository)
        {
            _OECHRepo = OECHRepo;
            _oPIARepository = oPIARepository;
        }
        public IActionResult OEHCView()
        {
            return View();
        }

        public JsonResult GetOEHCs()
        {
            var data = _OECHRepo.GetAll().Where(x => x.MessageFrom == "Manual"); ;
            return Json(data);
        }



        public JsonResult GetOEHCsList()
        {
            var data = _oPIARepository.Getoehcslist().ToList();

            return Json(data);
        }


        [HttpGet]
        public IEnumerable<OEHC> Get()
        {
            return _OECHRepo.GetAll().Where(x => x.MessageFrom == "Manual").OrderByDescending(x => x.OEHCId);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var dateTime = DateTime.Now;

            var data = new OEHC();
            data.Performed = dateTime;
            data.TotalRecords = 1;
            data.RecordSequence = 1;
            data.MessageFrom = "WEBOC";
            data.MessageTo = "BWP";
            data.FileName = $"OEHC_{DateFormatter.ConvertToyyyyMMddFormat(dateTime)}_{DateFormatter.ConvertToHHmmFormat(dateTime)}{".txt"}";

            JsonConvert.PopulateObject(values, data);

            _OECHRepo.Add(data);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(long key, string values)
        {
            var dateTime = DateTime.Now;

            var data = _OECHRepo.Find(key);
            data.Performed = dateTime;
            data.TotalRecords = 1;
            data.RecordSequence = 1;
            data.MessageFrom = "Manual";

            data.FileName = $"OEHC_{DateFormatter.ConvertToyyyyMMddFormat(dateTime)}_{DateFormatter.ConvertToHHmmFormat(dateTime)}{".txt"}";

            JsonConvert.PopulateObject(values, data);

            _OECHRepo.Update(data);

            return Ok();
        }



        [HttpDelete]
        public void Delete(long key)
        {
            var data = _OECHRepo.Find(key);

            _OECHRepo.Delete(data);
        }
    }
}