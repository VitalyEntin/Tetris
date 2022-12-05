using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Block
    {
        private int _offsetRow;
        private int _offsetCol;
        private int _id;
        private int _rotation;

        public enum Direction { Up, Right, Down, Left };
        public int OffsetRow => _offsetRow;
        public int OffsetCol => _offsetCol;
        public int Id => _id;
        public int Rotation => _rotation;

        public Block(int id)
        {
            _id = id;
            _offsetRow = 0;
            _offsetCol = 3;
            _rotation = 0;
        }
        public void RotateCW()
        {
            _rotation++;
            _rotation %= 4;
        }
        public void RotateCCW()
        {
            _rotation--;
            _rotation %= 4;
        }
        public void Move(Direction dir)
        {
            if (dir == Direction.Down) _offsetRow++;
            if (dir == Direction.Up) _offsetRow--;
            if (dir == Direction.Left) _offsetCol--;
            if (dir == Direction.Right) _offsetCol++;
        }
        public Element[] GetBlock()
        {
            Element[] elements = new Element[4];

            if (_id == 1)
            {
                if (_rotation == 0)
                {
                    elements[0] = new Element(_offsetRow + 1, _offsetCol, _id);
                    elements[1] = new Element(_offsetRow + 1, _offsetCol + 1, _id);
                    elements[2] = new Element(_offsetRow + 1, _offsetCol + 2, _id);
                    elements[3] = new Element(_offsetRow + 1, _offsetCol + 3, _id);
                }
                if (_rotation == 1)
                {
                    elements[0] = new Element(_offsetRow, _offsetCol + 2, _id);
                    elements[1] = new Element(_offsetRow + 1, _offsetCol + 2, _id);
                    elements[2] = new Element(_offsetRow + 2, _offsetCol + 2, _id);
                    elements[3] = new Element(_offsetRow + 3, _offsetCol + 2, _id);
                }
                if (_rotation == 2)
                {
                    elements[0] = new Element(_offsetRow + 2, _offsetCol, _id);
                    elements[1] = new Element(_offsetRow + 2, _offsetCol + 1, _id);
                    elements[2] = new Element(_offsetRow + 2, _offsetCol + 2, _id);
                    elements[3] = new Element(_offsetRow + 2, _offsetCol + 3, _id);
                }
                if (_rotation == 3)
                {
                    elements[0] = new Element(_offsetRow, _offsetCol + 1, _id);
                    elements[1] = new Element(_offsetRow + 1, _offsetCol + 1, _id);
                    elements[2] = new Element(_offsetRow + 2, _offsetCol + 1, _id);
                    elements[3] = new Element(_offsetRow + 3, _offsetCol + 1, _id);
                }
            }
            if (_id == 2)
            {
                if (_rotation == 0)
                {
                    elements[0] = new Element(_offsetRow, _offsetCol, _id);
                    elements[1] = new Element(_offsetRow + 1, _offsetCol, _id);
                    elements[2] = new Element(_offsetRow + 1, _offsetCol + 1, _id);
                    elements[3] = new Element(_offsetRow + 1, _offsetCol + 2, _id);
                }
                if (_rotation == 1)
                {
                    elements[0] = new Element(_offsetRow, _offsetCol + 2, _id);
                    elements[1] = new Element(_offsetRow, _offsetCol + 1, _id);
                    elements[2] = new Element(_offsetRow + 1, _offsetCol + 1, _id);
                    elements[3] = new Element(_offsetRow + 2, _offsetCol + 1, _id);
                }
                if (_rotation == 2)
                {
                    elements[0] = new Element(_offsetRow + 2, _offsetCol + 2, _id);
                    elements[1] = new Element(_offsetRow + 1, _offsetCol + 2, _id);
                    elements[2] = new Element(_offsetRow + 1, _offsetCol + 1, _id);
                    elements[3] = new Element(_offsetRow + 1, _offsetCol, _id);
                }
                if (_rotation == 3)
                {
                    elements[0] = new Element(_offsetRow + 2, _offsetCol, _id );
                    elements[1] = new Element(_offsetRow + 2, _offsetCol + 1, _id );
                    elements[2] = new Element(_offsetRow + 1, _offsetCol + 1, _id );
                    elements[3] = new Element(_offsetRow, _offsetCol + 1, _id );
                }
            }
            if (_id == 3)
            {
                if (_rotation == 0)
                {
                    elements[0] = new Element(_offsetRow + 1, _offsetCol, _id);
                    elements[1] = new Element(_offsetRow + 1, _offsetCol + 1, _id);
                    elements[2] = new Element(_offsetRow + 1, _offsetCol + 2, _id);
                    elements[3] = new Element(_offsetRow, _offsetCol + 2, _id);
                }
                if (_rotation == 1)
                {
                    elements[0] = new Element(_offsetRow, _offsetCol + 1, _id);
                    elements[1] = new Element(_offsetRow + 1, _offsetCol + 1, _id);
                    elements[2] = new Element(_offsetRow + 2, _offsetCol + 1, _id);
                    elements[3] = new Element(_offsetRow + 2, _offsetCol + 2, _id);
                }
                if (_rotation == 2)
                {
                    elements[0] = new Element(_offsetRow + 1, _offsetCol + 2, _id);
                    elements[1] = new Element(_offsetRow + 1, _offsetCol + 1, _id);
                    elements[2] = new Element(_offsetRow + 1, _offsetCol, _id);
                    elements[3] = new Element(_offsetRow + 2, _offsetCol, _id);
                }
                if (_rotation == 3)
                {
                    elements[0] = new Element(_offsetRow, _offsetCol, _id );
                    elements[1] = new Element(_offsetRow, _offsetCol + 1, _id );
                    elements[2] = new Element(_offsetRow + 1, _offsetCol + 1, _id );
                    elements[3] = new Element(_offsetRow + 2, _offsetCol + 1, _id );
                }
            }
            if (_id == 4)
            {

                elements[0] = new Element(_offsetRow  , _offsetCol, _id);
                elements[1] = new Element(_offsetRow , _offsetCol + 1, _id);
                elements[2] = new Element(_offsetRow + 1, _offsetCol , _id);
                elements[3] = new Element(_offsetRow+1, _offsetCol +1, _id);
            }
            if (_id == 5)
            {
                if (_rotation == 0 || _rotation==2)
                {
                    elements[0] = new Element(_offsetRow + 1, _offsetCol, _id);
                    elements[1] = new Element(_offsetRow + 1, _offsetCol + 1, _id);
                    elements[2] = new Element(_offsetRow , _offsetCol + 1, _id);
                    elements[3] = new Element(_offsetRow, _offsetCol + 2, _id);
                }
                if (_rotation == 1|| _rotation==3)
                {
                    elements[0] = new Element(_offsetRow, _offsetCol + 1, _id);
                    elements[1] = new Element(_offsetRow + 1, _offsetCol + 1, _id);
                    elements[2] = new Element(_offsetRow + 1, _offsetCol + 2, _id);
                    elements[3] = new Element(_offsetRow + 2, _offsetCol + 2, _id);
                }
            }
            if (_id == 6)
            {
                if (_rotation == 0)
                {
                    elements[0] = new Element(_offsetRow + 1, _offsetCol, _id);
                    elements[1] = new Element(_offsetRow + 1, _offsetCol + 1, _id);
                    elements[2] = new Element(_offsetRow + 1, _offsetCol + 2, _id);
                    elements[3] = new Element(_offsetRow, _offsetCol + 1, _id);
                }
                if (_rotation == 1)
                {
                    elements[0] = new Element(_offsetRow, _offsetCol + 1, _id);
                    elements[1] = new Element(_offsetRow + 1, _offsetCol + 1, _id);
                    elements[2] = new Element(_offsetRow + 2, _offsetCol + 1, _id);
                    elements[3] = new Element(_offsetRow + 1, _offsetCol + 2, _id);
                }
                if (_rotation == 2)
                {
                    elements[0] = new Element(_offsetRow + 1, _offsetCol + 2, _id);
                    elements[1] = new Element(_offsetRow + 1, _offsetCol + 1, _id);
                    elements[2] = new Element(_offsetRow + 1, _offsetCol, _id);
                    elements[3] = new Element(_offsetRow + 2, _offsetCol+1, _id);
                }
                if (_rotation == 3)
                {
                    elements[0] = new Element(_offsetRow, _offsetCol+1, _id );
                    elements[1] = new Element(_offsetRow+1, _offsetCol + 1, _id );
                    elements[2] = new Element(_offsetRow + 2, _offsetCol + 1, _id );
                    elements[3] = new Element(_offsetRow + 1, _offsetCol , _id );
                }
            }
            if (_id == 7)
            {
                if (_rotation == 0 || _rotation==2)
                {
                    elements[0] = new Element(_offsetRow , _offsetCol, _id);
                    elements[1] = new Element(_offsetRow , _offsetCol+1 , _id);
                    elements[2] = new Element(_offsetRow + 1, _offsetCol + 1, _id);
                    elements[3] = new Element(_offsetRow+1, _offsetCol + 2, _id);
                }
                if (_rotation == 1 || _rotation==3)
                {
                    elements[0] = new Element(_offsetRow, _offsetCol + 2, _id);
                    elements[1] = new Element(_offsetRow + 1, _offsetCol + 2, _id);
                    elements[2] = new Element(_offsetRow + 1, _offsetCol + 1, _id);
                    elements[3] = new Element(_offsetRow + 2, _offsetCol + 1, _id);
                }

            }
            return elements;
        }
    }

    public class Element
    {
        public int Id { get; set; }
        public int Row { set; get; }
        public int Column { set; get; }

        public Element(int row, int column, int id)
        {
            Id = id;
            Row = row;
            Column = column;
        }
    }
}
