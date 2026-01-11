using System.Text;
using GitArApi.Common.Mongo;
using GitArApi.AuthServiceApi.Services.Command.Abstractions;
using GitArApi.Common.Mongo.Constants;
using GitArApi.Common.Mongo.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.Configure<DocumentStoreConfiguration>(builder.Configuration.GetSection("ConnectionConfig"));
builder.Services.AddSingleton<MongoDbClient>();
builder.Services.AddScoped(typeof(IDocumentStore<>), typeof(MongoDbStore<>));

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtConfig"));

var jwtIssuer = builder.Configuration["JwtConfig:Issuer"] ?? "string";
var jwtAudience = builder.Configuration["JwtConfig:Audience"] ?? "string";
var jwtSecret = builder.Configuration["JwtConfig:Secret"] ?? "ASecretyKeyItis";

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
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration[jwtIssuer],
        ValidAudience = builder.Configuration[jwtAudience],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
