namespace Counter.Scripts.Contracts
{
    public interface IHolidayRule
    {
        bool IsHoliday(DateTime date);
    }
}
