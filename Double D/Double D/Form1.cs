using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Double_D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {//move to vehicle
            background = new Bitmap(this.Width, this.Height);
            coordinates = new PointF[] {
                new PointF(this.Width / 2 - 20,this.Height / 2 - 20),      //tl          
                new PointF(this.Width / 2 + 20,this.Height / 2 - 20),      //tr          
                new PointF(this.Width / 2 + 20,this.Height / 2 + 20),      //br
                new PointF(this.Width / 2 - 20,this.Height / 2  + 20),     //bl 
            }; 
            centre = new PointF(this.Width / 2, this.Height / 2);
            sG = Graphics.FromImage(background);           
            g = this.CreateGraphics();
            Timer t = new Timer();
            t.Interval = 100;
            t.Tick += T_Tick;
            t.Start();
           
        }

        Graphics g;
        Graphics sG;       
        Bitmap background;        
        PointF[] coordinates;
        PointF centre;
        bool up, down, left, right;
      
        private void T_Tick(object sender, EventArgs e)
        {
            sG.Clear(Color.FromArgb(255, Color.White));
            sG.FillPolygon(Brushes.Green, coordinates);            
            g.DrawImage(background, new Point(0, 0));

            RotatePoints(45);
           
        }
         public void RotatePoints(float angle)
         {      

             double radAngle, angleBeforeRotation, endAngle, modulus, newX, newY, X, Y;
             radAngle = Math.PI * angle / 180;
             for (int i = 0; i < coordinates.Length; i++)
             {
                 X = coordinates[i].X - centre.X;
                 Y = coordinates[i].Y - centre.Y;
                 if (X == 0)                
                 {
                     if (Y < 0)
                         angleBeforeRotation = 2 * Math.PI; 
                     else
                         angleBeforeRotation = Math.PI;                    
                 }
                 else
                 {
                     if (X > 0)
                         angleBeforeRotation = (Math.PI / 2) + Math.Atan(Y / X);
                     else
                         angleBeforeRotation = ((3 * Math.PI) / 2) + Math.Atan(Y / X);                   
                 }
                 endAngle = angleBeforeRotation + radAngle;
                 modulus = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
                 newX = (modulus * Math.Sin(endAngle));
                 newY = (modulus * Math.Cos(endAngle));
                 coordinates[i].X = (float)(centre.X + newX);
                 coordinates[i].Y = (float)(centre.Y - newY);
             }
         }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { up = true; }
            if (e.KeyCode == Keys.A) { left = true; }
            if (e.KeyCode == Keys.S) { down = true; }
            if (e.KeyCode == Keys.D) { right = true; }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { up = false; }
            if (e.KeyCode == Keys.A) { left = false; }
            if (e.KeyCode == Keys.S) { down = false; }
            if (e.KeyCode == Keys.D) { right = false; }
        }
    }
}

