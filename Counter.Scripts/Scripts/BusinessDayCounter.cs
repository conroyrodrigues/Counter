using Counter.Scripts.Contracts;

namespace Counter.Scripts.Scripts
{
    public class BusinessDayCounter
    {
        private readonly IEnumerable<IHolidayRule> _holidayRules;

        public BusinessDayCounter(IEnumerable<IHolidayRule> holidayRules)
        {
            _holidayRules = holidayRules;
        }
        /// <summary>
        ///  Calculates weekdays (Monday to Friday) between two dates.
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <returns></returns>
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            if (secondDate <= firstDate) return 0;

            var weekdaysCount = 0;
            var currentDate = firstDate.AddDays(1); // Exclude firstDate

            while (currentDate < secondDate) // Exclude secondDate
            {
                if (IsWeekday(currentDate)) weekdaysCount++;
                
                currentDate = currentDate.AddDays(1);
            }

            return weekdaysCount;
        }

        /// <summary>
        /// Calculates business days between two dates, excluding public holidays.
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <returns></returns>
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            if (secondDate <= firstDate) return 0;

            var businessDaysCount = 0;
            var currentDate = firstDate.AddDays(1); // Exclude firstDate

            while (currentDate < secondDate) // Exclude secondDate
            {
                if (IsWeekday(currentDate) && !IsHoliday(currentDate)) businessDaysCount++;

                currentDate = currentDate.AddDays(1);
            }

            return businessDaysCount;
        }

        /// <summary>
        /// Helper method to check if a date is a weekday (Monday-Friday
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool IsWeekday(DateTime date)
        {
            return date.DayOfWeek >= DayOfWeek.Monday && date.DayOfWeek <= DayOfWeek.Friday;
        }

        /// <summary>
        /// Check if a date matches any holiday rule
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool IsHoliday(DateTime date)
        {
            return _holidayRules.Any(rule => rule.IsHoliday(date));
        }
    }
}
