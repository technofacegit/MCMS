using MS.Web.Code.LIBS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MS.Web.Areas.Admin.Models
{
    public class AdminNotificationViewModel
    {

        public AdminNotificationViewModel(MessageType messageType, string message)
        {
            this.MessageType = messageType;
            this.Message = message;

        }

        public string Message { get; set; }
        public MessageType MessageType { get; set; }
    }
}