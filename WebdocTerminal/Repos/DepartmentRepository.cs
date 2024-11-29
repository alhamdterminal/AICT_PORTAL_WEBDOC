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
    public class DepartmentRepository : RepoBase<Department> , IDepartmentRepository
    {
        public DepartmentRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
         
        public  bool GetDepartment(string code , string name  , long id)
        {
            var res = Db.Departments.FromSql($"select * from Department where   IsDeleted = 0  and( DepartmentCode = {code} or DepartmentName = {name} ) and DepartmentId != {id} ").FirstOrDefault();

            if(res != null)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
