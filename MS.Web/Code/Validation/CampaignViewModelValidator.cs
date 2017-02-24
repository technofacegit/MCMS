using FluentValidation;
using MS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MS.Web.Code.Validation
{
    public class CampaignViewModelValidator : AbstractValidator<CampaignViewModel>
    {
        public CampaignViewModelValidator()
        {

           /// RuleFor(u => u.PromoNo).NotEmpty().WithMessage("*required");
           ///  RuleFor(u => u.OfferNo).NotEmpty().WithMessage("*required");
           /// RuleFor(u => u.OfferName).NotEmpty().WithMessage("*required");
          // RuleFor(u => u.CategoryTag).NotEmpty().WithMessage("Lütfen CategoryTag alanını doldurunuz.");
            RuleFor(u => u.CategoryId).NotEmpty().WithMessage("*Lütfen Kategori Tipini seçiniz.");
            ///RuleFor(u => u.EndDate).GreaterThan(DateTime.Now).WithMessage("Bitiş tarihi bugünden sonraki bir tarih olmalıdır.");
            RuleFor(u => u.PromoNo)
                         .Length(4, 20)
                         .WithMessage("Promo No alanı en az 4 karakter uzunluğunda olmalıdır.");
       
        }

    }
}