using FluentValidation;
using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace TikTalk.Backend.Api.Endpoints;

public static class AuthEndpoint
{
    public static WebApplication MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/api/auth/signup", async (CreateUserDto dto, IAuthService authService, IValidator<CreateUserDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors);

            var userId = await authService.SignUpUserAsync(dto);
            return Results.Ok(new { UserId = userId });
        })
        .WithName("SignUp")
        .WithOpenApi();
        app.MapPost("/api/auth/login", async (UserLoginDto dto, IAuthService authService, IValidator<UserLoginDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors);

            var result = await authService.LoginUserAsync(dto);
            return Results.Ok(result);
        })
        .WithName("Login")
        .WithOpenApi();

        app.MapPost("/api/auth/refresh", async (RefreshTokenDto dto, IAuthService authService, IValidator<RefreshTokenDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors);

            var result = await authService.RefreshTokenAsync(dto);
            return Results.Ok(result);
        })
        .WithName("RefreshToken")
        .WithOpenApi();

        app.MapPost("/api/auth/logout", async (RefreshTokenDto dto, IAuthService authService) =>
        {
            await authService.LogOutAsync(dto.Token);
            return Results.Ok();
        })
        .RequireAuthorization()
        .WithName("Logout")
        .WithOpenApi();

        return app;
    }
}
