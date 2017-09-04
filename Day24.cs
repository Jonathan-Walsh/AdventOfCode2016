using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2016Part2
{
    class Day24
    {

        static List<Pair> locationPairs;
        static Dictionary<string, int> locationPairsDict;
        public static void PartOne()
        {
            Maze maze = new Maze("input24.txt");
            locationPairs = GetListOfLocationPairs();
            foreach(Pair locationPair in locationPairs)
            {
                int shortestDistance = maze.FindShortestDistance(locationPair);
                locationPair.SetShortestDistance(shortestDistance);  
            }
            List<char[]> possibleRoutes = GetPossibleRoutes();
            locationPairsDict = MakeLocationPairsDict(locationPairs);
            char[] shortestRoute = FindShortestRoute(possibleRoutes);
            int numStepsOfShortestRoute = GetNumSteps(shortestRoute);
            Console.WriteLine(numStepsOfShortestRoute);
        }

        public static void PartTwo()
        {
            Maze maze = new Maze("input24.txt");
            locationPairs = GetListOfLocationPairs();
            foreach (Pair locationPair in locationPairs)
            {
                int shortestDistance = maze.FindShortestDistance(locationPair);
                locationPair.SetShortestDistance(shortestDistance);
            }
            List<char[]> possibleRoutes = GetPossibleRoutes2();
            locationPairsDict = MakeLocationPairsDict(locationPairs);
            char[] shortestRoute = FindShortestRoute(possibleRoutes);
            int numStepsOfShortestRoute = GetNumSteps(shortestRoute);
            Console.WriteLine(numStepsOfShortestRoute);
        }

        private static List<Pair> GetListOfLocationPairs()
        {
            List<Pair> locationPairs = new List<Pair>();
            char[] locations = new char[8] { '0', '1', '2', '3', '4', '5', '6', '7' };
            for (int i = 0; i < locations.Length - 1; i++)
            {
                for (int j = i + 1; j < locations.Length; j++)
                {
                    Pair nextPair = new Pair(locations[i], locations[j]);
                    locationPairs.Add(nextPair);
                }
            }

            return locationPairs;
        }

        private static List<char[]> GetPossibleRoutes()
        {
            List<char[]> possibleRoutes = new List<char[]>();
            string locations = "1234567";
            List<string> possibleLocationPermutations = GetPermutations(locations);
            foreach(string perm in possibleLocationPermutations)
            {
                char[] route = new char[8];
                route[0] = '0';
                for (int i = 0; i < perm.Length; i++)
                    route[i + 1] = perm[i];
                possibleRoutes.Add(route);
            }
            return possibleRoutes;
        }

        private static List<char[]> GetPossibleRoutes2()
        {
            List<char[]> possibleRoutes = new List<char[]>();
            string locations = "1234567";
            List<string> possibleLocationPermutations = GetPermutations(locations);
            foreach (string perm in possibleLocationPermutations)
            {
                char[] route = new char[9];
                route[0] = '0';
                for (int i = 0; i < perm.Length; i++)
                    route[i + 1] = perm[i];
                route[8] = '0';
                possibleRoutes.Add(route);
            }
            return possibleRoutes;
        }

        private static List<string> GetPermutations(string locations)
        {
            List<string> perms = new List<string>();

            if (locations.Length == 1)
            {
                perms.Add(locations);
            }
            else
            {
                for (int i = 0; i < locations.Length; i++)
                {
                    char a = locations[i];
                    List<string> smallerPerms = GetPermutations(locations.Substring(0, i) + locations.Substring(i + 1, locations.Length - i - 1));
                    foreach (string perm in smallerPerms)
                        perms.Add(a + perm);
                }
            }
            return perms;
        }

        private static Dictionary<string, int> MakeLocationPairsDict(List<Pair> locationPairs)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (Pair p in locationPairs)
            {
                char c1 = p.GetLocation1();
                char c2 = p.GetLocation2();
                string key = new string(new char[2] { c1, c2 });
                string key2 = new string(new char[2] { c2, c1 });
                dict.Add(key, p.GetShortestDistance());
                dict.Add(key2, p.GetShortestDistance());
            }
            return dict;
        }

        private static char[] FindShortestRoute(List<char[]> possibleRoutes)
        {
            char[] shortestRoute = possibleRoutes[0];
            foreach (char[] route in possibleRoutes)
                if (GetNumSteps(route) < GetNumSteps(shortestRoute))
                    shortestRoute = route;
            return shortestRoute;
        }

        private static int GetNumSteps(char[] route)
        {
            int numSteps = 0;
            for (int i = 0; i < route.Length - 1; i++)
            {
                string key = new string(new char[2] { route[i], route[i + 1] });
                numSteps += locationPairsDict[key];
            }
            return numSteps;
        }

        class Maze
        {
            char[,] maze;

            public Maze(string input)
            {
                string[] rows = File.ReadAllLines(input);
                maze = new char[rows.Length, rows[0].Length];

                for (int i = 0; i < rows.Length; i++)
                    for (int j = 0; j < rows[0].Length; j++)
                        maze[i, j] = rows[i][j];
            }

            public int FindShortestDistance(Pair locationPair)
            {
                char start = locationPair.GetLocation1();
                char end = locationPair.GetLocation2();
                Tuple<int, int> startLocation = FindLocation(start);
                Tuple<int, int> endLocation = FindLocation(end);
                
                return FindShortestDistance(startLocation, endLocation);
            }

            private Tuple<int, int> FindLocation(char c)
            {
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    for (int j = 0; j < maze.GetLength(1); j++)
                    {
                        if (maze[i, j] == c)
                            return new Tuple<int, int>(i, j);
                    }
                }

                throw new ArgumentException();
            }

            private int FindShortestDistance(Tuple<int, int> start, Tuple<int, int> end)
            {
                Queue<int> rowQueue = new Queue<int>();
                Queue<int> colQueue = new Queue<int>();
                int shortestDistance = -1;
                bool endReached = false;
                rowQueue.Enqueue(start.Item1);
                colQueue.Enqueue(start.Item2);
                bool[,] visited = new bool[maze.GetLength(0), maze.GetLength(1)];
                while(!endReached && rowQueue.Count > 0)
                {
                    shortestDistance++;
                    int numNodesInCurrentLevel = rowQueue.Count;
                    for (int i = 0; i < numNodesInCurrentLevel; i++)
                    {
                        int row = rowQueue.Dequeue();
                        int col = colQueue.Dequeue();

                        if (row == end.Item1 && col == end.Item2)
                            endReached = true;
                        else
                        {
                            if (row > 0 && maze[row - 1, col] != '#' && !visited[row - 1, col])
                            {
                                rowQueue.Enqueue(row - 1);
                                colQueue.Enqueue(col);
                                visited[row - 1, col] = true;
                            }
                            if (col > 0 && maze[row, col - 1] != '#' && !visited[row, col - 1])
                            {
                                rowQueue.Enqueue(row);
                                colQueue.Enqueue(col - 1);
                                visited[row, col - 1] = true;
                            }
                            if (row < maze.GetLength(0) - 1 && maze[row + 1, col] != '#' && !visited[row + 1, col])
                            {
                                rowQueue.Enqueue(row + 1);
                                colQueue.Enqueue(col);
                                visited[row + 1, col] = true;
                            }
                            if (col < maze.GetLength(1) - 1 && maze[row, col + 1] != '#' && !visited[row, col + 1])
                            {
                                rowQueue.Enqueue(row );
                                colQueue.Enqueue(col + 1);
                                visited[row, col + 1] = true;
                            }
                        }
                    }
                    
                }
                return shortestDistance;
            }

            public void PrintMaze()
            {
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    for (int j = 0; j < maze.GetLength(1); j++)
                    {
                        Console.Write(maze[i, j]);
                    }
                    Console.WriteLine();
                }   
            }
        }

        class Pair
        {
            char location1;
            char location2;
            int shortestDistance;

            public Pair(char location1, char location2)
            {
                this.location1 = location1;
                this.location2 = location2;
            }

            public void SetShortestDistance(int shortestDistance)
            {
                this.shortestDistance = shortestDistance;
            }

            public char GetLocation1()
            {
                return location1;
            }

            public char GetLocation2()
            {
                return location2;
            }

            public int GetShortestDistance()
            {
                return shortestDistance;
            }

            public void PrintPair()
            {
                Console.WriteLine(location1 + " " + location2);
            }

        }


    }
}
