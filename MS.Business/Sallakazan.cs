using MS.Core.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Business
{
     [MetadataType(typeof(SallaKazan_MD))]
    public partial class SallaKazan
    {
       public static List<SallaKazan> GetSallakazans()
       {
           return Global.Context.SallaKazans.OrderByDescending(x => x.id).ToList();
       }

       public static SallaKazan GetSallaKazan(int id)
       {
           return Global.Context.SallaKazans.FirstOrDefault(x => x.id == id);
       }

       public static int AktifSallKazanVarmi(DateTime dtBaslangic,DateTime dtBitis)
       {
           int bitisControl=Global.Context.SallaKazans.Count(x => dtBitis <= x.BitisTarih && dtBitis >= x.BaslangicTarih && x.Durum==true);
           int baslangicControl = Global.Context.SallaKazans.Count(x => dtBaslangic <= x.BitisTarih && dtBitis >= x.BitisTarih && x.Durum == true);
           return bitisControl + baslangicControl;
       }


    
    }

     public class SallaKazan_MD
     {
         [DisplayName("id")]
         public int id { get; set; }

         [DisplayName("Kampanya ID")]
         public int KampanyaID { get; set; }

         [DisplayName("Urun Kodu")]
         public decimal UrunKodu { get; set; }

         [Display(Name = "UrunAdi", ResourceType = typeof(Resource_tr_TR))]
         public string UrunAdi { get; set; }

         [DisplayName("Urun Gorsel")]
         public string UrunGorsel { get; set; }

         [DisplayName("Kota")]
         public int Kota { get; set; }

         [Display(Name = "IndirimOran", ResourceType = typeof(Resource_tr_TR))]
         public int IndirimOran { get; set; }

         [DataType(DataType.Date)]
         [DisplayFormat(DataFormatString =
            "{0:dd.MM.yyyy}",
             ApplyFormatInEditMode = true)]
         [DisplayName("Baslangic Tarih")]
         public DateTime BaslangicTarih { get; set; }

         [DataType(DataType.Date)]
         [DisplayFormat(DataFormatString =
            "{0:dd.MM.yyyy}",
             ApplyFormatInEditMode = true)]
         [Display(Name = "BitisTarih", ResourceType = typeof(Resource_tr_TR))]
         public DateTime BitisTarih { get; set; }

         [DisplayName("Durum")]
         public bool Durum { get; set; }

         [DisplayName("Metin")]
         public string Metin { get; set; }


     }
}
