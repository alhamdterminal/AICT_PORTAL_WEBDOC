using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class POLocationController : ParentController
    {
        private IPOLocationRepository _poLocationRepo;
        private ILoadingProgramRepository _loadingProgramRepo;
        private ILoadingProgramDetailRepository _loadingProgramDetailRepo;

        public POLocationController(IPOLocationRepository poLocationRepo , ILoadingProgramRepository loadingProgramRepo 
                                    , ILoadingProgramDetailRepository loadingProgramDetailRepo)
        {
            _poLocationRepo = poLocationRepo;
            _loadingProgramRepo = loadingProgramRepo;
            _loadingProgramDetailRepo = loadingProgramDetailRepo;
        }

        public IActionResult POLocationView()
        {
            return View();
        }

         

        [HttpPost]
        public IActionResult AddPOLocation(POLocation values ,  string poNumber , string lpnumber)
        {
            values.PONumber = poNumber;
            values.LoadingProgramNumber = lpnumber;
            _poLocationRepo.Add(values);

            return new OkObjectResult(new PostObjectResponce { error = false, Id = values.POLocationId, Message = "Saved" });
        }

        public JsonResult GetPoLocationByPONumber(string lpNumber , string poNumber)
        {
            var data = _poLocationRepo.GetFirst(x => x.LoadingProgramNumber == lpNumber && x.PONumber == poNumber);
            
            if(data != null)
            {
                return Json(data);
            }

            return Json("");
        }

        public IActionResult UpdatePOLocation(POLocation model , string poNumber, string   lpnumber,
                                            long POLocationId)
        {
            var data = _poLocationRepo.GetFirst(x => x.POLocationId == POLocationId);
            data.POLocationId = POLocationId;
            data.PONumber = poNumber;
            data.LoadingProgramNumber = lpnumber;
            data.Location = model.Location;

            _poLocationRepo.Update(data);

            return Ok();

        }





    }
}