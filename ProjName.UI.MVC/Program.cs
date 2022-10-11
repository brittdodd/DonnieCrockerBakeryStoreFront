using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjName.DATA.EF.models;
using ProjName.UI.MVC.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<StorefrontContext>(options => options.UseSqlServer(connectionString)); 

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount =true).AddRoles<IdentityRole>().AddRoleManager < RoleManager < IdentityRole >> ().AddEntityFrameworkStores < ApplicationDbContext > ();


//Shopping Cart - Step 1
//Register Session - must go after .AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);//how long the session will be stored in memory
    options.Cookie.HttpOnly = true; //allow us to set cookie options
    options.Cookie.IsEssential = true;//can't be declined - if someone wants to use our site, must accept this cookie in order to shop on the site. 
});




//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount =true).AddRoles<IdentityRole>().AddRoleManager < RoleManager < IdentityRole >> ().AddEntityFrameworkStores <ApplicationDbContext > ();

builder.Services.AddControllersWithViews();

var app = builder.Build();

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

//Shopping cart - Step 1
//Register session with the app
//This ALWAYS goes after UseRouting() and BEFORE UseAuthentication()
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
