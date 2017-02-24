using FluentValidation;
using MS.Business;
using MS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;

namespace MS.Web.Code.Validation
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private static Dictionary<Type, IValidator> _validators = new Dictionary<Type, IValidator>();

        static ValidatorFactory()
        {
            _validators.Add(typeof(IValidator<AdminLoginViewModel>), new AdminLoginViewModelValidator());
            _validators.Add(typeof(IValidator<CampaignCategoryViewModel>), new CampaignCategoryViewModelValidator());
            _validators.Add(typeof(IValidator<KazanclarViewModel>), new KazanclarViewModelValidator());
            _validators.Add(typeof(IValidator<BebeMoneyKatalogKategorileriViewModel>), new BebeMoneyKatalogKategorileriViewModelValidator());
            _validators.Add(typeof(IValidator<MilKatalogUrunleriViewModel>), new MilKatalogUrunleriViewModelValidator());
            _validators.Add(typeof(IValidator<TblDeviceViewModel>), new TblDeviceViewModelValidator());
            _validators.Add(typeof(IValidator<SallaKazanViewModel>), new SallaKazanViewModelValidator());
        }

        /// <summary>
        /// Creates an instance of a validator with the given type.
        /// </summary>
        /// <param name="validatorType">Type of the validator.</param>
        /// <returns>The newly created validator</returns>
        public override IValidator CreateInstance(Type validatorType)
        {
            IValidator validator;
            if (_validators.TryGetValue(validatorType, out validator))
                return validator;
            return validator;
        }
    }
}