using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CCP.Data;
using CCP.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CCPContextConnection") ?? throw new InvalidOperationException("Connection string 'CCPContextConnection' not found.");

builder.Services.AddDbContext<CCPContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<CCPUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<CCPContext>();

// Add services to the container.
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();

