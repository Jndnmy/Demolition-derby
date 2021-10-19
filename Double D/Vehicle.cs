using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Double_D
{
    class Vehicle
    {       
        /*remodel
        have a speed
        each tick, 'move', using friction to slow down the car
        if w or s is pressed, acc or decc(braking)
        */
        protected PointF[] coordinates;
        protected Component[] components;
        private PointF centre;
        private double direction; //radians
        private Rectangle hitbox;
        protected int id;
        protected float speed;
        protected float maxSpeed;
        protected float accRate;
        //array of small square class (bits of vehicle)
        //move shit

        //step 1- when key is held move or rotate by set degrees.
        public Vehicle(int x, int y, double direction, float speed, Brush colour)
        {
            this.direction = direction;
            this.speed = speed;
            accRate = 0.15F;
            id = 99; 
            components = new Component[]
            {
                new Component (x,y,99, colour)
            };
            coordinates = new PointF[] {
                new PointF(x - 10, y - 10),      //tl          
                new PointF(x + 10, y - 10),      //tr          
                new PointF(x + 10, y + 10),      //br
                new PointF(x - 10, y + 10),     //bl
                                                };
            centre = new PointF(x, y);
            hitbox = new Rectangle();
            updateHitbox();
        }
        public Vehicle(int x, int y,int id) //constructor
        {
            maxSpeed = 20;
            accRate = 0.15F;
            direction = 0; //facing up;
            components = new Component[] {
                new Component(x - 10, y - 10, id, Brushes.Red),      //tl          
                new Component(x + 10, y - 10, id, Brushes.Red),      //tr          
                new Component(x + 10, y + 10,id, Brushes.Green),      //br
                new Component(x - 10, y + 10,id, Brushes.Green),     //bl 
                new Component(x , y,id, Brushes.Yellow),
            };
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
        public void move()//complicated equations go here
        {
            changeCoords(-Math.Sin(direction)*speed,Math.Cos(direction)*speed);
            if (components != null)
            {
                for (int i = 0; i < components.Length; i++)
                {
                    components[i].changeCoords(-Math.Sin(direction) * speed, Math.Cos(direction) * speed);
                }
            }
        }

        public void accelerate(int forwOrBack)//forward or backwards
        {
            if ((speed >= 0 && (speed < maxSpeed || forwOrBack == 1) ) || (speed < 0 && (speed >= maxSpeed * -1 || forwOrBack == -1)  ))                
                speed += (accRate + (accRate * Math.Abs(speed)))* forwOrBack;
                
                
        }
        public void decelerate()
        {
            if (Math.Abs(speed) < 0.1)
                speed = 0;
            else
                speed -=  speed * accRate * 0.1F;
           
               
        }

        protected void updateHitbox()
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
        
        public void turn(float angle, PointF axis)
        {
            double speedClone = speed * -1;
            double originalAngle = direction * (180 / Math.PI);
            double radAngle = originalAngle + angle;
            radAngle = radAngle * (Math.PI / 180);
            double magnitude = Math.Pow(speedClone,2) * 0.1;

            //speed = (float)Math.Sqrt(Math.Pow((speed * Math.Cos(direction) + magnitude * Math.Cos(radAngle)),2)+Math.Pow((speed * Math.Sin(direction)+ magnitude * Math.Sin(radAngle)),2));
            double Y = (speedClone * Math.Sin(direction) + magnitude * Math.Sin(radAngle));
            double X = (speedClone * Math.Cos(direction) + magnitude * Math.Cos(radAngle));

            double angle2 = Math.Atan2(Y,X);
            //angle2 = (float)(angle2 + Math.PI);
            if (Double.IsNaN(angle2) || speedClone == 0 )
                return; //no rotation to be done
            else
                rotate(angle2 - direction, axis);
        }
        public void rotate(double angle, PointF axis)
        {
            double radAngle, angleBeforeRotation, endAngle, modulus, newX, newY, X, Y;
            //radAngle = Math.PI * angle / 180;
            radAngle = angle;
            direction += radAngle;
            if (direction < 0)
            {
                direction += (2 * Math.PI);
            }
            for (int i = 0; i < coordinates.Length; i++)
            {
                X = coordinates[i].X - axis.X;
                Y = coordinates[i].Y - axis.Y;
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
                coordinates[i].X = (float)(axis.X + newX);
                coordinates[i].Y = (float)(axis.Y - newY);
            }
            updateHitbox();
            if (components != null)
            {
                for (int i = 0; i < components.Length; i++)
                {
                    components[i].rotate(angle, axis);
                }
            }
        }
        public PointF[] getCoords()
        {
            return coordinates;
        }
        public Component[] getComponents()
        {
            return components;
        }
        public float getSpeed()
        {
            return speed;
        }
        public Double getDirection()
        {
            return direction;
        }
        public Rectangle getHitbox()
        {
            return hitbox;
        }
        public PointF getCenter()
        {
            return centre;
        }
        public void draw(Graphics sG)
        {
            for (int i = 0; i < components.Length; i++)
            {
                sG.FillPolygon(components[i].getColour(), components[i].getCoords());
                sG.DrawRectangle(Pens.Black, components[i].getHitbox());
            }
            //return sG;
        }
    }
}
