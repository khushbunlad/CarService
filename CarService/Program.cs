using CarService.Models.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Entity framework 
builder.Services.AddDbContext<CarServiceContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CarService")));

// Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.IOTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.Name = "DamanUserInterfaceSessionKey";
    //options.IdleTimeout = TimeSpan.FromSeconds(10);
    //options.IOTimeout = TimeSpan.FromMinutes(10);
});

// Set the limit to 256 MB
builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
{   
    options.MultipartBodyLengthLimit = 268435456;
    options.BufferBodyLengthLimit = 268435456;
    options.ValueLengthLimit = 268435456;
    options.ValueCountLimit = 268435456;
    options.BufferBody = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
