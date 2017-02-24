using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Business
{
    [MetadataType(typeof(Magazalar_MD))]
    public partial class Magazalar
    {
        public static List<Magazalar> GetMagazalar()
        {
            return Global.Context.Magazalar.ToList();
        }

        public static Magazalar GetMagazalar(int id)
        {
            return Global.Context.Magazalar.FirstOrDefault(x => x.MagazaID == id);
        }
    }

    public class Magazalar_MD
    {
        [DisplayName("Magaza ID")]
        public int MagazaID { get; set; }

        [DisplayName("Sira")]
        public float? Sira { get; set; }

        [DisplayName("Magaza No")]
        public float? MagazaNo { get; set; }

        [DisplayName("Magaza Adi")]
        public string MagazaAdi { get; set; }

        [DisplayName("Sube")]
        public string Sube { get; set; }

        [DisplayName("Marka Adi")]
        public string MarkaAdi { get; set; }

        [DisplayName("Format Adi")]
        public string FormatAdi { get; set; }

        [DisplayName("Durum")]
        public string Durum { get; set; }

        [DisplayName("Acilis Tarihi")]
        public DateTime? AcilisTarihi { get; set; }

        [DisplayName("Jet Kasa")]
        public float? JetKasa { get; set; }

        [DisplayName("X Koordinat")]
        public float? Xkoordinat { get; set; }

        [DisplayName("Y Koordinat")]
        public float? Ykoordinat { get; set; }

        [DisplayName("Tel")]
        public float? Tel { get; set; }

        [DisplayName("Tel2")]
        public float? Tel2 { get; set; }

        [DisplayName("Tel3")]
        public string Tel3 { get; set; }

        [DisplayName("Tel4")]
        public string Tel4 { get; set; }

        [DisplayName("Adres")]
        public string Adres { get; set; }

        [DisplayName("İl")]
        public string Il { get; set; }

        [DisplayName("İlçe")]
        public string Ilce { get; set; }

        [DisplayName("Acilis Pazar")]
        public DateTime? AcilisPazar { get; set; }

        [DisplayName("Kapanis")]
        public DateTime? Kapanis { get; set; }

        [DisplayName("Kapanis Pazar")]
        public DateTime? KapanisPazar { get; set; }

        [DisplayName("Unlu Mamüller")]
        public string UnluMamuller { get; set; }

        [DisplayName("Kuruyemis")]
        public string Kuruyemis { get; set; }

        [DisplayName("Meze ve Hazır Yemek")]
        public string MezeveHazirYemek { get; set; }

        [DisplayName("Balikci")]
        public string Balikci { get; set; }

        [DisplayName("BiletSatisi")]
        public string BiletSatisi { get; set; }

        [DisplayName("Cafe")]
        public string Cafe { get; set; }

        [DisplayName("İletişim")]
        public string Iletisim { get; set; }

        [DisplayName("Eczane")]
        public string Eczane { get; set; }

        [DisplayName("FastFood")]
        public string FastFood { get; set; }

        [DisplayName("Kitabevi")]
        public string Kitabevi { get; set; }

        [DisplayName("Kurutemizleme")]
        public string Kurutemizleme { get; set; }

        [DisplayName("Müzik Market")]
        public string MuzikMarket { get; set; }

        [DisplayName("ATM")]
        public string ATM { get; set; }

        [DisplayName("Cevreci Kiosk")]
        public string CevreciKiosk { get; set; }

        [DisplayName("Eglence Dunyam")]
        public string EglenceDunyam { get; set; }

        [DisplayName("MigrosTASATM")]
        public string MigrosTASATM { get; set; }

        [DisplayName("Fatura Tahsilat")]
        public string FaturaTahsilat { get; set; }

        [DisplayName("Hediyem Kart")]
        public string HediyemKart { get; set; }

    }
}
