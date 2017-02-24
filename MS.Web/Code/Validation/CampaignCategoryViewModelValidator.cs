using FluentValidation;
using MS.Business;
using MS.Web.Areas.Admin.Models;
using MS.Core.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;

namespace MS.Web.Code.Validation
{
    public class CampaignCategoryViewModelValidator : AbstractValidator<CampaignCategoryViewModel>
    {
        public CampaignCategoryViewModelValidator()
        {
            RuleFor(u => u.CategoryName).NotEmpty().WithMessage("*required");
            RuleFor(u => u.CategoryTag).NotEmpty().WithMessage("*required");
            RuleFor(u => u.CategoryType).NotEmpty().WithMessage("*required");
            RuleFor(u => u.CategoryType).GreaterThan(0).WithMessage("Please Select Category Type");
        }
     }
}