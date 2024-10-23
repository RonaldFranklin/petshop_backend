using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetShop.Modules.Services;

namespace PetShop.Data;

public class ServicesConfiguration : IEntityTypeConfiguration<ServiceModel>
{
    public void Configure(EntityTypeBuilder<ServiceModel> builder)
    {
        builder.ToTable("Servico");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ServiceType).HasColumnName("TipoServico").IsRequired();
        builder.Property(x => x.Price).HasColumnName("Preco");

        builder.HasMany(x => x.Schedulings).WithOne(p => p.Service).HasForeignKey(p => p.ServiceId).OnDelete(DeleteBehavior.Cascade);
    }
}