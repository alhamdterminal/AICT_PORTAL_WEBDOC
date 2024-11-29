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
    public class CargoRollOverController : ParentController
    {
        private ICargoRollOverRepository _cargoRollOverRepo;
        private IVesselExportRepository _vesselExportRepos;
        private IVoyageExportRepository _voyageExportRepo;
        private IEnteringCargoRepository _enteringCargoRepository;
        private ILoadingProgramRepository _loadingProgramRepository;
        private ICargoRepository _cargoRepository;
        private ICustomerNOCRepository _customerNOCRepository;
        private IInvoiceExportRepository _invoiceExportRepository;
        private IExportContainerItemRepositpory _exportContainerItemRepositpory;

        public CargoRollOverController(ICargoRollOverRepository cargoRollOverRepo, 
                                       IVesselExportRepository vesselExportRepos , 
                                       IVoyageExportRepository voyageExportRepo, 
                                       IEnteringCargoRepository enteringCargoRepository, 
                                       ILoadingProgramRepository loadingProgramRepository, 
                                       ICargoRepository cargoRepository,
                                       ICustomerNOCRepository customerNOCRepository,
                                       IInvoiceExportRepository invoiceExportRepository,
                                       IExportContainerItemRepositpory exportContainerItemRepositpory)
        {
            _cargoRollOverRepo = cargoRollOverRepo;
            _vesselExportRepos = vesselExportRepos;
            _voyageExportRepo = voyageExportRepo;
            _enteringCargoRepository = enteringCargoRepository;
            _loadingProgramRepository = loadingProgramRepository;
            _cargoRepository = cargoRepository;
            _customerNOCRepository = customerNOCRepository;
            _invoiceExportRepository = invoiceExportRepository;
            _exportContainerItemRepositpory = exportContainerItemRepositpory;
        }

        public IActionResult CargoRollOverView()
        {

            ViewData["VesselExport"] = _vesselExportRepos.GetAll()
                .Select(s => new SelectListItem { Text = s.VesselName, Value = s.VesselExportId.ToString() });

            //ViewData["GDNumbers"] = _customerNOCRepository.GetNocGds()
            //.Select(s => new SelectListItem { Text = s.GDNumber, Value = s.GDNumber.ToString() });

            ViewData["GDNumbers"] = _customerNOCRepository.GetCustomerNocAndCargoRollOver()
            .Select(s => new SelectListItem { Text = s.GDNumber, Value = s.GDNumber.ToString() });


            ViewData["ContainerNumber"] = _customerNOCRepository.GetCustomerNocAndCargoRollOverContainers()
            .Select(s => new SelectListItem { Text = s.ContainerNumber, Value = s.ExportContainerId.ToString() });




            return View();
        }

        public JsonResult GetGDDeatil(string gdnumber)
        {
            var data = _cargoRollOverRepo.GetGDDeatilbygdnumber(gdnumber);
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }
        public IActionResult AddCargoRollOver(string gdnumber, long voyageExportId, long vesselExportId)
        {

            try
            {

                var gdinfo = _customerNOCRepository.GetGDIfo(gdnumber);

                if (gdinfo != null)
                {
                    return new OkObjectResult(new PostObjectResponce { error = true, Message = "due to ovam not updated" });
                }

                var invoces = _invoiceExportRepository.GetInvoiceExportbygdnumber(gdnumber).ToList();


                if (invoces.Count() > 0)
                {
                    invoces.ForEach(x => x.VesselExportId = vesselExportId);
                    invoces.ForEach(x => x.VoyageExportId = voyageExportId);
                }

                _invoiceExportRepository.UpdateRange(invoces);

                var LoadingProgram = _loadingProgramRepository.GetLoadingProgrambygdnumber(gdnumber);
                if (LoadingProgram != null)
                {
                    LoadingProgram.VesselExportId = vesselExportId;
                    LoadingProgram.VoyageExportId = voyageExportId;
                    _loadingProgramRepository.Update(LoadingProgram);

                    var data = new CargoRollOver
                    {
                        GDNumber = gdnumber,
                        VoyageExportId = voyageExportId,
                        VesselExportId = vesselExportId
                    };
                    _cargoRollOverRepo.Add(data);

                    return new OkObjectResult(new PostObjectResponce { error = false, Message = "Updated" });

                }
                else
                {
                    return new OkObjectResult(new PostObjectResponce { error = true, Message = "gd not found" });
                }
            }
            catch (Exception e)
            {
                return new OkObjectResult(new PostObjectResponce { error = true, Message = e.InnerException.InnerException.Message });
            }



        }

        public IActionResult CargoRollOverContainerWise()
        {
            ViewData["VesselExport"] = _vesselExportRepos.GetAll()
                .Select(s => new SelectListItem { Text = s.VesselName, Value = s.VesselExportId.ToString() });


            ViewData["ContainerNumber"] = _customerNOCRepository.GetCustomerNocAndCargoRollOverContainers()
            .Select(s => new SelectListItem { Text = s.ContainerNumber, Value = s.ExportContainerId.ToString() });

            return View();
        }

        public JsonResult GetGDDeatilbyexportcontainerid(long ExportContainerId)
        {

            var data = _exportContainerItemRepositpory.GetAll().Where(x => x.ExportContainerId == ExportContainerId).ToList();

            return Json(data);

        }

        public IActionResult AddCargoRollOverContainerWise(long exportcontainerid, long voyageExportId, long vesselExportId)
        {

            try
            {
                var exportcontaineritems = _exportContainerItemRepositpory.GetAll().Where(x => x.ExportContainerId == exportcontainerid).ToList();

                var item = exportcontaineritems.Where(x => x.IsOvams == true).LastOrDefault();
                if (item != null)
                {
                    return new OkObjectResult(new PostObjectResponce { error = true, Message = "due to ovam not updated" });
                }

                foreach (var resitem in exportcontaineritems)
                {

                    var invoces = _invoiceExportRepository.GetInvoiceExportbygdnumber(resitem.GDNumber).ToList();


                    if (invoces.Count() > 0)
                    {
                        invoces.ForEach(x => x.VesselExportId = vesselExportId);
                        invoces.ForEach(x => x.VoyageExportId = voyageExportId);
                    }

                    _invoiceExportRepository.UpdateRange(invoces);

                    var LoadingProgram = _loadingProgramRepository.GetLoadingProgrambygdnumber(resitem.GDNumber);
                    if (LoadingProgram != null)
                    {
                        LoadingProgram.VesselExportId = vesselExportId;
                        LoadingProgram.VoyageExportId = voyageExportId;
                        _loadingProgramRepository.Update(LoadingProgram);

                        var data = new CargoRollOver
                        {
                            GDNumber = resitem.GDNumber,
                            VoyageExportId = voyageExportId,
                            VesselExportId = vesselExportId
                        };
                        _cargoRollOverRepo.Add(data);



                    }

                }

                return new OkObjectResult(new PostObjectResponce { error = false, Message = "Updated" });

            }
            catch (Exception e)
            {
                return new OkObjectResult(new PostObjectResponce { error = true, Message = e.InnerException.InnerException.Message });
            }



        }


    }
}