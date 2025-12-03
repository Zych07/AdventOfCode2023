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
            List<int> loopsWithOffset = new();

            string steps = lines[0];

            Dictionary<string, (string, string)> nodeInfo = new();
            List<string> currentNodes = new();

            for (int i = 2; i < lines.Length; i++)
            {
                var s = lines[i].Split(" = ");
                var lr = s[1].Replace("(", "").Replace(")", "").Split(", ");
                nodeInfo.Add(s[0], (lr[0], lr[1]));

                if (s[0].Last() == STARTING_NODE_LAST_CHAR)
                    if (currentNodes.Count == 0)
                        currentNodes.Add(s[0]);
            }

            int currentStep = 0;

            for (int i = 0; i < currentNodes.Count; i++)
            {
                currentStep = 0;
                Dictionary<string, List<int>> cyclesHelper = new();

                while (true)
                {

                    char direction = steps[currentStep % steps.Length];
                    currentStep++;

                    if (direction == 'L')
                        currentNodes[i] = nodeInfo[currentNodes[i]].Item1;
                    else
                        currentNodes[i] = nodeInfo[currentNodes[i]].Item2;

                    if (!cyclesHelper.ContainsKey(currentNodes[i]))
                    {
                        var l = new List<int>();
                        l.Add(currentStep);
                        cyclesHelper.Add(currentNodes[i], l);
                    }
                    else
                    {
                        if (cyclesHelper[currentNodes[i]].Contains(currentStep % steps.Length))
                        {
                            loopsWithOffset.Add(currentStep - currentStep % steps.Length);
                            break;
                        }
                        cyclesHelper[currentNodes[i]].Add(currentStep);
                    }
                }
            }
            return LCM(loopsWithOffset);
        }

        private long GCF(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        private long LCM(long a, long b) => (a / GCF(a, b)) * b;
        private long LCM(List<int> numbs)
        {
            long sum = numbs[0];

            for(int i=1; i<numbs.Count; i++)
                sum = LCM(sum, numbs[i]);

            return sum;
        }
    }
}
