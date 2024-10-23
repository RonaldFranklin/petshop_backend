using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Modules.Users;

namespace PetShop.Data;

public class UserConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("Cliente");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasColumnName("Nome").IsRequired();
        builder.Property(x => x.Email).HasColumnName("Email").IsRequired();
        builder.Property(x => x.Password).HasColumnName("Senha").IsRequired();
        builder.Property(x => x.Role).HasColumnName("Perfil").IsRequired();
        builder.Property(x => x.Phone).HasColumnName("Telefone");
        builder.Property(x => x.Address).HasColumnName("Endereco");
        
        builder.HasMany(x => x.Pets).WithOne(p => p.Owner).HasForeignKey(p => p.OwnerId).OnDelete(DeleteBehavior.Cascade);
    }
}
