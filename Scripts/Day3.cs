using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using Microsoft.VisualBasic;

namespace AdventOfCode2023
{

    public class Day3 : Day
    {
        private readonly (int, int)[] AdjacentDirection = [
            (-1,-1),    (-1,0),     (-1, 1),
            (0,-1),                 (0, 1),
            (1,-1),     (1,0),      (1, 1),
            ];
        struct Numbers
        {
            public Numbers((int, int) p, int v)
            {
                position = p;
                value = v;
            }
            public int value;
            public (int, int) position;

            public readonly bool EqualsPosistion((int, int) pos) => position.Item1 == pos.Item1 && position.Item2 == pos.Item2;
        }

        public long Part1(string[] lines)
        {
            List<(int, int)> symbolsPos = [];
            List<Numbers> numbers = [];
            int sumNumbers = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j].Equals('.'))
                        continue;

                    if (int.TryParse(lines[i][j].ToString(), out int number))
                        numbers.Add(new Numbers((i, j), number));
                    else
                        symbolsPos.Add((i, j));
                }
            }

            foreach (var symbol in symbolsPos)
            {
                List<Numbers> adjacentNumbers = [];
                foreach (var adjacents in AdjacentDirection)
                {
                    var adjPos = (symbol.Item1 + adjacents.Item1, symbol.Item2 + adjacents.Item2);
                    if (numbers.Any(x => x.EqualsPosistion(adjPos)))
                    {
                        var number = numbers.First(x => x.position == adjPos);
                        numbers.Remove(number);
                        adjacentNumbers.Add(number);
                        var n = number;
                        while (numbers.Any(x => number.position.Item1 == x.position.Item1 && number.position.Item2 - x.position.Item2 == 1))
                        {
                            number = numbers.First(x => number.position.Item1 == x.position.Item1 && number.position.Item2 - x.position.Item2 == 1);
                            numbers.Remove(number);
                            adjacentNumbers.Add(number);
                        }

                        number = n;
                        while (numbers.Any(x => number.position.Item1 == x.position.Item1 && number.position.Item2 - x.position.Item2 == -1))
                        {
                            number = numbers.First(x => number.position.Item1 == x.position.Item1 && number.position.Item2 - x.position.Item2 == -1);
                            numbers.Remove(number);
                            adjacentNumbers.Add(number);
                        }
                    }
                }

                var groupedNumbersPerLine = adjacentNumbers.OrderBy(x => x.position.Item2).GroupBy(x => x.position.Item1);


                foreach (var numbersInLine in groupedNumbersPerLine)
                {
                    string fullNumber = "";
                    int lastColumn = numbersInLine.ElementAt(0).position.Item2;
                    foreach (var number in numbersInLine)
                    {
                        if (lastColumn + 1 < number.position.Item2)
                        {
                            sumNumbers += int.Parse(fullNumber);
                            fullNumber = "";
                        }

                        fullNumber += number.value;
                        lastColumn = number.position.Item2;
                    }
                    sumNumbers += int.Parse(fullNumber);
                }
            }

            return sumNumbers;
        }

        public long Part2(string[] lines)
        {
            List<(int, int)> symbolsPos = [];
            List<Numbers> numbers = [];
            long sumGears = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j].Equals('.'))
                        continue;

                    if (int.TryParse(lines[i][j].ToString(), out int number))
                        numbers.Add(new Numbers((i, j), number));
                    else if (lines[i][j].Equals('*'))
                        symbolsPos.Add((i, j));
                }
            }

            foreach (var symbol in symbolsPos)
            {
                List<Numbers> adjacentNumbers = [];
                foreach (var adjacents in AdjacentDirection)
                {
                    var adjPos = (symbol.Item1 + adjacents.Item1, symbol.Item2 + adjacents.Item2);
                    if (numbers.Any(x => x.EqualsPosistion(adjPos)))
                    {
                        var number = numbers.First(x => x.position == adjPos);
                        numbers.Remove(number);
                        adjacentNumbers.Add(number);
                        var n = number;
                        while (numbers.Any(x => number.position.Item1 == x.position.Item1 && number.position.Item2 - x.position.Item2 == 1))
                        {
                            number = numbers.First(x => number.position.Item1 == x.position.Item1 && number.position.Item2 - x.position.Item2 == 1);
                            numbers.Remove(number);
                            adjacentNumbers.Add(number);
                        }

                        number = n;
                        while (numbers.Any(x => number.position.Item1 == x.position.Item1 && number.position.Item2 - x.position.Item2 == -1))
                        {
                            number = numbers.First(x => number.position.Item1 == x.position.Item1 && number.position.Item2 - x.position.Item2 == -1);
                            numbers.Remove(number);
                            adjacentNumbers.Add(number);
                        }
                    }
                }

                var groupedNumbersPerLine = adjacentNumbers.OrderBy(x => x.position.Item2).GroupBy(x => x.position.Item1);

                long sumGear = 1;
                int howManyNumbers = 0;
                foreach (var numbersInLine in groupedNumbersPerLine)
                {

                    string fullNumber = "";
                    int lastColumn = numbersInLine.ElementAt(0).position.Item2;

                    foreach (var number in numbersInLine)
                    {
                        if (lastColumn + 1 < number.position.Item2)
                        {
                            sumGear *= int.Parse(fullNumber);
                            fullNumber = "";
                            howManyNumbers++;
                        }

                        fullNumber += number.value;
                        lastColumn = number.position.Item2;
                    }
                    sumGear *= int.Parse(fullNumber);
                    howManyNumbers++;

                    if (howManyNumbers == 2)
                        sumGears += sumGear;
                }
            }

            return sumGears;
        }
    }
}