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
                var Unit1 = new Unit { Name = "Unit 1" };
                var Unit2 = new Unit { Name = "Unit 2" };
                var Unit3 = new Unit { Name = "Unit 3" };
                var Unit4 = new Unit { Name = "Unit 4" };

                context.AddRange
                    (
                        Unit1,
                        Unit2,
                        Unit3,
                        Unit4,
                        new Question { Content = "Question 1", Answer1 = "true", Answer2 = "false", Answer3 = "false", Answer4 = "false", CorrectAnswer = 1, Reference = "Reference 1", Unit = Unit1 },
                        new Question { Content = "Question 2", Answer1 = "false", Answer2 = "true", Answer3 = "false", Answer4 = "false", CorrectAnswer = 2, Reference = "Reference 2", Unit = Unit1 },
                        new Question { Content = "Question 3", Answer1 = "false", Answer2 = "false", Answer3 = "true", Answer4 = "false", CorrectAnswer = 3, Reference = "Reference 3", Unit = Unit1 },
                        new Question { Content = "Question 4", Answer1 = "false", Answer2 = "false", Answer3 = "false", Answer4 = "true", CorrectAnswer = 4, Reference = "Reference 4", Unit = Unit1 },
                        new Question { Content = "Question 5", Answer1 = "true", Answer2 = "false", Answer3 = "false", Answer4 = "false", CorrectAnswer = 1, Reference = "Reference 5", Unit = Unit2 },
                        new Question { Content = "Question 6", Answer1 = "false", Answer2 = "true", Answer3 = "false", Answer4 = "false", CorrectAnswer = 2, Reference = "Reference 6", Unit = Unit2 },
                        new Question { Content = "Question 7", Answer1 = "false", Answer2 = "false", Answer3 = "true", Answer4 = "false", CorrectAnswer = 3, Reference = "Reference 7", Unit = Unit3 },
                        new Question { Content = "Question 8", Answer1 = "false", Answer2 = "false", Answer3 = "false", Answer4 = "true", CorrectAnswer = 4, Reference = "Reference 8", Unit = Unit3 },
                        new Question { Content = "Question 9", Answer1 = "false", Answer2 = "false", Answer3 = "true", Answer4 = "false", CorrectAnswer = 3, Reference = "Reference 9", Unit = Unit4 },
                        new Question { Content = "Question 10", Answer1 = "false", Answer2 = "false", Answer3 = "false", Answer4 = "true", CorrectAnswer = 4, Reference = "Reference 10", Unit = Unit4 }
                    );

                context.SaveChanges();
            }
        }
    }
}
