using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class PreAlertPayOrderController : ParentController
    {
        public IPreAlertPayOrderRepository _preAlertPayOrderRepo;
        public IPreAlertPayOrderDetailRepository _preAlertPayOrderDetailRepo;
        public IPaymentUpdateRepository _paymentUpdateRepo;
        public IBankRepository  _bankRepository;
        public IBankBranchRepository _bankBranchRepo;

        public PreAlertPayOrderController(IPreAlertPayOrderRepository preAlertPayOrderRepo
                                         ,IPaymentUpdateRepository paymentUpdateRepo
                                         , IPreAlertPayOrderDetailRepository preAlertPayOrderDetailRepo
                                         , IBankRepository bankRepository
                                         , IBankBranchRepository bankBranchRepo
                                )
        {
            _preAlertPayOrderRepo = preAlertPayOrderRepo;
            _preAlertPayOrderDetailRepo = preAlertPayOrderDetailRepo;
            _paymentUpdateRepo = paymentUpdateRepo;
            _bankRepository = bankRepository;
            _bankBranchRepo = bankBranchRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PayOrderView()
        {

            ViewData["RequestNo"] = _paymentUpdateRepo.GetRequestNumbers().OrderBy(s => s.RequestNo)
                        .Select(s => new SelectListItem { Text = s.RequestNoWithDateTime, Value = s.RequestNo.ToString() });


            ViewData["BankList"] = _bankRepository.GetAll().Where(x => x.Type == "PayOrder")
                        .Select(s => new SelectListItem { Text = s.BankName, Value = s.BankId.ToString() });



            ViewData["BranchList"] = _bankBranchRepo.GetAll()
                        .Select(s => new SelectListItem { Text = s.BankBranchName, Value = s.BankBranchId.ToString() });

            return View();
        }


        public JsonResult GenPaymentUpdateCode(long RequestNumber)
        {
             var PaymentUpdateNumber = _preAlertPayOrderRepo.GetPreAlertPayOrders(RequestNumber).OrderBy(x=> x.ContainerNumber);

            return Json( PaymentUpdateNumber);

        }

        public IActionResult AddPreAlertPayOrder(PreAlertPayOrder values, List<PreAlertPayOrderDetail> Details)
        {


            List<PreAlertPayOrderDetail> PreAlertPayOrderDetails = new List<PreAlertPayOrderDetail>();

            _preAlertPayOrderRepo.Add(values);

            foreach (var item in Details)
            {
                var preAlertPayOrderDetail = new PreAlertPayOrderDetail
                {
                        
                    key = item.key,
                    Amount = item.Amount,
                    BankBranchId = values.BankBranchId,
                    BankId = values.BankId,
                    Bvp = values.Bvp,
                    ChequeNumber = values.ChequeNumber,
                    IsPreAlertPayOrder = item.IsPreAlertPayOrder,
                    ShippingAgentName = item.ShippingAgentName,
                    ShippingLineName = item.ShippingLineName,
                    PreAlertPayOrderDate = item.PreAlertPayOrderDate,
                    Remarks = item.Remarks,
                    PreAlertPayOrderId = values.PreAlertPayOrderId
                    //OperationDprtId = item.OperationDprtId,
                    //PartyId = item.PartyId,
                };

                PreAlertPayOrderDetails.Add(preAlertPayOrderDetail); 
            }

            _preAlertPayOrderDetailRepo.AddRange(PreAlertPayOrderDetails);



            var pocount = _preAlertPayOrderRepo.GetPreAlertPayOrdersCount(values.RequestNumber).OrderBy(x => x.ContainerNumber).Count();

            var payordr = _preAlertPayOrderRepo.GetAll().Where(x => x.RequestNumber == values.RequestNumber).ToList();
 
            var peralterdetail = new List<PreAlertPayOrderDetail>();


            foreach (var item in payordr)
            {
                var xxx = _preAlertPayOrderDetailRepo.GetAll().Where(x => x.PreAlertPayOrderId == item.PreAlertPayOrderId).ToList();

                peralterdetail.AddRange(xxx);  
            }


             
            if (pocount == peralterdetail.Count())
            {
                var pu = _paymentUpdateRepo.GetFirst(d=> d.RequestNumber == values.RequestNumber);
                pu.Status = true;
                _paymentUpdateRepo.Update(pu);
            }

            //return new OkObjectResult(new { error = false, Message = "Saved" });
            return new OkObjectResult(new { error = false, message = "created", PayOrderNo = values.PreAlertPayOrderNumber  });


        }


        public string GenPreAlertPayOrderNumber()
        {
            var date = DateTime.Now;

            var PaymentUpdateNumber = _preAlertPayOrderRepo.GeneratePayOrderNumber();

            return  "AICT-" + date.ToString("yy")  + date.ToString("MM") + PaymentUpdateNumber;

        }



        public IActionResult UpdatePreAlertPayorder()
        {
            return View();
        }


        public JsonResult GetPreAlertPayorderByDate(DateTime fromDate, DateTime todate)
        {
            var result = _preAlertPayOrderRepo.GetList(x =>
            Convert.ToDateTime(x.PayOrderCreatedDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(fromDate.Date.ToString("MM/dd/yyyy"))
            &&
            Convert.ToDateTime(x.PayOrderCreatedDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(todate.Date.ToString("MM/dd/yyyy"))
            , y => y.PreAlertPayOrderDetails).ToList();
            return Json(result);
        }

        public IActionResult DeletePreAlertPayOrder(long key)
        {
            var payOrder = _preAlertPayOrderRepo.Find(key);
            var payOrderlist = _preAlertPayOrderDetailRepo.GetAll().Where(x=> x.PreAlertPayOrderId == key).ToList();

            if(payOrder  != null)
            {
                var paymentupdate = _paymentUpdateRepo.GetAll().Where(x=> x.RequestNumber == payOrder.RequestNumber).LastOrDefault();

                if(paymentupdate != null)
                {
                    paymentupdate.Status = false;
                    _paymentUpdateRepo.Update(paymentupdate);
                }

            }

            _preAlertPayOrderRepo.Delete(payOrder);
            _preAlertPayOrderDetailRepo.DeleteRange(payOrderlist);

            return new OkObjectResult(new { error = false, Message = "Deleted" });

        }

        public IActionResult UpdatePreAlertPayOrderDetail(PreAlertPayOrderDetail data)
        {
           _preAlertPayOrderDetailRepo.Update(data);

            return new OkObjectResult(new { error = false, Message = "Updated" });

        }

    }
}
