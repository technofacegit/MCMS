using PushSharp.Apple;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MS.Business;

namespace PushSharp
{
    public class PushNotificationApple
    {
        private static PushBroker   _pushBroker;
        private AppleNotification   appNotif;
        private String              trackingNumber;
        private ProjectDbContext context = MS.Business.Global.Context;

        public PushNotificationApple(String vlTrackingNumber)
        {
            trackingNumber = vlTrackingNumber;
            if (_pushBroker == null)
            {
                //Create our push services broker
                _pushBroker = new PushBroker();

                //Wire up the events for all the services that the broker registers
                _pushBroker.OnNotificationSent += NotificationSent;
                _pushBroker.OnChannelException += ChannelException;
                _pushBroker.OnServiceException += ServiceException;
                _pushBroker.OnNotificationFailed += NotificationFailed;
                _pushBroker.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
                _pushBroker.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
                _pushBroker.OnChannelCreated += ChannelCreated;
                _pushBroker.OnChannelDestroyed += ChannelDestroyed;

                //-------------------------
                // APPLE NOTIFICATIONS
                //-------------------------
                //Configure and start Apple APNS
                // IMPORTANT: Make sure you use the right Push certificate.  Apple allows you to generate one for connecting to Sandbox,
                //   and one for connecting to Production.  You must use the right one, to match the provisioning profile you build your
                //   app with!
                // Make sure you provide the correct path to the certificate, in my case this is how I did it in a WCF service under Azure,
                // but in your case this might be different. Putting the .p12 certificate in the main directory of your service 
                // (in case you have a webservice) is never a good idea, people can download it from there..
                //System.Web.Hosting.HostingEnvironment.MapPath("~/folder/file");
                //var appleCert = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../Resources/PushSharp.Apns.Sandbox.p12"));
                var appleCert = File.ReadAllBytes(System.Web.Hosting.HostingEnvironment.MapPath("~/Code/Cert/migros_push_appstore.p12"));

                //IMPORTANT: If you are using a Development provisioning Profile, you must use the Sandbox push notification server 
                //  (so you would leave the first arg in the ctor of ApplePushChannelSettings as 'false')
                //  If you are using an AdHoc or AppStore provisioning profile, you must use the Production push notification server
                //  (so you would change the first arg in the ctor of ApplePushChannelSettings to 'true')
                _pushBroker.RegisterAppleService(new ApplePushChannelSettings(true, appleCert, "99",true)); //Extension method
            }
        }

        public PushNotificationApple()
        {
            if (_pushBroker == null)
            {
                //Create our push services broker
                _pushBroker = new PushBroker();

                //Wire up the events for all the services that the broker registers
                _pushBroker.OnNotificationSent += NotificationSent;
                _pushBroker.OnChannelException += ChannelException;
                _pushBroker.OnServiceException += ServiceException;
                _pushBroker.OnNotificationFailed += NotificationFailed;
                _pushBroker.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
                _pushBroker.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
                _pushBroker.OnChannelCreated += ChannelCreated;
                _pushBroker.OnChannelDestroyed += ChannelDestroyed;

                //-------------------------
                // APPLE NOTIFICATIONS
                //-------------------------
                //Configure and start Apple APNS
                // IMPORTANT: Make sure you use the right Push certificate.  Apple allows you to generate one for connecting to Sandbox,
                //   and one for connecting to Production.  You must use the right one, to match the provisioning profile you build your
                //   app with!
                // Make sure you provide the correct path to the certificate, in my case this is how I did it in a WCF service under Azure,
                // but in your case this might be different. Putting the .p12 certificate in the main directory of your service 
                // (in case you have a webservice) is never a good idea, people can download it from there..
                //System.Web.Hosting.HostingEnvironment.MapPath("~/folder/file");
                //var appleCert = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../Resources/PushSharp.Apns.Sandbox.p12"));
                var appleCert = File.ReadAllBytes(System.Web.Hosting.HostingEnvironment.MapPath("~/Code/Cert/migros_push_appstore.p12"));

                //IMPORTANT: If you are using a Development provisioning Profile, you must use the Sandbox push notification server 
                //  (so you would leave the first arg in the ctor of ApplePushChannelSettings as 'false')
                //  If you are using an AdHoc or AppStore provisioning profile, you must use the Production push notification server
                //  (so you would change the first arg in the ctor of ApplePushChannelSettings to 'true')
                _pushBroker.RegisterAppleService(new ApplePushChannelSettings(true, appleCert, "99")); //Extension method
            }
        }

        public void SendNotification(string deviceToken, string message)
        {
            //Fluent construction of an iOS notification
            //IMPORTANT: For iOS you MUST MUST MUST use your own DeviceToken here that gets generated within your iOS app itself when the Application Delegate
            //  for registered for remote notifications is called, and the device token is passed back to you
            if (_pushBroker != null)
            {
                
                appNotif = new AppleNotification()
                                           .ForDeviceToken(deviceToken)
                                           .WithAlert(message)
                                           .WithBadge(1)
                                           .WithSound("sound.caf");

                _pushBroker.QueueNotification(appNotif);
            
               
            }
        }

        private void ChannelDestroyed(object sender)
        {
            string s = "channel destroyed";
            ///HttpContext.Current.Response.Write("ChannelDestroyed:" + s);
        }
        private void ChannelCreated(object sender, PushSharp.Core.IPushChannel pushChannel)
        {
            String s = pushChannel.ToString();
          //  HttpContext.Current.Response.Write("ChannelCreated:" + s);
        }
        private void DeviceSubscriptionChanged(object sender, string oldSubscriptionId, string newSubscriptionId, PushSharp.Core.INotification notification)
        {
            String s = newSubscriptionId;
          //  HttpContext.Current.Response.Write("DeviceSubscriptionChanged:" + s);

            //DataSet1TableAdapters.spV4_AllDevicesWithTrackingNumberTableAdapter deviceAdapt
            //= new DataSet1TableAdapters.spV4_AllDevicesWithTrackingNumberTableAdapter();
            //deviceAdapt.spV4_AddNotificationLog(trackingNumber, oldSubscriptionId, "", "DEVICESUBSCRIPTIONCHANGED", "CHANGE");  
            context.spV4_AddNotificationLog(trackingNumber, oldSubscriptionId, "", "DEVICESUBSCRIPTIONCHANGED", "CHANGE"); 
        }
        private void DeviceSubscriptionExpired(object sender, string expiredSubscriptionId, DateTime expirationDateUtc, PushSharp.Core.INotification notification)
        {
            String s = expiredSubscriptionId;
          //  HttpContext.Current.Response.Write("DeviceSubscriptionExpired:" + s);

            //DataSet1TableAdapters.spV4_AllDevicesWithTrackingNumberTableAdapter deviceAdapt
            //= new DataSet1TableAdapters.spV4_AllDevicesWithTrackingNumberTableAdapter();
            //deviceAdapt.spV4_AddNotificationLog(trackingNumber, expiredSubscriptionId, "", "DEVICESUBSCRIPTIONEXPIRED", "EXCEPTION");  
            context.spV4_AddNotificationLog(trackingNumber, expiredSubscriptionId, "", "DEVICESUBSCRIPTIONEXPIRED", "EXCEPTION"); 
        }
        private void NotificationFailed(object sender, PushSharp.Core.INotification notification, Exception error)
        {
            String s = error.Message;
          //  HttpContext.Current.Response.Write("NotificationFailed:" + s);

            //DataSet1TableAdapters.spV4_AllDevicesWithTrackingNumberTableAdapter deviceAdapt
            //= new DataSet1TableAdapters.spV4_AllDevicesWithTrackingNumberTableAdapter();
            //deviceAdapt.spV4_AddNotificationLog(trackingNumber, ((PushSharp.Apple.AppleNotification)(notification)).DeviceToken, error.Message, "NOTIFICATIONFAILED", "EXCEPTION");  
            context.spV4_AddNotificationLog(trackingNumber, ((PushSharp.Apple.AppleNotification)(notification)).DeviceToken, error.Message, "NOTIFICATIONFAILED", "EXCEPTION"); 
        }
        private void ServiceException(object sender, Exception error)
        {
            String s = error.Message;
          //  HttpContext.Current.Response.Write("ServiceException:" + s);

            //DataSet1TableAdapters.spV4_AllDevicesWithTrackingNumberTableAdapter deviceAdapt
            // = new DataSet1TableAdapters.spV4_AllDevicesWithTrackingNumberTableAdapter();
            //deviceAdapt.spV4_AddNotificationLog(trackingNumber, "", error.Message, "SERVICEEXCEPTION", "EXCEPTION");  
            context.spV4_AddNotificationLog(trackingNumber, "", error.Message, "SERVICEEXCEPTION", "EXCEPTION");  
           
        }
        private void ChannelException(object sender, PushSharp.Core.IPushChannel pushChannel, Exception error)
        {
            String s = error.Message;
          //  HttpContext.Current.Response.Write("ChannelException:" + s);

            //DataSet1TableAdapters.spV4_AllDevicesWithTrackingNumberTableAdapter deviceAdapt
            //  = new DataSet1TableAdapters.spV4_AllDevicesWithTrackingNumberTableAdapter();
            //deviceAdapt.spV4_AddNotificationLog(trackingNumber,"", error.Message, "CHANNELEXCEPTION", "EXCEPTION");   
            context.spV4_AddNotificationLog(trackingNumber, "", error.Message, "CHANNELEXCEPTION", "EXCEPTION");   
        }
        private void NotificationSent(object sender, PushSharp.Core.INotification notification)
        {
            String s = notification.ToString();

            //DataSet1TableAdapters.spV4_AllDevicesWithTrackingNumberTableAdapter deviceAdapt 
            //    = new DataSet1TableAdapters.spV4_AllDevicesWithTrackingNumberTableAdapter();
            //deviceAdapt.spV4_AddNotificationLog(trackingNumber, ((PushSharp.Apple.AppleNotification)(notification)).DeviceToken, appNotif.Payload.ToJson(), "SUCCEEDED", "SUCCESS");
            context.spV4_AddNotificationLog(trackingNumber, ((PushSharp.Apple.AppleNotification)(notification)).DeviceToken, appNotif.Payload.ToJson(), "SUCCEEDED", "SUCCESS");
           // HttpContext.Current.Response.Write("NotificationSent:" +s);
        }
    }
}