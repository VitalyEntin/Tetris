using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class GameGrid
    {
        private readonly int _rows;
        private readonly int _columns;
        private int[,] cells;

        public int Rows => _rows;
        public int Columns => _columns;
        public int Cell(int row, int column) => cells[row, column];
        public GameGrid(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            cells = new int[rows, columns];
        }

        public void WriteBlockIntoGrid(Block block)
        {
            foreach (Element e in block.GetBlock())
            {
                cells[e.Row, e.Column] = e.Id;
            }
        }
        public bool IsRowFull(int row)
        {
            for (int i = 0; i < Columns; i++)
                if (cells[row, i] == 0) return false;
            return true;
        }
        public bool IsRowEmpty(int row)
        {
            for (int i = 0; i < Columns; i++)
                if (cells[row, i] > 0) return false;
            return true;
        }
        public int ClearRows()
        {
            int rowsCleared = 0;

            for (int i = Rows - 1; i >= 0; i--)
            {
                if (IsRowEmpty(i)) break;
                if (IsRowFull(i) && i > 0)
                {
                    rowsCleared++;
                    for (int j = i - 1; j >= 0; j--)
                        for (int k = 0; k < Columns; k++)
                            cells[j + 1, k] = cells[j, k];
                    i++;
                }
            }
            return rowsCleared;
        }

    }
}
