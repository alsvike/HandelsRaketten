using HandelsRaketten.Areas.Identity.Data;
using HandelsRaketten.Catalogs;
using HandelsRaketten.Data;
using HandelsRaketten.EFDBContext;
using HandelsRaketten.Hubs;
using HandelsRaketten.Models;
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Models.AdModels.SubCategories.Plants;
using HandelsRaketten.Services;
using HandelsRaketten.Services.AdServices;
using HandelsRaketten.Services.DbServices;
using HandelsRaketten.Services.GenericServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Protocol.Core.Types;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<HandelsRakettenContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<HandelsRakettenContext>();


// ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(connectionString));

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
});
builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmailSender, EmailSender>(i =>
{
    var emailSenderConfig = builder.Configuration.GetSection("EmailSender");
    return new EmailSender(
        emailSenderConfig["Host"],
        int.Parse(emailSenderConfig["Port"]),
        bool.Parse(emailSenderConfig["EnableSSL"]),
        emailSenderConfig["UserName"],
        emailSenderConfig["Password"]);
});

// Add services
builder.Services.AddSingleton<IAdService, AdService>();
builder.Services.AddSingleton<ISellerService, SellerService>();

// json file services
builder.Services.AddSingleton<GenericJsonFileService<IndoorPlantAd>>();
builder.Services.AddSingleton<GenericJsonFileService<OutdoorPlantAd>>();

// Database services
builder.Services.AddSingleton<IService<IndoorPlantAd>, DbGenericService<IndoorPlantAd>>();
builder.Services.AddSingleton<IService<OutdoorPlantAd>, DbGenericService<OutdoorPlantAd>>();
builder.Services.AddSingleton<IService<Seller>, DbGenericService<Seller>>();

builder.Services.AddSingleton<IAdDbService, AdDbService>();

// DbContext
builder.Services.AddDbContext<DbContextGeneric<IndoorPlantAd>>();
builder.Services.AddDbContext<DbContextGeneric<OutdoorPlantAd>>();
builder.Services.AddDbContext<DbContextGeneric<Seller>>();
builder.Services.AddDbContext<AdDbContext>();

// Catalogs
builder.Services.AddSingleton<AdCatalog>();
builder.Services.AddSingleton<PlantCatalog>();


// service for messages
builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<ChatHub>("/chathub");

app.Run();
