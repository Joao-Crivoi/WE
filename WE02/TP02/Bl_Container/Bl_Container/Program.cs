using Bl_Container.Repository;
using Bl_Container.Repository.impl;
using Bl_Container.Services;

var builder = WebApplication.CreateBuilder(args);

// Connection string
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Add services to the container.
builder.Services.AddControllersWithViews();


// Registrando Repositories
builder.Services.AddTransient<IBLRepository>(sp => new BLRepository(connectionString));
builder.Services.AddTransient<IContainerRepository>(sp => new ContainerRepository(connectionString));

// Registrando Services
builder.Services.AddTransient<IBLService, BLService>();
builder.Services.AddTransient<IContainerService, ContainerService>();

builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
