using System;
using System.Drawing;

namespace _19127044_Lab02
{
    class RectangleClass : Shape
    {
        public RectangleClass(int x1, int x2, int y1, int y2, Color uc, bool isFill, Color fillColor)
        {
            startPoint.X = x1;
            startPoint.Y = y1;
            endPoint.X = x2;
            endPoint.Y = y2;
            shapeColor = uc;
            id = 2;

            topLeft = startPoint;
            topRight = new Point(endPoint.X, startPoint.Y);
            botLeft = new Point(startPoint.X, endPoint.Y);
            botRight = endPoint;
            centerPoint = new Point((endPoint.X + startPoint.X) / 2, (endPoint.Y + startPoint.Y) / 2);

            midLeft = new Point((topLeft.X + botLeft.X) / 2, (topLeft.Y + botLeft.Y) / 2);
            midRight = new Point((topRight.X + botRight.X) / 2, (topRight.Y + botRight.Y) / 2);
            midUp = new Point((topLeft.X + topRight.X) / 2, (topLeft.Y + topRight.Y) / 2);
            midDown = new Point((botLeft.X + botRight.X) / 2, (botLeft.Y + botRight.Y) / 2);

            addControlPoints();

            this.isFill = isFill;
            this.fillColor = fillColor;
        }
      
        public void rotateShape(Point dragPoint)
        {
            AffineTransform at = new AffineTransform();
            double angle = CalculateAngle(centerPoint, dragPoint);

            oldAngle = oldAngle + angle * 180.0 / (2 * Math.PI);
            if (Math.Abs(oldAngle) > 180)
                oldAngle %= 180;
            double[,] rotateMatrix = at.createRotateMatrix(oldAngle);

            double[,] tL = new double[,] { { startPoint.X - centerPoint.X }, { startPoint.Y - centerPoint.Y }, { 1 } };
            double[,] tR = new double[,] { { endPoint.X - centerPoint.X}, { startPoint.Y - centerPoint.Y }, { 1 } };
            double[,] bL = new double[,] { { startPoint.X - centerPoint.X }, { endPoint.Y - centerPoint.Y }, { 1 } };
            double[,] bR = new double[,] { { endPoint.X - centerPoint.X }, { endPoint.Y - centerPoint.Y }, { 1 } };
          
            topLeft = at.Multiply(rotateMatrix, tL, centerPoint);
            topRight = at.Multiply(rotateMatrix, tR, centerPoint);
            botLeft = at.Multiply(rotateMatrix, bL, centerPoint);
            botRight = at.Multiply(rotateMatrix, bR, centerPoint);

            midLeft = new Point((topLeft.X + botLeft.X) / 2, (topLeft.Y + botLeft.Y) / 2);
            midRight = new Point((topRight.X + botRight.X) / 2, (topRight.Y + botRight.Y) / 2);
            midUp = new Point((topLeft.X + topRight.X) / 2, (topLeft.Y + topRight.Y) / 2);
            midDown = new Point((botLeft.X + botRight.X) / 2, (botLeft.Y + botRight.Y) / 2);

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
                if (p1.X < p2.X && p1.Y < p2.Y) {
                    sx = p1.X * 1.0 / p2.X * 1.0;
                    sy = p1.Y * 1.0 / p2.Y * 1.0;
                }
                //increase
                else if(p1.X > p2.X && p1.Y > p2.Y)
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

            double[,] startMat = new double[,] { { startPoint.X - offsetPoint.X }, { startPoint.Y - offsetPoint.Y }, { 1 } };
            double[,] endMat = new double[,] { { endPoint.X - offsetPoint.X }, { endPoint.Y - offsetPoint.Y }, { 1 } };

            double[,] tL = new double[,] { { topLeft.X - offsetPoint.X }, { topLeft.Y - offsetPoint.Y}, { 1 } };
            double[,] tR = new double[,] { { topRight.X - offsetPoint.X }, { topRight.Y - offsetPoint.Y }, { 1 } };
            double[,] bL = new double[,] { { botLeft.X - offsetPoint.X}, { botLeft.Y - offsetPoint.Y }, { 1 } };
            double[,] bR = new double[,] { { botRight.X - offsetPoint.X }, { botRight.Y - offsetPoint.Y }, { 1 } };

            topLeft = at.Multiply(scaleMatrix, tL, offsetPoint);
            topRight = at.Multiply(scaleMatrix, tR, offsetPoint);
            botLeft = at.Multiply(scaleMatrix, bL, offsetPoint);
            botRight = at.Multiply(scaleMatrix, bR, offsetPoint);

            midLeft = new Point((topLeft.X + botLeft.X) / 2, (topLeft.Y + botLeft.Y) / 2);
            midRight = new Point((topRight.X + botRight.X) / 2, (topRight.Y + botRight.Y) / 2);
            midUp = new Point((topLeft.X + topRight.X) / 2, (topLeft.Y + topRight.Y) / 2);
            midDown = new Point((botLeft.X + botRight.X) / 2, (botLeft.Y + botRight.Y) / 2);

            startPoint = at.Multiply(scaleMatrix, startMat, offsetPoint); ;
            endPoint = at.Multiply(scaleMatrix, endMat, offsetPoint); ;

            addControlPoints();
        }
        public override void drawShape(Graphics g, Pen myPen)
        {
            g.DrawEllipse(myPen, topLeft.X + 2, topLeft.Y + 2, 4, 4);
            g.DrawEllipse(myPen, topRight.X + 2, topRight.Y + 2, 4, 4);
            g.DrawEllipse(myPen, botLeft.X + 2, botLeft.Y + 2, 4, 4);
            g.DrawEllipse(myPen, botRight.X + 2, botRight.Y + 2, 4, 4);
            myPen.Color = shapeColor;

            LineClass l1 = new LineClass(topLeft.X, topRight.X, topLeft.Y, topRight.Y, shapeColor);
            l1.drawShape(g, myPen);
            LineClass l2 = new LineClass(topLeft.X, botLeft.X, topLeft.Y, botLeft.Y, shapeColor);
            l2.drawShape(g, myPen);

            LineClass l3 = new LineClass(botLeft.X, botRight.X, botLeft.Y, botRight.Y, shapeColor);
            l3.drawShape(g, myPen);
            LineClass l4 = new LineClass(topRight.X, botRight.X, topRight.Y, botRight.Y, shapeColor);
            l4.drawShape(g, myPen);

            foreach (Point p1 in l1.listGrid)
                listGrid.Add(p1);

            foreach (Point p2 in l2.listGrid)
                listGrid.Add(p2);

            foreach (Point p3 in l3.listGrid)
                listGrid.Add(p3);

            foreach (Point p4 in l4.listGrid)
                listGrid.Add(p4);
        }
        public void fillRectangle(Graphics g, Pen myPen, int method, Point sPoint)
        {
            fill(g, myPen, method, sPoint.X, sPoint.Y, startPoint);
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

            double[,] tL = new double[,] { { topLeft.X - centerPoint.X }, { topLeft.Y - centerPoint.Y }, { 1 } };
            double[,] tR = new double[,] { { topRight.X - centerPoint.X }, { topRight.Y - centerPoint.Y }, { 1 } };
            double[,] bL = new double[,] { { botLeft.X - centerPoint.X }, { botLeft.Y - centerPoint.Y }, { 1 } };
            double[,] bR = new double[,] { { botRight.X - centerPoint.X }, { botRight.Y - centerPoint.Y }, { 1 } };

            topLeft = at.Multiply(translateMatrix, tL, centerPoint);
            topRight = at.Multiply(translateMatrix, tR, centerPoint);
            botLeft = at.Multiply(translateMatrix, bL, centerPoint);
            botRight = at.Multiply(translateMatrix, bR, centerPoint);

            midLeft = new Point((topLeft.X + botLeft.X) / 2, (topLeft.Y + botLeft.Y) / 2);
            midRight = new Point((topRight.X + botRight.X) / 2, (topRight.Y + botRight.Y) / 2);
            midUp = new Point((topLeft.X + topRight.X) / 2, (topLeft.Y + topRight.Y) / 2);
            midDown = new Point((botLeft.X + botRight.X) / 2, (botLeft.Y + botRight.Y) / 2);

            startPoint = at.Multiply(translateMatrix, startMat, centerPoint);
            endPoint = at.Multiply(translateMatrix, endMat, centerPoint);

            centerPoint = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);

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

            double[,] startMat = new double[,] { { reflectPoint.X - startPoint.X }, { reflectPoint.Y - startPoint.Y }, { 1 } };
            double[,] endMat = new double[,] { {  reflectPoint.X - endPoint.X }, { reflectPoint.Y - endPoint.Y }, { 1 } };

            startPoint = at.Multiply(reflectMatrix, startMat, reflectPoint);
            endPoint = at.Multiply(reflectMatrix, endMat, reflectPoint);

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
