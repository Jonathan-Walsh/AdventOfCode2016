using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016Part2
{
    class Day19
    {
        static int numElves = 3012210;

        public static void PartOne()
        {
            SortedSet<int> elvesInCircle = new SortedSet<int>();
            for (int i = 0; i < numElves; i++)
                elvesInCircle.Add(i + 1);

            while (elvesInCircle.Count > 1)
            {
                int[] elvesLeft = elvesInCircle.ToArray<int>();

                bool oddNumOfElvesLeft = (elvesLeft.Length % 2 == 1);

                for (int i = 1; i < elvesLeft.Length; i += 2)
                    elvesInCircle.Remove(elvesLeft[i]);

                if (elvesInCircle.Count == 1)
                    break;
                else if (oddNumOfElvesLeft)
                    elvesInCircle.Remove(elvesLeft[0]);
            }

            Console.WriteLine(elvesInCircle.ElementAt(0));
        }
        
        public static void PartTwo()
        {
            List<int> elves = new List<int>();
            for (int i = 0; i < numElves; i++)
                elves.Add(i + 1);
            bool[] removed = new bool[numElves];
            while (elves.Count > 1)
            {
                int numElvesInCircle = elves.Count;
                int numRemovedInBetween = 0;
                for (int i = 0; i < elves.Count; i++)
                {
                    if (numElvesInCircle == 1)
                        break;
                    else if (removed[i])
                        numRemovedInBetween--;
                    else
                    {
                        int elfToRemove = (i + (numElvesInCircle / 2) + numRemovedInBetween) % elves.Count;
                        removed[elfToRemove] = true;
                        numRemovedInBetween++;
                        numElvesInCircle--;
                    }
                }

                elves = MakeNewCircle(elves, removed);
                removed = new bool[numElves];
            }

            Console.WriteLine(elves[0]);
        }

        private static List<int> MakeNewCircle(List<int> oldCircle, bool[] removed)
        {
            List<int> newCircle = new List<int>();
            for (int i = 0; i < oldCircle.Count; i++)
            {
                if (!removed[i])
                    newCircle.Add(oldCircle[i]);
            }
            return newCircle;
        }

    }
}
