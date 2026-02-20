using Htmx.Net.Toast.Extensions;
using Htmx.Net.Toast.Notyf;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUglify.Css;
using Web.Data;
using Web.Data.Models;
using Web.Data.Repository;
using WebOptimizer.Processors;
using Htmx.Net.Toast.Notyf.Enums;
using Htmx.Net.Toast.Notyf.Models;
using Htmx.Net.Toast.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages();

builder.Services.AddNotyf(config =>
{
    config.Duration = 10000;
    config.Dismissable = true;
    config.Position = NotyfPosition.TopRight;
    config.Ripple = true;
    config.CustomTypes = new List<NotyfNotificationOptions>
            {
				// Create a new notification type called "rawr" with some sensible purple defaults - Icons by HeroIcons
				new NotyfNotificationOptions
                {
                    Type = ToastNotificationType.Custom("rawr"),
                    BackgroundColor = "#5928a7",
                    Dismissible = true,
                    Duration = 10000,
                    Icon = "<svg xmlns=\"http://www.w3.org/2000/svg\" fill=\"none\" viewBox=\"0 0 24 24\" stroke-width=\"1.5\" stroke=\"currentColor\" style=\"width: 1.25em; height: 1.25em;\">\r\n  <path stroke-linecap=\"round\" stroke-linejoin=\"round\" d=\"M15.59 14.37a6 6 0 01-5.84 7.38v-4.8m5.84-2.58a14.98 14.98 0 006.16-12.12A14.98 14.98 0 009.631 8.41m5.96 5.96a14.926 14.926 0 01-5.841 2.58m-.119-8.54a6 6 0 00-7.381 5.84h4.8m2.581-5.84a14.927 14.927 0 00-2.58 5.84m2.699 2.7c-.103.021-.207.041-.311.06a15.09 15.09 0 01-2.448-2.448 14.9 14.9 0 01.06-.312m-2.24 2.39a4.493 4.493 0 00-1.757 4.306 4.493 4.493 0 004.306-1.758M16.5 9a1.5 1.5 0 11-3 0 1.5 1.5 0 013 0z\" />\r\n</svg>\r\n",
                    Message = "This is a default message",
                    Ripple = true
                }
            };
});

builder.Services.AddScoped<IRepository, SQLRepository>();

builder.Services.AddWebOptimizer(

pipeline =>
{

    pipeline.AddCssBundle("/css/bundle.css",
        "ZZ_Stylesheet/font-awesome.css",
        "ZZ_Stylesheet/icofont.css",
        "ZZ_Stylesheet/themify.css",
        "ZZ_Stylesheet/flag-icon.css",
        "ZZ_Stylesheet/feather-icon.css",
        "ZZ_Stylesheet/slick.css",
        "ZZ_Stylesheet/slick-theme.css",
        "ZZ_Stylesheet/scrollbar.css",
        "ZZ_Stylesheet/animate.css",
        "ZZ_Stylesheet/bootstrap.css",
        "ZZ_Stylesheet/style.css",
        "ZZ_Stylesheet/color-1.css",
        "ZZ_Stylesheet/responsive.css",
        "").UseContentRoot();
    pipeline.AddJavaScriptBundle("/js/bundle.js",
             new JsSettings()
             {
                 CodeSettings = {
                             MinifyCode = true
                         }
             },
         "ZZ_Script/jQuery_3_7_1.js",
         "ZZ_Script/bootstrap.bundle.js",
         "ZZ_Script/simplebar.js",
         "ZZ_Script/custom.js",
         "ZZ_Script/config.js",
         "ZZ_Script/sidebar-menu.js",
         "ZZ_Script/sidebar-pin.js",
         "ZZ_Script/token.js",
         "ZZ_Script/script.js"
        ).UseContentRoot();
    pipeline.AddJavaScriptBundle("/js/bundle2.js",
        new JsSettings()
        {
            CodeSettings = {
                        MinifyCode = false
                    }
        },
          "ZZ_Script/feather.js",
          "ZZ_Script/feather-icon.js"
        ).UseContentRoot();
},
     option =>
     {
         option.EnableTagHelperBundling = true;
         option.EnableCaching = false;
         option.AllowEmptyBundle = true;
     }
);

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
app.UseWebOptimizer(); //WebOptimizer

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.UseNotyf();
// apply pending EF Core migrations and run runtime seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();   // applies any pending migrations
        DataSeeder.Seed(db);     // run your idempotent seeder

        await DataSeeder.SeedRolesAndAdminAsync(services);
    }
    catch (Exception ex)
    {
        //  var logger = services.GetRequiredService<ILogger<Program>>();
        //   logger.LogError(ex, "Error while migrating or seeding the database.");
        var x = ex; // for debugging
        throw;
    }
}





app.Run();
