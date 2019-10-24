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
        public void addScore()
        {
            Score++;
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
