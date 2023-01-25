using Exemplo.InMemory.Model;
using Microsoft.EntityFrameworkCore;

namespace Exemplo.InMemory.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
          : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}