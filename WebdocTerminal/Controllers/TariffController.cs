using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Data;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Controllers
{
    [Authorize]
    public class TariffController : Controller
    {
        private IVesselRepository _vesselRepo;
        private IGateInRepository _gateinRepo;
        private ITariffRepository _tariffRepo;
        private IShippingAgentRepository _agentRepo;
        private IShippingAgentTariffRepository _agentTariffRepo;
        private IContainerIndexTariffRepository _indexTariffRepo;
        private ICYContainerRepository _cyContainerRepo;
        public IContainerIndexRepository _containerIndexRepo;
        public IStorageSlabRepository _storageSlabRepo;
        public ITarifHeadRepository _tarifHeadRepo;
        public IVesselExportRepository _vesselExportRepo;
        public IVoyageExportRepository _voyageExportRepo;
        public ICRLORepository _crloRepository;
        public IDisabledAgentTariffRepository _disabledAgentTariffRepository;
        public IDisabledAgentTariffCYRepository _disabledAgentTariffCYRepository;
        public IConsigneRepository _consigneRepo;
        public IClearingAgentRepository _clearingAgentRepo;
        public IGoodRepository _goodRepo;
        public IGoodsHeadRepository _goodsHeadRepository;
        public IShipperRepository _shipperRepo;
        public IISOCodeRepository _isocodeRepo;
        public IISOCodeHeadRepository _ISOCodeHeadRepository;
        public ITariffInformationRepository _tariffInformationRepo;
        public ITariffPercentageRepository _tariffPercentageRepo;
        public IPortAndTerminalRepository _portAndTerminalRepo;
        public IStorageFreeDayRepository  _storageFreeDayRepository;
        public ITariffInofrmationTariffRepository _tariffInofrmationTariffRepo;
        public IAICTAndLineIndexTariffRepository _aICTAndLineIndexTariffRepo;
        public ITerminalAndFFShareRateRepository _terminalAndFFShareRateRepo;
        public ITransportCollectionTariffRepository _transportCollectionTariffRepository;
        public ITransportCollectionRepository _transportCollectionRepository;
        public IPortChargeRepository _portChargeRepository;
        public IEmptyContainerTariffRepository _emptyContainerTariffRepository;
        public ApplicationDbContext _applicationdbcontext;
        public IEmptyContainerStorageSlabRepository _emptyContainerStorageSlabRepository;
        public IStorageFreeDaysForHolidayRepository _storageFreeDaysForHolidayRepository;
        public IInvoiceRepository _invoiceRepository;
        public IUnlockGenerateBillRemarkRepository _unlockGenerateBillRemarkRepository;
        public IStorageShareRateRepository _storageShareRateRepository;
        public ITariffPerBoxRateRepository _tariffPerBoxRateRepository;
         



        public TariffController(IVesselRepository vesselRepo, ICYContainerRepository cyContainerRepo, ITariffRepository tariffRepo, IShippingAgentRepository agentRepo,
            IShippingAgentTariffRepository agentTariffRepo, IContainerIndexTariffRepository indexTariffRepo,
            IContainerIndexRepository containerIndexRepo, IStorageSlabRepository storageSlabRepo, IGateInRepository gateinRepo,
            ITarifHeadRepository tarifHeadRepo, IVesselExportRepository vesselExportRepo, IVoyageExportRepository voyageExportRepo,
            ICRLORepository crloRepository , IDisabledAgentTariffRepository disabledAgentTariffRepository,
            IDisabledAgentTariffCYRepository disabledAgentTariffCYRepository,
            IConsigneRepository consigneRepo,
            IClearingAgentRepository clearingAgentRepo,
            IGoodRepository goodRepo,
            IShipperRepository shipperRepo,
            IISOCodeRepository isocodeRepo,
            IISOCodeHeadRepository ISOCodeHeadRepository,
            ITariffInformationRepository tariffInformationRepo,
            ITariffPercentageRepository tariffPercentageRepo,
            IPortAndTerminalRepository portAndTerminalRepo,
            IGoodsHeadRepository goodsHeadRepository,
            IStorageFreeDayRepository storageFreeDayRepository,
            ITariffInofrmationTariffRepository tariffInofrmationTariffRepo,
            IAICTAndLineIndexTariffRepository aICTAndLineIndexTariffRepo,
            ITerminalAndFFShareRateRepository terminalAndFFShareRateRepo,
            ITransportCollectionTariffRepository transportCollectionTariffRepository,
            ITransportCollectionRepository transportCollectionRepository,
            IPortChargeRepository portChargeRepository,
            IEmptyContainerTariffRepository emptyContainerTariffRepository,
            ApplicationDbContext applicationdbcontext,
            IEmptyContainerStorageSlabRepository emptyContainerStorageSlabRepository,
            IStorageFreeDaysForHolidayRepository storageFreeDaysForHolidayRepository,
            IInvoiceRepository invoiceRepository,
            IUnlockGenerateBillRemarkRepository unlockGenerateBillRemarkRepository,
            IStorageShareRateRepository storageShareRateRepository,
            ITariffPerBoxRateRepository tariffPerBoxRateRepository)
        {
            _cyContainerRepo = cyContainerRepo;
            _vesselRepo = vesselRepo;
            _gateinRepo = gateinRepo;
            _tariffRepo = tariffRepo;
            _agentRepo = agentRepo;
            _agentTariffRepo = agentTariffRepo;
            _indexTariffRepo = indexTariffRepo;
            _containerIndexRepo = containerIndexRepo;
            _storageSlabRepo = storageSlabRepo;
            _tarifHeadRepo = tarifHeadRepo;
            _vesselExportRepo = vesselExportRepo;
            _voyageExportRepo = voyageExportRepo;
            _crloRepository = crloRepository;
            _disabledAgentTariffRepository = disabledAgentTariffRepository;
            _disabledAgentTariffCYRepository = disabledAgentTariffCYRepository;
            _consigneRepo = consigneRepo;
            _clearingAgentRepo = clearingAgentRepo;
            _goodRepo = goodRepo;
            _goodsHeadRepository = goodsHeadRepository;
            _shipperRepo = shipperRepo;
            _isocodeRepo = isocodeRepo;
            _ISOCodeHeadRepository = ISOCodeHeadRepository;
            _tariffInformationRepo = tariffInformationRepo;
            _tariffPercentageRepo = tariffPercentageRepo;
            _portAndTerminalRepo = portAndTerminalRepo;
            _storageFreeDayRepository = storageFreeDayRepository;
            _tariffInofrmationTariffRepo = tariffInofrmationTariffRepo;
            _aICTAndLineIndexTariffRepo = aICTAndLineIndexTariffRepo;
            _terminalAndFFShareRateRepo = terminalAndFFShareRateRepo;
            _transportCollectionTariffRepository = transportCollectionTariffRepository;
            _transportCollectionRepository = transportCollectionRepository;
            _portChargeRepository = portChargeRepository;
            _emptyContainerTariffRepository = emptyContainerTariffRepository;
            _applicationdbcontext = applicationdbcontext;
            _emptyContainerStorageSlabRepository = emptyContainerStorageSlabRepository;
            _storageFreeDaysForHolidayRepository = storageFreeDaysForHolidayRepository;
            _invoiceRepository = invoiceRepository;
            _unlockGenerateBillRemarkRepository = unlockGenerateBillRemarkRepository;
            _storageShareRateRepository = storageShareRateRepository;
            _tariffPerBoxRateRepository = tariffPerBoxRateRepository;
        }

        public IActionResult TarifHead()
        {
            return View();
        }


        public JsonResult GetTariffHeads()
        {
            var res = _tarifHeadRepo.GetAll();

            return Json(res);
        }

        public IActionResult TariffCharges()
        {
            ViewData["TariffHead"] = _tarifHeadRepo.GetAll().Select(v => new SelectListItem { Text = v.Name, Value = v.TariffHeadId.ToString() });
            return View();
        }

        [HttpGet]
        public JsonResult GetTariffById(long id)
        {
            var data = _tariffRepo.GetFirst(x => x.TariffId == id);
            return Json(data);
        }
        public IActionResult AgentTariffInformation()
        {
            ViewData["ShippingAgents"] = _agentRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult IGMTariffInformation()
        {
            return View();
        }

        public IActionResult TariffAssigning()
        {
            ViewData["ShippingAgents"] = _agentRepo.GetAll()
             .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });


               ViewData["TariffHead"] = _tarifHeadRepo.GetAll().Select(v => new SelectListItem { Text = v.Name, Value = v.TariffHeadId.ToString() });

            //ViewData["Consignes"] = _consigneRepo.GetAll()
            //    .Select(v => new SelectListItem { Text = v.ConsigneName, Value = v.ConsigneId.ToString() });

            ViewData["ClearingAgents"] = _clearingAgentRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.Name, Value = v.ClearingAgentId.ToString() });

            ViewData["Goods"] = _goodsHeadRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });


            ViewData["Shippers"] = _shipperRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.ShipperName, Value = v.ShipperId.ToString() });


            ViewData["ISOCodes"]  = _ISOCodeHeadRepository.GetAll()
                .Select(s => new SelectListItem { Text = s.ISOCodeHeadDescription.ToString().Trim().ToUpper(), Value = s.ISOCodeHeadId.ToString() });


             
            //var isocodes = _isocodeRepo.GetAll()
            //    .Select(s => new SelectListItem { Text = s.Descrition.ToString().Trim().ToUpper(), Value = s.Descrition.Trim().ToUpper() }).Distinct().GroupBy(x => x.Text);


            //ViewData["ISOCodes"] = isocodes.Select(s => new SelectListItem { Text = s.Key.ToString().Trim().ToUpper(), Value = s.Key.Trim().ToUpper() });





            ViewData["AgentPercentTariff"]  = _tariffInformationRepo.GetShippingAgentFromPercentAgeTariff()
                .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });


            ViewData["PortAndTerminal"] = _portAndTerminalRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.PortName, Value = v.PortAndTerminalId.ToString() });


            var tariffs = _tariffRepo.GetList(t => t.TariffHead != null && t.TariffHead.Name.ToUpper().Contains("STORAGE"), t => t.TariffHead);
            ViewData["Tariffs"] = tariffs
             .Select(v => new SelectListItem { Text = v.TariffHead.Name + " Desc" + v.TariffHead.Description, Value = v.TariffId.ToString() });



            return View();
        }

        //public IActionResult GDTariff()
        //{

        //    ViewData["VesselExport"] = _vesselExportRepo.GetAll()
        //   .Select(v => new SelectListItem { Text = v.VesselName , Value = v.VesselExportId.ToString() });


        //    ViewData["VoyageExport"] = _voyageExportRepo.GetAll()
        //   .Select(v => new SelectListItem { Text = v.VoyageNumber , Value = v.VoyageExportId.ToString() });
        //    return View();
        //}

        //public IActionResult AddGDTariff(long EnteringCargoId)
        //{
        //    var containerIndexTariff = new ContainerIndexTariff
        //    {
        //        EnteringCargoId = EnteringCargoId
        //    };

        //    _indexTariffRepo.Add(containerIndexTariff);
        //    return Ok();
        //}


        //public IActionResult RemoveGDTariffCargo(long TariffId, long cargoId)
        //{
        //    var indexTariffs = _indexTariffRepo.GetList(t => t.TariffId == TariffId && t.EnteringCargoId == cargoId);

        //    if (indexTariffs != null)
        //        _indexTariffRepo.DeleteRange(indexTariffs);

        //    return new OkObjectResult(new { message = "Tariff Removed" });
        //}

        public JsonResult GetTariffByName(string name)
        {
            var data = _tariffRepo.GetFirst(x => x.TariffHead.Name.ToUpper() == name.ToUpper());
            return Json(data);
        }

        public IActionResult SaveTariffInfo(Tariff tariff)
        {
            try
            {
                var currentdate = DateTime.Now;
                //tariff.CBMRate = tariff.AICTCBMRate + tariff.FFCBMRate;
                //tariff.WeightRate = tariff.AICTWeightRate + tariff.FFWeightRate;
                //tariff.PerIndexRate = tariff.AICTPerIndexRate + tariff.FFPerIndexRate;
                //tariff.DevededIndexAmount = tariff.AICTDevededIndexAmount + tariff.FFDevededIndexAmount;
                //tariff.Rate20 = tariff.AICTRate20 + tariff.FFRate20;
                //tariff.Rate40 = tariff.AICTRate40+ tariff.FFRate40;
                //tariff.Rate45 = tariff.AICTRate45 + tariff.FFRate45;
                tariff.TariffDate = currentdate;
                _tariffRepo.Add(tariff);
            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }


            return new JsonResult(new { error = false   , message = "save" });


            //if (tariff.IsGeneral == true)
            //{
            //    var tariffInfo = new TariffInformation()
            //    {
            //        ClearingAgentId = null,
            //        ConsigneId = null,
            //        ContainerType = null,
            //        ShipperId = null,
            //        GoodId = null,
            //        ShippingAgentId = null,
            //        TariffType = "GeneralTariff",
            //        ContainerStatus = tariff.TariffType,
            //        TariffId = tariff.TariffId
            //    };

            //    _tariffInformationRepo.Add(tariffInfo);
            //}


        }

        public IActionResult SaveCustomerTariff(Tariff tariff, long ShippingAgentId)
        {

            var currentdate = DateTime.Now;

            tariff.TariffDate = currentdate;

            _tariffRepo.Add(tariff);

            var agentTariff = new ShippingAgentTariff
            {
                ShippingAgentId = ShippingAgentId,
                TariffId = tariff.TariffId
            };

            _agentTariffRepo.Add(agentTariff);

            return Ok();
        }

        public IActionResult SaveTariffHead(TariffHead tariffhead)
        {
            _tarifHeadRepo.Add(tariffhead);

            return Ok();
        }

        public IActionResult UpdateTariffInfo(Tariff tariff, long tariffid)
        {
            var data = _tariffRepo.GetFirst(x => x.TariffId == tariffid);
            data.Rate20 = tariff.Rate20;
            data.Rate40 = tariff.Rate40;
            data.Rate45 = tariff.Rate45;
            data.CBMRate = tariff.CBMRate;
            data.WeightRate = tariff.WeightRate;
            data.PerIndexRate = tariff.PerIndexRate;
            data.RoundCBMCode = tariff.RoundCBMCode;
            _tariffRepo.Update(data);
            return Ok();
        }

        public IActionResult SaveAgentTariff(ShippingAgentTariff tariff)
        {
            try
            {
                _agentTariffRepo.Add(tariff);

            }
            catch (Exception e)
            {

                throw;
            }

            return Ok();
        }

        public IActionResult updateAgentTariff(Tariff tariff)
        {
             
            try
            {
                //tariff.CBMRate = tariff.AICTCBMRate + tariff.FFCBMRate;
                //tariff.WeightRate = tariff.AICTWeightRate + tariff.FFWeightRate;
                //tariff.PerIndexRate = tariff.AICTPerIndexRate + tariff.FFPerIndexRate;
                //tariff.DevededIndexAmount = tariff.AICTDevededIndexAmount + tariff.FFDevededIndexAmount;
                //tariff.Rate20 = tariff.AICTRate20 + tariff.FFRate20;
                //tariff.Rate40 = tariff.AICTRate40 + tariff.FFRate40;
                //tariff.Rate45 = tariff.AICTRate45 + tariff.FFRate45;

                _tariffRepo.Update(tariff);
                return new JsonResult(new { error = false, message = "save" });

            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }

             
        }

        public IActionResult RemoveAgentTariff(long TariffId, long AgentId)
        {
            var agentTariff = _agentTariffRepo.GetFirst(t => t.TariffId == TariffId && t.ShippingAgentId == AgentId);

            if (agentTariff != null)
                _agentTariffRepo.Delete(agentTariff);

            return new OkObjectResult(new { message = "Tariff Removed" });
        }

        public IActionResult RemoveIGMTariff(long TariffId, long ContainerIndexId)
        {
            try
            {
                var indexTariff = _indexTariffRepo.GetFirst(t => t.TariffId == TariffId && t.ContainerIndexId == ContainerIndexId);

                if(indexTariff != null)
                {

                    _indexTariffRepo.Delete(indexTariff);
                    return new OkObjectResult(new { error = false, message = "Removed" });

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "tariff not found" });

                }


            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = "please try again" });
            }

       

            return new OkObjectResult(new { error = true, message = "please try again" });
        }

        public IActionResult RemoveIGMTariffBYId(long TariffId)
        {
            var tariff = _tariffRepo.GetFirst(t => t.TariffId == TariffId);

            if (tariff != null)
                _tariffRepo.Delete(tariff);

            return new OkObjectResult(new { message = "Tariff Removed" });
        }


        public IActionResult RemoveIGMTariffCYContainer(long TariffId, string igm ,  long IndexNo)
        {
            try
            {
                var cyContainer = _cyContainerRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == IndexNo);

                if(cyContainer != null)
                {
                    var indexTarif = _indexTariffRepo.GetAll().Where(t => t.TariffId == TariffId && t.CYContainerId == cyContainer.CYContainerId).ToList();

                    if(indexTarif  != null)
                    {
                        _indexTariffRepo.DeleteRange(indexTarif);

                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "Index Tariff Not Found" });
 
                    }

                }
                else
                { 
                    return new OkObjectResult(new { error = true, message = "Index Not Found" });
                }

 
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = "Please Try Again" });
                 

            }




            return new OkObjectResult(new { error = true, message = "please try again" });

        }



        public IActionResult SaveContainerIndexTariff(long containerIndexId, long tariffId, string igm, long? indexNo  , string type)
        {

            if(type == "CFS")
            {
                try
                {
                    if (containerIndexId > 0)
                    {


                        var tariff = new ContainerIndexTariff
                        {
                            ContainerIndexId = containerIndexId,
                            TariffId = tariffId
                        };

                        _indexTariffRepo.Add(tariff);

                        return new OkObjectResult(new { error = false, message = "save" });


                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "index not found" });

                    }
                }
                catch (Exception e)
                {

                    return new OkObjectResult(new { error = true, message = "pleae try again" });
                }
            }

            if (type == "CY")
            {
                try
                {
                    if (igm != null && indexNo != null && igm != "")
                    {
                        var cyContainer = _cyContainerRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == indexNo);

                        if (cyContainer != null)
                        {
                            var tariff = new ContainerIndexTariff
                            {
                                CYContainerId = cyContainer.CYContainerId,
                                TariffId = tariffId
                            };

                            _indexTariffRepo.Add(tariff);

                            return new OkObjectResult(new { error = false, message = "save" });

                        }
                        else
                        {
                            return new OkObjectResult(new { error = true, message = "Igm , Index not found" });

                        }

                    }

                    else
                    {
                        return new OkObjectResult(new { error = true, message = "igm , index not found" });

                    }
                }
                catch (Exception e)
                {

                    return new OkObjectResult(new { error = true, message = "Please Try again" });
                    
                }

            }



            return new OkObjectResult(new { error = true, message = "igm , index not found" });



        }


        //public IActionResult SaveGDTariff(long EnteringCargoId, long tariffId )
        //{

        //        var tariff = new ContainerIndexTariff
        //        {
        //            EnteringCargoId = EnteringCargoId,
        //            TariffId = tariffId
        //        };

        //        _indexTariffRepo.Add(tariff);



        //    return Ok();
        //}


        [HttpPost]
        public IActionResult UnlockGenerateBillCFS(int indexNumber , string igm )
        {
            var containerIndexs = _containerIndexRepo.GetContainerIndexBYigmIndex(indexNumber , igm);

            if (containerIndexs != null && containerIndexs.Count() > 0)
            {
 

                foreach (var item in containerIndexs)
                {
                    item.InvoiceLocked = false;
                    _containerIndexRepo.Update(item);
                }
            }

            return new JsonResult(new { error = false, message = "Un Locked Invoice Lock" });
             

        }


        public JsonResult checkCRLOMessage(int indexNumber, string igm)
        {

            var data = _crloRepository.GetFirst(x => x.IndexNumber == indexNumber && x.VIRNumber == igm);

            if (data != null)
            {
                return Json("True");
            }
             
            return Json("False");
        }




        [HttpPost]
        public IActionResult UnlockGenerateBillCY(string igm  , string containerNumber , int indexNo)
        {
            var containers = _cyContainerRepo.GetAll().Where(x=> x.VIRNo ==  igm && x.ContainerNo == containerNumber && x.IndexNo ==  indexNo).ToList();

            if(containers != null)
            {
                foreach (var item in containers)
                {
                    item.InvoiceLocked = false;
                    _cyContainerRepo.Update(item);
                }
            }
            

            return new OkObjectResult(new { message = "Un Locked Invoice Lock" });

        }


        [HttpPost]
        public IActionResult UpdateExtraFreeDays(long ContainerIndexId, long cyContainerId, int additionalfreedays)
        {
            if (ContainerIndexId > 0)
            {
                var containerIndex = _containerIndexRepo.GetFirst(x => x.ContainerIndexId == ContainerIndexId);
                containerIndex.AdditionalFreeDays = additionalfreedays;
                _containerIndexRepo.Update(containerIndex);
            }
            else
            {
                var cycontainer = _cyContainerRepo.GetFirst(x => x.CYContainerId == cyContainerId);
                cycontainer.AdditionalFreeDays = additionalfreedays;
                _cyContainerRepo.Update(cycontainer);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult AddStorageSlab(StorageSlab slab , long tariffid)
        {


            try
            {

                slab.Rate = slab.AICTRate + slab.FFRate;
                slab.Rate20 = slab.AICTRate20 + slab.FFRate20;
                slab.Rate40 = slab.AICTRate40 + slab.FFRate40;
                slab.Rate45 = slab.AICTRate45 + slab.FFRate45;
                slab.TariffId = tariffid;
                _storageSlabRepo.Add(slab);
                return new JsonResult(new { error = false, message = "save" });

            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }

             
        }

        public int GetExtraFreeDays(long tariffId)
        {
            var slabs = _storageSlabRepo.GetList(s => s.TariffId == tariffId);

            if (slabs.Count() > 0)
                return slabs.First().FreeDays;

            return 0;
        }

        [HttpPost]
        public IActionResult SaveExtraFreeDays(long freedays, long tariffId)
        {
            var slabs = _storageSlabRepo.GetList(s => s.TariffId == tariffId);

            foreach (var item in slabs)
            {
                item.FreeDays = Convert.ToInt32(freedays);
            }

            _storageSlabRepo.UpdateRange(slabs);

            return Ok();
        }

        public IActionResult StorageTariff()
        {
            var tariffs = _tariffRepo.GetList(t => t.TariffHead != null && t.TariffHead.Name.ToUpper().Contains("STORAGE"), t => t.TariffHead);
            ViewData["Tariffs"] = tariffs
             .Select(v => new SelectListItem { Text =  v.TariffHead.Name  + " Desc" + v.TariffHead.Description , Value = v.TariffId.ToString() });
            return View();
        }

        public IActionResult UnlockGenerateBill()
        {

            return View();
        }
        public IActionResult ExtraFreeDays()
        {
            return View();
        }

        public IActionResult OtherCharges()
        {
            ViewData["ShippingAgents"] = _agentRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            return View();
        }

        public IActionResult CustomerTariff()
        {
            ViewData["TariffHead"] = _tarifHeadRepo.GetAll()
           .Select(v => new SelectListItem { Text = v.Name, Value = v.TariffHeadId.ToString() });

            return View();
        }


        public IActionResult UnlockGenerateBillCFS(List<ContainerIndex> requ)
        {
            foreach (var item in requ)
            {

                var data = _containerIndexRepo.GetAll().Where(x => x.ContainerIndexId == item.ContainerIndexId).LastOrDefault();
                
                if(data != null)
                {

                    
                    data.InvoiceLocked = false;
                    _containerIndexRepo.Update(data);


                }

            }

            return new OkObjectResult(new { message = "Hold Invoice Locked" });
        }
          public IActionResult UnlockGenerateBillCY(List<CYContainer> requ)
        {
            foreach (var item in requ)
            {

                var data = _cyContainerRepo.GetAll().Where(x => x.CYContainerId == item.CYContainerId).LastOrDefault();
                
                if(data != null)
                {
                    data.InvoiceLocked = false;
                    _cyContainerRepo.Update(data);
                }

            }

            return new OkObjectResult(new { message = "Hold Invoice Locked" });
        }


        public IActionResult SaveDisabledAgentTariff(long containerIndexId , long agentTariffId)
        {

            var data = _disabledAgentTariffRepository.GetAll().Where(x => x.ContainerIndexId == containerIndexId && x.ShippingAgentTariffId == agentTariffId).FirstOrDefault();
          
            if(data != null)
            {
                return new JsonResult(new { error = true, message = "Already Removed !" });

            }
            var disabledAgentTariff = new DisabledAgentTariff
            {
                ShippingAgentTariffId = agentTariffId,
                ContainerIndexId = containerIndexId
            };
            _disabledAgentTariffRepository.Add(disabledAgentTariff);
             
            return new JsonResult(new { error = false, message = "Agent Tariff Remove For This Index" });
             
        }
        
        public IActionResult SaveDisabledAgentTariffCY(string igm , string indexNo, long agentTariffId)
        {
            if (igm != null && indexNo != null)
            {

                var cyContainers = _cyContainerRepo.GetList(c => c.VIRNo == igm && c.IndexNo == Convert.ToInt64(indexNo));

                if (cyContainers.Count() > 0)
                {
                    bool result = _disabledAgentTariffCYRepository.GetAll().Any(x => cyContainers.Any(y => y.CYContainerId == x.CYContainerId && x.ShippingAgentTariffId == agentTariffId));
                    if (result)
                    {
                        return new JsonResult(new { error = true, message = "Already Removed !" });

                    }
                    else
                    {
                        var DisabledAgentTariffCYs = new List<DisabledAgentTariffCY>();

                        foreach (var container in cyContainers)
                        {
                            DisabledAgentTariffCYs.Add(new DisabledAgentTariffCY
                            {
                                ShippingAgentTariffId = agentTariffId,
                                CYContainerId = container.CYContainerId
                            });
                        }

                        _disabledAgentTariffCYRepository.AddRange(DisabledAgentTariffCYs);
                        return new JsonResult(new { error = false, message = "Agent Tariff Remove For This Index" });

                    }



                }

                return new JsonResult(new { error = true, message = "Container Not Found" });

            }




            return new JsonResult(new { error = true, message = "IGM Index Not Found" });


             
        }
        
        public IActionResult IndexRemoveDiabledTariff(long containerIndexId)
        {

            var data = _disabledAgentTariffRepository.GetAll().Where(x => x.ContainerIndexId == containerIndexId).ToList();
          
            if(data.Count() > 0)
            {
                foreach (var item in data)
                {
                 
                    _disabledAgentTariffRepository.Delete(item);
                }
            }
            else
            {
                return new JsonResult(new { error = false, message = " AlReady Updated !" });

            }

            return new JsonResult(new { error = false, message = " Your Index Tariff Refersh ." });
             
        }

        public IActionResult IndexRemoveDiabledTariffCY(string igm, string indexNo )
        {
            if (igm != null && indexNo != null)
            {

                var cyContainers = _cyContainerRepo.GetList(c => c.VIRNo == igm && c.IndexNo == Convert.ToInt64(indexNo));

                if (cyContainers.Count() > 0)
                {

                    foreach (var item in cyContainers)
                    {
                        var data = _disabledAgentTariffCYRepository.GetAll().Where(x => x.CYContainerId == item.CYContainerId).ToList();

                        _disabledAgentTariffCYRepository.DeleteRange(data);


                    }


                    return new JsonResult(new { error = false, message = " Your Index Tariff Refersh ." });






                }

                return new JsonResult(new { error = true, message = "Container Not Found" });

            }




            return new JsonResult(new { error = true, message = "IGM Index Not Found" });



        }


        public IActionResult SaveTariffCondition(Tariff model , TariffInformation tariff)
        {
            try
            {
                try
                {

                    var currentdate = DateTime.Now;
                    model.TariffDate = currentdate;


                    if (model.TariffHeadId != null)
                    {

                        if(model.IsFixedRate == true)
                        {
                             if(model.IsGeneral == true)
                            {
                                return new JsonResult(new { error = true, message = "For Fixed Rate Type General Type Not allow" });
                            }
                            if (tariff.IndexCargoType != "LCL")
                            {
                                return new JsonResult(new { error = true, message = "Fixed Rate Tariff Is Only For CFS (LCL)" });
                            }
                        }

                        var tarif = _tariffRepo.GetAll(x=> x.TariffHead).Where(x => x.TariffHeadId == model.TariffHeadId).LastOrDefault();

                        if (tarif != null)
                        {
                            if (tarif.TariffHead !=  null)
                            {
                                if (tarif.TariffHead.Name.Contains("STORAGE"))
                                {
                                    model.TariffId = tarif.TariffId;
                                }
                                else
                                {
                                     
                                    _tariffRepo.Add(model);

                                }
                            }
                            else
                            {

                                 
                                _tariffRepo.Add(model);

                            }

                        }
                        else
                        {

                            
                            _tariffRepo.Add(model);

                        }

                    }
                    else
                    {
                        return new JsonResult(new { error = true, message = "Please Select Tariff Head" });

                    }


                }
                catch (Exception e)
                {

                    return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });

                }





                var res = _tariffInformationRepo.GetAll().Where(x => x.ShippingAgentId == tariff.ShippingAgentId && x.ConsigneId == tariff.ConsigneId && x.ClearingAgentId == tariff.ClearingAgentId
                                                                && x.GoodsHeadId == tariff.GoodsHeadId && x.ShipperId == tariff.ShipperId && x.ISOCodeHeadId == tariff.ISOCodeHeadId
                                                                && x.IndexCargoType == tariff.IndexCargoType && x.PortAndTerminalId == tariff.PortAndTerminalId).FirstOrDefault();
                if(res != null)
                {
                    var tariffinformationtariff = new TariffInofrmationTariff
                    {
                        TariffInformationId = res.TariffInformationId,
                        TariffId = model.TariffId

                    };

                    if(res.PercentAgeShippingAgentId == 0 && tariff.PercentAgeShippingAgentId != 0)
                    {
                        res.PercentAgeShippingAgentId = tariff.PercentAgeShippingAgentId;
                        _tariffInformationRepo.Update(res);

                    }

                    _tariffInofrmationTariffRepo.Add(tariffinformationtariff);
                }
                else
                {
                    _tariffInformationRepo.Add(tariff);

                    var tariffinformationtariff = new TariffInofrmationTariff
                    {
                        TariffInformationId = tariff.TariffInformationId,
                        TariffId = model.TariffId

                    };
                    _tariffInofrmationTariffRepo.Add(tariffinformationtariff);
                }


               


                return new JsonResult(new { error = false, message = "Save " });

            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = "Please Create Again" });

                throw;
            }

         }


        public IActionResult DeleteSaveTariffCondition(long TariffId)
        {

            var tariff = _tariffInofrmationTariffRepo.GetAll().Where(x=> x.TariffInofrmationTariffId == TariffId).LastOrDefault();

            try
            {
                if (tariff != null)
                {
                    _tariffInofrmationTariffRepo.Delete(tariff);

                    return new JsonResult(new { error = false, message = "delete" });

                }
                else
                {
                    return new JsonResult(new { error = true, message = "Record Not Found" });

                }
            }
            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });


            }

            //var tariff = _tariffRepo.Find(TariffId);

            //try
            //{
            //    if(tariff != null)
            //    {
            //        _tariffRepo.Delete(tariff);

            //        return new JsonResult(new { error = false, message = "delete" });

            //    }
            //    else
            //    {
            //        return new JsonResult(new { error = true, message = "Record Not Found" });

            //    }
            //}
            //catch (Exception e)
            //{

            //    return new JsonResult(new { error = true, message =  e.InnerException.InnerException.Message });


            //}

        }


        public IActionResult SaveTariffConditionByTariffCode(long createdCode, TariffInformation tariff)
        {
            try
            {

                var tariffdata = new Tariff();

                var tariffres = _tariffInformationRepo.GetAll().Where(x => x.TariffInformationId == createdCode).LastOrDefault();
                var tariffinfotariffres = _tariffInofrmationTariffRepo.GetAll().Where(x => x.TariffInformationId == createdCode).ToList();

                if(tariffres != null && tariffinfotariffres.Count() > 0)
                {


                    foreach (var item in tariffinfotariffres)
                    {

                        var tariffitem = _tariffRepo.GetAll(x => x.TariffHead).Where(x => x.TariffId == item.TariffId).LastOrDefault();

                        if(tariffitem != null)
                        {
                            //var tarif = _tariffRepo.GetAll(x => x.TariffHead).Where(x => x.TariffHeadId == tariffitem.TariffHeadId).LastOrDefault();

                            //if (tarif != null)
                            //{
                               
                                    if (tariffitem.TariffHead.Name.Contains("STORAGE"))
                                    {
                                        tariffdata.TariffId = tariffitem.TariffId;
                                    }
                                    else
                                    {
                                        tariffdata.TariffDate = tariffitem.TariffDate;
                                        tariffdata.ImplementFrom = tariffitem.ImplementFrom;
                                        tariffdata.Rate20 = tariffitem.Rate20;
                                        tariffdata.Rate40 = tariffitem.Rate40;
                                        tariffdata.Rate45 = tariffitem.Rate45;                                    
                                        tariffdata.CBMRate = tariffitem.CBMRate;
                                        tariffdata.WeightRate = tariffitem.WeightRate;
                                        tariffdata.PerIndexRate = tariffitem.PerIndexRate;
                                        tariffdata.RoundCBMCode = tariffitem.RoundCBMCode;
                                        tariffdata.DailyCharges = tariffitem.DailyCharges;
                                        tariffdata.IsActive = tariffitem.IsActive;
                                        tariffdata.TariffHeadId = tariffitem.TariffHeadId;
                                        tariffdata.IsCBMorRate = tariffitem.IsCBMorRate;
                                        tariffdata.ImplementTo = tariffitem.ImplementTo;
                                        tariffdata.IsGeneral = tariffitem.IsGeneral;
                                        tariffdata.DevededIndexAmount = tariffitem.DevededIndexAmount;
                                        tariffdata.IsDollerRate = tariffitem.IsDollerRate;
                                        tariffdata.PortAndTerminalId = tariffitem.PortAndTerminalId;
                                        tariffdata.TypeOfTarifff = tariffitem.TypeOfTarifff;
                                        tariffdata.IsFixedRate = tariffitem.IsFixedRate;
                                        tariffdata.TypeOfImplementation = tariffitem.TypeOfImplementation;


                                        _tariffRepo.Add(tariffdata);

                                    }
                           

                            //}
                   
                        }
                         

                        var res = _tariffInformationRepo.GetAll().Where(x => x.ShippingAgentId == tariff.ShippingAgentId && x.ConsigneId == tariff.ConsigneId && x.ClearingAgentId == tariff.ClearingAgentId
                                                      && x.GoodsHeadId == tariff.GoodsHeadId && x.ShipperId == tariff.ShipperId && x.ISOCodeHeadId == tariff.ISOCodeHeadId
                                                      && x.IndexCargoType == tariff.IndexCargoType && x.PortAndTerminalId == tariff.PortAndTerminalId).FirstOrDefault();

                        if (res != null)
                        {
                            var tariffinformationtariff = new TariffInofrmationTariff
                            {
                                TariffInformationId = res.TariffInformationId,
                                TariffId = tariffdata.TariffId

                            };

                            //if (res.PercentAgeShippingAgentId == 0 && tariff.PercentAgeShippingAgentId != 0)
                            //{
                            //    res.PercentAgeShippingAgentId = tariff.PercentAgeShippingAgentId;
                            //    _tariffInformationRepo.Update(res);

                            //}

                            _tariffInofrmationTariffRepo.Add(tariffinformationtariff);
                        }
                        else
                        {
                            _tariffInformationRepo.Add(tariff);

                            var tariffinformationtariff = new TariffInofrmationTariff
                            {
                                TariffInformationId = tariff.TariffInformationId,
                                TariffId = tariffdata.TariffId

                            };
                            _tariffInofrmationTariffRepo.Add(tariffinformationtariff);
                        }


                        tariffdata = new Tariff();


                    }



                    return new JsonResult(new { error = false, message = "Save " });
                }
                else
                {
                    return new JsonResult(new { error = true, message = "tariff code is not valid" });
                }
                  
            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = "Please Create Again" });

                throw;
            }

        }

        public IActionResult TariffPersentView()
        {

            ViewData["ShippingAgents"] = _agentRepo.GetAll()
             .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });


            return View();

        }


        public IActionResult SaveTariffPersent( long ShippingAgentId,  List<TariffPercentage> tariff)
        {
            try
            {

                var tariffpersen = _tariffPercentageRepo.GetAll().Where(x => x.ShippingAgentId == ShippingAgentId).ToList();
                if(tariffpersen.Count() > 0)
                {
                    _tariffPercentageRepo.DeleteRange(tariffpersen);
                }
                if (tariff.Count() > 0)
                {

                    foreach (var item in tariff)
                    {
                        item.TariffPercentageId = 0;
                        item.ShippingAgentId = ShippingAgentId;
                    }

                }


                _tariffPercentageRepo.AddRange(tariff);
                return new JsonResult(new { error = false, message = "Save " });



            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = "Please Create Again" });

                throw;
            }

        }


        public IActionResult UpdateTariffPersent(TariffPercentage model)
        {
            try
            {
                _tariffPercentageRepo.Update(model);

                return new JsonResult(new { error = false, message = "Save " });

            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = e.InnerException.InnerException });

                throw;
            }
        }




       public JsonResult GetTariffHeadsTypeWise()
        {
            var res = _tarifHeadRepo.GetAll().Where(x=> x.TariffHeadType == "TariffHead").ToList();

            return Json(res);
        }


        //public IActionResult AddStorageDays( StorageFreeDay model )
        //{

        //    try
        //    {
        //        var data = _storageFreeDayRepository.GetAll().Where(x=> x.ShippingAgentId == model.ShippingAgentId && x.ShipperId == model.ShipperId 
        //        && x.PortAndTerminalId == model.PortAndTerminalId && x.GoodsHeadId == model.GoodsHeadId && x.ClearingAgentId == model.ClearingAgentId 
        //        && x.ISOCodeHeadId == model.ISOCodeHeadId && x.ConsigneId == model.ConsigneId && x.IndexCargoType == model.IndexCargoType).FirstOrDefault();

        //        if(data != null)
        //        {

        //            data.StorageFreeDays = model.StorageFreeDays;

        //            _storageFreeDayRepository.Update(data);

        //        }
        //        else
        //        {

        //            _storageFreeDayRepository.Add(model);
        //        }


        //        return new JsonResult(new { error = false, message = "Save " });

        //    }
        //    catch (Exception e)
        //    {
        //        return new JsonResult(new { error = true, message = "Please Create Again" });

        //        throw;
        //    }

        //}
        public IActionResult AddStorageDays(long tariffid  , long? storageDays)
        {
            try
            {
                var res = _storageFreeDayRepository.GetAll().Where(x => x.TariffId == tariffid).LastOrDefault();

                if (res != null)
                {
                    res.StorageFreeDays = storageDays;

                    _storageFreeDayRepository.Update(res);
                }
                else
                {
                    var resdata = new StorageFreeDay()
                    {
                        TariffId = tariffid,
                        StorageFreeDays = storageDays
                    };
                    _storageFreeDayRepository.Add(resdata);
                }

                return new JsonResult(new { error = false, message = "Save" });
            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = e.InnerException.InnerException });

                  throw;
            }
           
        }

        public IActionResult AddTariffStorageDays(long tariffid, long? storageDays , long? dgfreeDays)
        {
            try
            {
                var res = _tariffInformationRepo.GetAll().Where(x => x.TariffInformationId == tariffid).LastOrDefault();

                if (res != null )
                {
                    //if(storageDays > 0)
                    //{

                        res.StorageFreeDays = storageDays;
                        res.DGFreeDays = dgfreeDays;

                        _tariffInformationRepo.Update(res);
                        return new JsonResult(new { error = false, message = "Updated" });
                    //}
                    //else
                    //{
                    //    return new JsonResult(new { error = true, message = "storageDays must be > 0" });
                    //}

                    
                }
                else
                {
                    return new JsonResult(new { error = true, message = "no tariff code find" });
                }

                //return new JsonResult(new { error = false, message = "Save" });
            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = e.InnerException.InnerException });

                throw;
            }

        }



        public IActionResult UpdateContaineSizeAmount(string igm  , int indexno  , double amountSize20   , double amountSize40   , double amountSize45)
        {
            try
            {

                var data = _cyContainerRepo.GetAll().Where(x => x.VIRNo == igm && x.IndexNo == indexno).ToList();

                if(data.Count() > 0)
                {
                    foreach (var item in data)
                    {
                        item.AmountSize20 = amountSize20;
                        item.AmountSize40 = amountSize40;
                        item.AmountSize45 = amountSize45;
                        _cyContainerRepo.Update(item);
                    }
                    return new JsonResult(new { error = false, message = "Updated" });

                }
                else
                {
                    return new JsonResult(new { error = true, message = "igm index  not found" });
                }



            }
            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = "Please try Again" });

                throw;
            }
        }



        public IActionResult AICTAndLineIndexTariffView()
        {
            ViewData["ShippingAgents"] = _agentRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });


            ViewData["GoodsHead"] = _goodsHeadRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });
             
            return View();

        }

        public IActionResult SetIndexTariffAmount(AICTAndLineIndexTariffViewModel model)
        {
            try
            {

               // var containerindex = _containerIndexRepo.GetAll().Where(x => x.ContainerIndexId == model.ContainerIndexId).LastOrDefault();
                  
                var containerindex = _containerIndexRepo.GetContainerIndexById(model.ContainerIndexId);
                 
                if (containerindex != null)
                {
                   if(containerindex.IsDelivered == true)
                    {
                        return new JsonResult(new { error = true, message = "Index Delivered" });
                    }

                    var tempUpdateQuery = string.Format(@"
                         UPDATE ContainerIndex SET FFCBM = '"+ model.HigherCBM + "'  WHERE  ContainerIndexId  = '" + model.ContainerIndexId + "'  ");
                    _containerIndexRepo.ExecuteNonSQL(tempUpdateQuery);
                     
                  //  _containerIndexRepo.Update(containerindex);

                    // if(model.AdditionalChargeAICTPerCBMRate > 0 || model.AdditionalChargeAICTPerIndexRate > 0 || model.AdditionalChargeFFPerIndexRate > 0 || model.AdditionalChargeFFPerIndexRate> 0 )
                    //{
                        var indetariff = _aICTAndLineIndexTariffRepo.GetAll().Where(x => x.VirNumber == model.IGMNo && x.IndexNumber == model.IndexNo).LastOrDefault();
                        if(indetariff != null)
                        {
                            indetariff.AICTPerCBMRate = model.AdditionalChargeAICTPerCBMRate;
                            indetariff.AICTPerIndexRate = model.AdditionalChargeAICTPerIndexRate;
                            indetariff.FFPerCBMRate = model.AdditionalChargeFFPerCBMRate;
                            indetariff.FFPerIndexRate = model.AdditionalChargeFFPerIndexRate;
                            indetariff.TotalCBMRate = model.AdditionalChargeAICTPerCBMRate + model.AdditionalChargeFFPerCBMRate;
                            indetariff.TotalPerIndexRate = model.AdditionalChargeAICTPerIndexRate + model.AdditionalChargeFFPerIndexRate;
                            _aICTAndLineIndexTariffRepo.Update(indetariff);
                            return new JsonResult(new { error = false, message = "Update" });

                        }
                        else
                        {
                            var indextariff = new AICTAndLineIndexTariff
                            {
                                VirNumber = model.IGMNo,
                                IndexNumber = model.IndexNo ??  0,
                                AICTPerCBMRate = model.AdditionalChargeAICTPerCBMRate,
                                AICTPerIndexRate = model.AdditionalChargeAICTPerIndexRate,
                                FFPerCBMRate = model.AdditionalChargeFFPerCBMRate,
                                FFPerIndexRate = model.AdditionalChargeFFPerIndexRate,
                                TotalCBMRate = model.AdditionalChargeAICTPerCBMRate + model.AdditionalChargeFFPerCBMRate,
                                TotalPerIndexRate = model.AdditionalChargeAICTPerIndexRate + model.AdditionalChargeFFPerIndexRate,

                            };
                            _aICTAndLineIndexTariffRepo.Add(indextariff);
                            return new JsonResult(new { error = false, message = "Update" });

                        }
                   // }
                     
                        return new JsonResult(new { error = false, message = "Update CBM" });
                     

                }
                else
                {

                    return new JsonResult(new { error = true, message = "Index Not Found" });
                }


            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }


        public IActionResult UpdateTariffAmountFromExcelSheet(List<AICTAndLineIndexTariffViewModel> model)
        {
            try
            {
                var count = 0;
                var countDelivered = 0;
                foreach (var item in model)
                {


                    var container = _containerIndexRepo.GetContainerIndexByIGMIndexAndContainer(item.IGMNo, item.ContainerNumber, Convert.ToInt64(item.IndexNo));
                    var indexexs = _containerIndexRepo.GetSingleIndexInfo(item.IGMNo, Convert.ToInt64(item.IndexNo));
                    if (container != null && indexexs != null)
                    {

                        var invoice = _invoiceRepository.GetAll().Where(x => x.ContainerIndexId == indexexs.ContainerIndexId).LastOrDefault();

                        if (invoice == null)
                        {
                            //this section is comment for per atif bhi Dear Owais , As discus kindly hold updation of CBM from Index index page.
                            //var tempUpdateQuery = string.Format(@"UPDATE ContainerIndex SET FFCBM = '" + item.HigherCBM + "'  WHERE  ContainerIndexId  = '" + container.ContainerIndexId + "'  ");
                            //_containerIndexRepo.ExecuteNonSQL(tempUpdateQuery);
                            //this section is comment for per atif bhi Dear Owais , As discus kindly hold updation of CBM from Index index page.


                            //var indetariff = _aICTAndLineIndexTariffRepo.GetAll().Where(x => x.VirNumber == item.IGMNo && x.IndexNumber == item.IndexNo).LastOrDefault();
                            //if (indetariff != null)
                            //{
                            //    indetariff.AICTPerCBMRate = item.AdditionalChargeAICTPerCBMRate;
                            //    indetariff.AICTPerIndexRate = item.AdditionalChargeAICTPerIndexRate;
                            //    indetariff.FFPerCBMRate = item.AdditionalChargeFFPerCBMRate;
                            //    indetariff.FFPerIndexRate = item.AdditionalChargeFFPerIndexRate;
                            //    indetariff.TotalCBMRate = item.AdditionalChargeAICTPerCBMRate + item.AdditionalChargeFFPerCBMRate;
                            //    indetariff.TotalPerIndexRate = item.AdditionalChargeAICTPerIndexRate + item.AdditionalChargeFFPerIndexRate;
                            //    _aICTAndLineIndexTariffRepo.Update(indetariff);
                            //}
                            //else
                            //{
                            var indextariff = new AICTAndLineIndexTariff
                            {
                                VirNumber = item.IGMNo,
                                IndexNumber = item.IndexNo ?? 0,

                                PerBoxRate = item.PerBoxRate,
                                AICTPerCBMRate = item.AdditionalChargeAICTPerCBMRate,
                                AICTPerIndexRate = item.AdditionalChargeAICTPerIndexRate,
                                FFPerCBMRate = item.AdditionalChargeFFPerCBMRate,
                                FFPerIndexRate = item.AdditionalChargeFFPerIndexRate,


                                FFPerCBMRateShareRate = item.FFPerCBMRate,
                                FFPerIndexRateShareRate = item.FFPerIndexRate,
                                AICTPerCBMRateShareRate = item.AICTPerCBMRate,
                                AICTPerIndexRateShareRate = item.AICTPerIndexRate,


                                TotalCBMRate = item.AdditionalChargeAICTPerCBMRate + item.AdditionalChargeFFPerCBMRate + item.FFPerCBMRate + item.AICTPerCBMRate,
                                TotalPerIndexRate = item.AdditionalChargeAICTPerIndexRate + item.AdditionalChargeFFPerIndexRate + item.FFPerIndexRate + item.AICTPerIndexRate + item.PerBoxRate,

                                Status = container.IsDelivered == true ? "Deliverd" : "UnDelivered",
                                CreatedDate = DateTime.Now,
                                ContainerNumber = item.ContainerNumber

                                //TotalCBMRate = item.AdditionalChargeAICTPerCBMRate + item.AdditionalChargeFFPerCBMRate,
                                //TotalPerIndexRate = item.AdditionalChargeAICTPerIndexRate + item.AdditionalChargeFFPerIndexRate,

                            };

                            if (indextariff.TotalCBMRate > 0 || indextariff.TotalPerIndexRate > 0)
                            {
                                _aICTAndLineIndexTariffRepo.Add(indextariff);
                            }


                            //}


                             
                                
                            
                             
                                count += 1;
                            
                        }
                        else
                        {
                            countDelivered += 1;
                        }
                      


                    }
                     


                    
                }

                return new JsonResult(new { error = false, message = "Total " + count + "Index Info Updated  and total " + countDelivered + " Are Deliverd" });

            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }





        public IActionResult SaveAictLineIndexAmount(List<AICTAndLineIndexTariffViewModel> model)
        {
            try
            {
                var count = 0;
                var countDelivered = 0;
                foreach (var item in model)
                {


                    var container = _containerIndexRepo.GetContainerIndexByIGMIndexAndContainer(item.IGMNo, item.ContainerNumber, Convert.ToInt64(item.IndexNo));
                    var indexexs = _containerIndexRepo.GetSingleIndexInfo(item.IGMNo, Convert.ToInt64(item.IndexNo));

                    if (container != null && indexexs != null)
                    {


                        var invoice = _invoiceRepository.GetAll().Where(x=> x.ContainerIndexId == indexexs.ContainerIndexId).LastOrDefault();

                        if(invoice == null)
                        {
                            //this section is comment for per atif bhi Dear Owais , As discus kindly hold updation of CBM from Index index page.
                            //var tempUpdateQuery = string.Format(@"UPDATE ContainerIndex SET FFCBM = '" + item.HigherCBM + "'  WHERE  ContainerIndexId  = '" + container.ContainerIndexId + "'  ");
                            //_containerIndexRepo.ExecuteNonSQL(tempUpdateQuery);
                            //this section is comment for per atif bhi Dear Owais , As discus kindly hold updation of CBM from Index index page.


                            //var indetariff = _aICTAndLineIndexTariffRepo.GetAll().Where(x => x.VirNumber == item.IGMNo && x.IndexNumber == item.IndexNo).LastOrDefault();
                            //if (indetariff != null)
                            //{
                            //    indetariff.AICTPerCBMRate = item.AdditionalChargeAICTPerCBMRate;
                            //    indetariff.AICTPerIndexRate = item.AdditionalChargeAICTPerIndexRate;
                            //    indetariff.FFPerCBMRate = item.AdditionalChargeFFPerCBMRate;
                            //    indetariff.FFPerIndexRate = item.AdditionalChargeFFPerIndexRate;
                            //    indetariff.TotalCBMRate = item.AdditionalChargeAICTPerCBMRate + item.AdditionalChargeFFPerCBMRate;
                            //    indetariff.TotalPerIndexRate = item.AdditionalChargeAICTPerIndexRate + item.AdditionalChargeFFPerIndexRate;
                            //    _aICTAndLineIndexTariffRepo.Update(indetariff);
                            //}
                            //else
                            //{
                            var indextariff = new AICTAndLineIndexTariff
                            {
                                VirNumber = item.IGMNo,
                                IndexNumber = item.IndexNo ?? 0,

                                AICTPerCBMRate = item.AdditionalChargeAICTPerCBMRate,
                                AICTPerIndexRate = item.AdditionalChargeAICTPerIndexRate,
                                FFPerCBMRate = item.AdditionalChargeFFPerCBMRate,
                                FFPerIndexRate = item.AdditionalChargeFFPerIndexRate,

                                FFPerCBMRateShareRate = item.FFPerCBMRate,
                                FFPerIndexRateShareRate = item.FFPerIndexRate,
                                AICTPerCBMRateShareRate = item.AICTPerCBMRate,
                                AICTPerIndexRateShareRate = item.AICTPerIndexRate,
                                PerBoxRate = item.PerBoxRate,

                                TotalCBMRate = item.AdditionalChargeAICTPerCBMRate + item.AdditionalChargeFFPerCBMRate + item.FFPerCBMRate + item.AICTPerCBMRate,
                                TotalPerIndexRate = item.AdditionalChargeAICTPerIndexRate + item.AdditionalChargeFFPerIndexRate + item.FFPerIndexRate + item.AICTPerIndexRate + item.PerBoxRate,
                                Status = container.IsDelivered == true ? "Deliverd" : "UnDelivered",
                                CreatedDate = DateTime.Now,
                                ContainerNumber = item.ContainerNumber

                            };

                            if (indextariff.TotalCBMRate > 0 || indextariff.TotalPerIndexRate > 0)
                            {
                                _aICTAndLineIndexTariffRepo.Add(indextariff);
                            }

                            //}
                             
                                count += 1;
                            
                        }
                        else
                        {
                            countDelivered += 1;
                        }

                       

                    }




                }
                return new JsonResult(new { error = false, message = "Total " + count + "Index Info Updated  and total " + countDelivered + " Are Deliverd" });


            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }



        public IActionResult SaveShareCondition( TerminalAndFFShareRate tariff)
        {
            try
            {
                 
                //var res = _terminalAndFFShareRateRepo.GetAll().Where(x => x.ShippingAgentId == tariff.ShippingAgentId && x.ConsigneId == tariff.ConsigneId && x.ClearingAgentId == tariff.ClearingAgentId
                //                                                && x.GoodsHeadId == tariff.GoodsHeadId && x.ShipperId == tariff.ShipperId && x.ISOCodeHeadId == tariff.ISOCodeHeadId
                //                                                && x.IndexCargoType == tariff.IndexCargoType && x.PortAndTerminalId == tariff.PortAndTerminalId).FirstOrDefault();
                //if (res != null)
                //{
                //    res.AICTRate20 = tariff.AICTRate20;
                //    res.AICTRate40 = tariff.AICTRate40;
                //    res.AICTRate45 = tariff.AICTRate45;
                //    res.AICTCBMRate = tariff.AICTCBMRate;
                //    res.AICTPerIndexRate = tariff.AICTPerIndexRate;

                //    res.FFRate20 = tariff.FFRate20;
                //    res.FFRate40 = tariff.FFRate40;
                //    res.FFRate45 = tariff.FFRate45;
                //    res.FFCBMRate = tariff.FFCBMRate;
                //    res.FFPerIndexRate = tariff.FFPerIndexRate;

                //    res.TotalAICTCBMRate = tariff.AICTCBMRate + tariff.FFCBMRate;
                //    res.TotalAICTPerIndexRate = tariff.AICTPerIndexRate + tariff.FFPerIndexRate;
                //    res.TotalAICTRate20 = tariff.AICTRate20 + tariff.FFRate20;
                //    res.TotalAICTRate40 = tariff.AICTRate40 + tariff.FFRate40;
                //    res.TotalAICTRate45 = tariff.AICTRate45 + tariff.FFRate45;

                //    _terminalAndFFShareRateRepo.Update(res);

                //}
                //else
                //{

                    tariff.TotalAICTCBMRate = tariff.AICTCBMRate + tariff.FFCBMRate;
                    tariff.TotalAICTPerIndexRate = tariff.AICTPerIndexRate + tariff.FFPerIndexRate;
                    tariff.TotalAICTRate20 = tariff.AICTRate20 + tariff.FFRate20;
                    tariff.TotalAICTRate40 = tariff.AICTRate40 + tariff.FFRate40;
                    tariff.TotalAICTRate45 = tariff.AICTRate45 + tariff.FFRate45;
  
                   _terminalAndFFShareRateRepo.Add(tariff);
                //} 
                return new JsonResult(new { error = false, message = "Save " });

            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = "Please Create Again" });

                throw;
            }

        }


        public IActionResult updateStorageSlab(StorageSlab res)
        {

            try
            {
                var result = _storageSlabRepo.GetAll().Where(x => x.StorageSlabId == res.StorageSlabId).LastOrDefault(); 
                if(result != null)
                {
                    result.Rate = res.AICTRate + res.FFRate;
                    result.Rate20 = res.AICTRate20 + res.FFRate20;
                    result.Rate40 = res.AICTRate40 + res.FFRate40;
                    result.Rate45 = res.AICTRate45 + res.FFRate45;
                    result.Description = res.Description;
                    result.AICTRate20 = res.AICTRate20;
                    result.AICTRate40 = res.AICTRate40;
                    result.AICTRate45 = res.AICTRate45;
                    result.FFRate20 = res.FFRate20;
                    result.FFRate40 = res.FFRate40;
                    result.FFRate45 = res.FFRate45;
                    result.AICTRate = res.AICTRate;
                    result.FFRate = res.FFRate;
                    result.NoOfDays = res.NoOfDays;

                    _storageSlabRepo.Update(result);
                    return new JsonResult(new { error = false, message = "save" });

                }
                else
                {
                    return new JsonResult(new { error = true, message = "result not found" });

                } 


            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }


        }

        public IActionResult DeleteupdateStorageSlab(long StorageSlabId)
        {

            try
            {
                var result = _storageSlabRepo.GetAll().Where(x => x.StorageSlabId == StorageSlabId).LastOrDefault();
                if (result != null)
                {
                    _storageSlabRepo.Delete(result);
                    return new JsonResult(new { error = false, message = "Delete" });

                }
                else
                {
                    return new JsonResult(new { error = true, message = "result not found" });

                }


            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }


        }
        
        public IActionResult SaveUnlockGenerateBill(List<UnLockGeneratBillViewModel> listofcontainers)
        {

            try
            {
                var containerindexlist = new List<ContainerIndex>();

                foreach (var item in listofcontainers)
                {
                    if(item.Status == "CFS")
                    {
                        var containers = _containerIndexRepo.GetontainerIndexByIGMandContainerNumber( item.VIRNo , item.ContainerNo).ToList();

                        if(containers.Count() > 0)
                        {
                            foreach (var newitem in containers)
                            {

                                var resdata = _containerIndexRepo.GetLastAICTAndLineIndexTariff(newitem.VirNo , newitem.IndexNo ?? 0);

                                if(resdata == null)
                                {
                                    return new JsonResult(new { error = true, message = "please update line index rates againts virno = " + newitem.VirNo + " and indexno  = " + newitem.IndexNo });

                                }
                                 
                                newitem.InvoiceLocked = false;
                                containerindexlist.Add(newitem);
                            }
                        }

                    }
                    
                }

                _containerIndexRepo.UpdateRange(containerindexlist);

                return new JsonResult(new { error = false, message = "Update" });


            }
            catch (Exception e)
            { 
                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }

        public IActionResult UpdateUnlocakRemakrs(string virno, string containerno, string remarks)
        {
            try
            {
                var obj = new UnlockGenerateBillRemark
                {
                    VirNo = virno,
                    ContainerNo = containerno,
                    Remarks = remarks
                };

                _unlockGenerateBillRemarkRepository.Add(obj);
 
                return Json(new { error = false, message = "Update" });
  
            }
            catch (Exception e)
            {
                return Json(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }


        public IActionResult DeleteTariffHead(long TariffHeadId)
        {
            try
            {
                var tariff = _tariffRepo.GetAll().Where(x => x.TariffHeadId == TariffHeadId).FirstOrDefault();
                if(tariff != null)
                {
                    return new JsonResult(new { error = true, message = "tariff head amount assign" });
                }
                 
                var tariffhead = _tarifHeadRepo.GetAll().Where(x=> x.TariffHeadId == TariffHeadId).FirstOrDefault();
  
                _tarifHeadRepo.Delete(tariffhead);

                return new JsonResult(new { error = false, message = "Delete" });

 

            }
            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }
        
        public IActionResult UpdateTariffHead(TariffHead res)
        {
            try
            {
              
                 
                var tariffhead = _tarifHeadRepo.GetAll().Where(x=> x.TariffHeadId == res.TariffHeadId).FirstOrDefault();
  
                if(tariffhead != null)
                {
                    tariffhead.Name = res.Name;
                    tariffhead.TariffHeadType = res.TariffHeadType;
                    tariffhead.Description = res.Description;

                    _tarifHeadRepo.Update(tariffhead);
                    return new JsonResult(new { error = false, message = "update" });


                }
                else
                {
                    return new JsonResult(new { error = true, message = "no record found" });
                }
 

            }
            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }


        public  IActionResult UpdateTariffContract()
        {

            return View();
        }


        public JsonResult GetTariffInformation()
        {
            var res = _tariffInformationRepo.GetAll();

            return Json(res);


        }


        public IActionResult UpdateTariffContractData(TariffInformation model)
        {
            try
            {
                var tariffinfo = _tariffInformationRepo.GetAll().Where(x => x.TariffInformationId == model.TariffInformationId).FirstOrDefault();
                if(tariffinfo != null)
                {
                     
                    tariffinfo.ShippingAgentId = model.ShippingAgentId;
                    tariffinfo.ClearingAgentId = model.ClearingAgentId;
                    tariffinfo.GoodsHeadId = model.GoodsHeadId;
                    tariffinfo.ShipperId = model.ShipperId;
                    tariffinfo.PortAndTerminalId = model.PortAndTerminalId;
                    tariffinfo.ISOCodeHeadId = model.ISOCodeHeadId;
                    tariffinfo.ConsigneId = model.ConsigneId;
                    tariffinfo.IndexCargoType = model.IndexCargoType;

                    _tariffInformationRepo.Update(tariffinfo);

                    return new JsonResult(new { error = false, message = "Updated "});
                }
                else
                {
                    return new JsonResult(new { error = true, message = "No Record Found" });
                }
            }
            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }

        }

        public IActionResult TransportCollectionView()
        {


          //  var res = _tariffInformationRepo.GetTariffInformations(null, null, null, null, null, null, null, "CY").ToList();
            
            ViewData["TariffHead"] = _tarifHeadRepo.GetAll().Select(v => new SelectListItem { Text = v.Name, Value = v.TariffHeadId.ToString() });

            ViewData["TransportCollections"] = _transportCollectionRepository.GetAll().Select(v => new SelectListItem { Text = v.TransportCollectionName, Value = v.TransportCollectionId.ToString() });

            ViewData["Consignes"] = _consigneRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.ConsigneName, Value = v.ConsigneId.ToString() });

            ViewData["ClearingAgents"] = _clearingAgentRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.Name, Value = v.ClearingAgentId.ToString() });

            ViewData["Goods"] = _goodsHeadRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });


            ViewData["Shippers"] = _shipperRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.ShipperName, Value = v.ShipperId.ToString() });


            ViewData["ISOCodes"] = _ISOCodeHeadRepository.GetAll()
                .Select(s => new SelectListItem { Text = s.ISOCodeHeadDescription.ToString().Trim().ToUpper(), Value = s.ISOCodeHeadId.ToString() });

             
            ViewData["PortAndTerminal"] = _portAndTerminalRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.PortName, Value = v.PortAndTerminalId.ToString() });
              
            return View();
        }


        public IActionResult SaveTransportCollectionTariffCondition(Tariff model ,   long tarifhead , long transcollectionid)
        {
            try
            {
                try
                {
                    if (model.TariffHeadId != null)
                    {


                        var tarif = _tariffRepo.GetAll(x => x.TariffHead).Where(x => x.TariffHeadId == model.TariffHeadId).LastOrDefault();

                        if (tarif != null)
                        {
                            if (tarif.TariffHead != null)
                            {
                                if (tarif.TariffHead.Name.Contains("STORAGE"))
                                {
                                    model.TariffId = tarif.TariffId;
                                }
                                else
                                {
                                    model.TypeOfTarifff = "MULTIPLE";
                                    _tariffRepo.Add(model);

                                }
                            }
                            else
                            {
                                model.TypeOfTarifff = "MULTIPLE";
                                _tariffRepo.Add(model);

                            }

                        }
                        else
                        {
                            model.TypeOfTarifff = "MULTIPLE";
                            _tariffRepo.Add(model);

                        }

                    }
                    else
                    {
                        return new JsonResult(new { error = true, message = "Please Select Tariff Head" });

                    }


                }
                catch (Exception e)
                {

                    return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });

                }

                  
                    var tariffinformationtariff = new TransportCollectionTariff
                    {
                        TransportCollectionId = transcollectionid,
                        TariffId = model.TariffId

                    };

                _transportCollectionTariffRepository.Add(tariffinformationtariff);
                 
                var tarnsportcollectionres = _transportCollectionRepository.GetAll().Where(x => x.TransportCollectionId == transcollectionid).LastOrDefault();

                if(tarnsportcollectionres != null)
                {
                    if(tarnsportcollectionres.ShippingagentCode != null)
                    {
                        var spcode = tarnsportcollectionres.ShippingagentCode.Split(",").ToList();
                        var spcodeintList = spcode.Select(s => Convert.ToInt32(s)).ToList();

                        
                        foreach (var item in spcodeintList)
                        {


                            long? shippingAgentID = item;
                            long? ConsigneId = tarnsportcollectionres.ConsigneeCode;
                            long? ClearingAgentId = tarnsportcollectionres.ClearningAgentCode;
                            long? GoodsHeadId = tarnsportcollectionres.GoodHeadCode;
                            long? ShipperId = tarnsportcollectionres.ShipperCode;
                            long? ISOCodeHeadId = tarnsportcollectionres.IsoCodeHeadCode;
                            long? PortAndTerminalId = tarnsportcollectionres.PortAndTerminalCode;
                            string IndexCargoType = tarnsportcollectionres.CargoType;




                            var res = _tariffInformationRepo.GetTariffInformations(shippingAgentID, ConsigneId, ClearingAgentId, GoodsHeadId, ShipperId, ISOCodeHeadId, PortAndTerminalId, IndexCargoType).ToList();

                            if(res.Count() > 0)
                            {
                                foreach (var itemres in res)
                                {
                                    var tariffInofrmationTariff = new TariffInofrmationTariff
                                    {
                                        TariffInformationId = itemres.TariffInformationId,
                                        TariffId = model.TariffId

                                    };

                                    _tariffInofrmationTariffRepo.Add(tariffInofrmationTariff);
                                }
                            }

                            //var tariffinfo = _tariffInformationRepo.GetAll().Where(x => x.ShippingAgentId == item && x.ConsigneId == tarnsportcollectionres.ConsigneeCode && x.ClearingAgentId == tarnsportcollectionres.ClearningAgentCode 
                            //&& x.GoodsHeadId == tarnsportcollectionres.GoodHeadCode && x.ShipperId == tarnsportcollectionres.ShipperCode && x.ISOCodeHeadId == tarnsportcollectionres.IsoCodeHeadCode
                            //&& x.IndexCargoType == tarnsportcollectionres.CargoType).FirstOrDefault();


                            //if(tariffinfo != null)
                            //{
                            //    var tariffInofrmationTariff = new TariffInofrmationTariff
                            //    {
                            //        TariffInformationId = tariffinfo.TariffInformationId,
                            //        TariffId = model.TariffId

                            //    };

                            //    _tariffInofrmationTariffRepo.Add(tariffInofrmationTariff);
                            //}


                        }

                    }


                    else
                    {


                        long? shippingAgentID = null;
                        long? ConsigneId = tarnsportcollectionres.ConsigneeCode;
                        long? ClearingAgentId = tarnsportcollectionres.ClearningAgentCode;
                        long? GoodsHeadId = tarnsportcollectionres.GoodHeadCode;
                        long? ShipperId = tarnsportcollectionres.ShipperCode;
                        long? ISOCodeHeadId = tarnsportcollectionres.IsoCodeHeadCode;
                        long? PortAndTerminalId = tarnsportcollectionres.PortAndTerminalCode;
                        string IndexCargoType = tarnsportcollectionres.CargoType;
                         
                        var res = _tariffInformationRepo.GetTariffInformations(shippingAgentID, ConsigneId, ClearingAgentId, GoodsHeadId, ShipperId, ISOCodeHeadId, PortAndTerminalId, IndexCargoType).ToList();

                        if (res.Count() > 0)
                        {
                            foreach (var itemres in res)
                            {
                                var tariffInofrmationTariff = new TariffInofrmationTariff
                                {
                                    TariffInformationId = itemres.TariffInformationId,
                                    TariffId = model.TariffId

                                };

                                _tariffInofrmationTariffRepo.Add(tariffInofrmationTariff);
                            }
                        }



                        //var tariffinfo = _tariffInformationRepo.GetAll().Where(x => x.ShippingAgentId ==  null && x.ConsigneId == tarnsportcollectionres.ConsigneeCode && x.ClearingAgentId == tarnsportcollectionres.ClearningAgentCode
                        //    && x.GoodsHeadId == tarnsportcollectionres.GoodHeadCode && x.ShipperId == tarnsportcollectionres.ShipperCode && x.ISOCodeHeadId == tarnsportcollectionres.IsoCodeHeadCode
                        //    && x.IndexCargoType == tarnsportcollectionres.CargoType).FirstOrDefault();

                        //if (tariffinfo != null)
                        //{
                        //    var tariffInofrmationTariff = new TariffInofrmationTariff
                        //    {
                        //        TariffInformationId = tariffinfo.TariffInformationId,
                        //        TariffId = model.TariffId

                        //    };

                        //    _tariffInofrmationTariffRepo.Add(tariffInofrmationTariff);
                        //}



                    }



                }


                return new JsonResult(new { error = false, message = "Save " });

            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = "Please Create Again" });

                throw;
            }

        }


        public IActionResult DeleteTransportCollectionTariffCondition(long TariffId)
        {

            var tariff = _transportCollectionTariffRepository.GetAll().Where(x => x.TransportCollectionTariffId == TariffId).LastOrDefault();

            try
            {
                if (tariff != null)
                { 
                     
                    _transportCollectionTariffRepository.Delete(tariff);
                     
                    var listdata = _tariffInofrmationTariffRepo.GetAll().Where(x => x.TariffId == tariff.TariffId).ToList();

                    _tariffInofrmationTariffRepo.DeleteRange(listdata);

                    return new JsonResult(new { error = false, message = "delete" });

                }
                else
                {
                    return new JsonResult(new { error = true, message = "Record Not Found" });

                }



            }
            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });

            }
             
        }


        public IActionResult AddTransportCollect(TransportCollection res)
        {
            try
            {  
                _transportCollectionRepository.Add(res);
                 
                return new JsonResult(new { error = false, message = "Save " });

            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = "Please Create Again" });

                throw;
            }

        }
        
        public IActionResult UpdateTransportCollect(long transportCollectionId , TransportCollection res )
        {
            try
            {

                var result = _transportCollectionRepository.GetAll().Where(x => x.TransportCollectionId == transportCollectionId).FirstOrDefault();
                if(result != null)
                {
                    result.ShippingagentCode = res.ShippingagentCode;
                    result.ClearningAgentCode = res.ClearningAgentCode;
                    result.ConsigneeCode = res.ConsigneeCode;
                    result.ShipperCode = res.ShipperCode;
                    result.PortAndTerminalCode = res.PortAndTerminalCode;
                    result.GoodHeadCode = res.GoodHeadCode;
                    result.IsoCodeHeadCode = res.IsoCodeHeadCode;
                    result.CargoType = res.CargoType;
                    result.TransportCollectionName = res.TransportCollectionName;


                    _transportCollectionRepository.Update(result);

                    return new JsonResult(new { error = false, message = "Save " });
                }
                else
                {
                    return new JsonResult(new { error = true, message = "no record found" });
                }
         

            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = "Please Create Again" });

                throw;
            }

        }

        public JsonResult GetTransportCollection()
        {
            var res = _transportCollectionRepository.GetAll();

            return Json(res);
        }


        public JsonResult GetTransportCollectionBy(long collectionid)
        {
           
            var res = _transportCollectionRepository.GetAll().Where(x => x.TransportCollectionId == collectionid).LastOrDefault();
             
                return  Json(res);
            
        }


        public IActionResult UpdatePortChargesAmountFromExcelSheet(List<PortChargesViewModel> model)
        {
            try
            {
                 
                var countDelivered = 0;

                model.RemoveAll(x => x.TotalCharges <= 0);
                var count = 0;
                if (model.Count() > 0)
                {
                    var type = model[0].Type;

                    if(type == "CFS")
                    {
                       
                        foreach (var item in model)
                        {


                            var res = _containerIndexRepo.GetIndexInfo(item.VirNumber, item.IndexNumber).ToList();

                            var containerindexidlist = string.Join(",", res.Select(n => n.ContainerIndexId.ToString()).ToArray());

                            var invoiceres = _invoiceRepository.GetCfsInvoices(containerindexidlist).ToList();


                            if (res != null)
                            {

                                if (invoiceres.Count() > 0)
                                {
                                    countDelivered += 1;
                                }

                                else
                                {


                                    var indetariff = _portChargeRepository.GetPortChargesByIgmIndexContainer( item.VirNumber ,item.IndexNumber ,item.ContainerNumber);
                                    if (indetariff != null)
                                    {
                                        indetariff.DemurrageCharges = item.DemurrageCharges;
                                        indetariff.WeighmentCharges = item.WeighmentCharges;
                                        indetariff.OverWeightPenalty = item.OverWeightPenalty;
                                        indetariff.DetentionChargesOrBulletSeal = item.DetentionChargesOrBulletSeal;
                                        indetariff.ThcPhcKdlpCharges = item.ThcPhcKdlpCharges;
                                        indetariff.LoloCharges = item.LoloCharges;
                                        indetariff.QictCharges = item.QictCharges;
                                        indetariff.OtherCharges = item.OtherCharges;
                                        indetariff.WashAndCleanCharges = item.WashAndCleanCharges;
                                        indetariff.ANF = item.ANF;
                                        indetariff.Pallet = item.Pallet;
                                        indetariff.Recover = item.Recover;
                                        indetariff.TransportCharges = item.TransportCharges;
                                        indetariff.TotalCharges = item.TotalCharges;
                                        _portChargeRepository.Update(indetariff);
                                    }
                                    else
                                    {
                                        var indextariff = new PortCharge
                                        {
                                            VirNumber = item.VirNumber,
                                            IndexNumber = item.IndexNumber,
                                            ContainerNumber = item.ContainerNumber,
                                            DemurrageCharges = item.DemurrageCharges,
                                            WeighmentCharges = item.WeighmentCharges,
                                            OverWeightPenalty = item.OverWeightPenalty,
                                            DetentionChargesOrBulletSeal = item.DetentionChargesOrBulletSeal,
                                            ThcPhcKdlpCharges = item.ThcPhcKdlpCharges,
                                            LoloCharges = item.LoloCharges,
                                            QictCharges = item.QictCharges,
                                            OtherCharges = item.OtherCharges,
                                            WashAndCleanCharges = item.WashAndCleanCharges,
                                            ANF = item.ANF,
                                            Pallet = item.Pallet,
                                            Recover = item.Recover,
                                            TransportCharges = item.TransportCharges,
                                            TotalCharges = item.TotalCharges,

                                        };
                                        _portChargeRepository.Add(indextariff);

                                    }
                                    count += 1;
                                }

                            }





                        }
                    }
                    if (type == "CY")
                    {
                        
                        foreach (var item in model)
                        {


                            var res = _cyContainerRepo.GetUndeliveredIndexbyigmindex(item.VirNumber, item.IndexNumber).ToList();

                            var containerindexidlist = string.Join(",", res.Select(n => n.CYContainerId.ToString()).ToArray());

                            var invoiceres = _invoiceRepository.GetCYInvoices(containerindexidlist).ToList();


                            if (res != null)
                            {
                                if (invoiceres.Count() > 0)
                                {
                                    countDelivered += 1;
                                }
                                else
                                {


                                    var indetariff = _portChargeRepository.GetPortChargesByIgmIndexContainer(item.VirNumber , item.IndexNumber , item.ContainerNumber);
                                    if (indetariff != null)
                                    {
                                        indetariff.DemurrageCharges = item.DemurrageCharges;
                                        indetariff.WeighmentCharges = item.WeighmentCharges;
                                        indetariff.OverWeightPenalty = item.OverWeightPenalty;
                                        indetariff.DetentionChargesOrBulletSeal = item.DetentionChargesOrBulletSeal;
                                        indetariff.ThcPhcKdlpCharges = item.ThcPhcKdlpCharges;
                                        indetariff.LoloCharges = item.LoloCharges;
                                        indetariff.QictCharges = item.QictCharges;
                                        indetariff.OtherCharges = item.OtherCharges;
                                        indetariff.WashAndCleanCharges = item.WashAndCleanCharges;
                                        indetariff.ANF = item.ANF;
                                        indetariff.Pallet = item.Pallet;
                                        indetariff.Recover = item.Recover;
                                        indetariff.TransportCharges = item.TransportCharges;
                                        indetariff.TotalCharges = item.TotalCharges;
                                        _portChargeRepository.Update(indetariff);
                                    }
                                    else
                                    {
                                        var indextariff = new PortCharge
                                        {
                                            VirNumber = item.VirNumber,
                                            IndexNumber = item.IndexNumber,
                                            ContainerNumber = item.ContainerNumber,
                                            DemurrageCharges = item.DemurrageCharges,
                                            WeighmentCharges = item.WeighmentCharges,
                                            OverWeightPenalty = item.OverWeightPenalty,
                                            DetentionChargesOrBulletSeal = item.DetentionChargesOrBulletSeal,
                                            ThcPhcKdlpCharges = item.ThcPhcKdlpCharges,
                                            LoloCharges = item.LoloCharges,
                                            QictCharges = item.QictCharges,
                                            OtherCharges = item.OtherCharges,
                                            WashAndCleanCharges = item.WashAndCleanCharges,
                                            ANF = item.ANF,
                                            Pallet = item.Pallet,
                                            Recover = item.Recover,
                                            TransportCharges = item.TransportCharges,
                                            TotalCharges = item.TotalCharges,

                                        };
                                        _portChargeRepository.Add(indextariff);

                                    }
                                    count += 1;
                                }

                            }




                        }
                    }
                } 

              
                return new JsonResult(new { error = false, message = "Total " + count + "Index Info Updated and total delivered indexes are " + countDelivered });


            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }


        public IActionResult UpdatePortChargesPerBoxAmountFromExcelSheet(List<PortChargesViewModel> datamodel)
        {
            try
            {
                var totalDelivered = 0;
                var totalUnDelivered = 0;
                var totalUnDeliveredIndex = 0;



                datamodel.RemoveAll(x => x.TotalCharges <= 0);
                var count = 0;
                if (datamodel.Count() > 0)
                {
                    var type = datamodel[0].Type;

                    if (type == "CFS")
                    {

                        foreach (var model in datamodel)
                        {


                            var containerindex = _containerIndexRepo.GetContainerIndexByIGMContainerNo(model.VirNumber , model.ContainerNumber).ToList();
                            if (containerindex.Count() > 0)
                            {
                                foreach (var item in containerindex)
                                {
                                    totalUnDeliveredIndex = containerindex.Where(x => x.IsDelivered == false).Count();


                                    if (item.IsDelivered == true)
                                    {
                                        totalDelivered += 1;
                                    }
                                    else
                                    {
                                        totalUnDelivered += 1;
                                    }

                                    if (item.IsDelivered == false)
                                    {
                                        var charges = _portChargeRepository.GetPortChargesByIgmIndexContainer( item.VirNo  , item.IndexNo ?? 0 , item.ContainerNo);

                                        if (totalUnDeliveredIndex > 0)
                                        {
                                            if (charges != null)
                                            {
                                                charges.DemurrageCharges = model.DemurrageCharges > 0 ? Math.Round(model.DemurrageCharges / totalUnDeliveredIndex) : 0;
                                                charges.WeighmentCharges = model.WeighmentCharges > 0 ? Math.Round(model.WeighmentCharges / totalUnDeliveredIndex) : 0;
                                                charges.OverWeightPenalty = model.OverWeightPenalty > 0 ? Math.Round(model.OverWeightPenalty / totalUnDeliveredIndex) : 0;
                                                charges.DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal > 0 ? Math.Round(model.DetentionChargesOrBulletSeal / totalUnDeliveredIndex) : 0;
                                                charges.ThcPhcKdlpCharges = model.ThcPhcKdlpCharges > 0 ? Math.Round(model.ThcPhcKdlpCharges / totalUnDeliveredIndex) : 0;
                                                charges.LoloCharges = model.LoloCharges > 0 ? Math.Round(model.LoloCharges / totalUnDeliveredIndex) : 0;
                                                charges.QictCharges = model.QictCharges > 0 ? Math.Round(model.QictCharges / totalUnDeliveredIndex) : 0;
                                                charges.OtherCharges = model.OtherCharges > 0 ? Math.Round(model.OtherCharges / totalUnDeliveredIndex) : 0;
                                                charges.WashAndCleanCharges = model.WashAndCleanCharges > 0 ? Math.Round(model.WashAndCleanCharges / totalUnDeliveredIndex) : 0;
                                                charges.ANF = model.ANF > 0 ? Math.Round(model.ANF / totalUnDeliveredIndex) : 0;
                                                charges.Pallet = model.Pallet > 0 ? Math.Round(model.Pallet / totalUnDeliveredIndex) : 0;
                                                charges.Recover = model.Recover > 0 ? Math.Round(model.Recover / totalUnDeliveredIndex) : 0;
                                                charges.TransportCharges = model.TransportCharges > 0 ? Math.Round(model.TransportCharges / totalUnDeliveredIndex) : 0;
                                                charges.TotalCharges = model.TotalCharges > 0 ? Math.Round(model.TotalCharges / totalUnDeliveredIndex) : 0;

                                                _portChargeRepository.Update(charges);


                                            }
                                            else
                                            {
                                                var charge = new PortCharge
                                                {
                                                    DemurrageCharges = model.DemurrageCharges > 0 ? Math.Round(model.DemurrageCharges / totalUnDeliveredIndex) : 0,
                                                    WeighmentCharges = model.WeighmentCharges > 0 ? Math.Round(model.WeighmentCharges / totalUnDeliveredIndex) : 0,
                                                    OverWeightPenalty = model.OverWeightPenalty > 0 ? Math.Round(model.OverWeightPenalty / totalUnDeliveredIndex) : 0,
                                                    DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal > 0 ? Math.Round(model.DetentionChargesOrBulletSeal / totalUnDeliveredIndex) : 0,
                                                    ThcPhcKdlpCharges = model.ThcPhcKdlpCharges > 0 ? Math.Round(model.ThcPhcKdlpCharges / totalUnDeliveredIndex) : 0,
                                                    LoloCharges = model.LoloCharges > 0 ? Math.Round(model.LoloCharges / totalUnDeliveredIndex) : 0,
                                                    QictCharges = model.QictCharges > 0 ? Math.Round(model.QictCharges / totalUnDeliveredIndex) : 0,
                                                    OtherCharges = model.OtherCharges > 0 ? Math.Round(model.OtherCharges / totalUnDeliveredIndex) : 0,
                                                    WashAndCleanCharges = model.WashAndCleanCharges > 0 ? Math.Round(model.WashAndCleanCharges / totalUnDeliveredIndex) : 0,
                                                    VirNumber = model.VirNumber,
                                                    IndexNumber = item.IndexNo ?? 0,
                                                    ContainerNumber = model.ContainerNumber,
                                                    ANF = model.ANF > 0 ? Math.Round(model.ANF / totalUnDeliveredIndex) : 0,
                                                    Pallet = model.Pallet > 0 ? Math.Round(model.Pallet / totalUnDeliveredIndex) : 0,
                                                    Recover = model.Recover > 0 ? Math.Round(model.Recover / totalUnDeliveredIndex) : 0,
                                                    TransportCharges = model.TransportCharges > 0 ? Math.Round(model.TransportCharges / totalUnDeliveredIndex) : 0,
                                                    TotalCharges = model.TotalCharges > 0 ? Math.Round(model.TotalCharges / totalUnDeliveredIndex) : 0,

                                                };
  
                                                _portChargeRepository.Add(charge);

                                            }
                                        }

                                    }


                                }


                            }
                             

                        }

                        return new JsonResult(new { error = false, message = "Total " + totalUnDelivered + "Continer Info Updated  and total " + totalDelivered + " Are Deliverd" });


                    }
                    if (type == "CY")
                    {

                        foreach (var model in datamodel)
                        {


                            var container = _containerIndexRepo.GetCYContainerByIGMContainerNo(model.VirNumber, model.ContainerNumber).ToList();
                            if (container.Count() > 0)
                            {

                                foreach (var item in container)
                                {
                                    totalUnDeliveredIndex = container.Where(x => x.IsDelivered == false).Count();

                                    if (item.IsDelivered == true)
                                    {
                                        totalDelivered += 1;
                                    }
                                    else
                                    {
                                        totalUnDelivered += 1;
                                    }

                                    if (item.IsDelivered == false)
                                    {
                                        var charges = _portChargeRepository.GetPortChargesByIgmIndexContainer( item.VIRNo  ,  item.IndexNo  ?? 0, item.ContainerNo);

                                        if (totalUnDeliveredIndex > 0)
                                        {
                                            if (charges != null)
                                            {
                                                charges.DemurrageCharges = model.DemurrageCharges > 0 ? Math.Round(model.DemurrageCharges / totalUnDeliveredIndex) : 0;
                                                charges.WeighmentCharges = model.WeighmentCharges > 0 ? Math.Round(model.WeighmentCharges / totalUnDeliveredIndex) : 0;
                                                charges.OverWeightPenalty = model.OverWeightPenalty > 0 ? Math.Round(model.OverWeightPenalty / totalUnDeliveredIndex) : 0;
                                                charges.DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal > 0 ? Math.Round(model.DetentionChargesOrBulletSeal / totalUnDeliveredIndex) : 0;
                                                charges.ThcPhcKdlpCharges = model.ThcPhcKdlpCharges > 0 ? Math.Round(model.ThcPhcKdlpCharges / totalUnDeliveredIndex) : 0;
                                                charges.LoloCharges = model.LoloCharges > 0 ? Math.Round(model.LoloCharges / totalUnDeliveredIndex) : 0;
                                                charges.QictCharges = model.QictCharges > 0 ? Math.Round(model.QictCharges / totalUnDeliveredIndex) : 0;
                                                charges.OtherCharges = model.OtherCharges > 0 ? Math.Round(model.OtherCharges / totalUnDeliveredIndex) : 0;
                                                charges.WashAndCleanCharges = model.WashAndCleanCharges > 0 ? Math.Round(model.WashAndCleanCharges / totalUnDeliveredIndex) : 0;
                                                charges.ANF = model.ANF > 0 ? Math.Round(model.ANF / totalUnDeliveredIndex) : 0;
                                                charges.Pallet = model.Pallet > 0 ? Math.Round(model.Pallet / totalUnDeliveredIndex) : 0;
                                                charges.Recover = model.Recover > 0 ? Math.Round(model.Recover / totalUnDeliveredIndex) : 0;
                                                charges.TransportCharges = model.TransportCharges > 0 ? Math.Round(model.TransportCharges / totalUnDeliveredIndex) : 0;

                                                charges.TotalCharges = model.TotalCharges > 0 ? Math.Round(model.TotalCharges / totalUnDeliveredIndex) : 0;

                                                _portChargeRepository.Update(charges);

                                            }
                                            else
                                            {
                                                var charge = new PortCharge
                                                {
                                                    DemurrageCharges = model.DemurrageCharges > 0 ? Math.Round(model.DemurrageCharges / totalUnDeliveredIndex) : 0,
                                                    WeighmentCharges = model.WeighmentCharges > 0 ? Math.Round(model.WeighmentCharges / totalUnDeliveredIndex) : 0,
                                                    OverWeightPenalty = model.OverWeightPenalty > 0 ? Math.Round(model.OverWeightPenalty / totalUnDeliveredIndex) : 0,
                                                    DetentionChargesOrBulletSeal = model.DetentionChargesOrBulletSeal > 0 ? Math.Round(model.DetentionChargesOrBulletSeal / totalUnDeliveredIndex) : 0,
                                                    ThcPhcKdlpCharges = model.ThcPhcKdlpCharges > 0 ? Math.Round(model.ThcPhcKdlpCharges / totalUnDeliveredIndex) : 0,
                                                    LoloCharges = model.LoloCharges > 0 ? Math.Round(model.LoloCharges / totalUnDeliveredIndex) : 0,
                                                    QictCharges = model.QictCharges > 0 ? Math.Round(model.QictCharges / totalUnDeliveredIndex) : 0,
                                                    OtherCharges = model.OtherCharges > 0 ? Math.Round(model.OtherCharges / totalUnDeliveredIndex) : 0,
                                                    WashAndCleanCharges = model.WashAndCleanCharges > 0 ? Math.Round(model.WashAndCleanCharges / totalUnDeliveredIndex) : 0,

                                                    VirNumber = model.VirNumber,
                                                    IndexNumber = item.IndexNo ?? 0,
                                                    ContainerNumber = model.ContainerNumber,
                                                    ANF = model.ANF > 0 ? Math.Round(model.ANF / totalUnDeliveredIndex) : 0,
                                                    Pallet = model.Pallet > 0 ? Math.Round(model.Pallet / totalUnDeliveredIndex) : 0,
                                                    Recover = model.Recover > 0 ? Math.Round(model.Recover / totalUnDeliveredIndex) : 0,
                                                    TransportCharges = model.TransportCharges > 0 ? Math.Round(model.TransportCharges / totalUnDeliveredIndex) : 0,
                                                    TotalCharges = model.TotalCharges > 0 ? Math.Round(model.TotalCharges / totalUnDeliveredIndex) : 0,
                                                };



                                                _portChargeRepository.Add(charge);

                                            }
                                        }

                                    }


                                }
                            }


                                
                        }
                    }
                }


                return new JsonResult(new { error = false, message = "Total " + count + "Index Info Updated " });


            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }

        public IActionResult DeleteTransportCollectionDetail(long transcollectionid)
        {
            try
            {
                var collection = _transportCollectionRepository.GetAll().Where(x => x.TransportCollectionId == transcollectionid).LastOrDefault();

                if(collection != null)
                {
                   

                    var collectionTariff = _transportCollectionTariffRepository.GetAll().Where(x => x.TransportCollectionId == transcollectionid).ToList();

                    if(collectionTariff.Count() > 0 )
                    {

                        foreach (var item in collectionTariff)
                        {
                            var listdata = _tariffInofrmationTariffRepo.GetAll().Where(x => x.TariffId == item.TariffId).ToList();

                            var tariffdata = _tariffRepo.GetAll().Where(x => x.TariffId == item.TariffId).LastOrDefault();

                            if (listdata.Count() > 0 )
                            {
                                _tariffInofrmationTariffRepo.DeleteRange(listdata);
                            }

                            if (tariffdata != null)
                            {
                                _tariffRepo.Delete(tariffdata);
                            }

                           
                          
                        }

                        _transportCollectionTariffRepository.DeleteRange(collectionTariff);


                    }

                    _transportCollectionRepository.Delete(collection);

                    return new JsonResult(new { error = false, message = "Deleted" });
                }
                else
                {
                    return new JsonResult(new { error = true, message = "No Result Found" });
                }
            }
            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }


        public IActionResult EmptyContainerTariffView()
        {
              
            ViewData["ShippingAgent"] = _agentRepo.GetAll()
                .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });
             
            var tariffs = _emptyContainerTariffRepository.GetList(t => t.EmptyContainerTariffName.ToUpper().Contains("STORAGE"));
             
            ViewData["EmptyContainerTariff"] = tariffs
             .Select(v => new SelectListItem { Text = v.EmptyContainerTariffName  , Value = v.EmptyContainerTariffId.ToString() });

            return View();
        }



        public JsonResult GetEmptyContainerTariff(long ShippingAgentId)
        {
            var res = _emptyContainerTariffRepository.GetAll().Where(x => x.ShippingAgentId == ShippingAgentId).ToList();

            return Json(res);
        }

        public IActionResult AddEmptycontainerTariff(long shippingAgentId , EmptyContainerTariff data)
        {
            try
            {
                data.ShippingAgentId = shippingAgentId;
                _emptyContainerTariffRepository.Add(data);

                return new JsonResult(new { error = true, message = "Save Info" });

            }

            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });

            }
           
             
        }

        public IActionResult updateEmptyContainerTariff(EmptyContainerTariff res)
        {

            try
            {
                var result = _emptyContainerTariffRepository.GetAll().Where(x => x.EmptyContainerTariffId == res.EmptyContainerTariffId).LastOrDefault();
                if (result != null)
                {
                    result.EmptyContainerTariffName = res.EmptyContainerTariffName;                    
                    result.Rate20 = res.Rate20;
                    result.Rate40 = res.Rate40;
                    result.Rate45 = res.Rate45;

                    _emptyContainerTariffRepository.Update(result);
                    return new JsonResult(new { error = false, message = "save" });

                }
                else
                {
                    return new JsonResult(new { error = true, message = "result not found" });

                }


            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }


        }

        public IActionResult DeleteEmptyContainerTariff(long emptyContainerTariffId)
        {

            try
            {
                var result = _emptyContainerTariffRepository.GetAll().Where(x => x.EmptyContainerTariffId == emptyContainerTariffId).LastOrDefault();
                if (result != null)
                {
                    _emptyContainerTariffRepository.Delete(result);
                    return new JsonResult(new { error = false, message = "Delete" });

                }
                else
                {
                    return new JsonResult(new { error = true, message = "result not found" });
                }
                 
            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }

        }

        public JsonResult GetEmptyContainerStorageSlabs(long tariffid)
        {
            var res = _emptyContainerStorageSlabRepository.GetAll().Where(x => x.EmptyContainerTariffId == tariffid).ToList();

            return Json(res);
        }



        [HttpPost]
        public IActionResult AddEmptycontainerStorageSlab(EmptyContainerStorageSlab data, long tariffid)
        { 
            try
            { 
                
                data.EmptyContainerTariffId = tariffid;

                _emptyContainerStorageSlabRepository.Add(data);
                return new JsonResult(new { error = false, message = "save" });

            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }


        }

        public IActionResult updateEmptyContainerStorageSlab(EmptyContainerStorageSlab res)
        {

            try
            {
                var result = _emptyContainerStorageSlabRepository.GetAll().Where(x => x.EmptyContainerStorageSlabId == res.EmptyContainerStorageSlabId).LastOrDefault();
                if (result != null)
                {
                
                    result.Rate20 = res.Rate20;
                    result.Rate40 = res.Rate40;
                    result.Rate45 = res.Rate45;
                    result.Description = res.Description;
                    result.NoOfDays = res.NoOfDays;

                    _emptyContainerStorageSlabRepository.Update(result);
                    return new JsonResult(new { error = false, message = "save" });

                }
                else
                {
                    return new JsonResult(new { error = true, message = "result not found" });

                }


            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }


        }

        public IActionResult DeleteEmptyContainerStorageSlab(long emptyContainerStorageSlabId)
        {

            try
            {
                var result = _emptyContainerStorageSlabRepository.GetAll().Where(x => x.EmptyContainerStorageSlabId == emptyContainerStorageSlabId).LastOrDefault();
                if (result != null)
                {
                    _emptyContainerStorageSlabRepository.Delete(result);
                    return new JsonResult(new { error = false, message = "Delete" });

                }
                else
                {
                    return new JsonResult(new { error = true, message = "result not found" });

                }


            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }


        }


        public JsonResult findrecordbycode(long tariffCode)
        {
            var res = _tariffInformationRepo.GetAll().Where(x => x.TariffInformationId == tariffCode).LastOrDefault();

            return Json( res);
        }



        public IActionResult Deleterecordbycode(long tariffCode)
        {

            try
            {
                var result = _tariffInformationRepo.GetAll().Where(x => x.TariffInformationId == tariffCode).LastOrDefault();
                if (result != null)
                {
                    var res = _tariffInofrmationTariffRepo.GetAll().Where(x => x.TariffInformationId == result.TariffInformationId).ToList();

                    if(res.Count() > 0)
                    {
                        _tariffInofrmationTariffRepo.DeleteRange(res);
                        _tariffInformationRepo.Delete(result);

                        return new JsonResult(new { error = false, message = "Delete" });


                    }

                    _tariffInformationRepo.Delete(result);
                    return new JsonResult(new { error = false, message = "Delete" });

                }
                else
                {
                    return new JsonResult(new { error = true, message = "result not found" });

                }


            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }


        }

        public IActionResult Updaterecordbycode(long tariffCode, TariffInformation tariff)
        {
            try
            {
                var result = _tariffInformationRepo.GetAll().Where(x => x.TariffInformationId == tariffCode).LastOrDefault();
                if (result != null)
                {

                    var res = _tariffInformationRepo.GetAll().Where(x => x.ShippingAgentId == tariff.ShippingAgentId && x.ConsigneId == tariff.ConsigneId && x.ClearingAgentId == tariff.ClearingAgentId
                                                                    && x.GoodsHeadId == tariff.GoodsHeadId && x.ShipperId == tariff.ShipperId && x.ISOCodeHeadId == tariff.ISOCodeHeadId
                                                                    && x.IndexCargoType == tariff.IndexCargoType && x.PortAndTerminalId == tariff.PortAndTerminalId).FirstOrDefault();

                    if(res != null && result.TariffInformationId != res.TariffInformationId)
                    {
                        return new JsonResult(new { error = true, message = "Already Avaible" });

                    }
                    else
                    {
                        result.ShippingAgentId = tariff.ShippingAgentId;
                        result.ConsigneId = tariff.ConsigneId;
                        result.ClearingAgentId = tariff.ClearingAgentId;
                        result.GoodsHeadId = tariff.GoodsHeadId;
                        result.ShipperId = tariff.ShipperId;
                        result.ISOCodeHeadId = tariff.ISOCodeHeadId;
                        result.IndexCargoType = tariff.IndexCargoType;
                        result.PortAndTerminalId = tariff.PortAndTerminalId;

                        _tariffInformationRepo.Update(result);

                    }


                    return new JsonResult(new { error = false, message = "Updated" });
                }
                else
                {
                    return new JsonResult(new { error = true, message = "result not found" });

                }
                


            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }
        }


      

        public IActionResult EnableDisableContract(long tariffCode)
        {
            try
            {
                var result = _tariffInformationRepo.GetAll().Where(x => x.TariffInformationId == tariffCode).LastOrDefault();
                if (result != null)
                {

                     if(result.IsEnableDisabled == false)
                    {

                        result.IsEnableDisabled = true;
                        _tariffInformationRepo.Update(result);

                        return new JsonResult(new { error = false, message = "contract disabled" });
                    }
                    
                    if(result.IsEnableDisabled == true)
                    {
                        result.IsEnableDisabled = false;
                        _tariffInformationRepo.Update(result);

                        return new JsonResult(new { error = false, message = "contract enabled" });
                    }


                    return new JsonResult(new { error = false, message = "Updated" });
                }
                else
                {
                    return new JsonResult(new { error = true, message = "result not found" });

                }
                 
            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }
        }


        public IActionResult StorageHolidaysView()
        { 
            return View();
        }

        public IActionResult AddStorageHolidays(string igm, int indexno, long? noofdays)
        {
            try
            {

                var res = new StorageFreeDaysForHoliday
                {
                    Freedays = noofdays,
                    IndexNumber = indexno,
                    VirNumber = igm                   
                };

                _storageFreeDaysForHolidayRepository.Add(res);

                return new JsonResult(new { error = false, message = "save" });
            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }
        }


        public IActionResult Addffsharerates(long TariffId , long tariffCode , long ffsharerate)
        {
            try
            { 
                var result = _tariffInformationRepo.GetAll().Where(x => x.TariffInformationId == tariffCode).LastOrDefault();
                if (result != null)
                {

                    var Tariff = _tariffRepo.GetAll().Where(x => x.TariffId == TariffId).LastOrDefault();

                    if(Tariff == null)
                    {
                        return new JsonResult(new { error = true, message = "no tariff found" });
                    }

                    var resdata = new StorageShareRate
                    {
                        ShareRate = ffsharerate,
                        TariffId = TariffId,
                        TariffInformationId = tariffCode
                    };
                    _storageShareRateRepository.Add(resdata);


                    return new JsonResult(new { error = false, message = "Updated" });
                }
                else
                {
                    return new JsonResult(new { error = true, message = "result not found" });

                }

            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }
        }


        #region Tariff PerBox Rate

        [HttpPost]
        public IActionResult AddTariffPerBoxRate(TariffPerBoxRate data, long tariffCode)
        {


            try
            {
                if(data.TypeOfImplementation == "" || data.TypeOfImplementation == null)
                {
                    return new JsonResult(new { error = true, message = "please select TypeOfImplementation" });
                }  
                if (data.TypeOfImplementation != "All" && data.ImplementFrom == null)
                {
                    return new JsonResult(new { error = true, message = "please  select from date " });
                } 
                else if (data.ImplementFrom != null && data.TypeOfImplementation == "All")
                {
                    return new JsonResult(new { error = true, message = "please  select Time Line Type" });                     
                }  
                else if (data.ImplementTo != null && data.TypeOfImplementation == "All")
                {
                    return new JsonResult(new { error = true, message = "please  select Time Line Type" });   
                }

                var res = new TariffPerBoxRate
                {
                    CBMRate = data.CBMRate,
                    DividedIndex = data.DividedIndex,
                    ImplementFrom = data.ImplementFrom,
                    ImplementTo = data.ImplementTo,
                    PerIndex = data.PerIndex,
                    PortAndTerminalId = data.PortAndTerminalId,
                    Rate20 = data.Rate20,
                    Rate40 = data.Rate40,
                    Rate45 = data.Rate45,
                    TariffInformationId = tariffCode,
                    TypeOfImplementation = data.TypeOfImplementation,
                    WeightRate = data.WeightRate,                    
                };

                _tariffPerBoxRateRepository.Add(res);

                return new JsonResult(new { error = false, message = "save" });

            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }


        }
        
        public IActionResult DeleteTariffPerBoxRate(long tariffPerBoxRateId)
        {


            try
            {
                var res = _tariffPerBoxRateRepository.GetAll().Where(x => x.TariffPerBoxRateId == tariffPerBoxRateId).LastOrDefault();

                if(res != null)
                {

                    _tariffPerBoxRateRepository.Delete(res);

                    return new JsonResult(new { error = false, message = "Delete" });
                }
                else
                {
                    return new JsonResult(new { error = true, message = "no record found" });
                }


            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }


        }

        #endregion

        public IActionResult MultiTariff()
        {
            return View();
        }
    }
}



