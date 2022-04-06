using SharpGL;
using SharpGL.SceneGraph;
namespace _19127044_Lab02
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_Line = new System.Windows.Forms.Button();
            this.btn_Circle = new System.Windows.Forms.Button();
            this.btn_Pallete = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnRectangle = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.timerBox = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnFill = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.polygonButton = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.angleBox = new System.Windows.Forms.NumericUpDown();
            this.btnScale = new System.Windows.Forms.RadioButton();
            this.rotateBtn = new System.Windows.Forms.RadioButton();
            this.labelPointer = new System.Windows.Forms.Label();
            this.moveBtn = new System.Windows.Forms.RadioButton();
            this.reflectBtn = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.angleBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Line
            // 
            this.btn_Line.Location = new System.Drawing.Point(17, 16);
            this.btn_Line.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Line.Name = "btn_Line";
            this.btn_Line.Size = new System.Drawing.Size(100, 28);
            this.btn_Line.TabIndex = 1;
            this.btn_Line.Text = "Line";
            this.btn_Line.UseVisualStyleBackColor = true;
            this.btn_Line.Click += new System.EventHandler(this.btn_Line_Click);
            // 
            // btn_Circle
            // 
            this.btn_Circle.Location = new System.Drawing.Point(125, 16);
            this.btn_Circle.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Circle.Name = "btn_Circle";
            this.btn_Circle.Size = new System.Drawing.Size(100, 28);
            this.btn_Circle.TabIndex = 2;
            this.btn_Circle.Text = "Circle";
            this.btn_Circle.UseVisualStyleBackColor = true;
            this.btn_Circle.Click += new System.EventHandler(this.btn_Circle_Click);
            // 
            // btn_Pallete
            // 
            this.btn_Pallete.Location = new System.Drawing.Point(1277, 17);
            this.btn_Pallete.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Pallete.Name = "btn_Pallete";
            this.btn_Pallete.Size = new System.Drawing.Size(100, 28);
            this.btn_Pallete.TabIndex = 3;
            this.btn_Pallete.Text = "Pallete";
            this.btn_Pallete.UseVisualStyleBackColor = true;
            this.btn_Pallete.Click += new System.EventHandler(this.btn_Pallete_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.Color = System.Drawing.Color.White;
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(17, 89);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1191, 456);
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picturebox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picturebox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picturebox_MouseUp);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clickToolStripMenuItem,
            this.shapeToolStripMenuItem,
            this.selectToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.moveToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(119, 124);
            // 
            // clickToolStripMenuItem
            // 
            this.clickToolStripMenuItem.Name = "clickToolStripMenuItem";
            this.clickToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.clickToolStripMenuItem.Text = "Click";
            this.clickToolStripMenuItem.Click += new System.EventHandler(this.clickToolStripMenuItem_Click);
            // 
            // shapeToolStripMenuItem
            // 
            this.shapeToolStripMenuItem.Name = "shapeToolStripMenuItem";
            this.shapeToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.shapeToolStripMenuItem.Text = "Draw";
            this.shapeToolStripMenuItem.Click += new System.EventHandler(this.shapeToolStripMenuItem_Click);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.selectToolStripMenuItem.Text = "Select";
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyShape_Click);
            // 
            // moveToolStripMenuItem
            // 
            this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            this.moveToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.moveToolStripMenuItem.Text = "Move";
            this.moveToolStripMenuItem.Click += new System.EventHandler(this.moveShape_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // btnRectangle
            // 
            this.btnRectangle.Location = new System.Drawing.Point(233, 16);
            this.btnRectangle.Margin = new System.Windows.Forms.Padding(4);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(100, 28);
            this.btnRectangle.TabIndex = 6;
            this.btnRectangle.Text = "Rectangle";
            this.btnRectangle.UseVisualStyleBackColor = true;
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(341, 16);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 7;
            this.button1.Text = "Ellipse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_Ellipse_Click);
            // 
            // timerBox
            // 
            this.timerBox.Location = new System.Drawing.Point(1040, 23);
            this.timerBox.Name = "timerBox";
            this.timerBox.Size = new System.Drawing.Size(168, 22);
            this.timerBox.TabIndex = 8;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Scanline Fill",
            "Flood Fill"});
            this.comboBox1.Location = new System.Drawing.Point(623, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.fillBox_SelectedIndexChange);
            // 
            // btnFill
            // 
            this.btnFill.Location = new System.Drawing.Point(790, 21);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(75, 23);
            this.btnFill.TabIndex = 10;
            this.btnFill.Text = "Fill";
            this.btnFill.UseVisualStyleBackColor = true;
            this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(1278, 54);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 29);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Clear all";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // polygonButton
            // 
            this.polygonButton.Location = new System.Drawing.Point(17, 52);
            this.polygonButton.Margin = new System.Windows.Forms.Padding(4);
            this.polygonButton.Name = "polygonButton";
            this.polygonButton.Size = new System.Drawing.Size(100, 28);
            this.polygonButton.TabIndex = 12;
            this.polygonButton.Text = "Polygon";
            this.polygonButton.UseVisualStyleBackColor = true;
            this.polygonButton.Click += new System.EventHandler(this.polygonBtn_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(623, 54);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(561, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Fill type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(947, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Timer box";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(973, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Angle";
            // 
            // angleBox
            // 
            this.angleBox.Location = new System.Drawing.Point(1040, 61);
            this.angleBox.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.angleBox.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.angleBox.Name = "angleBox";
            this.angleBox.Size = new System.Drawing.Size(120, 22);
            this.angleBox.TabIndex = 18;
            // 
            // btnScale
            // 
            this.btnScale.AutoSize = true;
            this.btnScale.Location = new System.Drawing.Point(1249, 114);
            this.btnScale.Name = "btnScale";
            this.btnScale.Size = new System.Drawing.Size(64, 21);
            this.btnScale.TabIndex = 21;
            this.btnScale.TabStop = true;
            this.btnScale.Text = "Scale";
            this.btnScale.UseVisualStyleBackColor = true;
            this.btnScale.CheckedChanged += new System.EventHandler(this.scaleButton_CheckedChanged);
            // 
            // rotateBtn
            // 
            this.rotateBtn.AutoSize = true;
            this.rotateBtn.Location = new System.Drawing.Point(1249, 152);
            this.rotateBtn.Name = "rotateBtn";
            this.rotateBtn.Size = new System.Drawing.Size(71, 21);
            this.rotateBtn.TabIndex = 22;
            this.rotateBtn.TabStop = true;
            this.rotateBtn.Text = "Rotate";
            this.rotateBtn.UseVisualStyleBackColor = true;
            this.rotateBtn.CheckedChanged += new System.EventHandler(this.rotateButton_checkChanged);
            // 
            // labelPointer
            // 
            this.labelPointer.AutoSize = true;
            this.labelPointer.Location = new System.Drawing.Point(125, 59);
            this.labelPointer.Name = "labelPointer";
            this.labelPointer.Size = new System.Drawing.Size(53, 17);
            this.labelPointer.TabIndex = 23;
            this.labelPointer.Text = "Pointer";
            // 
            // moveBtn
            // 
            this.moveBtn.AutoSize = true;
            this.moveBtn.Location = new System.Drawing.Point(1249, 188);
            this.moveBtn.Name = "moveBtn";
            this.moveBtn.Size = new System.Drawing.Size(63, 21);
            this.moveBtn.TabIndex = 24;
            this.moveBtn.TabStop = true;
            this.moveBtn.Text = "Move";
            this.moveBtn.UseVisualStyleBackColor = true;
            this.moveBtn.CheckedChanged += new System.EventHandler(this.moveBtn_checkChanged);
            // 
            // reflectBtn
            // 
            this.reflectBtn.AutoSize = true;
            this.reflectBtn.Location = new System.Drawing.Point(1249, 226);
            this.reflectBtn.Name = "reflectBtn";
            this.reflectBtn.Size = new System.Drawing.Size(77, 21);
            this.reflectBtn.TabIndex = 25;
            this.reflectBtn.TabStop = true;
            this.reflectBtn.Text = "Reflect ";
            this.reflectBtn.UseVisualStyleBackColor = true;
            this.reflectBtn.CheckedChanged += new System.EventHandler(this.ref_Ox_Btn_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1390, 554);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.reflectBtn);
            this.Controls.Add(this.moveBtn);
            this.Controls.Add(this.labelPointer);
            this.Controls.Add(this.rotateBtn);
            this.Controls.Add(this.btnScale);
            this.Controls.Add(this.angleBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.polygonButton);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnFill);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.timerBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRectangle);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.btn_Pallete);
            this.Controls.Add(this.btn_Circle);
            this.Controls.Add(this.btn_Line);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.angleBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Line;
        private System.Windows.Forms.Button btn_Circle;
        private System.Windows.Forms.Button btn_Pallete;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shapeToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnRectangle;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox timerBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnFill;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ToolStripMenuItem clickToolStripMenuItem;
        private System.Windows.Forms.Button polygonButton;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown angleBox;
        private System.Windows.Forms.RadioButton btnScale;
        private System.Windows.Forms.RadioButton rotateBtn;
        private System.Windows.Forms.Label labelPointer;
        private System.Windows.Forms.RadioButton moveBtn;
        private System.Windows.Forms.RadioButton reflectBtn;
    }
}

