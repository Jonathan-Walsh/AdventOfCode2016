using System.IO;
using System.Collections.Generic;
using System;

namespace AdventOfCode2016Part2
{
    class Day20
    {

        public static void PartOne()
        {
            List<BlockedIPRange> blockedIPRanges = GetBlockedIPRanges();
            blockedIPRanges.Sort();
            uint lowestAllowedIP = FindLowestAllowedIP(blockedIPRanges);
            Console.WriteLine(lowestAllowedIP);
        }

        public static void PartTwo()
        {
            List<BlockedIPRange> blockedIPRanges = GetBlockedIPRanges();
            blockedIPRanges.Sort();
            uint numAllowedIPs = FindNumAllowedIPs(blockedIPRanges);
            Console.WriteLine(numAllowedIPs);
        }

        private static List<BlockedIPRange> GetBlockedIPRanges()
        {
            StreamReader file = new StreamReader("input20.txt");
            List<BlockedIPRange> blockedIPRanges = new List<BlockedIPRange>();
            string line;
            while ((line = file.ReadLine()) != null)
            {
                string[] startAndEnd = line.Split('-');
                uint rangeStart = Convert.ToUInt32(startAndEnd[0]);
                uint rangeEnd = Convert.ToUInt32(startAndEnd[1]);
                BlockedIPRange nextBlockedIPRange = new BlockedIPRange(rangeStart, rangeEnd);
                blockedIPRanges.Add(nextBlockedIPRange);
            }

            return blockedIPRanges;
        }

        private static uint FindLowestAllowedIP(List<BlockedIPRange> blockedIPRanges)
        {
            uint highestEnd = blockedIPRanges[0].GetEnd();
            for (int i = 0; i < blockedIPRanges.Count - 1; i++)
            {
                BlockedIPRange range = blockedIPRanges[i];
                BlockedIPRange nextRange = blockedIPRanges[i + 1];

                if (range.GetEnd() > highestEnd)
                    highestEnd = range.GetEnd();

                if (nextRange.GetStart() > highestEnd + 1)
                    return highestEnd + 1;
            }

            throw new Exception("All IPs are blocked. Sorry");
        }

        private static uint FindNumAllowedIPs(List<BlockedIPRange> blockedIPRanges)
        {
            uint numAllowedIPs = 0;
            uint highestEnd = blockedIPRanges[0].GetEnd();
            for (int i = 0; i < blockedIPRanges.Count - 1; i++)
            {
                BlockedIPRange range = blockedIPRanges[i];
                BlockedIPRange nextRange = blockedIPRanges[i + 1];

                if (range.GetEnd() > highestEnd)
                    highestEnd = range.GetEnd();

                if (nextRange.GetStart() - 1 > highestEnd)
                {
                    numAllowedIPs += nextRange.GetStart() - highestEnd - 1;
                    highestEnd = nextRange.GetEnd();
                } 
            }

            return numAllowedIPs;
        }

        class BlockedIPRange : IComparable<BlockedIPRange>
        {
            public uint start;
            public uint end;

            public BlockedIPRange(uint start, uint end)
            {
                this.start = start;
                this.end = end;
            }

            public uint GetStart()
            {
                return start;
            }

            public uint GetEnd()
            {
                return end;
            } 

            public int CompareTo(BlockedIPRange otherRange)
            {
                if (otherRange == null)
                    return 1;
                else if (this.GetStart() > otherRange.GetStart())
                    return 1;
                else if (this.GetStart() < otherRange.GetStart())
                    return -1;
                else
                    return 0;
            }
        }
    }
}
