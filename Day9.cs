using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class Day9
{

   
    public static void PartOne()
    {
        char[] compressedFile = GetFile();
        long decompressedLength = GetDecompressedLength(compressedFile, "One");
        Console.WriteLine("Part One: {0} characters", decompressedLength - 1); //Subract 1 because file ends (I believe) with a \n character
    }

    private static char[] GetFile()
    {
        StreamReader file = new StreamReader("input9.txt");
        string fileString = file.ReadToEnd();
        return fileString.ToCharArray();
    }

    private static long GetDecompressedLength(char[] compressedFile, string part)
    {
        long index = 0;
        long decompressedLength = 0;
        bool insideBrackets = false;

        while (index < compressedFile.Length)
        {
            if (!insideBrackets)
            {
                if (compressedFile[index] == '(')
                {
                    insideBrackets = true;
                }
                else
                {
                    decompressedLength += 1;
                }
                index += 1;
            }       
            else
            {
                long numCharacters = GetNumCharacters(compressedFile, index);
                long numRepeats = GetNumRepeats(compressedFile, index);
                if (part.Equals("One"))
                {
                    decompressedLength += numCharacters * numRepeats;
                }
                if (part.Equals("Two"))
                {
                    char[] expansionOfFile = GetExpansion(compressedFile, index, numCharacters, numRepeats);
                    decompressedLength += GetDecompressedLength(expansionOfFile, "Two");
                }
                index = GetNewIndex(compressedFile, index, numCharacters);
                insideBrackets = false;
            }
        }

        return decompressedLength;
    }

    private static long GetNumCharacters(char[] file, long index)
    {
        string numCharsString = "";
        while(file[index] != 'x')
        {
            numCharsString += file[index];
            index += 1;
        }
        return long.Parse(numCharsString);
    }

    private static long GetNumRepeats(char[] file, long index)
    {
        while (file[index] != 'x')
        {
            index += 1;
        }
        index += 1;
        string numRepeatsString = "";
        while (file[index] != ')')
        {
            numRepeatsString += file[index];
            index += 1;
        }
        return long.Parse(numRepeatsString);
    }

    private static long GetNewIndex(char[] file, long index, long numCharacters)
    {
        while (file[index] != ')')
        {
            index += 1;
        }
        index += 1;
        return index + numCharacters;
    }

    public static void PartTwo()
    {
        char[] compressedFile = GetFile();
        long decompressedLength = GetDecompressedLength(compressedFile, "Two");
        Console.WriteLine("Part Two: {0} characters", decompressedLength - 1); //Subract 1 because file ends (I believe) with a \n character
    }

    //Part Two Only
    private static char[] GetExpansion(char[] file, long index, long numChars, long numRepeats)
    {
        while (file[index] != ')')
        {
            index += 1;
        }
        index += 1;

        char[] expansion = new char[numChars * numRepeats];

        for (long i = 0; i < numChars; i++)
        {
            for (long j = 0; j < numRepeats; j++)
            {
                long arrayIndex = i + (j * numChars);
                expansion[arrayIndex] = file[index + i];
            }
        }
        return expansion;
    }

}
