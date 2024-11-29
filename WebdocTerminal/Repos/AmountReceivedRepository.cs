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
    public class AmountReceivedRepository : RepoBase<AmountReceived>, IAmountReceivedRepository
    {
        public AmountReceivedRepository(IUserResolveService userResolveService) : base(userResolveService) { }

        public AmountReceivedViewmodel AmountReceivedByInvoice(string invoicenumber)
        {

            var received = new AmountReceivedViewmodel();

            var inv = (from invoice in Db.Invoices
                             where invoice.InvoiceNo == invoicenumber
                             select invoice).LastOrDefault();

            if(inv != null)
            {
                if(inv.BillType == "Normal")
                {

                    received.InvoiceId = inv.InvoiceId;
                    received.AmountReceived = inv.GrandTotal;
                    received.BillAmount = inv.GrandTotal;
                    received.BalanceAmount = inv.BalanceAmount;
                    received.SalesTax = inv.AfterSalesTax;
                     



                    var amountRecevied = Db.AmountReceiveds
                        .OrderByDescending(r => r.AmountReceivedId)
                        .FirstOrDefault(r => r.Invoice.InvoiceNo == inv.InvoiceNo);

                    if (amountRecevied != null)
                    {
                        received.BalanceAmount = amountRecevied.BalanceAmount;
                    }
                }

                if (inv.BillType == "Auction")
                {

                    received.InvoiceId = inv.InvoiceId;
                    received.AmountReceived = inv.AuctionGrandTotal;
                    received.BillAmount = inv.AuctionGrandTotal;
                    received.BalanceAmount = inv.BalanceAmount;
                    received.SalesTax = inv.AuctionSalesTax;
                      

                    var amountRecevied = Db.AmountReceiveds
                        .OrderByDescending(r => r.AmountReceivedId)
                        .FirstOrDefault(r => r.Invoice.InvoiceNo == inv.InvoiceNo);

                    if (amountRecevied != null)
                    {
                        received.BalanceAmount = amountRecevied.BalanceAmount;
                    }
                }

            }

          

            return received;
        }

        public string ConsigneeByInvoice(string invoicenumber)
        {
            var consignee = "";
            var inv = (from invoice in Db.Invoices
                       where invoice.InvoiceNo == invoicenumber
                       select invoice).FirstOrDefault();

            if(inv.ContainerIndexId != null)
            {
                var container = (from containerindex in Db.ContainerIndices
                                 from  consige in Db.Consignes.Where(x=> x.ConsigneId == containerindex.ConsigneId).DefaultIfEmpty()
                                 where containerindex.ContainerIndexId == inv.ContainerIndexId
                                 select consige.ConsigneName).FirstOrDefault();

                if(container != null)
                {
                    consignee = container;
                }
            }
            if (inv.CYContainerId != null)
            {
                var container = (from cycontainer in Db.CYContainers
                                 from consige in Db.Consignes.Where(x => x.ConsigneId == cycontainer.ConsigneId).DefaultIfEmpty()
                                 where cycontainer.CYContainerId == inv.CYContainerId
                                 select consige.ConsigneName).FirstOrDefault();

                if (container != null)
                {
                    consignee = container;
                }
            }

            return consignee;
        }

    }
}
