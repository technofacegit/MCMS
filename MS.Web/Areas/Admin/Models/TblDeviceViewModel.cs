using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MS.Web.Areas.Admin.Models
{
    public class TblDeviceViewModel
    {
       
            [DisplayName("Device ID")]
            public int DeviceID { get; set; }

            [DisplayName("Device Token")]
            public string DeviceToken { get; set; }

            [DisplayName("Application Name")]
            public string ApplicationName { get; set; }

            [DisplayName("OS Version")]
            public string OSVersion { get; set; }

            [DisplayName("Registeration Time")]
            public DateTime RegisterationTime { get; set; }

            [DisplayName("Unique ID")]
            public string UniqueID { get; set; }

            [DisplayName("Machine Name")]
            public string MachineName { get; set; }

            [DisplayName("User Agent")]
            public string UserAgent { get; set; }

            [DisplayName("Language")]
            public string Lang { get; set; }

            [DisplayName("Country")]
            public string Country { get; set; }

            [DisplayName("LastLog Time")]
            public DateTime LastLogTime { get; set; }

            [DisplayName("Log Count")]
            public int LogCount { get; set; }
        
    }
}