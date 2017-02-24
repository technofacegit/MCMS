using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using MS.Web.Code.LIBS;
using MS.Web.Models.Others;
using MS.Web.Models.Security;
using MS.Web.Code.Serialization;

namespace MS.Web.Controllers
{
    public class BaseController : Controller
    {
        #region "Authentication"

        public CustomPrincipal CurrentUser
        {
            get { return HttpContext.User as CustomPrincipal; }
        }

        public void CreateAuthenticationTicket(dynamic user, bool isPersist)
        {
            if (user != null)
            {
                CustomPrincipal principal = new CustomPrincipal(user, user.RoleType);
                principal.UserID = user.ID;

                var authTicket = new FormsAuthenticationTicket(1,
                    user.UserName,
                    DateTime.Now,
                    DateTime.Now.AddDays(1),
                    isPersist,
                    JsonConvert.SerializeObject(principal));

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);
            }
        }

        public void RemoveAuthentication()
        {
            FormsAuthentication.SignOut();
        }

        #endregion

        #region "Notificatons"

        private void ShowMessages(string title, string message, MessageType messageType, bool isCurrentView)
        {
            Notification model = new Notification
            {
                Heading = title,
                Message = message,
                Type = messageType
            };

            if (isCurrentView)
                this.ViewData.AddOrReplace("NotificationModel", model);
            else
                this.TempData.AddOrReplace("NotificationModel", model);
        }

        protected void ShowErrorMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Danger, isCurrentView);
        }

        protected void ShowSuccessMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Success, isCurrentView);
        }

        protected void ShowWarningMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Warning, isCurrentView);
        }

        protected void ShowInfoMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Info, isCurrentView);
        }


        #endregion

        #region "HTTP Errors"

        protected ActionResult Redirect404()
        {
            return Redirect("~/error/pagenotfound");
        }

        protected ActionResult Redirect500()
        {
            return Redirect("~/error/servererror");
        }

        protected ActionResult Redirect401()
        {
            return View();
        }

        #endregion

        #region "Exception Handling"

        public PartialViewResult CreateModelStateErrors()
        {
            return PartialView("_ValidationSummary", ModelState.Values.SelectMany(x => x.Errors));
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        #endregion

        #region "Serialization"

        public ActionResult NewtonSoftJsonResult(object data)
        {
            return new JsonNetResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

        #region "Dispose"

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion
    }
}