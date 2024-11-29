using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class GDManagementController : ParentController
    {
        private IEnteringCargoRepository _enteringCargoRepo;

        public GDManagementController(IEnteringCargoRepository enteringCargoRepo)
        {
            _enteringCargoRepo = enteringCargoRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetSearchPanel(string Type)
        {
            ViewData["Type"] = Type;

            return PartialView("GDSearch");
        }

        // public List<GDListViewModel> GetGDs(DateTime? StartDate, DateTime? EndDate, string GDNumber, string Type)
        public List<GDListViewModel> GetGDs(string Type)
        {
            var res = _enteringCargoRepo.GetGDS(Type);

            return res;
            //if(GDNumber == null)
            //{
            //    GDNumber = "";
            //}
            //return _enteringCargoRepo.GetGDS(StartDate, EndDate, GDNumber, Type);
        }

        [HttpPost]
        public IActionResult UpdateGDs(List<GDListViewModel> gds)
        {
            try
            {
                foreach (var gd in gds)
                {
                    var cargo = _enteringCargoRepo.GetEnteringCargoById(gd.Id);

                    if (cargo != null)
                    {

                        cargo.ANFStatus = gd.IsChecked ? "CLEARED" : "NOT CLEARED";
                        cargo.ANFDate = gd.PaperReadyDate;


                        _enteringCargoRepo.Update(cargo);
                    }


                }

                return new OkObjectResult(new { error = false, message = "Updated" });



            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }


            return Ok();
        }


        public IActionResult UpdateGDsPaperReady(List<GDListViewModel> gds)
        {
            try
            {
                foreach (var gd in gds)
                {
                    var cargo = _enteringCargoRepo.GetEnteringCargoById(gd.Id);

                    if (cargo != null)
                    {
                        cargo.PaperReady = gd.IsChecked ? "SUBMITTED" : "NOT SUBMITTED";

                        cargo.PaperReadyDate = gd.PaperReadyDate;

                        _enteringCargoRepo.Update(cargo);
                    }


                }
                return new OkObjectResult(new { error = false, message = "Updated" });


            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


            return Ok();
        }
    }
}