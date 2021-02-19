using CPE400Project.MapDisplay;
using System.Collections.Generic;
using CPE400Project.Exploration;
using System.Diagnostics;

namespace CPE400Project.Controller
{
    /// <summary>
    /// Class controller that will determine where the drones will travel,
    /// as well as what information will be displayed on the GUI
    /// </summary>
    public class ClassController
    {
        //IList that will store all drone's battery levels
        public IList<float> currentDroneBatteries { get; set; }

        public int MaxInstructionsRemaining
        {
            get
            {
                int ret = 0;
                foreach (var i in droneList)
                {
                    int moves = calculateBatteryUsage(i.Instructions);
                    if (moves > ret)
                    {
                        ret = moves;
                    }
                }
                return ret;
            }
        }

        public IList<Drone> droneList { get; set; }

        MapElement map { get; set; }

        //Constant running functions

        public ClassController(MapElement currentMap, IList<Drone> drones)
        {
            droneList = drones;
            map = currentMap;
        }

        
        //GENERAL UPDATE FUNCTION OF CONTROLLER
        //Function will update all drone properties as well as map properties
        public void ControllerUpdate()
        {
            foreach (var i in droneList)
            {
                i.update();
            }
                
        }

        //Function to calculate algorithm for where the drones should travel
        public void DetermineFlight()
        {

            int baseX = map.Map.HomeBase.XCenter;
            int baseY = map.Map.HomeBase.YCenter;

            int regionSize = (int)((map.Map.Width / droneList.Count) + 1);

            for (int i = 0; i < droneList.Count; i++)
            {
                int currentX = droneList[i].X;
                int currentY = droneList[i].Y;
                int battery = droneList[i].battery;
                int destX = i * regionSize;
                int destY = 5;

                int verticalStep = map.DroneVision;

                int moveDistance = (destX + regionSize < map.Map.Width) ? regionSize : (map.Map.Width - destX - 1);

                bool explored = false;
                bool firstRun = true;
                bool east = false;
                while (!explored)
                {
                    var list = mapTo(currentX, currentY, destX, destY);
                    foreach (var j in list)
                    {
                        droneList[i].Instructions.Add(j);
                    }

                    currentX = destX;
                    currentY = destY;
                    battery -= calculateBatteryUsage(list);



                    if (firstRun)
                    {
                        droneList[i].Instructions.Add(new Instruction(moveDistance, Directions.E));
                        battery -= moveDistance;
                        currentX += moveDistance;
                        firstRun = false;
                    }

                    
                    while (battery > calculateDistanceToHome(currentX, currentY) + (2 * moveDistance) + (2 * verticalStep) && currentY + verticalStep < map.Map.Height)
                    {
                        Directions direction = (east) ? Directions.E : Directions.W;
                        
                        droneList[i].Instructions.Add(new Instruction(verticalStep, Directions.N));
                        droneList[i].Instructions.Add(new Instruction(moveDistance, direction));
                        

                        if (east)
                        {
                            currentX += moveDistance;
                        }
                        else
                        {
                            currentX -= moveDistance;
                        }

                        east = !east;

                        battery -= verticalStep + moveDistance;
                        currentY += verticalStep;
                    }

                    destX = currentX;
                    destY = currentY;

                    if (currentY + verticalStep >= map.Map.Height)
                    {
                        explored = true;
                        list = mapTo(currentX, currentY, baseX, baseY);
                        foreach (var j in list)
                        {
                            droneList[i].Instructions.Add(j);
                        }
                        currentX = baseX;
                        currentY = baseY;
                        battery = droneList[i].battery;
                    }
                    //When battery needs to be recharged to continue
                    if (battery <= calculateDistanceToHome(currentX, currentY) + (2 * moveDistance) + (2 * verticalStep) && !explored)
                    {
                        list = mapTo(currentX, currentY, baseX, baseY);
                        foreach (var j in list)
                        {
                            droneList[i].Instructions.Add(j);
                        }
                        currentX = baseX;
                        currentY = baseY;
                        battery = droneList[i].battery;
                    }


                    
                }
            }

        }

        public int calculateDistanceToHome(int startX, int startY)
        {
            return calculateBatteryUsage(mapTo(startX, startY, map.Map.HomeBase.XCenter, map.Map.HomeBase.YCenter));
        }

        public int calculateBatteryUsage(IList<Instruction> instructions)
        {
            int total = 0;
            foreach (var i in  instructions)
            {
                total += i.NumUnits;
            }

            return total;
        }

        public IList<Instruction> mapTo(int srcX, int srcY, int targetX, int targetY)
        {

            IList<Instruction> instructions = new List<Instruction>();

            int currentX = srcX;
            int currentY = srcY;

            Instruction instruction = new Instruction();
            while (currentX != targetX || currentY != targetY)
            {
                Directions direction;
                if (currentX == targetX && currentY < targetY)
                {
                    direction = Directions.N;
                    currentY++;
                }
                else if (currentX < targetX && currentY < targetY)
                {
                    direction = Directions.NE;
                    currentX++;
                    currentY++;
                }
                else if (currentX < targetX && currentY == targetY)
                {
                    direction = Directions.E;
                    currentX++;
                }
                else if (currentX < targetX && currentY > targetY)
                {
                    direction = Directions.SE;
                    currentX++;
                    currentY--;
                }
                else if (currentX == targetX && currentY > targetY)
                {
                    direction = Directions.S;
                    currentY--;
                }
                else if (currentX > targetX && currentY > targetY)
                {
                    direction = Directions.SW;
                    currentY--;
                    currentX--;
                }
                else if (currentX > targetX && currentY == targetY)
                {
                    direction = Directions.W;
                    currentX--;
                }
                else
                {
                    direction = Directions.NW;
                    currentX--;
                    currentY++;
                }
                if (instruction.Direction != direction)
                {
                    if (instruction.Direction != 0)
                    {
                        instructions.Add(instruction);
                    }
                    instruction = new Instruction(1, direction);
                }
                else
                {
                    instruction.NumUnits++;
                }
            }

            instructions.Add(instruction);
            return instructions;

        }

        //Function that will call Dean's Map.updateView() function
        public void updateMap()
        {
            map.UpdateMap(droneList);
        }
    }
}
