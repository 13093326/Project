using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Questions.Any())
            {
                context.AddRange
                    (
                        new Question { Content = "Question 1" },
                        new Question { Content = "Question 2" }
                    );

                context.SaveChanges();
            }
        }
    }
}
