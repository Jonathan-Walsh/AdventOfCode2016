using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016Part2
{
    class Day13
    {
        static int favoriteNumber = 1362;
        static int endRow = 31;
        static int endCol = 39;

        public static void PartOne()
        {
            bool[,] visited = new bool[100, 100];
            Queue<int> rowQueue = new Queue<int>();
            Queue<int> colQueue = new Queue<int>();
            rowQueue.Enqueue(1);
            colQueue.Enqueue(1);
            int numSteps = -1;
            bool endReached = false;

            while (!endReached)
            {
                numSteps++;
                int numInLayer = rowQueue.Count;
                for (int i = 0; i < numInLayer; i++)
                {
                    int row = rowQueue.Dequeue();
                    int col = colQueue.Dequeue();
                    if (row == endRow && col == endCol)
                    {
                        endReached = true;
                        break;
                    }
                    if (row > 0)
                    {
                        if (!visited[row - 1, col] && IsOpenSpace(row - 1, col))
                        {
                            rowQueue.Enqueue(row - 1);
                            colQueue.Enqueue(col);
                            visited[row - 1, col] = true;
                        }
                    }
                    if (col > 0)
                    {
                        if (!visited[row, col - 1] && IsOpenSpace(row, col - 1))
                        {
                            rowQueue.Enqueue(row);
                            colQueue.Enqueue(col - 1);
                            visited[row, col - 1] = true;
                        }
                    }
                    if (row < 100)
                    {
                        if (!visited[row + 1, col] && IsOpenSpace(row + 1, col))
                        {
                            rowQueue.Enqueue(row + 1);
                            colQueue.Enqueue(col);
                            visited[row + 1, col] = true;
                        }
                    }
                    if (col < 100)
                    {
                        if (!visited[row, col + 1] && IsOpenSpace(row, col + 1))
                        {
                            rowQueue.Enqueue(row);
                            colQueue.Enqueue(col + 1);
                            visited[row, col + 1] = true;
                        }
                    }
                }
               
            }
            Console.WriteLine(numSteps); 
        }

        public static void PartTwo()
        {
            bool[,] visited = new bool[100, 100];
            Queue<int> rowQueue = new Queue<int>();
            Queue<int> colQueue = new Queue<int>();
            rowQueue.Enqueue(1);
            colQueue.Enqueue(1);
            int numSteps = 0;
            int numLocations = 0;
            while (numSteps < 50)
            {
                numSteps++;
                int numInLayer = rowQueue.Count;
                for (int i = 0; i < numInLayer; i++)
                {
                    int row = rowQueue.Dequeue();
                    int col = colQueue.Dequeue();
                    if (row > 0)
                    {
                        if (!visited[row - 1, col] && IsOpenSpace(row - 1, col))
                        {
                            rowQueue.Enqueue(row - 1);
                            colQueue.Enqueue(col);
                            visited[row - 1, col] = true;
                            numLocations++;
                        }
                    }
                    if (col > 0)
                    {
                        if (!visited[row, col - 1] && IsOpenSpace(row, col - 1))
                        {
                            rowQueue.Enqueue(row);
                            colQueue.Enqueue(col - 1);
                            visited[row, col - 1] = true;
                            numLocations++;
                        }
                    }
                    if (row < 100)
                    {
                        if (!visited[row + 1, col] && IsOpenSpace(row + 1, col))
                        {
                            rowQueue.Enqueue(row + 1);
                            colQueue.Enqueue(col);
                            visited[row + 1, col] = true;
                            numLocations++;
                        }
                    }
                    if (col < 100)
                    {
                        if (!visited[row, col + 1] && IsOpenSpace(row, col + 1))
                        {
                            rowQueue.Enqueue(row);
                            colQueue.Enqueue(col + 1);
                            visited[row, col + 1] = true;
                            numLocations++;
                        }
                    }
                }

            }
            Console.WriteLine(numLocations);
        }

        private static bool IsOpenSpace(int x, int y)
        {
            int num = (x * x) + (3 * x) + (2 * x * y) + y + (y * y);
            num += favoriteNumber;
            string binanyRepresentation = Convert.ToString(num, 2);
            if (HasEvenNumOfOneBits(binanyRepresentation))
                return true;
            else
                return false;
            }

        private static bool HasEvenNumOfOneBits(string binaryNum)
        {
            int numOneBits = 0;
            foreach (char bit in binaryNum)
            {
                if (bit != '0' && bit != '1')
                    throw new ArgumentException();
                else if (bit == '1')
                    numOneBits += 1;
            }

            if (numOneBits % 2 == 0)
                return true;
            else
                return false;
        }
    }
}
