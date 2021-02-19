using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE400Project.EnvironmentData
{
    /// <summary>
    /// Represents a pixel area in the map. Will be contained in a 2d Array within the map. 
    /// Contains status info for the particular chunk, i.e. exlplored, elevation, homeBase, and any other things that may need to be included
    /// </summary>
    public class Chunk
    {
        #region Constructors

        public Chunk(float elevation)
        {
            Elevation = elevation;
            HomeBase = false;
            Explored = false;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Value from 0 to 1, incremented by .1 ranges. This is used to define elevation of each pixel in map.
        /// </summary>
        public float Elevation { get; set;  }

        /// <summary>
        /// Defines wether or not the location has been mapped by drone.
        /// True = Has been seen and will display by elevation
        /// False = Has not been seen, and will display black
        /// </summary>
        public bool Explored { get; set; }

        /// <summary>
        /// Defines if the home base is located in this chunk. The base will be a 3x3 region.
        /// </summary>
        public bool HomeBase { get; set; }

        #endregion Properties

        #region Public Functions

        

        #endregion Public Functions
    }
}
