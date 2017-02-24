using FluentValidation;
using MS.Web.Areas.Admin.Models;
using MS.Core.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;


namespace MS.Web.Code.Validation
{
    public class AdminLoginViewModelValidator : AbstractValidator<AdminLoginViewModel>
    {
        
        public AdminLoginViewModelValidator()
        {
            RuleFor(u => u.UserName).NotEmpty();
            RuleFor(u => u.Password).NotEmpty().WithMessage("*required");
        }

    }

}