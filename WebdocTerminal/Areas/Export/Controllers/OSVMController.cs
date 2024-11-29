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
    public class OSVMController : ParentController
    {
        private IOSVMRepository _OSVMRepo;
        private IOPIARepository _oPIARepository;
        private IExportContainerItemRepositpory _exportContainerItemRepositpory;
        public OSVMController(IOSVMRepository OSVMRepo,
                             IOPIARepository oPIARepository,
                             IExportContainerItemRepositpory exportContainerItemRepositpory)
        {
            _OSVMRepo = OSVMRepo;
            _oPIARepository = oPIARepository;
            _exportContainerItemRepositpory = exportContainerItemRepositpory;
        }
        public IActionResult OSVMView()
        {
            return View();
        }


        public JsonResult GetOSVMs()
        {
            var data = _OSVMRepo.GetAll().Where(x => x.MessageFrom == "Manual");
            return Json(data);
        }
        
        public JsonResult GetPenddingosvms()
        {
            var data = _oPIARepository.GetPenddingosvms().ToList();
            return Json(data);
        }

        public JsonResult GetOSVMsList()
        {
            var data = _oPIARepository.Getosvmslist().ToList();
            return Json(data);
        }
        
        public JsonResult GetOSVMsRes()
        {
            var data = _OSVMRepo.GetAll().ToList();
            return Json(data);
        }


        [HttpGet]
        public IEnumerable<OSVM> Get()
        {
            return _OSVMRepo.GetAll().OrderByDescending(x => x.OSVMId);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {

            var dateTime = DateTime.Now;
            var data = new OSVM();
            data.Performed = dateTime;
            data.RecordSequence = 1;
            data.TotalRecords = 1;
            data.OPType = "A";
            data.MessageFrom = "WEBOC";
            data.MessageTo = "AIT";
            data.FileName = $"OSVM_{DateFormatter.ConvertToyyyyMMddFormat(dateTime)}_{DateFormatter.ConvertToHHmmFormat(dateTime)}{".txt"}";

            JsonConvert.PopulateObject(values, data);

            _OSVMRepo.Add(data);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(long key, string values)
        {
            var data = _OSVMRepo.Find(key);

            JsonConvert.PopulateObject(values, data);

            _OSVMRepo.Update(data);

            return Ok();
        }


        
        public IActionResult Updateosvm(OSVM model)
        {
            var data = _exportContainerItemRepositpory.GetAll().Where(x=> x.ContainerNumber == model.ContainerNo  && x.GDNumber == model.GDNumber).LastOrDefault();

            if(data != null && data.IsGateOut == true)
            {
                return new OkObjectResult(new { error = false, message = "can't update due to gateout mark" });
            }
            else
            {
                _OSVMRepo.Update(model);
                return new OkObjectResult(new { error = true, message = "Save Info" });
            }


            return Ok();
        }





        [HttpDelete]
        public void Delete(long key)
        {
            var data = _OSVMRepo.Find(key);

            _OSVMRepo.Delete(data);
        }
    }
}