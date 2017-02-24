using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MS.Web.Models.Security;

namespace MS.Web.Code.Attributes
{
    public class CustomAuthorization : AuthorizeAttribute
    {
        private object[] roleTypes;

        public CustomAuthorization(params object[] roleTypes)
        {
            this.roleTypes = roleTypes;
        }

        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentUser != null && CurrentUser.Identity.IsAuthenticated && roleTypes.Length > 0)
            {
                if (!CurrentUser.IsInRole(roleTypes))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "error",
                        action = "accessDenied",
                        area = ""
                    }));
                }
            }
            else
            {
                base.OnAuthorization(filterContext);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}
