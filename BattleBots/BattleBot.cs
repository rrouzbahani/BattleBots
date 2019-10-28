using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBots
{
    sealed class BattleBot : Vehicle
    {
        public override void Heal(int HPAddAmount)
        {
            HP += HPAddAmount;
        }
        public override void Refuel(int FAddAmount)
        {
            Fuel += FAddAmount;
        }
        public BattleBot()
        {
            Name = "Mr. Letts";
            Weapon = Game.WEAPON_FLAME_THROWER;
        }
        public BattleBot(string name, string weapon)
        {
            Name = name;
            Weapon = weapon;
        }
    }
}
