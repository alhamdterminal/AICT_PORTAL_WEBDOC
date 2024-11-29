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
    public class ExportContainerRepository : RepoBase<ExportContainer>, IExportContainerRepository
    {
        public ExportContainerRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<ServiceCompletionViewModel> GetUnServiceCompletionContainers()
        {

            var containers = (from exportContainer in Db.ExportContainers
                              where exportContainer.ExaminationArranged == false
                              select new ServiceCompletionViewModel
                              {
                                  ExportContainerId = exportContainer.ExportContainerId,
                                  ContainerNo = exportContainer.ContainerNumber,
                                  VehicleNo = exportContainer.VehicleNumber,
                                  Category = "E",
                                  GDNumber = exportContainer.GDNumber,
                                  ActivityType = "",
                                  Location = "",
                                  Weight = exportContainer.GrossWeight,
                                  NoOfPackages = exportContainer.NumberofPackages,
                                  PackageType = exportContainer.PackageType
                              }).ToList();

            return containers;
        }


        public List<ContainerAssociationViewModel> GetUnContainerAssociationContainers()
        {

            var containers = (from exportContainer in Db.ExportContainers
                              where exportContainer.ContainerAssociated == false
                              select new ContainerAssociationViewModel
                              {
                                  ExportContainerId = exportContainer.ExportContainerId,
                                  ContainerNumber = exportContainer.ContainerNumber,
                                  VehicleNumber = exportContainer.VehicleNumber,
                                  GDNumber = exportContainer.GDNumber,
                                  NumberofPackages = exportContainer.NumberofPackages,
                                  ShippingLineCode = "",
                                  ShippingLineName = "",
                                  ShippingLineNTN = "",
                                  ConsolidationStatus = ""


                              }).ToList();

            return containers;
        }

        public List<GateOutViewModel> GetGateOutExportContainers(string containerNo)
        {

            var containers = (from exportContainer in Db.ExportContainers
                              join exportContainerItem in Db.ExportContainerItems on exportContainer.ExportContainerId equals exportContainerItem.ExportContainerId
                              join osvm in Db.OSVMs on exportContainer.ContainerNumber equals osvm.ContainerNo
                              where exportContainer.IsGateout == false
                              && exportContainer.ContainerNumber == containerNo
                              && osvm.GDNumber == exportContainerItem.GDNumber
                              orderby exportContainerItem.OrderNumber ascending
                              select new GateOutViewModel
                              {
                                  GDNumber = exportContainerItem.GDNumber,
                                  VehicleNumber = osvm.VehicleNo,
                                  PCCSSSeal = osvm.PCCSSSealNumber,
                                  BondedCarrierName = "CARGO LOGISTICS INTERNATIONAL (PVT.) LTD.",
                                  ExportContainerId = exportContainer.ExportContainerId,
                                  ContainerNumber = exportContainer.ContainerNumber,
                                  BondedCarrierNTN = "36280437",
                                  Status = "F",
                                  CountryofExit = "PAKISTAN",
                                  LineSeal = "",
                                  ContainerGrossWeight = 0,
                                  
                                  IsChecked = false
                              }).ToList();

            foreach (var item in containers)
            {
                var dataogde = Db.OGDEs.FromSql($" select * from ogdes where GDNumber = {item.GDNumber}  and IsDeleted = 0").LastOrDefault();

                if (dataogde != null)
                {
                    item.Location = dataogde.MessageTo;
                }
            }

            return containers;

        }

        public List<ExportContainer> GetPendingcontainers()
        {
            var data = (from exportContainer in Db.ExportContainers                        
                        where
                          exportContainer.IsAssociated != true
                        select new ExportContainer
                        {
                            ContainerNumber = exportContainer.ContainerNumber,
                            ExportContainerId = exportContainer.ExportContainerId,

                        }).GroupBy(x => x.ContainerNumber).Select(x => x.LastOrDefault()).ToList();
            return data;
        }

        public List<ExportContainerItemsViewModel> GetPendingGds()
        { 
            var data = (from opiasrs in Db. OPIAs
                        join ocrl in Db.OCRLs on opiasrs.GDNumber.Trim().ToUpper() equals ocrl.GDNumber.Trim().ToUpper()
                         where 
                          ocrl.Status == "AL"
                          && !Db.OCRLs.Any(ss => ss.GDNumber == opiasrs.GDNumber && ss.Status == "OD")
                          && !Db.LoadingPrograms.Any(ss => ss.GDNumber == opiasrs.GDNumber && ss.GDAssociateStatus == true)                         
                        orderby ocrl.GDNumber descending
                        select new ExportContainerItemsViewModel
                        {
                            GDNumber = opiasrs.GDNumber

                        }).GroupBy(x => x.GDNumber).Select(x => x.LastOrDefault()).ToList();


            return data;
        }


        public List<ExportContainerItemsViewModel> GetPendingGdsForAssociation()
        {
            var data = (from opiasrs in Db.OPIAs
                        join ocrl in Db.OCRLs on opiasrs.GDNumber.Trim().ToUpper() equals ocrl.GDNumber.Trim().ToUpper()
                        where
                         ocrl.Status == "AL"
                         && !Db.OCRLs.Any(ss => ss.GDNumber == opiasrs.GDNumber && ss.Status == "OD")
                         && !Db.LoadingPrograms.Any(ss => ss.GDNumber == opiasrs.GDNumber && ss.GDSubmitedStatus == true)
                        orderby ocrl.GDNumber descending
                        select new ExportContainerItemsViewModel
                        {
                            GDNumber = opiasrs.GDNumber

                        }).GroupBy(x => x.GDNumber).Select(x => x.LastOrDefault()).ToList();


            return data;
        }

        public ExportContainerItemsViewModel GetgGdDetail(string Gdnumber)
        {
            var res = new ExportContainerItemsViewModel();

           var opiasres = Db.OPIAs.FromSql($"select * from OPIAs  where GDNumber = {Gdnumber}  and isdeleted = 0").LastOrDefault();
            

            if (opiasres != null)
            {
                res.NoOfPackages = Convert.ToInt32(opiasres.NoOfPackages) - Db.ExportContainerItems.Where(s => s.GDNumber == opiasres.GDNumber).Select(l => l.NumberOfPackages).DefaultIfEmpty(0).Sum();
                
                return res;
            }
            else
            {
                return null;

            } 

        }
        public List<ExportContainerItemsViewModel> GetExportContainerItems(long vesselid, long voyageId, long exportContainerId)
        {

            var data = (from loadingProgram in Db.LoadingPrograms
                        join enteringCargo in Db.EnteringCargos on loadingProgram.GDNumber equals enteringCargo.GDNumber
                        join opias in Db.OPIAs on loadingProgram.GDNumber equals opias.GDNumber
                        join ocrl in Db.OCRLs on loadingProgram.GDNumber.Trim().ToUpper() equals ocrl.GDNumber.Trim().ToUpper()
                        join ogde in Db.OGDEs on loadingProgram.GDNumber.Trim().ToUpper() equals ogde.GDNumber.Trim().ToUpper()
                        where loadingProgram.VesselExportId == vesselid && loadingProgram.VoyageExportId == voyageId
                        && ocrl.Status == "AL"
                       && !Db.ExportContainerItems.Any(s => s.GDNumber.Trim().ToUpper() == enteringCargo.GDNumber.Trim().ToUpper() && s.ExportContainerId == exportContainerId
                  && !Db.OCRLs.Any(ss => s.GDNumber == enteringCargo.GDNumber && ss.Status == "OD")

                        // || ((from x in Db.ExportContainerItems where x.GDNumber == enteringCargo.GDNumber select x.NumberOfPackages).Sum() == enteringCargo.NumberOfPackages) 
                        )
                        //&& !  exportContainerItems.Any(  s => s.GDNumber.Trim().ToUpper() == enteringCargo.GDNumber.Trim().ToUpper() 
                        //   && s.NumberOfPackages == enteringCargo.NumberOfPackages ) 

                        orderby ocrl.GDNumber descending
                        select new ExportContainerItemsViewModel
                        {
                            CargoReceivingInformationId = enteringCargo.EnteringCargoId,
                            AllowLoading = ocrl.Status,
                            Destination = loadingProgram.Destination,
                            ShipperName = opias.ConsignorName,
                            VesselExportId = loadingProgram.VesselExportId,
                            VoyageExportId = loadingProgram.VoyageExportId,
                            MessageTo = ogde.MessageTo,
                            GDNumber = enteringCargo.GDNumber,
                            ANFDate = enteringCargo.ANFDate != null ? enteringCargo.ANFDate : null,
                            PaperReadyDate = enteringCargo.PaperReadyDate != null ? enteringCargo.PaperReadyDate : null,
                            NoOfPackages = Convert.ToInt32(opias.NoOfPackages) - Db.ExportContainerItems.Where(s => s.GDNumber == enteringCargo.GDNumber).Select(l => l.NumberOfPackages).DefaultIfEmpty(0).Sum(),
                            OrderNumber = null,
                            Ischecked = false
                        }).GroupBy(x => x.GDNumber).Select(x => x.LastOrDefault()).ToList();
            //}).Distinct().ToList();

            data.RemoveAll(x => x.NoOfPackages <= 0);


            var dataSec = (from loadingProgram in Db.LoadingPrograms
                           join enteringCargo in Db.EnteringCargos on loadingProgram.GDNumber equals enteringCargo.GDNumber
                           join opias in Db.OPIAs on loadingProgram.GDNumber equals opias.GDNumber
                           join ocrl in Db.OCRLs on loadingProgram.GDNumber.Trim().ToUpper() equals ocrl.GDNumber.Trim().ToUpper()
                           join ogde in Db.OGDEs on loadingProgram.GDNumber.Trim().ToUpper() equals ogde.GDNumber.Trim().ToUpper()
                           join exportContainerItem in Db.ExportContainerItems on enteringCargo.GDNumber.Trim().ToUpper() equals exportContainerItem.GDNumber.Trim().ToUpper()
                           where loadingProgram.VesselExportId == vesselid && loadingProgram.VoyageExportId == voyageId
                              && exportContainerItem.ExportContainerId == exportContainerId && ocrl.Status == "AL"
                              &&
                              (exportContainerItem.IsSubmitted == false)
                              && !Db.OCRLs.Any(s => s.GDNumber == enteringCargo.GDNumber && s.Status == "OD")
                           orderby ocrl.GDNumber descending
                           select new ExportContainerItemsViewModel
                           {
                               CargoReceivingInformationId = enteringCargo.EnteringCargoId,
                               AllowLoading = ocrl.Status,
                               Destination = loadingProgram.Destination,
                               ShipperName = opias.ConsignorName,
                               VesselExportId = loadingProgram.VesselExportId,
                               VoyageExportId = loadingProgram.VoyageExportId,
                               MessageTo = ogde.MessageTo,
                               GDNumber = enteringCargo.GDNumber,
                               ANFDate = enteringCargo.ANFDate != null ? enteringCargo.ANFDate : null,
                               PaperReadyDate = enteringCargo.PaperReadyDate != null ? enteringCargo.PaperReadyDate : null,
                               // NoOfPackages = enteringCargo.NumberOfPackages,
                               NoOfPackages = Db.ExportContainerItems.Where(s => s.GDNumber == enteringCargo.GDNumber && s.ExportContainerId == exportContainerId).Select(l => l.NumberOfPackages).DefaultIfEmpty(0).Sum(),
                               OrderNumber = exportContainerItem.OrderNumber != null ? exportContainerItem.OrderNumber : null,
                               Ischecked = true
                           }).GroupBy(x => x.GDNumber).Select(x => x.LastOrDefault()).ToList();
            //   }).Distinct().ToList();
            dataSec.AddRange(data);


            //foreach (var item in dataSec)
            //{
            //    var res = Db.InvoiceExports.FromSql($"select * from InvoiceExport  where enteringcargoid = {item.CargoReceivingInformationId} and vesselexportid = {item.VesselExportId}  and voyageexportid =  {item.VoyageExportId} and isdeleted = 0").LastOrDefault();

            //    if (res != null)
            //    {
            //        item.InvoiceNumber = res.InvoiceNo;

            //    }

            //    var resinvoices = Db.InvoiceExports.FromSql($"select * from InvoiceExport  where enteringcargoid = {item.CargoReceivingInformationId}  and isdeleted = 0").ToList();

            //    if (resinvoices.Count() > 0)
            //    {
            //        foreach (var itemres in resinvoices)
            //        {
            //            if (itemres.IsAmountReceived == false)
            //            {
            //                item.IsAmountReceived = "";
            //                break;
            //            }
            //            else
            //            {
            //                item.IsAmountReceived = "Amount Receieved";
            //            }
            //        }
            //    }


            //}



            return dataSec;
        }

        public int? NumberOfPackages(string gdnumber)
        { 
            //var data = (from exportContainerItem in Db.ExportContainerItems
            //            where gdnumber
            //            select new ExportContainerItem
            //            {
            //                NumberOfPackages = (from x in Db.ExportContainerItems where x.GDNumber == exportContainerItem.GDNumber select x.NumberOfPackages).Sum(),
            //            }).ToList();


            //if (data != null)
            //{
            //    return data;
            //}

           

            return 0;

        }

        public List<ExportContainerItemsViewModel> GetExportContainerItemsBYContainerNumber(string containerNumber)
        {
            var data = (from exportContainer in Db.ExportContainers
                        join exportContainerItems in Db.ExportContainerItems on exportContainer.ExportContainerId equals exportContainerItems.ExportContainerId
                        join shippingLine in Db.ShippingLineExports on exportContainer.ShippingLineExportId equals shippingLine.ShippingLineExportId
                        where exportContainer.ContainerNumber == containerNumber && exportContainerItems.IsSubmitted != true
                        select new ExportContainerItemsViewModel
                        {
                            ExportContainerItemId = exportContainerItems.ExportContainerItemId,
                            ShippingLineId = shippingLine.ShippingLineExportId,
                            GDNumber = exportContainerItems.GDNumber,
                            NoOfPackages = exportContainerItems.NumberOfPackages

                        }).ToList();

            return data;
        }


        public long ExportContainersCount()
        {
            var data = (from exportContainer in Db.ExportContainers
                        select exportContainer).ToList();

            if (data == null)
            {
                return 0;
            }

            return data.Count;
        }
        public long ContainerReadyForSuffing()
        {
            var data = (from ocrl in Db.OCRLs
                        where 
                        !Db.ExportContainerItems.Any(s => s.GDNumber.Trim().ToUpper() == ocrl.GDNumber.Trim().ToUpper())
                        && ocrl.Status == "AL"

                        select ocrl).Distinct().ToList();

            if (data == null)
            {
                return 0;
            }

            return data.Count;
        }

        public ExportContainer GetExportContainerById(long exportcontainerid)
        {
            var res = Db.ExportContainers.FromSql($"select * from ExportContainers where ExportContainerId = {exportcontainerid}  and IsDeleted = 0").LastOrDefault();

            if (res != null)
            {
                return res;
            }
            else
            {
                return null;
            }


        }

        public List<ExportContainerItem> GetExportContainerItemsByExportContainerId(long exportcontainerid)
        {
            var asd = Db.ExportContainerItems.FromSql($"select * from ExportContainerItem where ExportContainerId = {exportcontainerid}  and IsDeleted = 0").ToList();

            return asd;
        }

        public ExportContainerItem GetExportContainerItemsByExportContainerIdandGD(long exportcontainerid, string gdnumber)
        {
            var asd = Db.ExportContainerItems.FromSql($"select * from ExportContainerItem where ExportContainerId = {exportcontainerid} and GDNumber = {gdnumber} and IsDeleted = 0").LastOrDefault();

            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public List<PendingGdsInvoicesViewModel> GetPendingGdsInvoicesViewModel()
        {


            var res = (from gateInExport in Db.GateInExports
                       join loadingProgram in Db.LoadingPrograms on gateInExport.GDNumber equals loadingProgram.GDNumber
                       join enteringCargo in Db.EnteringCargos on loadingProgram.GDNumber equals enteringCargo.GDNumber
                       from vesselExport in Db.VesselExports.Where(c => c.VesselExportId == loadingProgram.VesselExportId).DefaultIfEmpty()
                       from voyageExport in Db.VoyageExports.Where(c => c.VoyageExportId == loadingProgram.VoyageExportId).DefaultIfEmpty()
                       from shippingAgentExport in Db.ShippingAgentExports.Where(c => c.ShippingAgentExportId == loadingProgram.ShippingAgentExportId).DefaultIfEmpty()
                       from opias in Db.OPIAs.Where(c => c.GDNumber == loadingProgram.GDNumber).DefaultIfEmpty()
                       where
                       gateInExport.GateInStatus == "F"
                       && !Db.InvoiceExports.Any(s => s.EnteringCargoId == enteringCargo.EnteringCargoId && s.AtTimeOfInvoiceVesselExportId == loadingProgram.VesselExportId && s.AtTimeOfInvoiceVoyageExportId == loadingProgram.VoyageExportId)
                       && !Db.ExportContainerItems.Any(s => s.GDNumber == enteringCargo.GDNumber && s.VesselExportId == loadingProgram.VesselExportId && s.VoyageExportId == loadingProgram.VoyageExportId)
                       select new PendingGdsInvoicesViewModel
                       {
                           GDNumber = loadingProgram.GDNumber,
                           GateInDate = gateInExport.GateInDate,
                           ETD = voyageExport.ETD,
                           Vessel = vesselExport.VesselName,
                           Voyage = voyageExport.VoyageNumber,
                           ShippingAgent = shippingAgentExport.ShippingAgentName,
                           ClearingAgent = opias.ClearingAgentName,
                           Shipper = opias.ConsignorName,
                           CBM = Convert.ToDouble((from lpdetail in Db.LoadingProgramDetails where lpdetail.LoadingProgramId == loadingProgram.LoadingProgramId select lpdetail).Sum(s => s.CBM)),
                           Weight = (from lpdetail in Db.LoadingProgramDetails where lpdetail.LoadingProgramId == loadingProgram.LoadingProgramId select lpdetail).Sum(s => s.Weight) ?? 0,
                       }).ToList();


            return res;
        }

        public List<UnSettledInvoicesViewModel> GetUnSettledInvoicesViewModel()
        {


            var res = (from invoiceExport in Db.InvoiceExports
                       join enteringCargo in Db.EnteringCargos on invoiceExport.EnteringCargoId equals enteringCargo.EnteringCargoId
                       join loadingProgram in Db.LoadingPrograms on enteringCargo.GDNumber equals loadingProgram.GDNumber
                       from vesselExport in Db.VesselExports.Where(c => c.VesselExportId == loadingProgram.VesselExportId).DefaultIfEmpty()
                       from voyageExport in Db.VoyageExports.Where(c => c.VoyageExportId == loadingProgram.VoyageExportId).DefaultIfEmpty()
                       from shippingAgentExport in Db.ShippingAgentExports.Where(c => c.ShippingAgentExportId == loadingProgram.ShippingAgentExportId).DefaultIfEmpty()
                       from opias in Db.OPIAs.Where(c => c.GDNumber == loadingProgram.GDNumber).DefaultIfEmpty()
                       where
                          !Db.AmountReceivedExports.Any(s => s.InvoiceExportId == invoiceExport.InvoiceExportId)
                       select new UnSettledInvoicesViewModel
                       {
                           ShippingAgent = shippingAgentExport.ShippingAgentName,
                           ClearingAgent = opias.ClearingAgentName,
                           Shipper = opias.ConsignorName,
                           GdNumber = loadingProgram.GDNumber,
                           InvoiceNumber = invoiceExport.InvoiceNo,
                           InvoiceDate = invoiceExport.InvoiceDate,
                           AmountExTax = invoiceExport.BalanceAmountTotal,
                           SalesTax = Math.Round(invoiceExport.AfterSalesTax),
                           AmountIncTax = invoiceExport.GrandTotal,
                           DwellDays = Convert.ToInt64((DateTime.Now - invoiceExport.GateInDate).Value.TotalDays),
                           ContactNumber = invoiceExport.PhoneNumber,
                       }).ToList();


            if (res.Count() > 0)
            {
                res.OrderBy(x => x.DwellDays);

            }


            return res;
        }

        public List<UnSettledInvoicesViewModel> GetGDsAccoicatedbutAmountNotreceivedViewModel()
        {


            var res = (from invoiceExport in Db.InvoiceExports
                       join enteringCargo in Db.EnteringCargos on invoiceExport.EnteringCargoId equals enteringCargo.EnteringCargoId
                       join loadingProgram in Db.LoadingPrograms on enteringCargo.GDNumber equals loadingProgram.GDNumber
                       join export in Db.ExportContainerItems on enteringCargo.GDNumber equals export.GDNumber
                       from vesselExport in Db.VesselExports.Where(c => c.VesselExportId == loadingProgram.VesselExportId).DefaultIfEmpty()
                       from voyageExport in Db.VoyageExports.Where(c => c.VoyageExportId == loadingProgram.VoyageExportId).DefaultIfEmpty()
                       from shippingAgentExport in Db.ShippingAgentExports.Where(c => c.ShippingAgentExportId == loadingProgram.ShippingAgentExportId).DefaultIfEmpty()
                       from opias in Db.OPIAs.Where(c => c.GDNumber == loadingProgram.GDNumber).DefaultIfEmpty()
                       where
                          !Db.AmountReceivedExports.Any(s => s.InvoiceExportId == invoiceExport.InvoiceExportId)
                       select new UnSettledInvoicesViewModel
                       {
                           ShippingAgent = shippingAgentExport.ShippingAgentName,
                           ClearingAgent = opias.ClearingAgentName,
                           Shipper = opias.ConsignorName,
                           GdNumber = loadingProgram.GDNumber,
                           InvoiceNumber = invoiceExport.InvoiceNo,
                           InvoiceDate = invoiceExport.InvoiceDate,
                           AmountExTax = invoiceExport.BalanceAmountTotal,
                           SalesTax = invoiceExport.AfterSalesTax,
                           AmountIncTax = invoiceExport.GrandTotal,
                           //ApprovedBy = export.isAmountReceived,
                       }).ToList();





            return res;
        }

        public List<ExportAlongSideDataViewModel> GetExportAlongSideData()
        {


            var res = (from loadingProgram in Db.LoadingPrograms
                       join enteringCargo in Db.EnteringCargos on loadingProgram.GDNumber equals enteringCargo.GDNumber
                       from vesselExport in Db.VesselExports.Where(c => c.VesselExportId == loadingProgram.VesselExportId).DefaultIfEmpty()
                       from voyageExport in Db.VoyageExports.Where(c => c.VoyageExportId == loadingProgram.VoyageExportId).DefaultIfEmpty()
                       from shippingAgentExport in Db.ShippingAgentExports.Where(c => c.ShippingAgentExportId == loadingProgram.ShippingAgentExportId).DefaultIfEmpty()
                       from opias in Db.OPIAs.Where(c => c.GDNumber == loadingProgram.GDNumber).DefaultIfEmpty()
                       from gdhold in Db.GDHolds.Where(c => c.EnteringCargoId == enteringCargo.EnteringCargoId).DefaultIfEmpty()

                       where

                          !Db.ExportContainerItems.Any(s => s.GDNumber == loadingProgram.GDNumber)
                          &&
                          !Db.OCRLs.Any(s => s.GDNumber == loadingProgram.GDNumber && s.Status == "OD")
                       select new ExportAlongSideDataViewModel
                       {
                           ShippingAgent = shippingAgentExport.ShippingAgentName,
                           VesselVoyage = vesselExport.VesselName + " - " + voyageExport.VoyageNumber,
                           GDNumber = loadingProgram.GDNumber,
                           CargoReceivedDate = loadingProgram.RecevingStartTime,
                           Shipper = opias.ConsignorName,
                           ClearingAgent = opias.ClearingAgentName,
                           PortOfDischarge = loadingProgram.Destination,
                           NumberofPackge = Convert.ToInt64(opias.NoOfPackages),
                           PackgeType = opias.PackageType,
                           CBM = Convert.ToDouble((from lpdetail in Db.LoadingProgramDetails where lpdetail.LoadingProgramId == loadingProgram.LoadingProgramId select lpdetail).Sum(s => s.CBM)),
                           Weight = (from lpdetail in Db.LoadingProgramDetails where lpdetail.LoadingProgramId == loadingProgram.LoadingProgramId select lpdetail).Sum(s => s.Weight) ?? 0,
                           PaperReady = enteringCargo.PaperReadyDate,
                           Anf = enteringCargo.ANFDate,
                           Remarks = gdhold != null ? gdhold.Remarks : ""

                       }).ToList();


            if (res.Count() > 0)
            {
                foreach (var item in res)
                {
                    var resdata = Db.OCRLs.FromSql($"select * from ocrls where GDNumber = {item.GDNumber}  and IsDeleted = 0").LastOrDefault();

                    if (resdata != null)
                    {
                        item.AllowLoading = resdata.Status;
                    }

                    var CustomerNOC = Db.CustomerNOCs.FromSql($"select * from CustomerNOC where GDNumber = {item.GDNumber}  and IsDeleted = 0").LastOrDefault();

                    if (CustomerNOC != null)
                    {
                        var agent = Db.ShippingAgentExports.FromSql($"select * from ShippingAgentExport where ShippingAgentExportId = {CustomerNOC.ShippingAgentExportId}  and IsDeleted = 0").LastOrDefault();

                        if (agent != null)
                        {
                            item.NOC = agent.ShippingAgentName;
                        }
                    }





                }
            }





            return res;
        }

        public List<ExportContainerItem> GetExportContainerItemsByGdnumber(string GdNumebr)
        {
            var asd = Db.ExportContainerItems.FromSql($"select * from ExportContainerItem where GDNumber = {GdNumebr}  and IsDeleted = 0 and Ischecked = 0 ").ToList();

            return asd;
        }


        public List<ExportContainerItem> GetExportContainerItemsByContainerNumber(string ContainerNumber)
        {
            var asd = Db.ExportContainerItems.FromSql($"select * from ExportContainerItem where ContainerNumber = {ContainerNumber}  and IsDeleted = 0 and Ischecked = 0 ").ToList();

            return asd;
        }


        public string UpdateIscheckStatus(List<ExportContainerItem> model )
        {
            try
            {  
                Db.UpdateRange(model);
                Db.SaveChanges();

                return "true";
            }
            catch (Exception e)
            {

                return e.InnerException.InnerException.Message;
            }
            

        }

        public List<ExportContainerItem> GetExportContainerItemsByexportcontainerid(long exportcontainerid)
        {
            var asd = Db.ExportContainerItems.FromSql($"select * from ExportContainerItem where ExportContainerId = {exportcontainerid}  and IsDeleted = 0 and Ischecked = 0").ToList();

            return asd;
        }

        public List<ExportContainerItem> GetExportContainerItemsByGdnumberFOrAssociation(string GdNumebr)
        {
            var asd = Db.ExportContainerItems.FromSql($"select * from ExportContainerItem where GDNumber = {GdNumebr}  and IsDeleted = 0 and Ischecked = 1 and  IsSubmitted = 0 ").ToList();


            return asd;
        }
          public List<ExportContainerItem> GetExportContainerItemsByExportcontainerIdFOrAssociation(long exportcontainerid)
        {
            var asd = Db.ExportContainerItems.FromSql($"select * from ExportContainerItem where ExportContainerId = {exportcontainerid}  and IsDeleted = 0 and Ischecked = 1 and  IsSubmitted = 0 ").ToList();
             
            return asd;
        }

        public ExportContainer GetExportContainerByID(long exportcontainerid)
        {
            var asd = Db.ExportContainers.FromSql($"select * from ExportContainers where ExportContainerId = {exportcontainerid}  and IsDeleted = 0 and IsGateout = 0 ").LastOrDefault();

            return asd;
        }

        public List<ExportContainerItem> AlongsideGds()
        {

            var data = (from loadingProgram in Db.LoadingPrograms
                        join enteringCargo in Db.EnteringCargos on loadingProgram.GDNumber equals enteringCargo.GDNumber
                        join opias in Db.OPIAs on loadingProgram.GDNumber equals opias.GDNumber
                        join ocrl in Db.OCRLs on loadingProgram.GDNumber.Trim().ToUpper() equals ocrl.GDNumber.Trim().ToUpper()
                        join ogies in Db.OGIEs on loadingProgram.GDNumber.Trim().ToUpper() equals ogies.GDNumber.Trim().ToUpper()
                        join ogde in Db.OGDEs on loadingProgram.GDNumber.Trim().ToUpper() equals ogde.GDNumber.Trim().ToUpper()
                        where  
                        ocrl.Status == "AL"
                        && ogies.GateInStatus == "F"
                        && !Db.ExportContainerItems.Any(s => s.GDNumber.Trim().ToUpper() == enteringCargo.GDNumber.Trim().ToUpper())
                        && !Db.OCRLs.Any(ss => ss.GDNumber == enteringCargo.GDNumber && ss.Status == "OD")
                         
                        orderby ocrl.GDNumber descending
                        select new ExportContainerItem { 

                            GDNumber = opias.GDNumber,
                            NumberOfPackages =  Convert.ToInt32( opias.NoOfPackages ),
                            AllowLoading = ocrl.Status,
                            Destination = loadingProgram.Destination,
                            ShipperName = opias.ConsignorName,
                            Ischecked = false,
                             
                        }).GroupBy(x => x.GDNumber).Select(x => x.LastOrDefault()).ToList();
            
            return data;
        }

        public List<GateOutViewModel> GetGateOutExport( )
        {
            var containers = (from  exportContainerItem in Db.ExportContainerItems 
                              join osvm in Db.OSVMs on exportContainerItem.ContainerNumber equals osvm.ContainerNo
                              join boundedTranspoter in Db.BoundedTranspoters on osvm.BondedCarrierName equals boundedTranspoter.BoundedCarrierName
                              where exportContainerItem.IsGateOut == false
                              && osvm.GDNumber == exportContainerItem.GDNumber
                              && exportContainerItem.IsSubmitted == true
                              && exportContainerItem.Ischecked == true
                              orderby exportContainerItem.OrderNumber ascending
                              select new GateOutViewModel
                              {
                                  Key = exportContainerItem.GDNumber+"-"+ exportContainerItem.ContainerNumber,
                                  GDNumber = exportContainerItem.GDNumber,
                                  VehicleNumber = osvm.VehicleNo,
                                  PCCSSSeal = osvm.PCCSSSealNumber,
                                  BondedCarrierName = osvm.BondedCarrierName,
                                  ExportContainerId = exportContainerItem.ExportContainerId ?? 0,
                                  ContainerNumber = exportContainerItem.ContainerNumber,
                                  BondedCarrierNTN = boundedTranspoter.BoundedNTN,
                                  Status = "F",
                                  CountryofExit = "PAKISTAN",
                                  LineSeal = "",
                                  TransporterId = null,
                                  ContainerGrossWeight = 0,
                                  IsChecked = false
                              }).ToList();

            foreach (var item in containers)
            {
                var dataogde = Db.OGDEs.FromSql($" select * from ogdes where GDNumber = {item.GDNumber}  and IsDeleted = 0").LastOrDefault();

                if (dataogde != null)
                {
                    item.Location = dataogde.MessageTo;
                }
            }

            return containers;

        }

    }
}
