using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MS.Business;
using MS.Web.Code.LIBS;
using System.Collections;
using MS.Web.Code.Attributes;
using PushSharp;

namespace MS.Web.Areas.Admin.Conntrollers
{
    [AdminAuthorization]
    public class SendNotificationController : AdminBaseController
    {

        public ActionResult Index()
        {
            //var tblDevice = TblDevice.GetTblDevices();
            //return View(tblDevice);
            return View();
        }


        [HttpPost]
        public ActionResult SendCampNotifiaction(FormCollection FC)
        {
            try
            {
                string[] arraytoken = new string[50];
                string message = string.Empty;
                string FCvalue = string.Empty;
                string IOSToken = string.Empty;
                //FCvalue = FC["tockenvalueforcamp"].ToString();



                //if (FCvalue != null && FC["CampNO"] != null && FC["CampBody"] != null)
                //{
                //    int i = 0;
                //    foreach (string value in FCvalue.Split(','))
                //    {
                //        TblDevice TblDevice  =  TblDevice.GetTblDevicesByDeviceToken(value);
                //        if (TblDevice.ApplicationName.Contains("Migrosdroid"))
                //        {
                //            arraytoken[i] = value;
                //            i++;
                //        }
                //        else
                //        {
                //            IOSToken = value;
                //        }

                //    }

                //    message = FC["CampNO"];
                //    message += " " + FC["CampBody"];
                //}

                string s = Guid.NewGuid().ToString();
                var devices = Global.Context.spV4_AllDevicesWithTrackingNumber(s);

                if (FC["CampNO"] != null && FC["CampBody"] != null)
                {
                    message = FC["CampNO"];
                    message += " " + FC["CampBody"];
                    
                    foreach (var device in devices)
                    {
                        IOSToken = device.DeviceToken;

                        if (!string.IsNullOrEmpty(IOSToken))
                        {
                            //Apple apple = new Apple(Server.MapPath("~/Content/migros_test_adhoc.p12"), "99");
                            //apple.SendNotification(message, IOSToken);
                            PushSharp.PushNotificationService.PushNotificationService push = new PushSharp.PushNotificationService.PushNotificationService(s);
                            push.SendPushNotification(device.DeviceToken, message);
                        }
                    }
                }

                //if (arraytoken != null)
                //{
                //    Android android = new Android(SiteSession.Apikey);
                //    android.SendNotification(message, arraytoken);
                //}

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
                string IOSToken = string.Empty;


                if (FC["TextBody"] != null)
                {
                    message = FC["TextBody"];

                    string s = Guid.NewGuid().ToString();

                    int ix = Global.Context.spCMSNotifMaster(message,s);

                }

                //var devices = Global.Context.spV4_AllDevicesWithTrackingNumber(s);

                //if (FC["TextBody"] != null)
                //{
                //    message = FC["TextBody"];

                //    foreach (var device in devices)
                //    {
                //        IOSToken = device.DeviceToken;

                //        if (!string.IsNullOrEmpty(IOSToken))
                //        {
                //            PushSharp.PushNotificationService.PushNotificationService push = new PushSharp.PushNotificationService.PushNotificationService(s);
                //            push.SendPushNotification(device.DeviceToken, message);
                //        }
                //    }
                //}

                //if (FC["tockenvalueforText"] != null && FC["TextBody"] != null)
                //{
                //    int i = 0;
                //    foreach (string value in FC["tockenvalueforText"].Split(','))
                //    {
                //        TblDevice TblDevice = TblDevice.GetTblDevicesByDeviceToken(value);
                //        if (TblDevice.ApplicationName.Contains("Migrosdroid"))
                //        {
                //            arraytoken[i] = value;
                //            i++;
                //        }
                //        else
                //        {
                //            IOSToken = value;
                //        }

                //    }

                //    message = FC["TextBody"];

                //}

                //if (arraytoken != null)
                //{
                //    Android android = new Android(SiteSession.Apikey);
                //    android.SendNotification(message, arraytoken);
                //}

                //if (!string.IsNullOrEmpty(IOSToken))
                //{
                //    Apple apple = new Apple(Server.MapPath("~/Content/migros_test_adhoc.p12"), "99");
                //    apple.SendNotification(message, IOSToken);
                //}

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