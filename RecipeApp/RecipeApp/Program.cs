using System.IdentityModel.Tokens.Jwt;
using System.Text;
using App.DAL.EF;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RecipeApp;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// builder.Services.AddScoped<IAppUnitOfWork, AppUOW>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultUI()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// // clear default claims
// JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
// builder.Services
//     .AddAuthentication()
//     .AddCookie(options => { options.SlidingExpiration = true; })
//     .AddJwtBearer(options =>
//     {
//         options.RequireHttpsMetadata = false;
//         options.SaveToken = false;
//         options.TokenValidationParameters = new TokenValidationParameters()
//         {
//             ValidIssuer = builder.Configuration.GetValue<string>("JWT:issuer"),
//             ValidAudience = builder.Configuration.GetValue<string>("JWT:audience"),
//             IssuerSigningKey =
//                 new SymmetricSecurityKey(
//                     Encoding.UTF8.GetBytes(
//                         builder.Configuration.GetValue<string>("JWT:issuer")
//                     )
//                 ),
//             ClockSkew = TimeSpan.Zero,
//         };
//     });

builder.Services.AddControllersWithViews();

// ===================================================
WebApplication app = builder.Build();
// ===================================================

await DataSeeder.SeedAdminUser(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

app.Run();