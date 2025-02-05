var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.MapControllerRoute("default", "{controller=convertor}/{action=convertorV1}/{id?}");
app.Run();
