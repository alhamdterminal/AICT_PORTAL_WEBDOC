using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class PartyLedgerExportController : ParentController
    {
        public IBankRepository _bankRepo;
        public IPartyExportRepository _partyExportRepo;
        public IPartyLedgerExportRepository _partyLedgerExportRepo;
        public IChequeRecivedExportRepository _chequeRecivedExportRepo;
        public IInvoiceExportRepository _invoiceExportRepo;
        public IAmountReceivedExportRepository _amountReceivedExportRepo;
        public IRefundAmountRepository _refundAmountRepository;
        public ICreditToCustomerRepository _creditToCustomerRepository;
        public IRefundAmountExportRepository _refundAmountExportRepository;
             

        public PartyLedgerExportController(IBankRepository bankRepo, IPartyExportRepository partyExportRepo
                                                     , IPartyLedgerExportRepository partyLedgerExportRepo
                                                     , IChequeRecivedExportRepository chequeRecivedExportRepo
                                                     , IInvoiceExportRepository invoiceExportRepo
                                                     , IAmountReceivedExportRepository amountReceivedExportRepo
                                                     , IRefundAmountRepository refundAmountRepository
                                                     , ICreditToCustomerRepository creditToCustomerRepository
                                                     , IRefundAmountExportRepository refundAmountExportRepository)
        {
            _bankRepo = bankRepo;
            _partyExportRepo = partyExportRepo;
            _partyLedgerExportRepo = partyLedgerExportRepo;
            _chequeRecivedExportRepo = chequeRecivedExportRepo;
            _invoiceExportRepo = invoiceExportRepo;
            _amountReceivedExportRepo = amountReceivedExportRepo;
            _refundAmountRepository = refundAmountRepository;
            _creditToCustomerRepository = creditToCustomerRepository;
            _refundAmountExportRepository = refundAmountExportRepository;

        }
        public IActionResult ChequeReceiveExport()
        {
            ViewData["banks"] = _bankRepo.GetAll().Where(x=> x.Type == "Ledger")
               .Select(v => new SelectListItem { Text = v.BankName, Value = v.BankId.ToString() });

            ViewData["partys"] = _partyExportRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyExportId.ToString() });

            return View();
        }

        public IActionResult RefundAmountExport()
        {
            ViewData["banks"] = _bankRepo.GetAll().Where(x => x.Type == "Ledger")
               .Select(v => new SelectListItem { Text = v.BankName, Value = v.BankId.ToString() });

            ViewData["partys"] = _partyExportRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyExportId.ToString() });
            return View();
        }

        [HttpPost]
        public IActionResult AddRefundAmountExport(RefundAmountExport values, string KnockOf)
        {

            var invoice = _invoiceExportRepo.GetAll().Where(x => x.InvoiceNo == KnockOf).LastOrDefault();

            if (invoice != null)
            {
                var refundAmount = _refundAmountExportRepository.GetAll().Where(x => x.KnockOfInvoice == KnockOf && x.Type == "Cash" && values.Type == "Cash").FirstOrDefault();
                if (refundAmount != null)
                {
                    return new OkObjectResult(new { error = true, message = "This Invoice Already Refund With Cash" });

                }

                if (values.Type == "Cash" && values.CreditAmount > 1000)
                {
                    return new OkObjectResult(new { error = true, message = "Invalid Amount For Cash" });
                }
                else
                {
                    values.KnockOfInvoice = KnockOf;
                    _refundAmountExportRepository.Add(values);

                    var ledgerBalance = _partyLedgerExportRepo.GetBalanceAmountByParty(values.PartyExportId);

                    PartyLedgerExport partyLeger = new PartyLedgerExport();


                    partyLeger.LedgerDate = values.Date;
                    partyLeger.Type = values.Type;
                    partyLeger.RefundAmountExportId = values.RefundAmountExportId;
                    partyLeger.PartyExportId = values.PartyExportId;
                    partyLeger.BankId = values.BankId;
                    partyLeger.Credit = values.CreditAmount;
                    partyLeger.Debit = values.DebitAmount;

                    if (values.CreditAmount > 0)
                    {
                        partyLeger.Balance = ledgerBalance + values.CreditAmount;
                    }


                    if (values.DebitAmount > 0)
                    {
                        partyLeger.Balance = ledgerBalance - values.DebitAmount;
                    }
                    _partyLedgerExportRepo.Add(partyLeger);
                    return new OkObjectResult(new { error = false, message = "Refund Amount Saved" });

                }
            }
            else
            {
                return new OkObjectResult(new { error = true, message = "Invoice Nout Found" });
            }

        }


        public IActionResult AmountReceivedExport()
        {

            ViewData["invoices"] = _partyLedgerExportRepo.GetUnSettledInvoices()
            .GroupBy(x => x.InvoiceNo)
            .Select(v => new SelectListItem
            {
                Text = v.FirstOrDefault().InvoiceNo.ToString(),
                Value = v.FirstOrDefault().InvoiceExportId.ToString()
            });

            ViewData["partys"] = _partyExportRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.PartyName + "-" + v.Consignee, Value = v.PartyExportId.ToString() });

            return View();

        }


        [HttpPost]
        public IActionResult AddChequeReceiveExport(ChequeRecivedExport model)
        {

            var checkReceived = _chequeRecivedExportRepo.GetAll().Where(x => x.Number == model.Number && x.PartyExportId == model.PartyExportId && x.BankId == model.BankId).ToList();

            if (checkReceived.Count() > 0)
            {
                return new OkObjectResult(new { error = true, message = "Cheque Received Against This Cheque Number And Same Bank , Party" });
            }

            else
            {

                _chequeRecivedExportRepo.Add(model);

                var data = _partyLedgerExportRepo.GetBalanceAmountByParty(model.PartyExportId);



                PartyLedgerExport partyLedgerExport = new PartyLedgerExport();
                partyLedgerExport.LedgerDate = model.Date;
                partyLedgerExport.Type = model.Type;
                partyLedgerExport.ChequeRecivedExportId = model.ChequeRecivedExportId;
                partyLedgerExport.PartyExportId = model.PartyExportId;
                partyLedgerExport.BankId = model.BankId;
                partyLedgerExport.Credit = model.Amount;
                partyLedgerExport.Balance = data + model.Amount;
                _partyLedgerExportRepo.Add(partyLedgerExport);

                return new OkObjectResult(new { error = false, message = "Cheque Receive Created " });
            }
        }

        [HttpGet]
        public JsonResult GetLastChequeReceiveType()
        {
            var data = _chequeRecivedExportRepo.GetLastChequeReceiveByType();

            return Json(data);
        }

        [HttpGet]
        public JsonResult GetCreditToCustomer(long Id)
        {
            var data = _creditToCustomerRepository.GetcreditToCustomerByInvoiceId(Id);

            return Json(data);
        }




        [HttpPost]
        public IActionResult AddAmountReceivedExport(AmountReceivedExport model, string type)
        {


            var invoice = _invoiceExportRepo.Find(model.InvoiceExportId);

            _amountReceivedExportRepo.Add(model);

            if (model.BillAmount >= 0)
            {
                model.Type = type;

                var ledgerBalance = _partyLedgerExportRepo.GetBalanceAmountByParty(model.PartyExportId);

                var xres = ledgerBalance - model.BillAmount;
                var res = new PartyLedgerExport()
                {
                    Type = "LedBill",
                    Debit = model.BillAmount,
                    LedgerDate = model.AmountReceivedDate,
                    BillNo = invoice.InvoiceNo,
                    PartyExportId = model.PartyExportId,
                    AmountReceivedExportId = model.AmountReceivedExportId,
                    Balance = xres,

                };


                _partyLedgerExportRepo.Add(res);



            };

            if (model.CashAmount > 0)
            {
                model.Type = type;

                var ledgerBalance = _partyLedgerExportRepo.GetBalanceAmountByParty(model.PartyExportId);

                var xres = ledgerBalance + model.CashAmount;
                var res = new PartyLedgerExport()
                {
                    Type = "LedCash",
                    Credit = model.CashAmount,
                    LedgerDate = model.AmountReceivedDate,
                    BillNo = invoice.InvoiceNo,
                    PartyExportId = model.PartyExportId,
                    AmountReceivedExportId = model.AmountReceivedExportId,
                    Balance = xres,

                };


                _partyLedgerExportRepo.Add(res);

            }

            if (model.ChequeAmount > 0)
            {
                model.Type = type;

                var ledgerBalance = _partyLedgerExportRepo.GetBalanceAmountByParty(model.PartyExportId);

                var xres = ledgerBalance - model.ChequeAmount;
                var res = new PartyLedgerExport()
                {
                    Type = "LedCheque",
                    Debit = model.ChequeAmount,
                    LedgerDate = model.AmountReceivedDate,
                    BillNo = invoice.InvoiceNo,
                    PartyExportId = model.PartyExportId,
                    AmountReceivedExportId = model.AmountReceivedExportId,
                    Balance = xres,

                };


                _partyLedgerExportRepo.Add(res);

            }


            invoice.BalanceAmount -= (model.ChequeAmount + model.CashAmount);
            invoice.IsAmountReceived = true;

            _invoiceExportRepo.Update(invoice);

            return new OkObjectResult(new { error = false, message = "Invoice Saved!" });


            //var invoice = _invoiceExportRepo.Find(model.InvoiceExportId);

            //_amountReceivedExportRepo.Add(model);

            //if (model.ChequeAmount > 0)
            //{
            //    model.Type = type;

            //    var ledgerBalance = _partyLedgerExportRepo.GetBalanceAmountByParty(model.PartyExportId);

            //    PartyLedgerExport partyLeger = new PartyLedgerExport();

            //    partyLeger.Debit = model.ChequeAmount;

            //    partyLeger.LedgerDate = model.AmountReceivedDate;

            //    partyLeger.BillNo = invoice.InvoiceNo;

            //    partyLeger.PartyExportId = model.PartyExportId;

            //    partyLeger.AmountReceivedExportId = model.AmountReceivedExportId;

            //    partyLeger.Balance = ledgerBalance - model.ChequeAmount;

            //    _partyLedgerExportRepo.Add(partyLeger);

            //}

            //invoice.BalanceAmount -= (model.ChequeAmount + model.CashAmount);

            //_invoiceExportRepo.Update(invoice);

            //return new OkObjectResult(new { error = false, message = "Invoice Saved!" });

        }

        public IActionResult UpdatePartyLedgerExport()
        {
            ViewData["partys"] = _partyExportRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyExportId.ToString() });

            return View();
        }

        public JsonResult GetLedgerByParty(long PatryId)
        {
            var ledger = _partyLedgerExportRepo.GetAll().Where(x => x.PartyExportId == PatryId).ToList();
            if (ledger != null)
            {
                return Json(ledger);
            }

            return Json("");
        }

        public IActionResult UpdateLedger(PartyLedgerExport model)
        {

            if (model.AmountReceivedExportId != null)
            {
                var amountReceived = _amountReceivedExportRepo.GetAll().Where(x => x.AmountReceivedExportId == model.AmountReceivedExportId).FirstOrDefault();
                if (amountReceived != null)
                {
                    amountReceived.ChequeAmount = Convert.ToInt32(model.Debit);
                    _amountReceivedExportRepo.Update(amountReceived);
                }


            }
            if (model.ChequeRecivedExportId != null)
            {
                var chequeRecived = _chequeRecivedExportRepo.GetAll().Where(x => x.ChequeRecivedExportId == model.ChequeRecivedExportId).FirstOrDefault();
                chequeRecived.Amount = model.Credit;
                _chequeRecivedExportRepo.Update(chequeRecived);
            }

            _partyLedgerExportRepo.Update(model);

            return new OkObjectResult(new { error = false, message = "Ledger Updated" });
        }

        public IActionResult DeletePartyLedger(PartyLedgerExport model)
        {
            if (model.AmountReceivedExportId != null)
            {
                var amountReceived = _amountReceivedExportRepo.GetAll().Where(x => x.AmountReceivedExportId == model.AmountReceivedExportId).FirstOrDefault();
                _amountReceivedExportRepo.Delete(amountReceived);
            }
            if (model.ChequeRecivedExportId != null)
            {
                var chequeReceived = _chequeRecivedExportRepo.GetAll().Where(x => x.ChequeRecivedExportId == model.ChequeRecivedExportId).FirstOrDefault();
                _chequeRecivedExportRepo.Delete(chequeReceived);
            }
            var partyLedger = _partyLedgerExportRepo.GetAll().Where(x => x.PartyLedgerExportId == model.PartyLedgerExportId).FirstOrDefault();
            _partyLedgerExportRepo.Delete(partyLedger);

            return new OkObjectResult(new { error = false, message = "Ledger Deleted" });
        }




    }
}