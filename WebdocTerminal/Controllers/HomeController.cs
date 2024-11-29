using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Controllers
{
    [MyAuthorize]
    public class HomeController : Controller
    {
        private WebocProcessor webocProcessor;
        private IHostingEnvironment env;

        private IPermissionRepository _permissionRepo;

        private IIPAORepository _ipaoRepo;
        private IGIIORepository _giioRepo;

        private IIGMORepository _igmoRepo;

        private ISRCRepository _srcRepo;

        private ISRCORepository _srcoRepo;

        private IGTORepository _gtoRepo;

        private IGTOORepository _gtooRepo;

        private ISCMORepository _scmoRepo;

        private ICCMORepository _ccmoRepo;

        private IOPIARepository _oPIARepository;

        private IOGDERepository _oGDERepository;

        private IOGDCRepository _oGDCRepository;

        private IOGIERepository _oGIERepository;

        private IOSRCRepository _oSRCRepository;

        private IOGTERepository _oGTERepository;

        public HomeController(WebocProcessor _webocProcessor, IHostingEnvironment _env,
            IPermissionRepository permissionRepo,
            IOPIARepository oPIARepository,
            IOSRCRepository oSRCRepository,
            IOGDERepository oGDERepository,
            IOGIERepository oGIERepository,
            IOGDCRepository oGDCRepository,
            IIGMORepository igmoRepo, IIPAORepository ipaoRepo,
            IGTORepository gtoRepo, IGIIORepository igiioRepo,
            ISRCRepository srcRepo, ISRCORepository srcoRepo,
            IGTOORepository gtooRepo, ICCMORepository ccmoRepo,
            IOGTERepository oGTERepository,
            ISCMORepository scmoRepo)
        {
            webocProcessor = _webocProcessor;
            env = _env;

            _permissionRepo = permissionRepo;
            _oGDCRepository = oGDCRepository;
            _ipaoRepo = ipaoRepo;
            _oSRCRepository = oSRCRepository;
            _igmoRepo = igmoRepo;
            _giioRepo = igiioRepo;
            _gtoRepo = gtoRepo;
            _srcRepo = srcRepo;
            _srcoRepo = srcoRepo;
            _gtooRepo = gtooRepo;
            _scmoRepo = scmoRepo;
            _ccmoRepo = ccmoRepo;
            _oPIARepository = oPIARepository;
            _oGDERepository = oGDERepository;
            _oGIERepository = oGIERepository;
            _oGTERepository = oGTERepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DashboardImport()
        {
            return View();
        }

        public IActionResult DashboardExport()
        {
            return View();
        }

        public IActionResult UnAuthorizedView()
        {
            return View();
        }

        public IActionResult Designer()
        {
            return View();
        }

        public IActionResult Permissions(string Role)
        {
            return Json(_permissionRepo.GetRolePermissions(Role));
        }

        public IActionResult Viewer()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }






}


