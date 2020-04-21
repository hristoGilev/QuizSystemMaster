namespace QuizSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using QuizSystem.Data.Common.Repositories;
    using QuizSystem.Data.Models;
    using QuizSystem.Data.Repositories;
    using Xunit;

    public class ExamsServiceTests
    {
        [Fact]
        public async System.Threading.Tasks.Task GetByIdCorrectAsync()
        {
            var repository = new Mock<IDeletableEntityRepository<Exam>>();
            
            var repository1 = new Mock<IDeletableEntityRepository<Question>>();

            var repository2 = new Mock<IDeletableEntityRepository<QuestionMultiSelect>>();

            var repository3 = new Mock<IDeletableEntityRepository<ExamUser>>();
            var service = new ExamsService(
                repository.Object,
                repository1.Object,
                repository2.Object,
                repository3.Object);
            _ = await service.CreateAsync("Test", "test");
            Assert.Equal(2, repository.Invocations.Count);

            repository.Verify(x => x.All(), Times.Once);
        }
    }
}
