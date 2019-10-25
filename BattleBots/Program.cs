using System;
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
            
            Console.WriteLine("Welcome to the land of Poké.. errr I mean Battlebots \nWhat is your name?");
            string user = Console.ReadLine();
            Console.WriteLine("Which starter would you like to choose?");
            string Starter = Console.ReadLine();
            BattleBot Trainer = new BattleBot(user, Starter);

        }
    }
}
