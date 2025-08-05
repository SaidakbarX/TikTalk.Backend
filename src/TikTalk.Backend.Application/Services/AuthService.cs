using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Application.Interfaces.Repositories;
using TikTalk.Backend.Application.Interfaces.Services;
using TikTalk.Backend.Application.Helpers;
using TikTalk.Backend.Application.Services.Converters;
using TikTalk.Backend.Domain.Entities;
using TikTalk.Backend.Core.Exceptions;
using EventSystem.Application.Helpers.Security;

namespace TikTalk.Backend.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserLoginRepository _userLoginRepository;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IUserLoginRepository userLoginRepository,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _userLoginRepository = userLoginRepository;
        _tokenService = tokenService;
    }

    public async Task<long> SignUpUserAsync(CreateUserDto dto)
    {
        var existing = await _userRepository.GetByEmailAsync(dto.Email);
        if (existing is not null)
            throw new DuplicateException("Email already registered");

        var (hash, salt) = PasswordHasher.Hasher(dto.Password);

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            EmailConfirmed = false,
            CreatedAt = DateTime.UtcNow,
            PasswordHash = hash,
            Salt = salt
        };

        await _userRepository.AddAsync(user);
        return user.Id;
    }

    public async Task<LoginResponseDto> LoginUserAsync(UserLoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.ProviderKey);
        if (user == null)
            throw new EntityNotFoundException("User", dto.ProviderKey);

        if (dto.Provider.ToLower() == "email")
        {
            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new ValidationException("Password is required for email login");

            var isValid = PasswordHasher.Verify(dto.Password, user.PasswordHash, user.Salt);
            if (!isValid)
                throw new AuthException("Incorrect password");
        }

        var userDto = UserConverter.ToDto(user);
        var accessToken = _tokenService.GenerateToken(userDto);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var refresh = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false
        };

        await _refreshTokenRepository.CreateAsync(refresh);

        var login = new UserLogin
        {
            UserId = user.Id,
            Provider = dto.Provider,
            ProviderKey = dto.ProviderKey,
            LoginTime = dto.LoginTime
        };

        await _userLoginRepository.AddAsync(login);

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = refresh.ExpiresAt
        };
    }

    public async Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenDto dto)
    {
        var token = await _refreshTokenRepository.GetByTokenAsync(dto.Token, 0);
        if (token == null || token.IsRevoked || token.ExpiresAt < DateTime.UtcNow)
            throw new AuthException("Invalid or expired refresh token");

        token.IsRevoked = true;
        await _refreshTokenRepository.SaveChangesAsync();

        var user = token.User;
        var userDto = UserConverter.ToDto(user);

        var newAccessToken = _tokenService.GenerateToken(userDto);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        var newToken = new RefreshToken
        {
            UserId = user.Id,
            Token = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false
        };

        await _refreshTokenRepository.CreateAsync(newToken);

        return new LoginResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = newToken.ExpiresAt
        };
    }

    public async Task LogOutAsync(string refreshToken)
    {
        await _refreshTokenRepository.DeleteAsync(refreshToken);
    }
}