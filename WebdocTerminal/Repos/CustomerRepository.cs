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
    public class CustomerRepository : RepoBase<Customer> , ICustomerRepository
    {
        public CustomerRepository(IUserResolveService userResolveService) : base(userResolveService)
        {


        }

        public Customer CheckCustomer(long CustomerId, string code , string name)
        {
            var res = Db.Customers.FromSql($"select * from Customer where   CustomerId != {CustomerId}  and Code = {code} and Name = {name}  and IsDeleted = 0 ").LastOrDefault();

            return res;
        }
    }
}
