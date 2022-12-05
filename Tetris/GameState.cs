using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class GameState
    {
        private GameGrid _gameGrid;
        private Block _currentBlock;
        private Block _nextBlock;
        private int _score;
        private bool _isGameOver;
        private int _speed;
        private int _blockCounter;
        private int _rowsCleared;
        private int[] _highestScores;

        public GameGrid GameGrid => _gameGrid;
        public Block CurrentBlock => _currentBlock;
        public Block NextBlock => _nextBlock;
        public int Score => _score;
        public bool IsGameOver => _isGameOver;
        public int Speed => _speed;
        public int BlockCounter => _blockCounter;
        public bool IsHardDrop { set; get; }
        public bool PressedDown { set; get; }
        public int RowsCleared => _rowsCleared;
        public int Highscore => _highestScores[0];

        public GameState()
        {
            _gameGrid = new GameGrid(22, 10);
            _currentBlock = GetRandomBlock();
            _nextBlock = GetRandomBlock();
            _score = 0;
            _isGameOver = false;
            _speed = 500;
            _blockCounter = 0;
            _rowsCleared = 0;
            _highestScores = new int[10];

            try
            {
                ReadScoresFromFile();
            }
            catch { }
        }
        public Block GetRandomBlock()
        {
            Random rand = new Random();
            int id = rand.Next(7) + 1;

            return new Block(id);
        }
        public void MoveRight()
        {
            Element[] elements;
            bool canMove = true;

            _currentBlock.Move(Block.Direction.Right);
            elements = _currentBlock.GetBlock();

            foreach (Element element in elements)
                if (element.Column >= _gameGrid.Columns || _gameGrid.Cell(element.Row, element.Column) != 0) canMove = false;

            if (!canMove) _currentBlock.Move(Block.Direction.Left);
        }
        public void MoveLeft()
        {
            Element[] elements;
            bool canMove = true;

            _currentBlock.Move(Block.Direction.Left);
            elements = _currentBlock.GetBlock();

            foreach (Element element in elements)
                if (element.Column < 0 || _gameGrid.Cell(element.Row, element.Column) != 0) canMove = false;

            if (!canMove) _currentBlock.Move(Block.Direction.Right);
        }
        public bool MoveDown()
        {
            Element[] elements;
            bool canMove = true;

            _currentBlock.Move(Block.Direction.Down);
            elements = _currentBlock.GetBlock();

            foreach (Element element in elements)
            {
                if (element.Row >= _gameGrid.Rows) canMove = false;
                else if (_gameGrid.Cell(element.Row, element.Column) != 0) canMove = false;
            }
            if (!canMove)
            {
                _currentBlock.Move(Block.Direction.Up);

                _gameGrid.WriteBlockIntoGrid(CurrentBlock);

                int clearedRows = _gameGrid.ClearRows();
                AddScore(clearedRows);
                _rowsCleared += clearedRows;

                if (!_gameGrid.IsRowEmpty(1)) _isGameOver = true;

                _currentBlock = _nextBlock;
                _nextBlock = GetRandomBlock();
                _blockCounter++;

                AdjustSpeed();
            }
            else
            {
                AddScore(0);
                PressedDown = false;
            }

            return canMove;
        }

        private void AddScore(int RowsCleared)
        {
            if (_blockCounter > 0)
            {
                if (RowsCleared == 0 && PressedDown) _score += 1;
                if (RowsCleared == 0 && IsHardDrop) _score += 2;
                if (RowsCleared == 1) _score += 100;
                if (RowsCleared == 2) _score += 400;
                if (RowsCleared == 3) _score += 900;
                if (RowsCleared == 4) _score += 2000;
            }
            else if (_speed >= 100)
            {
                if (RowsCleared == 0 && PressedDown) _score += 2;
                if (RowsCleared == 0 && IsHardDrop) _score += 4;
                if (RowsCleared == 1) _score += 200;
                if (RowsCleared == 2) _score += 800;
                if (RowsCleared == 3) _score += 1800;
                if (RowsCleared == 4) _score += 4000;
            }
            else if (_speed >= 200)
            {
                if (RowsCleared == 0 && PressedDown) _score += 3;
                if (RowsCleared == 0 && IsHardDrop) _score += 6;
                if (RowsCleared == 1) _score += 300;
                if (RowsCleared == 2) _score += 1200;
                if (RowsCleared == 3) _score += 2700;
                if (RowsCleared == 4) _score += 6000;
            }
            else if (_speed >= 300)
            {
                if (RowsCleared == 0 && PressedDown) _score += 4;
                if (RowsCleared == 0 && IsHardDrop) _score += 8;
                if (RowsCleared == 1) _score += 400;
                if (RowsCleared == 2) _score += 1600;
                if (RowsCleared == 3) _score += 3600;
                if (RowsCleared == 4) _score += 8000;
            }
            else if (_speed >= 400)
            {
                if (RowsCleared == 0 && PressedDown) _score += 5;
                if (RowsCleared == 0 && IsHardDrop) _score += 10;
                if (RowsCleared == 1) _score += 500;
                if (RowsCleared == 2) _score += 2000;
                if (RowsCleared == 3) _score += 4500;
                if (RowsCleared == 4) _score += 10000;
            }
        }
        private void AdjustSpeed()
        {
            switch (_blockCounter)
            {
                case 30: _speed = 450; break;
                case 60: _speed = 400; break;
                case 100: _speed = 300; break;
                case 130: _speed = 250; break;
                case 160: _speed = 200; break;
                case 200: _speed = 500; break;
                case 220: _speed = 450; break;
                case 230: _speed = 400; break;
                case 240: _speed = 300; break;
                case 250: _speed = 200; break;
                case 260: _speed = 150; break;
                case 300: _speed = 100; break;
                case 330: _speed = 80; break;
                case 360: _speed = 60; break;
                case 400: _speed = 40; break;
                case 430: _speed = 60; break;
                case 460: _speed = 80; break;
                case 500: _speed = 20; break;

            }
        }
        public void Rotate()
        {
            Element[] elements;
            bool canRotate = true;

            _currentBlock.RotateCW();
            elements = _currentBlock.GetBlock();

            foreach (Element element in elements)
                if (element.Row > _gameGrid.Rows || element.Row < 0 || element.Column < 0 || element.Column > _gameGrid.Columns
                    || _gameGrid.Cell(element.Row, element.Column) != 0)
                    canRotate = false;

            if (!canRotate) _currentBlock.RotateCCW();
        }
        public void WriteScoresToFile()
        {
            string fileName = @".\Highscores.txt";

            if (Score > _highestScores[0]) _highestScores[0] = Score;

            if (File.Exists(fileName)) File.Delete(fileName);

            string[] highscores = new string[10];
            for (int i = 0; i < 10; i++)
                highscores[i] = _highestScores[i].ToString();
            File.WriteAllLines(fileName, highscores);

        }
        public void ReadScoresFromFile()
        {
            string fileName = @".\Highscores.txt";

            string[] highscores = new string[10];
            highscores = File.ReadAllLines(fileName);

            for (int i = 0; i < 10; i++)
                _highestScores[i] = Int32.Parse(highscores[i]);
        }
    }
}
