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

    public class Day8 : Day
    {


        public long Part1(string[] lines)
        {
            string STARTING_NODE = "AAA";
            string ENDING_NODE = "ZZZ";

            string steps = lines[0];

            Dictionary<string, (string, string)> nodeInfo = new();

            for (int i = 2; i < lines.Length; i++)
            {
                var s = lines[i].Split(" = ");
                var lr = s[1].Replace("(", "").Replace(")", "").Split(", ");
                nodeInfo.Add(s[0], (lr[0], lr[1]));
            }

            string currentNode = STARTING_NODE;

            int currentStep = 0;

            while (!currentNode.Equals(ENDING_NODE))
            {
                char direction = steps[currentStep % steps.Length];
                if (direction == 'L')
                    currentNode = nodeInfo[currentNode].Item1;
                else
                    currentNode = nodeInfo[currentNode].Item2;

                currentStep++;
            }

            return currentStep;
        }

        public long Part2(string[] lines)
        {
            char STARTING_NODE_LAST_CHAR = 'A';
            char ENDING_NODE_LAST_CHAR = 'Z';

            string steps = lines[0];

            Dictionary<string, (string, string)> nodeInfo = new();

            List<string> currentNodes = new();

            for (int i = 2; i < lines.Length; i++)
            {
                var s = lines[i].Split(" = ");
                var lr = s[1].Replace("(", "").Replace(")", "").Split(", ");
                nodeInfo.Add(s[0], (lr[0], lr[1]));

                if (s[0].Last() == STARTING_NODE_LAST_CHAR)
                    currentNodes.Add(s[0]);
            }

            int currentStep = 0;

            while (!currentNodes.All(x => x.Last() == ENDING_NODE_LAST_CHAR))
            {
                char direction = steps[currentStep % steps.Length];
                for (int i = 0; i < currentNodes.Count; i++)
                {
                    if (direction == 'L')
                        currentNodes[i] = nodeInfo[currentNodes[i]].Item1;
                    else
                        currentNodes[i] = nodeInfo[currentNodes[i]].Item2;
                }

                currentStep++;
                if (currentStep % 1000 == 0)
                    Console.WriteLine(currentStep);
            }

            return currentStep;
        }
    }
}
