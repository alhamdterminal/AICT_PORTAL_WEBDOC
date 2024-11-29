using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IDestuffedContainerRepository : IRepo<DestuffedContainer>
    {
        long DestuffedContainersCountCFS();
        long DestuffedContainersIndexCountCFS();
        List<ContainerIndex> getContainerIndex(string containerNumber, string virNo);

        DestuffedContainer GetDestuffByContainerIndexId(long containerinexid);
     }
}
