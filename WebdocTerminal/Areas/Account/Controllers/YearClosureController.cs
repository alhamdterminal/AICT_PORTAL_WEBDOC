using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Account.Controllers
{
    public class YearClosureController : ParentController
    {

        public IVoucherTypeRepository _voucherTypeRepository;
        public IUsersRepository _usersRepository;
        public IVoucherRepository _voucherRepository;
        public IVoucherDetailRepository _voucherDetailRepository;
        public IAllowBackDateTransactionRepository _allowBackDateTransactionRepository;
        private UserManager<IdentityUser> _userManager;
        private IFinancialYearRepository  _financialYearRepository;
        private IDepartmentRepository _departmentRepository;
        private IFinancialYearClosureRepository _financialYearClosureRepository;
        private IVoucherServiceTypeRepository _voucherServiceTypeRepository;
        public YearClosureController(IVoucherTypeRepository voucherTypeRepository,
                                IUsersRepository usersRepository,
                                IVoucherRepository voucherRepository,
                                IVoucherDetailRepository voucherDetailRepository,
                                IAllowBackDateTransactionRepository allowBackDateTransactionRepository,
                                UserManager<IdentityUser> userManager,
                                IFinancialYearRepository financialYearRepository,
                                IDepartmentRepository departmentRepository,
                                IFinancialYearClosureRepository financialYearClosureRepository,
                                IVoucherServiceTypeRepository voucherServiceTypeRepository)
        {
            _voucherTypeRepository = voucherTypeRepository;
            _usersRepository = usersRepository;
            _voucherRepository = voucherRepository;
            _voucherDetailRepository = voucherDetailRepository;
            _allowBackDateTransactionRepository = allowBackDateTransactionRepository;
            _userManager = userManager;
            _financialYearRepository = financialYearRepository;
            _departmentRepository = departmentRepository;
            _financialYearClosureRepository = financialYearClosureRepository;
            _voucherServiceTypeRepository = voucherServiceTypeRepository;
        }


        public IActionResult YearClosureView()
        {

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            ViewData["VoucherType"] = _voucherTypeRepository.GetAll().Where(x => x.CompanyId == users.CompanyId)
                                            .Select(v => new SelectListItem { Text = v.Name, Value = v.Code.ToString() });

            ViewData["FinancialYear"] = _financialYearRepository.GetPenddingFinancialYears(users.CompanyId ?? 0)
                                            .Select(v => new SelectListItem { Text = v.Year, Value = v.FinancialYearId.ToString() });

            ViewData["Department"] = _departmentRepository.GetAll().Where(x => x.CompanyId == users.CompanyId)
                                            .Select(v => new SelectListItem { Text = v.DepartmentName, Value = v.DepartmentCode.ToString() });

            return View();
        }

        public JsonResult GetClosureDetail(string year)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            var res = _financialYearClosureRepository.GetClosureDetail(users.CompanyId ?? 0 , year).ToList();

            return Json(res);
        }


        public IActionResult PostClosureYear(string year, string voucherType , long department)
        {

              
            var Errorres = new List<Voucher>();

            var voucherTyperes = _voucherTypeRepository.GetAll().Where(x => x.Code == voucherType).LastOrDefault();

            if(voucherTyperes  == null)
            {
                var reseror = new Voucher()
                {
                    VoucherNo = "",
                    Narration = "No VoucherType found",
                    VoucherId = 0,
                };

                Errorres.Add(reseror);
            }

            DateTime lastDay = new DateTime(Convert.ToInt32( year), 12, 31);


            var voucherlist = new List<Voucher>();

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            var fy = _financialYearRepository.GetAll().Where(x => x.Year == year).LastOrDefault();
            if(fy != null)
            {
                var fyc = _financialYearClosureRepository.GetAll().Where(x => x.FinancialYearId == fy.FinancialYearId).LastOrDefault();
                if(fyc != null)
                {
                    var reseror = new Voucher()
                    {
                        VoucherNo = "",
                        Narration = "Financial Year   " + year + " Already Closed ",
                        VoucherId = 0,
                    };

                    Errorres.Add(reseror);
                }

            }

            if (fy  == null)
            {
                var reseror = new Voucher()
                {
                    VoucherNo = "",
                    Narration = "Financial Year " + year + " Not open in setup",
                    VoucherId = 0,
                };

                Errorres.Add(reseror);
            }
            #region validation
            if (users != null)
            {
                if (users.CompanyId == null || users.CompanyId == 0)
                {
                    var reseror = new Voucher()
                    {
                        VoucherNo = "",
                        Narration = "company not avaible",
                        VoucherId = 0,
                    };

                    Errorres.Add(reseror);
                }
            }
            else
            {
                var reseror = new Voucher()
                {
                    VoucherNo = "",
                    Narration = "user not avalible",
                    VoucherId = 0,
                };

                Errorres.Add(reseror);
            }


            var res = _voucherRepository.GetAll().Where(x => x.Year == year && x.CompanyId == users.CompanyId && x.Status == "InProgress").ToList();

            foreach (var item in res)
            {
                var reseror = new Voucher()
                {
                    VoucherNo = "",
                    Narration = "Please Mark (Post Or Canceled)  Voucher No " + item.VoucherNo,
                    VoucherId = 0,
                };

                Errorres.Add(reseror);
            }

            var financialYear = _financialYearRepository.GetAll().Where(x => x.Year == year && x.CompanyId == users.CompanyId).LastOrDefault();
            if (financialYear != null)
            {
                var financialYearclosure = _financialYearClosureRepository.GetAll().Where(x => x.FinancialYearId == financialYear.FinancialYearId && x.CompanyId == users.CompanyId).LastOrDefault();

                if (financialYearclosure != null)
                {
                    var reseror = new Voucher()
                    {
                        VoucherNo = "",
                        Narration = "This Financial Year Already Closed ",
                        VoucherId = 0,
                    };
                    Errorres.Add(reseror);

                }

            }



            var resdata = _financialYearClosureRepository.GetClosureDetail(users.CompanyId ?? 0, year).ToList();

            if (resdata.Count() <= 0)
            {
                var reseror = new Voucher()
                {
                    VoucherNo = "",
                    Narration = "No Record Found For  Financial Year " + year + " Closed ",
                    VoucherId = 0,
                };
                Errorres.Add(reseror);
            }
             

            var resacc = resdata.Where(x => x.ChartOfAccountId == 0).ToList();

            if (resacc.Count() > 0)
            {
                var reseror = new Voucher()
                {
                    VoucherNo = "",
                    Narration = "Few COA are not matched please check closure sheet ",
                    VoucherId = 0,
                };
                Errorres.Add(reseror);
            }




            if (resdata.Count() > 0)
            {
                 
                var types = resdata.Select(x => x.ServiceType).ToList().GroupBy(x => x);
                  
                if (types.Count() > 0)
                {
                     
                    foreach (var item in types)
                    {
                        var VoucherServiceTyperes = _voucherServiceTypeRepository.GetAll().Where(x => x.CompanyId == users.CompanyId && x.Code == (item.Key == "R" || item.Key == "O" ? "I" : item.Key) ).LastOrDefault();

                        if(VoucherServiceTyperes != null)
                        { 
                            resdata.Where(x => x.ServiceType == item.Key).ToList().ForEach(x => x.VoucherServiceTypeId = VoucherServiceTyperes.VoucherServiceTypeId);

                        }
                        else
                        {
                            var reseror = new Voucher()
                            {
                                VoucherNo = "",
                                Narration = "No Voucher Service Type are not matched please check closure sheet " + item.Key,
                                VoucherId = 0,
                            };
                            Errorres.Add(reseror);
                        }
                    }
                    
                }
                else
                {
                    var reseror = new Voucher()
                    {
                        VoucherNo = "",
                        Narration = "Few Voucher Service Type are not matched please check closure sheet",
                        VoucherId = 0,
                    };
                    Errorres.Add(reseror);
                }

            }

            if (Errorres.Count() > 0)
            {
                return new OkObjectResult(new { error = true, message = Errorres });
            }
            #endregion

            var resno = _voucherRepository.GetVoucherNo(users.CompanyId ?? 0, voucherTyperes.VoucherTypeId, year, "12");

            if (resno != null && resno != "")
            {
                var fyclosure = new FinancialYearClosure()
                {
                    CompanyId = users.CompanyId ?? 0,
                    CreatedAt = DateTime.Now,
                    FinancialYearId = fy.FinancialYearId,
                    CreatedBy = users.UserId

                };

                var voucherRes = new Voucher()
                {
                    CompanyId = users.CompanyId ?? 0,
                    CreatedAt = DateTime.Now,
                    CreatedBy = users.UserId,
                    EditedAt = null,
                    EditedBy = null,
                    FileName = "",
                    Month = "12",
                    Narration = "",
                    Status = "Posted",
                    VerifyById = null,
                    VerifyDate = null,
                    VoucherNo = resno,
                    VoucherTypeId = voucherTyperes.VoucherTypeId,
                    Year = year,
                    VoucherDate = lastDay,
                };
                var voucerdetaillist = new List<VoucherDetail>();

                foreach (var item in resdata)
                {
                     
                    var VoucherDetailres = new VoucherDetail()
                    {
                        ChartOfAccountId = item.ChartOfAccountId,
                        ChequeOrReference = "",
                        CompanyId = users.CompanyId ?? 0,
                        CreatedAt = DateTime.Now,
                        CreatedBy = users.UserId,
                        EditedAt = null,
                        EditedBy = null,
                        Debit = item.Balance >= 0 ? 0 : Math.Abs( item.Balance ?? 0),
                        CustomerId = null,
                        Credit = item.Balance >= 0 ? item.Balance  : 0,
                        DepartmentId = department,
                        LineNumber = 0 ,
                        Narration = item.Narration,
                        TranDate = lastDay,
                        VoucherServiceTypeId = item.VoucherServiceTypeId,
                        VoucherTypeId = voucherTyperes.VoucherTypeId,

                    };

                    voucerdetaillist.Add(VoucherDetailres);
                }
 
                _voucherRepository.Add(voucherRes);

                voucerdetaillist.ForEach(x=> x.VoucherId = voucherRes.VoucherId);
                _voucherDetailRepository.AddRange(voucerdetaillist);

                _financialYearClosureRepository.Add(fyclosure);

                return new OkObjectResult(new { error = false, message = voucherRes });
            }
            else
            {
                return new OkObjectResult(new { error = true, message = "no code found" });
            }
             

        }


    }
}
