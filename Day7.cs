using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Day7
{
    public static void PartOne()
    {
        StreamReader file = new StreamReader("input7.txt");
        string line;
        int num = 0;
        while ((line = file.ReadLine()) != null)
        {
            if (SupportsTLS(line))
            {
                num++;
            }
        }

        Console.WriteLine(num);
            
    }

    private static bool SupportsTLS(string line)
    {
        int startI = 0;
        bool supportsTLS = false;
        bool insideBrackets = false;
        while (startI < line.Length - 3)
        {
            string seq = line.Substring(startI, 4);
            if (seq[3] == '[')
            {
                insideBrackets = true;
                startI += 4;
            }
            else if (seq[3] == ']')
            {
                insideBrackets = false;
                startI += 4;
            }
            else if (insideBrackets)
            {
                if (seq[0] == seq[3] && seq[1] == seq[2] && seq[0] != seq[1])
                {
                    return false;
                }
                startI += 1;
            }
            else                   //Outside of brackets
            {
                if (seq[0] == seq[3] && seq[1] == seq[2] && seq[0] != seq[1])
                {
                    supportsTLS = true;
                }
                startI += 1;
            }
        }
        return supportsTLS;
    }


    public static void PartTwo()
    {
        StreamReader file = new StreamReader("input7.txt");
        string line;
        int num = 0;
        while ((line = file.ReadLine()) != null)
        {
            if (SupportsSSL(line))
            {
                num++;
            }
        }

        Console.WriteLine(num);
    }

    private static bool SupportsSSL(string line)
    {
        int startI = 0;
        bool insideBrackets = false;

        while (startI < line.Length - 2)
        {
            string seq = line.Substring(startI, 3);
            bool possibleSeq = false;
            if (seq[2] == '[')
            {
                insideBrackets = true;
                startI += 3;
            }
            else if (seq[2] == ']')
            {
                insideBrackets = false;
                startI += 3;
            }
            else if (insideBrackets)
            {
                if (seq[0] == seq[2] && seq[0] != seq[1])
                {
                    possibleSeq = true;
                }
                startI += 1;
            }
            else                   //Outside of brackets
            {
                if (seq[0] == seq[2] && seq[0] != seq[1])
                {
                    possibleSeq = true;
                }
                startI += 1;
            }
            
            if (possibleSeq)
            {
                if (CheckForMatch(startI, seq, line, insideBrackets))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private static bool CheckForMatch(int startI, string seq, string line, bool insideBrackets)
    {
        bool insideBrackets2 = insideBrackets;

        while (startI < line.Length - 2)
        {
            string seq2 = line.Substring(startI, 3);
            if (seq2[2] == '[')
            {
                insideBrackets2 = true;
                startI += 3;
            }
            else if (seq2[2] == ']')
            {
                insideBrackets2 = false;
                startI += 3;
            }
            else if (insideBrackets2 == insideBrackets)
            {
                startI += 1;
            }
            else                   //Opposite of seq (one inside, one outside)
            {
                if (seq2[0] == seq2[2] && seq2[0] != seq2[1] && seq[0] == seq2[1] && seq[1] == seq2[0])
                {
                    return true;
                }
                startI += 1;
            }
        }

        return false;
    }

}

