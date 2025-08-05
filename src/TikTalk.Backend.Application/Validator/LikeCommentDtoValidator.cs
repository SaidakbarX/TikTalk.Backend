using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;

namespace TikTalk.Backend.Application.Validator;

public class LikeCommentDtoValidator : AbstractValidator<LikeCommentDto>
{
    public LikeCommentDtoValidator()
    {
        RuleFor(x => x.CommentId)
            .GreaterThan(0).WithMessage("CommentId noto‘g‘ri");
    }
}
