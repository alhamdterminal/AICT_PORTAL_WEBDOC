using Microsoft.EntityFrameworkCore;
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
    public class VIRORepository : RepoBase<VIRO>, IVIRORepository
    {
        public VIRORepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<VIRO> GetMenualViros()
        {
            var res = Db.VIROs.FromSql($"SELECT * From viro Where  MessageFrom = 'MANUAL' and IsDeleted = 0  ").ToList();
               
                return res;
            
        }

        public VIRO GetMenualVirosbyigm(string igm)
        {
            var res = Db.VIROs.FromSql($"SELECT * From viro Where  MessageFrom = 'MANUAL' and VIRNumber = {igm} and IsDeleted = 0  ").LastOrDefault();

            if(res != null)
            {

                return res;
            }

            return null;
        }

        public Voyage GetMenuaVoyagebyigm(string igm)
        {
            var res = Db.Voyages.FromSql($"SELECT * From Voyage Where    VIRNo = {igm} and IsDeleted = 0  ").LastOrDefault();

            if (res != null)
            {

                return res;
            }

            return null;
        }
    }
}
