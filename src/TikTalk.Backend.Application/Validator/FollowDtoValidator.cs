using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;

namespace TikTalk.Backend.Application.Validator;

public class FollowDtoValidator : AbstractValidator<FollowDto>
{
    public FollowDtoValidator()
    {
        RuleFor(x => x.FolloweeId)
            .GreaterThan(0).WithMessage("FolloweeId noto‘g‘ri");
    }
}