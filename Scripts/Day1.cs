using System;
using System.Diagnostics;
using System.Linq;

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
            string[] numbers = ["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

            int sum = 0;

            foreach (var line in lines)
            {
                int firstNumber = -1;
                int lastNumber = -1;

                for (int i = 0; i < line.Length; i++)
                {
                    if (int.TryParse(line[i].ToString(), out int number))
                    {
                        if (firstNumber == -1)
                            firstNumber = number;

                        lastNumber = number;
                    }
                    else
                    {

                        for (int j = 0; j < numbers.Length; j++)
                        {
                            if ( i + numbers[j].Length <= line.Length && line.Substring(i, numbers[j].Length) == numbers[j])
                            {
                                if (firstNumber == -1)
                                    firstNumber = j;

                                lastNumber = j;
                                break;
                            }
                        }
                    }

                }
                sum += firstNumber * 10 + lastNumber;
            }
            
            return sum;
        }
    }
}