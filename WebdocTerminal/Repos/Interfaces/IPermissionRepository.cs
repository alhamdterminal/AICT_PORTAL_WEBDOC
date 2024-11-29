using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IPermissionRepository : IRepo<Permission>
    {
        List<RolePermissionViewModel> GetRolePermissions(string Role);

        string GetIdByRoleName(string role);
        List<IdentityRole> GetRoles();

        List<AppPage> GetllAppPages();

        List<UserPermissionsViewModel> GetUserPermissions(string Role);


        List<AppPageItem> GetUserGetPermissionItems(string Role  , string url);

        List<UserPermissionsViewModel> GetUserPermissions(List<string> Role);


        List<AppPageItem> GetUserGetPermissionItemsList(List<string> Role);

    }
}
