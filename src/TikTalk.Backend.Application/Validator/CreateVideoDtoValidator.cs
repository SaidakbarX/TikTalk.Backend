using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;

namespace TikTalk.Backend.Application.Validator;

public class CreateVideoDtoValidator : AbstractValidator<CreateVideoDto>
{
    public CreateVideoDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title kerak")
            .MaximumLength(100);

        RuleFor(x => x.VideoUrl)
            .NotEmpty().WithMessage("Video URL kerak")
            .Must(url => url.StartsWith("http")).WithMessage("To‘g‘ri URL bering");

        RuleFor(x => x.ThumbnailUrl)
            .NotEmpty().WithMessage("Thumbnail kerak");

        RuleForEach(x => x.Hashtags)
            .MaximumLength(30).WithMessage("Hashtag juda uzun bo‘lishi mumkin emas");
    }
}