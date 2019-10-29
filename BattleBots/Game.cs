using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Threading;
using System.Timers;



namespace BattleBots
{
    class Game
    {
        public const string WEAPON_CIRCULAR_SAW = "Pikachu";
        public const string WEAPON_CLAW_CUTTER = "Squirtle";
        public const string WEAPON_FLAME_THROWER = "Pidgey";
        public const string WEAPON_SLEDGE_HAMMER = "Geodude";
        public const string WEAPON_SPINNNING_BLADE = "Swadloon";

        public static string[] WEAPONS = new string[] { WEAPON_CIRCULAR_SAW, WEAPON_CLAW_CUTTER, WEAPON_FLAME_THROWER, WEAPON_SLEDGE_HAMMER, WEAPON_SPINNNING_BLADE };

        private System.Timers.Timer timer;
        private Random rGen = new Random();
        private int intTimeSinceGameStart;
        private int intBattleStartTime;
        private int intTimeElapsed;
        private bool blnIsBattleSoundPlaying = false;
        System.Media.SoundPlayer openingSound = new System.Media.SoundPlayer(Resource1.Pokemon_Open);
        System.Media.SoundPlayer battleSound = new System.Media.SoundPlayer(Resource1.Pokemon_Battle);
        System.Media.SoundPlayer MeetingOak = new System.Media.SoundPlayer(Resource1.Pokemon_MeetingOak);

        public Game()
        {
            timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            intTimeSinceGameStart++;
            intTimeElapsed = intTimeSinceGameStart - intBattleStartTime;
        }

        public BattleBot PromptUserForBot()
        {
            openingSound.Play();
            Console.WriteLine("Do you want to enable the reading out of all the text?");
            if (Console.ReadLine().Trim().ToLower()[0] != 'y')
            {
                SpeakingConsole.EnableSpeaking = false;
            }
            Console.WriteLine("Welcome to Battle Bots, the theme of this game is Pokémon!\n");
            SpeakingConsole.WriteLine("\nYou will choose a pokémon and are given its strengths, and its weaknesses.\nAfter choosing your pokémon you are set into a battle. Every pokémon has HP and Power Points, more commonly known as PP");
            SpeakingConsole.WriteLine("\nWhen you have ran out of either of those, youre pokémon faints..");
            Console.WriteLine("\nAfter the Music stops press Enter to see what Prof. Oak has in store for us....");
            Console.ReadLine();
            openingSound.Stop();
            MeetingOak.Play();
            SpeakingConsole.WriteLine("Prof.Oak <> Hello there! Welcome to the world of pokémon!\nMy name is Oak! People call me the pokémon Prof!\nThis world is inhabited by creatures called pokémon!/nFor some people, pokémon are pets. Others use them for fights.\nMyself...I study pokémon as a profession.");
            string strName = SpeakingConsole.ReadLine();
            SpeakingConsole.WriteLine("\n Do you see that ball on the table? It's called a Poké Ball.\nIt holds a Pokémon inside. You may have it! Go on, take it! Go ahead, it's yours!");

            foreach (string weapon in WEAPONS)
            {
                string[] beatableWeapons = Array.FindAll(WEAPONS, w => CanBeat(weapon, w));
                SpeakingConsole.WriteLine("\n" + weapon + " Beats " + String.Join(" And ", beatableWeapons));
            }
            string strWeapon;
            while (((strWeapon = SpeakingConsole.ReadLine()) == "" || !IsValidWeapon(strWeapon)) && strName != "")
            {
                SpeakingConsole.WriteLine("Please enter a valid weapon from above");
            }
            MeetingOak.Stop();
            timer.Start();
            intTimeSinceGameStart = 0;
            if (IsValidWeapon(strWeapon))
            {
                if (strName != "")
                {
                    return new BattleBot(strName, GetValidWeaponName(strWeapon));
                }
                else
                {
                    return new BattleBot(GetValidWeaponName(strWeapon));
                }
            }
            else
            {
                return new BattleBot();
            }
        }
        public void Battle(ref BattleBot battleBot)
        {
            if (!blnIsBattleSoundPlaying)
            {
                battleSound.PlayLooping();
                blnIsBattleSoundPlaying = true;
            }
            if (battleBot.Fuel > 0 && battleBot.HP > 0)
            {
                intBattleStartTime = intTimeSinceGameStart;
                string computerWeapon = WEAPONS[rGen.Next(WEAPONS.Length)];
                SpeakingConsole.WriteLine("\nYou are being attacked by a " + computerWeapon + ". What do you do?");
                bool blnValidAction = false;
                while (!blnValidAction)
                {
                    SpeakingConsole.WriteLine("Attack, Defend, or Retreat");
                    string strAction = SpeakingConsole.ReadLine();
                    switch (strAction.Trim().ToLower())
                    {
                        case "attack":
                            blnValidAction = true;
                            if (CanBeat(battleBot.Weapon, computerWeapon))
                            {
                                battleBot.addScore(5);
                            }
                            else
                            {
                                battleBot.HPDown(5);
                            }
                            battleBot.FuelDown(2 * intTimeElapsed);
                            break;

                        case "defend":
                            blnValidAction = true;
                            if (CanBeat(battleBot.Weapon, computerWeapon))
                            {
                                battleBot.addScore(2);
                            }
                            else
                            {
                                battleBot.HPDown(2);
                            }
                            battleBot.FuelDown(intTimeElapsed);
                            break;

                        case "retreat":
                            blnValidAction = true;
                            if (rGen.Next(0, 4) == 0)
                            {
                                SpeakingConsole.WriteLine("Unfortunately, you couldn't escape in time");
                                battleBot.HPDown(7);
                            }
                            battleBot.FuelDown(3 * intTimeElapsed);
                            break;
                        case "absorb":
                            if (battleBot.Weapon == computerWeapon)
                            {
                                blnValidAction = true;
                                SpeakingConsole.WriteLine("You have succesfully absorbed the opponent's power");
                                battleBot.Refuel(10);
                                battleBot.Heal(10);
                            }
                            break;
                    }
                }
                Thread.Sleep(1000);
                SpeakingConsole.WriteLine("\nBot stats:\nName: " + battleBot.Name + "\nWeapon: " + battleBot.Weapon + "\nCondition Level: " + battleBot.HP + "\nFuel Level: " + battleBot.Fuel + "\nTurn Time: " + intTimeElapsed + "\nTotal Battle Time: " + intTimeSinceGameStart + "\nPoints: " + battleBot.Score + "\nHighest Score: " + battleBot.HighestScore);
                Thread.Sleep(1000);
                Battle(ref battleBot);
            }

            else
            {
                battleBot.UpdateHighScore(battleBot.Score);
                SpeakingConsole.WriteLine("Your bot has lost. Do you want to play again?");
                if (SpeakingConsole.ReadLine().Trim().ToLower()[0] == 'y')
                {
                    battleBot = PromptUserForBot();
                    Battle(ref battleBot);
                }

            }

                }
        private static bool CanBeat(string weapon, string otherWeapon)
        {
            switch (weapon)
            {
                case WEAPON_CIRCULAR_SAW:
                    if (otherWeapon == WEAPON_CLAW_CUTTER || otherWeapon == WEAPON_FLAME_THROWER)
                        return true;
                    break;
                case WEAPON_SLEDGE_HAMMER:
                    if (otherWeapon == WEAPON_SPINNNING_BLADE || otherWeapon == WEAPON_CIRCULAR_SAW)
                        return true;
                    break;
                case WEAPON_SPINNNING_BLADE:
                    if (otherWeapon == WEAPON_CIRCULAR_SAW || otherWeapon == WEAPON_FLAME_THROWER)
                        return true;
                    break;
                case WEAPON_CLAW_CUTTER:
                    if (otherWeapon == WEAPON_SLEDGE_HAMMER || otherWeapon == WEAPON_SPINNNING_BLADE)
                        return true;
                    break;
                case WEAPON_FLAME_THROWER:
                    if (otherWeapon == WEAPON_SLEDGE_HAMMER || otherWeapon == WEAPON_CLAW_CUTTER)
                        return true;
                    break;
            }
            return false;
        }
        private static bool IsValidWeapon(string weapon)
        {
            return Array.FindIndex(WEAPONS, s => weapon.Trim().ToLower() == s.Trim().ToLower()) != -1;
        }
        private static string GetValidWeaponName(string weapon)
        {
            return Array.Find(WEAPONS, s => weapon.Trim().ToLower() == s.Trim().ToLower());
        }
    }
        }
    


    

    
    


