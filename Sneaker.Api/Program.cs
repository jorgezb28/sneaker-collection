using Sneaker.Domain.IRepository;
using Sneaker.Infrastructure.Repository;
using Sneaker.Service.Profiles;
using Sneaker.Service.Services;
using Sneaker.Service.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Automapper
builder.Services.AddAutoMapper(typeof(SneakerCollectionProfile)); 

//Sevices
builder.Services.AddSingleton<ISneakerCollectionService, SneakerCollectionService>();

//Repository
builder.Services.AddSingleton<ISneakerRepository, SneakerRepositoryStatic>();
builder.Services.AddSingleton<ISizeRepository, SizeRepositoryStatic>();
builder.Services.AddSingleton<IBrandRepository, BrandRepositoryStatic>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();