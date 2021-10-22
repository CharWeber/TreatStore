using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TreatStore.Models
{
  public class TreatStoreContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet <Treat> Treats {get;set;}
    public DbSet <Flavor> Flavors {get;set;}
    public DbSet <FlavorTreat> FlavorTreats {get;set;}

    public TreatStoreContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}