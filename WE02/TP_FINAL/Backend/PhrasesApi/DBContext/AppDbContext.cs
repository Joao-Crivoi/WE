using Microsoft.EntityFrameworkCore;
using PhrasesApi.Models;
using System.Collections.Generic;

namespace PhrasesApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
    }
}
