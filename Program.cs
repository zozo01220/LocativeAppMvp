using LocativeApp.Components;
using LocativeApp.Components.Account;
using LocativeApp.Data;
using LocativeApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

// Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<LocativeApp.Services.RentService>();
builder.Services.AddScoped<OwnerService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<CurrentOwnerService>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

Console.WriteLine(connectionString);

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ===============================
// IDENTITY (CORRECT VERSION)
// ===============================
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
    //.AddDefaultUI(); // important pour Blazor template

// AUTH (IMPORTANT : NE PAS AJOUTER AddAuthentication + Cookies MANUELLEMENT)

// Email stub
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

// ===============================
// ROLE SEED (OK)
// ===============================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await IdentitySeed.SeedRolesAsync(roleManager);
    await SuperAdminSeed.CreateSuperAdmin(userManager, roleManager);

    //var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    //string[] roles = { "SuperAdmin", "Admin", "Tenant", "Candidate" };

    //foreach (var role in roles)
    //{
    //    if (!await roleManager.RoleExistsAsync(role))
    //        await roleManager.CreateAsync(new IdentityRole(role));
    //}
}



app.Run();