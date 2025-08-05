using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;

namespace TikTalk.Backend.Application.Validator;

public class UpdateVideoDtoValidator : AbstractValidator<UpdateVideoDto>
{
    public UpdateVideoDtoValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.Title));

        RuleFor(x => x.ThumbnailUrl)
            .Must(url => url.StartsWith("http"))
            .When(x => !string.IsNullOrWhiteSpace(x.ThumbnailUrl))
            .WithMessage("To‘g‘ri thumbnail URL kiriting");
    }
}