using Bogus;
using Microsoft.AspNetCore.Http.HttpResults;
using RenderTest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddRazorComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

var weathers = new[] { "Sunny", "Overcast", "Rainy", "Snowy", "Windy", "Foggy", "Thunderstorm", "Clear", "Cloudy", "Partly Cloudy" };

Randomizer.Seed = new Random(1337);

var testLatLong = new Faker<LatLong>()
	.CustomInstantiator(f => new LatLong(
		f.Address.Latitude(),
		f.Address.Longitude()
	));

var testUser = new Faker<User>()
	.CustomInstantiator(f => new User(
		f.Name.FullName(),
		f.Internet.Avatar()
	));

var testWeather = new Faker<WeatherForecast>()
	.CustomInstantiator(f => new WeatherForecast(
		f.Date.Past(),
		f.Random.Number(-30, 40),
		f.Address.Country(),
		f.Address.City(),
		f.PickRandom(weathers),
		testLatLong.Generate(),
		testUser.Generate(f.Random.Number(3, 10))
	));

var forecast = app.MapGroup("/forecast");
forecast.MapGet("json", () => testWeather.Generate());
forecast.MapGet("blazor", () => {
	var data = testWeather.Generate();
	return new RazorComponentResult<WeatherEntry>(new { Weather = data });
});
forecast.MapGet("slice", () => Results.Extensions.RazorSlice<RenderTest.Slices.WeatherEntry, WeatherForecast>(testWeather.Generate()));


app.Run();

public record WeatherForecast(DateTime Date, int Temperature, string Country, string City, string Summary, LatLong Location, List<User> Users);

public record LatLong(double Latitude, double Longitude);

public record User(string Name, string Avatar);