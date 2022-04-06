using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19127044_Lab02
{
    class EqulateralRectClass : Shape
    {
        public EqulateralRectClass(int x1, int y1, int x2, int y2, Color uc)
        {
            startPoint.X = x1;
            startPoint.Y = y1;
            endPoint.X = x2;
            endPoint.Y = y2;
            shapeColor = uc;
            id = 4;
        }
        public override void drawShape(Graphics g, Pen myPen)
        {
            throw new NotImplementedException();
        }

        public override void moveShape(Point movePoint)
        {
            throw new NotImplementedException();
        }

        public override bool reflectShape(Point reflectPoint)
        {
            throw new NotImplementedException();
        }

        public override void scaleShape(Point p1, Point p2)
        {
            throw new NotImplementedException();
        }
    }
}
