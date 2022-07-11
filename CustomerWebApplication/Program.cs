using CustomerWebApplication.Models;
using CustomerWebApplication.Utils;

var builder = WebApplication.CreateBuilder(args);

ConnectionStrings.SQLServer = builder.Configuration.GetConnectionString("SQLServer");
// Add services to the container.

builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddScoped<DBCustomerContext, DBCustomerContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(options =>
{
    options.WithOrigins("https://localhost:44462");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
