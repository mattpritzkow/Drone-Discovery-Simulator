using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPE400Project.Exploration;

namespace CPE400Project.Exploration
{
	public class Drone
	{

		//set all coordinates as float?
		//NEED TO GET WINDOW SIZE SOMEHOW
		public int battery { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public int HomeX { get; set; }
		public int HomeY { get; set; }
		public int maxBattery { get; set; }
		public IList<Instruction> Instructions { get; set; }


		/// <summary>
		/// Reduce the battery amount every second by 1, starts at 100
		/// </summary>
		public bool update()
		{
			
			if (X == HomeX && Y == HomeY)
			{
				battery = maxBattery;
			}
			if (battery > 0)
			{
				battery--;
				return executeInstruction();
				
			}
			return false;
		}


		public bool executeInstruction(){
			

			if(Instructions.Count != 0)
			{

				
				
				switch(Instructions[0].Direction)
				{
					case Directions.N:
						Y += 1;
						break;
					case Directions.NE:
						Y += 1;
						X += 1;
						break;
					case Directions.E:
						X += 1;
						break;
					case Directions.SE:
						Y -= 1;
						X += 1;
						break;
					case Directions.S:
						Y -= 1;
						break;
					case Directions.SW:
						Y -= 1;
						X -= 1;
						break;
					case Directions.W:
						X -= 1;
						break;
					case Directions.NW:
						Y += 1;
						X -= 1;
						break;
					default:
						break;
				
				}
				Instructions[0].NumUnits--;

				if (Instructions[0].NumUnits <= 0)
				{
					Instructions.RemoveAt(0);
				}

				return true;

				

			}
			return false;


		}

		/// <summary>
		/// Constructor of the drone class
		/// </summary>
		public Drone(int x, int y, int batteryLife, int homeX, int homeY)
		{
			battery = batteryLife;
			maxBattery = batteryLife;
			X = x;
			Y = y;
			HomeX = homeX;
			HomeY = homeY;
			Instructions = new List<Instruction>();

		}
	}
}