using Counter.Scripts.Service;

namespace Counter.Tests.Service
{
    public class FloatingHolidayServiceTests
    {
        [Fact]
        public void FloatingHoliday_CorrectlyIdentifiesHoliday()
        {
            // Arrange
            var holiday = new FloatingHolidayService(6, DayOfWeek.Monday, 2); // Second Monday of June
            var date = new DateTime(2023, 6, 12); // This is the second Monday of June 2023

            // Act
            var isHoliday = holiday.IsHoliday(date);

            // Assert

        }
    }
 }
