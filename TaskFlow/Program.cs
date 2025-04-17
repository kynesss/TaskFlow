using Microsoft.AspNetCore.Identity;
using TaskFlow.Domain.Identity;
using TaskFlow.Infrastructure.Extensions;
using TaskFlow.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnSignedIn = async context =>
    {
        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
        var user = await userManager.GetUserAsync(context.Principal!);

        if (user != null)
        {
            var roles = await userManager.GetRolesAsync(user);
            if (!roles.Any())
            {
                await userManager.AddToRoleAsync(user, Roles.User);
            }
        }
    };
});

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();
await seeder.SeedAsync();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();