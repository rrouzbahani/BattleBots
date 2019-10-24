using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBots
{
    abstract class Vehicle : Player
    {
        public int Fuel { get; set; } = 50;
        public int HP { get; set; } = 50;
        public void FuelDown(int amount)
        {
            Fuel-= amount;
        }
        public void HPDown(int HPDecrease)
        {
            HP -= HPDecrease;
        }
        abstract public void Heal();
        abstract public void Refuel();
    }
}
