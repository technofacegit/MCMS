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
using System.Globalization;
using System.IO;
using MS.Web.Models.Others;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MS.Web.Areas.Admin.Conntrollers
{
    [AdminAuthorization]
    public class SendNotificationController : AdminBaseController
    {

        public ActionResult Index()
        {
            //var tblDevice = TblDevice.GetTblDevices();
            //return View(tblDevice);

            var bildirimler = Global.Context.TblCMSNotifMaster.ToList().OrderByDescending(x => x.NotifDate).Take(10).ToList();
            return View(bildirimler);


            ///return View();
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

                if ( FC["CampNO"] != null && FC["OfferNo"] != null && FC["CampBody"] != null)
                {
                    message = FC["CampBody"];

                    String dtNotification = FC["dtNotificationCamp"];
                     String promoNo = FC["Campno"];
                    String offerNo = FC["OfferNo"];

                    DateTime dtNot = DateTime.ParseExact(dtNotification.ToString(), "d.M.yyyy HH:mm", CultureInfo.InvariantCulture);


                    string s = Guid.NewGuid().ToString();

                    int ix = Global.Context.spCMSNotifMaster(message, s, dtNot,promoNo,offerNo,"Kampanya");
                }


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

                //string s = Guid.NewGuid().ToString();
                //var devices = Global.Context.spV4_AllDevicesWithTrackingNumber(s);

                //if (FC["CampNO"] != null && FC["CampBody"] != null)
                //{
                //    message = FC["CampNO"];
                //    message += " " + FC["CampBody"];
                    
                //    foreach (var device in devices)
                //    {
                //        IOSToken = device.DeviceToken;

                //        if (!string.IsNullOrEmpty(IOSToken))
                //        {
                //            //Apple apple = new Apple(Server.MapPath("~/Content/migros_test_adhoc.p12"), "99");
                //            //apple.SendNotification(message, IOSToken);
                //            PushSharp.PushNotificationService.PushNotificationService push = new PushSharp.PushNotificationService.PushNotificationService(s);
                //            push.SendPushNotification(device.DeviceToken, message);
                //        }
                //    }
                //}

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
            //try
            //{
              





                string[] arraytoken = new string[50];
                string message = null;
                string IOSToken = string.Empty;


                if (FC["TextBody"] != null)
                {
                    message = FC["TextBody"];

                    String dtNotification = FC["dtNotification"];

                    DateTime dtNot = DateTime.ParseExact(dtNotification.ToString(), "d.M.yyyy HH:mm", CultureInfo.InvariantCulture);
                       

                    string s = Guid.NewGuid().ToString();

                    
                    // if (Request.Files.Count == 0)
                    //{
                         int ix = Global.Context.spCMSNotifMaster(message, s, dtNot,"0","0","Genel");
                     ///}else
                    if (Request.Files.Count > 0)
                    {
                        
                        var file = Request.Files[0];

                        if (file != null && file.ContentLength > 0)
                        {
                            ix = Global.Context.spCMSNotifMaster(message, s, dtNot,"0","0","Kitle");


                            Global.Context.spCMSClearNotifQueue(s);



                            //Create a DataTable.
                            DataTable dt = new DataTable();
                            dt.Columns.AddRange(new DataColumn[2] { 
                                new DataColumn("CardNumber", typeof(string)),
                                new DataColumn("NotificationKey",typeof(string)) });


                            List<DataTable> bulkDts = new List<DataTable>();
                            for (int i = 0; i < 50; i++)
                            {
                                bulkDts.Add(dt.Clone());
                            }

                            int j = 0;
                            StreamReader reader = new StreamReader(file.InputStream);
                            do
                            {


                                String line = reader.ReadLine();
                                int tblIndex = j / 30000;


                                DataRow dr = bulkDts[tblIndex].NewRow();
                                
                                    dr["CardNumber"] = line;
                                    dr["NotificationKey"] = s;

                               
                                bulkDts[tblIndex].Rows.Add(dr);

                                j++;
                                //TblCMSDeviceTmp devicetmp = new TblCMSDeviceTmp();
                                //devicetmp.CardNumber = line;
                                //devicetmp.NotificationKey = s;
                                //devicetmp.Save();
                                ///Global.Context.TblCMSDeviceTmp.AddObject(devicetmp);

                                ////Global.Context.spCMSAddNotifDeviceWithCardNumber(line, s);
                            } while (reader.Peek() != -1);
                            reader.Close();






                            string conString = ConfigurationManager.ConnectionStrings["migrosiphoneConnectionString"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(conString))
                            {
                                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                                {
                                    //Set the database table name.
                                    sqlBulkCopy.DestinationTableName = "TblCMSDeviceTmp";
                                    sqlBulkCopy.BulkCopyTimeout = 90;

                                    //[OPTIONAL]: Map the DataTable columns with that of the database table
                                    ///sqlBulkCopy.ColumnMappings.Add("Id", "CustomerId");
                                    sqlBulkCopy.ColumnMappings.Add("CardNumber", "CardNumber");
                                    sqlBulkCopy.ColumnMappings.Add("NotificationKey", "NotificationKey");

                                    con.Open();
                                    for (int x = 0; x < bulkDts.Count; x++) {
                                        if (bulkDts[x].Rows.Count > 0) {
                                            sqlBulkCopy.WriteToServer(bulkDts[x]);
                                        }
                                    }
                                    con.Close();
                                }
                            }




                           /// Global.Context.spCMSAddTmpToNotifQueue(s);

                            //var fileName = Server.MapPath.GetFileName(file.FileName);
                            //var path =Server.MapPath.Combine(Server.MapPath("~/Images/"), fileName);
                            //file.SaveAs(path);
                        }
                    }

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
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError("", "Some problem has occurred while sending notification!!"+ex.StackTrace);
            //    return CreateModelStateErrors();
            //}
            return RedirectToAction("Index");

        }



        [HttpGet]
        public ActionResult delete(string notifKey)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure want to delete this Notification ?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Title = "Delete Notification" },
                Footer = new ModalFooter
                {
                    SubmitButtonText = "Yes",
                    CancelButtonText = "No"
                }
            });
        }

        [HttpPost]
        public ActionResult delete(string notifKey, FormCollection FC)
        {
            try
            {

                Global.Context.spCMSDeleteNotifQueue(notifKey);
                ShowMessageBox(MessageType.Success, "Bildirim has been deleted successfully!!", false);
            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                {
                    message = "You can't delete this because it has some child records.";
                }
                ShowMessageBox(MessageType.Danger, message, false);
            }

            return RedirectToAction("Index");
        }


    }
}