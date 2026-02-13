using System.Reflection;
using Application;
using Infrastructure;
using MSA.Infrastructure;
using MSA.Infrastructure.Data;
using MSA.Infrastructure.Web;
using WebAPI;


var builder = WebApplication.CreateBuilder(args);

// Defined in MAS Infrastructure
// builder.AddKeyVaultIfConfigured()

builder.AddApplicationServices();
builder.AddInfrastructureServices();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // await app.InitialiseDatabaseAsync();
}
else
{
    // The default HSTS value is 30 days. See https://aka.ms/aspnetcore-hsts to adjust as needed.
    app.UseHsts();
}

// app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});

app.UseExceptionHandler(options => { });
// Redirect to swagger page
app.Map("/", () => Results.Redirect("/api"));
//app.MapEndpoints(Assembly.GetExecutingAssembly());

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (HttpContext httpContext) =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)]
                })
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");


app.Run();