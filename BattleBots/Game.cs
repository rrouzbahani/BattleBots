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
        public const string WEAPON_FLAME_THROWER = "Charmandar";
        public const string WEAPON_SLEDGE_HAMMER = "Geodude";
        public const string WEAPON_SPINNNING_BLADE = "Bulbasaur";

        public static string[] WEAPONS = new string[] { WEAPON_CIRCULAR_SAW, WEAPON_CLAW_CUTTER, WEAPON_FLAME_THROWER, WEAPON_SLEDGE_HAMMER, WEAPON_SPINNNING_BLADE };
        private static ConsoleKey[] KONAMI_CODE = new ConsoleKey[] { ConsoleKey.UpArrow, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.B, ConsoleKey.A };
        private System.Timers.Timer timer;
        private Random rGen = new Random();
        private int intTimeSinceGameStart;
        private int intBattleStartTime;
        private int intTimeElapsed;
        private bool blnIsBattleSoundPlaying = false;
        System.Media.SoundPlayer openingSound = new System.Media.SoundPlayer(Resource1.Pokemon_Open);
        System.Media.SoundPlayer battleSound = new System.Media.SoundPlayer(Resource1.Pokemon_Battle);
        System.Media.SoundPlayer MeetingOak = new System.Media.SoundPlayer(Resource1.Pokemon_MeetingOak);

        System.Media.SoundPlayer PikachuSFX = new System.Media.SoundPlayer(Resource1.Thunderbolt__1_);
        System.Media.SoundPlayer CharmandarSFX = new System.Media.SoundPlayer(Resource1.Flamethrower);
        System.Media.SoundPlayer BulbSFX = new System.Media.SoundPlayer(Resource1.Vine_Whip);
        System.Media.SoundPlayer SquirtleSFX = new System.Media.SoundPlayer(Resource1.Bubble_Beam_part_1);
        System.Media.SoundPlayer GeoSFX = new System.Media.SoundPlayer(Resource1.Rock_Tomb__1_);

        public static ConsoleColor[] WEAPON_COLORS = new ConsoleColor[] { ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.DarkRed, ConsoleColor.Gray, ConsoleColor.Green };
        public static string[] WEAPON_TYPES = new string[] { "Electric", "Water", "Flying", "Rock/Ground", "Grass" };

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
            Console.WriteLine("\n Welcome to Battle Bots, the theme of this game is Pokémon!");
            Console.WriteLine("\n You will choose a pokémon and are given its strengths, and its weaknesses.\n After choosing your pokémon you are set into a battle.\n Every pokémon has HP and Power Points, more commonly known as PP");
            Console.WriteLine("\n When you have ran out of either of those, youre pokémon faints..");
            Console.WriteLine("\n After the Music stops press Enter to see what Prof. Oak has in store for us....");
            Console.ReadLine();
            openingSound.Stop();
            MeetingOak.Play();
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine("Prof.Oak <> Hello there! Welcome to the world of pokémon!\nMy name is Oak! People call me the pokémon Prof!\nThis world is inhabited by creatures called pokémon!\nFor some people, pokémon are pets. Others use them for fights.\nMyself...I study pokémon as a profession.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("What is your name, younge trainer?");
            string strName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n ahhh, "+ strName + " was it? Do you see that ball on the table? It's called a Poké Ball.\nIt holds a Pokémon inside. You may have it! Go on, take it! Go ahead, it's yours!");
            Console.ForegroundColor = ConsoleColor.White;
            SpeakingConsole.WriteLine("\nPlease choose a Pokemon from the following:");

            foreach (string weapon in WEAPONS)
            {
                string[] beatableWeapons = Array.FindAll(WEAPONS, w => CanBeat(weapon, w));
                string[] unbeatableWeapons = Array.FindAll(WEAPONS, w => (!CanBeat(weapon, w)) && w != weapon);

                Console.ForegroundColor = GetColorForWeapon(weapon);
                SpeakingConsole.WriteLine("\n " + weapon + ": " + GetTypeForWeapon(weapon));
                SpeakingConsole.WriteLine("\n     Strengths: " + string.Join(" And ", beatableWeapons));
                SpeakingConsole.WriteLine("\n     Weekness: " + string.Join(" And ", unbeatableWeapons));
            }

            Console.ForegroundColor = ConsoleColor.White;


            string strWeapon;
            while (((strWeapon = SpeakingConsole.ReadLine()) == "" || !IsValidWeapon(strWeapon)) && strName != "")
            {
                SpeakingConsole.WriteLine("Please enter a valid weapon from above");
            }
            openingSound.Stop();
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
                Console.ForegroundColor = GetColorForWeapon(battleBot.Weapon);
                Console.WriteLine("███████████████████████████");
                SpeakingConsole.WriteLine("\n\t\t" + battleBot.Weapon + "           ");

                Console.ForegroundColor = ConsoleColor.White;
                SpeakingConsole.WriteLine("\n\t\t----- VS -----   ");

                Console.ForegroundColor = GetColorForWeapon(computerWeapon);
                SpeakingConsole.WriteLine("\n\t\t" + computerWeapon);    // Pokemon
                Console.WriteLine("███████████████████████████");

                Console.ForegroundColor = ConsoleColor.White;
                //SpeakingConsole.WriteLine("\nYou are being attacked by a " + computerWeapon + ". What do you do?");
                bool blnValidAction = false;
                char charReadKey = '\0';
                while (!blnValidAction)
                {
                    bool blnCheatCodeWorked = false;
                    SpeakingConsole.WriteLine("\nAttack, Defend, or Retreat");
                    for (int i = 0; i < KONAMI_CODE.Length; i++)
                    {
                        ConsoleKeyInfo key = Console.ReadKey();
                        charReadKey = key.KeyChar;
                        if (key.Key != KONAMI_CODE[i])
                        {
                            break;
                        }
                        if (i == KONAMI_CODE.Length - 1)
                        {
                            battleBot.addScore(20);
                            SpeakingConsole.WriteLine("\nYou have cheated, trainer!! But you will get 20 extra points because that's just how the world is (unfair)");
                            blnCheatCodeWorked = true;
                        }
                    }
                    if (blnCheatCodeWorked)
                        continue;

                    string strAction = SpeakingConsole.SpeakAndReturn(charReadKey + Console.ReadLine());
                    switch (strAction.Trim().ToLower())
                    {
                        case "attack":
                            blnValidAction = true;
                            if (CanBeat(battleBot.Weapon, computerWeapon))
                            {
                                if (IsCriticalTo(battleBot.Weapon, computerWeapon))
                                {
                                    battleBot.addScore(rGen.Next(6, 11));
                                    SpeakingConsole.WriteLine("You have critically destroyed your opponent!!");
                                }
                                else
                                {
                                    battleBot.addScore(5);
                                    SpeakingConsole.WriteLine("You have destroyed your opponent!!");
                                }
                            }
                            else
                            {
                                if (IsCriticalTo(battleBot.Weapon, computerWeapon))
                                {
                                    battleBot.HPDown(rGen.Next(6, 11));
                                    SpeakingConsole.WriteLine("You have tragically lost!!");
                                }
                                else
                                {
                                    battleBot.HPDown(5);
                                    SpeakingConsole.WriteLine("You have lost!!");
                                }
                            }
                            battleBot.FuelDown(2 * intTimeElapsed);
                            break;
                        case "defend":
                            blnValidAction = true;
                            if (CanBeat(battleBot.Weapon, computerWeapon))
                            {
                                battleBot.addScore(2);
                                SpeakingConsole.WriteLine("You have defended yourself like a noble man!!");
                            }
                            else
                            {
                                if (IsCriticalTo(battleBot.Weapon, computerWeapon))
                                {
                                    battleBot.HPDown(rGen.Next(3, 5));
                                    SpeakingConsole.WriteLine("Whoops, your shield has completely failed!!");
                                }
                                else
                                {
                                    battleBot.HPDown(2);
                                    SpeakingConsole.WriteLine("Whoops, your shield has failed!!");
                                }
                            }
                            battleBot.FuelDown(intTimeElapsed);
                            break;
                        case "retreat":
                            blnValidAction = true;
                            if (rGen.Next(0, 4) == 0)
                            {
                                SpeakingConsole.WriteLine("Unfortunately, you couldn't escape in time!!");
                                battleBot.HPDown(7);
                            }
                            else
                            {
                                SpeakingConsole.WriteLine("You have succesfully escaped from the battle like a coward!! No points for you!!");
                            }
                            battleBot.FuelDown(3 * intTimeElapsed);
                            break;
                        case "absorb":
                            if (battleBot.Weapon == computerWeapon)
                            {
                                blnValidAction = true;
                                SpeakingConsole.WriteLine("You have succesfully absorbed the opponent's power!! This tastes yummy OwO");
                                battleBot.Refuel(10);
                                battleBot.Heal(10);
                            }
                            break;
                    }

                }
                Thread.Sleep(1000);
                SpeakingConsole.WriteLine("\nBot stats:\nName: " + battleBot.Name + "\nWeapon: " + battleBot.Weapon + "\nHP: " + battleBot.HP + "\nPower Points Left: " + battleBot.Fuel + "\nTurn Time: " + intTimeElapsed + "\nTotal Battle Time: " + intTimeSinceGameStart + "\nPoints: " + battleBot.Score + "\nHighest Score: " + battleBot.HighestScore);
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
        private static bool IsCriticalTo(string weapon, string otherWeapon)
        {
            switch (weapon)
            {
                case WEAPON_CIRCULAR_SAW:
                    if (otherWeapon == WEAPON_FLAME_THROWER)
                        return true;
                    break;
                case WEAPON_CLAW_CUTTER:
                    if (otherWeapon == WEAPON_SPINNNING_BLADE)
                        return true;
                    break;
                case WEAPON_FLAME_THROWER:
                    if (otherWeapon == WEAPON_CLAW_CUTTER)
                        return true;
                    break;
                case WEAPON_SLEDGE_HAMMER:
                    if (otherWeapon == WEAPON_SPINNNING_BLADE)
                        return true;
                    break;
                case WEAPON_SPINNNING_BLADE:
                    if (otherWeapon == WEAPON_CIRCULAR_SAW)
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
        private static ConsoleColor GetColorForWeapon(string weapon)
        {
            return WEAPON_COLORS[Array.FindIndex(WEAPONS, s => weapon.Trim().ToLower() == s.Trim().ToLower())];
        }

        private static string GetTypeForWeapon(string weapon)
        {
            return WEAPON_TYPES[Array.FindIndex(WEAPONS, s => weapon.Trim().ToLower() == s.Trim().ToLower())];
        }
    }
        }
    


    

    
    


