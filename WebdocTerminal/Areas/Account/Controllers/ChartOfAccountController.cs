using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Account.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Account.Controllers
{
    public class ChartOfAccountController : ParentController
    {
        public IChartOfAccountRepository _chartOfAccountRepository;
        public IUsersRepository _usersRepository;
        public IAccountTypeRepository _accountTypeRepository;
        public IVoucherServiceTypeDetailRepository _voucherServiceTypeDetailRepository;
        public IAccountBalanceRepository _accountBalanceRepository;
        public ChartOfAccountController(IChartOfAccountRepository chartOfAccountRepository,
            IUsersRepository usersRepository,
            IAccountTypeRepository accountTypeRepository,
            IVoucherServiceTypeDetailRepository voucherServiceTypeDetailRepository,
            IAccountBalanceRepository accountBalanceRepository)
        {
            _chartOfAccountRepository = chartOfAccountRepository;
            _usersRepository = usersRepository;
            _accountTypeRepository = accountTypeRepository;
            _voucherServiceTypeDetailRepository = voucherServiceTypeDetailRepository;
            _accountBalanceRepository = accountBalanceRepository;
        }


        #region Char Of Account


        public IActionResult ChartOfAccountView()
        {
            return View();
        }

        public JsonResult GetChartOfAccount()
        {
            var resdata = new List<chartOfAccountViewModel>();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            if (users != null)
            {
                if (users.CompanyId != null || users.CompanyId != 0)
                {
                    var res = _chartOfAccountRepository.GetChartOfAccounts(users.CompanyId ?? 0).ToList();

                    return Json(res);
                }
                 
            }
            return Json(resdata);
        }


        public JsonResult GetChartOfAccountNameAndCode()
        {
            var consignees = _chartOfAccountRepository.GetAll().Select(x => new ChartOfAccount { AccountName = x.AccCode + "-" + x.AccountName, ChartOfAccountId = x.ChartOfAccountId}).ToList();

            return Json(consignees);
        }

        public JsonResult GetChartOfAccountCode()
        {
            var consignees = _chartOfAccountRepository.GetAll().Select(x => new ChartOfAccount { AccCode = x.AccCode , ChartOfAccountId = x.ChartOfAccountId }).ToList();

            return Json(consignees);
        }
        public IActionResult AddChartOfAccount(chartOfAccountViewModel model)
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


                if (model.AccCode.Length != 4)
                {
                    return new OkObjectResult(new { error = true, message = "code length must  = 4 digit." });
                }


                var rescoa = _chartOfAccountRepository.GetAll()
                             .Where(t =>
                             (t.AccountName.ToUpper() == model.AccountName.ToLower() || t.AccCode == model.AccCode)   //new Line add code add input
                             && t.CompanyId == users.CompanyId
                             && t.Status == true
                             ).FirstOrDefault();
                if (rescoa != null)
                {
                    return new OkObjectResult(new { error = true, message = "account Name/Code already exist." });

                }

                var accountType = _accountTypeRepository.GetAll()
                              .Where(t => t.AccountTypeId == model.AccountTypeId
                              && t.CompanyId == users.CompanyId
                              && t.IsActive == true
                              ).FirstOrDefault();
                if (accountType == null)
                {
                    return new OkObjectResult(new { error = true, message = "account Type not exist!" });

                }

                //if (Enumerable.Range(Convert.ToInt16(accountType.FromAccount), Convert.ToInt16(accountType.ToAccount)).Contains(Convert.ToInt16(model.AccCode)))  //true

                if (Convert.ToInt16(model.AccCode) < Convert.ToInt16(accountType.FromAccount) || Convert.ToInt16(model.AccCode) > Convert.ToInt16(accountType.ToAccount))
                {
                    return new OkObjectResult(new { error = true, message = "Account code out of account type range" });
                }

                var res = new ChartOfAccount()
                {
                    AccCode = model.AccCode,
                    AccountName = model.AccountName,
                    AccountTypeId = model.AccountTypeId,
                    AccPCode = "",
                    CompanyId = users.CompanyId ?? 0,
                    CreatedAt = DateTime.Now,
                    CreatedBy = users.UserId,
                    EditedAt = null,
                    EditedBy = null,
                    Status = model.Status,
                     
                };

                _chartOfAccountRepository.Add(res);

                var vd = new VoucherServiceTypeDetail()
                {
                    ChartOfAccountId = res.ChartOfAccountId,
                    VoucherServiceTypeId = model.VoucherServiceTypeId
                };

                _voucherServiceTypeDetailRepository.Add(vd);

                return new OkObjectResult(new { error = false, message = "Created" });


            }
            catch (Exception e)
            {
                 
               return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message+ "please create again" });

            }



        }


        public IActionResult UpdateChartOfAccount(chartOfAccountViewModel model)
        {
            try
            {
                var res = _chartOfAccountRepository.GetChartOfAccountbyId(model.ChartOfAccountId);

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


                    if (model.AccCode.Length != 4)
                    {
                        return new OkObjectResult(new { error = true, message = "code length must  = 4 digit." });
                    }

                    var chartofAccountResult = _chartOfAccountRepository.chartofAccountResults(users.CompanyId ?? 0, model.AccountName, model.AccCode, model.ChartOfAccountId);

                    if(chartofAccountResult  != null)
                    {
                        return new OkObjectResult(new { error = true, message = "Account Name/Code already exist" });
                    }
                    
                    var CheckAccountTypeRes = _chartOfAccountRepository.CheckAccountType( model.AccountTypeId,  users.CompanyId ?? 0);

                    if(CheckAccountTypeRes == null)
                    {
                        return new OkObjectResult(new { error = true, message = "account Type not exist!" });
                    }

                    if (Convert.ToInt16(model.AccCode) < Convert.ToInt16(CheckAccountTypeRes.FromAccount) || Convert.ToInt16(model.AccCode) > Convert.ToInt16(CheckAccountTypeRes.ToAccount))
                    {
                        return new OkObjectResult(new { error = true, message = "Account code out of account type range" });
                    }

                    res.AccountTypeId = model.AccountTypeId;
                    res.AccCode = model.AccCode;
                    res.AccountName = model.AccountName;
                    res.Status = model.Status;
                    res.EditedAt = DateTime.Now;
                    res.EditedBy = users.UserId;

                    var voucherServiceTypeDetail = _voucherServiceTypeDetailRepository.GetAll()
                    .Where(t => t.ChartOfAccountId == model.ChartOfAccountId).FirstOrDefault();
                    if (voucherServiceTypeDetail != null)
                    {

                        voucherServiceTypeDetail.VoucherServiceTypeId = model.VoucherServiceTypeId;
                        voucherServiceTypeDetail.ChartOfAccountId = model.ChartOfAccountId;

                        _voucherServiceTypeDetailRepository.Update(voucherServiceTypeDetail); 
                    }
                    else
                    {
                        var vd = new VoucherServiceTypeDetail()
                        {
                            ChartOfAccountId = model.ChartOfAccountId,
                            VoucherServiceTypeId = model.VoucherServiceTypeId
                        };

                        _voucherServiceTypeDetailRepository.Add(vd);
                    }

                    _chartOfAccountRepository.Update(res);

                    return new OkObjectResult(new { error = false, message = "Updated" });


                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no data found" });

                }
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message+"please create again" });
            }

        }
        #endregion




        #region Account Balance


        public IActionResult AccountBalanceView()
        {
            return View();
        }

        public JsonResult GetAccountBalance()
        {
            var resdata = new List<chartOfAccountViewModel>();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            if (users != null)
            {
                if (users.CompanyId != null || users.CompanyId != 0)
                {
                    var res = _accountBalanceRepository.GetAll().Where(x=> x.CompanyId == users.CompanyId ).ToList();

                    return Json(res);
                }

            }
            return Json(resdata);
        }

      

        public IActionResult AddAccountBalance(AccountBalance model)
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


               if(model.CustomerId != null)
                {
                    var rescoa = _accountBalanceRepository.GetAccountBalanceforcustomer(model.CustomerId ?? 0 , users.CompanyId ?? 0);
                    if (rescoa != null)
                    {
                        return new OkObjectResult(new { error = true, message = "Party:  Balance already exist." });
                    }
                }
                else
                {
                    var rescoa = _accountBalanceRepository.CheckAccountBalance(model.ChartOfAccountId, users.CompanyId ?? 0);

                    if (rescoa != null)
                    {
                        return new OkObjectResult(new { error = true, message = "Account  already exist." });
                    }
                }
                

                var res = new AccountBalance()
                {
                    ChartOfAccountId = model.ChartOfAccountId,
                    Credit = model.Credit,
                    Debit = model.Debit,
                    CustomerId = model.CustomerId,
                    TranDate = model.TranDate,
                    CompanyId = users.CompanyId ?? 0,
                    CreatedAt = DateTime.Now,
                    CreatedBy = users.UserId,
                    EditedAt = null,
                    EditedBy = null,
                    IsActive = model.IsActive,

                };

                _accountBalanceRepository.Add(res);
  
                return new OkObjectResult(new { error = false, message = "Created" });


            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message+"please create again" });

            }



        }


        public IActionResult UpdateAccountBalance(AccountBalance model)
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


                var res = _accountBalanceRepository.GetAccountBalanceById(model.AccountBalanceId , users.CompanyId ?? 0);

                if (res != null)
                {

                    if(model.CustomerId != null)
                    {
                        var CheckCustomer = _accountBalanceRepository.CheckAccountBalanceByCustomerId(model.AccountBalanceId, model.CustomerId ?? 0, users.CompanyId ?? 0);

                        return new OkObjectResult(new { error = true, message = "Party:  Balance already exist." });

                    }
                    else
                    {
                        var CheckChartOfAccount = _accountBalanceRepository.CheckAccountBalanceByChartOfAccountId(model.AccountBalanceId, model.ChartOfAccountId , users.CompanyId ?? 0);

                        return new OkObjectResult(new { error = true, message = "Account already exist." });
                    }


                    res.ChartOfAccountId = model.ChartOfAccountId;
                    res.Credit = model.Credit;
                    res.Debit = model.Debit;
                    res.CustomerId = model.CustomerId;
                    res.TranDate = model.TranDate;
                    res.IsActive = model.IsActive;
                    res.EditedAt = DateTime.Now;
                    res.EditedBy = users.UserId;
                     

                    _accountBalanceRepository.Update(res);

                    return new OkObjectResult(new { error = false, message = "Updated" });


                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no data found" });

                }
            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message+ "please create again" });
            }

        }
        #endregion



    }
}
