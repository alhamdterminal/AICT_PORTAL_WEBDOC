using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class CargoInformationController : ParentController
    {
        private IContainerRepository _containerRepo;
        private IContainerIndexRepository _containerIndexRepository;
        private ICYContainerRepository _cyContainerRepository;
        private IGateInRepository _gateInRepository;
        private IVoyageRepository _voyageRepository;
        private IDestuffedContainerRepository  _destuffedContainerRepository;


        public CargoInformationController(IContainerRepository containerRepo 
                            , IContainerIndexRepository containerIndexRepository
                            , ICYContainerRepository cyContainerRepository
                            , IGateInRepository gateInRepository
                            , IVoyageRepository voyageRepository
                            , IDestuffedContainerRepository destuffedContainerRepository)
        {
            _containerRepo = containerRepo;
            _containerIndexRepository = containerIndexRepository;
            _cyContainerRepository = cyContainerRepository;
            _gateInRepository = gateInRepository;
            _voyageRepository = voyageRepository;
            _destuffedContainerRepository = destuffedContainerRepository;
        }

        public IActionResult CargoInformation()
        {
            return View();
        }

        public IActionResult CargoDetail()
        {
            return View();
        }

        public IActionResult Section79ContainersCY()
        {
            return View();
        }


        public IActionResult Section79ContainersCFS()
        {
            return View();
        }

        public IActionResult UpdateContainerCFSAgent(long ContainerIndexId , long ShippingAgentId)
        
        {
            var containerIndex = _containerIndexRepository.GetAll().Where(x => x.ContainerIndexId == ContainerIndexId).FirstOrDefault();
            var voyage = _voyageRepository.GetAll().Where(x => x.VoyageId == containerIndex.VoyageId).FirstOrDefault();

            if (containerIndex != null)
            { 
                var containerIndexList = _containerIndexRepository.GetAll().Where(x => x.BLNo == containerIndex.BLNo && x.IndexNo == containerIndex.IndexNo).ToList();

                foreach (var item in containerIndexList)
                {

                    if (containerIndex != null && voyage != null)
                    {
                        var gatein = _gateInRepository.GetAll().Where(x => x.ContainerNo == item.ContainerNo && x.VIRNo == voyage.VIRNo).FirstOrDefault();
                        if(gatein != null)
                        {
                            gatein.ShippingAgentId = ShippingAgentId;
                            _gateInRepository.Update(gatein);
                        }
                    }

                    item.ShippingAgentId = ShippingAgentId;
                    _containerIndexRepository.Update(item);

                }
                  
            }

            

            return Ok();
        }


        public IActionResult UpdateContainerCFSContainerGateInInfo(  CargoInformationViewModel model)

        {
            var containerIndex = _containerIndexRepository.GetAll().Where(x => x.VirNo == model.VirNo && x.ContainerNo == model.ContainerNo).ToList(); 
            var gateincontainer = _gateInRepository.GetAll().Where(x => x.VIRNo == model.VirNo && x.ContainerNo == model.ContainerNo).LastOrDefault(); 

            if (containerIndex.Count() > 0  && gateincontainer != null)
            {

                gateincontainer.ContainerSize = model.Size.ToString();
                gateincontainer.ShippingAgentId = model.ShippingAgentId;

                foreach (var item in containerIndex)
                {
                    item.ShippingAgentId = model.ShippingAgentId;
                    item.Size = model.Size;
                    _containerIndexRepository.Update(item);

                }

                _gateInRepository.Update(gateincontainer);
                return new OkObjectResult(new { error = false, message = "Info Updated" });

            }


            return new OkObjectResult(new { error = true, message = "Not Found" });
        }


        public IActionResult UpdateContainerCFSContainerDestuffingInfo(CargoInformationViewModel model)

        {
            var containerIndex = _containerIndexRepository.GetAll().Where(x => x.ContainerIndexId == model.CotnainerId).LastOrDefault();
            var destuffContainer = _destuffedContainerRepository.GetAll().Where(x => x.ContainerIndexId == model.CotnainerId).LastOrDefault();

            if (containerIndex != null && destuffContainer != null)
            {

                destuffContainer.CBM = model.CBM;
                destuffContainer.Remarks = model.DestuffedRemark;
                destuffContainer.FoundWeight =    model.FoundWeight;
                destuffContainer.FoundPackageType = model.FoundUOP;
                destuffContainer.PackageQuantity = model.FoundQTY;


                containerIndex.CBM = model.CBM ?? 0;
                containerIndex.BLGrossWeight = model.FoundWeight;
 
                _containerIndexRepository.Update(containerIndex);
                  
                _destuffedContainerRepository.Update(destuffContainer);

                return new OkObjectResult(new { error = false, message = "Info Updated" });

            }

            return new OkObjectResult(new { error = true, message = "Not Found" });

        }



        public IActionResult UpdateContainerCYContainerGateInInfo(CargoInformationViewModel model)

        {
            var cycontainer = _cyContainerRepository.GetAll().Where(x => x.VIRNo == model.VirNo && x.ContainerNo == model.ContainerNo).ToList();
            var gateincontainer = _gateInRepository.GetAll().Where(x => x.VIRNo == model.VirNo && x.ContainerNo == model.ContainerNo).LastOrDefault();

            if (cycontainer.Count() > 0 && gateincontainer != null)
            {

                gateincontainer.ContainerSize = model.Size.ToString();
                gateincontainer.ShippingAgentId = model.ShippingAgentId;

                foreach (var item in cycontainer)
                {
                    item.ShippingAgentId = model.ShippingAgentId;
                    item.Size = model.Size;
                    _cyContainerRepository.Update(item);

                }

                _gateInRepository.Update(gateincontainer);

                return new OkObjectResult(new { error = false, message = "Info Updated" });

            }


            return new OkObjectResult(new { error = true, message = "Not Found" });
        }


        public IActionResult UpdateContainerCY(long CyConatinerId, long ShippingAgentId)
        {
            var cyContainer = _cyContainerRepository.GetAll().Where(x => x.CYContainerId == CyConatinerId).FirstOrDefault();

            if (cyContainer != null)
            {
                var gatein = _gateInRepository.GetAll().Where(x => x.ContainerNo == cyContainer.ContainerNo && x.VIRNo == cyContainer.VIRNo).FirstOrDefault();
                if (gatein != null)
                {

                    gatein.ShippingAgentId = ShippingAgentId;
                    _gateInRepository.Update(gatein);
                }

                 cyContainer.ShippingAgentId = ShippingAgentId;
                _cyContainerRepository.Update(cyContainer);
            
            
            }



            return Ok();
        }


        public IActionResult ConvertCyToCfs(long CyConatinerId )
        {
            if (CyConatinerId > 0)
            {
                var cyContainer = _cyContainerRepository.GetAll().Where(x => x.CYContainerId == CyConatinerId).LastOrDefault();
                //var cyContainers = _cyContainerRepository.GetAll().Where(x => x.BLNo == cyContainer.BLNo
                //                                                                 && x.ContainerNo == cyContainer.ContainerNo
                //                                                                 && x.IndexNo == cyContainer.IndexNo
                //                                                                 && x.ManifestedSealNumber == cyContainer.ManifestedSealNumber).ToList();

                if (cyContainer != null)
                {
                    if(cyContainer.IsGateIn == true )
                    {
                        var containerIndex = _containerIndexRepository.GetAll().Where(x => x.BLNo == cyContainer.BLNo
                                                                                 && x.ContainerNo == cyContainer.ContainerNo
                                                                                 && x.IndexNo == cyContainer.IndexNo
                                                                                 && x.ManifestedSealNumber == cyContainer.ManifestedSealNumber).LastOrDefault();
                        if (containerIndex != null)
                        {
                            containerIndex.ShippingAgentId = cyContainer.ShippingAgentId;
                            containerIndex.Size = cyContainer.Size;
                            containerIndex.IsGateIn = true;
                            _containerIndexRepository.Update(containerIndex);
                           
                        }
                        

                        _cyContainerRepository.Delete(cyContainer);
                        return new JsonResult(new { error = false, message = "Updated" });
                    }
                    

                }
                else
                {
                    return new JsonResult(new { error = true, message = "CY Container Not Fund" });

                }
            }
            else
            {
                return new JsonResult(new { error = true, message = "CY Container Not Fund" });

            }

            return new JsonResult(new { error = true, message = "CY Container Not Fund" });

        }


        public IActionResult ConvertCSFToCY(string Blnumber, string ContainerNumber)
        {
            if (Blnumber == null || ContainerNumber == null)
            {
                return new JsonResult(new { error = false, message = "Must Need ContainerNumber And Bl Number" });

            }
            var containerIndices = _containerIndexRepository.GetAll().Where(x => x.ContainerNo == ContainerNumber && x.BLNo == Blnumber).ToList();

            if (containerIndices != null)
            {
                foreach (var item in containerIndices)
                {
                    if(item.IsGateIn == true)
                    {
                        var cyContainers = _cyContainerRepository.GetAll().Where(x => x.BLNo == item.BLNo && x.ContainerNo == item.ContainerNo 
                                                                                && x.IndexNo == item.IndexNo).FirstOrDefault();
                        cyContainers.IsGateIn = true;
                        cyContainers.ShippingAgentId = item.ShippingAgentId;
                        _cyContainerRepository.Update(cyContainers);
                    }
                }

                _containerIndexRepository.DeleteRange(containerIndices);
            }

            //var cyContainers = _cyContainerRepository.GetAll().Where(x => x.BLNo == Blnumber && x.ContainerNo == ContainerNumber).ToList();

            //if (cyContainers != null)
            //{
            //    foreach (var item in cyContainers)
            //    {
            //        item.IsGateIn = true;
            //        _cyContainerRepository.Update(item);
            //    }

            //}

            return new JsonResult(new { error = false, message = "Updated" });
        }


        public IActionResult ContainerDetailView()
        {

            return View();
        }
             
    }
}