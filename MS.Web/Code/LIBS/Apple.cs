using PushSharp;
using PushSharp.Apple;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MS.Web.Code.LIBS
{
    public class Apple
    {
        private string p12File;
        private string password;

        public Apple(string _p12File, string _password)
        {
            p12File = _p12File;
            password = _password;
        }

        public void SendNotification(string message, string token)
        {
            //Create our push services broker
            var push = new PushBroker();

            //Registering the Apple Service and sending an iOS Notification
            var appleCert = File.ReadAllBytes(p12File);

            push.RegisterAppleService(new ApplePushChannelSettings(appleCert, password));

            push.QueueNotification(new AppleNotification()
                .ForDeviceToken(token)
                .WithAlert(message)
                .WithBadge(0)
                .WithSound("default"));
        }
    }
}