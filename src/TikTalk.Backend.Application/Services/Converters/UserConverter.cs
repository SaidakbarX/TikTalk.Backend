using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Application.Services.Converters;

public static class UserConverter
{
    public static UserDto ToDto(User user) => new()
    {
        Id = user.Id,
        Username = user.FullName,
        FullName = user.FullName,
        AvatarUrl = user.ProfileImageUrl ?? ""
    };

    public static UserWithRolesDto ToWithRolesDto(User user, List<string> roles) => new()
    {
        Id = user.Id,
        Username = user.FullName,
        FullName = user.FullName,
        Email = user.Email,
        AvatarUrl = user.ProfileImageUrl ?? "",
        Roles = roles
    };
}
