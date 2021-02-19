using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE400Project.Exploration
{
    public class Instruction
    {
        public Directions Direction { get; set; }
        public int NumUnits { get; set; }

         public Instruction(int numUnits, Directions direction)
        {
            Direction = direction;
            NumUnits = numUnits;
        }

        public Instruction()
        {
            NumUnits = -1;
        }
    }
}
