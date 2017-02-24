using FluentValidation;
using MS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MS.Web.Code.Validation
{
    public class BebeMoneyKatalogKategorileriViewModelValidator : AbstractValidator<BebeMoneyKatalogKategorileriViewModel>
    {
        public BebeMoneyKatalogKategorileriViewModelValidator()
        {
            RuleFor(u => u.BebeMoneyKategoriAdi).NotEmpty().WithMessage("*required");
            RuleFor(u => u.KategoriTag).NotEmpty().WithMessage("*required");
            

        }
    }
}