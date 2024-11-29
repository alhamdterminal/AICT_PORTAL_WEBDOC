using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IInvoiceExportRepository : IRepo<InvoiceExport>
    {

        InvoiceViewModel GetExportInvoice(long EnteringCargoId);

        List<InvoiceItemViewModel> GetExportCargoCBMTotal(long EnteringCargoId, int Weight, double CBM);

        List<InvoiceItemViewModel> GetIndexTotalExport(long EnteringCargoId);

        StorageTotalViewModel GetStorageTotalExport(long EnteringCargoId, DateTime BillDate, DateTime gateInDate, int Weight, double CBM);
        // CargoReceived GetCargoReceivedByEnteringCargoId(long EnteringCargoId);

        InvoiceViewModel GetExportInvoiceByBillNo(string BillNo);


        InvoiceExportViewModel GetExportInvoiceByGdnumber(string gdnumber);

        InvoiceExport GetInvoiceLast();
        List<InvoiceItemExport> GetInvoiceItemBycontainerIndexId(long EnteringCargoId);

        List<InvoiceExport> GetinvoicesBygdnumber(string gdnumber);

        List<InvoiceExport> GetinvoicesByInvoiceno(string invoiceno);
        InvoiceExport GetInvoiceByInvoiceId(long InvoiceId);

        AmountReceivedExport GetAmountReceivedByInvoiceId(long InvoiceId);

        List<PartyLedgerExport> GetPrtyLedgerListByAmountReceivedExportId(long AmountReceivedId);

        List<InvoiceExport> GetinvoicesBygdno(string gdno);

        List<ChequeRecivedExport> GetChequeReceivedDetailsExport(long partyId, long instrumentNo);

        InvoiceExport GetExportAgentNameAndPhoneNo(string val);


        List<InvoiceItemExportViewModel> GetExportCBMTariffsForInvoice(string gdnumber, int Weight, double CBM);

        ExportStorageTotalViewModel GetStorageTotalExportForInvoice(string gdnumber, DateTime BillDate, DateTime gateInDate, int Weight, double CBM);

        bool IsGdAssociatedWithInvoice(long enteringcargoid);

        List<InvoiceExport> GetInvoiceExportbygdnumber(string gdnumber);
        List<InvoiceItemViewModel> GetExportTariffList(string gdnumber, double Weight, double CBM);
    }
}
