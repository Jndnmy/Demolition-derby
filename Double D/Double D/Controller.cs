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
        public Controller(Vehicle vehicle, string personality, int team)
        {
            this.Vehicle = vehicle;
            this.Personality = personality;
            this.Team = team;
        }

        public string Personality { get => personality; set => personality = value; }
        public int Team { get => team; set => team = value; }
        internal Vehicle Vehicle { get => vehicle; set => vehicle = value; }
    }
}
