using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskManager.API.Filters;
using TaskManager.API.Middleware;
using TaskManager.Application.Interfacies;
using TaskManager.Application.Mapping;
using TaskManager.Application.Validators;
using TaskManager.Domain.Enums;
using TaskManager.Infrastructure.Authentication;
using TaskManager.Infrastructure.Identity;
using TaskManager.Infrastructure.Persistence;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddValidatorsFromAssemblyContaining<CreateProjectDtoValidator>();



builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
	options.Password.RequiredLength = 8;
	options.Password.RequireNonAlphanumeric = false;
	options.User.RequireUniqueEmail = true;
}
)
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
    builder.Services.AddScoped<ITaskRepository, TaskRepository>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAutoMapper(op=>op.AddProfile<MappingProfile>());

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>())
	.AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
    var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()!;

builder.Services.AddAuthentication(options=>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options=>
{

	options.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = jwt.Issuer,
		ValidAudience = jwt.Audience,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
	};
}
);

builder.Services
	.AddExceptionHandler<GlobalExceptionHandling>();

builder.Services.AddProblemDetails();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	foreach (var roleName in Enum.GetNames<UserRole>())
	{
		if (!await roleManager.RoleExistsAsync(roleName))
		{
			await roleManager.CreateAsync(new IdentityRole(roleName));
		}
	}
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();


app.UseCors("AllowAll"); 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

