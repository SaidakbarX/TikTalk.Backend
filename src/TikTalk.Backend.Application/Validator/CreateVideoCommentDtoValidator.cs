using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;

namespace TikTalk.Backend.Application.Validator;

public class CreateVideoCommentDtoValidator : AbstractValidator<CreateVideoCommentDto>
{
    public CreateVideoCommentDtoValidator()
    {
        RuleFor(x => x.VideoId)
            .GreaterThan(0).WithMessage("VideoId noto‘g‘ri");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Kommentariyani kiriting")
            .MaximumLength(300);
    }
}