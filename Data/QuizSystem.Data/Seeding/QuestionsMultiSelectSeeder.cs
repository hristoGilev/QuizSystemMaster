using QuizSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizSystem.Data.Seeding
{
    internal class QuestionsMultiSelectSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.QuestionMultiSelects.Any())
            {
                return;
            }

            List<QuestionMultiSelect> list = new List<QuestionMultiSelect>
            {
              new QuestionMultiSelect { Title = "Question70", Description = "Кога е основана българската държава?", AnswerTypeA = "1669 г.", AnswerTypeB = "1244 г.", AnswerTypeC = "681 год." },
              new QuestionMultiSelect { Title = "Question71", Description = "Кой покръства българите?", AnswerTypeA = "Княз Борис I", AnswerTypeB = "Тодор Живков", AnswerTypeC = "Никой. Те са езичници." },
              new QuestionMultiSelect { Title = "Question72", Description = "Каква е приблизителната скорост на звука в въздушна среда?", AnswerTypeA = "220 м/с", AnswerTypeB = "320 м/с", AnswerTypeC = "420 м/с" },
              new QuestionMultiSelect { Title = "Question73", Description = "Каква е приблизителната скорост на светлината във вакум?", AnswerTypeA = "300 000 м/с", AnswerTypeB = "300 000 км/с", AnswerTypeC = "300 000 дм/с" },
              new QuestionMultiSelect { Title = "Question74", Description = "Кой написва романа Под игото?", AnswerTypeA = "Иван Вазов", AnswerTypeB = "Иван Куликов", AnswerTypeC = "Иван Костов" },
              new QuestionMultiSelect { Title = "Question75", Description = "Кое от числата е четно", AnswerTypeA = "111111", AnswerTypeB = "123333", AnswerTypeC = "22222222" },
              new QuestionMultiSelect { Title = "Question76", Description = "Кое от числата е нечетно", AnswerTypeA ="12345678", AnswerTypeB = "1237777", AnswerTypeC = "1236868" },
              new QuestionMultiSelect { Title = "Question77", Description = "Акула", AnswerTypeA ="Делфин", AnswerTypeB = "", AnswerTypeC = "Шаран" },
              new QuestionMultiSelect { Title = "Question78", Description = "Коя мерна единица е за темлература", AnswerTypeA ="Градус Целзий", AnswerTypeB = "Градус/метър", AnswerTypeC = "Джаул" },
            };

            foreach (var item in list)
            {
                await dbContext.QuestionMultiSelects.AddAsync(item);
            }
        }
    }
}
