using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IGateInRepository : IRepo<GateIn>
    {
        long CountGateInContainerCFS();
        long CountGateInContainerCY();

        long CountGateInContainerIndexCFS();
        long CountUpcommingContainerCFS();
        long CountUpcommingContainerCY();
    }
}
