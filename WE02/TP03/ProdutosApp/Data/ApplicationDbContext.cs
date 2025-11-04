using Microsoft.EntityFrameworkCore;
using ProdutosApp.Models;

namespace ProdutosApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; } = default!;
    }
}