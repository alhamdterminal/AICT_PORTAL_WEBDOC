using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class ExportDeliveryOrderRepository : RepoBase<ExportDeliveryOrder>, IExportDeliveryOrderRepository
    {
        public ExportDeliveryOrderRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<string> GetGDsWithoutDOs()
        {
            var gds = (from enteringCargo in Db.EnteringCargos
                       where !Db.InvoiceExports.Any(es => (es.EnteringCargoId == enteringCargo.EnteringCargoId))
                       select enteringCargo.GDNumber).ToList();

            return gds;
        }

        public ExportDeliveryOrderViewModel GetDeliveryOrderDisplayInfo(string gdnmuber)
        {
            var displayInfo = (//from lp in Db.LoadingPrograms
                               //join entering in Db.EnteringCargos on lp.LoadingProgramNumber equals entering.LoadingProgramNumber
                               // join ocrl in Db.OCRLs on entering.GDNumber.Trim().ToUpper() equals ocrl.GDNumber.Trim().ToUpper()
                               from entering in Db.EnteringCargos
                               from lp in Db.LoadingPrograms.Where(x => x.LoadingProgramNumber == entering.LoadingProgramNumber).DefaultIfEmpty()
                               from ocrl in Db.OCRLs.Where(x => x.GDNumber.Trim().ToUpper() == gdnmuber.Trim().ToUpper() && x.Status == "OD").DefaultIfEmpty()
                               from opia in Db.OPIAs.Where(x => x.GDNumber.Trim().ToUpper() == gdnmuber.Trim().ToUpper()).DefaultIfEmpty()
                               from gatein in Db.GateInExports.Where(x => x.GDNumber.Trim().ToUpper() == gdnmuber.Trim().ToUpper()).DefaultIfEmpty()
                               from invoice in Db.InvoiceExports.Where(i => i.EnteringCargoId == entering.EnteringCargoId).DefaultIfEmpty()

                               where
                                entering.GDNumber.Trim().ToUpper() == gdnmuber.Trim().ToUpper()

                               select new ExportDeliveryOrderViewModel
                               {
                                   EnteringCargoId = entering != null ? entering.EnteringCargoId : 0,
                                   DrayOffStatus = ocrl != null ? ocrl.Status : "",
                                   InvoiceNo = invoice != null ? invoice.InvoiceNo : "",
                                   ClearingAgent = opia != null ? opia.ClearingAgentName : "",
                                   ChallanNumber = opia != null ? opia.ClearingAgentChallanNumber : "",
                                   Shipper = opia != null ? opia.ConsignorName : "",
                                   NoOfPackages = Convert.ToInt32(opia.NoOfPackages),
                                   GdDate = opia != null ? opia.Performed : null,
                                   Commodity = "",
                                   GDNumber = entering.GDNumber,
                                   AgentRepresentative = invoice != null ? invoice.ClearingAgentRep : "",
                                   AgentRepresentativeCNIC = invoice != null ? invoice.CNIC : ""

                               }).FirstOrDefault();

            return displayInfo;

        }


        public ExportDeliveryOrderViewModel GetExportGatePassDisplayInfo(string gdnmuber)
        {
            var displayInfo = (from entering in Db.EnteringCargos
                               from lp in Db.LoadingPrograms.Where(x => x.LoadingProgramNumber == entering.LoadingProgramNumber).DefaultIfEmpty()
                               from ocrl in Db.OCRLs.Where(x => x.GDNumber.Trim().ToUpper() == gdnmuber.Trim().ToUpper() && x.Status == "OD").DefaultIfEmpty()
                               from opia in Db.OPIAs.Where(x => x.GDNumber.Trim().ToUpper() == gdnmuber.Trim().ToUpper()).DefaultIfEmpty()
                               from gatein in Db.GateInExports.Where(x => x.GDNumber.Trim().ToUpper() == gdnmuber.Trim().ToUpper()).DefaultIfEmpty()
                               from invoice in Db.InvoiceExports.Where(i => i.EnteringCargoId == entering.EnteringCargoId).DefaultIfEmpty()
                               from exportDeliveryOrder in Db.ExportDeliveryOrders.Where(i => i.GDNumber == gdnmuber).DefaultIfEmpty()

                               where
                                entering.GDNumber.Trim().ToUpper() == gdnmuber.Trim().ToUpper()

                               select new ExportDeliveryOrderViewModel
                               {
                                   EnteringCargoId = entering != null ? entering.EnteringCargoId : 0,
                                   DrayOffStatus = ocrl != null ? ocrl.Status : "",
                                   InvoiceNo = invoice != null ? invoice.InvoiceNo : "",
                                   ClearingAgent = opia != null ? opia.ClearingAgentName : "",
                                   ChallanNumber = opia != null ? opia.ClearingAgentChallanNumber : "",
                                   Shipper = opia != null ? opia.ConsignorName : "",
                                   NoOfPackages = Convert.ToInt32(opia.NoOfPackages),
                                   GdDate = opia != null ? opia.Performed : null,
                                   Commodity = "",
                                   GDNumber = entering.GDNumber,
                                   AgentRepresentative = invoice != null ? invoice.ClearingAgentRep : "",
                                   AgentRepresentativeCNIC = invoice != null ? invoice.CNIC : "",
                                   ExportDeliveryOrderId = exportDeliveryOrder != null ? exportDeliveryOrder.ExportDeliveryOrderId : 0

                               }).FirstOrDefault();


            if (displayInfo != null)
            {
                var gatepass = Db.GatePassExports.FromSql($"select   *  from GatePassExport   where GDNumber  = {displayInfo.GDNumber} and   IsDeleted = 0").ToList();



                if (gatepass.Count() > 0)
                {
                    displayInfo.TotalDelivered = gatepass.Sum(x => x.TotalDelivered);
                    displayInfo.BalancePackages = displayInfo.NoOfPackages - displayInfo.TotalDelivered;


                }
                else
                {
                    displayInfo.TotalDelivered = 0;
                    displayInfo.BalancePackages = displayInfo.NoOfPackages;
                }


                var listofdoitemsexport = new List<DOItemExport>();
                foreach (var item in gatepass)
                {
                    var doitem = Db.DOItemExports.FromSql($"select   *  from DOItemExport   where GatePassExportId  = {item.GatePassExportId} and   IsDeleted = 0").ToList();

                    if (doitem.Count() > 0)
                    {
                        listofdoitemsexport.AddRange(doitem);
                    }
                }

                displayInfo.DOItemExports = listofdoitemsexport;


            }



            return displayInfo;

        }




        public long PreviousDONumber()
        {
            var lastDo = Table.OrderByDescending(c => c.DONumber).FirstOrDefault();
            return lastDo != null ? lastDo.DONumber : 0;
        }


        public List<OCRL> GetUnDeliveredGDS()
        {
            var res = Db.OCRLs.FromSql($"select ocrls.* from ocrls  join EnteringCargo on OCRLs.GDNumber = EnteringCargo.GDNumber left join ExportDeliveryOrders on EnteringCargo.EnteringCargoId = ExportDeliveryOrders.EnteringCargoId where ocrls.Status = 'OD' and ExportDeliveryOrders.EnteringCargoId is null").ToList();

            return res;

        }

        public List<ExportDeliveryOrder> GetUnDeliveredGatePassGDS()
        {
            var res = Db.ExportDeliveryOrders.FromSql($"  select *   from ExportDeliveryOrders  where    IsDeleted = 0 and  IsDeivered = 0").ToList();

            return res;

        }

        public List<GatePassExport> GetGatePassbyGDnumber(string gdnumber)
        {
            var res = Db.GatePassExports.FromSql($"select   * from GatePassExport   where GDNumber = {gdnumber} and IsDeleted = 0").ToList();

            return res;

        }


        public DOItemExport GetDOITemById(long id)
        {
            var res = Db.DOItemExports.FromSql($"select   * from doitemexport   where DOItemExportId = {id} and IsDeleted = 0").LastOrDefault();

            return res;

        }

        public GatePassExport GetGatepassExportById(long id)
        {
            var res = Db.GatePassExports.FromSql($"select   * from GatePassExport   where GatePassExportId = {id} and IsDeleted = 0").LastOrDefault();

            return res;

        }


        public List<GatePassExport> GetGatepassExportByGDNumber(string gdumber)
        {
            var res = Db.GatePassExports.FromSql($"select   * from GatePassExport   where GDNumber = {gdumber} and IsDeleted = 0").ToList();

            return res;

        }

        public DOItemExport GetDOItemExportByID(long id)
        {
            var res = Db.DOItemExports.FromSql($"select   * from DOItemExport   where GatePassExportId = {id} and IsDeleted = 0").LastOrDefault();

            return res;

        }

        public ExportDeliveryOrder GetExportDeliveryOrderByGDnumber(string gdnumber)
        {
            var res = Db.ExportDeliveryOrders.FromSql($"select   * from ExportDeliveryOrders   where GDNumber = {gdnumber} and IsDeleted = 0").LastOrDefault();

            return res;

        }



        public List<ExportDeliveryOrderViewModel> GetExportGatePassInfo(string gdnmuber)
        {
            var displayInfo = (from gatepassexpor in Db.GatePassExports
                               join doitemexport in Db.DOItemExports on gatepassexpor.GatePassExportId equals doitemexport.GatePassExportId

                               where
                                gatepassexpor.GDNumber.Trim().ToUpper() == gdnmuber.Trim().ToUpper()

                               select new ExportDeliveryOrderViewModel
                               {

                                   TotalDelivered = gatepassexpor.TotalDelivered,
                                   Remarks = gatepassexpor.Remarks,
                                   GDNumber = gatepassexpor.GDNumber,
                                   TruckNumber = doitemexport.TruckNumber,
                                   Status = doitemexport.Status,
                                   GatePassExportId = gatepassexpor.GatePassExportId,
                                   DOItemExportId = doitemexport.DOItemExportId



                               }).ToList();


            return displayInfo;

        }


    }
}
