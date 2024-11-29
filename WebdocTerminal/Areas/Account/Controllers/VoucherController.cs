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
    public class VoucherController : ParentController
    {
        public IVoucherTypeRepository _voucherTypeRepository;
        public IUsersRepository _usersRepository;
        public IVoucherRepository  _voucherRepository;
        public IVoucherDetailRepository  _voucherDetailRepository;
        public IAllowBackDateTransactionRepository _allowBackDateTransactionRepository;
        private UserManager<IdentityUser> _userManager;
        private IFinancialYearRepository _financialYearRepository;
        private IFinancialYearClosureRepository _financialYearClosureRepository;
        public VoucherController(IVoucherTypeRepository voucherTypeRepository,
                                IUsersRepository usersRepository,
                                IVoucherRepository voucherRepository,
                                IVoucherDetailRepository voucherDetailRepository,
                                IAllowBackDateTransactionRepository allowBackDateTransactionRepository,
                                UserManager<IdentityUser> userManager,
                                IFinancialYearRepository financialYearRepository,
                                IFinancialYearClosureRepository financialYearClosureRepository)
        {
            _voucherTypeRepository = voucherTypeRepository;
            _usersRepository = usersRepository;
            _voucherRepository = voucherRepository;
            _voucherDetailRepository = voucherDetailRepository;
            _allowBackDateTransactionRepository = allowBackDateTransactionRepository;
            _userManager = userManager;
            _financialYearRepository = financialYearRepository;
            _financialYearClosureRepository = financialYearClosureRepository;
        }


        #region Voucher


        public IActionResult VoucherView()
        {

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();
             
            ViewData["VoucherType"] = _voucherTypeRepository.GetAll().Where(x=> x.CompanyId == users.CompanyId)
                                            .Select(v => new SelectListItem { Text = v.Name, Value = v.VoucherTypeId.ToString() });

            return View();
        }


        public JsonResult GetVoucherDetail(long voucherid)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            var res = _voucherRepository.GetVoucherDetail(users.CompanyId ?? 0, voucherid).ToList();

            return Json(res);
        }

        public IActionResult AddVoucher(long VouchertypeId , DateTime date ,  VoucherDetail model)
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
                var res =  _voucherRepository.GetVoucherNo(users.CompanyId ?? 0, VouchertypeId, date.ToString("yyyy"), date.ToString("MM"));

                if (res != null && res != "")
                {
                    var voucherRes = new Voucher()
                    {
                        CompanyId = users.CompanyId ?? 0,
                        CreatedAt = DateTime.Now,
                        CreatedBy = users.UserId,
                        EditedAt = null,
                        EditedBy = null,
                        FileName = "",
                        Month = date.ToString("MM"),
                        Narration = "",
                        Status = "InProgress",
                        VerifyById = null,
                        VerifyDate = null,
                        VoucherNo = res,
                        VoucherTypeId = VouchertypeId,
                        Year = date.ToString("yyyy"),
                        VoucherDate = date,
                    };

                    var VoucherDetailres = new VoucherDetail()
                    {
                        ChartOfAccountId = model.ChartOfAccountId,
                        ChequeOrReference = model.ChequeOrReference,
                        CompanyId = users.CompanyId ?? 0,
                        CreatedAt = DateTime.Now,
                        CreatedBy = users.UserId,
                        EditedAt = null,
                        EditedBy = null,
                        Credit = model.Credit,
                        CustomerId = model.CustomerId,
                        Debit = model.Debit,
                        DepartmentId = model.DepartmentId,
                        LineNumber = model.LineNumber,
                        Narration = model.Narration,
                        TranDate = date,
                        VoucherServiceTypeId = model.VoucherServiceTypeId,
                        VoucherTypeId = VouchertypeId,
                        
                    };

                    _voucherRepository.Add(voucherRes);

                    VoucherDetailres.VoucherId = voucherRes.VoucherId;
                    _voucherDetailRepository.Add(VoucherDetailres);

                    return new OkObjectResult(new { error = false, message = voucherRes });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no code found" });
                }


            }
            catch (Exception e)
            { 
                return new OkObjectResult(new { error = true, message = e.InnerException != null ? e.InnerException.Message : e.Message });
            }

        }


        public IActionResult AddVoucherList(long VouchertypeId, DateTime date, List<VoucherDetail> model)
        {
            try
            {
               


                var voucherdetaillist = new List<VoucherDetail>();
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {
                    if (users.CompanyId == null || users.CompanyId == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "company not avaible" });
                    }
                }

                var financialYear = _financialYearRepository.GetAll().Where(x => x.Year == date.ToString("yyyy") && x.CompanyId == users.CompanyId).LastOrDefault();

                if (financialYear != null)
                {
                    var financialYearclosure = _financialYearClosureRepository.GetAll().Where(x => x.FinancialYearId == financialYear.FinancialYearId && x.CompanyId == users.CompanyId).LastOrDefault();

                    if (financialYearclosure != null)
                    {
                        return new OkObjectResult(new { error = true, message = "Financial Year Closed Entry Can't be Performed" });

                    }

                }

                if (Convert.ToDateTime(date.Date.ToString("MM/dd/yyyy")) < Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy")))
                {
                    var resforallowbackdate = _allowBackDateTransactionRepository.GetAll().Where(x => x.UserName == User.Identity.Name).LastOrDefault();
                    
                    if(resforallowbackdate == null)
                    {
                        return new OkObjectResult(new { error = true, message = "you'r not allow for back date voucher" });
                    }
                }

                if (model.Count() <= 0)
                {
                    return new OkObjectResult(new { error = true, message = "please add voucher detail" });
                }

                if ((model.Sum(x => x.Debit) - model.Sum(x => x.Credit)) != 0)
                {
                    return new OkObjectResult(new { error = true, message = "Debit and Credit Side Are Not Equal's" });
                }

                var res = _voucherRepository.GetVoucherNo(users.CompanyId ?? 0, VouchertypeId, date.ToString("yyyy"), date.ToString("MM"));

                if (res != null && res != "")
                {
                    var voucherRes = new Voucher()
                    {
                        CompanyId = users.CompanyId ?? 0,
                        CreatedAt = DateTime.Now,
                        CreatedBy = users.UserId,
                        EditedAt = null,
                        EditedBy = null,
                        FileName = "",
                        Month = date.ToString("MM"),
                        Narration = "",
                        Status = "InProgress",
                        VerifyById = null,
                        VerifyDate = null,
                        VoucherNo = res,
                        VoucherTypeId = VouchertypeId,
                        Year = date.ToString("yyyy"),
                        VoucherDate = date,
                    };

                    foreach (var item in model)
                    {
                        var VoucherDetailres = new VoucherDetail()
                        {
                            ChartOfAccountId = item.ChartOfAccountId,
                            ChequeOrReference = item.ChequeOrReference,
                            CompanyId = users.CompanyId ?? 0,
                            CreatedAt = DateTime.Now,
                            CreatedBy = users.UserId,
                            EditedAt = null,
                            EditedBy = null,
                            Credit = item.Credit,
                            CustomerId = item.CustomerId,
                            Debit = item.Debit,
                            DepartmentId = item.DepartmentId,
                            LineNumber = item.LineNumber,
                            Narration = item.Narration,
                            TranDate = date,
                            VoucherServiceTypeId = item.VoucherServiceTypeId,
                            VoucherTypeId = VouchertypeId,

                        };
                        voucherdetaillist.Add(VoucherDetailres);
                    }
                    

                    _voucherRepository.Add(voucherRes);
                    voucherdetaillist.ForEach(x => x.VoucherId = voucherRes.VoucherId);
                    _voucherDetailRepository.AddRange(voucherdetaillist);

                    return new OkObjectResult(new { error = false, message = voucherRes });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no code found" });
                }


            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException != null ? e.InnerException.Message : e.Message });
            }

        }


        public IActionResult AddVoucherDetailOnly(long VoucherId  , VoucherDetail model)
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

                var res = _voucherRepository.GetVoucherById(users.CompanyId ?? 0, VoucherId);
                if(res != null)
                {
                    if (res.Status != "InProgress")
                    {
                        return new OkObjectResult(new { error = true, message = "can't add due to vocher is posted" });
                    }

                    var VoucherDetailres = new VoucherDetail()
                    {
                        ChartOfAccountId = model.ChartOfAccountId,
                        ChequeOrReference = model.ChequeOrReference,
                        CompanyId = users.CompanyId ?? 0,
                        CreatedAt = DateTime.Now,
                        CreatedBy = users.UserId,
                        EditedAt = null,
                        EditedBy = null,
                        Credit = model.Credit,
                        CustomerId = model.CustomerId,
                        Debit = model.Debit,
                        DepartmentId = model.DepartmentId,
                        LineNumber = model.LineNumber,
                        Narration = model.Narration,
                        TranDate = res.VoucherDate,
                        VoucherServiceTypeId = model.VoucherServiceTypeId,
                        VoucherTypeId = res.VoucherTypeId,
                        VoucherId = res.VoucherId,
                        

                    };

                    _voucherDetailRepository.Add(VoucherDetailres);

                    return new OkObjectResult(new { error = false, message = "Created" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no data found" });

                } 

            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException != null ? e.InnerException.Message : e.Message });
            }

        }
        public IActionResult UpdateVoucherDetail(VoucherDetail model)
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

                var res = _voucherRepository.GetVoucherById(users.CompanyId ?? 0, model.VoucherId);
                if(res != null)
                {
                    if(res.Status != "InProgress")
                    {
                        return new OkObjectResult(new { error = true, message = "can't update due to vocher is posted" });
                    }

                    var resdata = _voucherRepository.GetVoucherDetailById(users.CompanyId ?? 0, model.VoucherDetailId);

                    if(resdata != null)
                    {
                        resdata.ChartOfAccountId = model.ChartOfAccountId;
                        resdata.ChequeOrReference = model.ChequeOrReference;
                        resdata.EditedAt = DateTime.Now;
                        resdata.EditedBy = users.UserId;
                        resdata.Credit = model.Credit;
                        resdata.CustomerId = model.CustomerId;
                        resdata.Debit = model.Debit;
                        resdata.DepartmentId = model.DepartmentId;
                        resdata.LineNumber = model.LineNumber;
                        resdata.Narration = model.Narration;
                        resdata.VoucherServiceTypeId = model.VoucherServiceTypeId;
                       
                        _voucherDetailRepository.Update(resdata);

                        return new OkObjectResult(new { error = false, message = "Updated" });
                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "no data found" });

                    } 

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no data found" });

                }




            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException != null ? e.InnerException.Message : e.Message });
            }

        }


        public IActionResult DeleteVoucherDetail( VoucherDetail model)
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

                var res = _voucherRepository.GetVoucherById(users.CompanyId ?? 0, model.VoucherId);
                if (res != null)
                {
                    if (res.Status != "InProgress")
                    {
                        return new OkObjectResult(new { error = true, message = "can't delete due to vocher is posted" });
                    }

                    var resdata = _voucherRepository.GetVoucherDetailById(users.CompanyId ?? 0, model.VoucherDetailId);

                    if (resdata != null)
                    {  
                        _voucherDetailRepository.Delete(resdata);

                        return new OkObjectResult(new { error = false, message = "Deleted" });
                         
                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "no data found" });

                    }

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "no data found" });

                }




            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, message = e.InnerException != null ? e.InnerException.Message : e.Message });
            }

        }


        public IActionResult VoucherListView()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();


            ViewData["Users"] = _usersRepository.GetFinnaceUsers().Where(x => x.CompanyId == users.CompanyId)
                                         .Select(v => new SelectListItem { Text = v.FirstName +"-"+ v.LastName, Value = v.UserId.ToString() });


            ViewData["VoucherType"] = _voucherTypeRepository.GetAll().Where(x => x.CompanyId == users.CompanyId)
                                         .Select(v => new SelectListItem { Text = v.Code + "-" + v.Name, Value = v.VoucherTypeId.ToString() });

            return View();
        }

        public JsonResult GetVoucherList(long? userId , DateTime fromdate , DateTime todate , string status , string search , long? voucherTypeId)
        {
            var resobj = new List<Voucher>();
            var userres = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userres).LastOrDefault();

             var  newfromdate = fromdate.ToString("MM/dd/yyyy");
             var newtodate = todate.ToString("MM/dd/yyyy");

             var res = _voucherRepository.GetVoucherList(userId ?? 0 , newfromdate, newtodate, status , search, users.CompanyId ?? 0 , voucherTypeId ?? 0).ToList();
             

            //if(status == null || status == "")
            //{ 
            //    IEnumerable<Voucher> ap = _voucherRepository.GetList(b => 
            //                  userId != null ? b.CreatedBy == userId :
            //                  fromdate != null ? b.CreatedAt >= fromdate :
            //                  todate != null ? b.CreatedAt <= todate :
            //                  b.CompanyId == users.CompanyId && b.Status != "Posted" , c => c.VoucherDetails);
            //    ap = ap.OrderByDescending(a => a.VoucherId);

            //    resobj.AddRange(ap);
            //}
            //else
            //{
            //    IEnumerable<Voucher> ap = _voucherRepository.GetList(b =>
            //                userId != null ? b.CreatedBy == userId :
            //                fromdate != null ? b.CreatedAt >= fromdate :
            //                todate != null ? b.CreatedAt <= todate :
            //                b.CompanyId == users.CompanyId && b.Status == status, c => c.VoucherDetails);
            //    ap = ap.OrderByDescending(a => a.VoucherId);

            //    resobj.AddRange(ap);

            //}


            return Json(res);
        }


        public IActionResult CancelVouchers(List<Voucher> model)
        {
            try
            {
                var Errorres = new List<Voucher>();

                var voucherlist = new List<Voucher>();

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

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
                foreach (var item in model)
                {
                   

                    if (item.Status == "Posted")
                    {
                        var reseror = new Voucher()
                        {
                            VoucherNo = item.VoucherNo,
                            Narration = "it's  Posted voucher",
                        };

                        Errorres.Add(reseror);
                    }
                    
                    if (item.Status == "Canceled")
                    {
                        var reseror = new Voucher()
                        {
                            VoucherNo = item.VoucherNo,
                            Narration = "it's  Canceled voucher",
                        };

                        Errorres.Add(reseror);
                    }

                    
                }

                if (Errorres.Count() > 0)
                {
                    return new OkObjectResult(new { error = true, message = Errorres });
                }

                long count = 0;
                foreach (var item in model)
                {
                    var resdata = _voucherRepository.GetVoucherAndVoucherDetailById(users.CompanyId ?? 0, item.VoucherId);
                    if(resdata  != null)
                    {
                        if(resdata.Status == "InProgress")
                        {
                            voucherlist.Add(resdata);
                            count += 1;
                        }
                    }

                }

                voucherlist.ForEach(x => x.Status = "Canceled");
                voucherlist.ForEach(x => x.VerifyById = users.UserId);
                voucherlist.ForEach(x => x.VerifyDate = DateTime.Now);

                _voucherRepository.UpdateRange(voucherlist);
               
                return new OkObjectResult(new { error = false, message =   count + " vouchers are Canceled" });
            }
            catch (Exception e)
            {
                throw;
            }
 

        }
        
        public IActionResult PostVouchers(List<Voucher> model)
        {
            try
            {
                var Errorres = new List<Voucher>();

                var voucherlist = new List<Voucher>();

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

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
                foreach (var item in model)
                {
                    if (item.VoucherDetails.Count() <= 0)
                    {
                        var reseror = new Voucher()
                        {
                            VoucherNo = item.VoucherNo,
                            Narration = "voucher detail is missing",
                        };

                        Errorres.Add(reseror);
                    }

                    if (item.Status != "InProgress")
                    {
                        var reseror = new Voucher()
                        {
                            VoucherNo = item.VoucherNo,
                            Narration = "it's not in progress ",
                        };

                        Errorres.Add(reseror);
                    }

                    if ((item.VoucherDetails.Sum(x => x.Debit) - item.VoucherDetails.Sum(x => x.Credit)) != 0)
                    {
                        var reseror = new Voucher()
                        {
                            VoucherNo = item.VoucherNo,
                            Narration = "debit and credit are not equals",
                        };

                        Errorres.Add(reseror);
                    }
                }

                if (Errorres.Count() > 0)
                {
                    return new OkObjectResult(new { error = true, message = Errorres });
                }

                long count = 0;
                foreach (var item in model)
                {
                    var resdata = _voucherRepository.GetVoucherAndVoucherDetailById(users.CompanyId ?? 0, item.VoucherId);
                    if(resdata  != null)
                    {
                        if(resdata.VoucherDetails.Count() > 0 && resdata.Status == "InProgress")
                        {
                            voucherlist.Add(resdata);
                            count += 1;
                        }
                    }

                }

                voucherlist.ForEach(x => x.Status = "Posted");
                voucherlist.ForEach(x => x.VerifyById = users.UserId);
                voucherlist.ForEach(x => x.VerifyDate = DateTime.Now);

                _voucherRepository.UpdateRange(voucherlist);
               
                return new OkObjectResult(new { error = false, message =   count + " vouchers are posted" });
            }
            catch (Exception e)
            {
                throw;
            }
 

        }


        public IActionResult UpdateVoucherView()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            ViewData["VoucherType"] = _voucherTypeRepository.GetAll().Where(x => x.CompanyId == users.CompanyId)
                                            .Select(v => new SelectListItem { Text = v.Name, Value = v.VoucherTypeId.ToString() });

            return View();
        }


        public JsonResult GetVoucherAndVoucherDetailById(long VoucherId)
        {

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

            if(users != null)
            {
                if(users.CompanyId != null || users.CompanyId != 0)
                {
                    var res = _voucherRepository.GetVoucherAndVoucherDetailById(users.CompanyId ?? 0, VoucherId);

                    return Json(res);
                }
                else
                {
                    return Json("");
                }
            }
            else
            {
                return Json("");
            }
           
        }

        public IActionResult PostVouchersFromExcel(string filename , List<Voucher> model)
        {
            try
            {
                var Errorres = new List<Voucher>();

                var voucherlist = new List<Voucher>();

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

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

                foreach (var item in model)
                {
                    if (item.VoucherNo == "")
                    {
                        var reseror = new Voucher()
                        {
                            VoucherNo = item.VoucherNo,
                            Narration = "voucher no is some record",
                        };

                        Errorres.Add(reseror);
                    }


                    if (item.VoucherTypeId == 0)
                    {
                        var reseror = new Voucher()
                        {
                            VoucherNo = item.VoucherNo,
                            Narration = "VoucherType is missing on voucher no" + item.VoucherNo,
                        };
                        Errorres.Add(reseror);
                    }




                    if (Convert.ToDateTime(item.VoucherDate.Date.ToString("MM/dd/yyyy")) < Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy")))
                    {
                        var resforallowbackdate = _allowBackDateTransactionRepository.GetAll().Where(x => x.UserName == User.Identity.Name).LastOrDefault();

                        if (resforallowbackdate == null)
                        {
                            var reseror = new Voucher()
                            {
                                VoucherNo = item.VoucherNo,
                                Narration = "not allow for back date voucher creation  on voucher no" + item.VoucherNo,
                            };
                            Errorres.Add(reseror);
                        }
                    }

                    if ((item.VoucherDetails.Sum(x => x.Debit) - item.VoucherDetails.Sum(x => x.Credit)) != 0)
                    {
                        var reseror = new Voucher()
                        {
                            VoucherNo = item.VoucherNo,
                            Narration = "Debit and Credit Side Are Not Equal's on voucher no" + item.VoucherNo,
                        };
                        Errorres.Add(reseror);
                         
                    }
                }

                foreach (var item in model)
                {
                    foreach (var newitem in item.VoucherDetails)
                    {
                        if (newitem.Debit < 0)
                        {
                            var reseror = new Voucher()
                            {
                                VoucherNo = item.VoucherNo,
                                Narration = "please add Debit in amount in  voucher no" + item.VoucherNo,
                            };
                            Errorres.Add(reseror);

                        }

                        if (newitem.Credit < 0)
                        {
                            var reseror = new Voucher()
                            {
                                VoucherNo = item.VoucherNo,
                                Narration = "please add Credit in amount in  voucher no" + item.VoucherNo,
                            };
                            Errorres.Add(reseror);

                        }

                        if (newitem.Credit <= 0 && newitem.Debit <= 0)
                        {
                            var reseror = new Voucher()
                            {
                                VoucherNo = item.VoucherNo,
                                Narration = "debit and credit both are < 0 in  voucher no" + item.VoucherNo,
                            };
                            Errorres.Add(reseror);

                        }

                        if (newitem.Credit > 0 && newitem.Debit > 0)
                        {
                            var reseror = new Voucher()
                            {
                                VoucherNo = item.VoucherNo,
                                Narration = "debit and credit both are > 0 in  voucher no" + item.VoucherNo,
                            };
                            Errorres.Add(reseror);
                        }


                        if (newitem.VoucherServiceTypeId == 0)
                        {
                            var reseror = new Voucher()
                            {
                                VoucherNo = item.VoucherNo,
                                Narration = "please add ServiceType in  voucher no" + item.VoucherNo,
                            };
                            Errorres.Add(reseror);
                        }
                        if (newitem.DepartmentId == 0)
                        {
                            var reseror = new Voucher()
                            {
                                VoucherNo = item.VoucherNo,
                                Narration = "please add Department in  voucher no" + item.VoucherNo,
                            };
                            Errorres.Add(reseror);
                        }

                        if (newitem.Narration == "")
                        {
                            var reseror = new Voucher()
                            {
                                VoucherNo = item.VoucherNo,
                                Narration = "please add Narration in  voucher no" + item.VoucherNo,
                            };
                            Errorres.Add(reseror);
                        }

                        if (newitem.ChartOfAccountId == 0)
                        {
                            var reseror = new Voucher()
                            {
                                VoucherNo = item.VoucherNo,
                                Narration = "please add ChartOfAccount in  voucher no" + item.VoucherNo,
                            };
                            Errorres.Add(reseror);
                        }

                        if (newitem.ChartOfAccountId == 0)
                        {
                            var reseror = new Voucher()
                            {
                                VoucherNo = item.VoucherNo,
                                Narration = "please add ChartOfAccount in  voucher no" + item.VoucherNo,
                            };
                            Errorres.Add(reseror);
                        }

                        if (newitem.ChartOfAccountId == 0)
                        {
                            var reseror = new Voucher()
                            {
                                VoucherNo = item.VoucherNo,
                                Narration = "please add ChartOfAccount in  voucher no" + item.VoucherNo,
                            };
                            Errorres.Add(reseror);
                        }
                    }
                    var financialYear = _financialYearRepository.GetAll().Where(x => x.Year == item.VoucherDate.ToString("yyyy") && x.CompanyId == users.CompanyId).LastOrDefault();
                    if (financialYear != null)
                    {
                        var financialYearclosure = _financialYearClosureRepository.GetAll().Where(x => x.FinancialYearId == financialYear.FinancialYearId && x.CompanyId == users.CompanyId).LastOrDefault();

                        if (financialYearclosure != null)
                        {
                             var reseror = new Voucher()
                            {
                                VoucherNo = item.VoucherNo,
                                Narration = "Financial Year Closed Entry Can't be Performed" + item.VoucherNo,
                            };
                            Errorres.Add(reseror);

                        }

                    }

                }


                var resvoucher = _voucherRepository.GetVoucherByfileName(filename);

                if(resvoucher != null)
                {
                    var reseror = new Voucher()
                    {
                        VoucherNo = filename,
                        Narration = "filename name already exist",
                    };
                    Errorres.Add(reseror);
                }

                if (Errorres.Count() > 0)
                {
                    return new OkObjectResult(new { error = true, message = Errorres });
                }



                var count = 0;
                foreach (var item in model)
                {

                    var res = _voucherRepository.GetVoucherNo(users.CompanyId ?? 0, item.VoucherTypeId, item.VoucherDate.ToString("yyyy"), item.VoucherDate.ToString("MM"));

                    if (res != null && res != "")
                    {


                        item.CompanyId = users.CompanyId ?? 0;
                        item.CreatedAt = DateTime.Now;
                        item.CreatedBy = users.UserId;
                        item.EditedAt = null;
                        item.EditedBy = null;
                        item.FileName = filename;
                        item.Month = item.VoucherDate.ToString("MM");
                        item.VoucherNo = res;
                        item.Year = item.VoucherDate.ToString("yyyy");
                        item.VoucherDate = item.VoucherDate;
                        item.VoucherTypeId = item.VoucherTypeId;
                        item.Status = item.Status;
                        item.VerifyDate = null;
                        item.VerifyById = null;

                        foreach (var newitem in item.VoucherDetails)
                        {
                            newitem.CompanyId = users.CompanyId ?? 0;
                            newitem.CreatedAt = DateTime.Now;
                            newitem.CreatedBy = users.UserId;
                            newitem.EditedAt = null;
                            newitem.EditedBy = null;
                            newitem.TranDate = item.VoucherDate;
                            newitem.VoucherId = item.VoucherId;
                            

                        }
                        _voucherRepository.Add(item);

                        count += 1;
                    }
                }

                return new OkObjectResult(new { error = false, message = "total " + count + " voucher created"  });
            }
            catch (Exception e)
            {
                throw;
            }


        }

        #endregion

        #region Transaction

        public JsonResult GetVoucherTransactions(DateTime fromdate, DateTime todate)
        {
            var resobj = new List<Voucher>();
            var userres = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userres).LastOrDefault();

            var newfromdate = fromdate.ToString("MM/dd/yyyy");
            var newtodate = todate.ToString("MM/dd/yyyy");

            var res = _voucherRepository.GetVoucherTransactions(  newfromdate, newtodate,  users.CompanyId ?? 0).ToList();
             
            return Json(res);
        }
        #endregion



        public IActionResult CreateDublicateVoucher(long voucherId)
        {
            try
            {
                var voucherdetaillist = new List<VoucherDetail>();
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {
                    if (users.CompanyId == null || users.CompanyId == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "company not avaible" });
                    }
                }



                var financialYear = _financialYearRepository.GetAll().Where(x => x.Year == DateTime.Now.ToString("yyyy") && x.CompanyId == users.CompanyId).LastOrDefault();

                if (financialYear != null)
                {
                    var financialYearclosure = _financialYearClosureRepository.GetAll().Where(x => x.FinancialYearId == financialYear.FinancialYearId && x.CompanyId == users.CompanyId).LastOrDefault();

                    if (financialYearclosure != null)
                    {
                        return new OkObjectResult(new { error = true, message = "Financial Year Closed Entry Can't be Performed" });

                    }

                }

                var vouchereres = _voucherRepository.GetVoucherById(users.CompanyId ?? 0 , voucherId);


                if (vouchereres != null)
                {
                    var voucherdetailres = _voucherRepository.GetVoucherDetail(users.CompanyId ?? 0, voucherId).ToList();

                    if (voucherdetailres.Count() > 0)
                    {

                        if ((voucherdetailres.Sum(x => x.Debit) - voucherdetailres.Sum(x => x.Credit)) != 0)
                        {
                            return new OkObjectResult(new { error = true, message = "Debit and Credit Side Are Not Equal's" });
                        }

                        var res = _voucherRepository.GetVoucherNo(users.CompanyId ?? 0, vouchereres.VoucherTypeId, DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"));

                        if (res != null && res != "")
                        {
                            var voucherRes = new Voucher()
                            {
                                CompanyId = users.CompanyId ?? 0,
                                CreatedAt = DateTime.Now,
                                CreatedBy = users.UserId,
                                EditedAt = null,
                                EditedBy = null,
                                FileName = "",
                                Month = DateTime.Now.ToString("MM"),
                                Narration = "",
                                Status = "InProgress",
                                VerifyById = null,
                                VerifyDate = null,
                                VoucherNo = res,
                                VoucherTypeId = vouchereres.VoucherTypeId,
                                Year = DateTime.Now.ToString("yyyy"),
                                VoucherDate = DateTime.Now,
                            };

                            foreach (var item in voucherdetailres)
                            {
                                var VoucherDetailres = new VoucherDetail()
                                {
                                    ChartOfAccountId = item.ChartOfAccountId,
                                    ChequeOrReference = item.ChequeOrReference,
                                    CompanyId = users.CompanyId ?? 0,
                                    CreatedAt = DateTime.Now,
                                    CreatedBy = users.UserId,
                                    EditedAt = null,
                                    EditedBy = null,
                                    Credit = item.Credit,
                                    CustomerId = item.CustomerId,
                                    Debit = item.Debit,
                                    DepartmentId = item.DepartmentId,
                                    LineNumber = item.LineNumber,
                                    Narration = item.Narration,
                                    TranDate = DateTime.Now,
                                    VoucherServiceTypeId = item.VoucherServiceTypeId,
                                    VoucherTypeId = vouchereres.VoucherTypeId,

                                };
                                voucherdetaillist.Add(VoucherDetailres);
                            }

                            _voucherRepository.Add(voucherRes);
                            voucherdetaillist.ForEach(x => x.VoucherId = voucherRes.VoucherId);
                            _voucherDetailRepository.AddRange(voucherdetaillist);

                            return new OkObjectResult(new { error = false, message = voucherRes });
                        }
                        else
                        {
                            return new OkObjectResult(new { error = true, message = "no code found" });
                        }



                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = " no voucher detail found " });
                    }


                }
                else
                {
                    return new OkObjectResult(new { error = true, message = " no voucher found " });
                }

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
            
        }

        public IActionResult CreateReversalVoucher(long voucherId)
        {
            try
            { 
                var vouchertype = _voucherTypeRepository.GetAll().Where(x => x.Code == "GV").LastOrDefault();

                if (vouchertype  == null)
                {
                    return new OkObjectResult(new { error = true, message = "no voucher type found" });
                }

                var voucherdetaillist = new List<VoucherDetail>();
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();

                if (users != null)
                {
                    if (users.CompanyId == null || users.CompanyId == 0)
                    {
                        return new OkObjectResult(new { error = true, message = "company not avaible" });
                    }
                }


                var financialYear = _financialYearRepository.GetAll().Where(x => x.Year == DateTime.Now.ToString("yyyy") && x.CompanyId == users.CompanyId).LastOrDefault();

                if (financialYear != null)
                {
                    var financialYearclosure = _financialYearClosureRepository.GetAll().Where(x => x.FinancialYearId == financialYear.FinancialYearId && x.CompanyId == users.CompanyId).LastOrDefault();

                    if (financialYearclosure != null)
                    {
                        return new OkObjectResult(new { error = true, message = "Financial Year Closed Entry Can't be Performed" });

                    }

                }

                var vouchereres = _voucherRepository.GetVoucherById(users.CompanyId ?? 0, voucherId);


                if (vouchereres != null)
                {
                    var voucherdetailres = _voucherRepository.GetVoucherDetail(users.CompanyId ?? 0, voucherId).ToList();

                    if (voucherdetailres.Count() > 0)
                    {

                        if ((voucherdetailres.Sum(x => x.Debit) - voucherdetailres.Sum(x => x.Credit)) != 0)
                        {
                            return new OkObjectResult(new { error = true, message = "Debit and Credit Side Are Not Equal's" });
                        }

                        var res = _voucherRepository.GetVoucherNo(users.CompanyId ?? 0, vouchertype.VoucherTypeId, DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"));

                        if (res != null && res != "")
                        {
                            var voucherRes = new Voucher()
                            {
                                CompanyId = users.CompanyId ?? 0,
                                CreatedAt = DateTime.Now,
                                CreatedBy = users.UserId,
                                EditedAt = null,
                                EditedBy = null,
                                FileName = "",
                                Month = DateTime.Now.ToString("MM"),
                                Narration = "",
                                Status = "InProgress",
                                VerifyById = null,
                                VerifyDate = null,
                                VoucherNo = res,
                                VoucherTypeId = vouchertype.VoucherTypeId,
                                Year = DateTime.Now.ToString("yyyy"),
                                VoucherDate = DateTime.Now,
                            };

                            foreach (var item in voucherdetailres)
                            {
                                var VoucherDetailres = new VoucherDetail()
                                {
                                    ChartOfAccountId = item.ChartOfAccountId,
                                    ChequeOrReference = item.ChequeOrReference,
                                    CompanyId = users.CompanyId ?? 0,
                                    CreatedAt = DateTime.Now,
                                    CreatedBy = users.UserId,
                                    EditedAt = null,
                                    EditedBy = null,
                                    Credit = item.Debit,
                                    CustomerId = item.CustomerId,
                                    Debit = item.Credit,
                                    DepartmentId = item.DepartmentId,
                                    LineNumber = item.LineNumber,
                                    Narration = item.Narration,
                                    TranDate = DateTime.Now,
                                    VoucherServiceTypeId = item.VoucherServiceTypeId,
                                    VoucherTypeId = vouchertype.VoucherTypeId,

                                };
                                voucherdetaillist.Add(VoucherDetailres);
                            }

                            _voucherRepository.Add(voucherRes);
                            voucherdetaillist.ForEach(x => x.VoucherId = voucherRes.VoucherId);
                            _voucherDetailRepository.AddRange(voucherdetaillist);

                            return new OkObjectResult(new { error = false, message = voucherRes });
                        }
                        else
                        {
                            return new OkObjectResult(new { error = true, message = "no code found" });
                        } 
                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = " no voucher detail found " });
                    }


                }
                else
                {
                    return new OkObjectResult(new { error = true, message = " no voucher found " });
                }

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }

        }

       
    }
}
