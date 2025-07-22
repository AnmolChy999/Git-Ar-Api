using System.Text;
using GitArApi.Common.Constants;
using GitArApi.Common.Mongo;
using GitArApi.Common.Mongo.Constants;
using GitArApi.Common.Mongo.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = JWTConstants.Issuer,
            ValidateIssuer = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTConstants.SecretKey)),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidAudience = "GitArApi"
        };
    }
    );


// Add services to the container.
builder.Services.Configure<DocumentStoreConfiguration>(builder.Configuration.GetSection("ConnectionConfig"));
builder.Services.AddSingleton<MongoDbClient>();
builder.Services.AddSingleton(typeof(IDocumentStore<>), typeof(MongoDbStore<>));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization(); 

app.MapControllers();

app.Run();
