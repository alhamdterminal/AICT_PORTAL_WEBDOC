using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public  interface IExportContainerRepository : IRepo<ExportContainer>
    {
        List<ServiceCompletionViewModel> GetUnServiceCompletionContainers();

        List<ContainerAssociationViewModel> GetUnContainerAssociationContainers();

        List<GateOutViewModel> GetGateOutExportContainers(string containerNo);

        List<ExportContainerItemsViewModel> GetExportContainerItems(long vesselid, long voyageId , long exportContainerId);

        List<ExportContainerItemsViewModel> GetExportContainerItemsBYContainerNumber(string containerNumber);

        long ExportContainersCount();

        long ContainerReadyForSuffing();

        ExportContainer GetExportContainerById(long exportcontainerid);

        List<ExportContainerItem> GetExportContainerItemsByExportContainerId(long exportcontainerid);

        ExportContainerItem GetExportContainerItemsByExportContainerIdandGD(long exportcontainerid, string gdnumber);

        List<PendingGdsInvoicesViewModel> GetPendingGdsInvoicesViewModel();

        List<UnSettledInvoicesViewModel> GetUnSettledInvoicesViewModel();

        List<UnSettledInvoicesViewModel> GetGDsAccoicatedbutAmountNotreceivedViewModel();

        List<ExportAlongSideDataViewModel> GetExportAlongSideData();
        List<ExportContainerItemsViewModel> GetPendingGds();
        List<ExportContainerItemsViewModel> GetPendingGdsForAssociation();

        List<ExportContainer> GetPendingcontainers();
        ExportContainerItemsViewModel GetgGdDetail(string Gdnumber);

        List<ExportContainerItem> GetExportContainerItemsByGdnumber(string GdNumebr);
        List<ExportContainerItem> GetExportContainerItemsByContainerNumber(string ContainerNumber);
        string UpdateIscheckStatus(List<ExportContainerItem> model);

        List<ExportContainerItem> GetExportContainerItemsByexportcontainerid(long exportcontainerid);

        List<ExportContainerItem> GetExportContainerItemsByGdnumberFOrAssociation(string GdNumebr);

        List<ExportContainerItem> GetExportContainerItemsByExportcontainerIdFOrAssociation(long exportcontainerid);

        ExportContainer GetExportContainerByID(long exportcontainerid);
        List<ExportContainerItem> AlongsideGds();

        List<GateOutViewModel> GetGateOutExport();
    }
}
