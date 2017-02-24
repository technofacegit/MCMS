using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MS.Web.Areas.Admin.Models
{
    public class MilKatalogUrunleriViewModel
    {
        [DisplayName("MilKatalogUrunID")]
        public int MilKatalogUrunID { get; set; }

        [DisplayName("Marka")]
        public string Marka { get; set; }

        [DisplayName("Urun Adi")]
        public string UrunAdi { get; set; }

        [DisplayName("Urun Gorsel")]
        public string UrunGorsel { get; set; }

        [DisplayName("Kampanya Metin")]
        public string KampanyaMetin { get; set; }


        [DisplayName("Kampanya ID")]
        public string KampanyaID { get; set; }

        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}