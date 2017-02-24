using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PushSharp.Android;
using PushSharp.Apple;
using PushSharp;

namespace MS.Web.Code.LIBS
{
    public class Android
    {
        private string apiKey;

        public Android(string _apiKey)
        {
            apiKey = _apiKey;
        }

        public void SendNotification(string message, params string[] registerIds)
        {
            //Create our push services broker
            var push = new PushBroker();
            

            //Registering the GCM Service and sending an Android Notification
            push.RegisterGcmService(new GcmPushChannelSettings(apiKey));
            //Fluent construction of an Android GCM Notification
            //IMPORTANT: For Android you MUST use your own RegistrationId here that gets generated within your Android app itself!

            var gcmNotification = new GcmNotification();
            push.QueueNotification(new GcmNotification().ForDeviceRegistrationId(registerIds)
                                  .WithJson("{\"message\":\"" + message + "\",\"badge\":0,\"sound\":\"default\"}"));
        }
    }
}