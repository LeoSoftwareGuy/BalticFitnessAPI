using Persistence;
using Application;
using Application.Support.Interfaces;
using BalticsFitness.API.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Infrastructure;
using Infrastructure.Models;
using BalticsFitness.API;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddPersistence(builder.Configuration)
            .AddApplication()
            .AddInfrastructure(builder.Configuration)
            .AddApiServices(builder.Configuration);

        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // does not really work, I was able to hit url with my client even with this commented out
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowMobileUI", policy =>
            {
                policy.WithOrigins("http://localhost:8081", "http://192.168.1.165:8081")
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });


        // Load JWT settings
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
        var secretKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey)
            };
        });

        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseApiServices();
 
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //await app.InitialiseDatabaseAsync();
        }

        app.UseCors("AllowMobileUI");
        //app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}