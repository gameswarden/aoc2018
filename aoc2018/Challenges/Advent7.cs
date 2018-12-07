using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc2018.Util;

namespace aoc2018.Challenges
{
    class Advent7 : Challenge
    {
        public override string INPUT { get { return "C:/Dev/aoc2018/aoc2018/Input/7.txt"; } }

        public override void A()
        {
            CaseName = "7A";
            Start();
            var input = Input.GetInputFromFile(INPUT);

            Steps = new SortedDictionary<string, Step>();
            CompletedSteps = new List<string>();

            foreach(var i in input)
            {
                var s = i.Replace("Step", string.Empty);
                var steps = Regex.Matches(s, "[A-Z]+");
                var step = steps[1].Value;
                var req = steps[0].Value;

                if (!Steps.ContainsKey(step))
                {
                    Steps[step] = new Step(step);
                }

                if (!Steps.ContainsKey(req))
                {
                    Steps[req] = new Step(req);
                }

                Steps[step].Requirements.Add(req);

            }

            var result = string.Empty;

            while(Steps.Any())
            {
                var step = Steps.First(s => Ready(s.Key));
                CompletedSteps.Add(step.Key);
                Steps.Remove(step.Key);
                result += step.Key;
            }

            Console.WriteLine(result);
            End();
        }

        public override void B()
        {
            CaseName = "7B";
            Start();

            var input = Input.GetInputFromFile(INPUT);

            Steps = new SortedDictionary<string, Step>();
            CompletedSteps = new List<string>();

            foreach (var i in input)
            {
                var s = i.Replace("Step", string.Empty);
                var steps = Regex.Matches(s, "[A-Z]+");
                var step = steps[1].Value;
                var req = steps[0].Value;

                if (!Steps.ContainsKey(step))
                {
                    Steps[step] = new Step(step);
                }

                if (!Steps.ContainsKey(req))
                {
                    Steps[req] = new Step(req);
                }

                Steps[step].Requirements.Add(req);

            }

            var workers = new List<Worker>();

            for(int i = 0; i < 5; i++)
            {
                workers.Add(new Worker());
            }

            ElapsedTime = 0;
            while(Steps.Any() || workers.Any(w => w.Step != null))
            {
                foreach(var worker in workers.Where(w => w.RemainingDuration <= 0))
                {
                    if (worker.Step != null)
                        CompleteStep(worker);

                    GetNextStep(worker);
                }

                foreach (var w in workers)
                {
                    if (w.Step != null)
                    {
                        w.RemainingDuration--;
                        if (w.RemainingDuration <= 0)
                        {
                            CompleteStep(w);
                            GetNextStep(w);
                        }
                    }

                }
                
                ElapsedTime++;
            }

            Console.WriteLine(ElapsedTime);

            End();

        }

        public SortedDictionary<string, Step> Steps { get; set; }

        public List<string> CompletedSteps { get; set; }

        public void CompleteStep(Worker worker)
        {
            CompletedSteps.Add(worker.Step);
        }

        public void GetNextStep(Worker worker)
        {
            worker.Step = Steps.FirstOrDefault(s => Ready(s.Key)).Key;
            if (worker.Step != null)
            {
                worker.RemainingDuration = worker.Step[0] - 4;
                Steps.Remove(worker.Step);
            }
        }

        public bool Ready(string name)
        {
            var step = Steps[name];

            return step.Requirements.All(r => CompletedSteps.Contains(r));
        }

        public int ElapsedTime { get; set; }
    }

    public class Step
    {
        public string Name { get; set; }
        public bool Complete { get; set; }
        public HashSet<string> Requirements { get; set; }

        public Step()
        { }

        public Step(string name)
        {
            Name = name;
            Requirements = new HashSet<string>();
        }
    }

    public class Worker
    {
        public string Step { get; set; }
        public int RemainingDuration { get; set; }

        public void GetNextStep()
        {
            
        }
    }
}
