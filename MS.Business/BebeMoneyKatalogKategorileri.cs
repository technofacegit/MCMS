using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Business
{
      [MetadataType(typeof(BebeMoneyKatalogKategorileri_MD))]
   public partial class BebeMoneyKatalogKategorileri
    {
       public static List<BebeMoneyKatalogKategorileri> GetBebeMoneyKatalogKategorileries()
        {
            return Global.Context.BebeMoneyKatalogKategorileris.ToList();
        }

       public static BebeMoneyKatalogKategorileri GetBebeMoneyKatalogKategorileri(int id)
        {
            return Global.Context.BebeMoneyKatalogKategorileris.FirstOrDefault(x => x.BebeMoneyKategoriID == id);
        }


    }


      public class BebeMoneyKatalogKategorileri_MD
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
