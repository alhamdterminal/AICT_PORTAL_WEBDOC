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
    public class OGTERepository : RepoBase<OGTE> , IOGTERepository
    {
        public OGTERepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        public long ContainerGateOutCount()
        {
            var data = (from ogte in Db.OGTEs
                        select ogte).ToList();

            if (data == null)
            {
                return 0;
            }

            return data.Count;
        }  
        public   long ReadyForGateOutContainer()
        {
            var data = (from  OSVM in Db.OSVMs
                        where 
                        !Db.OGTEs.Any(s => s.GDNumber.Trim().ToUpper() == OSVM.GDNumber.Trim().ToUpper() && 
                        s.ContainerNumber == OSVM.ContainerNo)
                         
                        select OSVM  ).Distinct().ToList();

            if (data == null)
            {
                return 0;
            }

            return data.Count;
        }

    }
}
