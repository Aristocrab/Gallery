using Gallery.WebApp.Services;

FillService.Fill();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapControllerRoute("default", "{controller=Albums}/{action=All}");
app.Run();