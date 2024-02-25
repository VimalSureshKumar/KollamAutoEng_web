using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KollamAutoEng_web.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("KollamAutoEng_webContextConnection") ?? throw new InvalidOperationException("Connection string 'KollamAutoEng_webContextConnection' not found.");

builder.Services.AddDbContext<KollamAutoEng_webContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<KollamAutoEng_webUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<KollamAutoEng_webContext>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
