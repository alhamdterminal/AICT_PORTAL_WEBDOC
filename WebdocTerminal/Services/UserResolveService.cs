using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Data;

namespace WebdocTerminal.Services
{
    public class UserResolveService : IUserResolveService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserResolveService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentSessionUserId(ApplicationDbContext dbContext)
        {
            if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.User != null)
                return httpContextAccessor.HttpContext.User.Identity.Name;

            return "EDI";
        }
    }
}
