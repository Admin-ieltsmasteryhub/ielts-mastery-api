using IeltsApp.Interface;
using IeltsApp;
using IeltsApp.DataAccess;
using IeltsApp.DataAccess.Services;
using IeltsApp.DataAccess.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options => { options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "IELTS App", Version = "v1" }); });
builder.Services.AddScoped<IOpenAIProxy>(provider => new OpenAIProxy(apiKey: "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", organizationId: "org-piSg8fs4ro5QyspbIqTToDe8"));

// Add services to the container.
builder.Services.Configure<IeltsMasterDatabaseSettings>(
    builder.Configuration.GetSection(nameof(IeltsMasterDatabaseSettings)));

builder.Services.AddSingleton<IIeltsMasterDatabaseSettings>(sp => sp.GetRequiredService<IOptions<IeltsMasterDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("IeltsMasterDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IBlogDatabaseService, BlogDatabaseService>();
builder.Services.AddCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
// Shows UseCors with CorsPolicyBuilder.
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "IELTS App"); });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
