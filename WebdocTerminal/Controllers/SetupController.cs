using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;
 
namespace WebdocTerminal.Controllers
{
    [MyAuthorize]
    public class SetupController : Controller
    {
        public ICountryRepository _countryRepository;
        public IUsersRepository _usersRepository;
        public ICityRepository  _cityRepository;
        public IDepartmentRepository _departmentRepository;
        public IAccountNatureRepository _accountNatureRepository;
        public IAccountCategoryRepository _accountCategoryRepository;
        public IAccountSubCategoryRepository _accountSubCategoryRepository;
        public IAccountTypeRepository _accountTypeRepository;
        public IVoucherServiceTypeRepository _voucherServiceTypeRepository;
        public IChartOfAccountRepository  _chartOfAccountRepository;
        public ICustomerRepository _customerRepository;
        public IVoucherTypeRepository _voucherTypeRepository;
        public IAllowBackDateTransactionRepository _allowBackDateTransactionRepository;
        public IFinancialYearRepository _financialYearRepository;
        public SetupController(ICountryRepository countryRepository ,
                               IUsersRepository usersRepository,
                               ICityRepository cityRepository,
                               IDepartmentRepository departmentRepository,
                               IAccountNatureRepository accountNatureRepository,
                               IAccountCategoryRepository accountCategoryRepository,
                               IAccountSubCategoryRepository accountSubCategoryRepository,
                               IAccountTypeRepository accountTypeRepository,
                               IVoucherServiceTypeRepository voucherServiceTypeRepository,
                               IChartOfAccountRepository chartOfAccountRepository,
                               ICustomerRepository customerRepository,
                               IVoucherTypeRepository voucherTypeRepository,
                               IAllowBackDateTransactionRepository allowBackDateTransactionRepository,
                               IFinancialYearRepository financialYearRepository)
        {
            _countryRepository = countryRepository;
            _usersRepository = usersRepository;
            _cityRepository = cityRepository;
            _departmentRepository = departmentRepository;
            _accountNatureRepository = accountNatureRepository;
            _accountCategoryRepository = accountCategoryRepository;
            _accountSubCategoryRepository = accountSubCategoryRepository;
            _accountTypeRepository = accountTypeRepository;
            _voucherServiceTypeRepository = voucherServiceTypeRepository;
            _chartOfAccountRepository = chartOfAccountRepository;
            _customerRepository = customerRepository;
            _voucherTypeRepository = voucherTypeRepository;
            _allowBackDateTransactionRepository = allowBackDateTransactionRepository;
            _financialYearRepository = financialYearRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Country

        public IActionResult CountryView()
        {
            return View();
        }

        public JsonResult GetCountries()
        {
            var res = _countryRepository.GetAll();

            return Json(res);
        }

        public IActionResult AddCountry(Country model)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if(users != null)
                {
                    model.CreatedAt = DateTime.Now;
                    model.CreatedBy = users.UserId;
                    model.EditedBy = null;
                    model.EditedAt = null;
                    _countryRepository.Add(model);

                    return new OkObjectResult(new { error = false, message = "Created" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no user  found" });
                }
                 

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
            

        }



        [HttpPost]
        public IActionResult UpdateCountry(Country model)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {

                    model.EditedAt = DateTime.Now;
                    model.EditedBy = users.UserId;
                    _countryRepository.Update(model);

                    return new OkObjectResult(new { error = false, message = "Updated" });
                }
                else
                {
                    return new OkObjectResult(new { error = false, message = "no user found" });
                }
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });


            }

        }

        #endregion

        #region City

        public IActionResult CityView()
        {
            return View();
        }

        public JsonResult GetCity()
        {
            var res = _cityRepository.GetAll();

            return Json(res);
        }

        public IActionResult AddCity(City model)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {
                    model.CreatedAt = DateTime.Now;
                    model.CreatedBy = users.UserId;
                    model.EditedBy = null;
                    model.EditedAt = null;
                    _cityRepository.Add(model);

                    return new OkObjectResult(new { error = false, message = "Created" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no user  found" });
                }


            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }

         
        public IActionResult UpdateCity(City model)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {

                    model.EditedAt = DateTime.Now;
                    model.EditedBy = users.UserId;
                    _cityRepository.Update(model);

                    return new OkObjectResult(new { error = false, message = "Updated" });
                }
                else
                {
                    return new OkObjectResult(new { error = false, message = "no user found" });
                }
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });


            }

        }

        #endregion


        #region Department

        public IActionResult DepartmentView()
        {
            return View();
        }

        public JsonResult GetDepartment()
        {
            var resdata = new List<Department>();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            if (users != null)
            {
                if (users.CompanyId != null || users.CompanyId != 0)
                {
                    resdata = _departmentRepository.GetAll().Where(x => x.CompanyId == users.CompanyId).ToList();

                    return Json(resdata);
                }

            }
            return Json(resdata);

            
        }

        public IActionResult AddDepartment(Department model)
        {
            try
            {
                if (model.DepartmentCode.Length != 4)
                {
                    return new OkObjectResult(new { error = true, message = "code length must 4 digit." });

                }

                var res = _departmentRepository.GetAll().Where(x => x.DepartmentName == model.DepartmentName || x.DepartmentCode == model.DepartmentCode).FirstOrDefault();

                if (res != null)
                {
                    return new OkObjectResult(new { error = true, message = "Department Name/Code already exist." });

                }



                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {
                    model.CreatedAt = DateTime.Now;
                    model.CreatedBy = users.UserId;
                    model.CompanyId = users.CompanyId ?? 0;
                    model.EditedBy = null;
                    model.EditedAt = null;
                    _departmentRepository.Add(model);

                    return new OkObjectResult(new { error = false, message = "Created" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no user  found" });
                }


            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }


        public IActionResult UpdateDepartment(Department model)
        {
            try
            {
                if (model.DepartmentCode.Length != 4)
                {
                    return new OkObjectResult(new { error = true, message = "code length must 4 digit." });

                }

                var res = _departmentRepository.GetDepartment(model.DepartmentCode, model.DepartmentName, model.DepartmentId);

                if(res == true)
                {
                    return new OkObjectResult(new { error = true, message = "Department Name/Code already exist." });

                }

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {

                    model.EditedAt = DateTime.Now;
                    model.EditedBy = users.UserId;
                    model.CompanyId = users.CompanyId ?? 0;
                    _departmentRepository.Update(model);

                    return new OkObjectResult(new { error = false, message = "Updated" });
                }
                else
                {
                    return new OkObjectResult(new { error = false, message = "no user found" });
                }
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });


            }

        }

        #endregion

        #region Account Nature

        public IActionResult AccountNatureView()
        {
            return View();
        }

        public JsonResult GetAccountNature()
        {
            var res = _accountNatureRepository.GetAll();

            return Json(res);
        }

        public IActionResult AddAccountNature(AccountNature model)
        {
            try
            {

                if (model.Code.Length != 1)
                {
                    return new OkObjectResult(new { error = true, message = "code length must 1 digit." });

                }

                var res = _accountNatureRepository.GetAll().Where(x => x.Name == model.Name || x.Code == model.Code).FirstOrDefault();

                if (res != null)
                {
                    return new OkObjectResult(new { error = true, message = "Name/Code already exist." });

                }


                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {
                    model.CreatedAt = DateTime.Now;
                    model.CreatedBy = users.UserId;
                    model.EditedBy = null;
                    model.EditedAt = null;
                    _accountNatureRepository.Add(model);

                    return new OkObjectResult(new { error = false, message = "Created" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no user  found" });
                }


            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }



        [HttpPost]
        public IActionResult UpdateAccountNature(AccountNature model)
        {
            try
            {

                if (model.Code.Length != 1)
                {
                    return new OkObjectResult(new { error = true, message = "code length must 1 digit." });
                }

                var res = _accountNatureRepository.GetAccountNature(model.Code, model.Name, model.AccountNatureId);

                if (res == true)
                {
                    return new OkObjectResult(new { error = true, message = "Department Name/Code already exist." });

                }

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {

                    model.EditedAt = DateTime.Now;
                    model.EditedBy = users.UserId;
                    _accountNatureRepository.Update(model);

                    return new OkObjectResult(new { error = false, message = "Updated" });
                }
                else
                {
                    return new OkObjectResult(new { error = false, message = "no user found" });
                }
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });


            }

        }

        #endregion

        #region Account Category

        public IActionResult AccountCategoryView()
        {
            return View();
        }

        public JsonResult GetAccountCategory()
        {
            var res = _accountCategoryRepository.GetAll();

            return Json(res);
        }

        public IActionResult AddAccountCategory(AccountCategory model)
        {
            try
            {
                if (Convert.ToInt16(model.ToAccount) < Convert.ToInt16(model.FromAccount))
                {
                    return new OkObjectResult(new { error = true, message = "to range should be great then from range." });
                }

                if (model.Code.Length != 4 )
                {
                    return new OkObjectResult(new { error = true, message = "code length must 4 digit." });
                }

                var res = _accountCategoryRepository.GetAll().Where(x => x.Name == model.Name || x.Code == model.Code).FirstOrDefault();

                if (res != null)
                {
                    return new OkObjectResult(new { error = true, message = "Name/Code already exist." });
                } 

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {
                    if (users.CompanyId == null || users.CompanyId == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "CompanyId is required." });

                    }

                    var resdata = _accountCategoryRepository.GetAccountCategory( users.CompanyId ?? 0 ,  model.FromAccount ,  model.ToAccount);

                    if (resdata == true)
                    {
                        return new OkObjectResult(new { error = true, message = "Category Range "+  model.FromAccount +"-"+ model.ToAccount +" already assigned " });
                    }


                    model.CreatedAt = DateTime.Now;
                    model.CreatedBy = users.UserId;
                    model.CompanyId = users.CompanyId ?? 0;
                    model.EditedBy = null;
                    model.EditedAt = null;
                    _accountCategoryRepository.Add(model);

                    return new OkObjectResult(new { error = false, message = "Created" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no user  found" });
                }


            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }


        public IActionResult UpdateAccountCategory(AccountCategory model)
        {
            try
            {

                var catgory = _accountCategoryRepository.GetAll().Where(x => x.AccountCategoryId == model.AccountCategoryId).LastOrDefault();

                if(catgory != null)
                {
                    if (Convert.ToInt16(model.ToAccount) < Convert.ToInt16(model.FromAccount))
                    {
                        return new OkObjectResult(new { error = true, message = "to range should be great then from range." });
                    }

                    if (model.Code.Length != 4)
                    {
                        return new OkObjectResult(new { error = true, message = "code length must 4 digit." });
                    }

                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();


                    if (users != null)
                    {

                        if (users.CompanyId == null || users.CompanyId == 0)
                        {
                            return new OkObjectResult(new { error = true, message = "CompanyId is required." });
                        }

                        var resdata = _accountCategoryRepository.GetAccountCategorybyId(model.AccountCategoryId, users.CompanyId ?? 0, model.FromAccount, model.ToAccount);

                        if (resdata != null)
                        {
                            return new OkObjectResult(new { error = true, message = "Category Range " + model.FromAccount + "-" + model.ToAccount + " already assigned to " + model.Name });
                        }

                        var accountSubCategory = _accountSubCategoryRepository.GetAll()
                                                .Where(sc => sc.AccountCategoryId == model.AccountCategoryId && sc.IsActive == true).FirstOrDefault();

                        if (accountSubCategory == null)
                        {
                            catgory.CompanyId = users.CompanyId ?? 0;
                            catgory.AccountNatureId = model.AccountNatureId;
                            catgory.FromAccount = model.FromAccount;
                            catgory.ToAccount = model.ToAccount;
                        }

                        catgory.EditedAt = DateTime.Now;
                        catgory.EditedBy = users.UserId;
                        catgory.Code = model.Code;
                        catgory.Name = model.Name;
                        catgory.IsActive = model.IsActive;
                        _accountCategoryRepository.Update(catgory);

                        return new OkObjectResult(new { error = false, message = "Updated" });
                    }
                    else
                    {
                        return new OkObjectResult(new { error = false, message = "no user found" });
                    }
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "non data found" });

                }

                
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });


            }

        }

        #endregion

        #region Account Sub Category

        public IActionResult AccountSubCategoryView()
        {
            return View();
        }

        public JsonResult GetAccountSubCategory()
        {
            var res = _accountSubCategoryRepository.GetAll();

            return Json(res);
        }

        public IActionResult AddAccountSubCategory(AccountSubCategory model)
        {
            try
            {
                 
                if (Convert.ToInt16(model.ToAccount) < Convert.ToInt16(model.FromAccount))
                {
                    return new OkObjectResult(new { error = true, message = "to range should be great then from range." });
                }

                if (model.Code.Length != 4)
                {
                    return new OkObjectResult(new { error = true, message = "code length must 4 digit." });
                }

                var res = _accountSubCategoryRepository.GetAll().Where(x => x.Name == model.Name || x.Code == model.Code).FirstOrDefault();

                if (res != null)
                {
                    return new OkObjectResult(new { error = true, message = "Name/Code already exist." });
                }

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {
                    if (users.CompanyId == null || users.CompanyId == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "CompanyId is required." });

                    }

                    var resdata = _accountSubCategoryRepository.CheckAccount(model.AccountCategoryId , users.CompanyId ?? 0 , model.FromAccount, model.ToAccount);

                    if (resdata == null)
                    {
                        return new OkObjectResult(new { error = true, message = "entered range" + model.FromAccount + "-" + model.ToAccount + " out of category range " });
                    }



                    var resdatasub = _accountSubCategoryRepository.CheckSubCategory( users.CompanyId ?? 0, model.FromAccount, model.ToAccount);

                    if (resdatasub != null)
                    {
                        return new OkObjectResult(new { error = true, message = " range" + model.FromAccount + "-" + model.ToAccount + " already assigned to  " + resdatasub.Name });
                    }



                    model.CreatedAt = DateTime.Now;
                    model.CreatedBy = users.UserId;
                    model.CompanyId = users.CompanyId ?? 0;
                    model.EditedBy = null;
                    model.EditedAt = null;
                    _accountSubCategoryRepository.Add(model);

                    return new OkObjectResult(new { error = false, message = "Created" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no user  found" });
                }


            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }


        public IActionResult UpdateAccountSubCategory(AccountSubCategory model)
        {
            try
            {

                var accountsubcatgry = _accountSubCategoryRepository.GetAll().Where(x => x.AccountSubCategoryId == model.AccountSubCategoryId).LastOrDefault();

                if (accountsubcatgry != null)
                {
                    if (Convert.ToInt16(model.ToAccount) < Convert.ToInt16(model.FromAccount))
                    {
                        return new OkObjectResult(new { error = true, message = "to range should be great then from range." });
                    }

                    if (model.Code.Length != 4)
                    {
                        return new OkObjectResult(new { error = true, message = "code length must 4 digit." });
                    }

                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();


                    if (users != null)
                    {

                        if (users.CompanyId == null || users.CompanyId == 0)
                        {
                            return new OkObjectResult(new { error = true, message = "CompanyId is required." });

                        }

                        var resdata = _accountSubCategoryRepository.CheckAccount(model.AccountCategoryId, users.CompanyId ?? 0, model.FromAccount, model.ToAccount);

                        if (resdata == null)
                        {
                            return new OkObjectResult(new { error = true, message = "entered range" + model.FromAccount + "-" + model.ToAccount + " out of category range " });
                        }

                        var subresdata = _accountSubCategoryRepository.CheckSubCategoryAvaible(model.AccountSubCategoryId, users.CompanyId ?? 0, model.FromAccount, model.ToAccount);

                        if (subresdata != null)
                        {
                            return new OkObjectResult(new { error = true, message = "SubCategory Range" + model.FromAccount + "-" + model.ToAccount + " already assigned to " + subresdata.Name });
                        }

                        var  accountType = _accountTypeRepository.GetAll()
                                 .Where(sc => sc.AccountSubCategoryId == model.AccountSubCategoryId && sc.IsActive == true).FirstOrDefault();

                        if (accountType == null)
                        {
                            accountsubcatgry.CompanyId = users.CompanyId ?? 0; ;
                            accountsubcatgry.AccountCategoryId = model.AccountCategoryId;
                            accountsubcatgry.FromAccount = model.FromAccount;
                            accountsubcatgry.ToAccount = model.ToAccount;
                        }
                         
                        accountsubcatgry.EditedAt = DateTime.Now;
                        accountsubcatgry.EditedBy = users.UserId;
                        accountsubcatgry.Code = model.Code;
                        accountsubcatgry.Name = model.Name;
                        accountsubcatgry.IsActive = model.IsActive;
                        _accountSubCategoryRepository.Update(accountsubcatgry);

                        return new OkObjectResult(new { error = false, message = "Updated" });
                    }
                    else
                    {
                        return new OkObjectResult(new { error = false, message = "no user found" });
                    }
                }
                else
                {
                    return new OkObjectResult(new { error = false, message = "no data found" });

                }


            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });


            }

        }

        #endregion

        #region Account Type 

        public IActionResult AccountTypeView()
        {
            return View();
        }

        public JsonResult GetAccountType()
        {
            var res = _accountTypeRepository.GetAll();

            return Json(res);
        }

        public IActionResult AddAccountType(AccountType model)
        {
            try
            {
                if (Convert.ToInt16(model.ToAccount) < Convert.ToInt16(model.FromAccount))
                {
                    return new OkObjectResult(new { error = true, message = "to range should be great then from range." });
                }

                if (model.Code.Length != 4)
                {
                    return new OkObjectResult(new { error = true, message = "code length must 4 digit." });
                }

                var res = _accountTypeRepository.GetAll().Where(x => x.Name == model.Name || x.Code == model.Code).FirstOrDefault();

                if (res != null)
                {
                    return new OkObjectResult(new { error = true, message = "Name/Code already exist." });
                }

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {
                    if (users.CompanyId == null || users.CompanyId == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "CompanyId is required." });

                    }

                    var resdata = _accountTypeRepository.CheckAccount(model.AccountSubCategoryId, users.CompanyId ?? 0, model.FromAccount, model.ToAccount);

                    if (resdata == null)
                    {
                        return new OkObjectResult(new { error = true, message = "entered range" + model.FromAccount + "-" + model.ToAccount + " out of category range " });
                    }



                    var resdatasub = _accountTypeRepository.CheckType(users.CompanyId ?? 0, model.FromAccount, model.ToAccount);

                    if (resdatasub != null)
                    {
                        return new OkObjectResult(new { error = true, message = " range" + model.FromAccount + "-" + model.ToAccount + " already assigned to  " + resdatasub.Name });
                    }
                     
                    model.CreatedAt = DateTime.Now;
                    model.CreatedBy = users.UserId;
                    model.CompanyId = users.CompanyId ?? 0;
                    model.EditedBy = null;
                    model.EditedAt = null;
                    _accountTypeRepository.Add(model);

                    return new OkObjectResult(new { error = false, message = "Created" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no user  found" });
                }


            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }


        public IActionResult UpdateAccountType(AccountType model)
        {
            try
            {

                var accounttyperes = _accountTypeRepository.GetAll().Where(x => x.AccountTypeId == model.AccountTypeId).LastOrDefault();

                if(accounttyperes != null)
                {
                    if (Convert.ToInt16(model.ToAccount) < Convert.ToInt16(model.FromAccount))
                    {
                        return new OkObjectResult(new { error = true, message = "to range should be great then from range." });
                    }

                    if (model.Code.Length != 4)
                    {
                        return new OkObjectResult(new { error = true, message = "code length must 4 digit." });
                    }

                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();


                    if (users != null)
                    {

                        if (users.CompanyId == null || users.CompanyId == 0)
                        {
                            return new OkObjectResult(new { error = true, message = "CompanyId is required." });
                        }

                        var resdata = _accountTypeRepository.CheckAccount(model.AccountSubCategoryId, users.CompanyId ?? 0, model.FromAccount, model.ToAccount);

                        if (resdata == null)
                        {
                            return new OkObjectResult(new { error = true, message = "entered range" + model.FromAccount + "-" + model.ToAccount + " out of subcategory range " });
                        }

                        var subresdata = _accountTypeRepository.CheckAccountTypeAvaible(model.AccountTypeId, users.CompanyId ?? 0, model.FromAccount, model.ToAccount);

                        if (subresdata != null)
                        {
                            return new OkObjectResult(new { error = true, message = "Range" + model.FromAccount + "-" + model.ToAccount + " already assigned to " + subresdata.Name });
                        }


                        var chartOfAccount = _chartOfAccountRepository.GetAll().Where(sc => sc.AccountTypeId == accounttyperes.AccountTypeId && sc.Status == true).FirstOrDefault();

                        if (chartOfAccount == null)
                        {
                            accounttyperes.CompanyId = users.CompanyId ?? 0;
                            accounttyperes.AccountSubCategoryId =  model.AccountSubCategoryId;
                        }

                        accounttyperes.FromAccount = model.FromAccount;
                        accounttyperes.ToAccount = model.ToAccount;
                        accounttyperes.Code = model.Code;
                        accounttyperes.Name = model.Name;
                        accounttyperes.IsActive = model.IsActive;
                        accounttyperes.EditedAt = DateTime.Now;
                        accounttyperes.EditedBy = users.UserId; 
                        _accountTypeRepository.Update(accounttyperes);

                        return new OkObjectResult(new { error = false, message = "Updated" });
                    }
                    else
                    {
                        return new OkObjectResult(new { error = false, message = "no user found" });
                    }
                }
                else
                {
                    return new OkObjectResult(new { error = false, message = "no data found" });
                }

             
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });


            }

        }

        #endregion

        #region Voucher Service Type

        public IActionResult VoucherServiceTypeView()
        {
            return View();
        }

        public JsonResult GetVoucherServiceType()
        {
            var resdata = new List<VoucherServiceType>();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            if (users != null)
            {
                if (users.CompanyId != null || users.CompanyId != 0)
                {
                    resdata = _voucherServiceTypeRepository.GetAll().Where(x => x.CompanyId == users.CompanyId).ToList();

                    return Json(resdata);
                }

            }
            return Json(resdata);
 
        }

        public IActionResult AddVoucherServiceType(VoucherServiceType model)
        {
            try
            {
                if (model.Code.Length == 0)
                {
                    return new OkObjectResult(new { error = true, message = "code length must  > 0 digit." });
                }

                var res = _voucherServiceTypeRepository.GetAll().Where(x => x.Name == model.Name || x.Code == model.Code).FirstOrDefault();

                if (res != null)
                {
                    return new OkObjectResult(new { error = true, message = "Name/Code already exist." });
                }

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {
                    if (users.CompanyId == null || users.CompanyId == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "CompanyId is required." });

                    }
                    model.CompanyId = users.CompanyId ?? 0;
                    model.CreatedAt = DateTime.Now;
                    model.CreatedBy = users.UserId;
                    model.EditedBy = null;
                    model.EditedAt = null;
                    _voucherServiceTypeRepository.Add(model);

                    return new OkObjectResult(new { error = false, message = "Created" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no user  found" });
                }


            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }



        [HttpPost]
        public IActionResult UpdateVoucherServiceType(VoucherServiceType model)
        {
            try
            {
                if (model.Code.Length == 0)
                {
                    return new OkObjectResult(new { error = true, message = "code length must  > 0 digit." });
                }

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {

                    var res = _voucherServiceTypeRepository.CheckVoucherServiceType(model.Code, model.Name, model.VoucherServiceTypeId);

                    if (res != null)
                    {
                        return new OkObjectResult(new { error = true, message = " Name/Code already exist. in name " + res.Name + " code " + res.Code });

                    }

                    if (users.CompanyId == null || users.CompanyId == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "CompanyId is required." });

                    }
                    model.CompanyId = users.CompanyId ?? 0;
                    model.EditedAt = DateTime.Now;
                    model.EditedBy = users.UserId;
                    _voucherServiceTypeRepository.Update(model);

                    return new OkObjectResult(new { error = false, message = "Updated" });
                }
                else
                {
                    return new OkObjectResult(new { error = false, message = "no user found" });
                }
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });


            }

        }

        #endregion


        #region Customer

        public IActionResult CustomerView()
        {
            return View();
        }

        public JsonResult GetCustomer()
        {
            var res = _customerRepository.GetAll();

            return Json(res);
        }

        public JsonResult GetCustomersCode()
        {
            var resdata = new List<Customer>();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            if (users != null)
            {
                if (users.CompanyId != null || users.CompanyId != 0)
                {
                    resdata = _customerRepository.GetAll().Where(x => x.CompanyId == users.CompanyId).Select(x => new Customer { Name = x.Code + "-" + x.Name, CustomerId = x.CustomerId, ChartOfAccountId = x.ChartOfAccountId , Code = x.Code}).ToList();

                    return Json(resdata);
                }

            }
            return Json(resdata);

        }

        public JsonResult GetCustomersNameAndCode()
        {
            var resdata = new List<Customer>();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            if (users != null)
            {
                if (users.CompanyId != null || users.CompanyId != 0)
                {
                    resdata = _customerRepository.GetAll().Where(x => x.CompanyId == users.CompanyId).Select(x => new Customer { Name = x.Code + "-" + x.Name, CustomerId = x.CustomerId, ChartOfAccountId = x.ChartOfAccountId }).ToList();

                    return Json(resdata);
                }

            }
            return Json(resdata);
             
        }

        public IActionResult AddCustomer(Customer model)
        {
            try
            {

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {
                    if (users.CompanyId == null || users.CompanyId == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "company not avaible" });
                    }
                }

                var customerName = _customerRepository.GetAll()
                                          .Where(c => c.Name.ToLower() == model.Name.ToLower()
                                          || c.Code == model.Code)
                                          .FirstOrDefault();
                if(customerName != null)
                {
                    return new OkObjectResult(new { error = true, message = "Party Name/Code already exist." });

                }
                model.CompanyId = users.CompanyId ?? 0;
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = users.UserId;
                model.EditedAt = null;
                model.EditedBy = null;
                _customerRepository.Add(model);

                return new OkObjectResult(new { error = false, message = "Created" });

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }


        public IActionResult UpdateCustomer(Customer model)
        {
            try
            {
                var res = _customerRepository.GetAll().Where(x => x.CustomerId == model.CustomerId).LastOrDefault();
                if(res != null)
                {
                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                    if (users != null)
                    {
                        if (users.CompanyId == null || users.CompanyId == 0)
                        {
                            return new OkObjectResult(new { error = true, message = "company not avaible" });
                        }
                    }


                    var customer = _customerRepository.CheckCustomer(model.CustomerId, model.Code, model.Name);
                    if(customer != null)
                    {
                        return new OkObjectResult(new { error = true, message = "Party Name/Code already exist." });

                    }

                    res.Address1 = model.Address1;
                    res.Address2 = model.Address2;
                    res.ChartOfAccountId = model.ChartOfAccountId;
                    res.CityId = model.CityId;
                    res.CompanyId = users.CompanyId ?? 0;
                    res.EditedAt = DateTime.Now;
                    res.EditedBy = users.UserId;
                    res.CountryId = model.CountryId;
                    res.Email = model.Email;
                    res.IsActive = model.IsActive;
                    res.Name = model.Name;
                    res.NTN = model.NTN;
                    res.Phone1 = model.Phone1;
                    res.SalesTaxRegNumber = model.SalesTaxRegNumber;
                    res.ZipCode = model.ZipCode;

                    _customerRepository.Update(res);

                    return new OkObjectResult(new { error = false, message = "Updated" });

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no data found" });

                }
                
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });


            }

        }

        #endregion


        #region Voucher Type

        public IActionResult VoucherTypeView()
        {
            return View();
        }

        public JsonResult GetVoucherType()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();


            if (users != null)
            {
                if (users.CompanyId != null || users.CompanyId != 0)
                {
                    var res = _voucherTypeRepository.GetAll().Where(x => x.CompanyId == users.CompanyId).ToList();

                    return Json(res);

                }
            }

            return Json("");

        }

        public IActionResult AddVoucherType(VoucherType model)
        {
            try
            {

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {
                    if (users.CompanyId == null || users.CompanyId == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "company not avaible" });
                    }
                }


                var vouchertyperes = _voucherTypeRepository.GetAll()
                                         .Where(c => c.Name.ToLower() == model.Name.ToLower()
                                         || c.Code == model.Code)
                                         .FirstOrDefault();
                if (vouchertyperes != null)
                {
                    return new OkObjectResult(new { error = true, message = "Name/Code already exist." });

                }

                if(model.Code.Length != 2)
                {
                    return new OkObjectResult(new { error = true, message = "Code must have 2 charachers" });
                }

                model.CompanyId = users.CompanyId ?? 0;
                    model.CreatedAt = DateTime.Now;
                    model.CreatedBy = users.UserId;
                    model.EditedBy = null;
                    model.EditedAt = null;
                    _voucherTypeRepository.Add(model);

                    return new OkObjectResult(new { error = false, message = "Created" });
                

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }



        [HttpPost]
        public IActionResult UpdateVoucherType(VoucherType model)
        {
            var res = _voucherTypeRepository.GetAll().Where(x => x.VoucherTypeId == model.VoucherTypeId).LastOrDefault();

            if (res != null)
            {

                try
                {


                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                    if (users != null)
                    {
                        if (users.CompanyId == null || users.CompanyId == 0)
                        {
                            return new OkObjectResult(new { error = true, message = "company not avaible" });
                        }
                    }

                   

                    var checkvoucer = _voucherTypeRepository.GetAll()
                                              .Where(sc => sc.VoucherTypeId != model.VoucherTypeId && sc.CompanyId == users.CompanyId 
                                                    && sc.Name == model.Name || sc.Code == model.Code).FirstOrDefault();

                    if(checkvoucer != null)
                    {
                        return new OkObjectResult(new { error = true, message = "Name/Code already exist." });

                    }
                    if (model.Code.Length != 2)
                    {
                        return new OkObjectResult(new { error = true, message = "Code must have 2 charachers" });
                    }



                    model.EditedAt = DateTime.Now;
                    model.EditedBy = users.UserId;
                    _voucherTypeRepository.Update(model);

                    return new OkObjectResult(new { error = false, message = "Updated" });


                }
                catch (Exception e)
                {
                    return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });


                }
            }
            else
            {
                return new OkObjectResult(new { error = true, message = "no data found" });

            }


        }

        #endregion

        #region Allow BackDate Transaction

        public IActionResult AllowBackDateTransaction()
        {
            return View();
        }

        public JsonResult GetAllowBackDateTransaction()
        {
            var res = _allowBackDateTransactionRepository.GetAll();

            return Json(res);
        }

        public IActionResult AddBackDateTransaction(string name )
        {
            try
            {
                var res = _allowBackDateTransactionRepository.GetAll().Where(x => x.UserName == name).LastOrDefault();

                if (res != null)
                {
                    return new OkObjectResult(new { error = true, message = "Already avaible" });
                }
                else
                {
                    var resdata = new AllowBackDateTransaction()
                    {
                        UserName = name
                    };

                    _allowBackDateTransactionRepository.Add(resdata);

                    return new OkObjectResult(new { error = false, message = "Save" });


                }
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });

            }



        }

        public IActionResult DeleteBackDateTransaction(long id)
        {
            try
            {

                var res = _allowBackDateTransactionRepository.GetAll().Where(x => x.AllowBackDateTransactionId == id).LastOrDefault();

                if(res != null)
                {
                    _allowBackDateTransactionRepository.Delete(res);
                    return new OkObjectResult(new { error = false, message = "Delete" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no data found" });
                }
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }

        #endregion

        #region FinancialYear

        public IActionResult FinancialYearView()
        {
            return View();
        }

        public JsonResult GetFinancialYear()
        {
            var resdata = new List<FinancialYear>();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            if (users != null)
            {
                if (users.CompanyId != null || users.CompanyId != 0)
                {
                    resdata = _financialYearRepository.GetAll().Where(x => x.CompanyId == users.CompanyId).ToList();

                    return Json(resdata);
                }

            }
            return Json(resdata);


        }

        public IActionResult AddFinancialYear(FinancialYear model)
        {
            try
            {
                 

                var res = _financialYearRepository.GetAll().Where(x => x.Year == model.Year ).LastOrDefault();

                if (res != null)
                {
                    return new OkObjectResult(new { error = true, message = "FinancialYear already exist." });

                } 

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {
                    model.CreatedAt = DateTime.Now;
                    model.CreatedBy = users.UserId;
                    model.CompanyId = users.CompanyId ?? 0;
                    model.EditedBy = null;
                    model.EditedAt = null;
                    _financialYearRepository.Add(model);

                    return new OkObjectResult(new { error = false, message = "Created" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no user  found" });
                }


            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }


        }


        public IActionResult UpdateFinancialYear(FinancialYear model)
        {
            try
            { 
                var res = _financialYearRepository.GetAll().Where(x=> x.Year == model.Year &&  x.FinancialYearId != model.FinancialYearId).LastOrDefault();

                if (res != null)
                {
                    return new OkObjectResult(new { error = true, message = "Financial Year already exist." });

                }

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {

                    model.EditedAt = DateTime.Now;
                    model.EditedBy = users.UserId;
                    model.CompanyId = users.CompanyId ?? 0;
                    _financialYearRepository.Update(model);

                    return new OkObjectResult(new { error = false, message = "Updated" });
                }
                else
                {
                    return new OkObjectResult(new { error = false, message = "no user found" });
                }
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });


            }

        }

        #endregion
    }
}
