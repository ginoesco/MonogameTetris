using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class GameBoard
    {
        List<int[,]> TetrisBoardList = new List<int[,]>();
        const int pixelWidth = 32;
        const int pixelLength = 32; 
        const int BoardWidth = 330;   //X,Y  position of the gameboard in the window
        const int BoardHeight = 200;
        const int boundsX = BoardWidth+pixelWidth*9;
        const int boundsY = BoardHeight + pixelWidth * 17;
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

                    int boardX = px*pixelWidth + x;
                    int boardY = py*pixelLength + y;
                   // Console.WriteLine("boardY {0}, boundsY {1}, py {2}", boardY, boundsY,py);
                    //Check if space is empty


                    if (block[py, px] != 0)
                    {
                        if (boardX <= BoardWidth || boardX >= boundsX)
                            return BlockStates.OffGrid;
                        if (boardY >= boundsY || gameboard[px, py] != 0)
                            return BlockStates.Blocked;
                        
                    }

                }
            }

            return BlockStates.CanSet;
        }

        public int[,] LoadBoard(int[,] gameboard, int[,] block, int x, int y)
        {
            int[,] loadboard = new int[10, 18];
            int boardX, boardY; 
            Array.Copy(gameboard, loadboard, gameboard.Length);
            int length = loadboard.GetLength(0);
            int width = loadboard.GetLength(1);
            for (int i = 0; i<block.GetLength(0); i++)
            {
                for(int k = 0; k<block.GetLength(0); k++)
                {
                    if(block[k,i] != 0)
                    {
                        for(int px = 0; px<width; px++)
                        {
                            for(int py = 0; py<length; py++)
                            {
                                boardX = 330 + px * pixelWidth;
                                boardY = 200 + py * pixelLength;

                                if(boardX == x && boardY == y)
                                {
                                    loadboard[px, py] = 1; 
                                }
                            }
                        }
                        
                    }
                }
            }
            
            return loadboard; 
        }

        public void UpdateBoard(int[,]gameboard, Texture2D block, SpriteBatch spriteBatch)
        {
            for(int i = 0; i<gameboard.GetLength(0);i++)
            {
                for(int k = 0; k<gameboard.GetLength(1); k++)
                {
                    if(gameboard[i,k] != 0)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(block, new Vector2(BoardWidth + i * pixelWidth, BoardHeight + k * pixelLength), Color.Red); 
                        spriteBatch.End();
                    }
                }
            }
        }
        public List<int[,]> GetGameBoard()
        {

            return TetrisBoardList;
        }
    }
}
