using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Helpers;

namespace WebdocTerminal.Areas.Export.Controllers
{
    [Area("Export")]
    [MyAuthorize]
    public class ParentController : Controller
    {
         
    }
}