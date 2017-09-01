using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016Part2
{
    class Day15
    {

        public static void PartOne()
        {
            Disc[] discs = new Disc[6] { new Disc(7, 0), new Disc(13, 0), new Disc(3, 2), new Disc(5, 2), new Disc(17, 0), new Disc(19, 7) };
            FindTimeOfButtonPress(discs);
        }

        public static void PartTwo()
        {
            Disc[] discs = new Disc[7] { new Disc(7, 0), new Disc(13, 0), new Disc(3, 2), new Disc(5, 2), new Disc(17, 0), new Disc(19, 7), new Disc(11, 0) };
            FindTimeOfButtonPress(discs);
        }

        private static void FindTimeOfButtonPress(Disc[] discs)
        {
            bool timeFound = false;
            int timeOfButtonPress = 0;

            while (!timeFound)
            {
                if (AreSlotsAligned(discs, timeOfButtonPress))
                    timeFound = true;
                else
                    timeOfButtonPress++;
            }

            Console.WriteLine(timeOfButtonPress);
            Console.Read();
        }

        private static bool AreSlotsAligned(Disc[] discs, int timeOfButtonPress)
        {
            bool allSlotsAligned = true;
            for (int i = 0; i < discs.Length; i++)
            {
                int timeCapsuleHitsDisc = timeOfButtonPress + i + 1;
                int position = discs[i].GetPosition(timeCapsuleHitsDisc);
                if (position != 0)
                {
                    allSlotsAligned = false;
                    break;
                }    
            }
            return allSlotsAligned;
        }


        class Disc
        {
            int numPositions;
            int startingPosition;

            public Disc (int numPositions, int startingPosition)
            {
                this.numPositions = numPositions;
                this.startingPosition = startingPosition;
            }

            public int GetPosition(int time)
            {
                return (startingPosition + time) % numPositions;
            }
        }

    }
}
