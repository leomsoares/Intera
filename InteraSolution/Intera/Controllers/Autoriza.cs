using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intera.Controllers
{
    public class Autoriza : ActionFilterAttribute
    {
        // GET: Autoriza
        public override void OnActionExecuted(ActionExecutedContext ctx)
        {
            if(ctx.RequestContext.HttpContext.Session["user"] != null)
            {
                base.OnActionExecuted(ctx);
            }
            else
            {
                ctx.Result = new RedirectResult("/default/Index");
            }
        }
    }
}