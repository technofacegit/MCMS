using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushSharp
{
    namespace PushNotificationService
    {
        public class PushNotificationService
        {
            private static PushNotificationApple _pushNotificationApple;
            private static String trackingNumber;

            public PushNotificationService()
            {
                if (_pushNotificationApple == null)
                {
                    _pushNotificationApple = new PushNotificationApple();
                }
            }


            public PushNotificationService(String vlTrackingNumber)
            {
                if (_pushNotificationApple == null)
                {
                    _pushNotificationApple = new PushNotificationApple(vlTrackingNumber);
                }
            }
            /// <summary>
            /// Send the push notification to the device
            /// </summary>
            /// <param name="deviceToken"></param>
            /// <param name="message"></param>
            /// <returns></returns>
            public bool SendPushNotification(string deviceToken, string message)
            {
                if (_pushNotificationApple != null)
                {
                    _pushNotificationApple.SendNotification(deviceToken, message);
                }
                return true;
            }
        }
    }
}