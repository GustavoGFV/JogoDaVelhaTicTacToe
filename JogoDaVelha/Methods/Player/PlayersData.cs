using JogoDaVelha.Enum;
using JogoDaVelha.Interfaces.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelha.Methods
{
    public class PlayersData : IPlayers
    {
        public void PlayersDatas(string value)
        {           
           Player1 = 0;
           Player2 = 0;

            if (value == ChooseEnum.X.ToString())
            {
                Player1 = (int)ChooseEnum.X;
                Player2 = (int)ChooseEnum.O;
            }
            else
            {
                Player1 = (int)ChooseEnum.O;
                Player2 = (int)ChooseEnum.X;
            }
        }
        public int Player1 { get; set; }
        public int Player2 { get; set; }
    }
}
