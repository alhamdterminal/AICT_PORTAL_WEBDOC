using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IGroundedContainerRepository : IRepo<GroundedContainer>
    {
        List<GroundingStatusViewModel> GrounedContainers();

        List<GroundingStatusViewModel> GrounedCYContainers();

        List<GroundingStatusViewModel> GroundingStatusCFS();

        long GrounedContainersCountCFS();
        long GrounedContainersCountCY();

        long GroundingAwaitedCFS();
        long GroundingAwaitedCY();

        long CountTotalIHCOEm();

        long CountTotalSRCO();

        long CountTotalECMO();

        bool GetFileNameSRC(string FileName);
        bool GetFileNameSRCO(string FileName);
        List<SRCO> GetUndeliveredSRCO(string virno, long indexno);
        List<SRC> GetUndeliveredSRC(string virno, string containerno);
    }
}
