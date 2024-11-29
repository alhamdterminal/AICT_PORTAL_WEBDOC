using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Export.Controllers
{
    public class TariffExportController : ParentController
    {
        public ITariffHeadExportRepository _tariffHeadExportRepo;
        public ITariffExportRepository _tariffExportRepo;
        public IVesselExportRepository _vesselExportRepo;
        public IVoyageExportRepository _voyageExportRepo;
        public IGDTariffRepository _GDTariffRepo;
        public IShippingAgentTariffExportRepository _shippingAgentTariffRepo;
        public IStorageSlabExportRepository  _storageSlabExportRepo;
        public IShippingAgentExportRepository _shippingAgentRepo;
        public INatureOfTariffRepository _natureOfTariffRepository;
        public IShippingAgentTariffExportRepository _shippingAgentTariffExportRepository;
        public IEnteringCargoRepository  _enteringCargoRepository;
        public IDisabledAgentTariffExportRepository _disabledAgentTariffExportRepository;
        public ILoadingProgramRepository  _loadingProgramRepository;
        public ITariffRateSlabWiseRepository _tariffRateSlabWiseRepository;
        
        public ITariffInformationExportRepository _tariffInformationExportRepository;
        public ITariffInofrmationTariffExportRepository _tariffInofrmationTariffExportRepository;
        public IExportTariffRepository _exportTariffRepository;
        public ITariffPercentageExportRepository _tariffPercentageExportRepository;
        public IShippingLineExportRepository _shippingLineExportRepository;
        public IClearingAgentExportRepository  _clearingAgentExportRepository;
        public IGoodsHeadExportRepository _goodsHeadExportRepository;
        public IPortAndTerminalExportRepository _portAndTerminalExportRepository;
        public IExportConsigneeRepository _exportConsigneeRepository;
        public ITerminalAndFFShareRateExportRepository _terminalAndFFShareRateExportRepository; 



        public TariffExportController(ITariffHeadExportRepository tariffHeadExportRepo,
                                      ITariffExportRepository tariffExportRepo,
                                      IVesselExportRepository vesselExportRepo,
                                      IVoyageExportRepository voyageExportRepo,
                                      IGDTariffRepository GDTariffRepo,
                                      IShippingAgentTariffExportRepository shippingAgentTariffRepo,
                                      IStorageSlabExportRepository storageSlabExportRepo,
                                      IShippingAgentExportRepository shippingAgentRepo,
                                      INatureOfTariffRepository natureOfTariffRepository,
                                      IShippingAgentTariffExportRepository shippingAgentTariffExportRepository,
                                      IEnteringCargoRepository enteringCargoRepository,
                                      IDisabledAgentTariffExportRepository disabledAgentTariffExportRepository,
                                      ILoadingProgramRepository loadingProgramRepository,
                                      ITariffRateSlabWiseRepository tariffRateSlabWiseRepository,

                                      ITariffInformationExportRepository tariffInformationExportRepository,
                                      ITariffInofrmationTariffExportRepository tariffInofrmationTariffExportRepository,
                                      IExportTariffRepository exportTariffRepository,
                                      ITariffPercentageExportRepository tariffPercentageExportRepository,
                                      IShippingLineExportRepository shippingLineExportRepository,
                                      IClearingAgentExportRepository clearingAgentExportRepository,
                                      IGoodsHeadExportRepository goodsHeadExportRepository,
                                      IPortAndTerminalExportRepository portAndTerminalExportRepository,
                                      IExportConsigneeRepository exportConsigneeRepository,
                                      ITerminalAndFFShareRateExportRepository terminalAndFFShareRateExportRepository)
        {
            _tariffHeadExportRepo = tariffHeadExportRepo;
            _tariffExportRepo = tariffExportRepo;
            _vesselExportRepo = vesselExportRepo;
            _voyageExportRepo = voyageExportRepo;
            _GDTariffRepo = GDTariffRepo;
            _storageSlabExportRepo = storageSlabExportRepo;
            _shippingAgentRepo = shippingAgentRepo;
            _shippingAgentTariffRepo = shippingAgentTariffRepo;
            _natureOfTariffRepository = natureOfTariffRepository;
            _shippingAgentTariffExportRepository = shippingAgentTariffExportRepository;
            _enteringCargoRepository = enteringCargoRepository;
            _disabledAgentTariffExportRepository = disabledAgentTariffExportRepository;
            _loadingProgramRepository = loadingProgramRepository;
            _tariffRateSlabWiseRepository = tariffRateSlabWiseRepository;

            _tariffInformationExportRepository = tariffInformationExportRepository;
            _tariffInofrmationTariffExportRepository = tariffInofrmationTariffExportRepository;
            _exportTariffRepository = exportTariffRepository;
            _tariffPercentageExportRepository = tariffPercentageExportRepository;
            _shippingLineExportRepository = shippingLineExportRepository;
            _clearingAgentExportRepository = clearingAgentExportRepository;
            _goodsHeadExportRepository = goodsHeadExportRepository;
            _portAndTerminalExportRepository = portAndTerminalExportRepository;
            _exportConsigneeRepository = exportConsigneeRepository;
            _terminalAndFFShareRateExportRepository = terminalAndFFShareRateExportRepository;
        }

        #region Tarif Head Export

        public IActionResult TarifHeadExport()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllTariffHeadExport()
        {
            var tariffHead = _tariffHeadExportRepo.GetAll();

            return Json(tariffHead);
        }

        public IActionResult UpdateTariffHead(TariffHeadExport res)
        {
            try
            {


                var tariffhead = _tariffHeadExportRepo.GetAll().Where(x => x.TariffHeadExportId == res.TariffHeadExportId).FirstOrDefault();

                if (tariffhead != null)
                {
                    tariffhead.Name = res.Name;
                    tariffhead.TariffHeadExportType = res.TariffHeadExportType;
                    tariffhead.Description = res.Description;

                    _tariffHeadExportRepo.Update(tariffhead);
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

        public IActionResult DeleteTariffHead(long TariffHeadExportId)
        {
            try
            {
                var tariff = _exportTariffRepository.GetAll().Where(x => x.TariffHeadExportId == TariffHeadExportId).FirstOrDefault();
                if (tariff != null)
                {
                    return new JsonResult(new { error = true, message = "tariff head amount assign" });
                }

                var tariffhead = _tariffHeadExportRepo.GetAll().Where(x => x.TariffHeadExportId == TariffHeadExportId).FirstOrDefault();

                _tariffHeadExportRepo.Delete(tariffhead);

                return new JsonResult(new { error = false, message = "Delete" });



            }
            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }


        public IActionResult SaveTariffHeadExport(TariffHeadExport tariffhead)
        {
            _tariffHeadExportRepo.Add(tariffhead);

            return Ok();
        }


        #endregion


        #region Tariff Persent Export

        public IActionResult TariffPersentExportView()
        {

            ViewData["ShippingAgents"] = _shippingAgentRepo.GetAll()
             .Select(v => new SelectListItem { Text = v.ShippingAgentName, Value = v.ShippingAgentExportId.ToString() });
             
            return View();

        }

        public JsonResult GetTariffHeadsTypeWise()
        {
            var res = _tariffHeadExportRepo.GetAll().Where(x => x.TariffHeadExportType == "TariffHead").ToList();

            return Json(res);
        }

        [HttpGet]
        public JsonResult GetTariffPercentages(long? shippingAgentExportId)
        {

            var tariffs = _tariffPercentageExportRepository.GetAll().Where(x => x.ShippingAgentExportId == shippingAgentExportId).ToList();

            return Json(tariffs);
        }


        public IActionResult SaveTariffPersent(long ShippingAgentExportId, List<TariffPercentageExport> tariff)
        {
            try
            {

                var tariffpersen = _tariffPercentageExportRepository.GetAll().Where(x => x.ShippingAgentExportId == ShippingAgentExportId).ToList();
                if (tariffpersen.Count() > 0)
                {
                    _tariffPercentageExportRepository.DeleteRange(tariffpersen);
                }
                if (tariff.Count() > 0)
                {

                    foreach (var item in tariff)
                    {
                        item.TariffPercentageExportId = 0;
                        item.ShippingAgentExportId = ShippingAgentExportId;
                    }

                }


                _tariffPercentageExportRepository.AddRange(tariff);
                return new JsonResult(new { error = false, message = "Save " });



            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = "Please Create Again" });

                throw;
            }

        }



        #endregion


        #region Tariff Assigning Export

        public IActionResult TariffAssigningExport()
        {
            ViewData["ShippingAgents"] = _shippingAgentRepo.GetAll()
             .Select(v => new SelectListItem { Text = v.ShippingAgentName, Value = v.ShippingAgentExportId.ToString() });

            ViewData["TariffHead"] = _tariffHeadExportRepo.GetAll().Select(v => new SelectListItem { Text = v.Name, Value = v.TariffHeadExportId.ToString() });
             
            ViewData["ClearingAgents"] = _clearingAgentExportRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.ClearingAgentName, Value = v.ClearingAgentExportId.ToString() });

            ViewData["Goods"] = _goodsHeadExportRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadExportId.ToString() });

            ViewData["shippingLineExport"] = _shippingLineExportRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.ShippingLineName, Value = v.ShippingLineExportId.ToString() });
            
            ViewData["AgentPercentTariff"] = _tariffInformationExportRepository.GetShippingAgentFromPercentAgeTariff()
                .Select(v => new SelectListItem { Text = v.ShippingAgentName, Value = v.ShippingAgentExportId.ToString() });

            ViewData["PortAndTerminal"] = _portAndTerminalExportRepository.GetAll()
                .Select(v => new SelectListItem { Text = v.PortName, Value = v.PortAndTerminalExportId.ToString() });

            var tariffs = _exportTariffRepository.GetList(t => t.TariffHeadExport != null && t.TariffHeadExport.Name.ToUpper().Contains("STORAGE"), t => t.TariffHeadExport);
            ViewData["Tariffs"] = tariffs
             .Select(v => new SelectListItem { Text = v.TariffHeadExport.Name + " Desc" + v.TariffHeadExport.Description, Value = v.ExportTariffId.ToString() });
             
            return View();
        }

        public JsonResult GetALlConsignees()
        {
            var consignees = _exportConsigneeRepository.GetAll().Select(x => new SelectListItem { Text = x.ConsigneName, Value = x.ExportConsigneeId.ToString() }).ToList();

            return Json(consignees);
        }


        public JsonResult GetAllPortAndTerminals()
        {
            var res = _portAndTerminalExportRepository.GetAll().Select(x => new SelectListItem { Text = x.PortName, Value = x.PortAndTerminalExportId.ToString() }).ToList();

            return Json(res);
        }


        [HttpGet]
        public JsonResult GetTariffsByPerametersId(long? ShippingAgentId, long? ConsigneId, long? ClearingAgentId, long? GoodId, long? ShipperId, long? portAndTerminalId, string indexCargoType)
        {
            var tariffs = _tariffInformationExportRepository.GetTariffsByPerametersId(ShippingAgentId, ConsigneId, ClearingAgentId, GoodId, ShipperId,   portAndTerminalId, indexCargoType).ToList();

            return Json(tariffs);
        }


        [HttpGet]
        public JsonResult GetStorageSlabsByTariff(long TariffId)
        {
            var slabs = _storageSlabExportRepo.GetList(c => c.ExportTariffId == TariffId);

            return Json(slabs);
        }


        public IActionResult updateStorageSlab(StorageSlabExport res)
        {

            try
            {
                var result = _storageSlabExportRepo.GetAll().Where(x => x.StorageSlabExportId == res.StorageSlabExportId).LastOrDefault();
                if (result != null)
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

                    _storageSlabExportRepo.Update(result);
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
                var result = _storageSlabExportRepo.GetAll().Where(x => x.StorageSlabExportId == StorageSlabId).LastOrDefault();
                if (result != null)
                {
                    _storageSlabExportRepo.Delete(result);
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


        [HttpPost]
        public IActionResult AddStorageSlab(StorageSlabExport slab, long tariffid)
        {


            try
            {

                slab.Rate = slab.AICTRate + slab.FFRate;
                slab.Rate20 = slab.AICTRate20 + slab.FFRate20;
                slab.Rate40 = slab.AICTRate40 + slab.FFRate40;
                slab.Rate45 = slab.AICTRate45 + slab.FFRate45;
                slab.ExportTariffId = tariffid;
                _storageSlabExportRepo.Add(slab);
                return new JsonResult(new { error = false, message = "save" });

            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }


        }

        #endregion


        #region Export Tariff 


        public IActionResult updateAgentTariff(ExportTariff tariff)
        {

            try
            { 
                _exportTariffRepository.Update(tariff);
                return new JsonResult(new { error = false, message = "save" });

            }
            catch (Exception e)
            {
                var message = e.InnerException.InnerException.Message;
                return new JsonResult(new { error = true, message = message });
            }


        }


        public IActionResult DeleteSaveTariffCondition(long TariffId)
        {

            var tariff = _tariffInofrmationTariffExportRepository.GetAll().Where(x => x.TariffInofrmationTariffExportId == TariffId).LastOrDefault();

            try
            {
                if (tariff != null)
                {
                    _tariffInofrmationTariffExportRepository.Delete(tariff);

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



        [HttpGet]
        public JsonResult GetShareRateByPerametersId(long? ShippingAgentId, long? GoodId, string indexCargoType)
        {
            var tariffs = _terminalAndFFShareRateExportRepository.GetShareRateByPerametersId(ShippingAgentId, GoodId, indexCargoType);

            return Json(tariffs);
        }


        public JsonResult GetShippingAgentById(long shippingagentid)
        {
            var result = _shippingAgentRepo.GetAll().Where(x => x.ShippingAgentExportId == shippingagentid).ToList();

            return Json(result);

        }

        public IActionResult SaveTariffCondition(ExportTariff model, TariffInformationExport tariff)
        {
            try
            {
                try
                {

                    var currentdate = DateTime.Now;
                    model.TariffDate = currentdate;


                    if (model.TariffHeadExportId != null)
                    {

                        if (model.IsFixedRate == true)
                        {
                            if (model.IsGeneral == true)
                            {
                                return new JsonResult(new { error = true, message = "For Fixed Rate Type General Type Not allow" });
                            }
                            if (tariff.IndexCargoType != "LCL")
                            {
                                return new JsonResult(new { error = true, message = "Fixed Rate Tariff Is Only For CFS (LCL)" });
                            }
                        }

                        var tarif = _exportTariffRepository.GetAll(x => x.TariffHeadExport).Where(x => x.TariffHeadExportId == model.TariffHeadExportId).LastOrDefault();

                        if (tarif != null)
                        {
                            if (tarif.TariffHeadExport != null)
                            {
                                if (tarif.TariffHeadExport.Name.Contains("STORAGE"))
                                {
                                    model.ExportTariffId = tarif.ExportTariffId;
                                }
                                else
                                { 
                                    _exportTariffRepository.Add(model);
                                }
                            }
                            else
                            {
                                _exportTariffRepository.Add(model);
                            }

                        }
                        else
                        {
                            _exportTariffRepository.Add(model);
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





                var res = _tariffInformationExportRepository.GetAll().Where(x => x.ShippingAgentExportId == tariff.ShippingAgentExportId && x.ExportConsigneeId == tariff.ExportConsigneeId 
                                                                && x.ClearingAgentExportId == tariff.ClearingAgentExportId
                                                                && x.GoodsHeadExportId == tariff.GoodsHeadExportId && x.ShippingLineExportId == tariff.ShippingLineExportId 
                                                                && x.IndexCargoType == tariff.IndexCargoType && x.PortAndTerminalExportId == tariff.PortAndTerminalExportId).FirstOrDefault();
                if (res != null)
                {
                    var tariffinformationtariff = new TariffInofrmationTariffExport
                    {
                        TariffInformationExportId = res.TariffInformationExportId,
                        ExportTariffId = model.ExportTariffId

                    };

                    if (res.PercentAgeShippingAgentExportId == 0 && tariff.PercentAgeShippingAgentExportId != 0)
                    {
                        res.PercentAgeShippingAgentExportId = tariff.PercentAgeShippingAgentExportId;
                        _tariffInformationExportRepository.Update(res);

                    }

                    _tariffInofrmationTariffExportRepository.Add(tariffinformationtariff);
                }
                else
                {
                    _tariffInformationExportRepository.Add(tariff);

                    var tariffinformationtariff = new TariffInofrmationTariffExport
                    {
                        TariffInformationExportId = tariff.TariffInformationExportId,
                        ExportTariffId = model.ExportTariffId

                    };
                    _tariffInofrmationTariffExportRepository.Add(tariffinformationtariff);
                } 
                return new JsonResult(new { error = false, message = "Save " });

            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = "Please Create Again" });

                throw;
            }

        }


        public IActionResult SaveShareCondition(TerminalAndFFShareRateExport tariff)
        {
            try
            { 

                tariff.TotalAICTCBMRate = tariff.AICTCBMRate + tariff.FFCBMRate;
                tariff.TotalAICTPerIndexRate = tariff.AICTPerIndexRate + tariff.FFPerIndexRate;
                tariff.TotalAICTRate20 = tariff.AICTRate20 + tariff.FFRate20;
                tariff.TotalAICTRate40 = tariff.AICTRate40 + tariff.FFRate40;
                tariff.TotalAICTRate45 = tariff.AICTRate45 + tariff.FFRate45;

                _terminalAndFFShareRateExportRepository.Add(tariff);
               
                return new JsonResult(new { error = false, message = "Save " });

            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = "Please Create Again" });

                throw;
            }

        }

        public JsonResult findrecordbycode(long tariffCode)
        {
            var res = _tariffInformationExportRepository.GetAll().Where(x => x.TariffInformationExportId == tariffCode).LastOrDefault();

            return Json(res);
        }


        public IActionResult Deleterecordbycode(long tariffCode)
        {

            try
            {
                var result = _tariffInformationExportRepository.GetAll().Where(x => x.TariffInformationExportId == tariffCode).LastOrDefault();
                if (result != null)
                {
                    var res = _tariffInofrmationTariffExportRepository.GetAll().Where(x => x.TariffInformationExportId == result.TariffInformationExportId).ToList();

                    if (res.Count() > 0)
                    {
                        _tariffInofrmationTariffExportRepository.DeleteRange(res);
                        _tariffInformationExportRepository.Delete(result);

                        return new JsonResult(new { error = false, message = "Delete" });


                    }

                    _tariffInformationExportRepository.Delete(result);
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


        public IActionResult EnableDisableContract(long tariffCode)
        {
            try
            {
                var result = _tariffInformationExportRepository.GetAll().Where(x => x.TariffInformationExportId == tariffCode).LastOrDefault();
                if (result != null)
                {

                    if (result.IsEnableDisabled == false)
                    {

                        result.IsEnableDisabled = true;
                        _tariffInformationExportRepository.Update(result);

                        return new JsonResult(new { error = false, message = "contract disabled" });
                    }

                    if (result.IsEnableDisabled == true)
                    {
                        result.IsEnableDisabled = false;
                        _tariffInformationExportRepository.Update(result);

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

        public IActionResult Updaterecordbycode(long tariffCode, TariffInformationExport tariff)
        {
            try
            {
                var result = _tariffInformationExportRepository.GetAll().Where(x => x.TariffInformationExportId == tariffCode).LastOrDefault();
                if (result != null)
                {

                    var res = _tariffInformationExportRepository.GetAll().Where(x => x.ShippingAgentExportId == tariff.ShippingAgentExportId && x.ExportConsigneeId == tariff.ExportConsigneeId
                                                                    && x.ClearingAgentExportId == tariff.ClearingAgentExportId
                                                                    && x.GoodsHeadExportId == tariff.GoodsHeadExportId && x.ShippingLineExportId == tariff.ShippingLineExportId
                                                                    && x.IndexCargoType == tariff.IndexCargoType && x.PortAndTerminalExportId == tariff.PortAndTerminalExportId).FirstOrDefault();

                    if (res != null && result.TariffInformationExportId != res.TariffInformationExportId)
                    {
                        return new JsonResult(new { error = true, message = "Already Avaible" });

                    }
                    else
                    {
                        result.ShippingAgentExportId = tariff.ShippingAgentExportId;
                        result.ExportConsigneeId = tariff.ExportConsigneeId;
                        result.ClearingAgentExportId = tariff.ClearingAgentExportId;
                        result.GoodsHeadExportId = tariff.GoodsHeadExportId;
                        result.ShippingLineExportId = tariff.ShippingLineExportId;
                        result.IndexCargoType = tariff.IndexCargoType;
                        result.PortAndTerminalExportId = tariff.PortAndTerminalExportId;

                        _tariffInformationExportRepository.Update(result);

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


        public IActionResult SaveTariffConditionByTariffCode(long createdCode, TariffInformationExport tariff)
        {
            try
            {

                var tariffdata = new ExportTariff();

                var tariffres = _tariffInformationExportRepository.GetAll().Where(x => x.TariffInformationExportId == createdCode).LastOrDefault();
                var tariffinfotariffres = _tariffInofrmationTariffExportRepository.GetAll().Where(x => x.TariffInformationExportId == createdCode).ToList();

                if (tariffres != null && tariffinfotariffres.Count() > 0)
                {


                    foreach (var item in tariffinfotariffres)
                    {

                        var tariffitem = _exportTariffRepository.GetAll(x => x.TariffHeadExport).Where(x => x.ExportTariffId == item.ExportTariffId).LastOrDefault();

                        if (tariffitem != null)
                        { 
                            if (tariffitem.TariffHeadExport.Name.Contains("STORAGE"))
                            {
                                tariffdata.ExportTariffId = tariffitem.ExportTariffId;
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
                                tariffdata.TariffHeadExportId = tariffitem.TariffHeadExportId;
                                tariffdata.IsCBMorRate = tariffitem.IsCBMorRate;
                                tariffdata.ImplementTo = tariffitem.ImplementTo;
                                tariffdata.IsGeneral = tariffitem.IsGeneral;
                                tariffdata.DevededIndexAmount = tariffitem.DevededIndexAmount;
                                tariffdata.IsDollerRate = tariffitem.IsDollerRate;
                                tariffdata.PortAndTerminalExportId = tariffitem.PortAndTerminalExportId;
                                tariffdata.TypeOfTarifff = tariffitem.TypeOfTarifff;
                                tariffdata.IsFixedRate = tariffitem.IsFixedRate;
                                tariffdata.TypeOfImplementation = tariffitem.TypeOfImplementation;


                                _exportTariffRepository.Add(tariffdata);

                            }
                             

                        }


                        var res = _tariffInformationExportRepository.GetAll().Where(x => x.ShippingAgentExportId == tariff.ShippingAgentExportId && x.ExportConsigneeId == tariff.ExportConsigneeId 
                                                      && x.ClearingAgentExportId == tariff.ClearingAgentExportId
                                                      && x.GoodsHeadExportId == tariff.GoodsHeadExportId && x.ShippingLineExportId == tariff.ShippingLineExportId 
                                                      && x.IndexCargoType == tariff.IndexCargoType && x.PortAndTerminalExportId == tariff.PortAndTerminalExportId).FirstOrDefault();

                        if (res != null)
                        {
                            var tariffinformationtariff = new TariffInofrmationTariffExport
                            {
                                TariffInformationExportId = res.TariffInformationExportId,
                                ExportTariffId = tariffdata.ExportTariffId

                            };
                             

                            _tariffInofrmationTariffExportRepository.Add(tariffinformationtariff);
                        }
                        else
                        {
                            _tariffInformationExportRepository.Add(tariff);

                            var tariffinformationtariff = new TariffInofrmationTariffExport
                            {
                                TariffInformationExportId = tariff.TariffInformationExportId,
                                ExportTariffId = tariffdata.ExportTariffId

                            };
                            _tariffInofrmationTariffExportRepository.Add(tariffinformationtariff);
                        }


                        tariffdata = new ExportTariff();


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

        #endregion


        #region Export Storage

        public IActionResult AddTariffStorageDays(long tariffid, long? storageDays, long? dgfreeDays)
        {
            try
            {
                var res = _tariffInformationExportRepository.GetAll().Where(x => x.TariffInformationExportId == tariffid).LastOrDefault();

                if (res != null)
                {
                    res.StorageFreeDays = storageDays;
                    res.DGFreeDays = dgfreeDays;

                    _tariffInformationExportRepository.Update(res);
                    return new JsonResult(new { error = false, message = "Updated" });

                }
                else
                {
                    return new JsonResult(new { error = true, message = "no tariff code find" });
                }

            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = e.InnerException.InnerException });

                throw;
            }

        }


        #endregion
        //public IActionResult AgentTariffInformation()
        //{
        //    ViewData["ShippingAgents"] = _shippingAgentRepo.GetAll()
        //        .Select(v => new SelectListItem { Text = v.ShippingAgentName, Value = v.ShippingAgentExportId.ToString() });

        //    return View();
        //}

        //public IActionResult TariffExportCharges()
        //{
        //    ViewData["TariffHead"] = _tariffHeadExportRepo.GetAll()
        //   .Select(v => new SelectListItem { Text = v.Name, Value = v.TariffHeadExportId.ToString() });
        //    return View();
        //}

        //[HttpGet]
        //public JsonResult GetTariffExprots()
        //{
        //    var tariffs = _tariffExportRepo.GetAll(t => t.TariffHeadExport);

        //    return Json(tariffs);
        //}

        //public IActionResult updateAgentTariffExport(AgentTariffExportViewModel tariff)
        //{
        //    var shippingAgenttariff = _shippingAgentTariffRepo.Find(tariff.ShippingAgentTariffExportId);
        //    if(shippingAgenttariff != null)
        //    {
        //        shippingAgenttariff.CBMMultiply = tariff.CBMMultiply;
        //        shippingAgenttariff.CBMMultiplyValue = tariff.CBMMultiplyValue;
        //        _shippingAgentTariffRepo.Update(shippingAgenttariff);
        //    }


        //    return Ok();
        //}

        //public IActionResult SaveTariffExportInfo(TariffExport tariff)
        //{
        //    _tariffExportRepo.Add(tariff);

        //    return Ok();
        //}

        //public IActionResult RemoveGDTariffCargo(long TariffId, long cargoId)
        //{
        //    var gDTariffs = _GDTariffRepo.GetList(t => t.TariffExportId == TariffId && t.EnteringCargoId == cargoId);

        //    if (gDTariffs != null)
        //        _GDTariffRepo.DeleteRange(gDTariffs);

        //    return new OkObjectResult(new { message = "Tariff Removed" });
        //}


        //public IActionResult GDTariff()
        //{
        //    ViewData["TariffHead"] = _tariffHeadExportRepo.GetAll()
        //   .Select(v => new SelectListItem { Text = v.Name + " Type " + v.TariffHeadExportType, Value = v.TariffHeadExportId.ToString() });

        //    ViewData["NatureOfTariffs"] = _natureOfTariffRepository.GetAll().Select(v => new SelectListItem { Text = v.NatureOfTariffName, Value = v.NatureOfTariffId.ToString() });

        //    return View();
        //}

        //public IActionResult SaveAgentTariff(ShippingAgentTariffExport tariff)
        //{
        //    _shippingAgentTariffRepo.Add(tariff);

        //    return Ok();
        //}


        //public IActionResult AssignFromOneAgentToAnother(long shippingAgent , long ToshippingAgent)
        //{
        //    var shippingAgentTariffExports = new List<ShippingAgentTariffExport>();
        //    var data = _shippingAgentTariffRepo.GetAll().Where(x => x.ShippingAgentExportId == shippingAgent).ToList();

        //    if(data != null)
        //    {

        //        var dataSec = _shippingAgentTariffRepo.GetAll().Where(x => x.ShippingAgentExportId == ToshippingAgent).ToList();
        //        if(dataSec != null && dataSec.Count() > 0 )
        //        {
        //            _shippingAgentTariffRepo.DeleteRange(dataSec); 
        //        }

        //        foreach (var item in data)
        //        {
        //            var shippingAgentTariffExport = new ShippingAgentTariffExport()
        //            {
        //                ShippingAgentExportId = ToshippingAgent,
        //                TariffExportId = item.TariffExportId,
        //                CBMMultiply = item.CBMMultiply,
        //                CBMMultiplyValue = item.CBMMultiplyValue

        //            };

        //            shippingAgentTariffExports.Add(shippingAgentTariffExport);
        //        }

        //        _shippingAgentTariffRepo.AddRange(shippingAgentTariffExports);

        //    }


        //    return new JsonResult(new { error = false, message = "Saved" });

        //}

        ////change due to new tariif assigning Export page create old name is DeleteSaveTariffCondition new name is DeleteSaveTariffConditionold

        //public IActionResult DeleteSaveTariffCondition(long TariffId, long agentdropid)
        //{

        //    var tariff = _tariffExportRepo.GetAll().Where(x => x.TariffExportId == TariffId).LastOrDefault();

        //    try
        //    {
        //        if (tariff != null)
        //        {

        //            var tariffhead = _tariffHeadExportRepo.GetAll().Where(x => x.TariffHeadExportId == tariff.TariffHeadExportId).LastOrDefault();
        //            if (tariffhead != null)
        //            {
        //                if (tariffhead.Name.Contains("STORAGE") || tariffhead.TariffHeadExportType == "SlabWise")
        //                {
        //                    var agenttariff = _shippingAgentTariffExportRepository.GetAll().Where(x => x.TariffExportId == TariffId && x.ShippingAgentExportId == agentdropid).LastOrDefault();

        //                    if (agenttariff != null)
        //                    {
        //                        _shippingAgentTariffExportRepository.Delete(agenttariff);
        //                        return new JsonResult(new { error = false, message = "delete" });
        //                    }
        //                    else
        //                    {
        //                        return new JsonResult(new { error = false, message = "delete" });
        //                    }
        //                }
        //                else
        //                {
        //                    _tariffExportRepo.Delete(tariff);
        //                    return new JsonResult(new { error = false, message = "delete" });

        //                }


        //            }
        //            else
        //            {
        //                return new JsonResult(new { error = true, message = "tariff Head not found" });
        //            }


        //        }
        //        else
        //        {
        //            return new JsonResult(new { error = true, message = "Record Not Found" });

        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });


        //    }


        //}

        ////change due to new tariif assigning Export page create old name is SaveTariffCondition new name is SaveTariffConditionold
        //public IActionResult SaveTariffCondition(TariffExport model, long shippingagentid)
        //{
        //    try
        //    {
        //        try
        //        {


        //            if (model.TariffHeadExportId != null)
        //            {
        //                var tarif = _tariffExportRepo.GetAll(x => x.TariffHeadExport).Where(x => x.TariffHeadExportId == model.TariffHeadExportId).LastOrDefault();

        //                if (tarif != null)
        //                {
        //                    if (tarif.TariffHeadExport != null)
        //                    {
        //                        if (tarif.TariffHeadExport.Name.Contains("STORAGE") || tarif.TariffHeadExport.TariffHeadExportType == "SlabWise")
        //                        {
        //                            model.TariffExportId = tarif.TariffExportId;
        //                        }
        //                        else
        //                        {
        //                            _tariffExportRepo.Add(model);

        //                        }
        //                    }
        //                    else
        //                    {
        //                        _tariffExportRepo.Add(model);

        //                    }

        //                }
        //                else
        //                {
        //                    _tariffExportRepo.Add(model);

        //                }

        //            }
        //            else
        //            {
        //                return new JsonResult(new { error = true, message = "Please Select Tariff Head" });

        //            }


        //        }
        //        catch (Exception e)
        //        {

        //            return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });

        //        }

        //        var shippingAgentTariff = new ShippingAgentTariffExport
        //        {
        //            ShippingAgentExportId = shippingagentid,
        //            TariffExportId = model.TariffExportId

        //        };

        //        _shippingAgentTariffExportRepository.Add(shippingAgentTariff);



        //        return new JsonResult(new { error = false, message = "Save " });

        //    }
        //    catch (Exception e)
        //    {
        //        return new JsonResult(new { error = true, message = "Please Create Again" });

        //        throw;
        //    }

        //}


        //public IActionResult SaveTariffConditionByagent(long agentdropdown, long secagentdropdown)
        //{
        //    try
        //    {
        //        var tariffdata = new TariffExport();
        //        var tariffinfotariffres = _shippingAgentTariffExportRepository.GetAll().Where(x => x.ShippingAgentExportId == agentdropdown).ToList();

        //        if (tariffinfotariffres.Count() > 0)
        //        {
        //            foreach (var item in tariffinfotariffres)
        //            {
        //                var tariffitem = _tariffExportRepo.GetAll(x => x.TariffHeadExport).Where(x => x.TariffExportId == item.TariffExportId).LastOrDefault();

        //                if (tariffitem != null)
        //                {
        //                    //var tarif = _tariffHeadExportRepo.GetAll().Where(x => x.TariffHeadExportId == tariffitem.TariffHeadExportId).LastOrDefault();

        //                    //if (tarif != null)
        //                    //{

        //                    if (tariffitem.TariffHeadExport.Name.Contains("STORAGE") || tariffitem.TariffHeadExport.TariffHeadExportType == "SlabWise")
        //                    {
        //                        tariffdata.TariffExportId = tariffitem.TariffExportId;
        //                    }
        //                    else
        //                    {
        //                        tariffdata.TariffDate = tariffitem.TariffDate;
        //                        tariffdata.ImplementFrom = tariffitem.ImplementFrom;
        //                        tariffdata.Rate20 = tariffitem.Rate20;
        //                        tariffdata.Rate40 = tariffitem.Rate40;
        //                        tariffdata.Rate45 = tariffitem.Rate45;
        //                        tariffdata.CBMRate = tariffitem.CBMRate;
        //                        tariffdata.WeightRate = tariffitem.WeightRate;
        //                        tariffdata.PerIndexRate = tariffitem.PerIndexRate;
        //                        tariffdata.RoundCBMCode = tariffitem.RoundCBMCode;
        //                        tariffdata.DailyCharges = tariffitem.DailyCharges;
        //                        tariffdata.IsActive = tariffitem.IsActive;
        //                        tariffdata.TariffHeadExportId = tariffitem.TariffHeadExportId;
        //                        tariffdata.IsCBMorRate = tariffitem.IsCBMorRate;
        //                        tariffdata.CBMFixedRate = tariffitem.CBMFixedRate;
        //                        tariffdata.CBMMultiplyValue = tariffitem.CBMMultiplyValue;
        //                        tariffdata.NatureOfTariffId = tariffitem.NatureOfTariffId;
        //                        tariffdata.TariffType = tariffitem.TariffType;

        //                        _tariffExportRepo.Add(tariffdata);

        //                    }

        //                    var agenttariffres = new ShippingAgentTariffExport
        //                    {
        //                        ShippingAgentExportId = secagentdropdown,
        //                        TariffExportId = tariffdata.TariffExportId
        //                    };

        //                    _shippingAgentTariffExportRepository.Add(agenttariffres);

        //                    tariffdata = new TariffExport();

        //                    //}

        //                }
        //            }
        //            return new JsonResult(new { error = false, message = "save" });
        //        }
        //        else
        //        {
        //            return new JsonResult(new { error = true, message = "there is no any tariff" });
        //        }


        //    }
        //    catch (Exception e)
        //    {

        //        return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });

        //    }
        //}




        //public IActionResult updateAgentTariff(TariffExport tariff)
        //{
        //    try
        //    {
        //        _tariffExportRepo.Update(tariff);

        //        return new JsonResult(new { error = false, message = "updated" });
        //    }
        //    catch (Exception e)
        //    {

        //        return new JsonResult(new { error = true, message = "please try again" });
        //    }
        //}

        //public IActionResult RemoveAgentTariff(long TariffExportId, long AgentId)
        //{
        //    var agentTariff = _shippingAgentTariffRepo.GetFirst(t => t.TariffExportId == TariffExportId && t.ShippingAgentExportId == AgentId);

        //    if (agentTariff != null)
        //        _shippingAgentTariffRepo.Delete(agentTariff);

        //    return new OkObjectResult(new { message = "Tariff Removed" });
        //}

        //public IActionResult AddGDTariff(long EnteringCargoId)
        //{
        //    var GDTariff = new GDTariff
        //    {
        //        EnteringCargoId = EnteringCargoId
        //    };

        //    _GDTariffRepo.Add(GDTariff);
        //    return Ok();
        //}

        //public IActionResult SaveGDTariff(long EnteringCargoId, long tariffExportId)
        //{

        //    var tariff = new GDTariff
        //    {
        //        EnteringCargoId = EnteringCargoId,
        //        TariffExportId = tariffExportId
        //    };

        //    _GDTariffRepo.Add(tariff);

        //    return Ok();
        //}

        //public IActionResult RemoveIGMTariffBYId(long TariffExportId)
        //{
        //    var tariff = _tariffExportRepo.GetFirst(t => t.TariffExportId == TariffExportId);

        //    if (tariff != null)
        //        _tariffExportRepo.Delete(tariff);

        //    return new OkObjectResult(new { message = "Tariff Removed" });
        //}

        //[HttpGet]
        //public JsonResult GetCargoTariffs(long EnteringCargoId)
        //{
        //    var tariffs = _GDTariffRepo.GetCargoTariffs(EnteringCargoId);

        //    return Json(tariffs);
        //}

        //public IActionResult StorageTariffExport()
        //{
        //    var tariffs = _tariffExportRepo.GetList(t => t.TariffHeadExport != null && t.TariffHeadExport.Name.ToUpper().Contains("STORAGE"), t => t.TariffHeadExport);
        //    ViewData["Tariffs"] = tariffs
        //     .Select(v => new SelectListItem { Text = v.TariffHeadExport.Name, Value = v.TariffExportId.ToString() });
        //    return View();
        //}


        //[HttpPost]
        //public IActionResult SaveExtraFreeDays(long freedays, long tariffExportId)
        //{
        //    var slabs = _storageSlabExportRepo.GetList(s => s.ExportTariffId == tariffExportId);

        //    foreach (var item in slabs)
        //    {
        //        item.FreeDays = Convert.ToInt32(freedays);
        //    }

        //    _storageSlabExportRepo.UpdateRange(slabs);

        //    return Ok();
        //}

        //[HttpGet]
        //public JsonResult GetStorageSlabsExportByTariff(long TariffExportId)
        //{
        //    var slabs = _storageSlabExportRepo.GetList(c => c.ExportTariffId == TariffExportId);

        //    return Json(slabs);
        //}

        //public int GetExtraFreeDays(long tariffExportId)
        //{
        //    var slabs = _storageSlabExportRepo.GetList(s => s.ExportTariffId == tariffExportId);

        //    if (slabs.Count() > 0)
        //        return slabs.First().FreeDays;

        //    return 0;
        //}

        //[HttpPost]
        //public IActionResult AddStorageSlabExport(StorageSlabExport slab)
        //{
        //    _storageSlabExportRepo.Add(slab);

        //    return Ok();

        //}

        //public IActionResult TariffInformationExportView()

        //{
        //    ViewData["ShippingAgents"] = _shippingAgentRepo.GetAll()
        //    .Select(v => new SelectListItem { Text = v.ShippingAgentName, Value = v.ShippingAgentExportId.ToString() });

        //    ViewData["TariffHead"] = _tariffHeadExportRepo.GetAll()
        //  .Select(v => new SelectListItem { Text = v.Name + " Type " + v.TariffHeadExportType, Value = v.TariffHeadExportId.ToString() });

        //    ViewData["NatureOfTariffs"] = _natureOfTariffRepository.GetAll().Select(v => new SelectListItem { Text = v.NatureOfTariffName, Value = v.NatureOfTariffId.ToString() });

        //    return View();

        //}


        //public IActionResult SaveTariffConditionForGDtariff(TariffExport model, string gdnumber)
        //{
        //    try
        //    {
        //        var enteringcargo = _enteringCargoRepository.GetEnteringCargo(gdnumber);
        //        if (enteringcargo == null)
        //        {
        //            return new JsonResult(new { error = true, message = "entering cargo not found" });
        //        }

        //        try
        //        {


        //            if (model.TariffHeadExportId != null)
        //            {
        //                var tarif = _tariffExportRepo.GetAll(x => x.TariffHeadExport).Where(x => x.TariffHeadExportId == model.TariffHeadExportId).LastOrDefault();

        //                if (tarif != null)
        //                {
        //                    if (tarif.TariffHeadExport != null)
        //                    {
        //                        if (tarif.TariffHeadExport.Name.Contains("STORAGE") || tarif.TariffHeadExport.TariffHeadExportType == "SlabWise")
        //                        {
        //                            model.TariffExportId = tarif.TariffExportId;
        //                        }
        //                        else
        //                        {
        //                            _tariffExportRepo.Add(model);

        //                        }
        //                    }
        //                    else
        //                    {
        //                        _tariffExportRepo.Add(model);

        //                    }

        //                }
        //                else
        //                {
        //                    _tariffExportRepo.Add(model);

        //                }

        //            }
        //            else
        //            {
        //                return new JsonResult(new { error = true, message = "Please Select Tariff Head" });

        //            }


        //        }
        //        catch (Exception e)
        //        {

        //            return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });

        //        }

        //        var contaienrtariff = new GDTariff
        //        {
        //            EnteringCargoId = enteringcargo.EnteringCargoId,
        //            TariffExportId = model.TariffExportId
        //        };

        //        _GDTariffRepo.Add(contaienrtariff);



        //        return new JsonResult(new { error = false, message = "Save " });

        //    }
        //    catch (Exception e)
        //    {
        //        return new JsonResult(new { error = true, message = "Please Create Again" });

        //        throw;
        //    }

        //}

        //public IActionResult RemoveGDTariff(long TariffId, string gdnumber)
        //{



        //    var enteringcargo = _enteringCargoRepository.GetEnteringCargo(gdnumber);
        //    if (enteringcargo != null)
        //    {

        //        var invoicelist = _enteringCargoRepository.GetInvoicesBygdnumber(enteringcargo.EnteringCargoId);

        //        if (invoicelist.Count() > 0)
        //        {
        //            foreach (var item in invoicelist)
        //            {
        //                var invoiceitem = _enteringCargoRepository.GetInvoiceExportitem(item.InvoiceExportId, TariffId);

        //                if (invoiceitem != null)
        //                {
        //                    return new OkObjectResult(new { error = true, message = "Can't Delete Duw To Invoice Created" });
        //                }
        //            }

        //        }


        //        var indexTariff = _GDTariffRepo.GetFirst(t => t.TariffExportId == TariffId && t.EnteringCargoId == enteringcargo.EnteringCargoId);

        //        if (indexTariff != null)
        //        {
        //            _GDTariffRepo.Delete(indexTariff);
        //            return new OkObjectResult(new { error = false, message = "delete" });
        //        }
        //        else
        //        {
        //            return new OkObjectResult(new { error = true, message = "tariff not found in gd tariff" });
        //        }




        //    }
        //    else
        //    {
        //        return new OkObjectResult(new { error = true, message = "gd not found" });
        //    }


        //}

        //public IActionResult SaveDisabledAgentTariffExport(string gdnumber, long agentTariffId, long tariffexportid)
        //{
        //    if (gdnumber != null)
        //    {

        //        var cargo = _enteringCargoRepository.GetEnteringCargo(gdnumber);

        //        if (cargo != null)
        //        {
        //            var invoicelist = _enteringCargoRepository.GetInvoicesBygdnumber(cargo.EnteringCargoId);

        //            if (invoicelist.Count() > 0)
        //            {
        //                foreach (var item in invoicelist)
        //                {
        //                    var invoiceitem = _enteringCargoRepository.GetInvoiceExportitem(item.InvoiceExportId, tariffexportid);

        //                    if (invoiceitem != null)
        //                    {
        //                        return new OkObjectResult(new { error = true, message = "Can't Delete Duw To Invoice Created" });
        //                    }
        //                }

        //            }

        //            var result = _disabledAgentTariffExportRepository.GetDisabledAgentTariffExport(cargo.EnteringCargoId, agentTariffId);
        //            if (result != null)
        //            {
        //                return new JsonResult(new { error = true, message = "Already Removed !" });

        //            }
        //            else
        //            {
        //                var DisabledAgentTariff = new DisabledAgentTariffExport
        //                {
        //                    ShippingAgentTariffExportId = agentTariffId,
        //                    EnteringCargoId = cargo.EnteringCargoId
        //                };

        //                _disabledAgentTariffExportRepository.Add(DisabledAgentTariff);
        //                return new JsonResult(new { error = false, message = "Agent Tariff Remove For This Index" });

        //            }

        //        }

        //        return new JsonResult(new { error = true, message = "GD Not Found" });

        //    }

        //    return new JsonResult(new { error = true, message = "GD Not Found" });



        //}

        //public IActionResult RefershAgentTariff(string gdnumber)
        //{
        //    if (gdnumber != null)
        //    {

        //        var cargo = _enteringCargoRepository.GetEnteringCargo(gdnumber);

        //        if (cargo != null)
        //        {


        //            var data = _disabledAgentTariffExportRepository.GetAll().Where(x => x.EnteringCargoId == cargo.EnteringCargoId).ToList();

        //            _disabledAgentTariffExportRepository.DeleteRange(data);

        //            return new JsonResult(new { error = false, message = " Your Agent Tariff Refersh ." });

        //        }

        //        return new JsonResult(new { error = true, message = "GD Not Found" });

        //    }




        //    return new JsonResult(new { error = true, message = "GD Not Found" });



        //}


        //public IActionResult SaveTariffTariffTypeForLP(long tarftype, string gdnumber)
        //{
        //    try
        //    {


        //        var lp = _loadingProgramRepository.GetLoadingProgrambygdnumber(gdnumber);
        //        if (lp != null)
        //        {
        //            lp.NatureOfTariffId = tarftype;
        //            _loadingProgramRepository.Update(lp);

        //            return new JsonResult(new { error = false, message = "Save " });
        //        }
        //        else
        //        {
        //            return new JsonResult(new { error = true, message = "no Lp found" });
        //        }






        //    }
        //    catch (Exception e)
        //    {
        //        return new JsonResult(new { error = true, message = "Please Create Again" });

        //        throw;
        //    }

        //}

        //public IActionResult NatureOfTariffView()
        //{
        //    return View();
        //}

        //public JsonResult GetNatureOfTariff()
        //{
        //    var res = _natureOfTariffRepository.GetAll();

        //    return Json(res);
        //}


        //public IActionResult AddNatureOfTariff(NatureOfTariff values)
        //{
        //    try
        //    {
        //        _natureOfTariffRepository.Add(values);

        //        return new JsonResult(new { error = false, message = "Save" });
        //    }
        //    catch (Exception e)
        //    {
        //        return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
        //    }

        //}


        //public IActionResult UpdateNatureOfTariff(NatureOfTariff values)
        //{
        //    try
        //    {
        //        _natureOfTariffRepository.Update(values);

        //        return new JsonResult(new { error = false, message = "Update" });

        //    }
        //    catch (Exception e)
        //    {
        //        return new JsonResult(new { error = false, message = e.InnerException.InnerException.Message });
        //    }
        //}


        //public IActionResult TariffRateSlabWiseExport()
        //{
        //    var tariffs = _tariffExportRepo.GetList(t => t.TariffHeadExport != null && t.TariffHeadExport.TariffHeadExportType == "SlabWise", t => t.TariffHeadExport);
        //    ViewData["Tariffs"] = tariffs
        //     .Select(v => new SelectListItem { Text = v.TariffHeadExport.Name, Value = v.TariffExportId.ToString() });

        //    return View();
        //}

        //[HttpGet]
        //public JsonResult GetlabsWiseRateExportByTariff(long TariffExportId)
        //{
        //    var slabs = _tariffRateSlabWiseRepository.GetList(c => c.TariffExportId == TariffExportId);

        //    return Json(slabs);
        //}


        //[HttpPost]
        //public IActionResult AddSlabWiseTariffExport(TariffRateSlabWise slab)
        //{
        //    slab.CreatedDate = DateTime.Now;

        //    _tariffRateSlabWiseRepository.Add(slab);

        //    return new JsonResult(new { error = false, message = "Save" });


        //}


        //[HttpPost]
        //public IActionResult UpdateSlabWiseTariffExport(TariffRateSlabWise slab)
        //{
        //    _tariffRateSlabWiseRepository.Update(slab);
        //    return new JsonResult(new { error = false, message = "Updated" });

        //}

        //public IActionResult DeleteParty(long key)
        //{
        //    var res = _tariffRateSlabWiseRepository.Find(key);

        //    _tariffRateSlabWiseRepository.Delete(res);

        //    return new JsonResult(new { error = false, message = "Deleted" });

        //}

        //public IActionResult ExtraFreeDays()
        //{
        //    ViewData["GDNumbers"] = _tariffExportRepo.GetUnDeliveredGDS()
        //    .Select(s => new SelectListItem { Text = s.GDNumber, Value = s.GDNumber.ToString() });

        //    return View();
        //}

        //[HttpPost]
        //public IActionResult UpdateExtraFreeDays(string gdnumber, int additionalfreedays, long waiverAmount, string approvedBy, string extraFreeDaysRemarks)
        //{

        //    var cargo = _enteringCargoRepository.GetEnteringCargo(gdnumber);


        //    if (cargo == null)
        //    {
        //        return new JsonResult(new { error = true, message = "index detail not found" });

        //    }
        //    else
        //    {


        //        if (additionalfreedays > 0 && waiverAmount > 0)
        //        {
        //            return new JsonResult(new { error = true, message = "can't insert both fields at a time" });
        //        }

        //        if (additionalfreedays <= 0 && waiverAmount <= 0)
        //        {
        //            return new JsonResult(new { error = true, message = "please fill one field" });
        //        }


        //        var invoices = _enteringCargoRepository.GetInvoicesBygdnumber(cargo.EnteringCargoId).ToList();

        //        if (invoices.Count() > 0)
        //        {
        //            return new JsonResult(new { error = true, message = "can't update due to invoice created" });
        //        }




        //        if (cargo != null)
        //        {

        //            if (additionalfreedays > 0 && cargo.WaiverAmount > 0)
        //            {
        //                return new JsonResult(new { error = true, message = "alreday assign waiver" });

        //            }

        //            if (waiverAmount > 0 && cargo.AdditionalFreeDays > 0)
        //            {
        //                return new JsonResult(new { error = true, message = "alreday assign Additional Free Days" });

        //            }


        //            if (additionalfreedays > 0 && cargo.WaiverAmount <= 0)
        //            {
        //                cargo.AdditionalFreeDays = additionalfreedays;
        //                cargo.ApprovedBy = approvedBy;
        //                cargo.ExtraFreeDaysRemarks = extraFreeDaysRemarks;
        //                _enteringCargoRepository.Update(cargo);
        //                return new JsonResult(new { error = false, message = "Additional Free Days  Updated" });

        //            }

        //            if (waiverAmount > 0 && (cargo.AdditionalFreeDays <= 0 || cargo.AdditionalFreeDays == null))
        //            {
        //                cargo.WaiverAmount = waiverAmount;
        //                cargo.ApprovedBy = approvedBy;
        //                cargo.ExtraFreeDaysRemarks = extraFreeDaysRemarks;
        //                _enteringCargoRepository.Update(cargo);
        //                return new JsonResult(new { error = false, message = "Waiver Amount Updated" });

        //            }
        //        }
        //        else
        //        {
        //            return new JsonResult(new { error = true, message = "index not found" });

        //        }



        //        //var cycontainer = _cyContainerRepo.GetFirst(x => x.CYContainerId == cyContainerId);
        //        //cycontainer.AdditionalFreeDays = additionalfreedays;
        //        //_cyContainerRepo.Update(cycontainer);
        //    }

        //    return Ok();
        //}


    }
}