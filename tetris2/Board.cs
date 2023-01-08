using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace tetris
{
    public class Board
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int Score { get; set; }
        public int LinesFilled { get; set; }
        private Tetris currTetris;
        private Label[,] BlockControls;

        private Brush NoBrush = Brushes.Transparent;
        private Brush SilverBrush = Brushes.Black;

        public Board(Grid TetrisGrid)
        {
            Rows = TetrisGrid.RowDefinitions.Count;
            Cols = TetrisGrid.ColumnDefinitions.Count;
            Score = 0;
            LinesFilled = 0;

            BlockControls = new Label[Cols, Rows];
            for (int i = 0; i < Cols; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    BlockControls[i, j] = new Label();
                    BlockControls[i, j].Background = NoBrush;
                    BlockControls[i, j].BorderBrush = SilverBrush;
                    BlockControls[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
                    Grid.SetRow(BlockControls[i, j], j);
                    Grid.SetColumn(BlockControls[i, j], i);
                    TetrisGrid.Children.Add(BlockControls[i, j]);
                }
            }
            currTetris = new Tetris();
            currTetrisDraw();
        }

        public void currTetrisDraw()
        {
            Point Position = currTetris.CurrPosition;
            Point[] Shape = currTetris.CurrShape;
            Brush Color = currTetris.CurrColor;

            foreach (Point S in Shape)
            {
                BlockControls[(int)(S.X + Position.X) + ((Cols / 2) - 1),
                (int)(S.Y + Position.Y) + 1].Background = Color;
            }
        }

        public void currTetrisErase()
        {
            Point Position = currTetris.CurrPosition;
            Point[] Shape = currTetris.CurrShape;
            foreach (Point S in Shape)
            {
                BlockControls[(int)(S.X + Position.X) + ((Cols / 2) - 1),
                    (int)(S.Y + Position.Y) + 1].Background = NoBrush;
            }
        }

        private void checkRows()
        {
            bool full;
            for (int i = Rows - 1; i >= 0; i--)
            {
                full = true;
                for (int j = 0; j < Cols; j++)
                {
                    if (BlockControls[j, i].Background == NoBrush)
                        full = false;
                }
                if (full)
                {                    
                    RemoveRow(i);
                    i++;
                    Score += 100;
                    LinesFilled += 1;
                }
            }
        }

        private void RemoveRow(int row)
        {
            for (int i = row; i > 2; i--)
            {
                for (int j = 0; j < Cols; j++)
                {
                    BlockControls[j, i].Background = BlockControls[j, i - 1].Background;
                }
            }
        }

        public bool checkLastRow()
        {            
            int row = 0;
            for (int j = 4; j < 8; j++)
            {
                if (BlockControls[j, row].Background != NoBrush)
                    return true;
            }            
            return false;
        }

        public void CurrTetrisMoveLeft()
        {
            Point Position = currTetris.CurrPosition;
            Point[] Shape = currTetris.CurrShape;
            currTetrisErase();
            bool move = true;
            foreach (Point S in Shape)
            {
                if (((int)(S.X + Position.X) + ((Cols / 2) - 1) - 1) < 0)
                    move = false;
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1) - 1),
                    (int)(S.Y + Position.Y) + 1].Background != NoBrush)
                    move = false;
            }
            if (move)
            {
                currTetris.moveLeft();
                currTetrisDraw();
            }
            else
            {
                currTetrisDraw();
            }
        }

        public void CurrTetrisMoveRight()
        {
            Point Position = currTetris.CurrPosition;
            Point[] Shape = currTetris.CurrShape;
            currTetrisErase();
            bool move = true;
            foreach (Point S in Shape)
            {
                if (((int)(S.X + Position.X) + ((Cols / 2) - 1) + 1) >= Cols)
                    move = false;
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1) + 1),
                    (int)(S.Y + Position.Y) + 1].Background != NoBrush)
                    move = false;
            }
            if (move)
            {
                currTetris.moveRight();
                currTetrisDraw();
            }
            else
            {
                currTetrisDraw();
            }
        }

        public void CurrTetrisMoveDown()
        {
            Point Position = currTetris.CurrPosition;
            Point[] Shape = currTetris.CurrShape;
            currTetrisErase();
            bool move = true;
            foreach (Point S in Shape)
            {
                if (((int)(S.Y + Position.Y) + 1 + 1) >= Rows)
                    move = false;
                else if (BlockControls[(int)(S.X + Position.X) + ((Cols / 2) - 1),
                    (int)(S.Y + Position.Y) + 1 + 1].Background != NoBrush)
                    move = false;
            }
            if (move)
            {
                currTetris.moveDown();
                currTetrisDraw();
            }
            else
            {
                currTetrisDraw();
                checkRows();
                currTetris = new Tetris();
            }
        }

        public void CurrTetrisMoveRotate()
        {
            Point Position = currTetris.CurrPosition;
            Point[] S = new Point[4];
            Point[] Shape = currTetris.CurrShape;
            currTetrisErase();
            bool move = true;
            Shape.CopyTo(S, 0);
            for (int i = 0; i < S.Length; i++)
            {
                double x = S[i].X;
                S[i].X = S[i].Y * -1;
                S[i].Y = x;
                if (((int)(S[i].Y + Position.Y) + 1) >= Rows)
                    move = false;
                else if (((int)(S[i].X + Position.X) + ((Cols / 2) - 1)) >= Cols)
                    move = false;
                else if (((int)(S[i].X + Position.X) + ((Cols / 2) - 1)) < 0)
                    move = false;
                else if (BlockControls[(int)(S[i].X + Position.X) + ((Cols / 2) - 1),
                    (int)(S[i].Y + Position.Y) + 1].Background != NoBrush)
                    move = false;
            }
            if (move)
            {
                currTetris.moveRotate();
                currTetrisDraw();
            }
            else
            {
                currTetrisDraw();
            }
        }
    }
}
