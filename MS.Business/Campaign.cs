using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Business
{
     [MetadataType(typeof(Campaign_MD))]
    public partial class Campaign
    {
        public static List<Campaign> GetCampaigns()
        {
            return Global.Context.Campaigns.OrderByDescending(x => x.EndDate).ToList();
        }

        public static Campaign GetCampaign(int id)
        {
            return Global.Context.Campaigns.FirstOrDefault(x => x.CampaignID == id);
        }

        
        public static List<Campaign> GetCampaignByCategory(int id)
        {
            return Global.Context.Campaigns.Where(x => x.CategoryId == id).OrderByDescending(x => x.EndDate).ToList();
        }

        public static List<Campaign> GetMigroskop()
        {
            return Global.Context.Campaigns.Where(x => x.CategoryTag == "migroskop").OrderByDescending(x => x.EndDate).ToList();
        }
    }


     public class Campaign_MD
    {
        [DisplayName("Campaign ID")]
        public int CampaignID { get; set; }

        [DisplayName("Category ID")]
        [Required(ErrorMessage = "Required")]
        public int CategoryId { get; set; }


        [DisplayName("Category Tag")]
        public string CategoryTag { get; set; }

        [DisplayName("Image Link")]
        public string ImageLink { get; set; }

        [DisplayName("Image Link2")]
        public string ImageLink2 { get; set; }

        [DisplayName("Promo No")]
        public string PromoNo { get; set; }

        [DisplayName("Offer No")]
        public string OfferNo { get; set; }

        [DisplayName("Offer Name")]
        public string OfferName { get; set; }

        [DisplayName("Offer Desc")]
        public string OfferDesc { get; set; }

        [DisplayName("Discount")]
        public string Discount { get; set; }

        [DisplayName("Discount Type")]
        public string DiscountType { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString =
        //   "{0:dd.MM.yyyy HH:mm}",
        //    ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString =
        //   "{0:dd.MM.yyyy HH:mm}",
        //    ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }


        [DisplayName("Optin Flag")]
        public bool OptinFlag { get; set; }

        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }

        [DisplayName("Status")]
        public bool Status { get; set; }


        [DisplayName("Created By")]
        public int CreatedBy { get; set; }

    }
}
