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
    public class ImportDropOfTicketRepository : RepoBase<ImportDropOfTicket> , IImportDropOfTicketRepository
    {

        public ImportDropOfTicketRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }



        public ImportDropOfTicket GetLastImportDropOfTicket(string Virno, string containerno)
        {

            var asd = Db.ImportDropOfTickets.FromSql($"SELECT * From ImportDropOfTicket Where VirNumber = {Virno}  and ContainerNo = {containerno} and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

    }
}
