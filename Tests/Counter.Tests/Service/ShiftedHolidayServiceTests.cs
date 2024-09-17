using Counter.Scripts.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counter.Tests.Service
{
    public class ShiftedHolidayServiceTests
    {
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
