using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE400Project.EnvironmentData
{
    /// <summary>
    /// Struct used to define the colours in map display.
    /// </summary>
    public struct ColorScale
    {
        public static byte[,] BiomeColors
        {
            get
            {
                return new byte[,]
                {
                    //Color format is:
                    //B, G, R, Null
                    { 188, 99, 64, 0 }, //Water, .0
                    { 241, 131, 94, 0 }, //Beach, .1
                    { 103, 166, 116, 0 }, //Forest, .2 
                    { 99, 124, 65, 0 }, //Jungle, .3
                    { 99, 124, 65, 0 }, //Jungle, .4
                    { 128, 186, 164, 0 }, //Savannah, .5
                    { 128, 186, 164, 0 }, //Savannah, .6
                    { 175, 208, 190, 0 }, //Desert, .7
                    { 175, 208, 190, 0 }, //Desert, .8
                    { 213, 208, 240, 0 }, //Snow, .9
                    { 213, 208, 240, 0 }, //Snow, 1
                    { 000, 000, 000, 0 }, //Unexplored 11
                    { 0, 0, 255, 0 }, //home base 12
                };
            }
        }
    }
}
