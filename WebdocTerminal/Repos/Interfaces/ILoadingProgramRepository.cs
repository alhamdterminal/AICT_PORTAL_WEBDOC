using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface ILoadingProgramRepository : IRepo<LoadingProgram>
    {
        long GenerateLPNumber();

        LoadingProgram GetLoadingProgramByLPNumber(string lpnumber);

        LoadingProgram GetLoadingProgrambygdnumber(string gdnumber);

        LoadingProgram GetLoadingProgramgdnumberInfo(string gdnumber);
        LoadingProgram GetLoadingProgramgInfobyid(long loadingProgramId);
    }
}
