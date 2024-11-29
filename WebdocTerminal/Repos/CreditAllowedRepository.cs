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
    public class CreditAllowedRepository : RepoBase<CreditAllowed> , ICreditAllowedRepository
    {

        public CreditAllowedRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        //public   (IUserResolveService userResolveService) : base(userResolveService)
        //{

        //}

        public List<CreditAllowed> GetCreditAllowDetail()
        {
            var creditAllow = Db.CreditAlloweds.FromSql($"select * from creditallowed where    cast( CreatedDate as date) = cast( Getdate() as date) and IsSettled = 0   and IsDeleted = 0  and IsCancel = 0  and InvoiceNo  is null   ").ToList();
             
              creditAllow.Where(x => x.IsApprove == true && x.IsReject == false).ToList().ForEach(x=> x.StatusType = "Approved");
              creditAllow.Where(x => x.IsApprove == false && x.IsReject == true).ToList().ForEach(x=> x.StatusType = "Rejected");

            return creditAllow;
        }

        public List<CreditAllowed> GetCreditAllowDetail( string virno  , long indexno)
        {
            var creditAllow = Db.CreditAlloweds.FromSql($"select * from creditallowed where virnumber ={virno} and indexnumber = {indexno}  and IsSettled = 0   and IsDeleted = 0  and IsCancel = 0   and IsReject = 0    ").ToList();

             return creditAllow;
        }

        public List<UnSettledCreditAllowed> GetUnSettledCreditAllow()
        {
            var resdata = new List<UnSettledCreditAllowed>();

            var rescfs = (from paymentReceived in Db.PaymentReceiveds
                       join invoice in Db.Invoices on paymentReceived.InoviceNo equals invoice.InvoiceNo
                       join containerIndex in Db.ContainerIndices on invoice.ContainerIndexId equals containerIndex.ContainerIndexId
                       where
                       paymentReceived.IsSettle == false
                       && paymentReceived.NatureOfAmount == "CreditAllowed"
                       && paymentReceived.CreditAllowed > 0
                       && invoice.ContainerIndexId != null
                       select new UnSettledCreditAllowed
                       {
                           InvoiceNumber = paymentReceived.InoviceNo,
                           VirNumber = containerIndex.VirNo,
                           IndexNumber = containerIndex.IndexNo ?? 0 ,
                           PaymentReceivedId = paymentReceived.PaymentReceivedId

                       }).ToList();

            if(rescfs.Count() > 0)
            {
                resdata.AddRange(rescfs);
            }
            
            var rescy = (from paymentReceived in Db.PaymentReceiveds
                       join invoice in Db.Invoices on paymentReceived.InoviceNo equals invoice.InvoiceNo
                       join container in Db.CYContainers on invoice.CYContainerId equals container.CYContainerId
                       where
                       paymentReceived.IsSettle == false
                       && paymentReceived.NatureOfAmount == "CreditAllowed"
                       && paymentReceived.CreditAllowed > 0
                       && invoice.CYContainerId != null
                       select new UnSettledCreditAllowed
                       {
                           InvoiceNumber = paymentReceived.InoviceNo,
                           VirNumber = container.VIRNo,
                           IndexNumber = container.IndexNo ?? 0,
                           PaymentReceivedId = paymentReceived.PaymentReceivedId
                       }).ToList();

            if(rescy.Count() > 0)
            {
                resdata.AddRange(rescy);
            }

            return resdata;
        }

        public double GetCreditAllowedAmount(string paymentReceivedIds)
        {
            long amount = 0;
            var iNo = paymentReceivedIds.Split(",").ToList();


            foreach (var item in iNo)
            {
                amount +=  Db.PaymentReceiveds.FromSql($"select * from PaymentReceived where PaymentReceivedId  =  {item} and IsDeleted = 0 ").ToList().Sum(x=> x.CreditAllowed);
                  
            }
             
            return amount;

        }


        public PaymentReceived GetPaymentById(long id)
        {
            var res =   Db.PaymentReceiveds.FromSql($"select * from PaymentReceived where PaymentReceivedId  =  {id} and IsDeleted = 0  and IsSettle = 0 and NatureOfAmount = 'CreditAllowed'  ").LastOrDefault();

            return res;
        }


        public double GetUnSetteledRefund(string paymentReceivedIds)
        {
            long amount = 0;
            var iNo = paymentReceivedIds.Split(",").ToList();
 
            foreach (var item in iNo)
            {
                amount += Db.PaymentReceiveds.FromSql($"select * from PaymentReceived where PaymentReceivedId  =  {item} and IsDeleted = 0  and IsSettle = 0 and NatureOfAmount = 'CreditAllowed' ").ToList().Sum(x => x.CreditAllowed);

            }

            return amount;

        }


        public List<PaymentReceived> GetListUnSetteledRefund(string paymentReceivedIds)
        {
            var res = new List<PaymentReceived>();

            var iNo = paymentReceivedIds.Split(",").ToList();

            foreach (var item in iNo)
            {
                var resdata = Db.PaymentReceiveds.FromSql($"select * from PaymentReceived where PaymentReceivedId  =  {item} and IsDeleted = 0  and IsSettle = 0 and NatureOfAmount = 'CreditAllowed' ").LastOrDefault();

                if(resdata != null)
                {
                    res.Add(resdata);
                }

            }

            return res;

        }


        public Invoice GetInvoiceByInvoiceNo(string invoiceno)
        {
            var res =  Db.Invoices.FromSql($"select * from Invoice where InvoiceNo =  {invoiceno} and IsDeleted = 0  ").LastOrDefault();

            return res;
        }


        public double GetPaymentReceivedById(long paymentReceivedIds)
        {  
             var res =   Db.PaymentReceiveds.FromSql($"select * from PaymentReceived where PaymentReceivedId  =  {paymentReceivedIds} and IsDeleted = 0 ").ToList().Sum(x => x.CreditAllowed);
              
            return res;

        }

        public List<CreditAllowRefoundSettlement> GetCreditAllowRefoundSettlement(long paymentReceivedIds)
        {
            var res = Db.CreditAllowRefoundSettlements.FromSql($"select * from CreditAllowRefoundSettlement where PaymentReceivedId  =  {paymentReceivedIds} and IsDeleted = 0 ").ToList();

            return res;

        }

    }
}
