using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;





namespace WebdocTerminal.Repos
{
    public class InvoiceRepository : RepoBase<Invoice>, IInvoiceRepository
    {
        private static IConfiguration _configuration;
        public string Con { get; set; }
        public InvoiceRepository(IUserResolveService userResolveService, IConfiguration configuration) : base(userResolveService)
        {
            _configuration = configuration;
            Con =  _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        }


        public long GetSealAmount(long CyContainerId)
        {
            long res = 0;
            var totalpartcontainers = 0;


            //new SealIssue work start

            var rescontainer = Db.CYContainers.FromSql($"select  * from  CYContainer where CYContainerId = {CyContainerId} and  IsDeleted = 0 ").LastOrDefault();

            if (rescontainer != null)
            {
                var containerlist = Db.CYContainers.FromSql($"select  * from  CYContainer where VIRNo = {rescontainer.VIRNo} and IndexNo = {rescontainer.IndexNo}  and  IsDeleted = 0 ").ToList();
                if (containerlist.Count() > 0)
                {
                    foreach (var item in containerlist)
                    {
                        long resamt = 0;
                        long rescountpart = 0;

                        if (item.IsPartialContainer == true)
                        {
                            var reslist = Db.CYContainers.FromSql($"select  * from  CYContainer where VIRNo = {item.VIRNo} and ContainerNo = {item.ContainerNo}  and  IsDeleted = 0 ").ToList();
                            foreach (var itemres in reslist)
                            {
                                rescountpart += 1;
                                var sealamount = Db.SealIssues.FromSql($"select  * from  SealIssue where CYContainerId = {itemres.CYContainerId}  and  IsDeleted = 0 ").ToList().Sum(x => x.Rate);
                                if (sealamount > 0)
                                {
                                    resamt += sealamount;
                                }

                            }
                            if (resamt > 0)
                            {
                                res += resamt / rescountpart;
                            }

                        }
                        else
                        {
                            var sealamount = Db.SealIssues.FromSql($"select  * from  SealIssue where CYContainerId = {item.CYContainerId}  and  IsDeleted = 0 ").ToList().Sum(x => x.Rate);

                            if (sealamount > 0)
                            {
                                res += sealamount;
                            }
                        }

                    }
                }
            }


            return res;
        }

        public double GetWaiverTotal(long ContainerIndexId)
        {

            var wavr = (from waiver in Db.Waivers
                        where waiver.ContainerIndexId == ContainerIndexId && waiver.IsWaive == true
                        && waiver.InvoiceCreated == false
                        select waiver.WaiverNumber).ToList().Distinct();


            if (wavr.Count() > 0)
            {
                var waivrno = wavr.LastOrDefault();

                var waivr = (from waiver in Db.Waivers
                             where waiver.WaiverNumber == waivrno
                             select waiver).ToList().Distinct();

                if (waivr.Count() > 0)
                {

                    double waiverdiscountamount = 0;

                    foreach (var item in waivr)
                    {
                        waiverdiscountamount += item.Discount;

                    }

                    return Math.Round(waiverdiscountamount, 2);

                }
                else
                {
                    return 0;
                }



            }

            else
            {
                return 0;
            }

            return 0;
        }

        public double GetCYWaiverTotal(long CYContainerId)
        {

            var wavr = (from waiver in Db.Waivers
                        where waiver.CYContainerId == CYContainerId && waiver.IsWaive == true
                        && waiver.InvoiceCreated == false
                        select waiver.WaiverNumber).ToList().Distinct();


            if (wavr.Count() > 0)
            {
                var waivrno = wavr.LastOrDefault();

                var waivr = (from waiver in Db.Waivers
                             where waiver.WaiverNumber == waivrno
                             select waiver).ToList().Distinct();

                if (waivr.Count() > 0)
                {

                    double waiverdiscountamount = 0;

                    foreach (var item in waivr)
                    {
                        waiverdiscountamount += item.Discount;

                    }

                    return Math.Round(waiverdiscountamount, 2);

                }
                else
                {
                    return 0;
                }



            }

            else
            {
                return 0;
            }

            return 0;
        }


        public string GetWaiverLastRemarksCFS(long ContainerIndexId)
        {

            var asd = Db.Waivers.FromSql($"select * from Waiver  where  ContainerIndexId = {ContainerIndexId} and IsDeleted = 0").LastOrDefault();

            if (asd != null)
            {
                return asd.Remarks;
            }
            return "";

        }


        public string GetWaiverLastRemarksCY(long CYContainerId)
        {

            var asd = Db.Waivers.FromSql($"select * from Waiver  where  CYContainerId = {CYContainerId} and IsDeleted = 0").LastOrDefault();

            if (asd != null)
            {
                return asd.Remarks;
            }
            return "";

        }


        public double GetPreviousWaiverTotalCFS(long ContainerIndexId)
        {


            var wavr = Db.Waivers.FromSql($"select * from Waiver  where  ContainerIndexId = {ContainerIndexId} and  IsWaive = 1 and InvoiceCreated = 1 and IsDeleted = 0").ToList();


            if (wavr.Count() > 0)
            {
                var res = wavr.Sum(x => x.Discount);
                return Math.Round(res, 2);
            }

            else
            {
                return 0;
            }

        }

        public List<Waiver> GetPreviousWaiverCFS(long ContainerIndexId)
        {
            var wavr = Db.Waivers.FromSql($"select * from Waiver  where  ContainerIndexId = {ContainerIndexId} and  IsWaive = 1 and InvoiceCreated = 1 and IsDeleted = 0").ToList();

            return wavr;

        }

        public double GetPreviousWaiverTotalCY(long CYContainerId)
        {


            var wavr = Db.Waivers.FromSql($"select * from Waiver  where  CYContainerId = {CYContainerId} and  IsWaive = 1 and InvoiceCreated = 1  and IsDeleted = 0").ToList();


            if (wavr.Count() > 0)
            {
                var res = wavr.Sum(x => x.Discount);

                return Math.Round(res, 2);
            }

            else
            {
                return 0;
            }

        }


        public List<Waiver> GetPreviousWaiverCY(long CYContainerId)
        {
            var wavr = Db.Waivers.FromSql($"select * from Waiver  where  CYContainerId = {CYContainerId} and  IsWaive = 1 and InvoiceCreated = 1 and IsDeleted = 0").ToList();

            return wavr;

        }

        public double GetAdditionalChargesTotal(long ContainerIndexId)
        {

            var containerIndex = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where  ContainerIndexId = {ContainerIndexId}   and IsDeleted = 0   ").LastOrDefault();

            if (containerIndex != null && containerIndex.ShippingAgentId != null && containerIndex.CargoType != null)
            {
                var shippingagnet = Db.ShippingAgents.FromSql($"select * from ShippingAgent   where  ShippingAgentId = {containerIndex.ShippingAgentId}   and IsDeleted = 0   ").LastOrDefault();

                if (shippingagnet != null)
                {

                    if (containerIndex.CargoType == "FCL" && shippingagnet.AllowSpecialChargeFCL == "Yes")
                    {
                        var chargeAssigning = Db.ChargeAssignings.FromSql($"select * from OtherChargeAssigning   where  ContainerIndexId = {ContainerIndexId}   and IsDeleted = 0   ").ToList().Distinct();

                        if (chargeAssigning.Count() > 0)
                        {
                            double chargeAssigningcountamount = 0;
                            chargeAssigningcountamount = chargeAssigning.Sum(x => x.ChargeAmount);

                            //double chargeAssigningcountamount = 0;
                            //foreach (var item in chargeAssigning)
                            //{
                            //    chargeAssigningcountamount += item.ChargeAmount;
                            //}

                            return chargeAssigningcountamount;



                        }

                        else
                        {
                            return 0;
                        }

                    }
                    if (containerIndex.CargoType == "LCL" && shippingagnet.AllowSpecialChargeLCL == "Yes")
                    {
                        var chargeAssigning = Db.ChargeAssignings.FromSql($"select * from OtherChargeAssigning   where  ContainerIndexId = {ContainerIndexId}   and IsDeleted = 0   ").ToList().Distinct();

                        if (chargeAssigning.Count() > 0)
                        {
                            double chargeAssigningcountamount = 0;
                            chargeAssigningcountamount = chargeAssigning.Sum(x => x.ChargeAmount);

                            //double chargeAssigningcountamount = 0;
                            //foreach (var item in chargeAssigning)
                            //{
                            //    chargeAssigningcountamount += item.ChargeAmount;
                            //}

                            return chargeAssigningcountamount;



                        }

                        else
                        {
                            return 0;
                        }
                    }

                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }

            //var chargeAssigning = Db.ChargeAssignings.FromSql($"select * from OtherChargeAssigning   where  ContainerIndexId = {ContainerIndexId} and   InvoiceCreated =  0  and IsDeleted = 0   ").ToList().Distinct();


            //var chargeAssigning = (from chargeAssignings in Db.ChargeAssignings
            //                       where chargeAssignings.ContainerIndexId == ContainerIndexId && chargeAssignings.InvoiceCreated == false
            //                       select chargeAssignings).ToList().Distinct();




            return 0;
        }

        public double GetInvoiceTotal(long ContainerIndexId)
        {

            //var inv = Db.Invoices.FromSql($"select * from invoice   where   Invoice.IsDeleted = 0  and Invoice.ContainerIndexId = {ContainerIndexId} and  invoice.IsCancelled =  0 and      (invoice.BillType = 'Normal' or invoice.BillType = 'Bill To Line') ").Select(x=> x.GrandTotal).ToList();


            //var inv = (from invoice in Db.Invoices
            //           where invoice.ContainerIndexId == ContainerIndexId && invoice.IsCancelled == false
            //           && 
            //           (invoice.BillType == "Normal" || invoice.BillType == "Bill To Line")
            //           select invoice.TotalCharges).ToList();

            var inv = Db.Invoices.FromSql($"select * from invoice   where   Invoice.IsDeleted = 0  and Invoice.ContainerIndexId = {ContainerIndexId} and  invoice.IsCancelled =  0 and      invoice.BillType in ( 'Normal' , 'Auction') ").Select(x => x.GrandTotal).ToList();

            if (inv.Count() > 0)
            {

                double perviouseAmount = 0;

                foreach (var item in inv)
                {
                    perviouseAmount += item;

                }

                return Math.Ceiling(perviouseAmount);
                //return   perviouseAmount;

            }

            else
            {
                return 0;
            }

            return 0;
        }

        public double GetInvoiceTotalCharges(long Containerd, string type)
        {
            double resamoutnt = 0;

            if (type == "CFS")
            {
                var invoicecfs = Db.Invoices.FromSql($"select * from invoice   where   Invoice.IsDeleted = 0  and Invoice.ContainerIndexId = {Containerd} and  invoice.IsCancelled =  0 and      invoice.BillType in ( 'Normal' , 'Auction') ").ToList().Sum(x => x.TotalCharges);
                resamoutnt = invoicecfs;
            }

            if (type == "CY")
            {
                var invoicecy = Db.Invoices.FromSql($"select * from invoice   where   Invoice.IsDeleted = 0  and Invoice.CYContainerId = {Containerd} and  invoice.IsCancelled =  0 and      invoice.BillType in ( 'Normal' , 'Auction')  ").ToList().Sum(x => x.TotalCharges);
                resamoutnt = invoicecy;
            }

            return resamoutnt;
        }

        public double GetPreviousPaidGST(long Containerd, string type)
        {
            double resamoutnt = 0;

            if (type == "CFS")
            {
                var invoicecfs = Db.Invoices.FromSql($"select * from invoice   where   Invoice.IsDeleted = 0  and Invoice.ContainerIndexId = {Containerd} and  invoice.IsCancelled =  0 and      invoice.BillType in ( 'Normal' , 'Auction') ").ToList().Sum(x => x.AfterSalesTax);
                resamoutnt = invoicecfs;
            }

            if (type == "CY")
            {
                var invoicecy = Db.Invoices.FromSql($"select * from invoice   where   Invoice.IsDeleted = 0  and Invoice.CYContainerId = {Containerd} and  invoice.IsCancelled =  0 and      invoice.BillType in ( 'Normal' , 'Auction')  ").ToList().Sum(x => x.AfterSalesTax);
                resamoutnt = invoicecy;
            }

            return resamoutnt;
        }

        public double GetPreviousPaidBillToLineAmount(long Containerd, string type)
        {
            double resamoutnt = 0;

            if (type == "CFS")
            {
                var invoicecfs = Db.Invoices.FromSql($"select * from invoice   where   Invoice.IsDeleted = 0  and Invoice.ContainerIndexId = {Containerd} and  invoice.IsCancelled =  0 and      invoice.BillType = 'Bill To Line' ").ToList().Sum(x => x.TotalCharges);
                resamoutnt = invoicecfs;
            }

            if (type == "CY")
            {
                var invoicecy = Db.Invoices.FromSql($"select * from invoice   where   Invoice.IsDeleted = 0  and Invoice.CYContainerId = {Containerd} and  invoice.IsCancelled =  0 and      invoice.BillType = 'Bill To Line'  ").ToList().Sum(x => x.TotalCharges);
                resamoutnt = invoicecy;
            }

            return resamoutnt;
        }
        public double GetPreviousPaidBillToLineAmountGST(long Containerd, string type)
        {
            double resamoutnt = 0;

            if (type == "CFS")
            {
                var invoicecfs = Db.Invoices.FromSql($"select * from invoice   where   Invoice.IsDeleted = 0  and Invoice.ContainerIndexId = {Containerd} and  invoice.IsCancelled =  0 and      invoice.BillType = 'Bill To Line' ").ToList().Sum(x => x.AfterSalesTax);
                resamoutnt = invoicecfs;
            }

            if (type == "CY")
            {
                var invoicecy = Db.Invoices.FromSql($"select * from invoice   where   Invoice.IsDeleted = 0  and Invoice.CYContainerId = {Containerd} and  invoice.IsCancelled =  0 and      invoice.BillType = 'Bill To Line'  ").ToList().Sum(x => x.AfterSalesTax);
                resamoutnt = invoicecy;
            }

            return resamoutnt;
        }
        public double GetPreviousPaidBillToLineAmountGrandTotal(long Containerd, string type)
        {
            double resamoutnt = 0;

            if (type == "CFS")
            {
                var invoicecfs = Db.Invoices.FromSql($"select * from invoice   where   Invoice.IsDeleted = 0  and Invoice.ContainerIndexId = {Containerd} and  invoice.IsCancelled =  0 and      invoice.BillType = 'Bill To Line' ").ToList().Sum(x => x.GrandTotal);
                resamoutnt = invoicecfs;
            }

            if (type == "CY")
            {
                var invoicecy = Db.Invoices.FromSql($"select * from invoice   where   Invoice.IsDeleted = 0  and Invoice.CYContainerId = {Containerd} and  invoice.IsCancelled =  0 and      invoice.BillType = 'Bill To Line'  ").ToList().Sum(x => x.GrandTotal);
                resamoutnt = invoicecy;
            }

            return resamoutnt;
        }




        public double GetInvoiceTotalCY(long CYContainerId)
        {

            //var inv = (from invoice in Db.Invoices
            //           where
            //           invoice.CYContainerId == CYContainerId && invoice.IsCancelled == false
            //           &&
            //          (invoice.BillType == "Normal" || invoice.BillType == "Bill To Line")
            //           select invoice.GrandTotal).ToList();

            var inv = Db.Invoices.FromSql($"select * from invoice   where   Invoice.IsDeleted = 0  and Invoice.CYContainerId = {CYContainerId} and  invoice.IsCancelled =  0 and      invoice.BillType in ( 'Normal' , 'Auction')  ").Select(x => x.GrandTotal).ToList();


            if (inv.Count() > 0)
            {

                double perviouseAmount = 0;

                foreach (var item in inv)
                {
                    perviouseAmount += item;

                }

                return Math.Ceiling(perviouseAmount);
                //return    perviouseAmount;
            }

            else
            {
                return 0;
            }

            return 0;
        }

        public double GetAdditionalChargesTotalCY(long CYContainerId)
        {


            var cycintainer = Db.CYContainers.FromSql($"select * from CYContainer   where  CYContainerId = {CYContainerId}   and IsDeleted = 0   ").LastOrDefault();


            if (cycintainer != null && cycintainer.ShippingAgentId != null)
            {

                var shippingagnet = Db.ShippingAgents.FromSql($"select * from ShippingAgent   where  ShippingAgentId = {cycintainer.ShippingAgentId}   and IsDeleted = 0   ").LastOrDefault();

                if (shippingagnet != null && shippingagnet.AllowSpecialChargeCY == "Yes")
                {
                    var data = Db.ChargeAssignings.FromSql($"select * from OtherChargeAssigning   where  CyContainerId = {CYContainerId}   and IsDeleted = 0   ").ToList().Distinct();
                    if (data.Count() > 0)
                    {

                        double AdditionalChargesamount = 0;

                        //foreach (var item in data)
                        //{
                        //    AdditionalChargesamount += item.ChargeAmount;
                        //}

                        AdditionalChargesamount = data.Sum(x => x.ChargeAmount);


                        return AdditionalChargesamount;


                    }

                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                return 0;
            }


            return 0;
        }

        public List<InvoiceItemViewModel> GetCBMTotal(long ContainerIndexId, long clearingAgentId, double Weight, double CBM, string indexcargotype, DateTime gateInDate, DateTime BillDate)
        {
            var totalItemsFixedRate = new List<InvoiceItemViewModel>();
            var totalItems = new List<InvoiceItemViewModel>();
            var datetime = DateTime.Now;

            var auctionamountinfo = (from auctionamt in Db.AuctionAmounts select auctionamt).LastOrDefault();

            if (indexcargotype == "FCL")
            {



                var dollerrate = (from rate in Db.ExchangeRates
                                  select rate.ExchangeRateAmount).FirstOrDefault();



                var container = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where ContainerIndexId = {ContainerIndexId} and IsDeleted = 0 ").LastOrDefault();


                var shippingagent = Db.ShippingAgents.FromSql($"SELECT * From ShippingAgent Where ShippingAgentId = {container.ShippingAgentId} and IsDeleted = 0 ").LastOrDefault();

                var calculateCBM = CBM;

                var calculateWeight = Weight;

                var weightOrCBM = WeightGreaterThenCBM(calculateWeight, calculateCBM);

                var weightConv = (Convert.ToDouble(calculateWeight) / 1000.0);


                var isocode = Db.ISOCodeHeads.FromSql($"select isoCodeHead.ISOCodeHeadId , isoCodeHead.ISOCodeHeadDescription  , isoCodeHead.DeleteDate , isoCodeHead.IsDeleted  from ISOCode   left join isoCodeHead  on ISOCode.ISOCodeHeadId = ISOCodeHead.ISOCodeHeadId  and isoCodeHead.IsDeleted = 0  Where     ISOCode.Code    = {container.ISOCode} and ISOCode.IsDeleted = 0 ").FirstOrDefault();

                if (isocode == null)
                {
                    return totalItems;

                }

                var tariffInfos = new List<InvoiceItemViewModel>();

                var res = GetTariffAllList(container.ShippingAgentId, isocode.ISOCodeHeadId, container.ConsigneId, clearingAgentId, container.ShipperId, container.GoodsHeadId, container.PortAndTerminalId, indexcargotype);

                if (container != null)
                {
                    var invoiceresdata = Db.Invoices.FromSql($"select * from Invoice Where ContainerIndexId = {container.ContainerIndexId} and  IsDeleted = 0 ").FirstOrDefault();

                    var resdata = new List<InvoiceItem>();

                    var totalItemsforviewmodel = new List<InvoiceItemViewModel>();

                    if (invoiceresdata != null)
                    {

                        var DeliveredCargogatepass = Db.DOItems.FromSql($"select DOItem.* from GatePass join DOItem  on GatePass.GatePassId = DOItem.GatePassId where GatePass.VirNumber ={container.VirNo} and  GatePass.IndexNo = {container.IndexNo} and  DOItem.Status = 'F' and DOItem.IsDeleted =0 and GatePass.IsDeleted = 0 ").ToList();

                        if (DeliveredCargogatepass.Count() > 0)
                        {
                            var invoiceItemsres = GetInvoiceItemBycontainerIndexId(container.ContainerIndexId).Where(x => x.Category != "SpecialHandlingCharges" && x.Category != "Storage").ToList();

                            var perviouswaiver = GetPreviousWaiverCFS(container.ContainerIndexId).Where(x => x.Category != "SpecialHandlingCharges" && x.Category != "Storage").ToList();

                            if (invoiceItemsres.Count() > 0)
                            {

                                foreach (var item in invoiceItemsres)
                                {
                                    var result = resdata.Find(t => t.Description == item.Description && t.Category == item.Category);

                                    if (result != null)
                                    {
                                        result.Amount += item.Amount;
                                    }
                                    else
                                    {
                                        resdata.Add(item);
                                    }
                                }

                                if (resdata.Count() > 0)
                                {
                                    var GeneralCBMRateWeightRate = (from resout in resdata
                                                                    select new InvoiceItemViewModel
                                                                    {
                                                                        Description = resout.Description,
                                                                        Amount = resout.Amount,
                                                                        Category = resout.Category
                                                                    }).Distinct().ToList();

                                    totalItemsforviewmodel.AddRange(GeneralCBMRateWeightRate);

                                }

                            }

                            if (perviouswaiver.Count() > 0)
                            {
                                var newobjlist = new List<Waiver>();

                                foreach (var item in perviouswaiver)
                                {
                                    var result = newobjlist.Find(t => t.Description == item.Description && t.Category == item.Category);

                                    if (result != null)
                                    {
                                        result.Discount += item.Discount;
                                    }
                                    else
                                    {
                                        newobjlist.Add(item);
                                    }
                                }

                                if (newobjlist.Count() > 0)
                                {
                                    var waivertariffRates = (from resout in newobjlist
                                                             select new InvoiceItemViewModel
                                                             {
                                                                 Description = resout.Description,
                                                                 Amount = resout.Discount,
                                                                 Category = resout.Category // from "Tariff" to resout.Category due to add aution work flow
                                                             }).Distinct().ToList();

                                    if (waivertariffRates.Count() > 0)
                                    {
                                        foreach (var item in waivertariffRates)
                                        {
                                            var result = totalItemsforviewmodel.Where(x => x.Category == item.Category && x.Description == item.Description).LastOrDefault();

                                            if (result == null && item.Amount > 0)
                                            {
                                                var irem = new InvoiceItemViewModel
                                                {
                                                    Amount = item.Amount,
                                                    Category = item.Category,
                                                    Description = item.Description
                                                };
                                                totalItemsforviewmodel.Add(irem);
                                            }
                                            else
                                            {
                                                var rasdes = totalItemsforviewmodel.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Amount);
                                                if (rasdes > 0)
                                                {
                                                    result.Amount += item.Amount;
                                                }
                                            }

                                        }
                                    }

                                }

                            }

                            #region special handling charges


                            if (container != null && shippingagent != null)
                            {

                                if (shippingagent.AllowSpecialChargeFCL == "No")
                                {


                                    var specialheandingcharges = (from OtherChargeAssigning in Db.ChargeAssignings
                                                                  where OtherChargeAssigning.ContainerIndexId == container.ContainerIndexId
                                                                  select new InvoiceItemViewModel
                                                                  {
                                                                      Description = OtherChargeAssigning.ChargeName,
                                                                      Amount = OtherChargeAssigning.ChargeAmount,
                                                                      TariffId = OtherChargeAssigning.OtherChargeAssigningId,
                                                                      TariffType = "AssignCharges",
                                                                      Category = "SpecialHandlingCharges",
                                                                      Type = "NoNGeneral"
                                                                  }).Distinct().ToList();

                                    if (specialheandingcharges.Count() > 0)
                                    {
                                        totalItemsforviewmodel.AddRange(specialheandingcharges);
                                    }

                                }

                            }

                            #endregion


                            totalItemsforviewmodel.RemoveAll(c => c.Amount <= 0);

                            foreach (var item in totalItemsforviewmodel)
                            {
                                item.Amount = Math.Round(item.Amount, 2);

                            }

                            if (totalItemsforviewmodel.Count() > 0)
                            {

                                totalItemsforviewmodel.ForEach(x => x.TariffInformationId = res);

                            }

                            return totalItemsforviewmodel;

                        }
                        else
                        {
                            var invoiceItemsres = GetInvoiceItemBycontainerIndexId(container.ContainerIndexId).Where(x => x.Category == "Tariff").ToList();
                            var perviouswaiver = GetPreviousWaiverCFS(container.ContainerIndexId).Where(x => x.Category == "Tariff").ToList();

                            if (invoiceItemsres.Count() > 0)
                            {

                                foreach (var item in invoiceItemsres)
                                {
                                    var result = resdata.Find(t => t.Description == item.Description && t.Category == item.Category);

                                    if (result != null)
                                    {
                                        result.Amount += item.Amount;
                                    }
                                    else
                                    {
                                        resdata.Add(item);
                                    }
                                }

                                if (resdata.Count() > 0)
                                {
                                    var GeneralCBMRateWeightRate = (from resout in resdata
                                                                    select new InvoiceItemViewModel
                                                                    {
                                                                        Description = resout.Description,
                                                                        Amount = resout.Amount,
                                                                        Category = "Tariff"
                                                                    }).Distinct().ToList();


                                    if (perviouswaiver.Count() > 0 && GeneralCBMRateWeightRate.Count() > 0)
                                    {
                                        foreach (var item in GeneralCBMRateWeightRate)
                                        {
                                            var result = perviouswaiver.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Discount);

                                            if (result > 0)
                                            {
                                                item.Amount += result;
                                            }
                                        }


                                    }

                                    totalItemsforviewmodel.AddRange(GeneralCBMRateWeightRate);

                                }

                            }

                            if (perviouswaiver.Count() > 0)
                            {
                                var newobjlist = new List<Waiver>();

                                foreach (var item in perviouswaiver)
                                {
                                    var result = newobjlist.Find(t => t.Description == item.Description && t.Category == item.Category);

                                    if (result != null)
                                    {
                                        result.Discount += item.Discount;
                                    }
                                    else
                                    {
                                        newobjlist.Add(item);
                                    }
                                }

                                if (newobjlist.Count() > 0)
                                {
                                    var waivertariffRates = (from resout in newobjlist
                                                             select new InvoiceItemViewModel
                                                             {
                                                                 Description = resout.Description,
                                                                 Amount = resout.Discount,
                                                                 Category = "Tariff"
                                                             }).Distinct().ToList();

                                    if (waivertariffRates.Count() > 0)
                                    {
                                        foreach (var item in waivertariffRates)
                                        {
                                            var result = totalItemsforviewmodel.Where(x => x.Category == item.Category && x.Description == item.Description).LastOrDefault();

                                            if (result == null && item.Amount > 0)
                                            {
                                                var irem = new InvoiceItemViewModel
                                                {
                                                    Amount = item.Amount,
                                                    Category = item.Category,
                                                    Description = item.Description
                                                };
                                                totalItemsforviewmodel.Add(irem);
                                            }
                                        }
                                    }



                                }
                            }


                            if (container != null)
                            {
                                var portchrges = new InvoiceItemViewModel
                                {
                                    Description = "Port Charges",
                                    Amount = Db.PortCharges.Where(s => s.VirNumber == container.VirNo && s.IndexNumber == container.IndexNo).Sum(s => s.TotalCharges),
                                    TariffId = container.ContainerIndexId,
                                    Category = "PortCharges",
                                    Type = "NoNGeneral"
                                };

                                totalItemsforviewmodel.Add(portchrges);

                            }



                            #region special handling charges


                            if (container != null && shippingagent != null)
                            {

                                if (shippingagent.AllowSpecialChargeFCL == "No")
                                {


                                    var specialheandingcharges = (from OtherChargeAssigning in Db.ChargeAssignings
                                                                  where OtherChargeAssigning.ContainerIndexId == container.ContainerIndexId
                                                                  select new InvoiceItemViewModel
                                                                  {
                                                                      Description = OtherChargeAssigning.ChargeName,
                                                                      Amount = OtherChargeAssigning.ChargeAmount,
                                                                      TariffId = OtherChargeAssigning.OtherChargeAssigningId,
                                                                      TariffType = "AssignCharges",
                                                                      Category = "SpecialHandlingCharges",
                                                                      Type = "NoNGeneral"
                                                                  }).Distinct().ToList();

                                    if (specialheandingcharges.Count() > 0)
                                    {

                                        //foreach (var item in specialheandingcharges)
                                        //{
                                        //    item.Amount -= GetPreviousWaiverCFS(container.ContainerIndexId).ToList().Where(x => x.Category == "SpecialHandlingChargers" && x.Description == item.Description).ToList().Sum(x => x.Discount);
                                        //}

                                        totalItemsforviewmodel.AddRange(specialheandingcharges);
                                    }

                                }

                            }

                            #endregion

                            #region auciton work previous invoice

                            var invoiceItemsauctionres = GetInvoiceItemBycontainerIndexId(container.ContainerIndexId).Where(x => x.Category == "Auction").ToList();

                            if (invoiceItemsauctionres.Count() > 0)
                            {

                                var invoiceItemresauctionitems = (from resout in invoiceItemsauctionres
                                                                  select new InvoiceItemViewModel
                                                                  {
                                                                      Description = resout.Description,
                                                                      Amount = resout.Amount,
                                                                      Category = "Auction",
                                                                      Type = "Auction"
                                                                  }).Distinct().ToList();



                                totalItemsforviewmodel.AddRange(invoiceItemresauctionitems);

                            }
                            #endregion


                            totalItemsforviewmodel.RemoveAll(c => c.Amount <= 0);

                            foreach (var item in totalItemsforviewmodel)
                            {
                                item.Amount = Math.Round(item.Amount, 2);

                            }

                            if (totalItemsforviewmodel.Count() > 0)
                            {

                                totalItemsforviewmodel.ForEach(x => x.TariffInformationId = res);

                            }

                            return totalItemsforviewmodel;
                        }


                    }


                }

                double sumvalue = 0;
                long percentagentid = 0;


                //  need 

                //var indexnumbrcount = 0;

                var conainerindex = new List<ContainerIndex>();

                //var containerslist = (from containerindexdata in Db.ContainerIndices
                //                      where containerindexdata.VirNo == container.VirNo && containerindexdata.IndexNo == container.IndexNo
                //                      select containerindexdata.ContainerNo).ToList();



                var containerslist = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {container.VirNo} and IndexNo = {container.IndexNo} and IsDeleted = 0").Select(x => x.ContainerNo).ToList();


                var indexcontainers = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {container.VirNo} and IndexNo = {container.IndexNo} and IsDeleted = 0").ToList();





                //foreach (var item in containerslist)
                //{
                //    //var data = (from containerindexdata in Db.ContainerIndices
                //    //            where containerindexdata.VirNo == container.VirNo && containerindexdata.ContainerNo == item
                //    //            select containerindexdata).ToList();

                //    var data = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {container.VirNo}  and ContainerNo = {item} and IsDeleted = 0").ToList();


                //    if (data.Count() > 0)
                //    {
                //        conainerindex.AddRange(data);
                //    }

                //}


                //if (conainerindex.Count() > 0)
                //{
                //    indexnumbrcount = conainerindex.Select(x => x.IndexNo).Distinct().Count();

                //}



                if (res > 0)
                {

                    #region WeightRate General

                    var GeneralCBMRateWeightRate = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                    from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                    join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                    where
                                                    tariffInofrmationTariff.TariffInformationId == res
                                                    && tariff.IsGeneral == true
                                                    //ovias&& tariff.TariffType == "CFS"
                                                    //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                    //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)

                                                    && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                    && (tariff.WeightRate > 0)
                                                    &&
                                                    (
                                                    tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                    tariff.TypeOfImplementation == "Arrival" ?
                                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.TypeOfImplementation == "Delivery" ?
                                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.ImplementFrom == null
                                                    )
                                                    &&
                                                    (
                                                    tariff.TypeOfImplementation == "All" ?
                                                    tariff.ImplementTo == null :
                                                    tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.ImplementTo == null
                                                    )

                                                    select new InvoiceItemViewModel
                                                    {
                                                        Description = tariff.TariffHead.Name,
                                                        //Amount = weightOrCBM == 1 ? (tariff.WeightRate * weightConv) : (tariff.CBMRate * calculateCBM),
                                                        Amount = ((tariff.IsDollerRate ? (tariff.WeightRate * dollerrate) : tariff.WeightRate) * weightConv),
                                                        TariffId = tariff.TariffId,
                                                        TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                        Category = "Tariff"
                                                    }).Distinct().ToList();

                    tariffInfos.AddRange(GeneralCBMRateWeightRate);

                    #endregion


                    #region PerIndexRate General
                    var GeneralPerIndexRate = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                               from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                               join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                               where
                                               tariffInofrmationTariff.TariffInformationId == res
                                               && tariff.IsGeneral == true
                                               //ovias&& tariff.TariffType == "CFS"
                                               //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                               //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                               && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                               && tariff.PerIndexRate > 0
                                                &&
                                                (
                                                tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                tariff.TypeOfImplementation == "Arrival" ?
                                                Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                tariff.TypeOfImplementation == "Delivery" ?
                                                Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                tariff.ImplementFrom == null
                                                )
                                                &&
                                                (
                                                tariff.TypeOfImplementation == "All" ?
                                                tariff.ImplementTo == null :
                                                tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                tariff.ImplementTo == null
                                                )
                                               select new InvoiceItemViewModel
                                               {
                                                   Description = tariff.TariffHead.Name,
                                                   //Amount =   tariff.PerIndexRate,
                                                   Amount = tariff.IsDollerRate ? (tariff.PerIndexRate * dollerrate) : tariff.PerIndexRate,
                                                   TariffId = tariff.TariffId,
                                                   TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                   Category = "Tariff"
                                               }).Distinct().ToList();

                    tariffInfos.AddRange(GeneralPerIndexRate);
                    #endregion


                    #region Size General

                    foreach (var indexdetail in indexcontainers)
                    {
                        if (indexdetail.IsPartialContainer == true)
                        {
                            var noofcontainers = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {indexdetail.VirNo}   and ContainerNo = {indexdetail.ContainerNo} and IsDeleted = 0").ToList();

                            if (noofcontainers.Count() > 0)
                            {
                                var CFSLCLtariffInfos = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                         from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                         join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                         where
                                                         tariffInofrmationTariff.TariffInformationId == res
                                                         && tariff.IsGeneral == true
                                                         //ovias&& tariff.TariffType == "CFSLCL"
                                                         //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                         //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                         && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                         && (tariff.Rate20 > 0 || tariff.Rate40 > 0 || tariff.Rate45 > 0)
                                                         &&
                                                                (
                                                                tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                                tariff.TypeOfImplementation == "Arrival" ?
                                                                Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                tariff.TypeOfImplementation == "Delivery" ?
                                                                Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                tariff.ImplementFrom == null
                                                                )
                                                                &&
                                                                (
                                                                tariff.TypeOfImplementation == "All" ?
                                                                tariff.ImplementTo == null :
                                                                tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                                Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                                Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                tariff.ImplementTo == null
                                                                )

                                                         select new InvoiceItemViewModel
                                                         {
                                                             Description = tariff.TariffHead.Name,
                                                             //Amount = container.Size == 20 ? (tariff.Rate20  / noofindees.Count()) : container.Size == 40 ? ( tariff.Rate40 / noofindees.Count()) : container.Size == 45 ? ( tariff.Rate45 / noofindees.Count()) : 0,
                                                             //Amount = container.Size == 20 ? ((tariff.IsDollerRate ? (tariff.Rate20 * Convert.ToInt64(dollerrate)) : tariff.Rate20) * containerslist.Count()) : container.Size == 40 ? ((tariff.IsDollerRate ? (tariff.Rate40 * Convert.ToInt64(dollerrate)) : tariff.Rate40) * containerslist.Count()) : container.Size == 45 ? ((tariff.IsDollerRate ? (tariff.Rate45 * Convert.ToInt64(dollerrate)) : tariff.Rate45) * containerslist.Count()) : 0,
                                                             Amount = indexdetail.Size == 20 ? (tariff.IsDollerRate ? (tariff.Rate20 * dollerrate) : tariff.Rate20) : indexdetail.Size == 40 ? (tariff.IsDollerRate ? (tariff.Rate40 * dollerrate) : tariff.Rate40) : indexdetail.Size == 45 ? (tariff.IsDollerRate ? (tariff.Rate45 * dollerrate) : tariff.Rate45) : 0,
                                                             TariffId = tariff.TariffId,
                                                             TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                             Category = "Tariff"
                                                         }).Distinct().ToList();

                                CFSLCLtariffInfos.ForEach(x => x.Amount = x.Amount / noofcontainers.Count());

                                tariffInfos.AddRange(CFSLCLtariffInfos);
                            }

                        }
                        else
                        {
                            var CFSLCLtariffInfos = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                     from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                     join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                     where
                                                     tariffInofrmationTariff.TariffInformationId == res
                                                     && tariff.IsGeneral == true
                                                     //ovias&& tariff.TariffType == "CFSLCL"
                                                     //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                     //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                     && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                     && (tariff.Rate20 > 0 || tariff.Rate40 > 0 || tariff.Rate45 > 0)
                                                     &&
                                                            (
                                                            tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                            tariff.TypeOfImplementation == "Arrival" ?
                                                            Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                            tariff.TypeOfImplementation == "Delivery" ?
                                                            Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                            tariff.ImplementFrom == null
                                                            )
                                                            &&
                                                            (
                                                            tariff.TypeOfImplementation == "All" ?
                                                            tariff.ImplementTo == null :
                                                            tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                            Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                            tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                            Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                            tariff.ImplementTo == null
                                                            )

                                                     select new InvoiceItemViewModel
                                                     {
                                                         Description = tariff.TariffHead.Name,
                                                         //Amount = container.Size == 20 ? (tariff.Rate20  / noofindees.Count()) : container.Size == 40 ? ( tariff.Rate40 / noofindees.Count()) : container.Size == 45 ? ( tariff.Rate45 / noofindees.Count()) : 0,
                                                         //Amount = container.Size == 20 ? ((tariff.IsDollerRate ? (tariff.Rate20 * Convert.ToInt64(dollerrate)) : tariff.Rate20) * containerslist.Count()) : container.Size == 40 ? ((tariff.IsDollerRate ? (tariff.Rate40 * Convert.ToInt64(dollerrate)) : tariff.Rate40) * containerslist.Count()) : container.Size == 45 ? ((tariff.IsDollerRate ? (tariff.Rate45 * Convert.ToInt64(dollerrate)) : tariff.Rate45) * containerslist.Count()) : 0,
                                                         Amount = indexdetail.Size == 20 ? (tariff.IsDollerRate ? (tariff.Rate20 * dollerrate) : tariff.Rate20) : indexdetail.Size == 40 ? (tariff.IsDollerRate ? (tariff.Rate40 * dollerrate) : tariff.Rate40) : indexdetail.Size == 45 ? (tariff.IsDollerRate ? (tariff.Rate45 * dollerrate) : tariff.Rate45) : 0,
                                                         TariffId = tariff.TariffId,
                                                         TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                         Category = "Tariff"
                                                     }).Distinct().ToList();

                            tariffInfos.AddRange(CFSLCLtariffInfos);
                        }


                    }



                    #endregion


                    foreach (var item in tariffInfos)
                    {
                        sumvalue += item.Amount;

                        percentagentid = item.TariffPercentId;

                    }


                }



                if (sumvalue > 0)
                {

                    var percenttariftotal = (from tariffPercentage in Db.TariffPercentages
                                             join tariffHead in Db.TariffHeads on tariffPercentage.TariffHeadId equals tariffHead.TariffHeadId
                                             where
                                              tariffPercentage.ShippingAgentId == percentagentid
                                             //&&
                                             //  tariffPercentage.TariffPercentType == "WITHPERCENT"
                                             select new InvoiceItemViewModel
                                             {
                                                 Description = tariffHead.Name,
                                                 Amount = (sumvalue / 100) * (tariffPercentage.RateOnPersent),
                                                 TariffId = tariffPercentage.TariffPercentageId,
                                                 Type = "General",
                                                 Category = "Tariff"
                                             }).Distinct().ToList();

                    totalItems.AddRange(percenttariftotal);
                }


                if (res > 0)
                {

                    #region WeightRate 

                    var tariffInfosnongeneral = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                 from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                 join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                 where
                                                  tariffInofrmationTariff.TariffInformationId == res
                                                  //ovias&& tariff.TariffType == "CFS"
                                                  && tariff.IsGeneral == false
                                                  //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                  //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                  && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                  //&& (tariff.CBMRate > 0 || tariff.WeightRate > 0)
                                                  && (tariff.WeightRate > 0)
                                                  &&
                                                    (
                                                    tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                    tariff.TypeOfImplementation == "Arrival" ?
                                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.TypeOfImplementation == "Delivery" ?
                                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.ImplementFrom == null
                                                    )
                                                    &&
                                                    (
                                                    tariff.TypeOfImplementation == "All" ?
                                                    tariff.ImplementTo == null :
                                                    tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.ImplementTo == null
                                                    )
                                                 select new InvoiceItemViewModel
                                                 {
                                                     Description = tariff.TariffHead.Name,
                                                     //Amount = weightOrCBM == 1 ? (tariff.WeightRate * weightConv) : (tariff.CBMRate * calculateCBM),
                                                     Amount = ((tariff.IsDollerRate ? (tariff.WeightRate * dollerrate) : tariff.WeightRate) * weightConv),
                                                     TariffId = tariff.TariffId,
                                                     TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                     Type = "NoNGeneral",
                                                     Category = "Tariff"
                                                 }).Distinct().ToList();


                    totalItems.AddRange(tariffInfosnongeneral);
                    #endregion



                    #region PerIndexRate
                    var indextariffInfosnongeneral = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                      from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                      join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                      where
                                                       tariffInofrmationTariff.TariffInformationId == res
                                                      //ovias && tariff.TariffType == "CFS"
                                                      && tariff.IsGeneral == false
                                                      //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                      //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                      && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                      && tariff.PerIndexRate > 0
                                                     &&
                                                        (
                                                        tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                        tariff.TypeOfImplementation == "Arrival" ?
                                                        Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.TypeOfImplementation == "Delivery" ?
                                                        Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.ImplementFrom == null
                                                        )
                                                        &&
                                                        (
                                                        tariff.TypeOfImplementation == "All" ?
                                                        tariff.ImplementTo == null :
                                                        tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                        Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                        Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.ImplementTo == null
                                                        )
                                                      select new InvoiceItemViewModel
                                                      {
                                                          Description = tariff.TariffHead.Name,
                                                          //Amount = tariff.PerIndexRate,
                                                          Amount = tariff.IsDollerRate ? (tariff.PerIndexRate * dollerrate) : tariff.PerIndexRate,
                                                          TariffId = tariff.TariffId,
                                                          TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                          Type = "NoNGeneral",
                                                          Category = "Tariff"
                                                      }).Distinct().ToList();


                    totalItems.AddRange(indextariffInfosnongeneral);
                    #endregion


                    #region Size

                    foreach (var indexdetail in indexcontainers)
                    {

                        if (indexdetail.IsPartialContainer == true)
                        {
                            var noofcontainers = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {indexdetail.VirNo}   and ContainerNo = {indexdetail.ContainerNo} and IsDeleted = 0").ToList();

                            if (noofcontainers.Count() > 0)
                            {
                                var CFSLCLtariffInfosnongeneral = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                                   from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                                   join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                                   where
                                                                    tariffInofrmationTariff.TariffInformationId == res
                                                                    && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                                   && (tariff.Rate20 > 0 || tariff.Rate40 > 0 || tariff.Rate45 > 0)
                                                                   //ovias && tariff.TariffType == "CFSLCL"
                                                                   && tariff.IsGeneral == false
                                                                   // && (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                                   // && (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                                   &&
                                                                    (
                                                                    tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                                    tariff.TypeOfImplementation == "Arrival" ?
                                                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                    tariff.TypeOfImplementation == "Delivery" ?
                                                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                    tariff.ImplementFrom == null
                                                                    )
                                                                    &&
                                                                    (
                                                                    tariff.TypeOfImplementation == "All" ?
                                                                    tariff.ImplementTo == null :
                                                                    tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                    tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                    tariff.ImplementTo == null
                                                                    )
                                                                   select new InvoiceItemViewModel
                                                                   {
                                                                       Description = tariff.TariffHead.Name,
                                                                       //Amount = container.Size == 20 ? (tariff.Rate20 / noofindees.Count()) : container.Size == 40 ? (tariff.Rate40 / noofindees.Count()) : container.Size == 45 ? (tariff.Rate45 / noofindees.Count()) : 0,
                                                                       //Amount = container.Size == 20 ? ((tariff.IsDollerRate ? (tariff.Rate20 * Convert.ToInt64(dollerrate)) : tariff.Rate20) * containerslist.Count()) : container.Size == 40 ? ((tariff.IsDollerRate ? (tariff.Rate40 * Convert.ToInt64(dollerrate)) : tariff.Rate40) * containerslist.Count()) : container.Size == 45 ? ((tariff.IsDollerRate ? (tariff.Rate45 * Convert.ToInt64(dollerrate)) : tariff.Rate45) * containerslist.Count()) : 0,
                                                                       Amount = indexdetail.Size == 20 ? (tariff.IsDollerRate ? (tariff.Rate20 * dollerrate) : tariff.Rate20) : indexdetail.Size == 40 ? (tariff.IsDollerRate ? (tariff.Rate40 * dollerrate) : tariff.Rate40) : indexdetail.Size == 45 ? (tariff.IsDollerRate ? (tariff.Rate45 * dollerrate) : tariff.Rate45) : 0,
                                                                       TariffId = tariff.TariffId,
                                                                       TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                                       Type = "NoNGeneral",
                                                                       Category = "Tariff"
                                                                   }).Distinct().ToList();

                                CFSLCLtariffInfosnongeneral.ForEach(x => x.Amount = x.Amount / noofcontainers.Count());

                                totalItems.AddRange(CFSLCLtariffInfosnongeneral);
                            }

                        }
                        else
                        {
                            var CFSLCLtariffInfosnongeneral = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                               from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                               join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                               where
                                                                tariffInofrmationTariff.TariffInformationId == res
                                                                && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                               && (tariff.Rate20 > 0 || tariff.Rate40 > 0 || tariff.Rate45 > 0)
                                                               //ovias && tariff.TariffType == "CFSLCL"
                                                               && tariff.IsGeneral == false
                                                               // && (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                               // && (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                               &&
                                                                (
                                                                tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                                tariff.TypeOfImplementation == "Arrival" ?
                                                                Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                tariff.TypeOfImplementation == "Delivery" ?
                                                                Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                tariff.ImplementFrom == null
                                                                )
                                                                &&
                                                                (
                                                                tariff.TypeOfImplementation == "All" ?
                                                                tariff.ImplementTo == null :
                                                                tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                                Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                                Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                tariff.ImplementTo == null
                                                                )
                                                               select new InvoiceItemViewModel
                                                               {
                                                                   Description = tariff.TariffHead.Name,
                                                                   //Amount = container.Size == 20 ? (tariff.Rate20 / noofindees.Count()) : container.Size == 40 ? (tariff.Rate40 / noofindees.Count()) : container.Size == 45 ? (tariff.Rate45 / noofindees.Count()) : 0,
                                                                   //Amount = container.Size == 20 ? ((tariff.IsDollerRate ? (tariff.Rate20 * Convert.ToInt64(dollerrate)) : tariff.Rate20) * containerslist.Count()) : container.Size == 40 ? ((tariff.IsDollerRate ? (tariff.Rate40 * Convert.ToInt64(dollerrate)) : tariff.Rate40) * containerslist.Count()) : container.Size == 45 ? ((tariff.IsDollerRate ? (tariff.Rate45 * Convert.ToInt64(dollerrate)) : tariff.Rate45) * containerslist.Count()) : 0,
                                                                   Amount = indexdetail.Size == 20 ? (tariff.IsDollerRate ? (tariff.Rate20 * dollerrate) : tariff.Rate20) : indexdetail.Size == 40 ? (tariff.IsDollerRate ? (tariff.Rate40 * dollerrate) : tariff.Rate40) : indexdetail.Size == 45 ? (tariff.IsDollerRate ? (tariff.Rate45 * dollerrate) : tariff.Rate45) : 0,
                                                                   TariffId = tariff.TariffId,
                                                                   TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                                   Type = "NoNGeneral",
                                                                   Category = "Tariff"
                                                               }).Distinct().ToList();


                            totalItems.AddRange(CFSLCLtariffInfosnongeneral);
                        }



                    }



                    #endregion



                }






                //if (container.PortAndTerminalId != null)
                //{

                //    if (container.ShippingAgentId != null)
                //    {
                //        var shipingagent = (from shipagnt in Db.ShippingAgents
                //                            where shipagnt.ShippingAgentId == container.ShippingAgentId
                //                            select shipagnt).LastOrDefault();

                //        if (!shipingagent.Name.Contains("MAERSK"))
                //        {
                //            var portandterminal = (from portandtermil in Db.PortAndTerminals
                //                                   where
                //                                   (portandtermil.PortAndTerminalId == container.PortAndTerminalId)
                //                                   && (portandtermil.RateAmount20 > 0 || portandtermil.RateAmount40 > 0 || portandtermil.RateAmount45 > 0)

                //                                   select new InvoiceItemViewModel
                //                                   {
                //                                       Description = "Port TP Charges",
                //                                       //Amount = container.Size == 20 ? (tariff.Rate20 / noofindees.Count()) : container.Size == 40 ? (tariff.Rate40 / noofindees.Count()) : container.Size == 45 ? (tariff.Rate45 / noofindees.Count()) : 0,
                //                                       Amount = container.Size == 20 ? portandtermil.RateAmount20 * containerslist.Count() : container.Size == 40 ? portandtermil.RateAmount40 * containerslist.Count() : container.Size == 45 ? portandtermil.RateAmount45 * containerslist.Count() : 0,
                //                                       TariffId = portandtermil.PortAndTerminalId,
                //                                       Type = "NoNGeneral"
                //                                   }).LastOrDefault();

                //            if (portandterminal != null)
                //            {
                //                totalItems.Add(portandterminal);
                //            }
                //        }

                //    }

                //}

                if (container != null)
                {
                    var portchrges = new InvoiceItemViewModel
                    {
                        Description = "Port Charges",
                        Amount = Db.PortCharges.Where(s => s.VirNumber == container.VirNo && s.IndexNumber == container.IndexNo).Sum(s => s.TotalCharges),
                        TariffId = container.ContainerIndexId,
                        Type = "NoNGeneral",
                        Category = "PortCharges"
                    };

                    totalItems.Add(portchrges);

                }





                #region devided indexs


                #region DevededIndexAmount

                if (res > 0)
                {
                    foreach (var indexdetail in indexcontainers)
                    {
                        var noofcontainers = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {indexdetail.VirNo}   and ContainerNo = {indexdetail.ContainerNo} and IsDeleted = 0").Select(x => x.IndexNo).Distinct().ToList();

                        if (noofcontainers.Count() > 0)
                        {
                            var indextariffInfosdevidedindexs = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                                 from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                                 join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                                 where
                                                                 tariffInofrmationTariff.TariffInformationId == res
                                                                 //ovias&&  tariff.TariffType == "CFS"
                                                                 && tariff.IsGeneral == false
                                                                //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                                //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                                && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                                && tariff.DevededIndexAmount > 0
                                                                &&
                                                                (
                                                                tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                                tariff.TypeOfImplementation == "Arrival" ?
                                                                Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                tariff.TypeOfImplementation == "Delivery" ?
                                                                Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                tariff.ImplementFrom == null
                                                                )
                                                                &&
                                                                (
                                                                tariff.TypeOfImplementation == "All" ?
                                                                tariff.ImplementTo == null :
                                                                tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                                Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                                Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                tariff.ImplementTo == null
                                                                )
                                                                 select new InvoiceItemViewModel
                                                                 {
                                                                     Description = tariff.TariffHead.Name,
                                                                     //Amount = (Convert.ToDouble( tariff.DevededIndexAmount) / containerCount.Count()),
                                                                     Amount = ((tariff.IsDollerRate ? (tariff.DevededIndexAmount * dollerrate) : tariff.DevededIndexAmount) / noofcontainers.Count()),
                                                                     TariffId = tariff.TariffId,
                                                                     Type = "NoNGeneral",
                                                                     Category = "Tariff"
                                                                 }).Distinct().ToList();


                            totalItems.AddRange(indextariffInfosdevidedindexs);
                        }

                    }

                }

                #endregion



                #endregion


                #region special handling charges


                if (container != null && shippingagent != null)
                {

                    if (shippingagent.AllowSpecialChargeFCL == "No")
                    {


                        var specialheandingcharges = (from OtherChargeAssigning in Db.ChargeAssignings
                                                      where OtherChargeAssigning.ContainerIndexId == container.ContainerIndexId
                                                      select new InvoiceItemViewModel
                                                      {
                                                          Description = OtherChargeAssigning.ChargeName,
                                                          Amount = OtherChargeAssigning.ChargeAmount,
                                                          TariffId = OtherChargeAssigning.OtherChargeAssigningId,
                                                          TariffType = "AssignCharges",
                                                          Type = "NoNGeneral",
                                                          Category = "SpecialHandlingCharges"
                                                      }).Distinct().ToList();

                        if (specialheandingcharges.Count() > 0)
                        {
                            totalItems.AddRange(specialheandingcharges);
                        }

                    }

                }

                #endregion


                #region auciton work current invoice


                var isauction = GetAuctionInfo("CFS", container.VirNo, container.IndexNo ?? 0);

                if (isauction == true)
                {
                    var handlingamount = 0.00;

                    foreach (var item in indexcontainers)
                    {
                        handlingamount += item.Size == 20 ? (auctionamountinfo.Rate20) : item.Size == 40 ? (auctionamountinfo.Rate40) : item.Size == 45 ? (auctionamountinfo.Rate45) : 0;
                    }

                    if (handlingamount > 0)
                    {
                        var aucitonamount = new InvoiceItemViewModel
                        {
                            Description = "AuctionHandingCharges",
                            Amount = handlingamount,
                            TariffId = auctionamountinfo.AuctionAmountId,
                            TariffType = "Auction",
                            Type = "Auction",
                            Category = "Auction"
                        };

                        totalItems.Add(aucitonamount);
                    }

                }

                #endregion


                totalItems.RemoveAll(c => c.Amount <= 0);

                foreach (var item in totalItems)
                {
                    item.Amount = Math.Round(item.Amount, 2);

                }

                if (totalItems.Count() > 0)
                {
                    foreach (var item in totalItems)
                    {
                        item.TariffInformationId = res;

                    }
                }



            }


            if (indexcargotype == "LCL")
            {

                var newtotalItems = new List<InvoiceItemViewModel>();

                var indexcbmcharge = 0.00;
                var indexperindexcharge = 0.00;
                var totalcbmperindexcharge = 0.00;


                var dollerrate = (from rate in Db.ExchangeRates
                                  select rate.ExchangeRateAmount).FirstOrDefault();


                //var container = (from containerindex in Db.ContainerIndices
                //                 where containerindex.ContainerIndexId == ContainerIndexId
                //                 select containerindex).LastOrDefault();

                var container = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where ContainerIndexId = {ContainerIndexId} and IsDeleted = 0").LastOrDefault();


                //var shippingagent = (from shippingAgent in Db.ShippingAgents
                //                     where shippingAgent.ShippingAgentId == container.ShippingAgentId
                //                     select shippingAgent).FirstOrDefault();


                var shippingagent = Db.ShippingAgents.FromSql($"SELECT * From ShippingAgent Where ShippingAgentId = {container.ShippingAgentId} and IsDeleted = 0").LastOrDefault();


                //if (shippingagent.WeightAllow == "Yes")
                //{
                //    Weight = (Weight * shippingagent.WeightAmount);
                //}





                var calculateCBM = CBM;

                var calculateWeight = Weight;

                var weightOrCBM = WeightGreaterThenCBM(calculateWeight, calculateCBM);

                var weightConv = (Convert.ToDouble(calculateWeight) / 1000.0);




                //var isocode = (from isocodes in Db.ISOCodes
                //               from isoCodeHead in Db.ISOCodeHeads.Where(x => x.ISOCodeHeadId == x.ISOCodeHeadId).DefaultIfEmpty()
                //               where container.ISOCode == isocodes.Code
                //               select isoCodeHead).FirstOrDefault();



                var isocode = Db.ISOCodeHeads.FromSql($"select isoCodeHead.ISOCodeHeadId , isoCodeHead.ISOCodeHeadDescription  , isoCodeHead.DeleteDate , isoCodeHead.IsDeleted  from ISOCode   left join isoCodeHead  on ISOCode.ISOCodeHeadId = ISOCodeHead.ISOCodeHeadId  and isoCodeHead.IsDeleted = 0 Where     ISOCode.Code    = {container.ISOCode} and ISOCode.IsDeleted = 0").FirstOrDefault();


                if (isocode == null)
                {
                    return totalItems;

                }



                var tariffInfos = new List<InvoiceItemViewModel>();

                var res = GetTariffAllList(container.ShippingAgentId, isocode.ISOCodeHeadId, container.ConsigneId, clearingAgentId, container.ShipperId, container.GoodsHeadId, container.PortAndTerminalId, indexcargotype);

                if (container != null)
                {
                    var resdata = new List<InvoiceItem>();
                    var totalItemsforviewmodel = new List<InvoiceItemViewModel>();

                    var invoiceresdata = Db.Invoices.FromSql($"select * from Invoice Where ContainerIndexId = {container.ContainerIndexId} and  IsDeleted = 0 ").FirstOrDefault();
                    if (invoiceresdata != null)
                    {
                        var DeliveredCargogatepass = Db.DOItems.FromSql($"select DOItem.* from GatePass join DOItem  on GatePass.GatePassId = DOItem.GatePassId where GatePass.VirNumber ={container.VirNo} and  GatePass.IndexNo = {container.IndexNo} and  DOItem.Status = 'F' and DOItem.IsDeleted =0 and GatePass.IsDeleted = 0 ").ToList();


                        if (DeliveredCargogatepass.Count() > 0)
                        {
                            var invoiceItemsres = GetInvoiceItemBycontainerIndexId(container.ContainerIndexId).Where(x => x.Category != "SpecialHandlingCharges" && x.Category != "Storage").ToList();

                            var perviouswaiver = GetPreviousWaiverCFS(container.ContainerIndexId).Where(x => x.Category != "SpecialHandlingCharges" && x.Category != "Storage").ToList();

                            if (invoiceItemsres.Count() > 0)
                            {

                                foreach (var item in invoiceItemsres)
                                {
                                    var result = resdata.Find(t => t.Description == item.Description && t.Category == item.Category);

                                    if (result != null)
                                    {
                                        result.Amount += item.Amount;
                                    }
                                    else
                                    {
                                        resdata.Add(item);

                                    }
                                }

                                if (resdata.Count() > 0)
                                {
                                    var GeneralCBMRateWeightRate = (from resout in resdata
                                                                    select new InvoiceItemViewModel
                                                                    {
                                                                        Description = resout.Description,
                                                                        Amount = resout.Amount,
                                                                        Category = resout.Category
                                                                    }).Distinct().ToList();

                                    totalItemsforviewmodel.AddRange(GeneralCBMRateWeightRate);

                                }

                            }



                            if (perviouswaiver.Count() > 0)
                            {
                                var newobjlist = new List<Waiver>();

                                foreach (var item in perviouswaiver)
                                {
                                    var result = newobjlist.Find(t => t.Description == item.Description && t.Category == item.Category);

                                    if (result != null)
                                    {
                                        result.Discount += item.Discount;
                                    }
                                    else
                                    {
                                        newobjlist.Add(item);
                                    }
                                }

                                if (newobjlist.Count() > 0)
                                {
                                    var waivertariffRates = (from resout in newobjlist
                                                             select new InvoiceItemViewModel
                                                             {
                                                                 Description = resout.Description,
                                                                 Amount = resout.Discount,
                                                                 Category = resout.Category // from "Tariff" to resout.Category due to add aution work flow
                                                             }).Distinct().ToList();

                                    if (waivertariffRates.Count() > 0)
                                    {
                                        foreach (var item in waivertariffRates)
                                        {
                                            var result = totalItemsforviewmodel.Where(x => x.Category == item.Category && x.Description == item.Description).LastOrDefault();

                                            if (result == null && item.Amount > 0)
                                            {
                                                var irem = new InvoiceItemViewModel
                                                {
                                                    Amount = item.Amount,
                                                    Category = item.Category,
                                                    Description = item.Description
                                                };
                                                totalItemsforviewmodel.Add(irem);
                                            }
                                            else
                                            {
                                                var rasdes = totalItemsforviewmodel.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Amount);
                                                if (rasdes > 0)
                                                {
                                                    result.Amount += item.Amount;
                                                }
                                            }

                                        }
                                    }

                                }

                            }


                            #region special handling charges


                            if (container != null && shippingagent != null)
                            {

                                if (shippingagent.AllowSpecialChargeLCL == "No")
                                {


                                    var specialheandingcharges = (from OtherChargeAssigning in Db.ChargeAssignings
                                                                  where OtherChargeAssigning.ContainerIndexId == container.ContainerIndexId
                                                                  select new InvoiceItemViewModel
                                                                  {
                                                                      Description = OtherChargeAssigning.ChargeName,
                                                                      Amount = OtherChargeAssigning.ChargeAmount,
                                                                      TariffId = OtherChargeAssigning.OtherChargeAssigningId,
                                                                      TariffType = "AssignCharges",
                                                                      Category = "SpecialHandlingCharges",
                                                                      Type = "NoNGeneral"
                                                                  }).Distinct().ToList();

                                    if (specialheandingcharges.Count() > 0)
                                    {
                                        totalItemsforviewmodel.AddRange(specialheandingcharges);
                                    }

                                }

                            }

                            #endregion


                            totalItemsforviewmodel.RemoveAll(c => c.Amount <= 0);

                            foreach (var item in totalItemsforviewmodel)
                            {
                                item.Amount = Math.Round(item.Amount, 2);

                            }

                            if (totalItemsforviewmodel.Count() > 0)
                            {

                                totalItemsforviewmodel.ForEach(x => x.TariffInformationId = res);

                            }

                            return totalItemsforviewmodel;


                        }
                        else
                        {
                            var invoiceItemsres = GetInvoiceItemBycontainerIndexId(container.ContainerIndexId).Where(x => x.Category == "Tariff").ToList();

                            var perviouswaiver = GetPreviousWaiverCFS(container.ContainerIndexId).Where(x => x.Category == "Tariff").ToList();

                            if (invoiceItemsres.Count() > 0)
                            {

                                foreach (var item in invoiceItemsres)
                                {
                                    var result = resdata.Find(t => t.Description == item.Description && t.Category == item.Category);

                                    if (result != null)
                                    {
                                        result.Amount += item.Amount;
                                    }
                                    else
                                    {
                                        resdata.Add(item);

                                    }
                                }

                                if (resdata.Count() > 0)
                                {
                                    var GeneralCBMRateWeightRate = (from resout in resdata
                                                                    select new InvoiceItemViewModel
                                                                    {
                                                                        Description = resout.Description,
                                                                        Amount = resout.Amount,
                                                                        Category = "Tariff"
                                                                    }).Distinct().ToList();

                                    if (perviouswaiver.Count() > 0 && GeneralCBMRateWeightRate.Count() > 0)
                                    {
                                        foreach (var item in GeneralCBMRateWeightRate)
                                        {
                                            var result = perviouswaiver.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Discount);

                                            if (result > 0)
                                            {
                                                item.Amount += result;
                                            }
                                        }
                                    }


                                    totalItemsforviewmodel.AddRange(GeneralCBMRateWeightRate);

                                }



                            }


                            if (perviouswaiver.Count() > 0)
                            {
                                var newobjlist = new List<Waiver>();

                                foreach (var item in perviouswaiver)
                                {
                                    var result = newobjlist.Find(t => t.Description == item.Description && t.Category == item.Category);

                                    if (result != null)
                                    {
                                        result.Discount += item.Discount;
                                    }
                                    else
                                    {
                                        newobjlist.Add(item);
                                    }
                                }

                                if (newobjlist.Count() > 0)
                                {
                                    var waivertariffRates = (from resout in newobjlist
                                                             select new InvoiceItemViewModel
                                                             {
                                                                 Description = resout.Description,
                                                                 Amount = resout.Discount,
                                                                 Category = "Tariff"
                                                             }).Distinct().ToList();

                                    if (waivertariffRates.Count() > 0)
                                    {
                                        foreach (var item in waivertariffRates)
                                        {
                                            var result = totalItemsforviewmodel.Where(x => x.Category == item.Category && x.Description == item.Description).LastOrDefault();

                                            if (result == null && item.Amount > 0)
                                            {
                                                var irem = new InvoiceItemViewModel
                                                {
                                                    Amount = item.Amount,
                                                    Category = item.Category,
                                                    Description = item.Description
                                                };
                                                totalItemsforviewmodel.Add(irem);
                                            }
                                        }
                                    }

                                }
                            }





                            if (container != null)
                            {
                                var portchrges = new InvoiceItemViewModel
                                {
                                    Description = "Port Charges",
                                    Amount = Db.PortCharges.Where(s => s.VirNumber == container.VirNo && s.IndexNumber == container.IndexNo).Sum(s => s.TotalCharges),
                                    TariffId = container.ContainerIndexId,
                                    Category = "PortCharges",
                                    Type = "NoNGeneral"
                                };

                                //if (portchrges != null)
                                //{
                                //    portchrges.Amount -= GetPreviousWaiverCFS(container.ContainerIndexId).ToList().Where(x => x.Category == "PortChargers").ToList().Sum(x => x.Discount);
                                //}

                                totalItemsforviewmodel.Add(portchrges);

                            }



                            #region special handling charges


                            if (container != null && shippingagent != null)
                            {

                                if (shippingagent.AllowSpecialChargeLCL == "No")
                                {


                                    var specialheandingcharges = (from OtherChargeAssigning in Db.ChargeAssignings
                                                                  where OtherChargeAssigning.ContainerIndexId == container.ContainerIndexId
                                                                  select new InvoiceItemViewModel
                                                                  {
                                                                      Description = OtherChargeAssigning.ChargeName,
                                                                      Amount = OtherChargeAssigning.ChargeAmount,
                                                                      TariffId = OtherChargeAssigning.OtherChargeAssigningId,
                                                                      TariffType = "AssignCharges",
                                                                      Category = "SpecialHandlingCharges",
                                                                      Type = "NoNGeneral"
                                                                  }).Distinct().ToList();

                                    if (specialheandingcharges.Count() > 0)
                                    {
                                        //foreach (var item in specialheandingcharges)
                                        //{
                                        //    item.Amount -= GetPreviousWaiverCFS(container.ContainerIndexId).ToList().Where(x => x.Category == "SpecialHandlingChargers" && x.Description == item.Description).ToList().Sum(x => x.Discount);
                                        //}

                                        totalItemsforviewmodel.AddRange(specialheandingcharges);
                                    }

                                }

                            }

                            #endregion

                            #region auciton work previous invoice

                            var invoiceItemsauctionres = GetInvoiceItemBycontainerIndexId(container.ContainerIndexId).Where(x => x.Category == "Auction").ToList();

                            if (invoiceItemsauctionres.Count() > 0)
                            {
                                var invoiceItemresauctionitems = (from resout in invoiceItemsauctionres
                                                                  select new InvoiceItemViewModel
                                                                  {
                                                                      Description = resout.Description,
                                                                      Amount = resout.Amount,
                                                                      Category = "Auction",
                                                                      Type = "Auction"
                                                                  }).Distinct().ToList();



                                totalItemsforviewmodel.AddRange(invoiceItemresauctionitems);
                            }

                            #endregion

                            totalItemsforviewmodel.RemoveAll(c => c.Amount <= 0);

                            foreach (var item in totalItemsforviewmodel)
                            {
                                item.Amount = Math.Round(item.Amount, 2);

                            }

                            if (totalItemsforviewmodel.Count() > 0)
                            {

                                totalItemsforviewmodel.ForEach(x => x.TariffInformationId = res);

                            }

                            return totalItemsforviewmodel;
                        }





                    }



                }

                double sumvalue = 0;
                long percentagentid = 0;



                //  need 

                //var indexnumbrcount = 0;

                var conainerindex = new List<ContainerIndex>();

                //var containerslist = (from containerindexdata in Db.ContainerIndices
                //                      where containerindexdata.VirNo == container.VirNo && containerindexdata.IndexNo == container.IndexNo
                //                      select containerindexdata.ContainerNo).ToList();


                var containerslist = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {container.VirNo} and IndexNo = {container.IndexNo} and IsDeleted = 0").Select(x => x.ContainerNo).ToList();

                var indexcontainersdata = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {container.VirNo} and IndexNo = {container.IndexNo} and IsDeleted = 0").ToList();

                //foreach (var item in containerslist)
                //{
                //    //var data = (from containerindexdata in Db.ContainerIndices
                //    //            where containerindexdata.VirNo == container.VirNo && containerindexdata.ContainerNo == item
                //    //            select containerindexdata).ToList();

                //    var data = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {container.VirNo}  and ContainerNo = {item} and IsDeleted = 0").ToList();

                //    if (data.Count() > 0)
                //    {
                //        conainerindex.AddRange(data);
                //    }

                //}


                //if (conainerindex.Count() > 0)
                //{
                //    indexnumbrcount = conainerindex.Select(x => x.IndexNo).Distinct().Count();

                //}

                #region Non general work start

                if (res > 0)
                {



                    var tariffInfosnongeneral = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                 from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                 join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                 where
                                                  tariffInofrmationTariff.TariffInformationId == res
                                                  //ovias&& tariff.TariffType == "CFS"
                                                  && tariff.IsGeneral == false
                                                  && tariff.IsFixedRate == false
                                                  //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                  //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                  && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                  && (tariff.CBMRate > 0 || tariff.WeightRate > 0)
                                                 &&
                                                    (
                                                    tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                    tariff.TypeOfImplementation == "Arrival" ?
                                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.TypeOfImplementation == "Delivery" ?
                                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.ImplementFrom == null
                                                    )
                                                    &&
                                                    (
                                                    tariff.TypeOfImplementation == "All" ?
                                                    tariff.ImplementTo == null :
                                                    tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.ImplementTo == null
                                                    )
                                                 select new InvoiceItemViewModel
                                                 {
                                                     Description = tariff.TariffHead.Name,
                                                     //Amount = weightOrCBM == 1 ? (tariff.WeightRate * weightConv) : (tariff.CBMRate * calculateCBM),
                                                     Amount = weightOrCBM == 1 ? ((tariff.IsDollerRate ? (tariff.WeightRate * dollerrate) : tariff.WeightRate) * weightConv) : ((tariff.IsDollerRate ? (tariff.CBMRate * dollerrate) : tariff.CBMRate) * calculateCBM),
                                                     TariffId = tariff.TariffId,
                                                     TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                     Type = "NoNGeneral",
                                                     TariffType = "CBM",
                                                     Category = "Tariff"
                                                 }).Distinct().ToList();


                    totalItems.AddRange(tariffInfosnongeneral);




                    var indextariffInfosnongeneral = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                      from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                      join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                      where
                                                       tariffInofrmationTariff.TariffInformationId == res
                                                      //ovias && tariff.TariffType == "CFS"
                                                      && tariff.IsGeneral == false
                                                      && tariff.IsFixedRate == false
                                                      //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                      //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                      && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                      && (tariff.PerIndexRate > 0)
                                                     &&
                                                        (
                                                        tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                        tariff.TypeOfImplementation == "Arrival" ?
                                                        Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.TypeOfImplementation == "Delivery" ?
                                                        Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.ImplementFrom == null
                                                        )
                                                        &&
                                                        (
                                                        tariff.TypeOfImplementation == "All" ?
                                                        tariff.ImplementTo == null :
                                                        tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                        Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                        Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.ImplementTo == null
                                                        )
                                                      select new InvoiceItemViewModel
                                                      {
                                                          Description = tariff.TariffHead.Name,
                                                          //Amount = tariff.PerIndexRate,
                                                          Amount = tariff.IsDollerRate ? (tariff.PerIndexRate * dollerrate) : tariff.PerIndexRate,
                                                          TariffId = tariff.TariffId,
                                                          TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                          Type = "NoNGeneral",
                                                          TariffType = "Index",
                                                          Category = "Tariff"
                                                      }).Distinct().ToList();


                    totalItems.AddRange(indextariffInfosnongeneral);


                    //   var CFSLCLtariffInfosnongeneral = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                    //                                         from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                    //                                         join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                    //                                         where
                    //                                          tariffInofrmationTariff.TariffInformationId == res
                    //                                         && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                    //                                          && (tariff.Rate20 > 0 || tariff.Rate40 > 0 || tariff.Rate45 > 0)
                    //                                         //ovias && tariff.TariffType == "CFSLCL"
                    //                                         && tariff.IsGeneral == false
                    //                                         && tariff.IsFixedRate == false
                    //                                      // && (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                    //                                      // && (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                    //                                         &&
                    //                                          (
                    //                                          tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                    //                                          tariff.TypeOfImplementation == "Arrival" ?
                    //                                          Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                    //                                          tariff.TypeOfImplementation == "Delivery" ?
                    //                                          Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                    //                                          tariff.ImplementFrom == null
                    //                                          )
                    //                                          &&
                    //                                          (
                    //                                          tariff.TypeOfImplementation == "All" ?
                    //                                          tariff.ImplementTo == null :
                    //                                          tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                    //                                          Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                    //                                          tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                    //                                          Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                    //                                          tariff.ImplementTo == null
                    //                                          )

                    //                                      select new InvoiceItemViewModel
                    //                                         {
                    //                                             Description = tariff.TariffHead.Name,
                    //                                             Amount = container.Size == 20 ? tariff.Rate20 / indexnumbrcount : container.Size == 40 ? tariff.Rate40 / indexnumbrcount : container.Size == 45 ? tariff.Rate45 / indexnumbrcount : 0,
                    //                                             //Amount =  container.Size == 20 ? ((tariff.IsDollerRate ? ( tariff.Rate20 * Convert.ToInt64(dollerrate)) :  tariff.Rate20) / indexnumbrcount) : container.Size == 40 ? ( (tariff.IsDollerRate ? ( tariff.Rate40 * Convert.ToInt64(dollerrate)) : (double)tariff.Rate40) / indexnumbrcount) : container.Size == 45 ? ( (tariff.IsDollerRate ? ( tariff.Rate45 * Convert.ToInt64(dollerrate)) :  tariff.Rate45) / indexnumbrcount) : 0,
                    //                                             TariffId = tariff.TariffId,
                    //                                             TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                    //                                             Type = "NoNGeneral",
                    //                                             TariffType = "Index"

                    //                                         }).Distinct().ToList();


                    //totalItems.AddRange(CFSLCLtariffInfosnongeneral);


                    foreach (var item in indexcontainersdata)
                    {
                        var countofindexes = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {item.VirNo}  and ContainerNo = {item.ContainerNo} and IsDeleted = 0").ToList().Select(x => x.IndexNo).Distinct().Count();


                        var CFSLCLtariffInfosnongeneral = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                           from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                           join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                           where
                                                            tariffInofrmationTariff.TariffInformationId == res
                                                           && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                            && (tariff.Rate20 > 0 || tariff.Rate40 > 0 || tariff.Rate45 > 0)
                                                           //ovias && tariff.TariffType == "CFSLCL"
                                                           && tariff.IsGeneral == false
                                                           && tariff.IsFixedRate == false
                                                           // && (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                           // && (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                           &&
                                                            (
                                                            tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                            tariff.TypeOfImplementation == "Arrival" ?
                                                            Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                            tariff.TypeOfImplementation == "Delivery" ?
                                                            Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                            tariff.ImplementFrom == null
                                                            )
                                                            &&
                                                            (
                                                            tariff.TypeOfImplementation == "All" ?
                                                            tariff.ImplementTo == null :
                                                            tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                            Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                            tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                            Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                            tariff.ImplementTo == null
                                                            )

                                                           select new InvoiceItemViewModel
                                                           {
                                                               //pak suzuki sy phly container.Size chl rha tha or forech ky elwa kam chl rha tha
                                                               Description = tariff.TariffHead.Name,
                                                               Amount = item.Size == 20 ? tariff.Rate20 / countofindexes : item.Size == 40 ? tariff.Rate40 / countofindexes : item.Size == 45 ? tariff.Rate45 / countofindexes : 0,
                                                               //Amount =  container.Size == 20 ? ((tariff.IsDollerRate ? ( tariff.Rate20 * Convert.ToInt64(dollerrate)) :  tariff.Rate20) / indexnumbrcount) : container.Size == 40 ? ( (tariff.IsDollerRate ? ( tariff.Rate40 * Convert.ToInt64(dollerrate)) : (double)tariff.Rate40) / indexnumbrcount) : container.Size == 45 ? ( (tariff.IsDollerRate ? ( tariff.Rate45 * Convert.ToInt64(dollerrate)) :  tariff.Rate45) / indexnumbrcount) : 0,
                                                               TariffId = tariff.TariffId,
                                                               TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                               Type = "NoNGeneral",
                                                               TariffType = "Index",
                                                               Category = "Tariff"

                                                           }).Distinct().ToList();


                        totalItems.AddRange(CFSLCLtariffInfosnongeneral);
                    }


                    if (res > 0)
                    {
                        foreach (var indexdetail in indexcontainersdata)
                        {
                            var noofcontainers = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {indexdetail.VirNo}   and ContainerNo = {indexdetail.ContainerNo} and IsDeleted = 0").Select(x => x.IndexNo).Distinct().ToList();

                            if (noofcontainers.Count() > 0)
                            {
                                var indextariffInfosdevidedindexs = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                                     from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                                     join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                                     where
                                                                     tariffInofrmationTariff.TariffInformationId == res
                                                                     //ovias&&  tariff.TariffType == "CFS"
                                                                     && tariff.IsGeneral == false
                                                                     && tariff.IsFixedRate == false
                                                                    //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                                    //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                                    && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                                     && tariff.DevededIndexAmount > 0
                                                                   &&
                                                                    (
                                                                    tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                                    tariff.TypeOfImplementation == "Arrival" ?
                                                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                    tariff.TypeOfImplementation == "Delivery" ?
                                                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                    tariff.ImplementFrom == null
                                                                    )
                                                                    &&
                                                                    (
                                                                    tariff.TypeOfImplementation == "All" ?
                                                                    tariff.ImplementTo == null :
                                                                    tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                    tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                    tariff.ImplementTo == null
                                                                    )
                                                                     select new InvoiceItemViewModel
                                                                     {
                                                                         Description = tariff.TariffHead.Name,
                                                                         //Amount = (Convert.ToDouble( tariff.DevededIndexAmount) / containerCount.Count()),
                                                                         Amount = ((tariff.IsDollerRate ? (tariff.DevededIndexAmount * dollerrate) : tariff.DevededIndexAmount) / noofcontainers.Count()),
                                                                         TariffId = tariff.TariffId,
                                                                         Type = "NoNGeneral",
                                                                         TariffType = "Index",
                                                                         Category = "Tariff"

                                                                     }).Distinct().ToList();


                                totalItems.AddRange(indextariffInfosdevidedindexs);
                            }
                        }
                    }




                    //if (indexcargotype == "LCL")
                    //{


                    //    if (container.PortAndTerminalId != null)
                    //    {

                    //        if (container.ShippingAgentId != null)
                    //        {
                    //            //var shipingagent = (from shipagnt in Db.ShippingAgents
                    //            //                    where shipagnt.ShippingAgentId == container.ShippingAgentId
                    //            //                    select shipagnt).LastOrDefault();

                    //            var shipingagent = Db.ShippingAgents.FromSql($"SELECT * From ShippingAgent Where ShippingAgentId = {container.ShippingAgentId} and IsDeleted = 0").LastOrDefault();



                    //            if (!shipingagent.Name.Contains("MAERSK"))
                    //            {
                    //                var portandterminal = (from portandtermil in Db.PortAndTerminals
                    //                                       where
                    //                                       (portandtermil.PortAndTerminalId == container.PortAndTerminalId)
                    //                                       && (portandtermil.RateAmount20 > 0 || portandtermil.RateAmount40 > 0 || portandtermil.RateAmount45 > 0)

                    //                                       select new InvoiceItemViewModel
                    //                                       {
                    //                                           Description = "Port TP Charges",
                    //                                           //Amount = container.Size == 20 ? (tariff.Rate20 / noofindees.Count()) : container.Size == 40 ? (tariff.Rate40 / noofindees.Count()) : container.Size == 45 ? (tariff.Rate45 / noofindees.Count()) : 0,
                    //                                           Amount = container.Size == 20 ? portandtermil.RateAmount20 / indexnumbrcount : container.Size == 40 ? portandtermil.RateAmount40 / indexnumbrcount : container.Size == 45 ? portandtermil.RateAmount45 / indexnumbrcount : 0,
                    //                                           TariffId = portandtermil.PortAndTerminalId,
                    //                                           Type = "NoNGeneral",
                    //                                           TariffType = "Index"

                    //                                       }).LastOrDefault();

                    //                if (portandterminal != null)
                    //                {
                    //                    totalItems.Add(portandterminal);
                    //                }
                    //            }

                    //        }



                    //    }

                    //}


                    #region for lcl fixed rates


                    var tariffInfosnongeneralfixedrates = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                           from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                           join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                           where
                                                            tariffInofrmationTariff.TariffInformationId == res
                                                            //ovias&& tariff.TariffType == "CFS"
                                                            && tariff.IsGeneral == false
                                                            && tariff.IsFixedRate == true
                                                            //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                            //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                            && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                            && (tariff.CBMRate > 0 || tariff.WeightRate > 0)
                                                            &&
                                                                  (
                                                                  tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                                  tariff.TypeOfImplementation == "Arrival" ?
                                                                  Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                  tariff.TypeOfImplementation == "Delivery" ?
                                                                  Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                  tariff.ImplementFrom == null
                                                                  )
                                                                  &&
                                                                  (
                                                                  tariff.TypeOfImplementation == "All" ?
                                                                  tariff.ImplementTo == null :
                                                                  tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                                  Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                  tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                                  Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                  tariff.ImplementTo == null
                                                                  )

                                                           select new InvoiceItemViewModel
                                                           {
                                                               Description = tariff.TariffHead.Name,
                                                               //Amount = weightOrCBM == 1 ? (tariff.WeightRate * weightConv) : (tariff.CBMRate * calculateCBM),
                                                               Amount = weightOrCBM == 1 ? ((tariff.IsDollerRate ? (tariff.WeightRate * dollerrate) : tariff.WeightRate) * weightConv) : ((tariff.IsDollerRate ? (tariff.CBMRate * dollerrate) : tariff.CBMRate) * calculateCBM),
                                                               TariffId = tariff.TariffId,
                                                               TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                               Type = "NoNGeneral",
                                                               TariffType = "CBM",
                                                               Category = "Tariff"
                                                           }).Distinct().ToList();


                    totalItemsFixedRate.AddRange(tariffInfosnongeneralfixedrates);




                    var indextariffInfosnongeneralfixedrates = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                                from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                                join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                                where
                                                                 tariffInofrmationTariff.TariffInformationId == res
                                                                //ovias && tariff.TariffType == "CFS"
                                                                && tariff.IsGeneral == false
                                                                && tariff.IsFixedRate == true
                                                                //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                                //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                                && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                                && tariff.PerIndexRate > 0
                                                                &&
                                                                  (
                                                                  tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                                  tariff.TypeOfImplementation == "Arrival" ?
                                                                  Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                  tariff.TypeOfImplementation == "Delivery" ?
                                                                  Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                  tariff.ImplementFrom == null
                                                                  )
                                                                  &&
                                                                  (
                                                                  tariff.TypeOfImplementation == "All" ?
                                                                  tariff.ImplementTo == null :
                                                                  tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                                  Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                  tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                                  Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                  tariff.ImplementTo == null
                                                                  )
                                                                select new InvoiceItemViewModel
                                                                {
                                                                    Description = tariff.TariffHead.Name,
                                                                    //Amount = tariff.PerIndexRate,
                                                                    Amount = tariff.IsDollerRate ? (tariff.PerIndexRate * dollerrate) : tariff.PerIndexRate,
                                                                    TariffId = tariff.TariffId,
                                                                    TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                                    Type = "NoNGeneral",
                                                                    TariffType = "Index",
                                                                    Category = "Tariff"
                                                                }).Distinct().ToList();


                    totalItemsFixedRate.AddRange(indextariffInfosnongeneralfixedrates);


                    //var CFSLCLtariffInfosnongeneralfixedrates = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                    //                                   from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                    //                                   join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                    //                                   where
                    //                                    tariffInofrmationTariff.TariffInformationId == res
                    //                                   && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                    //                                    && (tariff.Rate20 > 0 || tariff.Rate40 > 0 || tariff.Rate45 > 0)
                    //                                   //ovias && tariff.TariffType == "CFSLCL"
                    //                                   && tariff.IsGeneral == false
                    //                                   && tariff.IsFixedRate == true
                    //                                   // && (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                    //                                   // && (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                    //                                   &&
                    //                                    (
                    //                                    tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                    //                                    tariff.TypeOfImplementation == "Arrival" ?
                    //                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                    //                                    tariff.TypeOfImplementation == "Delivery" ?
                    //                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                    //                                    tariff.ImplementFrom == null
                    //                                    )
                    //                                    &&
                    //                                    (
                    //                                    tariff.TypeOfImplementation == "All" ?
                    //                                    tariff.ImplementTo == null :
                    //                                    tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                    //                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                    //                                    tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                    //                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                    //                                    tariff.ImplementTo == null
                    //                                    )

                    //                                             select new InvoiceItemViewModel
                    //                                   {
                    //                                       Description = tariff.TariffHead.Name,
                    //                                       Amount = container.Size == 20 ? tariff.Rate20 / indexnumbrcount : container.Size == 40 ? tariff.Rate40 / indexnumbrcount : container.Size == 45 ? tariff.Rate45 / indexnumbrcount : 0,
                    //                                       //Amount =  container.Size == 20 ? ((tariff.IsDollerRate ? ( tariff.Rate20 * Convert.ToInt64(dollerrate)) :  tariff.Rate20) / indexnumbrcount) : container.Size == 40 ? ( (tariff.IsDollerRate ? ( tariff.Rate40 * Convert.ToInt64(dollerrate)) : (double)tariff.Rate40) / indexnumbrcount) : container.Size == 45 ? ( (tariff.IsDollerRate ? ( tariff.Rate45 * Convert.ToInt64(dollerrate)) :  tariff.Rate45) / indexnumbrcount) : 0,
                    //                                       TariffId = tariff.TariffId,
                    //                                       TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                    //                                       Type = "NoNGeneral",
                    //                                       TariffType = "Index"

                    //                                   }).Distinct().ToList();
                    //totalItemsFixedRate.AddRange(CFSLCLtariffInfosnongeneralfixedrates);


                    foreach (var item in indexcontainersdata)
                    {
                        var countofindexes = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {item.VirNo}  and ContainerNo = {item.ContainerNo} and IsDeleted = 0").ToList().Select(x => x.IndexNo).Distinct().Count();


                        var CFSLCLtariffInfosnongeneralfixedrates = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                                     from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                                     join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                                     where
                                                                      tariffInofrmationTariff.TariffInformationId == res
                                                                     && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                                      && (tariff.Rate20 > 0 || tariff.Rate40 > 0 || tariff.Rate45 > 0)
                                                                     //ovias && tariff.TariffType == "CFSLCL"
                                                                     && tariff.IsGeneral == false
                                                                     && tariff.IsFixedRate == true
                                                                     // && (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                                     // && (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                                     &&
                                                                      (
                                                                      tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                                      tariff.TypeOfImplementation == "Arrival" ?
                                                                      Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                      tariff.TypeOfImplementation == "Delivery" ?
                                                                      Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                      tariff.ImplementFrom == null
                                                                      )
                                                                      &&
                                                                      (
                                                                      tariff.TypeOfImplementation == "All" ?
                                                                      tariff.ImplementTo == null :
                                                                      tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                                      Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                      tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                                      Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                      tariff.ImplementTo == null
                                                                      )

                                                                     select new InvoiceItemViewModel
                                                                     {
                                                                         //pak suzuki sy phly container.Size chl rha tha or forech ky elwa kam chl rha tha
                                                                         Description = tariff.TariffHead.Name,
                                                                         Amount = item.Size == 20 ? tariff.Rate20 / countofindexes : item.Size == 40 ? tariff.Rate40 / countofindexes : item.Size == 45 ? tariff.Rate45 / countofindexes : 0,
                                                                         //Amount =  container.Size == 20 ? ((tariff.IsDollerRate ? ( tariff.Rate20 * Convert.ToInt64(dollerrate)) :  tariff.Rate20) / indexnumbrcount) : container.Size == 40 ? ( (tariff.IsDollerRate ? ( tariff.Rate40 * Convert.ToInt64(dollerrate)) : (double)tariff.Rate40) / indexnumbrcount) : container.Size == 45 ? ( (tariff.IsDollerRate ? ( tariff.Rate45 * Convert.ToInt64(dollerrate)) :  tariff.Rate45) / indexnumbrcount) : 0,
                                                                         TariffId = tariff.TariffId,
                                                                         TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                                         Type = "NoNGeneral",
                                                                         TariffType = "Index",
                                                                         Category = "Tariff"

                                                                     }).Distinct().ToList();

                        totalItemsFixedRate.AddRange(CFSLCLtariffInfosnongeneralfixedrates);

                    }


                    if (res > 0)
                    {
                        foreach (var indexdetail in indexcontainersdata)
                        {
                            var noofcontainers = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {indexdetail.VirNo}   and ContainerNo = {indexdetail.ContainerNo} and IsDeleted = 0").Select(x => x.IndexNo).Distinct().ToList();

                            if (noofcontainers.Count() > 0)
                            {
                                var indextariffInfosdevidedindexsfixedrates = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                                               from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                                               join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                                               where
                                                                               tariffInofrmationTariff.TariffInformationId == res
                                                                               //ovias&&  tariff.TariffType == "CFS"
                                                                               && tariff.IsGeneral == false
                                                                               && tariff.IsFixedRate == true
                                                                              //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                                              //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                                              && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                                               && tariff.DevededIndexAmount > 0
                                                                              &&
                                                                              (
                                                                              tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                                              tariff.TypeOfImplementation == "Arrival" ?
                                                                              Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                              tariff.TypeOfImplementation == "Delivery" ?
                                                                              Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                                              tariff.ImplementFrom == null
                                                                              )
                                                                              &&
                                                                              (
                                                                              tariff.TypeOfImplementation == "All" ?
                                                                              tariff.ImplementTo == null :
                                                                              tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                                              Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                              tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                                              Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                                              tariff.ImplementTo == null
                                                                              )
                                                                               select new InvoiceItemViewModel
                                                                               {
                                                                                   Description = tariff.TariffHead.Name,
                                                                                   //Amount = (Convert.ToDouble( tariff.DevededIndexAmount) / containerCount.Count()),
                                                                                   Amount = ((tariff.IsDollerRate ? (tariff.DevededIndexAmount * dollerrate) : tariff.DevededIndexAmount) / noofcontainers.Count()),
                                                                                   TariffId = tariff.TariffId,
                                                                                   Type = "NoNGeneral",
                                                                                   TariffType = "Index",
                                                                                   Category = "Tariff"

                                                                               }).Distinct().ToList();


                                totalItemsFixedRate.AddRange(indextariffInfosdevidedindexsfixedrates);

                            }
                        }
                    }



                    totalItemsFixedRate.RemoveAll(c => c.Amount <= 0);

                    #endregion





                }





                #endregion


                totalItems.RemoveAll(c => c.Amount <= 0);


                #region general head work start

                var aictindextariff = Db.AICTAndLineIndexTariffs.FromSql($"SELECT * From AICTAndLineIndexTariff Where VirNumber = {container.VirNo} and IndexNumber = {container.IndexNo} and IsDeleted = 0 and Status = 'UnDelivered' ").LastOrDefault();

                if (aictindextariff != null)
                {
                    if (aictindextariff.TotalCBMRate != 0)
                    {
                        indexcbmcharge = weightOrCBM == 1 ? (aictindextariff.TotalCBMRate * weightConv) : (aictindextariff.TotalCBMRate * calculateCBM);
                    }
                    if (aictindextariff.TotalPerIndexRate != 0)
                    {
                        indexperindexcharge = aictindextariff.TotalPerIndexRate;
                    }

                    totalcbmperindexcharge = indexcbmcharge + indexperindexcharge;


                }
                else
                {
                    var sharerete = GetTerminalAndFFShareRate(container.ShippingAgentId, container.GoodsHeadId, indexcargotype);

                    if (sharerete != null)
                    {
                        if (sharerete.TotalAICTCBMRate != 0)
                        {
                            indexcbmcharge = weightOrCBM == 1 ? (sharerete.TotalAICTCBMRate * weightConv) : (sharerete.TotalAICTCBMRate * calculateCBM);
                        }
                        if (sharerete.TotalAICTPerIndexRate != 0)
                        {
                            indexperindexcharge = sharerete.TotalAICTPerIndexRate;
                        }

                        totalcbmperindexcharge = indexcbmcharge + indexperindexcharge;
                    }

                }


                var totalnongenralamount = totalItems.Sum(x => x.Amount);


                var aftertotal = totalcbmperindexcharge - totalnongenralamount;



                if (aftertotal < 2000 && aftertotal > 0)
                {
                    var shiffintingcharges = new InvoiceItemViewModel
                    {
                        Description = "Cargo Handling",
                        Amount = aftertotal,
                        TariffId = 111,
                        Type = "NoNGeneral",
                        Category = "Tariff"
                    };

                    newtotalItems.Add(shiffintingcharges);

                    newtotalItems.AddRange(totalItems);
                }


                if (aftertotal >= 2000)
                {

                    if (res > 0)
                    {

                        var GeneralPerIndexRate = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                   from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                   join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                   where
                                                   tariffInofrmationTariff.TariffInformationId == res
                                                   && tariff.IsGeneral == true
                                                   && tariff.IsFixedRate == false
                                                   // && (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                   //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                   && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                   //&& tariff.PerIndexRate > 0
                                                  &&
                                                        (
                                                        tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                        tariff.TypeOfImplementation == "Arrival" ?
                                                        Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.TypeOfImplementation == "Delivery" ?
                                                        Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.ImplementFrom == null
                                                        )
                                                        &&
                                                        (
                                                        tariff.TypeOfImplementation == "All" ?
                                                        tariff.ImplementTo == null :
                                                        tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                        Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                        Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.ImplementTo == null
                                                        )

                                                   select new InvoiceItemViewModel
                                                   {
                                                       Description = tariff.TariffHead.Name,
                                                       //Amount =   tariff.PerIndexRate,
                                                       //Amount = tariff.IsDollerRate ? (tariff.PerIndexRate * Convert.ToInt64(dollerrate)) : tariff.PerIndexRate,
                                                       Amount = 0,
                                                       TariffId = tariff.TariffId,
                                                       TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                       Category = "Tariff"
                                                   }).LastOrDefault();




                        if (GeneralPerIndexRate != null)
                        {
                            var percenttariftotal = (from tariffPercentage in Db.TariffPercentages
                                                     join tariffHead in Db.TariffHeads on tariffPercentage.TariffHeadId equals tariffHead.TariffHeadId
                                                     where
                                                      tariffPercentage.ShippingAgentId == GeneralPerIndexRate.TariffPercentId
                                                     //&&
                                                     //  tariffPercentage.TariffPercentType == "WITHPERCENT"
                                                     select new InvoiceItemViewModel
                                                     {
                                                         Description = tariffHead.Name,
                                                         Amount = (aftertotal / 100) * (tariffPercentage.RateOnPersent),
                                                         TariffId = tariffPercentage.TariffPercentageId,
                                                         Type = "General",
                                                         Category = "Tariff"
                                                     }).Distinct().ToList();

                            newtotalItems.AddRange(percenttariftotal);

                        }

                    }

                    newtotalItems.AddRange(totalItems);

                }



                if (aftertotal == 0)
                {
                    newtotalItems.AddRange(totalItems);
                }


                if (aftertotal < 0)
                {

                    aftertotal = aftertotal + totalnongenralamount;

                    if (aftertotal > 0)
                    {

                        var totlcbm = totalItems.Where(x => x.TariffType == "CBM").Sum(x => x.Amount);

                        var totlindx = totalItems.Where(x => x.TariffType == "Index").Sum(x => x.Amount);

                        var totalcbmindex = totlcbm + totlindx;

                        if (aftertotal == totalcbmindex)
                        {
                            newtotalItems.AddRange(totalItems);
                        }

                        else if (aftertotal > totalcbmindex)
                        {
                            var remaring = aftertotal - totalcbmindex;

                            var shiffintingcharges = new InvoiceItemViewModel
                            {
                                Description = "Cargo Handling",
                                Amount = remaring,
                                TariffId = 111,
                                Type = "NoNGeneral",
                                Category = "Tariff"
                            };

                            newtotalItems.Add(shiffintingcharges);

                            newtotalItems.AddRange(totalItems);
                        }

                        else if (aftertotal < totalcbmindex && aftertotal >= totlcbm)
                        {
                            var remaring = aftertotal - totlcbm;

                            if (remaring > 0)
                            {
                                var shiffintingcharges = new InvoiceItemViewModel
                                {
                                    Description = "Cargo Handling",
                                    Amount = remaring,
                                    TariffId = 111,
                                    Type = "NoNGeneral",
                                    Category = "Tariff"
                                };

                                newtotalItems.Add(shiffintingcharges);
                            }

                            var cbmtariff = totalItems.Where(x => x.TariffType == "CBM");

                            newtotalItems.AddRange(cbmtariff);

                        }

                        else if (aftertotal < totalcbmindex && aftertotal >= totlindx)
                        {
                            var remaring = aftertotal - totlindx;

                            if (remaring > 0)
                            {
                                var shiffintingcharges = new InvoiceItemViewModel
                                {
                                    Description = "Cargo Handling",
                                    Amount = remaring,
                                    TariffId = 111,
                                    Type = "NoNGeneral",
                                    Category = "Tariff"
                                };

                                newtotalItems.Add(shiffintingcharges);
                            }

                            var cbmtariff = totalItems.Where(x => x.TariffType == "Index");

                            newtotalItems.AddRange(cbmtariff);

                        }


                        else
                        {

                            var shiffintingcharges = new InvoiceItemViewModel
                            {
                                Description = "Cargo Handling",
                                Amount = aftertotal,
                                TariffId = 111,
                                Type = "NoNGeneral",
                                Category = "Tariff"
                            };

                            newtotalItems.Add(shiffintingcharges);
                        }


                    }


                    //var shiffintingcharges = new InvoiceItemViewModel
                    //{
                    //    Description = "Shifting Charges",
                    //    Amount = aftertotal,
                    //    TariffId = 111,
                    //    Type = "NoNGeneral"
                    //};
                    //newtotalItems.Add(shiffintingcharges);

                }

                #endregion


                if (container != null)
                {
                    var portchrges = new InvoiceItemViewModel
                    {
                        Description = "Port Charges",
                        Amount = Db.PortCharges.Where(s => s.VirNumber == container.VirNo && s.IndexNumber == container.IndexNo).Sum(s => s.TotalCharges),
                        TariffId = container.ContainerIndexId,
                        Type = "NoNGeneral",
                        Category = "PortCharges"
                    };

                    newtotalItems.Add(portchrges);

                }


                #region special handling charges




                if (container != null && shippingagent != null)
                {

                    if (shippingagent.AllowSpecialChargeLCL == "No")
                    {


                        var specialheandingcharges = (from OtherChargeAssigning in Db.ChargeAssignings
                                                      where OtherChargeAssigning.ContainerIndexId == container.ContainerIndexId
                                                      select new InvoiceItemViewModel
                                                      {
                                                          Description = OtherChargeAssigning.ChargeName,
                                                          Amount = OtherChargeAssigning.ChargeAmount,
                                                          TariffId = OtherChargeAssigning.OtherChargeAssigningId,
                                                          TariffType = "AssignCharges",
                                                          Type = "NoNGeneral",
                                                          Category = "SpecialHandlingCharges"
                                                      }).Distinct().ToList();

                        if (specialheandingcharges.Count() > 0)
                        {
                            newtotalItems.AddRange(specialheandingcharges);
                        }

                    }

                }

                #endregion

                #region auciton work current invoice


                var isauction = GetAuctionInfo("CFS", container.VirNo, container.IndexNo ?? 0);

                if (isauction == true)
                {
                    var handlingamount = 0.00;
                    var Weighmentamount = 0.00;


                    handlingamount = weightOrCBM == 1 ? (auctionamountinfo.Weight * weightConv) : (auctionamountinfo.CBM * CBM);

                    if (weightOrCBM == 1)
                    {
                        Weighmentamount = auctionamountinfo.Weight;
                    }

                    if (handlingamount > 0)
                    {
                        var aucitonamountHandingCharges = new InvoiceItemViewModel
                        {
                            Description = "AuctionHandingCharges",
                            Amount = handlingamount,
                            TariffId = auctionamountinfo.AuctionAmountId,
                            TariffType = "Auction",
                            Type = "Auction",
                            Category = "Auction"
                        };
                        newtotalItems.Add(aucitonamountHandingCharges);

                    }
                    if (Weighmentamount > 0)
                    {
                        var aucitonamountHandingCharges = new InvoiceItemViewModel
                        {
                            Description = "AuctionWeightmentCharges",
                            Amount = Weighmentamount,
                            TariffId = auctionamountinfo.AuctionAmountId,
                            TariffType = "Auction",
                            Type = "Auction",
                            Category = "Auction"
                        };
                        newtotalItems.Add(aucitonamountHandingCharges);

                    }

                }

                #endregion


                newtotalItems.AddRange(totalItemsFixedRate);

                newtotalItems.RemoveAll(c => c.Amount <= 0);

                foreach (var item in newtotalItems)
                {
                    item.Amount = Math.Round(item.Amount, 2);

                }

                if (newtotalItems.Count() > 0)
                {
                    foreach (var item in newtotalItems)
                    {
                        item.TariffInformationId = res;

                    }
                }

                return newtotalItems;

            }

            return totalItems;
        }

        public List<InvoiceItemViewModel> GetSizeTotal(string igm, int indexNo, long clearingAgentId, DateTime gateInDate, long containerCount, string indexcargotype, DateTime BillDate)
        {
            //var totalpartcontainers = 0;
            long tariffinfoid = 0;
            long totalgroundingfreedays = 0;
            var datetime = DateTime.Now;

            var auctionamountinfo = (from auctionamt in Db.AuctionAmounts select auctionamt).LastOrDefault();

            var dollerrate = (from rate in Db.ExchangeRates
                              select rate.ExchangeRateAmount).FirstOrDefault();



            var totalItems = new List<InvoiceItemViewModel>();

            var container = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {igm}   and IndexNo = {indexNo} and IsDeleted = 0").FirstOrDefault();

            //var container = (from cycontainer in Db.CYContainers
            //                 where
            //                 cycontainer.VIRNo == igm
            //                 &&
            //                 cycontainer.IndexNo == indexNo
            //                 select cycontainer).FirstOrDefault();


            //if (container != null)
            //{
            //    if (container.IsPartialContainer == true)
            //    {
            //        var partcontainers = (from cycontainer in Db.CYContainers where cycontainer.VIRNo == container.VIRNo && cycontainer.ContainerNo == container.ContainerNo   select cycontainer).ToList().Count();

            //        totalpartcontainers = partcontainers;
            //    }
            //}



            //var containerList = (from cycontainer in Db.CYContainers
            //                     where
            //                     cycontainer.VIRNo == igm
            //                     &&
            //                     cycontainer.IndexNo == indexNo
            //                     select cycontainer).ToList();

            var containerList = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {igm}   and IndexNo = {indexNo} and IsDeleted = 0").ToList();



            var isocode = (from isocodes in Db.ISOCodes
                           where container.ISOCode == isocodes.Code
                           select isocodes).FirstOrDefault();

            if (isocode == null)
            {
                return totalItems;

            }

            if (container != null)
            {
                var resdata = new List<InvoiceItem>();
                var totalItemsforviewmodel = new List<InvoiceItemViewModel>();
                long resfortariffcode = 0;

                var invoiceresdata = Db.Invoices.FromSql($"select * from Invoice Where CYContainerId = {container.CYContainerId} and  IsDeleted = 0 ").FirstOrDefault();

                if (invoiceresdata != null)
                {

                    var isocodeitemm = Db.ISOCodeHeads.FromSql($"select isoCodeHead.ISOCodeHeadId , isoCodeHead.ISOCodeHeadDescription  , isoCodeHead.DeleteDate , isoCodeHead.IsDeleted  from ISOCode   left join isoCodeHead  on ISOCode.ISOCodeHeadId = ISOCodeHead.ISOCodeHeadId  and isoCodeHead.IsDeleted = 0 Where     ISOCode.Code    = {container.ISOCode} and ISOCode.IsDeleted = 0").FirstOrDefault();
                    if (isocodeitemm == null)
                    {
                        return totalItems;
                    }

                    resfortariffcode = GetTariffAllList(container.ShippingAgentId, isocodeitemm.ISOCodeHeadId, container.ConsigneId, clearingAgentId, container.ShipperId, container.GoodsHeadId, container.PortAndTerminalId, indexcargotype);

                    var DeliveredCargogatepass = Db.DOItems.FromSql($"select DOItem.* from GatePass join DOItem  on GatePass.GatePassId = DOItem.GatePassId where GatePass.VirNumber ={container.VIRNo} and  GatePass.IndexNo = {container.IndexNo} and  DOItem.Status = 'F' and DOItem.IsDeleted =0 and GatePass.IsDeleted = 0 ").ToList();

                    if (DeliveredCargogatepass.Count() > 0)
                    {
                        var invoiceItemsres = GetInvoiceItemBycycontainerId(container.CYContainerId).Where(x => x.Category != "SpecialHandlingCharges" && x.Category != "Storage" && x.Category != "SealCharges").ToList();

                        var perviouswaiver = GetPreviousWaiverCY(container.CYContainerId).Where(x => x.Category != "SpecialHandlingCharges" && x.Category != "Storage" && x.Category != "SealCharges").ToList();

                        if (invoiceItemsres.Count() > 0)
                        {
                            foreach (var item in invoiceItemsres)
                            {
                                var result = resdata.Find(t => t.Description == item.Description && t.Category == item.Category);

                                if (result != null)
                                {
                                    result.Amount += item.Amount;
                                }
                                else
                                {
                                    resdata.Add(item);

                                }
                            }

                            if (resdata.Count() > 0)
                            {
                                var GeneralCBMRateWeightRate = (from resout in resdata
                                                                select new InvoiceItemViewModel
                                                                {
                                                                    Description = resout.Description,
                                                                    Amount = resout.Amount,
                                                                    TariffType = "OldInvoiceItem",
                                                                    Category = resout.Category
                                                                }).Distinct().ToList();

                                totalItemsforviewmodel.AddRange(GeneralCBMRateWeightRate);

                            }
                        }

                        if (perviouswaiver.Count() > 0)
                        {
                            var newobjlist = new List<Waiver>();

                            foreach (var item in perviouswaiver)
                            {
                                var result = newobjlist.Find(t => t.Description == item.Description && t.Category == item.Category);

                                if (result != null)
                                {
                                    result.Discount += item.Discount;
                                }
                                else
                                {
                                    newobjlist.Add(item);
                                }
                            }

                            if (newobjlist.Count() > 0)
                            {
                                var waivertariffRates = (from resout in newobjlist
                                                         select new InvoiceItemViewModel
                                                         {
                                                             Description = resout.Description,
                                                             Amount = resout.Discount,
                                                             Category = resout.Category // from "Tariff" to resout.Category due to add aution work flow
                                                         }).Distinct().ToList();

                                if (waivertariffRates.Count() > 0)
                                {
                                    foreach (var item in waivertariffRates)
                                    {
                                        var result = totalItemsforviewmodel.Where(x => x.Category == item.Category && x.Description == item.Description).LastOrDefault();

                                        if (result == null && item.Amount > 0)
                                        {
                                            var irem = new InvoiceItemViewModel
                                            {
                                                Amount = item.Amount,
                                                Category = item.Category,
                                                Description = item.Description
                                            };

                                            totalItemsforviewmodel.Add(irem);
                                        }
                                        else
                                        {
                                            var rasdes = totalItemsforviewmodel.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Amount);
                                            if (rasdes > 0)
                                            {
                                                result.Amount += item.Amount;
                                            }
                                        }
                                    }
                                }

                            }

                        }



                        #region work for special handling
                        if (container != null)
                        {


                            var cycintainer = Db.CYContainers.FromSql($"select * from CYContainer   where  CYContainerId = {container.CYContainerId}   and IsDeleted = 0   ").LastOrDefault();


                            if (cycintainer != null && cycintainer.ShippingAgentId != null)
                            {
                                var shippingagnet = Db.ShippingAgents.FromSql($"select * from ShippingAgent   where  ShippingAgentId = {cycintainer.ShippingAgentId}   and IsDeleted = 0   ").LastOrDefault();

                                if (shippingagnet != null && shippingagnet.AllowSpecialChargeCY == "No")
                                {

                                    var specialheandingcharges = (from OtherChargeAssigning in Db.ChargeAssignings
                                                                  where OtherChargeAssigning.CyContainerId == container.CYContainerId
                                                                  select new InvoiceItemViewModel
                                                                  {
                                                                      Description = OtherChargeAssigning.ChargeName,
                                                                      Amount = OtherChargeAssigning.ChargeAmount,
                                                                      TariffId = OtherChargeAssigning.OtherChargeAssigningId,
                                                                      TariffType = "CYPerIndex",
                                                                      Type = "NoNGeneral",
                                                                      Category = "SpecialHandlingCharges"
                                                                  }).Distinct().ToList();

                                    if (specialheandingcharges.Count() > 0)
                                    {
                                        totalItemsforviewmodel.AddRange(specialheandingcharges);
                                    }

                                }

                            }


                        }

                        #endregion

                        totalItemsforviewmodel.RemoveAll(c => c.Amount <= 0);


                        foreach (var item in totalItemsforviewmodel)
                        {
                            item.Amount = Math.Round(item.Amount, 2);

                        }

                        if (totalItemsforviewmodel.Count() > 0)
                        {

                            totalItemsforviewmodel.ForEach(x => x.TariffInformationId = resfortariffcode);

                        }

                        return totalItemsforviewmodel;


                    }
                    else
                    {

                        var invoiceItemsres = GetInvoiceItemBycycontainerId(container.CYContainerId).Where(x => x.Category == "Tariff").ToList();
                        var perviouswaiver = GetPreviousWaiverCY(container.CYContainerId).Where(x => x.Category == "Tariff").ToList();


                        if (invoiceItemsres.Count() > 0)
                        {
                            foreach (var item in invoiceItemsres)
                            {
                                var result = resdata.Find(t => t.Description == item.Description && t.Category == item.Category);

                                if (result != null)
                                {
                                    result.Amount += item.Amount;
                                }
                                else
                                {
                                    resdata.Add(item);

                                }
                            }

                            if (resdata.Count() > 0)
                            {
                                var GeneralCBMRateWeightRate = (from resout in resdata
                                                                select new InvoiceItemViewModel
                                                                {
                                                                    Description = resout.Description,
                                                                    Amount = resout.Amount,
                                                                    TariffType = "OldInvoiceItem",
                                                                    Category = "Tariff"
                                                                }).Distinct().ToList();



                                if (perviouswaiver.Count() > 0 && GeneralCBMRateWeightRate.Count() > 0)
                                {
                                    foreach (var item in GeneralCBMRateWeightRate)
                                    {
                                        var result = perviouswaiver.Where(x => x.Category == item.Category && x.Description == item.Description).ToList().Sum(x => x.Discount);

                                        if (result > 0)
                                        {
                                            item.Amount += result;
                                        }
                                    }
                                }

                                totalItemsforviewmodel.AddRange(GeneralCBMRateWeightRate);

                            }
                        }


                        if (perviouswaiver.Count() > 0)
                        {
                            var newobjlist = new List<Waiver>();

                            foreach (var item in perviouswaiver)
                            {
                                var result = newobjlist.Find(t => t.Description == item.Description && t.Category == item.Category);

                                if (result != null)
                                {
                                    result.Discount += item.Discount;
                                }
                                else
                                {
                                    newobjlist.Add(item);
                                }
                            }

                            if (newobjlist.Count() > 0)
                            {
                                var waivertariffRates = (from resout in newobjlist
                                                         select new InvoiceItemViewModel
                                                         {
                                                             Description = resout.Description,
                                                             Amount = resout.Discount,
                                                             Category = "Tariff"
                                                         }).Distinct().ToList();

                                if (waivertariffRates.Count() > 0)
                                {
                                    foreach (var item in waivertariffRates)
                                    {
                                        var result = totalItemsforviewmodel.Where(x => x.Category == item.Category && x.Description == item.Description).LastOrDefault();

                                        if (result == null)
                                        {
                                            var irem = new InvoiceItemViewModel
                                            {
                                                Amount = item.Amount,
                                                Category = item.Category,
                                                Description = item.Description
                                            };
                                            totalItemsforviewmodel.Add(irem);
                                        }
                                    }
                                }

                            }
                        }


                        #region work for special handling
                        if (container != null)
                        {


                            var cycintainer = Db.CYContainers.FromSql($"select * from CYContainer   where  CYContainerId = {container.CYContainerId}   and IsDeleted = 0   ").LastOrDefault();


                            if (cycintainer != null && cycintainer.ShippingAgentId != null)
                            {
                                var shippingagnet = Db.ShippingAgents.FromSql($"select * from ShippingAgent   where  ShippingAgentId = {cycintainer.ShippingAgentId}   and IsDeleted = 0   ").LastOrDefault();

                                if (shippingagnet != null && shippingagnet.AllowSpecialChargeCY == "No")
                                {

                                    var specialheandingcharges = (from OtherChargeAssigning in Db.ChargeAssignings
                                                                  where OtherChargeAssigning.CyContainerId == container.CYContainerId
                                                                  select new InvoiceItemViewModel
                                                                  {
                                                                      Description = OtherChargeAssigning.ChargeName,
                                                                      Amount = OtherChargeAssigning.ChargeAmount,
                                                                      TariffId = OtherChargeAssigning.OtherChargeAssigningId,
                                                                      TariffType = "CYPerIndex",
                                                                      Type = "NoNGeneral",
                                                                      Category = "SpecialHandlingCharges"
                                                                  }).Distinct().ToList();

                                    if (specialheandingcharges.Count() > 0)
                                    {
                                        totalItemsforviewmodel.AddRange(specialheandingcharges);
                                    }

                                }

                            }


                        }

                        #endregion

                        #region auciton work previous invoice

                        var invoiceItemsauctionres = GetInvoiceItemBycycontainerId(container.CYContainerId).Where(x => x.Category == "Auction").ToList();

                        if (invoiceItemsauctionres.Count() > 0)
                        {
                            var invoiceItemresauctionitems = (from resout in invoiceItemsauctionres
                                                              select new InvoiceItemViewModel
                                                              {
                                                                  Description = resout.Description,
                                                                  Amount = resout.Amount,
                                                                  Category = "Auction",
                                                                  Type = "Auction"
                                                              }).Distinct().ToList();

                            totalItemsforviewmodel.AddRange(invoiceItemresauctionitems);
                        }

                        #endregion

                        #region examination work


                        if (containerList.Count() > 0)
                        {
                            var examinationcharges = (from examinationChargeAssign in Db.ExaminationChargeAssigns
                                                      where
                                                      examinationChargeAssign.CYContainerId == container.CYContainerId
                                                      &&
                                                      examinationChargeAssign.ChargeAmount20 > 0
                                                      select new InvoiceItemViewModel
                                                      {
                                                          Description = examinationChargeAssign.ChargeName,
                                                          Amount = examinationChargeAssign.ChargeAmount20,
                                                          TariffId = examinationChargeAssign.ExaminationChargeAssignId,
                                                          TariffType = "ExaminationAssignCharges",
                                                          Type = "NoNGeneral",
                                                          Category = "ExaminationCharges"
                                                      }).Distinct().ToList();

                            if (examinationcharges.Count() > 0)
                            {
                                totalItemsforviewmodel.AddRange(examinationcharges);
                            }

                            var examinationcharges40 = (from examinationChargeAssign in Db.ExaminationChargeAssigns
                                                        where
                                                        examinationChargeAssign.CYContainerId == container.CYContainerId
                                                        &&
                                                        examinationChargeAssign.ChargeAmount40 > 0
                                                        select new InvoiceItemViewModel
                                                        {
                                                            Description = examinationChargeAssign.ChargeName,
                                                            Amount = examinationChargeAssign.ChargeAmount40,
                                                            TariffId = examinationChargeAssign.ExaminationChargeAssignId,
                                                            TariffType = "ExaminationAssignCharges",
                                                            Type = "NoNGeneral",
                                                            Category = "ExaminationCharges"
                                                        }).Distinct().ToList();

                            if (examinationcharges40.Count() > 0)
                            {
                                totalItemsforviewmodel.AddRange(examinationcharges40);
                            }


                            var examinationcharges45 = (from examinationChargeAssign in Db.ExaminationChargeAssigns
                                                        where
                                                        examinationChargeAssign.CYContainerId == container.CYContainerId
                                                        &&
                                                        examinationChargeAssign.ChargeAmount45 > 0
                                                        select new InvoiceItemViewModel
                                                        {
                                                            Description = examinationChargeAssign.ChargeName,
                                                            Amount = examinationChargeAssign.ChargeAmount45,
                                                            TariffId = examinationChargeAssign.ExaminationChargeAssignId,
                                                            TariffType = "ExaminationAssignCharges",
                                                            Type = "NoNGeneral",
                                                            Category = "ExaminationCharges"
                                                        }).Distinct().ToList();

                            if (examinationcharges45.Count() > 0)
                            {
                                totalItemsforviewmodel.AddRange(examinationcharges45);
                            }

                        }


                        #endregion

                        #region port charges rates

                        var portchrges = new InvoiceItemViewModel
                        {
                            Description = "Port Charges",
                            Amount = Db.PortCharges.Where(s => s.VirNumber == container.VIRNo && s.IndexNumber == container.IndexNo).Sum(s => s.TotalCharges),
                            TariffId = container.CYContainerId,
                            Type = "NoNGeneral",
                            TariffType = "CYPerIndex",
                            Category = "PortCharges"
                        };

                        totalItemsforviewmodel.Add(portchrges);

                        #endregion


                        #region Examination Tariff Rates



                        #endregion


                        totalItemsforviewmodel.RemoveAll(c => c.Amount <= 0);

                        //if (totalpartcontainers > 0)
                        //{
                        //    foreach (var item in totalItemsforviewmodel.Where(x => x.TariffType != "ExaminationAssignCharges" && x.TariffType != "OldInvoiceItem" && x.TariffType != "CYPerIndex"))
                        //    {
                        //        item.Amount = (item.Amount / totalpartcontainers);

                        //    }
                        //}


                        foreach (var item in totalItemsforviewmodel)
                        {
                            item.Amount = Math.Round(item.Amount, 2);

                        }

                        if (totalItemsforviewmodel.Count() > 0)
                        {

                            totalItemsforviewmodel.ForEach(x => x.TariffInformationId = resfortariffcode);

                        }

                        return totalItemsforviewmodel;
                    }



                }


            }


            var newitemlist = new List<InvoiceItemViewModel>();

            var isocodeitem = Db.ISOCodeHeads.FromSql($"select isoCodeHead.ISOCodeHeadId , isoCodeHead.ISOCodeHeadDescription  , isoCodeHead.DeleteDate , isoCodeHead.IsDeleted  from ISOCode   left join isoCodeHead  on ISOCode.ISOCodeHeadId = ISOCodeHead.ISOCodeHeadId  and isoCodeHead.IsDeleted = 0 Where     ISOCode.Code    = {container.ISOCode} and ISOCode.IsDeleted = 0").FirstOrDefault();


            if (isocodeitem == null)
            {
                return totalItems;
            }

            var res = GetTariffAllList(container.ShippingAgentId, isocodeitem.ISOCodeHeadId, container.ConsigneId, clearingAgentId, container.ShipperId, container.GoodsHeadId, container.PortAndTerminalId, indexcargotype);

            if (res > 0)
            {
                foreach (var containeritem in containerList)
                {



                    var tarifList = new List<Tariff>();





                    tariffinfoid = res;


                    var tariffInfos = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                       from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                       join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                       where
                                        tariffInofrmationTariff.TariffInformationId == res
                                       && (tariff.Rate20 > 0 || tariff.Rate40 > 0 || tariff.Rate45 > 0)
                                       //&& tariff.TariffType == "CY"
                                       && tariff.IsGeneral == true
                                       //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                       //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                       && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                       &&
                                        (
                                        tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                        tariff.TypeOfImplementation == "Arrival" ?
                                        Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                        tariff.TypeOfImplementation == "Delivery" ?
                                        Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                        tariff.ImplementFrom == null
                                        )
                                        &&
                                        (
                                        tariff.TypeOfImplementation == "All" ?
                                        tariff.ImplementTo == null :
                                        tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                        Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                        tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                        Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                        tariff.ImplementTo == null
                                        )
                                       select new InvoiceItemViewModel
                                       {
                                           Description = tariff.TariffHead.Name,
                                           // Amount = container.Size == 20 ? tariff.Rate20 : container.Size == 40 ? tariff.Rate40 : container.Size == 45 ? tariff.Rate45 : 0,
                                           Amount = containeritem.Size == 20 ? (tariff.IsDollerRate ? (tariff.Rate20 * dollerrate) : tariff.Rate20) : containeritem.Size == 40 ? (tariff.IsDollerRate ? (tariff.Rate40 * dollerrate) : tariff.Rate40) : containeritem.Size == 45 ? (tariff.IsDollerRate ? (tariff.Rate45 * dollerrate) : tariff.Rate45) : 0,
                                           TariffId = tariff.TariffId,
                                           TariffType = "CY",
                                           TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                           Category = "Tariff"
                                       }).Distinct().ToList();


                    if (containeritem.IsPartialContainer == true)
                    {
                        var noofcontainers = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VirNo = {containeritem.VIRNo}   and ContainerNo = {containeritem.ContainerNo} and IsDeleted = 0").ToList();
                        if (noofcontainers.Count() > 0 && tariffInfos.Count() > 0)
                        {
                            tariffInfos.ForEach(x => x.Amount = x.Amount / noofcontainers.Count());
                        }

                    }

                    newitemlist.AddRange(tariffInfos);







                }

                var tariffInfosCYPerIndex = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                             from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                             join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                             where
                                             (tariff.PerIndexRate > 0)
                                             // && tariff.TariffType == "CY"
                                             && tariff.IsGeneral == true
                                             && tariffInofrmationTariff.TariffInformationId == res
                                             //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                             //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                             && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                             &&
                                                    (
                                                    tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                    tariff.TypeOfImplementation == "Arrival" ?
                                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.TypeOfImplementation == "Delivery" ?
                                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.ImplementFrom == null
                                                    )
                                                    &&
                                                    (
                                                    tariff.TypeOfImplementation == "All" ?
                                                    tariff.ImplementTo == null :
                                                    tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.ImplementTo == null
                                                    )



                                             select new InvoiceItemViewModel
                                             {
                                                 Description = tariff.TariffHead.Name,
                                                 //Amount = tariff.PerIndexRate,
                                                 Amount = tariff.IsDollerRate ? (tariff.PerIndexRate * dollerrate) : tariff.PerIndexRate,
                                                 TariffId = tariff.TariffId,
                                                 TariffType = "CYPerIndex",
                                                 TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                 Category = "Tariff"
                                             }).Distinct().ToList();


                newitemlist.AddRange(tariffInfosCYPerIndex);

            }

            double sumvalue = 0;
            long percentagentid = 0;

            foreach (var item in newitemlist)
            {
                sumvalue += item.Amount;

                percentagentid = item.TariffPercentId;

            }

            //var newitemlistigm = new List<InvoiceItemViewModel>();

            //foreach (var item in containerList)
            //{
            //    var igmgeneralItems = (from igmTariff in Db.ContainerIndexTariffs
            //                           join tariff in Db.Tariffs on igmTariff.TariffId equals tariff.TariffId
            //                           where
            //                           igmTariff.CYContainerId == container.CYContainerId
            //                           && tariff.TariffType == "CY"
            //                           && tariff.IsGeneral == true
            //                           && (tariff.Rate20 > 0 || tariff.Rate40 > 0 || tariff.Rate45 > 0)
            //                           select new InvoiceItemViewModel
            //                           {
            //                               Description = tariff.TariffHead.Name,
            //                               //Amount = container.Size == 20 ? tariff.Rate20 : container.Size == 40 ? tariff.Rate40 : container.Size == 45 ? tariff.Rate45 : 0,
            //                               Amount = item.Size == 20 ? (tariff.IsDollerRate ? (tariff.Rate20 * Convert.ToInt64(dollerrate)) : tariff.Rate20) : item.Size == 40 ? (tariff.IsDollerRate ? (tariff.Rate40 * Convert.ToInt64(dollerrate)) : tariff.Rate40) : item.Size == 45 ? (tariff.IsDollerRate ? (tariff.Rate45 * Convert.ToInt64(dollerrate)) : tariff.Rate45) : 0,
            //                               TariffId = tariff.TariffId,
            //                               TariffType = "CY",
            //                           }).Distinct().ToList();


            //    newitemlistigm.AddRange(igmgeneralItems);
            //}



            //var igmgeneralItemsCYPerIndex = (from igmTariff in Db.ContainerIndexTariffs
            //                                 join tariff in Db.Tariffs on igmTariff.TariffId equals tariff.TariffId
            //                                 where
            //                                 igmTariff.CYContainerId == container.CYContainerId
            //                                 && tariff.TariffType == "CYPerIndex"
            //                                 && tariff.IsGeneral == true
            //                                 && (tariff.PerIndexRate > 0)
            //                                 select new InvoiceItemViewModel
            //                                 {
            //                                     Description = tariff.TariffHead.Name,
            //                                     // Amount = tariff.PerIndexRate,
            //                                     Amount = tariff.IsDollerRate ? (tariff.PerIndexRate * Convert.ToInt64(dollerrate)) : tariff.PerIndexRate,
            //                                     TariffId = tariff.TariffId,
            //                                     TariffType = "CYPerIndex",
            //                                 }).Distinct().ToList();


            //newitemlistigm.AddRange(igmgeneralItemsCYPerIndex);

            //foreach (var item in newitemlistigm)
            //{
            //    sumvalue += item.Amount;

            //}


            if (sumvalue > 0)
            {


                var percenttariftotal = (from tariffPercentage in Db.TariffPercentages
                                         join tariffHead in Db.TariffHeads on tariffPercentage.TariffHeadId equals tariffHead.TariffHeadId
                                         where
                                          tariffPercentage.ShippingAgentId == percentagentid
                                         //&&
                                         //  tariffPercentage.TariffPercentType == "WITHPERCENT"
                                         select new InvoiceItemViewModel
                                         {
                                             Description = tariffHead.Name,
                                             Amount = (sumvalue / 100) * (tariffPercentage.RateOnPersent),
                                             TariffId = tariffPercentage.TariffPercentageId,
                                             Type = "General",
                                             Category = "Tariff"
                                         }).Distinct().ToList();




                totalItems.AddRange(percenttariftotal);
            }

            if (res > 0)
            {
                foreach (var item in containerList)
                {

                    tariffinfoid = res;
                    var tariffInfosnongeneral = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                 from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                 join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                 where
                                                 tariffInofrmationTariff.TariffInformationId == res
                                                 && (tariff.Rate20 > 0 || tariff.Rate40 > 0 || tariff.Rate45 > 0)
                                                 //&& tariff.TariffType == "CY"
                                                 && tariff.IsGeneral == false
                                                 && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                 //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                 //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                 &&
                                                        (
                                                        tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                        tariff.TypeOfImplementation == "Arrival" ?
                                                        Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.TypeOfImplementation == "Delivery" ?
                                                        Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.ImplementFrom == null
                                                        )
                                                        &&
                                                        (
                                                        tariff.TypeOfImplementation == "All" ?
                                                        tariff.ImplementTo == null :
                                                        tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                        Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                        Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.ImplementTo == null
                                                        )
                                                 select new InvoiceItemViewModel
                                                 {
                                                     Description = tariff.TariffHead.Name,
                                                     // Amount = container.Size == 20 ? tariff.Rate20 : container.Size == 40 ? tariff.Rate40 : container.Size == 45 ? tariff.Rate45 : 0,
                                                     Amount = item.Size == 20 ? (tariff.IsDollerRate ? (tariff.Rate20 * dollerrate) : tariff.Rate20) : item.Size == 40 ? (tariff.IsDollerRate ? (tariff.Rate40 * dollerrate) : tariff.Rate40) : item.Size == 45 ? (tariff.IsDollerRate ? (tariff.Rate45 * dollerrate) : tariff.Rate45) : 0,
                                                     TariffId = tariff.TariffId,
                                                     TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                     TariffType = "CY",
                                                     Type = "NoNGeneral",
                                                     Category = "Tariff"
                                                 }).Distinct().ToList();


                    if (item.IsPartialContainer == true)
                    {
                        var noofcontainers = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VirNo = {item.VIRNo}   and ContainerNo = {item.ContainerNo} and IsDeleted = 0").ToList();
                        if (noofcontainers.Count() > 0 && tariffInfosnongeneral.Count() > 0)
                        {
                            tariffInfosnongeneral.ForEach(x => x.Amount = x.Amount / noofcontainers.Count());
                        }

                    }

                    totalItems.AddRange(tariffInfosnongeneral);




                }


                var tariffInfosnongeneralCYPerIndex = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                                       from tariffInfo in Db.TariffInformations.Where(x => x.TariffInformationId == res).DefaultIfEmpty()
                                                       join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                                       where
                                                       tariffInofrmationTariff.TariffInformationId == res

                                                       && (tariff.PerIndexRate > 0)
                                                       // && tariff.TariffType == "CY"
                                                       && tariff.IsGeneral == false
                                                       //&& (tariffInfo.GoodsHeadId == container.GoodsHeadId || tariffInfo.GoodsHeadId == null)
                                                       //&& (tariffInfo.ShipperId == container.ShipperId || tariffInfo.ShipperId == null)
                                                       //&& (tariffInfo.ClearingAgentId == clearingAgentId || tariffInfo.ClearingAgentId == null)
                                                       //&& (tariffInfo.ConsigneId == container.ConsigneId || tariffInfo.ConsigneId == null)
                                                       //&& (tariffInfo.PortAndTerminalId == container.PortAndTerminalId || tariffInfo.PortAndTerminalId == null)
                                                       //&& (tariffInfo.ShippingAgentId == container.ShippingAgentId || tariffInfo.ShippingAgentId == null)
                                                       //&& (tariffInfo.ContainerType == isocodeitem.Descrition || tariffInfo.ContainerType == null)
                                                       //&& (tariff.ImplementFrom != null ? Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementFrom == null)
                                                       //&& (tariff.ImplementTo != null ? Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) : tariff.ImplementTo == null)
                                                       && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                                    &&
                                                    (
                                                    tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                    tariff.TypeOfImplementation == "Arrival" ?
                                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.TypeOfImplementation == "Delivery" ?
                                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.ImplementFrom == null
                                                    )
                                                    &&
                                                    (
                                                    tariff.TypeOfImplementation == "All" ?
                                                    tariff.ImplementTo == null :
                                                    tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                    Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                    Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                    tariff.ImplementTo == null
                                                    )
                                                       select new InvoiceItemViewModel
                                                       {
                                                           Description = tariff.TariffHead.Name,
                                                           //Amount = tariff.PerIndexRate,
                                                           Amount = tariff.IsDollerRate ? (tariff.PerIndexRate * dollerrate) : tariff.PerIndexRate,
                                                           TariffId = tariff.TariffId,
                                                           TariffPercentId = tariffInfo.PercentAgeShippingAgentId,
                                                           TariffType = "CYPerIndex",
                                                           Type = "NoNGeneral",
                                                           Category = "Tariff"
                                                       }).Distinct().ToList();


                totalItems.AddRange(tariffInfosnongeneralCYPerIndex);
                //if (container.PortAndTerminalId != null)
                //{

                //    if (container.ShippingAgentId != null)
                //    {
                //        var shipingagent = (from shipagnt in Db.ShippingAgents
                //                            where shipagnt.ShippingAgentId == container.ShippingAgentId
                //                            select shipagnt).LastOrDefault();

                //        if (!shipingagent.Name.Contains("MAERSK"))
                //        {

                //            var portandterminal = (from portandtermil in Db.PortAndTerminals
                //                                   where
                //                                   (portandtermil.PortAndTerminalId == container.PortAndTerminalId)
                //                                   && (portandtermil.RateAmount20 > 0 || portandtermil.RateAmount40 > 0 || portandtermil.RateAmount45 > 0)

                //                                   select new InvoiceItemViewModel
                //                                   {
                //                                       Description = "Port TP Charges",
                //                                       Amount = item.Size == 20 ? portandtermil.RateAmount20 : item.Size == 40 ? portandtermil.RateAmount40 : item.Size == 45 ? portandtermil.RateAmount45 : 0,
                //                                       //Amount = container.Size == 20 ? portandtermil.RateAmount20 * containerslist.Count() : container.Size == 40 ? portandtermil.RateAmount40 * containerslist.Count() : container.Size == 45 ? portandtermil.RateAmount45 * containerslist.Count() : 0,
                //                                       TariffId = portandtermil.PortAndTerminalId,
                //                                       TariffType = "CY",
                //                                       Type = "NoNGeneral"
                //                                   }).LastOrDefault();

                //            if (portandterminal != null)
                //            {
                //                totalItems.Add(portandterminal);
                //            }
                //        };

                //    }

                //}






                //var igmItems = (from igmTariff in Db.ContainerIndexTariffs
                //                join tariff in Db.Tariffs on igmTariff.TariffId equals tariff.TariffId
                //                where
                //                igmTariff.CYContainerId == container.CYContainerId
                //                && tariff.TariffType == "CY"
                //                && tariff.IsGeneral == false
                //                && (tariff.Rate20 > 0 || tariff.Rate40 > 0 || tariff.Rate45 > 0)
                //                select new InvoiceItemViewModel
                //                {
                //                    Description = tariff.TariffHead.Name,
                //                    //Amount = container.Size == 20 ? tariff.Rate20 : container.Size == 40 ? tariff.Rate40 : container.Size == 45 ? tariff.Rate45 : 0,
                //                    Amount = item.Size == 20 ? (tariff.IsDollerRate ? (tariff.Rate20 * Convert.ToInt64(dollerrate)) : tariff.Rate20) : item.Size == 40 ? (tariff.IsDollerRate ? (tariff.Rate40 * Convert.ToInt64(dollerrate)) : tariff.Rate40) : item.Size == 45 ? (tariff.IsDollerRate ? (tariff.Rate45 * Convert.ToInt64(dollerrate)) : tariff.Rate45) : 0,
                //                    TariffId = tariff.TariffId,
                //                    TariffType = "CY"

                //                }).Distinct().ToList();

                //totalItems.AddRange(igmItems);





            }






            //var igmItemsCYPerIndex = (from igmTariff in Db.ContainerIndexTariffs
            //                          join tariff in Db.Tariffs on igmTariff.TariffId equals tariff.TariffId
            //                          where
            //                          igmTariff.CYContainerId == container.CYContainerId
            //                          && tariff.TariffType == "CYPerIndex"
            //                          && tariff.IsGeneral == false
            //                          && (tariff.PerIndexRate > 0)
            //                          select new InvoiceItemViewModel
            //                          {
            //                              Description = tariff.TariffHead.Name,
            //                              //Amount = tariff.PerIndexRate,
            //                              Amount = tariff.IsDollerRate ? (tariff.PerIndexRate * Convert.ToInt64(dollerrate)) : tariff.PerIndexRate,
            //                              TariffId = tariff.TariffId,
            //                              TariffType = "CYPerIndex"

            //                          }).Distinct().ToList();

            //totalItems.AddRange(igmItemsCYPerIndex);






            //  var newtotalItems = new List<InvoiceItemViewModel>();

            //foreach (var item in totalItems)
            //{
            //    if (item.TariffType == "CY")
            //    {
            //        item.Amount = (item.Amount * containerCount);
            //        newtotalItems.Add(item);
            //    }
            //    else
            //    {
            //        newtotalItems.Add(item);
            //    }
            //}


            if (container != null)
            {


                var cycintainer = Db.CYContainers.FromSql($"select * from CYContainer   where  CYContainerId = {container.CYContainerId}   and IsDeleted = 0   ").LastOrDefault();


                if (cycintainer != null && cycintainer.ShippingAgentId != null)
                {
                    var shippingagnet = Db.ShippingAgents.FromSql($"select * from ShippingAgent   where  ShippingAgentId = {cycintainer.ShippingAgentId}   and IsDeleted = 0   ").LastOrDefault();

                    if (shippingagnet != null && shippingagnet.AllowSpecialChargeCY == "No")
                    {

                        var specialheandingcharges = (from OtherChargeAssigning in Db.ChargeAssignings
                                                      where OtherChargeAssigning.CyContainerId == container.CYContainerId
                                                      select new InvoiceItemViewModel
                                                      {
                                                          Description = OtherChargeAssigning.ChargeName,
                                                          Amount = OtherChargeAssigning.ChargeAmount,
                                                          TariffId = OtherChargeAssigning.OtherChargeAssigningId,
                                                          TariffType = "CYPerIndex",
                                                          Type = "NoNGeneral",
                                                          Category = "SpecialHandlingCharges"
                                                      }).Distinct().ToList();

                        if (specialheandingcharges.Count() > 0)
                        {
                            totalItems.AddRange(specialheandingcharges);
                        }

                    }

                }





            }




            if (containerList.Count() > 0)
            {
                //foreach (var item in containerList)
                //{
                //var examinationcharges = (from examinationChargeAssign in Db.ExaminationChargeAssigns
                //                          where examinationChargeAssign.CYContainerId == container.CYContainerId
                //                          select new InvoiceItemViewModel
                //                          {
                //                              Description = examinationChargeAssign.ChargeName,
                //                              Amount = item.Size == 20 ? examinationChargeAssign.ChargeAmount20 : item.Size == 40 ? examinationChargeAssign.ChargeAmount40 : item.Size == 45 ? examinationChargeAssign.ChargeAmount45 : 0,
                //                              TariffId = examinationChargeAssign.ExaminationChargeAssignId,
                //                              TariffType = "ExaminationAssignCharges",
                //                              Type = "NoNGeneral"
                //                          }).Distinct().ToList();

                //if (examinationcharges.Count() > 0)
                //{
                //    totalItems.AddRange(examinationcharges);
                //}
                //}


                var examinationcharges = (from examinationChargeAssign in Db.ExaminationChargeAssigns
                                          where
                                          examinationChargeAssign.CYContainerId == container.CYContainerId
                                          &&
                                          examinationChargeAssign.ChargeAmount20 > 0
                                          select new InvoiceItemViewModel
                                          {
                                              Description = examinationChargeAssign.ChargeName,
                                              Amount = examinationChargeAssign.ChargeAmount20,
                                              TariffId = examinationChargeAssign.ExaminationChargeAssignId,
                                              TariffType = "ExaminationAssignCharges",
                                              Type = "NoNGeneral",
                                              Category = "ExaminationCharges"
                                          }).Distinct().ToList();

                if (examinationcharges.Count() > 0)
                {
                    totalItems.AddRange(examinationcharges);
                }

                var examinationcharges40 = (from examinationChargeAssign in Db.ExaminationChargeAssigns
                                            where
                                            examinationChargeAssign.CYContainerId == container.CYContainerId
                                            &&
                                            examinationChargeAssign.ChargeAmount40 > 0
                                            select new InvoiceItemViewModel
                                            {
                                                Description = examinationChargeAssign.ChargeName,
                                                Amount = examinationChargeAssign.ChargeAmount40,
                                                TariffId = examinationChargeAssign.ExaminationChargeAssignId,
                                                TariffType = "ExaminationAssignCharges",
                                                Type = "NoNGeneral",
                                                Category = "ExaminationCharges"
                                            }).Distinct().ToList();

                if (examinationcharges40.Count() > 0)
                {
                    totalItems.AddRange(examinationcharges40);
                }


                var examinationcharges45 = (from examinationChargeAssign in Db.ExaminationChargeAssigns
                                            where
                                            examinationChargeAssign.CYContainerId == container.CYContainerId
                                            &&
                                            examinationChargeAssign.ChargeAmount45 > 0
                                            select new InvoiceItemViewModel
                                            {
                                                Description = examinationChargeAssign.ChargeName,
                                                Amount = examinationChargeAssign.ChargeAmount45,
                                                TariffId = examinationChargeAssign.ExaminationChargeAssignId,
                                                TariffType = "ExaminationAssignCharges",
                                                Type = "NoNGeneral",
                                                Category = "ExaminationCharges"
                                            }).Distinct().ToList();

                if (examinationcharges45.Count() > 0)
                {
                    totalItems.AddRange(examinationcharges45);
                }

            }



            #region Examination Charges work flow


            //if (examinationtarifflist.Count() > 0)
            //{

            //    var groundingcharges = examinationtarifflist.Where(x => x.Description.Contains("GROUNDING CHARGES")).LastOrDefault();

            //if (groundingcharges != null)
            //{
            var fressdaysgrounding = GetGroundingFreeDays(container.ShippingAgentId, container.ConsigneId, clearingAgentId, container.GoodsHeadId, "CY");

            totalgroundingfreedays = fressdaysgrounding;
            //  }

            //}




            var ihcdata = new List<IHC>();

            var examinationIHCViewModel = new List<ExaminationIHCViewModel>();

            foreach (var item in containerList)
            {
                var containerihcdetail = new ExaminationIHCViewModel
                {

                    ContainerNumber = item.ContainerNo,
                    CaroType = item.CargoType,
                    EMCount = Db.IHCs.Where(s => s.VIRNumber == item.VIRNo && s.ContainerNumber == item.ContainerNo && s.HandlingCode == "EM").Count(),
                    ESCount = Db.IHCs.Where(s => s.VIRNumber == item.VIRNo && s.ContainerNumber == item.ContainerNo && s.HandlingCode == "ES").Count(),
                    GroundingFreedays = Db.GroundedDays.Where(s => s.CYContainerId == item.CYContainerId).Sum(x => x.Days),
                    Size = item.Size,
                    VirNumber = item.VIRNo,
                };

                if (containerihcdetail.EMCount > 0 || containerihcdetail.ESCount > 0)
                {
                    examinationIHCViewModel.Add(containerihcdetail);
                }

            }

            var examinationtarifflist = new List<InvoiceItemViewModel>();

            if (examinationIHCViewModel.Count() > 0)
            {

                foreach (var item in examinationIHCViewModel)
                {


                    if (item.ESCount > 0)
                    {
                        var resextariff = GetExaminationTariffAllList(container.ShippingAgentId, container.GoodsHeadId, "CY", "ES");

                        if (resextariff > 0)
                        {
                            var examinationsizewise = (from examinationTariffInformationTariff in Db.ExaminationTariffInformationTariffs
                                                       from examinationTariffInformation in Db.ExaminationTariffInformation.Where(x => x.ExaminationTariffInformationId == resextariff).DefaultIfEmpty()
                                                       join examinationTariff in Db.ExaminationTariffs on examinationTariffInformationTariff.ExaminationTariffId equals examinationTariff.ExaminationTariffId
                                                       join examinationCharge in Db.ExaminationCharges on examinationTariff.ExaminationChargeId equals examinationCharge.ExaminationChargeId
                                                       where
                                                       examinationTariffInformationTariff.ExaminationTariffInformationId == resextariff
                                                       && (examinationTariff.Rate20 > 0 || examinationTariff.Rate40 > 0 || examinationTariff.Rate45 > 0)
                                                       select new InvoiceItemViewModel
                                                       {
                                                           Description = examinationCharge.ExaminationChargeName,
                                                           Amount = item.Size == 20 ? (examinationTariff.Rate20 * item.EMCount) : item.Size == 40 ? (examinationTariff.Rate40 * item.EMCount) : item.Size == 45 ? (examinationTariff.Rate45 * item.EMCount) : 0,
                                                           TariffId = examinationTariff.ExaminationTariffId,
                                                           TariffType = "CY",
                                                           Type = "NoNGeneral",
                                                           Category = "ExaminationTariffCharges"
                                                       }).Distinct().ToList();

                            if (item.GroundingFreedays == 0)
                            {
                                examinationsizewise.RemoveAll(x => x.Description.Contains("GROUNDING CHARGES"));
                            }
                            else
                            {

                                if (examinationsizewise.Count() > 0 && item.GroundingFreedays <= totalgroundingfreedays)
                                {
                                    examinationsizewise.RemoveAll(x => x.Description.Contains("GROUNDING CHARGES"));

                                }


                            }
                            if (examinationsizewise.Exists(p => p.Description.Contains("GROUNDING CHARGES")))
                            {

                                var dys = item.GroundingFreedays - totalgroundingfreedays;

                                foreach (var newitem in examinationsizewise)
                                {
                                    if (newitem.Description == "GROUNDING CHARGES")
                                    {
                                        newitem.Amount = newitem.Amount * dys ?? 0;
                                    }
                                }
                            }

                            examinationtarifflist.AddRange(examinationsizewise);

                            var examinationperindex = (from examinationTariffInformationTariff in Db.ExaminationTariffInformationTariffs
                                                       from examinationTariffInformation in Db.ExaminationTariffInformation.Where(x => x.ExaminationTariffInformationId == resextariff).DefaultIfEmpty()
                                                       join examinationTariff in Db.ExaminationTariffs on examinationTariffInformationTariff.ExaminationTariffId equals examinationTariff.ExaminationTariffId
                                                       join examinationCharge in Db.ExaminationCharges on examinationTariff.ExaminationChargeId equals examinationCharge.ExaminationChargeId
                                                       where
                                                       examinationTariffInformationTariff.ExaminationTariffInformationId == resextariff
                                                       && (examinationTariff.PerIndexRate > 0)
                                                       select new InvoiceItemViewModel
                                                       {
                                                           Description = examinationCharge.ExaminationChargeName,
                                                           Amount = examinationTariff.PerIndexRate * item.EMCount,
                                                           TariffId = examinationTariff.ExaminationTariffId,
                                                           TariffType = "CY",
                                                           Type = "NoNGeneral",
                                                           Category = "ExaminationTariffCharges"
                                                       }).Distinct().ToList();
                            if (item.GroundingFreedays == 0)
                            {
                                examinationperindex.RemoveAll(x => x.Description.Contains("GROUNDING CHARGES"));
                            }
                            else
                            {

                                if (examinationperindex.Count() > 0 && item.GroundingFreedays <= totalgroundingfreedays)
                                {
                                    examinationperindex.RemoveAll(x => x.Description.Contains("GROUNDING CHARGES"));

                                }


                            }
                            if (examinationperindex.Exists(p => p.Description.Contains("GROUNDING CHARGES")))
                            {

                                var dys = item.GroundingFreedays - totalgroundingfreedays;

                                foreach (var newitem in examinationperindex)
                                {
                                    if (newitem.Description == "GROUNDING CHARGES")
                                    {
                                        newitem.Amount = newitem.Amount * dys ?? 0;
                                    }
                                }
                            }

                            examinationtarifflist.AddRange(examinationperindex);

                        }
                    }


                    if (item.EMCount > 0)
                    {

                        long emcount = 0;

                        emcount = item.EMCount;

                        if (item.ESCount > 0 && item.EMCount > 0)
                        {
                            emcount -= 1;

                        }

                        if (emcount > 0)
                        {
                            var resextariffEM = GetExaminationTariffAllList(container.ShippingAgentId, container.GoodsHeadId, "CY", "EM");

                            if (resextariffEM > 0)
                            {
                                var examinationsizewise = (from examinationTariffInformationTariff in Db.ExaminationTariffInformationTariffs
                                                           from examinationTariffInformation in Db.ExaminationTariffInformation.Where(x => x.ExaminationTariffInformationId == resextariffEM).DefaultIfEmpty()
                                                           join examinationTariff in Db.ExaminationTariffs on examinationTariffInformationTariff.ExaminationTariffId equals examinationTariff.ExaminationTariffId
                                                           join examinationCharge in Db.ExaminationCharges on examinationTariff.ExaminationChargeId equals examinationCharge.ExaminationChargeId
                                                           where
                                                           examinationTariffInformationTariff.ExaminationTariffInformationId == resextariffEM
                                                           && (examinationTariff.Rate20 > 0 || examinationTariff.Rate40 > 0 || examinationTariff.Rate45 > 0)
                                                           select new InvoiceItemViewModel
                                                           {
                                                               Description = examinationCharge.ExaminationChargeName,
                                                               Amount = item.Size == 20 ? (examinationTariff.Rate20 * emcount) : item.Size == 40 ? (examinationTariff.Rate40 * emcount) : item.Size == 45 ? (examinationTariff.Rate45 * emcount) : 0,
                                                               TariffId = examinationTariff.ExaminationTariffId,
                                                               TariffType = "CY",
                                                               Type = "NoNGeneral",
                                                               Category = "ExaminationTariffCharges"
                                                           }).Distinct().ToList();



                                if (item.GroundingFreedays == 0)
                                {
                                    examinationsizewise.RemoveAll(x => x.Description.Contains("GROUNDING CHARGES"));
                                }
                                else
                                {

                                    if (examinationsizewise.Count() > 0 && item.GroundingFreedays <= totalgroundingfreedays)
                                    {
                                        examinationsizewise.RemoveAll(x => x.Description.Contains("GROUNDING CHARGES"));

                                    }


                                }
                                if (examinationsizewise.Exists(p => p.Description.Contains("GROUNDING CHARGES")))
                                {

                                    var dys = item.GroundingFreedays - totalgroundingfreedays;

                                    foreach (var newitem in examinationsizewise)
                                    {
                                        if (newitem.Description == "GROUNDING CHARGES")
                                        {
                                            newitem.Amount = newitem.Amount * dys ?? 0;
                                        }
                                    }
                                }

                                examinationtarifflist.AddRange(examinationsizewise);

                                var examinationperindex = (from examinationTariffInformationTariff in Db.ExaminationTariffInformationTariffs
                                                           from examinationTariffInformation in Db.ExaminationTariffInformation.Where(x => x.ExaminationTariffInformationId == resextariffEM).DefaultIfEmpty()
                                                           join examinationTariff in Db.ExaminationTariffs on examinationTariffInformationTariff.ExaminationTariffId equals examinationTariff.ExaminationTariffId
                                                           join examinationCharge in Db.ExaminationCharges on examinationTariff.ExaminationChargeId equals examinationCharge.ExaminationChargeId
                                                           where
                                                           examinationTariffInformationTariff.ExaminationTariffInformationId == resextariffEM
                                                           && (examinationTariff.PerIndexRate > 0)
                                                           select new InvoiceItemViewModel
                                                           {
                                                               Description = examinationCharge.ExaminationChargeName,
                                                               Amount = examinationTariff.PerIndexRate * emcount,
                                                               TariffId = examinationTariff.ExaminationTariffId,
                                                               TariffType = "CY",
                                                               Type = "NoNGeneral",
                                                               Category = "ExaminationTariffCharges"
                                                           }).Distinct().ToList();

                                if (item.GroundingFreedays == 0)
                                {
                                    examinationperindex.RemoveAll(x => x.Description.Contains("GROUNDING CHARGES"));
                                }
                                else
                                {

                                    if (examinationperindex.Count() > 0 && item.GroundingFreedays <= totalgroundingfreedays)
                                    {
                                        examinationperindex.RemoveAll(x => x.Description.Contains("GROUNDING CHARGES"));

                                    }


                                }
                                if (examinationperindex.Exists(p => p.Description.Contains("GROUNDING CHARGES")))
                                {

                                    var dys = item.GroundingFreedays - totalgroundingfreedays;

                                    foreach (var newitem in examinationperindex)
                                    {
                                        if (newitem.Description == "GROUNDING CHARGES")
                                        {
                                            newitem.Amount = newitem.Amount * dys ?? 0;
                                        }
                                    }
                                }




                                examinationtarifflist.AddRange(examinationperindex);


                            }
                        }


                    }

                }
            }

            #endregion





            totalItems.AddRange(examinationtarifflist);




            if (container != null)
            {
                var portchrges = new InvoiceItemViewModel
                {
                    Description = "Port Charges",
                    Amount = Db.PortCharges.Where(s => s.VirNumber == container.VIRNo && s.IndexNumber == container.IndexNo).Sum(s => s.TotalCharges),
                    TariffId = container.CYContainerId,
                    TariffType = "CYPerIndex",
                    Type = "NoNGeneral",
                    Category = "PortCharges"
                };

                totalItems.Add(portchrges);

            }

            #region auciton work current invoice


            var isauction = GetAuctionInfo("CY", container.VIRNo, container.IndexNo ?? 0);

            if (isauction == true)
            {
                var handlingamount = 0.00;

                foreach (var item in containerList)
                {
                    handlingamount += item.Size == 20 ? (auctionamountinfo.Rate20) : item.Size == 40 ? (auctionamountinfo.Rate40) : item.Size == 45 ? (auctionamountinfo.Rate45) : 0;
                }

                if (handlingamount > 0)
                {
                    var aucitonamount = new InvoiceItemViewModel
                    {
                        Description = "AuctionHandingCharges",
                        Amount = handlingamount,
                        TariffId = auctionamountinfo.AuctionAmountId,
                        TariffType = "Auction",
                        Type = "Auction",
                        Category = "Auction"
                    };
                    totalItems.Add(aucitonamount);
                }

            }

            #endregion



            totalItems.RemoveAll(c => c.Amount <= 0);


            //if (totalpartcontainers >  0)
            //{
            //    foreach (var item in totalItems.Where(x=> x.TariffType != "ExaminationAssignCharges" && x.TariffType != "CYPerIndex"))
            //    {
            //        item.Amount = (item.Amount / totalpartcontainers);

            //    }
            //} 


            foreach (var item in totalItems)
            {
                item.Amount = Math.Round(item.Amount, 2);

            }


            if (totalItems.Count() > 0)
            {
                foreach (var item in totalItems)
                {
                    item.TariffInformationId = tariffinfoid;

                }
            }


            return totalItems;

        }





        public List<InvoiceItemViewModel> checkDuplicationCharges(List<InvoiceItemViewModel> modelList, List<InvoiceItemViewModel> model)
        {
            foreach (var item in model)
            {
                var result = modelList.Find(t => t.Description == item.Description && t.Amount == item.Amount);

                if (result != null)
                {
                    result.Amount += item.Amount;
                }
                else
                {

                    modelList.Add(item);
                }

            }

            return modelList;


        }

        public List<InvoiceItemViewModel> GetCFSSizeTotal(long IndexNo, string virno)
        {
            var totalItems = new List<InvoiceItemViewModel>();

            var ContaineIndexs = (from containerIndex in Db.ContainerIndices
                                  join voyage in Db.Voyages on containerIndex.VoyageId equals voyage.VoyageId
                                  where containerIndex.IndexNo == IndexNo && voyage.VIRNo == virno
                                  select containerIndex);

            foreach (var item in ContaineIndexs)
            {
                var agentitems = (from tariff in Db.Tariffs
                                  join agentTariff in Db.ShippingAgentTariffs on tariff.TariffId equals agentTariff.TariffId
                                  join container in Db.ContainerIndices on agentTariff.ShippingAgentId equals container.ShippingAgentId
                                  where container.ContainerIndexId == item.ContainerIndexId && (tariff.Rate20 > 0 || tariff.Rate40 > 0 || tariff.Rate45 > 0)
                                  && !Db.DisabledAgentTariffs.Any(s => s.ContainerIndexId == container.ContainerIndexId && s.ShippingAgentTariffId == agentTariff.ShippingAgentTariffId)

                                  select new InvoiceItemViewModel
                                  {
                                      Description = tariff.TariffHead.Name,
                                      Amount = container.Size == 20 ? tariff.Rate20 : container.Size == 40 ? tariff.Rate40 : container.Size == 45 ? tariff.Rate45 : 0,
                                      TariffId = tariff.TariffId
                                  }).Distinct().ToList();

                var igmItems = (from tariff in Db.Tariffs
                                join igmTariff in Db.ContainerIndexTariffs on tariff.TariffId equals igmTariff.TariffId
                                join container in Db.ContainerIndices on igmTariff.ContainerIndexId equals container.ContainerIndexId
                                where container.ContainerIndexId == item.ContainerIndexId && (tariff.Rate20 > 0 || tariff.Rate40 > 0 || tariff.Rate45 > 0)
                                select new InvoiceItemViewModel
                                {
                                    Description = tariff.TariffHead.Name,
                                    Amount = container.Size == 20 ? tariff.Rate20 : container.Size == 40 ? tariff.Rate40 : container.Size == 45 ? tariff.Rate45 : 0,
                                    TariffId = tariff.TariffId
                                }).Distinct().ToList();

                totalItems.AddRange(igmItems);
                totalItems.AddRange(agentitems);

            }







            return totalItems;
        }


        public StorageTotalViewModel GetStorageTotal(long ContainerIndexId, long clearingAgentId, DateTime BillDate, DateTime gateInDate, DateTime destuffdate, double Weight, double CBM, string indexcargotype)
        {
            var datetime = DateTime.Now;
            if (indexcargotype == "LCL")
            {

                var storageTotal = 0.0;
                var storageDays = 0;
                var noOfDays = 0;
                var deliveredweight = 0;
                var freedaysCounted = false;

                var totalFreeDays = 0;
                var totalBalanceCargo = 0.00;

                int additionalFreeDays = 0;
                var isdgcargo = false;
                var checkstaus = false;
                var FreeDaysType = "";
                var StorageApplicableon = "";


                var invoiceDays = 0;
                var invoicestorageTotal = 0.00;

                var storagepplicablebilltype = "";

                long storagesharePercentage = 0;

                double aictPerBoxRateShare = 0.00;

                var container = Db.ContainerIndices.Find(ContainerIndexId);


                if (container != null)
                {
                    if (container.IsDGCargo == true)
                    {
                        isdgcargo = true;
                    }
                    else
                    {
                        isdgcargo = false;
                    }
                    //    var gatePassDeliverdWeight = (from gatepass in Db.OrderDetails
                    //                    where
                    //                    gatepass.UnitType == "WEIGHT" && gatepass.IndexNo == container.IndexNo && gatepass.VirNumber == container.VirNo
                    //                    select gatepass.Delivered).Sum();
                    //    if(gatePassDeliverdWeight != null)
                    //    {
                    //        deliveredweight = gatePassDeliverdWeight ?? 0;
                    //    }
                }


                #region stop for after delivered of cargo

                var invdys = 0;

                var invamtss = 0.00;

                var gatepss = (from gatepass in Db.OrderDetails
                               join doitem in Db.DOItems on gatepass.GatePassId equals doitem.GatePassId
                               where
                               gatepass.VirNumber == container.VirNo
                               &&
                               gatepass.IndexNo == container.IndexNo
                               &&
                               doitem.Status == "F"
                               select doitem.Status
                             ).ToList();

                if (gatepss.Count() > 0)
                {

                    var delivereddys = (from inovice in Db.Invoices
                                        where container.ContainerIndexId == inovice.ContainerIndexId &&
                                        inovice.BillType == "Normal"
                                        select inovice).LastOrDefault();



                    var invoicgeamt = (from inovice in Db.Invoices
                                       join invoiceitem in Db.InvoiceItems on inovice.InvoiceId equals invoiceitem.InvoiceId
                                       where container.ContainerIndexId == inovice.ContainerIndexId &&
                                       invoiceitem.Description.Contains("Storage Amount")
                                       select invoiceitem).Sum(x => x.Amount);

                    if (invoicgeamt > 0)
                    {
                        invamtss = invoicgeamt;
                    }


                    var perviouswaiverLCLres = GetPreviousWaiverCFS(container.ContainerIndexId).Where(x => x.Category == "Storage" && x.Description == "Storage Amount").ToList();

                    if (perviouswaiverLCLres.Count() > 0)
                    {

                        var result = perviouswaiverLCLres.Where(x => x.Category == "Storage" && x.Description == "Storage Amount").ToList().Sum(x => x.Discount);

                        if (result > 0)
                        {
                            invamtss += result;
                        }

                    }

                    return new StorageTotalViewModel { StorageDays = 0, StorageTotal = invamtss, AddtionalFreeDays = delivereddys.AdditionalFreeDays ?? 0, TotalFreeDays = delivereddys.TotalFreeDays ?? 0, TotalBalanceCargo = totalBalanceCargo, StorageFreeDaysType = FreeDaysType, StorageApplicableon = "Weight", StorageSharePercent = 0, AictPerBoxRate = 0 };

                }
                #endregion

                var shippingAget = Db.ShippingAgents.Find(container.ShippingAgentId);

                if (shippingAget.WeightAllow == "Yes")
                {
                    Weight = (Weight * shippingAget.WeightAmount);
                }


                //var isocode = (from isocodes in Db.ISOCodes
                //               where container.ISOCode == isocodes.Code
                //               select isocodes).FirstOrDefault();

                //var isocode = (from isocodes in Db.ISOCodes
                //               from isoCodeHead in Db.ISOCodeHeads.Where(x => x.ISOCodeHeadId == x.ISOCodeHeadId).DefaultIfEmpty()
                //               where container.ISOCode == isocodes.Code
                //               select isoCodeHead).FirstOrDefault();


                var isocode = Db.ISOCodeHeads.FromSql($"select isoCodeHead.ISOCodeHeadId , isoCodeHead.ISOCodeHeadDescription  , isoCodeHead.DeleteDate , isoCodeHead.IsDeleted  from ISOCode   left join isoCodeHead  on ISOCode.ISOCodeHeadId = ISOCodeHead.ISOCodeHeadId  and isoCodeHead.IsDeleted = 0 Where     ISOCode.Code    = {container.ISOCode} and ISOCode.IsDeleted = 0").FirstOrDefault();


                if (clearingAgentId != null || clearingAgentId != 0 && checkstaus == false)
                {
                    var clearingAgent = Db.ClearingAgents.FromSql($"select * from ClearingAgent   Where ClearingAgentId    = {clearingAgentId} and IsDeleted = 0").FirstOrDefault();

                    if (clearingAgent != null)
                    {
                        if (clearingAgent.BillDateTypeLCL != null)
                        {
                            storagepplicablebilltype = clearingAgent.BillDateTypeLCL;
                            checkstaus = true;
                        }
                    }

                }


                if (container.ConsigneId != null && checkstaus == false)
                {
                    var consignee = Db.Consignes.FromSql($"select * from Consigne   Where ConsigneId    = {container.ConsigneId} and IsDeleted = 0").FirstOrDefault();

                    if (consignee != null)
                    {
                        if (consignee.BillDateTypeLCL != null)
                        {
                            storagepplicablebilltype = consignee.BillDateTypeLCL;
                            checkstaus = true;
                        }
                    }

                }

                if (shippingAget != null && checkstaus == false)
                {
                    if (shippingAget.BillDateType != null)
                    {
                        storagepplicablebilltype = shippingAget.BillDateType;

                        checkstaus = true;
                    }

                }


                //var calculateCBM = CBM > 0 ? CBM : destuff.CBM ?? 0;

                //var calculateWeight = Weight > 0 ? Weight : destuff.FoundWeight;

                var calculateCBM = CBM;

                var calculateWeight = Weight;

                var weightOrCBM = WeightGreaterThenCBM(calculateWeight, calculateCBM);

                var weightConv = (Convert.ToDouble(calculateWeight) / 1000.0);





                if (weightOrCBM == 1)
                {
                    if (deliveredweight > 0)
                    {
                        weightConv = (weightConv - deliveredweight);
                    }
                }

                var tarifList = new List<Tariff>();


                var res = GetTariffAllList(container.ShippingAgentId, isocode.ISOCodeHeadId, container.ConsigneId, clearingAgentId, container.ShipperId, container.GoodsHeadId, container.PortAndTerminalId, indexcargotype);

                if (res > 0)
                {

                    var tarifListres = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                        join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId

                                        where
                                        tariffInofrmationTariff.TariffInformationId == res
                                        //&& tariff.TariffType == "CFS"
                                        && tariff.TariffHead.Name.Contains("STORAGE")
                                        && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                        &&
                                                        (
                                                        tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                                        tariff.TypeOfImplementation == "Arrival" ?
                                                        Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.TypeOfImplementation == "Delivery" ?
                                                        Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.ImplementFrom == null
                                                        )
                                                        &&
                                                        (
                                                        tariff.TypeOfImplementation == "All" ?
                                                        tariff.ImplementTo == null :
                                                        tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                                        Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                                        Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                        tariff.ImplementTo == null
                                                        )
                                        select tariff).ToList();

                    tarifList.AddRange(tarifListres);
                }



                if (tarifList.Count() > 0)
                {
                    //var das = getStoragefreeDays(container.ShippingAgentId, container.ConsigneId, clearingAgentId, container.GoodsHeadId, tarifList[0].TariffId);
                    var das = getStoragefreeDays(container.ShippingAgentId, container.ConsigneId, clearingAgentId, container.GoodsHeadId, res, isdgcargo, "LCL");

                    additionalFreeDays = Convert.ToInt32(das.NoofFreeDays);
                    FreeDaysType = das.FreeDaysType;

                    foreach (var item in tarifList)
                    {
                        var storagepercent = Db.StorageShareRates.FromSql($"select  * from StorageShareRate where TariffInformationId = {res} and TariffId = {item.TariffId} and  IsDeleted = 0").LastOrDefault();

                        if (storagepercent != null)
                        {
                            storagesharePercentage += storagepercent.ShareRate;
                        }
                    }

                }


                var nooffreedays = 0;


                nooffreedays = additionalFreeDays;

                //var TariffIds = (from tariff in Db.Tariffs
                //                 join tariffInfo in Db.TariffInformations on tariff.TariffId equals tariffInfo.TariffId
                //                 where
                //                   tariff.TariffType == "CFS"
                //                     && (tariffInfo.GoodsHeadId == container.GoodsHeadId || tariffInfo.GoodsHeadId == null)
                //                    && (tariffInfo.ShipperId == container.ShipperId || tariffInfo.ShipperId == null)
                //                    && (tariffInfo.ClearingAgentId == clearingAgentId || tariffInfo.ClearingAgentId == null)
                //                    && (tariffInfo.ConsigneId == container.ConsigneId || tariffInfo.ConsigneId == null)
                //                    && (tariffInfo.PortAndTerminalId == container.PortAndTerminalId || tariffInfo.PortAndTerminalId == null)
                //                    && (tariffInfo.ShippingAgentId == container.ShippingAgentId || tariffInfo.ShippingAgentId == null)
                //                    && (tariffInfo.ContainerType == isocode.Descrition || tariffInfo.ContainerType == null)

                //                 && tariff.TariffHead.Name.Contains("STORAGE")
                //                 select tariff).ToList();


                //var nooffreedays = 0;


                // need to work

                //nooffreedays = additionalFreeDays;


                //var firstFreedays = (from storageFreeDays in Db.StorageFreeDays
                //                     where
                //                           (storageFreeDays.GoodsHeadId == container.GoodsHeadId || storageFreeDays.GoodsHeadId == null)
                //                        && (storageFreeDays.ShipperId == container.ShipperId || storageFreeDays.ShipperId == null)
                //                        && (storageFreeDays.ClearingAgentId == clearingAgentId || storageFreeDays.ClearingAgentId == null)
                //                        && (storageFreeDays.ConsigneId == container.ConsigneId || storageFreeDays.ConsigneId == null)
                //                        && (storageFreeDays.PortAndTerminalId == container.PortAndTerminalId || storageFreeDays.PortAndTerminalId == null)
                //                        && (storageFreeDays.ShippingAgentId == container.ShippingAgentId || storageFreeDays.ShippingAgentId == null)
                //                        && (storageFreeDays.ISOCodeHeadId == isocode.ISOCodeHeadId || storageFreeDays.ISOCodeHeadId == null)
                //                        && (storageFreeDays.IndexCargoType == indexcargotype || storageFreeDays.IndexCargoType == null)

                //                     select storageFreeDays).FirstOrDefault();

                //if (firstFreedays != null)
                //{
                //    nooffreedays = Convert.ToInt32(firstFreedays.StorageFreeDays);
                //} 

                //  else
                //{
                //    nooffreedays = 5;
                //}

                nooffreedays = (nooffreedays - 1);


                //var distinctTariffIds = TariffIds.Distinct<long>();

                if (storagepplicablebilltype == "VesselArrival")
                {
                    //noOfDays = Convert.ToInt32((BillDate - destuff.TellySheet.DestuffDate.Value.AddDays(3)).TotalDays);
                    noOfDays = Convert.ToInt32((BillDate.Date - container.Voyage.BerthOn.Value.AddDays((nooffreedays)).Date).Days);

                    totalFreeDays = Convert.ToInt32((BillDate.Date - container.Voyage.BerthOn.Value.AddDays((-1)).Date).Days);

                }
                if (storagepplicablebilltype == "Destuffed")
                {
                    //noOfDays = Convert.ToInt32((BillDate - destuff.TellySheet.DestuffDate.Value.AddDays(3)).TotalDays);
                    noOfDays = Convert.ToInt32((BillDate.Date - destuffdate.AddDays((nooffreedays)).Date).Days);

                    totalFreeDays = Convert.ToInt32((BillDate.Date - destuffdate.AddDays((-1)).Date).Days);


                }
                if (storagepplicablebilltype == "GateIn")
                {

                    //noOfDays = Convert.ToInt32((BillDate - container.CFSContainerGateInDate.Value.AddDays(3)).TotalDays);

                    noOfDays = Convert.ToInt32((BillDate.Date - gateInDate.AddDays((nooffreedays)).Date).Days);

                    totalFreeDays = Convert.ToInt32((BillDate.Date - gateInDate.AddDays((-1)).Date).Days);

                }

                //if (container.AdditionalFreeDays > 0)
                //{

                //    noOfDays = noOfDays - container.AdditionalFreeDays;
                //}



                List<StorageDaysViewModel> storageDayslist = new List<StorageDaysViewModel>();

                for (int i = 0; i < noOfDays; i++)
                {
                    var storageDay = new StorageDaysViewModel
                    {

                        IndexNo = i,
                        Status = "UnDelivered"
                    };
                    storageDayslist.Add(storageDay);
                }
                if (container != null)
                {

                    var invoicesstoragedays = Db.Invoices.FromSql($"select  * from Invoice where ContainerIndexId = {container.ContainerIndexId} and BillType = 'Normal' and  IsDeleted = 0").Sum(x => x.StorageDays);

                    //var invoicesstoragedays = (from inovice in Db.Invoices
                    //                           where container.ContainerIndexId == inovice.ContainerIndexId &&
                    //                           inovice.BillType == "Normal"
                    //                           select inovice).Sum(x => x.StorageDays);

                    if (invoicesstoragedays > 0)
                    {
                        invoiceDays = invoicesstoragedays;
                    }

                    var invoicesstorageamount = (from inovice in Db.Invoices
                                                 join invoiceitem in Db.InvoiceItems on inovice.InvoiceId equals invoiceitem.InvoiceId
                                                 where container.ContainerIndexId == inovice.ContainerIndexId &&
                                                 invoiceitem.Description.Contains("Storage Amount")
                                                 select invoiceitem).Sum(x => x.Amount);


                    if (invoicesstorageamount > 0)
                    {
                        invoicestorageTotal = invoicesstorageamount;
                    }
                }

                if (invoiceDays > 0)
                {

                    for (int i = 0; i < invoiceDays; i++)
                    {
                        storageDayslist.Where(x => x.Status == "UnDelivered").FirstOrDefault().Status = "Delivered";
                    }

                }


                storageDays = storageDayslist.Where(x => x.Status == "UnDelivered").Count();


                //storageDays = noOfDays;


                totalBalanceCargo = weightOrCBM == 1 ? weightConv : calculateCBM;




                foreach (var tariffId in tarifList)
                {
                    var storageSlabs = Db.StorageSlabs.Where(c => c.TariffId == tariffId.TariffId).ToList();

                    if (storageSlabs.Count() > 0)
                    {



                        if (!freedaysCounted)
                        {
                            //noOfDays -= storageSlabs.FirstOrDefault().FreeDays;

                            //storageDays = noOfDays;

                            freedaysCounted = true;

                        }

                        if (storageDayslist.Where(x => x.Status == "UnDelivered").Count() < 0)
                        {
                            noOfDays = 0;

                            storageDays = noOfDays;
                        }

                        //var unitToMultiply = weightOrCBM == 1 ? weightConv : calculateCBM;



                        foreach (var storageSlab in storageSlabs)
                        {
                            if (noOfDays <= 0)
                                break;

                            if (!storageSlab.NoOfDays.ToUpper().Contains("OVER"))
                            {
                                //var period = (noOfDays - Convert.ToInt32(storageSlab.NoOfDays)) > 0 ? Convert.ToInt32(storageSlab.NoOfDays) : noOfDays;
                                var period = (storageDayslist.Count()) - (Convert.ToInt32(storageSlab.NoOfDays) > 0 ? Convert.ToInt32(storageSlab.NoOfDays) : storageDayslist.Count());

                                if (period > Convert.ToInt32(storageSlab.NoOfDays) || storageDayslist.Count() > Convert.ToInt32(storageSlab.NoOfDays))
                                    period = Convert.ToInt32(storageSlab.NoOfDays);
                                else if (period < Convert.ToInt32(storageSlab.NoOfDays))
                                    period = storageDayslist.Count();

                                var takeitems = storageDayslist.Take(period).Select(x => x).ToList();

                                foreach (var itemstor in takeitems)
                                {
                                    if (tariffId.IsCBMorRate == true)
                                    {
                                        if (itemstor.Status == "UnDelivered")
                                        {
                                            storageTotal += (storageSlab.Rate * totalBalanceCargo);
                                        }
                                    }


                                    storageDayslist.RemoveAll(x => x.IndexNo == itemstor.IndexNo);
                                }

                                if (storageDayslist.Count() < Convert.ToInt32(storageSlab.NoOfDays))
                                {
                                    noOfDays -= period;

                                }

                                else
                                {
                                    noOfDays -= Convert.ToInt32(storageSlab.NoOfDays);

                                }
                            }
                            else
                            {
                                foreach (var itemstor in storageDayslist)
                                {
                                    if (itemstor.Status == "UnDelivered")
                                    {
                                        if (tariffId.IsCBMorRate == true)
                                        {
                                            if (itemstor.Status == "UnDelivered")
                                            {
                                                storageTotal += (storageSlab.Rate * totalBalanceCargo);
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }

                if (storageDays < 0)
                {
                    storageDays = 0;
                }

                if (invoicestorageTotal > 0)
                {
                    storageTotal += invoicestorageTotal;
                }
                //}

                storageTotal = Math.Round(storageTotal, 2);



                var perviouswaiverLCL = GetPreviousWaiverCFS(container.ContainerIndexId).Where(x => x.Category == "Storage" && x.Description == "Storage Amount").ToList();

                if (perviouswaiverLCL.Count() > 0)
                {

                    var result = perviouswaiverLCL.Where(x => x.Category == "Storage" && x.Description == "Storage Amount").ToList().Sum(x => x.Discount);

                    if (result > 0)
                    {
                        storageTotal += result;
                    }

                }

                if (res > 0)
                {
                    var indexcontainersdata = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {container.VirNo} and IndexNo = {container.IndexNo} and IsDeleted = 0").ToList();


                    foreach (var item in indexcontainersdata)
                    {

                        var resdataoftariffPerBoxRate = (from tariffPerBoxRate in Db.TariffPerBoxRates
                                                         where
                                                          tariffPerBoxRate.TariffInformationId == res
                                                         && (tariffPerBoxRate.PortAndTerminalId != null ? tariffPerBoxRate.PortAndTerminalId == container.PortAndTerminalId : tariffPerBoxRate.PortAndTerminalId == null)
                                                          && (tariffPerBoxRate.Rate20 > 0 || tariffPerBoxRate.Rate40 > 0 || tariffPerBoxRate.Rate45 > 0)
                                                         &&
                                                          (
                                                          tariffPerBoxRate.TypeOfImplementation == "All" ? tariffPerBoxRate.ImplementFrom == null :
                                                          tariffPerBoxRate.TypeOfImplementation == "Arrival" ?
                                                          Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariffPerBoxRate.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                          tariffPerBoxRate.TypeOfImplementation == "Delivery" ?
                                                          Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariffPerBoxRate.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                          tariffPerBoxRate.ImplementFrom == null
                                                          )
                                                          &&
                                                          (
                                                          tariffPerBoxRate.TypeOfImplementation == "All" ?
                                                          tariffPerBoxRate.ImplementTo == null :
                                                          tariffPerBoxRate.TypeOfImplementation == "Arrival" && tariffPerBoxRate.ImplementTo != null ?
                                                          Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariffPerBoxRate.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                          tariffPerBoxRate.TypeOfImplementation == "Delivery" && tariffPerBoxRate.ImplementTo != null ?
                                                          Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariffPerBoxRate.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                          tariffPerBoxRate.ImplementTo == null
                                                          )

                                                         select new InvoiceItemViewModel
                                                         {
                                                             Amount = item.Size == 20 ? tariffPerBoxRate.Rate20 : item.Size == 40 ? tariffPerBoxRate.Rate40 : item.Size == 45 ? tariffPerBoxRate.Rate45 : 0,
                                                         }).ToList();

                        aictPerBoxRateShare += resdataoftariffPerBoxRate.Sum(x => x.Amount);

                    }
                }

                return new StorageTotalViewModel { StorageDays = storageDays, StorageTotal = storageTotal, AddtionalFreeDays = additionalFreeDays, TotalFreeDays = totalFreeDays, TotalBalanceCargo = totalBalanceCargo, StorageFreeDaysType = FreeDaysType, StorageApplicableon = "Weight", StorageSharePercent = storagesharePercentage, AictPerBoxRate = aictPerBoxRateShare };
            }
            if (indexcargotype == "FCL")
            {
                bool checkpartcontainers = false;

                var storageTotal = 0.0;
                var storageDays = 0;
                var noOfDays = 0;
                var deliveredweight = 0.0;

                var invoiceDays = 0;
                var totalFreeDays = 0;
                var invoicestorageTotal = 0.00;
                var totalBalanceCargo = 0.00;

                var freedaysCounted = false;

                var isdgcargo = false;
                var FreeDaysType = "";
                var StorageApplicableon = "";
                var FirstInvoiceType = "";
                var FirstGatepassType = "";
                var checkstaus = false;

                var storagepplicablebilltype = "";

                long storagesharePercentage = 0;

                double aictPerBoxRateShare = 0.00;

                var containerlist = new List<ContainerIndex>();

                int additionalFreeDays = 0;

                var container = Db.ContainerIndices.Find(ContainerIndexId);

                if (container != null)
                {

                    //if (container.IsPartialContainer == true)
                    //{
                    //    checkpartcontainers = true;
                    //}

                    if (container.IsDGCargo == true)
                    {
                        isdgcargo = true;
                    }
                    else
                    {
                        isdgcargo = false;
                    }

                    var invoiceres = Db.Invoices.FromSql($"SELECT * From Invoice Where ContainerIndexId = {container.ContainerIndexId}  and IsDeleted = 0 ").FirstOrDefault();

                    if (invoiceres != null)
                    {
                        FirstInvoiceType = invoiceres.StorageApplicableon;
                    }

                    var gatepassres = Db.OrderDetails.FromSql($"SELECT * From GatePass Where IndexNo = {container.IndexNo} and  VirNumber =   {container.VirNo} and IsDeleted = 0 ").FirstOrDefault();

                    if (gatepassres != null)
                    {
                        FirstGatepassType = gatepassres.UnitType;
                    }
                    if (FirstInvoiceType == "Weight")
                    {
                        var gatePassDeliverdWeight = (from gatepass in Db.OrderDetails
                                                      where
                                                      gatepass.UnitType == "WEIGHT" && gatepass.IndexNo == container.IndexNo && gatepass.VirNumber == container.VirNo
                                                      select gatepass.Delivered).Sum();
                        if (gatePassDeliverdWeight != null && gatePassDeliverdWeight >= 0)
                        {
                            deliveredweight = Math.Round(gatePassDeliverdWeight, 2);
                        }

                    }

                    containerlist = Db.ContainerIndices.FromSql($"select * from ContainerIndex where VirNo = {container.VirNo}  and IndexNo = {container.IndexNo}  and IsDeleted = 0").ToList();


                }



                var shippingAget = Db.ShippingAgents.Find(container.ShippingAgentId);


                //var isocode = (from isocodes in Db.ISOCodes
                //               from isoCodeHead in Db.ISOCodeHeads.Where(x => x.ISOCodeHeadId == x.ISOCodeHeadId).DefaultIfEmpty()
                //               where container.ISOCode == isocodes.Code
                //               select isoCodeHead).FirstOrDefault();

                var isocode = Db.ISOCodeHeads.FromSql($"select isoCodeHead.ISOCodeHeadId , isoCodeHead.ISOCodeHeadDescription  , isoCodeHead.DeleteDate , isoCodeHead.IsDeleted  from ISOCode   left join isoCodeHead  on ISOCode.ISOCodeHeadId = ISOCodeHead.ISOCodeHeadId  and isoCodeHead.IsDeleted = 0 Where     ISOCode.Code    = {container.ISOCode} and ISOCode.IsDeleted = 0").FirstOrDefault();



                if (clearingAgentId != null || clearingAgentId != 0 && checkstaus == false)
                {
                    var clearingAgent = Db.ClearingAgents.FromSql($"select * from ClearingAgent   Where ClearingAgentId    = {clearingAgentId} and IsDeleted = 0").FirstOrDefault();

                    if (clearingAgent != null)
                    {
                        if (clearingAgent.BillDateTypeFCL != null)
                        {
                            storagepplicablebilltype = clearingAgent.BillDateTypeFCL;
                            checkstaus = true;
                        }
                    }

                }


                if (container.ConsigneId != null && checkstaus == false)
                {
                    var consignee = Db.Consignes.FromSql($"select * from Consigne   Where ConsigneId    = {container.ConsigneId} and IsDeleted = 0").FirstOrDefault();

                    if (consignee != null)
                    {
                        if (consignee.BillDateTypeFCL != null)
                        {
                            storagepplicablebilltype = consignee.BillDateTypeFCL;
                            checkstaus = true;
                        }

                    }

                }

                if (shippingAget != null && checkstaus == false)
                {
                    if (shippingAget.BillDateTypeFCL != null)
                    {
                        storagepplicablebilltype = shippingAget.BillDateTypeFCL;
                        checkstaus = true;
                    }

                }

                var calculateWeight = Weight;

                var weightConv = (Convert.ToDouble(calculateWeight) / 1000.0);

                var resdata = Db.ShortExcessWeigths.FromSql($"SELECT * From ShortExcessWeigth Where VirNumber = {container.VirNo} and IndexNumber = {container.IndexNo} and IsDeleted = 0 and ContainerType = 'CFS' ").LastOrDefault();

                if (resdata != null)
                {
                    if (resdata.ShortWeight > 0)
                    {
                        weightConv -= resdata.ShortWeight;
                    }

                    if (resdata.ExcesstWeight > 0)
                    {
                        weightConv += resdata.ExcesstWeight;
                    }
                }

                if (deliveredweight > 0)
                {
                    //var deliveredweightintoton = (Convert.ToDouble(deliveredweight) / 1000.0);
                    var deliveredweightintoton = deliveredweight;
                    weightConv = (weightConv - deliveredweightintoton);
                }


                totalBalanceCargo = weightConv;


                var tarifList = new List<Tariff>();


                var res = GetTariffAllList(container.ShippingAgentId, isocode.ISOCodeHeadId, container.ConsigneId, clearingAgentId, container.ShipperId, container.GoodsHeadId, container.PortAndTerminalId, indexcargotype);

                if (res > 0)
                {

                    var tarifListres = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                        join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId

                                        where
                                        tariffInofrmationTariff.TariffInformationId == res
                                        //&& tariff.TariffType == "CFS"
                                        && tariff.TariffHead.Name.Contains("STORAGE")
                                        && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                        &&
                                        (
                                        tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                        tariff.TypeOfImplementation == "Arrival" ?
                                        Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                        tariff.TypeOfImplementation == "Delivery" ?
                                        Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                        tariff.ImplementFrom == null
                                        )
                                        &&
                                        (
                                        tariff.TypeOfImplementation == "All" ?
                                        tariff.ImplementTo == null :
                                        tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                        Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                        tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                        Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                        tariff.ImplementTo == null
                                        )
                                        select tariff).ToList();

                    tarifList.AddRange(tarifListres);
                }


                if (res > 0)
                {
                    var indexcontainersdata = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {container.VirNo} and IndexNo = {container.IndexNo} and IsDeleted = 0").ToList();


                    foreach (var item in indexcontainersdata)
                    {

                        var resdataoftariffPerBoxRate = (from tariffPerBoxRate in Db.TariffPerBoxRates
                                                         where
                                                          tariffPerBoxRate.TariffInformationId == res
                                                         && (tariffPerBoxRate.PortAndTerminalId != null ? tariffPerBoxRate.PortAndTerminalId == container.PortAndTerminalId : tariffPerBoxRate.PortAndTerminalId == null)
                                                          && (tariffPerBoxRate.Rate20 > 0 || tariffPerBoxRate.Rate40 > 0 || tariffPerBoxRate.Rate45 > 0)
                                                         &&
                                                          (
                                                          tariffPerBoxRate.TypeOfImplementation == "All" ? tariffPerBoxRate.ImplementFrom == null :
                                                          tariffPerBoxRate.TypeOfImplementation == "Arrival" ?
                                                          Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariffPerBoxRate.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                          tariffPerBoxRate.TypeOfImplementation == "Delivery" ?
                                                          Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariffPerBoxRate.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                          tariffPerBoxRate.ImplementFrom == null
                                                          )
                                                          &&
                                                          (
                                                          tariffPerBoxRate.TypeOfImplementation == "All" ?
                                                          tariffPerBoxRate.ImplementTo == null :
                                                          tariffPerBoxRate.TypeOfImplementation == "Arrival" && tariffPerBoxRate.ImplementTo != null ?
                                                          Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariffPerBoxRate.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                          tariffPerBoxRate.TypeOfImplementation == "Delivery" && tariffPerBoxRate.ImplementTo != null ?
                                                          Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariffPerBoxRate.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                          tariffPerBoxRate.ImplementTo == null
                                                          )

                                                         select new InvoiceItemViewModel
                                                         {
                                                             Amount = item.Size == 20 ? tariffPerBoxRate.Rate20 : item.Size == 40 ? tariffPerBoxRate.Rate40 : item.Size == 45 ? tariffPerBoxRate.Rate45 : 0,
                                                         }).ToList();

                        aictPerBoxRateShare += resdataoftariffPerBoxRate.Sum(x => x.Amount);

                    }
                }


                if (tarifList.Count() > 0)
                {
                    //var das = getStoragefreeDays(container.ShippingAgentId, container.ConsigneId, clearingAgentId, container.GoodsHeadId, tarifList[0].TariffId);
                    var das = getStoragefreeDays(container.ShippingAgentId, container.ConsigneId, clearingAgentId, container.GoodsHeadId, res, isdgcargo, "FCL");

                    additionalFreeDays = Convert.ToInt32(das.NoofFreeDays);

                    FreeDaysType = das.FreeDaysType;

                    foreach (var item in tarifList)
                    {
                        var storagepercent = Db.StorageShareRates.FromSql($"select  * from StorageShareRate where TariffInformationId = {res} and TariffId = {item.TariffId} and  IsDeleted = 0").LastOrDefault();

                        if (storagepercent != null)
                        {
                            storagesharePercentage += storagepercent.ShareRate;
                        }
                    }

                }


                var nooffreedays = 0;


                nooffreedays = additionalFreeDays;


                // var nooffreedays = 0;

                // need to work
                //  nooffreedays = additionalFreeDays;

                nooffreedays = (nooffreedays - 1);

                if (storagepplicablebilltype == "VesselArrival")
                {
                    noOfDays = Convert.ToInt32((BillDate.Date - container.Voyage.BerthOn.Value.AddDays((nooffreedays)).Date).Days);

                    totalFreeDays = Convert.ToInt32((BillDate.Date - container.Voyage.BerthOn.Value.AddDays((-1)).Date).Days);

                }
                if (storagepplicablebilltype == "Destuffed")
                {
                    noOfDays = Convert.ToInt32((BillDate.Date - destuffdate.AddDays((nooffreedays)).Date).Days);

                    totalFreeDays = Convert.ToInt32((BillDate.Date - destuffdate.AddDays((-1)).Date).Days);

                }
                if (storagepplicablebilltype == "GateIn")
                {
                    noOfDays = Convert.ToInt32((BillDate.Date - gateInDate.AddDays((nooffreedays)).Date).Days);

                    totalFreeDays = Convert.ToInt32((BillDate.Date - gateInDate.AddDays((-1)).Date).Days);

                }


                List<StorageDaysViewModel> storageDayslist = new List<StorageDaysViewModel>();

                for (int i = 0; i < noOfDays; i++)
                {
                    var storageDay = new StorageDaysViewModel
                    {

                        IndexNo = i,
                        Status = "UnDelivered"
                    };
                    storageDayslist.Add(storageDay);
                }



                if (container != null)
                {


                    var invoicesstoragedays = (from inovice in Db.Invoices
                                               where container.ContainerIndexId == inovice.ContainerIndexId &&
                                               inovice.BillType == "Normal"
                                               select inovice).Sum(x => x.StorageDays);

                    if (invoicesstoragedays > 0)
                    {
                        invoiceDays = invoicesstoragedays;
                    }

                    var invoicesstorageamount = (from inovice in Db.Invoices
                                                 join invoiceitem in Db.InvoiceItems on inovice.InvoiceId equals invoiceitem.InvoiceId
                                                 where container.ContainerIndexId == inovice.ContainerIndexId &&
                                                 invoiceitem.Description.Contains("Storage Amount")
                                                 select invoiceitem).Sum(x => x.Amount);


                    if (invoicesstorageamount > 0)
                    {
                        invoicestorageTotal = invoicesstorageamount;
                    }



                }


                if (invoiceDays > 0)
                {

                    for (int i = 0; i < invoiceDays; i++)
                    {
                        storageDayslist.Where(x => x.Status == "UnDelivered").FirstOrDefault().Status = "Delivered";
                    }

                }

                storageDays = storageDayslist.Where(x => x.Status == "UnDelivered").Count();

                //storageDays = noOfDays;

                if (tarifList.Count() > 0)
                {

                    foreach (var tariffId in tarifList)
                    {
                        if (tariffId.IsCBMorRate == true)
                        {

                            StorageApplicableon = "Weight";
                        }

                        if (tariffId.IsCBMorRate == false)
                        {
                            StorageApplicableon = "Size";

                        }

                        var storageSlabs = Db.StorageSlabs.Where(c => c.TariffId == tariffId.TariffId).ToList();

                        if (storageSlabs.Count() > 0)
                        {
                            if (!freedaysCounted)
                            {
                                //noOfDays -= storageSlabs.FirstOrDefault().FreeDays;

                                //storageDays = noOfDays;

                                freedaysCounted = true;

                            }

                            if (storageDayslist.Where(x => x.Status == "UnDelivered").Count() < 0)
                            {
                                noOfDays = 0;

                                storageDays = noOfDays;
                            }

                            foreach (var storageSlab in storageSlabs)
                            {
                                if (noOfDays <= 0)
                                    break;

                                if (!storageSlab.NoOfDays.ToUpper().Contains("OVER"))
                                {
                                    var period = (storageDayslist.Count()) - (Convert.ToInt32(storageSlab.NoOfDays) > 0 ? Convert.ToInt32(storageSlab.NoOfDays) : storageDayslist.Count());

                                    if (period > Convert.ToInt32(storageSlab.NoOfDays) || storageDayslist.Count() > Convert.ToInt32(storageSlab.NoOfDays))
                                        period = Convert.ToInt32(storageSlab.NoOfDays);
                                    else if (period < Convert.ToInt32(storageSlab.NoOfDays))
                                        period = storageDayslist.Count();

                                    var takeitems = storageDayslist.Take(period).Select(x => x).ToList();


                                    foreach (var itemstor in takeitems)
                                    {
                                        if (itemstor.Status == "UnDelivered")
                                        {
                                            if (FirstInvoiceType == "Weight")
                                            {
                                                storageTotal += (storageSlab.Rate * weightConv);
                                                StorageApplicableon = "Weight";
                                            }
                                            else if (FirstInvoiceType == "Size" && FirstGatepassType == "PACKAGES")
                                            {


                                                foreach (var item in containerlist.Where(x => x.IsDelivered == false))
                                                {
                                                    #region new work for part shipment

                                                    if (item.IsPartialContainer == true)
                                                    {
                                                        var noofcontainers = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {item.VirNo}   and ContainerNo = {item.ContainerNo} and IsDeleted = 0").ToList();
                                                        if (noofcontainers.Count() > 0)
                                                        {

                                                            storageTotal += item.Size == 20 ? Convert.ToDouble(storageSlab.Rate20) / noofcontainers.Count() : item.Size == 40 ? Convert.ToDouble(storageSlab.Rate40) / noofcontainers.Count() : item.Size == 45 ? Convert.ToDouble(storageSlab.Rate45) / noofcontainers.Count() : 0;
                                                            StorageApplicableon = "Size";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        storageTotal += item.Size == 20 ? storageSlab.Rate20 : item.Size == 40 ? storageSlab.Rate40 : item.Size == 45 ? storageSlab.Rate45 : 0;
                                                        StorageApplicableon = "Size";
                                                    }
                                                    #endregion 

                                                    //#region new work for per box rates shipment
                                                    //    storageTotal += item.Size == 20 ? storageSlab.Rate20 : item.Size == 40 ? storageSlab.Rate40 : item.Size == 45 ? storageSlab.Rate45 : 0;
                                                    //    StorageApplicableon = "Size";   
                                                    //#endregion

                                                }

                                            }
                                            else if (FirstInvoiceType == "Size" && FirstGatepassType == "WEIGHT")
                                            {
                                                var totalgatepassdeliveredweight = 0.0;
                                                var totalcontainerscount = containerlist.Count();

                                                var totalmanifestedweightConv = (Convert.ToDouble(calculateWeight) / 1000.0);


                                                var resdatashortexcess = Db.ShortExcessWeigths.FromSql($"SELECT * From ShortExcessWeigth Where VirNumber = {container.VirNo} and IndexNumber = {container.IndexNo} and IsDeleted = 0 and ContainerType = 'CFS' ").LastOrDefault();

                                                if (resdatashortexcess != null)
                                                {
                                                    if (resdatashortexcess.ShortWeight > 0)
                                                    {
                                                        totalmanifestedweightConv -= resdatashortexcess.ShortWeight;
                                                    }

                                                    if (resdatashortexcess.ExcesstWeight > 0)
                                                    {
                                                        totalmanifestedweightConv += resdatashortexcess.ExcesstWeight;
                                                    }
                                                }



                                                totalmanifestedweightConv = Math.Round(totalmanifestedweightConv, 2);

                                                var AVGCONTWEIGHT = totalmanifestedweightConv / totalcontainerscount;

                                                AVGCONTWEIGHT = Math.Round(AVGCONTWEIGHT, 2);

                                                var gatePassesDeliverdWeight = (from gatepass in Db.OrderDetails
                                                                                where
                                                                                gatepass.UnitType == "WEIGHT" && gatepass.IndexNo == container.IndexNo && gatepass.VirNumber == container.VirNo
                                                                                select gatepass.Delivered).Sum();
                                                if (gatePassesDeliverdWeight >= 0)
                                                {
                                                    totalgatepassdeliveredweight = Math.Round(gatePassesDeliverdWeight, 2);
                                                }


                                                var TOALDELIVERED = totalgatepassdeliveredweight;

                                                var BalanceWeight = totalmanifestedweightConv - TOALDELIVERED;

                                                BalanceWeight = Math.Round(BalanceWeight, 2);


                                                var BALANCECONTAINER = BalanceWeight / AVGCONTWEIGHT;

                                                BALANCECONTAINER = Math.Round(BALANCECONTAINER, 2);


                                                var containersizemax = containerlist.Max(x => x.Size);


                                                storageTotal += containersizemax == 20 ? BALANCECONTAINER * storageSlab.Rate20 : containersizemax == 40 ? BALANCECONTAINER * storageSlab.Rate40 : containersizemax == 45 ? BALANCECONTAINER * storageSlab.Rate45 : 0;

                                                StorageApplicableon = "Size";

                                            }
                                            else
                                            {
                                                if (tariffId.IsCBMorRate == true)
                                                {
                                                    storageTotal += (storageSlab.Rate * weightConv);
                                                    StorageApplicableon = "Weight";
                                                }

                                                if (tariffId.IsCBMorRate == false)
                                                {
                                                    foreach (var item in containerlist.Where(x => x.IsDelivered == false))
                                                    {

                                                        #region new work for part shipment
                                                        //if (checkpartcontainers == true)
                                                        if (item.IsPartialContainer == true)
                                                        {
                                                            var noofcontainers = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {item.VirNo}   and ContainerNo = {item.ContainerNo} and IsDeleted = 0").ToList();
                                                            if (noofcontainers.Count() > 0)
                                                            {
                                                                storageTotal += item.Size == 20 ? Convert.ToDouble(storageSlab.Rate20) / noofcontainers.Count() : item.Size == 40 ? Convert.ToDouble(storageSlab.Rate40 / noofcontainers.Count()) : item.Size == 45 ? Convert.ToDouble(storageSlab.Rate45) / noofcontainers.Count() : 0;
                                                                StorageApplicableon = "Size";
                                                            }

                                                        }
                                                        else
                                                        {
                                                            storageTotal += item.Size == 20 ? storageSlab.Rate20 : item.Size == 40 ? storageSlab.Rate40 : item.Size == 45 ? storageSlab.Rate45 : 0;
                                                            StorageApplicableon = "Size";
                                                        }
                                                        #endregion

                                                        //#region new work for per box rates shipment
                                                        //storageTotal += item.Size == 20 ? storageSlab.Rate20 : item.Size == 40 ? storageSlab.Rate40 : item.Size == 45 ? storageSlab.Rate45 : 0;
                                                        //StorageApplicableon = "Size";
                                                        //#endregion
                                                    }
                                                }
                                            }


                                        }

                                        storageDayslist.RemoveAll(x => x.IndexNo == itemstor.IndexNo);
                                    }




                                    if (storageDayslist.Count() < Convert.ToInt32(storageSlab.NoOfDays))
                                    {
                                        noOfDays -= period;

                                    }

                                    else
                                    {
                                        noOfDays -= Convert.ToInt32(storageSlab.NoOfDays);

                                    }



                                }
                                else
                                {

                                    foreach (var itemstor in storageDayslist)
                                    {
                                        if (itemstor.Status == "UnDelivered")
                                        {
                                            if (FirstInvoiceType == "Weight")
                                            {
                                                storageTotal += (storageSlab.Rate * weightConv);
                                                StorageApplicableon = "Weight";
                                            }
                                            else if (FirstInvoiceType == "Size" && FirstGatepassType == "PACKAGES")
                                            {
                                                foreach (var item in containerlist.Where(x => x.IsDelivered == false))
                                                {
                                                    #region new work for part shipment

                                                    //if (checkpartcontainers == true)
                                                    if (item.IsPartialContainer == true)
                                                    {
                                                        var noofcontainers = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {item.VirNo}   and ContainerNo = {item.ContainerNo} and IsDeleted = 0").ToList();
                                                        if (noofcontainers.Count() > 0)
                                                        {
                                                            storageTotal += item.Size == 20 ? Convert.ToDouble(storageSlab.Rate20) / noofcontainers.Count() : item.Size == 40 ? Convert.ToDouble(storageSlab.Rate40) / noofcontainers.Count() : item.Size == 45 ? Convert.ToDouble(storageSlab.Rate45) / noofcontainers.Count() : 0;
                                                            StorageApplicableon = "Size";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        storageTotal += item.Size == 20 ? storageSlab.Rate20 : item.Size == 40 ? storageSlab.Rate40 : item.Size == 45 ? storageSlab.Rate45 : 0;
                                                        StorageApplicableon = "Size";
                                                    }
                                                    #endregion

                                                    //#region new work for per box rates shipment
                                                    //    storageTotal += item.Size == 20 ? storageSlab.Rate20 : item.Size == 40 ? storageSlab.Rate40 : item.Size == 45 ? storageSlab.Rate45 : 0;
                                                    //    StorageApplicableon = "Size";
                                                    //#endregion
                                                }
                                            }
                                            else if (FirstInvoiceType == "Size" && FirstGatepassType == "WEIGHT")
                                            {
                                                var totalgatepassdeliveredweight = 0.0;

                                                var totalcontainerscount = containerlist.Count();

                                                var totalmanifestedweightConv = (Convert.ToDouble(calculateWeight) / 1000.0);


                                                var resdatashortexcess = Db.ShortExcessWeigths.FromSql($"SELECT * From ShortExcessWeigth Where VirNumber = {container.VirNo} and IndexNumber = {container.IndexNo} and IsDeleted = 0 and ContainerType = 'CFS' ").LastOrDefault();

                                                if (resdatashortexcess != null)
                                                {
                                                    if (resdatashortexcess.ShortWeight > 0)
                                                    {
                                                        totalmanifestedweightConv -= resdatashortexcess.ShortWeight;
                                                    }

                                                    if (resdatashortexcess.ExcesstWeight > 0)
                                                    {
                                                        totalmanifestedweightConv += resdatashortexcess.ExcesstWeight;
                                                    }
                                                }


                                                totalmanifestedweightConv = Math.Round(totalmanifestedweightConv, 2);

                                                var AVGCONTWEIGHT = totalmanifestedweightConv / totalcontainerscount;

                                                AVGCONTWEIGHT = Math.Round(AVGCONTWEIGHT, 2);


                                                var gatePassesDeliverdWeight = (from gatepass in Db.OrderDetails
                                                                                where
                                                                                gatepass.UnitType == "WEIGHT" && gatepass.IndexNo == container.IndexNo && gatepass.VirNumber == container.VirNo
                                                                                select gatepass.Delivered).Sum();
                                                if (gatePassesDeliverdWeight >= 0)
                                                {
                                                    totalgatepassdeliveredweight = Math.Round(gatePassesDeliverdWeight, 2);
                                                }


                                                var TOALDELIVERED = totalgatepassdeliveredweight;

                                                var BalanceWeight = totalmanifestedweightConv - TOALDELIVERED;

                                                BalanceWeight = Math.Round(BalanceWeight, 2);


                                                var BALANCECONTAINER = BalanceWeight / AVGCONTWEIGHT;

                                                BALANCECONTAINER = Math.Round(BALANCECONTAINER, 2);


                                                var containersizemax = containerlist.Max(x => x.Size);


                                                storageTotal += containersizemax == 20 ? BALANCECONTAINER * storageSlab.Rate20 : containersizemax == 40 ? BALANCECONTAINER * storageSlab.Rate40 : containersizemax == 45 ? BALANCECONTAINER * storageSlab.Rate45 : 0;

                                                StorageApplicableon = "Size";

                                            }
                                            else
                                            {
                                                if (tariffId.IsCBMorRate == true)
                                                {
                                                    storageTotal += (storageSlab.Rate * weightConv);
                                                    StorageApplicableon = "Weight";
                                                }

                                                if (tariffId.IsCBMorRate == false)
                                                {

                                                    foreach (var item in containerlist.Where(x => x.IsDelivered == false))
                                                    {
                                                        #region new work for part shipment
                                                        //if (checkpartcontainers == true)
                                                        if (item.IsPartialContainer == true)
                                                        {
                                                            var noofcontainers = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {item.VirNo}   and ContainerNo = {item.ContainerNo} and IsDeleted = 0").ToList();
                                                            if (noofcontainers.Count() > 0)
                                                            {
                                                                storageTotal += item.Size == 20 ? Convert.ToDouble(storageSlab.Rate20) / noofcontainers.Count() : item.Size == 40 ? Convert.ToDouble(storageSlab.Rate40) / noofcontainers.Count() : item.Size == 45 ? Convert.ToDouble(storageSlab.Rate45) / noofcontainers.Count() : 0;
                                                                StorageApplicableon = "Size";
                                                            }

                                                        }
                                                        else
                                                        {

                                                            storageTotal += item.Size == 20 ? storageSlab.Rate20 : item.Size == 40 ? storageSlab.Rate40 : item.Size == 45 ? storageSlab.Rate45 : 0;
                                                            StorageApplicableon = "Size";
                                                        }
                                                        #endregion

                                                        //#region new work for per box rates shipment
                                                        //    storageTotal += item.Size == 20 ? storageSlab.Rate20 : item.Size == 40 ? storageSlab.Rate40 : item.Size == 45 ? storageSlab.Rate45 : 0;
                                                        //    StorageApplicableon = "Size";   
                                                        //#endregion
                                                    }

                                                }
                                            }

                                        }
                                    }

                                }
                            }
                        }
                    }

                    if (storageDays < 0)
                    {
                        storageDays = 0;
                    }


                    //if (deliveredweight > 0 )
                    //{
                    if (invoicestorageTotal > 0)
                    {
                        storageTotal += invoicestorageTotal;
                    }
                    //}

                    storageTotal = Math.Round(storageTotal, 2);




                    var invdys = 0;
                    var invamtss = 0.00;

                    #region stop for after delivered of cargo
                    var gatepss = (from gatepass in Db.OrderDetails
                                   join doitem in Db.DOItems on gatepass.GatePassId equals doitem.GatePassId
                                   where
                                   gatepass.VirNumber == container.VirNo
                                   &&
                                   gatepass.IndexNo == container.IndexNo
                                   &&
                                   doitem.Status == "F"
                                   select doitem.Status
                                 ).ToList();

                    if (gatepss.Count() > 0)
                    {

                        var deliveereddys = (from inovice in Db.Invoices
                                             where container.ContainerIndexId == inovice.ContainerIndexId &&
                                             inovice.BillType == "Normal"
                                             select inovice).LastOrDefault();

                        var invoicgeamt = (from inovice in Db.Invoices
                                           join invoiceitem in Db.InvoiceItems on inovice.InvoiceId equals invoiceitem.InvoiceId
                                           where container.ContainerIndexId == inovice.ContainerIndexId &&
                                           invoiceitem.Description.Contains("Storage Amount")
                                           select invoiceitem).Sum(x => x.Amount);

                        if (invoicgeamt > 0)
                        {
                            invamtss = invoicgeamt;
                        }

                        var perviouswaiverresdel = GetPreviousWaiverCFS(container.ContainerIndexId).Where(x => x.Category == "Storage" && x.Description == "Storage Amount").ToList();

                        if (perviouswaiverresdel.Count() > 0)
                        {

                            var result = perviouswaiverresdel.Where(x => x.Category == "Storage" && x.Description == "Storage Amount").ToList().Sum(x => x.Discount);

                            if (result > 0)
                            {
                                invamtss += result;
                            }

                        }

                        return new StorageTotalViewModel { StorageDays = 0, StorageTotal = invamtss, AddtionalFreeDays = deliveereddys.AdditionalFreeDays ?? 0, TotalFreeDays = deliveereddys.TotalFreeDays ?? 0, TotalBalanceCargo = totalBalanceCargo, StorageFreeDaysType = FreeDaysType, StorageApplicableon = StorageApplicableon, StorageSharePercent = storagesharePercentage, AictPerBoxRate = 0.00 };

                    }
                    #endregion


                    var perviouswaiverres = GetPreviousWaiverCFS(container.ContainerIndexId).Where(x => x.Category == "Storage" && x.Description == "Storage Amount").ToList();

                    if (perviouswaiverres.Count() > 0)
                    {

                        var result = perviouswaiverres.Where(x => x.Category == "Storage" && x.Description == "Storage Amount").ToList().Sum(x => x.Discount);

                        if (result > 0)
                        {
                            storageTotal += result;
                        }

                    }

                    return new StorageTotalViewModel { StorageDays = storageDays, StorageTotal = storageTotal, AddtionalFreeDays = additionalFreeDays, TotalFreeDays = totalFreeDays, TotalBalanceCargo = totalBalanceCargo, StorageFreeDaysType = FreeDaysType, StorageApplicableon = StorageApplicableon, StorageSharePercent = storagesharePercentage, AictPerBoxRate = aictPerBoxRateShare };
                }
                else
                {

                    if (storageDays < 0)
                    {
                        storageDays = 0;
                    }

                    storageTotal = Math.Round(storageTotal, 2);

                    var perviouswaiverresfcl = GetPreviousWaiverCFS(container.ContainerIndexId).Where(x => x.Category == "Storage" && x.Description == "Storage Amount").ToList();

                    if (perviouswaiverresfcl.Count() > 0)
                    {

                        var result = perviouswaiverresfcl.Where(x => x.Category == "Storage" && x.Description == "Storage Amount").ToList().Sum(x => x.Discount);

                        if (result > 0)
                        {
                            storageTotal += result;
                        }

                    }

                    return new StorageTotalViewModel { StorageDays = storageDays, StorageTotal = storageTotal, AddtionalFreeDays = additionalFreeDays, TotalFreeDays = totalFreeDays, TotalBalanceCargo = totalBalanceCargo, StorageFreeDaysType = FreeDaysType, StorageApplicableon = StorageApplicableon, StorageSharePercent = storagesharePercentage, AictPerBoxRate = aictPerBoxRateShare };
                }

            }
            else
            {

                return new StorageTotalViewModel { StorageDays = 0, StorageTotal = 0, AddtionalFreeDays = 0, TotalFreeDays = 0, TotalBalanceCargo = 0, StorageFreeDaysType = "", StorageApplicableon = "", StorageSharePercent = 0, AictPerBoxRate = 0.00 };
            }

        }



        public StorageTotalViewModel GetStorageTotalCY(string igm, int indexNo, DateTime BillDate, DateTime gateInDate, long clearingAgentId, string indexcargotype)
        {
            var storageTotal = 0.00;
            var storageDays = 0;
            var noOfDays = 0;
            var invoiceDays = 0;
            var invoicestorageTotal = 0.00;
            //var totalpartcontainers = 0;
            var totalFreeDays = 0;
            var totalBalanceCargo = 0;
            var freedaysCounted = false;
            var isdgcargo = false;
            var FreeDaysType = "";
            var StorageApplicableon = "";
            var checkstaus = false;
            var storagepplicablebilltype = "";


            var datetime = DateTime.Now;

            int additionalFreeDays = 0;

            long storagesharePercentage = 0;

            double aictPerBoxRateShare = 0.00;


            var container = (from containercy in Db.CYContainers where containercy.VIRNo == igm && containercy.IndexNo == indexNo select containercy).FirstOrDefault();



            if (container != null)
            {
                //if (container.IsPartialContainer == true)
                //{
                //    var partcontainers = (from cycontainer in Db.CYContainers where  cycontainer.VIRNo == container.VIRNo && cycontainer.ContainerNo == container.ContainerNo select cycontainer) .ToList().Count();

                //    totalpartcontainers = partcontainers;
                //}
                if (container.IsDGCargo == true)
                {
                    isdgcargo = true;
                }
                else
                {
                    isdgcargo = false;
                }
            }

            var shippingAget = Db.ShippingAgents.Find(container.ShippingAgentId);


            var containerlist = (from containercy in Db.CYContainers where containercy.VIRNo == igm && containercy.IndexNo == indexNo select containercy).ToList();


            totalBalanceCargo = containerlist.Where(x => x.IsDelivered == false).Count();


            //var isocode = (from isocodes in Db.ISOCodes
            //               where container.ISOCode == isocodes.Code
            //               select isocodes).FirstOrDefault();

            //var isocode = (from isocodes in Db.ISOCodes
            //               from isoCodeHead in Db.ISOCodeHeads.Where(x => x.ISOCodeHeadId == x.ISOCodeHeadId).DefaultIfEmpty()
            //               where container.ISOCode == isocodes.Code
            //               select isoCodeHead).FirstOrDefault();

            var isocode = Db.ISOCodeHeads.FromSql($"select isoCodeHead.ISOCodeHeadId , isoCodeHead.ISOCodeHeadDescription  , isoCodeHead.DeleteDate , isoCodeHead.IsDeleted  from ISOCode   left join isoCodeHead  on ISOCode.ISOCodeHeadId = ISOCodeHead.ISOCodeHeadId  and isoCodeHead.IsDeleted = 0 Where     ISOCode.Code    = {container.ISOCode} and ISOCode.IsDeleted = 0").FirstOrDefault();



            if (clearingAgentId != null || clearingAgentId != 0 && checkstaus == false)
            {
                var clearingAgent = Db.ClearingAgents.FromSql($"select * from ClearingAgent   Where ClearingAgentId    = {clearingAgentId} and IsDeleted = 0").FirstOrDefault();

                if (clearingAgent != null)
                {
                    if (clearingAgent.BillDateTypeCY != null)
                    {
                        storagepplicablebilltype = clearingAgent.BillDateTypeCY;
                        checkstaus = true;
                    }
                }

            }


            if (container.ConsigneId != null && checkstaus == false)
            {
                var consignee = Db.Consignes.FromSql($"select * from Consigne   Where ConsigneId    = {container.ConsigneId} and IsDeleted = 0").FirstOrDefault();

                if (consignee != null)
                {
                    if (consignee.BillDateTypeCY != null)
                    {
                        storagepplicablebilltype = consignee.BillDateTypeCY;
                        checkstaus = true;
                    }
                }

            }

            if (shippingAget != null && checkstaus == false)
            {
                if (shippingAget.BillDateTypeCY != null)
                {
                    storagepplicablebilltype = shippingAget.BillDateTypeCY;
                    checkstaus = true;
                }

            }

            var tarifList = new List<Tariff>();


            var res = GetTariffAllList(container.ShippingAgentId, isocode.ISOCodeHeadId, container.ConsigneId, clearingAgentId, container.ShipperId, container.GoodsHeadId, container.PortAndTerminalId, indexcargotype);

            if (res > 0)
            {

                var tarifListres = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                    join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId
                                    //join tariffHead in Db.TariffHeads on tariff.TariffHeadId equals tariffHead.TariffHeadId
                                    where
                                    tariffInofrmationTariff.TariffInformationId == res
                                    //&& tariff.TariffType == "CY"
                                    && tariff.TariffHead.Name.Contains("STORAGE")
                                    && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                    &&
                                       (
                                       tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                       tariff.TypeOfImplementation == "Arrival" ?
                                       Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                       tariff.TypeOfImplementation == "Delivery" ?
                                       Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                       tariff.ImplementFrom == null
                                       )
                                       &&
                                       (
                                       tariff.TypeOfImplementation == "All" ?
                                       tariff.ImplementTo == null :
                                       tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                       Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                       tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                       Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                       tariff.ImplementTo == null
                                       )
                                    select tariff).ToList();

                tarifList.AddRange(tarifListres);

            }



            if (res > 0)
            {
                var indexcontainersdata = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {container.VIRNo} and IndexNo = {container.IndexNo} and IsDeleted = 0").ToList();


                foreach (var item in indexcontainersdata)
                {

                    var resdataoftariffPerBoxRate = (from tariffPerBoxRate in Db.TariffPerBoxRates
                                                     where
                                                      tariffPerBoxRate.TariffInformationId == res
                                                     && (tariffPerBoxRate.PortAndTerminalId != null ? tariffPerBoxRate.PortAndTerminalId == container.PortAndTerminalId : tariffPerBoxRate.PortAndTerminalId == null)
                                                      && (tariffPerBoxRate.Rate20 > 0 || tariffPerBoxRate.Rate40 > 0 || tariffPerBoxRate.Rate45 > 0)
                                                     &&
                                                      (
                                                      tariffPerBoxRate.TypeOfImplementation == "All" ? tariffPerBoxRate.ImplementFrom == null :
                                                      tariffPerBoxRate.TypeOfImplementation == "Arrival" ?
                                                      Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariffPerBoxRate.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                      tariffPerBoxRate.TypeOfImplementation == "Delivery" ?
                                                      Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariffPerBoxRate.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                                      tariffPerBoxRate.ImplementFrom == null
                                                      )
                                                      &&
                                                      (
                                                      tariffPerBoxRate.TypeOfImplementation == "All" ?
                                                      tariffPerBoxRate.ImplementTo == null :
                                                      tariffPerBoxRate.TypeOfImplementation == "Arrival" && tariffPerBoxRate.ImplementTo != null ?
                                                      Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariffPerBoxRate.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                      tariffPerBoxRate.TypeOfImplementation == "Delivery" && tariffPerBoxRate.ImplementTo != null ?
                                                      Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariffPerBoxRate.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                                      tariffPerBoxRate.ImplementTo == null
                                                      )

                                                     select new InvoiceItemViewModel
                                                     {
                                                         Amount = item.Size == 20 ? tariffPerBoxRate.Rate20 : item.Size == 40 ? tariffPerBoxRate.Rate40 : item.Size == 45 ? tariffPerBoxRate.Rate45 : 0,
                                                     }).ToList();

                    aictPerBoxRateShare += resdataoftariffPerBoxRate.Sum(x => x.Amount);

                }
            }




            if (tarifList.Count() > 0)
            {
                //var das = getStoragefreeDays(container.ShippingAgentId, container.ConsigneId, clearingAgentId, container.GoodsHeadId, tarifList[0].TariffId);
                var das = getStoragefreeDays(container.ShippingAgentId, container.ConsigneId, clearingAgentId, container.GoodsHeadId, res, isdgcargo, "CY");

                additionalFreeDays = Convert.ToInt32(das.NoofFreeDays);

                FreeDaysType = das.FreeDaysType;

                foreach (var item in tarifList)
                {
                    var storagepercent = Db.StorageShareRates.FromSql($"select  * from StorageShareRate where TariffInformationId = {res} and TariffId = {item.TariffId} and  IsDeleted = 0").LastOrDefault();

                    if (storagepercent != null)
                    {
                        storagesharePercentage += storagepercent.ShareRate;
                    }
                }

            }

            //var relationTariff = (from tariff in Db.Tariffs
            //                      join tariffInfo in Db.TariffInformations on tariff.TariffId equals tariffInfo.TariffId
            //                      where
            //                        tariff.TariffType == "CY"
            //                          && (tariffInfo.GoodsHeadId == container.GoodsHeadId || tariffInfo.GoodsHeadId == null)
            //                         && (tariffInfo.ShipperId == container.ShipperId || tariffInfo.ShipperId == null)
            //                         && (tariffInfo.ClearingAgentId == clearingAgentId || tariffInfo.ClearingAgentId == null)
            //                         && (tariffInfo.ConsigneId == container.ConsigneId || tariffInfo.ConsigneId == null)
            //                         && (tariffInfo.PortAndTerminalId == container.PortAndTerminalId || tariffInfo.PortAndTerminalId == null)
            //                         && (tariffInfo.ShippingAgentId == container.ShippingAgentId || tariffInfo.ShippingAgentId == null)
            //                         && (tariffInfo.ContainerType == isocode.Descrition || tariffInfo.ContainerType == null)

            //                      && tariff.TariffHead.Name.Contains("STORAGE")
            //                      select tariff).ToList();



            var nooffreedays = 0;


            nooffreedays = additionalFreeDays;


            //new work
            //var firstFreedays = (from storageFreeDays in Db.StorageFreeDays
            //                     where
            //                           (storageFreeDays.GoodsHeadId == container.GoodsHeadId || storageFreeDays.GoodsHeadId == null)
            //                        && (storageFreeDays.ShipperId == container.ShipperId || storageFreeDays.ShipperId == null)
            //                        && (storageFreeDays.ClearingAgentId == clearingAgentId || storageFreeDays.ClearingAgentId == null)
            //                        && (storageFreeDays.ConsigneId == container.ConsigneId || storageFreeDays.ConsigneId == null)
            //                        && (storageFreeDays.PortAndTerminalId == container.PortAndTerminalId || storageFreeDays.PortAndTerminalId == null)
            //                        && (storageFreeDays.ShippingAgentId == container.ShippingAgentId || storageFreeDays.ShippingAgentId == null)
            //                        && (storageFreeDays.ISOCodeHeadId == isocode.ISOCodeHeadId || storageFreeDays.ISOCodeHeadId == null)
            //                        && (storageFreeDays.IndexCargoType == indexcargotype || storageFreeDays.IndexCargoType == null)

            //                     select storageFreeDays).FirstOrDefault();

            //if (firstFreedays != null)
            //{
            //    nooffreedays = Convert.ToInt32(firstFreedays.StorageFreeDays);
            //}
            //else
            //{
            //    nooffreedays = 5;
            //}

            nooffreedays = (nooffreedays - 1);


            //  var noOfDays = Convert.ToInt32((BillDate - gatein.GateInDateTime.Value.AddDays(3)).TotalDays);

            if (storagepplicablebilltype == "VesselArrival")
            {

                //noOfDays = Convert.ToInt32((BillDate - destuff.TellySheet.DestuffDate.Value.AddDays(3)).TotalDays);
                noOfDays = Convert.ToInt32((BillDate.Date - container.BerthOn.Value.AddDays((nooffreedays)).Date).Days);


                totalFreeDays = Convert.ToInt32((BillDate.Date - container.BerthOn.Value.AddDays((-1)).Date).Days);




            }
            if (storagepplicablebilltype == "GateIn")
            {

                //noOfDays = Convert.ToInt32((BillDate - container.CFSContainerGateInDate.Value.AddDays(3)).TotalDays);

                noOfDays = Convert.ToInt32((BillDate.Date - gateInDate.AddDays((nooffreedays)).Date).Days);

                totalFreeDays = Convert.ToInt32((BillDate.Date - gateInDate.AddDays((-1)).Date).Days);

            }

            #region stop for after delivered of cargo
            var gatepss = (from gatepass in Db.OrderDetails
                           join doitem in Db.DOItems on gatepass.GatePassId equals doitem.GatePassId
                           where
                           gatepass.VirNumber == container.VIRNo
                           &&
                           gatepass.IndexNo == container.IndexNo
                           &&
                           doitem.Status == "F"
                           select doitem.Status
                         ).ToList();

            if (gatepss.Count() > 0)
            {

                var delivereddys = (from inovice in Db.Invoices
                                    where container.CYContainerId == inovice.CYContainerId &&
                                    inovice.BillType == "Normal"
                                    select inovice).LastOrDefault();



                var invoicgeamt = (from inovice in Db.Invoices
                                   join invoiceitem in Db.InvoiceItems on inovice.InvoiceId equals invoiceitem.InvoiceId
                                   where container.CYContainerId == inovice.CYContainerId &&
                                   invoiceitem.Description.Contains("Storage Amount")
                                   select invoiceitem).Sum(x => x.Amount);

                if (invoicgeamt > 0)
                {
                    invoicestorageTotal = invoicgeamt;
                }


                var perviouswaiverres = GetPreviousWaiverCY(container.CYContainerId).Where(x => x.Category == "Storage").ToList();

                if (perviouswaiverres.Count() > 0)
                {

                    var result = perviouswaiverres.Where(x => x.Category == "Storage").ToList().Sum(x => x.Discount);

                    if (result > 0)
                    {
                        invoicestorageTotal += result;
                    }

                }

                return new StorageTotalViewModel { StorageDays = 0, StorageTotal = invoicestorageTotal, AddtionalFreeDays = delivereddys.AdditionalFreeDays ?? 0, TotalFreeDays = delivereddys.TotalFreeDays ?? 0, TotalBalanceCargo = totalBalanceCargo, StorageFreeDaysType = FreeDaysType, StorageApplicableon = "Size", StorageSharePercent = 0, AictPerBoxRate = 0.00 };

            }
            #endregion



            List<StorageDaysViewModel> storageDayslist = new List<StorageDaysViewModel>();

            for (int i = 0; i < noOfDays; i++)
            {
                var storageDay = new StorageDaysViewModel
                {

                    IndexNo = i,
                    Status = "UnDelivered"
                };
                storageDayslist.Add(storageDay);
            }


            if (container != null)
            {

                var invoicesstoragedays = (from inovice in Db.Invoices
                                           where container.CYContainerId == inovice.CYContainerId &&
                                           inovice.BillType == "Normal"
                                           select inovice).Sum(x => x.StorageDays);

                if (invoicesstoragedays > 0)
                {
                    invoiceDays = invoicesstoragedays;
                }

                var invoicesstorageamount = (from inovice in Db.Invoices
                                             join invoiceitem in Db.InvoiceItems on inovice.InvoiceId equals invoiceitem.InvoiceId
                                             where container.CYContainerId == inovice.CYContainerId &&
                                             invoiceitem.Description.Contains("Storage Amount")
                                             select invoiceitem).Sum(x => x.Amount);


                if (invoicesstorageamount > 0)
                {
                    invoicestorageTotal = invoicesstorageamount;
                }



            }


            if (invoiceDays > 0)
            {

                for (int i = 0; i < invoiceDays; i++)
                {
                    storageDayslist.Where(x => x.Status == "UnDelivered").FirstOrDefault().Status = "Delivered";
                }

            }


            storageDays = storageDayslist.Where(x => x.Status == "UnDelivered").Count();

            //storageDayslist.Where(x=> x.Status == "UnDelivered").Count()

            if (tarifList.Count() > 0)
            {
                foreach (var tariff in tarifList)
                {
                    var storageSlabs = Db.StorageSlabs.Where(c => c.TariffId == tariff.TariffId).ToList();


                    if (storageSlabs.Count() > 0)
                    {


                        if (!freedaysCounted)
                        {
                            //noOfDays -= storageSlabs.FirstOrDefault().FreeDays;

                            //storageDays = noOfDays;

                            freedaysCounted = true;

                        }

                        if (storageDayslist.Where(x => x.Status == "UnDelivered").Count() < 0)
                        {
                            noOfDays = 0;

                            storageDays = noOfDays;
                        }


                        foreach (var storageSlab in storageSlabs)
                        {
                            if (noOfDays <= 0)
                                break;

                            if (!storageSlab.NoOfDays.ToUpper().Contains("OVER"))
                            {
                                var period = (storageDayslist.Count()) - (Convert.ToInt32(storageSlab.NoOfDays) > 0 ? Convert.ToInt32(storageSlab.NoOfDays) : storageDayslist.Count());

                                if (period > Convert.ToInt32(storageSlab.NoOfDays) || storageDayslist.Count() > Convert.ToInt32(storageSlab.NoOfDays))
                                    period = Convert.ToInt32(storageSlab.NoOfDays);
                                else if (period < Convert.ToInt32(storageSlab.NoOfDays))
                                    period = storageDayslist.Count();


                                var takeitems = storageDayslist.Take(period).Select(x => x).ToList();


                                foreach (var itemstor in takeitems)
                                {
                                    if (itemstor.Status == "UnDelivered")
                                    {
                                        if (tariff.IsCBMorRate == false)
                                        {
                                            foreach (var item in containerlist.Where(x => x.IsDelivered == false))
                                            {
                                                if (item.IsPartialContainer == true)
                                                {
                                                    var reslist = Db.CYContainers.FromSql($"select  * from  CYContainer where VIRNo = {item.VIRNo} and ContainerNo = {item.ContainerNo}  and  IsDeleted = 0 ").ToList().Count();

                                                    storageTotal += item.Size == 20 ? storageSlab.Rate20 / reslist : item.Size == 40 ? storageSlab.Rate40 / reslist : item.Size == 45 ? storageSlab.Rate45 / reslist : 0;

                                                }
                                                else
                                                {
                                                    storageTotal += item.Size == 20 ? storageSlab.Rate20 : item.Size == 40 ? storageSlab.Rate40 : item.Size == 45 ? storageSlab.Rate45 : 0;
                                                }

                                            }
                                        }

                                    }

                                    storageDayslist.RemoveAll(x => x.IndexNo == itemstor.IndexNo);
                                }


                                //foreach (var item in takeitems)
                                //{
                                //    storageDayslist.RemoveAll(x => x.IndexNo == item.IndexNo);
                                //}

                                if (storageDayslist.Count() < Convert.ToInt32(storageSlab.NoOfDays))
                                {
                                    noOfDays -= period;

                                }

                                else
                                {
                                    noOfDays -= Convert.ToInt32(storageSlab.NoOfDays);

                                }

                            }
                            else
                            {
                                //  for (int i = 0; i < noOfDays; i++)
                                //{
                                foreach (var itemstor in storageDayslist)
                                {
                                    if (itemstor.Status == "UnDelivered")
                                    {
                                        if (tariff.IsCBMorRate == false)
                                        {
                                            foreach (var item in containerlist.Where(x => x.IsDelivered == false))
                                            {
                                                if (item.IsPartialContainer == true)
                                                {
                                                    var reslist = Db.CYContainers.FromSql($"select  * from  CYContainer where VIRNo = {item.VIRNo} and ContainerNo = {item.ContainerNo}  and  IsDeleted = 0 ").ToList().Count();

                                                    storageTotal += item.Size == 20 ? storageSlab.Rate20 / reslist : item.Size == 40 ? storageSlab.Rate40 / reslist : item.Size == 45 ? storageSlab.Rate45 / reslist : 0;
                                                }
                                                else
                                                {
                                                    storageTotal += item.Size == 20 ? storageSlab.Rate20 : item.Size == 40 ? storageSlab.Rate40 : item.Size == 45 ? storageSlab.Rate45 : 0;
                                                }

                                            }
                                        }
                                    }
                                }

                                // }
                            }



                        }
                    }


                }

                if (storageDays < 0)
                {
                    storageDays = 0;
                }

                //if (totalpartcontainers > 0 && storageTotal > 0)
                //{
                //    storageTotal = storageTotal / totalpartcontainers;
                //}

                // var r = containerlist.Find( x => x.IsDelivered == true);

                //if (r != null)
                //{
                if (invoicestorageTotal > 0)
                {
                    storageTotal += invoicestorageTotal;
                }
                // }




                storageTotal = Math.Round(storageTotal, 2);

                var perviouswaiverresdata = GetPreviousWaiverCY(container.CYContainerId).Where(x => x.Category == "Storage").ToList();

                if (perviouswaiverresdata.Count() > 0)
                {

                    var result = perviouswaiverresdata.Where(x => x.Category == "Storage").ToList().Sum(x => x.Discount);

                    if (result > 0)
                    {
                        storageTotal += result;
                    }

                }

                return new StorageTotalViewModel { StorageDays = storageDays, StorageTotal = storageTotal, AddtionalFreeDays = additionalFreeDays, TotalFreeDays = totalFreeDays, TotalBalanceCargo = totalBalanceCargo, StorageFreeDaysType = FreeDaysType, StorageApplicableon = "Size", StorageSharePercent = storagesharePercentage, AictPerBoxRate = aictPerBoxRateShare };


            }

            //if(totalpartcontainers > 0)
            //{
            //    storageTotal =  storageTotal / totalpartcontainers;
            //}


            if (storageDays < 0)
            {
                storageDays = 0;
            }

            storageTotal = Math.Round(storageTotal, 2);


            var perviouswaiver = GetPreviousWaiverCY(container.CYContainerId).Where(x => x.Category == "Storage").ToList();

            if (perviouswaiver.Count() > 0)
            {

                var result = perviouswaiver.Where(x => x.Category == "Storage").ToList().Sum(x => x.Discount);

                if (result > 0)
                {
                    storageTotal += result;
                }

            }

            return new StorageTotalViewModel { StorageDays = storageDays, StorageTotal = storageTotal, AddtionalFreeDays = additionalFreeDays, TotalFreeDays = totalFreeDays, TotalBalanceCargo = totalBalanceCargo, StorageFreeDaysType = FreeDaysType, StorageApplicableon = "Size", StorageSharePercent = storagesharePercentage, AictPerBoxRate = aictPerBoxRateShare };
        }



        public long GetTariffAllList(long? ShippingAgentId, long? ISOCodeHeadId, long? ConsigneId, long? clearingAgentId, long? ShipperId, long? GoodsHeadId, long? PortAndTerminalId, string indexcargotype)
        {
            long tariifinfoId = 0;

            #region MyRegion
            if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId  is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && ISOCodeHeadId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && ISOCodeHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId  is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && ShipperId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is  null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }

            }
            if (ShippingAgentId != null && ConsigneId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && ISOCodeHeadId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && clearingAgentId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId ={ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && ISOCodeHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && ShipperId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && GoodsHeadId != null && clearingAgentId != null && ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && PortAndTerminalId != null && clearingAgentId != null && ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && clearingAgentId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && clearingAgentId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && GoodsHeadId != null && clearingAgentId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && ShipperId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId  is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && GoodsHeadId != null && ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && GoodsHeadId != null && ISOCodeHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && ISOCodeHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId ={PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && clearingAgentId != null && ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && clearingAgentId != null && ISOCodeHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && clearingAgentId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && clearingAgentId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ShipperId != null && ISOCodeHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null  and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ShipperId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ConsigneId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && clearingAgentId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && ISOCodeHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShippingAgentId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && clearingAgentId != null && ISOCodeHeadId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && ShipperId != null && ISOCodeHeadId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && clearingAgentId != null && ShipperId != null && ISOCodeHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && clearingAgentId != null && ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && clearingAgentId != null && ISOCodeHeadId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && clearingAgentId != null && ISOCodeHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && clearingAgentId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && ConsigneId != null && ShipperId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && ConsigneId != null && ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}   and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}   and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && ISOCodeHeadId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}   and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && clearingAgentId != null && ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null   and IsDeleted = 0  ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && clearingAgentId != null && ISOCodeHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null   and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && clearingAgentId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && clearingAgentId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0  ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();


                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && ShipperId != null && ISOCodeHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();


                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && ShipperId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0  ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();


                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();


                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && ISOCodeHeadId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0  ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();


                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && ISOCodeHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0  ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();


                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && PortAndTerminalId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0  ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();


                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && clearingAgentId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0  ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();


                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();


                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && ISOCodeHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0  ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();


                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();


                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();


                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ConsigneId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();


                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (GoodsHeadId != null && clearingAgentId != null && ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (GoodsHeadId != null && clearingAgentId != null && ISOCodeHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId} and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (GoodsHeadId != null && clearingAgentId != null && ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (PortAndTerminalId != null && clearingAgentId != null && ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (GoodsHeadId != null && clearingAgentId != null && ISOCodeHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (PortAndTerminalId != null && clearingAgentId != null && ISOCodeHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (PortAndTerminalId != null && clearingAgentId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (clearingAgentId != null && ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (clearingAgentId != null && ISOCodeHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (clearingAgentId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (clearingAgentId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (clearingAgentId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && ShipperId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShipperId != null && ISOCodeHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId ={ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShipperId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShipperId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ShipperId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (ISOCodeHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null  and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (GoodsHeadId != null && PortAndTerminalId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId ={PortAndTerminalId} and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (GoodsHeadId != null && indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype && x.IsEnableDisabled == false).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }
            if (indexcargotype != null)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From  TariffInformation Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null  and PortAndTerminalId is null and IsDeleted = 0 ").Where(x => x.IndexCargoType == indexcargotype).LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TariffInformationId;

                    return tariifinfoId;
                }
            }


            #endregion

            return tariifinfoId;
        }


        //storage Slab for CFS All Index  wise 

        //  public StorageTotalViewModel GetStorageTotalIndexWise(long indexNo, string virno, DateTime BillDate)
        public StorageTotalViewModel GetStorageTotalIndexWise(long ContainerIndexId, DateTime BillDate, int Weight, double CBM)
        {
            var storageTotalRateWise = 0;

            var storageTotalCbmWeightWise = 0.0;

            var storageTotal = 0.0;

            var storageDays = 0;

            int TotalCotainers = 0;

            var noOfDays = 0;

            var freedaysCounted = false;

            var container = Db.ContainerIndices.Find(ContainerIndexId);

            if (container != null)
            {

                TotalCotainers = Db.ContainerIndices.Where(x => x.BLNo == container.BLNo).Count();


            }

            var shippingAget = Db.ShippingAgents.Find(container.ShippingAgentId);


            var destuff = Db.DestuffedContainers.Include(c => c.TellySheet)
                .FirstOrDefault(c => c.ContainerIndexId == ContainerIndexId);


            if (destuff != null && destuff.TellySheet != null && destuff.TellySheet.DestuffDate != null)
            {
                var calculateCBM = CBM > 0 ? CBM : destuff.CBM ?? 0;

                var calculateWeight = Weight > 0 ? Weight : destuff.FoundWeight;

                var weightOrCBM = WeightGreaterThenCBM(calculateWeight ?? 0, calculateCBM);

                var weightConv = (Convert.ToDouble(calculateWeight) / 1000.0);

                var AgentTariffIds = (from tariff in Db.Tariffs
                                      join agentTariff in Db.ShippingAgentTariffs on tariff.TariffId equals agentTariff.TariffId
                                      where
                                      agentTariff.ShippingAgentId == container.ShippingAgentId && tariff.TariffHead.Name.Contains("STORAGE")
                                      &&
                                    !Db.DisabledAgentTariffs.Any(s => s.ContainerIndexId == ContainerIndexId && s.ShippingAgentTariffId == agentTariff.ShippingAgentTariffId)
                                      select tariff).ToList();


                var TariffIds = (from tariff in Db.Tariffs
                                 join igmTariff in Db.ContainerIndexTariffs on tariff.TariffId equals igmTariff.TariffId
                                 where igmTariff.ContainerIndexId == ContainerIndexId && tariff.TariffHead.Name.Contains("STORAGE")
                                 select tariff).ToList();

                TariffIds = TariffIds.Concat(AgentTariffIds).ToList();

                var cbm = destuff.CBM ?? 0;

                //var distinctTariffIds = TariffIds.Distinct<long>();

                // 5/3/ 2021 var noOfDays = Convert.ToInt32((BillDate - destuff.TellySheet.DestuffDate.Value.AddDays(3)).TotalDays);


                if (shippingAget.BillDateType == "Destuffed")
                {
                    // noOfDays = Convert.ToInt32((BillDate - destuff.TellySheet.DestuffDate.Value.AddDays(3)).TotalDays);

                    noOfDays = Convert.ToInt32((BillDate.Date - destuff.TellySheet.DestuffDate.Value.AddDays(4).Date).Days);


                }
                if (shippingAget.BillDateType == "GateIn")
                {
                    // noOfDays = Convert.ToInt32((BillDate - container.CFSContainerGateInDate.Value.AddDays(3)).TotalDays);

                    noOfDays = Convert.ToInt32((BillDate.Date - container.CFSContainerGateInDate.Value.AddDays(4).Date).Days);


                }

                if (container.AdditionalFreeDays > 0)
                {

                    noOfDays = noOfDays - container.AdditionalFreeDays;

                }

                storageDays = noOfDays;

                foreach (var tariffId in TariffIds)
                {
                    var storageSlabs = Db.StorageSlabs.Where(c => c.TariffId == tariffId.TariffId).ToList();

                    if (!freedaysCounted)
                    {
                        noOfDays -= storageSlabs.FirstOrDefault().FreeDays;

                        storageDays = noOfDays;

                        freedaysCounted = true;

                    }

                    if (noOfDays < 0)
                    {
                        noOfDays = 0;

                        storageDays = noOfDays;
                    }

                    var unitToMultiply = weightOrCBM == 1 ? weightConv : calculateCBM;

                    foreach (var storageSlab in storageSlabs)
                    {
                        if (noOfDays <= 0)
                            break;

                        if (!storageSlab.NoOfDays.ToUpper().Contains("OVER"))
                        {
                            var period = (noOfDays - Convert.ToInt32(storageSlab.NoOfDays)) > 0 ? Convert.ToInt32(storageSlab.NoOfDays) : noOfDays;


                            for (int i = 0; i < period; i++)
                            {
                                if (tariffId.IsCBMorRate == true)
                                {
                                    storageTotalCbmWeightWise += (storageSlab.Rate * unitToMultiply);
                                }
                                else
                                {
                                    storageTotalRateWise += container.Size == 20 ? storageSlab.Rate20 : container.Size == 40 ? storageSlab.Rate40 : container.Size == 45 ? storageSlab.Rate45 : 0;

                                }

                            }

                            noOfDays -= Convert.ToInt32(storageSlab.NoOfDays);
                        }
                        else
                        {
                            for (int i = 0; i < noOfDays; i++)
                            {
                                if (tariffId.IsCBMorRate == true)
                                {
                                    storageTotalCbmWeightWise += (storageSlab.Rate * unitToMultiply);
                                }
                                else
                                {
                                    storageTotalRateWise += container.Size == 20 ? storageSlab.Rate20 : container.Size == 40 ? storageSlab.Rate40 : container.Size == 45 ? storageSlab.Rate45 : 0;

                                }
                            }
                        }
                    }

                }

            }

            if (TotalCotainers > 0 && storageTotalRateWise > 0)
            {
                storageTotal = (Convert.ToInt32(TotalCotainers) * Convert.ToInt32(storageTotalRateWise))
                    + (storageTotalCbmWeightWise);
            }
            else
            {
                storageTotal = Convert.ToInt32(storageTotalRateWise) + (storageTotalCbmWeightWise);
            }


            return new StorageTotalViewModel { StorageDays = storageDays, StorageTotal = storageTotal };
        }

        public List<InvoiceItemViewModel> GetIndexTotal(long ContainerIndexId)
        {
            var totalItems = new List<InvoiceItemViewModel>();

            var agentitems = (from tariff in Db.Tariffs
                              join agentTariff in Db.ShippingAgentTariffs on tariff.TariffId equals agentTariff.TariffId
                              join containerIndex in Db.ContainerIndices on agentTariff.ShippingAgentId equals containerIndex.ShippingAgentId
                              where containerIndex.ContainerIndexId == ContainerIndexId && tariff.PerIndexRate > 0
                              && !Db.DisabledAgentTariffs.Any(s => s.ContainerIndexId == ContainerIndexId && s.ShippingAgentTariffId == agentTariff.ShippingAgentTariffId)

                              select new InvoiceItemViewModel
                              {
                                  Description = tariff.TariffHead.Name,
                                  Amount = tariff.PerIndexRate,
                                  TariffId = tariff.TariffId
                              }).Distinct().ToList();

            var igmItems = (from tariff in Db.Tariffs
                            join igmTariff in Db.ContainerIndexTariffs on tariff.TariffId equals igmTariff.TariffId
                            join containerIndex in Db.ContainerIndices on igmTariff.ContainerIndexId equals containerIndex.ContainerIndexId
                            where containerIndex.ContainerIndexId == ContainerIndexId && tariff.PerIndexRate > 0
                            select new InvoiceItemViewModel
                            {
                                Description = tariff.TariffHead.Name,
                                Amount = tariff.PerIndexRate,
                                TariffId = tariff.TariffId
                            }).Distinct().ToList();

            totalItems.AddRange(igmItems);
            totalItems.AddRange(agentitems);

            return totalItems;
        }

        internal double WeightGreaterThenCBM(double Weight, double CBM)
        {
            var CBMToWeight = CBM * 1000;

            if (CBMToWeight < Weight)
                return 1;

            return 0;
        }

        public InvoiceViewModel GetGeneratedInvoice(string BillNo)
        {
            var inv = (from invoice in Db.Invoices
                       join index in Db.ContainerIndices on invoice.ContainerIndexId equals index.ContainerIndexId
                       join voyage in Db.Voyages on index.VoyageId equals voyage.VoyageId
                       where invoice.InvoiceNo == BillNo
                       select new InvoiceViewModel
                       {
                           InvoiceId = invoice.InvoiceId,
                           BillNo = invoice.InvoiceNo,
                           CBM = invoice.CBM,
                           //Consignee = invoice.Consignee,
                           ContainerNo = index.ContainerNo,
                           DestuffDate = invoice.DestuffDate,
                           GateInDate = invoice.GateInDate,
                           IGM = voyage.VIRNo,
                           InvoiceDate = invoice.InvoiceDate,
                           Size = invoice.Size,
                           IsAdvanceBill = invoice.IsAdvanceBill,
                           // ClearingAgentId = invoice.ClearingAgentId ?? 0,
                           //TotalAmount = invoice.TotalAmount,
                           StorageTotal = invoice.StorageTotal
                           //GrandTotal = invoice.GrandTotal
                       }).FirstOrDefault();

            return inv;
        }

        public InvoiceViewModel GetGeneratedInvoiceCY(string BillNo)
        {
            var inv = (from invoice in Db.Invoices
                       join container in Db.CYContainers on invoice.CYContainerId equals container.CYContainerId
                       where invoice.InvoiceNo == BillNo
                       select new InvoiceViewModel
                       {
                           InvoiceId = invoice.InvoiceId,
                           BillNo = invoice.InvoiceNo,
                           CBM = invoice.CBM,
                           //Consignee = invoice.Consignee,
                           ContainerNo = container.ContainerNo,
                           DestuffDate = invoice.DestuffDate,
                           GateInDate = invoice.GateInDate,
                           IGM = container.VIRNo,
                           InvoiceDate = invoice.InvoiceDate,
                           Size = invoice.Size,
                           IsAdvanceBill = invoice.IsAdvanceBill,
                           //ClearingAgentId = invoice.ClearingAgentId ?? 0,
                           //TotalAmount = invoice.TotalAmount,
                           StorageTotal = invoice.StorageTotal
                           // GrandTotal = invoice.GrandTotal


                       }).FirstOrDefault();

            return inv;
        }



        public InvoiceViewModel GetInvoice(long ContainerIndexId)
        {

            double examount = 0.00;
            var resdata = Db.ExchangeRates.FromSql($"select * from ExchangeRate where  IsDeleted = 0  ").LastOrDefault();

            if (resdata != null)
            {
                examount = resdata.ExchangeRateAmount;

            }

            var invoice = (from containerIndex in Db.ContainerIndices
                           from gdio in Db.GDIOs.Where(s => s.VIRNumber == containerIndex.VirNo && s.IndexNumber == containerIndex.IndexNo).DefaultIfEmpty()
                           from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerIndex.ShippingAgentId).DefaultIfEmpty()
                               //from gatein in Db.GateIns.Where(s => s.VIRNo == containerIndex.VirNo && s.ContainerNo == containerIndex.ContainerNo).DefaultIfEmpty()
                           from consignee in Db.Consignes.Where(s => s.ConsigneId == containerIndex.ConsigneId).DefaultIfEmpty()
                           from good in Db.GoodsHeads.Where(s => s.GoodsHeadId == containerIndex.GoodsHeadId).DefaultIfEmpty()
                           from shipper in Db.Shippers.Where(s => s.ShipperId == containerIndex.ShipperId).DefaultIfEmpty()
                           from portAndTerminal in Db.PortAndTerminals.Where(s => s.PortAndTerminalId == containerIndex.PortAndTerminalId).DefaultIfEmpty()
                               ///from billtoline in Db.BillToLines.Where(s => s.ContainerIndexId == containerIndex.ContainerIndexId &&  s.InoviceCreated == false).DefaultIfEmpty()
                           from destuff in Db.DestuffedContainers.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                           from saleTax in Db.SalesTaxes.Where(x => x.Type == "Import").DefaultIfEmpty()
                           from examinationRequest in Db.ExaminationRequests.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                           from isocode in Db.ISOCodes.Where(x => x.Code == containerIndex.ISOCode).DefaultIfEmpty()

                           where containerIndex.ContainerIndexId == ContainerIndexId
                           select new InvoiceViewModel
                           {
                               ContainerIndexId = containerIndex.ContainerIndexId,
                               Description = containerIndex.Description,
                               ContainerNo = containerIndex.ContainerNo,
                               GateInDate = GetCFSContainerGateInDate(ContainerIndexId),
                               //DestuffDate = destuff.TellySheet != null ? destuff.TellySheet.DestuffDate : null,
                               DestuffDate = GetDestuffingDate(ContainerIndexId),
                               // AdditionalFreeDays = FreeDaysCount("CFS" , ContainerIndexId),
                               OtherCharges = GetAdditionalChargesTotal(ContainerIndexId),
                               ShippingAgentId = shippingAgent != null ? shippingAgent.ShippingAgentId : 0,
                               ChargesName = shippingAgent != null ? shippingAgent.ChargesName : "",
                               ShippingAgentName = shippingAgent != null ? shippingAgent.Name : "",
                               VehcileAmountAllow = shippingAgent != null ? shippingAgent.VehcileAmountAllow : "",
                               ConsigneeNameIGM = consignee != null ? consignee.ConsigneName : "",
                               ConsigneeNTNNumber = consignee != null ? consignee.ConsigneNTN : "",
                               Good = good != null ? good.GoodsHeadName : "",
                               Shipper = shipper != null ? shipper.ShipperName : "",
                               PortAndTerminal = portAndTerminal != null ? portAndTerminal.PortName : "",
                               IndexCargoType = containerIndex.CargoType,
                               //BillToLineNumber = GetBillToLineNumber(ContainerIndexId),
                               WaiVerAmount = GetWaiverTotal(ContainerIndexId),
                               LastWaiverRemarks = GetWaiverLastRemarksCFS(ContainerIndexId),
                               PreviousWaiVerAmount = GetPreviousWaiverTotalCFS(ContainerIndexId),
                               SalesTax = saleTax != null ? saleTax.SalesTaxAmount : 0,
                               SealChargers = 0,
                               ClearingAgentId = examinationRequest != null ? examinationRequest.ClearingAgentId ?? 0 : 0,
                               CNIC = examinationRequest != null ? examinationRequest.CNIC : "",
                               PhoneNumber = examinationRequest != null ? examinationRequest.PhoneNumber : "",
                               ClearingAgentRep = examinationRequest != null ? examinationRequest.ClearingAgentRep : "",
                               ISOCodeDesc = isocode != null ? isocode.Descrition : "",
                               CBM = containerIndex.CBM,
                               FFCBM = containerIndex.FFCBM,
                               MeasurementCBM = containerIndex.MeasurementCBM,
                               ExaminationRequestCBM = examinationRequest != null ? examinationRequest.ExaminationRequestCBM : 0,
                               ClearingAgentRegNumber = examinationRequest != null ? examinationRequest.CustomRegNo : "",
                               //Weight = Db.DestuffedContainers.Where(s => s.ContainerIndex.VoyageId == containerIndex.VoyageId && s.ContainerIndex.IndexNo == containerIndex.IndexNo).Sum(s => s.FoundWeight),
                               Weight = containerIndex.ManifestedWeight * 1000,
                               // PortCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VirNo && s.IndexNumber == containerIndex.IndexNo).Sum(s => s.TotalCharges),
                               PreviousBillAmount = GetInvoiceTotal(ContainerIndexId),
                               PreviousBillTotalCharges = GetInvoiceTotalCharges(ContainerIndexId, "CFS"),

                               //PreviousPaidGST = GetPreviousPaidGST(ContainerIndexId, "CFS"),

                               PreviousPaidBillToLineAmount = GetPreviousPaidBillToLineAmount(ContainerIndexId, "CFS"),
                               //PreviousPaidBillToLineAmountGST = GetPreviousPaidBillToLineAmountGST(ContainerIndexId, "CFS"),
                               //PreviousPaidBillToLineAmountGrandTotal = GetPreviousPaidBillToLineAmountGrandTotal(ContainerIndexId , "CFS"),



                               Consignee = gdio != null ? gdio.ConsigneeName : "",
                               ClearingAgentGDIO = gdio != null ? gdio.ClearingAgent : "",
                               IsPartialDelivery = containerIndex.IsPartialContainer == true ? "YES" : "NO",
                               ConsigneeNTN = gdio != null ? gdio.ConsigneeNTN : "",
                               Size = containerIndex.Size,
                               ActualQty = containerIndex.NoOfPackages,
                               ActualWeight = containerIndex.BLGrossWeight,
                               BLNumber = containerIndex.BLNo,
                               Packages = containerIndex.PackageType,
                               //TotalBalanceCargo = GetBalanceCargoIfo(ContainerIndexId),
                               IsDelivered = containerIndex.IsDelivered,
                               InvoiceDate = GetInoiceInfo(ContainerIndexId),
                               CreditAmount = CreditAllowedCFS(ContainerIndexId),
                               ExchangeRateAmount = examount,
                               ShortWeight = Getshortsweight(ContainerIndexId),
                               ExcesstWeight = Getexcessweight(ContainerIndexId),

                               //AuctionHandlingCharges = GetAuctionAmount(containerIndex.CBM, containerIndex.MeasurementCBM,
                               //             examinationRequest != null ? examinationRequest.ExaminationRequestCBM : 0, containerIndex.FFCBM, 
                               //             Db.DestuffedContainers.Where(s => s.ContainerIndex.VoyageId == containerIndex.VoyageId && s.ContainerIndex.IndexNo == containerIndex.IndexNo).Sum(s => s.FoundWeight)??0.00
                               //             , "CFS", "", 0, containerIndex.ContainerIndexId).HanndlingWeight,
                               //AuctionWeightmentCharges = GetAuctionAmount(containerIndex.CBM, containerIndex.MeasurementCBM,
                               //             examinationRequest != null ? examinationRequest.ExaminationRequestCBM : 0, containerIndex.FFCBM,
                               //             Db.DestuffedContainers.Where(s => s.ContainerIndex.VoyageId == containerIndex.VoyageId && s.ContainerIndex.IndexNo == containerIndex.IndexNo).Sum(s => s.FoundWeight) ?? 0.00
                               //             , "CFS", "", 0, containerIndex.ContainerIndexId).Weight,
                               // StorageDays = gatein != null ?  Convert.ToInt32((DateTime.Now - gatein.GateInDateTime).Value.TotalDays) : 0
                           }).FirstOrDefault();


            if (invoice != null)
            {
                var containidnex = Db.ContainerIndices.FromSql($"select * from ContainerIndex where ContainerIndexId    = {invoice.ContainerIndexId}  and IsDeleted = 0").LastOrDefault();

                var crlo = Db.CRLOs.FromSql($"select * from CRLO where VIRNumber  = {containidnex.VirNo} and IndexNumber = {containidnex.IndexNo}  and IsDeleted = 0").LastOrDefault();

                var soc = Db.ScheduleOfCharges.FromSql($"select top 1 * from ScheduleOfCharge where IGMNo  = {containidnex.VirNo} and Indexno = {containidnex.IndexNo}  and IsDeleted = 0 order by ScheduleOfChargeId desc").LastOrDefault();

                if (soc != null)
                {

                    if (Convert.ToDateTime(soc.AdvanceDate.Value.Date.ToString("MM/dd/yyyy")) < Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy")))
                    {
                        invoice.IsSocCreated = false;
                        invoice.AdvanceDate = null;
                    }
                    else
                    {
                        invoice.IsSocCreated = true;
                        invoice.AdvanceDate = soc.AdvanceDate;
                    }


                }
                else
                {
                    invoice.IsSocCreated = false;
                    invoice.AdvanceDate = null;
                }

                if (crlo != null)
                {
                    invoice.DocumentCode = crlo.DocumentCodes;
                }
                else
                {
                    invoice.DocumentCode = "";
                }

                if (containidnex != null)
                {
                    var DeliveredCargogatepass = Db.DOItems.FromSql($"select DOItem.* from GatePass join DOItem  on GatePass.GatePassId = DOItem.GatePassId where GatePass.VirNumber ={containidnex.VirNo} and  GatePass.IndexNo = {containidnex.IndexNo} and  DOItem.Status = 'F' and DOItem.IsDeleted =0 and GatePass.IsDeleted = 0 ").ToList();

                    if (DeliveredCargogatepass.Count() > 0)
                    {
                        invoice.DeliveryStatus = "DeliVered";
                    }
                    else
                    {
                        invoice.DeliveryStatus = "UnDeliVered";
                    }



                    var countofindexes = Db.ContainerIndices.FromSql($"select c.* from containerindex x  join containerindex c on c.VirNo = x.VirNo and c.ContainerNo = x.ContainerNo and c.IsDeleted =0 where x.virno  = {containidnex.VirNo} and x.IndexNo = {containidnex.IndexNo} and x.IsDeleted =0").ToList().Select(x => x.IndexNo).Distinct().Count();

                    invoice.TotalArriveIndexes = countofindexes;
                }

            }

            return invoice;
        }

        public double Getshortsweight(long Containerindexid)
        {
            double shortsweight = 0.0;


            var container = Db.ContainerIndices.FromSql($"select * from ContainerIndex where ContainerIndexId  = {Containerindexid}  and IsDeleted = 0").LastOrDefault();

            if (container != null)
            {
                var creditAllow = Db.ShortExcessWeigths.FromSql($"select * from ShortExcessWeigth where VirNumber  = {container.VirNo} and IndexNumber = {container.IndexNo} and   IsDeleted = 0  and ContainerType = 'CFS' ").LastOrDefault();

                if (creditAllow != null)
                {
                    return creditAllow.ShortWeight;
                }


            }

            return shortsweight;
        }

        public double Getexcessweight(long Containerindexid)
        {
            double shortsweight = 0.0;


            var container = Db.ContainerIndices.FromSql($"select * from ContainerIndex where ContainerIndexId  = {Containerindexid}  and IsDeleted = 0").LastOrDefault();

            if (container != null)
            {
                var creditAllow = Db.ShortExcessWeigths.FromSql($"select * from ShortExcessWeigth where VirNumber  = {container.VirNo} and IndexNumber = {container.IndexNo} and   IsDeleted = 0  and ContainerType = 'CFS' ").LastOrDefault();

                if (creditAllow != null)
                {
                    return creditAllow.ExcesstWeight;
                }

            }

            return shortsweight;
        }

        public double CreditAllowedCFS(long Containerindexid)
        {
            var amount = 0;

            var datetime = DateTime.Now;
            var container = Db.ContainerIndices.FromSql($"select * from ContainerIndex where ContainerIndexId  = {Containerindexid}  and IsDeleted = 0").LastOrDefault();

            if (container != null)
            {
                var creditAllow = Db.CreditAlloweds.FromSql($"select * from creditallowed where VirNumber  = {container.VirNo} and IndexNumber = {container.IndexNo} and cast( CreatedDate as date) = cast( Getdate() as date) and IsSettled = 0   and IsDeleted = 0  and IsCancel = 0  and InvoiceNo  is null and IsApprove =1 and IsReject = 0 ").LastOrDefault();

                if (creditAllow != null)
                {
                    return creditAllow.CreditAllowedRs;
                }


            }

            return amount;
        }


        public double CreditAllowedCY(long CYContainerid)
        {
            var amount = 0;

            var datetime = DateTime.Now;

            var container = Db.CYContainers.FromSql($"select * from CYContainer where CYContainerId  = {CYContainerid}  and IsDeleted = 0").LastOrDefault();

            if (container != null)
            {
                var creditAllow = Db.CreditAlloweds.FromSql($"select * from creditallowed where VirNumber  = {container.VIRNo} and IndexNumber = {container.IndexNo} and cast( CreatedDate as date) = cast( Getdate() as date)and IsSettled = 0   and IsDeleted = 0  and IsCancel = 0  and InvoiceNo  is null and IsApprove =1 and IsReject = 0 ").LastOrDefault();


                if (creditAllow != null)
                {
                    return creditAllow.CreditAllowedRs;
                }

            }

            return amount;
        }

        public int FreeDaysCount(string type, long containerid)
        {
            var nooffreedays = 0;
            if (type == "CFS")
            {
                var conindex = (from containerindex in Db.ContainerIndices
                                where containerindex.ContainerIndexId == containerid
                                select containerindex).FirstOrDefault();

                if (conindex != null && conindex.GoodsHeadId != null)
                {

                    var goodhead = (from goodsHead in Db.GoodsHeads
                                    where goodsHead.GoodsHeadId == conindex.GoodsHeadId
                                    select goodsHead).FirstOrDefault();

                    if (goodhead == null)
                    {
                        return 0;
                    }


                    var examintion = (from examinationRequest in Db.ExaminationRequests
                                      where examinationRequest.ContainerIndexId == containerid
                                      select examinationRequest.ClearingAgentId).FirstOrDefault();

                    //var isocod = (from isocode in Db.ISOCodes
                    //              where isocode.Code == conindex.ISOCode
                    //              select isocode.Descrition).FirstOrDefault();


                    //var isocode = (from isocodes in Db.ISOCodes
                    //               from isoCodeHead in Db.ISOCodeHeads.Where(x => x.ISOCodeHeadId == x.ISOCodeHeadId).DefaultIfEmpty()
                    //               where conindex.ISOCode == isocodes.Code
                    //               select isoCodeHead).FirstOrDefault();

                    var isocode = Db.ISOCodeHeads.FromSql($"select isoCodeHead.ISOCodeHeadId , isoCodeHead.ISOCodeHeadDescription  , isoCodeHead.DeleteDate , isoCodeHead.IsDeleted  from ISOCode   left join isoCodeHead  on ISOCode.ISOCodeHeadId = ISOCodeHead.ISOCodeHeadId  and isoCodeHead.IsDeleted = 0 Where     ISOCode.Code    = {conindex.ISOCode} and ISOCode.IsDeleted = 0").FirstOrDefault();

                    if (isocode == null)
                    {
                        return 0;

                    }



                    if (conindex != null)
                    {

                        if (goodhead.GoodsHeadName.Contains("CHEMICAL / DG CARGO"))
                        {
                            nooffreedays = goodhead.StorageFreeDays;
                        }
                        else
                        {
                            var firstFreedays = (from storageFreeDays in Db.StorageFreeDays
                                                 where
                                                         (storageFreeDays.GoodsHeadId == conindex.GoodsHeadId || storageFreeDays.GoodsHeadId == null)
                                                     && (storageFreeDays.ShipperId == conindex.ShipperId || storageFreeDays.ShipperId == null)
                                                     && (storageFreeDays.ClearingAgentId == examintion || storageFreeDays.ClearingAgentId == null)
                                                     && (storageFreeDays.ConsigneId == conindex.ConsigneId || storageFreeDays.ConsigneId == null)
                                                     && (storageFreeDays.PortAndTerminalId == conindex.PortAndTerminalId || storageFreeDays.PortAndTerminalId == null)
                                                     && (storageFreeDays.ShippingAgentId == conindex.ShippingAgentId || storageFreeDays.ShippingAgentId == null)
                                                     && (storageFreeDays.ISOCodeHeadId == isocode.ISOCodeHeadId || storageFreeDays.ISOCodeHeadId == null)


                                                 select storageFreeDays).FirstOrDefault();

                            if (firstFreedays != null && firstFreedays.StorageFreeDays > 0)
                            {
                                nooffreedays = Convert.ToInt32(firstFreedays.StorageFreeDays);
                            }
                            else
                            {
                                nooffreedays = goodhead.StorageFreeDays;
                            }

                        }


                    }
                    else
                    {
                        return 0;
                    }

                }
                else
                {
                    return 0;
                }



            }

            if (type == "CY")
            {
                var conindex = (from cycontainer in Db.CYContainers

                                where cycontainer.CYContainerId == containerid
                                select cycontainer).FirstOrDefault();

                if (conindex != null && conindex.GoodsHeadId != null)
                {
                    var goodhead = (from goodsHead in Db.GoodsHeads
                                    where goodsHead.GoodsHeadId == conindex.GoodsHeadId
                                    select goodsHead).FirstOrDefault();

                    if (goodhead == null)
                    {
                        return 0;
                    }

                    var examintion = (from examinationRequest in Db.ExaminationRequests
                                      where examinationRequest.CYContainerId == containerid
                                      select examinationRequest.ClearingAgentId).FirstOrDefault();

                    //var isocod = (from isocode in Db.ISOCodes
                    //              where isocode.Code == conindex.ISOCode
                    //              select isocode.Descrition).FirstOrDefault();



                    //var isocode = (from isocodes in Db.ISOCodes
                    //               from isoCodeHead in Db.ISOCodeHeads.Where(x => x.ISOCodeHeadId == x.ISOCodeHeadId).DefaultIfEmpty()
                    //               where conindex.ISOCode == isocodes.Code
                    //               select isoCodeHead).FirstOrDefault();

                    var isocode = Db.ISOCodeHeads.FromSql($"select isoCodeHead.ISOCodeHeadId , isoCodeHead.ISOCodeHeadDescription  , isoCodeHead.DeleteDate , isoCodeHead.IsDeleted  from ISOCode   left join isoCodeHead  on ISOCode.ISOCodeHeadId = ISOCodeHead.ISOCodeHeadId  and isoCodeHead.IsDeleted = 0 Where     ISOCode.Code    = {conindex.ISOCode} and ISOCode.IsDeleted = 0").FirstOrDefault();


                    if (isocode == null)
                    {
                        return 0;

                    }



                    if (conindex != null)
                    {

                        if (goodhead.GoodsHeadName.Contains("CHEMICAL / DG CARGO"))
                        {
                            nooffreedays = goodhead.StorageFreeDays;
                        }
                        else
                        {
                            var firstFreedays = (from storageFreeDays in Db.StorageFreeDays
                                                 where
                                                         (storageFreeDays.GoodsHeadId == conindex.GoodsHeadId || storageFreeDays.GoodsHeadId == null)
                                                     && (storageFreeDays.ShipperId == conindex.ShipperId || storageFreeDays.ShipperId == null)
                                                     && (storageFreeDays.ClearingAgentId == examintion || storageFreeDays.ClearingAgentId == null)
                                                     && (storageFreeDays.ConsigneId == conindex.ConsigneId || storageFreeDays.ConsigneId == null)
                                                     && (storageFreeDays.PortAndTerminalId == conindex.PortAndTerminalId || storageFreeDays.PortAndTerminalId == null)
                                                     && (storageFreeDays.ShippingAgentId == conindex.ShippingAgentId || storageFreeDays.ShippingAgentId == null)
                                                     && (storageFreeDays.ISOCodeHeadId == isocode.ISOCodeHeadId || storageFreeDays.ISOCodeHeadId == null)


                                                 select storageFreeDays).FirstOrDefault();

                            if (firstFreedays != null && firstFreedays.StorageFreeDays > 0)
                            {
                                nooffreedays = Convert.ToInt32(firstFreedays.StorageFreeDays);
                            }
                            else
                            {
                                nooffreedays = goodhead.StorageFreeDays;
                            }

                        }

                    }

                }
                else
                {
                    return 0;
                }



            }




            return nooffreedays;
        }


        public AuctionAmount GetAuctionAmount(double CBM, double weight, string type, string igm, long index)
        {
            var handlingamount = 0.00;
            var Weighmentamount = 0.00;

            var auctionamountinfo = (from auctionamt in Db.AuctionAmounts
                                     select auctionamt).LastOrDefault();


            if (type == "CFS")
            {

                //var container = (from containerindex in Db.ContainerIndices
                //                 where containerindex.VirNo == igm && containerindex.IndexNo == index
                //                 select containerindex).FirstOrDefault();

                var container = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where  VirNo = {igm} and  IndexNo = {index} and IsDeleted = 0 ").FirstOrDefault();


                var containers = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where  VirNo = {igm} and  IndexNo = {index} and IsDeleted = 0 ").ToList();



                if (container != null && container.IsAuction == true)
                {
                    var auc = (from auction in Db.Auctions
                               where auction.ContainerIndexId == container.ContainerIndexId
                               select auction).FirstOrDefault();


                    if (auc != null)
                    {

                        if (container.CargoType == "LCL")
                        {
                            var calculateWeight = weight;

                            var weightOrCBM = WeightGreaterThenCBM(calculateWeight, CBM);

                            var weightConv = (Convert.ToDouble(calculateWeight) / 1000.0);

                            handlingamount = weightOrCBM == 1 ? (auctionamountinfo.Weight * weightConv) : (auctionamountinfo.CBM * CBM);

                            if (weightOrCBM == 1)
                            {
                                Weighmentamount = auctionamountinfo.Weight;
                            }
                        }
                        if (container.CargoType == "FCL")
                        {
                            if (containers.Count() > 0)
                            {

                                foreach (var item in containers)
                                {

                                    handlingamount += item.Size == 20 ? (auctionamountinfo.Rate20) : item.Size == 40 ? (auctionamountinfo.Rate40) : item.Size == 45 ? (auctionamountinfo.Rate45) : 0;


                                }


                            }
                        }



                    }


                }


            }

            if (type == "CY")
            {
                var containers = (from cycontainer in Db.CYContainers
                                  where cycontainer.VIRNo == igm && cycontainer.IndexNo == index
                                  select cycontainer).ToList();

                var container = (from cycontainer in Db.CYContainers
                                 where cycontainer.VIRNo == igm && cycontainer.IndexNo == index
                                 select cycontainer).FirstOrDefault();
                if (container != null && container.IsAuction == true)
                {
                    if (containers.Count() > 0)
                    {
                        var auc = (from auction in Db.Auctions
                                   where auction.CYContainerId == container.CYContainerId
                                   select auction).FirstOrDefault();

                        if (auc != null)
                        {
                            foreach (var item in containers)
                            {

                                handlingamount += item.Size == 20 ? (auctionamountinfo.Rate20) : item.Size == 40 ? (auctionamountinfo.Rate40) : item.Size == 45 ? (auctionamountinfo.Rate45) : 0;


                            }
                        }

                    }
                }



            }


            return new AuctionAmount { HanndlingWeight = handlingamount, Weight = Weighmentamount };

        }


        public DateTime? GetCFSContainerGateInDate(long ContainerIndexId)
        {

            //var containerindex = (from container in Db.ContainerIndices
            //                      where
            //                      container.ContainerIndexId == ContainerIndexId
            //                      select container).FirstOrDefault();

            var containerindex = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where  ContainerIndexId = {ContainerIndexId} and IsDeleted = 0 ").FirstOrDefault();



            if (containerindex != null)
            {
                var res = (from indexres in Db.ContainerIndices
                           where
                           indexres.VirNo == containerindex.VirNo && indexres.IndexNo == containerindex.IndexNo
                           && indexres.CFSContainerGateInDate != null && indexres.IsGateIn == true
                           orderby indexres.CFSContainerGateInDate descending
                           select indexres.CFSContainerGateInDate).FirstOrDefault();


                //var res = (from indexres in Db.ContainerIndices
                //           where
                //           indexres.VirNo == containerindex.VirNo && indexres.IndexNo == containerindex.IndexNo
                //           && containerindex.CFSContainerGateInDate != null && containerindex.IsGateIn == true                             
                //           select containerindex.CFSContainerGateInDate).LastOrDefault();                             

                if (res != null)
                {

                    return res;
                }
            }
            else
            {
                return null;

            }



            return null;

        }

        public DateTime? GetDestuffingDate(long ContainerIndexId)
        {
            var containerindex = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where  ContainerIndexId = {ContainerIndexId} and IsDeleted = 0 ").FirstOrDefault();

            if (containerindex != null)
            {
                var res = (from indexres in Db.ContainerIndices
                           join destuffedContainer in Db.DestuffedContainers on indexres.ContainerIndexId equals destuffedContainer.ContainerIndexId
                           join tellySheet in Db.TellySheets on destuffedContainer.TellySheetId equals tellySheet.TellySheetId
                           where
                           indexres.VirNo == containerindex.VirNo && indexres.IndexNo == containerindex.IndexNo
                           orderby tellySheet.DestuffDate descending
                           select tellySheet.DestuffDate).FirstOrDefault();

                if (res != null)
                {
                    return res;
                }
                else
                {
                    return null;
                }

            }

            return null;
        }


        public DateTime? GetCYContainerGateInDate(long CyContainerId)
        {
            //var container = (from cycontainer in Db.CYContainers
            //                 where cycontainer.CYContainerId == CyContainerId
            //                 select cycontainer).FirstOrDefault();

            var container = Db.CYContainers.FromSql($"select * from CYContainer   where  CYContainerId = {CyContainerId} and IsDeleted = 0 ").FirstOrDefault();

            if (container != null)
            {
                var res = (from cycontainerres in Db.CYContainers
                           where
                           cycontainerres.VIRNo == container.VIRNo && cycontainerres.IndexNo == container.IndexNo
                           && cycontainerres.CYContainerGateInDate != null && cycontainerres.IsGateIn == true
                           orderby cycontainerres.CYContainerGateInDate descending
                           select cycontainerres.CYContainerGateInDate).FirstOrDefault();

                if (res != null)
                {

                    return res;
                }
            }
            else
            {
                return null;
            }



            return null;

        }


        public string GetBalanceCargoIfo(long ContainerIndexId)
        {
            var asd = Db.OrderDetails.FromSql($"select GatePass.GatePassId , GatePass.DODate , GatePass.DOYear , GatePass.DONumber , GatePass.Remarks , GatePass.DeliveryOrderId , GatePass.BalancePackages	, GatePass.GatePassNumber , GatePass.TotalPackages , GatePass.Delivered , GatePass.TotalDelivered , GatePass.GatePassDate , GatePass.UnitType , GatePass.DeleteDate , GatePass.IsDeleted , GatePass.GatePassType	, GatePass.RandDClerkId	, GatePass.Shift , GatePass.GatePasscontainerNumber , GatePass.IndexNo , GatePass.VirNumber from DeliveryOrder left join GatePass on DeliveryOrder.DeliveryOrderId = GatePass.DeliveryOrderId and GatePass.IsDeleted = 0 Where DeliveryOrder.ContainerIndexId = {ContainerIndexId} and DeliveryOrder.IsDeleted = 0").LastOrDefault();
            if (asd != null)
            {
                return asd.BalancePackages.ToString();
            }

            var indexinfo = Db.ContainerIndices.FromSql($"select * From ContainerIndex Where  ContainerIndexId = {ContainerIndexId}  and IsDeleted = 0 ").LastOrDefault();
            if (indexinfo != null)
            {
                if (indexinfo.CargoType == "LCL")
                {
                    return indexinfo.NoOfPackages.ToString();
                }
                if (indexinfo.CargoType == "FCL")
                {
                    return indexinfo.ContainerGrossWeight.ToString() + ".00";
                }
            }

            return null;
        }

        public DateTime? GetInoiceInfo(long ContainerIndexId)
        {
            var asd = Db.Invoices.FromSql($"select * From Invoice Where  ContainerIndexId = {ContainerIndexId}  and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                return asd.AdvanceDate;
            }
            return null;
        }


        public DateTime? GetInoiceInfocy(long CyContainerId)
        {
            var asd = Db.Invoices.FromSql($"select * From Invoice Where  CYContainerId = {CyContainerId}  and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                return asd.AdvanceDate;
            }
            return null;
        }

        public InvoiceViewModel GetInvoiceCY(long CyContainerId)
        {
            double examount = 0.00;
            var resdata = Db.ExchangeRates.FromSql($"select * from ExchangeRate where  IsDeleted = 0  ").LastOrDefault();

            if (resdata != null)
            {
                examount = resdata.ExchangeRateAmount;

            }
            var invoice = (from container in Db.CYContainers
                               //from gatein in Db.GateIns.Where(g => g.ContainerNo == container.ContainerNo).DefaultIfEmpty()
                           from shippingAgent in Db.ShippingAgents.Where(s => s.ShippingAgentId == container.ShippingAgentId).DefaultIfEmpty()
                           from consignee in Db.Consignes.Where(s => s.ConsigneId == container.ConsigneId).DefaultIfEmpty()
                           from good in Db.GoodsHeads.Where(s => s.GoodsHeadId == container.GoodsHeadId).DefaultIfEmpty()
                           from shipper in Db.Shippers.Where(s => s.ShipperId == container.ShipperId).DefaultIfEmpty()
                           from portAndTerminal in Db.PortAndTerminals.Where(s => s.PortAndTerminalId == container.PortAndTerminalId).DefaultIfEmpty()
                               //from billToLine in Db.BillToLines.Where(s => s.CyContainerId == container.CYContainerId && s.InoviceCreated == false).DefaultIfEmpty()
                           from saleTax in Db.SalesTaxes.Where(x => x.Type == "Import").DefaultIfEmpty()
                           from examinationRequest in Db.ExaminationRequests.Where(x => x.CYContainerId == container.CYContainerId).DefaultIfEmpty()
                           from isocode in Db.ISOCodes.Where(x => x.Code == container.ISOCode).DefaultIfEmpty()

                           where container.CYContainerId == CyContainerId
                           select new InvoiceViewModel
                           {
                               CyContainerId = container.CYContainerId,
                               ContainerNo = container.ContainerNo,
                               Description = container.Description,
                               // GateInDate = gatein != null ? gatein.GateInDateTime : null,
                               GateInDate = GetCYContainerGateInDate(CyContainerId),
                               // AdditionalFreeDays = FreeDaysCount("CY" , CyContainerId) ,
                               ShippingAgentId = shippingAgent != null ? shippingAgent.ShippingAgentId : 0,
                               ShippingAgentName = shippingAgent != null ? shippingAgent.Name : "",
                               VehcileAmountAllow = shippingAgent != null ? shippingAgent.VehcileAmountAllow : "",
                               ConsigneeNameIGM = consignee != null ? consignee.ConsigneName : "",
                               ClearingAgentId = examinationRequest != null ? examinationRequest.ClearingAgentId ?? 0 : 0,
                               CNIC = examinationRequest != null ? examinationRequest.CNIC : "",
                               PhoneNumber = examinationRequest != null ? examinationRequest.PhoneNumber : "",
                               ClearingAgentRep = examinationRequest != null ? examinationRequest.ClearingAgentRep : "",
                               Good = good != null ? good.GoodsHeadName : "",
                               Shipper = shipper != null ? shipper.ShipperName : "",
                               PortAndTerminal = portAndTerminal != null ? portAndTerminal.PortName : "",
                               ISOCodeDesc = isocode != null ? isocode.Descrition : "",
                               // BillToLineNumber = GeCytBillToLineNumber(CyContainerId),
                               WaiVerAmount = GetCYWaiverTotal(CyContainerId),
                               LastWaiverRemarks = GetWaiverLastRemarksCY(CyContainerId),
                               PreviousWaiVerAmount = GetPreviousWaiverTotalCY(CyContainerId),
                               PreviousBillAmount = GetInvoiceTotalCY(CyContainerId),
                               PreviousBillTotalCharges = GetInvoiceTotalCharges(CyContainerId, "CY"),

                               //PreviousPaidGST = GetPreviousPaidGST(CyContainerId, "CY"),


                               PreviousPaidBillToLineAmount = GetPreviousPaidBillToLineAmount(CyContainerId, "CY"),
                               //PreviousPaidBillToLineAmountGST = GetPreviousPaidBillToLineAmountGST(CyContainerId, "CY"),
                               //PreviousPaidBillToLineAmountGrandTotal = GetPreviousPaidBillToLineAmountGrandTotal(CyContainerId, "CY"),


                               // PortCharges = Db.PortCharges.Where(s => s.VirNumber == container.VIRNo && s.IndexNumber == container.IndexNo).Sum(s => s.TotalCharges),
                               IndexCargoType = container.CargoType,
                               SalesTax = saleTax != null ? saleTax.SalesTaxAmount : 0,
                               SealChargers = GetSealAmount(CyContainerId),
                               Weight = container.BLGrossWeight != null ? Convert.ToInt32(container.BLGrossWeight) : 0,
                               OtherCharges = GetAdditionalChargesTotalCY(CyContainerId),
                               Size = container.Size,
                               IsPartialDelivery = container.IsPartialContainer == true ? "YES" : "NO",
                               ConsigneeNTNNumber = consignee != null ? consignee.ConsigneNTN : "",
                               ClearingAgentRegNumber = examinationRequest != null ? examinationRequest.CustomRegNo : "",
                               ActualQty = container.NoOfPackages,
                               ActualWeight = container.BLGrossWeight,
                               BLNumber = container.BLNo,
                               Packages = container.PackageType,
                               //StorageDays = gatein != null ? Convert.ToInt32((DateTime.Now - gatein.GateInDateTime).Value.TotalDays) : 0 ,
                               DestuffDate = null,
                               InvoiceDate = GetInoiceInfocy(CyContainerId),
                               CreditAmount = CreditAllowedCY(CyContainerId),
                               ExchangeRateAmount = examount,
                               ShortWeight = 0.0,
                               ExcesstWeight = 0.0
                           }).FirstOrDefault();

            if (invoice != null)
            {
                var containidnex = Db.CYContainers.FromSql($"select * from CYContainer where CYContainerId    = {invoice.CyContainerId}  and IsDeleted = 0").LastOrDefault();

                var crlo = Db.CRLs.FromSql($"select crl.* from CYContainer  join crl on CYContainer.VIRNo = crl.VIRNumber and CYContainer.ContainerNo = crl.ContainerNumber and CRL.IsDeleted =0 where CYContainer.VIRNo  = {containidnex.VIRNo}  and CYContainer.IndexNo = {containidnex.IndexNo} and CYContainer.IsDeleted = 0").ToList();
                if (crlo.Count() > 0)
                {

                    invoice.DocumentCode = String.Join(",", crlo.Select(s => s.DocumentCodes).Distinct());
                }
                else
                {
                    invoice.DocumentCode = "";
                }

                var soc = Db.ScheduleOfCharges.FromSql($"select top 1 * from ScheduleOfCharge where IGMNo  = {containidnex.VIRNo} and Indexno = {containidnex.IndexNo}  and IsDeleted = 0 order by ScheduleOfChargeId desc").LastOrDefault();

                if (soc != null)
                {
                    if (Convert.ToDateTime(soc.AdvanceDate.Value.Date.ToString("MM/dd/yyyy")) < Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy")))
                    {
                        invoice.IsSocCreated = false;
                        invoice.AdvanceDate = null;
                    }
                    else
                    {
                        invoice.IsSocCreated = true;
                        invoice.AdvanceDate = soc.AdvanceDate;
                    }

                    //invoice.IsSocCreated = true;
                    //invoice.AdvanceDate = soc.AdvanceDate;
                }
                else
                {
                    invoice.IsSocCreated = false;
                    invoice.AdvanceDate = null;
                }

                if (containidnex != null)
                {
                    var DeliveredCargogatepass = Db.DOItems.FromSql($"select DOItem.* from GatePass join DOItem  on GatePass.GatePassId = DOItem.GatePassId where GatePass.VirNumber ={containidnex.VIRNo} and  GatePass.IndexNo = {containidnex.IndexNo} and  DOItem.Status = 'F' and DOItem.IsDeleted =0 and GatePass.IsDeleted = 0 ").ToList();

                    if (DeliveredCargogatepass.Count() > 0)
                    {
                        invoice.DeliveryStatus = "DeliVered";
                    }
                    else
                    {
                        invoice.DeliveryStatus = "UnDeliVered";
                    }

                    var countofindexes = Db.CYContainers.FromSql($"select c.* from CYContainer x  join CYContainer c on c.VirNo = x.VirNo and c.ContainerNo = x.ContainerNo and c.IsDeleted =0 where x.virno  = {containidnex.VIRNo} and x.IndexNo = {containidnex.IndexNo} and x.IsDeleted =0").ToList().Select(x => x.IndexNo).Distinct().Count();

                    invoice.TotalArriveIndexes = countofindexes;
                }

            }

            return invoice;
        }



        public AICTAndLineIndexTariffViewModel GetCollectionBreakup(string virno, string indexno, DateTime BillDate)
        {



            var listItems = new List<AICTAndLineIndexTariffViewModel>();
            var newlistItems = new List<AICTAndLineIndexTariffViewModel>();


            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {virno} and IndexNo = {indexno} and  CargoType = 'LCL'  and IsDeleted = 0  ").FirstOrDefault();
            if (asd != null)
            {

                var indexid = asd.ContainerIndexId;

                var ExaminationRequestres = Db.ExaminationRequests.FromSql($"SELECT * From ExaminationRequest Where ContainerIndexId = {indexid}  and IsDeleted = 0  ").LastOrDefault();

                var data = (from containerIndex in Db.ContainerIndices
                            from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerIndex.ShippingAgentId).DefaultIfEmpty()
                            from portcharg in Db.PortCharges.Where(x => x.VirNumber == containerIndex.VirNo && x.IndexNumber == containerIndex.IndexNo).DefaultIfEmpty()
                            from destuff in Db.DestuffedContainers.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()

                            where
                            containerIndex.ContainerIndexId == indexid
                            select new AICTAndLineIndexTariffViewModel
                            {
                                Key = containerIndex.VirNo + "-" + containerIndex.IndexNo,
                                ContainerIndexId = containerIndex.ContainerIndexId,
                                ContainerNumber = containerIndex.ContainerNo,
                                //ArrivalDate = containerIndex.CFSContainerGateInDate,
                                ArrivalDate = GetCFSContainerGateInDate(indexid),
                                IsDelivered = containerIndex.IsDelivered,
                                IGMNo = containerIndex.VirNo,
                                IndexNo = containerIndex.IndexNo,
                                DeliveryDate = containerIndex.CFSContainerGateOutDate,
                                PortCharges = portcharg != null ? portcharg.TotalCharges : 0,
                                SpecialCharges = GetAdditionalChargesTotalDeliverredUnDeliverd(indexid),
                                CargoType = asd.CargoType,
                                ClearingAgentId = ExaminationRequestres != null ? ExaminationRequestres.ClearingAgentId ?? 0 : 0,
                                DesstuffedDate = GetDestuffingDate(indexid),
                                //DesstuffedDate = destuff.TellySheet != null ? destuff.TellySheet.DestuffDate : null,
                                DestuffMark = destuff.TellySheet != null ? true : false,
                                GateInMark = containerIndex.CFSContainerGateInDate != null ? true : false,
                                Weight = containerIndex.ManifestedWeight,
                                HigherCBM = higherCBM(asd.CBM, asd.MeasurementCBM, ExaminationRequestres != null ? ExaminationRequestres.ExaminationRequestCBM : 0, asd.FFCBM),

                            }).LastOrDefault();

                data.StorageCharges = GetStorageTotalLineShare(data.ContainerIndexId, data.ClearingAgentId, BillDate, data.ArrivalDate ?? DateTime.Now, data.DesstuffedDate ?? DateTime.Now, data.Weight ?? 0.00, data.HigherCBM ?? 0, data.CargoType, data.DestuffMark, data.GateInMark);

                if (data.ContainerIndexId != 0 && data.ClearingAgentId != 0 && data.GateInMark == true)
                {
                    if (data.Weight > 0 || data.HigherCBM > 0)
                    {

                        data.TariffList = GetCBMTotal(data.ContainerIndexId, data.ClearingAgentId, data.Weight ?? 0, data.HigherCBM ?? 0, data.CargoType, data.ArrivalDate ?? DateTime.Now, DateTime.Now).ToList();

                    }
                }

                data.BillToLine = CalculateBillToLineCFS(data.StorageCharges, 0, data.PortCharges, data.StorageCharges, 0, data.IGMNo, data.IndexNo ?? 0, "CFS"
                                          , data.TariffList);

                return data;
            }

            return null;


        }

        public double CalculateBillToLineCFS(double storageAmount, double sealChargr, double portChargAmount, double otherChargAmount, double vehicleChargs, string igm, long indexno, string type
                                          , List<InvoiceItemViewModel> tariffdata)
        {
            List<BillToLine> billToLines = new List<BillToLine>();
            var billToLineAmount = 0.00;


            var resbillToLine = Db.BillToLines.FromSql($"SELECT * From  BillToLine  and IsDeleted = 0 ").Where(x => x.VirNo == igm && x.IndexNo == indexno && x.IndexType == type && x.InoviceCreated == true).ToList();

            if (resbillToLine.Count() > 0)
            {
                billToLineAmount = resbillToLine.Sum(x => x.InvoiceAmount);

            }

            var undeliverdresbillToLine = Db.BillToLines.FromSql($"SELECT * From  BillToLine  and IsDeleted = 0 ").Where(x => x.VirNo == igm && x.IndexNo == indexno && x.IndexType == type && x.InoviceCreated == false).LastOrDefault();

            if (undeliverdresbillToLine != null)
            {
                foreach (var item in tariffdata)
                {
                    var BillToLine = new BillToLine
                    {
                        TariffAmount = item.Amount,
                        Type = "Tariff",

                    };
                    billToLines.Add(BillToLine);
                }


                if (storageAmount > 0)
                {
                    var res = new BillToLine
                    {
                        Type = "Storage",
                        TariffAmount = storageAmount,
                    };

                    billToLines.Add(res);
                }
                if (sealChargr > 0)
                {
                    var res = new BillToLine
                    {
                        Type = "Other",
                        TariffAmount = sealChargr,
                    };

                    billToLines.Add(res);
                }
                if (portChargAmount > 0)
                {
                    var res = new BillToLine
                    {
                        Type = "Other",
                        TariffAmount = portChargAmount,
                    };

                    billToLines.Add(res);
                }
                if (otherChargAmount > 0)
                {
                    var res = new BillToLine
                    {
                        Type = "Other",
                        TariffAmount = otherChargAmount,
                    };

                    billToLines.Add(res);
                }
                if (vehicleChargs > 0)
                {
                    var res = new BillToLine
                    {
                        Type = "Other",
                        TariffAmount = vehicleChargs,
                    };

                    billToLines.Add(res);
                }


                if (undeliverdresbillToLine.Type == "HundredPercent")
                {
                    var Amount = billToLines.Sum(x => x.TariffAmount);

                    billToLineAmount += Amount;
                }

                if (undeliverdresbillToLine.Type == "ExStorage")
                {
                    var Amount = billToLines.Where(x => x.Type == "Tariff" || x.Type == "Other").Sum(x => x.TariffAmount);

                    billToLineAmount += Amount;
                }
                if (undeliverdresbillToLine.Type == "OnlyTariff")
                {
                    var Amount = billToLines.Where(x => x.Type == "Tariff").Sum(x => x.TariffAmount);

                    billToLineAmount += Amount;
                }

                if (undeliverdresbillToLine.Type == "ManualAmount")
                {
                    var Amount = undeliverdresbillToLine.TariffAmount;

                    billToLineAmount += Amount;
                }


            }

            return Math.Round(billToLineAmount);

        }


        //public CargoReceived GetCargoReceivedByEnteringCargoId(long EnteringCargoId)
        //{
        //    var data = (from enteringCargo in Db.EnteringCargos
        //                join loadingProgram in Db.LoadingPrograms on enteringCargo.LoadingProgramNumber equals loadingProgram.LoadingProgramNumber
        //                join cargoReceived in Db.CargoReceiveds on loadingProgram.LoadingProgramId equals cargoReceived.LoadingProgramId
        //                where enteringCargo.EnteringCargoId == EnteringCargoId
        //                select cargoReceived).FirstOrDefault();
        //    return data;
        //}






        public IEnumerable<AICTAndLineIndexTariffViewModel> GetCargodetailCFSConatiner(string virno, string containerno, string indexno, long? agent, string type, DateTime? from, DateTime? to)
        {



            var indexdetil = new List<ImportFielsViewModel>();


            var BillDate = DateTime.Now;

            var containerList = new List<ContainerIndex>();

            var listItems = new List<AICTAndLineIndexTariffViewModel>();
            var newlistItems = new List<AICTAndLineIndexTariffViewModel>();

            //var containerList = (from containerIndex in Db.ContainerIndices
            //                     where
            //                    (containerIndex.VirNo == virno || virno == null || virno == "")
            //                      &&
            //                    (containerIndex.ContainerNo == containerno || containerno == null || containerno == "")
            //                      &&
            //                    (containerIndex.IndexNo == Convert.ToInt64(indexno) || indexno == null || indexno == "")
            //                     &&
            //                     (containerIndex.ShippingAgentId == agent || agent == null)
            //                     &&
            //                     (containerIndex.CargoType == "LCL")
            //                     select containerIndex).ToList();


            var fromdate = from?.ToString("MM/dd/yyyy");
            var todate = to?.ToString("MM/dd/yyyy");


            //if (type == "ALL")
            //{
            //    containerList = Db.ContainerIndices.FromSql($" select * from ContainerIndex where CargoType = 'LCL' and (ShippingAgentId = {agent} Or  {agent} IS  null   or {agent} = '') and (Virno = {virno} Or  {virno} IS  null   or {virno} = '') and (indexno = {indexno} Or  {indexno} IS  null   or {indexno} = '') and (containerno = {containerno} Or  {containerno} IS  null   or {containerno} = '')  ").ToList();
            //}

            if (type == "DLV")
            {

                containerList = Db.ContainerIndices.FromSql($" select * from ContainerIndex where CargoType = 'LCL' and (ShippingAgentId = {agent} Or  {agent} IS  null   or {agent} = '') and (Virno = {virno} Or  {virno} IS  null   or {virno} = '') and (indexno = {indexno} Or  {indexno} IS  null   or {indexno} = '') and (containerno = {containerno} Or  {containerno} IS  null   or {containerno} = '') and  (Cast(CFSContainerGateOutDate AS date) >= Cast({fromdate} AS date) Or {fromdate} IS  null   or {fromdate} = '') and  (Cast(CFSContainerGateOutDate AS date) <= Cast({todate} AS date) Or {todate} IS  null   or {todate} = '') and CFSContainerGateOutDate is not null ").ToList();

                //var containsdsderList = Db.ContainerIndices.FromSql($" select * from ContainerIndex where CargoType = 'LCL' and (ShippingAgentId = {agent} Or  {agent} IS  null   or {agent} = '') and (Virno = {virno} Or  {virno} IS  null   or {virno} = '') and (indexno = {indexno} Or  {indexno} IS  null   or {indexno} = '') and (containerno = {containerno} Or  {containerno} IS  null   or {containerno} = '') and  (Cast(CFSContainerGateOutDate AS date) >= Cast({fromdate} AS date) Or {fromdate} IS  null   or {fromdate} = '') and  (Cast(CFSContainerGateOutDate AS date) <= Cast({todate} AS date) Or {todate} IS  null   or {todate} = '') ").ToList()
                //                          .Select(v => new IGMIndexViewModel { VirNumber = v.VirNo, IndexNo = v.IndexNo })
                //                         .GroupBy(x => x.IndexNo).Select(x => x.FirstOrDefault()).ToList();

            }

            if (type == "UNDLV")
            {

                containerList = Db.ContainerIndices.FromSql($" select * from ContainerIndex where CargoType = 'LCL' and (ShippingAgentId = {agent} Or  {agent} IS  null   or {agent} = '') and (Virno = {virno} Or  {virno} IS  null   or {virno} = '') and (indexno = {indexno} Or  {indexno} IS  null   or {indexno} = '') and (containerno = {containerno} Or  {containerno} IS  null   or {containerno} = '')and  (Cast(CFSContainerGateInDate AS date) >= Cast({fromdate} AS date) Or {fromdate} IS  null   or {fromdate} = '') and  (Cast(CFSContainerGateInDate AS date) <= Cast({todate} AS date) Or {todate} IS  null   or {todate} = '') and CFSContainerGateOutDate is  null  ").ToList();

            }

            if (type == "ALL")
            {

                containerList = Db.ContainerIndices.FromSql($" select * from ContainerIndex where CargoType = 'LCL' and (ShippingAgentId = {agent} Or  {agent} IS  null   or {agent} = '') and (Virno = {virno} Or  {virno} IS  null   or {virno} = '') and (indexno = {indexno} Or  {indexno} IS  null   or {indexno} = '') and (containerno = {containerno} Or  {containerno} IS  null   or {containerno} = '')and  (Cast(CFSContainerGateInDate AS date) >= Cast({fromdate} AS date) Or {fromdate} IS  null   or {fromdate} = '') and  (Cast(CFSContainerGateInDate AS date) <= Cast({todate} AS date) Or {todate} IS  null   or {todate} = '')  ").ToList();

            }


            var invoiceDetails = new List<AICTAndLineTariffViewModel>();


            foreach (var item in containerList)
            {

                var res = GetInvoiceDetail(item.ContainerIndexId);

                invoiceDetails.Add(res);

            }


            foreach (var item in invoiceDetails)
            {
                double AICTPerCBMRate = 0;
                double AICTPerIndexRate = 0;
                double FFPerCBMRate = 0;
                double FFPerIndexRate = 0;


                double AICTPerCBMRateShareRate = 0;
                double AICTPerIndexRateShareRate = 0;
                double FFPerCBMRateShareRate = 0;
                double FFPerIndexRateShareRate = 0;

                double TotalCBM = 0;
                double TotalIndex = 0;
                double TotalCharges = 0;

                double rate20 = 0;
                double rate40 = 0;
                double rate45 = 0;

                double PerBox = 0;

                var aicttariff = Db.AICTAndLineIndexTariffs.FromSql($"SELECT * From AICTAndLineIndexTariff Where VirNumber = {item.Virnumber} and IndexNumber  = {item.IndexNumber}  and IsDeleted = 0  and Status = 'UnDelivered' ").LastOrDefault();

                if (aicttariff != null)
                {
                    AICTPerCBMRateShareRate = aicttariff.AICTPerCBMRateShareRate;
                    AICTPerIndexRateShareRate = aicttariff.AICTPerIndexRateShareRate;
                    FFPerCBMRateShareRate = aicttariff.FFPerCBMRateShareRate;
                    FFPerIndexRateShareRate = aicttariff.FFPerIndexRateShareRate;

                    AICTPerCBMRate = aicttariff.AICTPerCBMRate;
                    AICTPerIndexRate = aicttariff.AICTPerIndexRate;
                    FFPerCBMRate = aicttariff.FFPerCBMRate;
                    FFPerIndexRate = aicttariff.FFPerIndexRate;

                    PerBox = aicttariff.PerBoxRate;

                    TotalCBM = aicttariff.TotalCBMRate;
                    TotalIndex = aicttariff.TotalPerIndexRate;
                    TotalCharges = aicttariff.TotalCBMRate + aicttariff.TotalPerIndexRate;


                    var findindex = indexdetil.FindAll(x => x.VirNumber == item.Virnumber && x.IndexNo == item.IndexNumber).ToList();

                    if (findindex.Count() <= 0)
                    {

                        var res = new ImportFielsViewModel
                        {
                            IndexNo = item.IndexNumber,
                            VirNumber = item.Virnumber,

                        };
                        indexdetil.Add(res);
                    }



                }
                else
                {
                    AICTPerCBMRateShareRate = item.TariffInformationId != 0 ? Db.TerminalAndFFShareRates.Where(s => s.TerminalAndFFShareRateId == item.TariffInformationId).Select(s => s.AICTCBMRate).LastOrDefault() : 0;
                    AICTPerIndexRateShareRate = item.TariffInformationId != 0 ? Db.TerminalAndFFShareRates.Where(s => s.TerminalAndFFShareRateId == item.TariffInformationId).Select(s => s.AICTPerIndexRate).LastOrDefault() : 0;
                    FFPerCBMRateShareRate = item.TariffInformationId != 0 ? Db.TerminalAndFFShareRates.Where(s => s.TerminalAndFFShareRateId == item.TariffInformationId).Select(s => s.FFCBMRate).LastOrDefault() : 0;
                    FFPerIndexRateShareRate = item.TariffInformationId != 0 ? Db.TerminalAndFFShareRates.Where(s => s.TerminalAndFFShareRateId == item.TariffInformationId).Select(s => s.FFPerIndexRate).LastOrDefault() : 0;


                    rate20 = item.TariffInformationId != 0 ? Db.TerminalAndFFShareRates.Where(s => s.TerminalAndFFShareRateId == item.TariffInformationId).Select(s => s.TotalAICTRate20).LastOrDefault() : 0;
                    rate40 = item.TariffInformationId != 0 ? Db.TerminalAndFFShareRates.Where(s => s.TerminalAndFFShareRateId == item.TariffInformationId).Select(s => s.TotalAICTRate40).LastOrDefault() : 0;
                    rate45 = item.TariffInformationId != 0 ? Db.TerminalAndFFShareRates.Where(s => s.TerminalAndFFShareRateId == item.TariffInformationId).Select(s => s.TotalAICTRate45).LastOrDefault() : 0;

                    AICTPerCBMRate = 0;
                    AICTPerIndexRate = 0;
                    FFPerCBMRate = 0;
                    FFPerIndexRate = 0;

                    TotalCBM = 0;
                    TotalIndex = 0;
                    TotalCharges = 0;

                }



                var data = (from containerIndex in Db.ContainerIndices
                                //join desstuff in Db.DestuffedContainers on containerIndex.ContainerIndexId equals desstuff.ContainerIndexId
                            from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerIndex.ShippingAgentId).DefaultIfEmpty()
                            from goodhead in Db.GoodsHeads.Where(x => x.GoodsHeadId == containerIndex.GoodsHeadId).DefaultIfEmpty()
                                //  from aicttariff in Db.AICTAndLineIndexTariffs.Where(x => x.VirNumber == containerIndex.VirNo && x.IndexNumber == containerIndex.IndexNo).DefaultIfEmpty()
                            from portcharg in Db.PortCharges.Where(x => x.VirNumber == containerIndex.VirNo && x.IndexNumber == containerIndex.IndexNo).DefaultIfEmpty()
                            where
                            containerIndex.ContainerIndexId == item.ContainerIndexId
                            select new AICTAndLineIndexTariffViewModel
                            {
                                Key = containerIndex.VirNo + "-" + containerIndex.IndexNo,
                                ContainerIndexId = containerIndex.ContainerIndexId,
                                ContainerNumber = containerIndex.ContainerNo,
                                //ArrivalDate = containerIndex.CFSContainerGateInDate,
                                ArrivalDate = GetCFSContainerGateInDate(item.ContainerIndexId),
                                IsDelivered = containerIndex.IsDelivered,
                                shippingAgent = shippingAgent.Name,
                                GoodHeadName = goodhead != null ? goodhead.GoodsHeadName : "",
                                IGMNo = containerIndex.VirNo,
                                IndexNo = containerIndex.IndexNo,

                                //DeliveryDate = containerIndex.CFSContainerGateOutDate,
                                HigherCBM = item.HigherCBM,
                                BillToLine = item.BillToLineAmount,


                                AICTPerCBMRate = AICTPerCBMRateShareRate,
                                AICTPerIndexRate = AICTPerIndexRateShareRate,
                                FFPerCBMRate = FFPerCBMRateShareRate,
                                FFPerIndexRate = FFPerIndexRateShareRate,

                                AdditionalChargeAICTPerCBMRate = AICTPerCBMRate,
                                AdditionalChargeAICTPerIndexRate = AICTPerIndexRate,
                                AdditionalChargeFFPerCBMRate = FFPerCBMRate,
                                AdditionalChargeFFPerIndexRate = FFPerIndexRate,

                                TotalCBMRate = TotalCBM,
                                TotalPerIndexRate = TotalIndex,
                                TotalCharges = TotalCharges,

                                Rate20 = rate20,
                                Rate40 = rate40,
                                Rate45 = rate45,

                                PerBoxRate = PerBox,

                                PortCharges = portcharg != null ? portcharg.TotalCharges : 0,
                                SpecialCharges = item.OtherCharges,
                                CargoType = item.CargoType,
                                ClearingAgentId = item.ClearingAgentId,
                                DesstuffedDate = item.DestuffDate,
                                DestuffMark = item.DestuffMark,
                                GateInMark = item.GateInMark,
                                Weight = containerIndex.ManifestedWeight

                            }).ToList().OrderBy(x => x.IndexNo);

                listItems.AddRange(data);


            }


            if (listItems.Count() > 0)
            {
                foreach (var item in listItems)
                {

                    var invoiceres = Db.Invoices.FromSql($"SELECT * From Invoice Where ContainerIndexId = {item.ContainerIndexId}   and IsDeleted = 0").LastOrDefault();

                    if (invoiceres != null)
                    {
                        item.DeliveryDate = invoiceres.AdvanceDate;
                    }

                    var findindex = indexdetil.FindAll(x => x.VirNumber == item.IGMNo && x.IndexNo == item.IndexNo).ToList();

                    if (findindex.Count() <= 0)
                    {
                        var indexcontainersdata = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {item.IGMNo} and IndexNo = {item.IndexNo} and IsDeleted = 0").ToList();


                        foreach (var itemres in indexcontainersdata)
                        {
                            var countofindexes = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {itemres.VirNo}  and ContainerNo = {itemres.ContainerNo} and IsDeleted = 0").ToList().Select(x => x.IndexNo).Distinct().Count();

                            item.PerBoxRate += itemres.Size == 20 ? item.Rate20 / countofindexes : itemres.Size == 40 ? item.Rate40 / countofindexes : itemres.Size == 45 ? item.Rate45 / countofindexes : 0;

                        }

                        var res = new ImportFielsViewModel
                        {
                            IndexNo = item.IndexNo,
                            VirNumber = item.IGMNo,

                        };
                        indexdetil.Add(res);


                    }



                }


            }

            //if(listItems.Count() > 0)
            //{
            //    foreach (var item in listItems)
            //    {
            //        item.StorageCharges = GetStorageTotalLineShare(item.ContainerIndexId, item.ClearingAgentId, DateTime.Now, item.ArrivalDate ??  DateTime.Now, item.DesstuffedDate ?? DateTime.Now, item.Weight ?? 0.00, item.HigherCBM ?? 0, item.CargoType, item.DestuffMark, item.GateInMark);
            //    }
            //}


            return listItems;
        }

        public AICTAndLineTariffViewModel GetInvoiceDetail(long ContainerIndexId)
        {

            var invoicedetai = new AICTAndLineTariffViewModel();

            //var containerind = (from containerindx in Db.ContainerIndices
            //                      where containerindx.ContainerIndexId == ContainerIndexId select
            //                      containerindx).FirstOrDefault();


            var containerind = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where ContainerIndexId = {ContainerIndexId}  and IsDeleted = 0 ").FirstOrDefault();




            //var ISOCOD = (from iso in Db.ISOCodes
            //                      where containerind.ISOCode == iso.Code select
            //                      iso).FirstOrDefault();

            //long? isocodehdid = null; 

            //var ISOCOD = (from isocodes in Db.ISOCodes
            //               from isoCodeHead in Db.ISOCodeHeads.Where(x => x.ISOCodeHeadId == x.ISOCodeHeadId).DefaultIfEmpty()
            //               where containerind.ISOCode == isocodes.Code
            //               select isoCodeHead).FirstOrDefault();
            //if(ISOCOD != null)
            //{
            //    isocodehdid = ISOCOD.ISOCodeHeadId;
            //}
            //else
            //{
            //    isocodehdid = null;
            //}


            //var examinatin = (from examinationRequest in Db.ExaminationRequests
            //                      where examinationRequest.ContainerIndexId == ContainerIndexId
            //                  select examinationRequest).FirstOrDefault();

            if (containerind != null)
            {
                invoicedetai = (from containerIndex in Db.ContainerIndices
                                    //from billtoline in Db.BillToLines.Where(s => s.ContainerIndexId == containerIndex.ContainerIndexId && s.InoviceCreated == false).DefaultIfEmpty()
                                from destuff in Db.DestuffedContainers.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                                from examinationRequest in Db.ExaminationRequests.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                                    //  from isocode in Db.ISOCodes.Where(x => x.Code == containerIndex.ISOCode).DefaultIfEmpty()

                                where containerIndex.ContainerIndexId == ContainerIndexId
                                select new AICTAndLineTariffViewModel
                                {
                                    ContainerIndexId = containerIndex.ContainerIndexId,
                                    Virnumber = containerIndex.VirNo,
                                    IndexNumber = containerIndex.IndexNo,
                                    GateInDate = GetCFSContainerGateInDate(ContainerIndexId),
                                    DestuffDate = GetDestuffingDate(ContainerIndexId),
                                    //DestuffDate = destuff.TellySheet != null ? destuff.TellySheet.DestuffDate : null,
                                    //BillToLineAmount = GetBillToLineTotalDeliverredUnDeliverd(ContainerIndexId),
                                    //OtherCharges = GetAdditionalChargesTotalDeliverredUnDeliverd(ContainerIndexId),
                                    BillToLineAmount = 0,
                                    OtherCharges = 0,
                                    CBM = containerIndex.CBM,
                                    MeasurementCBM = containerIndex.MeasurementCBM,
                                    ClearingAgentId = examinationRequest != null ? examinationRequest.ClearingAgentId ?? 0 : 0,
                                    ExaminationRequestCBM = examinationRequest != null ? examinationRequest.ExaminationRequestCBM : 0,
                                    FFCBM = containerIndex.FFCBM,
                                    HigherCBM = higherCBM(containerIndex.CBM, containerIndex.MeasurementCBM, examinationRequest != null ? examinationRequest.ExaminationRequestCBM : 0, containerIndex.FFCBM),
                                    // TariffInformationId = GetShareRate( containerind.ShippingAgentId   , isocodehdid, containerind.ConsigneId, examinatin != null ? examinatin.ClearingAgentId : null, containerind.ShipperId, containerind.GoodsHeadId, containerind.PortAndTerminalId  ),
                                    TariffInformationId = GetShareRate(containerind.ShippingAgentId, containerind.GoodsHeadId),
                                    CargoType = containerIndex.CargoType,
                                    DestuffMark = destuff.TellySheet != null ? true : false,
                                    GateInMark = containerIndex.CFSContainerGateInDate != null ? true : false
                                }).FirstOrDefault();

            }

            return invoicedetai;

        }

        public double higherCBM(double CBM, double MeasurementCBM, double ExaminationRequestCBM, double FFCBM)
        {

            var res = Math.Max(CBM, MeasurementCBM);
            var rescbm = Math.Max(res, ExaminationRequestCBM);
            var cbm = Math.Max(rescbm, FFCBM);

            return cbm;
        }


        public double GetAdditionalChargesTotalDeliverredUnDeliverd(long ContainerIndexId)
        {

            var chargeAssigning = (from chargeAssignings in Db.ChargeAssignings
                                   where chargeAssignings.ContainerIndexId == ContainerIndexId
                                   select chargeAssignings).ToList().Distinct();


            if (chargeAssigning.Count() > 0)
            {


                double chargeAssigningcountamount = 0;

                foreach (var item in chargeAssigning)
                {
                    chargeAssigningcountamount += item.ChargeAmount;

                }

                return chargeAssigningcountamount;



            }

            else
            {
                return 0;
            }

            return 0;
        }

        public double GetBillToLineTotalDeliverredUnDeliverd(long ContainerIndexId)
        {
            var billToLineAmount = 0.00;

            var asd = Db.ContainerIndices.FromSql($"select * From ContainerIndex Where  ContainerIndexId = {ContainerIndexId}  and IsDeleted = 0 ").FirstOrDefault();
            if (asd != null)
            {

                var resbillToLine = (from billToLine in Db.BillToLines
                                     where billToLine.VirNo == asd.VirNo && billToLine.IndexNo == asd.IndexNo && billToLine.InoviceCreated == true && billToLine.IndexType == "CFS"
                                     select billToLine).ToList();


                if (resbillToLine.Count() > 0)
                {
                    billToLineAmount = resbillToLine.Sum(x => x.InvoiceAmount);

                }
            }

            return billToLineAmount;
        }

        //public long GetShareRate(long? ShippingAgentId, long? ISOCodeHeadId, long? ConsigneId, long? clearingAgentId, long? ShipperId, long? GoodsHeadId, long? PortAndTerminalId)
        //{
        //    long tariifinfoId = 0;

        //    #region MyRegion
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x => x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId  is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && ISOCodeHeadId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && ISOCodeHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId  is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && ShipperId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is  null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }

        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && ISOCodeHeadId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && clearingAgentId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId ={ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && ISOCodeHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && ShipperId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && ConsigneId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && GoodsHeadId != null && clearingAgentId != null && ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && PortAndTerminalId != null && clearingAgentId != null && ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && clearingAgentId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && clearingAgentId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && GoodsHeadId != null && clearingAgentId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && ShipperId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId  is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && GoodsHeadId != null && ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && GoodsHeadId != null && ISOCodeHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && clearingAgentId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && ISOCodeHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId ={PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && clearingAgentId != null && ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && clearingAgentId != null && ISOCodeHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && clearingAgentId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && clearingAgentId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ShipperId != null && ISOCodeHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ShipperId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ConsigneId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && clearingAgentId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && ISOCodeHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShippingAgentId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null && ConsigneId != null && clearingAgentId != null && ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && clearingAgentId != null && ISOCodeHeadId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && ShipperId != null && ISOCodeHeadId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && clearingAgentId != null && ShipperId != null && ISOCodeHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && clearingAgentId != null && ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && clearingAgentId != null && ISOCodeHeadId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && clearingAgentId != null && ISOCodeHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && clearingAgentId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null && ConsigneId != null && ShipperId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null && ConsigneId != null && ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && ISOCodeHeadId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && clearingAgentId != null && ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && clearingAgentId != null && ISOCodeHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && clearingAgentId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && clearingAgentId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();


        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && ShipperId != null && ISOCodeHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();


        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && ShipperId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();


        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();


        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && ISOCodeHeadId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();


        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && ISOCodeHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();


        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && PortAndTerminalId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId} ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();


        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && clearingAgentId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();


        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();


        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && ISOCodeHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();


        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();


        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();


        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ConsigneId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId = {ConsigneId} and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();


        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (GoodsHeadId != null && clearingAgentId != null && ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (GoodsHeadId != null && clearingAgentId != null && ISOCodeHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId} and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null && clearingAgentId != null && ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (GoodsHeadId != null && clearingAgentId != null && ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (PortAndTerminalId != null && clearingAgentId != null && ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId} ").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (GoodsHeadId != null && clearingAgentId != null && ISOCodeHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (PortAndTerminalId != null && clearingAgentId != null && ISOCodeHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (PortAndTerminalId != null && clearingAgentId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (clearingAgentId != null && ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (clearingAgentId != null && ISOCodeHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (clearingAgentId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (clearingAgentId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (clearingAgentId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null && ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null && ShipperId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null && ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShipperId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShipperId != null && ISOCodeHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId ={ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShipperId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShipperId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ShipperId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId = {ShipperId} and ISOCodeHeadId is null  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null && GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null && GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId = {PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (ISOCodeHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId = {ISOCodeHeadId}  and GoodsHeadId is null    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (GoodsHeadId != null && PortAndTerminalId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId ={PortAndTerminalId}").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if (GoodsHeadId != null  )
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId = {GoodsHeadId}    and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }
        //    if ("LCL" != null)
        //    {
        //        var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From  TerminalAndFFShareRate Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and ShipperId is null and ISOCodeHeadId is null  and GoodsHeadId is null  and PortAndTerminalId is null").Where(x =>  x.IndexCargoType == "LCL").LastOrDefault();

        //        if (res != null)
        //        {
        //            tariifinfoId = res.TerminalAndFFShareRateId;

        //            return tariifinfoId;
        //        }
        //    }


        //    #endregion


        //    return tariifinfoId;
        //}



        public long GetShareRate(long? ShippingAgentId, long? GoodsHeadId)
        {
            long tariifinfoId = 0;

            #region MyRegion
            if (ShippingAgentId != null && GoodsHeadId != null)
            {
                var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and GoodsHeadId = {GoodsHeadId}  and IndexCargoType = 'LCL'  and IsDeleted = 0  ").LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TerminalAndFFShareRateId;

                    return tariifinfoId;
                }
            }


            if (ShippingAgentId != null)
            {
                var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and GoodsHeadId  is null  and IndexCargoType = 'LCL'  and IsDeleted = 0 ").LastOrDefault();

                if (res != null)
                {
                    tariifinfoId = res.TerminalAndFFShareRateId;

                    return tariifinfoId;
                }
            }
            if (GoodsHeadId != null)
            {
                var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From TerminalAndFFShareRate Where ShippingAgentId   is null  and GoodsHeadId = { GoodsHeadId}  and IndexCargoType = 'LCL' and IsDeleted = 0  ").LastOrDefault();


                if (res != null)
                {
                    tariifinfoId = res.TerminalAndFFShareRateId;

                    return tariifinfoId;
                }
            }


            #endregion


            return tariifinfoId;
        }


        public double GetStorageTotalLineShare(long ContainerIndexId, long clearingAgentId, DateTime BillDate, DateTime gateInDate, DateTime destuffdate, double Weight, double CBM, string indexcargotype, bool DestuffMark, bool GateInMark)
        {
            var storageTotal = 0.0;
            var storageDays = 0;
            var noOfDays = 0;
            var deliveredweight = 0;
            var freedaysCounted = false;
            var isdgcargo = false;

            if (ContainerIndexId != 0 && clearingAgentId != 0 && gateInDate != null && destuffdate != null && indexcargotype != null && GateInMark == true && DestuffMark == true)
            {

                if (Weight > 0 || CBM > 0)
                {

                    int additionalFreeDays = 0;

                    var container = Db.ContainerIndices.Find(ContainerIndexId);

                    if (container.IsDGCargo == true)
                    {
                        isdgcargo = true;
                    }
                    else
                    {
                        isdgcargo = false;
                    }

                    var shippingAget = Db.ShippingAgents.Find(container.ShippingAgentId);

                    if (shippingAget.WeightAllow == "Yes")
                    {
                        Weight = (Weight * shippingAget.WeightAmount);
                    }



                    //var isocode = (from isocodes in Db.ISOCodes
                    //               from isoCodeHead in Db.ISOCodeHeads.Where(x => x.ISOCodeHeadId == x.ISOCodeHeadId).DefaultIfEmpty()
                    //               where container.ISOCode == isocodes.Code
                    //               select isoCodeHead).FirstOrDefault();


                    var isocode = Db.ISOCodeHeads.FromSql($"select isoCodeHead.ISOCodeHeadId , isoCodeHead.ISOCodeHeadDescription  , isoCodeHead.DeleteDate , isoCodeHead.IsDeleted  from ISOCode   left join isoCodeHead  on ISOCode.ISOCodeHeadId = ISOCodeHead.ISOCodeHeadId  and isoCodeHead.IsDeleted = 0 Where     ISOCode.Code    = {container.ISOCode} and ISOCode.IsDeleted = 0").FirstOrDefault();


                    var calculateCBM = CBM;

                    var calculateWeight = Weight;

                    var weightOrCBM = WeightGreaterThenCBM(calculateWeight, calculateCBM);

                    var weightConv = (Convert.ToDouble(calculateWeight) / 1000.0);

                    if (weightOrCBM == 1)
                    {
                        if (deliveredweight > 0)
                        {
                            weightConv = (weightConv - deliveredweight);
                        }
                    }

                    var tarifList = new List<Tariff>();


                    var res = GetTariffAllList(container.ShippingAgentId, isocode.ISOCodeHeadId, container.ConsigneId, clearingAgentId, container.ShipperId, container.GoodsHeadId, container.PortAndTerminalId, indexcargotype);

                    if (res > 0)
                    {

                        var tarifListres = (from tariffInofrmationTariff in Db.TariffInofrmationTariffs
                                            join tariff in Db.Tariffs on tariffInofrmationTariff.TariffId equals tariff.TariffId

                                            where
                                            tariffInofrmationTariff.TariffInformationId == res
                                            //&& tariff.TariffType == "CFS"
                                            && tariff.TariffHead.Name.Contains("STORAGE")
                                            && (tariff.PortAndTerminalId != null ? tariff.PortAndTerminalId == container.PortAndTerminalId : tariff.PortAndTerminalId == null)
                                            &&
                                            (
                                            tariff.TypeOfImplementation == "All" ? tariff.ImplementFrom == null :
                                            tariff.TypeOfImplementation == "Arrival" ?
                                            Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                            tariff.TypeOfImplementation == "Delivery" ?
                                            Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(tariff.ImplementFrom.Value.Date.ToString("MM/dd/yyyy")) :
                                            tariff.ImplementFrom == null
                                            )
                                            &&
                                            (
                                            tariff.TypeOfImplementation == "All" ?
                                            tariff.ImplementTo == null :
                                            tariff.TypeOfImplementation == "Arrival" && tariff.ImplementTo != null ?
                                            Convert.ToDateTime(gateInDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                            tariff.TypeOfImplementation == "Delivery" && tariff.ImplementTo != null ?
                                            Convert.ToDateTime(BillDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(tariff.ImplementTo.Value.Date.ToString("MM/dd/yyyy")) :
                                            tariff.ImplementTo == null
                                            )
                                            select tariff).ToList();

                        tarifList.AddRange(tarifListres);
                    }



                    if (tarifList.Count() > 0)
                    {
                        //var das = getStoragefreeDays(container.ShippingAgentId, container.ConsigneId, clearingAgentId, container.GoodsHeadId, tarifList[0].TariffId);
                        var das = getStoragefreeDays(container.ShippingAgentId, container.ConsigneId, clearingAgentId, container.GoodsHeadId, res, isdgcargo, "LCL");

                        additionalFreeDays = Convert.ToInt32(das.NoofFreeDays);

                    }


                    var nooffreedays = 0;


                    nooffreedays = additionalFreeDays;



                    nooffreedays = (nooffreedays - 1);



                    if (shippingAget.BillDateType == "VesselArrival")
                    {
                        noOfDays = Convert.ToInt32((BillDate.Date - container.Voyage.BerthOn.Value.AddDays((nooffreedays)).Date).Days);


                    }
                    if (shippingAget.BillDateType == "Destuffed")
                    {

                        noOfDays = Convert.ToInt32((BillDate.Date - destuffdate.AddDays((nooffreedays)).Date).Days);

                    }
                    if (shippingAget.BillDateType == "GateIn")
                    {

                        noOfDays = Convert.ToInt32((BillDate.Date - gateInDate.AddDays((nooffreedays)).Date).Days);

                    }

                    storageDays = noOfDays;

                    foreach (var tariffId in tarifList)
                    {
                        var storageSlabs = Db.StorageSlabs.Where(c => c.TariffId == tariffId.TariffId).ToList();

                        if (storageSlabs.Count() > 0)
                        {



                            if (!freedaysCounted)
                            {
                                noOfDays -= storageSlabs.FirstOrDefault().FreeDays;

                                storageDays = noOfDays;

                                freedaysCounted = true;

                            }

                            if (noOfDays < 0)
                            {
                                noOfDays = 0;

                                storageDays = noOfDays;
                            }

                            var unitToMultiply = weightOrCBM == 1 ? weightConv : calculateCBM;

                            foreach (var storageSlab in storageSlabs)
                            {
                                if (noOfDays <= 0)
                                    break;

                                if (!storageSlab.NoOfDays.ToUpper().Contains("OVER"))
                                {
                                    var period = (noOfDays - Convert.ToInt32(storageSlab.NoOfDays)) > 0 ? Convert.ToInt32(storageSlab.NoOfDays) : noOfDays;


                                    for (int i = 0; i < period; i++)
                                    {
                                        if (tariffId.IsCBMorRate == true)
                                        {
                                            storageTotal += (storageSlab.Rate * unitToMultiply);
                                        }


                                    }

                                    noOfDays -= Convert.ToInt32(storageSlab.NoOfDays);
                                }
                                else
                                {
                                    for (int i = 0; i < noOfDays; i++)
                                    {
                                        if (tariffId.IsCBMorRate == true)
                                        {
                                            storageTotal += (storageSlab.Rate * unitToMultiply);
                                        }

                                    }
                                }
                            }
                        }
                    }

                    if (storageDays < 0)
                    {
                        storageDays = 0;
                    }

                    return storageTotal;

                }
            }

            return storageTotal;
        }




        public long GetExaminationTariffAllList(long? ShippingAgentId, long? GoodsHeadId, string indexcargotype, string examinationType)
        {
            long tariifinfoId = 0;

            if (Db.ExaminationTariffInformation.Where(
                t => t.ShippingAgentId == ShippingAgentId && t.GoodsHeadId == GoodsHeadId && t.IndexCargoType == indexcargotype && t.ExaminationType == examinationType
                && t.ShippingAgentId != null && t.GoodsHeadId != null && t.IndexCargoType != null && t.ExaminationType != null
                && ShippingAgentId != null && GoodsHeadId != null && indexcargotype != null && examinationType != null).Count() > 0)
            {
                var res = Db.ExaminationTariffInformation.Where(
                    t => t.ShippingAgentId == ShippingAgentId && t.GoodsHeadId == GoodsHeadId && t.IndexCargoType == indexcargotype && t.ExaminationType == examinationType
                     && t.ShippingAgentId != null && t.GoodsHeadId != null && t.IndexCargoType != null && t.ExaminationType != null
                && ShippingAgentId != null && GoodsHeadId != null && indexcargotype != null && examinationType != null
                 ).FirstOrDefault();

                if (res != null)
                {

                    tariifinfoId = res.ExaminationTariffInformationId;
                }
            }
            else if (Db.ExaminationTariffInformation.Where(t => t.GoodsHeadId == GoodsHeadId && t.IndexCargoType == indexcargotype && t.ExaminationType == examinationType
                && t.ShippingAgentId == null && t.GoodsHeadId != null && t.IndexCargoType != null && t.ExaminationType != null
                && ShippingAgentId != null && GoodsHeadId != null && indexcargotype != null && examinationType != null).Count() > 0)
            {
                var res = Db.ExaminationTariffInformation.Where(t => t.ShippingAgentId == null && t.GoodsHeadId == GoodsHeadId && t.IndexCargoType == indexcargotype
                 && t.ShippingAgentId == null && t.GoodsHeadId != null && t.IndexCargoType != null && t.ExaminationType != null
                && ShippingAgentId != null && GoodsHeadId != null && indexcargotype != null && examinationType != null).FirstOrDefault();

                if (res != null)
                {


                    tariifinfoId = res.ExaminationTariffInformationId;

                }
            }

            return tariifinfoId;
        }



        public long GetGroundingFreeDays(long? ShippingAgentId, long? ConsigneeId, long? clearingAgentId, long? goodheadid, string indexcargotype)
        {
            long freedays = 1;


            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneeId} and ClearingAgentId = {clearingAgentId} and GoodsHeadId = {goodheadid} and  CargoType = {indexcargotype} and IsDeleted = 0 ").LastOrDefault();

                if (res != null && res.GroundingFreeDays != null)
                {
                    freedays = res.GroundingFreeDays ?? 0;

                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId is null  and ConsigneId = {ConsigneeId} and ClearingAgentId = {clearingAgentId} and GoodsHeadId = {goodheadid} and  CargoType = {indexcargotype} and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.GroundingFreeDays != null)
                {
                    freedays = res.GroundingFreeDays ?? 0;

                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and GoodsHeadId = {goodheadid}  and  CargoType = {indexcargotype} and IsDeleted = 0 ").LastOrDefault();

                if (res != null && res.GroundingFreeDays != null)
                {
                    freedays = res.GroundingFreeDays ?? 0;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneeId} and ClearingAgentId is null and GoodsHeadId = {goodheadid} and  CargoType = {indexcargotype}  and IsDeleted = 0 ").LastOrDefault();

                if (res != null && res.GroundingFreeDays != null)
                {
                    freedays = res.GroundingFreeDays ?? 0;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneeId} and ClearingAgentId = {clearingAgentId} and GoodsHeadId  is null and  CargoType = {indexcargotype}  and IsDeleted = 0 ").LastOrDefault();

                if (res != null && res.GroundingFreeDays != null)
                {
                    freedays = res.GroundingFreeDays ?? 0;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId is null  and ConsigneId  is null and ClearingAgentId = {clearingAgentId} and GoodsHeadId = {goodheadid}  and  CargoType = {indexcargotype} and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.GroundingFreeDays != null)
                {
                    freedays = res.GroundingFreeDays ?? 0;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId is null  and ConsigneId = {ConsigneeId} and ClearingAgentId is null and GoodsHeadId = {goodheadid} and  CargoType = {indexcargotype}  and IsDeleted = 0 ").LastOrDefault();

                if (res != null && res.GroundingFreeDays != null)
                {
                    freedays = res.GroundingFreeDays ?? 0;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and GoodsHeadId = {goodheadid} and  CargoType = {indexcargotype}  and IsDeleted = 0 ").LastOrDefault();

                if (res != null && res.GroundingFreeDays != null)
                {
                    freedays = res.GroundingFreeDays ?? 0;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId is null  and ConsigneId = {ConsigneeId} and ClearingAgentId = {clearingAgentId} and GoodsHeadId is null and  CargoType = {indexcargotype} and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.GroundingFreeDays != null)
                {
                    freedays = res.GroundingFreeDays ?? 0;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and GoodsHeadId is null and  CargoType = {indexcargotype} and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.GroundingFreeDays != null)
                {
                    freedays = res.GroundingFreeDays ?? 0;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneeId} and ClearingAgentId is null and GoodsHeadId is null and  CargoType = {indexcargotype}  and IsDeleted = 0 ").LastOrDefault();

                if (res != null && res.GroundingFreeDays != null)
                {
                    freedays = res.GroundingFreeDays ?? 0;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and GoodsHeadId is null and  CargoType = {indexcargotype} and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.GroundingFreeDays != null)
                {
                    freedays = res.GroundingFreeDays ?? 0;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId is null  and ConsigneId = {ConsigneeId} and ClearingAgentId is null and GoodsHeadId is null and  CargoType = {indexcargotype} and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.GroundingFreeDays != null)
                {
                    freedays = res.GroundingFreeDays ?? 0;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and GoodsHeadId is null and  CargoType = {indexcargotype} and IsDeleted = 0 ").LastOrDefault();

                if (res != null && res.GroundingFreeDays != null)
                {
                    freedays = res.GroundingFreeDays ?? 0;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and GoodsHeadId = {goodheadid} and  CargoType = {indexcargotype}  and IsDeleted = 0 ").LastOrDefault();

                if (res != null && res.GroundingFreeDays != null)
                {
                    freedays = res.GroundingFreeDays ?? 0;
                    return freedays;
                }
            }

            //if (Db.GroundingFreeDays.Where(t => t.ShippingAgentId == ShippingAgentId && t.ConsigneId == ConsigneeId 
            //                               && t.ShippingAgentId != null && t.ConsigneId != null 
            //                               && ShippingAgentId != null && ConsigneeId != null).Count() > 0)
            //{
            //    var res = Db.GroundingFreeDays.Where(t => t.ShippingAgentId == ShippingAgentId && t.ConsigneId == ConsigneeId && t.ShippingAgentId != null && t.ConsigneId != null
            //                                          && ShippingAgentId != null && ConsigneeId != null).FirstOrDefault();

            //    if (res != null)
            //    {
            //         if(res.GroundingFreeDays != null)
            //        {
            //            freedays = res.GroundingFreeDays ?? 0;
            //        }
            //    }
            //}
            //else if (Db.GroundingFreeDays.Where(t => t.ShippingAgentId == ShippingAgentId && t.ConsigneId == null &&  t.ShippingAgentId != null && t.ConsigneId == null 
            //&& ShippingAgentId != null && ConsigneeId != null ).Count() > 0)
            //{
            //    var res = Db.GroundingFreeDays.Where(t => t.ShippingAgentId == ShippingAgentId && t.ConsigneId == null && t.ShippingAgentId != null && t.ConsigneId == null
            //&& ShippingAgentId != null ).FirstOrDefault();

            //    if (res != null)
            //    {

            //      //  freedays = res.GroundingFreeDays;

            //    }
            //}

            return freedays;
        }


        public List<InvoiceItemViewModel> GetEmptyContainertariff(long shippingAgentId, string size, DateTime arrivedate)
        {

            long totalfreedays = 0;
            long storageTotal = 0;
            var todaydate = DateTime.Now;
            var containersize = Convert.ToInt32(size);

            var tariffInfos = new List<InvoiceItemViewModel>();

            var tariffitems = (from emptycontainertariff in Db.EmptyContainerTariffs
                               where emptycontainertariff.ShippingAgentId == shippingAgentId
                               select new InvoiceItemViewModel
                               {
                                   Description = emptycontainertariff.EmptyContainerTariffName,
                                   TariffId = emptycontainertariff.EmptyContainerTariffId,
                                   Amount = containersize == 20 ? emptycontainertariff.Rate20 : containersize == 40 ? emptycontainertariff.Rate40 : containersize == 45 ? emptycontainertariff.Rate45 : 0,

                               }).ToList();


            tariffInfos.AddRange(tariffitems);




            var storagetariff = tariffInfos.Where(x => x.Description.Contains("STORAGE")).ToList();

            if (storagetariff.Count() > 0)
            {
                var freedays = (from emptyContainerTaxAndFreeDay in Db.EmptyContainerTaxAndFreeDays
                                where emptyContainerTaxAndFreeDay.ShippingAgentId == shippingAgentId
                                select emptyContainerTaxAndFreeDay).LastOrDefault();

                if (freedays != null)
                {

                    if (freedays.FreeDays > 0)
                    {
                        totalfreedays = freedays.FreeDays;
                    }


                }

                totalfreedays = (totalfreedays - 1);

                var noOfDays = Convert.ToInt32((todaydate.Date - arrivedate.Date.AddDays(totalfreedays).Date).Days);

                foreach (var tariff in storagetariff)
                {
                    var storageSlabs = Db.EmptyContainerStorageSlabs.Where(c => c.EmptyContainerTariffId == tariff.TariffId).ToList();

                    if (storageSlabs.Count() > 0)
                    {
                        foreach (var storageSlab in storageSlabs)
                        {
                            if (noOfDays <= 0)
                                break;


                            if (!storageSlab.NoOfDays.ToUpper().Contains("OVER"))
                            {
                                var period = (noOfDays) - (Convert.ToInt32(storageSlab.NoOfDays) > 0 ? Convert.ToInt32(storageSlab.NoOfDays) : noOfDays);

                                if (period > Convert.ToInt32(storageSlab.NoOfDays) || noOfDays > Convert.ToInt32(storageSlab.NoOfDays))
                                    period = Convert.ToInt32(storageSlab.NoOfDays);
                                else if (period < Convert.ToInt32(storageSlab.NoOfDays))
                                    period = noOfDays;

                                for (int i = 0; i < period; i++)
                                {

                                    storageTotal += containersize == 20 ? storageSlab.Rate20 : containersize == 40 ? storageSlab.Rate40 : containersize == 45 ? storageSlab.Rate45 : 0;


                                }

                                if (noOfDays < Convert.ToInt32(storageSlab.NoOfDays))
                                    noOfDays -= period;
                                else
                                    noOfDays -= Convert.ToInt32(storageSlab.NoOfDays);
                            }
                            else
                            {
                                for (int i = 0; i < noOfDays; i++)
                                {

                                    storageTotal += containersize == 20 ? storageSlab.Rate20 : containersize == 40 ? storageSlab.Rate40 : containersize == 45 ? storageSlab.Rate45 : 0;

                                }
                            }



                        }

                    }



                }


                if (storageTotal > 0)
                {

                    var invoiceitm = new InvoiceItemViewModel
                    {
                        Description = "STORAGE",
                        Amount = storageTotal,

                    };

                    tariffInfos.Add(invoiceitm);

                }




            }

            tariffInfos.RemoveAll(x => x.Amount <= 0);

            return tariffInfos;

        }



        public EmptyContainerTaxAndFreeDay GetEmptyContainerdays(long shippingAgentId, DateTime arrivedate)
        {
            var todaydate = DateTime.Now;
            long totalfreedays = 0;
            long totalsaletax = 0;

            var freedays = (from emptyContainerTaxAndFreeDay in Db.EmptyContainerTaxAndFreeDays
                            where emptyContainerTaxAndFreeDay.ShippingAgentId == shippingAgentId
                            select emptyContainerTaxAndFreeDay).LastOrDefault();

            if (freedays != null)
            {
                if (freedays.FreeDays > 0)
                {
                    totalfreedays = freedays.FreeDays;
                }

                if (freedays.SalesTax > 0)
                {
                    totalsaletax = freedays.SalesTax;
                }



            }

            totalfreedays = (totalfreedays - 1);

            var noOfDays = Convert.ToInt32((todaydate.Date - arrivedate.Date.AddDays(totalfreedays).Date).Days);

            totalfreedays = noOfDays;

            if (totalfreedays <= 0)
            {
                totalfreedays = 0;
            }

            var EmptyContainerTaxAndFreeDay = new EmptyContainerTaxAndFreeDay();

            EmptyContainerTaxAndFreeDay.FreeDays = totalfreedays;
            EmptyContainerTaxAndFreeDay.SalesTax = totalsaletax;

            return EmptyContainerTaxAndFreeDay;
        }




        public FreeDaysViewModel getStoragefreeDays(long? ShippingAgentId, long? ConsigneId, long? clearingAgentId, long? GoodsHeadId, long tariffid, bool isdgcargo, string indexcargotype)
        {
            if (GoodsHeadId != null)
            {
                var res = (from goodsHead in Db.GoodsHeads
                           where goodsHead.GoodsHeadId == GoodsHeadId
                           select goodsHead).FirstOrDefault();

                if (res != null)
                {

                    var days = GetGroundingFreeDaysForStorage(ShippingAgentId, ConsigneId, clearingAgentId, GoodsHeadId, indexcargotype);

                    if (days != null)
                    {
                        return new FreeDaysViewModel { FreeDaysType = "Conditional", NoofFreeDays = days ?? 0 };
                    }

                    var sdays = GetStorageFreeDaysFromTariffId(tariffid, isdgcargo);

                    if (sdays != null)
                    {
                        if (isdgcargo == true && sdays != null)
                        {
                            return new FreeDaysViewModel { FreeDaysType = "DG  Days", NoofFreeDays = sdays ?? 0 };
                        }
                        else
                        {
                            return new FreeDaysViewModel { FreeDaysType = "Storage Days", NoofFreeDays = sdays ?? 0 };
                        }

                    }

                    return new FreeDaysViewModel { FreeDaysType = "Goods Head", NoofFreeDays = res.StorageFreeDays };

                }
                else
                {
                    return new FreeDaysViewModel { FreeDaysType = "", NoofFreeDays = 0 };
                }

            }
            return new FreeDaysViewModel { FreeDaysType = "", NoofFreeDays = 0 };

        }



        public long? GetStorageFreeDaysFromTariffId(long tariffid, bool isdgcargo)
        {
            long? freedays = null;

            if (tariffid != null || tariffid != 0)
            {
                var res = Db.TariffInformations.FromSql($"SELECT * From TariffInformation Where TariffInformationId = {tariffid}  and IsDeleted = 0 ").LastOrDefault();

                if (isdgcargo == true)
                {
                    if (res != null && res.DGFreeDays != null)
                    {
                        freedays = res.DGFreeDays;

                        return freedays;
                    }
                }
                else
                {
                    if (res != null && res.StorageFreeDays != null)
                    {
                        freedays = res.StorageFreeDays;

                        return freedays;
                    }

                }

            }


            return freedays;
        }

        public long? GetGroundingFreeDaysForStorage(long? ShippingAgentId, long? ConsigneeId, long? clearingAgentId, long? goodheadid, string indexcargotype)
        {
            long? freedays = null;


            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneeId} and ClearingAgentId = {clearingAgentId} and GoodsHeadId = {goodheadid}  and  CargoType = {indexcargotype}   and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.StorageFreeFreeDays != null)
                {
                    freedays = res.StorageFreeFreeDays;

                    return freedays;
                }
            }
            if (ConsigneeId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId is null  and ConsigneId = {ConsigneeId} and ClearingAgentId = {clearingAgentId} and GoodsHeadId = {goodheadid}  and  CargoType = {indexcargotype}   and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.StorageFreeFreeDays != null)
                {
                    freedays = res.StorageFreeFreeDays;

                    return freedays;
                }
            }
            if (ShippingAgentId != null && clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and GoodsHeadId = {goodheadid}  and  CargoType = {indexcargotype}  and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.StorageFreeFreeDays != null)
                {
                    freedays = res.StorageFreeFreeDays;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneeId} and ClearingAgentId is null and GoodsHeadId = {goodheadid}  and  CargoType = {indexcargotype}  and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.StorageFreeFreeDays != null)
                {
                    freedays = res.StorageFreeFreeDays;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null && clearingAgentId != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneeId} and ClearingAgentId = {clearingAgentId} and GoodsHeadId  is null  and  CargoType = {indexcargotype}  and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.StorageFreeFreeDays != null)
                {
                    freedays = res.StorageFreeFreeDays;
                    return freedays;
                }
            }
            if (clearingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId is null  and ConsigneId  is null and ClearingAgentId = {clearingAgentId} and GoodsHeadId = {goodheadid}   and  CargoType = {indexcargotype}  and IsDeleted = 0 ").LastOrDefault();

                if (res != null && res.StorageFreeFreeDays != null)
                {
                    freedays = res.StorageFreeFreeDays;
                    return freedays;
                }
            }
            if (ConsigneeId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId is null  and ConsigneId = {ConsigneeId} and ClearingAgentId is null and GoodsHeadId = {goodheadid}  and  CargoType = {indexcargotype}  and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.StorageFreeFreeDays != null)
                {
                    freedays = res.StorageFreeFreeDays;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and GoodsHeadId = {goodheadid}  and  CargoType = {indexcargotype}  and IsDeleted = 0 ").LastOrDefault();

                if (res != null && res.StorageFreeFreeDays != null)
                {
                    freedays = res.StorageFreeFreeDays;
                    return freedays;
                }
            }
            if (ConsigneeId != null && clearingAgentId != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId is null  and ConsigneId = {ConsigneeId} and ClearingAgentId = {clearingAgentId} and GoodsHeadId is null  and  CargoType = {indexcargotype}   and IsDeleted = 0 ").LastOrDefault();

                if (res != null && res.StorageFreeFreeDays != null)
                {
                    freedays = res.StorageFreeFreeDays;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && clearingAgentId != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and GoodsHeadId is null  and  CargoType = {indexcargotype}  and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.StorageFreeFreeDays != null)
                {
                    freedays = res.StorageFreeFreeDays;
                    return freedays;
                }
            }
            if (ShippingAgentId != null && ConsigneeId != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId = {ConsigneeId} and ClearingAgentId is null and GoodsHeadId is null  and  CargoType = {indexcargotype}  and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.StorageFreeFreeDays != null)
                {
                    freedays = res.StorageFreeFreeDays;
                    return freedays;
                }
            }
            if (clearingAgentId != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId = {clearingAgentId} and GoodsHeadId is null  and  CargoType = {indexcargotype}  and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.StorageFreeFreeDays != null)
                {
                    freedays = res.StorageFreeFreeDays;
                    return freedays;
                }
            }
            if (ConsigneeId != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId is null  and ConsigneId = {ConsigneeId} and ClearingAgentId is null and GoodsHeadId is null  and  CargoType = {indexcargotype}  and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.StorageFreeFreeDays != null)
                {
                    freedays = res.StorageFreeFreeDays;
                    return freedays;
                }
            }
            if (ShippingAgentId != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId = {ShippingAgentId}  and ConsigneId is null and ClearingAgentId is null and GoodsHeadId is null  and  CargoType = {indexcargotype}  and IsDeleted = 0  ").LastOrDefault();

                if (res != null && res.StorageFreeFreeDays != null)
                {
                    freedays = res.StorageFreeFreeDays;
                    return freedays;
                }
            }
            if (goodheadid != null)
            {
                var res = Db.GroundingFreeDays.FromSql($"SELECT * From GroundingFreeDay Where ShippingAgentId is null  and ConsigneId is null and ClearingAgentId is null and GoodsHeadId = {goodheadid}  and  CargoType = {indexcargotype}  and IsDeleted = 0 ").LastOrDefault();

                if (res != null && res.StorageFreeFreeDays != null)
                {
                    freedays = res.StorageFreeFreeDays;
                    return freedays;
                }
            }

            return freedays;
        }


        public List<InvoiceItem> GetInvoiceItemBycontainerIndexId(long ContainerIndexId)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.InvoiceItems.FromSql($"select InvoiceItem.* from invoice join InvoiceItem on Invoice.InvoiceId = InvoiceItem.InvoiceId and InvoiceItem.IsDeleted = 0  where Invoice.ContainerIndexId = {ContainerIndexId} and Invoice.IsDeleted = 0").ToList();

            return asd;

        }

        public List<InvoiceItem> GetInvoiceItemBycycontainerId(long CYContainerId)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.InvoiceItems.FromSql($"select InvoiceItem.* from invoice join InvoiceItem on Invoice.InvoiceId = InvoiceItem.InvoiceId and InvoiceItem.IsDeleted = 0  where Invoice.CYContainerId = {CYContainerId} and Invoice.IsDeleted = 0").ToList();

            return asd;

        }


        public Invoice GetInvoiceLast()
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.Invoices.FromSql($"select * from invoice  where IsDeleted = 0 and InvoiceCategory = 'AICT' ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public Invoice GetInvoiceLastForFF()
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.Invoices.FromSql($"select * from invoice  where IsDeleted = 0 and InvoiceCategory = 'FF' ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public CreditAllowInfoViewModel GetInfoForCreditAllowCFS(string IGM, long index)
        {


            var res = new CreditAllowInfoViewModel();
            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {IGM} and IndexNo = {index} and IsDeleted = 0").FirstOrDefault();

            var st = Db.SalesTaxes.FromSql($"select * from SalesTax where Type  = 'Import' and IsDeleted = 0").FirstOrDefault();

            var creditAllow = Db.CreditAlloweds.FromSql($"select * from creditallowed where VirNumber  = {IGM} and IndexNumber = {index}  and IsDeleted = 0").LastOrDefault();

            var invoicedetai = (from containerIndex in Db.ContainerIndices
                                from destuff in Db.DestuffedContainers.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                                from examinationRequest in Db.ExaminationRequests.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()

                                where containerIndex.ContainerIndexId == asd.ContainerIndexId
                                select new AICTAndLineTariffViewModel
                                {
                                    ContainerIndexId = containerIndex.ContainerIndexId,
                                    Virnumber = containerIndex.VirNo,
                                    IndexNumber = containerIndex.IndexNo,
                                    GateInDate = GetCFSContainerGateInDate(asd.ContainerIndexId),
                                    CBM = containerIndex.CBM,
                                    Weight = containerIndex.ManifestedWeight * 1000,
                                    MeasurementCBM = containerIndex.MeasurementCBM,
                                    DestuffDate = GetDestuffingDate(asd.ContainerIndexId),
                                    //DestuffDate = destuff.TellySheet != null ? destuff.TellySheet.DestuffDate : null,
                                    ClearingAgentId = examinationRequest != null ? examinationRequest.ClearingAgentId ?? 0 : 0,
                                    ExaminationRequestCBM = examinationRequest != null ? examinationRequest.ExaminationRequestCBM : 0,
                                    FFCBM = containerIndex.FFCBM,
                                    HigherCBM = higherCBM(containerIndex.CBM, containerIndex.MeasurementCBM, examinationRequest != null ? examinationRequest.ExaminationRequestCBM : 0, containerIndex.FFCBM),
                                    CargoType = containerIndex.CargoType,
                                }).FirstOrDefault();

            if (invoicedetai != null)
            {
                var resdata = GetCBMTotal(invoicedetai.ContainerIndexId, invoicedetai.ClearingAgentId, invoicedetai.Weight ?? 0, invoicedetai.HigherCBM ?? 0, invoicedetai.CargoType, invoicedetai.GateInDate ?? DateTime.Now, DateTime.Now);

                var storagerate = GetStorageTotal(invoicedetai.ContainerIndexId, invoicedetai.ClearingAgentId, DateTime.Now, invoicedetai.GateInDate ?? DateTime.Now, invoicedetai.DestuffDate ?? DateTime.Now, invoicedetai.Weight ?? 0, invoicedetai.HigherCBM ?? 0, invoicedetai.CargoType);


                res.OtherCharges = GetAdditionalChargesTotalForCreditallow(asd.ContainerIndexId);

                res.FFAictShare = resdata.Sum(x => x.Amount);

                res.Storage = storagerate.StorageTotal;

                var total = res.FFAictShare + res.Storage + res.OtherCharges;

                var totalamountst = (total / 100) * st.SalesTaxAmount;

                res.TotalCharges = total + totalamountst;

                res.CBM = invoicedetai.HigherCBM ?? 0;
                res.Weight = invoicedetai.Weight ?? 0;

                var data = (from containerIndex in Db.ContainerIndices
                            from ff in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerIndex.ShippingAgentId).DefaultIfEmpty()
                            from cons in Db.Consignes.Where(x => x.ConsigneId == containerIndex.ConsigneId).DefaultIfEmpty()
                            from ca in Db.ClearingAgents.Where(x => x.ClearingAgentId == invoicedetai.ClearingAgentId).DefaultIfEmpty()


                            where containerIndex.ContainerIndexId == asd.ContainerIndexId
                            select new CreditAllowInfoViewModel
                            {
                                FF = ff != null ? ff.Name : "",
                                Consignee = cons != null ? cons.ConsigneName : "",
                                ClearingAgent = ca.Name,


                            }).FirstOrDefault();

                if (data != null)
                {

                    data.CBM = res.CBM;
                    data.ClearingAgent = data.ClearingAgent;
                    data.Consignee = data.Consignee;
                    data.FF = data.FF;
                    data.FFAictShare = res.FFAictShare;
                    data.Storage = res.Storage;
                    data.TotalCharges = res.TotalCharges;
                    data.Weight = res.Weight;
                    data.OtherCharges = res.OtherCharges;
                    data.CreditAllowedRs = creditAllow != null ? creditAllow.CreditAllowedRs : 0;
                    data.CreditDays = creditAllow != null ? creditAllow.CreditDays : 0;
                    data.Remarks = creditAllow != null ? creditAllow.Remarks : "";

                    data.InvoiceNo = creditAllow != null ? creditAllow.InvoiceNo : "";
                    data.CreatedDate = creditAllow != null ? creditAllow.CreatedDate : null;



                }


                return data;
            }


            return res;


        }


        public CreditAllowInfoViewModel GetInfoForCreditAllowCY(string IGM, long index)
        {


            var res = new CreditAllowInfoViewModel();

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VirNo = {IGM} and IndexNo = {index} and IsDeleted = 0").FirstOrDefault();

            var st = Db.SalesTaxes.FromSql($"select * from SalesTax where Type  = 'Import' and IsDeleted = 0").FirstOrDefault();
            var creditAllow = Db.CreditAlloweds.FromSql($"select * from creditallowed where VirNumber  = {IGM} and IndexNumber = {index}  and IsDeleted = 0").LastOrDefault();

            var invoicedetai = (from containerIndex in Db.CYContainers
                                from examinationRequest in Db.ExaminationRequests.Where(x => x.CYContainerId == containerIndex.CYContainerId).DefaultIfEmpty()

                                where containerIndex.CYContainerId == asd.CYContainerId
                                select new AICTAndLineTariffViewModel
                                {
                                    Virnumber = containerIndex.VIRNo,
                                    IndexNumber = containerIndex.IndexNo,
                                    GateInDate = GetCYContainerGateInDate(asd.CYContainerId),
                                    ClearingAgentId = examinationRequest != null ? examinationRequest.ClearingAgentId ?? 0 : 0,
                                    CargoType = containerIndex.CargoType,
                                    Weight = containerIndex.BLGrossWeight != null ? Convert.ToInt32(containerIndex.BLGrossWeight) : 0,
                                }).FirstOrDefault();

            if (invoicedetai != null)
            {
                var resdata = GetSizeTotal(IGM, Convert.ToInt32(index), invoicedetai.ClearingAgentId, invoicedetai.GateInDate ?? DateTime.Now, 1, "CY", DateTime.Now);

                var storagerate = GetStorageTotalCY(IGM, Convert.ToInt32(index), DateTime.Now, invoicedetai.GateInDate ?? DateTime.Now, invoicedetai.ClearingAgentId, "CY");


                var sealchrges = GetSealAmount(asd.CYContainerId);

                var additionalchrges = GetadditionChargesTotalCYCreditallow(asd.CYContainerId);

                res.OtherCharges = sealchrges + additionalchrges;

                res.FFAictShare = resdata.Sum(x => x.Amount);

                res.Storage = storagerate.StorageTotal;

                var total = res.FFAictShare + res.Storage + res.OtherCharges;

                var totalamountst = (total / 100) * st.SalesTaxAmount;

                res.TotalCharges = total + totalamountst;

                res.CBM = invoicedetai.HigherCBM ?? 0;
                res.Weight = invoicedetai.Weight ?? 0;

                var data = (from containerIndex in Db.CYContainers
                            from ff in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerIndex.ShippingAgentId).DefaultIfEmpty()
                            from cons in Db.Consignes.Where(x => x.ConsigneId == containerIndex.ConsigneId).DefaultIfEmpty()
                            from ca in Db.ClearingAgents.Where(x => x.ClearingAgentId == invoicedetai.ClearingAgentId).DefaultIfEmpty()

                            where containerIndex.CYContainerId == asd.CYContainerId
                            select new CreditAllowInfoViewModel
                            {
                                FF = ff != null ? ff.Name : "",
                                Consignee = cons != null ? cons.ConsigneName : "",
                                ClearingAgent = ca.Name,

                            }).FirstOrDefault();

                if (data != null)
                {

                    data.CBM = res.CBM;
                    data.ClearingAgent = data.ClearingAgent;
                    data.Consignee = data.Consignee;
                    data.FF = data.FF;
                    data.FFAictShare = res.FFAictShare;
                    data.Storage = res.Storage;
                    data.TotalCharges = res.TotalCharges;
                    data.Weight = res.Weight;

                    data.CreditAllowedRs = creditAllow != null ? creditAllow.CreditAllowedRs : 0;
                    data.CreditDays = creditAllow != null ? creditAllow.CreditDays : 0;
                    data.Remarks = creditAllow != null ? creditAllow.Remarks : "";


                    data.InvoiceNo = creditAllow != null ? creditAllow.InvoiceNo : "";
                    data.CreatedDate = creditAllow != null ? creditAllow.CreatedDate : null;


                }


                return data;
            }


            return res;


        }


        public Invoice GetInvoiceByInvoiceNumber(string invoiceno)
        {
            var res = Db.Invoices.FromSql($"select * from Invoice where InvoiceNo = {invoiceno} and IsDeleted = 0").LastOrDefault();

            if (res != null)
            {
                return res;

            }
            return null;
        }

        public Invoice GetInvoiceunsetteldexcessinvoice(string invoiceno)
        {
            var res = Db.Invoices.FromSql($"select * from Invoice where InvoiceNo = {invoiceno} and (KnockOffInvoiceNumber = '' or KnockOffInvoiceNumber is null   ) and IsDeleted = 0").LastOrDefault();

            if (res != null)
            {
                return res;

            }
            return null;
        }

        public List<PaymentReceived> GetPaymentReceivedByInvoiceNumber(string invoiceno)
        {
            var res = Db.PaymentReceiveds.FromSql($"select * from PaymentReceived where InoviceNo = {invoiceno} and IsDeleted = 0").ToList();

            return res;

        }


        public TerminalAndFFShareRate GetTerminalAndFFShareRate(long? ShippingAgentId, long? goodheadid, string indexcargotype)
        {

            if (ShippingAgentId != null && goodheadid != null)
            {
                var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId} and GoodsHeadId = {goodheadid}  and IndexCargoType = {indexcargotype} and IsDeleted = 0  ").LastOrDefault();

                if (res != null)
                {
                    return res;
                }
            }

            if (ShippingAgentId != null)
            {
                var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and GoodsHeadId is null  and IndexCargoType = {indexcargotype} and IsDeleted = 0  ").LastOrDefault();

                if (res != null)
                {
                    return res;
                }
            }
            if (goodheadid != null)
            {
                var res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From TerminalAndFFShareRate Where ShippingAgentId is null and GoodsHeadId = {goodheadid} and IndexCargoType = {indexcargotype}  and IsDeleted = 0 ").LastOrDefault();

                if (res != null)
                {
                    return res;
                }
            }

            return null;
        }

        public Invoice GetCFSFirstInvoice(long ContainerIndexId)
        {
            var asd = Db.Invoices.FromSql($"select * From Invoice Where  ContainerIndexId = {ContainerIndexId}  and IsDeleted = 0 ").FirstOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public Invoice GetCYFirstInvoice(long CYContainerId)
        {
            var asd = Db.Invoices.FromSql($"select * From Invoice Where  CYContainerId = {CYContainerId}  and IsDeleted = 0 ").FirstOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public DeliveryOrder GetDeliveryOrder(long dono)
        {
            var asd = Db.DeliveryOrders.FromSql($"select * From DeliveryOrder Where  DONumber = {dono}  and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public CYContainer GetFirstCyContainer(long CyContainerId)
        {
            var asd = Db.CYContainers.FromSql($"select * From CYContainer Where  CYContainerId = {CyContainerId}  and IsDeleted = 0 ").FirstOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public DeliveryOrder GetDeliveryOrderLast()
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.DeliveryOrders.FromSql($"select * from DeliveryOrder  where  IsDeleted = 0").LastOrDefault();

            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public DataTable dtTable = new DataTable();

        public List<AictLineIndexUploadReportViewModel> UploadAictLineIndexReport(DateTime? fromdate, DateTime? todate, DateTime? uploadfromdate, DateTime? uploadtodate, string igm, string Containerno,
                                                                                 long? masterline, long? line, string Delivered, string billtoline)
        {

            var BillDate = DateTime.Now;


            var containerList = new List<AictLineIndexUploadReportViewModel>();

            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            string conString = Con;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("LineBoxIndexWiseDetailReport", con))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@fromdate", fromdate?.ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("@todate", todate?.ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("@uploadfromdate", uploadfromdate?.ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("@uploadtodate", uploadtodate?.ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("@igm", igm);
                    command.Parameters.AddWithValue("@Containerno", Containerno);
                    command.Parameters.AddWithValue("@masterline", masterline);
                    command.Parameters.AddWithValue("@line", line);
                    command.Parameters.AddWithValue("@Delivered", Delivered);
                    command.Parameters.AddWithValue("@billtoline", billtoline);

                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(command);

                    sda.Fill(ds);

                    containerList = ds.Tables[0].AsEnumerable()
                        .Select(datarow => new AictLineIndexUploadReportViewModel()
                        {
                            MasterLineName = datarow.Field<string>("MasterShippingAgentName"),
                            LineName = datarow.Field<string>("Name"),
                            ContainerNumber = datarow.Field<string>("ContainerNo"),
                            ArrivalDate = datarow.Field<DateTime>("CFSContainerGateInDate"),
                            Size = datarow.Field<int>("Size"),
                            IGMNo = datarow.Field<string>("VirNo"),
                            IndexNo = datarow.Field<int?>("IndexNo"),
                            HigherCBM = datarow.Field<double>("TOTALCBM"),
                            PerBoxRate = datarow.Field<double>("PerBoxRate"),
                            ContainerIndexId = datarow.Field<long>("ContainerIndexId"),
                            AICTPerCBMRate = datarow.Field<double>("AICTPerCBMRate"),
                            AICTPerIndexRate = datarow.Field<double>("AICTPerIndexRate"),
                            AdditionalChargeAICTPerCBMRate = datarow.Field<double>("AICTPerCBMRateShareRate"),
                            AdditionalChargeAICTPerIndexRate = datarow.Field<double>("AICTPerIndexRateShareRate"),
                            TotalAICT = datarow.Field<double>("TotalAICT"),

                            FFPerCBMRate = datarow.Field<double>("FFPerCBMRate"),
                            FFPerIndexRate = datarow.Field<double>("FFPerIndexRate"),
                            AdditionalChargeFFPerCBMRate = datarow.Field<double>("FFPerCBMRateShareRate"),
                            AdditionalChargeFFPerIndexRate = datarow.Field<double>("FFPerIndexRateShareRate"),
                            TotalFF = datarow.Field<double>("TotalFF"),

                            TotalCBMRate = datarow.Field<double>("TotalCBMRate"),
                            TotalPerIndexRate = datarow.Field<double>("TotalPerIndexRate"),

                            TotalCharges = datarow.Field<double>("TotalAmount"),

                            BillToLine = datarow.Field<double>("BillToLine"),
                            SpecialCharges = datarow.Field<double>("SpecialCharges"),
                            PortCharges = datarow.Field<double>("PortCharges"),
                            DeliveryDate = datarow.Field<DateTime?>("GatePassDate"),

                        }).ToList();

                    con.Close();
                }
            }



            var invoiceDetails = new List<AICTAndLineTariffViewModel>();

            //if(containerList.Count() > 0)
            //{             
            //     foreach (var item in containerList)
            //    {
            //        var resdata = GetInvoiceDetail(item.ContainerIndexId);
            //        if (resdata != null)
            //        {
            //            item.StorageCharges = GetStorageTotalLineShare(resdata.ContainerIndexId, resdata.ClearingAgentId, DateTime.Now, item.ArrivalDate ?? DateTime.Now, resdata.DestuffDate ?? DateTime.Now, resdata.Weight ?? 0.00, resdata.HigherCBM ?? 0, resdata.CargoType, resdata.DestuffMark, resdata.GateInMark);
            //        }
            //        item.HigherCBM = resdata.HigherCBM;                 
            //    }
            //}


            return containerList;
        }

        public double GetadditionChargesTotalCYCreditallow(long CYContainerId)
        {
            var data = Db.ChargeAssignings.FromSql($"select * from OtherChargeAssigning   where  CyContainerId = {CYContainerId}   and IsDeleted = 0   ").ToList().Distinct();
            if (data.Count() > 0)
            {
                double AdditionalChargesamount = 0;
                AdditionalChargesamount = data.Sum(x => x.ChargeAmount);
                return AdditionalChargesamount;
            }

            else
            {
                return 0;
            }


        }


        public double GetAdditionalChargesTotalForCreditallow(long ContainerIndexId)
        {

            var chargeAssigning = Db.ChargeAssignings.FromSql($"select * from OtherChargeAssigning   where  ContainerIndexId = {ContainerIndexId}   and IsDeleted = 0   ").ToList().Distinct();

            if (chargeAssigning.Count() > 0)
            {
                double chargeAssigningcountamount = 0;
                chargeAssigningcountamount = chargeAssigning.Sum(x => x.ChargeAmount);
                return chargeAssigningcountamount;


            }

            else
            {
                return 0;
            }





        }



        public List<InvoiceItem> GetInvoiceItemById(long ContainerIndexId)
        {
            var res = (from Invoice in Db.Invoices
                       join ivoiceitem in Db.InvoiceItems on Invoice.InvoiceId equals ivoiceitem.InvoiceId
                       where
                       Invoice.ContainerIndexId == ContainerIndexId
                       select new InvoiceItem
                       {
                           Amount = ivoiceitem.Amount,
                           Description = ivoiceitem.Description,
                           Type = ivoiceitem.Type,
                           Category = ivoiceitem.Type
                       }).ToList();

            return res;

        }

        public IEnumerable<AICTAndLineIndexTariffRatesViewModel> AICTAndLineIndexTariffRates(string virno, string containerno, string indexno, long? agent, long? goodhead, string type, DateTime? from, DateTime? to)
        {



            var listItems = new List<AICTAndLineIndexTariffRatesViewModel>();



            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string conString = Con;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("AICTAndLineIndexTariffRates", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 360;

                    command.Parameters.AddWithValue("@virnumber", virno);
                    command.Parameters.AddWithValue("@indexnumber", indexno);
                    command.Parameters.AddWithValue("@containernumber", containerno);
                    command.Parameters.AddWithValue("@shippingAgentId", agent);
                    command.Parameters.AddWithValue("@goodheadId", goodhead);
                    command.Parameters.AddWithValue("@status", type);
                    command.Parameters.AddWithValue("@fromdate", from?.ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("@todate", to?.ToString("MM/dd/yyyy"));


                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(command);

                    sda.Fill(ds);

                    listItems = ds.Tables[0].AsEnumerable()
                        .Select(datarow => new AICTAndLineIndexTariffRatesViewModel()
                        {
                            Key = datarow.Field<string>("key"),
                            containerIndexId = datarow.Field<long>("ContainerIndexId"),
                            IGMNo = datarow.Field<string>("IGMNo"),
                            ContainerNumber = datarow.Field<string>("ContainerNumber"),
                            ContainerSize = datarow.Field<int>("ContainerSize"),
                            IndexNo = datarow.Field<int?>("IndexNo"),
                            ShippingAgentName = datarow.Field<string>("ShippingAgentName"),
                            GoodHeadName = datarow.Field<string>("GoodHeadName"),
                            CFSContainerGateInDate = datarow.Field<DateTime?>("ArrivalDate"),
                            IsDelivered = datarow.Field<int>("IsDelivered") == 1 ? true : false,
                            DeliveryDate = datarow.Field<DateTime?>("DeliveryDate"),
                            HigherCBM = datarow.Field<double>("HigherCBM"),
                            AICTPerCBMRate = datarow.Field<double>("AICTPerCBMRate"),
                            AICTPerIndexRate = datarow.Field<double>("AICTPerIndexRate"),
                            FFPerCBMRate = datarow.Field<double>("FFPerCBMRate"),
                            FFPerIndexRate = datarow.Field<double>("FFPerIndexRate"),
                            AdditionalChargeAICTPerCBMRate = datarow.Field<double>("AdditionalChargeAICTPerCBMRate"),
                            AdditionalChargeAICTPerIndexRate = datarow.Field<double>("AdditionalChargeAICTPerIndexRate"),
                            AdditionalChargeFFPerCBMRate = datarow.Field<double>("AdditionalChargeFFPerCBMRate"),
                            AdditionalChargeFFPerIndexRate = datarow.Field<double>("AdditionalChargeFFPerIndexRate"),
                            BTLRemarks = datarow.Field<string>("BTLRemarks"),
                            TotalCBMRate = 0,
                            TotalPerIndexRate = 0,
                            TotalCharges = 0,
                            PerBoxRate = datarow.Field<double>("PerBoxRate"),
                            billToLine = datarow.Field<string>("billToLine"),

                        }).ToList();

                    con.Close();

                }
            }
            return listItems;
        }


        public List<Invoice> GetCfsInvoices(string containerindexid)
        {
            var res = Db.Invoices.FromSql($"select * from Invoice where  ContainerIndexId  in (select data from [dbo].[Split]( {containerindexid}, ',') )   and IsDeleted = 0   ").ToList();

            return res;

        }

        public List<Invoice> GetCYInvoices(string cycontainerId)
        {
            var res = Db.Invoices.FromSql($"select * from Invoice where  CYContainerId  in   (select data from [dbo].[Split]( {cycontainerId}, ',') )     and IsDeleted = 0   ").ToList();

            return res;

        }

        public bool GetAuctionInfo(string type, string igm, long index)
        {
            if (type == "CFS")
            {
                var containers = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where  VirNo = {igm} and  IndexNo = {index} and IsDeleted = 0 ").ToList();
                if (containers.Count() > 0)
                {
                    var containerindexidlist = string.Join(",", containers.Select(n => n.ContainerIndexId.ToString()).ToArray());

                    var res = Db.Auctions.FromSql($"select * from Auction where  ContainerIndexId  in   (select data from [dbo].[Split]( {containerindexidlist}, ',') )     and IsDeleted = 0   ").ToList();
                    if (res.Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }

            }
            if (type == "CY")
            {
                var containers = Db.CYContainers.FromSql($"select * from CYContainer   where  VIRNo = {igm} and  IndexNo = {index} and IsDeleted = 0 ").ToList();
                if (containers.Count() > 0)
                {
                    var containerindexidlist = string.Join(",", containers.Select(n => n.CYContainerId.ToString()).ToArray());

                    var res = Db.Auctions.FromSql($"select * from Auction where  CYContainerId  in   (select data from [dbo].[Split]( {containerindexidlist}, ',') )     and IsDeleted = 0   ").ToList();
                    if (res.Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }


        public List<User> GetLineUsers()
        {
            var res = Db.Users.FromSql($"select * from users where ShippingAgentId is not null and isdeleted = 0").ToList();

            return (res);
        }


        public List<AictBillingViewModel> GetAictBillingtoLine(long shippingAgentId, DateTime fromdate, DateTime todate)
        {

            var reasdasd = GetAictBillingDetail(shippingAgentId, fromdate, todate).ToList();

            //var reslist = new List<AictBillingViewModel>();

            //var fromdateres =  fromdate.ToString("MM/dd/yyyy");
            //var todateres = todate.ToString("MM/dd/yyyy");

            //var percentForAictBillingToLines = Db.PercentForAictBillingToLines.FromSql($"select * from PercentForAictBillingToLine where ShippingAgentId = {shippingAgentId} and isdeleted = 0").LastOrDefault();

            //if(percentForAictBillingToLines != null)
            //{
            //    var invociees = Db.Invoices.FromSql($"select * from Invoice where InvoiceCategory = 'FF' and Type = 'CFS'  and isdeleted = 0 and cast (InvoiceDate as date) >= cast ({fromdateres} as date) and cast (InvoiceDate as date) <= cast ({todateres} as date) ").ToList();

            //    if(invociees.Count() > 0)
            //    {
            //        var cfsres = (from invoice in invociees
            //                      join containerIndex in Db.ContainerIndices on invoice.ContainerIndexId equals containerIndex.ContainerIndexId
            //                      where
            //                       !Db.AictBillingItems.Any(s => s.LineInvoiceno == invoice.InvoiceNo)

            //                      select new AictBillingViewModel
            //                      {
            //                          VirNo = containerIndex.VirNo,
            //                          IndexNo = containerIndex.IndexNo ?? 0,
            //                          LineInvoiceno = invoice.InvoiceNo,
            //                          LineInvoiceDate = invoice.InvoiceDate,
            //                          AictInvoiceNo = Db.Invoices.FromSql($"select * from Invoice where PerfomaReceiptNumber = {invoice.InvoiceNo} and isdeleted = 0").LastOrDefault().InvoiceNo,
            //                          AictInvoiceDate = Db.Invoices.FromSql($"select * from Invoice where PerfomaReceiptNumber = {invoice.InvoiceNo} and isdeleted = 0").LastOrDefault().InvoiceDate,
            //                          LineStorage = Db.InvoiceItems.FromSql($"select  InvoiceItem.* from  Invoice join InvoiceItem on InvoiceItem.InvoiceId = Invoice.InvoiceId and InvoiceItem.IsDeleted = 0 and InvoiceItem.Category  = 'Storage' where Invoice.InvoiceNo = {invoice.InvoiceNo} and Invoice.IsDeleted = 0  ").ToList().Sum(x=> x.Amount)  ,
            //                          LineServiceChages = Db.InvoiceItems.FromSql($"select  InvoiceItem.* from  Invoice join InvoiceItem on InvoiceItem.InvoiceId = Invoice.InvoiceId and InvoiceItem.IsDeleted = 0 and InvoiceItem.Category  != 'Storage' where Invoice.InvoiceNo = {invoice.InvoiceNo} and Invoice.IsDeleted = 0  ").ToList().Sum(x=> x.Amount) ,
            //                          LineTotalCharges = Db.InvoiceItems.FromSql($"select  InvoiceItem.* from  Invoice join InvoiceItem on InvoiceItem.InvoiceId = Invoice.InvoiceId and InvoiceItem.IsDeleted = 0  where Invoice.InvoiceNo = {invoice.InvoiceNo} and Invoice.IsDeleted = 0  ").ToList().Sum(x=> x.Amount) ,
            //                          StoragePercentToLine = percentForAictBillingToLines.StoragePercentToLine,
            //                          ServiceChargesPercentToLine = percentForAictBillingToLines.ServiceChargesPercentToLine,
            //                          IsChecked = false

            //                      }).ToList();

            //        if (cfsres.Count() > 0)
            //        {
            //            reslist.AddRange(cfsres);
            //        }

            //    }


            //    var invocieescy = Db.Invoices.FromSql($"select * from Invoice where InvoiceCategory = 'FF' and Type = 'CY'  and isdeleted = 0 and cast (InvoiceDate as date) >= cast ({fromdateres} as date) and cast (InvoiceDate as date) <= cast ({todateres} as date) ").ToList();

            //    if(invocieescy.Count() > 0)
            //    {
            //        var cyres = (from invoice in invocieescy
            //                     join cycontainer in Db.CYContainers on invoice.CYContainerId equals cycontainer.CYContainerId
            //                     where
            //                       !Db.AictBillingItems.Any(s => s.LineInvoiceno == invoice.InvoiceNo)
            //                     select new AictBillingViewModel
            //                     {
            //                         VirNo = cycontainer.VIRNo,
            //                         IndexNo = cycontainer.IndexNo ?? 0,
            //                         LineInvoiceno = invoice.InvoiceNo,
            //                         LineInvoiceDate = invoice.InvoiceDate,
            //                         AictInvoiceNo = Db.Invoices.FromSql($"select * from Invoice where PerfomaReceiptNumber = {invoice.InvoiceNo} and isdeleted = 0").LastOrDefault().InvoiceNo,
            //                         AictInvoiceDate = Db.Invoices.FromSql($"select * from Invoice where PerfomaReceiptNumber = {invoice.InvoiceNo} and isdeleted = 0").LastOrDefault().InvoiceDate,
            //                         LineStorage = Db.InvoiceItems.FromSql($"select  InvoiceItem.* from  Invoice join InvoiceItem on InvoiceItem.InvoiceId = Invoice.InvoiceId and InvoiceItem.IsDeleted = 0 and InvoiceItem.Category  = 'Storage' where Invoice.InvoiceNo = {invoice.InvoiceNo} and Invoice.IsDeleted = 0  ").ToList().Sum(x => x.Amount),
            //                         LineServiceChages = Db.InvoiceItems.FromSql($"select  InvoiceItem.* from  Invoice join InvoiceItem on InvoiceItem.InvoiceId = Invoice.InvoiceId and InvoiceItem.IsDeleted = 0 and InvoiceItem.Category  != 'Storage' where Invoice.InvoiceNo = {invoice.InvoiceNo} and Invoice.IsDeleted = 0  ").ToList().Sum(x => x.Amount),
            //                         LineTotalCharges = Db.InvoiceItems.FromSql($"select  InvoiceItem.* from  Invoice join InvoiceItem on InvoiceItem.InvoiceId = Invoice.InvoiceId and InvoiceItem.IsDeleted = 0  where Invoice.InvoiceNo = {invoice.InvoiceNo} and Invoice.IsDeleted = 0  ").ToList().Sum(x => x.Amount),
            //                         StoragePercentToLine = percentForAictBillingToLines.StoragePercentToLine,
            //                         ServiceChargesPercentToLine = percentForAictBillingToLines.ServiceChargesPercentToLine,

            //                         IsChecked = false

            //                     }).ToList();

            //        if (cyres.Count() > 0)
            //        {
            //            reslist.AddRange(cyres);
            //        }
            //    }

            //}

            //if(reslist.Count() > 0)
            //{
            //    foreach (var item in reslist)
            //    {
            //        var storage = item.LineStorage * item.StoragePercentToLine / 100;
            //        var servicechagres = item.LineServiceChages * item.ServiceChargesPercentToLine / 100;
            //        item.AICTBillingToLine = storage + servicechagres;
            //    }

            //    reslist.ForEach(a => a.AICTBillingToLine = Math.Round(a.AICTBillingToLine));
            //}

            return reasdasd;
        }


        public AictBilling GetAictBillingNumberDetail(long shippingagentID)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.AictBillings.FromSql($"select * from AictBilling  where IsDeleted = 0 and ShippingAgentId = {shippingagentID} ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public List<AictBilling> GetAictBillingList(DateTime fromdate, DateTime todate)
        {


            var fromdateres = fromdate.ToString("MM/dd/yyyy");
            var todateres = todate.ToString("MM/dd/yyyy");

            var invociees = Db.AictBillings.FromSql($"select * from AictBilling where   isdeleted = 0 and cast (PerformedDate as date) >= cast ({fromdateres} as date) and cast (PerformedDate as date) <= cast ({todateres} as date) ").ToList();


            return invociees;
        }

        public List<AictBillingViewModel> GetAictBillingDetail(long shippingAgentId, DateTime fromdate, DateTime todate)
        {

            var reslist = new List<AictBillingViewModel>();

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string conString = Con;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("GetAictBillingtoLine", con))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@fromdate", fromdate.ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("@todate", todate.ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("@shippingagentId", shippingAgentId);


                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(command);

                    sda.Fill(ds);

                    reslist = ds.Tables[0].AsEnumerable()
                        .Select(datarow => new AictBillingViewModel()
                        {
                            VirNo = datarow.Field<string>("VirNo"),
                            IndexNo = datarow.Field<long>("IndexNo"),
                            LineInvoiceno = datarow.Field<string>("LineInvoiceno"),
                            LineInvoiceDate = datarow.Field<DateTime?>("LineInvoiceDate"),
                            AictInvoiceNo = datarow.Field<string>("AictInvoiceNo"),
                            AictInvoiceDate = datarow.Field<DateTime?>("AictInvoiceDate"),
                            LineStorage = datarow.Field<double>("LineStorage"),
                            LineServiceChages = datarow.Field<double>("LineServiceChages"),
                            LineTotalCharges = datarow.Field<double>("LineTotalCharges"),
                            StoragePercentToLine = datarow.Field<double>("StoragePercentToLine"),
                            ServiceChargesPercentToLine = datarow.Field<double>("ServiceChargesPercentToLine"),
                            IsChecked = false

                        }).ToList();

                    con.Close();

                    if (reslist.Count() > 0)
                    {

                        foreach (var item in reslist)
                        {
                            var storage = item.LineStorage * item.StoragePercentToLine / 100;
                            var servicechagres = item.LineServiceChages * item.ServiceChargesPercentToLine / 100;
                            item.AICTBillingToLine = storage + servicechagres;
                        }

                        reslist.ForEach(a => a.AICTBillingToLine = Math.Round(a.AICTBillingToLine));
                        return reslist;
                    }
                }
            }

            return reslist;
        }


        public List<AictBillingViewModel> GetAictBillingSettleUnSettleInvoices(long shippingAgentId, string nature, DateTime fromdate, DateTime todate)
        {

            var reslist = new List<AictBillingViewModel>();

            if (nature == "Settle")
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                string conString = Con;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand command = new SqlCommand("GetAictBillingtoLineFOrSettleUnSettel", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@fromdate", fromdate.ToString("MM/dd/yyyy"));
                        command.Parameters.AddWithValue("@todate", todate.ToString("MM/dd/yyyy"));
                        command.Parameters.AddWithValue("@shippingagentId", shippingAgentId);
                        command.Parameters.AddWithValue("@nature", nature);


                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(command);

                        sda.Fill(ds);

                        reslist = ds.Tables[0].AsEnumerable()
                            .Select(datarow => new AictBillingViewModel()
                            {
                                VirNo = datarow.Field<string>("VirNo"),
                                IndexNo = datarow.Field<long>("IndexNo"),
                                LineInvoiceno = datarow.Field<string>("LineInvoiceno"),
                                LineInvoiceDate = datarow.Field<DateTime?>("LineInvoiceDate"),
                                AictInvoiceNo = datarow.Field<string>("AictInvoiceNo"),
                                AictInvoiceDate = datarow.Field<DateTime?>("AictInvoiceDate"),
                                LineStorage = datarow.Field<double>("LineStorage"),
                                LineServiceChages = datarow.Field<double>("LineServiceChages"),
                                StoragePercentToLine = datarow.Field<double>("StoragePercentToLine"),
                                ServiceChargesPercentToLine = datarow.Field<double>("ServiceChargesPercentToLine"),
                                LineTotalCharges = datarow.Field<double>("LineTotalCharges"),
                                AICTBillingToLine = datarow.Field<double>("AICTBillingToLine"),
                                AictBillingNumber = datarow.Field<string>("AictBillingNumber"),

                            }).ToList();

                        con.Close();

                    }
                }

            }
            else
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                string conString = Con;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand command = new SqlCommand("GetAictBillingtoLineFOrSettleUnSettel", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@fromdate", null);
                        command.Parameters.AddWithValue("@todate", null);
                        command.Parameters.AddWithValue("@shippingagentId", shippingAgentId);
                        command.Parameters.AddWithValue("@nature", nature);


                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(command);

                        sda.Fill(ds);

                        reslist = ds.Tables[0].AsEnumerable()
                            .Select(datarow => new AictBillingViewModel()
                            {
                                VirNo = datarow.Field<string>("VirNo"),
                                IndexNo = datarow.Field<long>("IndexNo"),
                                LineInvoiceno = datarow.Field<string>("LineInvoiceno"),
                                LineInvoiceDate = datarow.Field<DateTime?>("LineInvoiceDate"),
                                AictInvoiceNo = datarow.Field<string>("AictInvoiceNo"),
                                AictInvoiceDate = datarow.Field<DateTime?>("AictInvoiceDate"),
                                LineStorage = datarow.Field<double>("LineStorage"),
                                LineServiceChages = datarow.Field<double>("LineServiceChages"),
                                StoragePercentToLine = datarow.Field<double>("StoragePercentToLine"),
                                ServiceChargesPercentToLine = datarow.Field<double>("ServiceChargesPercentToLine"),
                                LineTotalCharges = datarow.Field<double>("LineTotalCharges"),
                                AICTBillingToLine = datarow.Field<double>("AICTBillingToLine"),
                                AictBillingNumber = datarow.Field<string>("AictBillingNumber"),

                            }).ToList();

                        con.Close();

                        if (reslist.Count() > 0)
                        {

                            foreach (var item in reslist)
                            {
                                var storage = item.LineStorage * item.StoragePercentToLine / 100;
                                var servicechagres = item.LineServiceChages * item.ServiceChargesPercentToLine / 100;
                                item.AICTBillingToLine = storage + servicechagres;
                            }

                            reslist.ForEach(a => a.AICTBillingToLine = Math.Round(a.AICTBillingToLine));
                        }

                    }
                }
            }

            return reslist;
        }


        public string GetQueryResult(long? shippingAgent, long? clearingagent, long? consignee, long? goodsHeads, string type, string cargotype, DateTime? fromdate, DateTime? todate)
        {
            string output;
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                string conString = Con;

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conString);
                builder.ConnectTimeout = 360;
                try
                {
                    using (SqlConnection con = new SqlConnection(builder.ToString()))
                    {
                        con.Open();

                        try
                        {
                            using (SqlCommand command = new SqlCommand("FullCrSummary", con))
                            {

                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandTimeout = 0;

                                command.Parameters.AddWithValue("@shippingAgent", shippingAgent);
                                command.Parameters.AddWithValue("@clearingagent", clearingagent);
                                command.Parameters.AddWithValue("@consignee", consignee);
                                command.Parameters.AddWithValue("@goodsHeads", goodsHeads);
                                command.Parameters.AddWithValue("@type", type);
                                command.Parameters.AddWithValue("@cargotype", cargotype);
                                command.Parameters.AddWithValue("@fromdate", fromdate?.ToString("MM/dd/yyyy"));
                                command.Parameters.AddWithValue("@todate", todate?.ToString("MM/dd/yyyy"));
                                SqlDataAdapter sda = new SqlDataAdapter(command);
                                sda.Fill(ds);
                                output = Newtonsoft.Json.JsonConvert.SerializeObject(ds);

                            }

                        }
                        catch (Exception e)
                        {
                            string reas = e.InnerException.InnerException.Message;
                            return reas;
                        }
                        con.Close();
                    }
                }
                catch (Exception e)
                {

                    string reas = e.InnerException.InnerException.Message;
                    return reas;
                }

            }
            catch (Exception e)
            {
                string reas = e.InnerException.InnerException.Message;
                return reas;
            }


            return output;
        }

        public List<InvoiceHeadsViewModel> GetInvoiceItemHeadsDetail()
        {
            var resdata = (from res in Db.InvoiceItems
                           select new InvoiceHeadsViewModel
                           {
                               Category = res.Category,
                               Description = res.Description,

                           }).Distinct().ToList();

            return resdata;
        }


        public List<Invoice> GetInvoicesByIGMIndex(string virno, string indexno, string type)
        {
            var resdata = new List<Invoice>();

            if (type == "CFS")
            {
                var containerindex = Db.ContainerIndices.FromSql($"select * from ContainerIndex where  VirNo  = {virno}  and IndexNo = {indexno}  and IsDeleted = 0   ").ToList();
                if (containerindex.Count() > 0)
                {
                    var containerindexidlist = string.Join(",", containerindex.Select(n => n.ContainerIndexId.ToString()).ToArray());

                    var res = Db.Invoices.FromSql($"select * from Invoice where  ContainerIndexId  in (select data from [dbo].[Split]( {containerindexidlist}, ',') )   and IsDeleted = 0   ").ToList();

                    resdata.AddRange(res);
                }

            }
            if (type == "CY")
            {
                var cycontainer = Db.CYContainers.FromSql($"select * from CYContainer where  VirNo  = {virno}  and IndexNo = {indexno}  and IsDeleted = 0   ").ToList();
                if (cycontainer.Count() > 0)
                {
                    var cycontaineridlist = string.Join(",", cycontainer.Select(n => n.CYContainerId.ToString()).ToArray());

                    var cyres = Db.Invoices.FromSql($"select * from Invoice where  CYContainerId  in (select data from [dbo].[Split]( {cycontaineridlist}, ',') )   and IsDeleted = 0   ").ToList();

                    resdata.AddRange(cyres);
                }
            }

            return resdata;
        }

    }

}
