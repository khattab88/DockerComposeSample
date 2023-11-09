using DockerComposeSample.Data;
using DockerComposeSample.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:80");

// Add services to the container.

var server = builder.Configuration["DatabaseServer"] ?? "";
var port = builder.Configuration["DatabasePort"] ?? "";
var user = builder.Configuration["DatabaseUser"] ?? "";
var password = builder.Configuration["DatabasePassword"] ?? "";
var database = builder.Configuration["DatabaseName"] ?? "";

var connectionString = $"Server={server}, {port}; User Id={user}; Password={password}; Initial Catalog={database}; TrustServerCertificate=true;";

builder.Services.AddDbContext<ApplicationDbContext>(options => 
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllersWithViews();


var app = builder.Build();

// Apply database migrations
DatabaseManagementService.ApplyMigrations(app);

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
