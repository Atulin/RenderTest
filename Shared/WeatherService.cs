using Bogus;

namespace Shared;

public class WeatherService
{
	private readonly Faker<WeatherForecast> _testWeather;
	
	public WeatherService()
	{
		Randomizer.Seed = new Random(1337);
		
		var weathers = new[] { "Sunny", "Overcast", "Rainy", "Snowy", "Windy", "Foggy", "Thunderstorm", "Clear", "Cloudy", "Partly Cloudy" };

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

		_testWeather = new Faker<WeatherForecast>()
			.CustomInstantiator(f => new WeatherForecast(
				f.Date.Past(),
				f.Random.Number(-30, 40),
				f.Address.Country(),
				f.Address.City(),
				f.PickRandom(weathers),
				testLatLong.Generate(),
				testUser.Generate(f.Random.Number(3, 10))
			));
	}

	public WeatherForecast GetOne() => _testWeather.Generate();
	public List<WeatherForecast> GetMany(int count) => _testWeather.Generate(count);
}

public record WeatherForecast(DateTime Date, int Temperature, string Country, string City, string Summary, LatLong Location, List<User> Users);

public record LatLong(double Latitude, double Longitude);

public record User(string Name, string Avatar);