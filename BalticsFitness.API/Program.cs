using Persistence;
using Application;
using Application.Support.Interfaces;
using BalticsFitness.API.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Infrastructure;
using Infrastructure.Models;
using MediatR;
using Application.System.SeedSampleData;
using Persistence.Interfaces;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddPersistence(builder.Configuration);
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


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


        // Seed data at startup
        //using (var scope = app.Services.CreateScope())
        //{
        //    var services = scope.ServiceProvider;
        //    try
        //    {
        //        var mediator = services.GetRequiredService<IMediator>();
        //        await mediator.Send(new SeedSampleDataCommand(), CancellationToken.None);
        //    }
        //    catch (Exception ex)
        //    {
        //        var logger = services.GetRequiredService<ILogger<Program>>();
        //        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
        //    }
        //}

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}