using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RevisionApplication.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<TestSet> TestSet { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
    }
}
