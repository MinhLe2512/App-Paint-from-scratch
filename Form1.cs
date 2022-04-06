using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using SharpGL;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace _19127044_Lab02
{
    public partial class Form1 : Form
    {
        Color userColor;
        ////0. Line, 1. Circle
        private short idShape;

        private bool isMouseDown = false;
        private Bitmap _canvas;

        private List<Shape> listShapes = new List<Shape>();
        private int length = 0;
        //Select 1 or many shapes
        private List<int> listSelectedShapes = new List<int>();
        private Rectangle selectRectangle;
       
        private int mStart_X, mStart_Y, mEnd_X, mEnd_Y;

        private Point p1, p2;
        private Point StartDownLocation;
        
        private String drawCase = "Drawing";
        //For drawing polygon
        private List<Point> listPoints;
        //Click event
        private Rectangle clickRectangle;
        private static bool ok = false;
        private int clickedShape = -1;
        //Fill event
        private int method = 1;//flood fill by default
        public Form1()
        {
            InitializeComponent();
            p1 = new Point();
            p2 = new Point();
            StartDownLocation = new Point();
            userColor = Color.Black;
            idShape = 0;
            selectRectangle = new Rectangle();
            clickRectangle = new Rectangle();
            _canvas = new Bitmap(1032, 491, PixelFormat.Format32bppRgb);
            Graphics g = Graphics.FromImage(_canvas);
            g.Clear(Color.White);
           
        }

        //Pick color
        private void btn_Pallete_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                userColor = colorDialog.Color;
            }
        }

        private void fillShape(Graphics g, Pen myPen, int i, Point startPoint)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (listShapes[i].id == 1)
            {
                CircleClass cl = (CircleClass)listShapes[i];
                if (ok)
                {
                    cl.fillCircle(g, myPen, startPoint);
                }
            }
            else if (listShapes[i].id == 2)
            {
                if (ok)
                {
                    RectangleClass cl = (RectangleClass)listShapes[i];
                    cl.fillRectangle(g, myPen, method, startPoint);
                }
            }
            else if (listShapes[i].id == 3)
            {
                if (ok)
                {
                    EllipseClass cl = (EllipseClass)listShapes[i];
                    cl.fillEllipse(g, myPen, startPoint);
                }
            }
            else if (listShapes[i].id == 4)
            {
                if (ok)
                {
                    PolygonClass pl = (PolygonClass)listShapes[i];
                    pl.fillPolygon(g, myPen, method, startPoint);
                }
            }
            else
            {
                LineClass lc = (LineClass)listShapes[i];
                lc.shapeColor = myPen.Color;
                lc.drawShape(g, myPen);
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            timerBox.Text = ts.Milliseconds.ToString() + " ms";
            this.Invalidate();
        }
        private void reDraw(Graphics g, Pen myPen)
        {
            for (int i = 0; i < listShapes.Count; i++)
            {
                listShapes[i].drawShape(g, myPen);
                if (listShapes[i].isFill)
                {
                    ok = true;
                    Pen fillPen = new Pen(listShapes[i].fillColor);
                    fillShape(g, fillPen, i, listShapes[i].startColoring);
                    ok = false;
                }
                
            }
        }
        //Shape = Line
        private void btn_Line_Click(object sender, EventArgs e) {
            if (drawCase == "Drawing")
                idShape = 0;
            else
                MessageBox.Show("Please pick Drawing to use this button", "Alert");
        }
        //Shape = Circle
        private void btn_Circle_Click(object sender, EventArgs e) {
            if (drawCase == "Drawing")
                idShape = 1;
            else
                MessageBox.Show("Please pick Drawing to use this button", "Alert");
        }
        private void btnRectangle_Click(object sender, EventArgs e) {
            if (drawCase == "Drawing")
                idShape = 2;
            else
                MessageBox.Show("Please pick Drawing to use this button", "Alert");
        }
        private void btn_Ellipse_Click(object sender, EventArgs e) {
            if (drawCase == "Drawing")
                idShape = 3;
            else
                MessageBox.Show("Please pick Drawing to use this button", "Alert");
        }

        private void polygonBtn_Click(object sender, EventArgs e)
        {
            if (drawCase == "Drawing")
                idShape = 4;
            else
                MessageBox.Show("Please pick Drawing to use this button", "Alert");
        }
        private void clickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawCase = "Click";
        }
        //Copy state
        private void copyShape_Click(object sender, EventArgs e)
        {
            drawCase = "Copy";
        }
        //Move state
        private void moveShape_Click(object sender, EventArgs e)
        {
            drawCase = "Move";
        }
        //Edit state
        private void editShape_Click(object sender, EventArgs e)
        {
            drawCase = "Edit";
        }
        //Select state
        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawCase = "Select";
        }
        //Draw state
        private void shapeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawCase = "Drawing";
        }

        private void picturebox_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            mStart_X = e.X;
            mStart_Y = e.Y;
            mEnd_X = e.X;
            mEnd_Y = e.Y;
            StartDownLocation = e.Location;
        }

        private Pen fillPen;
        private void btnFill_Click(object sender, EventArgs e)
        {
            if (clickedShape != -1)
            {
                fillPen = new Pen(userColor);

                if (drawCase == "Click")
                {
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        fillPen.Color = colorDialog.Color;
                        MessageBox.Show("Click inside shape to fill");
                        drawCase = "fillToughImage";
                    }
                }
                else
                    MessageBox.Show("Please click and select a shape first");

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            drawCase = "Drawing";
            MessageBox.Show("Clear all and set draw case to Drawing");
            listShapes.Clear();
            Graphics g = Graphics.FromImage(_canvas);
            g.Clear(Color.White);
            pictureBox.Refresh();
            btnScale.Checked = false;
            rotateBtn.Checked = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ok = false;
            listShapes[clickedShape].isFill = true;
            listShapes[clickedShape].fillColor = fillPen.Color;
            drawCase = "Click";
        }

        private void fillBox_SelectedIndexChange(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.Equals("Boundary Fill"))
                method = 2;
            else if (comboBox1.SelectedItem.Equals("Flood Fill"))
                method = 1;
        }

        private bool isScale = false;
        private bool isRotate = false;
        private bool isMove = false;
        private bool isReflect = false;

        private void scaleButton_CheckedChanged(object sender, EventArgs e)
        {
            if (btnScale.Checked)
            {
                isScale = true;
                MessageBox.Show("Select a shape to scale");
                drawCase = "Edit";
            }
            else
            {
                isScale = false;
            }
        }

        private void rotateButton_checkChanged(object sender, EventArgs e)
        {
            if (rotateBtn.Checked)
            {
                isRotate = true;
                MessageBox.Show("Select a shape to rotate");
                drawCase = "Edit";
            }
            else
                isRotate = false;
        }

        private void moveBtn_checkChanged(object sender, EventArgs e)
        {
            if (moveBtn.Checked)
            {
                isMove = true;
                MessageBox.Show("Select a shape to move");
                drawCase = "Move";
            }
            else
                isMove = false;
        }
        private void ref_Ox_Btn_CheckedChanged(object sender, EventArgs e)
        {
            if (reflectBtn.Checked)
            {
                isReflect = true;
                MessageBox.Show("Select a shape to reflect");
                drawCase = "Edit";
            }
            else
                isMove = false;
        }

        private void picturebox_MouseUp(object sender, MouseEventArgs e)
        {
            Pen myPen = new Pen(userColor, 1);
            isMouseDown = false;
            
            if (e.Button == MouseButtons.Left)
            {
                switch (drawCase)
                {
                    case "Drawing":
                        //Line
                        if (idShape == 0)
                        {
                            //Draw new line
                            LineClass line = new LineClass(mStart_X, mEnd_X, mStart_Y, mEnd_Y, userColor);
                            listShapes.Add(line);
                            using (Graphics g = Graphics.FromImage(_canvas))
                            {
                                line.drawShape(g, myPen);
                            }
                        }
                        //Circle
                        else if (idShape == 1)
                        {
                            CircleClass circle = new CircleClass(mStart_X, mStart_Y, mEnd_X, mEnd_Y, userColor, false, Color.White);
                            listShapes.Add(circle);
                            using (Graphics g = Graphics.FromImage(_canvas))
                            {
                                circle.drawShape(g, myPen);
                            }
                        }
                        //Rectangle
                        else if (idShape == 2)
                        {
                            RectangleClass rectangle = new RectangleClass(mStart_X, mEnd_X, mStart_Y, mEnd_Y, userColor, false, Color.White);
                            listShapes.Add(rectangle);
                            using (Graphics g = Graphics.FromImage(_canvas))
                            {
                                rectangle.drawShape(g, myPen);
                            }
                        }
                        //Ellipse
                        else if (idShape == 3)
                        {
                            EllipseClass ellipse = new EllipseClass(mStart_X, mEnd_X, mStart_Y, mEnd_Y, userColor, false, Color.White);
                            listShapes.Add(ellipse);
                            using (Graphics g = Graphics.FromImage(_canvas))
                            {
                                ellipse.drawShape(g, myPen);
                            }
                        }
                        //Polygon
                        else if (idShape == 4)
                        {
                            if (listPoints == null)
                                listPoints = new List<Point>();
                            listPoints.Add(new Point(mEnd_X, mEnd_Y));
                        }
                        break;
                    case "Select":
                        listSelectedShapes.Clear();
                        for (int i = 0; i < listShapes.Count; i++)
                        {
                            if (selectRectangle.Contains(listShapes[i].startPoint)
                                && selectRectangle.Contains(listShapes[i].endPoint))
                            {
                                listSelectedShapes.Add(i);
                            }
                        }
                        break;
                    case "Copy":
                        //Copy shape
                        if (listSelectedShapes.Count == 0)
                            MessageBox.Show("Please select a shape first", "Error");
                        else
                        {
                            for (int i = 0; i < listSelectedShapes.Count; i++)
                            {
                                Shape tmpShape = listShapes[listSelectedShapes[i]];
                                idShape = tmpShape.id;
                                //Copy shape
                                if (idShape == 0)
                                {
                                    LineClass copyLine = new LineClass(p1.X + tmpShape.startPoint.X, p2.X + tmpShape.endPoint.X,
                                        p1.Y + tmpShape.startPoint.Y, p2.Y + tmpShape.endPoint.Y, tmpShape.shapeColor);
                                    listShapes.Add(copyLine);
                                    using (Graphics g = Graphics.FromImage(_canvas))
                                    {
                                        copyLine.drawShape(g, myPen);
                                        if (copyLine.isFill)
                                        {
                                            Pen fillPen = new Pen(copyLine.fillColor);
                                            
                                            fillShape(g, fillPen, listShapes.Count - 1, selectRectangle.Location);
                                            fillPen.Dispose();
                                        }
                                    }
                                }
                                else if (idShape == 1)
                                {
                                    CircleClass copyCircle = new CircleClass(p1.X + tmpShape.startPoint.X, p1.Y + tmpShape.startPoint.Y,
                                        p2.X + tmpShape.endPoint.X, p2.Y + tmpShape.endPoint.Y, tmpShape.shapeColor,
                                        tmpShape.isFill, tmpShape.fillColor);
                                    listShapes.Add(copyCircle);
                                    using (Graphics g = Graphics.FromImage(_canvas))
                                    {
                                        copyCircle.drawShape(g, myPen);
                                        if (copyCircle.isFill)
                                        {
                                            ok = true;
                                            Pen fillPen = new Pen(copyCircle.fillColor);
                                            Point newPoint = new Point(tmpShape.startColoring.X + p2.X,
                                                tmpShape.startColoring.Y + p2.Y);
                                            copyCircle.setColorMap(tmpShape.coloredPixels);
                                            fillShape(g, fillPen, listShapes.Count - 1, newPoint);
                                            fillPen.Dispose();
                                            ok = false;
                                        }
                                    }
                                }
                                else if (idShape == 2)
                                {
                                    RectangleClass copyRectangle = new RectangleClass(p1.X + tmpShape.startPoint.X, p2.X + tmpShape.endPoint.X,
                                        p1.Y + tmpShape.startPoint.Y, p2.Y + tmpShape.endPoint.Y, tmpShape.shapeColor, tmpShape.isFill,
                                        tmpShape.fillColor);
                                   
                                    using (Graphics g = Graphics.FromImage(_canvas))
                                    {
                                        copyRectangle.drawShape(g, myPen);
                                        if (copyRectangle.isFill)
                                        {
                                            ok = true;
                                            Pen fillPen = new Pen(copyRectangle.fillColor);
                                            Point newPoint = new Point(tmpShape.startColoring.X + p2.X,
                                                tmpShape.startColoring.Y + p2.Y);
                                            copyRectangle.setColorMap(tmpShape.coloredPixels);
                                            fillShape(g, fillPen, listShapes.Count - 1, newPoint);
                                            fillPen.Dispose();
                                            ok = false;
                                        }
                                    }
                                    listShapes.Add(copyRectangle);
                                }
                                else if (idShape == 3)
                                {
                                    EllipseClass copyEllipse = new EllipseClass(p1.X + tmpShape.startPoint.X, p2.X + tmpShape.endPoint.X,
                                        p1.Y + tmpShape.startPoint.Y, p2.Y + tmpShape.endPoint.Y, tmpShape.shapeColor, tmpShape.isFill,
                                        tmpShape.fillColor);
                                    listShapes.Add(copyEllipse);
                                    using (Graphics g = Graphics.FromImage(_canvas))
                                    {
                                        copyEllipse.drawShape(g, myPen);
                                        if (copyEllipse.isFill)
                                        {
                                            ok = true;
                                            Pen fillPen = new Pen(copyEllipse.fillColor);
                                            Point newPoint = new Point(tmpShape.startColoring.X + p2.X,
                                               tmpShape.startColoring.Y + p2.Y);
                                            copyEllipse.setColorMap(tmpShape.coloredPixels);
                                            fillShape(g, fillPen, listShapes.Count - 1, newPoint);
                                            fillPen.Dispose();
                                            ok = false;
                                        }
                                    }
                                }
                                else if (idShape == 4)
                                {
                                    List<Point> tmpList = new List<Point>();
                                    for (int num = 0; num < tmpShape.controlPoints.Count; num++)
                                        tmpList.Add(new Point(tmpShape.controlPoints[num].X + p2.X, tmpShape.controlPoints[num].Y + p2.Y));

                                    PolygonClass copyPolygon = new PolygonClass(tmpList, tmpShape.shapeColor,
                                        tmpShape.isFill, tmpShape.shapeColor);
                                    listShapes.Add(copyPolygon);
                                    using (Graphics g = Graphics.FromImage(_canvas))
                                    {
                                        copyPolygon.drawShape(g, myPen);
                                        if (copyPolygon.isFill)
                                        {
                                            ok = true;
                                            //clickRectangle.Location = p2;
                                            Pen fillPen2 = new Pen(copyPolygon.fillColor);
                                            Point newPoint = new Point(tmpShape.startColoring.X + p2.X,
                                               tmpShape.startColoring.Y + p2.Y);
                                            copyPolygon.setColorMap(tmpShape.coloredPixels);
                                            fillShape(g, fillPen2, listShapes.Count - 1, newPoint);
                                            fillPen2.Dispose();
                                            ok = false;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "Move":
                        //Move shape
                        if (clickedShape > -1)
                        {
                            Shape tmpShape = listShapes[clickedShape];
                            tmpShape.moveShape(new Point(mEnd_X, mEnd_Y));
                            listShapes[clickedShape] = tmpShape;
                            using (Graphics g = Graphics.FromImage(_canvas))
                            {
                                g.Clear(Color.White);
                                Pen linePen = new Pen(tmpShape.shapeColor);
                                reDraw(g, linePen);
                                linePen.Dispose();
                            }
                        }
                        break;
                    case "Click":
                        clickRectangle.Location = new Point(e.X - 10, e.Y - 10);
                        clickRectangle.Size = new Size(20, 20);
                        

                        for (int i = 0; i < listShapes.Count; i++)
                        {
                            for (int j = 0; j < listShapes[i].listGrid.Count; j++)
                            {
                                if (clickRectangle.Contains(listShapes[i].listGrid[j]))
                                {
                                    clickedShape = i;
                                    break;
                                }
                            }
                        }
                        pictureBox.Refresh();
                        break;
                    case "fillToughImage":
                        clickRectangle.Location = new Point(e.X, e.Y);
                        clickRectangle.Size = new Size(2, 2);
                        ok = true;
                        using (Graphics g = Graphics.FromImage(_canvas))
                        {
                            fillShape(g, fillPen, clickedShape, clickRectangle.Location);
                            this.pictureBox.Refresh();
                        }
                        break;
                    case "Edit":
                        clickRectangle.Location = new Point(mStart_X - 10, mStart_Y - 10);
                        clickRectangle.Size = new Size(20, 20);
                        if (clickedShape > -1)
                        {
                            Shape tmpShape = listShapes[clickedShape];
                            for (int num = 0; num < tmpShape.controlPoints.Count; num++)
                            {
                                if (clickRectangle.Contains(tmpShape.controlPoints[num]))
                                {
                                    if (isScale)
                                    {
                                        tmpShape.scaleShape(tmpShape.controlPoints[num], new Point(mEnd_X, mEnd_Y));
                                        listShapes[clickedShape] = tmpShape;
                                        using (Graphics g = Graphics.FromImage(_canvas))
                                        {
                                            g.Clear(Color.White);
                                            Pen linePen = new Pen(tmpShape.shapeColor);
                                            reDraw(g, linePen);
                                            linePen.Dispose();
                                        }
                                    }
                                    else if (isRotate)
                                    {
                                        if (tmpShape.id == 2)
                                        {
                                            RectangleClass tmpRect = (RectangleClass)tmpShape;
                                            tmpRect.rotateShape(new Point(mEnd_X, mEnd_Y));
                                            listShapes[clickedShape] = tmpShape;
                                            using (Graphics g = Graphics.FromImage(_canvas))
                                            {
                                                g.Clear(Color.White);
                                                Pen linePen = new Pen(tmpShape.shapeColor);
                                                reDraw(g, linePen);
                                                linePen.Dispose();
                                            }
                                        }
                                        else if(tmpShape.id == 4)
                                        {
                                            PolygonClass tmpPoly = (PolygonClass)tmpShape;
                                            tmpPoly.rotateShape(new Point(mEnd_X, mEnd_Y));
                                            listShapes[clickedShape] = tmpShape;
                                            using (Graphics g = Graphics.FromImage(_canvas))
                                            {
                                                g.Clear(Color.White);
                                                Pen linePen = new Pen(tmpShape.shapeColor);
                                                reDraw(g, linePen);
                                                linePen.Dispose();
                                            }
                                        }
                                        angleBox.Value = Convert.ToDecimal(tmpShape.oldAngle);
                                    }
                                    else if (isReflect)
                                    {
                                        if (tmpShape.reflectShape(tmpShape.controlPoints[num]))
                                        {
                                            listShapes[clickedShape] = tmpShape;
                                            using (Graphics g = Graphics.FromImage(_canvas))
                                            {
                                                g.Clear(Color.White);
                                                Pen linePen = new Pen(tmpShape.shapeColor);
                                                reDraw(g, linePen);
                                                linePen.Dispose();
                                            }
                                            pictureBox.Invalidate();
                                            MessageBox.Show("Back to edit mode", "Successfully");
                                            isReflect = false;
                                            reflectBtn.Checked = false;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Control point is invalid", "Failed");
                                            isReflect = false;
                                            reflectBtn.Checked = false;
                                            break;
                                        }
                          
                                    }
                                    //isScale = false;
                                   // btnScale.Checked = false;
                                }
                            }
                        }
                        break;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (drawCase == "Drawing")
                {
                    if (idShape == 4)
                    {
                        if (listPoints.Count != 0)
                        {
                            this.contextMenuStrip.Enabled = false;
                            PolygonClass polygon = new PolygonClass(listPoints, userColor, false, Color.White);
                            listShapes.Add(polygon);
                            using (Graphics g = Graphics.FromImage(_canvas))
                                polygon.drawShape(g, myPen);
                            listPoints.Clear();
                        }
                    }
                }
            }
            pictureBox.Invalidate();
            this.contextMenuStrip.Enabled = true;

        }

        private void picturebox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == false) return;
            mEnd_X = e.X;
            mEnd_Y = e.Y;
            
            switch(drawCase)
            {
                case "Drawing":
                    break;
                case "Edit":
                    p1.X = e.X - StartDownLocation.X;
                    p1.Y = e.Y - StartDownLocation.Y;
                    p2.X = e.X - StartDownLocation.X;
                    p2.Y = e.Y - StartDownLocation.Y;
                    break;
                case "Copy":
                    p1.X = e.X - StartDownLocation.X;
                    p1.Y = e.Y - StartDownLocation.Y;
                    p2.X = e.X - StartDownLocation.X;
                    p2.Y = e.Y - StartDownLocation.Y;
                    break;
                case "Select":
                    //Rectangle for selection
                    selectRectangle.Location = new Point(
                        Math.Min(mStart_X, mEnd_X),
                        Math.Min(mStart_Y, mEnd_Y));
                    selectRectangle.Size = new Size(
                        Math.Abs(mStart_X - mEnd_X),
                        Math.Abs(mStart_Y - mEnd_Y));
                    break;
                case "Move":
                    p1.X = e.X - StartDownLocation.X;
                    p1.Y = e.Y - StartDownLocation.Y;
                    p2.X = e.X - StartDownLocation.X;
                    p2.Y = e.Y - StartDownLocation.Y;
                    
                    break;
            }
            labelPointer.Text = "p1: (" + mStart_X + ", " + mStart_Y + ")" + " p2: (" + mEnd_X + ", " + mEnd_Y + ")";
            pictureBox.Invalidate();
        }
        void drawDashShapes(Pen myPen, Shape tmpShape, Graphics g, int id)
        {
            if (id == 0)
                g.DrawLine(myPen, tmpShape.startPoint.X + p1.X, tmpShape.startPoint.Y + p1.Y,
                    tmpShape.endPoint.X + p2.X, tmpShape.endPoint.Y + p2.Y);
            else if (id == 1)
                g.DrawEllipse(myPen, tmpShape.startPoint.X + p1.X, tmpShape.startPoint.Y + p1.Y,
                    (tmpShape.endPoint.X - tmpShape.startPoint.X + p2.X - p1.X) * 2,
                    (tmpShape.endPoint.Y + p2.Y - tmpShape.startPoint.Y - p1.Y) * 2);
            else if (id == 2)
                g.DrawRectangle(myPen, tmpShape.startPoint.X + p1.X, tmpShape.startPoint.Y + p1.Y,
                    tmpShape.endPoint.X - tmpShape.startPoint.X + p2.X - p1.X,
                    tmpShape.endPoint.Y - tmpShape.startPoint.Y + p2.Y - p1.Y);
            else if (id == 3)
                g.DrawEllipse(myPen, tmpShape.startPoint.X + p1.X, tmpShape.startPoint.Y + p1.Y,
                    tmpShape.endPoint.X - tmpShape.startPoint.X + p2.X - p1.X,
                    tmpShape.endPoint.Y - tmpShape.startPoint.Y + p2.Y - p1.Y);
        }


        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(userColor, 2);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            /*RectangleClass rect = new RectangleClass(273, 368, 131, 182, Color.Black, false, Color.White);
            listShapes.Add(rect);
            using (Graphics x = Graphics.FromImage(_canvas))
            {
                rect.drawShape(x, myPen);
            }*/
        

            //e.Graphics.DrawImage(_canvas, 0, 0);
            if (listShapes.Count > length)
            {
                using (Bitmap tmp = new Bitmap(_canvas))
                {
                    using (Graphics x = Graphics.FromImage(tmp))
                    {
                        listShapes[listShapes.Count - 1].drawShape(x, myPen);
                        e.Graphics.DrawImage(tmp, 0, 0);
                    }
                }
                length++;
            }
            else
            {
                e.Graphics.DrawImage(_canvas, 0, 0);
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            timerBox.Text = ts.Milliseconds.ToString() + " ms";
            //Select brush
            Brush selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));
            //Copy pen
            Pen copyPen = new Pen(Color.Blue, 1);

            switch (drawCase)
            {
                case "Select":
                    if (selectRectangle != null && selectRectangle.Width > 0 && selectRectangle.Height > 0)            
                        e.Graphics.FillRectangle(selectionBrush, selectRectangle);
                    break;
                case "Drawing":
                    if (isMouseDown) {
                        switch (idShape) {
                            //Outline line segment
                            case 0:
                                g.DrawLine(myPen, mStart_X, mStart_Y, mEnd_X, mEnd_Y);
                                break;
                            //Outline Circle
                            case 1:
                                g.DrawEllipse(myPen, mStart_X, mStart_Y, (mEnd_X - mStart_X) * 2, (mEnd_Y - mStart_Y) * 2);
                                break;
                            //Outline Rectangle
                            case 2:
                                g.DrawRectangle(myPen, mStart_X, mStart_Y, mEnd_X - mStart_X, mEnd_Y - mStart_Y);
                                break;
                            case 3:
                                g.DrawEllipse(myPen, mStart_X, mStart_Y, (mEnd_X - mStart_X) * 2, (mEnd_Y - mStart_Y) * 2);
                                break;
                        }
                    }
                    if (idShape == 4)
                    {
                        if (listPoints != null && listPoints.Count != 0)
                        {
                            for (int n = 0; n < listPoints.Count; n++)
                            {
                                g.DrawEllipse(myPen, listPoints[n].X - 2, listPoints[n].Y - 2, 4, 4);
                            }
                        }
                    }
                    break;
                case "Copy":
                    if (isMouseDown) {
                        for (int i = 0; i < listSelectedShapes.Count; i++)
                        {
                            Shape tmpShape = listShapes[listSelectedShapes[i]];
                            idShape = tmpShape.id;
                            drawDashShapes(copyPen, tmpShape, g, idShape);
                        }
                    }
                    break;
                case "Move":
                    if (isMouseDown)
                    {
                        for (int i = 0; i < listSelectedShapes.Count; i++)
                        {
                            Shape tmpShape = listShapes[listSelectedShapes[i]];
                            idShape = tmpShape.id;
                            drawDashShapes(copyPen, tmpShape, g, idShape);
                        }
                    }
                    break;
                case "Click":
                    if (clickedShape != -1)
                    {
                        for (int i = 0; i < listShapes[clickedShape].controlPoints.Count; i++)
                        {
                            Point tmp = listShapes[clickedShape].controlPoints[i];
                            myPen.Color = Color.Red;
                            e.Graphics.DrawEllipse(myPen, tmp.X - 2, tmp.Y - 2, 4, 4);
                        }
                    }
                    break;
                case "Edit":
                    if (clickedShape != -1)
                    {
                        for (int i = 0; i < listShapes[clickedShape].controlPoints.Count; i++)
                        {
                            Point tmp = listShapes[clickedShape].controlPoints[i];
                          
                            myPen.Color = Color.Red;
                         
                            e.Graphics.DrawEllipse(myPen, tmp.X + 2, tmp.Y + 2, 4, 4);
                        }
                    }
                    break;
            }
            myPen.Dispose();
        }
    }
}
