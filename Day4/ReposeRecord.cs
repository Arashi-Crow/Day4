using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    class ReposeRecord
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("ReposeReport.txt");
            string[] report = sr.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            PartA(report);
            PartB(report);
            Console.ReadKey();
        }
        private static void PartA(string[] report)
        {
            int guard = 0, start = 0;
            Dictionary<int, int> sleepTime = new Dictionary<int, int>();
            Dictionary<int, Dictionary<int, int>> sleepMinutes = new Dictionary<int, Dictionary<int, int>>();
            Array.Sort(report);
            foreach (string record in report)
            {
                string[] parts = record.Split(' ');
                switch (parts[2])
                {
                    case "Guard":
                        guard = int.Parse(parts[3].Substring(1));
                        if (!sleepTime.ContainsKey(guard))
                        {
                            sleepTime[guard] = 0;
                        }

                        if (!sleepMinutes.ContainsKey(guard))
                        {
                            sleepMinutes[guard] = new Dictionary<int, int>();
                        }

                        break;
                    case "falls":
                        start = int.Parse(parts[1].Substring(3, 2));
                        break;
                    case "wakes":
                        int end = int.Parse(parts[1].Substring(3, 2));
                        sleepTime[guard] += end - start;
                        for (var i = start; i <= end; i++)
                        {
                            sleepMinutes[guard].TryGetValue(i, out int m);
                            sleepMinutes[guard][i] = m + 1;
                        }

                        break;
                }
            }

            int max = 0;
            foreach (KeyValuePair<int, int> time in sleepTime)
            {
                if (time.Value > max)
                {
                    max = time.Value;
                    guard = time.Key;
                }
            }

            max = 0;
            int minute = 0;
            foreach (KeyValuePair<int, int> minutes in sleepMinutes[guard])
            {
                if (minutes.Value > max)
                {
                    max = minutes.Value;
                    minute = minutes.Key;
                }
            }
            Console.WriteLine("PartA Result");
            Console.WriteLine($" GuardID: {guard} MinutesAsleep {minute} Result = {guard * minute}");
        }

        private static void PartB(string[] report)
        {
            Array.Sort(report);
            int guard = 0, start = 0;
            Dictionary<int, Dictionary<int, int>> sleepMinutes = new Dictionary<int, Dictionary<int, int>>();

            foreach (string record in report)
            {
                string[] parts = record.Split(' ');
                switch (parts[2])
                {
                    case "Guard":
                        guard = int.Parse(parts[3].Substring(1));
                        if (!sleepMinutes.ContainsKey(guard))
                        {
                            sleepMinutes[guard] = new Dictionary<int, int>();
                        }

                        break;
                    case "falls":
                        start = int.Parse(parts[1].Substring(3, 2));
                        break;
                    case "wakes":
                        var end = int.Parse(parts[1].Substring(3, 2));
                        for (var i = start; i <= end; i++)
                        {
                            sleepMinutes[guard].TryGetValue(i, out int m);
                            sleepMinutes[guard][i] = m + 1;
                        }

                        break;
                }
            }
            int max = 0;
            int minute = 0;

            foreach (var guardId in sleepMinutes)
            {
                foreach (var guardMinutes in guardId.Value)
                {
                    if (guardMinutes.Value > max)
                    {
                        max = guardMinutes.Value;
                        guard = guardId.Key;
                        minute = guardMinutes.Key;
                    }
                }
            }
            Console.WriteLine("PartB Result");
            Console.WriteLine($" GuardID: {guard} MinutesAsleep {minute} Result = {guard * minute}");

        }
    }
}
