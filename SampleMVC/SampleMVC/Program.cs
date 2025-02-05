var builder = WebApplication.CreateBuilder(args);
//register the controller with views
builder.Services.AddControllersWithViews();
var app = builder.Build();
/*app.MapGet("/", () => "Hello World!");
app.MapGet("/sayhi", () => "I am MG."); //port:/sayhi
app.MapGet("/me/ok/zip", () => "I am Zip."); //port:/me/ok/zip*/
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
//Map the controller  as the routes
app.MapDefaultControllerRoute();
app.Run();
