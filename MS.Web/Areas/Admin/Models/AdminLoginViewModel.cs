using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MS.Core.Language;
using MS.Core.Language;

namespace MS.Web.Areas.Admin.Models
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessageResourceName = "requiredID", ErrorMessageResourceType = typeof(Resource_tr_TR))]
        public string UserName { get; set; }
        [Required(ErrorMessageResourceName = "requiredPassword", ErrorMessageResourceType = typeof(Resource_tr_TR))]
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}