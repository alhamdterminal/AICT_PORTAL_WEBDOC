using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Controllers
{
    public class PermissionsController : Controller
    {
        private IPermissionRepository _permissionRepo;

        private IUsersRepository _userRepo;

        private IAppPageItemRepository _appPageItemRepository;

        public PermissionsController(IPermissionRepository permissionRepo, IUsersRepository userRepo , IAppPageItemRepository appPageItemRepository)
        {
            _userRepo = userRepo;
            _permissionRepo = permissionRepo;
            _appPageItemRepository = appPageItemRepository;

        }

        public IActionResult Index()
        {
            //ViewData["Roles"] = _userRepo.GetRoles().Where(x => x.Name != "ACCOUNT(FINANCE)" && x.Name != "FinanceSuperAdmin" && x.Name != "FinanceUser" && x.Name != "FinanceAdmin").Select(s => new SelectListItem { Text = s.Name, Value = s.Name }).ToList();
            ViewData["Roles"] = _userRepo.GetRoles().Select(s => new SelectListItem { Text = s.Name, Value = s.Name }).ToList();

            return View();
        }

        //public IActionResult PermissionsPartial(string Role)
        //{
        //    var permissions = _permissionRepo.GetRolePermissions(Role);

        //    return PartialView("_permissions", permissions);
        //}

        public JsonResult PermissionsPartial(string Role)
        {

            //var role = _userRepo.GetRoles().Where(x=> x.Name == Role).FirstOrDefault();
            //var permissions = _permissionRepo.GetAll().Where(x=> x.RoleId == role.Id);
            var permissions = _permissionRepo.GetRolePermissions(Role);

            if (permissions != null)
            {
                return Json(permissions);

            }
            return Json("");
        }
        //public bool Read { get; set; }
        //public bool Create { get; set; }
        //public bool Update { get; set; }

        public IActionResult AddPermissions(List<Areas.Export.Models.RolePermissionViewModel> pms, string role)
        {

            var RoleId = _permissionRepo.GetIdByRoleName(role);

            var permission = _permissionRepo.GetAll().Where(x => x.RoleId == RoleId).ToList();

            var appPageItem = _appPageItemRepository.GetAll().Where(x => x.RoleId == RoleId).ToList();

            if (permission != null)
            {
                 _permissionRepo.DeleteRange(permission);
            }
            if (appPageItem != null)
            {
               _appPageItemRepository.DeleteRange(appPageItem);


            }


            //var pages = _permissionRepo.GetllAppPages();

            List<Permission> Permissions = new List<Permission>();
            List<AppPageItem> AppPageItems = new List<AppPageItem>();

            foreach (var item in pms)
            {
                //long PageId = 0;
                //var page = pages.FirstOrDefault(p => p.PageName == item.Page);
                //if (page != null)
                //    PageId = page.AppPageId;

                //if (item.Permissions.Contains("Read"))
                //{
                //    Read = true;
                //}
                //else
                //{
                //    Read = false;
                //}
                //if (item.Permissions.Contains("Create"))
                //{
                //    Create = true;
                //}
                //else
                //{
                //    Create = false;
                //}
                //if (item.Permissions.Contains("Update"))
                //{
                //    Update = true;
                //}
                //else
                //{
                //    Update = false;
                //}

                var data = new Permission()
                {
                    RoleId = RoleId,
                    AppPageId = item.AppPageId,
                    Read = item.Read,
                    Create = item.Create,
                    Update = item.Update

                };

                if (item.AppPageId > 0)
                    Permissions.Add(data);

                foreach (var x in item.PageItem)
                {
                    var xx = new AppPageItem()
                    {
                        FieldName = x.FieldName,
                        AppPageId = x.AppPageId,
                        IsChecked = x.IsChecked, 
                        RoleId = RoleId

                    };
                    AppPageItems.Add(xx);
                }

            }



             _permissionRepo.AddRange(Permissions);

            _appPageItemRepository.AddRange(AppPageItems);

            return new JsonResult(new { error = false, message = "Saved" });
        }


    }
}