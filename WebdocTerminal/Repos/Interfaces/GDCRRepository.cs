using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos.Interfaces
{
    public class GDCRRepository : RepoBase<GDCR>, IGDCRRepository
    {
        public GDCRRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        public GDCR GetInfo(string Virno, string containerno, string blnumber)
        {
            var asd = Db.GDCRs.FromSql($"SELECT * From GDCR Where VirNumber = {Virno}  and OldContainerNumber = {containerno} and BLNumber = {blnumber}").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public List<CYContainer> GetContainerInfo(string Virno, string containerno, string blnumber)
        {
            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno}  and ContainerNo = {containerno} and BLNo = {blnumber}").ToList();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

    }
}
