using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public   interface IHoldRepository : IRepo<Hold>
    {
        long HoldContainersCountCFS();
        long HoldContainersCountCY();

        Hold GetHoldContainerIndexById(long containerinexid);

        Hold GetHoldContainerById(long containerid);

        List<Hold> GetHoldDetail(string virno, long indexno, string type);
    }
}
