using System;
using System.Drawing;

namespace _19127044_Lab02
{
    class EllipseClass : Shape
    {
        private void plotRectangle(Graphics g, Pen myPen, int x, int y)
        {
            g.DrawRectangle(myPen, startPoint.X + x, startPoint.Y + y, 2, 2);
            listGrid.Add(new Point(startPoint.X + x, startPoint.Y + y));

            g.DrawRectangle(myPen, startPoint.X - x, startPoint.Y + y, 2, 2);
            listGrid.Add(new Point(startPoint.X - x, startPoint.Y + y));

            g.DrawRectangle(myPen, startPoint.X + x, startPoint.Y - y, 2, 2);
            listGrid.Add(new Point(startPoint.X + x, startPoint.Y - y));

            g.DrawRectangle(myPen, startPoint.X - x, startPoint.Y - y, 2, 2);
            listGrid.Add(new Point(startPoint.X - x, startPoint.Y - y));
        }
        private int a, b;
        public EllipseClass(int x1, int x2, int y1, int y2, Color uc, bool isFill, Color fillColor)
        {
            startPoint.X = x1;
            startPoint.Y = y1;
            endPoint.X = x2;
            endPoint.Y = y2;
            shapeColor = uc;
            a = Math.Abs(startPoint.Y - endPoint.Y);
            b = Math.Abs(startPoint.X - endPoint.X);
            coloredPixels = new int[b * 2, a * 2];

            topLeft = new Point(startPoint.X - b, startPoint.Y - a);
            topRight = new Point(startPoint.X + b, startPoint.Y - a);
            botLeft = new Point(startPoint.X - b, startPoint.Y + a);
            botRight = new Point(startPoint.X + b, startPoint.Y + a);
            centerPoint = startPoint;

            midLeft = new Point((topLeft.X + botLeft.X) / 2, (topLeft.Y + botLeft.Y) / 2);
            midRight = new Point((topRight.X + botRight.X) / 2, (topRight.Y + botRight.Y) / 2);
            midUp = new Point((topLeft.X + topRight.X) / 2, (topLeft.Y + topRight.Y) / 2);
            midDown = new Point((botLeft.X + botRight.X) / 2, (botLeft.Y + botRight.Y) / 2);

            addControlPoints();

            this.isFill = isFill;
            this.fillColor = fillColor;

            id = 3;
        }
        public override void drawShape(Graphics g, Pen myPen)
        {

            g.DrawEllipse(myPen, startPoint.X + 2, startPoint.Y + 2, 4, 4);
            g.DrawEllipse(myPen, endPoint.X + 2, endPoint.Y + 2, 4, 4);
            int x = 0;
            int y = a;

            float dx = 2 * a * a * x;
            float dy = 2 * b * b * y;
            myPen.Color = shapeColor;
            double p1o = a * a - (b * b * a) + (0.25 * b * b);
            plotRectangle(g, myPen, x, y);
            while (dx < dy)
            {
                if (p1o < 0)
                {
                    x++;
                    dx += 2 * a * a;
                    p1o += dx + a * a;
                }
                else
                {
                    x++;
                    y--;
                    dx += 2 * a * a;
                    dy -= 2 * b * b;
                    p1o += dx - dy + a * a;
                }
                plotRectangle(g, myPen, x, y);
            }
            double p2o = a * a * (x + 0.5) * (x + 0.5) + b * b * (y - 1) * (y - 1) - a * a * b * b;

            while (y >= 0)
            {
                if (p2o > 0)
                {
                    y--;
                    dy -= 2 * b * b;
                    p2o += b * b - dy; 
                }
                else
                {
                    y--;
                    x++;
                    dy -= 2 * b * b;
                    dx += 2 * a * a;
                    p2o += dx - dy + b * b;
                }
                plotRectangle(g, myPen, x, y);
            }
        }
        public void fillEllipse(Graphics g, Pen myPen, Point sPoint)
        {
            fill(g, myPen, 1, sPoint.X, sPoint.Y, controlPoints[0]);
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

            AffineTransform at = new AffineTransform();
            double[,] scaleMatrix = at.createScaleMatrix(sx, sy);


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
            endPoint.Y = topRight.Y;

            a = Math.Abs(startPoint.Y - endPoint.Y);
            b = Math.Abs(startPoint.X - endPoint.X);

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

            calculateControlPoints(b, a);

            centerPoint = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);

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

            calculateControlPoints(b, a);

            centerPoint = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
            addControlPoints();

            return true;
        }
    }
}
