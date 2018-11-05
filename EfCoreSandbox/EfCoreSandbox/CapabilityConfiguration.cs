using EfCoreSandbox.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreSandbox
{
    class CapabilityConfiguration : IEntityTypeConfiguration<Capability>
    {
        public void Configure(EntityTypeBuilder<Capability> builder)
        {
            builder.HasKey(p => new { p.AgentId, p.Name });
            builder.ToTable("Capability");
        }
    }
}
