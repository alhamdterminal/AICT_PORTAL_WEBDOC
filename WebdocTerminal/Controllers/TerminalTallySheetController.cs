using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Controllers
{
    [MyAuthorize]
    public class TerminalTallySheetController : Controller
    {
        private ITTSORepository _ttsoRepo;
        private readonly IOptions<AppConfig> _config;
        private WebocProcessor _webocProcessor;
        private IHostingEnvironment env;

        public TerminalTallySheetController(ITTSORepository ttsoRepo , IOptions<AppConfig> config,
             IHostingEnvironment _env, WebocProcessor webocProcessor)
        {
            _ttsoRepo = ttsoRepo;
            _config = config;
            _webocProcessor = webocProcessor;
            env = _env;
        }
        public IActionResult TerminalTallySheetView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTerminalTallySheet(List<StuffingConfirmationViewModel> data)
        {
            var dateTime = DateTime.Now;
            List<TTSO> tTSOs = new List<TTSO>();
            foreach (var item in data)
            {
                var ttso = new TTSO
                {
                    BLNo = item.BLNo,
                    ContainerNo = item.ContainerNo,
                    Category = "I", 
                    IndexNo = item.IndexNo ?? 0,
                    OpType = "A",
                    Performed = dateTime, 
                    VIRNo = item.VIRNo,
                    Weight = item.Weight,
                    MessageFrom = "AIT",
                    MessageTo = "WEBOC",
                    FileName = $"TTSO_{DateFormatter.ConvertToyyyyMMddFormat(dateTime)}_{DateFormatter.ConvertToHHmmFormat(dateTime)}{".txt"}"

                };
                tTSOs.Add(ttso);
            }
             

            _ttsoRepo.AddRange(tTSOs);

            return Ok();

        }

    }
}