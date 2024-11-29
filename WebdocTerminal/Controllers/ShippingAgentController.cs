using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Controllers
{
    [MyAuthorize]
    public class ShippingAgentController : Controller
    {
        private IShippingAgentRepository _repo;
        private IPartyRepository _partyRepository;
        private IPartyLedgerRepository _partyLedgerRepo;
        private IMasterShippingAgentRepository _masterShippingAgentRepo;

        public ShippingAgentController(IShippingAgentRepository repo,
                                       IPartyRepository partyRepository,
                                       IPartyLedgerRepository partyLedgerRepo,
                                       IMasterShippingAgentRepository masterShippingAgentRepo)
        {
            _repo = repo;
            _partyRepository = partyRepository;
            _partyLedgerRepo = partyLedgerRepo;
            _masterShippingAgentRepo = masterShippingAgentRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {
            var shippingAgents = _repo.GetAll();
            return Json(shippingAgents);
        }

        [HttpPost]
        public IActionResult Post(ShippingAgent shippingAgent)
        {



            var res = _repo.GetAll().Where(x => x.NTNNumber == shippingAgent.NTNNumber || x.LineCode == shippingAgent.LineCode).LastOrDefault();

            if (res != null)
            {
                return new OkObjectResult(new { error = true, message = "NTN already avaible" });

            }
            //shippingAgent.LineCode = GetLineCode();

            _repo.Add(shippingAgent);

            //var party = new Party
            //{
            //    PartyName =  shippingAgent.Name ,
            //    Consignee = shippingAgent.NTNNumber,
            //    ShippingAgentId = shippingAgent.ShippingAgentId
            //};
            //_partyRepository.Add(party);


            return new OkObjectResult(new { error = false, message = "Save" });

        }


        public string GetLineCode()
        {


            var invo = _repo.GetLast();

            if (invo != null)
            {
                var no = invo.LineCode;

                var number = Convert.ToInt16(no) + 1;
                var linecode = number;

                return linecode.ToString();

            }
            else
            {
                var no = "1";

                var Number = no;

                return Number;

            }


        }

        [HttpPut]
        public IActionResult Put(ShippingAgent shippingAgent)
        {

            var data = _repo.GetAll().Where(x => x.LineCode == shippingAgent.LineCode).LastOrDefault();

            if (data != null)
            {
                return new JsonResult(new { error = true, message = "This Line  code Already Exist" });
            }

            _repo.Update(shippingAgent);


            return new OkObjectResult(new { ShippingAgentId = shippingAgent.ShippingAgentId });
        }

        [HttpPost]
        public IActionResult updateShippingAgent(ShippingAgent ShippingAgent)
        {

             

            //var party = _partyRepository.GetAll().Where(x => x.ShippingAgentId == ShippingAgent.ShippingAgentId).FirstOrDefault();

            //if (party != null)
            //{
            //    party.PartyName = ShippingAgent.Name;
            //    party.Consignee = ShippingAgent.NTNNumber;

            //     _partyRepository.Update(party);

            //}
             _repo.Update(ShippingAgent);

            return new OkObjectResult(new { ShippingAgentId = ShippingAgent.ShippingAgentId });
        }
        public void Delete(long key)
        {

            var agent = _repo.Find(key);


            var party = _partyRepository.GetAll().Where(x => x.ShippingAgentId == key).FirstOrDefault();

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



        #region MyRegion


        public IActionResult MasterLineView()
        {
            return View();
        }

        public JsonResult GetMasterLine()
        {
            var mastershippingAgents = _masterShippingAgentRepo.GetAll();
            return Json(mastershippingAgents);
        }

        [HttpPost]
        public IActionResult MasterLinePost(MasterShippingAgent shippingAgent)
        {



            shippingAgent.MasterShippingAgentCode = GetMasterLineCode();

            _masterShippingAgentRepo.Add(shippingAgent);

            return new OkObjectResult(new { error = false, message = "Save" });

        }


        public long GetMasterLineCode()
        {


            var invo = _masterShippingAgentRepo.GetLast();

            if (invo != null)
            {
                var no = invo.MasterShippingAgentCode;

                var number = Convert.ToInt16(no) + 1;
                var linecode = number;

                return linecode;

            }
            else
            {
                var no = 1;

                return no;

            }


        }

        [HttpPut]
        public IActionResult MasterLinePut(MasterShippingAgent shippingAgent)
        {

            _masterShippingAgentRepo.Update(shippingAgent);

            return new OkObjectResult(new { ShippingAgentId = shippingAgent.MasterShippingAgentId });
        }

        [HttpPost]
        public IActionResult updateMasterShippingAgent(MasterShippingAgent ShippingAgent)
        {


            _masterShippingAgentRepo.Update(ShippingAgent);

            return new OkObjectResult(new { ShippingAgentId = ShippingAgent.MasterShippingAgentId });
        }

        #endregion



    }
}