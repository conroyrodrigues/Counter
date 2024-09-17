using Counter.Scripts.Contracts;

namespace Counter.Scripts.Service
{
    /// <summary>
    /// For holidays that occur on a fixed date every year.
    /// </summary>
    public class FixedHolidayService : IHolidayRule
    {
        private readonly int _month;
        private readonly int _day;

        public FixedHolidayService(int month, int day)
        {
            _month = month;
            _day = day;
        }

        public bool IsHoliday(DateTime date)
        {
            return date.Month == _month && date.Day == _day;
        }
    }
}
