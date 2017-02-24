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
    public class KazanclarViewModelValidator : AbstractValidator<KazanclarViewModel>
    {
        public KazanclarViewModelValidator()
        {
            RuleFor(u => u.KazancTitle).NotEmpty().WithMessage("*required");
            RuleFor(u => u.KazancTag).NotEmpty().WithMessage("*required");
        }
    }
}