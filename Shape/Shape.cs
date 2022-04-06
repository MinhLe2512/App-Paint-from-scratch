using System;
using System.Collections.Generic;
using System.Drawing;

namespace _19127044_Lab02
{
    abstract class Shape
    {
        public Point startPoint;
        public Point endPoint;
        public Color shapeColor;
        public short id;
        public int[,] coloredPixels;
        public List<Point> listGrid = new List<Point>();
        public List<Point> controlPoints = new List<Point>();

        public bool isFill = false;
        public Color fillColor;
        public Point centerPoint;

        public Point startColoring;

        //Scale purposes
        public Point topLeft, topRight, botLeft, botRight, midLeft, midRight, midUp, midDown;
        //Rotate purposes
        public double oldAngle = 0;

        public void createColorMap()
        {
            int minX = 9999, minY = 9999, maxX = 0, maxY = 0;
            for (int i = 0; i < controlPoints.Count; i++)
            {
                if (controlPoints[i].X > maxX)
                    maxX = controlPoints[i].X;
                if (controlPoints[i].X < minX)
                    minX = controlPoints[i].X;
                if (controlPoints[i].Y < minY)
                    minY = controlPoints[i].Y;
                if (controlPoints[i].Y > maxY)
                    maxY = controlPoints[i].Y;
            }
            coloredPixels = new int[maxX - minX, maxY - minY];
        }
        public abstract void scaleShape(Point p1, Point p2);
        public abstract void moveShape(Point movePoint);

        public void calculateControlPoints(int offset1, int offset2)
        {
            topLeft = new Point(startPoint.X - offset1, startPoint.Y - offset2);
            topRight = new Point(startPoint.X + offset1, startPoint.Y - offset2);
            botLeft = new Point(startPoint.X - offset1, startPoint.Y + offset2);
            botRight = new Point(startPoint.X + offset1, startPoint.Y + offset2);

            midLeft = new Point((topLeft.X + botLeft.X) / 2, (topLeft.Y + botLeft.Y) / 2);
            midRight = new Point((topRight.X + botRight.X) / 2, (topRight.Y + botRight.Y) / 2);
            midUp = new Point((topLeft.X + topRight.X) / 2, (topLeft.Y + topRight.Y) / 2);
            midDown = new Point((botLeft.X + botRight.X) / 2, (botLeft.Y + botRight.Y) / 2);
        }
        public abstract bool reflectShape(Point reflectPoint);
        public abstract void drawShape(Graphics g, Pen myPen);
        private bool validCoord(int x, int y)
        {
            for (int num = 0; num < listGrid.Count; num++)
            {
                if ((x == listGrid[num].X - 1&& y == listGrid[num].Y - 1) ||
                    (x == listGrid[num].X + 1 && y == listGrid[num].Y + 1) ||
                    (x == listGrid[num].X - 1 && y == listGrid[num].Y + 1) ||
                    (x == listGrid[num].X + 1 && y == listGrid[num].Y - 1))
                    return false;
            }
            return true;
        }
        public void setColorMap(int[,] colorMap)
        {
            coloredPixels = new int[colorMap.GetLength(0), colorMap.GetLength(1)];
            for (int i = 0; i < colorMap.GetLength(0); i++)
            {
                for (int j = 0; j < colorMap.GetLength(1); j++)
                {
                    coloredPixels[i, j] = colorMap[i, j];
                }
            }
        }
        public static double CalculateAngle(Point center, Point point)
        {
            return (double)Math.Atan2(point.Y - center.Y, point.X - center.X);
        }
        public void addControlPoints()
        {
            controlPoints.Clear();
            controlPoints.Add(topLeft);
            controlPoints.Add(topRight);
            controlPoints.Add(botLeft);
            controlPoints.Add(botRight);

            controlPoints.Add(midLeft);
            controlPoints.Add(midRight);
            controlPoints.Add(midUp);
            controlPoints.Add(midDown);

            createColorMap();
        }
        private void bfs(Graphics g, Pen myPen, int i, int j, Point sPoint)
        {
            List<Point> qPoints = new List<Point>();
            qPoints.Add(new Point(i, j));

            int i_c;
            int j_c;
            while (qPoints.Count != 0)
            {
                Point front = qPoints[0];
                int x = front.X;
                int y = front.Y;
                i_c = front.X - sPoint.X;
                j_c = front.Y - sPoint.Y;

                qPoints.RemoveAt(0);

                coloredPixels[i_c, j_c] = 1;
                g.DrawRectangle(myPen, x, y, 2, 2);
                if (validCoord(x + 1, y)
                    && coloredPixels[i_c + 1, j_c] != 1)
                {
                    qPoints.Add(new Point(x + 1, y));
                    coloredPixels[i_c + 1, j_c] = 1;
                }

                // For Left side Pixel or Cell
                if (validCoord(x - 1, y)
                    && coloredPixels[i_c - 1, j_c] != 1)
                {
                    qPoints.Add(new Point(x - 1, y));
                    coloredPixels[i_c - 1, j_c] = 1;
                }

                // For Up side Pixel or Cell
                if (validCoord(x, y + 1)
                    && coloredPixels[i_c, j_c + 1] != 1)
                {
                    qPoints.Add(new Point(x, y + 1));
                    coloredPixels[i_c, j_c + 1] = 1;
                }

                // For Down side Pixel or Cell
                if (validCoord(x, y - 1)
                    && coloredPixels[i_c, j_c - 1] != 1)
                {
                    qPoints.Add(new Point(x, y - 1));
                    coloredPixels[i_c, j_c - 1] = 1;
                }

            }
        }
        //Draw small shape
        private void dfs(Graphics g, Pen myPen, int i, int j, Point sPoint)
         {
            for (int num = 0; num < listGrid.Count; num++)
            {
                if (i == listGrid[num].X && j == listGrid[num].Y)
                    return;
            }

            int i_c = i - sPoint.X;
            int j_c = j - sPoint.Y;
            if (coloredPixels[i_c, j_c] == 1)
                return;
            g.DrawRectangle(myPen, i, j, 2, 2);


            coloredPixels[i_c, j_c] = 1;
            dfs(g, myPen, i + 1, j, sPoint);
            dfs(g, myPen, i - 1, j, sPoint);
            dfs(g, myPen, i, j + 1, sPoint);
            dfs(g, myPen, i, j - 1, sPoint);
         }
        public void fill(Graphics g, Pen myPen, int method, int i, int j, Point sPoint)
        {
            startColoring = sPoint;
            if (!isFill)
            {
                int old_color = coloredPixels[i - sPoint.X, j - sPoint.Y];
                //Point tmp = startPoint;

                if (old_color != 1)
                {
                    if (method == 1)
                        bfs(g, myPen, i, j, sPoint);
                    else if (method == 2)
                        dfs(g, myPen, i, j, sPoint);
                }
                if (id != 4)
                {
                    fillColor = myPen.Color;
                    isFill = true;
                }
            }
            else
            {
                for (int x = 0; x < coloredPixels.GetLength(0); x++)
                {
                    for (int y = 0; y < coloredPixels.GetLength(1); y++)
                    {
                        if (coloredPixels[x, y] != 0)
                            g.DrawRectangle(myPen, x + sPoint.X, y + sPoint.Y, 2, 2);
                    }
                }
            }
        }

    }
}
