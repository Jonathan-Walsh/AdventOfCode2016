using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016Part2
{
    class Day16
    {
        static string input = "10011111011011001";

        public static void PartOne()
        {
            int diskLength = 272;
            FindCheckSum(diskLength);
        }

        public static void PartTwo()
        {
            int diskLength = 35651584;
            FindCheckSum(diskLength);
        }

        private static void FindCheckSum(int diskLength)
        {
            while (input.Length < diskLength)
                input = Transform(input);

            if (input.Length > diskLength)
                input = Shorten(input, diskLength);

            string checkSum = GetCheckSum(input);
            while (checkSum.Length % 2 == 0)
            {
                checkSum = GetCheckSum(checkSum);
            }

            Console.WriteLine(checkSum);
            Console.Read();
        }

        private static string Transform (string a)
        {
            string b = (string) a.Clone();
            int length = b.Length;

            char[] reversedCharsOfB = new char[length];
            for (int i = 0; i < length; i++)
                reversedCharsOfB[i] = b[length - 1 - i];
            b = new string(reversedCharsOfB);

            char[] flippedBitsOfB = new char[length];
            for (int i = 0; i < length; i++)
            {
                if (b[i] == '1')
                    flippedBitsOfB[i] = '0';
                else if (b[i] == '0')
                    flippedBitsOfB[i] = '1';
                else
                    throw new ArgumentException();
            }
            b = new string(flippedBitsOfB);

            StringBuilder bBuilder = new StringBuilder();
            bBuilder.Append(a);
            bBuilder.Append('0');
            bBuilder.Append(b);
            b = bBuilder.ToString();

            return b;
        }

        private static string Shorten(string input, int length)
        {
            if (length > input.Length)
                throw new ArgumentException();

            return input.Substring(0, length);
        }

        public static string GetCheckSum(string input)
        {
            char[] checkSum = new char[input.Length / 2];
            for (int i = 0; i < input.Length; i += 2)
            {
                if (input[i] == input[i + 1])
                    checkSum[i / 2] = '1';
                else
                    checkSum[i / 2] = '0';
            }

            return new string(checkSum);
        }
    }
}
