using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace AdventOfCode2016Part2
{
    class Day17
    {
        static string passcode = "hhhxzeay";

        public static void PartOne()
        {

            string shortestPath = FindShortestPath();
            Console.WriteLine(shortestPath);
        }

        public static void PartTwo()
        {
            int longestNumSteps = FindLongestNumSteps();
            Console.WriteLine(longestNumSteps);
        }

        private static string FindShortestPath()
        {
            int row = 0;
            int col = 0;
            string currentPath = "";
            Queue<int> rowQueue = new Queue<int>();
            Queue<int> colQueue = new Queue<int>();
            Queue<string> pathQueue = new Queue<string>();
            rowQueue.Enqueue(0);
            colQueue.Enqueue(0);
            pathQueue.Enqueue("");
            
            while (row != 3 || col != 3)
            {
                string hash = GetMD5Hash(passcode + currentPath);
                
                if (row > 0 && hash[0] >= 'b' && hash[0] <= 'f')
                {
                    rowQueue.Enqueue(row - 1);
                    colQueue.Enqueue(col);
                    pathQueue.Enqueue(currentPath + "U");
                }

                if (col > 0 && hash[2] >= 'b' && hash[2] <= 'f')
                {
                    rowQueue.Enqueue(row);
                    colQueue.Enqueue(col - 1);
                    pathQueue.Enqueue(currentPath + "L");
                }

                if (row < 3 && hash[1] >= 'b' && hash[1] <= 'f')
                {
                    rowQueue.Enqueue(row + 1);
                    colQueue.Enqueue(col);
                    pathQueue.Enqueue(currentPath + "D");
                }

                if (col < 3 && hash[3] >= 'b' && hash[3] <= 'f')
                {
                    rowQueue.Enqueue(row);
                    colQueue.Enqueue(col + 1);
                    pathQueue.Enqueue(currentPath + "R");
                }

                row = rowQueue.Dequeue();
                col = colQueue.Dequeue();
                currentPath = pathQueue.Dequeue();
            }
            return currentPath;
        }

        private static int FindLongestNumSteps()
        {
            int longestNumSteps = 0;
            int row = 0;
            int col = 0;
            string currentPath = "";
            Queue<int> rowQueue = new Queue<int>();
            Queue<int> colQueue = new Queue<int>();
            Queue<string> pathQueue = new Queue<string>();
            rowQueue.Enqueue(0);
            colQueue.Enqueue(0);
            pathQueue.Enqueue("");

            while (rowQueue.Count > 0)
            {
                int numInQueue = rowQueue.Count;
                for (int i = 0; i < rowQueue.Count; i++)
                {
                    if (row == 3 && col == 3)
                        longestNumSteps = currentPath.Length;
                    else
                    {
                        string hash = GetMD5Hash(passcode + currentPath);

                        if (row > 0 && hash[0] >= 'b' && hash[0] <= 'f')
                        {
                            rowQueue.Enqueue(row - 1);
                            colQueue.Enqueue(col);
                            pathQueue.Enqueue(currentPath + "U");
                        }

                        if (col > 0 && hash[2] >= 'b' && hash[2] <= 'f')
                        {
                            rowQueue.Enqueue(row);
                            colQueue.Enqueue(col - 1);
                            pathQueue.Enqueue(currentPath + "L");
                        }

                        if (row < 3 && hash[1] >= 'b' && hash[1] <= 'f')
                        {
                            rowQueue.Enqueue(row + 1);
                            colQueue.Enqueue(col);
                            pathQueue.Enqueue(currentPath + "D");
                        }

                        if (col < 3 && hash[3] >= 'b' && hash[3] <= 'f')
                        {
                            rowQueue.Enqueue(row);
                            colQueue.Enqueue(col + 1);
                            pathQueue.Enqueue(currentPath + "R");
                        }
                    }
                    row = rowQueue.Dequeue();
                    col = colQueue.Dequeue();
                    currentPath = pathQueue.Dequeue();
                }
            }
            return longestNumSteps;
        }

        private static string GetMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder hashBuilder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
                hashBuilder.Append(hashBytes[i].ToString("x2"));

            return hashBuilder.ToString();
        }

    }
}
