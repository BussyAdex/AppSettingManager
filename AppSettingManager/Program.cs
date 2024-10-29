using AppSettingManager.Models;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureKeyVault(
	new Uri(builder.Configuration.GetValue<string>("KeyVaultName")),
	new DefaultAzureCredential());

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilo"));

builder.Services.Configure<SocialLoginSettings>(builder.Configuration.GetSection("SocialLoginSetting"));

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
