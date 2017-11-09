using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    class Shapes
    {
        List<int[,]> ShapeList = new List<int[,]>();
        List<Color> ColorList = new List<Color>(); 
        const int pixelWidth = 31;
        const int pixelLength = 32; 

        public Shapes()
        {
            ColorList.Add(Color.DarkOrange);
            ColorList.Add(Color.Cyan);
            ColorList.Add(Color.Yellow);
            ColorList.Add(Color.Blue);
            ColorList.Add(Color.MediumVioletRed);
            ColorList.Add(Color.LightGreen);
            ColorList.Add(Color.Purple);


            //T shape
            ShapeList.Add(new int[4, 4]{{0,1,0,0},
                                        {1,1,1,0},
                                        {0,0,0,0},
                                        {0,0,0,0} });
            //Z shape
            ShapeList.Add(new int[4, 4]{{1,1,0,0},
                                        {0,1,1,0},
                                        {0,0,0,0},
                                        {0,0,0,0} });

            //S shape
            ShapeList.Add(new int[4, 4]{{0,0,1,1},
                                        {0,1,1,0},
                                        {0,0,0,0},
                                        {0,0,0,0} });
            //L Shape
            ShapeList.Add(new int[4, 4]{{0,1,0,0},
                                        {0,1,0,0},
                                        {0,1,1,0},
                                        {0,0,0,0} });

            //J Shape
            ShapeList.Add(new int[4, 4]{{0,0,0,0},
                                        {1,0,0,0},
                                        {1,1,1,0},
                                        {0,0,0,0} });
            //Square Shape
            ShapeList.Add(new int[4, 4]{{0,0,0,0},
                                        {0,1,1,0},
                                        {0,1,1,0},
                                        {0,0,0,0} });
            //Line Shape
            ShapeList.Add(new int[4, 4]{{0,1,0,0},
                                        {0,1,0,0},
                                        {0,1,0,0},
                                        {0,1,0,0} });
        }

        public List<int[,]> GetShapeList()
        {
            return ShapeList;
        }

        public List<Color> GetColorList()
        {
            return ColorList; 
        }
    }
}
