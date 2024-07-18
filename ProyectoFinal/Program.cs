using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal;

var builder = WebApplication.CreateBuilder(args);

var politicaUsuariosAutentificados = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

// Add services to the container.
builder.Services.AddControllersWithViews(opc => 
    opc.Filters.Add(new AuthorizeFilter(politicaUsuariosAutentificados)));

builder.Services.AddDbContext<ApplicationDBContext>(opc =>
opc.UseSqlServer("name=MyConnectionTrust"));

builder.Services.AddAuthentication();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opc =>
{
    opc.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();

builder.Services.PostConfigure<CookieAuthenticationOptions>(
    IdentityConstants.ApplicationScheme, opc => {
        opc.LoginPath = "/User/Login";
        opc.AccessDeniedPath = "/User/Login";
    }
);

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

app.Run();
