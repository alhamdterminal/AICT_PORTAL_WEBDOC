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
    public class ECMRepository : RepoBase<ECM>, IECMRepository
    {
        public ECMRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public long ECMContainersCountCFS()
        {
            var ihcoCountainer = (from ihco in Db.IHCOs
                                  where ihco.HandlingCode == "EM"
                                  select ihco
                         ).ToList();
            if (ihcoCountainer.Count == 0)
            {
                return 0;
            }

            else
            {
                return ihcoCountainer.Count;
            }
        }


        public long ECMContainersCountCY()
        {
            var ihcCountainer = (from ihc in Db.IHCs
                                  where ihc.HandlingCode == "EM"
                                 select ihc
                         ).ToList();
            if (ihcCountainer.Count == 0)
            {
                return 0;
            }

            else
            {
                return ihcCountainer.Count;
            }
        }

    }
}
