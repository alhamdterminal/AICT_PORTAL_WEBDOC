using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;


namespace WebdocTerminal.Areas.Export.Controllers
{
    public class ExportSetupController : ParentController
    {
        public IShipperExportRepository _shipperExportRepository;
        public IShippingLineExportRepository _shippingLineExportRepository;
        public IPortAndTerminalExportRepository _portAndTerminalExportRepository;
        public IExportConsigneeRepository _exportConsigneeRepository;
        public IDeliveredYardExportRepository _deliveredYardExportRepository;
        public IOtherChargeExportRepository _otherChargeExportRepository;
        public IOtherChargeAssigningExportRepository _otherChargeAssigningExportRepository;
        private IUsersRepository _userRepo;
        private IPortChargeExportRepository _portChargeExportRepository;

        public ExportSetupController(IShipperExportRepository shipperExportRepository,
                           IShippingLineExportRepository shippingLineExportRepository,
                           IPortAndTerminalExportRepository portAndTerminalExportRepository,
                           IExportConsigneeRepository exportConsigneeRepository,
                           IDeliveredYardExportRepository deliveredYardExportRepository,
                           IOtherChargeExportRepository otherChargeExportRepository,
                           IOtherChargeAssigningExportRepository otherChargeAssigningExportRepository,
                           IUsersRepository userRepo,
                           IPortChargeExportRepository portChargeExportRepository)
        {
            _shipperExportRepository = shipperExportRepository;
            _shippingLineExportRepository = shippingLineExportRepository;
            _portAndTerminalExportRepository = portAndTerminalExportRepository;
            _exportConsigneeRepository = exportConsigneeRepository;
            _deliveredYardExportRepository = deliveredYardExportRepository;
            _otherChargeExportRepository = otherChargeExportRepository;
            _otherChargeAssigningExportRepository = otherChargeAssigningExportRepository;
            _userRepo = userRepo;
            _portChargeExportRepository = portChargeExportRepository;
        }

        #region Shipper Export

        public IActionResult ShipperExportView()
        {
            return View();
        }
        public JsonResult GetShipperExport()
        {
            var data = _shipperExportRepository.GetAll();
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddShipperExport(ShipperExport values)
        {

            var data = _shipperExportRepository.GetAll().Where(x => x.NTNNumber == values.NTNNumber).FirstOrDefault();
            if (data != null)
            {
                return new JsonResult(new { error = true, message = "This Shipper NTN Already Exist" });
            }

            _shipperExportRepository.Add(values);

            return new JsonResult(new { error = false, message = "Saved" });


        }


        [HttpPost]
        public IActionResult UpdateShipperExport(ShipperExport values)
        {
            var shipper = _shipperExportRepository.GetAll().Where(x => x.ShipperName.Trim().ToUpper() == values.ShipperName.Trim().ToUpper() && x.ShipperExportId != values.ShipperExportId).FirstOrDefault();

            if (shipper != null)
            {
                return new JsonResult(new { error = true, message = "This Shipper Already Exist" });
            }

            _shipperExportRepository.Update(values);

            return new JsonResult(new { error = false, message = "Update" });
        }

        #endregion
         
        #region ShippingLine Export

        public IActionResult ShippingLineExportView()
        {
            return View();
        }
        public JsonResult GetShippingLineExport()
        {
            var data = _shippingLineExportRepository.GetAll();
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddShippingLineExport(ShippingLineExport values)
        {

            var data = _shippingLineExportRepository.GetAll().Where(x => x.NTNNumber == values.NTNNumber).FirstOrDefault();
            if (data != null)
            {
                return new JsonResult(new { error = true, message = "This NTN Already Exist" });
            }

            _shippingLineExportRepository.Add(values);

            return new JsonResult(new { error = false, message = "Saved" });


        }


        [HttpPost]
        public IActionResult UpdateShippingLineExport(ShippingLineExport values)
        {
            var shipper = _shippingLineExportRepository.GetAll().Where(x => x.ShippingLineName.Trim().ToUpper() == values.ShippingLineName.Trim().ToUpper() && x.ShippingLineExportId != values.ShippingLineExportId).FirstOrDefault();

            if (shipper != null)
            {
                return new JsonResult(new { error = true, message = "Already Exist" });
            }

            _shippingLineExportRepository.Update(values);

            return new JsonResult(new { error = false, message = "Update" });
        }

        #endregion
         
        #region Port And Terminal Export

        public IActionResult PortAndTerminalExportView()
        {
            return View();
        }
        public JsonResult GetPortAndTerminalExport()
        {
            var data = _portAndTerminalExportRepository.GetAll();
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddPortAndTerminalExport(PortAndTerminalExport values)
        {

            var data = _portAndTerminalExportRepository.GetAll().Where(x => x.PortName == values.PortName).FirstOrDefault();
            if (data != null)
            {
                return new JsonResult(new { error = true, message = "This Already Exist" });
            }

            _portAndTerminalExportRepository.Add(values);

            return new JsonResult(new { error = false, message = "Saved" });


        }


        [HttpPost]
        public IActionResult UpdatePortAndTerminalExport(PortAndTerminalExport values)
        {

            var rees = _portAndTerminalExportRepository.GetAll().Where(x => x.PortName.Trim().ToUpper() == values.PortName.Trim().ToUpper() && x.PortAndTerminalExportId != values.PortAndTerminalExportId).FirstOrDefault();

            if (rees != null)
            {
                return new JsonResult(new { error = true, message = "Already Exist" });
            }

            _portAndTerminalExportRepository.Update(values);

            return new JsonResult(new { error = false, message = "Update" });
        }

        #endregion
         
        #region consignee Export

        public IActionResult ExportConsigneeView()
        {
            return View();
        }
        public JsonResult GetconsigneeExport()
        {
            var data = _exportConsigneeRepository.GetAll();
            return Json(data);
        }

        [HttpPost]
        public IActionResult AdconsigneeExport(ExportConsignee values)
        {

            var data = _exportConsigneeRepository.GetAll().Where(x => x.ConsigneName == values.ConsigneName ).FirstOrDefault();
            if (data != null)
            {
                return new JsonResult(new { error = true, message = "Name Already Exist" });
            }

            var datantn = _exportConsigneeRepository.GetAll().Where(x => x.ConsigneNTN == values.ConsigneNTN).FirstOrDefault();
            if (datantn != null)
            {
                return new JsonResult(new { error = true, message = "NTN Already Exist" });
            }

            _exportConsigneeRepository.Add(values);

            return new JsonResult(new { error = false, message = "Saved" });


        }


        [HttpPost]
        public IActionResult UpdateExportConsignee(ExportConsignee values)
        {

            var rees = _exportConsigneeRepository.GetAll().Where(x => x.ConsigneName.Trim().ToUpper() == values.ConsigneName.Trim().ToUpper() && x.ExportConsigneeId != values.ExportConsigneeId).FirstOrDefault();

            if (rees != null)
            {
                return new JsonResult(new { error = true, message =  "Name Already  Exist" });
            }


            var reesntn = _exportConsigneeRepository.GetAll().Where(x => x.ConsigneNTN.Trim().ToUpper() == values.ConsigneNTN.Trim().ToUpper() && x.ExportConsigneeId != values.ExportConsigneeId).LastOrDefault();

            if (reesntn != null)
            {
                return new JsonResult(new { error = true, message = "NTN Already Exist" });
            }

            _exportConsigneeRepository.Update(values);

            return new JsonResult(new { error = false, message = "Update" });
        }

        #endregion

        #region Delivered Yard Export

        public IActionResult DeliveredYardExportView()
        {
            return View();
        }
        public JsonResult GetDeliveredYardExpor()
        {
            var data = _deliveredYardExportRepository.GetAll();
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddDeliveredYardExport(DeliveredYardExport values)
        {

            var data = _deliveredYardExportRepository.GetAll().Where(x => x.DeliveredYardName == values.DeliveredYardName ).FirstOrDefault();
            if (data != null)
            {
                return new JsonResult(new { error = true, message = "Already Exist" });
            }


            _deliveredYardExportRepository.Add(values);

            return new JsonResult(new { error = false, message = "Saved" });


        }


        [HttpPost]
        public IActionResult UpdateDeliveredYardExport(DeliveredYardExport values)
        {

            var rees = _deliveredYardExportRepository.GetAll().Where(x => x.DeliveredYardName.Trim().ToUpper() == values.DeliveredYardName.Trim().ToUpper() && x.DeliveredYardExportId != values.DeliveredYardExportId).FirstOrDefault();

            if (rees != null)
            {
                return new JsonResult(new { error = true, message =  "Already  Exist" });
            }
             
            _deliveredYardExportRepository.Update(values);

            return new JsonResult(new { error = false, message = "Update" });
        }

        #endregion
         
        #region Other Charge Export
        public IActionResult OtherChargeExportView()
        {
            return View();
        }

        public JsonResult GetOtherCharge()
        {
            var OtherCharges = _otherChargeExportRepository.GetAll();
             
            return Json(OtherCharges);
 

        }


        [HttpPost]
        public IActionResult AddOtherCharge(OtherChargeExport data)
        {
            _otherChargeExportRepository.Add(data);

            return new OkObjectResult(new { error = false, Message = "Saved" });
        }

        [HttpPost]
        public IActionResult UpdateOtherCharge(OtherChargeExport data)
        {
            _otherChargeExportRepository.Update(data);
            return new OkObjectResult(new { error = false, Message = "Saved" });
        }




        #endregion
         
        #region Other Charge Assigning Export
        public IActionResult OtherChargeAssigningExportView()
        {
            return View();
        }

        public JsonResult GetOtherChargeAssigningexport(string gdnumber)
        {

            var OtherChargeAssignings = _otherChargeAssigningExportRepository.GetAll().Where(x => x.GDNumber == gdnumber).ToList();
             
            return Json(OtherChargeAssignings);
           
        }

        [HttpPost]
        public IActionResult AddOtherChargeAssigningExport(string gdnumber, OtherChargeAssigningExport data)
        {

            var userIdentity = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userRepo.GetAll().Where(x => x.IdentityUserId == userIdentity).FirstOrDefault();
            var UserName = user.FirstName + " - " + user.LastName;
           if (data.ChargeQuantity != 0)
                    {
                        data.ChargeAmount = data.ChargeAmount * data.ChargeQuantity;
                    }

                    if (data.ChargeQuantity == 0)
                    {
                        data.ChargeQuantity = 1;
                        data.ChargeAmount = data.ChargeAmount * data.ChargeQuantity;
                    }



                    data.GDNumber = gdnumber;
                    data.InvoiceCreated = false;
                    data.CreatedDate = DateTime.Now;
                    data.UserName = UserName;
            _otherChargeAssigningExportRepository.Add(data);
                    return new OkObjectResult(new { error = false, Message = "Saved" });
                    //}

               
       
        }

        public IActionResult DeleteOtherChargeAssigned(long key)
        {


            var data = _otherChargeAssigningExportRepository.Find(key);

            if (data != null)
            {

                 
                    if (data.InvoiceCreated == true)
                    {
                        return new OkObjectResult(new { error = true, Message = "Invoice Created" });
                    }

                    _otherChargeAssigningExportRepository.Delete(data);

                    return new OkObjectResult(new { error = false, Message = "Deleted" });
                    
                }
             
            return new OkObjectResult(new { error = true, Message = "not found" });


        }


        #endregion

        #region port charges export


        public IActionResult PortChargesExportView()
        {
            return View();
        }

        public JsonResult GetPortChargesDetail(string gdnumber)
        {
            var res = _portChargeExportRepository.GetAll().Where(x => x.GDNumber == gdnumber).ToList();

            return Json(res);
        }
         
        #endregion



    }
}