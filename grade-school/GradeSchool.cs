using System;
using System.Linq;
using System.Collections.Generic;


public class School
{
    IDictionary<int, IList<string>> _rostersByGrades
        = new Dictionary<int, IList<string>>();

    public void Add(string student, int grade)
    {
        IList<string> roster;
        if ( ! _rostersByGrades.TryGetValue(grade, out roster))
        {
            _rostersByGrades.Add(grade, roster = new List<string>());
        }

        roster.Add(student);
    }

    public IEnumerable<string> Roster()
    {
        return _rostersByGrades
            .Keys
            .OrderBy(grade => grade)
            .SelectMany(Grade);
    }

    public IEnumerable<string> Grade(int grade)
    {
        IList<string> roster;
        if ( ! _rostersByGrades.TryGetValue(grade, out roster))
        {
            return Enumerable.Empty<string>();
        }

        return roster.OrderBy(name => name);
    }
}
