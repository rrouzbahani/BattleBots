using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleBots
{
    class Game
    {
        Timer timer;
        public int TimeStartGame;
        public int TimeStartBattle;
        public int TimeElapsed;
        public void game()
        {
            timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeStartGame++;
            TimeElapsed = TimeStartGame - TimeStartBattle;
        }
        public void TheGame()
        {
            Random rGen = new Random();
            int rNum = 0; rNum = rGen.Next(1,6);
            switch (rNum)
            {
                case 1:
                    {

                        break;
                    }


            }

        }
    }
}
