using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Helpers;

namespace WebdocTerminal.Areas.Account.Controllers
{
    [Area("Account")]
    [MyAuthorize]
    public class ParentController : Controller
    {
        
    }
}
