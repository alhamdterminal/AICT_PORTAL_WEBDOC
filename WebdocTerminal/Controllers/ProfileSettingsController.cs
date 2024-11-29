using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebdocTerminal.Areas.Identity.Pages.Account;
using WebdocTerminal.Data;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Controllers
{
    [MyAuthorize]
    public class ProfileSettingsController : Controller
    {
        private  UserManager<IdentityUser> _userManager;
        private  SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private IUsersRepository _userRepo;
        private readonly ILogger<LogoutModel> _logger;

        public ProfileSettingsController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,RoleManager<IdentityRole> roleManager,
            IUsersRepository userRepo
            , ILogger<LogoutModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userRepo = userRepo;
            _logger = logger;

        }

        public int MyProperty { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            var appUser = _userRepo.GetFirst(e=>e.IdentityUserId == identityUser.Id);
            var role = await _userManager.GetRolesAsync(identityUser);
            UsersViewModel model = new UsersViewModel();
            model.FirstName = appUser.FirstName;
            model.LastName = appUser.LastName;
            model.Role = role[0];
            model.DateOfBirth = appUser.DateOfBirth;
            model.ContactNo = appUser.ContactNo;
            model.Email = appUser.Email;
            model.CNIC = appUser.CNIC;
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            var appUser = _userRepo.GetFirst(e => e.IdentityUserId == identityUser.Id);
            var role = await _userManager.GetRolesAsync(identityUser);
            UsersViewModel model = new UsersViewModel();
            model.FirstName = appUser.FirstName;
            model.LastName = appUser.LastName;
            model.Role = role[0];
            model.DateOfBirth = appUser.DateOfBirth;
            model.ContactNo = appUser.ContactNo;
            model.Email = appUser.Email;
            model.CNIC = appUser.CNIC;
            model.Roles = _roleManager.Roles;
            //ViewData["roles"] = ;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UsersViewModel model)
        {
            var id = _userManager.GetUserId(HttpContext.User);
            var user = _userRepo.GetFirst(e=>e.IdentityUserId == id);
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DateOfBirth = model.DateOfBirth;
            user.CNIC = model.CNIC;
            user.ContactNo = model.ContactNo;
            _userRepo.Update(user);
            

            IdentityUser user1 = await _userManager.GetUserAsync(User);
          
            user1.NormalizedEmail = model.Email;
            await _userManager.SetEmailAsync(user1,model.Email);
            await _userManager.UpdateNormalizedEmailAsync(user1);
            
            return RedirectToAction("Profile");
        }
        public async Task<IActionResult> UpdatePassword(string CurrentPassword,string NewPassword)
        {


            //    var user = await _userManager.GetUserAsync(User);
            //    var result = await _userManager.ChangePasswordAsync(user,CurrentPassword,NewPassword);
            ////_userManager.ResetPasswordAsync();

            //return Json(result);
            var returnUrl = "/Identity/Account/Login";


            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var newPassword = _userManager.PasswordHasher.HashPassword(user, NewPassword);

                user.PasswordHash = newPassword;

                var res = await _userManager.UpdateAsync(user);

                if (res.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    _logger.LogInformation("User logged out.");
                     
                    return LocalRedirect(returnUrl);
                     
                }
            }


            return RedirectToAction("Index");


        }

    }
}