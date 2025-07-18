
using Scalar.AspNetCore;
using GitArApi.SongStoreApi.Repository.Configuration;
using GitArApi.SongStoreApi.Services.Query;
using GitArApi.SongStoreApi.Services.Query.Abstractions;
using GitArApi.SongStoreApi.Services.Command;
using GitArApi.SongStoreApi.Services.Command.Abstractions;
using GitArApi.SongStoreApi.Repository;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.Configure<DocumentStoreConfiguration>(builder.Configuration.GetSection("ConnectionConfig"));
builder.Services.AddSingleton<MongoDbClient>();
builder.Services.AddSingleton(typeof(IDocumentStore<>), typeof(MongoDbStore<>));



builder.Services.AddSingleton<ISongQueryServices, SongQueryServices>();
builder.Services.AddSingleton<ISongCommandServices, SongCommandServices>();




// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
