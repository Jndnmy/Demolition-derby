using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace Double_D
{
    class Component : Vehicle
    {
        private Brush colour;
        public Component(int x, int y, int parent, Brush colour) : base() //constructor
        {
            coordinates = new PointF[] {
                new PointF(x - 20, y - 20),      //tl          
                new PointF(x + 20, y - 20),      //tr          
                new PointF(x + 20, y + 20),      //br
                new PointF(x - 20, y + 20),     //bl 
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
