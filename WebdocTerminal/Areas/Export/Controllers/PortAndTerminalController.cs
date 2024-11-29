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
    public class PortAndTerminalController : ParentController
    {
        private IPortAndTerminalRepository _portAndTerminalRepo;

        public PortAndTerminalController(IPortAndTerminalRepository portAndTerminalRepo)
        {
            _portAndTerminalRepo = portAndTerminalRepo;
        }
        public IActionResult PortAndTerminalView()
        {
            return View();
        }

        public JsonResult GetPortAndTerminals()
        {
            var data = _portAndTerminalRepo.GetAll();
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddPortAndTerminal(PortAndTerminal values)
        {

            var data = _portAndTerminalRepo.GetFirst(x => x.TerminalCode.Trim().ToUpper() == values.TerminalCode.Trim().ToUpper());
            if (data != null)
            {
                return new JsonResult(new { error = true, message = "This Terminal Code Already Exist" });
            }

            _portAndTerminalRepo.Add(values);

            return new JsonResult(new { error = false, message = "Saved" });


        }


        [HttpPost]
        public IActionResult UpdatePortAndTerminal(PortAndTerminal values)
        {

            var portAndTerminal = _portAndTerminalRepo.GetFirst(x => x.TerminalCode.Trim().ToUpper() == values.TerminalCode.Trim().ToUpper() && x.PortAndTerminalId != values.PortAndTerminalId);

            if (portAndTerminal != null)
            {
                return new JsonResult(new { error = true, message = "This Terminal Code Already Exist" });
            }

            _portAndTerminalRepo.Update(values);

            return new JsonResult(new { error = false, message = "Update" });
        }

        public void Delete(long key)
        {
            var data = _portAndTerminalRepo.Find(key);

            _portAndTerminalRepo.Delete(data);
        }


    }
}