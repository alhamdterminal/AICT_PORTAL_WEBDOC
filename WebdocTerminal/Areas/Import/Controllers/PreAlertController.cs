using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class PreAlertController : ParentController
    {
        public IPaymentUpdateRepository _paymentUpdateRepo;
        public IPreAlertRepository _preAlertRepo;
        public IPreAlterDetailRepository _preAlterDetailRepo;
        public IShippingAgentRepository _shippingAgentRepo;
        public IShippingLineRepository _shippingLineRepo;
        public IPortAndTerminalRepository _portAndTerminalRepo;
        public IPaymentUpdateDetailRepository _paymentUpdateDetailRepo;
        public IVesselRepository _vesselRepo;
        public IPreAlertPayOrderRepository _preAlertPayOrderRepo;
        public IPreAlertVesselRepository _preAlertVesselRepo;
        public IVIRORepository _viroRepository;
        public PreAlertController(IPaymentUpdateRepository paymentUpdateRepo 
                                  , IPreAlertRepository preAlertRepo
                                  , IPreAlterDetailRepository preAlterDetailRepo
                                  , IShippingAgentRepository shippingAgentRepo
                                  , IShippingLineRepository shippingLineRepo
                                  , IPortAndTerminalRepository portAndTerminalRepo
                                  , IPaymentUpdateDetailRepository paymentUpdateDetailRepo
                                  , IVesselRepository vesselRepo
                                  , IPreAlertPayOrderRepository preAlertPayOrderRepo
                                  , IPreAlertVesselRepository preAlertVesselRepo
                                  ,  IVIRORepository viroRepository)
        {
            _paymentUpdateRepo = paymentUpdateRepo;
            _preAlertRepo = preAlertRepo;
            _preAlterDetailRepo = preAlterDetailRepo;
            _shippingAgentRepo = shippingAgentRepo;
            _shippingLineRepo = shippingLineRepo;
            _portAndTerminalRepo = portAndTerminalRepo;
            _paymentUpdateDetailRepo = paymentUpdateDetailRepo;
            _vesselRepo = vesselRepo;
            _preAlertPayOrderRepo = preAlertPayOrderRepo;
            _preAlertVesselRepo = preAlertVesselRepo;
            _viroRepository = viroRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PreAlertView()
        {


            ViewData["ShippingAgent"] = _shippingAgentRepo.GetAll().OrderBy(s => s.Name)
                .Select(s => new SelectListItem { Text = s.Name, Value = s.ShippingAgentId.ToString() });
            
             
            ViewData["PortAndTerminal"] = _portAndTerminalRepo.GetAll().OrderBy(s => s.PortName)
                .Select(s => new SelectListItem { Text = s.PortName, Value = s.PortAndTerminalId.ToString() });
             

            //ViewData["Vessels"] = _preAlertVesselRepo.GetAll().OrderBy(s => s.PreAlertVesselName)
            //     .Select(s => new SelectListItem { Text = s.PreAlertVesselName, Value = s.PreAlertVesselId.ToString() });


            return View();
        }

        public IActionResult UpdatePreAlertView()
        { 
            return View();
        }


        public JsonResult GetPreAlterByDate(DateTime fromDate ,DateTime todate)
        {
            var result = _preAlertRepo.GetList(x =>  
            Convert.ToDateTime(x.PreAlertDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(fromDate.Date.ToString("MM/dd/yyyy"))
            &&
            Convert.ToDateTime(x.PreAlertDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(todate.Date.ToString("MM/dd/yyyy"))
            , y=> y.PreAlterDetails).ToList();
            return Json(result);
        }



        public IActionResult DeletePreAlert(long key)
        {
            var result = _preAlertRepo.Find(key);

            var data = _paymentUpdateRepo.GetAll().Where(x => x.PreAlertNumber == result.PreAlertNumber).ToList();

            if (data.Count() > 0)
            {
                return new OkObjectResult(new { error = true, Message = "Not Deleted Due To Payment Update Created " });
            }
            else
            {

                var res = _preAlertRepo.GetPaymentUpdateStatusByPrealertid(key);

                if(res == true)
                {
                    return new OkObjectResult(new { error = false, Message = "can't delete due to payment created" });
                }

                var resultlist = _preAlterDetailRepo.GetAll().Where(x => x.PreAlertId == key).ToList();

                _preAlertRepo.Delete(result);

                _preAlterDetailRepo.DeleteRange(resultlist);

                return new OkObjectResult(new { error = false, Message = "Deleted" });

            }
        }
        public IActionResult DeletePreAlertDetail(long key)
        {
            var deatilresult = _preAlterDetailRepo.Find(key);
            if(deatilresult != null)
            {
                var result = _preAlertRepo.Find(deatilresult.PreAlertId);

                if(result != null)
                {
                    var data = _paymentUpdateRepo.GetAll().Where(x => x.PreAlertNumber == result.PreAlertNumber).ToList();

                    if (data.Count() > 0)
                    {
                        return new OkObjectResult(new { error = true, Message = "Not Deleted Due To Payment Update Created " });
                    }
                    else
                    {
                        var res = _preAlertRepo.GetPaymentUpdateStatusByPrealertid(deatilresult.PreAlertId ?? 0);

                        if (res == true)
                        {
                            return new OkObjectResult(new { error = false, Message = "can't delete due to payment created" });
                        }
                         
                        _preAlterDetailRepo.Delete(deatilresult);

                        return new OkObjectResult(new { error = false, Message = "Deleted" });
                    }

                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "Not Deleted Due To no aleret found " });

                }

            }
            else
            {
                return new OkObjectResult(new { error = true, Message = "Not Deleted Due To   no detail found " });

            }


           
            
        }

        public IActionResult UpdatePreAlert(PreAlterDetail data)
        { 
            var res = _preAlertRepo.GetPayOrderStatusByPrealertid(data.PreAlertId ?? 0);

            if (res == true)
            {
                return new OkObjectResult(new { error = true, Message = "can't update due to PO created" });

            }
            else
            {
                _preAlterDetailRepo.Update(data);

                return new OkObjectResult(new { error = false, Message = "Updated" });
            }
 
        }


        public IActionResult UpdateAlert(PreAlert data)
        {

            var res = _preAlertRepo.GetPayOrderStatusByPrealertid(data.PreAlertId);

            if(res == true)
            {
                return new OkObjectResult(new { error = true, Message = "can't update due to PO created" });

            }
            else
            {
                _preAlertRepo.Update(data);

                return new OkObjectResult(new { error = false, Message = "Updated" });
            }

        }
         


        public IActionResult AddPreAlert(PreAlert values, List<PreAlterDetail> Details)
        {
            try
            {


                foreach (var item in Details)
                {
                    var result = _preAlterDetailRepo.GetCreatePerAlertDetailInfo( item.ContainerNumber.Trim().ToUpper() , values.Vessel.Trim().ToUpper() , values.Voyage.Trim().ToUpper()  , values.ETADate);

                    if (result != null && (item.Remarks == null || item.Remarks == ""))
                    {
                        return new OkObjectResult(new { error = true, Message = "This " + item.ContainerNumber + " container   " + item.Vessel + " Vessel " + values.Voyage + " voyage already avaible please add remarks" });

                    }
                }


                List<PreAlterDetail> preAlterDetails = new List<PreAlterDetail>();

                _preAlertRepo.Add(values);

                foreach (var item in Details)
                {
                    var preAlterDetail = new PreAlterDetail
                    {
                        
                        CargoType = item.CargoType,
                        ContainerNumber = item.ContainerNumber,
                        MasterBLNumber = item.MasterBLNumber,
                        VoyageNumber = values.Voyage,
                        Vessel = values.Vessel,
                        ETADate = values.ETADate,
                        Remarks = item.Remarks,
                        PreAlertId = values.PreAlertId,
                        Type = item.Type,
                        SOCCOC = item.SOCCOC,
                        ShippingLineId = item.ShippingLineId,
                        PortAndTerminalName = item.PortAndTerminalName,
                        CreatedDate = DateTime.Now,
                        //PortAndTerminalId = item.PortAndTerminalId,
                        Size = item.Size
                      
                    };

                    preAlterDetails.Add(preAlterDetail);


                }
                 

                _preAlterDetailRepo.AddRange(preAlterDetails);


                return new OkObjectResult(new { error = false, PreAlertNumber = values.PreAlertNumber, Message = "Saved" });
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true , Message = "please try again" });

            }

           


           

        }
         
             
        public string GenPreAlertCode()
        {
            var date = DateTime.Now;
            var peralterNumber = _preAlertRepo.GeneratePreAlterNumber();
            string value = peralterNumber.ToString();
            return value + "/" + date.ToString("yy");

        }


        public IActionResult PaymentUpdateView()
        {
             //var containerNumbers = _preAlertRepo.GetPreAlertNumber().Select(v => v.PreAlertNumber).Distinct(); 
             var containerNumbers = _preAlertRepo.GetAll().Where(x=> x.AlertStatus == false).Select(v => v.PreAlertNumber).Distinct(); 

            return View(containerNumbers);
        }



       


        public IActionResult AddPaymentUpdate(PaymentUpdate values, string  number , List<PaymentUpdateDetail> Details)
        {
             
            List<PaymentUpdateDetail> PaymentUpdateDetails = new List<PaymentUpdateDetail>();

            values.PreAlertNumber = number;
            _paymentUpdateRepo.Add(values);

            foreach (var item in Details)
            {
                var paymentUpdateDetail = new PaymentUpdateDetail
                {
                    SecDeposit = item.SecDeposit,
                    THCDoc = item.THCDoc,
                    THCInsurance = item.THCInsurance,
                    THC = item.THC,
                    THCOthers = item.THCOthers,
                    TotalTHCCharges = item.THCDoc + item.THCInsurance + item.THC + item.THCOthers,
                    Lolo = item.Lolo,
                    Detention = item.Detention,
                    LOLOIncInTHC = item.LOLOIncInTHC,
                    TotalCharges = item.SecDeposit + item.THCDoc + item.THCInsurance + item.THC + item.THCOthers + item.Lolo + item.Detention    ,
                    PreAlterDetailId = item.PreAlterDetailId,
                    ShippingAgentIdForSD = item.ShippingAgentIdForSD,
                    ShippingAgentIdForTHC = item.ShippingAgentIdForTHC,
                    ShippingAgentIdForLoLo = item.ShippingAgentIdForLoLo,
                    ShippingLineIdForSD = item.ShippingLineIdForSD,
                    ShippingLineIdForTHC = item.ShippingLineIdForTHC,
                    ShippingLineIdForLOLO = item.ShippingLineIdForLOLO,
                    PaymentUpdateId = values.PaymentUpdateId
                };

                PaymentUpdateDetails.Add(paymentUpdateDetail); 
            }

            _paymentUpdateDetailRepo.AddRange(PaymentUpdateDetails);


            var iNo = values.PreAlertNumber.Split(",").ToList();

            foreach (var item in iNo)
            {
                _preAlertRepo.GetAlert(item);
            }


            return new OkObjectResult(new { error = false   , Message = "Saved" });

        }


        public long GenPaymentUpdateCode()
        {
             var PaymentUpdateNumber = _paymentUpdateRepo.GeneratePaymentUpdateNumber();

             return PaymentUpdateNumber;

        }


        public IActionResult UpdatePaymentUpdateDetailView()
        {
            return View();
        }


        public JsonResult GetPaymentUpdatesByDate(DateTime fromDate, DateTime todate)
        {
            var result = _paymentUpdateRepo.GetList(x =>
            Convert.ToDateTime(x.PaymentUpdateCreatedDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(fromDate.Date.ToString("MM/dd/yyyy"))
            &&
            Convert.ToDateTime(x.PaymentUpdateCreatedDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(todate.Date.ToString("MM/dd/yyyy"))
            , y => y.PaymentUpdateDetails).ToList();
            return Json(result);
        }

        public IActionResult DeletePaymentUpdate(long key)
        {
            var result = _paymentUpdateRepo.Find(key);

            if(result != null)
            {
                var data = _preAlertPayOrderRepo.GetAll().Where(x => x.RequestNumber == result.RequestNumber).ToList();

                if (data.Count() > 0)
                {

                    return new OkObjectResult(new { error = true, Message = "Not Deleted Due To PayOrder Created " });

                }
                else
                {

                    var resultlist = _paymentUpdateDetailRepo.GetAll().Where(x => x.PaymentUpdateId == key).ToList();

                    _paymentUpdateRepo.Delete(result);
                    _paymentUpdateDetailRepo.DeleteRange(resultlist);

                    var iNo = result.PreAlertNumber.Split(",").ToList();

                    foreach (var item in iNo)
                    {
                        _preAlertRepo.updateAlertStaus(item);
                    }
                     
                    return new OkObjectResult(new { error = false, Message = "Deleted" });

                }
            }
            else
            {
                return new OkObjectResult(new { error = true, Message = "no detail found" });

            }

        }

        public IActionResult UpdatePaymentUpdateDetail(PaymentUpdateDetail data)
        {

            var res = _paymentUpdateRepo.GetAll().Where(x => x.PaymentUpdateId == data.PaymentUpdateId).LastOrDefault();

            if(res != null)
            {

                var po = _preAlertPayOrderRepo.GetAll().Where(x => x.RequestNumber == res.RequestNumber).LastOrDefault();

                if(po != null)
                { 
                    return new OkObjectResult(new { error = true, Message = "can't update due to po created" });
                }
                else
                {
                    data.TotalTHCCharges = data.THCDoc + data.THCInsurance + data.THC + data.THCOthers;
                    _paymentUpdateDetailRepo.Update(data);

                    return new OkObjectResult(new { error = false, Message = "Updated" });
                }

            }
            else
            {
                return new OkObjectResult(new { error = true, Message = "no payment found" });
            }
             
        }

        public IActionResult DeletePaymentUpdateDetail(long key)
        {
            var dataDetail = _paymentUpdateDetailRepo.GetAll().Where(x => x.PaymentUpdateDetailId == key).LastOrDefault();

            if(dataDetail != null)
            {
                var preAlterDetail = _preAlterDetailRepo.GetAll().Where(x => x.PreAlterDetailId == dataDetail.PreAlterDetailId).LastOrDefault();

                if(preAlterDetail != null)
                {
                    var data = _paymentUpdateRepo.GetAll().Where(x => x.PaymentUpdateId == dataDetail.PaymentUpdateId).LastOrDefault();

                    if (data != null)
                    {
                        var po = _preAlertPayOrderRepo.GetAll().Where(x => x.RequestNumber == data.RequestNumber).LastOrDefault();

                        if (po != null)
                        {
                            return new OkObjectResult(new { error = true, Message = "can't update due to po created" });

                        }
                        else
                        {
                            _paymentUpdateDetailRepo.Delete(dataDetail);

                            return new OkObjectResult(new { error = false, Message = "deleted " });
                        }

                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, Message = "Not Deleted Due To no payment found " });

                    }
                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "Not Deleted Due To no prealert detail found " });
                }                 
            }
            else
            {
                return new OkObjectResult(new { error = true, Message = "Not Deleted Due To no detail found " });
            }
             
        }


        public IActionResult PreAlertBindingView()
        {

            ViewData["ShippingAgent"] = _shippingAgentRepo.GetAll().OrderBy(s => s.Name)
             .Select(s => new SelectListItem { Text = s.Name, Value = s.ShippingAgentId.ToString() });


            ViewData["PortAndTerminal"] = _portAndTerminalRepo.GetAll().OrderBy(s => s.PortName)
                .Select(s => new SelectListItem { Text = s.PortName, Value = s.PortAndTerminalId.ToString() });
             
             
            return View();
        }


        public IActionResult BindWithIGM(string igm , List<PreAlertForWebocViewModel> datalist)
        {

            var res = _viroRepository.GetAll().Where(x => x.VIRNumber == igm).LastOrDefault();

            if( res!= null)
            {

                var listrec = string.Join(",", datalist.Select(n => n.PreAlterDetailId.ToString()).ToArray());

                var resdata = _preAlertRepo.GetpreAlterDetails(listrec).ToList();

                if(resdata.Count() > 0)
                {
                    resdata.ForEach(x => x.VirNumber = igm);

                    _preAlterDetailRepo.UpdateRange(resdata);

                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "no record  found" });
                }


            }


            return new OkObjectResult(new { error = true, Message = "no data  found" });
        }

    }
}
