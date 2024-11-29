using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class ISOCodeController : ParentController
    {

        public IISOCodeRepository _IISOCodeRepo;

        public ISOCodeController(IISOCodeRepository IISOCodeRepo)
        {
            _IISOCodeRepo = IISOCodeRepo;
        }
        public IActionResult ISOCodeView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetISOCodes()
        {
            var ISOCodes = _IISOCodeRepo.GetAll();
            return Json(ISOCodes);
        }

        [HttpPost]
        public IActionResult AddISOCode(ISOCode data)
        {
            _IISOCodeRepo.Add(data);

            return new OkObjectResult(new { error = false,   Message = "Saved" });
        }

        [HttpPost]
        public IActionResult updateISOCode(ISOCode data)
        {
            _IISOCodeRepo.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        public void Delete(long key)
        {
            var agent = _IISOCodeRepo.Find(key);

            _IISOCodeRepo.Delete(agent);
        }


    }
}
