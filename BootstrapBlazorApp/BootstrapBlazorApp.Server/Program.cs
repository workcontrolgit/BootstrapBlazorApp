using BootstrapBlazorApp.Shared.Data;
using System.Text;

//  create a new instance of the WebApplication class
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
// add support for Razor Pages
builder.Services.AddRazorPages();
// add support for Server-Side Blazor
builder.Services.AddServerSideBlazor();

// add Bootstrap Blazor components from https://github.com/dotnetcore/BootstrapBlazor
builder.Services.AddBootstrapBlazor();

// register a service with the application's dependency injection container as a singleton
builder.Services.AddSingleton<WeatherForecastService>();

// Increase Table Data service operation class
builder.Services.AddTableDemoDataService();

builder.Services.AddHttpClient();

// build and configure the application's host
var app = builder.Build();

// check running in the development environment

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}
// serving static files such as images, CSS, and JavaScript files from the application's web root
app.UseStaticFiles();
// add the routing middleware
app.UseRouting();
// map the SignalR hub for Blazor Server-Side
app.MapBlazorHub();
// specify a fallback page that will be used when the application cannot find a matching endpoint
app.MapFallbackToPage("/_Host");

// configure the application's request processing pipeline and handle incoming HTTP requests
app.Run();
