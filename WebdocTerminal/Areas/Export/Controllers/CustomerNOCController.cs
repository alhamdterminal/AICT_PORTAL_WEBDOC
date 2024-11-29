using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class CustomerNOCController : ParentController
    {
        private IShippingAgentExportRepository _shippingAgentExportRepo;
        private ICustomerNOCRepository _customerNOCRepo;
        private ILoadingProgramRepository _loadingProgramRepo;
        private IEnteringCargoRepository _enteringCargoRepo;

        public CustomerNOCController(IShippingAgentExportRepository shippingAgentExportRepo,
                                     ICustomerNOCRepository customerNOCRepo,
                                     ILoadingProgramRepository loadingProgramRepo,
                                     IEnteringCargoRepository enteringCargoRepo)
        {
            _shippingAgentExportRepo = shippingAgentExportRepo;
            _customerNOCRepo = customerNOCRepo;
            _loadingProgramRepo = loadingProgramRepo;
            _enteringCargoRepo = enteringCargoRepo;
        }

        public IActionResult CustomerNOCView()
        {
            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll()
            .Select(s => new SelectListItem { Text = s.ShippingAgentName, Value = s.ShippingAgentExportId.ToString() });

            //ViewData["GDNumbers"] = _customerNOCRepo.GetNocGds()
            // .Select(s => new SelectListItem { Text = s.GDNumber, Value = s.GDNumber.ToString() });

            ViewData["GDNumbers"] = _customerNOCRepo.GetCustomerNocAndCargoRollOver()
             .Select(s => new SelectListItem { Text = s.GDNumber, Value = s.GDNumber.ToString() });


            return View();
        }

        public IActionResult AddCustomerNOC(CustomerNOC model)
        {

            try
            {
                var lp = _loadingProgramRepo.GetLoadingProgrambygdnumber(model.GDNumber);
                if (lp != null)
                {
                    lp.ShippingAgentExportId = model.ShippingAgentExportBId;
                    _loadingProgramRepo.Update(lp);

                    _customerNOCRepo.Add(model);


                }

                return new OkObjectResult(new PostObjectResponce { error = false, Message = "Updated" });

            }
            catch (Exception e)
            {
                return new OkObjectResult(new PostObjectResponce { error = true, Message = e.InnerException.InnerException.Message });

            }

        }

        public JsonResult GetCargoDetail(string gdnumber)
        {

            var data = _customerNOCRepo.GetCargoDetail(gdnumber);
            if (data != null)
            {

                return Json(data);
            }

            return Json("");
        }
    }
}