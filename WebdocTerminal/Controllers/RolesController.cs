using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Controllers
{
    public class RolesController : Controller
    {
        private IUsersRepository _userRepo;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(IUsersRepository userRepo, RoleManager<IdentityRole> roleManager)
        {
            _userRepo = userRepo;
            _roleManager = roleManager;
        }
        public IActionResult RolesView()
        {
            return View();
        }

        public JsonResult GetRole()
        {
            var data = _userRepo.GetRoles();
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }

        public IActionResult CreateRoles(string name)
        {
            Task<IdentityResult> roleResult;

            Task<bool> hasAdminRole = _roleManager.RoleExistsAsync(name);
            hasAdminRole.Wait();

            if (!hasAdminRole.Result)
            {
                roleResult = _roleManager.CreateAsync(new IdentityRole(name));
                roleResult.Wait();
                return new JsonResult(new { error = false, message = "Save" });
            }


            return new JsonResult(new { error = true, message = "Already Exist" });

        }


    }
}