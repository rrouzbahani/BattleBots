using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBots
{
    class Vehicle : Player
    {
        public int Fuel {get;set;}
        public int HP {get;set;}
        public int Points {get;set;}

        public void FuelDown()
        {
            Fuel--;
        }
        public void HPDown()
        {
            HP--;
        }
        public void PointsUp()
        {
            Points++;
        }
        public void HPUp()
        {
            HP++;
        }
        public void FuelUp()
        {
            Fuel++;
        }
        abstract 
    }
}
