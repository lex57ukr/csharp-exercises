using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.Immutable;


public enum Schedule
{
    Teenth,
    First,
    Second,
    Third,
    Fourth,
    Last
}


public class Meetup
{
    delegate DateTime DateTimeSelector(IEnumerable<DateTime> days);

    static readonly Calendar Calendar
        = new GregorianCalendar(GregorianCalendarTypes.USEnglish);

    static readonly IDictionary<Schedule, DateTimeSelector> Schedulers
        = new Dictionary<Schedule, DateTimeSelector>
        {
            [Schedule.First]  = days => days.First(),
            [Schedule.Second] = days => days.Skip(1).First(),
            [Schedule.Third]  = days => days.Skip(2).First(),
            [Schedule.Fourth] = days => days.Skip(3).First(),
            [Schedule.Teenth] = days => days.Where(IsTeenth).First(),
            [Schedule.Last]   = days => days.Last(),
        }.ToImmutableDictionary();

    readonly ImmutableArray<DateTime> _days;

    public Meetup(int month, int year)
    {
        _days = Enumerable
            .Range(1, Calendar.GetDaysInMonth(year, month))
            .Select(day => new DateTime(year, month, day))
            .ToImmutableArray();
    }

    public DateTime Day(DayOfWeek dayOfWeek, Schedule schedule)
        => Schedulers[schedule].Invoke(Dates(dayOfWeek));

    IEnumerable<DateTime> Dates(DayOfWeek dayOfWeek)
        => _days.Where(day => day.DayOfWeek == dayOfWeek);

    static bool IsTeenth(DateTime value)
        => (value.Day >= 13 && value.Day <= 19);
}
