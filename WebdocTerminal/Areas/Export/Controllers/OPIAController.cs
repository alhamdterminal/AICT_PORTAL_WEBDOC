using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class OPIAController : ParentController
    {
        private IOPIARepository _OPIARepo;

        public OPIAController(IOPIARepository OPIARepo)
        {
            _OPIARepo = OPIARepo;
        }
        public IActionResult OPIAView()
        {
            return View();
        }


        public JsonResult GetOPIAs()
        {
            var data = _OPIARepo.GetOpiaslist().ToList();
            return Json(data);
        }



        [HttpGet]
        public IEnumerable<OPIA> Get()
        {
            return _OPIARepo.GetAll().OrderByDescending(x => x.OPIAId);
        }

        [HttpPost]
        public IActionResult Post(OPIA values)
        {
            try
            {

                var dateTime = DateTime.Now;

                var data = new OPIA();
                data.TotalRecords = 1;
                data.RecordSequence = 1;
                data.GDNumber = values.GDNumber;
                data.NoOfPackages = values.NoOfPackages;
                data.PackageType = values.PackageType;
                data.ConsignorName = values.ConsignorName;
                data.ConsignorNTN = values.ConsignorNTN;
                data.ConsignorAddress = values.ConsignorAddress;
                data.CongisneeName = values.CongisneeName;
                data.CongisneeAddress = values.CongisneeAddress;
                data.ClearingAgentChallanNumber = values.ClearingAgentChallanNumber;
                data.ClearingAgentName = values.ClearingAgentName;
                data.GrossWeight = values.GrossWeight;
                data.Performed = dateTime;
                data.MessageFrom = values.MessageFrom;
                data.MessageTo = values.MessageTo;

                data.OperationType = "A";
                data.MessageFrom = "Manual";

                data.FileName = $"OPIA_{DateFormatter.ConvertToyyyyMMddFormat(dateTime)}_{DateFormatter.ConvertToHHmmFormat(dateTime)}{".txt"}";
                var filterdData = _OPIARepo.GetFirst(x => x.GDNumber.Trim().ToUpper() == data.GDNumber.Trim().ToUpper());
                if (filterdData != null)
                {
                    return new JsonResult(new { error = true, message = "GD Number Already Exist" });
                }

                _OPIARepo.AddAllEDIMessages(data);

                return new JsonResult(new { error = false, message = "Saved" });

            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }

        [HttpPost]
        public IActionResult updateOPIAS(OPIA values)
        {

            // _OPIARepo.Update(values);
            return new JsonResult(new { error = false, message = "Not Allow" });
        }

        [HttpPut]
        public IActionResult Put(long key, string values)
        {
            var data = _OPIARepo.Find(key);

            JsonConvert.PopulateObject(values, data);

            _OPIARepo.Update(data);

            return Ok();
        }



        [HttpDelete]
        public void Delete(long key)
        {
            var data = _OPIARepo.Find(key);

            _OPIARepo.Delete(data);
        }

        public IActionResult OPIAUpdation()
        {
            ViewData["OPIAS"] = _OPIARepo.GetNotGateInOPIAsList().Select(v => new SelectListItem { Text = v.GDNumber, Value = v.GDNumber });

            return View();
        }
        public JsonResult GetopiaByGd(string Gdnumber)
        {
            var res = _OPIARepo.GetOpiaByGdnumber(Gdnumber).ToList();

            if (res.Count() > 0)
            {
                return Json(res);
            }
            return Json("");
        }


        [HttpPost]
        public IActionResult updateOPIASdetail(OPIA values)
        {
            try
            {
                _OPIARepo.Update(values);

                return new JsonResult(new { error = false, message = "Updated" });

            }
            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }

        }
    }
}