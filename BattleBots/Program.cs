﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBots
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Game game = new Game();
            BattleBot bot = game.PromptUserForBot();
            SpeakingConsole.WriteLine("Bot stats:\nName: " + bot.Name + ",\nWeapon: " + bot.Weapon + ",\nCondition Level: " + bot.HP + ",\nFuel Level: " + bot.Fuel);
            game.Battle(ref bot);
            Console.ReadLine();

        }
    }
}
