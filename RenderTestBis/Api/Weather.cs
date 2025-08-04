using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared;

namespace RenderTestBis.Api;

[Handler]
[MapGet("/weather")]
public static partial class Weather
{
	public sealed record Query;

	private static ValueTask<RazorSliceHttpResult<List<WeatherForecast>>> Handle(Query _, WeatherService weather, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		var data = weather.GetMany(5);
		var res = Results.Extensions.RazorSlice<Slices.Weather, List<WeatherForecast>>(data);
		return ValueTask.FromResult(res);
	}
}