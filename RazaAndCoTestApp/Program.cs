using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RazaAndCoTestApp.Data;
using RazaAndCoTestApp.Midlleware;
using RazaAndCoTestApp.Services;
using System;
using System.Text;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//  Read JWT key
var jwtKey = builder.Configuration["Jwt:Key"]
             ?? "THIS_IS_A_SUPER_SECRET_KEY_RazaAndCo_%!@$#";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "RazaAndCo";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "RazaAndCoUsers";
if (string.IsNullOrEmpty(jwtKey))
{
    throw new Exception("JWT Key is missing in configuration");
}
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:3000")//  app origin
              .AllowAnyHeader()                     // Allow headers like Content-Type, Authorization
              .AllowAnyMethod()                     // Allow GET, POST, PUT, DELETE
              .AllowCredentials();                  // If you send cookies (optional)
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseInMemoryDatabase("RazaCoTestDb")
);


builder.Services.AddScoped<IJwtService, JwtService>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// Authorization
builder.Services.AddAuthorization();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RazaCo Technical Test API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token as: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.Seed(db);
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RazaCo Test API v1");
        c.RoutePrefix = string.Empty; // opens swagger at root
    });
}
app.UseCors("AllowAll");
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication(); // MUST be before authorization
app.UseAuthorization();

app.MapControllers();

app.Run();


