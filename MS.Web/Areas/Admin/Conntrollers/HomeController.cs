using MS.Business;
using MS.Core.Language;
using MS.Web.Areas.Admin.Models;
using MS.Web.Code.Attributes;
using MS.Web.Code.Libs;
using MS.Web.Code.LIBS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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

       
        public ActionResult ClearMobileCache()
        {


            HttpWebRequest req = null;
            HttpWebResponse res = null;
            try
            {

                string data = "act=clearcontentcache";

                String url = System.Configuration.ConfigurationManager.AppSettings["CacheUrl"].ToString();
                req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                //req.Timeout = 30000; 

                byte[] dataStream = Encoding.UTF8.GetBytes(data);


                req.ContentLength = dataStream.Length;
                Stream newStream = req.GetRequestStream();
                newStream.Write(dataStream, 0, dataStream.Length);
                newStream.Close();

                res = (HttpWebResponse)req.GetResponse();
                Stream responseStream = res.GetResponseStream();
                var streamReader = new StreamReader(responseStream);

                //Read the response into an xml document 
               String s=streamReader.ReadToEnd();

                
               ShowMessageBox(MessageType.Success, "İçerik devreye alındı.\r\nLütfen Mobil uygulamayı kontrol ediniz", true);
            }
            catch (Exception ex)
            {
                ShowMessageBox(MessageType.Warning, "Hata oluştu:"+ex.Message, true);
          
               // Response.Write(ex.Message);
            }

            //return RedirectToAction("Confirmation");
            return View("Dashboard");
           // return View("Dashboard");
           // return RedirectToAction("Index");
            ////return View();
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