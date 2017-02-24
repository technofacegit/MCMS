using FluentValidation;
using MS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MS.Web.Code.Validation
{
    public class SallaKazanViewModelValidator : AbstractValidator<SallaKazanViewModel>
    {
        public SallaKazanViewModelValidator()
        {
            RuleFor(u => u.KampanyaID).GreaterThan(0).WithMessage("KampanyaID geçerli bir numara olmalı");
            ////RuleFor(u => u.UrunKodu).GreaterThan(0).WithMessage("UrunKodu geçerli bir kod olmalı !");
            RuleFor(u => u.UrunAdi).NotEmpty().WithMessage(Core.Language.Resource_tr_TR.required);
        }
    }
}