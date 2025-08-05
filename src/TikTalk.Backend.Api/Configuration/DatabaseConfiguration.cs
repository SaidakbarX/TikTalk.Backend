using Microsoft.EntityFrameworkCore;
using System;
using TikTalk.Backend.Infrastructure.Persistence.DbContexts;

namespace TikTalk.Backend.Api.Configuration;

public static class DatabaseConfigurations
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<TikTalkDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        // Agar transaction yoki retry strategy kerak bo‘lsa, bu yerga yoziladi.
    }
}