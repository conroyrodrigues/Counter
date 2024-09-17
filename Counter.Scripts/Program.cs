using Counter.Scripts.Contracts;
using Counter.Scripts.Scripts;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<IHolidayRule>(new FixedHolidayService(4, 25)); // Anzac Day
services.AddSingleton<IHolidayRule>(new FixedHolidayService(12, 25)); // Christmass Day
services.AddSingleton<IHolidayRule>(new FixedHolidayService(12, 26)); // Boxing Day
services.AddSingleton<IHolidayRule>(new ShiftedHolidayService(1, 1));   // New Year's Day
services.AddSingleton<IHolidayRule>(new FloatingHolidayService(6, DayOfWeek.Monday, 2)); // Queen's Birthday on the second Monday in June

services.AddTransient<BusinessDayCounter>();

var serviceProvider = services.BuildServiceProvider();

var counter = serviceProvider.GetService<BusinessDayCounter>();

// Use the BusinessDayCounter
var startDate = new DateTime(2013, 10, 7);
var endDate = new DateTime(2013, 10, 9);

var weekdays = counter.WeekdaysBetweenTwoDates(startDate, endDate);
var businessDays = counter.BusinessDaysBetweenTwoDates(startDate, endDate);

Console.WriteLine($"Weekdays: {weekdays}");
Console.WriteLine($"Business Days: {businessDays}");

Console.ReadLine();
