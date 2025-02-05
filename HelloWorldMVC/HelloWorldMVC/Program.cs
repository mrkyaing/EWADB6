var builder = WebApplication.CreateBuilder(args);
//register the Controllers and Views as a routes 
builder.Services.AddControllersWithViews();
var app = builder.Build();
//routing mapping in here 
//https://{port}/
app.MapGet("/", () => "Hello World!");//mininal routing style
//https://{port}/sayme
app.MapGet("/SayMe", () => "I am MG.");
//https://{port}/sayme/myfriend
app.MapGet("/SayMe/MyFriend", () => "Su Su is my best friend.");
//Mapping the routes that defined actin method in controllers 
app.MapDefaultControllerRoute();
//finally run the the web app 
app.Run();
