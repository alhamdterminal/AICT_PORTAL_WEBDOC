using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IACKRepository : IRepo<ACK>
    {
        List<ExportFilesViewModel> getExportfilesBygdnumer(string GDNumber);

        List<ImportFielsViewModel> getImportfilesByvirNumberCFS(string virNumber);
        List<ImportFielsViewModel> getImportfilesByvirNumberCY(string virNumber);

        List<ImportFielsViewModel> getfilesInfo(string VIRNumber, string ContainerNo, string gdnumber, string BLno);
    }
}
