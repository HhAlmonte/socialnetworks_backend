using BussinessLogic.Data;
using Core.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
var identityBuilder = builder.Services.AddIdentityCore<UserEntities>();

// IdentityBuilder container
identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);
identityBuilder.AddEntityFrameworkStores<SecurityDbContext>();
identityBuilder.AddSignInManager<SignInManager<UserEntities>>();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<SecurityDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("IdentitySecurityConnection"));
});

builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// DbContext - DbContextData
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
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

app.UseAuthorization();

app.MapControllers();

app.Run();
