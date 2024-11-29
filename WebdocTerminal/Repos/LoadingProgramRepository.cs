using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class LoadingProgramRepository : RepoBase<LoadingProgram>, ILoadingProgramRepository
    {
        public LoadingProgramRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        public long GenerateLPNumber()
        {
            var lastGeneratedLp = GetAll().OrderByDescending(l => l.LoadingProgramId).FirstOrDefault();
            var lastLp = Regex.Match(lastGeneratedLp.LoadingProgramNumber, @"\d+").Value;

            return Int32.Parse(lastLp) + 1;
        }


        public LoadingProgram GetLoadingProgramByLPNumber(string lpnumber)
        {

            var asd = Db.LoadingPrograms.FromSql($"select * from LoadingProgram where LoadingProgramNumber = {lpnumber}  and IsDeleted = 0").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public LoadingProgram GetLoadingProgrambygdnumber(string gdnumber)
        {

            var asd = Db.LoadingPrograms.FromSql($"select * from LoadingProgram where GDNumber = {gdnumber}  and IsDeleted = 0").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }




        public LoadingProgram GetLoadingProgramgdnumberInfo(string gdnumber)
        {

            var asd = Db.LoadingPrograms.FromSql($"select * from LoadingProgram where GDNumber = {gdnumber}  and IsDeleted = 0").Include(x => x.LoadingProgramDetails).LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public LoadingProgram GetLoadingProgramgInfobyid(long loadingProgramId)
        {

            var asd = Db.LoadingPrograms.FromSql($"select * from LoadingProgram where LoadingProgramId = {loadingProgramId}  and IsDeleted = 0").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


    }
}
