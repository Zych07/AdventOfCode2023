using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.VisualBasic;
using System.Threading;
namespace AdventOfCode2023
{

    public class Day5 : Day
    {
        public class Maps
        {
            public List<Range> Ranges = new();
        }
        public class Range
        {
            public long Destination;
            public long Source;
            public long Lenght;
        }
        public class Seed
        {
            public long Start;
            public long Lenght;
        }
        public long Part1(string[] lines)
        {
            List<long> seeds = new();
            List<Maps> maps = new();

            Maps? currentMap = null;

            for (int i = 0; i < lines.Length; i++)
            {
                if (i == 0)
                {
                    var numbers = lines[i].Split(": ")[1].Split(" ");
                    foreach (var number in numbers)
                        seeds.Add(long.Parse(number));
                }
                else
                {
                    if (lines[i].Length <= 1)
                    {
                        i++;
                        if (currentMap != null)
                            maps.Add(currentMap);
                        currentMap = new();
                        continue;
                    }
                    else
                    {
                        var numbers = lines[i].Split(" ");
                        Range range = new();
                        range.Destination = long.Parse(numbers[0]);
                        range.Source = long.Parse(numbers[1]);
                        range.Lenght = long.Parse(numbers[2]);

                        currentMap?.Ranges.Add(range);
                    }
                }
            }
            maps.Add(currentMap);

            long lowestLocation = long.MaxValue;
            foreach (var seed in seeds)
            {
                long currentValue = seed;
                for (int i = 0; i < maps.Count; i++)
                {
                    var range = maps[i].Ranges.FirstOrDefault(x => x.Source <= currentValue && x.Source + x.Lenght > currentValue);
                    if (range != null)
                    {
                        long offset = currentValue - range.Source;
                        currentValue = range.Destination + offset;
                    }
                }
                if (currentValue < lowestLocation)
                    lowestLocation = currentValue;
            }

            return lowestLocation;
        }

        List<Maps> _maps = new();
        public long Part2(string[] lines)
        {
            List<Seed> seeds = new();

            Maps? currentMap = null;

            for (int i = 0; i < lines.Length; i++)
            {
                if (i == 0)
                {
                    var numbers = lines[i].Split(": ")[1].Split(" ");
                    int c = 0;
                    Seed currentSeed = new Seed();
                    foreach (var number in numbers)
                    {
                        if (c == 0)
                        {
                            currentSeed.Start = long.Parse(number);
                            c++;
                        }
                        else
                        {
                            currentSeed.Lenght = long.Parse(number);
                            c = 0;
                            seeds.Add(currentSeed);
                            currentSeed = new Seed();

                        }
                    }
                }
                else
                {
                    if (lines[i].Length <= 1)
                    {
                        i++;
                        if (currentMap != null)
                            _maps.Add(currentMap);
                        currentMap = new();
                        continue;
                    }
                    else
                    {
                        var numbers = lines[i].Split(" ");
                        Range range = new();
                        range.Destination = long.Parse(numbers[0]);
                        range.Source = long.Parse(numbers[1]);
                        range.Lenght = long.Parse(numbers[2]);

                        currentMap?.Ranges.Add(range);
                    }
                }
            }
            _maps.Add(currentMap);

            List<Thread> threads = new();
            foreach (var seed in seeds)
            {
                Thread t = new Thread(() => CalculateSeed(seed))
                {
                    Name = seed.Start.ToString()
                };
                threads.Add(t);
            }

            foreach (var t in threads)
                t.Start();

            Console.WriteLine("Main Thread Ended");
            Console.Read();
            return -1;
        }

        private long CalculateSeed(Seed seed)
        {
            Console.WriteLine("Method1 Started using " + Thread.CurrentThread.Name);
            int progress = 0;
            long lowestLocation = long.MaxValue;
            for (long s = seed.Start; s < seed.Start + seed.Lenght; s++)
            {
                long currentValue = s;

                for (int i = 0; i < _maps.Count; i++)
                {
                    var range = _maps[i].Ranges.FirstOrDefault(x => x.Source <= currentValue && x.Source + x.Lenght > currentValue);
                    if (range != null)
                    {
                        long offset = currentValue - range.Source;
                        currentValue = range.Destination + offset;
                    }
                }
                if (currentValue < lowestLocation)
                    lowestLocation = currentValue;

                if (s % (long)(seed.Lenght / 100) == 0)
                {
                    progress++;
                    Console.WriteLine(progress);
                }
            }
            Console.WriteLine("L: " + lowestLocation);
            return lowestLocation;
        }

    }
}
