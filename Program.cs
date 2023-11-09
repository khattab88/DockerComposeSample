using DockerComposeSample.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:8080");

// Add services to the container.

var server = builder.Configuration["Database:Server"] ?? "";
var port = builder.Configuration["Database:Port"] ?? "";
var user = builder.Configuration["Database:User"] ?? "";
var password = builder.Configuration["Database:Password"] ?? "";
var database = builder.Configuration["Database:Name"] ?? "";

var connectionString = $"Server={server},{port}; UserId={user}; Password={password}; InitialCatalog={database};";

builder.Services.AddDbContext<ApplicationDbContext>(options => 
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
