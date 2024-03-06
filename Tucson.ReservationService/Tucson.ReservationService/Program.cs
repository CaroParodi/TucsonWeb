using Tucson.Business.ClientValidation;
using Tucson.Business.ClientValidation.Interfaces;
using Tucson.Data.Interfaces;
using Tucson.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

var strategies = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(IClientReservationStrategy).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

foreach (var strategy in strategies)
{
    builder.Services.AddTransient(
        typeof(IClientReservationStrategy),
        strategy);
}
var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();
