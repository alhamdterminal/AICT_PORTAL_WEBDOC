using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IEmptyGateOutContainerRepository : IRepo<EmptyGateOutContainer>
    {
        List<EmptyGateOutContainerViewModel> GetEmptyGateOutContainers();

        List<EmptyGateOutContainerViewModel> GetEmptyDeliverContainerGatePass(string container);


    }
}
