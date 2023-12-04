using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using Microsoft.VisualBasic;

namespace AdventOfCode2023
{

    public class Day4 : Day
    {
        public long Part1(string[] lines)
        {
            long sumOfPowers = 0;
            foreach (var line in lines)
            {
                var numbers = line.Split(": ")[1].Replace("  ", " ").Split(" | ");
                var leftNumbers = numbers[0].Split(" ");
                var rightNumbers = numbers[1].Split(" ");

                var intersectCount = leftNumbers.Intersect(rightNumbers).Count();

                if (intersectCount > 0)
                    sumOfPowers += (long)MathF.Pow(2, intersectCount - 1);
            }

            return sumOfPowers;
        }

        public long Part2(string[] lines)
        {
            long sumOfTotalCards = 0;
            List<int> nextCardsMultiply = new();

            foreach (var line in lines)
            {
                var numbers = line.Split(": ")[1].Replace("  ", " ").Split(" | ");
                var leftNumbers = numbers[0].Split(" ");
                var rightNumbers = numbers[1].Split(" ");

                int howManyCurrentCards = 1;
                if (nextCardsMultiply.Count > 0)
                {
                    howManyCurrentCards += nextCardsMultiply[0];
                    nextCardsMultiply.RemoveAt(0);
                }

                var intersectCount = leftNumbers.Intersect(rightNumbers).Count();

                for (int i = 0; i < intersectCount; i++)
                {
                    if (nextCardsMultiply.Count <= i)
                        nextCardsMultiply.Add(howManyCurrentCards);
                    else
                        nextCardsMultiply[i] += howManyCurrentCards;
                }

                sumOfTotalCards += howManyCurrentCards;
            }

            return sumOfTotalCards;
        }
    }
}