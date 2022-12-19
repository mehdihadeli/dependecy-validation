using System.Reflection;
using dependecy_validation;
using dependecy_validation.WeatherForecast.GettingWeatherForecast;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((context, options) =>
{
    // https://andrewlock.net/new-in-asp-net-core-3-service-provider-validation/
    options.ValidateScopes = context.HostingEnvironment.IsDevelopment() ||
                             context.HostingEnvironment.IsStaging();
    options.ValidateOnBuild = true;
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast",async (IMediator mediator, ITestService testService) =>
    {
        var res = await mediator.Send(new GetWeatherForecastQuery());
        return res;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();