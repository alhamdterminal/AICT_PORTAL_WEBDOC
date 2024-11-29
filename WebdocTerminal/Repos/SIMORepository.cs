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
    public class SIMORepository : RepoBase<SIMO>, ISIMORepository
    {
        public SIMORepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public SIMO GetSIMOInfo(string BLNo , long indexno ,  string Virno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.SIMOs.FromSql($"SELECT * From SIMO Where BLNumber = {BLNo} and IndexNumber = {indexno} and VIRNumber = {Virno}   and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public SIMO GetSIMOInfoByIGMIndex( string Virno , long indexno )
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.SIMOs.FromSql($"SELECT * From SIMO Where   VIRNumber = {Virno}  and IndexNumber = {indexno}  and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }
    }
}
