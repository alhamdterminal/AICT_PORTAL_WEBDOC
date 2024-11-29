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
    public class OGDEController : ParentController
    {
        private IOGDERepository _OGDERepo;
        private IOPIARepository _oPIARepository;

        public OGDEController(IOGDERepository OGDERepo,
                              IOPIARepository oPIARepository)
        {
            _OGDERepo = OGDERepo;
            _oPIARepository = oPIARepository;
        }
        public IActionResult OGDEView()
        {
            return View();
        }


        public JsonResult GetOGDEs()
        {
            var data = _OGDERepo.GetAll().Where(x => x.MessageFrom == "Manual");
            return Json(data);
        }

        public JsonResult GetOGDEsList()
        {
            var data = _oPIARepository.Getogdelist().ToList();
            return Json(data);
        }

        [HttpGet]
        public IEnumerable<OGDE> Get()
        {
            return _OGDERepo.GetAll().Where(x => x.MessageFrom == "Manual").OrderByDescending(x => x.OGDEId);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var dateTime = DateTime.Now;

            var data = new OGDE();
            data.Performed = dateTime;
            data.TotalRecords = 1;
            data.RecordSequence = 1;
            data.OperationType = "A";
            data.MessageFrom = "WEBOC";
            data.MessageTo = "BWP";
            data.FileName = $"OGDE_{DateFormatter.ConvertToyyyyMMddFormat(dateTime)}_{DateFormatter.ConvertToHHmmFormat(dateTime)}{".txt"}";

            JsonConvert.PopulateObject(values, data);

            _OGDERepo.Add(data);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(long key, string values)
        {
            var data = _OGDERepo.Find(key);


            var dateTime = DateTime.Now;

            data.Performed = dateTime;
            data.TotalRecords = 1;
            data.RecordSequence = 1;
            data.OperationType = "A";
            data.MessageFrom = "Manual";

            data.FileName = $"OGDE_{DateFormatter.ConvertToyyyyMMddFormat(dateTime)}_{DateFormatter.ConvertToHHmmFormat(dateTime)}{".txt"}";



            JsonConvert.PopulateObject(values, data);

            _OGDERepo.Update(data);

            return Ok();
        }



        [HttpDelete]
        public void Delete(long key)
        {
            //var data = _OGDERepo.Find(key);

            //_OGDERepo.Delete(data);
        }
    }
}