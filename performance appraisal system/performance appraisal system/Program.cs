using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using performance_appraisal_system.Data;
using performance_appraisal_system.Migrations;
using performance_appraisal_system.Repository;
using performance_appraisal_system.Services;
using performance_appraisal_system.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//connecting to the databse 
string? con = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EmployeeContext>(builder => { builder.UseSqlServer(con).EnableSensitiveDataLogging(); ; });
  
   
//Adding the service for the basic app settings which provide the defaule app name mentioned in appsetting.json
builder.Services.AddSingleton<_basicAppInfo,basicAppInfo>();

//adding service for the validation login
builder.Services.AddTransient<_LoginValidator,loginValidator>();

//adding Services for the Employee(eg : Add employee,Retrive employee details)

builder.Services.AddScoped<IEmployeeService,EmployeeService>();


//adding services for the competiencies

builder.Services.AddScoped<ICompitencies, competenciesServices>();

//building services for the appraisal form and the services

builder.Services.AddScoped<IAppraisalfromService,AppraisalFormService>();

//adding cookies authentication for the members and setting the defuault route if authentications fails

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/Home/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.AccessDeniedPath = "/Error/AccessDenied";

    });



//for the development ENV, TO AVOID the project reopning to see changes...

#if DEBUG

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
#endif


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


//ading the policies it helps to access the page only who has valid Role

builder.Services.AddAuthorization(options =>
{
    //setting policy for the HR
    options.AddPolicy("BlongsToHR", policy =>
    {
        policy.RequireClaim("Designation", "HR");
    });

    //setting privacy for the Manager
    options.AddPolicy("BlongsToManager", policy =>
    {
        policy.RequireClaim("Designation", "Manager");
    });

   
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//for the session


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
//for the authentication purpose
app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
