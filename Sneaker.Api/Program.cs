using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sneaker.Api;
using Sneaker.Domain.DomainServices;
using Sneaker.Domain.DomainServices.Implementations;
using Sneaker.Domain.IRepository;
using Sneaker.Infrastructure.Context;
using Sneaker.Infrastructure.Repository;
using Sneaker.Service.Profiles;
using Sneaker.Service.Services;
using Sneaker.Service.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

//Automapper
builder.Services.AddAutoMapper(typeof(SneakerCollectionProfile)); 

//Sevices
builder.Services.AddSingleton<ISneakerDomainService, SneakerDomainService>();
builder.Services.AddSingleton<ISneakerCollectionService, SneakerCollectionService>();
builder.Services.AddSingleton<IAuthService, AuthService>();

//Repository
builder.Services.AddSingleton<ISneakerRepository, SneakerRepositoryStatic>();
builder.Services.AddSingleton<ISizeRepository, SizeRepositoryStatic>();
builder.Services.AddSingleton<IBrandRepository, BrandRepositoryStatic>();
builder.Services.AddSingleton<IUserRepository, UserRepositoryStatic>();

/* ---SQL implementation for repositories---
builder.Services.AddSingleton<ISneakerRepository, SneakerRepository>();
builder.Services.AddSingleton<ISizeRepository, SizeRepository>();
builder.Services.AddSingleton<IBrandRepository, BrandRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
*/

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo{Title = "Sneaker Collection API", Version = "v1"});
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.Services.Configure<AppSetting>(
    builder.Configuration.GetSection("AppSettings"));

//Adding JWT Auth
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:ApiSecret"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Add context
builder.Services.AddDbContext<SneakerContext>(options =>
{
    options.UseSqlServer("Server=localhost,1433; Database=SneakerCollection;User=sa; Password=1StrongPass");
});

var app = builder.Build();

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