using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MS.Web.Areas.Admin.Models
{
    public class SallaKazanViewModel
    {
        [DisplayName("id")]
        public int id { get; set; }

        [DisplayName("Kampanya ID")]
        public int KampanyaID { get; set; }

        //[DisplayName("Urun Kodu")]
        //public decimal UrunKodu { get; set; }

        [DisplayName("Urun Adi")]
        public string UrunAdi { get; set; }

        [DisplayName("Urun Gorsel")]
        public string UrunGorsel { get; set; }

        [DisplayName("Kota")]
        public int Kota { get; set; }

        [DisplayName("Indirim Oran")]
        public int IndirimOran { get; set; }

        [DisplayFormat(DataFormatString =
           "{0:dd.MM.yyyy}",
            ApplyFormatInEditMode = true)]
        [DisplayName("Baslangic Tarih")]
        public DateTime BaslangicTarih { get; set; }

        [DisplayFormat(DataFormatString =
           "{0:dd.MM.yyyy}",
            ApplyFormatInEditMode = true)]
        [DisplayName("Bitis Tarih")]
        public DateTime BitisTarih { get; set; }

        [DisplayName("Durum")]
        public bool Durum { get; set; }

        [DisplayName("Metin")]
        public string Metin { get; set; }
    }
}