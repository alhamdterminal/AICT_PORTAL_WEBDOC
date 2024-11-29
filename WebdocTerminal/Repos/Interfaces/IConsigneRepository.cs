using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
  public  interface IConsigneRepository : IRepo<Consigne>
    {
        void ExecuteNonSQL(string sql, params object[] param);
        Consigne GetConsigneByName(string consigneeName);

        Consigne GetConsigneById(long consigneId);
    }
}
