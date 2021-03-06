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
             
            newCar = new Vehicle(this.Width / 2, this.Height / 2, 0);

            controller.Add(new Controller(newCar, "Player", 0));
            newCar = new Vehicle(this.Width / 4, this.Height / 4, 0);
            controller.Add(new Controller(newCar, "Violent", 1));
            sG = Graphics.FromImage(background);
            g = this.CreateGraphics();
            Timer t = new Timer();
            t.Interval = 100;
            t.Tick += T_Tick;
            t.Start();
           
        }
        private void CheckDm()
        {
            int componentNum;
            for (int i =0; i< controller.Count(); i++)
            {
                componentNum = controller[i].Vehicle.componentDestroyed();
                if (componentNum > 0)
                {
                    for (int j = 0; j < componentNum; j++) {
                        controller.Add(new Controller(controller[i].Vehicle.Shed(), "Component", 99));
                    }
                }
            }
        }
        Graphics g;
        Graphics sG;
        Bitmap background;
        Vehicle newCar;
        List<Controller> controller = new List<Controller>();
        bool up, down, left, right,ctrl,k;

        private void T_Tick(object sender, EventArgs e)
        {
            if (up == true)
                controller[0].Forward(); //int equal to -1 or 1
            if (down == true)
                controller[0].Reverse();
            if (left == true)
                controller[0].Turnleft();
            if (right == true)
                controller[0].Turnright();

            if (up == false && down == false)
            {
                controller[0].Vehicle.decelerate();
            }
            if (k == true)
            {
                controller[0].Vehicle.selfdestruct();
            }

            controller[0].Vehicle.move();
            foreach (Controller movecon in controller)
            {

                movecon.Move(controller);
                movecon.Vehicle.move();
            }
            sG.Clear(Color.FromArgb(255, Color.White));
            CheckDm();//probably should have some of this stuff not fire every tick
            foreach (Controller drawCon in controller)
            {
                drawCon.Vehicle.draw(sG);//Iterate through controller
                
            }

           //sG.DrawRectangle(Pens.Black, ourCar.getHitbox());
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
            if (e.KeyCode == Keys.K) { k = true; }
            if (e.KeyCode == Keys.Control) { ctrl = true; }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { up = false; }
            if (e.KeyCode == Keys.A) { left = false; }
            if (e.KeyCode == Keys.S) { down = false; }
            if (e.KeyCode == Keys.D) { right = false; }
            if (e.KeyCode == Keys.K) { k = false; }
            if (e.KeyCode == Keys.Control) { ctrl = false; }
        }

    }
    
}


