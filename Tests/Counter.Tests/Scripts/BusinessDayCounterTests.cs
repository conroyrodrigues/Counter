using Counter.Scripts.Contracts;
using Counter.Scripts.Scripts;
using Counter.Scripts.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Counter.Tests.Scripts
{
    public class BusinessDayCounterTests
    {
        private BusinessDayCounter _counter;

        public BusinessDayCounterTests()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IHolidayRule>(new FixedHolidayService(4, 25)); // Anzac Day
            services.AddSingleton<IHolidayRule>(new FixedHolidayService(12, 25)); // Christmass Day
            services.AddSingleton<IHolidayRule>(new FixedHolidayService(12, 26)); // Boxing Day
            services.AddSingleton<IHolidayRule>(new ShiftedHolidayService(1, 1));   // New Year's Day
            services.AddSingleton<IHolidayRule>(new FloatingHolidayService(6, DayOfWeek.Monday, 2)); // Queen's Birthday on the second Monday in June

            services.AddTransient<BusinessDayCounter>();

            var serviceProvider = services.BuildServiceProvider();

            _counter = serviceProvider.GetService<BusinessDayCounter>();
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_ReturnsCorrectCount_WithFixedAndShiftedHolidays()
        {
            // Arrange
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2013, 10, 9);

            // Act
            var result = _counter.BusinessDaysBetweenTwoDates(firstDate, secondDate);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_ReturnsCorrectCount_With_Holidays()
        {
            // Arrange
            var firstDate = new DateTime(2013, 12, 24);
            var secondDate = new DateTime(2013, 12, 27);

            // Act
            var result = _counter.BusinessDaysBetweenTwoDates(firstDate, secondDate);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_WithComplexHolidays_ReturnsCorrectCount()
        {
            // Arrange
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2014, 1, 1);

            // Act
            var result = _counter.BusinessDaysBetweenTwoDates(firstDate, secondDate);

            // Assert
            Assert.Equal(59, result);
        }

        [Fact]
        public void WeekdaysBetweenTwoDates_ReturnsCorrectCount()
        {
            // Arrange
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2013, 10, 9);

            // Act
            var result = _counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void WeekdaysBetweenTwoDates_ReturnsCorrectCount_WithoutPublicHolidays()
        {
            // Arrange
            var firstDate = new DateTime(2013, 10, 5);
            var secondDate = new DateTime(2013, 10, 14);

            // Act
            var result = _counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void WeekdaysBetweenTwoDates_WithComplexHolidays_ReturnsCorrectCount()
        {
            // Arrange
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2014, 1, 1);

            // Act
            var result = _counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            // Assert
            Assert.Equal(61, result);
        }

        [Fact]
        public void WeekdaysBetweenTwoDates_ReturnsZero_WhenSecondDateIsBeforeFirstDate()
        {
            // Arrange
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2013, 10, 5);

            // Act
            var result = _counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            // Assert
            Assert.Equal(0, result);
        }

    }
}
