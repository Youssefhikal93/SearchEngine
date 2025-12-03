using Search.API.Services;
using Search.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient();
builder.Services.AddScoped<ISearchEngine, GoogleSearchService>();
builder.Services.AddScoped<ISearchEngine, WikipediaSearchService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:57455")
              .AllowAnyMethod() 
              .AllowAnyHeader();
    });
});

builder.Services.AddMemoryCache();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthorization();

app.MapControllers();

app.Run();
