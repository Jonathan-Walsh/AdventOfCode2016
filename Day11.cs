using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Day11
{
    class Floor
    {
        public List<string> microchipList;
        public List<string> rtgList;

        public Floor(List<string> microchips, List<string> rtgs)
        {
            microchipList = microchips;
            rtgList = rtgs;
        }

        public Floor()
        {
            microchipList = new List<string>();
            rtgList = new List<string>();
        }

        public static void MoveItem(Floor oldFloor, Floor newFloor, string item, string itemType)
        {
            if (itemType == "microchip")
            {
                if (oldFloor.microchipList.Contains(item))
                {
                    oldFloor.microchipList.Remove(item);
                    newFloor.microchipList.Add(item);
                    newFloor.microchipList.Sort();
                }
            }
            else if (itemType == "RTG")
            {
                if (oldFloor.rtgList.Contains(item))
                {
                    oldFloor.rtgList.Remove(item);
                    newFloor.rtgList.Add(item);
                    newFloor.rtgList.Sort();
                }
            }
            else
                throw new ArgumentException();
        }
    }

    static Floor floor0, floor1, floor2, floor3;
    static Floor[] floors;
    static int totalItemPairs;

    public static void PartOne()
    {
         floor0 = new Floor(new List<string> { "Th" }, new List<string> { "Th", "Pl", "St" });
         floor1 = new Floor(new List<string> { "Pl", "St" }, new List<string> {  });
         floor2 = new Floor(new List<string> { "Pr", "Ru" }, new List<string> { "Pr", "Ru" });
         floor3 = new Floor();
         floors = new Floor[] {floor0, floor1, floor2, floor3 };
         totalItemPairs = 5;

        Console.WriteLine("Part One: " + FindMinNumSteps(floors) + " steps");
    }

    public static void PartTwo()
    {
        floor0 = new Floor(new List<string> { "El", "Di", "Th" }, new List<string> { "El", "Di", "Pl", "St", "Th" });
        floor1 = new Floor(new List<string> { "Pl", "St" }, new List<string> { });
        floor2 = new Floor(new List<string> { "Pr", "Ru" }, new List<string> { "Pr", "Ru" });
        floor3 = new Floor();
        floors = new Floor[] { floor0, floor1, floor2, floor3 };
        totalItemPairs = 7;

        Console.WriteLine("Part One: " + FindMinNumSteps(floors) + " steps");
    }

    private static int FindMinNumSteps(Floor[] floors)
    {
        Queue<int> elevatorQueue = new Queue<int>();
        elevatorQueue.Enqueue(0);
        Queue<Floor[]> stateQueue = new Queue<Floor[]>();
        stateQueue.Enqueue(floors);
        HashSet<string> visitedStates = new HashSet<string>();
        visitedStates.Add(GetState(floors, 0));
        bool allObjectsOnFourthFloor = false;
        int numSteps = -1;
        while(!allObjectsOnFourthFloor)
        {
            int numStatesInStep = elevatorQueue.Count;
            for (int i = 0; i < numStatesInStep; i++)
            {
                int elevatorFloor = elevatorQueue.Dequeue();
                Floor[] floorState = stateQueue.Dequeue();
                if (CheckForSolution(floorState))
                {
                    allObjectsOnFourthFloor = true;
                    break;
                }
                List<Tuple<List<string>, List<string>>> itemsOnElevator = GetItemsOnElevator(floorState[elevatorFloor]);
                foreach(Tuple<List<string>, List<string>> items in itemsOnElevator)
                {
                    if (elevatorFloor > 0 && !AreEmptyBelow(elevatorFloor, floorState))
                    {
                        Floor[] newState = MakeNewState(floorState);
                        foreach(string mc in items.Item1)
                            Floor.MoveItem(newState[elevatorFloor], newState[elevatorFloor - 1], mc, "microchip");
                        foreach(string rtg in items.Item2)
                            Floor.MoveItem(newState[elevatorFloor], newState[elevatorFloor - 1], rtg, "RTG");

                        if (IsStateSafe(newState) && !visitedStates.Contains(GetState(newState, elevatorFloor - 1)))
                        {
                            visitedStates.Add(GetState(newState, elevatorFloor - 1));
                            elevatorQueue.Enqueue(elevatorFloor - 1);
                            stateQueue.Enqueue(newState);
                        }
                    }
                    if (elevatorFloor < 3)
                    {
                        Floor[] newState = MakeNewState(floorState);
                        foreach (string mc in items.Item1)
                            Floor.MoveItem(newState[elevatorFloor], newState[elevatorFloor + 1], mc, "microchip");
                        foreach (string rtg in items.Item2)
                            Floor.MoveItem(newState[elevatorFloor], newState[elevatorFloor + 1], rtg, "RTG");

                        if (IsStateSafe(newState) && !visitedStates.Contains(GetState(newState, elevatorFloor + 1)))
                        {
                            visitedStates.Add(GetState(newState, elevatorFloor + 1));
                            elevatorQueue.Enqueue(elevatorFloor + 1);
                            stateQueue.Enqueue(newState);
                        }
                    }
                }
                
            }
            numSteps++; 
        }


        return numSteps;
    }

    private static string GetState(Floor[] floors, int elevator)
    {
        StringBuilder stateBuilder = new StringBuilder();
        for (int i = 0; i < floors.Length; i++)
        {
            stateBuilder.Append(i);
            stateBuilder.Append("mc");
            foreach (string mc in floors[i].microchipList)
                stateBuilder.Append(mc);
            stateBuilder.Append("rtg");
            foreach (string rtg in floors[i].rtgList)
                stateBuilder.Append(rtg);
        }
        stateBuilder.Append(elevator);
        return stateBuilder.ToString();
    }

    private static List<Tuple<List<string>, List<string>>> GetItemsOnElevator(Floor currentFloor)
    {
        List<Tuple<List<string>, List<string>>> x = new List<Tuple<List<string>, List<string>>>();
        for (int i = 0; i < currentFloor.microchipList.Count; i++)
        {
            List<string> y = new List<string>();
            y.Add(currentFloor.microchipList[i]);
            x.Add(new Tuple<List<string>, List<string>>(y, new List<string>()));
            for (int j = i + 1; j < currentFloor.microchipList.Count; j++)
            {
                List<string> z = new List<string>();
                z.Add(currentFloor.microchipList[i]);
                z.Add(currentFloor.microchipList[j]);
                x.Add(new Tuple<List<string>, List<string>>(z, new List<string>()));
            }
        }
        for (int i = 0; i < currentFloor.rtgList.Count; i++)
        {
            List<string> y = new List<string>();
            y.Add(currentFloor.rtgList[i]);
            x.Add(new Tuple<List<string>, List<string>>(new List<string>(), y));
            for (int j = i + 1; j < currentFloor.rtgList.Count; j++)
            {
                List<string> z = new List<string>();
                z.Add(currentFloor.rtgList[i]);
                z.Add(currentFloor.rtgList[j]);
                x.Add(new Tuple<List<string>, List<string>>(new List<string>(), z));
            }
        }

        foreach(string mc in currentFloor.microchipList)
        {
            foreach (string rtg in currentFloor.rtgList)
            {
                List<string> y = new List<string>();
                List<string> z = new List<string>();
                y.Add(mc);
                z.Add(rtg);
                x.Add(new Tuple<List<string>, List<string>>(y, z));
            }
        }

        return x;
    }

    private static Floor[] MakeNewState(Floor[] floors)
    {
        Floor[] newState = new Floor[floors.Length];
        for (int i = 0; i < floors.Length; i++)
        {
            List<string> mcList = new List<string>();
            foreach (string mc in floors[i].microchipList)
                mcList.Add(mc);
            List<string> rtgList = new List<string>();
            foreach (string rtg in floors[i].rtgList)
                rtgList.Add(rtg);
            newState[i] = new Floor(mcList, rtgList);
        }
        return newState;
    }

    private static bool IsStateSafe(Floor[] state)
    {
        foreach (Floor f in state)
        {
            foreach (string mc in f.microchipList)
                if (!f.rtgList.Contains(mc) && f.rtgList.Count > 0)
                    return false;
        }

        return true;
    }

    private static bool AreEmptyBelow(int elevator, Floor[] floors)
    {
        for (int i = 0; i < elevator; i++)
            if (floors[i].microchipList.Count > 0 || floors[i].rtgList.Count > 0)
                return false;

        return true;
    }

    private static bool CheckForSolution(Floor[] floors)
    {
        for (int i = 0; i < floors.Length - 1; i++)
        {
            //Check if any of the lower floors are not empty
            if (floors[i].microchipList.Count > 0 || floors[i].rtgList.Count > 0)
                return false;
        }

        //Check if the top floor is full
        Floor topFloor = floors[floors.Length - 1];
        if (topFloor.microchipList.Count < totalItemPairs || topFloor.rtgList.Count < totalItemPairs)
            return false;

        return true;
    }

}



