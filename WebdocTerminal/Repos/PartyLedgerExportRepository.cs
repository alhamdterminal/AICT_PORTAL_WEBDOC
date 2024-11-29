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
    public class PartyLedgerExportRepository : RepoBase<PartyLedgerExport> , IPartyLedgerExportRepository
    {
        public PartyLedgerExportRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public IEnumerable<InvoiceExport> GetInvoicesNotInAmountReceivedExport()
        {
            var data = (from invoiceExport in Db.InvoiceExports
                        where
                        !Db.AmountReceivedExports.Any(c => (c.InvoiceExport.InvoiceNo == invoiceExport.InvoiceNo) && (c.BalanceAmount == 0))
                        && invoiceExport.InvoiceType == "E"
                        && invoiceExport.IsCancelled == false
                        select invoiceExport).ToList();

            return data;
        }

        public double GetBalanceAmountByParty(long? partyExportId)
        {
            var data = (from PartyLedgerExport in Db.PartyLedgerExports
                        where PartyLedgerExport.PartyExportId == partyExportId
                        select
                           PartyLedgerExport.Balance
                        ).LastOrDefault();

            return data;
        }

        public IEnumerable<InvoiceExport> GetUnSettledInvoices()
        {

            var asd = Db.InvoiceExports.FromSql($"select * from InvoiceExport where IsAmountReceived = 0   and IsDeleted = 0").ToList();
            if (asd != null)
            {
                return asd;
            }

            return null;

        }

        public List<PartyLedgerExport> GetLedgerByPartyId(long partyId)
        {
            var res = Db.PartyLedgerExports.FromSql($" select * from PartyLedgerExport where PartyExportId = {partyId} and IsDeleted = 0 and Type != 'LedCash' and  Type != 'LedBill' ").ToList();

            return res;


        }
    }
}
