using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IEnteringCargoRepository : IRepo<EnteringCargo>
    {
        ExportCargoViewModel GetCargoDetails(string gdnumber);

        EnteringCargoVIewModel GetEnteringCargo(string GDNumber, string LPNumner);

        EnteringCargoVIewModel GetEnteringCargoByGD(string GDNumber);

        //  List<GDListViewModel> GetGDS(DateTime? StartDate , DateTime? EndDate, string GDNumber, string Type);
        List<GDListViewModel> GetGDS(string Type);
        List<GDInvoicesViewModel> GetGDInvoices(string GDNumber);

        List<GDInvoicesViewModel> GetGDInvoicesExprot(string gdnumber);

        long WaitingforExaminationMarkedGDs();
        long GroundedGDs();
        EnteringCargo GetEnteringCargo(string gdnumber);


        List<InvoiceExport> GetInvoicesBygdnumber(long EnteringCargoId);
        EnteringCargo GetEnteringCargoById(long enteringCargoById);

        List<ExportContainerItem> GetExportContainerItemByExportContainerid(long exportcontainerid);

        InvoiceItemExport GetInvoiceExportitem(long invoiceid, long Tariffid);

        List<GDListViewModel> GetAlongSideGDs();
    }
}
