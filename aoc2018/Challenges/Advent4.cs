using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent4 : Challenge
    {
        public override string INPUT { get { return "C:/Dev/aoc2018/aoc2018/Input/4.txt"; } }


        public override void Execute()
        {
            A();
            B();
        }

        private void A()
        {
            CaseName = "4A";
            Start();
            var input = Input.GetInputFromFile(INPUT);

            var events = ParseEvents(input);

            var guards = ParseEventsToGuards(events);

            var sleepyGuard = guards.OrderByDescending(g => g.Value.Sum(v => v.Value)).First();

            Console.WriteLine(sleepyGuard.Key * sleepyGuard.Value.OrderByDescending(v => v.Value).First().Key);

            End();
        }

        private void B()
        {
            CaseName = "4B";
            Start();
            var input = Input.GetInputFromFile(INPUT);

            var events = ParseEvents(input);

            var guards = ParseEventsToGuards(events);

            var maxSleepingMinuteGuard = guards.OrderByDescending(g => g.Value.Any() ? g.Value.Max(v => v.Value) : 0).First();
            var maxSleepingGuardMinute = maxSleepingMinuteGuard.Value.OrderByDescending(m => m.Value).First().Key;

            Console.WriteLine(maxSleepingMinuteGuard.Key * maxSleepingGuardMinute);

            End();

        }

        private SortedDictionary<int, SortedDictionary<int, int>> ParseEventsToGuards(SortedDictionary<DateTime, string> events)
        {
            var activeGuard = 0;
            var sleepStart = 0;
            var result = new SortedDictionary<int, SortedDictionary<int, int>>();

            foreach (var e in events)
            {
                if (e.Value.Contains("begins shift"))
                {
                    var guardID = int.Parse(Regex.Match(e.Value, "[\\d]+").Value);
                    activeGuard = guardID;
                    if (!result.ContainsKey(activeGuard))
                        result[activeGuard] = new SortedDictionary<int, int>();
                    sleepStart = 0;
                }
                else if (e.Value == "falls asleep")
                {
                    sleepStart = e.Key.Minute;
                }
                else if (e.Value == "wakes up")
                {
                    for (int i = sleepStart; i < e.Key.Minute; i++)
                    {
                        if (!result[activeGuard].ContainsKey(i))
                            result[activeGuard][i] = 0;
                        result[activeGuard][i]++;
                    }
                }
            }

            return result;
        }

        private SortedDictionary<DateTime, string> ParseEvents(List<string> input)
        {
            var result = new SortedDictionary<DateTime, string>();

            foreach (var i in input)
            {
                var match = Regex.Match(i, "(?:\\[)([\\d\\s-:]+)(?:[\\]\\s]+)([\\s\\w\\d#]+)");
                var timeStamp = DateTime.Parse(match.Groups[1].Value);
                var message = match.Groups[2].Value;

                result[timeStamp] = message;
            }

            return result;
        }
    }

    public class Event
    {
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }

        public static Event ParseEventString(string i)
        {
            var result = new Event();



            return result;
        }
    }
}

