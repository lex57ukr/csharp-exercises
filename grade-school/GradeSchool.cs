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
        if (_rostersByGrades.TryGetValue(grade, out roster))
        {
            roster.Add(student);
        }
        else
        {
            _rostersByGrades.Add(grade, new List<string> { student });
        }
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

        return roster
            .AsEnumerable()
            .OrderBy(name => name);
    }
}
