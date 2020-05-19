using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RevisionApplication.Models;

namespace RevisionApplication.Repository
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        // Set the objects for the code first database. 
        public DbSet<QuestionRating> QuestionRating { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<TestQuestion> TestQuestion { get; set; }
        public DbSet<TestSet> TestSet { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<UserSetting> UserSetting { get; set; }
        public DbSet<UnitSelection> UnitSelection { get; set; }
    }
}
