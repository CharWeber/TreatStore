namespace TreatStore.Models
{
  public class TreatStoreContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet Treats {get;set;}
    public DbSet Flavors {get;set;}
    public DbSet FlavorTreats {get;set;}

    public TreatStoreContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}