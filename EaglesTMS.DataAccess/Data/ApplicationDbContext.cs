namespace EaglesTMS.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        #region OnModelOld
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<ApplicationUser>().Property(x => x.CreationDate).HasDefaultValue(Convert.ToDateTime("1/1/2023"));
        //    builder.Entity<Nationalities>().Property(x => x.CreationDate).HasDefaultValue(Convert.ToDateTime("1/1/2023"));
        //}
        #endregion
        #region SaveChangesAsync
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
        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Job>()
            .Property(e => e.CreationDate)
            .HasDefaultValue(DateTime.UtcNow);
            base.OnModelCreating(builder);
        }
        public DbSet<Nationalities> Nationalities { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
    }
}
