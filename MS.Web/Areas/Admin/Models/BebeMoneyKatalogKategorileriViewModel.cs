using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MS.Web.Areas.Admin.Models
{
    public class BebeMoneyKatalogKategorileriViewModel
    {
        [DisplayName("Bebe Money Kategori ID")]
        public int BebeMoneyKategoriID { get; set; }

        [DisplayName("Kategori Adi")]
        public string BebeMoneyKategoriAdi { get; set; }


        [DisplayName("Kategori Tag")]
        public string KategoriTag { get; set; }

        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}