using System;
using System.Drawing;

namespace _19127044_Lab02
{
    class LineClass: Shape
    {
        private double width, height;
        public LineClass(int x1, int x2, int y1, int y2, Color uc)
        {
            startPoint.X = x1;
            startPoint.Y = y1;
            endPoint.X = x2;
            endPoint.Y = y2;
            shapeColor = uc;
            controlPoints.Add(startPoint);
            controlPoints.Add(endPoint);
            centerPoint = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);

            width = endPoint.X - startPoint.X;
            height = endPoint.Y - startPoint.Y;
            id = 0;
        }
        public override void drawShape(Graphics g, Pen myPen)
        {
            int p1_X = startPoint.X;
            int p1_Y = startPoint.Y;
            int p2_X = endPoint.X;
            int p2_Y = endPoint.Y;
            myPen.Color = shapeColor;
            g.DrawEllipse(myPen, startPoint.X - 2, startPoint.Y - 2, 4, 4);
            g.DrawEllipse(myPen, endPoint.X - 2, endPoint.Y - 2, 4, 4);
            int dX = p2_X - p1_X;
            int dY = p2_Y - p1_Y;

            int stepX = 1;
            int stepY = 1;
            if (dX < 0)
            {
                dX = -dX;
                stepX = -1;
            }
            else
                stepX = 1;
            if (dY < 0)
            {
                dY = -dY;
                stepY = -1;
            }
            else 
                stepY = 1;

            
            //m < 1
            if (dY < dX)
            {
                float d = dY - dX / 2;
                //myPen.
                g.DrawRectangle(myPen, p1_X, p1_Y, 2, 2);
                listGrid.Add(new Point(p1_X, p1_Y));
                while (Math.Abs(p1_X - p2_X) != 0)
                {          
                    if (d < 0)
                        d += dY;
                    else
                    {
                        d += dY - dX;
                        p1_Y += stepY;
                    }
                    p1_X += stepX;
                    g.DrawRectangle(myPen, p1_X, p1_Y, 2, 2);
                    listGrid.Add(new Point(p1_X, p1_Y));
                }
            }
            //m > 1
            else if (dX < dY)
            {
                float d = dX - dY / 2;
                //myPen.
                g.DrawRectangle(myPen, p1_X, p1_Y, 2, 2);
                listGrid.Add(new Point(p1_X, p1_Y));
                while (Math.Abs(p1_Y - p2_Y) != 0)
                {
                    if (d < 0)
                        d += dX;
                    else
                    {
                        d += dX - dY;
                        p1_X += stepX;
                    }
                    p1_Y += stepY;
                    g.DrawRectangle(myPen, p1_X, p1_Y, 2, 2);
                    listGrid.Add(new Point(p1_X, p1_Y));
                }
            }
            
        }

        public override void moveShape(Point movePoint)
        {
            double tx = movePoint.X - centerPoint.X;
            double ty = movePoint.Y - centerPoint.Y;

            AffineTransform at = new AffineTransform();
            double[,] translateMatrix = at.createTranslateMatrix(tx, ty);
            //double[,] translateMatrixEnd = at.createTranslateMatrix(txEnd, tyEnd);

            double[,] startMat = new double[,] { {startPoint.X - centerPoint.X }, { startPoint.Y - centerPoint.Y }, { 1 } };
            double[,] endMat = new double[,] { { endPoint.X - centerPoint.X }, { endPoint.Y - centerPoint.Y }, { 1 } };

            startPoint = at.Multiply(translateMatrix, startMat, centerPoint);
            endPoint = at.Multiply(translateMatrix, endMat, centerPoint);

            centerPoint = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
        }

        public override bool reflectShape(Point reflectPoint)
        {
            AffineTransform at = new AffineTransform();
            double[,] reflectMatrix = at.createReflection();

            //reflect ox * 1
            if (reflectPoint == startPoint)
                reflectMatrix = at.createReflectionX();

            //reflect oy
            else if (reflectPoint == endPoint)
                reflectMatrix = at.createReflectionY();

            else
                return false;

            double[,] startMat = new double[,] { { -(startPoint.X - reflectPoint.X) }, { -(startPoint.Y - reflectPoint.Y) }, { 1 } };
            double[,] endMat = new double[,] { { -(endPoint.X - reflectPoint.X) }, { -(endPoint.Y - reflectPoint.Y) }, { 1 } };

            startPoint = at.Multiply(reflectMatrix, startMat, reflectPoint);
            endPoint = at.Multiply(reflectMatrix, endMat, reflectPoint);

            controlPoints.Clear();
            controlPoints.Add(startPoint);
            controlPoints.Add(endPoint);

            return true;
        }

        public override void scaleShape(Point p1, Point p2)
        {
            if (p1 == startPoint)
                startPoint = p2;
            else if (p1 == endPoint)
                endPoint = p2;
        }
    }
}
