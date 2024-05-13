using HandelsRaketten.Areas.Identity.Data;
using HandelsRaketten.Catalogs;
using HandelsRaketten.Data;
using HandelsRaketten.EFDBContext;
using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Models.AdModels.SubCategories.PlantAccessories;
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
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<HandelsRakettenContext>();
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

// json file services
builder.Services.AddSingleton<GenericJsonFileService<Fertilizer>>();
builder.Services.AddSingleton<GenericJsonFileService<GardeningTool>>();
builder.Services.AddSingleton<GenericJsonFileService<Tool>>();
builder.Services.AddSingleton<GenericJsonFileService<Soil>>();
builder.Services.AddSingleton<GenericJsonFileService<IndoorPlant>>();
builder.Services.AddSingleton<GenericJsonFileService<OutdoorPlant>>();

//// Database services
builder.Services.AddSingleton<IService<Fertilizer>, DbGenericService<Fertilizer>>();
builder.Services.AddSingleton<IService<GardeningTool>, DbGenericService<GardeningTool>>();
builder.Services.AddSingleton<IService<Tool>, DbGenericService<Tool>>();
builder.Services.AddSingleton<IService<Soil>, DbGenericService<Soil>>();
builder.Services.AddSingleton<IService<IndoorPlant>, DbGenericService<IndoorPlant>>();
builder.Services.AddSingleton<IService<OutdoorPlant>, DbGenericService<OutdoorPlant>>();

// DbContext
builder.Services.AddDbContext<DbContextGeneric<Fertilizer>>();
builder.Services.AddDbContext<DbContextGeneric<GardeningTool>>();
builder.Services.AddDbContext<DbContextGeneric<Tool>>();
builder.Services.AddDbContext<DbContextGeneric<IndoorPlant>>();
builder.Services.AddDbContext<DbContextGeneric<OutdoorPlant>>();
builder.Services.AddDbContext<DbContextGeneric<Soil>>();

// Repositories
builder.Services.AddSingleton<AdCatalog>();
builder.Services.AddSingleton<PlantCatalog>();
builder.Services.AddSingleton<PlantAccessoryCatalog>();

builder.Services.AddSingleton<GenericCatalog<IndoorPlant>>();
builder.Services.AddSingleton<GenericCatalog<OutdoorPlant>>();
builder.Services.AddSingleton<GenericCatalog<Fertilizer>>();
builder.Services.AddSingleton<GenericCatalog<GardeningTool>>();
builder.Services.AddSingleton<GenericCatalog<Soil>>();
builder.Services.AddSingleton<GenericCatalog<Tool>>();

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

app.Run();
