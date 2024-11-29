using Microsoft.AspNetCore.Identity;
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
    public class UsersRepository : RepoBase<User>, IUsersRepository
    {
        public UsersRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public List<string> Roles()
        {
            return Db.Roles.Select(r => r.Name).ToList();
        }

        public List<string> RolesWithOutFinance()
        {
            return Db.Roles.Where(x => x.Name != "ACCOUNT(FINANCE)" && x.Name != "FinanceSuperAdmin" && x.Name != "FinanceUser" && x.Name != "FinanceAdmin").Select(r => r.Name).ToList();
        }

        public List<IdentityRole> GetRoles()
        {
            var data = (from r in Db.Roles
                     select r).ToList();

            return data; ;
        }


    

        public List<string> GetUserRoles(long UserId)
        {
            var roles = (from role in Db.Roles
                         join userrole in Db.UserRoles on role.Id equals userrole.RoleId
                         join user in Db.Users on userrole.UserId equals user.IdentityUserId
                         where user.UserId == UserId
                         select role.Name).ToList();

            return roles;
        }

        public List<UserViewModel> GetUsers()
        {
            var users = (from usr in Db.Users
                         join identity in Db.IdentityUsers on usr.IdentityUserId equals identity.Id
                         
                         select new UserViewModel
                         {
                             UserId = usr.UserId,
                             FirstName = usr.FirstName,
                             LastName = usr.LastName,
                             ContactNo = usr.ContactNo,
                             Username = identity.UserName,
                             CNIC = usr.CNIC,
                             DateOfBirth = usr.DateOfBirth,
                             Email = usr.Email,
                             IdentityId = identity.Id,
                             Status = usr.Status

                         }).ToList();


            foreach (var user in users)
            {
                var roles = (from role in Db.Roles
                             join userrole in Db.UserRoles on role.Id equals userrole.RoleId
                             where userrole.UserId == user.IdentityId
                             select role.Name).ToList();

                user.Role = roles;
            }

            return users;
        }

        public UserViewModel GetUserById(long UserId)
        {
            var user = (from usr in Db.Users
                        join identity in Db.IdentityUsers on usr.IdentityUserId equals identity.Id
                        where usr.UserId == UserId
                        select new UserViewModel
                        {
                            UserId = usr.UserId,
                            FirstName = usr.FirstName,
                            LastName = usr.LastName,
                            ContactNo = usr.ContactNo,
                            Username = identity.UserName,
                            CNIC = usr.CNIC,
                            DateOfBirth = usr.DateOfBirth ,
                            Email = usr.Email,
                            IdentityId = identity.Id,
                            CompanyId = usr.CompanyId,
                            ShippingAgentId = usr.ShippingAgentId,
                            ShippingAgentExportId = usr.ShippingAgentExportId
                        }).FirstOrDefault();


            var roles = (from role in Db.Roles
                         join userrole in Db.UserRoles on role.Id equals userrole.RoleId
                         where userrole.UserId == user.IdentityId
                         select role.Name).ToList();

            user.Role = roles;


            return user;
        }


        public List<User> GetFinnaceUsers()
        {
            var res = (from user in Db.Users
                       join aspnetuserrol in Db.UserRoles on user.IdentityUserId equals aspnetuserrol.UserId
                       join aspnerol in Db.Roles on aspnetuserrol.RoleId equals aspnerol.Id
                       where
                       aspnerol.Name  ==  "ACCOUNT(FINANCE)" || aspnerol.Name == "FinanceAdmin" || aspnerol.Name == "FinanceUser" || aspnerol.Name == "FinanceSuperAdmin"
                       select user).ToList();


            return res;
        }

    }
}
