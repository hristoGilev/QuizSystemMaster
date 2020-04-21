using Moq;
using QuizSystem.Data.Common.Repositories;
using QuizSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace QuizSystem.Services.Data.Tests
{
    public class QuestionsServiceTests
    {
        [Fact]
        public void Create()
        {
            var repository = new Mock<IDeletableEntityRepository<Question>>();
            var service = new QuestionsService(repository.Object);
            _ = service.CreateAsync("Title", "Test", "1");

            Assert.Equal(2, repository.Invocations.Count);

            repository.Verify(x => x.All(), Times.Once);
        }
    }
}
