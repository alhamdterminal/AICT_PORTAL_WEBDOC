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
    public class OCRLRepository : RepoBase<OCRL> , IOCRLRepository
    {
        public OCRLRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public long TotalDrayOFFGDs()
        {
            var data = (from ocrl in Db.OCRLs
                        where ocrl.Status == "OD"
                        select ocrl).ToList();

            if (data == null)
            {
                return 0;
            }

            return data.Count;
        }


        public OCRL GetOcrlfromGdnumber(string Gdnumber)
        {
            var res = Db.OCRLs.FromSql($"select * from OCRLs  where GDNumber = {Gdnumber}  and isdeleted = 0").LastOrDefault();

            return res;
        }
    }
}
