using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Modules.Scheduling;

namespace PetShop.Data;

public class SchedulingConfiguration : IEntityTypeConfiguration<SchedulingModel>
{
    public void Configure(EntityTypeBuilder<SchedulingModel> builder)
    {
        builder.ToTable("Agendamento");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Date).HasColumnName("Data").IsRequired();
        builder.Property(x => x.Time).HasColumnName("Hora").IsRequired();
        builder.Property(x => x.PetId).HasColumnName("PetId").IsRequired();
        builder.Property(x => x.ServiceId).HasColumnName("ServicoId").IsRequired();
        builder.Property(x => x.Status).HasColumnName("Status");

        builder.HasOne(x => x.Pet).WithMany(c => c.Schedulings).HasForeignKey(x => x.PetId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Service).WithMany(c => c.Schedulings).HasForeignKey(x => x.ServiceId).OnDelete(DeleteBehavior.Cascade);
    }
}
