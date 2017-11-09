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

        public GameBoard()
        {
            int[,] TetrisBoard = new int[10, 18];
            TetrisBoardList.Add(TetrisBoard);
        }

        public List<int[,]> GetGameBoard()
        {
            
            return TetrisBoardList; 
        }
    }
}
