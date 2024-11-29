using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{

    [MyAuthorize]
    public class InvoiceExportController : ParentController
    {
        public IClearingAgentExportRepository _clearingAgentExportRepo;
        public IInvoiceExportRepository _invoiceExportRepo;
        public IInvoiceDocumentExportRepository _invoiceDocumentExportRepository;
        public IAmountReceivedExportRepository _amountReceivedExportRepo;
        public IPartyLedgerExportRepository _partyLedgerExportRepository;
        private IHostingEnvironment env;
        private IEnteringCargoRepository _enteringCargoRepository;
        private IInvoiceItemExportRepository _invoiceItemExportRepository;
        private IPartyExportRepository _partyExportRepository;
        private IChequeRecivedExportRepository _chequeRecivedExportRepository;
        private ILoadingProgramRepository _loadingProgramRepository;

        public InvoiceExportController(IClearingAgentExportRepository clearingAgentExportRepo,
                                        IInvoiceExportRepository invoiceExportRepo,
                                        IInvoiceDocumentExportRepository invoiceDocumentExportRepository,
                                        IAmountReceivedExportRepository amountReceivedExportRepo,
                                        IPartyLedgerExportRepository partyLedgerExportRepository,
                                        IHostingEnvironment _env,
                                        IEnteringCargoRepository enteringCargoRepository,
                                        IInvoiceItemExportRepository invoiceItemExportRepository,
                                        IPartyExportRepository partyExportRepository,
                                        IChequeRecivedExportRepository chequeRecivedExportRepository,
                                        ILoadingProgramRepository loadingProgramRepository
                                        )
        {
            _clearingAgentExportRepo = clearingAgentExportRepo;
            _invoiceExportRepo = invoiceExportRepo;
            _invoiceDocumentExportRepository = invoiceDocumentExportRepository;
            _partyLedgerExportRepository = partyLedgerExportRepository;
            _amountReceivedExportRepo = amountReceivedExportRepo;
            env = _env;
            _enteringCargoRepository = enteringCargoRepository;
            _invoiceItemExportRepository = invoiceItemExportRepository;
            _partyExportRepository = partyExportRepository;
            _chequeRecivedExportRepository = chequeRecivedExportRepository;
            _loadingProgramRepository = loadingProgramRepository;
        }
        public IActionResult InvoiceExportView()
        {
            return View();
        }
        public decimal cbm { get; set; }

        //  , string igm, long indexNo use less perameter


        public string GetLastInvoiceNo()
        {

            var year = DateTime.Now.ToString("yyyy");

            var invo = _invoiceExportRepo.GetInvoiceLast();

            if (invo != null)
            {
                if (invo.Year == year)
                {
                    var no = invo.InvoiceNo.Substring(0, invo.InvoiceNo.IndexOf("/"));

                    var number = Convert.ToInt64(no) + 1;
                    var InvoiceNo = number + "/" + year;

                    return InvoiceNo;
                }
                else
                {
                    var no = 1;

                    var InvoiceNo = no + "/" + year;

                    return InvoiceNo;
                }
            }
            else
            {
                var no = 1;

                var InvoiceNo = no + "/" + year;

                return InvoiceNo;

            }


        }


        public IActionResult SaveBill(InvoiceExport invoice, IList<InvoiceItemExport> allItems, string gdnumber)
        {

            var entringcargo = _enteringCargoRepository.GetEnteringCargo(gdnumber);

            var loadingprogram = _loadingProgramRepository.GetLoadingProgrambygdnumber(gdnumber);

            if (entringcargo != null && loadingprogram != null)
            {

                invoice.EnteringCargoId = entringcargo.EnteringCargoId;
                invoice.VesselExportId = loadingprogram.VesselExportId;
                invoice.VoyageExportId = loadingprogram.VoyageExportId;


                invoice.AtTimeOfInvoiceVesselExportId = loadingprogram.VesselExportId;
                invoice.AtTimeOfInvoiceVoyageExportId = loadingprogram.VoyageExportId;


                List<InvoiceItemExport> invoiceitemList = new List<InvoiceItemExport>();
                List<InvoiceItemExport> normalinvoiceitemList = new List<InvoiceItemExport>();
                List<InvoiceItemExport> oldinvoiceitem = new List<InvoiceItemExport>();

                invoice.InvoiceDate = DateTime.Now;
                invoice.IsCancelled = false;
                invoice.InvoiceType = "I";
                invoice.Year = DateTime.Now.ToString("yyyy");
                string NormalNumber = "";


                bool subbillstatus = false;

                foreach (var item in allItems)
                {
                    var invoiceItem = new InvoiceItemExport
                    {
                        NatureOfTariff = item.NatureOfTariff,
                        Description = item.Description,
                        Amount = item.Amount,
                        Type = item.Type,
                        ExportTariffId = item.ExportTariffId

                    };
                    invoiceitemList.Add(invoiceItem);
                }
                if (invoice.StorageTotal > 0)
                {
                    var res = new InvoiceItemExport
                    {
                        NatureOfTariff = "Storage Amount",
                        Description = "Storage Amount",
                        Type = "Storage",
                        Amount = invoice.StorageTotal
                    };

                    invoiceitemList.Add(res);
                }



                if (invoice.EnteringCargoId != null)
                {

                    if (invoice.AdvanceDate == null)
                    {
                        invoice.AdvanceDate = DateTime.Now;
                    }

                    var enteringCargo = _enteringCargoRepository.GetEnteringCargoById(invoice.EnteringCargoId ?? 0);

                    var invoices = _invoiceExportRepo.GetInvoiceItemBycontainerIndexId(invoice.EnteringCargoId ?? 0).ToList();

                    if (invoices.Count() > 0)
                    {


                        subbillstatus = true;

                        oldinvoiceitem = invoices;

                        var newinvoiceitem = new List<InvoiceItemExport>();

                        if (oldinvoiceitem.Count() > 0)
                        {
                            foreach (var item in oldinvoiceitem)
                            {
                                var result = newinvoiceitem.Find(t => t.Description == item.Description);

                                if (result != null)
                                {
                                    result.Amount += item.Amount;

                                }
                                else
                                {

                                    newinvoiceitem.Add(item);

                                }


                            }


                            foreach (var item in newinvoiceitem)
                            {

                                var result = invoiceitemList.Find(t => t.Description == item.Description);

                                if (result != null)
                                {
                                    result.Amount -= item.Amount;

                                }
                                else
                                {

                                    invoiceitemList.Add(item);

                                }
                            }

                        }


                        invoiceitemList.RemoveAll(c => c.Amount <= 0);

                    }


                    normalinvoiceitemList = invoiceitemList;

                    var invoicetotalchrges = normalinvoiceitemList.Sum(x => x.Amount);
                    var invoicetotalchrgesafterwaiver = invoicetotalchrges - invoice.WaiverAmount;
                    var invoicetotalchrgesaftertax = ((invoicetotalchrgesafterwaiver) * (Convert.ToDouble(invoice.SalesTax) / 100));

                    var invoi = new InvoiceExport
                    {
                        AdvanceDate = invoice.AdvanceDate,

                        BalanceAmount = invoice.BalanceAmount,
                        CBM = invoice.CBM,
                        InvoiceType = "E",
                        ClearingAgentExportId = invoice.ClearingAgentExportId,
                        EnteringCargoId = invoice.EnteringCargoId,
                        ShippingAgent = invoice.ShippingAgent,

                        DestuffDate = invoice.DestuffDate,
                        GateInDate = invoice.GateInDate,
                        ConsigneeNTN = invoice.ConsigneeNTN,
                        InvoiceDate = invoice.InvoiceDate,
                        Year = DateTime.Now.ToString("yyyy"),
                        TariffAmountTotal = invoice.TariffAmountTotal,
                        AdditionalFreeDays = invoice.AdditionalFreeDays,
                        InvoiceNo = GetLastInvoiceNo(),
                        //InvoiceNo = invoice.InvoiceNo,
                        WaiverAmount = invoice.WaiverAmount,
                        IsSubBill = subbillstatus,
                        VesselExportId = invoice.VesselExportId,
                        VoyageExportId = invoice.VoyageExportId,
                        IsAdvanceBill = invoice.IsAdvanceBill,
                        IsCancelled = false,

                        CNIC = invoice.CNIC,
                        ClearingAgentRep = invoice.ClearingAgentRep,
                        PhoneNumber = invoice.PhoneNumber,
                        InvoiceItemExports = normalinvoiceitemList,
                        Size = invoice.Size,
                        StorageDays = invoice.StorageDays,
                        Weight = invoice.Weight,
                        Type = invoice.Type,
                        OtherChargeAmount = invoice.OtherChargeAmount,

                        Consignee = invoice.Consignee,

                        CBMTotal = normalinvoiceitemList.Where(x => x.NatureOfTariff == "CBM").Sum(x => x.Amount),
                        IndexTotal = normalinvoiceitemList.Where(x => x.NatureOfTariff == "Index").Sum(x => x.Amount),
                        BWTotal = normalinvoiceitemList.Where(x => x.Type == "AdditionalBWT").Sum(x => x.Amount),
                        FFTotal = normalinvoiceitemList.Where(x => x.Type == "AdditionalFF").Sum(x => x.Amount),


                        StorageTotal = invoice.StorageTotal,
                        TotalCharges = normalinvoiceitemList.Sum(x => x.Amount),
                        PreviousBillAmount = invoice.PreviousBillAmount,
                        BalanceAmountTotal = normalinvoiceitemList.Sum(x => x.Amount),
                        SalesTax = invoice.SalesTax,
                        AfterSalesTax = invoicetotalchrgesaftertax,
                        GrandTotal = invoicetotalchrgesafterwaiver + invoicetotalchrgesaftertax,
                        AfterDiscount = invoicetotalchrgesafterwaiver,

                    };

                    invoi.GrandTotal = Math.Round(invoi.GrandTotal);

                    _invoiceExportRepo.Add(invoi);
                    NormalNumber = invoi.InvoiceNo;

                    if (invoice.WaiverAmount > 0 && enteringCargo != null)
                    {
                        enteringCargo.WaiverAmount = 0;
                        _enteringCargoRepository.Update(enteringCargo);
                    }


                }



                return new OkObjectResult(new { error = false, message = "created", InvoiceNo = NormalNumber });
            }

            else
            {
                return new OkObjectResult(new { error = true, message = "gd not found" });
            }

            //  return new OkObjectResult(new { InvoiceExportId = invoice.InvoiceExportId, InvoiceNo = invoice.InvoiceNo });

        }

        [HttpPost]
        public IActionResult SaveCapturedImage(string name, long BillNo)
        {
            var files = HttpContext.Request.Form.Files;
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        // Getting Filename  
                        var fileName = file.FileName;
                        // Unique filename "Guid"  
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                        // Getting Extension  
                        var fileExtension = Path.GetExtension(fileName);
                        // Concating filename + fileExtension (unique filename)  
                        var newFileName = string.Concat(name, myUniqueFileName, fileExtension);
                        //  Generating Path to store photo   
                        var filepath = Path.Combine(env.ContentRootPath, "CameraPhotos") + $@"\{newFileName}";

                        if (!string.IsNullOrEmpty(filepath))
                        {
                            // Storing Image in Folder  
                            StoreInFolder(file, filepath);

                            //var invoice = _invoiceExportRepo.GetFirst(b => b.InvoiceNo == BillNo);

                            //if (invoice != null)
                            //{
                            //    invoice.ImageUrl = filepath;
                            //    _invoiceExportRepo.Update(invoice);
                            //}
                        }

                    }
                }
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }


        private void StoreInFolder(IFormFile file, string fileName)
        {
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }

        public IActionResult ReprintInvoice()
        {
            return View();
        }


        public IActionResult DeleteInvoice(string BillNo, string remarks)
        {
            var invoice = _invoiceExportRepo.GetFirst(x => x.InvoiceNo == BillNo);

            invoice.IsCancelled = true;
            invoice.IsDeleted = true;
            invoice.Remarks = remarks;
            if (invoice != null)
            {
                var amountRecive = _amountReceivedExportRepo.GetFirst(x => x.InvoiceExportId == invoice.InvoiceExportId);

                if (amountRecive != null)
                {

                    //var partyLedger = _partyLedgerExportRepository.GetFirst(x => x.AmountReceivedExportId == amountRecive.AmountReceivedExportId);
                    //if (partyLedger != null)
                    //{
                    //    _partyLedgerExportRepository.Delete(partyLedger);
                    //    _amountReceivedExportRepo.Delete(amountRecive);
                    //    _invoiceExportRepo.Update(invoice);
                    //    return new OkObjectResult(new { error = true, message = "Invoice Deleted" });
                    //}
                    //_amountReceivedExportRepo.Delete(amountRecive);
                    //_invoiceExportRepo.Update(invoice);

                    return new OkObjectResult(new { error = true, message = "Invoice Not Deleted Because Amount Received" });
                }

                _invoiceExportRepo.Update(invoice);
                return new OkObjectResult(new { error = true, message = "Invoice Deleted" });




            }

            return new OkObjectResult(new { error = true, message = "Invoice Not Found" });
        }

        public async Task<IActionResult> AddInVoiceDocument(IFormFile formData, string name)
        {
            IList<string> filepaths = new List<string>();

            var f = formData;
            string storePath = f.Name;
            string docFolder = Path.Combine("InvoiceDouments", storePath);

            try
            {
                if (f == null || f.Length == 0)
                {
                    return Content("File not selected");
                }

                string path = FileUploadHandler.GetFilePathForUpload(docFolder, f.FileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await f.CopyToAsync(stream);
                }
            }
            catch (Exception e)
            {
                return Json(new { result = "Upload Failed", error = e.Message });
            }

            string filePath = FileUploadHandler.FileReturnPath(docFolder, f.FileName);

            filepaths.Add(filePath);

            return Ok();
        }


        public IActionResult ReprintInvoiceExport()
        {
            return View();
        }

        public IActionResult DeleteInvoiceOnly(long invoiceId)
        {
            var invoice = _invoiceExportRepo.GetInvoiceByInvoiceId(invoiceId);

            invoice.IsCancelled = true;
            invoice.IsDeleted = true;

            if (invoice != null)
            {

                var res = _invoiceExportRepo.IsGdAssociatedWithInvoice(invoice.EnteringCargoId ?? 0);

                if (res == true)
                {
                    return new OkObjectResult(new { error = true, message = "Can't Delete Due To GD-Associated" });

                }
                var amountRecive = _invoiceExportRepo.GetAmountReceivedByInvoiceId(invoice.InvoiceExportId);

                if (amountRecive != null)
                {

                    return new OkObjectResult(new { error = true, message = "Can't Delete Due To Payment Received" });

                }
                else
                {

                    _invoiceExportRepo.Update(invoice);
                    return new OkObjectResult(new { error = false, message = "Invoice Deleted" });

                }


            }

            return new OkObjectResult(new { error = true, message = "Invoice Not Found" });
        }



        public IActionResult DeleteInvoiceExportView()
        {

            ViewData["Party"] = _partyExportRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.PartyName, Value = v.PartyExportId.ToString() });


            return View();
        }

        public IActionResult DeleteInvoiceFromInvoice(long invoiceId)
        {
            var invoice = _invoiceExportRepo.GetInvoiceByInvoiceId(invoiceId);


            if (invoice != null)
            {
                //var res = _invoiceExportRepo.IsGdAssociatedWithInvoice(invoice.EnteringCargoId ?? 0);

                //if (res == true)
                //{
                invoice.IsCancelled = true;
                invoice.IsDeleted = true;

                _invoiceExportRepo.Update(invoice);
                return new OkObjectResult(new { error = false, message = "Invoice Deleted" });


                //}
                //else
                //{
                //    return new OkObjectResult(new { error = true, message = "Can't Delete Due To GD-Not-Associated" });
                //}

            }

            return new OkObjectResult(new { error = true, message = "Invoice Not Found" });
        }



        public IActionResult DeleteInvoiceAll(long invoiceId)
        {
            var invoice = _invoiceExportRepo.GetInvoiceByInvoiceId(invoiceId);

            //invoice.IsCancelled = true;
            //invoice.IsDeleted = true;
            invoice.IsAmountReceived = false;

            if (invoice != null)
            {
                var amountRecive = _invoiceExportRepo.GetAmountReceivedByInvoiceId(invoice.InvoiceExportId);

                if (amountRecive != null)
                {

                    var partyLedger = _invoiceExportRepo.GetPrtyLedgerListByAmountReceivedExportId(amountRecive.AmountReceivedExportId);

                    if (partyLedger.Count() > 0)
                    {
                        _partyLedgerExportRepository.DeleteRange(partyLedger);

                        _amountReceivedExportRepo.Delete(amountRecive);

                        _invoiceExportRepo.Update(invoice);

                        return new OkObjectResult(new { error = false, message = "Invoice Deleted" });
                    }

                    _amountReceivedExportRepo.Delete(amountRecive);

                    _invoiceExportRepo.Update(invoice);

                    return new OkObjectResult(new { error = false, message = "Invoice Deleted" });

                }


                _invoiceExportRepo.Update(invoice);
                return new OkObjectResult(new { error = true, message = "amount received not found" });




            }

            return new OkObjectResult(new { error = true, message = "Invoice Not Found" });
        }

        public IActionResult DeleteChequeReceived(long ChequeReceivedId)
        {
            var res = _chequeRecivedExportRepository.GetAll().Where(x => x.ChequeRecivedExportId == ChequeReceivedId).LastOrDefault();

            if (res != null)
            {
                var ledgr = _partyLedgerExportRepository.GetAll().Where(x => x.ChequeRecivedExportId == res.ChequeRecivedExportId).LastOrDefault();

                if (ledgr != null)
                {
                    _partyLedgerExportRepository.Delete(ledgr);

                    _chequeRecivedExportRepository.Delete(res);

                    return new OkObjectResult(new { error = false, message = "Cheque Received And Ledger Info Deleted" });

                }

                _chequeRecivedExportRepository.Delete(res);

                return new OkObjectResult(new { error = false, message = "Cheque Received Info Deleted" });
            }

            return new OkObjectResult(new { error = true, message = "No Record Found" });
        }

    }
}