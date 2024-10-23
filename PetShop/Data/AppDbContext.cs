using Microsoft.EntityFrameworkCore;
using PetShop.Modules.Pets;
using PetShop.Modules.Scheduling;
using PetShop.Modules.Services;
using PetShop.Modules.Users;

namespace PetShop.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ServiceModel> Services => Set<ServiceModel>();
    public DbSet<PetModel> Pets => Set<PetModel>();
    public DbSet<UserModel> Users => Set<UserModel>();
    public DbSet<SchedulingModel> Schedulings => Set<SchedulingModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ServicesConfiguration());
        modelBuilder.ApplyConfiguration(new PetsConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new SchedulingConfiguration());
    }
}