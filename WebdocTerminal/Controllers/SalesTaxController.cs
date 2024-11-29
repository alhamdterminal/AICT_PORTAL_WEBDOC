using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Controllers
{
    [MyAuthorize]
    public class SalesTaxController : Controller
    {
        private ISalesTaxRepository _salesTaxRepo;
        private IInvoiceRepository _invoiceRepository;
        private ISalesTaxIndexWiseRepository _salesTaxIndexWiseRepo;

        public SalesTaxController(ISalesTaxRepository salesTaxRepo , ISalesTaxIndexWiseRepository salesTaxIndexWiseRepo , IInvoiceRepository invoiceRepository)
        {
            _salesTaxRepo = salesTaxRepo;
            _salesTaxIndexWiseRepo = salesTaxIndexWiseRepo;
            _invoiceRepository = invoiceRepository;
        }

        public IActionResult SalesTaxView()
        {
            return View();
        }

        public IActionResult ImportSalesTaxForIndex()
        {
            return View();
        }

        [HttpGet]
        public JsonResult getSalesTax()
        {
            var data = _salesTaxRepo.GetFirst();
            return Json(data);
        }

        [HttpPost]
        public IActionResult UpdateSalesTax(int salesTax , long id)
        {
            var value = _salesTaxRepo.GetFirst(x => x.SalesTaxId == id);
            value.SalesTaxAmount = salesTax;
            _salesTaxRepo.Update(value);
            return Ok();
        }

        [HttpGet]
        public JsonResult getSalesTaxImpoer()
        {
            var data = _salesTaxRepo.GetAll().Where(x=> x.Type == "Import").FirstOrDefault();
            return Json(data);
        }

        [HttpGet]
        public JsonResult getSalesTaxExport()
        {
            var data = _salesTaxRepo.GetAll().Where(x => x.Type == "Export").FirstOrDefault();
            return Json(data);
        }

        [HttpPost]
        public IActionResult UpdateSalesTaxImport(int salesTax, long id)
        {
            var value = _salesTaxRepo.GetFirst(x => x.SalesTaxId == id);
            value.SalesTaxAmount = salesTax;
            _salesTaxRepo.Update(value);
            return Ok();
        }

        [HttpPost]
        public IActionResult UpdateSalesTaxExport(int salesTax, long id)
        {
            var value = _salesTaxRepo.GetFirst(x => x.SalesTaxId == id);
            value.SalesTaxAmount = salesTax;
            _salesTaxRepo.Update(value);
            return Ok();
        }


        [HttpPost]
        public IActionResult SaleTaxIndexForCFS(long ContainerIndexId , long salesTaxAmount)
        {


            var invoice = _invoiceRepository.GetAll().Where(x => x.ContainerIndexId == ContainerIndexId && x.IsCancelled != true).FirstOrDefault();
            if (invoice != null)
            {
                return new OkObjectResult(new { error = true, message = "can not update because invoice created" });

            }



            var salesTaxIndexWise = new SalesTaxIndexWise
            {
                SalesTaxAmount = salesTaxAmount,
                ContainerIndexId = ContainerIndexId
            };

            _salesTaxIndexWiseRepo.Add(salesTaxIndexWise);

           return new OkObjectResult(new { error = false, message = "Successfully Save!" });
        }
        
        public IActionResult UpdateSaleTaxIndexForCFS(long ContainerIndexId , long salesTaxAmount)
        {

            var invoice = _invoiceRepository.GetAll().Where(x => x.ContainerIndexId == ContainerIndexId && x.IsCancelled != true).FirstOrDefault();
            if(invoice != null)
            {
                return new OkObjectResult(new { error = true, message = "can not update because invoice created" });

            }

            var data = _salesTaxIndexWiseRepo.GetAll().Where(x => x.ContainerIndexId == ContainerIndexId).FirstOrDefault();
            if(data != null)
            {

                data.SalesTaxAmount = salesTaxAmount;
                 
                _salesTaxIndexWiseRepo.Update(data);

                return new OkObjectResult(new { error = false, message = "Updated" });


            }

            return new OkObjectResult(new { error = true, message = "This igm and index not saved" });
        }

        public IActionResult SaleTaxIndexForCY(long cycontainerid, long salesTaxAmount)
        {

            var invoice = _invoiceRepository.GetAll().Where(x => x.CYContainerId == cycontainerid && x.IsCancelled != true).FirstOrDefault();
            if (invoice != null)
            {
                return new OkObjectResult(new { error = true, message = "can not update because invoice created" });

            }


            var salesTaxIndexWise = new SalesTaxIndexWise
            {
                SalesTaxAmount = salesTaxAmount,
                CYContainerId = cycontainerid
            };

            _salesTaxIndexWiseRepo.Add(salesTaxIndexWise);

           return new OkObjectResult(new { error = false, message = "Successfully Save!" });
        }

        public IActionResult UpdateSaleTaxIndexForCY(long CYContainerId , long salesTaxAmount)
        {

            var invoice = _invoiceRepository.GetAll().Where(x => x.CYContainerId == CYContainerId && x.IsCancelled != true).FirstOrDefault();
            if (invoice != null)
            {
                return new OkObjectResult(new { error = true, message = "can not update because invoice created" });

            }


            var data = _salesTaxIndexWiseRepo.GetAll().Where(x => x.CYContainerId == CYContainerId).FirstOrDefault();
            if (data != null)
            {

                data.SalesTaxAmount = salesTaxAmount;

                _salesTaxIndexWiseRepo.Update(data);

                return new OkObjectResult(new { error = false, message = "Updated" });


            }

            return new OkObjectResult(new { error = true, message = "This igm and index not saved" });
        }
    }
}