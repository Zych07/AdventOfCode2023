using System;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace AdventOfCode2023
{

    public class Day1 : Day
    {

        public long Part1(string[] lines)
        {
            int sum = 0;

            foreach (var line in lines)
            {
                int firstNumber = -1;
                int lastNumber = -1;

                foreach (var c in line)
                {
                    if (int.TryParse(c.ToString(), out int number))
                    {
                        if (firstNumber == -1)
                            firstNumber = number;

                        lastNumber = number;
                    }
                }
                sum += firstNumber * 10 + lastNumber;
            }
            return sum;
        }

        public long Part2(string[] lines)
        {
            string[] numbers = ["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
            List<string> newInput = [];

            foreach (var line in lines)
            {
                string newLine = "";
                for (int i = 0; i < line.Length; i++)
                    for (int j = 0; j < numbers.Length; j++)
                        if (line.IndexOf(numbers[j], i) == i)
                            newLine += j % 10;

                newInput.Add(newLine);
            }

            return Part1([.. newInput]);
        }
    }
}