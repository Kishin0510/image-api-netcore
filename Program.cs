using image_api_netcore.src.Data;
using image_api_netcore.src.Services.Implements;
using image_api_netcore.src.Services.Interfaces;
using image_api_netcore.src.Repositories.Implements;
using image_api_netcore.src.Repositories.Interfaces;
using System.Text;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
Cloudinary cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
cloudinary.Api.Secure = true;

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDBContext>(opt => opt.UseSqlite("Data Source=database.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();
var key = builder.Configuration.GetSection("AppSettings:Token").Value;
if (string.IsNullOrEmpty(key))
{
    throw new ArgumentNullException("AppSettings:Token", "JWT secret key is not configured.");
}
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalHost",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowLocalhost");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
