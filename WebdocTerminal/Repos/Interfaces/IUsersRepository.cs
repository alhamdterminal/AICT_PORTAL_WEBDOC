using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IUsersRepository : IRepo<User>
    {
        List<UserViewModel> GetUsers();
        UserViewModel GetUserById(long UserId);
        List<string> Roles();
        List<string> GetUserRoles(long UserId);

        List<IdentityRole> GetRoles();

        List<User> GetFinnaceUsers();

        List<string> RolesWithOutFinance();
    }
}
