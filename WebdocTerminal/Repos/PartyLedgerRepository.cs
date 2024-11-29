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
    public class PartyLedgerRepository : RepoBase<PartyLedger> , IPartyLedgerRepository
    {
        public PartyLedgerRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public double GetBalanceAmountByParty(long? partyId)
        {
            var data = (from PartyLedger in Db.PartyLedgers
                        where PartyLedger.PartyId == partyId
                        select  
                           PartyLedger.Balance
                        ).LastOrDefault();

            return data;
        }

        public double GetBalanceAmountByPartyForAmountReceived(long? partyId)
        {
             var data = (from PartyLedger in Db.PartyLedgers
                        where PartyLedger.PartyId == partyId
                        select    PartyLedger.Balance
                          ).LastOrDefault();
         
            return data;              
            
        }

        public IEnumerable<Invoice>  GetInvoicesNotInAmountReceived()
        {
            var data = (from Invoice in Db.Invoices
                        where 
                        !Db.AmountReceiveds.Any(c => (c.Invoice.InvoiceNo == Invoice.InvoiceNo) && (c.BalanceAmount == 0) )
                         && Invoice.IsCancelled == false
                         && (Invoice.BillType == "Normal"  || Invoice.BillType == "Auction"  )
                        select Invoice).ToList();

            return data;
        }

        public IEnumerable<Invoice> GetInvoicesNotInAmountReceivedExport()
        {
            var data = (from Invoice in Db.Invoices
                        where
                        !Db.AmountReceiveds.Any(c => (c.Invoice.InvoiceNo == Invoice.InvoiceNo) && (c.BalanceAmount == 0))
                       // && Invoice.InvoiceType == "E"
                        && Invoice.IsCancelled == false
                        select Invoice).ToList();

            return data;
        }


        public IEnumerable<PartyLedgerViewModel> GetPartyLedgerDetailPartyWise(long? partyId)
        {
            var data = (from partyLedger in Db.PartyLedgers
                        from chequeRecived in Db.ChequeReciveds.Where(x=> x.ChequeRecivedId == partyLedger.ChequeRecivedId).DefaultIfEmpty()

                        where partyLedger.PartyId == partyId
                        select new PartyLedgerViewModel { 
                             PartyId = partyLedger.PartyId,
                            Balance = partyLedger.Balance,
                            BankId = partyLedger.BankId,
                            BillNo = partyLedger.BillNo,
                            Credit = partyLedger.Credit,
                            Debit = partyLedger.Debit,
                            LedgerDate = partyLedger.LedgerDate,
                            Type = partyLedger.Type,
                            Number = chequeRecived.Number,
                            AmountReceivedId = partyLedger.AmountReceivedId,
                            ChequeRecivedId = partyLedger.ChequeRecivedId,
                            PartyLedgerId = partyLedger.PartyLedgerId,
                            RefundAmountId = partyLedger.RefundAmountId
                            
                            

                        }).ToList();

            return data;

        }


        public List<PartyLedger> GetrPartyUnSettledAmount(long partyId)
        {
            var res = (from partyLedger in Db.PartyLedgers
                       where
                       partyLedger.PartyId == partyId
                       && partyLedger.IsSettled == false
                       select partyLedger).ToList();

            return res;
        }


        public double PartyBalanceAmount(long partyid)
        {

            var res = Db.Parties.FromSql($"select * from Party where ClearingAgentId = {partyid} and IsDeleted = 0 ").LastOrDefault();

            if(res != null)
            { 
                var inv = Db.PartyLedgers.FromSql($"select * from PartyLedger where PartyId = {res.PartyId} and IsDeleted = 0  ").Sum(x=> x.Credit - x.Debit);

                return inv;
            }
             
            return 0;
             
             
              
        }
        public Party PartyByClearnignAgentID(long ClearingAgentId)
        {

            var res = Db.Parties.FromSql($"select * from Party where ClearingAgentId = {ClearingAgentId} and IsDeleted = 0 ").LastOrDefault();

            if (res != null)
            { 
                return res;
            }

            return null;
             
        }


        public CreditAllowed CreditAllowedCFSBYcontainerindexid(long Containerindexid)
        {
              
            var container = Db.ContainerIndices.FromSql($"select * from ContainerIndex where ContainerIndexId  = {Containerindexid}  and IsDeleted = 0").LastOrDefault();

            if (container != null)
            {
                var creditAllow = Db.CreditAlloweds.FromSql($"select * from creditallowed where VirNumber  = {container.VirNo} and IndexNumber = {container.IndexNo} and cast( CreatedDate as date) = cast( Getdate() as date) and IsSettled = 0   and IsDeleted = 0   and IsApprove = 1 and IsCancel = 0 and IsReject =0 ").LastOrDefault();

                if (creditAllow != null)
                {
                    return creditAllow;
                }


            }

            return null;
        }


        public CreditAllowed CreditAllowedCYbycycontainerid(long CYContainerid)
        {
             
            var container = Db.CYContainers.FromSql($"select * from CYContainer where CYContainerId  = {CYContainerid}  and IsDeleted = 0").LastOrDefault();

            if (container != null)
            {
                var creditAllow = Db.CreditAlloweds.FromSql($"select * from creditallowed where VirNumber  = {container.VIRNo} and IndexNumber = {container.IndexNo} and cast( CreatedDate as date) = cast( Getdate() as date) and IsSettled = 0   and IsDeleted = 0  and IsApprove = 1 and IsCancel = 0 and IsReject =0").LastOrDefault();


                if (creditAllow != null)
                {
                    return creditAllow;
                }

            }

            return null;
        }


        public Invoice GetInvoiceByInvoiceNo(string invoiceno)
        {

            var invoice = Db.Invoices.FromSql($"select * from invoice where InvoiceNo = {invoiceno}  and IsDeleted = 0").LastOrDefault();

            if (invoice != null)
            { 
                return invoice;
                 
            }

            return null;
        }
        public Invoice FindInvoiceAndupdate(string invoiceno , string newinvoiceno)
        {

            var invoice = Db.Invoices.FromSql($"select * from invoice where InvoiceNo = {invoiceno}  and IsDeleted = 0").LastOrDefault();

            if (invoice != null)
            {   
                //invoice.ExcessAmount = 0;
                invoice.KnockOffInvoiceNumber = newinvoiceno;
                Db.Update(invoice);
                Db.SaveChanges();
                return invoice;
                 
            }

            return null;
        }


        public PartyCodeAndBalanceViewModel PartyInfo(long partyid   , string igm , long index, string type)
        {             
            var resdata = new PartyCodeAndBalanceViewModel();

            var res = Db.Parties.FromSql($"select * from Party where ClearingAgentId = {partyid} and IsDeleted = 0 ").LastOrDefault();

            if (res != null)
            {

                if(type == "CFS")
                {
                    var containerindex = Db.ContainerIndices.FromSql($"select * from ContainerIndex where VirNo = {igm} and IndexNo = {index} and IsDeleted = 0 ").FirstOrDefault();

                    var users = Db.Users.FromSql($"select * from Users where  ShippingAgentId  = {containerindex.ShippingAgentId} and IsDeleted = 0 ").FirstOrDefault();

                    if(users != null)
                    {
                        resdata.PartyId = res.PartyId;
                        resdata.Nature = "ClearingAgent";
                        resdata.balance = Db.PartyLedgers.FromSql($"select * from PartyLedger where PartyId = {res.PartyId} and IsDeleted = 0 and ShippingAgentId = {users.ShippingAgentId} ").Sum(x => x.Credit - x.Debit);
                    }
                    else
                    {
                        resdata.PartyId = res.PartyId;
                        resdata.Nature = "ClearingAgent";
                        resdata.balance = Db.PartyLedgers.FromSql($"select * from PartyLedger where PartyId = {res.PartyId} and IsDeleted = 0 and ShippingAgentId is null  ").Sum(x => x.Credit - x.Debit);
                    }

                }

                else if (type == "CY")
                {
                    var containerindex = Db.CYContainers.FromSql($"select * from CYContainer where VirNo = {igm} and IndexNo = {index} and IsDeleted = 0 ").FirstOrDefault();

                    var users = Db.Users.FromSql($"select * from Users where  ShippingAgentId  = {containerindex.ShippingAgentId} and IsDeleted = 0 ").FirstOrDefault();

                    if (users != null)
                    {
                        resdata.PartyId = res.PartyId;
                        resdata.Nature = "ClearingAgent";
                        resdata.balance = Db.PartyLedgers.FromSql($"select * from PartyLedger where PartyId = {res.PartyId} and IsDeleted = 0 and ShippingAgentId = {users.ShippingAgentId} ").Sum(x => x.Credit - x.Debit);
                    }
                    else
                    {
                        resdata.PartyId = res.PartyId;
                        resdata.Nature = "ClearingAgent";
                        resdata.balance = Db.PartyLedgers.FromSql($"select * from PartyLedger where PartyId = {res.PartyId} and IsDeleted = 0 and ShippingAgentId is null  ").Sum(x => x.Credit - x.Debit);
                    }
                }
                else
                {
                    return null;
                } 
                 
                  
                return resdata;
            }

            return null;             
        }


        public CreditAllowed CreditAllowedCFSBYid(long Containerindexid)
        {

            var container = Db.ContainerIndices.FromSql($"select * from ContainerIndex where ContainerIndexId  = {Containerindexid}  and IsDeleted = 0").LastOrDefault();

            if (container != null)
            {
                var creditAllow = Db.CreditAlloweds.FromSql($"select * from creditallowed where VirNumber  = {container.VirNo} and IndexNumber = {container.IndexNo}   and IsSettled = 0   and IsDeleted = 0   and IsApprove = 1 and IsCancel = 0 and IsReject =0 ").LastOrDefault();

                if (creditAllow != null)
                {
                    return creditAllow;
                }


            }

            return null;
        }


        public CreditAllowed CreditAllowedCYbyid(long CYContainerid)
        {

            var container = Db.CYContainers.FromSql($"select * from CYContainer where CYContainerId  = {CYContainerid}  and IsDeleted = 0").LastOrDefault();

            if (container != null)
            {
                var creditAllow = Db.CreditAlloweds.FromSql($"select * from creditallowed where VirNumber  = {container.VIRNo} and IndexNumber = {container.IndexNo}  and IsSettled = 0   and IsDeleted = 0  and IsApprove = 1 and IsCancel = 0 and IsReject =0").LastOrDefault();


                if (creditAllow != null)
                {
                    return creditAllow;
                }

            }

            return null;
        }

    }
}
