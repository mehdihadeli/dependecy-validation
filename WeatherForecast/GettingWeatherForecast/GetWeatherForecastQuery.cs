using MediatR;

namespace dependecy_validation.WeatherForecast.GettingWeatherForecast;

public class GetWeatherForecastQuery : IRequest<List<WeatherForecast>>
{
}

public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastQuery, List<WeatherForecast>>
{
    private readonly List<string> summaries = new List<string>
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<List<WeatherForecast>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        var forecasts = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Count)]
                ))
            .ToList();

        return Task.FromResult(forecasts);
    }
}