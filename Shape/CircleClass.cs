using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using Point = System.Drawing.Point;

namespace _19127044_Lab02
{
    class CircleClass: Shape
    {
        private int radius;

        public CircleClass(int cX, int cY, int eX, int eY, Color userColor, bool isFill, Color fillColor)
        {
            startPoint.X = cX;
            startPoint.Y = cY;
            endPoint.X = eX;
            endPoint.Y = eY;
            shapeColor = userColor;

            radius = (int)Math.Sqrt(Math.Pow(startPoint.X - endPoint.X, 2)
                + Math.Pow(startPoint.Y - endPoint.Y, 2));
            centerPoint = startPoint;

            calculateControlPoints(radius, radius);

            addControlPoints();
            coloredPixels = new int[radius * 2, radius * 2];
            id = 1;

            this.isFill = isFill;
            this.fillColor = fillColor;
        }
        private void plotRectangle(Graphics g, Pen myPen, int x, int y)
        {
            g.DrawRectangle(myPen, startPoint.X + x, startPoint.Y + y, 2, 2);
            listGrid.Add(new Point(startPoint.X + x, startPoint.Y + y));

            g.DrawRectangle(myPen, startPoint.X + x, startPoint.Y - y, 2, 2);
            listGrid.Add(new Point(startPoint.X + x, startPoint.Y - y));

            g.DrawRectangle(myPen, startPoint.X - x, startPoint.Y + y, 2, 2);
            listGrid.Add(new Point(startPoint.X - x, startPoint.Y + y));

            g.DrawRectangle(myPen, startPoint.X - x, startPoint.Y - y, 2, 2);
            listGrid.Add(new Point(startPoint.X - x, startPoint.Y - y));

            g.DrawRectangle(myPen, startPoint.X + y, startPoint.Y + x, 2, 2);
            listGrid.Add(new Point(startPoint.X + y, startPoint.Y + x));

            g.DrawRectangle(myPen, startPoint.X + y, startPoint.Y - x, 2, 2);
            listGrid.Add(new Point(startPoint.X + y, startPoint.Y - x));

            g.DrawRectangle(myPen, startPoint.X - y, startPoint.Y + x, 2, 2);
            listGrid.Add(new Point(startPoint.X - y, startPoint.Y + x));

            g.DrawRectangle(myPen, startPoint.X - y, startPoint.Y - x, 2, 2);
            listGrid.Add(new Point(startPoint.X - y, startPoint.Y - x));
        }
        public override void drawShape(Graphics g, Pen myPen)
        {
            listGrid.Clear();
            g.DrawEllipse(myPen, startPoint.X + 2, startPoint.Y + 2, 4, 4);
            g.DrawEllipse(myPen, endPoint.X + 2, endPoint.Y + 2, 4, 4);

            
            //listGrid.Add(endPoint);
            int x0 = 0, y0 = radius, p0 = 1 - radius;
            plotRectangle(g, myPen, x0, y0);
            myPen.Color = shapeColor;
            while (x0 < y0)
            {
                if (p0 < 0)
                    p0 += 2 * x0 + 1;
                else
                {
                    y0--;
                    p0 = p0 + 2 * x0 - 2 * y0 + 1;
                }
                x0++;
                plotRectangle(g, myPen, x0, y0);
            }
            
        }
       
        public void fillCircle(Graphics g, Pen myPen, Point xPoint)
        {
            fill(g, myPen, 1, xPoint.X, xPoint.Y, controlPoints[0]);   
        }

        public override void scaleShape(Point p1, Point p2)
        {
            Point offsetPoint = p1;
            double sx = 1.0, sy = 1.0;
            if (p1 == topLeft)
            {
                offsetPoint = botRight;
                //decrease
                if (p1.X < p2.X && p1.Y < p2.Y)
                {
                    sx = p1.X * 1.0 / p2.X * 1.0;
                    sy = p1.Y * 1.0 / p2.Y * 1.0;
                }
                //increase
                else if (p1.X > p2.X && p1.Y > p2.Y)
                {
                    sx = p1.X * 1.0 / p2.X * 1.0;
                    sy = p1.Y * 1.0 / p2.Y * 1.0;
                }
            }
            else if (p1 == topRight)
            {
                offsetPoint = botLeft;
                //increase
                if (p1.X < p2.X && p1.Y > p2.Y)
                {
                    sx = p2.X * 1.0 / p1.X * 1.0;
                    sy = p1.Y * 1.0 / p2.Y * 1.0;
                }
                //decrease
                else if (p1.X > p2.X && p1.Y < p2.Y)
                {
                    sx = p2.X * 1.0 / p1.X * 1.0;
                    sy = p1.Y * 1.0 / p2.Y * 1.0;
                }
            }
            else if (p1 == botLeft)
            {
                offsetPoint = topRight;
                //increase
                if (p1.X > p2.X && p1.Y < p2.Y)
                {
                    sx = p1.X * 1.0 / p2.X * 1.0;
                    sy = p2.Y * 1.0 / p1.Y * 1.0;
                }
                //decrease
                else if (p1.X < p2.X && p1.Y > p2.Y)
                {
                    sx = p1.X * 1.0 / p2.X * 1.0;
                    sy = p2.Y * 1.0 / p1.Y * 1.0;
                }
            }
            else if (p1 == botRight)
            {
                offsetPoint = topLeft;
                //increase
                if (p1.X < p2.X && p1.Y < p2.Y)
                {
                    sx = p2.X * 1.0 / p1.X * 1.0;
                    sy = p2.Y * 1.0 / p1.Y * 1.0;
                }
                //decrease
                else if (p1.X > p2.X && p1.Y > p2.Y)
                {
                    sx = p2.X * 1.0 / p1.X * 1.0;
                    sy = p2.Y * 1.0 / p1.Y * 1.0;
                }
            }

            double scaleAmount = (sx + sy) / 2;
            AffineTransform at = new AffineTransform();
            double[,] scaleMatrix = at.createScaleMatrix(scaleAmount, scaleAmount);


            double[,] tL = new double[,] { { topLeft.X - offsetPoint.X }, { topLeft.Y - offsetPoint.Y }, { 1 } };
            double[,] tR = new double[,] { { topRight.X - offsetPoint.X }, { topRight.Y - offsetPoint.Y }, { 1 } };
            double[,] bL = new double[,] { { botLeft.X - offsetPoint.X }, { botLeft.Y - offsetPoint.Y }, { 1 } };
            double[,] bR = new double[,] { { botRight.X - offsetPoint.X }, { botRight.Y - offsetPoint.Y }, { 1 } };

            topLeft = at.Multiply(scaleMatrix, tL, offsetPoint);
            topRight = at.Multiply(scaleMatrix, tR, offsetPoint);
            botLeft = at.Multiply(scaleMatrix, bL, offsetPoint);
            botRight = at.Multiply(scaleMatrix, bR, offsetPoint);

            midLeft = new Point((topLeft.X + botLeft.X) / 2, (topLeft.Y + botLeft.Y) / 2);
            midRight = new Point((topRight.X + botRight.X) / 2, (topRight.Y + botRight.Y) / 2);
            midUp = new Point((topLeft.X + topRight.X) / 2, (topLeft.Y + topRight.Y) / 2);
            midDown = new Point((botLeft.X + botRight.X) / 2, (botLeft.Y + botRight.Y) / 2);

            startPoint.X = (topLeft.X + topRight.X) / 2;
            startPoint.Y = (topLeft.Y + botLeft.Y) / 2;
            endPoint.X = topRight.X;
            endPoint.Y = startPoint.Y;

            radius = (int)Math.Sqrt(Math.Pow(startPoint.X - endPoint.X, 2)
                + Math.Pow(startPoint.Y - endPoint.Y, 2));

            addControlPoints();
        }

        public override void moveShape(Point movePoint)
        {
            double tx = movePoint.X - centerPoint.X;
            double ty = movePoint.Y - centerPoint.Y;

            AffineTransform at = new AffineTransform();
            double[,] translateMatrix = at.createTranslateMatrix(tx, ty);
            //double[,] translateMatrixEnd = at.createTranslateMatrix(txEnd, tyEnd);

            double[,] startMat = new double[,] { { startPoint.X - centerPoint.X }, { startPoint.Y - centerPoint.Y }, { 1 } };
            double[,] endMat = new double[,] { { endPoint.X - centerPoint.X }, { endPoint.Y - centerPoint.Y }, { 1 } };

            startPoint = at.Multiply(translateMatrix, startMat, centerPoint);
            endPoint = at.Multiply(translateMatrix, endMat, centerPoint);

            centerPoint = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
            radius = (int)Math.Sqrt(Math.Pow(startPoint.X - endPoint.X, 2)
                + Math.Pow(startPoint.Y - endPoint.Y, 2));

            calculateControlPoints(radius, radius);

            addControlPoints();
        }

        public override bool reflectShape(Point reflectPoint)
        {
            AffineTransform at = new AffineTransform();
            double[,] reflectMatrix = at.createReflection();

            double offsetX = 1.0, endsetX = 1.0;
            //reflect ox * 1
            if (reflectPoint == midRight || reflectPoint == midLeft)
                reflectMatrix = at.createReflectionX();

            //reflect oy
            else if (reflectPoint == midUp || reflectPoint == midDown)
                reflectMatrix = at.createReflectionY();

            else
                return false;

            double[,] startMat = new double[,] { { -(startPoint.X - reflectPoint.X) }, { -(startPoint.Y - reflectPoint.Y) }, { 1 } };
            double[,] endMat = new double[,] { { -(endPoint.X - reflectPoint.X) }, { -(endPoint.Y - reflectPoint.Y) }, { 1 } };

            startPoint = at.Multiply(reflectMatrix, startMat, reflectPoint);
            endPoint = at.Multiply(reflectMatrix, endMat, reflectPoint);

            calculateControlPoints(radius, radius);

            centerPoint = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
            addControlPoints();

            return true;
        }
    }
}
