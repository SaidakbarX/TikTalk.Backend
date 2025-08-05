using CloudinaryDotNet;
using FluentValidation;
using FluentValidation.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using TikTalk.Backend.Api.Configuration;
using TikTalk.Backend.Api.Endpoints;
using TikTalk.Backend.Api.Extensions;
using TikTalk.Backend.Application.Validator;

namespace TikTalk.Backend.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();


        ///Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.ConfigureDatabase();
        ServiceCollectionExtensions2.AddSwaggerWithJwt(builder.Services);

        builder.Services.ConfigureDependencies();
        builder.ConfigureJwtSettings();
        builder.ConfigureJwtAuth();
        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomOperationIds(apiDesc =>
                apiDesc.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
        });
        builder.Services.AddSingleton(x =>
        {
            var config = builder.Configuration.GetSection("Cloudinary");
            var account = new Account(
                config["CloudName"],
                config["ApiKey"],
                config["ApiSecret"]
            );

            return new Cloudinary(account);
        });


        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.MapLikeEndpoints();
        app.MapFollowEndpoints();
        app.MapUserEndpoints();
        app.MapVideoEndpoints();
        app.MapVideoCommentEndpoints();
        app.MapAnalyticsEndpoints();
        app.MapAuthEndpoints();





        app.MapControllers();

        app.Run();
    }
}
