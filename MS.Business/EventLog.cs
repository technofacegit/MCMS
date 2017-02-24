using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Business
{
    [MetadataType(typeof(EventLog_MD))]
    public partial class EventLog
    {
        public static List<EventLog> GetEventLogs()
        {
            return Global.Context.EventLogs.ToList();
        }

        public static List<EventLog> GetEventLogsForRecord(int id,string type)
        {
            return Global.Context.EventLogs.Where(x => x.EventEntityID == id && (x.EventObjectType.Equals(type)==true)).ToList();
        }

        //public static Campaign GetCampaign(int id)
        //{
        //    return Global.Context.Campaigns.FirstOrDefault(x => x.CampaignID == id);
        //}

        //public static List<Campaign> GetCampaignByCategory(int id)
        //{
        //    return Global.Context.Campaigns.Where(x => x.CategoryId == id).ToList();
        //}
    }


    public class EventLog_MD
    {
        [DisplayName("Event ID")]
        public int EventID { get; set; }

        [DisplayName("Event ID")]
        public int EventEntityID { get; set; }

        [DisplayName("EventObjectType")]
        public string EventObjectType { get; set; }


        [DisplayName("EventData")]
        public string EventData { get; set; }

        [DisplayName("User ID")]
        public int UserID { get; set; }

        [DisplayName("Event Date")]
        public DateTime EventDate { get; set; }

        [DisplayName("Event Text")]
        public string EventText { get; set; }
    }
}
