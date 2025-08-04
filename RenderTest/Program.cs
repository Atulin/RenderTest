using Microsoft.AspNetCore.Http.HttpResults;
using RenderTest;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();
builder.Services.AddSingleton<WeatherService>();

var app = builder.Build();

app.UseHttpsRedirection();

var forecast = app.MapGroup("/forecast");
forecast.MapGet("json", (WeatherService weather) => weather.GetOne());
forecast.MapGet("blazor", (WeatherService weather) => new RazorComponentResult<WeatherEntry>(new { Weather = weather.GetOne() }));
forecast.MapGet("slice", (WeatherService weather) => Results.Extensions.RazorSlice<RenderTest.Slices.WeatherEntry, WeatherForecast>(weather.GetOne()));

app.Run();