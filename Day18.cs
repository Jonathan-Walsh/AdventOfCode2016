using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016Part2
{
    class Day18
    {
        static string input = ".^.^..^......^^^^^...^^^...^...^....^^.^...^.^^^^....^...^^.^^^...^^^^.^^.^.^^..^.^^^..^^^^^^.^^^..^";

        public static void PartOne()
        {
            FindNumSafeTiles(40);
        }

        public static void PartTwo()
        {
            FindNumSafeTiles(400000);
        }

        private static void FindNumSafeTiles(int numRows)
        {
            char[,] rowsOfTiles = new char[numRows, input.Length];
            AddFirstRow(rowsOfTiles);
            IdentifyRestOfTiles(rowsOfTiles);
            int numSafeTiles = GetNumSafeTiles(rowsOfTiles);

            Console.WriteLine(numSafeTiles);
        }

        private static void AddFirstRow(char[,] rowsOfTiles)
        {
            for (int i = 0; i < rowsOfTiles.GetLength(1); i++)
                rowsOfTiles[0, i] = input[i];
        }

        private static void IdentifyRestOfTiles(char[,] rowsOfTiles)
        {
            int numRows = rowsOfTiles.GetLength(0);
            int numTilesInRow = rowsOfTiles.GetLength(1);

            for (int i = 1; i < numRows; i++)
            {
                for (int j = 0; j < numTilesInRow; j++)
                {
                    char left, center, right;
                    char safe = '.';
                    char trap = '^';

                    if (j == 0)
                        left = safe;
                    else
                        left = rowsOfTiles[i - 1, j - 1];

                    center = rowsOfTiles[i - 1, j];

                    if (j == numTilesInRow - 1)
                        right = safe;
                    else
                        right = rowsOfTiles[i - 1, j + 1];

                    if (left == right)
                        rowsOfTiles[i, j] = safe;
                    else
                        rowsOfTiles[i, j] = trap;
                }
            }
        }

        private static int GetNumSafeTiles(char[,] rowsOfTiles)
        {
            int numSafeTiles = 0;

            foreach (char tile in rowsOfTiles)
                if (tile == '.')
                    numSafeTiles++;

           return numSafeTiles;
        }
    }
}
