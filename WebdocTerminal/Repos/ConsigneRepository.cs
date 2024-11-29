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
    public class ConsigneRepository : RepoBase<Consigne> , IConsigneRepository
    {    
        public ConsigneRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        void IConsigneRepository.ExecuteNonSQL(string sql, params object[] param)
        {
            Db.Database.ExecuteSqlCommand(sql, param);
        }

        public Consigne GetConsigneByName(string consigneeName)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.Consignes.FromSql($"SELECT * From Consigne Where ConsigneName like  %{consigneeName}%   and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public Consigne GetConsigneById(long consigneId)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.Consignes.FromSql($"SELECT * From Consigne Where ConsigneId =  {consigneId}   and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }
    }
}
