using AutoFixture;
using DryCleaningCompany.Application.Services;
using DryCleaningCompany.Infrastructure.Repositories;
using Moq;

namespace DryCleaningCompany.Application.Tests.Services
{
    [TestClass]
    public class ScheduleServiceTests
    {
        private Fixture fixture = new Fixture();
        private MockRepository mockRepository;

        private Mock<IUnitOfWork> mockUnitOfWork;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockUnitOfWork = this.mockRepository.Create<IUnitOfWork>();
        }

        private ScheduleService CreateService()
        {
            return new ScheduleService(this.mockUnitOfWork.Object);
        }

        [TestMethod]
        public async Task CalculateNewSchedule_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            DateTime date = fixture.Create<DateTime>();
            int minutes = fixture.Create<int>();

            // Act
            var result = await service.CalculateNewSchedule(date, minutes);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }
    }
}