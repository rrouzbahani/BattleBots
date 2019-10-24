using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBots
{
    class Player
    {
        private string name;
        private int score;
        private int highestscore;
        private string weapon;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Weapon
        {
            get
            {
                return weapon;
            }
            set
            {
                weapon = value;
            }
        }
        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }
        public int HighestScore
        {
            get
            {
                return highestscore;
            }
            set
            {
                highestscore = value;
            }
        }
        public Player()
        {
            Name = "Player";
            Weapon = "Sledge Hammer";
        }
        public Player(string name, string weapon)
        {
            Name = name;
            Weapon = weapon;
        }
        public void Scoring()
        {
            for(int i = 0; i < 1; i++)
            {
                HighestScore = Score;

            }
            if (Score > HighestScore)
                HighestScore = Score;
        }
    }
}
