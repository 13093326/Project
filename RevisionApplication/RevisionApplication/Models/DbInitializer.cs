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
            // V2 
            if (!context.Questions.Any())
            {
                context.AddRange
                    (
                        new Question { Content = "Question 1", Answer1 = "true", Answer2 = "false", Answer3 = "false", Answer4 = "false", CorrectAnswer = 1, Reference = "Reference 1" },
                        new Question { Content = "Question 2", Answer1 = "false", Answer2 = "true", Answer3 = "false", Answer4 = "false", CorrectAnswer = 2, Reference = "Reference 2" },
                        new Question { Content = "Question 3", Answer1 = "false", Answer2 = "false", Answer3 = "true", Answer4 = "false", CorrectAnswer = 3, Reference = "Reference 3" },
                        new Question { Content = "Question 4", Answer1 = "false", Answer2 = "false", Answer3 = "false", Answer4 = "true", CorrectAnswer = 4, Reference = "Reference 4" }
                    );

                context.SaveChanges();
            }
        }
    }
}
