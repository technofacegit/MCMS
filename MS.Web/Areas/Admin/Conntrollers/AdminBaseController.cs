using MS.Web.Areas.Admin.Models;
using MS.Web.Code.LIBS;
using MS.Web.Code.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MS.Web.Areas.Admin.Conntrollers
{
    
    public class AdminBaseController : Controller
    {
        //
        // GET: /Admin/AdminBase/

        #region "Message Notifications"

        public void ShowMessageBox(MessageType type, string message, bool isCurrentView = true)
        {
            if (isCurrentView)
                ViewData["MessageModel"] = new AdminNotificationViewModel(type, message);
            else
                TempData["MessageModel"] = new AdminNotificationViewModel(type, message);
        }

        #endregion

        #region "Serialization"

        public JsonResult NewtonsoftJsonResult(object data)
        {
            return new JsonNetResult { Data = data };
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

        public void RemoveAuthentication()
        {
            SiteSession.TblKullanicilar = null;
        }

    }
}