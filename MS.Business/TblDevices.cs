using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Business
{
   [MetadataType(typeof(TblDevice_MD))]
   public partial class TblDevice
    {
       public static List<TblDevice> GetTblDevices()
        {
            return Global.Context.TblDevices.Where(x=>x.DeviceToken != "(null)" && x.DeviceToken != "").ToList();
        }

       public static TblDevice GetTblDevice(int id)
        {
            return Global.Context.TblDevices.FirstOrDefault(x => x.DeviceID == id);
        }

       public static TblDevice GetTblDevicesByDeviceToken(string DeviceToken)
       {
           return Global.Context.TblDevices.FirstOrDefault(x => x.DeviceToken == DeviceToken);
       }

    }

   public class TblDevice_MD
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
       public DateTime LastLogTime { get; set;}

        [DisplayName("Log Count")]
       public int LogCount { get; set; }
   }
}
