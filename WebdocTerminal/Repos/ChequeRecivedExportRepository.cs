using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class ChequeRecivedExportRepository : RepoBase<ChequeRecivedExport> , IChequeRecivedExportRepository
    {
        public ChequeRecivedExportRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public string GetLastChequeReceiveByType()
        {
            var data = (from ChequeRecived in Db.ChequeRecivedExports
                        select
                          ChequeRecived.Type
                        ).LastOrDefault();
            return data;
        }
    }
}
