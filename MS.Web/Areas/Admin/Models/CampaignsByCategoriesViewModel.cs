using MS.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MS.Web.Areas.Admin.Models
{
    public class CampaignsByCategoriesViewModel
    {
        
        [DisplayName("Category ID")]
        public int CategoryID { get; set; }

        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [DisplayName("Status")]
        public bool Status { get; set; }

        public IEnumerable<CampaignViewModel> Campaigns { get; set; }
    }
}