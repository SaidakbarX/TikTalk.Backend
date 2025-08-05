using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;

namespace TikTalk.Backend.Application.Validator;

public class UpdateVideoCommentDtoValidator : AbstractValidator<UpdateVideoCommentDto>
{
    public UpdateVideoCommentDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Komment matni bo‘sh bo‘lmasligi kerak")
            .MaximumLength(300);
    }
}
