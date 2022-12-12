using FlyApp.Server.Data;
using FlyApp.Shared;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<WeatherDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("WeatherDatabase"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

// migrate
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<WeatherDbContext>();
    context.Database.Migrate();

    // create seed
    if (!context.WeatherForecasts.Any())
    {
        await context.WeatherForecasts.AddRangeAsync(
            new WeatherForecast() { Id = "1", Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)), TemperatureC = 20, Summary = "Mild" },
            new WeatherForecast() { Id = "2", Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)), TemperatureC = 10, Summary = "Cool" },
            new WeatherForecast() { Id = "3", Date = DateOnly.FromDateTime(DateTime.Now.AddDays(3)), TemperatureC = 30, Summary = "Hot" });
        await context.SaveChangesAsync();
    }
}

app.Run();

