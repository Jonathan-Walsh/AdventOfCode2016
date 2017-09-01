using System.IO;
using System.Collections.Generic;
using System;

namespace AdventOfCode2016Part2
{
    class Day12
    {
        static int a;
        static int b;
        static int c;
        static int d;

        public static void PartOne()
        {
            List<string[]> instructions = GetListOfInstructionsFromFile();
            ReadInstructions(instructions);
            Console.WriteLine(a);
            Console.Read();
        } 

        public static void PartTwo()
        {
            c = 1;
            PartOne();
        }

        private static List<string[]> GetListOfInstructionsFromFile()
        {
            StreamReader file = new StreamReader("input12.txt");
            List<string[]> instructions = new List<string[]>();
            string line;

            while ((line = file.ReadLine()) != null)
            {
                string[] instruction = line.Split();
                instructions.Add(instruction);
            }

            return instructions;
        }

        private static void ReadInstructions(List<string[]> instructions)
        {
            int programCounter = 0;
            while (programCounter < instructions.Count)
            {
                string[] currentInstruction = instructions[programCounter];
                programCounter = DoInstruction(currentInstruction, programCounter);
            }
        }

        private static int DoInstruction(string[] instruction, int pc)
        {
            string instructionType = instruction[0];
            if (instructionType == "cpy")
            {
                Copy(instruction[1], instruction[2]);
                pc += 1;
            }
            else if (instructionType == "inc")
            {
                Inc(instruction[1]);
                pc += 1;
            }
            else if (instructionType == "dec")
            {
                Dec(instruction[1]);
                pc += 1;
            }  
            else if (instructionType == "jnz")
                pc += Jump(instruction[1], instruction[2]);
            else
                throw new ArgumentException();

            return pc;
        }

        private static void Copy(string valueOrRegister, string register)
        {
            int value = GetVal(valueOrRegister);
            

            SetRegisterVal(register, value);
        }

        private static bool IsRegister(string str)
        {
            if (str.Length > 1)
                return false;

            if (str[0] >= 'a' && str[0] <= 'd')
                return true;
            else
                return false;
        }

        private static void Inc(string register)
        {
            int currentVal = GetRegisterVal(register);
            SetRegisterVal(register, currentVal + 1);
        }

        private static void Dec(string register)
        {
            int currentVal = GetRegisterVal(register);
            SetRegisterVal(register, currentVal - 1);
        }

        private static int Jump(string valueOrRegister, string jumpDistance)
        {
            int val = GetVal(valueOrRegister);
            if (val == 0)
                return 1;
            else
                return Convert.ToInt32(jumpDistance);
        }

        private static int GetVal(string valueOrRegister)
        {
            int value;
            if (IsRegister(valueOrRegister))
                value = GetRegisterVal(valueOrRegister);
            else
                value = Convert.ToInt32(valueOrRegister);
            return value;
        }

        private static int GetRegisterVal(string register)
        {
            if (register == "a")
                return a;
            else if (register == "b")
                return b;
            else if (register == "c")
                return c;
            else if (register == "d")
                return d;
            else
                throw new ArgumentException();
        }

        private static void SetRegisterVal(string register, int val)
        {
            if (register == "a")
                a = val;
            else if (register == "b")
                b = val;
            else if (register == "c")
                c = val;
            else if (register == "d")
                d = val;
            else
                throw new ArgumentException();
        }
    }
}
