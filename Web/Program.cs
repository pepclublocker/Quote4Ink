using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

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
        "ZZ_Stylesheet/color-3.css",
        "ZZ_Stylesheet/responsive.css",
        "").UseContentRoot();
    pipeline.AddJavaScriptBundle("/js/bundle.js",
         "ZZ_Script/jquery.min.js",
         "ZZ_Script/bootstrap.bundle.min.js",
         //"ZZ_Script/feather.min.js",
         //"ZZ_Script/feather-icon.js",
         "ZZ_Script/simplebar.js",
        "ZZ_Script/custom.js",
          "ZZ_Script/config.js",
        "").UseContentRoot();
},
     option =>
     {
         option.EnableTagHelperBundling = true;
         option.EnableCaching = false;
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

app.Run();
