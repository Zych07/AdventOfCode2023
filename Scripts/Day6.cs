using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.VisualBasic;
using System.Threading;
using System.Text.RegularExpressions;

namespace AdventOfCode2023
{

    public class Day6 : Day
    {

        public long Part1(string[] lines)
        {
            List<int> times = new();
            List<int> distances = new();

            times = Regex.Replace(lines[0], @"\s+", " ").Split(": ")[1].Split(' ').Select(x => int.Parse(x)).ToList();
            distances = Regex.Replace(lines[1], @"\s+", " ").Split(": ")[1].Split(' ').Select(x => int.Parse(x)).ToList();

            long result = 1;

            for (int i = 0; i < times.Count; i++)
            {
                int howManyPossibles = 0;
                for (int t = 0; t < times[i]; t++)
                {
                    int currentSpeed = t;
                    int timeLeft = times[i] - currentSpeed;

                    if (timeLeft * currentSpeed > distances[i])
                        howManyPossibles++;
                }

                result *= howManyPossibles;
            }

            return result;
        }

        public long Part2(string[] lines)
        {
            List<long> times = new();
            List<long> distances = new();

            times = Regex.Replace(lines[0], @"\s+", "").Split(":")[1].Split(' ').Select(x => long.Parse(x)).ToList();
            distances = Regex.Replace(lines[1], @"\s+", "").Split(":")[1].Split(' ').Select(x => long.Parse(x)).ToList();

            long result = 1;

            for (int i = 0; i < times.Count; i++)
            {
                int howManyPossibles = 0;
                for (int t = 0; t < times[i]; t++)
                {
                    long currentSpeed = t;
                    long timeLeft = times[i] - currentSpeed;

                    if (timeLeft * currentSpeed > distances[i])
                        howManyPossibles++;
                }

                result *= howManyPossibles;
            }

            return result;
        }
    }
}
