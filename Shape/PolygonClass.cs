using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19127044_Lab02
{
    class PolygonClass : Shape
    {
        private List<Point> listPolygonPoints = new List<Point>();
        public PolygonClass(List<Point> listPoints, Color userColor, bool isFill, Color fillColor)
        {
            for (int i = 0; i < listPoints.Count; i++)
                listPolygonPoints.Add(listPoints[i]);

            fintControlPoints(listPoints);

            addControlPoints();

            startPoint = controlPoints[0];
            endPoint = controlPoints[controlPoints.Count - 1];
            shapeColor = userColor;
            this.isFill = isFill;
            this.fillColor = fillColor;
            this.id = 4;

            createColorMap();
        }

 
        public override void drawShape(Graphics g, Pen myPen)
        {
            for (int i = 0; i < listPolygonPoints.Count - 1; i++)
            {
                Point tmpPoint = listPolygonPoints[i];
                Point nextPoint = listPolygonPoints[i + 1];
                LineClass tmpLine = new LineClass(tmpPoint.X, nextPoint.X, tmpPoint.Y, nextPoint.Y, shapeColor);
                tmpLine.drawShape(g, myPen);
                for (int j = 0; j < tmpLine.listGrid.Count; j++)
                    this.listGrid.Add(tmpLine.listGrid[j]);
            }
            LineClass finalLine = new LineClass(listPolygonPoints[0].X, listPolygonPoints[listPolygonPoints.Count - 1].X,
                listPolygonPoints[0].Y, listPolygonPoints[listPolygonPoints.Count - 1].Y, shapeColor);
            finalLine.drawShape(g, myPen);
            for (int j = 0; j < finalLine.listGrid.Count; j++)
                this.listGrid.Add(finalLine.listGrid[j]);
        }
        public void fillPolygon(Graphics g, Pen myPen, int method, Point clickPoint)
        {
            fill(g, myPen, method, clickPoint.X, clickPoint.Y, topLeft);
        }

        private void fintControlPoints(List<Point> listPoints)
        {
            int minX = 9999, minY = 9999, maxX = 0, maxY = 0;

            int sumX = 0, sumY = 0;
            for (int i = 0; i < listPoints.Count; i++)
            {
                if (listPoints[i].X > maxX)
                    maxX = listPoints[i].X;
                if (listPoints[i].X < minX)
                    minX = listPoints[i].X;
                if (listPoints[i].Y < minY)
                    minY = listPoints[i].Y;
                if (listPoints[i].Y > maxY)
                    maxY = listPoints[i].Y;

                sumX += listPoints[i].X;
                sumY += listPoints[i].Y;
               
            }
            topLeft = new Point(minX, minY);
            topRight = new Point(maxX, minY);
            botLeft = new Point(minX, maxY);
            botRight = new Point(maxX, maxY);

            midLeft = new Point((topLeft.X + botLeft.X) / 2, (topLeft.Y + botLeft.Y) / 2);
            midRight = new Point((topRight.X + botRight.X) / 2, (topRight.Y + botRight.Y) / 2);
            midUp = new Point((topLeft.X + topRight.X) / 2, (topLeft.Y + topRight.Y) / 2);
            midDown = new Point((botLeft.X + botRight.X) / 2, (botLeft.Y + botRight.Y) / 2);

            centerPoint = new Point(sumX / listPoints.Count, sumY / listPoints.Count);
        }
        public override void moveShape(Point movePoint)
        {
            double tx = movePoint.X - centerPoint.X;
            double ty = movePoint.Y - centerPoint.Y;

            AffineTransform at = new AffineTransform();
            double[,] translateMatrix = at.createTranslateMatrix(tx, ty);
            //double[,] translateMatrixEnd = at.createTranslateMatrix(txEnd, tyEnd);
            for (int i = 0; i < listPolygonPoints.Count; i++)
            {
                Point tmp = listPolygonPoints[i];
                double[,] tmpMatrix = new double[,] { { tmp.X - centerPoint.X},
                    { tmp.Y - centerPoint.Y}, { 1 } };
                tmp = at.Multiply(translateMatrix, tmpMatrix, centerPoint); ;
                listPolygonPoints[i] = tmp;
            }

            fintControlPoints(listPolygonPoints);

            addControlPoints();
        }

        public void rotateShape(Point dragPoint)
        {
            double angle = CalculateAngle(centerPoint, dragPoint);
            AffineTransform at = new AffineTransform();

            oldAngle = oldAngle + angle * 180.0 / (2 * Math.PI);
            if (Math.Abs(oldAngle) > 180)
                oldAngle %= 180;
            double[,] rotateMatrix = at.createRotateMatrix(oldAngle);

            for (int i = 0; i < listPolygonPoints.Count; i++)
            {
                Point tmp = listPolygonPoints[i];
                double[,] tmpMatrix = new double[,] { { tmp.X - centerPoint.X},
                    { tmp.Y - centerPoint.Y}, { 1 } };
                tmp = at.Multiply(rotateMatrix, tmpMatrix, centerPoint);
                listPolygonPoints[i] = tmp;
            }

            fintControlPoints(listPolygonPoints);

            addControlPoints();
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

            for (int i = 0; i < listPolygonPoints.Count; i++)
            {
                Point tmp = listPolygonPoints[i];
                double[,] tmpMatrix = new double[,] { { tmp.X - offsetPoint.X},
                    { tmp.Y - offsetPoint.Y}, { 1 } };
                tmp = at.Multiply(scaleMatrix, tmpMatrix, offsetPoint); ;
                listPolygonPoints[i] = tmp;
            }

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

            addControlPoints();
        }

        public override bool reflectShape(Point reflectPoint)
        {
            AffineTransform at = new AffineTransform();
            double[,] reflectMatrix = at.createReflection();

            //reflect ox * 1
            if (reflectPoint == midRight || reflectPoint == midLeft)
                reflectMatrix = at.createReflectionX();

            //reflect oy
            else if (reflectPoint == midUp || reflectPoint == midDown)
                reflectMatrix = at.createReflectionY();

            else
                return false;

            for (int i = 0; i < listPolygonPoints.Count; i++)
            {
                Point tmp = listPolygonPoints[i];
                double[,] tmpMatrix = new double[,] { { reflectPoint.X - tmp.X },
                    { reflectPoint.Y - tmp.Y }, { 1 } };
                tmp = at.Multiply(reflectMatrix, tmpMatrix, reflectPoint);
                listPolygonPoints[i] = tmp;
            }

            double[,] tL = new double[,] { { reflectPoint.X - topLeft.X }, { reflectPoint.Y - topLeft.Y }, { 1 } };
            double[,] tR = new double[,] { { reflectPoint.X - topRight.X }, { reflectPoint.Y - topRight.Y }, { 1 } };
            double[,] bL = new double[,] { { reflectPoint.X - botLeft.X }, { reflectPoint.Y - botLeft.Y }, { 1 } };
            double[,] bR = new double[,] { { reflectPoint.X - botRight.X }, { reflectPoint.Y - botRight.Y }, { 1 } };

            topLeft = at.Multiply(reflectMatrix, tL, reflectPoint);
            topRight = at.Multiply(reflectMatrix, tR, reflectPoint);
            botLeft = at.Multiply(reflectMatrix, bL, reflectPoint);
            botRight = at.Multiply(reflectMatrix, bR, reflectPoint);

            midLeft = new Point((topLeft.X + botLeft.X) / 2, (topLeft.Y + botLeft.Y) / 2);
            midRight = new Point((topRight.X + botRight.X) / 2, (topRight.Y + botRight.Y) / 2);
            midUp = new Point((topLeft.X + topRight.X) / 2, (topLeft.Y + topRight.Y) / 2);
            midDown = new Point((botLeft.X + botRight.X) / 2, (botLeft.Y + botRight.Y) / 2);

            centerPoint = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
            addControlPoints();

            return true;
        }
    }
}
