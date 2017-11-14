using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class GameBoard
    {
        List<int[,]> TetrisBoardList = new List<int[,]>();
        const int pixelWidth = 32;
        const int BoardWidth = 426;   //X,Y  position of the gameboard in the window
        const int BoardHeight = 200;
        const int boundsX = 500;
        public GameBoard()
        {
            int[,] TetrisBoard = new int[10, 18];
            TetrisBoardList.Add(TetrisBoard);
        }

        public enum BlockStates
        {
            Blocked,
            OffGrid,
            CanSet
        }
        int leftMostPx = 999;


        public BlockStates CheckPlacement(int[,] gameboard, int[,] block, int x, int y)
        {
            int blockDim = block.GetLength(0);
           // Console.WriteLine("X,Y: {0},{1}", x, y); 

            for (int px = 0; px < blockDim; px++)
            {
                for (int py = 0; py < blockDim; py++)
                {

                    int boardX = px + x;
                    int boardY = py + y;

                    //Check if space is empty


                    if (block[px, py] != 0)
                    {

                       // if (boardX < leftMostPx)
                       // {
                           // Console.WriteLine("X,boundsX: {0},{1}", boardX, boundsX);

                            //leftMostPx = boardX;
                            if (boardX <= BoardWidth || boardX >= boundsX)
                                return BlockStates.OffGrid;
                            else if (boardY >= BoardHeight || block[px, py] != 0)
                               return BlockStates.Blocked;
                        //}
                    }

                }
            }

            return BlockStates.CanSet;
        }

        public List<int[,]> GetGameBoard()
        {

            return TetrisBoardList;
        }
    }
}
