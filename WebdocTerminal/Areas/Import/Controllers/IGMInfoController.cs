using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class IGMInfoController : ParentController
    {
        private IVesselRepository _vesselRepo;
        private IVoyageRepository _voyageRepo;
        private IGoodRepository _goodRepo;
        private IConsigneRepository _consigneRepo;
        private IShippingLineRepository _shippingLineRepo;
        private IShippingAgentRepository _shippingAgentRepo;
        private IShipperRepository _shipperRepo;
        private IPortAndTerminalRepository _portAndTerminalRepo;
        private IContainerIndexRepository _containerIndexRepo;
        private ICYContainerRepository _cycontainerRepo;
        private IGoodsHeadRepository _goodsHeadRepository;
        private IContainerSizeAmountRepository _containerSizeAmountRepo;
        private IISOCodeRepository _isocodeRepo;
        private IIGMORepository _igmoRepository; 
        private IPortOfLoadingRepository _portOfLoadingRepository; 
        private IInvoiceRepository _invoiceRepository; 




        public IGMInfoController(IVesselRepository vesselRepo,
                                 IVoyageRepository voyageRepo,
                                  IGoodRepository goodRepo,
                                  IConsigneRepository consigneRepo,
                                  IShippingLineRepository shippingLineRepo,
                                  IShippingAgentRepository shippingAgentRepo,
                                  IShipperRepository shipperRepo,
                                  IPortAndTerminalRepository portAndTerminalRepo,
                                  IContainerIndexRepository containerIndexRepo,
                                  ICYContainerRepository cycontainerRepo,
                                  IGoodsHeadRepository goodsHeadRepository,
                                  IContainerSizeAmountRepository containerSizeAmountRepo,
                                  IISOCodeRepository isocodeRepo,
                                  IIGMORepository igmoRepository,
                                  IPortOfLoadingRepository portOfLoadingRepository,
                                  IInvoiceRepository invoiceRepository)
        {
            _vesselRepo = vesselRepo;
            _voyageRepo = voyageRepo;
            _goodRepo = goodRepo;
            _consigneRepo = consigneRepo;
            _shippingLineRepo = shippingLineRepo;
            _shippingAgentRepo = shippingAgentRepo;
            _shipperRepo = shipperRepo;
            _portAndTerminalRepo = portAndTerminalRepo;
            _containerIndexRepo = containerIndexRepo;
            _cycontainerRepo = cycontainerRepo;
            _goodsHeadRepository = goodsHeadRepository;
            _containerSizeAmountRepo = containerSizeAmountRepo;
            _isocodeRepo = isocodeRepo;
            _igmoRepository = igmoRepository;
            _portOfLoadingRepository = portOfLoadingRepository;
            _invoiceRepository = invoiceRepository;
        }

        public IActionResult IGMDetailView()
        {


            ViewData["Goods"] = _goodsHeadRepository.GetAll()
              .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });

            //ViewData["Consigne"] = _consigneRepo.GetAll()
            //  .Select(v => new SelectListItem { Text = v.ConsigneName, Value = v.ConsigneId.ToString() });

            ViewData["ShippingLine"] = _shippingLineRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.ShippingLineName, Value = v.ShippingLineId.ToString() });

            ViewData["ShippingAgent"] = _shippingAgentRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["Shipper"] = _shipperRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.ShipperName, Value = v.ShipperId.ToString() });

            ViewData["PortAndTerminal"] = _portAndTerminalRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.PortName, Value = v.PortAndTerminalId.ToString() });


            ViewData["PortOfLoading"] = _portOfLoadingRepository.GetAll()
              .Select(v => new SelectListItem { Text = v.PortOfLoadingName, Value = v.PortOfLoadingId.ToString() });


            return View();
        }


        public IActionResult IGMUploading()
        {

            ViewData["Goods"] = _goodsHeadRepository.GetAll()
              .Select(v => new SelectListItem { Text = v.GoodsHeadName, Value = v.GoodsHeadId.ToString() });

            ViewData["ShippingLine"] = _shippingLineRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.ShippingLineName, Value = v.ShippingLineId.ToString() });

            ViewData["ShippingAgent"] = _shippingAgentRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["Shipper"] = _shipperRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.ShipperName, Value = v.ShipperId.ToString() });

            ViewData["PortAndTerminal"] = _portAndTerminalRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.PortName, Value = v.PortAndTerminalId.ToString() });


            return View();
        }

        public IActionResult UpdateContaierIndexDetail(double measurementCBM, long consigneId, string cargotype, double manifestedWeight, long containerId , bool isDGCargo ,   bool ispart)
        {
            try
            {
               // var result = _containerIndexRepo.GetAll().Where(x => x.ContainerIndexId == containerId).LastOrDefault();
                var result = _containerIndexRepo.GetContainerIndexById(containerId);

                var res = _containerIndexRepo.GetIndexInfo(result.VirNo , result.IndexNo ?? 0).ToList();


                //var invoiceres = _invoiceRepository.GetAll().Where(x => res.Any(y => y.ContainerIndexId == x.ContainerIndexId)).ToList();

                var containerindexidlist = string.Join(",", res.Select(n => n.ContainerIndexId.ToString()).ToArray());

                var invoiceres = _invoiceRepository.GetCfsInvoices(containerindexidlist).ToList();

                if (invoiceres.Count() > 0)
                {
                    return new JsonResult(new { error = true, message = "can't update due to invoice created agains't IGM : " + res[0].VirNo + " and INDEX " + res[0].IndexNo });
                }


                if (result != null)
                {
                    if(consigneId == 0)
                    {
                        result.ConsigneId = null;
                    }
                    else
                    {
                        result.ConsigneId = consigneId;
                    }


                    if (ispart == true)
                    {
                        var resultlist = _containerIndexRepo.GetContainerIndexByIGMAndContainerNo(result.VirNo , result.ContainerNo).ToList();
                        if(resultlist.Count() > 0)
                        {
                            resultlist.ForEach(x => x.IsPartialContainer = true);

                            _containerIndexRepo.UpdateRange(resultlist);

                        }
                    }
                    else
                    {
                        var resultlist = _containerIndexRepo.GetContainerIndexByIGMAndContainerNo(result.VirNo, result.ContainerNo).ToList();
                        if (resultlist.Count() > 0)
                        {
                            resultlist.ForEach(x => x.IsPartialContainer = false);

                            _containerIndexRepo.UpdateRange(resultlist);

                        }
                    }

                    result.MeasurementCBM = measurementCBM;
                    result.CargoType = cargotype;
                    result.ManifestedWeight = manifestedWeight;
                   
                    result.IsDGCargo = isDGCargo;

                    _containerIndexRepo.Update(result);



                }



                return new JsonResult(new { error = false, message = "Saved" });
            }
            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = "please try again" });
            }



        }


        public IActionResult UpdateContaierIndexDetailCY(string cargotype, long containerId  , bool ispart)
        {
            try
            {
                //var result = _cycontainerRepo.GetAll().Where(x => x.CYContainerId == containerId).LastOrDefault();
                var result = _cycontainerRepo.GetContainerById( containerId);

                var res = _cycontainerRepo.GetUndeliveredIndexbyigmindex( result.VIRNo , result.IndexNo ?? 0).ToList();


                //var invoiceres = _invoiceRepository.GetAll().Where(x => res.Any(y => y.CYContainerId == x.CYContainerId)).ToList();
                var containerindexidlist = string.Join(",", res.Select(n => n.CYContainerId.ToString()).ToArray());

                var invoiceres = _invoiceRepository.GetCYInvoices(containerindexidlist).ToList();

                if (invoiceres.Count() > 0)
                {
                    return new JsonResult(new { error = true, message = "can't update due to invoice created agains't IGM : " + res[0].VIRNo + " and INDEX " + res[0].IndexNo });
                }



                if (result != null)
                {
                    result.CargoType = cargotype;                       
                    _cycontainerRepo.Update(result);

                }


                if (ispart == true)
                {
                    var resultlist = _cycontainerRepo.GetContainerIndexByIGMAndContainerNo(result.VIRNo, result.ContainerNo).ToList();
                    if (resultlist.Count() > 0)
                    {
                        resultlist.ForEach(x => x.IsPartialContainer = true);

                        _cycontainerRepo.UpdateRange(resultlist);

                    }
                }
                else
                {
                    var resultlist = _cycontainerRepo.GetContainerIndexByIGMAndContainerNo(result.VIRNo, result.ContainerNo).ToList();
                    if (resultlist.Count() > 0)
                    {
                        resultlist.ForEach(x => x.IsPartialContainer = false);

                        _cycontainerRepo.UpdateRange(resultlist);

                    }
                }



                return new JsonResult(new { error = false, message = "Saved" });
            }
            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = "please try again" });

            } 

        }


        public IActionResult UpdateContaierIndexDetailMultiple(List<ContainerIndex> containers)
        {
            try
            {
                var containerindexids = string.Join(",", containers.Select(n => n.ContainerIndexId.ToString()).ToArray());

                var resdata = _containerIndexRepo.GetContainerIndexByIds(containerindexids).Select(x => new SelectListItem { Text = x.VirNo, Value = x.IndexNo.ToString() }).GroupBy(x => x.Value).Select(x => x.FirstOrDefault()).ToList();
                 
                foreach (var item in resdata)
                {
                    var topindexes = _containerIndexRepo.GetIndexInfo( item.Text , Convert.ToInt64(item.Value)).ToList();

                    var containerindexidlist = string.Join(",", topindexes.Select(n => n.ContainerIndexId.ToString()).ToArray());

                    var invoiceres = _invoiceRepository.GetCfsInvoices(containerindexidlist).ToList();

                    if (invoiceres.Count() > 0)
                    {
                        return new JsonResult(new { error = true, message = "invoice created agains't IGM : " + item.Text +" and INDEX " + item.Value });
                    }
                }



                var listres = new List<ContainerIndex>();

                foreach (var item in containers)
                {
                    var result = _containerIndexRepo.GetContainerIndexById(item.ContainerIndexId);

                    if (result != null)
                    {
                        result.ConsigneId = item.ConsigneId;
                        result.CargoType = item.CargoType;

                        listres.Add(result);

                    }
                }

                _containerIndexRepo.UpdateRange(listres);

                return new JsonResult(new { error = false, message = "Saved" });
            }
            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = "please try again" });

            }



        }




        public IActionResult UpdateContaierDetail(ContainerIndexDetail model, string containertype, string VirNo, string ContainerNo, long? consid)
        {

            var ContainerIndexList = new List<ContainerIndex>();
            var CYContainerList = new List<CYContainer>();
            var cNo = model.ContainerNo.Split(",").ToList();
            var iNo = model.ContainerNo.Split(",").ToList();


            if (containertype == "CFSIndexWise")
            {

                foreach (var itmIndex in iNo)
                {
                    var containerindexinfo = _containerIndexRepo.GetContainerIndexById(Convert.ToInt64(itmIndex));

                    if (containerindexinfo != null)
                    {
                        var resultindexes = _containerIndexRepo.GetIndexInfo(containerindexinfo.VirNo, Convert.ToInt64(containerindexinfo.IndexNo));

                        if (resultindexes.Count() > 0)
                        {
                            var containerindexidlist = string.Join(",", resultindexes.Select(n => n.ContainerIndexId.ToString()).ToArray());

                            var invoiceres = _invoiceRepository.GetCfsInvoices(containerindexidlist).ToList();

                            if (invoiceres.Count() > 0)
                            {
                                return new JsonResult(new { error = true, message = "invoice created agains't IGM : " + containerindexinfo.VirNo + " and INDEX " + containerindexinfo.IndexNo });
                            }
                        }
                    }
                }
                foreach (var itmIndex in iNo)
                {
                    //var result = _containerIndexRepo.GetAll().Where(x => x.VirNo == model.VirNo && x.IndexNo == Convert.ToInt32(itmIndex)).ToList();

                    // var result = _containerIndexRepo.GetAll().Where(x => x.VirNo == model.VirNo && x.ContainerIndexId.ToString() == itmIndex).ToList();
                    //if (result.Count() > 0)
                    //{
                    //    foreach (var item in result)
                    //    {
                    //        item.ShipperId = model.ShipperId;
                    //        //item.MeasurementCBM = model.MeasurementCBM;
                    //        item.PortAndTerminalId = model.PortAndTerminalId;
                    //        item.ShippingAgentId = model.ShippingAgentId;
                    //        item.GoodsHeadId = model.GoodsHeadId;
                    //        item.Size = GetSizeByISOCode(item.ISOCode);
                    //        item.ShippingLineId = model.ShippingLineId;
                    //        item.IsPartialContainer = model.IsPartialContainer;
                    //        ContainerIndexList.Add(item);
                    //    }
                    //}


                    //var result = _containerIndexRepo.GetContainerIndexById(Convert.ToInt64( itmIndex));


                    //if (result != null)
                    //{

                    //    result.ShipperId = model.ShipperId;
                    //    //item.MeasurementCBM = model.MeasurementCBM;
                    //    result.PortAndTerminalId = model.PortAndTerminalId;
                    //    result.ShippingAgentId = model.ShippingAgentId;
                    //    result.GoodsHeadId = model.GoodsHeadId;
                    //    result.Size = GetSizeByISOCode(result.ISOCode);
                    //    result.ShippingLineId = model.ShippingLineId;
                    //    result.IsPartialContainer = model.IsPartialContainer;
                    //    ContainerIndexList.Add(result);
                    //}



                    var containerindexinfo = _containerIndexRepo.GetContainerIndexById(Convert.ToInt64(itmIndex));

                    if (containerindexinfo != null)
                    {
                        var resultindexes = _containerIndexRepo.GetIndexInfo(containerindexinfo.VirNo, Convert.ToInt64(containerindexinfo.IndexNo));

                        if (resultindexes.Count() > 0)
                        {
                            foreach (var item in resultindexes)
                            {
                                item.ShipperId = model.ShipperId != null ? model.ShipperId : 30098;
                                item.PortOfLoadingId = model.PortOfLoadingId != null ? model.PortOfLoadingId : 1;
                                //item.MeasurementCBM = model.MeasurementCBM;
                                item.PortAndTerminalId = model.PortAndTerminalId;
                                item.ShippingAgentId = model.ShippingAgentId;
                                item.GoodsHeadId = model.GoodsHeadId;
                                item.ShippingLineId = model.ShippingLineId;
                                item.Size = GetSizeByISOCode(item.ISOCode);
                                //item.IsPartialContainer = model.IsPartialContainer;
                                item.IsDGCargo = model.IsDGCargo;
                                ContainerIndexList.Add(item);

                            }
                        }
                    }



                }
            }

            if (containertype == "CFSContainerWise")
            {

                foreach (var itm in cNo)
                {
                    var resdata = _containerIndexRepo.GetContainerIndexByIGMAndContainerNo(model.VirNo, itm.Trim()).Select(x => new SelectListItem { Text = x.VirNo, Value = x.IndexNo.ToString() }).GroupBy(x => x.Value).Select(x => x.FirstOrDefault()).ToList();

                    foreach (var item in resdata)
                    {
                        var topindexes = _containerIndexRepo.GetIndexInfo(item.Text, Convert.ToInt64(item.Value)).ToList();

                        var containerindexidlist = string.Join(",", topindexes.Select(n => n.ContainerIndexId.ToString()).ToArray());

                        var invoiceres = _invoiceRepository.GetCfsInvoices(containerindexidlist).ToList();

                        if (invoiceres.Count() > 0)
                        {
                            return new JsonResult(new { error = true, message = "invoice created agains't IGM : " + item.Text + " and INDEX " + item.Value });
                        }
                    }
                }


                foreach (var itm in cNo)
                {

                   // var result = _containerIndexRepo.GetAll().Where(x => x.VirNo == model.VirNo && x.ContainerNo == itm.Trim()).ToList();
                    var result = _containerIndexRepo.GetContainerIndexByIGMAndContainerNo(model.VirNo , itm.Trim()).ToList();

                    if (result.Count() > 0)
                    {
                        foreach (var item in result)
                        {
                            item.ShipperId = model.ShipperId != null ? model.ShipperId : 30098;
                            item.PortOfLoadingId = model.PortOfLoadingId != null ? model.PortOfLoadingId : 1;
                            //item.MeasurementCBM = model.MeasurementCBM;
                            item.PortAndTerminalId = model.PortAndTerminalId;
                            item.ShippingAgentId = model.ShippingAgentId;
                            item.GoodsHeadId = model.GoodsHeadId;
                            item.ShippingLineId = model.ShippingLineId;
                            item.Size = GetSizeByISOCode(item.ISOCode);
                            //item.IsPartialContainer = model.IsPartialContainer;
                            item.IsDGCargo = model.IsDGCargo;

                            ContainerIndexList.Add(item);

                        }
                    }
                }
            }
            if (containertype == "CYContainers")
            {
 
                foreach (var itm in iNo)
                {
                    var topindexes = _cycontainerRepo.GetContainerIndexByIGMAndContainerNo(model.VirNo, Convert.ToInt32(itm)).ToList();

                     var containerindexidlist = string.Join(",", topindexes.Select(n => n.CYContainerId.ToString()).ToArray());

                    var invoiceres = _invoiceRepository.GetCYInvoices(containerindexidlist).ToList();

                    if (invoiceres.Count() > 0)
                    {
                        return new JsonResult(new { error = true, message = "invoice created agains't IGM : " + model.VirNo + " and INDEX " + itm });
                    }
                }



                foreach (var itm in iNo)
                {
                   // var result = _cycontainerRepo.GetAll().Where(x => x.VIRNo == model.VirNo && x.IndexNo.ToString().Trim() == itm.ToString().Trim()).ToList();
                    var result = _cycontainerRepo.GetContainerIndexByIGMAndContainerNo(model.VirNo,  Convert.ToInt32( itm )).ToList();


                    if (result.Count() > 0)
                    {
                        foreach (var item in result)
                        {
                            item.ShipperId = model.ShipperId != null ? model.ShipperId : 30098;
                            item.PortOfLoadingId = model.PortOfLoadingId != null ? model.PortOfLoadingId : 1;
                            item.PortAndTerminalId = model.PortAndTerminalId;
                            item.ShippingAgentId = model.ShippingAgentId;
                            item.ConsigneId = consid;
                            item.GoodsHeadId = model.GoodsHeadId;
                            item.ShippingLineId = model.ShippingLineId;
                            //item.IsPartialContainer = model.IsPartialContainer;
                            item.Size = GetSizeByISOCode(item.ISOCode);
                            item.IsDGCargo = model.IsDGCargo;
                            CYContainerList.Add(item);

                        }
                    }
                }
            }

            _containerIndexRepo.UpdateRange(ContainerIndexList);
            _cycontainerRepo.UpdateRange(CYContainerList);

            return new JsonResult(new { error = false, message = "Saved" });
            //}
            //else
            //{
            //    return new JsonResult(new { error = true, message = "Container number not found" });
            //}
        }


        public int GetSizeByISOCode(string isocode)
        {
            var data = _isocodeRepo.GetAll().Where(x => x.Code == isocode).FirstOrDefault();
            if (data != null)
            {
                return Convert.ToInt32(data.ContainerSize);
            }
            else
            {
                return 0;
            }

        }

        public JsonResult GetConsignees()
        {
            var consignees = _consigneRepo.GetAll().Select(x => new SelectListItem { Text = x.ConsigneName, Value = x.ConsigneId.ToString() }).ToList();

            return Json(consignees);
        }

        public IActionResult UpdateIGMO(string igm, List<AICT_TEMP_DAT_FILE_CONTAINER> model)
        {
            try
            {

                string connectionString = "Data Source=192.168.1.6;Initial Catalog=Tosdb_Uat;User ID=ovais;Password=khan321--";

                var igmos = new List<IGMO>();
                var ConainerIndexes = new List<ContainerIndex>();

                foreach (var item in model)
                {
                    long? consigneeid = null;
                    if (item.PARTY_NAME != null || item.PARTY_NAME != "")
                    {
                       var consres = _consigneRepo.GetAll().Where(x => x.ConsigneName.Contains(item.PARTY_NAME)).LastOrDefault();
                        if(consres != null)
                        {
                            consigneeid = consres.ConsigneId;
                        }
                    }
                    
                    var igmo = new IGMO
                    {
                        BLNumber = item.BL_NO,
                        ContainerNumber = item.CONTAINER_NO,
                        IndexNumber = item.INDEX_NO,
                        VIRNumber = igm,
                        MarksAndNumber = item.MARKS_NO,
                        Description = item.GOOD_DESCRIPTION,
                        Status = "CFS",
                        ManifestedSealNumber = item.MANIFESETED_SEAL_NO,
                        CreateDT = DateTime.Now,
                        BLGrossWeight = item.GROSS_WEIGHT,
                        NumberofPackages = item.NO_OF_PACKAGES,
                        PackageType = item.PACKAGES + "=" + item.NO_OF_PACKAGES,

                    };
                     
                    var conainerIndex = new ContainerIndex
                    {
                        BLNo = item.BL_NO,
                        ContainerNo = item.CONTAINER_NO,
                        IndexNo = item.INDEX_NO,
                        VirNo = igm,
                        MarksAndNumber = item.MARKS_NO,
                        ConsigneId = consigneeid,
                        Description = item.GOOD_DESCRIPTION,
                        Status = "CFS",
                        ManifestedSealNumber = item.MANIFESETED_SEAL_NO,
                        BLGrossWeight = item.GROSS_WEIGHT,
                        NoOfPackages = Convert.ToUInt16( item.NO_OF_PACKAGES),
                        PackageType = item.PACKAGES+"="+ item.NO_OF_PACKAGES,
                        ManifestedWeight = item.NET_WEIGHT ?? 0,
                        Size  = Convert.ToInt16( item.CONTAINER_SIZE),
                        
                         
                    };

                    igmos.Add(igmo);
                    ConainerIndexes.Add(conainerIndex);
                }

                igmos.RemoveAll(c => c.ContainerNumber == null || c.BLNumber == null || c.IndexNumber ==  0);
                ConainerIndexes.RemoveAll(c => c.ContainerNo == null || c.BLNo == null || c.IndexNo == 0);


                foreach (var item in igmos)
                {
 

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("GetIGMOFromConatinerBLIndex", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@blnumber", item.BLNumber);
                        cmd.Parameters.AddWithValue("@containerno", item.ContainerNumber);
                        cmd.Parameters.AddWithValue("@indexno", item.IndexNumber);
                        cmd.Parameters.AddWithValue("@VirNo", igm);
                        cmd.Parameters.AddWithValue("@marksAndNumber", item.MarksAndNumber);
                        cmd.Parameters.AddWithValue("@description", item.Description);
                        cmd.Parameters.AddWithValue("@status", "CFS");
                        cmd.Parameters.AddWithValue("@manifestedSealNumber", item.ManifestedSealNumber);
                        cmd.Parameters.AddWithValue("@blgrossWeight", item.BLGrossWeight);
                        cmd.Parameters.AddWithValue("@noOfPackages", item.NumberofPackages);
                        cmd.Parameters.AddWithValue("@packageType", item.PackageType); 
                         
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    //var res = _igmoRepository.GetAll().Where(x => x.BLNumber == item.BLNumber && x.ContainerNumber == item.ContainerNumber && x.IndexNumber == item.IndexNumber).LastOrDefault();

                    //if (res != null)
                    //{
                    //    res.BLNumber = item.BLNumber;
                    //    res.ContainerNumber = item.ContainerNumber;
                    //    res.IndexNumber = item.IndexNumber;
                    //    res.VIRNumber = igm;
                    //    res.MarksAndNumber = item.MarksAndNumber;
                    //    res.Description = item.Description;
                    //    res.Status = "CFS";
                    //    res.ManifestedSealNumber = item.ManifestedSealNumber;
                    //    res.CreateDT = DateTime.Now;
                    //    res.BLGrossWeight = item.BLGrossWeight;
                    //    res.NumberofPackages = item.NumberofPackages;
                    //    res.PackageType = item.PackageType;

                    //    _igmoRepository.Update(res);

                    //}

                    //else
                    //{
                    //    _igmoRepository.Add(item);

                    //}

                }

                foreach (var item in ConainerIndexes)
                {

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("GetContainerIndexFromConatinerBLIndex", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@blnumber", item.BLNo);
                        cmd.Parameters.AddWithValue("@containerno", item.ContainerNo);
                        cmd.Parameters.AddWithValue("@indexno", item.IndexNo);
                        cmd.Parameters.AddWithValue("@VirNo", igm);
                        cmd.Parameters.AddWithValue("@marksAndNumber", item.MarksAndNumber);
                        cmd.Parameters.AddWithValue("@description", item.Description);
                        cmd.Parameters.AddWithValue("@status", "CFS");
                        cmd.Parameters.AddWithValue("@manifestedSealNumber", item.ManifestedSealNumber);
                        cmd.Parameters.AddWithValue("@blgrossWeight", item.BLGrossWeight);
                        cmd.Parameters.AddWithValue("@noOfPackages", item.NoOfPackages);
                        cmd.Parameters.AddWithValue("@packageType", item.PackageType);
                        cmd.Parameters.AddWithValue("@consigneeId", item.ConsigneId);
                        cmd.Parameters.AddWithValue("@manifestedWeight", item.ManifestedWeight);
                        cmd.Parameters.AddWithValue("@size", item.Size);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                     
                    //var res = _containerIndexRepo.GetAll().Where(x => x.BLNo == item.BLNo && x.ContainerNo == item.ContainerNo && x.IndexNo == item.IndexNo).LastOrDefault();

                    //if (res != null)
                    //{
                    //    res.BLNo = item.BLNo;
                    //    res.ContainerNo = item.ContainerNo;
                    //    res.IndexNo = item.IndexNo;
                    //    res.VirNo = igm;
                    //    res.MarksAndNumber = item.MarksAndNumber;
                    //    res.Description = item.Description;
                    //    res.Status = "CFS";
                    //    res.ManifestedSealNumber = item.ManifestedSealNumber;
                    //    res.BLGrossWeight = item.BLGrossWeight;
                    //    res.NoOfPackages = item.NoOfPackages;
                    //    res.PackageType = item.PackageType;
                    //    res.ManifestedWeight = item.ManifestedWeight;
                    //    res.Size = item.Size;

                    //    _containerIndexRepo.Update(res);

                    //}
                    //else
                    //{
                    //    _containerIndexRepo.Add(item);
                    //}

                }

                return new JsonResult(new { error = false, message = "Updated" });

            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> f)
        {
 
            try
            {

                var filepath = System.IO.Directory.GetCurrentDirectory();

                string pathMCT = Path.Combine(filepath + "\\wwwroot\\DatFiles", "MCIGMCT.dat");
                string pathMCS = Path.Combine(filepath + "\\wwwroot\\DatFiles", "MCIGMCS.dat");
                string pathMIX = Path.Combine(filepath + "\\wwwroot\\DatFiles", "MCIGMIX.dat");
                string pathMSR = Path.Combine(filepath + "\\wwwroot\\DatFiles", "MCIGMSR.dat");
                string pathMMS = Path.Combine(filepath + "\\wwwroot\\DatFiles", "MCIGMMS.dat");

                System.IO.File.Delete(pathMCT);
                System.IO.File.Delete(pathMCS);
                System.IO.File.Delete(pathMIX);
                System.IO.File.Delete(pathMSR);
                System.IO.File.Delete(pathMMS);

                foreach (var item in f)
                {
                    string path = FileUploadHandler.GetFilePathForUpload(filepath + "\\wwwroot\\DatFiles", item.FileName);

                    if (!System.IO.File.Exists(path))
                    {
                        using (FileStream stream = new FileStream(path, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }
                    }
                     
                }

                return new JsonResult(new { error = false, message = "Files Uploaded" });



            }
            catch (Exception e)
            {
                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }



          return  new JsonResult(new { error = true, message = "please try again" });
        }



        //public JsonResult ProcessDatFiles(string pathMCT, string pathMCS, string pathMIX, string pathMSR)


        [HttpPost]

        public JsonResult ProcessDatFiles( )
        {
            ResultBase result = new ResultBase();

            try
            {
                 
                //DAT_FILE_DATA_MODEL DatFilesData = ProcessingDatFiles( pathMCT,  pathMCS,  pathMIX,  pathMSR);  
                DAT_FILE_DATA_MODEL DatFilesData = ProcessingDatFiles( );
                List<AICT_TEMP_DAT_FILE_CONTAINER> DatFileDataList = ProcessDatFiles(DatFilesData);
                if (DatFileDataList != null && DatFileDataList.Count > 0)
                {
                    result.Results = DatFileDataList;
                    result.Message = "done";
                }
                else
                {
                    result.Message = "no data";
                }
                result.HasError = false;
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
                result.HasError = true;
            }
             


            return Json(result);
        }


        private void StoreInFolder(IFormFile file, string fileName)
        {
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                file.CopyTo(fs);
                fs.Flush();
            }




        }

        private DAT_FILE_DATA_MODEL ProcessingDatFiles( )
        {
            DAT_FILE_DATA_MODEL NewDatFilesData = new DAT_FILE_DATA_MODEL();
            try
            {
                var filepath = System.IO.Directory.GetCurrentDirectory();

                string pathMCT = Path.Combine(filepath + "\\wwwroot\\DatFiles", "MCIGMCT.dat");
                string pathMCS = Path.Combine(filepath + "\\wwwroot\\DatFiles", "MCIGMCS.dat");
                string pathMIX = Path.Combine(filepath + "\\wwwroot\\DatFiles", "MCIGMIX.dat");
                string pathMSR = Path.Combine(filepath + "\\wwwroot\\DatFiles", "MCIGMSR.dat");
                 
                //string pathMCT , string pathMCS , string pathMIX , string pathMSR
                //path = string.Empty;
                //path = path + "MCIGMCT.dat"; 
                 
                if (System.IO.File.Exists(pathMCT))
                {
                    string[] readText = System.IO.File.ReadAllLines(pathMCT);
                    foreach (var item in readText)
                    {
                        try
                        {
                            NewDatFilesData.MCT_FILE_DATA.Add(new MCT_DAT_FILE_MODEL()
                            {
                                INDEX_NO = Convert.ToInt32(item.Substring(14, 4).Trim()),
                                CONTAINER_SIZE = item.Substring(22, 2).Trim(),
                                CONTAINER_NO = item.Substring(26, 13).Trim().Replace("-", ""),
                                SEAL_NO = item.Substring(41, 10).Trim(),
                            });
                        }
                        catch
                        {
                            continue;
                        }

                    }
                     

                }

                //   path = string.Empty;
                //path = path + "MCIGMCS.dat";

                if (System.IO.File.Exists(pathMCS))
                {
                    List<MCS_DAT_FILE_MODEL> NewList = new List<MCS_DAT_FILE_MODEL>();
                    string[] readText = System.IO.File.ReadAllLines(pathMCS);
                    foreach (var item in readText)
                    {
                        try
                        {
                            NewList.Add(new MCS_DAT_FILE_MODEL()
                            {
                                INDEX_NO = Convert.ToInt32(item.Substring(14, 4).Trim()),
                                MARKS_NO = item.Substring(28, 30).Trim(),
                            });
                        }
                        catch
                        {
                            continue;
                        }

                        Console.WriteLine(item);
                    }
                    foreach (var item in NewList.GroupBy(a => a.INDEX_NO).Select(x => x.First()))
                    {
                        MCS_DAT_FILE_MODEL MCS_DATA = new MCS_DAT_FILE_MODEL();
                        int? index = item.INDEX_NO;
                        var obj = NewList.Where(a => a.INDEX_NO == index).ToList();
                        if (obj != null)
                        {
                            foreach (var mark in obj)
                            {
                                MCS_DATA.INDEX_NO = index;
                                MCS_DATA.MARKS_NO += mark.MARKS_NO;
                            }
                        }
                        NewDatFilesData.MCS_FILE_DATA.Add(MCS_DATA);
                    }
                     
                }


                ////MIX File Data
                //path = "C:\\datfile\\MCIGMIX.dat";
                //path = path + "MCIGMIX.dat";

                if (System.IO.File.Exists(pathMIX))
                {
                    string[] readText = System.IO.File.ReadAllLines(pathMIX);
                    foreach (var item in readText)
                    {
                        try
                        {
                            NewDatFilesData.MIX_FILE_DATA.Add(new MIX_DAT_FILE_MODEL()
                            {
                                INDEX_NO = Convert.ToInt32(item.Substring(14, 4).Trim()),
                                PARTY_NAME = item.Substring(129, 50).Trim(),
                                PARTY_ADRESS = item.Substring(179, 60).Trim(),
                                TOTAL_CONTAINERS = item.Substring(333, 4).Trim(),
                                GROSS_WEIGHT = item.Substring(385, 10).Trim(),
                                NET_WEIGHT = item.Substring(401, 10).Trim(),
                                PORT_DESC = item.Substring(420, 15).Trim(),
                                BL_NUMBER = item.Substring(19, 20).Trim(),
                                BL_DATE = item.Substring(49, 8).Trim(),
                                CONSIGNEE_NAME = item.Substring(277, 50).Trim(),
                                CITY = item.Substring(239, 15).Trim()
                            });
                        }
                        catch
                        {
                            continue;
                        }

                    }
                     

                }

                //path = path + "\\MCIGMSR.dat";

                if (System.IO.File.Exists(pathMSR))
                {
                    string[] readText = System.IO.File.ReadAllLines(pathMSR);
                    foreach (var item in readText)
                    {
                        try
                        {
                            NewDatFilesData.MSR_FILE_DATA.Add(new MSR_DAT_FILE_MODEL()
                            {
                                INDEX_NO = Convert.ToInt32(item.Substring(14, 4).Trim()),
                                GOODS_DESCRIPTION = item.Substring(41, 175).Trim(),
                                GOODS = item.Substring(206, 30).Trim(),
                                PACKAGES = item.Substring(260, 8).Trim(),
                                NO_OF_PACKAGES = item.Substring(271, 4).Trim(),
                            });
                        }
                        catch
                        {
                            continue;
                        }

                    }
                     
 
                }


            }
            catch (Exception ex)
            {
                // Logger.WriteException(ex, "ContainersController -- >> ProcessingDatFiles");
            }
            return NewDatFilesData;
        }


        public List<AICT_TEMP_DAT_FILE_CONTAINER> ProcessDatFiles(DAT_FILE_DATA_MODEL DatFilesData)
        {
            string status = "NO DATA";
            List<AICT_TEMP_DAT_FILE_CONTAINER> Temp_DATA = new List<AICT_TEMP_DAT_FILE_CONTAINER>();
            foreach (var item in DatFilesData.MCT_FILE_DATA)
            {
                Temp_DATA.Add(new AICT_TEMP_DAT_FILE_CONTAINER()
                {
                    CONTAINER_NO = item.CONTAINER_NO,
                    INDEX_NO = item.INDEX_NO,
                    CONTAINER_SIZE = item.CONTAINER_SIZE,
                    MANIFESETED_SEAL_NO = item.SEAL_NO,
                });
            }

            foreach (var item in DatFilesData.MCS_FILE_DATA)
            {
                var obj = Temp_DATA.Where(a => a.INDEX_NO == item.INDEX_NO).FirstOrDefault();
                if (obj != null)
                {
                    obj.MARKS_NO = item.MARKS_NO;
                }
            }

            foreach (var item in DatFilesData.MIX_FILE_DATA)
            {
                var obj = Temp_DATA.Where(a => a.INDEX_NO == item.INDEX_NO).FirstOrDefault();
                if (obj != null)
                {
                    obj.PARTY_NAME = item.PARTY_NAME;
                    obj.PARTY_ADDRESS = item.PARTY_ADRESS;
                    obj.BL_NO = item.BL_NUMBER;
                    obj.BL_DATE = Convert.ToDateTime(item.BL_DATE.Insert(4, "/").Insert(7, "/"));
                    //obj.CITY = item.CITY;
                    obj.GROSS_WEIGHT = Convert.ToDouble(item.GROSS_WEIGHT);
                    obj.NET_WEIGHT = Convert.ToDouble(item.NET_WEIGHT);
                }
            }
            foreach (var item in DatFilesData.MSR_FILE_DATA)
            {
                var obj = Temp_DATA.Where(a => a.INDEX_NO == item.INDEX_NO).FirstOrDefault();
                if (obj != null)
                {
                    obj.GOOD_DESCRIPTION = item.GOODS_DESCRIPTION;
                    obj.PACKAGES = item.PACKAGES;
                    obj.NO_OF_PACKAGES = Convert.ToInt64(item.NO_OF_PACKAGES);
                }
            }

            return Temp_DATA;
        }


    }




    public class ResultBase
    {
        public bool HasError { get; set; }
        public string Message { get; set; }
        public object Results { get; set; }
        public string StatusCode { get; set; }
        public string ReturnUrl { get; set; }
    }


    public class MCT_DAT_FILE_MODEL
    {
        public string IGM_NO { get; set; }
        public string CONTAINER_NO { get; set; }
        public int? INDEX_NO { get; set; }
        public string CONTAINER_SIZE { get; set; }
        public string SEAL_NO { get; set; }
    }

    public class MCS_DAT_FILE_MODEL
    {
        public int? INDEX_NO { get; set; }
        public string MARKS_NO { get; set; }
        public string SEAL_NO { get; set; }
    }

    public class MIX_DAT_FILE_MODEL
    {
        public int? INDEX_NO { get; set; }
        public string PARTY_NAME { get; set; }
        public string TOTAL_CONTAINERS { get; set; }
        public string GROSS_WEIGHT { get; set; }
        public string NET_WEIGHT { get; set; }
        public string PORT_DESC { get; set; }
        public string BL_NUMBER { get; set; }
        public string BL_DATE { get; set; }
        public string PARTY_ADRESS { get; set; }
        public string CITY { get; set; }
        public string CONSIGNEE_NAME { get; set; }

    }

    public class MSR_DAT_FILE_MODEL
    {
        public int? INDEX_NO { get; set; }
        public string GOODS_DESCRIPTION { get; set; }
        public string GOODS { get; set; }
        public string PACKAGES { get; set; }
        public string NO_OF_PACKAGES { get; set; }
    }

    public class TEMP_DAT_FILE_CONTAINER
    {
        public long DAT_FILE_CONTAINER_ID { get; set; } // DAT_FILE_CONTAINER_ID (Primary key)
        public string CONTAINER_SIZE { get; set; } // CONTAINER_SIZE
        public string CONTAINER_NO { get; set; } // CONTAINER_NO
        public int? INDEX_NO { get; set; } // INDEX_NO
        public string BL_NO { get; set; } // BL_NO
        public string MANIFESETED_SEAL_NO { get; set; }
        public string CUSTOMER_NAME { get; set; }
    }

    public class DAT_FILE_DATA_MODEL
    {
        public List<MCT_DAT_FILE_MODEL> MCT_FILE_DATA { get; set; }
        public List<MCS_DAT_FILE_MODEL> MCS_FILE_DATA { get; set; }
        public List<MIX_DAT_FILE_MODEL> MIX_FILE_DATA { get; set; }
        public List<MSR_DAT_FILE_MODEL> MSR_FILE_DATA { get; set; }
        public DAT_FILE_DATA_MODEL()
        {
            MCT_FILE_DATA = new List<MCT_DAT_FILE_MODEL>();
            MCS_FILE_DATA = new List<MCS_DAT_FILE_MODEL>();
            MIX_FILE_DATA = new List<MIX_DAT_FILE_MODEL>();
            MSR_FILE_DATA = new List<MSR_DAT_FILE_MODEL>();
        }

    }

    public partial class AICT_TEMP_DAT_FILE_CONTAINER
    {
        public string CONTAINER_SIZE { get; set; } // CONTAINER_SIZE
        public string IGM_NO { get; set; } // IGM_NO
        public string CONTAINER_NO { get; set; } // CONTAINER_NO
        public int? INDEX_NO { get; set; } // INDEX_NO
        public string BL_NO { get; set; } // BL_NO
        public string DESCRIPTION { get; set; } // DESCRIPTION
        public string MARKS_NO { get; set; } // MARKS_NO
        public string MANIFESETED_SEAL_NO { get; set; } // MANIFESETED_SEAL_NO
        public string PARTY_NAME { get; set; } // PARTY_NAME
        public string PARTY_ADDRESS { get; set; } // PARTY_ADDRESS
        public double? GROSS_WEIGHT { get; set; } // GROSS_WEIGHT
        public double? NET_WEIGHT { get; set; } // NET_WEIGHT
        public string PORT_DESCRIPTION { get; set; } // PORT_DESCRIPTION
        public string CONSIGNOR_NAME { get; set; } // CONSIGNOR_NAME
        public string CONSIGNOR_ORIGIN_COUNTRY { get; set; } // CONSIGNOR_ORIGIN_COUNTRY
        public string GOOD_DESCRIPTION { get; set; } // GOOD_DESCRIPTION
        public string PACKAGES { get; set; } // PACKAGES
        public long? NO_OF_PACKAGES { get; set; } // NO_OF_PACKAGES
        public DateTime? BL_DATE { get; set; } // BL_DATE
        public DateTime DATE_ADDED { get; set; } // DATE_ADDED
        public DateTime? DATE_UPDATED { get; set; } // DATE_UPDATED
        public bool? IS_ASSOCIATED { get; set; } // IS_ASSOCIATED

        public AICT_TEMP_DAT_FILE_CONTAINER()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
