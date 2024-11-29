using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Account.Controllers
{
    public class ChequePrintingController : ParentController
    {

        public IUsersRepository _usersRepository;
        public IChequePrintingRepository _chequePrintingRepository;
        public ChequePrintingController(IUsersRepository usersRepository,
                                        IChequePrintingRepository chequePrintingRepository)
        {
            _usersRepository = usersRepository;
            _chequePrintingRepository = chequePrintingRepository;
        }

        public IActionResult ChequePrinting()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userId).LastOrDefault();


            ViewData["Users"] = _usersRepository.GetFinnaceUsers().Where(x => x.CompanyId == users.CompanyId)
                                         .Select(v => new SelectListItem { Text = v.FirstName + "-" + v.LastName, Value = v.UserId.ToString() });

            return View();
        }


        public JsonResult GetChequePrintingList(long? userId, DateTime fromdate, DateTime todate, string search)
        {
            var userres = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _usersRepository.GetAll().Where(x => x.IdentityUserId == userres).LastOrDefault();

            if (users != null)
            {
                if (users.CompanyId == null || users.CompanyId == 0)
                {
                    return Json("");
                }
            }
            var res = _chequePrintingRepository.GetChequePrintingList(users.CompanyId, userId, fromdate, todate, search).ToList();

            return Json(res);
        }


        public IActionResult AddChequePrintingLog(ChequePrinting data)
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

                var res = new ChequePrinting()
                {
                    Amount = data.Amount,
                    AmountInWords = data.AmountInWords,
                    BankName = data.BankName,
                    ChequeNo = data.ChequeNo,
                    CompanyId = users.CompanyId ?? 0,
                    Count =  data.Count,
                    CreatedAt = DateTime.Now,
                    CreatedBy = users.UserId,
                    EditedAt=  null,
                    EditedBy = null,
                    IsPrinted = true,
                    PartyName = data.PartyName,
                    VoucherDate = data.VoucherDate,
                    VoucherDetailId = data.VoucherDetailId,
                    VoucherId = data.VoucherId,
                    VoucherNo = data.VoucherNo
                };

                _chequePrintingRepository.Add(res);

                return new OkObjectResult(new { error = false, message = "Updated" });

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = "please try again" });

            }
        }

    }
}
