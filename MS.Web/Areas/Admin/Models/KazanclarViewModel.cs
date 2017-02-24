using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MS.Web.Areas.Admin.Models
{
    public class KazanclarViewModel
    {
        [DisplayName("Kazanci ID")]
        public int KazanciID { get; set; }

        [DisplayName("KazancTipi")]
        public string KazancTipi { get; set; }

        [DisplayName("KazancTitle")]
        public string KazancTitle { get; set; }

        [DisplayName("KazancBackground")]
        public string KazancBackground { get; set; }

        [DisplayName("Item başlık")]
        public string ItemTitle { get; set; }

        [DisplayName("Offer Name")]
        public string OfferName { get; set; }

        [DisplayName("Kazanc Tag")]
        public string KazancTag { get; set; }

        [DisplayName("KazancOptinGerekliMi")]
        public bool KazancOptinGerekliMi { get; set; }

        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }

        [DisplayName("ShowTL")]
        public int ShowTL { get; set; }

        [DisplayName("Status")]
        public bool Status { get; set; }

        [DisplayName("Show Info")]
        public bool ShowInfo { get; set; }
    }
}