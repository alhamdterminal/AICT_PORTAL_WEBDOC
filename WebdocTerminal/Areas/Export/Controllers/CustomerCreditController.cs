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
    public class CustomerCreditController : ParentController
    {
        private ICreditToCustomerRepository _repo;
        private IVoyageExportRepository _voyageExportRepo;
        private IVesselExportRepository _vesselExportRepo;

        public CustomerCreditController(ICreditToCustomerRepository repo, IVoyageExportRepository voyageExportRepo, IVesselExportRepository vesselExportRepo)
        {
            _repo = repo;
            _voyageExportRepo = voyageExportRepo;
            _vesselExportRepo = vesselExportRepo;
        }

        public IActionResult Index()
        {
            ViewData["VoyageExports"] = _voyageExportRepo.GetAll()
                .Select(x => new SelectListItem { Text = x.VoyageNumber, Value = x.VoyageExportId.ToString() });

            ViewData["VesselExports"] = _vesselExportRepo.GetAll()
                .Select(s => new SelectListItem { Text = s.VesselName, Value = s.VesselExportId.ToString() });

            return View();
        }

        public CreditToCustomerViewModel FindCreditToCustomer(string gdnumber)
        {
            return _repo.GetCreditToCustomer(gdnumber);
        }

        public IActionResult SaveCreditToCustomer(CreditToCustomer model)
        {
            _repo.Add(model);
            return new OkObjectResult(new PostObjectResponce { error = false, Id = model.CreditToCustomerId, Message = "Saved" });

        }

        public void UpdateCreditToCustomer(CreditToCustomer model)
        {
            _repo.Update(model);
        }
    }
}