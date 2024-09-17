using Counter.Scripts.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
