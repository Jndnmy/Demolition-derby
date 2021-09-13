using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Double_D
{
    class Vehicle
    {        
        private PointF[] coordinates;
        private PointF centre;
        private double direction; //radians
        private Rectangle hitbox;
        //array of small square class (bits of vehicle)
        //move shit

        //step 1- when key is held move or rotate by set degrees.
        public Vehicle(int x, int y) //constructor
        {
            direction = 0; //facing up;
            coordinates = new PointF[] {
                new PointF(x - 20, y - 20),      //tl          
                new PointF(x + 20, y - 20),      //tr          
                new PointF(x + 20, y + 20),      //br
                new PointF(x - 20, y + 20),     //bl 
            };
            centre = new PointF(x, y);
            hitbox = new Rectangle();
            updateHitbox();
        }
        public void move(int velocity)//complicated equations go here
        {
            changeCoords(-Math.Sin(direction)*velocity,Math.Cos(direction)*velocity);
        }
        private void updateHitbox()
        {
            float leastX = 10000f;
            float mostX = -10000f;
            float leastY = 10000f;
            float mostY = -10000f;
            for (int i = 0; i < coordinates.Length; i++)
            {
                if (coordinates[i].X > mostX)
                    mostX = coordinates[i].X;
                if (coordinates[i].X < leastX)
                    leastX = coordinates[i].X;
                if (coordinates[i].Y > mostY)
                    mostY = coordinates[i].Y;
                if (coordinates[i].Y < leastY)
                    leastY = coordinates[i].Y;
            }
            float differenceX = mostX - leastX;
            float differenceY = mostY - leastY;           
            hitbox.X = (int)leastX;
            hitbox.Y = (int)leastY;
            hitbox.Width = (int)differenceX;
            hitbox.Height = (int)differenceY;
            

        }
        private void changeCoords(double dx, double dy) //after all move logic is worked out, actually change the coordinates
        {
            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i].X += (float) dx;
                coordinates[i].Y += (float) dy;
            }
            updateCenter();
            updateHitbox();
        }
        private void updateCenter()//reusable code
        {
            float sumX = 0, sumY = 0;
            for (int i =0; i < coordinates.Length; i++)
            {
                sumX += coordinates[i].X;
                sumY += coordinates[i].Y;
            }
            centre.X = sumX / coordinates.Length;
            centre.Y = sumY / coordinates.Length;
        }
        public void rotate(float angle)
        {
            double radAngle, angleBeforeRotation, endAngle, modulus, newX, newY, X, Y;
            radAngle = Math.PI * angle / 180;
            direction += radAngle;
            if (direction < 0)
            {
                direction += (2 * Math.PI);
            }
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
            updateHitbox();
        }
        public PointF[] getCoords()
        {
            return coordinates;
        }
        public Rectangle getHitbox()
        {
            return hitbox;
        }

    }
}
