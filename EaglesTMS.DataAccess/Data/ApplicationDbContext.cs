

namespace EaglesTMS.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().Property(x => x.CreationDate).HasDefaultValue(Convert.ToDateTime("1/1/2023"));
            builder.Entity<Nationalities>().Property(x => x.CreationDate).HasDefaultValue(Convert.ToDateTime("1/1/2023"));

        }

        public DbSet<Nationalities> Nationalities { get; set; }
    }
}
