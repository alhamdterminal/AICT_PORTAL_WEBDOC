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
    public class EmptyReceivingController : ParentController
    {
        private IEmptyReceivingRepository _emptyReceivingRepo;
        private IShippingLineRepository  _shippingLineRepo;
        private IExportContainerRepository _exportContainerRepo;
        private IShippingAgentExportRepository  _shippingAgentExportRepo;

        public EmptyReceivingController(IEmptyReceivingRepository emptyReceivingRepo , IShippingLineRepository shippingLineRepo 
                                        , IShippingAgentExportRepository shippingAgentExportRepo, IExportContainerRepository exportContainerRepo)
        {

            _emptyReceivingRepo = emptyReceivingRepo;
            _shippingLineRepo = shippingLineRepo;
            _shippingAgentExportRepo = shippingAgentExportRepo;
            _exportContainerRepo = exportContainerRepo;
        }

        public IActionResult EmptyReceivingView()
        {

            ViewData["ShippingLine"] = _shippingLineRepo.GetAll()
                   .Select(s => new SelectListItem { Text = s.ShippingLineName, Value = s.ShippingLineId.ToString() });

            ViewData["ShippingAgent"] = _shippingAgentExportRepo.GetAll()
                   .Select(s => new SelectListItem { Text = s.ShippingAgentName, Value = s.ShippingAgentExportId.ToString() });

            ViewData["CRO"] = _exportContainerRepo.GetAll().OrderBy(x => x.CRONumber)
                   .Select(s => new SelectListItem { Text = s.CRONumber, Value = s.ContainerNumber.ToString() });

            return View();
        }

        public JsonResult GetContainersBYCRO(string croNo)
        {
            var data = _emptyReceivingRepo.GetList(x => x.CRONumber == croNo);
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }


        public JsonResult GetExportContainersBYCROnumber(string croNo)
        {
            var data = _exportContainerRepo.GetList(x => x.CRONumber == croNo)
                        .Select(x => new SelectListItem { Text = x.ContainerNumber  , Value = x.ContainerNumber});


            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }

        public JsonResult getContainerDetail(string croNumber, string containerNumber)
        {
            var data = _exportContainerRepo.GetFirst(x => x.CRONumber == croNumber && x.ContainerNumber == containerNumber);
            if (data != null)
            {

                return Json(data);

            }
            return Json("");
        }



        public IActionResult AddEmptyReceiving(EmptyReceiving model, string croNumber, string containerNumber)
        {
            model.CRONumber = croNumber;
            model.ContainerNumber = containerNumber;
            _emptyReceivingRepo.Add(model);
            return new OkObjectResult(new PostObjectResponce { error = false, Id = model.EmptyReceivingId, Message = "Saved" });
        }

        public JsonResult GetEmptyReceiving(string croNumber, string containerNo)
        {
            var data = _emptyReceivingRepo.GetFirst(x => x.CRONumber == croNumber && x.ContainerNumber == containerNo);

            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }

        public IActionResult updateEmptyReceiving(EmptyReceiving model, string croNumber, string containerNumber, long EmptyReceivingId)
        {
            var data = _emptyReceivingRepo.GetFirst(x => x.EmptyReceivingId == EmptyReceivingId);

            data.ContainerNumber = containerNumber;
            data.ContainerCondition = model.ContainerCondition;
            data.ContainerSize = model.ContainerSize;
            data.ContainerTareWeight = model.ContainerTareWeight;
            data.EmptyReceivingId = EmptyReceivingId;
            data.CRONumber = croNumber;
            data.ShippingLineId = model.ShippingLineId;
            data.ShippingAgentExportId = model.ShippingAgentExportId;
            data.RecevingDate = model.RecevingDate;
            data.VehicleNumber = model.VehicleNumber;


            _emptyReceivingRepo.Update(data);

            return Ok();
        }
    }
}