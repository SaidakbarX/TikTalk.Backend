using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;

namespace TikTalk.Backend.Application.Validator;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x => x.Provider)
            .NotEmpty().WithMessage("Provider bo‘sh bo‘lmasligi kerak");

        RuleFor(x => x.ProviderKey)
            .NotEmpty().WithMessage("ProviderKey bo‘sh bo‘lmasligi kerak");

        RuleFor(x => x.LoginTime)
            .NotEmpty().WithMessage("LoginTime noto‘g‘ri");

        // 🔐 Agar provider "email" bo‘lsa, parol majburiy
        When(x => x.Provider?.ToLower() == "email", () =>
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Email orqali login uchun parol kerak")
                .MinimumLength(6).WithMessage("Parol kamida 6 belgidan iborat bo‘lishi kerak");
        });
    }
}