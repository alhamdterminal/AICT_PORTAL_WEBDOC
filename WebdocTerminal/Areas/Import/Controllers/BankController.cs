using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class BankController : ParentController
    {
        public IBankRepository _bankRepo;
        public IBankBranchRepository _bankBranchRepo;
        public BankController(IBankRepository bankRepo
                              , IBankBranchRepository bankBranchRepo)
        {
            _bankRepo = bankRepo;
            _bankBranchRepo = bankBranchRepo;
        }

        public IActionResult Bank()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetBank()
        {
            var banks = _bankRepo.GetAll();
            return Json(banks);
        }


        [HttpGet]
        public JsonResult GetBankPayOder()
        {
            var banks = _bankRepo.GetAll().Where(x=> x.Type == "PayOrder");
            return Json(banks);
        }


        [HttpPost]
        public IActionResult AddBank(Bank bank)
        {
            _bankRepo.Add(bank);
            return new OkObjectResult(new { BankId = bank.BankId});
        }

        [HttpPost]
        public IActionResult updateBank(Bank bank)
        {
            _bankRepo.Update(bank);
            return new OkObjectResult(new { BankId = bank.BankId });
        }

         public void Delete(long key)
        {
            var agent = _bankRepo.Find(key);

            _bankRepo.Delete(agent);
        }



        public IActionResult BankBranch()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetBankBranch()
        {
            var bankBranch = _bankBranchRepo.GetAll();
            return Json(bankBranch);
        }

        [HttpPost]
        public IActionResult AddBankBranch(BankBranch bankbranch)
        {
            _bankBranchRepo.Add(bankbranch);

            return new OkObjectResult(new { BankBranchId = bankbranch.BankBranchId });
        }

        [HttpPost]
        public IActionResult UpdateBankBranch(BankBranch bankbranch)
        {
            _bankBranchRepo.Update(bankbranch);

            return new OkObjectResult(new { BankBranchId = bankbranch.BankBranchId });
        }




    }
}