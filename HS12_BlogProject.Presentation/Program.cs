
using System.Text;
using HS12_BlogProject.Domain.Entities;
using HS12_BlogProject.Domain.Repositories;
using HS12_BlogProject.Infrastructure;
using HS12_BlogProject.Infrastructure.Repositories;
using HS12_BlogProject.Presentation.APIService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IApiService, ApiService>(opt =>
{
	opt.BaseAddress = new Uri("o-eser-blogtestapi.azurewebsites.net/api");
});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
	options.MinimumSameSitePolicy = SameSiteMode.None;
	options.HttpOnly = HttpOnlyPolicy.Always;
	options.Secure = CookieSecurePolicy.Always;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				options.LoginPath = "/account/Login";

			});

#region API sonrasý kaldýrýlan kodlar
//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<AppDbContext>();

//builder.Services.AddIdentity<AppUser, IdentityRole>(opt=>
//    {
//        opt.SignIn.RequireConfirmedEmail = false;
//        opt.SignIn.RequireConfirmedPhoneNumber = false;
//        opt.SignIn.RequireConfirmedAccount = false;
//        opt.User.RequireUniqueEmail = false;
//        opt.Password.RequireUppercase=false;
//        opt.Password.RequireNonAlphanumeric=false;
//        opt.Password.RequiredLength=3;
//        opt.Password.RequireLowercase=false;

//}).AddEntityFrameworkStores<AppDbContext>();



//builder.Services.AddTransient<IGenreService, GenreService>();
//builder.Services.AddTransient<IGenreRepository, GenreRepository>();
////builder.Services.AddAutoMapper(typeof(Mapping));
//builder.Services.AddAutoMapper(typeof(Program));

//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
//{
//    builder.RegisterModule(new DependencyResolver());
//});

#endregion

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

app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();
