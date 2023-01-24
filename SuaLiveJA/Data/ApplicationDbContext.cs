using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SuaLiveJA.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SuaLiveJA.Models.Contato> Cotato { get; set; } = default!;
        public DbSet<SuaLiveJA.Models.Evento> Evento { get; set; }
        public DbSet<SuaLiveJA.Models.Iasd> Iasd { get; set; }
        public DbSet<SuaLiveJA.Models.Secao> Secao { get; set; }
        public DbSet<SuaLiveJA.Models.Usuario> Usuario { get; set; }
    }
}