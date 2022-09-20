using BussinessLogic.Data;
using BussinessLogic.Logic;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApi.DTOs;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
var identityBuilder = builder.Services.AddIdentityCore<UserEntities>();

// IdentityBuilder container.

identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);
identityBuilder.AddEntityFrameworkStores<SecurityDbContext>();
identityBuilder.AddSignInManager<SignInManager<UserEntities>>();

// Add services to the container.

builder.Services.AddScoped<IAzureBlobStorageService, StorageServices>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

builder.Services.AddControllers();

builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddCors();

builder.Services.AddDbContext<SecurityDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("IdentitySecurityConnection"));
});

builder.Services.AddDbContext<ContentDbContext>( x => {
    x.UseSqlServer(builder.Configuration.GetConnectionString("ContentConnection"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
        ValidIssuer = builder.Configuration["token:Issuer"],
        ValidateIssuer = true,
        ValidateAudience = false
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Token security",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
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
            new string[]{}
        }
    });
});

var app = builder.Build();

// DbContext - DbContextData
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        // ContentDbContext
        var context = services.GetRequiredService<ContentDbContext>();
        await context.Database.MigrateAsync();
        await ContentDbContextData.SeedContentAsync(context, loggerFactory);

        // IdentityDbContext
        var userManager = services.GetRequiredService<UserManager<UserEntities>>();
        var identityContext = services.GetRequiredService<SecurityDbContext>();
        await identityContext.Database.MigrateAsync();
        await SecurityDbContextData.SeedUserAsync(userManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Errores en el proceso de migracion"); ;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
