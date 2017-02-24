using MS.Business;
using MS.Web.Code.Attributes;
using MS.Web.Code.LIBS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MS.Web.Areas.Admin.Conntrollers
{
    [AdminAuthorization]
    public class SqlQueryController : AdminBaseController
    {
        //
        // GET: /Admin/SqlQuery/
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Index(string querytext)
        {
            try
            {
                //Global.Context.ExecuteStoreCommand(querytext);
                //ShowMessageBox(MessageType.Success, "Sorgulama başarıyla gerçekleşti.", false);
            }
            catch (Exception ex)
            {
                ShowMessageBox(MessageType.Danger, "Sorgulama sırasında hata oluştu. Hata Mesajı: " + ex.Message, false);
            }
            return View();
        }
	}
}