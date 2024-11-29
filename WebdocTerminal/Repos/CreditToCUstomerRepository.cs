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
    public class CreditToCustomerRepository : RepoBase<CreditToCustomer>, ICreditToCustomerRepository
    {
        public CreditToCustomerRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public CreditToCustomerViewModel GetCreditToCustomer(string GDNumber)
        {
            var model = (from lp in Db.LoadingPrograms
                         join ec in Db.EnteringCargos on lp.LoadingProgramNumber equals ec.LoadingProgramNumber
                         from invoice in Db.InvoiceExports.Where(i => i.EnteringCargoId == ec.EnteringCargoId).DefaultIfEmpty()
                         from ctc in Db.CreditToCustomers.Where(i => i.EnteringCargoId == ec.EnteringCargoId).DefaultIfEmpty()
                         where ec.GDNumber.Trim().ToUpper() == GDNumber.Trim().ToUpper()
                         select new CreditToCustomerViewModel
                         {
                             EnteringCargoId = ec.EnteringCargoId,
                             LPNo = lp.LoadingProgramNumber,
                             InvoiceAmount = invoice != null ? invoice.GrandTotal : 0,
                             InvoiceNo = invoice != null ? invoice.InvoiceNo : null,
                             VesselId = lp.VesselExportId ?? 0,
                             VoyageId = lp.VoyageExportId ?? 0,
                             CreditToCustomerId = ctc != null ? ctc.CreditToCustomerId : 0,
                             AuthorizedBy = ctc != null ? ctc.AuthorizedBy : "",
                             AuthorizedDays = ctc != null ? ctc.AuthorizedDays : 0
                         }).FirstOrDefault();

            return model;
        }

        public CreditToCustomer GetcreditToCustomerByInvoiceId(long Id)
        {
            var model = (from invoice in Db.InvoiceExports
                         from ctc in Db.CreditToCustomers.Where(i => i.EnteringCargoId == invoice.EnteringCargoId).DefaultIfEmpty()
                         where
                         (invoice.InvoiceExportId == Id) && (invoice.IsCancelled == false)
                         select ctc).FirstOrDefault();

            return model;
        }
    }
}
