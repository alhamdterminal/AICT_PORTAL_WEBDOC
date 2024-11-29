using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class PartyLedgerController : ParentController
    {
        public IBankRepository _bankrepo;
        public IPartyRepository _partyrepo;
        public IRefundAmountRepository _refundAmountrepo;
        public IChequeRecivedRepository _chequeReceiverepo;
        public IInvoiceRepository _invoiceRepo;
        public IAmountReceivedRepository _amountReceivedRepo;
        public IPartyLedgerRepository _partyLedgerRepo;
        private UserManager<IdentityUser> _userManager;
        private IExcessAmountRefundSettlementRepository _excessAmountRefundSettlementRepository;
        private IUsersRepository _userRepo;
        private IShippingAgentRepository _shippingAgentRepository;

        public PartyLedgerController(IBankRepository bankrepo, IPartyRepository partyrepo, IRefundAmountRepository refundAmountrepo, IChequeRecivedRepository chequeReceiverepo, IInvoiceRepository invoiceRepo,
                                    IAmountReceivedRepository amountReceivedRepo,
                                    IPartyLedgerRepository partyLedgerRepo,
                                    UserManager<IdentityUser> userManager,
                                    IExcessAmountRefundSettlementRepository excessAmountRefundSettlementRepository,
                                    IUsersRepository userRepo,
                                    IShippingAgentRepository shippingAgentRepository)
        {
            _bankrepo = bankrepo;
            _partyrepo = partyrepo;
            _refundAmountrepo = refundAmountrepo;
            _chequeReceiverepo = chequeReceiverepo;
            _invoiceRepo = invoiceRepo;
            _amountReceivedRepo = amountReceivedRepo;
            _partyLedgerRepo = partyLedgerRepo;
            _userManager = userManager;
            _excessAmountRefundSettlementRepository = excessAmountRefundSettlementRepository;
            _userRepo = userRepo;

            _shippingAgentRepository = shippingAgentRepository;

        }
        public IActionResult ChequeReceive()
        {
            ViewData["banks"] = _bankrepo.GetAll().Where(x => x.Type == "Ledger")
                .Select(v => new SelectListItem { Text = v.BankName, Value = v.BankId.ToString() });
            ViewData["partys"] = _partyrepo.GetAll()
                .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyId.ToString() });
            return View();
        }

        public IActionResult RefundAmount()
        {
            ViewData["banks"] = _bankrepo.GetAll().Where(x => x.Type == "Ledger")
                .Select(v => new SelectListItem { Text = v.BankName, Value = v.BankId.ToString() });


            //ViewData["partys"] = _partyrepo.GetAll()
            //    .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyId.ToString() });


            return View();
        }


        public IActionResult ExcessRefundView()
        {
            ViewData["banks"] = _bankrepo.GetAll().Where(x => x.Type == "Ledger")
                .Select(v => new SelectListItem { Text = v.BankName, Value = v.BankId.ToString() });


            //ViewData["partys"] = _partyrepo.GetAll()
            //    .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyId.ToString() });


            return View();
        }


        public JsonResult GetALlparties()
        {
            var res = _partyrepo.GetAll().Select(x => new SelectListItem { Text = x.PartyName, Value = x.PartyId.ToString() }).ToList();

            return Json(res);
        }


        public JsonResult GetALlpartiesFF()
        {
            var res = _partyrepo.GetAll().Where(x=> x.ShippingAgentId != null).Select(x => new SelectListItem { Text = x.PartyName, Value = x.PartyId.ToString() }).ToList();

            return Json(res);
        }

        public IActionResult AmountReceived()
        {
            //ViewData["invoices"] = _partyLedgerRepo.GetInvoicesNotInAmountReceived()
            //.GroupBy(x => x.InvoiceNo)
            //.Select(v => new SelectListItem
            //{
            //    Text = v.FirstOrDefault().InvoiceNo.ToString(),
            //    Value = v.FirstOrDefault().InvoiceId.ToString()
            //});

            //ViewData["partys"] = _partyrepo.GetAll()
            //    .Select(v => new SelectListItem { Text = v.PartyName + "-" + v.Consignee, Value = v.PartyId.ToString() });

            ViewData["banks"] = _bankrepo.GetAll().Where(x=> x.Type == "Ledger")
               .Select(v => new SelectListItem { Text = v.BankName, Value = v.BankId.ToString() });

            ViewData["shippingagent"] = _shippingAgentRepository.GetAll() .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });
            return View();
        }


        public IActionResult SettledAmountBillToline()
        {
 
            //ViewData["partys"] = _partyrepo.GetAll()
            //    .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyId.ToString() });
             
            return View();
        }
        //public IActionResult AmountReceivedExport()
        //{
        //    ViewData["invoices"] = _partyLedgerRepo.GetInvoicesNotInAmountReceivedExport()
        //    .GroupBy(x => x.InvoiceNo)
        //    .Select(v => new SelectListItem
        //    {
        //        Text = v.FirstOrDefault().InvoiceNo.ToString(),
        //        Value = v.FirstOrDefault().InvoiceId.ToString()
        //    });

        //    ViewData["partys"] = _partyrepo.GetAll()
        //        .Select(v => new SelectListItem { Text = v.PartyName + "-" + v.Consignee, Value = v.PartyId.ToString() });
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> AddRefundAmount(RefundAmount values, string KnockOf)
        {
            var invoice = _invoiceRepo.GetAll().Where(x => x.InvoiceNo ==  KnockOf ).LastOrDefault();

            if (invoice != null)
            {

                var refundAmount = _refundAmountrepo.GetAll().Where(x => x.KnockOfInvoice == KnockOf && x.Type == "Cash" && values.Type == "Cash").FirstOrDefault();
                if (refundAmount != null)
                {
                    return new OkObjectResult(new { error = true, message = "This Invoice Already Refund With Cash" });

                }
                var identityUser = await _userManager.GetUserAsync(User);

                var role = await _userManager.GetRolesAsync(identityUser);
                 
                if (values.Type == "Cash" && values.CreditAmount > 2500 && role.Contains("ADMINISTRATOR") == false)
                {
                     return new OkObjectResult(new { error = true, message = "Invalid Amount For Cash" });
                }
                else
                {

                    values.KnockOfInvoice = KnockOf;
                    _refundAmountrepo.Add(values);

                    var data = _partyLedgerRepo.GetBalanceAmountByParty(values.PartyId);

                    PartyLedger partyLedger = new PartyLedger();
                    partyLedger.LedgerDate = values.Date;
                    partyLedger.Type = values.Type;
                    partyLedger.RefundAmountId = values.RefundAmountId;
                    partyLedger.PartyId = values.PartyId;
                    partyLedger.BankId = values.BankId;
                    partyLedger.Credit = values.CreditAmount;
                    partyLedger.Debit = values.DebitAmount;
                    partyLedger.IsSettled = true;
                    if (values.CreditAmount > 0)
                    {
                        partyLedger.Balance = data + values.CreditAmount;
                    }


                    if (values.DebitAmount > 0)
                    {
                        partyLedger.Balance = data - values.DebitAmount;
                    }



                    _partyLedgerRepo.Add(partyLedger);
                    return new OkObjectResult(new { error = false, message = "Refund Amount Saved" });

                }

            }
            else
            {
                return new OkObjectResult(new { error = true, message = "Invoice " + KnockOf + " Nout Found" });
            } 

        }
        [HttpPost]
        public  IActionResult SaveExcessRefund(ExcessAmountRefundSettlement values  )
        {
             
            var res = _invoiceRepo.GetAll().Where(x => x.InvoiceNo == values.InvoiceNo).LastOrDefault();


            if (res != null)
            {
                 
              //  res.ExcessAmount = 0;

                if(res.ExcessAmount <= 0)
                {
                    return new OkObjectResult(new { error = true, message = "excess amount is less then 0" });
                }

                if (res.KnockOffInvoiceNumber != null)
                {
                    return new OkObjectResult(new { error = true, message = "excess amount  already refound from "  + res.KnockOffInvoiceNumber });
                }

                var totalamt = values.AICTServiceCharges + values.Amount + values.OnllineAccountAmount;

                if(res.ExcessAmount == totalamt)
                {
                    res.ExcessAmount = res.ExcessAmount - totalamt;

                    _invoiceRepo.Update(res);

                    values.CreatedDate = DateTime.Now;
                    _excessAmountRefundSettlementRepository.Add(values);

                    return new OkObjectResult(new { error = false, message = "save" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "excess amount  not match with total amount" });
                }

            }
            


            return new OkObjectResult(new { error = true, message = "invoice not found for settlement" });

                

              


        }

        [HttpPost]
        public IActionResult AddChequeReceive(ChequeRecived model , long? shippingagentId )
        {
            try
            {
                //var res =  GetLoginUserInfo();

                model.ShippingAgentId = shippingagentId;
                model.Date = DateTime.Now;

                if (model.Type != "Cash" && model.Type != "CPR" && model.Type != "FundTransferOnline")
                {
                    var checkReceived = _chequeReceiverepo.GetAll().Where(x => x.Number  == model.Number && x.PartyId == model.PartyId && x.BankId == model.BankId).ToList();

                    if (checkReceived.Count() > 0)
                    {
                        return new OkObjectResult(new { error = true, message = "Cheque Received Against This Cheque Number And Same Bank , Party" });
                    }
                }
                 else if (model.Type == "CPR") 
                {
                    var checkReceived = _chequeReceiverepo.GetAll().Where(x => x.Number  == model.Number  && x.PartyId == model.PartyId).ToList();

                    if (checkReceived.Count() > 0)
                    {
                        return new OkObjectResult(new { error = true, message = "Cheque Received Against This  Number And Same Party" });
                    }
                }

                  
                    _chequeReceiverepo.Add(model);

                    var data = _partyLedgerRepo.GetBalanceAmountByParty(model.PartyId);


                    PartyLedger partyLedger = new PartyLedger();
                    partyLedger.LedgerDate = model.ChequeDate;
                    partyLedger.Type = model.Type;
                    partyLedger.ChequeRecivedId = model.ChequeRecivedId;
                    partyLedger.PartyId = model.PartyId;
                    partyLedger.BankId = model.BankId;
                    partyLedger.Credit = model.Amount;
                    partyLedger.Balance = data + model.Amount;
                    partyLedger.IsSettled = true;
                    partyLedger.ShippingAgentId = model.ShippingAgentId;

                    //if (res == 0)
                    //{
                    //     partyLedger.ShippingAgentId = null;
                    //}
                    //else
                    //{
                    //    partyLedger.ShippingAgentId = res;
                    //} 

                    _partyLedgerRepo.Add(partyLedger);

                    return new OkObjectResult(new { error = false, message = "Saved" });

                

                return new OkObjectResult(new { error = true, message = "please try again" });


            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
          
           
        }



        public long GetLoginUserInfo()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var appUser = _userRepo.GetFirst(e => e.IdentityUserId == userId);

            if (appUser != null && appUser.ShippingAgentId != null)
            {
                return appUser.ShippingAgentId ?? 0;
            }

            return 0;

        }



        [HttpPost]
        public async Task<IActionResult> AddAmountReceived(AmountReceived model )
        {
              
            var balance = GetBalanceLedgerPartyId(model.PartyId);

            var identityUser = await _userManager.GetUserAsync(User);

            var role = await _userManager.GetRolesAsync(identityUser);

            var totalbalanceamount = balance - model.ChequeAmount;

            if (totalbalanceamount < 0)
            {
                var res =  role.Contains("ADMINISTRATOR");

                if (res == false)
                {
                    return new OkObjectResult(new { error = true, message = "You Don't Have Permission To Credit Allow" });

                }
            }

            var invoice = _invoiceRepo.Find(model.InvoiceId);

            _amountReceivedRepo.Add(model);

             ///var ledgerBalance = _partyLedgerRepo.GetBalanceAmountByParty(model.PartyId);

            PartyLedger partyLeger = new PartyLedger();

            partyLeger.Debit = model.ChequeAmount;

            partyLeger.LedgerDate = model.AmountReceivedDate;

            partyLeger.BillNo = invoice.InvoiceNo;

            partyLeger.PartyId = model.PartyId;

            partyLeger.AmountReceivedId = model.AmountReceivedId;

            partyLeger.Balance = totalbalanceamount;
            partyLeger.IsSettled = true;

            _partyLedgerRepo.Add(partyLeger);

            invoice.BalanceAmount = model.ChequeAmount;

            _invoiceRepo.Update(invoice);

            return new OkObjectResult(new { error = false, message = "Invoice Saved!" });
             
        }


        public double GetBalanceLedgerPartyId(long? partyId)
        {
            var data = _partyLedgerRepo.GetAll().Where(x => x.PartyId == partyId).ToList();
            if (data != null)
            {
                var debit = 0.0;
                var credit = 0.0;
                debit = data.Sum(x => x.Debit);
                credit = data.Sum(x => x.Credit);
                var balaance = credit - debit;
                return  balaance;
            }
            return 0;
        }

        [HttpGet]
        public JsonResult GetLastChequeReceiveType()
        {
            var data = _chequeReceiverepo.GetLastChequeReceiveByType();

            return Json(data);
        }

        public JsonResult GetInvoices()
        {
            var data = _partyLedgerRepo.GetInvoicesNotInAmountReceived()
             .Select(v => new SelectListItem { Text = v.InvoiceId.ToString(), Value = v.InvoiceId.ToString() });

            return Json(data);
        }


        public IActionResult UpdatePartyLedgerImport()
        {
            ViewData["partys"] = _partyrepo.GetAll()
                .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyId.ToString() });

            return View();
        }

        public JsonResult GetLedgerByParty(long PatryId)
        {
           // var ledger = _partyLedgerRepo.GetAll().Where(x => x.PartyId == PatryId).ToList();
            var ledger = _partyLedgerRepo.GetPartyLedgerDetailPartyWise(PatryId).Where(x => x.PartyId == PatryId).ToList();
            if (ledger != null)
            {
                return Json(ledger);
            }

            return Json("");
        }

        public IActionResult UpdateLedger(PartyLedgerViewModel model)
        {
            var partyledger = _partyLedgerRepo.GetFirst(x => x.PartyLedgerId == model.PartyLedgerId);

            if(partyledger != null)
            {
                if (model.AmountReceivedId != null)
                {
                    var amountReceived = _amountReceivedRepo.GetAll().Where(x => x.AmountReceivedId == model.AmountReceivedId).FirstOrDefault();
                    if (amountReceived != null)
                    {
                        amountReceived.ChequeAmount = Convert.ToInt32(model.Debit);
                        _amountReceivedRepo.Update(amountReceived);
                    }


                }
                if (model.ChequeRecivedId != null)
                {
                    var chequeRecived = _chequeReceiverepo.GetAll().Where(x => x.ChequeRecivedId == model.ChequeRecivedId).FirstOrDefault();
                    chequeRecived.Amount = model.Credit;
                    chequeRecived.Number = model.Number ;
                    _chequeReceiverepo.Update(chequeRecived);
                }

                partyledger.Debit = model.Debit;
                partyledger.Credit = model.Credit; 
                
            }

           
            _partyLedgerRepo.Update(partyledger);

            return new OkObjectResult(new { error = false, message = "Ledger Updated" });
        }

        public IActionResult DeletePartyLedger(PartyLedger model)
        {
            if (model.AmountReceivedId != null)
            {
                var amountReceived = _amountReceivedRepo.GetAll().Where(x => x.AmountReceivedId == model.AmountReceivedId).FirstOrDefault();
                _amountReceivedRepo.Delete(amountReceived);
            }
            if (model.ChequeRecivedId != null)
            {
                var chequeReceived = _chequeReceiverepo.GetAll().Where(x => x.ChequeRecivedId == model.ChequeRecivedId).FirstOrDefault();
                _chequeReceiverepo.Delete(chequeReceived);
            }
            var partyLedger = _partyLedgerRepo.GetAll().Where(x => x.PartyLedgerId == model.PartyLedgerId).FirstOrDefault();
            _partyLedgerRepo.Delete(partyLedger);

            return new OkObjectResult(new { error = false, message = "Ledger Deleted" });
        }


        public IActionResult UpdateSettledAmountBillToLine(List<PartyLedger> model)
        {
            try
            {
                foreach (var item in model)
                {
                    var partyledgr = _partyLedgerRepo.GetAll().Where(x => x.PartyLedgerId == item.PartyLedgerId).FirstOrDefault();
                    if(partyledgr != null)
                    {
                        partyledgr.IsSettled = true;
                        _partyLedgerRepo.Update(partyledgr);
                    }
                    
                }


                return new OkObjectResult(new { error = false, message =  "Updated" });
            }
            catch (Exception e)
            { 
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }
        }



    }
}