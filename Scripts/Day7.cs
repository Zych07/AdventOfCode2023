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

    public class Day7 : Day
    {
        public long Part1(string[] lines)
        {
            Dictionary<char, int> VALUES = new() { { 'A', 13 }, { 'K', 12 }, { 'Q', 11 }, { 'J', 10 }, { 'T', 9 }, { '9', 8 }, { '8', 7 }, { '7', 6 }, { '6', 5 }, { '5', 4 }, { '4', 3 }, { '3', 2 }, { '2', 1 }, { '1', 0 } };

            List<List<(string, int)>> groupedValues = new();

            for (int i = 0; i < 7; i++)
                groupedValues.Add(new List<(string, int)>());

            foreach (var line in lines)
            {
                var s = line.Split(' ');
                var cards = s[0];
                var groupedCards = cards.GroupBy(x => x);

                if (groupedCards.Any(x => x.Count() == 5))
                    groupedValues[6].Add((s[0], int.Parse(s[1])));
                else if (groupedCards.Any(x => x.Count() == 4))
                    groupedValues[5].Add((s[0], int.Parse(s[1])));
                else if (groupedCards.Any(x => x.Count() == 3) && groupedCards.Any(x => x.Count() == 2))
                    groupedValues[4].Add((s[0], int.Parse(s[1])));
                else if (groupedCards.Any(x => x.Count() == 3))
                    groupedValues[3].Add((s[0], int.Parse(s[1])));
                else if (groupedCards.Where(x => x.Count() == 2).Count() == 2)
                    groupedValues[2].Add((s[0], int.Parse(s[1])));
                else if (groupedCards.Any(x => x.Count() == 2))
                    groupedValues[1].Add((s[0], int.Parse(s[1])));
                else
                    groupedValues[0].Add((s[0], int.Parse(s[1])));
            }

            int rank = 1;
            long sumBids = 0;

            for (int i = 0; i < 7; i++)
            {
                var sorted = groupedValues[i].OrderBy(x => VALUES[x.Item1[0]]).ThenBy(x => VALUES[x.Item1[1]]).ThenBy(x => VALUES[x.Item1[2]]).ThenBy(x => VALUES[x.Item1[3]]).ThenBy(x => VALUES[x.Item1[4]]);
                foreach (var card in sorted)
                {
                    sumBids += rank * card.Item2;
                    rank++;
                }
            }

            return sumBids;
        }

        public long Part2(string[] lines)
        {
            Dictionary<char, int> VALUES = new() { { 'A', 13 }, { 'K', 12 }, { 'Q', 11 }, { 'J', 0 }, { 'T', 9 }, { '9', 8 }, { '8', 7 }, { '7', 6 }, { '6', 5 }, { '5', 4 }, { '4', 3 }, { '3', 2 }, { '2', 1 }, { '1', 0 } };

            List<List<(string, int)>> groupedValues = new();

            for (int i = 0; i < 7; i++)
                groupedValues.Add(new List<(string, int)>());

            foreach (var line in lines)
            {
                var s = line.Split(' ');
                var cards = s[0];
                var groupedCards = cards.GroupBy(x => x);


                if (groupedCards.Any(x => x.Count() == 5) || (groupedCards.Any(x => x.Key == 'J') && groupedCards.First(y => y.Key == 'J').Count() < 5 && groupedCards.Any(x => groupedCards.First(y => y.Key == 'J').Count() + x.Count(x => x != 'J') == 5)))
                    groupedValues[6].Add((s[0], int.Parse(s[1])));
                else if (groupedCards.Any(x => x.Count() == 4) || (groupedCards.Any(x => x.Key == 'J') && groupedCards.First(y => y.Key == 'J').Count() < 4 && groupedCards.Any(x => groupedCards.First(y => y.Key == 'J').Count() + x.Count(x => x != 'J') == 4)))
                    groupedValues[5].Add((s[0], int.Parse(s[1])));
                else if ((groupedCards.Any(x => x.Count() == 3) && groupedCards.Any(x => x.Count() == 2)) || (groupedCards.Any(x => x.Key == 'J') && groupedCards.Where(x => x.Count() == 2).Count() == 2 && groupedCards.First(y => y.Key == 'J').Count() < 3 && groupedCards.Any(x => groupedCards.First(y => y.Key == 'J').Count() + x.Count(x => x != 'J') == 3)))
                    groupedValues[4].Add((s[0], int.Parse(s[1])));
                else if (groupedCards.Any(x => x.Count() == 3) || (groupedCards.Any(x => x.Key == 'J') && groupedCards.First(y => y.Key == 'J').Count() < 3 && groupedCards.Any(x => groupedCards.First(y => y.Key == 'J').Count() + x.Count(x => x != 'J') == 3)))
                    groupedValues[3].Add((s[0], int.Parse(s[1])));
                else if (groupedCards.Where(x => x.Count() == 2).Count() == 2 || (groupedCards.Any(x => x.Key == 'J') && groupedCards.First(y => y.Key == 'J').Count() < 2 && groupedCards.Any(x => x.Key != 'J' && x.Count() == 2) && groupedCards.Any(x => groupedCards.First(y => y.Key == 'J').Count() + x.Count(x => x != 'J') == 2)))
                    groupedValues[2].Add((s[0], int.Parse(s[1])));
                else if (groupedCards.Any(x => x.Count() == 2) || (groupedCards.Any(x => x.Key == 'J') && groupedCards.First(y => y.Key == 'J').Count() < 2 && groupedCards.Any(x => groupedCards.First(y => y.Key == 'J').Count() + x.Count(x => x != 'J') == 2)))
                    groupedValues[1].Add((s[0], int.Parse(s[1])));
                else
                    groupedValues[0].Add((s[0], int.Parse(s[1])));
            }

            int rank = 1;
            long sumBids = 0;

            for (int i = 0; i < 7; i++)
            {
                var sorted = groupedValues[i].OrderBy(x => VALUES[x.Item1[0]]).ThenBy(x => VALUES[x.Item1[1]]).ThenBy(x => VALUES[x.Item1[2]]).ThenBy(x => VALUES[x.Item1[3]]).ThenBy(x => VALUES[x.Item1[4]]);
                foreach (var card in sorted)
                {
                    sumBids += rank * card.Item2;
                    rank++;
                }
            }

            return sumBids;
        }
    }
}
