﻿using System;
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
            ourCar = new Vehicle(this.Width/2,this.Height/2,0);
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
        Vehicle ourCar;
        bool up, down, left, right;
      
        private void T_Tick(object sender, EventArgs e)
        {
            if (up == true) {
                ourCar.move(-15);
                for (int i = 0; i < ourCar.getComponents().Length - 2; i++) { 
                    ourCar.getComponents()[i].move(-15); }}
            if (down == true) {
                ourCar.move(15);
                for (int i = 0; i < ourCar.getComponents().Length - 2; i++)
                {
                    ourCar.getComponents()[i].move(15);
                }
            }
            if (left == true)
            {
                ourCar.rotate(-15, ourCar.getCenter());
                for (int i = 0; i < ourCar.getComponents().Length - 2; i++)
                {
                    ourCar.getComponents()[i].rotate(-15,ourCar.getCenter());
                }
            }
            if (right == true)
            {
                ourCar.rotate(15, ourCar.getCenter());
                for (int i = 0; i < ourCar.getComponents().Length - 2; i++)
                {
                    ourCar.getComponents()[i].rotate(15, ourCar.getCenter());
                } }
            sG.Clear(Color.FromArgb(255, Color.White));
            for (int i = 0; i < ourCar.getComponents().Length-2; i++)
            {
               sG.FillPolygon(Brushes.Green, ourCar.getComponents()[i].getCoords());
                sG.DrawRectangle(Pens.Black, ourCar.getComponents()[i].getHitbox());
              
            }
            g.DrawImage(background, new Point(0, 0));
            //ourCar.rotate(5);

        }
         
        private bool checkColl(Rectangle rect1, Rectangle rect2)
        {
            return rect1.IntersectsWith(rect2);
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

