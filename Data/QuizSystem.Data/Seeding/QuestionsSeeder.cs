using Microsoft.EntityFrameworkCore;
using QuizSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizSystem.Data.Seeding
{
    internal class QuestionsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
           if (dbContext.Questions.Any())
            {
                return;
            }

           List<Question> questions = new List<Question>()

           { new Question { Title = "Question43", Description = "Напишете една аксиома за права линия в геометрията." },
            new Question { Title = "Question4", Description = "Намерете най-малкото общо кратно на две числа." },
            new Question { Title = "Question5", Description ="Дайте определение за точка." },
            new Question { Title = "Question59", Description = "Who is doctor Who" },
            new Question { Title = "Question567", Description = "What is 42?" },
            new Question { Title = "Question431", Description = "Кой е хан Тервил?" },
            new Question { Title = "Question52", Description = "Напишете теоремата на Питагор." },
            new Question { Title = "Question56", Description = "Кой е най високия връх в България?" },
            new Question { Title = "Question Question51", Description = "Напишете формулата за лице на кръг." },
           };
           foreach (var item in questions)
            {
                await dbContext.Questions.AddAsync(item);
            }
        }
    }
}
