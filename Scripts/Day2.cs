using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using Microsoft.VisualBasic;

namespace AdventOfCode2023
{

    public class Day2 : Day
    {
        private readonly Dictionary<string, int> AVAILABLE_CUBES = new() { { "blue", 14 }, { "red", 12 }, { "green", 13 } };
        private Dictionary<string, int> _coloredCubes = new() { { "blue", 0 }, { "red", 0 }, { "green", 0 } };

        public long Part1(string[] lines)
        {
            int sumGID = 0;

            foreach (var line in lines)
            {
                var gameID = line.Split(": ");
                int gID = int.Parse(gameID[0].Replace("Game ", ""));
                var reaches = gameID[1].Split("; ");

                bool isEnoughCubes = true;

                foreach (var reach in reaches)
                {
                    var cubes = reach.Split(", ");

                    foreach (var cube in cubes)
                    {
                        var info = cube.Split(" ");
                        int count = int.Parse(info[0]);
                        string color = info[1];
                        _coloredCubes[color] += count;
                    }

                    foreach (var item in _coloredCubes)
                    {
                        if (item.Value > AVAILABLE_CUBES[item.Key])
                            isEnoughCubes = false;

                        _coloredCubes[item.Key] = 0;
                    }

                    if (!isEnoughCubes)
                        break;
                }

                if (isEnoughCubes)
                    sumGID += gID;
            }
            return sumGID;
        }

        public long Part2(string[] lines)
        {
            int sumGID = 0;

            foreach (var line in lines)
            {
                var reaches = line.Split(": ")[1].Split("; ");
                foreach (var reach in reaches)
                {
                    var cubes = reach.Split(", ");

                    foreach (var cube in cubes)
                    {
                        var info = cube.Split(" ");
                        int count = int.Parse(info[0]);
                        string color = info[1];

                        _coloredCubes[color] = int.Max(_coloredCubes[color], count);
                    }
                }

                int power = 1;
                foreach (var item in _coloredCubes)
                {
                    power *= int.Clamp(item.Value, 1, int.MaxValue);
                    _coloredCubes[item.Key] = 0;
                }

                sumGID += power;
            }
            return sumGID;
        }
    }
}