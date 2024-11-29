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
    public class ContainerAssociationController : ParentController
    {
        private IExportContainerRepository _exportContainerRepo;
        private IOGDCRepository _ogdcRepo;
        private IContainerAssociationRepository _containerAssociationRepo;

        public ContainerAssociationController(IExportContainerRepository exportContainerRepo, IOGDCRepository ogdcRepo,
                                                IContainerAssociationRepository containerAssociationRepo)
        {
            _exportContainerRepo = exportContainerRepo;
            _ogdcRepo = ogdcRepo;
            _containerAssociationRepo = containerAssociationRepo;
        }
        public IActionResult ContainerAssociation()
        {
            return View();
        }

        public IActionResult SaveContainerAssociation(IList<ContainerAssociationViewModel> containers)
        {
            var local = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");

            var datetime = TimeZoneInfo.ConvertTime(DateTime.Now, local);

            var containerAssociations = new List<ContainerAssociation>();
            var oGDCs = new List<OGDC>();

            foreach (var container in containers)
            {
                var containerAssociation = new ContainerAssociation
                {
                    AssociationDate = datetime,
                    ContainerNumber = container.ContainerNumber,
                    VehicleNumber = container.VehicleNumber,
                    GDNumber = container.GDNumber,
                    NumberofPackages = container.NumberofPackages,
                    ShippingLineCode = container.ShippingLineCode,
                    ShippingLineNTN = container.ShippingLineNTN,
                    ShippingLineName = container.ShippingLineName,
                    ConsolidationStatus = container.ConsolidationStatus

                };

                containerAssociations.Add(containerAssociation);

                var exportcontainer = _exportContainerRepo.Find(container.ExportContainerId);
                exportcontainer.ContainerAssociated = true;
                _exportContainerRepo.Update(exportcontainer);

                var oGDC = new OGDC
                {
                    ContainerNumber = container.ContainerNumber,
                    VehicleNumber = container.VehicleNumber,
                    GDNumber = container.GDNumber,
                    NumberofPackages = Convert.ToInt32(container.NumberofPackages),
                    ShippingLineNTN = container.ShippingLineNTN,
                    ShippingLineCode = container.ShippingLineCode,
                    ShippingLineName = container.ShippingLineName,
                    ContainerConsolidationStatus = container.ConsolidationStatus,
                    OperationType = "A",
                    MessageFrom = "BWP",
                    MessageTo = "WEBOC",
                    FileName = $"SRCO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                };

                oGDCs.Add(oGDC);
            }

            _ogdcRepo.AddRange(oGDCs);

            _containerAssociationRepo.AddRange(containerAssociations);

            return Ok();
        }
    }
}