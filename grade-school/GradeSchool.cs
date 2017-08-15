using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


public class School
{
    IImmutableList<(int grade, string name)> _roster
        = ImmutableList<(int grade, string name)>.Empty;

    public void Add(string student, int grade)
    {
        var rec = (grade: grade, name: student);
        _roster = _roster.Add(rec);
    }

    public IEnumerable<int> Grades() => _roster
        .OrderBy(r => r.grade)
        .Select(r => r.grade)
        .Distinct();

    public IEnumerable<string> Roster() => Grades().SelectMany(Grade);

    public IEnumerable<string> Grade(int grade) => _roster
        .Where(r => r.grade == grade)
        .OrderBy(r => r.name)
        .Select(r => r.name);
}
