using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Helpers
{
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly HttpContext httpContext;
        public bool Authorize(DashboardContext context)
        {

            //var resasdasd = context.GetHttpContext().User.Identity.Name;
            //var resasdasd1 = context.GetHttpContext().User.IsInRole("Admin");
          
            //string hangfireRole = httpContext.Session.GetString("HangFireRole");

            //if (hangfireRole.ToLower() == "admin")
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return true;
        }

        //public bool Authorize(IDictionary<string, object> owinEnvironment)
        //{
        //    bool boolAuthorizeCurrentUserToAccessHangFireDashboard = true;

        //    //if (Microsoft.AspNetCore.Http.HttpContext..User.Identity.IsAuthenticated)
        //    //{
        //    //    if (HttpContext.Current.User.IsInRole("Account Administrator"))
        //    //        boolAuthorizeCurrentUserToAccessHangFireDashboard = true;
        //    //}

        //    return boolAuthorizeCurrentUserToAccessHangFireDashboard;
        //}

        //public bool Authorize([NotNull] DashboardContext context)
        //{
        //    return true;
        //}

    }
}
