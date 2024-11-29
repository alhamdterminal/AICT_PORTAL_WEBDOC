using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class PreAlertPayOrderRepository : RepoBase<PreAlertPayOrder> , IPreAlertPayOrderRepository
    {
        public PreAlertPayOrderRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<PreAlertPayOrderViewModel> GetPreAlertPayOrdersCount(long RequestNumber)
        {
            var items = new List<PreAlertPayOrderViewModel>();

            var secdeposit = (from paymentUpdate in Db.PaymentUpdates
                              join paymentUpdateDetail in Db.PaymentUpdateDetails on paymentUpdate.PaymentUpdateId equals paymentUpdateDetail.PaymentUpdateId
                              join preAlterDetail in Db.PreAlterDetails on paymentUpdateDetail.PreAlterDetailId equals preAlterDetail.PreAlterDetailId
  
                              where paymentUpdate.RequestNumber == RequestNumber && paymentUpdateDetail.SecDeposit > 0
                              select new PreAlertPayOrderViewModel
                              {
                                  Key = RequestNumber + "-" + preAlterDetail.ContainerNumber + "-" + "Security Deposit",
                                  Description = "Security Deposit",
                                  Amount = paymentUpdateDetail.SecDeposit,
                                  ContainerNumber = preAlterDetail.ContainerNumber
                              }).ToList();

            var thc = (from paymentUpdate in Db.PaymentUpdates
                       join paymentUpdateDetail in Db.PaymentUpdateDetails on paymentUpdate.PaymentUpdateId equals paymentUpdateDetail.PaymentUpdateId
                       join preAlterDetail in Db.PreAlterDetails on paymentUpdateDetail.PreAlterDetailId equals preAlterDetail.PreAlterDetailId 

                       where paymentUpdate.RequestNumber == RequestNumber && paymentUpdateDetail.TotalTHCCharges > 0 
                       select new PreAlertPayOrderViewModel
                       {
                           Key = RequestNumber + "-" + preAlterDetail.ContainerNumber + "-" + "THC Charges",
                             
                           Description = "THC Charges",
                           Amount = paymentUpdateDetail.TotalTHCCharges,
                           ContainerNumber = preAlterDetail.ContainerNumber  
                       }).ToList();

            var lolo = (from paymentUpdate in Db.PaymentUpdates
                        join paymentUpdateDetail in Db.PaymentUpdateDetails on paymentUpdate.PaymentUpdateId equals paymentUpdateDetail.PaymentUpdateId
                        join preAlterDetail in Db.PreAlterDetails on paymentUpdateDetail.PreAlterDetailId equals preAlterDetail.PreAlterDetailId 


                        where paymentUpdate.RequestNumber == RequestNumber && paymentUpdateDetail.Lolo > 0 
                        select new PreAlertPayOrderViewModel
                        {
                            Key = RequestNumber + "-" + preAlterDetail.ContainerNumber + "-" + "LOLO Charges",  
                            Description = "LOLO Charges",
                            Amount = paymentUpdateDetail.Lolo,
                            ContainerNumber = preAlterDetail.ContainerNumber

                        }).ToList();
            
            var detention = (from paymentUpdate in Db.PaymentUpdates
                        join paymentUpdateDetail in Db.PaymentUpdateDetails on paymentUpdate.PaymentUpdateId equals paymentUpdateDetail.PaymentUpdateId
                        join preAlterDetail in Db.PreAlterDetails on paymentUpdateDetail.PreAlterDetailId equals preAlterDetail.PreAlterDetailId 


                        where paymentUpdate.RequestNumber == RequestNumber && paymentUpdateDetail.Detention > 0 
                        select new PreAlertPayOrderViewModel
                        {
                            Key = RequestNumber + "-" + preAlterDetail.ContainerNumber + "-" + "Detention",  
                            Description = "Detention",
                            Amount = paymentUpdateDetail.Detention,
                            ContainerNumber = preAlterDetail.ContainerNumber

                        }).ToList();

            items.AddRange(secdeposit);
            items.AddRange(thc);
            items.AddRange(lolo);
            items.AddRange(detention);

            return items;

        }


        public List<PreAlertPayOrderViewModel> GetPreAlertPayOrders(long RequestNumber)
        {
            var items = new List<PreAlertPayOrderViewModel>();

            var secdeposit = (from paymentUpdate in Db.PaymentUpdates
                        join paymentUpdateDetail in Db.PaymentUpdateDetails on  paymentUpdate.PaymentUpdateId equals paymentUpdateDetail.PaymentUpdateId
                        join preAlterDetail in Db.PreAlterDetails on paymentUpdateDetail.PreAlterDetailId equals preAlterDetail.PreAlterDetailId
                        join preAlert in Db.PreAlerts on preAlterDetail.PreAlertId equals preAlert.PreAlertId
                        from shippingLine in Db.ShippingLines.Where(x=> x.ShippingLineId == paymentUpdateDetail.ShippingLineIdForSD).DefaultIfEmpty()
                        from shippingAgent in Db.ShippingAgents.Where(x=> x.ShippingAgentId == paymentUpdateDetail.ShippingAgentIdForSD).DefaultIfEmpty()
                        //from preAlertPayOrder in Db.PreAlertPayOrders.Where(x=> x.RequestNumber == RequestNumber.ToString()).DefaultIfEmpty()
                        //from preAlertPayOrderDetail in Db.PreAlertPayOrderDetails.Where(x=> x.PreAlertPayOrderId == preAlertPayOrder.PreAlertPayOrderId).DefaultIfEmpty()
                        //from party in Db.Parties.Where(x=> x.PartyId == preAlertPayOrderDetail.PartyId).DefaultIfEmpty()
                        //from bank in Db.Banks.Where(x=> x.BankId == preAlertPayOrderDetail.BankId).DefaultIfEmpty()
                        //from bankBranch in Db.BankBranches.Where(x=> x.BankBranchId == preAlertPayOrderDetail.BankBranchId).DefaultIfEmpty()
                        //from operationDprt in Db.OperationDprts.Where(x=> x.OperationDprtId == preAlertPayOrderDetail.OperationDprtId).DefaultIfEmpty()

                          where paymentUpdate.RequestNumber == RequestNumber && paymentUpdateDetail.SecDeposit > 0
                        &&
                        !Db.PreAlertPayOrderDetails.Any(x=> x.key == RequestNumber + "-" + preAlterDetail.ContainerNumber + "-" + "Security Deposit")
                        select new PreAlertPayOrderViewModel
                        {
                          PreAlertNumber = preAlert.PreAlertNumber,
                          Key = RequestNumber +"-"+ preAlterDetail.ContainerNumber + "-" + "Security Deposit",
                          Description = "Security Deposit",
                          Amount = paymentUpdateDetail.SecDeposit,
                          ContainerNumber = preAlterDetail.ContainerNumber,
                          Size = preAlterDetail.Size,
                          PreAlertPayOrderDate = DateTime.Now,
                          ShippingLineName = shippingLine != null ? shippingLine.ShippingLineName : "",
                          ShippingAgentName = shippingAgent != null ? shippingAgent.Name : "",
                          Remarks = preAlterDetail.Remarks,
                            //PartyId = party != null ? party.PartyId : 0,
                            //BankId = bank != null ? bank.BankId : 0,
                            //BankBranchId = bankBranch != null ? bankBranch.BankBranchId : 0,
                            //OperationDprtId = operationDprt != null ? operationDprt.OperationDprtId : 0,
                            //Bvp = preAlertPayOrderDetail != null ? preAlertPayOrderDetail.Bvp : "",
                            //ChequeNumber = preAlertPayOrderDetail != null ? preAlertPayOrderDetail.ChequeNumber : "",
                            //Remarks = preAlertPayOrderDetail != null ? preAlertPayOrderDetail.Remarks : "",
                            //IsPreAlertPayOrder = preAlertPayOrderDetail != null ? preAlertPayOrderDetail.IsPreAlertPayOrder : false

                        }).ToList();

            var thc = (from paymentUpdate in Db.PaymentUpdates
                        join paymentUpdateDetail in Db.PaymentUpdateDetails on paymentUpdate.PaymentUpdateId equals paymentUpdateDetail.PaymentUpdateId
                       join preAlterDetail in Db.PreAlterDetails on paymentUpdateDetail.PreAlterDetailId equals preAlterDetail.PreAlterDetailId
                       join preAlert in Db.PreAlerts on preAlterDetail.PreAlertId equals preAlert.PreAlertId
                       from shippingLine in Db.ShippingLines.Where(x => x.ShippingLineId == paymentUpdateDetail.ShippingLineIdForTHC).DefaultIfEmpty()
                       from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == paymentUpdateDetail.ShippingAgentIdForTHC).DefaultIfEmpty()

                       where paymentUpdate.RequestNumber == RequestNumber && paymentUpdateDetail.TotalTHCCharges > 0
                         &&
                         !Db.PreAlertPayOrderDetails.Any(x => x.key == RequestNumber + "-" + preAlterDetail.ContainerNumber + "-" + "THC Charges")
                       select new PreAlertPayOrderViewModel
                        {
                           PreAlertNumber = preAlert.PreAlertNumber,
                           Key = RequestNumber + "-" + preAlterDetail.ContainerNumber + "-" + "THC Charges",

                           //  Key = paymentUpdateDetail.BeneficaryForTHC + "-" + paymentUpdateDetail.TotalTHCCharges + "-" + preAlterDetail.ContainerNumber,
                           //  Description = shippingAgent != null ? shippingAgent.Name : "" paymentUpdateDetail.BeneficaryForTHC,
                           ShippingLineName = shippingLine != null ? shippingLine.ShippingLineName : "",
                           ShippingAgentName = shippingAgent != null ? shippingAgent.Name : "",
                           Remarks = preAlterDetail.Remarks,
                           Description = "THC Charges",
                           Amount = paymentUpdateDetail.TotalTHCCharges,
                           ContainerNumber = preAlterDetail.ContainerNumber,
                           Size = preAlterDetail.Size,
                           PreAlertPayOrderDate = DateTime.Now 


                       }).ToList();
            
            var lolo = (from paymentUpdate in Db.PaymentUpdates
                        join paymentUpdateDetail in Db.PaymentUpdateDetails on paymentUpdate.PaymentUpdateId equals paymentUpdateDetail.PaymentUpdateId
                        join preAlterDetail in Db.PreAlterDetails on paymentUpdateDetail.PreAlterDetailId equals preAlterDetail.PreAlterDetailId
                        join preAlert in Db.PreAlerts on preAlterDetail.PreAlertId equals preAlert.PreAlertId
                        from shippingLine in Db.ShippingLines.Where(x => x.ShippingLineId == paymentUpdateDetail.ShippingLineIdForLOLO).DefaultIfEmpty()
                        from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == paymentUpdateDetail.ShippingAgentIdForLoLo).DefaultIfEmpty()


                        where paymentUpdate.RequestNumber == RequestNumber && paymentUpdateDetail.Lolo > 0
                          &&
                         !Db.PreAlertPayOrderDetails.Any(x => x.key == RequestNumber + "-" + preAlterDetail.ContainerNumber + "-" + "LOLO Charges")
                        select new PreAlertPayOrderViewModel
                        {
                            PreAlertNumber = preAlert.PreAlertNumber,
                            Key = RequestNumber + "-" + preAlterDetail.ContainerNumber + "-" + "LOLO Charges",

                            //  Key = paymentUpdateDetail.BeneficaryForLoLo + "-" + paymentUpdateDetail.Lolo + "-" + preAlterDetail.ContainerNumber,
                            ShippingLineName = shippingLine != null ? shippingLine.ShippingLineName : "",
                            ShippingAgentName = shippingAgent != null ? shippingAgent.Name : "",
                            Remarks = preAlterDetail.Remarks,
                            Description = "LOLO Charges",
                            Amount = paymentUpdateDetail.Lolo,
                            ContainerNumber = preAlterDetail.ContainerNumber,
                            Size = preAlterDetail.Size,
                            PreAlertPayOrderDate = DateTime.Now 


                        }).ToList();


            var detention = (from paymentUpdate in Db.PaymentUpdates
                        join paymentUpdateDetail in Db.PaymentUpdateDetails on paymentUpdate.PaymentUpdateId equals paymentUpdateDetail.PaymentUpdateId
                        join preAlterDetail in Db.PreAlterDetails on paymentUpdateDetail.PreAlterDetailId equals preAlterDetail.PreAlterDetailId
                        join preAlert in Db.PreAlerts on preAlterDetail.PreAlertId equals preAlert.PreAlertId
                        from shippingLine in Db.ShippingLines.Where(x => x.ShippingLineId == paymentUpdateDetail.ShippingLineIdForLOLO).DefaultIfEmpty()
                        from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == paymentUpdateDetail.ShippingAgentIdForLoLo).DefaultIfEmpty()


                        where paymentUpdate.RequestNumber == RequestNumber && paymentUpdateDetail.Detention > 0
                          &&
                         !Db.PreAlertPayOrderDetails.Any(x => x.key == RequestNumber + "-" + preAlterDetail.ContainerNumber + "-" + "Detention")
                        select new PreAlertPayOrderViewModel
                        {
                            PreAlertNumber = preAlert.PreAlertNumber,
                            Key = RequestNumber + "-" + preAlterDetail.ContainerNumber + "-" + "Detention",

                            //  Key = paymentUpdateDetail.BeneficaryForLoLo + "-" + paymentUpdateDetail.Lolo + "-" + preAlterDetail.ContainerNumber,
                            ShippingLineName = shippingLine != null ? shippingLine.ShippingLineName : "",
                            ShippingAgentName = shippingAgent != null ? shippingAgent.Name : "",
                            Remarks = preAlterDetail.Remarks,
                            Description = "Detention",
                            Amount = paymentUpdateDetail.Detention,
                            ContainerNumber = preAlterDetail.ContainerNumber,
                            Size = preAlterDetail.Size,
                            PreAlertPayOrderDate = DateTime.Now


                        }).ToList();


            items.AddRange(secdeposit);
            items.AddRange(thc);
            items.AddRange(lolo);
            items.AddRange(detention);

            return items;

        }

        public long GeneratePayOrderNumber()
        {

            var preAlertPayOrders = Db.PreAlertPayOrders.FromSql($"select * from PreAlertPayOrder ").ToList();

            //var preAlertPayOrders = (from preAlertPayOrder in Db.PreAlertPayOrders
            //                      select preAlertPayOrder).ToList();



            if (preAlertPayOrders.Count() > 0)
            {
                var lastpreAlertPayOrder = preAlertPayOrders.Count();
                return lastpreAlertPayOrder + 1;

            }
            else
            {

                return 1;

            }

        }



    }
}
