using MS.Business;
using MS.Core.Language;
using MS.Web.Areas.Admin.Models;
using MS.Web.Code.Attributes;
using MS.Web.Code.Libs;
using MS.Web.Code.LIBS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MS.Web.Areas.Admin.Conntrollers
{
    
    public class HomeController : AdminBaseController
    {
     
        public ActionResult Index()
        {

            if (SiteSession.TblKullanicilar != null)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["returnurl"]))
                {
                    return Redirect(Request.QueryString["returnurl"]);
                }
                else
                {
                    return RedirectToAction("dashboard", "home");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(AdminLoginViewModel user)
        {

            if (ModelState.IsValid)
            {
                
                TblKullanicilar objUser = TblKullanicilar.ValidLogin(user.UserName,user.Password);
                if (objUser != null)
                {
                    SiteSession.TblKullanicilar = objUser;
                    if (!string.IsNullOrEmpty(Request.QueryString["returnurl"]))
                    {
                        return Redirect(Request.QueryString["returnurl"]);
                    }
                    else
                    {
                        return RedirectToAction("dashboard", "home");
                    }
                }
                else
                { ShowMessageBox(MessageType.Danger, Resource_tr_TR.InvalidUserErrorMessage, true); }
            }
            else
            {
                ShowMessageBox(MessageType.Danger, Resource_tr_TR.InvalidUserErrorMessage, true);
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            RemoveAuthentication();
            return RedirectToAction("Index");
        }

       
        [HttpGet]
        [AdminAuthorization]
        public ActionResult Dashboard()
        {
            return View();
        }
      
         

       
	}
}