using FluentValidation;
using MS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MS.Web.Code.Validation
{
    public class MilKatalogUrunleriViewModelValidator : AbstractValidator<MilKatalogUrunleriViewModel>
    {
        public MilKatalogUrunleriViewModelValidator()
        {
            RuleFor(u => u.UrunAdi).NotEmpty().WithMessage("*required");
            RuleFor(u => u.Marka).NotEmpty().WithMessage("*required");
            RuleFor(u => u.KampanyaMetin).NotEmpty().WithMessage("*required");
            RuleFor(u => u.KampanyaID).NotEmpty().WithMessage("*required");
        }
    }
}