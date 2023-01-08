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
    public class Tetris
    {
        private Point currPosition;
        private Point[] currShape;
        private Brush currColor;
        private bool rotate;

        public Tetris()
        {
            currPosition = new Point(0, 0);
            currShape = setRandomShape();
        }

        public Point CurrPosition { get { return currPosition; } }
        public Point[] CurrShape { get { return currShape; } }
        public Brush CurrColor { get { return currColor; } }

        public void moveLeft()
        {
            currPosition.X -= 1;
        }

        public void moveRight()
        {
            currPosition.X += 1;
        }

        public void moveDown()
        {
            currPosition.Y += 1;
        }

        public void moveRotate()
        {
            if (rotate)
            {
                for (int i = 0; i < currShape.Length; i++)
                {
                    double x = currShape[i].X;
                    currShape[i].X = currShape[i].Y * -1;
                    currShape[i].Y = x;
                }
            }
        }

        private Point[] setRandomShape()
        {
            Random rand = new Random();
            switch (rand.Next() % 7)
            {
                case 0:
                    rotate = true;
                    currColor = Brushes.Cyan;
                    return new Point[]
                    {
                        new Point(0, -1),
                        new Point(-1, -1),
                        new Point(1, -1),
                        new Point(2, -1)
                    };
                case 1:
                    rotate = true;
                    currColor = Brushes.Blue;
                    return new Point[]
                    {
                        new Point(1, -1),
                        new Point(-1, 0),
                        new Point(0, 0),
                        new Point(1, 0)
                    };
                case 2:
                    rotate = true;
                    currColor = Brushes.Cornsilk;
                    return new Point[]
                    {
                        new Point(0, 0),
                        new Point(-1, 0),
                        new Point(1, 0),
                        new Point(-1, -1)
                    };
                case 3:
                    rotate = false;
                    currColor = Brushes.Yellow;
                    return new Point[]
                    {
                        new Point(0, 0),
                        new Point(0, -1),
                        new Point(1, 0),
                        new Point(1, -1)
                    };
                case 4:
                    rotate = true;
                    currColor = Brushes.Green;
                    return new Point[]
                    {
                        new Point(0, 0),
                        new Point(-1, 0),
                        new Point(0, -1),
                        new Point(1, 0)
                    };
                case 5:
                    rotate = true;
                    currColor = Brushes.Purple;
                    return new Point[]
                    {
                        new Point(0, 0),
                        new Point(-1, 0),
                        new Point(1, 0),
                        new Point(1, 1)
                    };
                case 6:
                    rotate = true;
                    currColor = Brushes.Red;
                    return new Point[]
                    {
                        new Point(0, -1),
                        new Point(-1, -1),
                        new Point(0, 0),
                        new Point(1, 0)
                    };
                default:
                    return null;
            }
        }

    }
}
