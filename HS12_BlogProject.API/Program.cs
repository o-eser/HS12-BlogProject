using Autofac.Extensions.DependencyInjection;
using Autofac;
using HS12_BlogProject.Application.IoC;
using HS12_BlogProject.Domain.Entities;
using HS12_BlogProject.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer þemasýný kullanan JWT Yetkilendirme baþlýðý.\r\n\r\n Aþaðýdaki metin giriþine 'Bearer' [boþluk] ve ardýndan üretilen tokený girin.\r\n\r\nÖrnek: \"Bearer 1safsfsdfdfd\"",
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
         {
             new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
  Id = "Bearer"
  }
  },
  new string[] {}
  }
  });
});

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
}));

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
	opt.SignIn.RequireConfirmedEmail = false;
	opt.SignIn.RequireConfirmedPhoneNumber = false;
	opt.SignIn.RequireConfirmedAccount = false;
	opt.User.RequireUniqueEmail = false;
	opt.Password.RequireUppercase = false;
	opt.Password.RequireNonAlphanumeric = false;
	opt.Password.RequiredLength = 3;
	opt.Password.RequireLowercase = false;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

// Configure JWT Authentication
//JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["secretKey"];

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        //ValidateLifetime = true,
        //ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["validIssuer"],
        ValidAudience = jwtSettings["validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});



//builder.Services.AddTransient<IGenreService, GenreService>();
//builder.Services.AddTransient<IGenreRepository, GenreRepository>();
////builder.Services.AddAutoMapper(typeof(Mapping));
//builder.Services.AddAutoMapper(typeof(Program));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
	builder.RegisterModule(new DependencyResolver());
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWTAuthDemo v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
