using SimpleWebAPIWithNoSQL.Config;
using SimpleWebAPIWithNoSQL.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.Configure<DbSetting>(builder.Configuration.GetSection("POSDatabase"));
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI();
    app.UseSwagger();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
