using Counter.Scripts.Contracts;

namespace Counter.Scripts.Service
{
    /// <summary>
    /// For holidays that occur on a specific day of a specific week of a month.
    /// </summary>
    public class FloatingHolidayService : IHolidayRule
    {
        private readonly int _month;
        private readonly DayOfWeek _dayOfWeek;
        private readonly int _occurrence;

        public FloatingHolidayService(int month, DayOfWeek dayOfWeek, int occurrence)
        {
            _month = month;
            _dayOfWeek = dayOfWeek;
            _occurrence = occurrence;
        }

        public bool IsHoliday(DateTime date)
        {
           return date.Month == _month && 
                date.Date == new DateTime(date.Year, _month, 1)
                .AddDays(((int)_dayOfWeek - (int)new DateTime(date.Year, _month, 1).DayOfWeek + 7) % 7)
                .AddDays((_occurrence - 1) * 7)
                .Date;
        }
    }
}
