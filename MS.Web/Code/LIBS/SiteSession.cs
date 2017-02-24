using MS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MS.Web.Code.LIBS
{
    public class SiteSession
    {
        public static TblKullanicilar TblKullanicilar
        {
            get { return HttpContext.Current.Session["AdminUser"] == null ? null : (TblKullanicilar)HttpContext.Current.Session["AdminUser"]; }
            set { HttpContext.Current.Session["AdminUser"] = value; }
        }

        public static string Apikey
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["Apikey"].ToString(); }
           
        }

        public static Dictionary<string, object> SessionOthers
        {
            get
            {
                if (HttpContext.Current.Session["SessionOthers"] == null)
                    HttpContext.Current.Session["SessionOthers"] = new Dictionary<string, object>();

                return (Dictionary<string, object>)HttpContext.Current.Session["SessionOthers"];
            }
        }
    }
}