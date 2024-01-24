using Microsoft.OpenApi.Models;
using Flights.Data;
using Flights.Domain.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add db context

builder.Services.AddDbContext<Entities>(options => 
options.UseInMemoryDatabase(databaseName: "Flights"), 
ServiceLifetime.Singleton);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer 
    { 
        Description = "Development Server",
        Url = "https://localhost:7148"
    });

    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"] + e.ActionDescriptor.RouteValues["controller"]}");
});

builder.Services.AddSingleton<Entities>();

var app = builder.Build();

var entities = app.Services.CreateScope().ServiceProvider.GetService<Entities>();
var random = new Random();

Flight[] flightsToSeed = new Flight[]
    {
        new (   Guid.NewGuid(),
                "British Airways",
                random.Next(15000, 20000).ToString(),
                new TimePlace("London, England",DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlace("Johannesburg",DateTime.Now.AddHours(random.Next(4, 10))),
                    random.Next(5)),
        new (   Guid.NewGuid(),
                "Emirates",
                random.Next(15000, 25000).ToString(),
                new TimePlace("Dubai",DateTime.Now.AddHours(random.Next(1, 10))),
                new TimePlace("Nairobi, Kenya",DateTime.Now.AddHours(random.Next(4, 15))),
                random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "British Airways",
                random.Next(19000, 25000).ToString(),
                new TimePlace("London, England",DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlace("Durban",DateTime.Now.AddHours(random.Next(4, 18))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "CemAir",
                random.Next(10000, 25000).ToString(),
                new TimePlace("Cape Town",DateTime.Now.AddHours(random.Next(1, 21))),
                new TimePlace("Cairo, Egypt",DateTime.Now.AddHours(random.Next(4, 21))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Kulula",
                random.Next(9000, 12000).ToString(),
                new TimePlace("Gqheberha",DateTime.Now.AddHours(random.Next(1, 23))),
                new TimePlace("Mauritius",DateTime.Now.AddHours(random.Next(4, 25))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Africa  World Airlines",
                random.Next(9000, 15000).ToString(),
                new TimePlace("Bloemfontein",DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlace("Seychelles",DateTime.Now.AddHours(random.Next(4, 19))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Kenya Airways",
                random.Next(9000, 15000).ToString(),
                new TimePlace("Nairobi, Kenya",DateTime.Now.AddHours(random.Next(1, 55))),
                new TimePlace("Polokwane",DateTime.Now.AddHours(random.Next(4, 58))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Airlink",
                random.Next(9000, 15000).ToString(),
                new TimePlace("Bloemfontein",DateTime.Now.AddHours(random.Next(1, 58))),
                new TimePlace("Barcelona, Spain",DateTime.Now.AddHours(random.Next(4, 60))),
                    random.Next(1, 853))
           };
entities.Flights.AddRange(flightsToSeed);

entities.SaveChanges();

app.UseCors(builder => builder
.WithOrigins("*")
.AllowAnyMethod()
.AllowAnyHeader()
);

app.UseSwagger().UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
