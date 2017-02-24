using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MS.Business;
using MS.Web.Code.LIBS;
using System.Collections;

namespace MS.Web.Areas.Admin.Conntrollers
{
    public class TblDeviceController : AdminBaseController
    {
        
        public ActionResult Index()
        {
               var tblDevice = TblDevice.GetTblDevices();
               return View(tblDevice);
        }

        
        [HttpPost]
        public ActionResult SendCampNotifiaction(FormCollection FC)
        {
            try
            {
                string[] arraytoken = new string[50];
                string message = string.Empty;
                string FCvalue = string.Empty;
                FCvalue = FC["tockenvalueforcamp"].ToString();

                if (FCvalue != null && FC["CampNO"] != null && FC["CampBody"] != null)
                {
                    int i = 0;
                    foreach (string value in FCvalue.Split(','))
                    {

                        arraytoken[i] = value;
                        i++;
                    }

                    message = FC["CampNO"];
                    message += " " + FC["CampBody"];
                }


                Android android = new Android(SiteSession.Apikey);
                android.SendNotification(message, arraytoken);
                ShowMessageBox(MessageType.Success, "Campaign Notification has been sent successfully!!", false);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Some problem has occurred while sending notification!!");
                return CreateModelStateErrors();
            }
           
            return RedirectToAction("Index");
           
        }

        [HttpPost]
        public ActionResult SendTextNotifiaction(FormCollection FC)
        {
            try
            {
                string[] arraytoken = new string[50];
                string message = null;
                if (FC["tockenvalueforText"] != null && FC["TextBody"] != null)
                {
                    int i = 0;
                    foreach (string value in FC["tockenvalueforText"].Split(','))
                    {

                        arraytoken[i] = value;
                        i++;
                    }

                    message = FC["TextBody"];

                }


                Android android = new Android(SiteSession.Apikey);
                android.SendNotification(message, arraytoken);
                ShowMessageBox(MessageType.Success, "Text Notification has been sent successfully!!", false);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Some problem has occurred while sending notification!!");
                return CreateModelStateErrors();
            }
            return RedirectToAction("Index");

        }

       
	}
}