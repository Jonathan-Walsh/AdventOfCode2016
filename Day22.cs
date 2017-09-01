using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2016Part2
{
    class Day22
    {

        public static void PartOne()
        {
            List<Node> nodeList = GetListOfNodes();
            int numViablePairs = GetNumViablePairs(nodeList);
            Console.WriteLine(numViablePairs);
        }

        private static List<Node> GetListOfNodes()
        {
            List<Node> nodeList = new List<Node>();
            string[] lines = File.ReadAllLines("input22.txt");
            for (int i = 2; i < lines.Length; i++)
            {
                string line = lines[i];
                Node nextNode = ExtractNodeData(line);
                nodeList.Add(nextNode);
            }

            return nodeList;
        }

        private static Node ExtractNodeData(string line)
        {
            string[] data = line.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int used = ExtractNum(data[2]);
            int available = ExtractNum(data[3]);
            return new Node(used, available);
        }

        private static int ExtractNum(string data)
        {
            string numString = data.Substring(0, data.Length - 1);
            int num = Convert.ToInt32(numString);
            return num;
        }

        private static int GetNumViablePairs(List<Node> nodeList)
        {
            int numViablePairs = 0;
            for (int i = 0; i < nodeList.Count - 1; i++)
            {
                for (int j = i + 1; j < nodeList.Count; j++)
                {
                    if (nodeList[i].IsViablePairWith(nodeList[j]))
                        numViablePairs++;

                    if (nodeList[j].IsViablePairWith(nodeList[i]))
                        numViablePairs++;
                }
            }

            return numViablePairs;
        }

        class Node
        {
            public int used;
            public int available;

            public Node(int used, int available)
            {
                this.used = used;
                this.available = available;
            }

            public bool IsViablePairWith(Node other)
            {
                return (this.available >= other.used) && (other.used > 0);
            }
        }

        //Printed out the matrix and counted it in my head
        //I had to get around the wall, then one space to the left of the G was 60 steps
        //61 after moving G to (0, 35). Then 35 repititions of 5 steps to get it to (0, 0)
        // 61 + 5 * 35 = 236
        //I've been wanting to do one sort of "by hand". Cool seeing how I used programming to easily visualize the data and then use my other skills to solve the problem!
        //UPDATE: Looked at the reddit solutions and saw that this was the expected to solve it. Glad I figured that out quickly!
        public static void PartTwo()
        {
            char[,] nodeMatrix = GetNodeMatrix();
            for (int i = 0; i < nodeMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < nodeMatrix.GetLength(1); j++)
                    Console.Write(nodeMatrix[i, j]);
                Console.WriteLine();
            }
        }

        //This assumes a lot of things about the data.
        //Lots of things here specific to this problem
        private static char[,] GetNodeMatrix()
        {
            int numRows = 25;
            int numCols = 37;
            char[,] nodeMatrix = new char[numRows, numCols];
            string[] lines = File.ReadAllLines("input22.txt");

            for (int i = 0; i < numCols; i++)
            {
                for (int j = 0; j < numRows; j++)
                {
                    string line = lines[i * numRows + j + 2];
                    string[] data = line.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    int used = ExtractNum(data[2]);
                    nodeMatrix[j, i] = GetCorrespondingChar(used);
                }
            }

            nodeMatrix[0, 36] = 'G';

            return nodeMatrix;
        }

        private static char GetCorrespondingChar(int used)
        {
            if (used == 0)
                return '_';
            else if (used < 100)
                return '.';
            else
                return '#';
        }


    }
}
