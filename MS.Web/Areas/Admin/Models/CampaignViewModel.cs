using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MS.Web.Areas.Admin.Models
{
    public class CampaignViewModel
    {
        [DisplayName("Campaign ID")]
        public int CampaignID { get; set; }

        [DisplayName("Category ID")]
        [Required(ErrorMessage="Required")]
        public int? CategoryId { get; set; }


        [DisplayName("Category Tag")]
        public string CategoryTag { get; set; }

        [DisplayName("Image Link")]
        public string ImageLink { get; set; }

        [DisplayName("Image Link2")]
        public string ImageLink2 { get; set; }

        [DisplayName("Kampanya No - Promo No")]
        public string PromoNo { get; set; }

        [DisplayName("Teklif No - Offer No")]
        public string OfferNo { get; set; }

        [DisplayName("Teklif Adı")]
        public string OfferName { get; set; }

        [DisplayName("Teklif Detayı")]
        [AllowHtml]
        public string OfferDesc { get; set; }

        [DisplayName("Discount")]
        public string Discount { get; set; }

        [DisplayName("Discount Type")]
        public string DiscountType { get; set; }

        [DisplayName("Extra Data")]
        public string ExtraData { get; set; }

        [DisplayName("Start Date")]
        public string StartDate { get; set; }

        [DisplayName("Start Time")]
        public string StartTime { get; set; }

        [DisplayName("End Date")]
        public string EndDate { get; set; }

        [DisplayName("End Time")]
        public string EndTime { get; set; }


        [DisplayName("Optin Flag")]
        public bool OptinFlag { get; set; }

        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }

        [DisplayName("Status")]
        public bool Status { get; set; }

        [DisplayName("UpdateLogs")]
        public string UpdateLogs { get; set; }




    }
}