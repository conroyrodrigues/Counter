using Counter.Scripts.Contracts;
using Counter.Scripts.Scripts;
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

        [Fact]
        public void FloatingHoliday_CorrectlyIdentifiesHoliday()
        {
            // Arrange
            var holiday = new FloatingHolidayService(6, DayOfWeek.Monday, 2); // Second Monday of June
            var date = new DateTime(2023, 6, 12); // This is the second Monday of June 2023

            // Act
            var isHoliday = holiday.IsHoliday(date);

            // Assert
            Assert.True(isHoliday);
        }

        [Fact]
        public void FixedDateHoliday_CorrectlyIdentifiesHoliday()
        {
            // Arrange
            var holiday = new FixedHolidayService(12, 25); // Christmas
            var date = new DateTime(2023, 12, 25); // Christmas 2023

            // Act
            var isHoliday = holiday.IsHoliday(date);

            // Assert
            Assert.True(isHoliday);
        }

        [Fact]
        public void ShiftedHoliday_ShiftsToMondayWhenOnWeekend()
        {
            // Arrange
            var holiday = new ShiftedHolidayService(1, 1); // New Year's Day
            var newYearSaturday = new DateTime(2022, 1, 1); // Saturday
            var newYearObserved = new DateTime(2022, 1, 3); // Observed on Monday, Jan 3

            // Act
            var isHolidayOnSaturday = holiday.IsHoliday(newYearSaturday);
            var isHolidayOnMonday = holiday.IsHoliday(newYearObserved);

            // Assert
            Assert.False(isHolidayOnSaturday); // Not a holiday on the weekend
            Assert.True(isHolidayOnMonday); // Shifted to Monday
        }

    }
}
