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
    public class OSIMRepository : RepoBase<OSIM> , IOSIMRepository
    {
        public OSIMRepository(IUserResolveService userResolveService) : base(userResolveService)
        {


        }
        public long getCurrentGDHold()
        {
            long x = 0;
            var gdnumbers = (from osim in Db.OSIMs
                             select osim.GDNumber).Distinct().ToList();

            foreach (var item in gdnumbers)
            {
                var data = (from osim in Db.OSIMs
                            where osim.GDNumber == item
                            select osim
                                 ).LastOrDefault();
                if (data != null && data.Status == "HO")
                {
                    x += 1;
                }
            }



            return x;
        }


        public OSIM GetOSIMInfo(string gdnumber)
        {

            var asd = Db.OSIMs.FromSql($"select * from osims where GDNumber = {gdnumber}  and IsDeleted = 0").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }
    }
}
