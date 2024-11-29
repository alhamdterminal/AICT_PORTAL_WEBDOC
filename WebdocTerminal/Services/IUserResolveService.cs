using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Data;

namespace WebdocTerminal.Services
{
    public interface IUserResolveService
    {
        string GetCurrentSessionUserId(ApplicationDbContext dbContext);
    }
}
