using System;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using static System.Linq.Enumerable;


public static class FoodChain
{
    public static string Recite(int startVerse, int endVerse)
    {
        var verses = Range(
            startVerse,
            endVerse - startVerse + 1
        ).Select(x => Verses[x - 1]);

        var text = string.Join("\n\n", verses);
        return text;
    }

    private static readonly Verse[] Verses = new Verse("fly")
        .I("don't know why she swallowed the fly. Perhaps she'll die").Stop()
        .Swallowed("spider").It("wriggled and jiggled and tickled inside her").Stop()
        .Swallowed("bird").How("absurd to swallow a bird").Exclaim()
        .Swallowed("cat").Imagine("that, to swallow a cat").Exclaim()
        .Swallowed("dog").What("a hog, to swallow a dog").Exclaim()
        .Swallowed("goat").Just("opened her throat and swallowed a goat").Exclaim()
        .Swallowed("cow").I("don't know how she swallowed a cow").Exclaim()
        .Swallowed("horse").She("'s dead, of course").Exclaim()
        .Done()
        .ToArray();

    [DebuggerDisplay("{Subject}")]
    private class Verse
        : IEnumerable<Verse>
    {
        private Verse _next, _prev;
        private string _opinion, _action;
        private char _punctuation;
        private bool _that;

        public string Subject { get; }

        public Verse(string subject, Verse prev = null)
        {
            _prev = prev;
            this.Subject = subject;
        }

        public Verse It(string action)
            => Opinionate(nameof(It), action).Do(() => _that = true);

        public Verse How(string action)
            => Opinionate(nameof(How), action);

        public Verse Imagine(string action)
            => Opinionate(nameof(Imagine), action);

        public Verse What(string action)
            => Opinionate(nameof(What), action);

        public Verse Just(string action)
            => Opinionate(nameof(Just), action);

        public Verse I(string action)
            => Opinionate(nameof(I), action);

        public Verse She(string action)
            => Opinionate(nameof(She), action);

        public Verse Stop()
            => Do(() => _punctuation = '.');

        public Verse Exclaim()
            => Do(() => _punctuation = '!');

        public Verse Swallowed(string subject)
            => _next = new Verse(subject, this);

        public Verse Done()
            => _prev?.Done() ?? this;

        public IEnumerator<Verse> GetEnumerator()
        {
            var curr = this;
            do
            {
                yield return curr;
                curr = curr._next;
            } while (curr != null);
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public override string ToString()
        {
            var buff = new StringBuilder();

            buff.AppendFormat(
                "I know an old lady who swallowed a {0}.",
                this.Subject
            );

            AddOpinion(buff);

            if (_next != null)
            {
                _prev?.AddEverythingSwallowed(buff);
            }

            return buff.ToString();
        }

        private void AddEverythingSwallowed(StringBuilder buff)
        {
            buff.Append("\n");
            buff.AppendFormat(
                "She swallowed the {0} to catch the {1}",
                _next.Subject,
                this.Subject
            );

            if (_that)
            {
                buff.AppendFormat(" that {0}", _action);
            }

            buff.Append(".");

            if (_prev != null)
            {
                _prev.AddEverythingSwallowed(buff);
            }
            else
            {
                AddOpinion(buff);
            }
        }

        private void AddOpinion(StringBuilder buff)
        {
            buff.Append("\n");
            buff.Append(_opinion);
            buff.Append(_punctuation);
        }

        private Verse Opinionate(string start, string action)
        {
            _action  = action;
            _opinion = action.StartsWith('\'')
                ? $"{start}{action}"
                : $"{start} {action}";

            return this;
        }

        private Verse Do(Action action)
        {
            action();
            return this;
        }
    }
}
