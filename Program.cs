using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CCP.Data;
using CCP.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using CCP.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
var connectionString = builder.Configuration.GetConnectionString("CCPContextConnection") ?? throw new InvalidOperationException("Connection string 'CCPContextConnection' not found.");

builder.Services.AddDbContext<CCPContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<CCPUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<CCPContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Retrieve SendGrid API key from user secrets configuration
var sendGridApiKey = builder.Configuration["SENDGRID_API_KEY"];
Console.WriteLine("SENDGRID_API_KEY: " + sendGridApiKey);

// Configure SendGrid
builder.Services.AddSingleton<ISendGridClient>(new SendGridClient(sendGridApiKey));

// Register the email service
builder.Services.AddTransient<EmailService>();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

