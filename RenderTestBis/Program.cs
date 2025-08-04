using RenderTestBis;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddRenderTestBisHandlers();

builder.Services.AddSingleton<WeatherService>();

var app = builder.Build();

app.MapRazorPages();
app.MapStaticAssets();
app.MapRenderTestBisEndpoints();

app.UseHttpsRedirection();

app.Run();