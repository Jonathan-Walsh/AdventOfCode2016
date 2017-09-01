using System;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2016Part2
{
    class Day14
    {
        //I should have worked a little harder on my Pt.2 algorthm because it currently runs VERY slowly
        // and I had to run it many times because of small mistakes. I also should've had it hash until it found all  64 keys
        // Right now it is hardcoded to compute 30,000 hashes and then, if there are 64 in there, it's fine.

        static string salt = "ahsbgdzn";

        public static void PartOne()
        {
            int numKeysFound = 0;
            int i = 0;
            while (numKeysFound < 64)
            {
                if (IsKey(i))
                {
                    numKeysFound++;
                    Console.WriteLine("Key #{0} Found: {1}", numKeysFound, i);
                }
                    
                i++;
            }
            Console.Read();
        }

        public static void PartTwo()
        {
            string[] hashes = new string[30000];
            for (int j = 0; j < hashes.Length; j++)
                hashes[j] = GetMD5Hash(salt + j.ToString(), 2017);

            int numKeysFound = 0;
            int i = 0;

            while (numKeysFound < 64)
            {
                if (IsKey(i, hashes))
                {
                    numKeysFound++;
                    Console.WriteLine("Key #{0} Found: {1}", numKeysFound, i);
                }

                i++;
            }
            Console.Read();
        }

        private static bool IsKey(int i)
        {
            string hash = GetMD5Hash(salt + i.ToString());
            if (ContainsRepeatedChar(hash, 3))
            {
                char repeatedChar = GetRepeatedChar(hash, 3);
                for (int j = i + 1; j < i + 1000; j++)
                {
                    string nextHash = GetMD5Hash(salt + j.ToString());
                    if (ContainsRepeatedChar(nextHash, 5, repeatedChar))
                        return true;
                }
            }
            return false;
        }

        private static bool IsKey(int i, string[] hashes)
        {
            string hash = hashes[i];
            if (ContainsRepeatedChar(hashes[i], 3))
            {
                char repeatedChar = GetRepeatedChar(hash, 3);
                for (int j = i + 1; j < i + 1000; j++)
                {
                    string nextHash = hashes[j];
                    if (ContainsRepeatedChar(nextHash, 5, repeatedChar))
                        return true;
                }
            }
            return false;
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

        private static string GetMD5Hash(string input, int numHashes)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes;
            for (int i = 0; i < numHashes; i++)
            {
                bytes = System.Text.Encoding.ASCII.GetBytes(input);
                bytes = md5.ComputeHash(bytes);
                StringBuilder hashBuilder = new StringBuilder();
                for (int j = 0; j < bytes.Length; j++)
                    hashBuilder.Append(bytes[j].ToString("x2"));
                input = hashBuilder.ToString();
            }
            string hash = input;
            return hash;
        }
        private static bool ContainsRepeatedChar(string hash, int numRepeats)
        {
            for (int i = 0; i <= hash.Length - numRepeats; i ++)
            {
                int j = 1;
                while (j < numRepeats && hash[i] == hash[i+j])
                    j++;

                if (j == numRepeats)
                    return true;
            }
            return false;
        }

        private static bool ContainsRepeatedChar(string hash, int numRepeats, char repeatedChar)
        {
            if (ContainsRepeatedChar(hash, numRepeats) && repeatedChar == GetRepeatedChar(hash, numRepeats))
                return true;
            else
                return false;
        }

        private static char GetRepeatedChar(string hash, int numRepeats)
        {
            for (int i = 0; i < hash.Length; i++)
            {
                int j = 1;
                while (j < numRepeats && hash[i] == hash[i + j])
                    j++;

                if (j == numRepeats)
                    return hash[i];
            }

            throw new ArgumentException();
        }

    }
}
