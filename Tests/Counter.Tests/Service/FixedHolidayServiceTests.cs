using Counter.Scripts.Service;

namespace Counter.Tests.Service
{
    public class FixedHolidayServiceTests
    {
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
    }
}
