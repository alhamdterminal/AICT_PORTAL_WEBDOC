using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Controllers
{
    public class ShippingLineController : Controller
    {
        private IShippingLineRepository _shippingLineRepo;
        private IPartyRepository _partyRepository;
        private IPartyLedgerRepository _partyLedgerRepo;

        public ShippingLineController(IShippingLineRepository shippingLineRepo,
                                     IPartyRepository partyRepository,
                                     IPartyLedgerRepository partyLedgerRepo)
        {
            _shippingLineRepo = shippingLineRepo;
            _partyRepository = partyRepository;
            _partyLedgerRepo = partyLedgerRepo;
        }

        public IActionResult ShippingLine()
        {
            return View();
        }

        public JsonResult GetShippingLines()
        {
            var data = _shippingLineRepo.GetAll();

            return Json(data);
        }

        [HttpPost]
        public IActionResult AddShippingLine(ShippingLine values)
        {

            var data = _shippingLineRepo.GetAll().Where(x => x.NTNNumber  ==  values.NTNNumber || x.ShippingLineCode ==  values.ShippingLineCode ).LastOrDefault();

            if (data != null)
            {
                return new JsonResult(new { error = true, message = "This Line ntn or code Already Exist" });
            }

            //values.ShippingLineCode = GetLineCode();

            _shippingLineRepo.Add(values);

            //var party = new Party
            //{
            //    PartyName = values.ShippingLineName,
            //    Consignee = values.NTNNumber,
            //    ShippingLineId = values.ShippingLineId
            //};

            //_partyRepository.Add(party);


            return new JsonResult(new { error = false, message = "Saved" });


        }

        public string GetLineCode()
        {


            var invo = _shippingLineRepo.GetLast();

            if (invo != null)
            {
                var no = invo.ShippingLineCode;

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


        [HttpPost]
        public IActionResult updateShippingLine(ShippingLine shippingLine)
        {
            var shippingline = _shippingLineRepo.GetFirst(x => x.ShippingLineCode.Trim().ToUpper() == shippingLine.ShippingLineCode.Trim().ToUpper() && x.ShippingLineId != shippingLine.ShippingLineId);

            if (shippingline != null)
            {
                return new JsonResult(new { error = true, message = "This Line Code Already Exist" });
            }

            //var party = _partyRepository.GetAll().Where(x => x.ShippingLineId == shippingLine.ShippingLineId).FirstOrDefault();

            //if (party != null)
            //{
            //    party.PartyName = shippingLine.ShippingLineName;
            //    party.Consignee = shippingLine.NTNNumber;

            //    _partyRepository.Update(party);

            //}



            _shippingLineRepo.Update(shippingLine);

            



            return new JsonResult(new { error = false, message = "Update" });
        }

         public void Delete(long key)
        {

            //_shippingLineRepo.Delete(shippingLine);

            var agent = _shippingLineRepo.Find(key);

            var party = _partyRepository.GetAll().Where(x => x.ShippingLineId == key).FirstOrDefault();

            if (party != null)
            {
                var partyledger = _partyLedgerRepo.GetAll().Where(x => x.PartyId == party.PartyId).FirstOrDefault();

                if (partyledger == null)
                {

                    _shippingLineRepo.Delete(agent);
                    _partyRepository.Delete(party);
                }

            }

            else
            {
                _shippingLineRepo.Delete(agent);
            }

        }
    }
}