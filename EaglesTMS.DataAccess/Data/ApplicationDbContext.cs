

namespace EaglesTMS.DataAccess.Data
{
    public class ApplicationDbContext 
    {
    //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    //````{
    //    }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<ApplicationUser>().Property(x => x.CreationDate).HasDefaultValue(Convert.ToDateTime("1/1/2023"));
        //    builder.Entity<Nationalities>().Property(x => x.CreationDate).HasDefaultValue(Convert.ToDateTime("1/1/2023"));
        //}
        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        //{
        //    foreach (var entry in ChangeTracker.Entries<IBaseEntity>().ToList())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.Entity.CreationDate = DateTime.UtcNow;
        //            break;
        //        }
        //    }
        //    var result = await base.SaveChangesAsync(cancellationToken);
        //    return result;
        //}
        //public DbSet<Nationalities> Nationalities { get; set; }
        //public DbSet<Job> Jobs { get; set; }
    }
}
