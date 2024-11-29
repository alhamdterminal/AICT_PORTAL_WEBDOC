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
    public class VoucherServiceTypeRepository : RepoBase<VoucherServiceType> , IVoucherServiceTypeRepository
    {
        public VoucherServiceTypeRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public VoucherServiceType CheckVoucherServiceType(string code, string name, long id)
        {
            var res = Db.VoucherServiceTypes.FromSql($"select * from VoucherServiceType where   IsDeleted = 0  and( Code = {code} or Name = {name} ) and VoucherServiceTypeId != {id} ").FirstOrDefault();

            return res;

        }
    }
}
