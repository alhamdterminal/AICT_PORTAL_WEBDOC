using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class CCMORepository : RepoBase<CCMO>, ICCMORepository
    {
        public CCMORepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public CCMO GetCCMOsByIgmContainer(string virno  , string containerno)
        {
            var res = Db.CCMOs.FromSql($"SELECT * From CCMO Where VIRNo = {virno}  and  ContainerNo = {containerno} and IsDeleted = 0 ").LastOrDefault();

            if (res != null)
            {
                return res;
            }
            return null;
        }


        public List<CCMO> GetCCMOsByContainerAndGd(string containerno , string gdnumber)
        {
            var res = Db.CCMOs.FromSql($"SELECT * From CCMO Where  ContainerNo = {containerno} and TPNo = {gdnumber} and IsDeleted = 0 ").ToList();
             
            return res;
             
        }

        public  List<CCMO> ccmodatewiselist(string containerno)
        {
            var resdata = new List<CCMO>();

            var res = Db.CCMOs.FromSql($"SELECT * From CCMO Where  ContainerNo = {containerno}  and IsDeleted = 0 ").LastOrDefault();

            if(res != null)
            {
                var resdate = res.Performed?.ToString("MM/dd/yyyy");

                resdata = Db.CCMOs.FromSql($"SELECT * From CCMO Where  ContainerNo = {containerno}  and cast(Performed as date) = cast({resdate} as date)   and IsDeleted = 0 ").ToList();

            }

            return resdata;

        }


    }
}
