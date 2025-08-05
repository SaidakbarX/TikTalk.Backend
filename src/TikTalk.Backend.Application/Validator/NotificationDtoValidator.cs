using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;

namespace TikTalk.Backend.Application.Validator;

public class NotificationDtoValidator : AbstractValidator<NotificationDto>
{
    public NotificationDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title kerak");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message kerak");
    }
}