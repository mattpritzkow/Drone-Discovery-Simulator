using CPE400Project.DataGeneration;
using CPE400Project.EnvironmentData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE400Project.EnvironmentData
{
    public class Map
    {
        #region Constructors

        public Map(int height, int width)
        {
            GenerateMap(height, width);
        }


        #endregion Constructors

        #region Properties

        /// <summary>
        /// 2D List of data in class. 
        /// </summary>
        public Chunk[,] Chunks { get; set; }


        /// <summary>
        /// Width of the image in pixels
        /// </summary>
        public int Width
        {
            get { return Chunks.GetLength(0) - 1; }
        }

        /// <summary>
        /// Height of the image in pixels
        /// </summary>
        public int Height
        {
            get { return Chunks.GetLength(1) - 1; }
        }

        public Region HomeBase { get; set; }

        #endregion Properties

        #region Public Functions

        /// <summary>
        /// Randomly generates the map to specifications. Uses noise as generation method.
        /// </summary>
        /// <param name="height">Height in pixels for map</param>
        /// <param name="width">Width in pixels for map</param>
        public void GenerateMap(int height, int width)
        {
            //Create the Terrain
            Random random = new Random();
            TerrainGeneration.Seed = random.Next();
            Chunks = TerrainGeneration.GenerateElevationProfile(width, height);

            //Next, define the home base
            int baseOrigin;
            baseOrigin = random.Next() % (width - 7);

            HomeBase = new Region(baseOrigin, baseOrigin + 6, 0, 6);

            for (int i = baseOrigin; i < baseOrigin + 7; i++)
            {
                for (int j = 0; j <  7; j++)
                {
                    this[i, j].HomeBase = true;
                }
            }
            


        }

        #endregion Public Fucntions

        #region Overides
        /// <summary>
        /// Indexing method for map class - will return reference to internal chunk array.
        /// Can access data like: [x][y] with this.
        /// </summary>
        /// <param name="key">Index of X coordinate to access.</param>
        /// <returns></returns>
        public Chunk this[int x, int y]
        {
            get
            {
                return Chunks[x, y];
            }
            set
            {
                if (x > Width || y > Height)
                {
                    throw new IndexOutOfRangeException("Cannot index outside of range of list.");
                }
                Chunks[x, y] = value;
            }
        }

        #endregion Overides
    }
}
