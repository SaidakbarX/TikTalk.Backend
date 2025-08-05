using TikTalk.Application.Settings;

namespace TikTalk.Backend.Api.Configuration;

public static class JwtSettingsConfiguration
{
    public static void ConfigureJwtSettings(this WebApplicationBuilder builder)
    {
        var jwtSection = builder.Configuration.GetSection("Jwt");

        var lifetime = jwtSection["Lifetime"];
        var securityKey = jwtSection["SecurityKey"];
        var audience = jwtSection["Audience"];
        var issuer = jwtSection["Issuer"];

        if (string.IsNullOrWhiteSpace(lifetime) ||
            string.IsNullOrWhiteSpace(securityKey) ||
            string.IsNullOrWhiteSpace(audience) ||
            string.IsNullOrWhiteSpace(issuer))
        {
            throw new InvalidOperationException("Jwt configuration values are missing or invalid in appsettings.json.");
        }

        var jwtSettings = new JwtAppSettings(issuer, audience, securityKey, lifetime);
        builder.Services.AddSingleton(jwtSettings);
    }
}
