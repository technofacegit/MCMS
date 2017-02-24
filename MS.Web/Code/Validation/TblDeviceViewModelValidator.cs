using FluentValidation;
using MS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MS.Web.Code.Validation
{
    public class TblDeviceViewModelValidator : AbstractValidator<TblDeviceViewModel>
    {
        public TblDeviceViewModelValidator()
        {

            RuleFor(u => u.DeviceToken).NotEmpty().WithMessage("*required");
            RuleFor(u => u.Lang).NotEmpty().WithMessage("*required");
            RuleFor(u => u.ApplicationName).NotEmpty().WithMessage("*required");
            RuleFor(u => u.OSVersion).NotEmpty().WithMessage("*required");   
            RuleFor(u => u.MachineName).NotEmpty().WithMessage("*required");
            RuleFor(u => u.UserAgent).NotEmpty().WithMessage("*required");
            RuleFor(u => u.Country).NotEmpty().WithMessage("*required");

        }

    }
}