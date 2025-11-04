using Microsoft.EntityFrameworkCore;
using ProdutosApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura o banco de dados SQLite com a connection string do appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona o suporte a controllers e views (MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Garante que o banco seja criado automaticamente se ainda não existir
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

// Configura o pipeline de requisições HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Rota padrão da aplicação
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Produtos}/{action=Index}/{id?}");

app.Run();
