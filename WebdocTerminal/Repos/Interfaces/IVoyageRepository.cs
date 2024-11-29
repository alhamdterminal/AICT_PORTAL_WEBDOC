using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IVoyageRepository : IRepo<Voyage>
    {
        List<Voyage> GetVoyages();
        List<Voyage> GetVoyagesCY();

        List<IndexDropdownViewModel> GetContainerIndexs(string voyageNo, string IGM);
        List<CYContainer> GetCYContainers(string IGM);

        List<Voyage> GetVoyageIGMSList();
        Voyage GetVoyageByIGM(string virno);

        List<string> GetPreAlertVesselName();
        List<string> GetPreAlertVoyage(string vesselname);
    }
}
