using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using MoneyMate_WebApp.DataAccess;
using MoneyMate_WebApp.DataAccess.Entities;
using MoneyMate_WebApp.DataAccess.Installers;
using MoneyMate_WebApp.BusinessLogic.Installers;

var builder = WebApplication.CreateBuilder(args);

// 1) Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// 2) Database, repositories, business logic
builder.Services.AddDataContext(builder.Configuration)
    .AddRepositories()
    .AddUnitOfWork()
    .AddAccountService()
    .AddTransactionService()
    .AddCategoryService()
    .AddTypeService()
    .AddCurrencyService();

// 3) Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<MoneyMateContext>()
    .AddDefaultTokenProviders();

// 4) Configure cookie settings for Identity
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/UserAccount/Login";
    options.LogoutPath = "/UserAccount/Logout";
    options.AccessDeniedPath = "/UserAccount/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
});

// 5) MVC + localization
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

// 6) Configure supported cultures
var supportedCultures = new[] { "en", "uk" };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.SetDefaultCulture(supportedCultures[1])  // default "uk"
           .AddSupportedCultures(supportedCultures)
           .AddSupportedUICultures(supportedCultures);

    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
    options.RequestCultureProviders.Insert(1, new CookieRequestCultureProvider());
});

var app = builder.Build();

static async Task SeedRolesAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

    string[] roleNames = { "Admin", "User" };

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        }
    }
}


async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string adminEmail = "admin@money.com";
    string adminPassword = "123";

    // шукатимемо серед ApplicationUser
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        var user = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            Name = "Administrator"
        };

        var result = await userManager.CreateAsync(user, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}



// 7) Request localization
app.UseRequestLocalization(app.Services.GetRequiredService<Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>().Value);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

// 8) Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// 9) Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserAccount}/{action=Login}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRolesAsync(services);
    await SeedAdminUserAsync(services);
}

app.Run();
