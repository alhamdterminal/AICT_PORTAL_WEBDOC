using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    [Authorize]
    public class ServiceCompletionController : ParentController
    {
        private IServiceCompletionRepository _serviceCompletionRepo;
        private IExportContainerRepository _exportContainerRepo;
        private IOGDERepository _ogdeRepo;

        public ServiceCompletionController(IServiceCompletionRepository serviceCompletionRepo , IExportContainerRepository exportContainerRepo ,
                                            IOGDERepository ogdeRepo)
        {
            _serviceCompletionRepo = serviceCompletionRepo ;
            _exportContainerRepo = exportContainerRepo;
            _ogdeRepo = ogdeRepo;
        }
        public IActionResult ServiceCompletionView()
        {
            return View();
        }

        public IActionResult SaveServiceCompletionContainers(List<ServiceCompletionViewModel> containers)
        {
            var datetime = DateTime.Now;

            var serviceCompletions = new List<ServiceCompletion>();
            var oGDEs = new List<OGDE>();

            foreach (var container in containers)
            {
                var serviceCompletion = new ServiceCompletion
                {
                    ServiceDate = datetime,
                    ActivityType = container.ActivityType,
                    Category = "E",
                    Location = container.Location,
                     Weight = container.Weight
                };

                serviceCompletions.Add(serviceCompletion);

                var exportcontainer = _exportContainerRepo.Find(container.ExportContainerId);
                exportcontainer.ExaminationArranged= true;
                _exportContainerRepo.Update(exportcontainer);

                var oGDE = new OGDE
                {
                    ContainerNo = container.ContainerNo,
                    VehicleNo = container.VIRNumber,
                    GDNumber = container.GDNumber,
                    PackageType = container.PackageType,
                    NoOfPackages = Convert.ToInt32(container.NoOfPackages),
                    Performed = datetime,
                    MessageFrom = "WeBOC",
                    MessageTo = "BWP",
                    FileName = $"SRCO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                };

                oGDEs.Add(oGDE);
            }

            _ogdeRepo.AddRange(oGDEs);

            _serviceCompletionRepo.AddRange(serviceCompletions);

           
            return Ok();
        }

    }
}