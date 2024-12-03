using AutoFixture;
using DryCleaningCompany.Application.Services;
using DryCleaningCompany.Domain.Entities;
using DryCleaningCompany.Infrastructure.Repositories;
using Moq;
using System.Globalization;

namespace DryCleaningCompany.Application.Tests.Services
{
    [TestClass]
    public class ScheduleServiceTests
    {
        private Fixture fixture = new Fixture();
        private MockRepository mockRepository;

        private Mock<IUnitOfWork> mockUnitOfWork;
        private Mock<IGenericRepository<ShopSchedule>> mockShopScheduleRepository;
        private Mock<IGenericRepository<Schedule>> mockScheduleRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockUnitOfWork = this.mockRepository.Create<IUnitOfWork>();

            this.mockShopScheduleRepository = this.mockRepository.Create<IGenericRepository<ShopSchedule>>();
            this.mockScheduleRepository = this.mockRepository.Create<IGenericRepository<Schedule>>();
            this.RepositoryMockSetup();
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
            Assert.IsNotNull(result);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        [DataRow(120, "2010-06-07 09:10", "2010-06-07 11:10")]
        [DataRow(15, "2010-06-08 14:48", "2010-06-10 09:03")]
        [DataRow(420, "2010-12-24 06:45", "2010-12-27 11:00")]
        public async Task CalculateNewSchedule_ReturnDate_ExpectedBehavior(int minutes, string date, string expected)
        {
            // Arrange
            var service = this.CreateService();
            var dateFormat = "yyyy-MM-dd HH:mm";
            var dateParse = DateTime.ParseExact(date, dateFormat, CultureInfo.InvariantCulture);

            // Act
            var result = await service.CalculateNewSchedule(dateParse, minutes);

            // Assert
            Assert.AreEqual(expected, result.FinalDate.ToString(dateFormat));
            this.mockRepository.VerifyAll();
        }

        private void RepositoryMockSetup()
        {
            var result = new List<ShopSchedule>()
            {
                new ShopSchedule()
                {
                    BusinessHourType = Domain.Core.BusinessHourType.Open,
                    OpeningHour = new TimeOnly(9, 0),
                    ClosingHour = new TimeOnly(15, 0),
                },

                new ShopSchedule()
                {
                    BusinessHourType = Domain.Core.BusinessHourType.Open,
                    Day = DayOfWeek.Friday,
                    OpeningHour = new TimeOnly(10, 0),
                    ClosingHour = new TimeOnly(17, 0),
                },

                new ShopSchedule()
                {
                    BusinessHourType = Domain.Core.BusinessHourType.Open,
                    Date = new DateOnly(2010, 12, 24),
                    OpeningHour = new TimeOnly(8, 0),
                    ClosingHour = new TimeOnly(13, 0),
                },

                new ShopSchedule()
                {
                    BusinessHourType = Domain.Core.BusinessHourType.Close,
                    Day = DayOfWeek.Sunday
                },

                new ShopSchedule()
                {
                    BusinessHourType = Domain.Core.BusinessHourType.Close,
                    Day = DayOfWeek.Wednesday
                },

                new ShopSchedule()
                {
                    BusinessHourType = Domain.Core.BusinessHourType.Close,
                    Date = new DateOnly(2010, 12, 25)
                }
            };

            this.mockShopScheduleRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(result);
            this.mockUnitOfWork.Setup(x => x.ShopSchedules).Returns(this.mockShopScheduleRepository.Object);

            this.mockScheduleRepository.Setup(x => x.AddAsync(It.IsAny<Schedule>())).Returns(Task.CompletedTask);
            this.mockUnitOfWork.Setup(x => x.Schedules).Returns(this.mockScheduleRepository.Object);

            this.mockUnitOfWork.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
        }
    }
}