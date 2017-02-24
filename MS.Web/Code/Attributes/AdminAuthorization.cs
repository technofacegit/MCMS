using MS.Web.Code.LIBS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MS.Web.Code.Attributes
{
    public class AdminAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (SiteSession.TblKullanicilar == null)
            {
                filterContext.Result = new RedirectResult("~/admin/home/?returnurl=" + filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri);
            }
        }
    }
}