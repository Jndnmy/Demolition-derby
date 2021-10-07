using System;
using System.Collections.Generic;
using System.Text;

namespace Double_D
{
    class Controller
    {
        private Vehicle vehicle;
        private string personality;
        int team;
        int target;
        public Controller(Vehicle vehicle, string personality, int team)
        {
            this.Vehicle = vehicle;
            this.Personality = personality;
            this.Team = team;
            target = 0;
        }

        public string Personality { get => personality; set => personality = value; }
        public int Team { get => team; set => team = value; }
        public void Newtarget(List<Controller> targets)
        {
            double distance;
            double 
                maxdistance= 200000;
            for (int i = 0; i < targets.Count; i++)
            {
                distance = Math.Sqrt(Math.Pow(Math.Abs(vehicle.getCenter().Y - targets[i].vehicle.getCenter().Y), 2) + Math.Pow(Math.Abs(vehicle.getCenter().X - targets[i].vehicle.getCenter().X), 2));
                if ((distance > 0) & (distance < maxdistance)) {
                    target = i;
                    maxdistance = distance;
                }
            }
        }

        public void Forward()
        {
            Vehicle.accelerate(1);
        }
        public void Reverse()
        {
            Vehicle.accelerate(-1);
        }
        public void Turnright(){
            Vehicle.turn(10, Vehicle.getCenter());
            }
        public void Turnleft()
        {
            Vehicle.turn(-10, Vehicle.getCenter());
        }
        internal Vehicle Vehicle { get => vehicle; set => vehicle = value; }
    }
}
