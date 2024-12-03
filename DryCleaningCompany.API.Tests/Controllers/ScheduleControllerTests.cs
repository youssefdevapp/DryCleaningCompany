using AutoFixture;
using DryCleaningCompany.API.Controllers;
using DryCleaningCompany.API.Dtos;
using DryCleaningCompany.Domain.Entities;
using DryCleaningCompany.Domain.Services;
using Moq;

namespace DryCleaningCompany.API.Tests.Controllers
{
    [TestClass]
    public class ScheduleControllerTests
    {
        private Fixture fixture = new Fixture();
        private MockRepository mockRepository;

        private Mock<IScheduleService> mockScheduleService;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockScheduleService = this.mockRepository.Create<IScheduleService>();
        }

        private ScheduleController CreateScheduleController()
        {
            return new ScheduleController(
                this.mockScheduleService.Object);
        }

        [TestMethod]
        public async Task Calculate_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var scheduleController = this.CreateScheduleController();
            ScheduleRequest request = fixture.Create<ScheduleRequest>();
            Schedule entity = fixture.Create<Schedule>();
            mockScheduleService.Setup(r => r.CalculateNewSchedule(request.Date, request.Minutes)).ReturnsAsync(entity);

            // Act
            var result = await scheduleController.Calculate(request);

            // Assert
            Assert.IsNotNull(result);
            this.mockRepository.VerifyAll();
        }
    }
}