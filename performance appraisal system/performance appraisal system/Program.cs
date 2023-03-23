using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using performance_appraisal_system.Data;
using performance_appraisal_system.Services;
using performance_appraisal_system.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//connecting to the databse 
string? con = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EmployeeContext>(builder => builder.UseSqlServer(con));

//Adding the service for the basic app settings which provide the defaule app name mentioned in appsetting.json
builder.Services.AddSingleton<_basicAppInfo,basicAppInfo>();

//adding service for the validation login
builder.Services.AddTransient<_LoginValidator,loginValidator>();

//adding cookies authentication for the members and setting the defuault route if authentications fails

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/Home/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);

    });



//for the development ENV, TO AVOID the project reopning to see changes...

#if DEBUG

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();  
#endif

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
//for the authentication purpose
app.UseAuthentication();

app.UseAuthorization();
//for the session


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
