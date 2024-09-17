using Counter.Scripts.Contracts;

namespace Counter.Scripts.Service
{
    /// <summary>
    /// For holidays that move if they fall on a weekend.
    /// </summary>
    public class ShiftedHolidayService: IHolidayRule
    {
        private readonly int _month;
        private readonly int _day;

        public ShiftedHolidayService(int month, int day)
        {
            _month = month;
            _day = day;
        }

        public bool IsHoliday(DateTime date)
        {
            var holiday = new DateTime(date.Year, _month, _day);

            DateTime adjustedHoliday = holiday.AddDays( 
                holiday.DayOfWeek == DayOfWeek.Saturday ? 2 
                : holiday.DayOfWeek == DayOfWeek.Sunday ? 1 : 0 );

            return adjustedHoliday.Date == date.Date;
        }
    }
}
