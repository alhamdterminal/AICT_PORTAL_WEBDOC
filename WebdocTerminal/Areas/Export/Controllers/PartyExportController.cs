using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class PartyExportController : ParentController
    {
        public IPartyExportRepository _partyExportRepo;
        public PartyExportController(IPartyExportRepository partyExportRepo)
        {
            _partyExportRepo = partyExportRepo;
        }
        public IActionResult PartyExportView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPartyExport()
        {
            var partys = _partyExportRepo.GetAll();
            return Json(partys);
        }

        [HttpPost]
        public IActionResult AddParty(PartyExport Party)
        {
            _partyExportRepo.Add(Party);
            return new OkObjectResult(new { PartyExportId = Party.PartyExportId });
        }

        [HttpPost]
        public IActionResult UpdateParty(PartyExport Party)
        {
            _partyExportRepo.Update(Party);
            return new OkObjectResult(new { PartyExportId = Party.PartyExportId });
        }

        public void DeleteParty(long key)
        {
            var agent = _partyExportRepo.Find(key);

            _partyExportRepo.Delete(agent);
        }
    }
}