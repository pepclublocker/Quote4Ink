using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUglify.Css;
using Web.Data;
using Web.Data.Models;
using WebOptimizer.Processors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => { options.SignIn.RequireConfirmedAccount = true; })
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultUI()
          // .AddSignInManager<CustomSignIn<ApplicationUser>>()
          .AddDefaultTokenProviders();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

//builder.Services.AddScoped<IRepository, SQLRepository>();

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

// apply pending EF Core migrations and run runtime seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();   // applies any pending migrations
        DataSeeder.Seed(db);     // run your idempotent seeder
    }
    catch (Exception ex)
    {
        //  var logger = services.GetRequiredService<ILogger<Program>>();
        //   logger.LogError(ex, "Error while migrating or seeding the database.");
        throw;
    }
}





app.Run();
