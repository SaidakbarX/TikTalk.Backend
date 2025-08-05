using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;

namespace TikTalk.Backend.Application.Validator;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Ism to‘ldirilishi shart")
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email to‘ldirilishi kerak")
            .EmailAddress().WithMessage("Email noto‘g‘ri formatda");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Parol bo‘sh bo‘lmasligi kerak")
            .MinimumLength(6).WithMessage("Parol kamida 6 belgidan iborat bo‘lishi kerak");
    }
}