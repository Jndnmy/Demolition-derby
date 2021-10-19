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
        public Component(int x, int y, int parent, Brush colour) : base() //constructor
        {
            coordinates = new PointF[] {
                new PointF(x - 10, y - 10),      //tl          
                new PointF(x + 10, y - 10),      //tr          
                new PointF(x + 10, y + 10),      //br
                new PointF(x - 10, y + 10),     //bl 
            };
            id = parent;
            this.colour = colour;
            components = null;
        }
        public Brush getColour()
        {
            return colour;
        }

    }
}
