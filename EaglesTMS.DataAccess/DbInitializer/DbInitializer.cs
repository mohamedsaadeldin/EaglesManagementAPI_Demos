
namespace EaglesTMS.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }

        public void Initialize()
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }
            if (!_roleManager.RoleExistsAsync(SD.EaglesAdmins).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.EaglesAdmins)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.CompanyAdmins)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Accountant)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.TechnicalSupport)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.BasicAdminForCompany)).GetAwaiter().GetResult();

                var user = new ApplicationUser();
                user.Email = "mkhalph@egyeagles.com";
                user.UserName = user.Email;
                user.FirstName = "mohammed";
                user.LastName = "khalph";
                user.CountryId = 63;
                user.PhoneNumber = "00201005177409";
                user.CreationDate = DateTime.UtcNow;
                user.IsActive = true;
                user.IsDeleted = false;
                var result = _userManager.CreateAsync(user, "Aa_123456789$$").GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, SD.EaglesAdmins).GetAwaiter().GetResult();
                }
                var token = _userManager.GenerateEmailConfirmationTokenAsync(user).GetAwaiter().GetResult();
                _userManager.ConfirmEmailAsync(user, token).GetAwaiter().GetResult();

            }

        }


    }
}
