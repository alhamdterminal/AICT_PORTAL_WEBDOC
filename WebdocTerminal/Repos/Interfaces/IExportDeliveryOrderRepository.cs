using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IExportDeliveryOrderRepository : IRepo<ExportDeliveryOrder>
    {
        List<string> GetGDsWithoutDOs();
        ExportDeliveryOrderViewModel GetDeliveryOrderDisplayInfo(string gdnmuber);
        long PreviousDONumber();

        List<OCRL> GetUnDeliveredGDS();

        List<ExportDeliveryOrder> GetUnDeliveredGatePassGDS();

        ExportDeliveryOrderViewModel GetExportGatePassDisplayInfo(string gdnmuber);

        List<GatePassExport> GetGatePassbyGDnumber(string gdnumber);

        DOItemExport GetDOITemById(long id);

        GatePassExport GetGatepassExportById(long id);

        List<GatePassExport> GetGatepassExportByGDNumber(string gdumber);

        DOItemExport GetDOItemExportByID(long id);

        ExportDeliveryOrder GetExportDeliveryOrderByGDnumber(string gdnumber);

        List<ExportDeliveryOrderViewModel> GetExportGatePassInfo(string gdnmuber);
         
    }

}
