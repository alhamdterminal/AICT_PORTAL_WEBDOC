using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IInvoiceRepository : IRepo<Invoice>
    {
        InvoiceViewModel GetInvoice(long ContainerIndexId);

        //InvoiceViewModel GetExportInvoice(long EnteringCargoId);

        InvoiceViewModel GetInvoiceCY(long CyContainerId);

        List<InvoiceItemViewModel> GetCBMTotal(long ContainerIndexId, long clearingAgentId, double Weight, double CBM , string indexcargotype, DateTime gateInDate , DateTime BillDate);

        //List<InvoiceItemViewModel> GetExportCargoCBMTotal(long ContainerIndexId, int Weight, double CBM);

        List<InvoiceItemViewModel> GetSizeTotal(string igm  , int indexNo ,  long clearingAgentId, DateTime gateInDate , long containerCount , string indexcargotype , DateTime BillDate);

        //List<InvoiceItemViewModel> GetSizeTotal(long CyContainerId, long clearingAgentId, DateTime gateInDate, long containerCount);

        List<InvoiceItemViewModel> GetCFSSizeTotal(long IndexNo , string virno);

        List<InvoiceItemViewModel> GetIndexTotal(long ContainerIndexId);

        //List<InvoiceItemViewModel> GetIndexTotalExport(long EnteringCargoId);

        StorageTotalViewModel GetStorageTotal(long ContainerIndexId, long clearingAgentId, DateTime BillDate, DateTime gateInDate, DateTime destuffdate, double Weight, double CBM, string indexcargotype );

        //StorageTotalViewModel GetStorageTotalExport(long EnteringCargoId, DateTime BillDate, int Weight, double CBM);

        // Storage Total for Index Wise
       // StorageTotalViewModel GetStorageTotalIndexWise(long indexNo, string virno, DateTime BillDate);
         StorageTotalViewModel GetStorageTotalIndexWise(long ContainerIndexId , DateTime BillDate, int Weight, double CBM);
        StorageTotalViewModel GetStorageTotalCY(string igm, int indexNo, DateTime BillDate, DateTime gateInDate, long clearingAgentId ,string indexcargotype );

        InvoiceViewModel GetGeneratedInvoice(string BillNo);

        //InvoiceViewModel GetExportInvoiceByBillNo(long BillNo);

        InvoiceViewModel GetGeneratedInvoiceCY(string BillNo);

        //CargoReceived GetCargoReceivedByEnteringCargoId(long EnteringCargoId);

        double GetWaiverTotal(long ContainerIndexId);
        double GetCYWaiverTotal(long CYContainerId);

        IEnumerable<AICTAndLineIndexTariffViewModel> GetCargodetailCFSConatiner(string virno, string containerno, string indexno, long? agent, string type, DateTime? fromdate, DateTime? todate);

        AuctionAmount GetAuctionAmount(double CBM, double weight, string type, string igm, long index);

        List<InvoiceItemViewModel> GetEmptyContainertariff(long shippingAgentId , string size , DateTime arrivedate);
        EmptyContainerTaxAndFreeDay GetEmptyContainerdays(long shippingAgentId   , DateTime arrivedate);

        List<InvoiceItem> GetInvoiceItemBycontainerIndexId(long ContainerIndexId);
        Invoice GetInvoiceLast();
        Invoice GetInvoiceLastForFF();

        AICTAndLineIndexTariffViewModel GetCollectionBreakup(string virno , string indexno, DateTime BillDate);
         
        CreditAllowInfoViewModel GetInfoForCreditAllowCFS(string IGM, long index);

        CreditAllowInfoViewModel GetInfoForCreditAllowCY(string IGM, long index);


        Invoice GetInvoiceByInvoiceNumber(string invoiceno);
        List<PaymentReceived> GetPaymentReceivedByInvoiceNumber(string invoiceno);

        Invoice GetCFSFirstInvoice(long ContainerIndexId);

        Invoice GetCYFirstInvoice(long CYContainerId);

        DeliveryOrder GetDeliveryOrder(long dono);

        CYContainer GetFirstCyContainer(long CyContainerId);

        DeliveryOrder GetDeliveryOrderLast();

        List<AictLineIndexUploadReportViewModel> UploadAictLineIndexReport(DateTime? fromdate, DateTime? todate, DateTime? uploadfromdate, DateTime? uploadtodate, string igm, string Containerno,
                                                                                 long? masterline, long? line, string Delivered, string billtoline);


        List<InvoiceItem> GetInvoiceItemById(long ContainerIndexId);

        List<Waiver> GetPreviousWaiverCY(long CYContainerId);
        //List<double> GetCYStorageSizeAmount(long CYContainerId);
        List<Waiver> GetPreviousWaiverCFS(long ContainerIndexId);
         
        List<InvoiceItem> GetInvoiceItemBycycontainerId(long CYContainerId);

        Invoice GetInvoiceunsetteldexcessinvoice(string invoiceno);

        List<Invoice> GetCfsInvoices(string containerindexid);
        List<Invoice> GetCYInvoices(string cycontainerId);

        bool GetAuctionInfo(string type, string igm, long index);

        List<User> GetLineUsers();

        List<AictBillingViewModel> GetAictBillingtoLine(long shippingAgentId, DateTime fromdate, DateTime todate);

        AictBilling GetAictBillingNumberDetail(long shippingagentID);

        List<AictBilling> GetAictBillingList(DateTime fromdate, DateTime todate);
        List<AictBillingViewModel> GetAictBillingSettleUnSettleInvoices(long shippingAgentId, string nature, DateTime fromdate, DateTime todate);
        IEnumerable<AICTAndLineIndexTariffRatesViewModel> AICTAndLineIndexTariffRates(string virno, string containerno, string indexno, long? agent, long? goodhead, string type, DateTime? from, DateTime? to);

        string GetQueryResult(long? shippingAgent, long? clearingagent, long? consignee , long? goodsHeads, string type, string cargotype, DateTime? fromdate, DateTime? todate);

        List<InvoiceHeadsViewModel> GetInvoiceItemHeadsDetail();

        List<Invoice> GetInvoicesByIGMIndex(string virno, string indexno, string type);
    }
}
