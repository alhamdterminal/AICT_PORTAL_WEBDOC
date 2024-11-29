using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Helpers;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Controllers
{
    [MyAuthorize]
    public class AuditController : Controller
    {
        private IAuditRepository _auditRepo;

        public AuditController(IAuditRepository auditRepo)
        {
            _auditRepo = auditRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAuditData()
        {
            var _audit = _auditRepo.GetAll().OrderByDescending(a=>a.DateTime);

            return Json(_audit);
        }
    }
}