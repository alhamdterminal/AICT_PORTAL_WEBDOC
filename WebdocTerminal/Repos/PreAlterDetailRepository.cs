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
    public class PreAlterDetailRepository : RepoBase<PreAlterDetail> , IPreAlterDetailRepository
    {
        public PreAlterDetailRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public PreAlterDetail GetCreatePerAlertDetailInfo(string containerno, string vessel, string voyage, DateTime etadate)
        {
            var newetddate = etadate.ToString("MM/dd/yyyy");

            var res = Db.PreAlterDetails.FromSql($"SELECT * From PreAlterDetail Where ContainerNumber = {containerno}  and Vessel  = {vessel}  and VoyageNumber = {voyage} and  cast ( ETADate  as date) = cast( {newetddate} as date) and IsDeleted = 0  ").LastOrDefault();

            return res;

        }

    }
}
