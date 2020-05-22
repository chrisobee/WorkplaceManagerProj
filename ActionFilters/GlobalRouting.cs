using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WorkplaceManager.ActionFilters
{
    public class GlobalRouting : IActionFilter
    {
        private readonly ClaimsPrincipal _claimsPrincipal;
        public GlobalRouting(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"];
            if (controller.Equals("Home"))
            {
                if (_claimsPrincipal.IsInRole("Manager"))
                {
                    context.Result = new RedirectToActionResult("Index", "Managers", null);
                }
                else if(_claimsPrincipal.IsInRole("Senior Manager"))
                {
                    context.Result = new RedirectToActionResult("Index", "SeniorManagers", null);
                }
                else if (_claimsPrincipal.IsInRole("Employee"))
                {
                    context.Result = new RedirectToActionResult("Index", "Employees", null);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
