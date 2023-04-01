using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Lr5.Controllers;

namespace Lr5.Filters
{

    // разобраться с глобальной переменной isAuthorized и авторизацией

    public class SetIsAuthorizedFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is HomeController controller)
            {
                bool isAuthorized = false;
                if (context.HttpContext.User.Identity.IsAuthenticated)
                {
                    isAuthorized = true;
                }
                controller.ViewData["isAuthorized"] = isAuthorized;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is SignOutResult)
            {
                if (context.Controller is HomeController controller)
                {
                    controller.ViewData["isAuthorized"] = false;
                }
            }
        }
    }
}
