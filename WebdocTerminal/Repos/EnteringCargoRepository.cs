using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;
 

namespace WebdocTerminal.Repos
{
    public class EnteringCargoRepository : RepoBase<EnteringCargo>, IEnteringCargoRepository
    {
        public EnteringCargoRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public ExportCargoViewModel GetCargoDetails(string gdnumber)
        {
            var cargoDetails = (from cargo in Db.EnteringCargos
                                join lp in Db.LoadingPrograms on cargo.LoadingProgramNumber equals lp.LoadingProgramNumber
                                join vesselExport in Db.VesselExports on lp.VesselExportId equals vesselExport.VesselExportId
                                join voyageExport in Db.VoyageExports on lp.VoyageExportId equals voyageExport.VoyageExportId
                                where cargo.GDNumber.Trim().ToUpper() == gdnumber.Trim().ToUpper()
                                select new ExportCargoViewModel
                                {
                                    GD = cargo.GDNumber,
                                    LpNo = lp.LoadingProgramNumber,
                                    Vessel = vesselExport.VesselName,
                                    Voyage = voyageExport.VoyageNumber,
                                    CargoId = cargo.EnteringCargoId

                                }).FirstOrDefault();

            return cargoDetails;
        }

        public List<GDInvoicesViewModel> GetGDInvoices(string GDNumber)
        {
            var data = (from enteringCargo in Db.EnteringCargos
                        join invoiceExport in Db.InvoiceExports on enteringCargo.EnteringCargoId equals invoiceExport.EnteringCargoId
                        where
                        enteringCargo.GDNumber.Trim().ToUpper() == GDNumber.Trim().ToUpper()
                        &&
                        invoiceExport.IsDeleted == false && invoiceExport.IsCancelled == false
                        select new GDInvoicesViewModel
                        {
                            InvoiceExportId = invoiceExport.InvoiceExportId,
                            InvoiceNo = invoiceExport.InvoiceNo,
                            IsSubBill = invoiceExport.IsSubBill

                        }).ToList();

            return data;

        }


        public List<GDInvoicesViewModel> GetGDInvoicesExprot(string gdnumber)
        {

            var asd = Db.EnteringCargos.FromSql($"select * from EnteringCargo where GDNumber = {gdnumber}  and IsDeleted = 0").LastOrDefault();
            if (asd != null)
            {
                var data = (from invoiceExport in Db.InvoiceExports
                            where
                             invoiceExport.EnteringCargoId == asd.EnteringCargoId
                             &&
                            invoiceExport.IsDeleted == false && invoiceExport.IsCancelled == false
                            select new GDInvoicesViewModel
                            {
                                InvoiceExportId = invoiceExport.InvoiceExportId,
                                InvoiceNo = invoiceExport.InvoiceNo,
                                IsSubBill = invoiceExport.IsSubBill
                            }).ToList();

                return data;
            }



            return null;

        }

        //public List<GDListViewModel> GetGDS(DateTime? StartDate, DateTime? EndDate, string GDNumber, string Type)
        public List<GDListViewModel> GetGDS(string Type)
        {

            var gdList = (from lp in Db.LoadingPrograms
                          join ec in Db.EnteringCargos on lp.GDNumber equals ec.GDNumber
                          from opia in Db.OPIAs.Where(x => x.GDNumber.Trim().ToUpper() == ec.GDNumber.Trim().ToUpper()).DefaultIfEmpty()
                          from gatein in Db.GateInExports.Where(x => x.GDNumber.Trim().ToUpper() == ec.GDNumber.Trim().ToUpper()).DefaultIfEmpty()

                          where
                            //(
                            //   (ec.OPIAReceiveTime >= StartDate
                            //    &&
                            //    ec.OPIAReceiveTime <= EndDate)
                            // ||
                            //(ec.GDNumber.Trim().ToUpper() == GDNumber.Trim().ToUpper())
                            //)
                            !Db.ExportContainerItems.Any(s => s.GDNumber == ec.GDNumber)
                            &&
                            !Db.OCRLs.Any(s => s.GDNumber == ec.GDNumber && s.Status == "OD")

                          select new GDListViewModel
                          {
                              IsDeleted = Type == "ANFStatus" ? true : false,
                              Id = ec.EnteringCargoId,
                              ClearingAgent = opia != null ? opia.ClearingAgentName : "",
                              DateReceived = gatein != null ? gatein.GateInDate : null,
                              GDNumber = ec.GDNumber,
                              NoOfPackages = opia != null ? Convert.ToInt32(opia.NoOfPackages) : 0,
                              Shipper = opia != null ? opia.ConsignorName : "",
                              // PaperReadyDate = ec.PaperReadyDate != null ? ec.PaperReadyDate : null,
                              PaperReadyDate = Type == "ANFStatus" ? (ec.ANFDate != null ? ec.ANFDate : null) : Type == "PaperReady" ? (ec.PaperReadyDate != null ? ec.PaperReadyDate : null) : null,
                              // IsChecked = Type == "ANFStatus" ? (ec.ANFStatus == "CLEARED" ? true : false) : Type == "PaperReady" ? (ec.PaperReady == "SUBMITTED" ? true : false) : false
                              //IsChecked = Type == "ANFStatus" ? (ec.ANFStatus == "CLEARED" ? true : false) : Type == "PaperReady" ? (ec.PaperReady == "SUBMITTED" ? true : false) : false
                          }).ToList();


             
            var containerMerged = (from container in gdList
                                   group container by container.GDNumber into g
                                   select new GDListViewModel
                                   {
                                       IsDeleted = g.FirstOrDefault().IsDeleted,
                                       Id = g.FirstOrDefault().Id,
                                       ClearingAgent = g.FirstOrDefault().ClearingAgent,
                                       DateReceived = g.FirstOrDefault().DateReceived,
                                       GDNumber = g.FirstOrDefault().GDNumber,
                                       NoOfPackages = g.FirstOrDefault().NoOfPackages,
                                       Shipper = g.FirstOrDefault().Shipper,
                                       PaperReadyDate = g.FirstOrDefault().PaperReadyDate,
                                       
                                   }).ToList();


            return containerMerged;
        }

        public EnteringCargoVIewModel GetEnteringCargo(string GDNumber, string LPNumner)
        {
            var cargo = (from lp in Db.LoadingPrograms
                         from ec in Db.EnteringCargos.Where(l => l.GDNumber.Trim().ToUpper() == GDNumber.Trim().ToUpper()).DefaultIfEmpty()
                         from opia in Db.OPIAs.Where(x => x.GDNumber.Trim().ToUpper() == GDNumber.Trim().ToUpper()).DefaultIfEmpty()
                         where lp.LoadingProgramNumber.Trim().ToUpper() == LPNumner.Trim().ToUpper()


                         select new EnteringCargoVIewModel
                         {
                             ChallanNumber = lp != null ? lp.ClearingAgentExport.ChallanNumber : "",
                             ClearingAgent = lp != null ? lp.ClearingAgentExport.ClearingAgentName : "",
                             EnteringCargoId = ec != null ? ec.EnteringCargoId : 0,
                             GateInDate = Db.GateInExports.Where(s => s.GDNumber == ec.GDNumber).Select(s => s.GateInDate).FirstOrDefault(),
                             GrossWeight = opia != null ? opia.GrossWeight : 0,
                             LPNumber = lp != null ? lp.LoadingProgramNumber : "",
                             NoOfPackages = opia != null ? opia.NoOfPackages : 0,
                             PackageType = opia.PackageType,
                             //ShipperName = lp != null ? lp.Shipper.ShipperName : "",
                             ShipperName =   "",
                             Remarks = ec != null ? ec.Remarks : "",
                             ExportVehicles = ec != null ? ec.ExportVehicles : null,
                         }).FirstOrDefault();

            return cargo;

        }


        public EnteringCargoVIewModel GetEnteringCargoByGD(string GDNumber)
        {
            var cargo = (from ec in Db.EnteringCargos
                         from lp in Db.LoadingPrograms.Where(l => l.LoadingProgramNumber == ec.LoadingProgramNumber).DefaultIfEmpty()
                         from opia in Db.OPIAs.Where(x => x.GDNumber.Trim().ToUpper() == GDNumber.Trim().ToUpper()).DefaultIfEmpty()
                         where ec.GDNumber.Trim().ToUpper() == GDNumber.Trim().ToUpper()


                         select new EnteringCargoVIewModel
                         {
                             ChallanNumber = lp != null ? lp.ClearingAgentExport.ChallanNumber : "",
                             ClearingAgent = lp != null ? lp.ClearingAgentExport.ClearingAgentName : "",
                             EnteringCargoId = ec != null ? ec.EnteringCargoId : 0,
                             GateInDate = Db.GateInExports.Where(s => s.GDNumber == ec.GDNumber).Select(s => s.GateInDate).FirstOrDefault(),
                             //GrossWeight = ec != null ? ec.GrossWeight : 0,
                             GrossWeight = opia != null ? opia.GrossWeight : 0,
                             LPNumber = lp != null ? lp.LoadingProgramNumber : "",
                             NoOfPackages = ec != null ? Convert.ToDouble(ec.NumberOfPackages) : 0,
                             PackageType = ec.PackageType,
                             //ShipperName = lp != null ? lp.Shipper.ShipperName : "",
                             ShipperName = "",
                             Remarks = ec != null ? ec.Remarks : "",
                             ExportVehicles = ec != null ? ec.ExportVehicles : null
                         }).FirstOrDefault();

            return cargo;

        }

        public long WaitingforExaminationMarkedGDs()
        {
            var data = (from enteringCargo in Db.EnteringCargos
                        where enteringCargo.isGrounded == false
                        select enteringCargo).ToList();

            if (data == null)
            {
                return 0;
            }

            return data.Count;
        }
        public long GroundedGDs()
        {
            var data = (from enteringCargo in Db.EnteringCargos
                        where enteringCargo.isGrounded == true
                        select enteringCargo).ToList();

            if (data == null)
            {
                return 0;
            }

            return data.Count;
        }


        public EnteringCargo GetEnteringCargo(string gdnumber)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.EnteringCargos.FromSql($"select * from EnteringCargo where GDNumber = {gdnumber}  and IsDeleted = 0").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }



        public List<InvoiceExport> GetInvoicesBygdnumber(long EnteringCargoId)
        {

            var data = Db.InvoiceExports.FromSql($" select * from InvoiceExport where EnteringCargoId = {EnteringCargoId}  and IsDeleted = 0").ToList();

            return data;

        }

        public EnteringCargo GetEnteringCargoById(long enteringCargoById)
        {

            var asd = Db.EnteringCargos.FromSql($"SELECT * From EnteringCargo Where EnteringCargoId = {enteringCargoById} and IsDeleted = 0").FirstOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public List<ExportContainerItem> GetExportContainerItemByExportContainerid(long exportcontainerid)
        {

            var asd = Db.ExportContainerItems.FromSql($"SELECT * From ExportContainerItem Where ExportContainerId = {exportcontainerid} and IsGateOut = 0 and IsDeleted = 0").ToList();

            return asd;

        }


        public InvoiceItemExport GetInvoiceExportitem(long invoiceid, long Tariffid)
        {

            var asd = Db.InvoiceItemExports.FromSql($"SELECT * From InvoiceItemExport Where InvoiceExportId = {invoiceid}  and TariffExportId = {Tariffid}  and IsDeleted = 0").FirstOrDefault();

            return asd;

        }

        public List<GDListViewModel> GetAlongSideGDs()
        {

             var res = Db.GateInExports.FromSql($"SELECT *FROM GateInExports WHERE  NOT EXISTS (SELECT 1    FROM ExportContainerItem   WHERE ExportContainerItem.GDNumber = GateInExports.GDNumber) and 	NOT EXISTS (SELECT 1 FROM OCRLs WHERE OCRLs.GDNumber = GateInExports.GDNumber and OCRLs.Status = 'OD' )").ToList().Select(x=> x.GDNumber).Distinct();


            var gdList = (from gatein in res
                          select new GDListViewModel
                          {
                              GDNumber = gatein,
                          }).ToList();


            //var gdList = (from gatein in Db.GateInExports
            //              where
            //                !Db.ExportContainerItems.Any(s => s.GDNumber == gatein.GDNumber)
            //                &&
            //                !Db.OCRLs.Any(s => s.GDNumber == gatein.GDNumber && s.Status == "OD")
            //              select new GDListViewModel
            //              {
            //                  GDNumber = gatein.GDNumber,
            //              }).ToList();

            return gdList;
        }

    }
}
