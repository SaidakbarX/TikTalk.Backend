using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Application.Interfaces.Repositories;
using TikTalk.Backend.Application.Interfaces.Services;
using TikTalk.Backend.Application.Services.Converters;
using TikTalk.Backend.Domain.Entities;
using TikTalk.Backend.Core.Exceptions;
using EventSystem.Application.Helpers.Security;

namespace TikTalk.Backend.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IRoleRepository _roleRepository;

    public UserService(
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _roleRepository = roleRepository;
    }

    public async Task<UserDto?> GetByIdAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null) return null;

        return UserConverter.ToDto(user);
    }

    public async Task<UserWithRolesDto?> GetWithRolesAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null) return null;

        var roleNames = await _userRoleRepository.GetRoleNamesByUserIdAsync(id);

        return UserConverter.ToWithRolesDto(user, roleNames);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        var existing = await _userRepository.GetByEmailAsync(dto.Email);
        if (existing is not null)
            throw new DuplicateException("Email already registered");

        var (hash, salt) = PasswordHasher.Hasher(dto.Password);

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            ProfileImageUrl = "",
            EmailConfirmed = false,
            CreatedAt = DateTime.UtcNow,
            PasswordHash = hash,
            Salt = salt
        };

        await _userRepository.AddAsync(user);
        return UserConverter.ToDto(user);
    }

    public async Task UpdateProfileAsync(long id, string? fullName, string? avatarUrl)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null) 
            throw new EntityNotFoundException("User", id);

        if (!string.IsNullOrWhiteSpace(fullName))
            user.FullName = fullName;

        if (!string.IsNullOrWhiteSpace(avatarUrl))
            user.ProfileImageUrl = avatarUrl;

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
    }
}