using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;
using System.IO;

namespace WebdocTerminal.Controllers
{
    public class DynamicReportController : Controller
    {

        public IACKRepository _aCKRepository;

        private IHostingEnvironment env;
        public DynamicReportController(IACKRepository aCKRepository, IHostingEnvironment _env)
        {
            _aCKRepository = aCKRepository;
            env = _env;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DynamicReports()
        {
            return View();
        }

        public IActionResult EDIMessages()
        {
            return View();
        }



        public JsonResult GetEDIMessages(string FileName)
        {
            var data = _aCKRepository.GetAll().Where(c => c.FileName.ToUpper() == FileName.ToUpper()).ToList();
            if(data != null)
            {
                return Json(data);
            }
            return Json("");
        }



        public FileResult Download(string filename)
        {
            var path = Path.Combine(env.ContentRootPath, "EDIFiles/Processed", filename);
            return File(System.IO.File.ReadAllBytes(path), "application/octet-stream", filename);
        }


        public FileResult DownloadACK(string filename)
        {
            if (filename.ToUpper().Contains("OGIE") ||
                       filename.ToUpper().Contains("OSRC") ||
                       filename.ToUpper().Contains("OGDC") ||
                       filename.ToUpper().Contains("OGTE") ||
                       filename.ToUpper().Contains("GIIO") ||
                       filename.ToUpper().Contains("SRC") ||
                       filename.ToUpper().Contains("SRCO") ||
                       filename.ToUpper().Contains("GTO") ||
                       filename.ToUpper().Contains("GTOO") ||
                       filename.ToUpper().Contains("GTTO") ||
                       filename.ToUpper().Contains("SCMO") ||
                       filename.ToUpper().Contains("TTSO") ||
                       filename.ToUpper().Contains("PGOO"))
            {

                var path = Path.Combine(env.ContentRootPath, "EDIFiles/Processed", filename);
                return File(System.IO.File.ReadAllBytes(path), "application/octet-stream", filename);

            }
            else
            {
                var path = Path.Combine(env.ContentRootPath, "EDIFiles/ACK_Sent", filename);
                return File(System.IO.File.ReadAllBytes(path), "application/octet-stream", filename);


            }

        }

        public IActionResult EDIMessagesExport()
        {
            return View();
        }


        public JsonResult GetEDIMessagesFromGDnumber(string GDNumber)
        {
            var data = _aCKRepository.getExportfilesBygdnumer(GDNumber.Trim().ToUpper());
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }



        public IActionResult EDIMessagesImport()
        {
            return View();
        }

        public JsonResult getImportfilesByvirNumber(string VIRNumber)
        {
            var data = _aCKRepository.getImportfilesByvirNumberCFS(VIRNumber.Trim().ToUpper());
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }

        public IActionResult EDIMessagesImportCY()
        {
            return View();
        }

        public JsonResult getImportfilesByvirNumberCY(string VIRNumber)
        {
            var data = _aCKRepository.getImportfilesByvirNumberCY(VIRNumber.Trim().ToUpper());
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }


        public JsonResult getfilesInfo(string VIRNumber, string ContainerNo, string gdnumber, string BLno )
        {
            var data = _aCKRepository.getfilesInfo(VIRNumber  , ContainerNo   , gdnumber  , BLno  );
            if (data != null)
            {
                return Json(data);
            }
            return Json("");
        }
    }
}