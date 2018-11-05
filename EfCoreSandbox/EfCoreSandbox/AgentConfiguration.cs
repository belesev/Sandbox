using EfCoreSandbox.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreSandbox
{
    class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id);
            builder.HasMany(p => p.Capabilities)
                .WithOne()
                .HasForeignKey("AgentId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("Agent");
        }
    }
}
