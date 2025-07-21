using GitArApi.Common.Mongo;
using GitArApi.Common.Mongo.Constants;
using GitArApi.Common.Mongo.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DocumentStoreConfiguration>(builder.Configuration.GetSection("ConnectionConfig"));
builder.Services.AddSingleton<MongoDbClient>();
builder.Services.AddSingleton(typeof(IDocumentStore<>), typeof(MongoDbStore<>));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();
