using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Interfaces.Repo;
using Core.Interfaces.Services;
using Core.Services;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<ILogRepository, LogRepository>();

JwtSecurityTokenHandler.DefaultMapInboundClaims = true;

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.UseSecurityTokenValidators = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {            
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
            NameClaimType = ClaimTypes.Name          
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("JWT Authentication Failed: " + context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("JWT Authentication Succeeded");

                if (context.Principal?.Identity is ClaimsIdentity identity)
                {
                    Console.WriteLine("Claims Received:");
                    foreach (var claim in identity.Claims)
                    {
                        Console.WriteLine($"  • {claim.Type} = {claim.Value}");
                    }

                    var hasRole = identity.Claims.Any(c =>
                        c.Type == ClaimTypes.Role || c.Type == "role" ||
                        c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

                    Console.WriteLine(hasRole
                        ? "Role claim was received."
                        : "No role claim found.");
                }
                else
                {
                    Console.WriteLine("No ClaimsIdentity available.");
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var authHeader = context.Request.Headers["Authorization"].ToString();
    Console.WriteLine($"[DEBUG] Authorization Header: {authHeader}");

    if (context.User?.Identity?.IsAuthenticated == true)
    {
        Console.WriteLine($"[DEBUG] Authenticated User: {context.User.Identity.Name}");

        foreach (var claim in context.User.Claims)
        {
            Console.WriteLine($"[DEBUG] Claim: {claim.Type} = {claim.Value}");
        }

        Console.WriteLine($"[DEBUG] Roles (IsInRole): Logger = {context.User.IsInRole("Logger")}");
    }
    else
    {
        Console.WriteLine("[DEBUG] User is NOT authenticated.");
    }

    await next();
});



app.MapControllers();

app.Run();
