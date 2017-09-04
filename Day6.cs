using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016Part2
{
    class Day6
    {
        public static void PartOne()
        {
            StreamReader file = new StreamReader("input6.txt");
            List<string> lines = new List<string>();
            string line;

            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }

            string answer = GetFreqs(lines, 1);
            Console.WriteLine(answer);
        }

        public static void PartTwo()
        {
            StreamReader file = new StreamReader("input6.txt");
            List<string> lines = new List<string>();
            string line;

            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }

            string answer = GetFreqs(lines, 2);
            Console.WriteLine(answer);
        }

        private static string GetFreqs(List<string> lines, int part)
        {
        //Create frequency arrays for each position
            int[] freqs0 = new int[26];
            int[] freqs1 = new int[26];
            int[] freqs2 = new int[26];
            int[] freqs3 = new int[26];
            int[] freqs4 = new int[26];
            int[] freqs5 = new int[26];
            int[] freqs6 = new int[26];
            int[] freqs7 = new int[26];
         //Update arrays
            for (int i = 0; i < lines.Count; i++)
            {
                int char0 = Convert.ToInt16(lines[i][0]) - 97;
                freqs0[char0] += 1;

                int char1 = Convert.ToInt16(lines[i][1]) - 97;
                freqs1[char1] += 1;

                int char2 = Convert.ToInt16(lines[i][2]) - 97;
                freqs2[char2] += 1;

                int char3 = Convert.ToInt16(lines[i][3]) - 97;
                freqs3[char3] += 1;

                int char4 = Convert.ToInt16(lines[i][4]) - 97;
                freqs4[char4] += 1;

                int char5 = Convert.ToInt16(lines[i][5]) - 97;
                freqs5[char5] += 1;

                int char6 = Convert.ToInt16(lines[i][6]) - 97;
                freqs6[char6] += 1;

                int char7 = Convert.ToInt16(lines[i][7]) - 97;
                freqs7[char7] += 1;
            }
        //Find most frequent characters
            int max0, max1, max2, max3, max4, max5, max6, max7;
            max0 = max1 = max2 = max3 = max4 = max5 = max6 = max7 = 0;
            if (part == 1)
            {
                for (int j = 1; j < 26; j++)
                {
                    if (freqs0[j] > freqs0[max0]) { max0 = j; }
                    if (freqs1[j] > freqs1[max1]) { max1 = j; }
                    if (freqs2[j] > freqs2[max2]) { max2 = j; }
                    if (freqs3[j] > freqs3[max3]) { max3 = j; }
                    if (freqs4[j] > freqs4[max4]) { max4 = j; }
                    if (freqs5[j] > freqs5[max5]) { max5 = j; }
                    if (freqs6[j] > freqs6[max6]) { max6 = j; }
                    if (freqs7[j] > freqs7[max7]) { max7 = j; }
                }
            }
            else
            {
                for (int j = 1; j < 26; j++)
                {
                    if (freqs0[j] < freqs0[max0]) { max0 = j; }
                    if (freqs1[j] < freqs1[max1]) { max1 = j; }
                    if (freqs2[j] < freqs2[max2]) { max2 = j; }
                    if (freqs3[j] < freqs3[max3]) { max3 = j; }
                    if (freqs4[j] < freqs4[max4]) { max4 = j; }
                    if (freqs5[j] < freqs5[max5]) { max5 = j; }
                    if (freqs6[j] < freqs6[max6]) { max6 = j; }
                    if (freqs7[j] < freqs7[max7]) { max7 = j; }
                }
            }
        //Create string
            char[] answer = new char[8];
            answer[0] = Convert.ToChar(max0 + 97);
            answer[1] = Convert.ToChar(max1 + 97);
            answer[2] = Convert.ToChar(max2 + 97);
            answer[3] = Convert.ToChar(max3 + 97);
            answer[4] = Convert.ToChar(max4 + 97);
            answer[5] = Convert.ToChar(max5 + 97);
            answer[6] = Convert.ToChar(max6 + 97);
            answer[7] = Convert.ToChar(max7 + 97);

            return new string(answer);
        }
    }
}
