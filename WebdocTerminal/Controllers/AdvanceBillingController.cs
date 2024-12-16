using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebdocTerminal.Data;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvanceBillingController : ControllerBase
    {
        private IInvoiceRepository _invoiceRepo;
        private APICallsController _aPICalls;
        private ICYContainerRepository _cyContaienrRepo;
        private IUsersRepository _userRepo;
        private IContainerRepository _containerRepo;
        private ApplicationDbContext Db;
        public AdvanceBillingController(IInvoiceRepository invoiceRepo, ICYContainerRepository cyContaienrRepo, IUsersRepository userRepo, IContainerRepository containerRepo, ApplicationDbContext dbContext)
        {
            _invoiceRepo = invoiceRepo;
            _cyContaienrRepo = cyContaienrRepo;
            _userRepo = userRepo;
            _containerRepo = containerRepo;
            Db = dbContext;
            //_aPICalls = aPICalls;
        }
        // HTTP GET method to generate the bill for CY containers (Noman Ali)
        [HttpGet("GetList")]
        public IActionResult GetList(string igm, int indexNo, DateTime BillDate)
        {
            try
            {
                var storageTotal = 0; // Replace 'StorageTotal' with the actual property name
                var _storageDays = 0;   // Replace 'StorageDays' with the actual property name
                var _sealcharges = 0;
                int ClearingAgent = 72193;

                var GateInDate = Db.CYContainers.Where(c => c.VIRNo == igm && c.IndexNo == indexNo && c.IsDeleted == false).Select(c => c.CYContainerGateInDate).FirstOrDefault();

                var containerCount = Db.CYContainers
                       .Where(c => c.VIRNo == igm &&
                                   c.IndexNo == indexNo &&
                                   c.IsDeleted == false)
                       .Count();
                var indexcargotype = Db.CYContainers.Where(c => c.VIRNo == igm && c.IndexNo == indexNo && c.IsDeleted == false).Select(c => c.Status).FirstOrDefault();

                // Call the repository method to get the data
                var result = _invoiceRepo.GetSizeTotal(
                    igm,
                    indexNo,
                    ClearingAgent,
                    DateTime.Parse(GateInDate.ToString()),
                    containerCount,
                    indexcargotype,
                     BillDate //DateTime.Parse("11/22/2024 12:00:00 AM")
                );

                var storage = _invoiceRepo.GetStorageTotalCY(igm, indexNo, BillDate, DateTime.Parse(GateInDate.ToString()), ClearingAgent, indexcargotype);

                var _sealCharges2 = GetInvoiceCY(igm, indexNo); // GateinDate

                var storageDaysAmount = GetCYContainerListBYIGM(igm, indexNo);

                var totalAmount = result.Sum(item => item.Amount);

                // Check if the result contains the properties you need
                if (storage != null)
                {
                    // Assuming `storage` is a single object
                    storageTotal = (int)storage.StorageTotal; // Replace 'StorageTotal' with the actual property name
                    _storageDays = storage.StorageDays;   // Replace 'StorageDays' with the actual property name
                }

                if (_sealCharges2 != 0)
                {

                    _sealcharges = _sealCharges2;
                }

                var totalStorageDaysAmount = storageDaysAmount * _storageDays;

                totalAmount += _sealCharges2 + totalStorageDaysAmount + storageTotal;

                var totalAmountGST1 = totalAmount * 0.15;

                var totalwithGST = totalAmount + totalAmountGST1;

                return Ok(new Dictionary<string, object>
                        {
                    {"Totalwithoutgst",Math.Round(totalAmount) },
                    {"Totalwithgst",Math.Round(totalwithGST) },

                        });
            }
            catch (Exception)
            {
                // Handle and log exceptions
                return NotFound();
            }
        }

        // HTTP GET method to generate the bill for CFS containers (Noman Ali)
        [HttpGet("GetListCFS")]
        public IActionResult GetListCFS(string igm, int indexNo, DateTime BillDate, double? cbm)
        {
            try
            {
                int ClearingAgent = 72193;

                var GateInDate = Db.ContainerIndices.Where(c => c.VirNo == igm && c.IndexNo == indexNo && c.IsDeleted == false).Select(c => c.CFSContainerGateInDate).FirstOrDefault();

                var containerIndexID = Db.ContainerIndices.Where(c => c.VirNo == igm && c.IndexNo == indexNo && c.IsDeleted == false).Select(c => c.ContainerIndexId).FirstOrDefault();

                var containerCount = Db.ContainerIndices
                       .Where(c => c.VirNo == igm &&
                                   c.IndexNo == indexNo &&
                                   c.IsDeleted == false)
                       .Count();

                var indexcargotype = Db.ContainerIndices.Where(c => c.VirNo == igm && c.IndexNo == indexNo && c.IsDeleted == false).Select(c => c.CargoType).FirstOrDefault();
                var _ActualWeight = GetInvoice_Weight(containerIndexID);
                var _CBM = GetInvoice_CBM(containerIndexID);

                if (cbm != null)
                {
                    _CBM = cbm.Value;
                }

                var destuffDate = Db.DestuffedContainers
                         .Where(dc => dc.ContainerIndexId == containerIndexID)
                         .Select(dc => dc.TellySheet.DestuffDate)
                         .FirstOrDefault();
                var result = _invoiceRepo.GetStorageTotal(containerIndexID, ClearingAgent, BillDate, DateTime.Parse(GateInDate.ToString()), DateTime.Parse(destuffDate.ToString()), _ActualWeight, _CBM, indexcargotype);

                var v2 = GetCBMTariffs(containerIndexID, ClearingAgent, _ActualWeight, _CBM, indexcargotype, DateTime.Parse(GateInDate.ToString()), BillDate);

                var totalAmount = v2 + result.StorageTotal;

                var totalAmountGST1 = totalAmount * 0.15;

                var totalwithGST = totalAmount + totalAmountGST1;

                return Ok(new Dictionary<string, object>
                        {
                            {"Totalwithoutgst",Math.Round(totalAmount) },
                            {"Totalwithgst",Math.Round(totalwithGST) },
                            {"CBM",_CBM}
                        });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public int GetInvoice_Weight(long ContainerIndexId)
        {
            var resdata = new InvoiceViewModel();

            var invoice = _invoiceRepo.GetInvoice(ContainerIndexId);



            var resuser = GetLoginUserInfo();

            if (resuser != 0 && resuser != invoice.ShippingAgentId)
            {
                resdata.SealChargers = 0;
                resdata.SalesTax = 0;

            }

            var ffuserslist = GetFFUsers();

            if (ffuserslist.Count() > 0 && ffuserslist.Where(x => x.ShippingAgentId == invoice.ShippingAgentId).Count() > 0 && resuser == 0)
            {
                invoice.IsLinkedWithFF = true;
            }

            int _ActualWeight = (int)invoice.ActualWeight;
            return _ActualWeight;
        }
        public double GetInvoice_CBM(long ContainerIndexId)
        {
            var resdata = new InvoiceViewModel();

            var invoice = _invoiceRepo.GetInvoice(ContainerIndexId);

            var resuser = GetLoginUserInfo();

            if (resuser != 0 && resuser != invoice.ShippingAgentId)
            {
                resdata.SealChargers = 0;
                resdata.SalesTax = 0;

            }

            var ffuserslist = GetFFUsers();

            if (ffuserslist.Count() > 0 && ffuserslist.Where(x => x.ShippingAgentId == invoice.ShippingAgentId).Count() > 0 && resuser == 0)
            {
                invoice.IsLinkedWithFF = true;
            }

            double _CBM = invoice.CBM.Value;
            return _CBM;
        }
        public double GetCBMTariffs(long ContainerIndexId, long clearingAgentId, double Weight, double CBM, string indexcargotype, DateTime gateInDate, DateTime BillDate)
        {
            var items = _invoiceRepo.GetCBMTotal(ContainerIndexId, clearingAgentId, Weight, CBM, indexcargotype, gateInDate, BillDate);

            double total = 0;
            foreach (var item in items)
            {
                total += item.Amount;
            }

            return total;
        }
        public int GetInvoiceCY(string igm, int indexNo)
        {
            var resdata = new InvoiceViewModel();
            //var cyContainer = _cyContaienrRepo.GetFirst(s => s.VIRNo == igm && s.IndexNo == indexNo);
            var cyContainer = _cyContaienrRepo.GetContainerCYByIGMAndIndex(igm, indexNo);
            var item = _invoiceRepo.GetInvoiceCY(cyContainer != null ? cyContainer.CYContainerId : 0);
            var resuser = GetLoginUserInfo();
            if (resuser != 0 && resuser != item.ShippingAgentId)
            {
                resdata.SealChargers = 0;
                resdata.SalesTax = 0;
                return 0;
            }
            var ffuserslist = GetFFUsers();
            if (ffuserslist.Count() > 0 && ffuserslist.Where(x => x.ShippingAgentId == item.ShippingAgentId).Count() > 0 && resuser == 0)
            {
                item.IsLinkedWithFF = true;
            }
            int SCharges = (int)item.SealChargers;

            return SCharges;
        }
        [HttpGet]
        public int GetCYContainerListBYIGM(string igm, long indexNo)
        {
            var data = _containerRepo.GetCYContainerListBYIGM(igm, indexNo).ToList();
            if (data.Count() > 0)
            {
                int totalContainerAmount = (int)data.Sum(item => item.ContainerAmount); // Replace 'ContainerAmount' with the actual property name

                return totalContainerAmount;
            }

            return 0;

        }
        private JsonResult Json(InvoiceViewModel item)
        {
            throw new NotImplementedException();
        }
        public long GetLoginUserInfo()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var appUser = _userRepo.GetFirst(e => e.IdentityUserId == userId);

            if (appUser != null && appUser.ShippingAgentId != null)
            {
                return appUser.ShippingAgentId ?? 0;
            }

            return 0;

        }
        public List<User> GetFFUsers()
        {
            var appUser = _userRepo.GetAll().Where(x => x.ShippingAgentId != null).ToList();

            return appUser;

        }
    }
}
