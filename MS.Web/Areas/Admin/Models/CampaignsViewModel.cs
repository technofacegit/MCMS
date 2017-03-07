using MS.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MS.Web.Areas.Admin.Models
{
    public class CampaignsViewModel
    {
        public IEnumerable<CampaignViewModel> Campaigns { get; set; }
    }
}