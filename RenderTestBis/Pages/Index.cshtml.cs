using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared;

namespace RenderTestBis.Pages;

public class Index(WeatherService weather) : PageModel
{
	public required List<WeatherForecast> WeatherForecast { get; set; }
	
	public void OnGet()
	{
		WeatherForecast = weather.GetMany(5);
	}
}