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
        private int points;
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
        public int Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
            }
        }
        public Player()
        {
            Name = "Player";
        }
        public Player(string name)
        {
            Name = name;
        }
        public void addScore(int points)
        {
            Score += points;
        }
        public void UpdateHighScore(int newScore)
        {
            HighestScore = newScore > HighestScore ? newScore : HighestScore;
        }
    }
}
