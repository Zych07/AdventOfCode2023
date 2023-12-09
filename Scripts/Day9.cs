using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Xml;
using Microsoft.VisualBasic;


namespace AdventOfCode2023
{

    public class Day9 : Day
    {
        public long Part1(string[] lines)
        {

            long sumID = 0;
            foreach (var line in lines)
            {
                List<long> numbers = new();
                List<long> lastNumbers = new();

                var num = line.Split(' ');

                foreach (var n in num)
                    numbers.Add(long.Parse(n));


                while (!numbers.All(x => x == 0))
                {
                    for (int i = 0; i < numbers.Count - 1; i++)
                        numbers[i] = numbers[i + 1] - numbers[i];

                    lastNumbers.Add(numbers[numbers.Count - 1]);
                    numbers.RemoveAt(numbers.Count - 1);
                }

                sumID += lastNumbers.Sum();
            }
            return sumID;
        }

        public long Part2(string[] lines)
        {
            long sumID = 0;
            foreach (var line in lines)
            {
                List<long> numbers = new();
                List<long> firstNumbers = new();
                List<long> leftNumbers = new();

                var num = line.Split(' ');

                foreach (var n in num)
                    numbers.Add(long.Parse(n));


                while (!numbers.All(x => x == 0))
                {
                    firstNumbers.Add(numbers[0]);

                    for (int i = 0; i < numbers.Count - 1; i++)
                        numbers[i] = numbers[i + 1] - numbers[i];

                    numbers.RemoveAt(numbers.Count - 1);
                }

                leftNumbers.Add(0);

                for (int i = firstNumbers.Count - 1; i >= 0; i--)
                    leftNumbers.Add(firstNumbers[i] - leftNumbers[firstNumbers.Count - 1 - i]);

                sumID += leftNumbers.Last();
            }
            return sumID;
        }
    }
}