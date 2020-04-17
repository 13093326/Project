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
        public DbSet<QuestionRating> QuestionRatings { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ReportTestHistory> ReportTestHistory { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<TestSet> TestSet { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
    }
}
