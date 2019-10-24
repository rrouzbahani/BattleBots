using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBots
{
    abstract class Vehicle : Player
    {
        public const string WEAPON_CIRCULAR_SAW = "Circular Saw";
        public const string WEAPON_CLAW_CUTTER = "Claw Cutter";
        public const string WEAPON_FLAME_THROWER = "Flame Thrower";
        public const string WEAPON_SLEDGE_HAMMER = "Sledge Hammer";
        public const string WEAPON_SPINNNING_BLADE = "Spinning Blade";

        public int Fuel { get; set; } = 50;
        public int HP { get; set; } = 50;
        public string Weapon { get; set; }

        public void FuelDown(int amount)
        {
            Fuel-= amount;
        }
        public void HPDown(int HPDecrease)
        {
            HP -= HPDecrease;
        }
        abstract public void Heal(int HPAddAmount);
        abstract public void Refuel(int FAddAmount);
        public Vehicle()
        {
            Weapon = WEAPON_CIRCULAR_SAW;
            Name = "Ash";
        }
        public Vehicle(string name, string weapon)
        {
            Weapon = weapon;
            Name = name;
        }
    }
}
