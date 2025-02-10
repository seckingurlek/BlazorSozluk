using BlazorSozluk.Common.ViewModels.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.User
{
    public class LoginUserCommentValidator: AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommentValidator() 
        {
            RuleFor(i => i.EmailAdress)
                .NotNull()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("{PopertyName} not a valid email adress");


            RuleFor(i => i.Password)
                .NotNull()
                .MinimumLength(6).WithMessage("{PopertyName} should ar least be {MinLenght} characters");

        }


    }
}
