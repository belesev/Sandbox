using EfCoreSandbox.Model;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSandbox
{
    class AgentContext : DbContext
    {
        public virtual DbSet<Agent> Agents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
                optionsBuilder.UseSqlServer(@"Server=SERG\RESTO;Database=Agents;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgentConfiguration());
            modelBuilder.ApplyConfiguration(new CapabilityConfiguration());
        }
    }
}
