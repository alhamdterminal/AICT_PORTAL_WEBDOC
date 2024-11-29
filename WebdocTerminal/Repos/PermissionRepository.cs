using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class PermissionRepository : RepoBase<Permission>, IPermissionRepository
    {
        public PermissionRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        //public List<PermissionsViewModel> GetRolePermissions(string Role)
        //{
        //    var permissions = (from roles in Db.Roles
        //                       join perm in Db.Permissions on roles.Id equals perm.RoleId
        //                       where roles.Name == Role
        //                       select perm).ToList();

        //    var pages = (from page in Db.AppPages
        //                 from perm in permissions.Where(p => p.AppPageId == page.AppPageId).DefaultIfEmpty()
        //                 select page).ToList();

        //    var permissionViewModel = new List<PermissionsViewModel>();

        //    foreach (var page in pages)
        //    {
        //        permissionViewModel.Add(new PermissionsViewModel { PageName = page.PageName });

        //        if(page.Permissions != null)
        //        {
        //            foreach (var perm in page.Permissions)
        //            {
        //                if (perm.Read && perm.Create && perm.Update)
        //                    permissionViewModel.FirstOrDefault(p => p.PageName == page.PageName).Permissions = new List<string> { "Read", "Create", "Update" };
        //                else if (perm.Read && perm.Create)
        //                    permissionViewModel.FirstOrDefault(p => p.PageName == page.PageName).Permissions = new List<string> { "Read", "Create" };
        //                else if (perm.Read)
        //                    permissionViewModel.FirstOrDefault(p => p.PageName == page.PageName).Permissions = new List<string> { "Read" };
        //                else
        //                    permissionViewModel.FirstOrDefault(p => p.PageName == page.PageName).Permissions = new List<string> { "" };
        //            }
        //        }

        //    }

        //    return permissionViewModel;
        //}


        public List<RolePermissionViewModel> GetRolePermissions(string Role)
        {
            var roleId = (from roles in Db.Roles
                          
                          where roles.Name == Role
                          select roles.Id).FirstOrDefault();

            var appPageItemRoleId = (from appPageItem in Db.AppPageItems
                          where appPageItem.RoleId == roleId
                          select appPageItem.RoleId).FirstOrDefault();

            var permissions = (from roles in Db.Roles
                               join perm in Db.Permissions on roles.Id equals perm.RoleId
                               where roles.Name == Role
                               select perm).ToList();

            var pages = (from page in Db.AppPages
                         
                         from perm in permissions.Where(p => p.AppPageId == page.AppPageId).DefaultIfEmpty()
                         where page.PageType != "FinanceAccounts"
                         select new RolePermissionViewModel
                         {

                             AppPageId = page.AppPageId,
                             Create = perm.Create,
                             Read = perm.Read,
                             Update = perm.Update,
                             PageName = page.PageName,
                             PageType = page.PageType,
                             PageItem = (
                             
                             from pageItem in Db.AppPageItems.Where(x => x.AppPageId == page.AppPageId   ).DefaultIfEmpty() select  
                                         pageItem).Where(x =>
appPageItemRoleId != null ? x.RoleId == appPageItemRoleId : x.RoleId == null
).ToList()

                         }).ToList();

          
            return pages;
        }

        public List<IdentityRole> GetRoles()
        {
            return Db.Roles.ToList();
        }

        public string GetIdByRoleName(string role)
        {
            var data = (from roles in Db.Roles
                       where roles.Name.Trim().ToUpper().Contains(role.Trim().ToUpper()) 
                        select roles.Id).FirstOrDefault();

            return data;
        }

        public List<AppPage> GetllAppPages() {
            return Db.AppPages.ToList();
        }

        public List<UserPermissionsViewModel> GetUserPermissions(string Role)
        {
            var permissions = (from permission in Db.Permissions
                               join role in Db.Roles on permission.RoleId equals role.Id
                               join page in Db.AppPages on permission.AppPageId equals page.AppPageId
                               where role.Name == Role
                               select new UserPermissionsViewModel
                               {
                                   Url = page.PageUrl,
                                   Create = permission.Create,
                                   Read = permission.Read,
                                   Update = permission.Update
                               }).ToList();

            return permissions;
                               
        }

        public List<UserPermissionsViewModel> GetUserPermissions(List<string> Role)
        {
            var listdata = new List<UserPermissionsViewModel>();
            foreach (var item in Role)
            {
                var permissions = (from permission in Db.Permissions
                                   join role in Db.Roles on permission.RoleId equals role.Id
                                   join page in Db.AppPages on permission.AppPageId equals page.AppPageId
                                   where role.Name == item 
                                   && permission.Read == true
                                   select new UserPermissionsViewModel
                                   {
                                       Url = page.PageUrl,
                                       Create = permission.Create,
                                       Read = permission.Read,
                                       Update = permission.Update
                                   }).ToList();

                if(permissions.Count() > 0)
                {
                   
                    listdata.AddRange(permissions);
                }
            }



            var Merged = (from data in listdata
                          group data by data.Url into g
                          select new UserPermissionsViewModel
                          {
                              Url = g.FirstOrDefault().Url,
                              Create = g.FirstOrDefault().Create,
                              Read = g.FirstOrDefault().Read,
                              Update = g.FirstOrDefault().Update
                          }).ToList();


            return Merged;

        }

        public List<AppPageItem> GetUserGetPermissionItems(string Role , string url)
        {

            var roleId = (from roles in Db.Roles

                          where roles.Name == Role
                          select roles.Id).FirstOrDefault();

            var appPageItemRoleId = (from appPageItem in Db.AppPageItems
                                     where appPageItem.RoleId == roleId
                                     select appPageItem.RoleId).FirstOrDefault();

            var permissions = (from appPage in Db.AppPages 
                               join pageItem in Db.AppPageItems on appPage.AppPageId equals pageItem.AppPageId
                               where appPage.PageUrl == url
                               select pageItem).Where(x =>
                               appPageItemRoleId != null ? x.RoleId == appPageItemRoleId : x.RoleId == null
                               ).ToList();

            return permissions;
        }

        public List<AppPageItem> GetUserGetPermissionItemsList(List<string> Role)
        {

            var listdata = new List<AppPageItem>();



            foreach (var item in Role)
            {

                var resdata = (from appPageItem in Db.AppPageItems
                               join role in Db.Roles on appPageItem.RoleId equals role.Id
                               where
                               role.Name == item
                               && appPageItem.IsChecked == true
                               select new AppPageItem
                               {
                                   FieldName = appPageItem.FieldName,
                                   IsChecked = appPageItem.IsChecked,
                                   AppPageId = appPageItem.AppPageId,
                                   RoleId = appPageItem.RoleId
                                   
                               }).ToList();
 
                 
                if (resdata.Count() > 0)
                {

                    listdata.AddRange(resdata);
                }
            }



            var Merged = (from data in listdata
                          group data by data.FieldName into g
                          select new AppPageItem
                          {
                              FieldName = g.FirstOrDefault().FieldName,
                              IsChecked = g.FirstOrDefault().IsChecked,
                              AppPageId = g.FirstOrDefault().AppPageId,
                              RoleId = g.FirstOrDefault().RoleId
                          }).ToList();


            return Merged;





        }

    }
}
