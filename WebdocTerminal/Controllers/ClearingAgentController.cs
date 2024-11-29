using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Controllers
{
    [MyAuthorize]
    public class ClearingAgentController : Controller
    {
        private IClearingAgentRepository _repo;
        private IPartyRepository _partyRepository;
        private IPartyLedgerRepository _partyLedgerRepo;

        public ClearingAgentController(IClearingAgentRepository repo,
                                      IPartyRepository partyRepository,
                                      IPartyLedgerRepository partyLedgerRepo)
        {
            _repo = repo;
            _partyRepository = partyRepository;
            _partyLedgerRepo = partyLedgerRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
         
 
        public JsonResult Get()
        {
            var clearingAgents = _repo.GetAll();
            return Json(clearingAgents);
        }


        [HttpPost]
        public IActionResult Post(ClearingAgent clearingAgent)
        {
            var res = _repo.GetAll().Where(x => x.NTNNumber == clearingAgent.NTNNumber).LastOrDefault();

            if (res != null)
            {
                return new OkObjectResult(new { error = true, message = "NTN already avaible" });

            }

            _repo.Add(clearingAgent);

            var party = new Party
            {
                PartyName = clearingAgent.Name,
                Consignee = clearingAgent.NTNNumber,
                ClearingAgentId = clearingAgent.ClearingAgentId
            };


            _partyRepository.Add(party);

            return new OkObjectResult(new { error = false, message = "Save" });

         }

        [HttpPost]
        public IActionResult Put(ClearingAgent clearingAgent)
        {
            

            var party = _partyRepository.GetAll().Where(x => x.ClearingAgentId == clearingAgent.ClearingAgentId).FirstOrDefault();

            if (party != null)
            {

                party.PartyName = clearingAgent.Name;
                party.Consignee = clearingAgent.NTNNumber;

                _partyRepository.Update(party);
            }

            _repo.Update(clearingAgent);


            return new OkObjectResult(new { ClearingAgentId = clearingAgent.ClearingAgentId });
        }
         
        public void Delete(long key)
        {
            var agent = _repo.Find(key);
 
            var party = _partyRepository.GetAll().Where(x => x.ClearingAgentId == key).FirstOrDefault();

            if (party != null)
            {
                var partyledger = _partyLedgerRepo.GetAll().Where(x => x.PartyId == party.PartyId).FirstOrDefault();

                if (partyledger == null)
                {

                    _repo.Delete(agent);
                    _partyRepository.Delete(party);
                }

            }

            else
            {
                _repo.Delete(agent);
            }
        }

    }
}