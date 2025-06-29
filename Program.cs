using Cicekci.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IFlowerService, FlowerService>();
builder.Services.AddSingleton<CartService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
        AddCookie(
                x =>
                {
                    x.Cookie.Name = "NETCORE.Auth";
                    x.LoginPath = "/Login/Index"; //giriþ yapýlmadýðýnda buraya gidiyor.
                    x.AccessDeniedPath = "/AccesDenied/Index"; //eriþim izni yoksa buraya gidiyor.
                }
                );//kimlik doðrulama için.

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
