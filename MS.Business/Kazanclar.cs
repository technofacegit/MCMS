using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.Core.Language;

namespace MS.Business
{
    [MetadataType(typeof(Kazanclar_MD))]
    public partial class Kazanclar
    {
        public static List<Kazanclar> GetKazanclars()
        {
            return Global.Context.Kazanclars.ToList();
        }

        public static Kazanclar GetKazanclar(int id)
        {
            return Global.Context.Kazanclars.FirstOrDefault(x => x.KazanciID == id);
        }


    }

    public class Kazanclar_MD
    {
        [DisplayName("Kazanci ID")]
        public int KazanciID { get; set; }

        [Display(Name = "KazancTipi", ResourceType = typeof(Resource_tr_TR))]
        public string KazancTipi { get; set; }

        [Display(Name = "KazancTitle", ResourceType = typeof(Resource_tr_TR))]
        public string KazancTitle { get; set; }

        [Display(Name = "KazancBackground", ResourceType = typeof(Resource_tr_TR))]
        public string KazancBackground { get; set; }

        [Display(Name = "Itembaşlık", ResourceType = typeof(Resource_tr_TR))]
        public string ItemTitle { get; set; }

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
