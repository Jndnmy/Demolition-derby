using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace Double_D
{
    class Component : Vehicle
    {
        private Brush colour;
        private string attribute;
        private int health;
        private PointF center;
        public Component(int x, int y, int parent, Brush colour) : base() //constructor
        {
            Center = new PointF(x, y);
            coordinates = new PointF[] {
                new PointF(x - 10, y - 10),      //tl          
                new PointF(x + 10, y - 10),      //tr          
                new PointF(x + 10, y + 10),      //br
                new PointF(x - 10, y + 10),     //bl 
            };
            id = parent;
            this.Colour = colour;
            this.health = 150;
 
        }
        public Component(float x, float y, int parent, Brush colour) : base() //constructor
        {
            Center = new PointF(x, y);
            coordinates = new PointF[] {
                new PointF(x - 10, y - 10),      //tl          
                new PointF(x + 10, y - 10),      //tr          
                new PointF(x + 10, y + 10),      //br
                new PointF(x - 10, y + 10),     //bl 
            };
            id = parent;
            this.Colour = colour;
            this.health = 150;
        }

        public int Health { get => health; set => health = value; }
        public PointF Center { get => center; set => center = value; }
        public Brush Colour { get => colour; set => colour = value; }

        public Brush getColour()
        {
            return Colour;
        }

    }
}
