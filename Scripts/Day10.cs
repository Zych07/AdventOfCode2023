using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Xml;
using Microsoft.VisualBasic;


namespace AdventOfCode2023
{

    public class Day10 : Day
    {
        public static int HEIGHT;
        public static int WIDTH;
        public long Part1(string[] lines)
        {
            HEIGHT = lines.Count();
            WIDTH = lines[0].Count();

            Graph g = new Graph(lines.Length * lines[0].Length);

            (int, int) starting = (0, 0);

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    switch (lines[i][j])
                    {
                        case '|':
                            if (i - 1 >= 0 && i + 1 < lines.Length)
                                if (IsDown(lines[i - 1][j]) && IsUp(lines[i + 1][j]))
                                {
                                    g.AddEdge((i - 1, j), (i, j));
                                    g.AddEdge((i, j), (i + 1, j));
                                }
                            break;
                        case '-':
                            if (j - 1 >= 0 && j + 1 < lines[i].Length)
                                if (IsRight(lines[i][j - 1]) && IsLeft(lines[i][j + 1]))
                                {
                                    g.AddEdge((i, j - 1), (i, j));
                                    g.AddEdge((i, j), (i, j + 1));
                                }
                            break;
                        case 'L':
                            if (i - 1 >= 0 && j + 1 < lines[i].Length)
                                if (IsDown(lines[i - 1][j]) && IsLeft(lines[i][j + 1]))
                                {
                                    g.AddEdge((i - 1, j), (i, j));
                                    g.AddEdge((i, j), (i, j + 1));
                                }
                            break;
                        case 'J':
                            if (j - 1 >= 0 && i - 1 >= 0)
                                if (IsDown(lines[i - 1][j]) && IsRight(lines[i][j - 1]))
                                {
                                    g.AddEdge((i - 1, j), (i, j));
                                    g.AddEdge((i, j), (i, j - 1));
                                }
                            break;
                        case '7':
                            if (j - 1 >= 0 && i + 1 < lines.Length)
                                if (IsUp(lines[i + 1][j]) && IsRight(lines[i][j - 1]))
                                {
                                    g.AddEdge((i, j - 1), (i, j));
                                    g.AddEdge((i, j), (i + 1, j));
                                }
                            break;
                        case 'F':
                            if (j + 1 < lines.Length && i + 1 < lines.Length)
                                if (IsUp(lines[i + 1][j]) && IsLeft(lines[i][j + 1]))
                                {
                                    g.AddEdge((i, j + 1), (i, j));
                                    g.AddEdge((i, j), (i + 1, j));
                                }
                            break;
                        case 'S':
                            starting = (i, j);
                            break;
                    }
                }

            }

            bool isUpConnected = starting.Item1 - 1 >= 0 && IsDown(lines[starting.Item1 - 1][starting.Item2]);
            bool isDownConnected = starting.Item1 + 1 < HEIGHT && IsUp(lines[starting.Item1 + 1][starting.Item2]);
            bool IsLeftConnected = starting.Item2 - 1 >= 0 && IsRight(lines[starting.Item1][starting.Item2 - 1]);
            bool IsRightConnected = starting.Item2 + 1 < WIDTH && IsLeft(lines[starting.Item1][starting.Item2 + 1]);

            if (isUpConnected)
                g.AddEdge(starting, (starting.Item1 - 1, starting.Item2));
            if (isDownConnected)
                g.AddEdge(starting, (starting.Item1 + 1, starting.Item2));
            if (IsLeftConnected)
                g.AddEdge(starting, (starting.Item1, starting.Item2 - 1));
            if (IsRightConnected)
                g.AddEdge(starting, (starting.Item1, starting.Item2 + 1));


            return g.DFS(starting);
        }

        public long Part2(string[] lines)
        {

            return -1;
        }

        private bool IsDown(char pipe) => pipe == '|' || pipe == 'F' || pipe == '7';
        private bool IsUp(char pipe) => pipe == '|' || pipe == 'J' || pipe == 'L';
        private bool IsLeft(char pipe) => pipe == '-' || pipe == 'J' || pipe == '7';
        private bool IsRight(char pipe) => pipe == '-' || pipe == 'F' || pipe == 'L';


        public class Graph
        {
            int V;
            List<(int, int)>[] adj;

            public Graph(int v)
            {
                adj = new List<(int, int)>[v];
                V = v;
            }

            public void AddEdge((int, int) u, (int, int) v)
            {
                if (adj[u.Item1 * HEIGHT + u.Item2] == null)
                    adj[u.Item1 * HEIGHT + u.Item2] = new List<(int, int)>();
                if (adj[v.Item1 * HEIGHT + v.Item2] == null)
                    adj[v.Item1 * HEIGHT + v.Item2] = new List<(int, int)>();
                adj[u.Item1 * HEIGHT + u.Item2].Add(v);
                adj[v.Item1 * HEIGHT + v.Item2].Add(u);
            }
            public void RemoveEdge((int, int) u, (int, int) v)
            {
                adj[u.Item1 * HEIGHT + u.Item2].Remove(v);
                adj[v.Item1 * HEIGHT + v.Item2].Remove(u);
            }

            private void DFSHelper((int, int) src, bool[] visited, int[] distances, int depth)
            {
                Queue<(int, int)> nodes = new();
                nodes.Enqueue(src);
                distances[src.Item1 * HEIGHT + src.Item2] = 0;

                while (nodes.Count != 0)
                {
                    src = nodes.Dequeue();

                    Console.Write(src + " (" + distances[src.Item1 * HEIGHT + src.Item2] + ") :");

                    visited[src.Item1 * HEIGHT + src.Item2] = true;

                    if (adj[src.Item1 * HEIGHT + src.Item2] != null)
                    {
                        foreach (var item in adj[src.Item1 * HEIGHT + src.Item2])
                        {
                            if (!visited[item.Item1 * HEIGHT + item.Item2])
                            {
                                Console.Write(item);
                                visited[item.Item1 * HEIGHT + item.Item2] = true;
                                distances[item.Item1 * HEIGHT + item.Item2] = distances[src.Item1 * HEIGHT + src.Item2] + 1;
                                nodes.Enqueue(item);
                            }
                        }
                    }
                    Console.WriteLine("");

                }
            }

            public int DFS((int, int) starting)
            {
                int[] distances = new int[V];
                bool[] visited = new bool[adj.Length + 1];
                DFSHelper(starting, visited, distances, 0);

                return distances.Max();
            }
        }
    }
}