using AuthKeeper.Mapping;
using HRM_Project.DTOs.Response;
using HRM_Project.Middlewares;
using HRM_Project.Models.DB;
using HRM_Project.Models.Options;
using HRM_Project.Services.Implementations;
using HRM_Project.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // Add this for MySQL
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string MyAllowSpecificOrigins = "MyAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .WithOrigins()
                                 .AllowCredentials();
                      });
});
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errorResponse = context.ModelState
                .Where(x => x.Value.Errors.Any())
                .SelectMany(x => x.Value.Errors.Select(e => new ValidationErrorDto(x.Key, e.ErrorMessage)))
                .ToList();

            return new BadRequestObjectResult(errorResponse);
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                                {
                                    {
                                        new OpenApiSecurityScheme
                                        {
                                            Reference = new OpenApiReference
                                            {
                                                Type = ReferenceType.SecurityScheme,
                                                Id = "Bearer"
                                            },
                                            Scheme = "oauth2",
                                            Name = "Bearer",
                                            In = ParameterLocation.Header,
                                        },
                                        new List<string>()
                                    }
                                });
});

//Connecting to MySql server
var connectionString = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();

//SPA
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp";
});

//Implementation
builder.Services.AddSingleton<IPasswordHasher<BaseUser>, PasswordHasher<BaseUser>>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<TokenManagerMiddleware>();
builder.Services.AddSingleton<ILogService, LogService>();
builder.Services.AddTransient<ITokenManager, TokenManager>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<ICityService, CityService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddTransient<IDemoService, DemoService>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();

//Service for AutoMapping
builder.Services.AddAutoMapper(typeof(MappingProfile));

// JWT Bearer
var jwtSection = builder.Configuration.GetSection("jwt");
var jwtOptions = new JwtOptions();
jwtSection.Bind(jwtOptions);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = jwtOptions.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
        };
    });

builder.Services.Configure<JwtOptions>(jwtSection);

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApiLogHandlerMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<TokenManagerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseSpaStaticFiles();

// send post requests with incorrect addresses to SPA
app.Use(async (context, next) =>
{
    if (context.Request.Method != HttpMethods.Get)
        context.Request.Method = HttpMethods.Get;
    await next.Invoke();
});

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";
});

app.Run();
