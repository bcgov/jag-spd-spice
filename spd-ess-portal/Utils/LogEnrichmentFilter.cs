using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog.Context;

namespace Gov.Jag.Spice.Public.Utils
{
    public class LogEnrichmentFilter : IActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogEnrichmentFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var principal = _httpContextAccessor.HttpContext.User;

            ViewModels.User user = null;
            if (principal.Identity.IsAuthenticated)
            {
                user = new ViewModels.User(principal);
            }

            LogContext.PushProperty("User", user, true);
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
