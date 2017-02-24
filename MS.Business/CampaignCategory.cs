using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Business
{
     [MetadataType(typeof(CampaignCategory_MD))]
    public partial class CampaignCategory
    {
        public static List<CampaignCategory> GetCampaignCategories()
        {
            return Global.Context.CampaignCategories.ToList();
        }

        public static CampaignCategory GetCampaignCategory(int id)
        {
            return Global.Context.CampaignCategories.FirstOrDefault(x => x.CategoryID == id);
        }
    }


    public class CampaignCategory_MD
    {
        [DisplayName("Category ID")]
        public int CategoryID { get; set; }


        [DisplayName("Category Name")]
       
        public string CategoryName { get; set; }

        [DisplayName("Category Tag")]
        public string CategoryTag { get; set; }

        [DisplayName("Category Image")]
        public string CategoryImage { get; set; }

        [DisplayName("Category Background")]
        public string CategoryBackground { get; set; }

        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }

        [DisplayName("Status")]
        public bool Status { get; set; }

        [DisplayName("Category Type")]
        public int CategoryType { get; set; }

        [DisplayName("Visible On Main Page")]
        public bool VisibleOnMainPage { get; set; }

        [DisplayName("Visible On Camp Page")]
        public bool VisibleOnCampPage { get; set; }

        [DisplayName("Visible On Not Login")]
        public bool VisibleOnNotLogin { get; set; }


        [DisplayName("Show Login Panel")]
        public bool ShowLoginPanel { get; set; }

        [DisplayName("Cache Key")]
        public bool CacheKey { get; set; }

        [DisplayName("Created By")]
        public int CreatedBy { get; set; }

        [DisplayName("New Category Background")]
        public string NewCategoryBackground { get; set; }

        [DisplayName("New Category Board Image")]
        public string NewCategoryBoardImage { get; set; }

        [DisplayName("Optin Text")]
        public string OptinText { get; set; }

        [DisplayName("OptinHoverText")]
        public string OptinHoverText { get; set; }
    }
}
