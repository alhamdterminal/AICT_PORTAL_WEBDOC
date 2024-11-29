using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class ServicesInformationController : ParentController
    {
        public IServicesTypeRepository _servicesTypeRepo;
        public IServicesSectionRepository _servicesSectionRepo;
        public INatureOfPaymentRepository _natureOfPaymentRepo;
        public IServicesInfoRepository _servicesInfoRepo;
        public IOperationDprtRepository _operationDprtRepo;
        public ServicesInformationController(IServicesTypeRepository servicesTypeRepo
                                   , IServicesSectionRepository servicesSectionRepo
                                   , INatureOfPaymentRepository natureOfPaymentRepo
                                   , IServicesInfoRepository servicesInfoRepo
                                   , IOperationDprtRepository operationDprtRepo)
        {
            _servicesTypeRepo = servicesTypeRepo;
            _servicesSectionRepo = servicesSectionRepo;
            _natureOfPaymentRepo = natureOfPaymentRepo;
            _servicesInfoRepo = servicesInfoRepo;
            _operationDprtRepo = operationDprtRepo;
        }


        public IActionResult ServicesType()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetServicesType()
        {
            var servicesTypes = _servicesTypeRepo.GetAll();
            return Json(servicesTypes);
        }

        [HttpPost]
        public IActionResult AddServicesType(ServicesType servicesType)
        {
            _servicesTypeRepo.Add(servicesType);
            return new OkObjectResult(new { ServicesTypeId = servicesType.ServicesTypeId });
        }

        [HttpPost]
        public IActionResult updateServicesType(ServicesType servicesType)
        {
            _servicesTypeRepo.Update(servicesType);
            return new OkObjectResult(new { ServicesTypeId = servicesType.ServicesTypeId });
        }


        public IActionResult ServiceSection()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetServiceSection()
        {
            var servicesSections = _servicesSectionRepo.GetAll();
            return Json(servicesSections);
        }

        [HttpPost]
        public IActionResult AddServiceSection(ServicesSection servicesSection)
        {
            _servicesSectionRepo.Add(servicesSection);
            return new OkObjectResult(new { ServicesSectionId = servicesSection.ServicesSectionId });
        }

        [HttpPost]
        public IActionResult UpdateServicesSection(ServicesSection servicesSection)
        {
            _servicesSectionRepo.Update(servicesSection);
            return new OkObjectResult(new { ServicesSectionId = servicesSection.ServicesSectionId });
        }


        public IActionResult NatureOfPayment()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetNatureOfPayment()
        {
            var natureOfPayments = _natureOfPaymentRepo.GetAll();
            return Json(natureOfPayments);
        }

        [HttpPost]
        public IActionResult AddNatureOfPayment(NatureOfPayment natureOfPayment)
        {
            _natureOfPaymentRepo.Add(natureOfPayment);
            return new OkObjectResult(new { NatureOfPaymentId = natureOfPayment.NatureOfPaymentId });
        }

        [HttpPost]
        public IActionResult UpdateNatureOfPayment(NatureOfPayment natureOfPayment)
        {
            _natureOfPaymentRepo.Update(natureOfPayment);
            return new OkObjectResult(new { NatureOfPaymentId = natureOfPayment.NatureOfPaymentId });
        }


        public IActionResult ServiceInfo()
        {


            ViewData["ServicesSection"] = _servicesSectionRepo.GetAll().OrderBy(s => s.ServicesSectionCode)
                .Select(s => new SelectListItem { Text = s.ServicesSectionCode, Value = s.ServicesSectionId.ToString() });
            
            ViewData["ServicesType"] = _servicesTypeRepo.GetAll().OrderBy(s => s.ServicesTypeName)
                .Select(s => new SelectListItem { Text = s.ServicesTypeName, Value = s.ServicesTypeId.ToString() });
            
            ViewData["NatureOfPayment"] = _natureOfPaymentRepo.GetAll().OrderBy(s => s.NatureOfPaymentName)
                .Select(s => new SelectListItem { Text = s.NatureOfPaymentName, Value = s.NatureOfPaymentId.ToString() });


            return View();
        }

        [HttpGet]
        public JsonResult GetServiceInfo()
        {
            var serviceInfo = _servicesInfoRepo.GetServicesInfo();
            return Json(serviceInfo);
        }

        [HttpPost]
        public IActionResult AddServiceInfo(ServicesInfo serviceInfo)
        {
            _servicesInfoRepo.Add(serviceInfo);

            return new OkObjectResult(new { error = false,   Message = "Saved" });

        }

        [HttpPost]
        public IActionResult UpdateServiceInfo(ServicesInfo serviceInfo)
        {
            _servicesInfoRepo.Update(serviceInfo);

            return new OkObjectResult(new { ServicesInfoId = serviceInfo.ServicesInfoId });
        }


        public IActionResult OperationView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetOperationDprt()
        {
            var operationDprt = _operationDprtRepo.GetAll();

            return Json(operationDprt);
        }

        [HttpPost]
        public IActionResult AddOperationDprt(OperationDprt operationDprt)
        {
            _operationDprtRepo.Add(operationDprt);

            return new OkObjectResult(new { OperationDprtId = operationDprt.OperationDprtId });
        }

        [HttpPost]
        public IActionResult UpdateOperationDprt(OperationDprt operationDprt)
        {
            _operationDprtRepo.Update(operationDprt);

            return new OkObjectResult(new { OperationDprtId = operationDprt.OperationDprtId });
        }




    }
}
