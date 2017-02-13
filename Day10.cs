using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class Day10
{

    private static List<Bot> botList = new List<Bot>();
    private static bool botFound;
    private static bool outputProductFound;
    private static int output0;
    private static int output1;
    private static int output2;

    public static void PartOneAndTwo()
    {
        StreamReader file = new StreamReader("input10.txt");

    //Seperate instructions into initial and bot to bot
        List<string> initialInstructions = new List<string>();
        List<string> botToBotInstructions = new List<string>();
        string line;
        while ((line = file.ReadLine()) != null)
        {
            if (line.Split()[0].Equals("value"))
                initialInstructions.Add(line);
            else
                botToBotInstructions.Add(line);
        }

        GiveStartingChips(initialInstructions);
        //Find the bot carrying the value 61 and value 17 chips
        botFound = false;
        outputProductFound = false;
        output0 = 0; output1 = 0; output2 = 0;
        while (!botFound || !outputProductFound)
        {
            TransferChips(botToBotInstructions);
        }
    }


    private static void GiveStartingChips(List<string> instructions)
    {
        foreach (string instruction in instructions)
        {
            int botNum = Convert.ToInt32(instruction.Split()[5]);
            int chipVal = Convert.ToInt32(instruction.Split()[1]);
            GiveChip(botNum, chipVal);
        }
    }

    private static void TransferChips(List<string> instructions)
    {
        foreach (string instruction in instructions)
        {
            int botNum = Convert.ToInt32(instruction.Split()[1]);
            if (BotInList(botNum))
            {
                Bot bot = GetBot(botNum);
                if (bot.chipVal1 > 0 && bot.chipVal2 > 0)
                {
                    int lowChipVal;
                    int highChipVal;
                    //Determine the two chip values
                    if (bot.chipVal1 <= bot.chipVal2)
                    {
                        lowChipVal = bot.chipVal1;
                        highChipVal = bot.chipVal2;
                    }
                    else
                    {
                        lowChipVal = bot.chipVal2;
                        highChipVal = bot.chipVal1;
                    }
                    //Give the chips
                    if (instruction.Split()[5].Equals("bot"))
                    {
                        int botNum2 = Convert.ToInt32(instruction.Split()[6]);
                        GiveChip(botNum2, lowChipVal);
                    }
                    else
                    {
                        int output = Convert.ToInt32(instruction.Split()[6]);
                        if (output == 0)
                            output0 = lowChipVal;
                        else if (output == 1)
                        {
                            output1 = lowChipVal;
                        }
                        else if (output == 2)
                        {
                            output2 = lowChipVal;
                        }
                    }
                    if (instruction.Split()[10].Equals("bot"))
                    {
                        int botNum3 = Convert.ToInt32(instruction.Split()[11]);
                        GiveChip(botNum3, highChipVal);
                    }
                    else
                    {
                        int output = Convert.ToInt32(instruction.Split()[6]);
                        if (output == 0)
                            output0 = highChipVal;
                        else if (output == 1)
                        {
                            output1 = highChipVal;
                        }
                        else if (output == 2)
                        {
                            output2 = highChipVal;
                        }
                    }
                    bot.chipVal1 = 0;
                    bot.chipVal2 = 0;
                    if (!botFound && FindWantedBot())        //Part One
                    {
                        botFound = true;
                    }
                    if (!outputProductFound)        //Part Two
                    {
                        CheckOutputProduct();
                    }
                }
            }
        }
    }

    private static bool FindWantedBot()
    {
        foreach (Bot bot in botList)
        {
            if (bot.chipVal1 == 17 && bot.chipVal2 == 61)
            {
                Console.WriteLine("Part One: {0}", bot.botNum);
                return true;
            }
            else if (bot.chipVal1 == 61 && bot.chipVal2 == 17)
            {
                Console.WriteLine("Part One: {0}", bot.botNum);
                return true;
            }
        }
        return false;
    }

    private static void GiveChip(int botNum, int chipVal)
    {
        foreach (Bot bot in botList)
        {
            if (bot.botNum == botNum)
            {
                if (bot.chipVal1 == 0)
                    bot.chipVal1 = chipVal;
                else
                    bot.chipVal2 = chipVal;
                return;
            }      
        }
        botList.Add(new Bot(botNum, chipVal));
    }

    private static bool BotInList(int botNum)
    {
        foreach (Bot bot in botList)
        {
            if (bot.botNum == botNum)
                return true;
        }
        return false;
    }

    private static Bot GetBot(int botNum)
    {
        foreach (Bot bot in botList)
        {
            if (bot.botNum == botNum)
                return bot;
        }
        return null;
    }

    private static void CheckOutputProduct()
    {
        if (output0 > 0 && output1 > 0 && output2 > 0)
        {
            int outputProduct = output0 * output1 * output2;
            Console.WriteLine("Part Two: {0}", outputProduct);
            outputProductFound = true;
        }
    }

    private class Bot
    {
        public int botNum;
        public int chipVal1;
        public int chipVal2;

        public Bot(int botNum, int chipVal)
        {
            this.botNum = botNum;
            this.chipVal1 = chipVal;
        }
    }
}
