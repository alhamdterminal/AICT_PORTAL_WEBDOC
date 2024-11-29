using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Controllers
{
    [MyAuthorize]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
 

        private ICompanyRepository _companyRepository;

        private IUsersRepository _userRepo;

        private IShippingAgentRepository _shippingAgentRepo;

        private IShippingAgentExportRepository _shippingAgentExportRepository;
        private IUsersEmailRepository _usersEmailRepository;

        public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
                               IUsersRepository userRepo, IShippingAgentRepository shippingAgentRepo, 
                               ICompanyRepository companyRepository, IShippingAgentExportRepository shippingAgentExportRepository
                               , SignInManager<IdentityUser> signInManager , IUsersEmailRepository usersEmailRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepo = userRepo;
            _shippingAgentRepo = shippingAgentRepo;
            _companyRepository = companyRepository;
            _shippingAgentExportRepository = shippingAgentExportRepository;
            _signInManager = signInManager;
            _usersEmailRepository = usersEmailRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserRegistration()
        {
            ViewData["Agents"] = _shippingAgentRepo.GetAll()
          .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["ExportAgents"] = _shippingAgentExportRepository.GetAll()
            .Select(v => new SelectListItem { Text = v.ShippingAgentName, Value = v.ShippingAgentExportId.ToString() });

            ViewData["Companies"] = _companyRepository.GetAll().Where(x => x.Status == true)
             .Select(v => new SelectListItem { Text = v.CompanyName, Value = v.CompanyId.ToString() });

            ViewData["Roles"] = _userRepo.GetRoles().Where(x=> x.Name != "ACCOUNT(FINANCE)" && x.Name != "FinanceSuperAdmin" && x.Name != "FinanceUser" && x.Name != "FinanceAdmin" ).Select(s => new SelectListItem { Text = s.Name, Value = s.Name }).ToList();


            return View();
        }

        public IActionResult UpdateUser(long userId)
        {
            var user = _userRepo.GetUserById(userId);

            ViewData["ShippingAgent"] = _shippingAgentRepo.GetAll()
              .Select(v => new SelectListItem { Text = v.Name, Value = v.ShippingAgentId.ToString() });

            ViewData["ShippingagentExport"] = _shippingAgentExportRepository.GetAll()
            .Select(v => new SelectListItem { Text = v.ShippingAgentName, Value = v.ShippingAgentExportId.ToString() });
             
            ViewData["Roles"] = _userRepo.RolesWithOutFinance(); 

            ViewData["Companies"] = _companyRepository.GetAll().Where(x=> x.Status == true)
            .Select(v => new SelectListItem { Text = v.CompanyName, Value = v.CompanyId.ToString() });

            return View(user);
        }

        public async Task<IActionResult> AddUser(UserViewModel user)
        {
            var user1 = new IdentityUser { UserName = user.Username, Email = user.Email };

            var result = await _userManager.CreateAsync(user1, user.Password);

            if (result.Succeeded)
            {
                var newUser = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    CNIC = user.CNIC,
                    ContactNo = user.ContactNo,
                    DateOfBirth = user.DateOfBirth,
                    ShippingAgentId = user.ShippingAgentId,
                    ShippingAgentExportId = user.ShippingAgentExportId,
                    IdentityUserId = user1.Id,
                    CompanyId = user.CompanyId,
                   Status = true
                };

                _userRepo.Add(newUser);

                if (user.Role != null && user.Role.Count > 0)
                {
                    foreach (var role in user.Role)
                    {
                        var newUserRole = await _userManager.AddToRoleAsync(user1, role);
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            var user = _userRepo.Find(model.UserId);

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.DateOfBirth = model.DateOfBirth;
                user.CNIC = model.CNIC;
                user.ContactNo = model.ContactNo;
                user.Email = model.Email;
                user.CompanyId = model.CompanyId;
                user.ShippingAgentId = model.ShippingAgentId;
                user.ShippingAgentExportId = model.ShippingAgentExportId;
                
                _userRepo.Update(user);
            }

            var identityUser = await _userManager.FindByIdAsync(model.IdentityId);

            if (identityUser != null && user != null)
            {
                var currentRoles = _userRepo.GetUserRoles(model.UserId);

                var res = await _userManager.RemoveFromRolesAsync(identityUser, currentRoles);

                if (model.Role.Count > 0)
                {
                    foreach (var role in model.Role)
                    {
                        var newUserRole = await _userManager.AddToRoleAsync(identityUser, role);
                    }
                }

            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdatePassword(UserViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                var newPassword = _userManager.PasswordHasher.HashPassword(user, model.Password);

                user.PasswordHash = newPassword;

                var res = await _userManager.UpdateAsync(user);

                if (res.Succeeded)
                {

                    return RedirectToAction("Index");

                }
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public List<UserViewModel> GetUsers()
        {
            var users = _userRepo.GetUsers();
            return users;
        }

        public IActionResult DeleteUser(long userId)
        {
           var user = _userRepo.GetAll().Where(x=> x.UserId == userId).FirstOrDefault();

           user.Status = false;
            _userRepo.Update(user);
             
            return new JsonResult(new { error = false, message = "Delete" });
        }


        public IActionResult EditStatus(User model)
        {
            var user = _userRepo.Find(model.UserId);

            if (user != null)
            {
              user.Status = model.Status;
                _userRepo.Update(user);
            }

            return new JsonResult(new { error = false, message = "Save" });
        }


        public async Task<IActionResult> LogoutUser( )
        {
             await _signInManager.SignOutAsync();
            return new JsonResult(new { error = false, message = "User logged out." });
        }



        public IActionResult UsersEmailView() 
        {
            return View();
        }


        public JsonResult GetUserEmails()
        {
            var usersEmails = _usersEmailRepository.GetAll();
            return Json(usersEmails);
        }



        [HttpPost]
        public IActionResult Post(UsersEmail UserEmail)
        {
            _usersEmailRepository.Add(UserEmail);
            return new OkObjectResult(new { UsersEmailId = UserEmail.UsersEmailId });

        }


        [HttpPost]
        public IActionResult updateUserEmail(UsersEmail UserEmail)
        {
            _usersEmailRepository.Update(UserEmail);
            return new OkObjectResult(new { UsersEmailId = UserEmail.UsersEmailId });
        }
        public void Delete(long key)
        {
            var result = _usersEmailRepository.Find(key);

            _usersEmailRepository.Delete(result);
        }


    }
}