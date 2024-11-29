using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class PartyController : ParentController
    {
        public IPartyRepository _partyRepo;
        public PartyController(IPartyRepository partyRepo)
        {
            _partyRepo = partyRepo;
        }

        public IActionResult PartyView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetParty()
        {
            var partys = _partyRepo.GetAll();
            return Json(partys);
        }

        [HttpPost]
        public IActionResult AddParty(Party Party)
        {
            _partyRepo.Add(Party);
            return new OkObjectResult(new { PartyId = Party.PartyId});
        }

        [HttpPost]
        public IActionResult UpdateParty(Party Party)
        {
            _partyRepo.Update(Party);
            return new OkObjectResult(new { PartyId = Party.PartyId });
        }

        public void DeleteParty(long key)
        {
            var agent = _partyRepo.Find(key);

            _partyRepo.Delete(agent);
        }

    }
}