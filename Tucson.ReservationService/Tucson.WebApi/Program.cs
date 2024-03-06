using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using Tucson.Business.ClientValidation;
using Tucson.Business.ClientValidation.Interfaces;
using Tucson.Data.Config;
using Tucson.Data.Interfaces;
using Tucson.Data.Repositories;
using Tucson.Services.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<TucsonDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);

builder.Services.AddTransient<IReservationService, ReservationService>();
builder.Services.AddTransient<IReservationsRepository, ReservationsRepository>();
builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<IPendingReservationsRepository, PendingReservationsRepository>();
builder.Services.AddTransient<IClientReservationContext, ClientReservationContext>();

var strategies = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(IClientReservationStrategy).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

foreach (var strategy in strategies)
{
    builder.Services.AddTransient(
        typeof(IClientReservationStrategy),
        strategy);
}



builder.Services.AddControllers();
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

