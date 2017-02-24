using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Business
{
     [MetadataType(typeof(MilKatalogUrunleri_MD))]
   public partial class MilKatalogUrunleri
    {
       public static List<MilKatalogUrunleri> GetMilKatalogUrunleries()
        {
            return Global.Context.MilKatalogUrunleris.ToList();
        }

       public static MilKatalogUrunleri GetMilKatalogUrunleri(int id)
        {
            return Global.Context.MilKatalogUrunleris.FirstOrDefault(x => x.MilKatalogUrunID == id);
        }
    }

     public class MilKatalogUrunleri_MD
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
