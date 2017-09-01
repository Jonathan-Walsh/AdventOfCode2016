using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2016Part2
{
    class Day21
    {
        static string input = "abcdefgh";
        static string input2 = "fbgdceah";


        public static void PartOne()
        {
            Password password = new Password(input);
            List<string> instructions = GetInstructions();
            password.Scramble(instructions);
            Console.WriteLine(password.ToString());
        }

        public static void PartTwo()
        {
            Password password = new Password(input2);
            List<string> instructions = GetInstructions();
            password.Unscramble(instructions);
            Console.WriteLine(password.ToString());
        }

        private static List<string> GetInstructions()
        {
            List<string> instructions = new List<string>();
            StreamReader file = new StreamReader("input21.txt");
            List<string> blockedIPRanges = new List<string>();
            string line;

            while ((line = file.ReadLine()) != null)
                instructions.Add(line);

            return instructions;
        }

        class Password
        {
            char[] password;

            public Password(string password)
            {
                this.password = password.ToCharArray();
            }

            override public string ToString()
            {
                return new string(password);
            }

            public void Scramble(List<string> instructions)
            {
                foreach(string instruction in instructions)
                {
                    string[] words = instruction.Split();
                    if (words[0] == "swap" && words[1] == "position")
                        SwapPosition(System.Convert.ToInt32(words[2]), System.Convert.ToInt32(words[5]));
                    else if (words[0] == "swap" && words[1] == "letter")
                        SwapLetter(words[2][0], words[5][0]);
                    else if (words[0] == "rotate" && words[1] == "right")
                        RotateRight(System.Convert.ToInt32(words[2]));
                    else if (words[0] == "rotate" && words[1] == "left")
                        RotateLeft(System.Convert.ToInt32(words[2]));
                    else if (words[0] == "rotate" && words[1] == "based")
                        RotateByPosition(words[6][0]);
                    else if (words[0] == "reverse")
                        Reverse(System.Convert.ToInt32(words[2]), System.Convert.ToInt32(words[4]));
                    else if (words[0] == "move")
                        Move(System.Convert.ToInt32(words[2]), System.Convert.ToInt32(words[5]));
                    else
                        throw new ArgumentException();
                }
            }

            public void Unscramble(List<string> instructions)
            {
                for (int i = instructions.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine(password);
                    string instruction = instructions[i];
                    Console.WriteLine(instruction);
                    
                    string[] words = instruction.Split();
                    if (words[0] == "swap" && words[1] == "position")
                        SwapPosition(System.Convert.ToInt32(words[2]), System.Convert.ToInt32(words[5]));
                    else if (words[0] == "swap" && words[1] == "letter")
                        SwapLetter(words[2][0], words[5][0]);
                    else if (words[0] == "rotate" && words[1] == "right")
                        RotateLeft(System.Convert.ToInt32(words[2]));
                    else if (words[0] == "rotate" && words[1] == "left")
                        RotateRight(System.Convert.ToInt32(words[2]));
                    else if (words[0] == "rotate" && words[1] == "based")
                        RotateByPositionUnscramble(words[6][0]);
                    else if (words[0] == "reverse")
                        Reverse(System.Convert.ToInt32(words[2]), System.Convert.ToInt32(words[4]));
                    else if (words[0] == "move")
                        Move(System.Convert.ToInt32(words[5]), System.Convert.ToInt32(words[2]));
                    else
                        throw new ArgumentException();
                }
            }

            private void SwapPosition(int position1, int position2)
            {
                char temp = password[position1];
                password[position1] = password[position2];
                password[position2] = temp;
            }

            private void SwapLetter(char letter1, char letter2)
            {
                char temp = '*';
                Convert(letter1, temp);
                Convert(letter2, letter1);
                Convert(temp, letter2);
            }

            private void Convert(char oldChar, char newChar)
            {
                for (int i = 0; i < password.Length; i++)
                    if (password[i] == oldChar)
                        password[i] = newChar;
            }

            private void Reverse(int startSeq, int endSeq)
            {
                char[] newPassword = new char[password.Length];

                int i = 0;
                while (i < startSeq)
                {
                    newPassword[i] = password[i];
                    i++;
                }

                for (int j = startSeq; j < endSeq + 1; j++)
                {
                    newPassword[j] = password[endSeq - (j - startSeq)];
                }

                i = endSeq + 1;
                while (i < password.Length)
                {
                    newPassword[i] = password[i];
                    i++;
                }

                password = newPassword;
            }

            private void RotateLeft(int numSteps)
            {
                char[] newPassword = new char[password.Length];
                for (int i = 0; i < password.Length; i++)
                    newPassword[i] = password[(i + numSteps) % password.Length];
                password = newPassword;
            }

            private void RotateRight(int numSteps)
            {
                Reverse(0, password.Length - 1);
                RotateLeft(numSteps);
                Reverse(0, password.Length - 1);
            }

            private void RotateByPosition(char c)
            {
                bool charFound = false;
                int i = 0;
                while (!charFound)
                {
                    if (password[i] == c)
                    {
                        int numSteps = 1 + i;
                        if (i >= 4)
                            numSteps += 1;
                        RotateRight(numSteps);
                        charFound = true;
                    }
                    else
                        i++;
                }
            }

            private void RotateByPositionUnscramble(char c)
            {
                bool charFound = false;
                int i = 0;
                while (!charFound)
                {
                    if (password[i] == c)
                    {
                        if (i == 0 || i == 1)
                            RotateLeft(1);
                        else if (i == 2)
                            RotateLeft(6);
                        else if (i == 3)
                            RotateLeft(2);
                        else if (i == 4)
                            RotateLeft(7);
                        else if (i == 5)
                            RotateLeft(3);
                        else if (i == 6)
                            RotateLeft(0);
                        else if (i == 7)
                            RotateLeft(4);
                        charFound = true;
                    }
                    else
                        i++;
                }
            }

            private void Move(int oldPosition, int newPosition)
            {
                char[] newPassword = new char[password.Length];
                int currentCharIndex = 0;
                for (int i = 0; i < password.Length; i++)
                {
                    if (currentCharIndex == oldPosition)
                        currentCharIndex++;

                    if (i == newPosition)
                        newPassword[i] = password[oldPosition];
                    else
                    {
                        newPassword[i] = password[currentCharIndex];
                        currentCharIndex++;
                    }    
                }

                password = newPassword;
            }
        }
    }
}
