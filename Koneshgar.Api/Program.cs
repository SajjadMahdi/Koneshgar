using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.Utilities.IoC;
using FluentValidation;
using Koneshgar.Application.Configurations;
using Koneshgar.Application.DependencyResolvers;
using Koneshgar.Application.Utilities.Extensions;
using Koneshgar.Application.Utilities.Helpers;
using Koneshgar.DataLayer.Contexts;
using Koneshgar.Domain.Models.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddNewtonsoftJson(opts =>
        {
            opts.SerializerSettings.Converters.Add(new StringEnumConverter());
            opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                        });
        });

        //builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        builder.Services.AddDbContext<TaskContext>(options =>
        {
            //options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection"));
            options.UseNpgsql(
            builder.Configuration.GetConnectionString("PostgreSqlConnection"));
        });
        builder.Services.AddIdentity<User, IdentityRole>()
                       .AddEntityFrameworkStores<TaskContext>()
                       .AddDefaultTokenProviders();
        builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWTOptions"));
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
        {
            var jwtOptions = builder.Configuration.GetSection("JWTOptions").Get<JWTOptions>();
            opts.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience[0],
                IssuerSigningKey = SecurityKeyHelper.GetSymmetricSecurityKey(jwtOptions.SecurityKey),
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 9;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
        });

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Services.AddDependencyResolvers((IConfiguration)builder.Configuration, new ICoreModule[] {
               new CoreModule()
            }); //here

        // Register services directly with Autofac here.
        // Don't call builder.Populate(), that happens in AutofacServiceProviderFactory.
        builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

        ValidatorOptions.Global.LanguageManager.Enabled = false;

        var app = builder.Build();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }



        using (var scopeToMigrate = app.Services.CreateScope())
        {
            var db = scopeToMigrate.ServiceProvider.GetRequiredService<TaskContext>();
            db.Database.Migrate();
        }


        app.UseCustomExceptionMiddleware();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseCors(builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}