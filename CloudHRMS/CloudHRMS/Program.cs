using CloudHRMS.DAO;
using CloudHRMS.Services;
using CloudHRMS.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
//Add the Identity UIs
builder.Services.AddRazorPages();
//add the user and role to dbContext 
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<HRMSDbContext>();
var config = builder.Configuration;
builder.Services.AddDbContext<HRMSDbContext>(o => o.UseSqlServer(config.GetConnectionString("CloudHRMSConnectionString")));
//Add the IUnit Of work to access database operation process .
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IPositionService, PositionService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IShiftService, ShiftService>();
builder.Services.AddTransient<IShiftAssignService, ShiftAssignService>();
builder.Services.AddTransient<IAttendancePolicyService, AttendancePolicyService>();
builder.Services.AddTransient<IDailyAttendanceService, DailyAttendanceService>();
builder.Services.AddTransient<IAttendanceMasterService, AttendanceMasterService>();
builder.Services.AddTransient<IPayrollService, PayrollService>();
builder.Services.AddTransient<IUserService, UserService>();
//adding the connection to the PostgreSQL database
//builder.Services.AddDbContext<HRMSDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//frist enable Authenticaiton Process 
app.UseAuthentication();
//second enable Authorization process
app.UseAuthorization();
app.UseStatusCodePages(async context => {
    if (context.HttpContext.Response.StatusCode == 404) {
        context.HttpContext.Response.Redirect("/Home/AccessDenied");
    }
});
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
//Map the razor page routes
app.MapRazorPages();
app.Run();
