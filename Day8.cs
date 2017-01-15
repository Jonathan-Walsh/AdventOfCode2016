using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class Day8
{

    public static void PartOneAndTwo()
    {
        string[][] lines = GetLines();
        int[,] screen = new int[6, 50];     //1 = on, 0 = off

        foreach (string[] line in lines)
        {
            if (line.Length == 2)       //rect AxB
            {
                int indexX = line[1].IndexOf("x");
                int cols = Int32.Parse(line[1].Substring(0, indexX));
                int rows = Int32.Parse(line[1].Substring(indexX + 1));

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        screen[i, j] = 1;
                    }
                }

            }
            else if (line[1] == "row")              //Rotate row
            {
                int row = Int32.Parse(line[2].Substring(2));
                int rotations = Int32.Parse(line[4]);
                int[] newRow = new int[50];
                for (int i = 0; i < 50; i++)
                {
                    int val = screen[row, i];
                    int newIndex = (i + rotations) % 50;
                    newRow[newIndex] = val;
                }
                for (int j = 0; j < 50; j++)
                {
                    screen[row, j] = newRow[j];
                }
            }
            else                   //Rotate column
            {
                int col = Int32.Parse(line[2].Substring(2));
                int rotations = Int32.Parse(line[4]);
                int[] newCol = new int[50];
                for (int i = 0; i < 6; i++)
                {
                    int val = screen[i, col];
                    int newIndex = (i + rotations) % 6;
                    newCol[newIndex] = val;
                }
                for (int j = 0; j < 6; j++)
                {
                    screen[j, col] = newCol[j];
                }
            }
        }


        Console.WriteLine(GetPixels(screen));
    }

    private static string[][] GetLines()
    {
        StreamReader file = new StreamReader("input8.txt");
        List<string> lines = new List<string>();
        string line;

        while ((line = file.ReadLine()) != null)
        {
            lines.Add(line);
        }

        string[][] splitLines = new string[lines.Count][];
        int i = 0;
        foreach (string jointLine in lines) {
            splitLines[i] = jointLine.Split(' ');
            i += 1;
        }

        return splitLines;
    }

    private static int GetPixels(int[,] screen)
    {
        int numPixels = 0;
        for (int i = 0; i < screen.GetLength(0); i++)
        {
            for (int j = 0; j < screen.GetLength(1); j++)
            {
                if (screen[i,j] == 1) { numPixels += 1; Console.Write(screen[i, j]); }
                else { Console.Write(" "); }
            }
            Console.WriteLine();
        }
        return numPixels;
    }

}
