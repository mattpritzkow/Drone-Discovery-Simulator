using CPE400Project.EnvironmentData;
using CPE400Project.Exploration;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CPE400Project.MapDisplay
{
    /// <summary>
    /// Creates a display for the drones and current map.
    /// Non-discovered regions will be shaded gray while discoverd will show elevation
    /// </summary>
    public class MapElement : UserControl
    {
        #region Constructors
        public MapElement()
        {
            //Create new stackpanel to place objects in. This way it's easier to manage what's in there.
            //Adding a buggon or something for debuggin is easier this way.
            ParentContainer = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            MapImagePane = new Image();
            ParentContainer.Children.Add(MapImagePane);



            //Make this element's content that of the parent canvas.
            //this means the user will see the Parent Canvas.
            Content = ParentContainer;

            DefaultStyleKeyProperty.OverrideMetadata(typeof(MapElement), new FrameworkPropertyMetadata(typeof(MapElement)));

        }

        #endregion Constructors

        #region Properties
        /// <summary>
        /// Main Container for image. All of the elements will be children of this.
        /// Stack panel gives easy additions and customizability.
        /// </summary>
        public StackPanel ParentContainer { get; set; }
        public IList<Drone> Drones { get; set; }
        /// <summary>
        /// This will hold the 2D image. the Writeablebitmap needs to be contained within this.
        /// </summary>
        public Image MapImagePane { get; set; }
        /// <summary>
        /// This will be the 2d image. Writeable bitmap allows for editing of small regions - something that will need to happen often.
        /// </summary>
        public WriteableBitmap MapImage { get; set; }
        
        /// <summary>
        /// This is the radius a drone can see in pixels. Default will 10 Pixels
        /// </summary>
        public int DroneVision { get; set; }

        /// <summary>
        /// This is the width of the map multiplied by the width in bytes of the RGB scale
        /// </summary>
        public int RawStride { get; set; }

        #endregion Properties

        #region Dependency Properties
        /// <summary>
        /// This is the map itself - user must allocate one to their desired size and pass it in.
        /// When this value is updated, the map will re-draw.
        /// </summary>
        public static readonly DependencyProperty MapProperty = DependencyProperty.Register("Map", typeof(Map), typeof(MapElement));
        public Map Map
        {
            get
            { 
                return (Map)GetValue(MapProperty);
            }
            set     
            {
                SetValue(MapProperty, value);
                DrawMap();
                MarkRegionExplored(Map.HomeBase.XCenter, Map.HomeBase.YCenter);
            }
        }





        #endregion Dependency Properties

        #region Public Functions

        public void MarkDrone(int x, int y)
        {

            const int droneRadius = 2;

            int maxX = (x + droneRadius < Map.Width) ? x + droneRadius : Map.Width;
            int minX = (x - droneRadius > 0) ? x - droneRadius : 0;
            int maxY = (y + droneRadius < Map.Height) ? y + droneRadius : Map.Height;
            int minY = (y - droneRadius > 0) ? y - droneRadius : 0;

            int drawWidth = maxX - minX;
            int drawHeight = maxY - minY;
            int localStride = 4 * drawWidth;
            byte[] editArea = new byte[localStride * drawHeight];

            for (int i = 0; i < editArea.Length; i++)
            {
                editArea[i] = 000;
            }

            MapImage.WritePixels(
                    new Int32Rect(minX, minY, drawWidth, drawHeight),
                    editArea,
                    localStride,
                    0
                    );

            MapImagePane.Source = MapImage;
        }

        /// <summary>
        /// This will mark a region explored in a radius surrounding the centerpoint: x, y
        /// </summary>
        /// <param name="x">Position in the map on the x - coordinate to update</param>
        /// <param name="y">Position in the map on the y - coordinate to update</param>
        public void MarkRegionExplored(int x, int y)
        {
            //First mark a sphere as explored
            for (int i = 0; i < DroneVision; i++)
            {
                for (double j = 0; j < 360; j += 0.1)
                {
                    int xMod = (int)(Math.Cos(j * Math.PI / 180) * i);
                    int yMod = (int)(Math.Sin(j * Math.PI / 180) * i);


                    if (xMod + x >= 0 && xMod + x <= Map.Width
                        && yMod + y >= 0 && yMod + y <= Map.Height)
                    {
                        Map[xMod + x, yMod + y].Explored = true;
                    }
                }

            }

            
            int maxX = (x + DroneVision < Map.Width) ? x + DroneVision : Map.Width;
            int minX = (x - DroneVision > 0) ? x - DroneVision : 0;
            int maxY = (y + DroneVision < Map.Height) ? y + DroneVision : Map.Height;
            int minY = (y - DroneVision > 0) ? y - DroneVision : 0;

            int drawWidth = maxX - minX;
            int drawHeight = maxY - minY;
            int localStride = 4 * drawWidth;
            byte[] editArea = new byte[localStride * drawHeight];



            for (int i = 0; i < drawHeight; i++)
            {
                for (int j = 0; j < localStride; j += 4)
                {

                    //at each point - we need to see if it is discovered in the actual map. 
                    //first we need to define where we are globally
                    int actualJ = (j / 4) + (minX);
                    int actualI = i + (minY);

                    if (actualJ >= 0 && actualI >= 0)
                    {



                        int editAreaIndex = (i * localStride) + j;

                        if (Map[actualJ, actualI].HomeBase)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                editArea[editAreaIndex + k] = ColorScale.BiomeColors[12, k];
                            }
                        }
                        else if (Map[actualJ, actualI].Explored)
                        {

                            int biomeIndex = (int)(10 * Map[actualJ, actualI].Elevation);


                            for (int k = 0; k < 4; k++)
                            {
                                editArea[editAreaIndex + k] = ColorScale.BiomeColors[biomeIndex, k];
                            }
                        }

                        else
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                editArea[editAreaIndex + k] = ColorScale.BiomeColors[11, k];
                            }
                        }
                    }
                }
            }

            MapImage.WritePixels(
                    new Int32Rect(minX, minY, drawWidth, drawHeight),
                    editArea,
                    localStride,
                    0
                    );

            MapImagePane.Source = MapImage;
           
        }

        /// <summary>
        /// Code to draw and place the map.
        /// Will first define basic info for drawing
        /// Then creates a byte array to define colors at each point in bitmap
        /// Then the bitmap will be placed on screen.
        /// </summary>
        public void DrawMap()
        {

            //First define parameters.
            ParentContainer.Width = Map.Width;
            ParentContainer.Height = Map.Height;

            PixelFormat pf = PixelFormats.Bgr32;

            RawStride = (Map.Width * pf.BitsPerPixel + 7) / 8;
            byte[] rawImage = new byte[RawStride * Map.Height];


            //Next use those parameters to populate the array and define each pixel's colour.
            for (int i = 0; i < Map.Height; i++)
            {
                for (int j = 0; j < RawStride; j += 4)
                {
                    int index = (i * RawStride) + j;
                    int actualJ = j / 4;

                    if (Map[actualJ, i].HomeBase)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            rawImage[index + k] = ColorScale.BiomeColors[12, k];
                        }
                    }

                    else if (Map[actualJ, i].Explored)
                    {

                        int BiomeIndex = (int)(10 * Map[i, actualJ].Elevation);


                        for (int k = 0; k < 4; k++)
                        {
                            rawImage[index + k] = ColorScale.BiomeColors[BiomeIndex, k];
                        }
                    }

                    else
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            rawImage[index + k] = ColorScale.BiomeColors[11, k];
                        }
                    }
                }
            }

            //Finally generate and apply bitmap image.
            BitmapSource baseMap;

            MapImagePane.Width = Map.Width;
            MapImagePane.Height = Map.Height;
            
            baseMap = BitmapSource.Create(Map.Width, Map.Height, 96, 96, pf, null, rawImage, RawStride);

            MapImage = new WriteableBitmap(baseMap);

            MapImagePane.Source = MapImage;

        }
            
        public void UpdateMap(IList<Drone> drones)
        {
            Drones = drones;
            foreach ( var i in Drones )
            {
                MarkRegionExplored(i.X, i.Y);
                
            }
            foreach (var i in Drones)
            {
                MarkDrone(i.X, i.Y);
            }
        }
        

        #endregion Public Functions

    }
}
